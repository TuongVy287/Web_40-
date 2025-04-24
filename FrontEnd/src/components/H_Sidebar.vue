<script setup lang="ts">
import { ref, onMounted, onUnmounted } from 'vue';

interface MenuItem {
  id: number;
  name: string;
  icon: string;
  active: boolean;
}

const menuItems = ref<MenuItem[]>([
  { id: 1, name: 'Dashboard', icon: 'home', active: true },
  { id: 2, name: 'Front desk', icon: 'desk', active: false },
  { id: 3, name: 'Guest', icon: 'user', active: false },
  { id: 4, name: 'Rooms', icon: 'bed', active: false },
  { id: 5, name: 'Deal', icon: 'tag', active: false },
  { id: 6, name: 'Rate', icon: 'dollar', active: false },
]);

const activeMenu = ref<string>('Dashboard');
const isSidebarOpen = ref<boolean>(false);
const isMobileOrTablet = ref<boolean>(window.innerWidth < 1025);

const emit = defineEmits(['menu-change']);

const setActiveMenu = (menuName: string): void => {
  activeMenu.value = menuName;
  menuItems.value.forEach(item => {
    item.active = item.name === menuName;
  });
  emit('menu-change', menuName);
};

const toggleSidebar = () => {
  isSidebarOpen.value = !isSidebarOpen.value;
};

const handleResize = () => {
  isMobileOrTablet.value = window.innerWidth < 1025;
};

onMounted(() => {
  window.addEventListener('resize', handleResize);
});

onUnmounted(() => {
  window.removeEventListener('resize', handleResize);
});

defineExpose({ setActiveMenu, activeMenu });
</script>

<template>
  <div>
    <button
      v-if="!isSidebarOpen && isMobileOrTablet"
      @click="toggleSidebar"
      class="sidebar-toggle-button"
    >
      ☰
    </button>

    <div
      v-if="isSidebarOpen && isMobileOrTablet"
      class="overlay"
      @click="toggleSidebar"
    ></div>

    <div
      :class="[
        'sidebar-mobile',
        isSidebarOpen ? 'open' : '',
        'h-screen bg-white py-5 px-3 flex flex-col border-r'
      ]"
      v-if="isMobileOrTablet"
      :style="{ transform: isSidebarOpen ? 'translateX(0)' : 'translateX(-100%)' }"
    >
      <div class="mb-8 px-2">
        <div class="text-blue-500 font-bold flex items-center text-2xl">
          <span class="text-3xl mr-2">⊲</span> Novotel
        </div>
      </div>

      <div class="flex-1">
        <ul class="space-y-1">
          <li v-for="item in menuItems" :key="item.id">
            <a
              href="#"
              @click.prevent="setActiveMenu(item.name)"
              :class="[
                'sidebar-menu-item flex items-center px-4 py-3 rounded-lg',
                item.active ? 'bg-blue-50 text-blue-600' : 'text-gray-600 hover:bg-gray-100'
              ]"
              :data-menu-name="item.name"
            >
              <span class="w-6 h-6 flex items-center justify-center mr-2">
                <i v-if="item.icon === 'home'" class="text-lg">🏠</i>
                <i v-else-if="item.icon === 'desk'" class="text-lg">🖥️</i>
                <i v-else-if="item.icon === 'user'" class="text-lg">👤</i>
                <i v-else-if="item.icon === 'bed'" class="text-lg">🛏️</i>
                <i v-else-if="item.icon === 'tag'" class="text-lg">🏷️</i>
                <i v-else-if="item.icon === 'dollar'" class="text-lg">💰</i>
              </span>
              {{ item.name }}
            </a>
          </li>
        </ul>
      </div>
    </div>

    <div class="sidebar-desktop" v-else>
      <div class="h-screen bg-white w-48 py-5 px-3 flex flex-col border-r">
        <div class="mb-8 px-2">
          <div class="text-blue-500 font-bold flex items-center text-2xl">
            <span class="text-3xl mr-2">⊲</span> Novotel
          </div>
        </div>

        <div class="flex-1">
          <ul class="space-y-1">
            <li v-for="item in menuItems" :key="item.id">
              <a
                href="#"
                @click.prevent="setActiveMenu(item.name)"
                :class="[
                  'sidebar-menu-item flex items-center px-4 py-3 rounded-lg',
                  item.active ? 'bg-blue-50 text-blue-600' : 'text-gray-600 hover:bg-gray-100'
                ]"
                :data-menu-name="item.name"
              >
                <span class="w-6 h-6 flex items-center justify-center mr-2">
                  <i v-if="item.icon === 'home'" class="text-lg">🏠</i>
                  <i v-else-if="item.icon === 'desk'" class="text-lg">🖥️</i>
                  <i v-else-if="item.icon === 'user'" class="text-lg">👤</i>
                  <i v-else-if="item.icon === 'bed'" class="text-lg">🛏️</i>
                  <i v-else-if="item.icon === 'tag'" class="text-lg">🏷️</i>
                  <i v-else-if="item.icon === 'dollar'" class="text-lg">💰</i>
                </span>
                {{ item.name }}
              </a>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.sidebar-desktop {
  position: relative;
  z-index: 1;
  width: 220px; /* Kích thước Sidebar cho PC */
  height: 100vh; /* Chiều cao đầy đủ của màn hình */
  overflow: hidden; /* Không cho phép cuộn */
  display: block; /* Để Sidebar hiển thị */
}

.sidebar-mobile {
  position: absolute;
  z-index: 10;
  width: 60%; /* Kích thước Sidebar trên mobile */
  transform: translateX(-100%);
  transition: transform 0.3s ease; /* Hiệu ứng chuyển động */
}

.sidebar-mobile.open {
  transform: translateX(0);
}

.overlay {
  position: fixed;
  inset: 0;
  background: rgba(0, 0, 0, 0.6);
  z-index: 3; 
}

.sidebar-toggle-button {
  position: absolute;
  top: 1rem; /* Khoảng cách từ trên */
  left: 1rem; /* Khoảng cách từ trái */
  z-index: 50;
  padding: 0.5rem; /* Khoảng cách nội dung bên trong nút */
  background-color: transparent; /* Màu nền (Xanh dương) */
  color: #000; /* Màu chữ */
  border: none; /* Không viền */
  border-radius: 0.375rem; /* Bo tròn góc */
  cursor: pointer; /* Hiệu ứng con trỏ khi hover */
  transition: background-color 0.3s ease; /* Hiệu ứng chuyển màu mượt mà */
  font-size: 2rem;
  transform: translateY(-12%);
}

.sidebar-toggle-button:hover {
  background-color: #2563eb; /* Màu khi hover */
}

/* Các thuộc tính cho menu item */
.sidebar-mobile .sidebar-menu-item {
  font-size: 1.5rem;  /* Kích thước chữ lớn hơn */
  padding: 15px;      /* Tăng kích thước vùng chạm */
}

.sidebar-mobile .sidebar-menu-item {
  margin-bottom: 10px; /* Khoảng cách giữa các mục */
}

.sidebar-mobile {
  font-size: 1.2rem; /* Kích thước chữ tổng thể trong sidebar */
}
</style>