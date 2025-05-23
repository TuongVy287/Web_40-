// src/main.js
import { createApp } from 'vue';
import App from './App.vue';
import router from './router'; // Import router
import '@fortawesome/fontawesome-free/css/all.css';
import axios from 'axios'
import VueAxios from 'vue-axios'
import vuetify from './plugins/vuetify';
import './assets/styles.css'

createApp(App)
  .use(router)
  .use(VueAxios, axios)
  .use(vuetify)
  .mount('#app');