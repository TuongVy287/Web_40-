<script setup lang="ts">
import { ref, onMounted } from 'vue';
import axios from 'axios';

// Reactive references for room data
const occupiedRooms = ref([]);
const availableRooms = ref([]);

// Function to fetch data from the API
const fetchData = async () => {
  try {
    const response = await axios.get('http://localhost:5250/api/Phong/thongke');
    const data = response.data;

    // Map the API data into the format needed for your display
    occupiedRooms.value = [
      { status: 'Đã đặt', count: data.trangThai.find(room => room.trangThaiPhong === 'Đã đặt')?.soLuong || 0 },
      ...data.daDat.map(room => ({
        status: room.loaiPhong, count: room.soLuong
      }))
    ];

    availableRooms.value = [
      { status: 'Trống', count: data.trangThai.find(room => room.trangThaiPhong === 'Trống')?.soLuong || 0 },
      ...data.trong.map(room => ({
        status: room.loaiPhong, count: room.soLuong
      }))
    ];
  } catch (error) {
    console.error('Error fetching data:', error);
  }
};

// Fetch data on component mount
onMounted(fetchData);
</script>

<template>
  <div class="bg-white p-6 rounded-md mb-6">
    <h2 class="text-xl font-semibold text-gray-800 mb-4">Room status</h2>

    <div class="grid grid-cols-2 gap-6">
      <!-- Occupied Rooms -->
      <div class="space-y-4">
        <div class="flex justify-between items-center border-b pb-3">
          <span class="font-medium text-gray-600">{{ occupiedRooms[0]?.status }}</span>
          <span class="text-gray-700 font-semibold">{{ occupiedRooms[0]?.count }}</span>
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
          <span class="font-medium text-gray-600">{{ availableRooms[0]?.status }}</span>
          <span class="text-gray-700 font-semibold">{{ availableRooms[0]?.count }}</span>
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