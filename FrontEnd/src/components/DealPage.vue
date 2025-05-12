<script setup lang="ts">
import { ref, computed } from 'vue';
// import InvoiceDetailModal from './modals/InvoiceDetailModal.vue'; // Sử dụng modal hóa đơn
import InvoiceDetailModal from './modals/InvoiceDetailModal.vue';
// Define service interface
interface Service {
  name: string;
  price: number;
  quantity: number; // Thêm trường số lượng
}

interface InvoiceItem {
  id: number;
  guestName: string;
  reference: string;
  roomType: string;
  price: number; // Giá phòng
  checkInDate: string;
  checkOutDate: string;
  totalAmount: number; // Sẽ được tính toán từ modal
  paymentStatus: string;
  customerCode?: string; // Mã khách hàng
  paymentMethod?: string; // Phương thức thanh toán
  services?: Service[]; // Danh sách dịch vụ
}

// Hàm tính tổng tiền
const calculateTotalAmount = (invoice: InvoiceItem): number => {
  const numberOfDays = Math.floor((new Date(invoice.checkOutDate).getTime() - new Date(invoice.checkInDate).getTime()) / (1000 * 3600 * 24));
  const servicesTotal = invoice.services?.reduce((sum, service) => sum + (service.price * service.quantity), 0) || 0;
  return (invoice.price * numberOfDays) + servicesTotal;
};

// Dữ liệu mẫu cho hóa đơn
const invoices = ref<InvoiceItem[]>([
  {
    id: 1,
    guestName: 'Nguyễn Văn A',
    reference: '#5644',
    roomType: 'VIP',
    price: 199,
    checkInDate: '2023-03-01',
    checkOutDate: '2023-03-21',
    totalAmount: 0,
    paymentStatus: 'Paid',
    customerCode: 'CUST001',
    paymentMethod: 'Credit Card',
    services: [
      { name: 'Room Service', price: 20, quantity: 1 },
      { name: 'Spa', price: 50, quantity: 2 },
    ]
  },
  {
    id: 2,
    guestName: 'Trần Thị B',
    reference: '#6112',
    roomType: 'Double',
    price: 249,
    checkInDate: '2023-03-10',
    checkOutDate: '2023-03-25',
    totalAmount: 0,
    paymentStatus: 'Unpaid',
    customerCode: 'CUST002',
    paymentMethod: 'Cash',
    services: [
      { name: 'Laundry', price: 10, quantity: 1 },
    ]
  },
]);

// Tính tổng tiền cho mỗi hóa đơn
invoices.value.forEach(invoice => {
  invoice.totalAmount = calculateTotalAmount(invoice);
});

// Trạng thái modal
const isModalOpen = ref(false);
const selectedInvoice = ref<InvoiceItem | null>(null);

// Biến tìm kiếm
const searchQuery = ref('');

// Tính toán danh sách hóa đơn đã lọc
const filteredInvoices = computed(() => {
  if (!searchQuery.value) {
    return invoices.value;
  }
  return invoices.value.filter(invoice =>
    invoice.guestName.toLowerCase().includes(searchQuery.value.toLowerCase())
  );
});

// Mở modal với hóa đơn đã chọn
const openInvoiceDetails = (invoice: InvoiceItem) => {
  selectedInvoice.value = { ...invoice }; // Tạo bản sao để tránh vấn đề phản ứng
  isModalOpen.value = true;
};

// Đóng modal
const closeModal = () => {
  isModalOpen.value = false;
  selectedInvoice.value = null;
};

// Xử lý lưu
const handleSave = (updatedInvoice: InvoiceItem) => {
  if (updatedInvoice && updatedInvoice.id) {
    const index = invoices.value.findIndex(i => i.id === updatedInvoice.id);
    if (index !== -1) {
      invoices.value[index] = { ...updatedInvoice, totalAmount: calculateTotalAmount(updatedInvoice) }; // Cập nhật totalAmount
    }
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

    <!-- Invoice Detail Modal -->
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