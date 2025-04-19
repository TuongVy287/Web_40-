<template>
  <div id="app">
    <Sidebar />
    <div class="main-content">
      <Header />
      <router-view></router-view> <!-- Router view sẽ hiển thị các trang khác nhau -->
    </div>
  </div>
</template>

<script>
import Sidebar from './components/Sidebar.vue';
import Header from './components/Header.vue';
export default {
  name: "App",
  methods: {
    testApi(){
    this.axios.get("http://localhost:5250/api/DichVu").then((response) => {
        console.log(response.data)
      })
    }
  },

  components: {
    Sidebar,
    Header
  }
};
</script>

<style scoped>
.main-content {
  margin-left: 220px; /* Kích thước của sidebar */
}
</style>
