<template>
  <div id="app">
    <!-- Kiểm tra trạng thái đăng nhập -->
    <SignIn v-if="!isLoggedIn" />
    <div v-else>
      <Sidebar />
      <div class="main-content">
        <Header />
        <router-view></router-view>
      </div>
    </div>
  </div>
</template>

<script>
import Sidebar from './components/Sidebar.vue';
import Header from './components/Header.vue';
import SignIn from './components/SignIn.vue';

export default {
  name: "App",
  methods: {

  },
  components: {
    Sidebar,
    Header,
    SignIn,
  },
  data() {
    return {
      isLoggedIn: false
    };
  },
  created() {
    // Kiểm tra token đăng nhập trong localStorage
    const token = localStorage.getItem('token');
    this.isLoggedIn = !!token;
  }
};
</script>

<style scoped>
/* .main-content {
  margin-left: 220px; /* Kích thước của sidebar 
} */
</style>
