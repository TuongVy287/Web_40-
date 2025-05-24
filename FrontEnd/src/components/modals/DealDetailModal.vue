<script setup lang="ts">
import { defineProps, defineEmits, ref, onMounted } from 'vue';
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

const fetchInvoiceDetails = async (idHoaDon: number) => {
  try {
    const response = await axios.get(`http://localhost:5250/api/ChiTietHoaDon/${idHoaDon}`);
    props.invoice.services = response.data.map((service: any) => ({
      name: service.tenDichVu,
      price: service.giaDichVu,
      quantity: service.soLuong
    }));
  } catch (error) {
    console.error("Có lỗi xảy ra khi tải danh sách dịch vụ trong hóa đơn!", error);
  }
};

onMounted(() => {
  fetchServices();
  if (props.invoice) {
    fetchInvoiceTotalAmount(props.invoice.id).then(amount => {
      if (props.invoice) {
        props.invoice.totalAmount = amount;
      }
    });
  }
});

const closeModal = () => {
  emit('close');
};

const calculateTotalAmount = (invoice: InvoiceDetail): number => {
  return invoice.totalAmount;
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
      await axios.post('http://localhost:5250/api/ChiTietHoaDon/them', {
        IDHoaDon: invoiceId,
        IDDichVu: selectedService.idDichVu,
        SoLuong: service.quantity,
        DVT: 'Item'
      }).then(() => {
        service.price = selectedService.giaDichVu; // cập nhật giá dịch vụ
      });
    } catch (error) {
      console.error("Có lỗi xảy ra khi thêm dịch vụ vào hóa đơn!", error);
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
            <div class="flex flex-col space-y-2">
              <div v-for="(service, index) in invoice.services" :key="index" class="flex items-center">
                <select v-model="service.name" class="px-3 py-2 border border-gray-300 rounded-md flex-grow" style="width: 33%; margin-bottom: 10px;">
                  <option v-for="s in services" :key="s.idDichVu" :value="s.tenDichVu">{{ s.tenDichVu }}</option>
                </select>
                <input type="number" readonly class="px-3 py-2 border border-gray-300 rounded-md ml-2" style="width: 33%; margin-bottom: 10px;" :value="services.find(s => s.tenDichVu === service.name)?.giaDichVu" placeholder="Price" />
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
          <input type="number" :value="calculateTotalAmount(invoice)" readonly class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm" />
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
/* Add any additional styling here */
input {
  color: black;
}
select {
  color: black;
}
</style>