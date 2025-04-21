<script setup lang="ts">
const currentDate = 'Friday, November 18, 2022';
</script>

<template>
  <div class="flex justify-between items-center w-full py-4 px-6 bg-white border-b w-header tm-header">
    <div class="flex-1 max-w-xl">
      <div class="relative">
        <div class="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none mar-10">
          <svg class="w-4 h-4 text-gray-400" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
          </svg>
        </div>
        <input
          type="search"
          class="w-full pl-10 pr-4 py-2 rounded-md bg-gray-100 border-0 placeholder-gray-400 text-sm focus:ring-0 focus:outline-none mar"
          placeholder="Search for rooms and offers"
        />
      </div>
    </div>

    <div class="text-gray-600 font-medium mx-6 hid">
      {{ currentDate }}
    </div>

    <div class="flex items-center ">
      <button class="relative p-2 mx-3">
        <svg class="w-6 h-6 text-gray-600" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 17h5l-1.405-1.405A2.032 2.032 0 0118 14.158V11a6.002 6.002 0 00-4-5.659V5a2 2 0 10-4 0v.341C7.67 6.165 6 8.388 6 11v3.159c0 .538-.214 1.055-.595 1.436L4 17h5m6 0v1a3 3 0 11-6 0v-1m6 0H9" />
        </svg>
      </button>

      <div class="w-10 h-10 rounded-full bg-gray-200 overflow-hidden hid">
        <img src="https://randomuser.me/api/portraits/men/32.jpg" alt="User avatar" class="w-full h-full object-cover " />
      </div>
    </div>
  </div>
</template>

<style>
  @media (max-width: 1023px) {
    .hid {
      display: none;
    }

    .mar {
      width: 80%;
      margin-left: 15%;
    }

    .mar-10 {
      margin-left: 10%;
    }
  } 

  @media (max-width: 739px) {
    .mar-10 {
      margin-left: 15% !important;
    }
  }
</style>