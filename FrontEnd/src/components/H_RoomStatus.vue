<script setup lang="ts">
// Room status data
const occupiedRooms = [
  { status: 'Occupied rooms', count: 104 },
  { status: 'Clean', count: 90 },
  { status: 'Dirty', count: 4 },
  { status: 'Inspected', count: 60 },
];

const availableRooms = [
  { status: 'Available rooms', count: 20 },
  { status: 'Clean', count: 30 },
  { status: 'Dirty', count: 19 },
  { status: 'Inspected', count: 30 },
];
</script>

<template>
  <div class="bg-white p-6 rounded-md mb-6">
    <h2 class="text-xl font-semibold text-gray-800 mb-4">Room status</h2>

    <div class="grid grid-cols-2 gap-6">
      <!-- Occupied Rooms -->
      <div class="space-y-4">
        <div class="flex justify-between items-center border-b pb-3">
          <span class="font-medium text-gray-600">{{ occupiedRooms[0].status }}</span>
          <span class="text-gray-700 font-semibold">{{ occupiedRooms[0].count }}</span>
        </div>

        <div v-for="(item, index) in occupiedRooms.slice(1)" :key="`occupied-${index}`"
             class="flex justify-between items-center py-2">
          <span class="text-gray-500">{{ item.status }}</span>
          <span class="text-gray-700">{{ item.count }}</span>
        </div>
      </div>

      <!-- Available Rooms -->
      <div class="space-y-4">
        <div class="flex justify-between items-center border-b pb-3">
          <span class="font-medium text-gray-600">{{ availableRooms[0].status }}</span>
          <span class="text-gray-700 font-semibold">{{ availableRooms[0].count }}</span>
        </div>

        <div v-for="(item, index) in availableRooms.slice(1)" :key="`available-${index}`"
             class="flex justify-between items-center py-2">
          <span class="text-gray-500">{{ item.status }}</span>
          <span class="text-gray-700">{{ item.count }}</span>
        </div>
      </div>
    </div>
  </div>
</template>
