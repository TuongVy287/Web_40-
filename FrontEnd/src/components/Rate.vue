<template>
  <div class="nhanvien">
    <h2>Danh sách nhân viên</h2>
    <button @click="openAddModal" class="add-btn">+ Thêm nhân viên</button>

    <table>
      <thead>
        <tr>
          <th>Mã nhân viên</th>
          <th>Họ tên</th>
          <th>Giới tính</th>
          <th>Chức danh</th>
          <th>Email</th>
          <th>Ca làm việc</th>
          <th>Lương</th>
          <th>Ngày tạo</th>
          <th>Hành động</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="nv in nhanVienList" :key="nv.idNhanVien">
          <td>{{ nv.maNV }}</td>
          <td>{{ nv.hoTen }}</td>
          <td>{{ nv.gioiTinh === 1 ? 'Nam' : 'Nữ' }}</td>
          <td>{{ nv.chucDanh }}</td>
          <td>{{ nv.email }}</td>
          <td>{{ nv.caLamViec }}</td>
          <td>{{ nv.luong }}</td>
          <td>{{ formatDate(nv.ngayTao) }}</td>
          <td>
            <button @click="openEditModal(nv)" class="action-btn">✏️</button>
            <button @click="deleteNhanVien(nv)" class="action-btn">🗑️</button>
          </td>
        </tr>
      </tbody>
    </table>

    <!-- Modal sửa nhân viên -->
    <transition name="fade">
      <div v-if="showEditModal" class="modal-overlay">
        <div class="modal-content">
          <h3>Chỉnh sửa nhân viên</h3>
          <form @submit.prevent="updateNhanVien" class="edit-form">
            <div class="form-col">

              <label>Mã nhân viên:</label>
              <input v-model="editedNhanVien.maNV" disabled />

              <label>Họ tên:</label>
              <input v-model="editedNhanVien.hoTen" />

              <label>Giới tính:</label>
              <select v-model.number="editedNhanVien.gioiTinh">
                <option :value="1">Nam</option>
                <option :value="0">Nữ</option>
              </select>

              <label>Chức danh:</label>
              <input v-model="editedNhanVien.chucDanh" />

              <label>Số điện thoại:</label>
              <input v-model="editedNhanVien.soDienThoai" />

              <label>Email:</label>
              <input v-model="editedNhanVien.email" type="email" />
            </div>

            <div class="form-col">
              <label>Ca làm việc:</label>
              <input v-model="editedNhanVien.caLamViec" />

              <label>Ngày tạo:</label>
              <input v-model="editedNhanVien.ngayTao" type="date" disabled />

              <label>Lương:</label>
              <input v-model.number="editedNhanVien.luong" type="number" min="0" />

              <label>Mật khẩu hiện tại:</label>
              <input v-model="editedNhanVien.matKhau" type="password" disabled />

              <label>Mật khẩu mới:</label>
              <input v-model="editedNhanVien.newPassword" type="password" disabled />

              <label>Xóa (0: không xóa, 1: đã xóa):</label>
              <select v-model.number="editedNhanVien.xoa" disabled>
                <option :value="0">0</option>
                <option :value="1">1</option>
              </select>
            </div>

            <div class="modal-actions">
              <button type="submit" :disabled="isUpdating">
                {{ isUpdating ? 'Đang lưu...' : 'Lưu' }}
              </button>
              <button type="button" @click="closeEditModal">Huỷ</button>
            </div>
          </form>
        </div>
      </div>
    </transition>

    <!-- Modal thêm nhân viên -->
    <transition name="fade">
      <div v-if="showAddModal" class="modal-overlay">
        <div class="modal-content">
          <h3>Thêm nhân viên mới</h3>
          <form @submit.prevent="addNhanVien" class="edit-form">
            <div class="form-col">

              <label>Mã nhân viên:</label>
              <input v-model="newNhanVien.maNV" required />

              <label>Họ tên:</label>
              <input v-model="newNhanVien.hoTen" required />

              <label>Giới tính:</label>
              <select v-model.number="newNhanVien.gioiTinh" required>
                <option :value="1">Nam</option>
                <option :value="0">Nữ</option>
              </select>

              <label>Chức danh:</label>
              <input v-model="newNhanVien.chucDanh" required />

              <label>Số điện thoại:</label>
              <input v-model="newNhanVien.soDienThoai" required />

              <label>Email:</label>
              <input v-model="newNhanVien.email" type="email" required />
            </div>

            <div class="form-col">
              <label>Ca làm việc:</label>
              <input v-model="newNhanVien.caLamViec" required />

              <label>Ngày tạo:</label>
              <input v-model="newNhanVien.ngayTao" type="date" required />

              <label>Lương:</label>
              <input v-model.number="newNhanVien.luong" type="number" min="0" required />

              <label>Mật khẩu:</label>
              <input v-model="newNhanVien.matKhau" type="password" required />

              <label>Xóa (0: không xóa, 1: đã xóa):</label>
              <select v-model.number="newNhanVien.xoa" required>
                <option :value="0">0</option>
                <option :value="1">1</option>
              </select>
            </div>

            <div class="modal-actions">
              <button type="submit" :disabled="isAdding">
                {{ isAdding ? 'Đang thêm...' : 'Thêm' }}
              </button>
              <button type="button" @click="closeAddModal">Huỷ</button>
            </div>
          </form>
        </div>
      </div>
    </transition>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      nhanVienList: [],
      showEditModal: false,
      editedNhanVien: null,
      isUpdating: false,

      showAddModal: false,
      newNhanVien: {
        maNV: '',
        hoTen: '',
        gioiTinh: 1,
        chucDanh: '',
        soDienThoai: '',
        email: '',
        caLamViec: '',
        ngayTao: '',
        luong: 0,
        matKhau: '',
        xoa: 0,
      },
      isAdding: false,
    };
  },
  created() {
    this.fetchNhanVien();
  },
  methods: {
    async fetchNhanVien() {
      try {
        const res = await axios.get('http://localhost:5250/api/NhanVien');
        this.nhanVienList = res.data;
      } catch (err) {
        console.error('Lỗi khi lấy dữ liệu nhân viên:', err);
      }
    },
    async deleteNhanVien(nv) {
  if (!nv || !nv.maNV) {
    alert("Mã nhân viên không hợp lệ!");
    return;
  }
  
  if (confirm('Bạn có chắc muốn xoá nhân viên này không?')) {
    try {
      await axios.delete(`http://localhost:5250/api/NhanVien/Xoa/${maNV}`);
      await this.fetchNhanVien();
    } catch (err) {
      console.error('Lỗi khi xoá nhân viên:', err);
      alert('Xóa nhân viên thất bại!');
    }
  }
},

    openEditModal(nv) {
      this.editedNhanVien = { ...nv };
      if (this.editedNhanVien.newPassword === null) {
        this.editedNhanVien.newPassword = '';
      }
      this.showEditModal = true;
    },
    closeEditModal() {
      this.editedNhanVien = null;
      this.showEditModal = false;
    },
    async updateNhanVien() {
      if (!this.editedNhanVien.soDienThoai || this.editedNhanVien.soDienThoai.trim() === '') {
        alert('Số điện thoại không được để trống');
        return;
      }
      if (this.editedNhanVien.luong <= 0) {
        alert('Lương phải lớn hơn 0');
        return;
      }
      this.isUpdating = true;
      try {
        const payload = {
          idNhanVien: this.editedNhanVien.idNhanVien,
          maNV: this.editedNhanVien.maNV,
          hoTen: this.editedNhanVien.hoTen,
          gioiTinh: this.editedNhanVien.gioiTinh,
          chucDanh: this.editedNhanVien.chucDanh,
          soDienThoai: this.editedNhanVien.soDienThoai,
          email: this.editedNhanVien.email,
          caLamViec: this.editedNhanVien.caLamViec,
          ngayTao: this.editedNhanVien.ngayTao,
          luong: this.editedNhanVien.luong,
          matKhau: this.editedNhanVien.matKhau,
          xoa: this.editedNhanVien.xoa,
          newPassword: this.editedNhanVien.newPassword || '',
        };
        await axios.put('http://localhost:5250/api/NhanVien/SuaNhanVien', payload);
        await this.fetchNhanVien();
        this.closeEditModal();
      } catch (error) {
        console.error('Lỗi khi cập nhật nhân viên:', error.response?.data || error.message);
        alert('Cập nhật thất bại, vui lòng kiểm tra lại dữ liệu.');
      } finally {
        this.isUpdating = false;
      }
    },

    openAddModal() {
      // Reset form thêm mới
      this.newNhanVien = {
        maNV: '',
        hoTen: '',
        gioiTinh: 1,
        chucDanh: '',
        soDienThoai: '',
        email: '',
        caLamViec: '',
        ngayTao: new Date().toISOString().substr(0, 10), // ngày hiện tại
        luong: 0,
        matKhau: '',
        xoa: 0,
      };
      this.showAddModal = true;
    },
    closeAddModal() {
      this.showAddModal = false;
    },
    async addNhanVien() {
      // Validate dữ liệu
      if (!this.newNhanVien.maNV.trim()) {
        alert('Mã nhân viên không được để trống');
        return;
      }
      if (!this.newNhanVien.hoTen.trim()) {
        alert('Họ tên không được để trống');
        return;
      }
      if (!this.newNhanVien.soDienThoai.trim()) {
        alert('Số điện thoại không được để trống');
        return;
      }
      if (this.newNhanVien.luong <= 0) {
        alert('Lương phải lớn hơn 0');
        return;
      }
      if (!this.newNhanVien.matKhau.trim()) {
        alert('Mật khẩu không được để trống');
        return;
      }

      this.isAdding = true;
      try {
        await axios.post('http://localhost:5250/api/Auth/DangKy', this.newNhanVien);
        await this.fetchNhanVien();
        this.closeAddModal();
      } catch (error) {
        console.error('Lỗi khi thêm nhân viên:', error.response?.data || error.message);
        alert('Thêm nhân viên thất bại, vui lòng kiểm tra lại dữ liệu.');
      } finally {
        this.isAdding = false;
      }
    },

    formatDate(dateString) {
      if (!dateString) return 'N/A';
      const date = new Date(dateString);
      return date.toLocaleDateString();
    },
  },
};
</script>

