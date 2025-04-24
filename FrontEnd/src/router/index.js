// src/router/index.js
import { createRouter, createWebHistory } from 'vue-router';
import RoomList from '../components/RoomList.vue';
import Rate from '../components/Rate.vue';
import FrontDesk from '../components/FrontDesk.vue';
import BookingCreate from '../components/BookingCreate.vue';
import Guest from '../components/GuestPage.vue'
import Deals from '../components/DealPage.vue'
import Dashboard from '../components/Dashboard.vue'

const routes = [
  { path: '/', component: Dashboard }, // Đường dẫn mặc định
  { path: '/front-desk', component: FrontDesk },
  { path: '/create-booking',component: BookingCreate },
  { path: '/guests',component: Guest },
  { path: '/rooms', component: RoomList },
  { path: '/deals', component: Deals },
  { path: '/rate', component: Rate },
  
  
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;