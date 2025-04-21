<script setup lang="ts">
import { defineProps, defineEmits } from 'vue';

interface DealDetail {
  id: number;
  name: string;
  reference: string;
  price: string;
  discount: string;
  roomType: string;
  roomFacility: string;
  startDate: string;
  endDate: string;
  reservations?: number; // Add as optional
  status?: string; // Add as optional
  tags?: string[];
}

const props = defineProps<{
  isOpen: boolean;
  deal: DealDetail | null;
}>();

const emit = defineEmits(['close', 'save']);

const closeModal = () => {
  emit('close');
};

const saveChanges = () => {
  // In a real app, you would validate and save the data here
  if (props.deal) {
    emit('save', props.deal);
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
      <div v-if="deal" class="flex flex-col">
        <!-- Title -->
        <h3 class="text-lg font-medium text-gray-900 mb-4">Deal Details</h3>

        <!-- Form Grid -->
        <div class="grid grid-cols-2 gap-x-6 gap-y-4">
          <!-- Deal name -->
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">Deal name</label>
            <input
              type="text"
              v-model="deal.name"
              class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm w-9"
            />
          </div>

          <!-- Reference number -->
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">Reference number</label>
            <input
              type="text"
              v-model="deal.reference"
              class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm w-9"
            />
          </div>

          <!-- Tags -->
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">Tags</label>
            <div class="relative">
              <select
                class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm appearance-none pr-10"
              >
                <option disabled selected>Select tags</option>
              </select>
              <div class="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 text-gray-700">
                <svg class="h-4 w-4" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor">
                  <path fill-rule="evenodd" d="M5.293 7.293a1 1 0 011.414 0L10 10.586l3.293-3.293a1 1 0 111.414 1.414l-4 4a1 1 0 01-1.414 0l-4-4a1 1 0 010-1.414z" clip-rule="evenodd" />
                </svg>
              </div>
            </div>
          </div>

          <!-- Price -->
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">Price</label>
            <input
              type="text"
              v-model="deal.price"
              placeholder="Enter the price of deal"
              class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm w-9"
            />
          </div>

          <!-- Room facility - full width text area -->
          <div class="col-span-2">
            <label class="block text-sm font-medium text-gray-700 mb-1">Room facility</label>
            <textarea
              v-model="deal.roomFacility"
              rows="4"
              placeholder="Enter a description..."
              class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm w-input"
            ></textarea>
          </div>

          <!-- Room type -->
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">Room type</label>
            <div class="relative">
              <select
                v-model="deal.roomType"
                class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm appearance-none pr-10"
              >
                <option disabled>Select room type</option>
                <option value="Single">Single</option>
                <option value="Double">Double</option>
                <option value="Triple">Triple</option>
                <option value="VIP">VIP</option>
              </select>
              <div class="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 text-gray-700">
                <svg class="h-4 w-4" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor">
                  <path fill-rule="evenodd" d="M5.293 7.293a1 1 0 011.414 0L10 10.586l3.293-3.293a1 1 0 111.414 1.414l-4 4a1 1 0 01-1.414 0l-4-4a1 1 0 010-1.414z" clip-rule="evenodd" />
                </svg>
              </div>
            </div>
          </div>

          <!-- Discount -->
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">Discount</label>
            <input
              type="text"
              v-model="deal.discount"
              placeholder="Enter discount value"
              class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm w-9"
            />
          </div>

          <!-- Start date -->
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">Start date</label>
            <input
              type="text"
              v-model="deal.startDate"
              placeholder="Start date"
              class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm w-9"
            />
          </div>

          <!-- End date -->
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">End date</label>
            <input
              type="text"
              v-model="deal.endDate"
              placeholder="End date"
              class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm w-9"
            />
          </div>
        </div>

        <!-- Action buttons -->
        <div class="flex justify-end space-x-3 mt-6">
          <button
            @click="closeModal"
            class="px-4 py-2 text-sm font-medium text-gray-700 bg-white border border-gray-300 rounded-md shadow-sm hover:bg-gray-50"
          >
            Cancel
          </button>
          <button
            @click="saveChanges"
            class="px-4 py-2 text-sm font-medium text-white bg-blue-600 border border-transparent rounded-md shadow-sm hover:bg-blue-700"
          >
            Save
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
/* Add any additional styling here */
</style>
