<script setup lang="ts">
import { ref, computed } from 'vue';
import GuestDetailModal from './modals/GuestDetailModal.vue';

// Define guest item interface
interface GuestItem {
  id: number;
  name: string;
  email: string;
  phone: string;
  roomPreference: string;
  lastStay: string;
  status: string;
  // Additional fields for detail view
  registrationNumber: string; // Make required to match GuestDetail
  checkInDate: string; // Make required to match GuestDetail
  stay: string; // Make required to match GuestDetail
  discount: string; // Make required to match GuestDetail
  avatar?: string;
}

// Sample data
const guests = ref<GuestItem[]>([
  {
    id: 1,
    name: 'John Smith',
    email: 'john.smith@example.com',
    phone: '+1 (123) 456-7890',
    roomPreference: 'Single',
    lastStay: '12/03/2023',
    status: 'Repeat',
    registrationNumber: '24665',
    checkInDate: '18/12/23',
    stay: '4 nights',
    discount: '$ 100'
  },
  {
    id: 2,
    name: 'Emma Johnson',
    email: 'emma.j@example.com',
    phone: '+1 (234) 567-8901',
    roomPreference: 'Double',
    lastStay: '05/02/2023',
    status: 'New',
    registrationNumber: '25542',
    checkInDate: '20/12/23',
    stay: '2 nights',
    discount: '$ 0'
  },
  {
    id: 3,
    name: 'Michael Brown',
    email: 'michael.b@example.com',
    phone: '+1 (345) 678-9012',
    roomPreference: 'VIP',
    lastStay: '20/03/2023',
    status: 'VIP',
    registrationNumber: '26778',
    checkInDate: '15/12/23',
    stay: '7 nights',
    discount: '$ 250'
  },
  {
    id: 4,
    name: 'Olivia Davis',
    email: 'olivia.d@example.com',
    phone: '+1 (456) 789-0123',
    roomPreference: 'Double',
    lastStay: '08/01/2023',
    status: 'Repeat',
    registrationNumber: '27889',
    checkInDate: '22/12/23',
    stay: '3 nights',
    discount: '$ 75'
  },
]);

// Filter option
const filterActive = ref('All');

// Method to set active filter
const setFilter = (filter: string) => {
  filterActive.value = filter;
};

// Computed property for filtered guests
const filteredGuests = computed(() => {
  if (filterActive.value === 'All') {
    return guests.value;
  }
  return guests.value.filter(guest => guest.status === filterActive.value);
});

// Modal state
const isModalOpen = ref(false);
const selectedGuest = ref<GuestItem | null>(null);

// Function to open the modal with the selected guest
const openGuestDetails = (guest: GuestItem) => {
  selectedGuest.value = guest;
  isModalOpen.value = true;
};

// Function to close the modal
const closeModal = () => {
  isModalOpen.value = false;
  selectedGuest.value = null;
};
</script>

<template>
  <div class="guest-page p-6">
    <div class="flex justify-between items-center mb-6">
      <h1 class="text-xl font-medium text-gray-700">Guest</h1>
    </div>

    <!-- Filter and Add Guest Button -->
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
          @click="setFilter('VIP')"
          :class="[
            'py-2 px-4 rounded-full text-sm font-medium',
            filterActive === 'VIP' ? 'bg-blue-500 text-white' : 'bg-white text-gray-600 border'
          ]"
        >
          VIP
        </button>
        <button
          @click="setFilter('Repeat')"
          :class="[
            'py-2 px-4 rounded-full text-sm font-medium',
            filterActive === 'Repeat' ? 'bg-blue-500 text-white' : 'bg-white text-gray-600 border'
          ]"
        >
          Repeat
        </button>
      </div>
      <button class="bg-blue-500 text-white py-2 px-4 rounded-lg text-sm font-medium">
        Add guest
      </button>
    </div>

    <!-- Guests Table -->
    <div class="bg-white rounded-lg shadow overflow-hidden w-bar">
      <table class="min-w-full divide-y divide-gray-200 w-table">
        <thead class="bg-gray-50">
          <tr>
            <th scope="col" class="px-6 px-tablet-table py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Name
            </th>
            <th scope="col" class="px-6 px-tablet-table py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Email
            </th>
            <th scope="col" class="px-6 px-tablet-table py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Phone
            </th>
            <th scope="col" class="px-6 px-tablet-table py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Room Preference
            </th>
            <th scope="col" class="px-6 px-tablet-table py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Last Stay
            </th>
            <th scope="col" class="px-6 px-tablet-table py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Status
            </th>
            <th scope="col" class="relative px-6 px-tablet-table py-3">
              <span class="sr-only">Actions</span>
            </th>
          </tr>
        </thead>
        <tbody class="bg-white divide-y divide-gray-200">
          <tr
            v-for="guest in filteredGuests"
            :key="guest.id"
            @click="openGuestDetails(guest)"
            class="cursor-pointer hover:bg-gray-50"
          >
            <td class="px-6 px-tablet-table py-4 whitespace-nowrap text-sm font-medium text-gray-900">
              {{ guest.name }}
            </td>
            <td class="px-6 px-tablet-table py-4 whitespace-nowrap text-sm text-gray-500">
              {{ guest.email }}
            </td>
            <td class="px-6 px-tablet-table py-4 whitespace-nowrap text-sm text-gray-500">
              {{ guest.phone }}
            </td>
            <td class="px-6 px-tablet-table py-4 whitespace-nowrap text-sm text-gray-500">
              {{ guest.roomPreference }}
            </td>
            <td class="px-6 px-tablet-table py-4 whitespace-nowrap text-sm text-gray-500">
              {{ guest.lastStay }}
            </td>
            <td class="px-6 px-tablet-table py-4 whitespace-nowrap">
              <span
                :class="[
                  'px-2 inline-flex text-xs leading-5 font-semibold rounded-full',
                  guest.status === 'VIP' ? 'bg-purple-100 text-purple-800' :
                  guest.status === 'Repeat' ? 'bg-blue-100 text-blue-800' :
                  'bg-green-100 text-green-800'
                ]"
              >
                {{ guest.status }}
              </span>
            </td>
            <td class="px-6 px-tablet-table py-4 whitespace-nowrap text-right text-sm font-medium">
              <button type="button" class="text-gray-400 hover:text-gray-500">
                ⋮
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>

    <!-- Guest Detail Modal -->
    <GuestDetailModal
      :is-open="isModalOpen"
      :guest="selectedGuest"
      @close="closeModal"
    />
  </div>
</template>

<style scoped>
.guest-page {
  height: 100%;
  width: 100%;
  background-color: #ffffff;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
  border-radius: 1%;
  margin-top: 80px;
  padding: 20px;
}

@media (min-width: 740px) and (max-width: 1023px) {
  .guest-page {
    width: 89%;
  }

  .px-tablet-table {
    padding-left: 10px !important;
    padding-right: 0 !important;
  }

  .w-bar {
    width: 101%;
  }
}

@media (max-width: 739px) {
  .guest-page {
    padding: 0;
    padding-left: 10px;
    width: 97%;
  }

  .px-tablet-table {
    padding-right: 2rem !important;
  }
}

</style>
