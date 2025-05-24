<script setup lang="ts">
import { ref, computed, onMounted } from 'vue';
import GuestDetailModal from './modals/GuestDetailModal.vue';

// Define guest item interface
interface GuestItem {
  id: number;
  name: string;
  email: string;
  phone: string;
  roomPreference: string;
  registrationNumber: string;
  checkInDate: string;
  stay: string;
  discount: string;
  avatar?: string;
}

// Guests data from API
const guests = ref<GuestItem[]>([]);

// Fetch data from API
const fetchGuests = async () => {
  try {
    const response = await fetch('http://localhost:5250/api/KhachHang/guest');
    const data = await response.json();
    
    // Map API data to GuestItem
    guests.value = data.map((guest, index) => {
      const checkInDate = new Date(guest.ngayNhan);
      const checkOutDate = new Date(guest.ngayTra);
      const stayDuration = Math.ceil(
        (checkOutDate.getTime() - checkInDate.getTime()) / (1000 * 3600 * 24)
      );

      return {
        id: index + 1,
        name: guest.hoTen,
        email: guest.email,
        phone: guest.soDienThoai,
        roomPreference: guest.loaiPhong,
        registrationNumber: `KH${index + 1}`,
        checkInDate: checkInDate.toLocaleDateString('en-GB'), // Định dạng ngày
        stay: `${stayDuration} đêm`,
        discount: '$0', // Add logic for discount if available
      };
    });
  } catch (error) {
    console.error('Failed to fetch guests:', error);
  }
};

// Call fetchGuests on mounted
onMounted(fetchGuests);

// Filter option
const filterActive = ref('All');

// Method to set active filter
const setFilter = (filter: string) => {
  filterActive.value = filter;
};

// Computed property for filtered guests
const filteredGuests = computed(() => guests.value);

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

    <!-- Guests Table -->
    <div class="bg-white rounded-lg shadow overflow-hidden w-bar">
      <table class="min-w-full divide-y divide-gray-200 w-table">
        <thead class="bg-gray-50">
          <tr>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Name
            </th>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Email
            </th>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Phone
            </th>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Room Preference
            </th>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Check-In Date
            </th>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Stay
            </th>
            <th scope="col" class="relative px-6 py-3">
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
            <td class="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">
              {{ guest.name }}
            </td>
            <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
              {{ guest.email }}
            </td>
            <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
              {{ guest.phone }}
            </td>
            <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
              {{ guest.roomPreference }}
            </td>
            <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
              {{ guest.checkInDate }}
            </td>
            <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
              {{ guest.stay }}
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
