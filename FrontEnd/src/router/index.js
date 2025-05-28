// src/router/index.js
import { createRouter, createWebHistory } from 'vue-router';
import RoomList from '../components/RoomList.vue';
import Rate from '../components/Personnel.vue';
import FrontDesk from '../components/FrontDesk.vue';
import BookingCreate from '../components/BookingCreate.vue';
import Guest from '../components/GuestPage.vue'
import Deals from '../components/DealPage.vue'
import Dashboard from '../components/Dashboard.vue'
import SignIn from '@/components/SignIn.vue';
import CccdReader from '../components/CCCDFileReader.vue'; // Import the new component

const routes = [
  // { path: '/', component: SignIn }, // Đường dẫn mặc định
  { path: '/', component: Dashboard },
  { path: '/front-desk', component: FrontDesk },
  { path: '/create-booking', component: BookingCreate },
  { path: '/guests', component: Guest },
  { path: '/rooms', component: RoomList },
  { path: '/deals', component: Deals },
  { path: '/rate', component: Rate },
  { path: '/cccdreader', component: CccdReader }


];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
