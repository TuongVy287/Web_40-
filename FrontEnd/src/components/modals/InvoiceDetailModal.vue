<script setup lang="ts">
import { defineProps, defineEmits, ref } from 'vue';

interface Service {
  name: string;
  price: number;
  quantity: number; // Số lượng dịch vụ
}

interface InvoiceDetail {
  id: number;
  guestName: string;
  roomType: string;
  price: number; // Giá phòng
  checkInDate: string;
  checkOutDate: string;
  totalAmount: number; 
  paymentStatus: string;
  customerCode?: string; 
  paymentMethod?: string; 
  services?: Service[]; // Danh sách dịch vụ
}

// Props and emits
const props = defineProps<{
  isOpen: boolean;
  invoice: InvoiceDetail | null;
}>();

const emit = defineEmits(['close', 'save']);

const closeModal = () => {
  emit('close');
};

// Tính tổng số tiền
const calculateTotalAmount = (invoice: InvoiceDetail): number => {
  const numberOfDays = Math.floor((new Date(invoice.checkOutDate).getTime() - new Date(invoice.checkInDate).getTime()) / (1000 * 3600 * 24));
  const servicesTotal = invoice.services?.reduce((sum, service) => sum + (service.price * service.quantity), 0) || 0; // Tính tổng theo số lượng
  return (invoice.price * numberOfDays) + servicesTotal;
};

const saveChanges = () => {
  if (props.invoice) {
    props.invoice.totalAmount = calculateTotalAmount(props.invoice); // Cập nhật tổng số tiền
    emit('save', props.invoice);
  }
  closeModal();
};
</script>

<template>
  <div v-if="isOpen" class="fixed inset-0 z-50 flex items-center justify-center">
    <!-- Backdrop -->
    <div class="fixed inset-0 bg-black bg-opacity-50" @click="closeModal"></div>

    <!-- Modal content -->
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
            <label class="block text-sm font-medium text-gray-700 mb-1">Customer Code</label>
            <input type="text" v-model="invoice.customerCode" class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm" />
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
              <div v-for="service in invoice.services" :key="service.name" class="flex items-center">
                <input type="text" class="px-3 py-2 border border-gray-300 rounded-md flex-grow" style="width: 33%; margin-bottom: 10px;" v-model="service.name" />
                <input type="number" class="px-3 py-2 border border-gray-300 rounded-md ml-2" style="width: 33%; margin-bottom: 10px;" v-model="service.price" placeholder="Price" />
                <input type="number" class="px-3 py-2 border border-gray-300 rounded-md ml-2" style="width: 33%; margin-bottom: 10px;" v-model="service.quantity" placeholder="Quantity" />
              </div>
            </div>
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
          <button @click="saveChanges" class="px-4 py-2 text-sm font-medium text-white bg-blue-600 border border-transparent rounded-md shadow-sm hover:bg-blue-700">Thanh toán</button>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
/* Add any additional styling here */
input{
  color: black;
}
select{
  color:black;
}
</style>