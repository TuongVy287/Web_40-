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

  components: {
    Sidebar,
    Header,
    SignIn,
  },
  data() {
    return {
      isLoggedIn: false, // Trạng thái đăng nhập
    };
  },
  created() {
    // Kiểm tra xem localStorage có user không
    const user = localStorage.getItem('user');
    this.isLoggedIn = !!user;

    // Lắng nghe sự kiện thay đổi trạng thái đăng nhập (từ login hoặc logout)
    window.addEventListener('user-changed', () => {
      this.isLoggedIn = !!localStorage.getItem('user');
    });
  }


};
</script>


<style scoped>
.main-content {
  margin-left: 220px;
  /* Kích thước của sidebar*/
}
</style>
