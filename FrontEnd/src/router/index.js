// src/router/index.js
import { createRouter, createWebHistory } from 'vue-router';
import RoomList from '../components/RoomList.vue';
import Rate from '../components/Rate.vue';
import FrontDesk from '../components/FrontDesk.vue';
import BookingCreate from '../components/BookingCreate.vue';
const routes = [
  { path: '/', component: RoomList }, // Đường dẫn mặc định
  { path: '/rooms', component: RoomList },
  { path: '/rate', component: Rate },
  { path: '/front-desk', component: FrontDesk },
  { path: '/create-booking',component: BookingCreate,  }
  
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;