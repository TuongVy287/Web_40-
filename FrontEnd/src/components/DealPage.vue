<script setup lang="ts">
import { ref, computed, onMounted } from 'vue';
import axios from 'axios';
import InvoiceDetailModal from './modals/InvoiceDetailModal.vue';

// Service and Invoice interfaces
interface Service {
  name: string;
  price: number;
  quantity: number;
}

interface InvoiceItem {
  id: number;
  guestName: string;
  reference: string;
  roomType: string;
  price: number;
  checkInDate: string;
  checkOutDate: string;
  totalAmount: number;
  paymentStatus: string;
  customerCode?: string;
  paymentMethod?: string;
  services?: Service[];
}

// Invoice data
const invoices = ref<InvoiceItem[]>([]);

// Fetch invoices from API
const fetchInvoices = async () => {
  try {
    const response = await axios.get('http://localhost:5250/api/ThongTinDeal/all');
    invoices.value = response.data.map((item: any) => ({
      id: item.idHoaDon,
      guestName: item.hoTen,
      reference: `#${item.idDatPhong}`,
      roomType: item.loaiPhong,
      price: item.gia,
      checkInDate: item.ngayNhan.split('T')[0],
      checkOutDate: item.ngayTra.split('T')[0],
      totalAmount: item.tongTien,
      paymentStatus: 'Unknown',
      services: []
    }));
  } catch (error) {
    console.error('Failed to fetch invoices', error);
  }
};

// Effect to fetch data when component mounts
onMounted(fetchInvoices);

// Modal control
const isModalOpen = ref(false);
const selectedInvoice = ref<InvoiceItem | null>(null);

// Search and filter invoices
const searchQuery = ref('');
const filteredInvoices = computed(() => {
  if (!searchQuery.value) {
    return invoices.value;
  }
  return invoices.value.filter(invoice =>
    invoice.guestName.toLowerCase().includes(searchQuery.value.toLowerCase())
  );
});

// Open and close modal
const openInvoiceDetails = (invoice: InvoiceItem) => {
  selectedInvoice.value = { ...invoice };
  isModalOpen.value = true;
};

const closeModal = () => {
  isModalOpen.value = false;
  selectedInvoice.value = null;
};

const handleSave = async (updatedInvoice: InvoiceItem) => {
  if (updatedInvoice && updatedInvoice.id) {
    const index = invoices.value.findIndex(i => i.id === updatedInvoice.id);
    if (index !== -1) {
      invoices.value[index] = { ...updatedInvoice };
    }
    // Gọi lại API để tải dữ liệu cập nhật
    await fetchInvoices();
  }
  closeModal();
};
</script>

<template>
  <div class="invoice-page">
    <h1>Thanh toán</h1>
    <div class="rate-controls">
      <input
        type="text"
        v-model="searchQuery"
        placeholder="Tìm kiếm theo tên khách hàng"
        class="search-input"
      />
    </div>
    <div class="table-container">
      <table>
        <thead>
          <tr>
            <th>Mã</th>
            <th>Tên khách hàng</th>
            <th>Loại phòng</th>
            <th>Giá</th>
            <th>Ngày nhận phòng</th>
            <th>Ngày trả phòng</th>
            <th>Tổng số tiền</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="invoice in filteredInvoices" :key="invoice.id" @click="openInvoiceDetails(invoice)" class="cursor-pointer hover:bg-gray-50 transition-colors duration-150 ease-in-out">
            <td>{{ invoice.reference }}</td>
            <td>{{ invoice.guestName }}</td>
            <td>{{ invoice.roomType }}</td>
            <td>{{ invoice.price }}</td>
            <td>{{ invoice.checkInDate }}</td>
            <td>{{ invoice.checkOutDate }}</td>
            <td>{{ invoice.totalAmount }}</td>
            <td>
              <button type="button" class="text-gray-400 hover:text-gray-600 transition duration-150 ease-in-out">⋮</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>

    <InvoiceDetailModal :is-open="isModalOpen" :invoice="selectedInvoice" @close="closeModal" @save="handleSave" />
  </div>
</template>

<style scoped>
.invoice-page {
    margin-left: 50px;
    padding: 20px;
    background-color: #f9f9f9;
    border-radius: 1%;
    box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
    margin-top: 80px;
}

h1 {
    color: #f39c12;
    margin-bottom: 20px;
    font-size: 24px;
}

.search-input {
    padding: 12px;
    border: 1px solid #ddd;
    border-radius: 30px;
    width: 300px;
    font-size: 16px;
    transition: border-color 0.3s ease;
    position: relative;
    left: 75%;
}

.search-input:focus {
    border-color: #f39c12;
    outline: none;
}

.table-container {
    margin-top: 20px;
}

table {
    width: 100%;
    border-collapse: collapse;
}

th,
td {
    padding: 14px;
    text-align: center;
    font-size: 16px;
}

th {
    background-color: #f1f1f1;
    color: #333;
    border: 1px solid #ddd;
}

tr:hover {
    background-color: #e9e9e9;
}
</style>