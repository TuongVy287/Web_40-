<script setup lang="ts">
import { ref, computed } from 'vue';

// Define check-in item interface
interface CheckInItem {
  id: number;
  guestName: string;
  roomNumber: string;
  checkInDate: string;
  checkOutDate: string;
  status: string;
  payment: string;
}

// Sample data
const checkIns = ref<CheckInItem[]>([
  { id: 1, guestName: 'John Smith', roomNumber: '101', checkInDate: '01/04/2023', checkOutDate: '05/04/2023', status: 'Check-in', payment: 'Paid' },
  { id: 2, guestName: 'Mary Johnson', roomNumber: '205', checkInDate: '02/04/2023', checkOutDate: '08/04/2023', status: 'Checked-in', payment: 'Pending' },
  { id: 3, guestName: 'Robert Davis', roomNumber: '310', checkInDate: '03/04/2023', checkOutDate: '07/04/2023', status: 'Checked-in', payment: 'Paid' },
  { id: 4, guestName: 'Jennifer Lee', roomNumber: '402', checkInDate: '05/04/2023', checkOutDate: '12/04/2023', status: 'Reserved', payment: 'Deposit' },
]);

// Filter option
const filterActive = ref('All');

// Method to set active filter
const setFilter = (filter: string) => {
  filterActive.value = filter;
};

// Computed property for filtered check-ins
const filteredCheckIns = computed(() => {
  if (filterActive.value === 'All') {
    return checkIns.value;
  }
  return checkIns.value.filter(item => item.status.includes(filterActive.value));
});
</script>

<template>
  <div class="front-desk-page p-6" >
    <div class="flex justify-between items-center mb-6">
      <h1 class="text-xl font-medium text-gray-700">Front Desk</h1>
    </div>

    <!-- Filter and Check-in Button -->
    <div class="flex justify-between items-center mb-6 w-bar">
      <div class="flex space-x-2">
        <button
          @click="setFilter('All')"
          :class="[
            'py-2 px-4 rounded-full text-sm font-medium',
            filterActive === 'All' ? 'bg-blue-500 text-white' : 'bg-white text-gray-600 border'
          ]"
        >
          All
        </button>
        <button
          @click="setFilter('Check-in')"
          :class="[
            'py-2 px-4 rounded-full text-sm font-medium',
            filterActive === 'Check-in' ? 'bg-blue-500 text-white' : 'bg-white text-gray-600 border'
          ]"
        >
          Check-in
        </button>
        <button
          @click="setFilter('Check-out')"
          :class="[
            'py-2 px-4 rounded-full text-sm font-medium',
            filterActive === 'Check-out' ? 'bg-blue-500 text-white' : 'bg-white text-gray-600 border'
          ]"
        >
          Check-out
        </button>
      </div>
      <button class="bg-blue-500 text-white py-2 px-4 rounded-lg text-sm font-medium">
        New check-in
      </button>
    </div>

    <!-- Front Desk Table -->
    <div class="bg-white rounded-lg shadow overflow-hidden w-bar">
      <table class="min-w-full divide-y divide-gray-200 w-table">
        <thead class="bg-gray-50">
          <tr>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Guest Name
            </th>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Room Number
            </th>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Check-in Date
            </th>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Check-out Date
            </th>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Status
            </th>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Payment
            </th>
            <th scope="col" class="relative px-6 py-3">
              <span class="sr-only">Actions</span>
            </th>
          </tr>
        </thead>
        <tbody class="bg-white divide-y divide-gray-200">
          <tr v-for="checkIn in filteredCheckIns" :key="checkIn.id">
            <td class="px-6 py-4 whitespace-nowrap pd-tablet text-sm font-medium text-gray-900">
              {{ checkIn.guestName }}
            </td>
            <td class="px-6 py-4 whitespace-nowrap pd-tablet text-sm text-gray-500">
              {{ checkIn.roomNumber }}
            </td>
            <td class="px-6 py-4 whitespace-nowrap pd-tablet text-sm text-gray-500">
              {{ checkIn.checkInDate }}
            </td>
            <td class="px-6 py-4 whitespace-nowrap pd-tablet text-sm text-gray-500">
              {{ checkIn.checkOutDate }}
            </td>
            <td class="px-6 py-4 whitespace-nowrap pd-tablet">
              <span
                :class="[
                  'px-2 inline-flex text-xs leading-5 font-semibold rounded-full',
                  checkIn.status === 'Check-in' ? 'bg-yellow-100 text-yellow-800' :
                  checkIn.status === 'Checked-in' ? 'bg-green-100 text-green-800' :
                  'bg-blue-100 text-blue-800'
                ]"
              >
                {{ checkIn.status }}
              </span>
            </td>
            <td class="px-6 py-4 whitespace-nowrap">
              <span
                :class="[
                  'px-2 inline-flex text-xs leading-5 font-semibold rounded-full',
                  checkIn.payment === 'Paid' ? 'bg-green-100 text-green-800' :
                  checkIn.payment === 'Pending' ? 'bg-yellow-100 text-yellow-800' :
                  'bg-blue-100 text-blue-800'
                ]"
              >
                {{ checkIn.payment }}
              </span>
            </td>
            <td class="px-6 py-4 whitespace-nowrap text-right text-sm font-medium">
              <button type="button" class="text-gray-400 hover:text-gray-500">
                ⋮
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<style scoped>
.front-desk-page {
  height: 100%;
  width: 100%;
  background-color: #F9FAFB;
}

@media (min-width: 740px) and (max-width: 1023px) {
  .front-desk-page {
    width: 94%;
  }
}
</style>
