<template>
  <div class="nhanvien">
    <h2>Danh sách nhân viên
      <button @click="openAddModal" class="add-btn">+ Thêm nhân viên</button>
    </h2>

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
        <tr v-for="nv in pagedNhanVienList" :key="nv.idNhanVien">
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

    <!-- Pagination controls -->
    <div class="pagination">
      <button @click="prevPage" :disabled="currentPage === 1">Previous</button>
      <span>Page {{ currentPage }} of {{ totalPages }}</span>
      <button @click="nextPage" :disabled="currentPage === totalPages">Next</button>
    </div>

    <!-- Modal sửa nhân viên -->
    <transition name="fade">
      <div v-if="showEditModal" class="modal-overlay">
        <div class="modal-content">
          <h3>Chỉnh sửa nhân viên</h3>
          <form @submit.prevent="updateNhanVien" class="edit-form">
            <div class="form-col">
              <label>Họ tên:</label>
              <input v-model="editedNhanVien.hoTen" />

              <label>Chức danh:</label>
              <input v-model="editedNhanVien.chucDanh" />

              <label>Giới tính:</label>
              <select v-model.number="editedNhanVien.gioiTinh">
                <option :value="1">Nam</option>
                <option :value="0">Nữ</option>
              </select>

              <label>Email:</label>
              <input v-model="editedNhanVien.email" type="email" />
            </div>

            <div class="form-col">
              <label>Mã nhân viên:</label>
              <input v-model="editedNhanVien.maNV" disabled />

              <label>Số điện thoại:</label>
              <input v-model="editedNhanVien.soDienThoai" />

              <label>Ca làm việc:</label>
              <select v-model="editedNhanVien.caLamViec" required>
                <option value="Sáng">Sáng</option>
                <option value="Chiều">Chiều</option>
                <option value="Tối">Tối</option>
              </select>

              <label>Lương:</label>
              <input v-model.number="editedNhanVien.luong" type="number" min="0" />

              <!-- <label>Mật khẩu hiện tại:</label>
              <input v-model="editedNhanVien.matKhau" type="password" disabled />

              <label>Mật khẩu mới:</label>
              <input v-model="editedNhanVien.newPassword" type="password" disabled /> -->

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

              <label>Họ tên:</label>
              <input v-model="newNhanVien.hoTen" required />

              <label>Chức danh:</label>
              <select v-model="newNhanVien.chucDanh" required>
                <option value="Quản lý">Quản lý</option>
                <option value="Nhân viên">Nhân viên</option>
              </select>

              <label>Số điện thoại:</label>
              <input v-model="newNhanVien.soDienThoai" required />

              <label>Email:</label>
              <input v-model="newNhanVien.email" type="email" required />
            </div>

            <div class="form-col">
              <label>Giới tính:</label>
              <select v-model.number="newNhanVien.gioiTinh" required>
                <option :value="1">Nam</option>
                <option :value="0">Nữ</option>
              </select>

              <label>Ca làm việc:</label>
              <select v-model.number="newNhanVien.caLamViec" required>
                <option value="Sáng">Sáng</option>
                <option value="Chiều">Chiều</option>
                <option value="Tối">Tối</option>
              </select>

              <label>Lương:</label>
              <input v-model.number="newNhanVien.luong" type="number" min="0" required />

              <label>Mật khẩu:</label>
              <input v-model="newNhanVien.matKhau" type="password" required />
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
      currentPage: 1,
      pageSize: 4,
    };
  },
  created() {
    this.fetchNhanVien();
  },
  computed: {
    pagedNhanVienList() {
      const start = (this.currentPage - 1) * this.pageSize;
      return this.nhanVienList.slice(start, start + this.pageSize);
    },
    totalPages() {
      return Math.ceil(this.nhanVienList.length / this.pageSize);
    },
  },
  watch: {
    currentPage(newPage) {
      if (newPage < 1) this.currentPage = 1;
      if (newPage > this.totalPages) this.currentPage = this.totalPages;
    },
  },
  mounted() {
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
      console.log('Xoá nhân viên:', nv);
      if (confirm('Bạn có chắc muốn xoá nhân viên này không?')) {
        try {
          await axios.delete(`http://localhost:5250/api/NhanVien/Xoa/${nv.maNV}`,);
          await this.fetchNhanVien();
        } catch (err) {
          console.error('Lỗi khi xoá nhân viên:', err);
          alert('Xóa nhân viên thất bại!');
        }
      }
    },
    async nextPage() {
      if (this.currentPage < this.totalPages) {
        this.currentPage++;
      }
    },
    async prevPage() {
      if (this.currentPage > 1) {
        this.currentPage--;
      }
    },
    openEditModal(nv) {

      this.editedNhanVien = { ...nv };
      console.log('editedNhanVien', this.editedNhanVien.ngayTao);
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
          // matKhau: this.editedNhanVien.matKhau,
          // xoa: this.editedNhanVien.xoa,
          // newPassword: this.editedNhanVien.newPassword || '',
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
      if (!dateString) return '';
      const date = new Date(dateString);
      const day = String(date.getDate()).padStart(2, '0');
      const month = String(date.getMonth() + 1).padStart(2, '0');
      const year = date.getFullYear();
      return `${day}/${month}/${year}`;
    }
    ,
  },
};
</script>

