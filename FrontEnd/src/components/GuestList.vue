<script setup lang="ts">
// Guest reservation data - updated to match the image exactly
interface Guest {
  id: string;
  name: string;
  roomNumber: string;
  totalAmount: string;
  amountPaid: string;
  status: string;
}

const guests: Guest[] = [
  {
    id: '#5644',
    name: 'Alexander',
    roomNumber: 'A647',
    totalAmount: '$ 467',
    amountPaid: '$ 200',
    status: 'Checked-in'
  },
  {
    id: '#6112',
    name: 'Pegasus',
    roomNumber: 'A456',
    totalAmount: '$ 645',
    amountPaid: '$ 250',
    status: 'Not Checked-in'
  },
  {
    id: '#6141',
    name: 'Martin',
    roomNumber: 'A645',
    totalAmount: '$ 686',
    amountPaid: '$ 400',
    status: 'Not Checked-in'
  },
  {
    id: '#6535',
    name: 'Cecil',
    roomNumber: 'A684',
    totalAmount: '$ 8413',
    amountPaid: '$ 2500',
    status: 'Clean'
  },
  {
    id: '#6541',
    name: 'Luke',
    roomNumber: 'B464',
    totalAmount: '$ 841',
    amountPaid: '$ 400',
    status: 'Clean'
  },
  {
    id: '#9846',
    name: 'Yadrin',
    roomNumber: 'C648',
    totalAmount: '$ 684',
    amountPaid: '$ 300',
    status: 'Clean'
  },
  {
    id: '#4921',
    name: 'Kiand',
    roomNumber: 'D644',
    totalAmount: '$ 984',
    amountPaid: '$ 513',
    status: 'Pick up'
  },
  {
    id: '#4921',
    name: 'Kiand',
    roomNumber: 'D644',
    totalAmount: '$ 984',
    amountPaid: '$ 513',
    status: 'Pick up'
  },
  {
    id: '#9841',
    name: 'Turen',
    roomNumber: 'B641',
    totalAmount: '$ 984',
    amountPaid: '$ 600',
    status: 'Dirty'
  },
  {
    id: '#9841',
    name: 'Turen',
    roomNumber: 'B641',
    totalAmount: '$ 984',
    amountPaid: '$ 600',
    status: 'Dirty'
  }
];

// Pagination
const currentPage = 1;
const totalPages = 7;
const pages = Array.from({ length: totalPages }, (_, i) => i + 1);

// Function to get status class based on status
const getStatusClass = (status: string): string => {
  switch(status.toLowerCase()) {
    case 'checked-in':
      return 'bg-blue-100 text-blue-600';
    case 'not checked-in':
      return 'bg-red-100 text-red-600';
    case 'clean':
      return 'bg-green-100 text-green-600';
    case 'pick up':
      return 'bg-yellow-100 text-yellow-600';
    case 'dirty':
      return 'bg-red-100 text-red-600';
    default:
      return 'bg-gray-100 text-gray-600';
  }
};

// For pagination buttons
const isCurrentPage = (page: number): boolean => page === currentPage;
</script>

