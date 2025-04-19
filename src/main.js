// src/main.js
import { createApp } from 'vue';
import App from './App.vue';
import router from './router'; // Import router
import '@fortawesome/fontawesome-free/css/all.css';

createApp(App)
  .use(router) // Sử dụng router
  .mount('#app');