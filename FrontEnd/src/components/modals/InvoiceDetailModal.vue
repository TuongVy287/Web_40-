<script setup lang="ts">
import { defineProps, defineEmits, ref, onMounted, watch } from 'vue';
import axios from 'axios';

// Các interface không thay đổi
interface Service {
  idDichVu: number;
  tenDichVu: string;
  giaDichVu: number;
  moTa: string;
  xoa: boolean;
}

interface ServiceSelection {
  name: string;
  price: number;
  quantity: number;
}

interface InvoiceDetail {
  id: number;
  guestName: string;
  roomType: string;
  price: number;
  checkInDate: string;
  checkOutDate: string;
  totalAmount: number;
  paymentStatus: string;
  customerCode?: string;
  paymentMethod?: string;
  services?: ServiceSelection[];
}

const props = defineProps<{
  isOpen: boolean;
  invoice: InvoiceDetail | null;
}>();

const emit = defineEmits(['close', 'save']);

const services = ref<Service[]>([]);

const fetchServices = async () => {
  try {
    const response = await axios.get('http://localhost:5250/api/DichVu');
    services.value = response.data;
  } catch (error) {
    console.error("Có lỗi xảy ra khi tải danh sách dịch vụ!", error);
  }
};

const fetchInvoiceTotalAmount = async (idHoaDon: number) => {
  try {
    const response = await axios.get('http://localhost:5250/api/ThongTinDeal/all');
    const invoiceDeal = response.data.find((deal: any) => deal.idHoaDon === idHoaDon);
    return invoiceDeal ? invoiceDeal.tongTien : 0;
  } catch (error) {
    console.error("Có lỗi xảy ra khi lấy tổng tiền hóa đơn!", error);
    return 0;
  }
};

const fetchInvoiceServices = async (idHoaDon: number) => {
  try {
    const response = await axios.get(`http://localhost:5250/api/ChiTietHoaDon/${idHoaDon}`);
    console.log("Dữ liệu từ API:", response.data);
    return response.data;
  } catch (error) {
    console.error("Có lỗi xảy ra khi lấy dịch vụ của hóa đơn!", error);
    return [];
  }
};

const groupServices = (servicesData: any[]) => {
  const groupedServices = servicesData.reduce((acc: any, current: any) => {
    const existing = acc.find((item: any) => item.tenDichVu === current.tenDichVu);
    if (existing) {
      existing.soLuong += current.soLuong;
    } else {
      acc.push({ ...current });
    }
    return acc;
  }, []);
  return groupedServices;
};

onMounted(() => {
  fetchServices();
  if (props.invoice) {
    fetchInvoiceServices(props.invoice.id).then((servicesData) => {
      if (props.invoice) {
        const groupedServices = groupServices(servicesData);
        props.invoice.services = groupedServices.map((service: any) => ({
          name: service.tenDichVu,
          price: service.giaDichVu,
          quantity: service.soLuong
        }));
      }
    });

    fetchInvoiceTotalAmount(props.invoice.id).then((amount) => {
      if (props.invoice) {
        props.invoice.totalAmount = amount;
      }
    });
  }
});

watch(() => props.invoice, (newInvoice) => {
  if (newInvoice) {
    fetchInvoiceServices(newInvoice.id).then((servicesData) => {
      if (newInvoice) {
        const groupedServices = groupServices(servicesData);
        newInvoice.services = groupedServices.map((service: any) => ({
          name: service.tenDichVu,
          price: service.giaDichVu,
          quantity: service.soLuong
        }));
      }
    });

    fetchInvoiceTotalAmount(newInvoice.id).then((amount) => {
      if (newInvoice) {
        newInvoice.totalAmount = amount;
      }
    });
  }
}, { immediate: true });

const closeModal = () => {
  emit('close');
};

const saveChanges = async () => {
  if (props.invoice) {
    for (const service of props.invoice.services || []) {
      await addServiceToInvoice(props.invoice.id, service);
    }
    emit('save', props.invoice);
  }
  closeModal();
};

const addService = () => {
  if (props.invoice) {
    if (!props.invoice.services) {
      props.invoice.services = [];
    }
    props.invoice.services.push({
      name: '',
      price: 0,
      quantity: 0
    });
  }
};