<template>
  <div class="py-4 px-6 bg-white flex-1 overflow-auto">
    <!-- Header -->
    <div class="mb-4">
      <h2 class="text-lg font-semibold text-gray-800">Guests</h2>
    </div>

    <!-- Action buttons -->
    <div class="flex space-x-4 mb-6">
      <button class="bg-blue-50 px-4 py-2 rounded-full text-blue-600 border border-blue-200">
        Check in
      </button>
      <button class="bg-gray-50 px-4 py-2 rounded-full text-gray-600 border border-gray-200">
        Check out
      </button>
    </div>

    <!-- Table header -->
    <div class="flex justify-end items-center mb-4">
      <div class="flex items-center space-x-3">
        <button class="bg-gray-100 px-3 py-2 rounded-md text-gray-700 flex items-center">
          <span class="mr-2">Filter</span>
          <svg xmlns="http://www.w3.org/2000/svg" class="h-4 w-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 4a1 1 0 011-1h16a1 1 0 011 1v2.586a1 1 0 01-.293.707l-6.414 6.414a1 1 0 00-.293.707V17l-4 4v-6.586a1 1 0 00-.293-.707L3.293 7.293A1 1 0 013 6.586V4z" />
          </svg>
        </button>

        <div class="relative">
          <div class="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none">
            <svg class="w-4 h-4 text-gray-400" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
            </svg>
          </div>
          <input
            type="search"
            class="w-64 pl-10 pr-4 py-2 rounded-md bg-gray-100 border-0 placeholder-gray-400 text-sm focus:ring-0 focus:outline-none"
            placeholder="Search by room number"
          >
        </div>
      </div>
    </div>

    <!-- Guest table -->
    <div class="bg-white rounded-md overflow-hidden border border-gray-200">
      <!-- Table header -->
      <div class="grid grid-cols-7 bg-gray-50 text-gray-600 text-sm font-medium">
        <div class="py-3 px-4">Reservation ID</div>
        <div class="py-3 px-4">Name</div>
        <div class="py-3 px-4">Room Number</div>
        <div class="py-3 px-4">Total amount</div>
        <div class="py-3 px-4">Amount paid</div>
        <div class="py-3 px-4">Status</div>
        <div class="py-3 px-4"></div>
      </div>

      <!-- Table rows -->
      <div v-for="guest in guests" :key="guest.id" class="grid grid-cols-7 border-t border-gray-200 hover:bg-gray-50">
        <div class="py-4 px-4 text-gray-700">{{ guest.id }}</div>
        <div class="py-4 px-4 text-gray-700">{{ guest.name }}</div>
        <div class="py-4 px-4 text-gray-700">{{ guest.roomNumber }}</div>
        <div class="py-4 px-4 text-gray-700">{{ guest.totalAmount }}</div>
        <div class="py-4 px-4 text-gray-700">{{ guest.amountPaid }}</div>
        <div class="py-4 px-4">
          <span :class="['py-1 px-3 rounded-full text-xs', getStatusClass(guest.status)]">
            {{ guest.status }}
          </span>
        </div>
        <div class="py-4 px-4 flex justify-end">
          <button class="text-gray-400 hover:text-gray-600">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" viewBox="0 0 20 20" fill="currentColor">
              <path d="M10 6a2 2 0 110-4 2 2 0 010 4zM10 12a2 2 0 110-4 2 2 0 010 4zM10 18a2 2 0 110-4 2 2 0 010 4z" />
            </svg>
          </button>
        </div>
      </div>
    </div>

    <!-- Pagination -->
    <div class="flex justify-between items-center mt-6">
      <button class="flex items-center px-4 py-2 text-gray-600 bg-white border border-gray-300 rounded-md">
        <svg xmlns="http://www.w3.org/2000/svg" class="h-4 w-4 mr-2" fill="none" viewBox="0 0 24 24" stroke="currentColor">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 19l-7-7 7-7" />
        </svg>
        Previous
      </button>

      <div class="flex space-x-2">
        <button
          v-for="page in pages"
          :key="page"
          :class="[
            'w-8 h-8 flex items-center justify-center rounded-md',
            isCurrentPage(page) ? 'bg-blue-600 text-white' : 'bg-white text-gray-600 border border-gray-300'
          ]"
        >
          {{ page }}
        </button>
      </div>

      <button class="flex items-center px-4 py-2 text-gray-600 bg-white border border-gray-300 rounded-md">
        Next
        <svg xmlns="http://www.w3.org/2000/svg" class="h-4 w-4 ml-2" fill="none" viewBox="0 0 24 24" stroke="currentColor">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7" />
        </svg>
      </button>
    </div>
  </div>
</template>
