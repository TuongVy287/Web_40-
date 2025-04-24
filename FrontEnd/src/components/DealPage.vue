<script setup lang="ts">
import { ref, computed } from 'vue';
import DealDetailModal from './modals/DealDetailModal.vue';

// Define deal item interface
interface DealItem {
  id: number;
  reference: string;
  name: string;
  reservations: number;
  endDate: string;
  roomType: string;
  status: string;
  // Additional fields for detail modal - make required to match DealDetail
  price: string;
  discount: string;
  roomFacility: string;
  startDate: string;
  tags?: string[];
}

// Sample data matching the image
const deals = ref<DealItem[]>([
  {
    id: 1,
    reference: '#5644',
    name: 'Family deal',
    reservations: 10,
    endDate: '21/3/23',
    roomType: 'VIP',
    status: 'Ongoing',
    price: '$199',
    discount: '15%',
    roomFacility: 'Free WiFi, Air conditioning, TV, Mini-bar, Sea view',
    startDate: '01/3/23',
    tags: ['Family', 'Summer']
  },
  {
    id: 2,
    reference: '#6112',
    name: 'Christmas deal',
    reservations: 12,
    endDate: '25/3/23',
    roomType: 'Single, Double',
    status: 'Full',
    price: '$249',
    discount: '10%',
    roomFacility: 'Free WiFi, Air conditioning, TV, Balcony',
    startDate: '10/3/23',
    tags: ['Holiday', 'Winter']
  },
  {
    id: 3,
    reference: '#6141',
    name: 'Family deal',
    reservations: 15,
    endDate: '-',
    roomType: 'Triple',
    status: 'Inactive',
    price: '$279',
    discount: '20%',
    roomFacility: 'Free WiFi, Air conditioning, TV, Swimming pool access',
    startDate: '05/3/23',
    tags: ['Family', 'Spring']
  },
  {
    id: 4,
    reference: '#5535',
    name: 'Black Friday',
    reservations: 10,
    endDate: '1/5/23',
    roomType: 'VIP',
    status: 'New',
    price: '$349',
    discount: '30%',
    roomFacility: 'Free WiFi, Air conditioning, TV, Mini-bar, City view, Spa access',
    startDate: '15/4/23',
    tags: ['Promotion', 'Special']
  },
]);

// Filter options
const filterActive = ref('Ongoing');

// Methods to handle filter change
const setFilter = (filter: string) => {
  filterActive.value = filter;
};

// Computed property for filtered deals
const filteredDeals = computed(() => {
  if (filterActive.value === 'All') {
    return deals.value;
  } else if (filterActive.value === 'Ongoing') {
    return deals.value.filter(deal => deal.status === 'Ongoing' || deal.status === 'New');
  } else if (filterActive.value === 'Finished') {
    return deals.value.filter(deal => deal.status === 'Full' || deal.status === 'Inactive');
  }
  return deals.value;
});

// Modal state
const isModalOpen = ref(false);
const selectedDeal = ref<DealItem | null>(null);

// Function to open the modal with the selected deal
const openDealDetails = (deal: DealItem) => {
  selectedDeal.value = { ...deal }; // Create a copy to avoid reactivity issues
  isModalOpen.value = true;
};

// Function to close the modal
const closeModal = () => {
  isModalOpen.value = false;
  selectedDeal.value = null;
};

// Function to handle save
const handleSave = (updatedDeal: DealItem) => {
  if (updatedDeal && updatedDeal.id) {
    // Find and update the deal in the deals array
    const index = deals.value.findIndex(d => d.id === updatedDeal.id);
    if (index !== -1) {
      deals.value[index] = { ...updatedDeal };
    }
  }
  closeModal();
};
</script>

<template>
  <div class="deal-page p-6 overflow-x-auto">
    <div class="max-w-7xl mx-auto mr-tablet">
      <div class="flex justify-between items-center mb-6">
        <h1 class="text-xl font-medium text-gray-700">Deal</h1>
      </div>

      <!-- Filter and Add Deal Button -->
      <div class="flex flex-col sm:flex-row sm:justify-between sm:items-center mb-6 gap-4 w-bar">
        <div class="flex flex-wrap gap-2">
          <button @click="setFilter('Ongoing')" :class="[
            'py-2 px-4 rounded-full text-sm font-medium',
            filterActive === 'Ongoing' ? 'bg-blue-500 text-white' : 'bg-white text-gray-600 border'
          ]">
            Ongoing
          </button>
          <button @click="setFilter('Finished')" :class="[
            'py-2 px-4 rounded-full text-sm font-medium',
            filterActive === 'Finished' ? 'bg-blue-500 text-white' : 'bg-white text-gray-600 border'
          ]">
            Finished
          </button>
        </div>
        <button class="bg-blue-500 text-white py-2 px-4 rounded-lg text-sm font-medium">
          Add deal
        </button>
      </div>

      <!-- Deals Table -->
      <div class="bg-white rounded-lg shadow overflow-x-auto w-bar">
        <table class="min-w-full divide-y divide-gray-200 w-table">
          <thead class="bg-gray-50">
            <tr>
              <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Reference number
              </th>
              <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Deal name
              </th>
              <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Reservations left
              </th>
              <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                End date
              </th>
              <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Room type
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
            <tr v-for="deal in filteredDeals" :key="deal.id" @click="openDealDetails(deal)"
              class="cursor-pointer hover:bg-gray-50">
              <td class="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">
                {{ deal.reference }}
              </td>
              <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                {{ deal.name }}
              </td>
              <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                {{ deal.reservations }}
              </td>
              <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                {{ deal.endDate }}
              </td>
              <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                {{ deal.roomType }}
              </td>
              <td class="px-6 py-4 whitespace-nowrap">
                <span :class="[
                  'px-2 inline-flex text-xs leading-5 font-semibold rounded-full',
                  deal.status === 'Ongoing' ? 'bg-blue-100 text-blue-800' :
                    deal.status === 'Full' ? 'bg-red-100 text-red-800' :
                      deal.status === 'Inactive' ? 'bg-red-100 text-red-800' :
                        'bg-green-100 text-green-800'
                ]">
                  {{ deal.status }}
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

      <!-- Deal Detail Modal -->
      <DealDetailModal :is-open="isModalOpen" :deal="selectedDeal" @close="closeModal" @save="handleSave" />
    </div>
  </div>
</template>

<style scoped>
.deal-page {
  height: 100%;
  width: 97.7%;
  background-color: #F9FAFB;
}

@media (min-width: 760px) and (max-width: 1023px) {
  .deal-page {
    padding: 1rem;
    width: 96%;
  }

  .mr-tablet {
    margin-left: 1rem;
    margin-right: -1rem;
  }

  .px-6 {
    padding-left: 10px !important;
  }
}

@media (max-width: 759px) {
  .deal-page {
    padding: 1rem;
    width: 90%;
  }
}

@media (min-width: 1200px) {
  .max-w-7xl {
    max-width: 1200px;
  }
}
</style>
