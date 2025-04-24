<!-- src/components/Dashboard.vue -->
<template>
    <div class="space-y-4">
      <Overview />
      <Rooms />
      <RoomStatus />
      <Occupacy />
    </div>
  </template>
  
  <script setup>
  import Overview from './Overview.vue'
  import Rooms from './H_Rooms.vue'
  import RoomStatus from './H_RoomStatus.vue'
  import Occupacy from './OccupancyChart.vue'
  </script>
  