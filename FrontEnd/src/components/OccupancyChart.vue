<script setup lang="ts">
import { ref, onMounted } from 'vue';
import axios from 'axios';

// Monthly occupancy percentages data
const monthlyData = ref<{ month: string; percentage: number }[]>([]);

// Yearly occupancy percentages data (initially empty)
const yearlyData = ref<{ year: number; percentage: number }[]>([]);

// A variable to decide which data to show
const isMonthly = ref(true);

// Function to fetch monthly data from the backend
const fetchMonthlyData = async () => {
  try {
    console.log("Fetching monthly data...");
    const response = await axios.get('http://localhost:5250/api/ThongKe/SoLuongNguoiTheoThang?nam=2025');
    console.log("Monthly raw data:", response.data);
    monthlyData.value = response.data.map(item => ({
      month: new Date(2025, item.month - 1).toLocaleString('default', { month: 'short' }),
      percentage: item.tyLeSuDungPhanTram // Chuyển thành %
    }));
    console.log("Processed monthly data:", monthlyData.value);
  } catch (error) {
    console.error("Error fetching monthly data:", error);
  }
};

// Function to fetch yearly data from the backend
const fetchYearlyData = async () => {
  try {
    console.log("Fetching yearly data...");
    const response = await axios.get('http://localhost:5250/api/ThongKe/SoLuongNguoiTheoThang?nam=t%E1%BA%A5t%20c%E1%BA%A3');
    console.log("Yearly raw data:", response.data);
    yearlyData.value = response.data.map(item => ({
      year: item.year,
      percentage: item.tyLeSuDungPhanTram // Chuyển thành %
    }));
    console.log("Processed yearly data:", yearlyData.value);
  } catch (error) {
    console.error("Error fetching yearly data:", error);
  }
};

// Fetch data on component mount
onMounted(() => {
  fetchMonthlyData();
  fetchYearlyData();
});

// Toggle the display mode
const toggleMode = () => {
  isMonthly.value = !isMonthly.value;
};
</script>

<template>
  <div class="bg-white p-6 rounded-md relative none">
    <div class="flex justify-between items-center mb-4">
      <h2 class="text-xl font-semibold text-gray-800">Occupancy Statistics</h2>

      <button @click="toggleMode" class="bg-gray-100 hover:bg-gray-200 px-3 py-1 rounded-md text-gray-700 text-sm flex items-center">
        <span>{{ isMonthly ? 'Doanh thu theo tháng' : 'Doanh thu theo năm' }}</span>
        <svg xmlns="http://www.w3.org/2000/svg" class="h-4 w-4 ml-1" fill="none" viewBox="0 0 24 24" stroke="currentColor">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 9l-7 7-7-7" />
        </svg>
      </button>
    </div>

    <div class="flex">
      <!-- Y-axis labels -->
      <div class="flex flex-col justify-between pr-2 text-xs text-gray-400 h-44">
        <div>100%</div>
        <div>75%</div>
        <div>50%</div>
        <div>25%</div>
        <div>0%</div>
      </div>

      <!-- Chart bars -->
      <div v-if="isMonthly" class="h-44 flex items-end space-x-4 flex-1">
        <div v-for="(data, index) in monthlyData" :key="index" class="flex flex-col items-center flex-1 w-full bg-blue-400 rounded-t-sm" :style="{ height: `${data.percentage}%`, maxWidth: '99px', position: 'relative' }">
          <!-- Percentage label -->
          <div class="percentage-label">{{ data.percentage == 0 ? undefined :  data.percentage }}</div>
          <!-- Month label -->
          <div class="text-xs text-gray-500 mt-2" style="font-weight:bold; position: absolute; bottom: -25px;">{{ data.month }}</div>
        </div>
      </div>

      <div v-else class="h-44 flex items-end space-x-4 flex-1">
        <div v-for="(data, index) in yearlyData" :key="index" class="flex flex-col items-center flex-1 w-full bg-blue-400 rounded-t-sm" :style="{ height: `${data.percentage}%` , maxWidth: '99px', position: 'relative' }">
          <!-- Year label -->
          <div class="percentage-label">{{ data.percentage == 0 ? '' :  data.percentage}}</div>
          <div class="text-xs text-gray-500 mt-2" style="font-weight:bold; position: absolute; bottom: -25px;">{{ data.year }}</div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
@media (max-width: 760px) {
  .none {
    display: none;
  }
}

.chart-column {
  display: flex;
  flex-direction: column;
  align-items: center;
  max-width: 99px;
  width: 100%;
  position: relative;
}

.chart-bar {
  background-color: #3b82f6;
  width: 100%;
  border-radius: 4px 4px 0 0;
  transition: height 0.3s ease;
  position: relative;
  display: flex;
  justify-content: center;
  align-items: flex-end;
  overflow: hidden;
}

.chart-label {
  font-weight: bold;
  color: #4b5563;
  margin-top: 4px;
}

.percentage-label {
  color: white;
  font-size: 12px;
  margin-bottom: 4px;
}

.percentage-label {
  color: #000;
  font-size: 12px;
  font-weight: 500;
  margin-bottom: 4px;
  text-align: center;
  position: absolute;
  bottom: 98%;
}
</style>