<style scoped>
.nhanvien {
  margin-left: 50px;
  padding: 20px;
  background-color: #f9f9f9;
  border-radius: 1%;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
  margin-top: 80px;
}

.pagination {
  margin-top: 20px;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.pagination button {
  padding: 12px 20px;
  background-color: #f39c12;
  color: white;
  border: none;
  border-radius: 30px;
  cursor: pointer;
  font-size: 16px;
  transition: background-color 0.3s ease, transform 0.3s ease;
}

.pagination button:hover {
  background-color: #e67e22;
  transform: scale(1.05);
}

.pagination button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}

.nhanvien h2 {
  color: #f39c12;
  margin-bottom: 20px;
  font-size: 24px;
}

.nhanvien table {
  width: 100%;
  border-collapse: collapse;
  font-size: 14px;
  margin-top: 20px;
  height: 300px;
  overflow-y: auto;
}

.nhanvien th,
.nhanvien td {
  /* border: 1px solid #ddd;
  padding: 8px;
  text-align: center;
  width: 400px; */
  padding: 14px;
  border: 1px solid #ddd;
  text-align: center;
  font-size: 16px;
  border-radius: 8px;
}

.nhanvien th {
  background-color: #f1f1f1;
  color: #333;
}

.nhanvien tr:hover {
  background-color: #f1f1f1;
}

.nhanvien .action-btn {
  background-color: transparent;
  border: none;
  cursor: pointer;
  font-size: 18px;
  margin: 0 4px;
}

.nhanvien .action-btn:hover {
  color: #f39c12;
}

.nhanvien .modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 9999;
}

.nhanvien .modal-content {
  background: white;
  padding: 20px;
  border-radius: 12px;
  width: 700px;
  max-width: 90vw;
  max-height: 90vh;
  overflow-y: auto;
}

.nhanvien .edit-form {
  display: flex;
  gap: 24px;
  justify-content: space-between;
  margin-top: 15px;
}

.nhanvien .form-col {
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: 10px;
}

.nhanvien label {
  font-weight: 600;
  font-size: 13px;
  margin-bottom: 3px;
}

.nhanvien input,
.nhanvien select {
  padding: 6px 10px;
  border-radius: 6px;
  border: 1.5px solid #ccc;
  font-size: 14px;
  outline: none;
  transition: border-color 0.3s;
  color: black;
}

.nhanvien input:focus,
.nhanvien select:focus {
  border-color: #f39c12;
}

.nhanvien .modal-actions {
  margin-top: 20px;
  text-align: right;
}

.nhanvien .modal-actions button {
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

.nhanvien .modal-actions button:hover:not(:disabled) {
  background-color: #d78c0b;
}

.nhanvien .modal-actions button:disabled {
  background-color: #f0c27b;
  cursor: default;
}

.nhanvien .fade-enter-active,
.nhanvien .fade-leave-active {
  transition: opacity 0.25s;
}

.nhanvien .fade-enter-from,
.nhanvien .fade-leave-to {
  opacity: 0;
}

/* Nút thêm nhân viên */
.add-btn {
  padding: 12px;
  font-size: 16px;
  position: absolute;
  right: 0;
  top: 0;
  /* Nếu muốn nút nằm cùng dòng với tiêu đề, dùng float hoặc flex ở h2 */
}

.nhanvien h2 {
  color: #f39c12;
  margin-bottom: 10px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  position: relative;
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
  background-color: rgba(0, 0, 0, 0.5);
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

/* input[type="text"],
input[type="email"],
input[type="number"],
input[type="password"],
input[type="date"], */
input,
select {
  padding: 6px 10px;
  border-radius: 6px;
  border: 1.5px solid #ccc;
  font-size: 14px;
  outline: none;
  transition: border-color 0.3s;
  color: black;
}

input:focus,
select:focus {
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
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.25s;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
</style>
