<template>
  <nav class="sidebar">
    <h2>Vanhtel</h2>
    <ul>
      <li>
        <router-link to="/" exact-active-class="active" class="sidebar-link">Dashboard</router-link>
      </li>
      <li>
        <router-link to="/front-desk" exact-active-class="active" class="sidebar-link">Front desk</router-link>
      </li>
      <li>
        <router-link to="/guests" exact-active-class="active" class="sidebar-link">Guests</router-link>
      </li>
      <li>
        <router-link to="/rooms" exact-active-class="active" class="sidebar-link">Rooms</router-link>
      </li>
      <li>
        <router-link to="/deals" exact-active-class="active" class="sidebar-link">Deals</router-link>
      </li>
      <li>
<<<<<<< HEAD
        <router-link to="/rate" exact-active-class="active" class="sidebar-link">Personnel</router-link>
=======
        <router-link v-if="user.chucDanh == 'Quản lý'" to="/rate" exact-active-class="active" class="sidebar-link">Nhân
          viên</router-link>
>>>>>>> 462159742cf7b81e52d104d12ecff069071d0284
      </li>
    </ul>
  </nav>
</template>

<script>
export default {
  name: 'Sidebar',
  data() {
    return {
      // Dữ liệu cần thiết cho sidebar
      user: localStorage.getItem('user') ? JSON.parse(localStorage.getItem('user')) : null,
    };
  },
};
</script>

<style scoped>
.sidebar {
  width: 250px;
  background-color: #ffffff;
  /* Màu nền trắng */
  padding: 20px;
  position: fixed;
  height: 100%;
  border-right: 2px solid #e4e4e4;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
  border-radius: 10px;
  transition: all 0.3s ease;
  /* Mượt mà khi thay đổi */
}

.sidebar h2 {
  color: #f39c12;
  /* Màu cho tên khách sạn */
  margin-bottom: 20px;
  font-size: 28px;
  font-weight: bold;
  text-transform: uppercase;
}

.sidebar ul {
  list-style-type: none;
  /* Không có dấu đầu dòng */
  padding: 0;
}

.sidebar li {
  margin-bottom: 12px;
  /* Khoảng cách giữa các mục */
}

.sidebar-link {
  display: block;
  /* Chuyển đổi router-link thành khối chiếm toàn bộ không gian */
  padding: 12px 20px;
  /* Padding cho các mục */
  font-size: 16px;
  border-radius: 8px;
  transition: background-color 0.3s ease, color 0.3s ease;
  /* Hiệu ứng chuyển động */
  color: black;
  /* Màu chữ mặc định */
  text-decoration: none;
  /* Bỏ gạch chân */
}

.sidebar-link:hover {
  background-color: #f39c12;
  /* Màu nền khi hover */
  color: #fff;
  /* Màu chữ trắng */
}

.sidebar .active {
  font-weight: bold;
  /* Chữ đậm cho mục đang hoạt động */
  background-color: #f39c12;
  /* Màu nền cho mục đang hoạt động */
  color: #fff;
  /* Màu chữ trắng */
}
</style>
