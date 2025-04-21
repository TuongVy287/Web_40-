<script setup lang="ts">
import { ref, computed } from 'vue';

// Define rate item interface
interface RateItem {
  id: number;
  roomType: string;
  weekdayRate: string;
  weekendRate: string;
  holidayRate: string;
  seasonalRate: string;
  status: string;
}

// Sample data
const rates = ref<RateItem[]>([
  { id: 1, roomType: 'Single Room', weekdayRate: '$90', weekendRate: '$120', holidayRate: '$150', seasonalRate: '$130', status: 'Active' },
  { id: 2, roomType: 'Double Room', weekdayRate: '$120', weekendRate: '$160', holidayRate: '$200', seasonalRate: '$180', status: 'Active' },
  { id: 3, roomType: 'Triple Room', weekdayRate: '$150', weekendRate: '$200', holidayRate: '$250', seasonalRate: '$230', status: 'Active' },
  { id: 4, roomType: 'VIP Suite', weekdayRate: '$200', weekendRate: '$280', holidayRate: '$350', seasonalRate: '$320', status: 'Promotional' },
]);

// Filter option
const filterActive = ref('Standard');

// Method to set active filter
const setFilter = (filter: string) => {
  filterActive.value = filter;
};

// Map filter name to status for filtering
const getStatusByFilter = (filter: string): string => {
  if (filter === 'Standard') return 'Active';
  return filter;
};

// Computed property for filtered rates
const filteredRates = computed(() => {
  if (filterActive.value === 'All') {
    return rates.value;
  }
  return rates.value.filter(rate => rate.status === getStatusByFilter(filterActive.value));
});
</script>

<template>
  <div class="rate-page p-6">
    <div class="flex justify-between items-center mb-6">
      <h1 class="text-xl font-medium text-gray-700">Rate</h1>
    </div>

    <!-- Filter and Add Rate Button -->
    <div class="flex justify-between items-center mb-6 w-bar">
      <div class="flex space-x-2">
        <button
          @click="setFilter('Standard')"
          :class="[
            'py-2 px-4 rounded-full text-sm font-medium',
            filterActive === 'Standard' ? 'bg-blue-500 text-white' : 'bg-white text-gray-600 border'
          ]"
        >
          Standard
        </button>
        <button
          @click="setFilter('Promotional')"
          :class="[
            'py-2 px-4 rounded-full text-sm font-medium',
            filterActive === 'Promotional' ? 'bg-blue-500 text-white' : 'bg-white text-gray-600 border'
          ]"
        >
          Promotional
        </button>
        <button
          @click="setFilter('Seasonal')"
          :class="[
            'py-2 px-4 rounded-full text-sm font-medium',
            filterActive === 'Seasonal' ? 'bg-blue-500 text-white' : 'bg-white text-gray-600 border'
          ]"
        >
          Seasonal
        </button>
      </div>
      <button class="bg-blue-500 text-white py-2 px-4 rounded-lg text-sm font-medium">
        Add rate
      </button>
    </div>

    <!-- Rates Table -->
    <div class="bg-white rounded-lg shadow overflow-hidden w-bar">
      <table class="min-w-full divide-y divide-gray-200 w-table">
        <thead class="bg-gray-50">
          <tr>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Room Type
            </th>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Weekday Rate
            </th>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Weekend Rate
            </th>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Holiday Rate
            </th>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Seasonal Rate
            </th>
            <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
              Status
            </th>
            <th scope="col" class="relative px-6 py-3">
              <span class="sr-only">Actions</span>
            </th>
          </tr>
        </thead>
        <tbody class="bg-white divide-y divide-gray-200">
          <tr v-for="rate in filteredRates" :key="rate.id">
            <td class="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">
              {{ rate.roomType }}
            </td>
            <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
              {{ rate.weekdayRate }}
            </td>
            <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
              {{ rate.weekendRate }}
            </td>
            <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
              {{ rate.holidayRate }}
            </td>
            <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
              {{ rate.seasonalRate }}
            </td>
            <td class="px-6 py-4 whitespace-nowrap">
              <span
                :class="[
                  'px-2 inline-flex text-xs leading-5 font-semibold rounded-full',
                  rate.status === 'Active' ? 'bg-green-100 text-green-800' :
                  rate.status === 'Promotional' ? 'bg-blue-100 text-blue-800' :
                  'bg-yellow-100 text-yellow-800'
                ]"
              >
                {{ rate.status }}
              </span>
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
.rate-page {
  height: 100%;
  width: 100%;
  background-color: #F9FAFB;
}
</style>
