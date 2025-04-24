<script setup lang="ts">
import { ref, computed } from 'vue';

// Define room item interface
interface RoomItem {
  id: number;
  number: string;
  type: string;
  floor: string;
  status: string;
  price: string;
  lastCleaned: string;
}

// Sample data
const rooms = ref<RoomItem[]>([
  { id: 1, number: '101', type: 'Single', floor: '1', status: 'Occupied', price: '$120', lastCleaned: 'Today' },
  { id: 2, number: '102', type: 'Double', floor: '1', status: 'Available', price: '$150', lastCleaned: 'Yesterday' },
  { id: 3, number: '201', type: 'VIP Suite', floor: '2', status: 'Reserved', price: '$250', lastCleaned: 'Today' },
  { id: 4, number: '202', type: 'Triple', floor: '2', status: 'Cleaning', price: '$180', lastCleaned: 'In Progress' },
]);

// Filter option
const filterActive = ref('All Rooms');

// Method to set active filter
const setFilter = (filter: string) => {
  filterActive.value = filter;
};

// Computed property for filtered rooms
const filteredRooms = computed(() => {
  if (filterActive.value === 'All Rooms') {
    return rooms.value;
  }
  // For 'Available' and 'Occupied' filters
  return rooms.value.filter(room => room.status === filterActive.value);
});
</script>

<template>
  <div class="room-page p-6">
    <div class="flex justify-between items-center mb-6">
      <h1 class="text-xl font-medium text-gray-700">Rooms</h1>
    </div>

    <!-- Filter and Add Room Button -->
    <div class="flex justify-between items-center mb-6 w-bar">
      <div class="flex space-x-2">
        <button
          @click="setFilter('All Rooms')"
          :class="[
            'py-2 px-4 rounded-full text-sm font-medium',
            filterActive === 'All Rooms' ? 'bg-blue-500 text-white' : 'bg-white text-gray-600 border'
          ]"
        >
          All Rooms
        </button>
        <button
          @click="setFilter('Available')"
          :class="[
            'py-2 px-4 rounded-full text-sm font-medium',
            filterActive === 'Available' ? 'bg-blue-500 text-white' : 'bg-white text-gray-600 border'
          ]"
        >
          Available
        </button>
        <button
          @click="setFilter('Occupied')"
          :class="[
            'py-2 px-4 rounded-full text-sm font-medium',
            filterActive === 'Occupied' ? 'bg-blue-500 text-white' : 'bg-white text-gray-600 border'
          ]"
        >
          Occupied
        </button>
      </div>
      <button class="bg-blue-500 text-white py-2 px-4 rounded-lg text-sm font-medium">
        Add room
      </button>
    </div>

    <!-- Rooms Table -->
    <div class="bg-white rounded-lg shadow overflow-hidden w-bar">
      <table class="min-w-full divide-y divide-gray-200 w-table">
        <thead class="bg-gray-50">
          <tr>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Room Number
            </th>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Type
            </th>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Floor
            </th>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Status
            </th>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Price
            </th>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Last Cleaned
            </th>
            <th scope="col" class="relative px-6 py-3">
              <span class="sr-only">Actions</span>
            </th>
          </tr>
        </thead>
        <tbody class="bg-white divide-y divide-gray-200">
          <tr v-for="room in filteredRooms" :key="room.id">
            <td class="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">
              {{ room.number }}
            </td>
            <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
              {{ room.type }}
            </td>
            <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
              {{ room.floor }}
            </td>
            <td class="px-6 py-4 whitespace-nowrap">
              <span
                :class="[
                  'px-2 inline-flex text-xs leading-5 font-semibold rounded-full',
                  room.status === 'Available' ? 'bg-green-100 text-green-800' :
                  room.status === 'Occupied' ? 'bg-red-100 text-red-800' :
                  room.status === 'Reserved' ? 'bg-yellow-100 text-yellow-800' :
                  'bg-blue-100 text-blue-800'
                ]"
              >
                {{ room.status }}
              </span>
            </td>
            <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
              {{ room.price }}
            </td>
            <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
              {{ room.lastCleaned }}
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
.room-page {
  height: 100%;
  width: 100%;
  background-color: #F9FAFB;
}
</style>
