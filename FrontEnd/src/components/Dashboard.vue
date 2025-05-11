<!-- src/components/Dashboard.vue -->
<template>
  <div class="space-y-4">
    <Overview />
    <Rooms />
    <RoomStatus />
    <Occupacy />
  </div>
</template>

<script>
import Overview from './Overview.vue'
import Rooms from './H_Rooms.vue'
import RoomStatus from './H_RoomStatus.vue'
import Occupacy from './OccupancyChart.vue'

export default {
  name: 'Dashboard',
  components: {
    Overview,
    Rooms,
    RoomStatus,
    Occupacy
  },
  data() {
    return {
      // Các biến dữ liệu khác nếu cần
    }
  },
  methods: {
    // Các phương thức khác nếu cần
  }
}

</script>
