<template>
  <header class="header">
    <div class="search-container">
      <input type="text" placeholder="Search for rooms and offers" />
      <button class="add-room-button">Add room</button>
    </div>
    <div class="user-options">
      <i class="fas fa-bell icon"></i>
      <i class="fas fa-user-circle icon user-avatar" @click="toggleDropdown"></i>

      <!-- Dropdown thông tin nhân viên -->
      <div v-if="showDropdown" class="dropdown-menu">
        <p><strong>Mã NV:</strong> {{ user.maNV }}</p>
        <p><strong>Họ tên:</strong> {{ user.hoTen }}</p>
        <button @click="logout" class="logout-button">Đăng xuất</button>
      </div>
    </div>
  </header>
</template>

<script>
export default {
  name: 'Header',
  data() {
    return {
      showDropdown: false,
      user: {}
    }
  },
  mounted() {
    const stored = localStorage.getItem('user');
    if (stored) {
      const parsed = JSON.parse(stored);
      this.user = parsed.user; // Lấy ra phần "user" bên trong
    }
    // Ẩn dropdown nếu click bên ngoài
    window.addEventListener('click', this.handleOutsideClick);
  },
  beforeDestroy() {
    window.removeEventListener('click', this.handleOutsideClick);
  },
  methods: {
    toggleDropdown(event) {
      event.stopPropagation(); // Không cho click lan ra ngoài
      this.showDropdown = !this.showDropdown;
      console.log(this.user)
    },
    handleOutsideClick(event) {
      const dropdown = this.$el.querySelector('.dropdown-menu');
      if (dropdown && !dropdown.contains(event.target)) {
        this.showDropdown = false;
      }
    },
    logout() {
      localStorage.removeItem('user');
      window.dispatchEvent(new Event('user-changed'));
    }
  }
}
</script>

<style scoped>
.header {
  padding: 20px;
  background-color: #ffffff;
  display: flex;
  justify-content: space-between;
  align-items: center;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
  margin-left: 50px;
  margin-bottom: 50px;
}

.search-container {
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.search-container input {
  padding: 12px;
  border-radius: 30px;
  border: 1px solid #ddd;
  flex: 1;
  font-size: 16px;
  transition: border-color 0.3s ease;
}

.search-container input:focus {
  border-color: #f39c12;
}

.add-room-button {
  background-color: #f39c12;
  color: white;
  border: none;
  padding: 12px 20px;
  border-radius: 30px;
  cursor: pointer;
  font-size: 16px;
  transition: background-color 0.3s ease, transform 0.3s ease;
}

.add-room-button:hover {
  background-color: #e67e22;
  transform: scale(1.05);
}

.user-options {
  position: relative;
  display: flex;
  align-items: center;
}

.icon {
  font-size: 30px;
  margin-left: 20px;
  cursor: pointer;
  transition: transform 0.3s ease;
}

.icon:hover {
  transform: scale(1.1);
  color: #f39c12;
}

.dropdown-menu {
  position: absolute;
  top: 60px;
  right: 0;
  background: white;
  border: 1px solid #ddd;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.15);
  padding: 15px;
  border-radius: 10px;
  z-index: 1000;
  width: 220px;
}

.dropdown-menu p {
  margin: 5px 0;
  font-size: 14px;
  color: #333;
}

.logout-button {
  margin-top: 10px;
  background-color: #e74c3c;
  color: white;
  padding: 8px 14px;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  font-weight: bold;
  width: 100%;
  transition: background-color 0.3s ease;
}

.logout-button:hover {
  background-color: #c0392b;
}
</style>
