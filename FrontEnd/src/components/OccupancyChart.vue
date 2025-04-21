<script setup lang="ts">
// Monthly occupancy percentages data
const monthlyData = [
  { month: 'May', percentage: 85 },
  { month: 'Jun', percentage: 70 },
  { month: 'Jul', percentage: 80 },
  { month: 'Aug', percentage: 45 },
  { month: 'Sep', percentage: 95 },
  { month: 'Oct', percentage: 85 },
  { month: 'Nov', percentage: 85 },
  { month: 'Dec', percentage: 85 },
  { month: 'Jan', percentage: 95 },
  { month: 'Feb', percentage: 95 },
];
</script>

<template>
  <div class="bg-white p-6 rounded-md relative none">
    <div class="flex justify-between items-center mb-4">
      <h2 class="text-xl font-semibold text-gray-800">Occupancy Statistics</h2>

      <button class="bg-gray-100 hover:bg-gray-200 px-3 py-1 rounded-md text-gray-700 text-sm flex items-center">
        <span>Monthly</span>
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
      <div class="h-44 flex items-end space-x-4 flex-1">
        <div v-for="(data, index) in monthlyData" :key="index" class="flex flex-col items-center flex-1 w-full bg-blue-400 rounded-t-sm"  :style="{ height: `${data.percentage}%` }">

          <!-- Month label -->
          <div class="text-xs text-gray-500 mt-2">{{ data.month }}</div>
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
/* Any additional styles */
</style>
