<script setup lang="ts">
import { ref, provide, watch } from 'vue';

const activePage = ref('Dashboard');

// Set active page function
const setActivePage = (page: string) => {
  console.log('NavigationController: Setting active page to', page);
  activePage.value = page;
};

// Make the active page and setter available to child components
provide('activePage', activePage);
provide('setActivePage', setActivePage);

// Watch for changes to activePage
watch(activePage, (newPage) => {
  console.log('Active page changed to:', newPage);
});
</script>

<template>
  <slot :active-page="activePage" :set-active-page="setActivePage"></slot>
</template>
