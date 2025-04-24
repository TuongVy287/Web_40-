<script setup lang="ts">
import { defineProps, defineEmits } from 'vue';

interface GuestDetail {
  id: number;
  name: string;
  registrationNumber: string;
  checkInDate: string;
  roomType?: string; // Make roomType optional
  roomPreference?: string; // Add roomPreference as an optional field
  stay: string;
  discount: string;
  avatar?: string;
}

defineProps<{
  isOpen: boolean;
  guest: GuestDetail | null;
}>();

const emit = defineEmits(['close']);

const closeModal = () => {
  emit('close');
};
</script>

<template>
  <div v-if="isOpen" class="fixed inset-0 z-50 flex items-center justify-center">
    <!-- Backdrop -->
    <div class="fixed inset-0 bg-black bg-opacity-50" @click="closeModal"></div>

    <!-- Modal content -->
    <div class="relative bg-white rounded-lg w-full max-w-md mx-auto p-6 shadow-lg">
      <div v-if="guest" class="flex flex-col space-y-4">
        <!-- Guest Avatar -->
        <div class="flex justify-center mb-2">
          <div class="w-20 h-20 rounded-full overflow-hidden">
            <img
              :src="guest.avatar || 'https://randomuser.me/api/portraits/men/32.jpg'"
              alt="Guest avatar"
              class="w-full h-full object-cover"
            />
          </div>
        </div>

        <!-- Guest Details -->
        <div class="space-y-4">
          <!-- Guest name -->
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">Guest name</label>
            <input
              type="text"
              :value="guest.name"
              readonly
              class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm text-gray-700 w-9"
            />
          </div>

          <!-- Registration number -->
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">Registration number</label>
            <input
              type="text"
              :value="guest.registrationNumber"
              readonly
              class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm text-gray-700 w-9"
            />
          </div>

          <!-- Check in date -->
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">Check in date</label>
            <input
              type="text"
              :value="guest.checkInDate"
              readonly
              class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm text-gray-700 w-9"
            />
          </div>

          <!-- Room type -->
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">Room type</label>
            <input
              type="text"
              :value="guest.roomType || guest.roomPreference"
              readonly
              class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm text-gray-700 w-9"
            />
          </div>

          <!-- Stay -->
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">Stay</label>
            <input
              type="text"
              :value="guest.stay"
              readonly
              class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm text-gray-700 w-9"
            />
          </div>

          <!-- Discount -->
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">Discount</label>
            <input
              type="text"
              :value="guest.discount"
              readonly
              class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm text-gray-700 w-9"
            />
          </div>
        </div>

        <!-- Action buttons -->
        <div class="flex justify-end space-x-3 mt-4">
          <button
            @click="closeModal"
            class="px-4 py-2 text-sm font-medium text-gray-700 bg-white border border-gray-300 rounded-md shadow-sm hover:bg-gray-50"
          >
            Close
          </button>
          <button
            class="px-4 py-2 text-sm font-medium text-white bg-blue-600 border border-transparent rounded-md shadow-sm hover:bg-blue-700"
          >
            Edit
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
/* Add any additional styling here */
</style>