const addServiceToInvoice = async (invoiceId: number, service: ServiceSelection) => {
  const selectedService = services.value.find(s => s.tenDichVu === service.name);
  if (selectedService) {
    try {
      const payload = {
        IDHoaDon: invoiceId,
        IDDichVu: selectedService.idDichVu,
        TenDichVu: selectedService.tenDichVu, // Thêm trường này
        SoLuong: service.quantity,
        DVT: 'Kg'
      };

      console.log("Payload:", payload);

      await axios.post('http://localhost:5250/api/ChiTietHoaDon/them', payload);
    } catch (error) {
      if (error.response) {
        console.error("Lỗi:", error.response.data.errors);
      } else {
        console.error("Có lỗi xảy ra khi thêm dịch vụ vào hóa đơn!", error.message);
      }
    }
  }
};
</script>

<template>
  <div v-if="isOpen" class="fixed inset-0 z-50 flex items-center justify-center">
    <div class="fixed inset-0 bg-black bg-opacity-50" @click="closeModal"></div>

    <div class="relative bg-white rounded-lg w-full max-w-3xl mx-auto p-6 shadow-lg">
      <div v-if="invoice" class="flex flex-col">
        <h3 class="text-lg font-medium text-gray-900 mb-4">Invoice Details</h3>
        
        <div class="grid grid-cols-2 gap-x-6 gap-y-4">
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">Guest Name</label>
            <input type="text" v-model="invoice.guestName" class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm" />
          </div>

          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">Room Type</label>
            <input type="text" v-model="invoice.roomType" class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm" />
          </div>

          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">Price</label>
            <input type="number" v-model="invoice.price" class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm" />
          </div>

          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">Check-In Date</label>
            <input type="text" v-model="invoice.checkInDate" class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm" />
          </div>

          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">Check-Out Date</label>
            <input type="text" v-model="invoice.checkOutDate" class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm" />
          </div>

          <div class="col-span-2">
            <label class="block text-sm font-medium text-gray-700 mb-1">Services</label>
            <div class="services-container flex flex-col space-y-2">
              <div v-for="(service, index) in invoice.services" :key="index" class="flex items-center">
                <select v-model="service.name" class="px-3 py-2 border border-gray-300 rounded-md flex-grow" style="width: 33%; margin-bottom: 10px;">
                  <option v-for="s in services" :key="s.idDichVu" :value="s.tenDichVu">{{ s.tenDichVu }}</option>
                </select>
                <input type="number" readonly class="px-3 py-2 border border-gray-300 rounded-md ml-2" style="width: 33%; margin-bottom: 10px;" :value="service.price" placeholder="Price" />
                <input type="number" class="px-3 py-2 border border-gray-300 rounded-md ml-2" style="width: 33%; margin-bottom: 10px;" v-model="service.quantity" placeholder="Quantity" />
              </div>
            </div>
            <button @click="addService" class="mt-2 flex items-center justify-center w-8 h-8 text-white bg-green-600 border border-transparent rounded-full shadow-sm hover:bg-green-700">
              <span class="text-xl" style="color: #000 !important;">+</span>
            </button>
          </div>
        </div>

        <div>
          <label class="block text-sm font-medium text-gray-700 mb-1">Total Amount</label>
          <input type="number" v-model="invoice.totalAmount" readonly class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm" />
        </div>

        <div>
          <label class="block text-sm font-medium text-gray-700 mb-1">Payment Status</label>
          <select v-model="invoice.paymentStatus" class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm">
            <option value="Paid">Paid</option>
            <option value="Unpaid">Unpaid</option>
            <option value="Pending">Pending</option>
          </select>
        </div>

        <div>
          <label class="block text-sm font-medium text-gray-700 mb-1">Payment Method</label>
          <select v-model="invoice.paymentMethod" class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm">
            <option value="Credit Card">Credit Card</option>
            <option value="Cash">Cash</option>
            <option value="Bank Transfer">Bank Transfer</option>
            <option value="PayPal">PayPal</option>
          </select>
        </div>

        <div class="flex justify-end space-x-3 mt-6">
          <button @click="closeModal" class="px-4 py-2 text-sm font-medium text-gray-700 bg-white border border-gray-300 rounded-md shadow-sm hover:bg-gray-50">Hủy</button>
          <button @click="saveChanges" class="px-4 py-2 text-sm font-medium text-white bg-blue-600 border border-transparent rounded-md shadow-sm hover:bg-blue-700">Cập nhật</button>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
input {
  color: black;
}
select {
  color: black;
}
.services-container {
  max-height: 300px; /* Đặt chiều cao tối đa */
  overflow-y: auto;  /* Thêm thanh cuộn dọc */
}
</style>