<style scoped>
.nhanvien {
  margin: 40px;
  background: #fff;
  padding: 20px;
  border-radius: 10px;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
}

h2 {
  color: #f39c12;
  margin-bottom: 10px;
}

/* Nút thêm nhân viên */
.add-btn {
  background-color: #f31212;
  border: none;
  color: white;
  padding: 10px 16px;
  border-radius: 8px;
  font-weight: 600;
  margin-bottom: 15px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

table {
  width: 100%;
  border-collapse: collapse;
  font-size: 14px;
}

th, td {
  border: 1px solid #ddd;
  padding: 8px;
  text-align: center;
}

th {
  background-color: #f39c12;
  color: white;
  font-weight: 600;
}

/* Nút hành động */
.action-btn {
  background-color: transparent;
  border: none;
  cursor: pointer;
  font-size: 18px;
  margin: 0 4px;
}

.action-btn:hover {
  color: #f39c12;
}

/* Modal */
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0,0,0,0.5);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 9999;
}

.modal-content {
  background: white;
  padding: 20px;
  border-radius: 12px;
  width: 700px;
  max-width: 90vw;
  max-height: 90vh;
  overflow-y: auto;
}

.edit-form {
  display: flex;
  gap: 24px;
  justify-content: space-between;
  margin-top: 15px;
}

.form-col {
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: 10px;
}

label {
  font-weight: 600;
  font-size: 13px;
  margin-bottom: 3px;
}

input[type="text"],
input[type="email"],
input[type="number"],
input[type="password"],
input[type="date"],
select {
  padding: 6px 10px;
  border-radius: 6px;
  border: 1.5px solid #ccc;
  font-size: 14px;
  outline: none;
  transition: border-color 0.3s;
}

input:focus, select:focus {
  border-color: #f39c12;
}

/* Modal actions */
.modal-actions {
  margin-top: 20px;
  text-align: right;
}

.modal-actions button {
  background-color: #f39c12;
  border: none;
  color: white;
  padding: 8px 18px;
  border-radius: 8px;
  font-weight: 600;
  cursor: pointer;
  margin-left: 12px;
  transition: background-color 0.3s ease;
}

.modal-actions button:hover:not(:disabled) {
  background-color: #d78c0b;
}

.modal-actions button:disabled {
  background-color: #f0c27b;
  cursor: default;
}

/* Transition */
.fade-enter-active, .fade-leave-active {
  transition: opacity 0.25s;
}
.fade-enter-from, .fade-leave-to {
  opacity: 0;
}
</style>
