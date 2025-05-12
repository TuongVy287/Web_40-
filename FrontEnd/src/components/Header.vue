<template>
  <header class="header">
    <div class="search-container">
      <input type="text" placeholder="Search for rooms and offers" />
    </div>
    <div class="user-options">
      <i class="fas fa-bell icon"></i>
      <i class="fas fa-user-circle icon user-avatar" @click="toggleDropdown"></i>

      <!-- Dropdown thông tin nhân viên -->
      <div v-if="showDropdown" class="dropdown-menu">
        <p><strong>Mã NV:</strong> {{ user.maNV }}</p>
        <p><strong>Họ tên:</strong> {{ user.hoTen }}</p>
        <button @click="openEditModal" class="edit-button">Chỉnh sửa thông tin</button>
        <button @click="logout" class="logout-button">Đăng xuất</button>
      </div>
    </div>

    <!-- Modal chỉnh sửa thông tin -->
    <div v-if="showModal" class="modal-overlay">
      <div class="modal-centered">
        <div class="modal-content scrollable">
          <h3>Chỉnh sửa thông tin</h3>
          <div class="form-group"><label>Họ tên</label><input v-model="user.hoTen" /></div>
        
          <div class="form-group"><label>SĐT</label><input v-model="user.soDienThoai" /></div>
          <div class="form-group"><label>Email</label><input v-model="user.email" /></div>
          <div class="modal-actions">
            <button @click="updateNhanVien">Lưu</button>
            <button @click="closeEditModal">Đóng</button>
          </div>
        </div>
      </div>
    </div>
  </header>
</template>


<script>
import axios from 'axios';

export default {
  name: 'Header',
  data() {
    return {
      showDropdown: false,
      showModal: false,
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
    openEditModal() {
      this.showModal = true;
      this.showDropdown = false;
    },
    closeEditModal() {
      this.showModal = false;
    },
    async updateNhanVien() {
  try {
    console.log('Dữ liệu gửi đi:', JSON.stringify(this.user, null, 2));

    const res = await axios.put('http://localhost:5250/api/NhanVien/SuaNhanVien', this.user);

    alert('Cập nhật thành công!');
    this.closeEditModal();
    localStorage.setItem('user', JSON.stringify({ user: this.user }));
  } catch (err) {
    console.error('Lỗi chi tiết:', err.response?.data || err.message);
    alert('Lỗi cập nhật: ' + JSON.stringify(err.response?.data || err.message));
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
.edit-button {
  margin-top: 10px;
  background-color: #3498db;
  color: white;
  padding: 8px 14px;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  font-weight: bold;
  width: 100%;
  transition: background-color 0.3s ease;
}
.edit-button:hover {
  background-color: #2980b9;
}
/* Modal overlay và canh giữa */
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5); /* nền mờ */
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 2000;
}

.modal-centered {
  background-color: white;
  border-radius: 10px;
  width: 400px;
  max-height: 90vh;
  overflow: hidden;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.3);
}

.modal-content {
  padding: 20px;
}

.scrollable {
  overflow-y: auto;
  max-height: 80vh;
}

.modal-content h3 {
  color: #f39c12;
  margin-bottom: 10px;
}

.form-group {
  margin-bottom: 10px;
}

.form-group label {
  font-size: 14px;
  display: block;
  margin-bottom: 4px;
}

.form-group input,
.form-group select {
  width: 100%;
  padding: 6px;
  border: 1px solid #000000;
  border-radius: 4px;
  color: black;
}

.modal-actions {
  display: flex;
  justify-content: space-between;
  margin-top: 10px;
}

.modal-actions button {
  padding: 6px 12px;
  background-color: #f39c12;
  color: white;
  border: none;
  border-radius: 6px;
  cursor: pointer;
}

.modal-actions button:hover {
  background-color: #e67e22;
}

</style>
