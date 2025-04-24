<template>
  <div class="bg-white p-6 rounded-md mb-6">
    <h2 class="text-xl font-semibold text-gray-800 mb-4">Rooms</h2>

    <div class="grid grid-cols-4 gap-6">
      <div
        v-for="room in rooms"
        :key="room.id"
        class="bg-white border border-gray-200 rounded-md overflow-hidden shadow-sm relative"
      >
        <!-- Deals badge -->
        <div v-if="room.dealsCount > 0" class="absolute top-3 left-3 bg-green-100 text-green-800 px-2 py-1 text-xs rounded-md none">
          {{ room.dealsCount }} Deals
        </div>

        <!-- Card header with more options -->
        <div class="flex justify-end p-3">
          <button class="text-gray-400 hover:text-gray-600">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" viewBox="0 0 20 20" fill="currentColor">
              <path d="M10 6a2 2 0 110-4 2 2 0 010 4zM10 12a2 2 0 110-4 2 2 0 010 4zM10 18a2 2 0 110-4 2 2 0 010 4z" />
            </svg>
          </button>
        </div>

        <!-- Room type and availability -->
        <div class="px-4 pb-4">
          <h3 class="font-medium text-gray-800">{{ room.type }}</h3>
          <div class="flex items-center mt-2 mb-4">
            <span class="text-lg font-bold">{{ room.available }}</span>
            <span class="text-gray-500 text-sm">/{{ room.total }}</span>
          </div>

          <!-- Price -->
          <div class="mt-2">
            <span class="text-blue-600 font-bold text-xl">$ {{ room.price }}</span>
            <span class="text-gray-500 text-sm">/ day</span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
// Room data
const rooms = [
  {
    id: 1,
    type: 'Single sharing',
    dealsCount: 2,
    available: 2,
    total: 30,
    price: 568
  },
  {
    id: 2,
    type: 'Double sharing',
    dealsCount: 2,
    available: 2,
    total: 35,
    price: 1068
  },
  {
    id: 3,
    type: 'Triple sharing',
    dealsCount: 2,
    available: 2,
    total: 25,
    price: 1568
  },
  {
    id: 4,
    type: 'VIP Suit',
    dealsCount: 4,
    available: 4,
    total: 10,
    price: 2568
  },
];
</script>

<style scoped>
@media (max-width: 760px) {
  .grid {
    grid-template-columns: repeat(2, 1fr); /* Thay đổi bố cục thành 2 cột */
  }

  .none {
    display: none;
  }
}
</style>