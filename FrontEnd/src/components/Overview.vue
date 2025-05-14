<script>
import axios from "axios";
// Overview data
export default {
  data() {
    return {
      overviewStats: [
        { title: "Today's Check-in", value: 23 },
        { title: "Today's Check-out", value: 13 },
        { title: "Total In hotel", value: 60 },
        { title: "Total Available room", value: 10 },
        { title: "Total Occupied room", value: 90 },
      ],
    };
  },
  methods: {
    // Fetch data from API if needed
    async fetchOverviewData() {
      try {
        const response = await axios.get("http://localhost:5250/api/ThongKe/homnay");
        const data = response.data;
        this.overviewStats = [
          { title: "Today's Check-in", value: data.ngayNhan },
          { title: "Today's Check-out", value: data.ngayTra },
          { title: "Total In hotel", value: data.khachHang ?? 0 },
          { title: "Total Available room", value: data.phongTrong },
          { title: "Total Occupied room", value: data.daDat }
        ];
      } catch (error) {
        console.error("Error fetching overview data:", error);
      }
    },
  },
  mounted() {
    // Fetch data when component is mounted
    this.fetchOverviewData();
  },
};
</script>

<template>
  <div class="bg-white p-6 rounded-md mb-6">
    <h2 class="text-xl font-semibold text-gray-800 mb-4">Overview</h2>

    <div class="grid overview-grid">
      <div v-for="(stat, index) in overviewStats" :key="index" class="stat-card bg-gray-50 p-4 rounded-md">
        <div class="text-sm text-gray-500">{{ stat.title }}</div>
        <div class="text-3xl font-semibold text-blue-600 mt-2">{{ stat.value }}</div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.overview-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(150px, 1fr));
  /* Responsive grid */
  gap: 1.5rem;
  /* Khoảng cách giữa các thẻ */
}

.stat-card {
  padding: 20px;
  transition: transform 0.2s;
  /* Hiệu ứng chuyển động */
}

.stat-card:hover {
  transform: scale(1.05);
  /* Hiệu ứng phóng to khi hover */
}

/* Kiểu dáng phản hồi cho màn hình nhỏ */
@media (max-width: 760px) {
  .overview-grid {
    grid-template-columns: 1fr;
    /* Xếp chồng các mục thành một cột */
  }

  .stat-card {
    font-size: 1.5rem;
    /* Tăng kích thước chữ */
    padding: 18px;
    /* Điều chỉnh padding */
  }
}
</style>
