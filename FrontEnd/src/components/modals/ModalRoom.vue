<template>
  <div v-if="isOpen" class="modal-overlay">
    <div class="modal-content">
      <h2>Thông tin đặt phòng</h2>

      <form @submit.prevent="submitForm">
        <div class="form-grid">
          <!-- CCCD (đầu tiên, 2 cột, có nút tìm) -->
          <div class="form-group full-width id-card-group">
            <label>Căn cước công dân</label>
            <div class="id-card-input">
              <input v-model="form.idCard"  />
              <button type="button" @click="searchGuestByIdCard">Tìm</button>
            </div>
          </div>

          <!-- Thông tin khách -->
          <div class="form-group">
            <label>Họ tên</label>
            <input v-model="form.guestName" required />
          </div>
          <div class="form-group">
            <label>Ngày sinh</label>
            <input type="date" v-model="form.birthDate" />
          </div>
          <div class="form-group">
            <label>SĐT</label>
            <input v-model="form.phone" required />
          </div>
          <div class="form-group">
            <label>Giới tính</label>
            <select v-model="form.gender">
              <option value="Nam">Nam</option>
              <option value="Nữ">Nữ</option>
              <option value="Khác">Khác</option>
            </select>
          </div>
          
          
          <div class="form-group">
            <label>Email</label>
            <input type="email" v-model="form.email" />
          </div>
          <div class="form-group full-width">
            <label>Địa chỉ</label>
            <input v-model="form.address" />
          </div>

          <!-- Thông tin phòng -->
          <div class="form-group">
            <label>Loại phòng</label>
            <input :value="room?.loaiPhong" readonly />
          </div>
          <div class="form-group">
            <label>Tên phòng</label>
            <input :value="room?.tenPhong" readonly />
          </div>
          <div class="form-group full-width">
            <label>Giá</label>
            <input :value="formatPrice(room?.gia)" readonly />
          </div>
        </div>

        <!-- Nút hành động -->
        <div class="form-actions">
          <button type="submit">Xác nhận</button>
          <button type="button" @click="$emit('close')">Hủy</button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    isOpen: Boolean,
    room: Object,
  },
  data() {
    return {
      form: {
        guestName: '',
        gender: 'Nam',
        birthDate: '',
        phone: '',
        email: '',
        idCard: '',
        address: '',
      },
    };
  },
  methods: {
    submitForm() {
      console.log('Đặt phòng:', {
        ...this.form,
        roomId: this.room.idPhong,
      });
      this.$emit('close');
    },
    formatPrice(value) {
      if (!value) return '';
      return new Intl.NumberFormat('vi-VN', {
        style: 'currency',
        currency: 'VND',
      }).format(value);
    },
    searchGuestByIdCard() {
      console.log('Tìm khách theo CCCD:', this.form.idCard);
      const mockGuest = {
        guestName: 'Nguyễn Văn A',
        gender: 'Nam',
        birthDate: '1990-01-01',
        phone: '0909123456',
        email: 'vana@example.com',
        address: '123 Đường ABC, Quận 1, TP.HCM',
      };
      if (this.form.idCard === '123456789') {
        Object.assign(this.form, mockGuest);
      } else {
        alert('Không tìm thấy khách hàng với CCCD này.');
      }
    },
  },
};
</script>

<style scoped>
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
}

.modal-content {
  background: #fff;
  padding: 30px;
  border-radius: 12px;
  max-width: 600px;
  width: 100%;
}

h2 {
  margin-bottom: 20px;
  color: #ea9c00;
  text-align: center;
}

.form-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 16px;
}

.full-width {
  grid-column: span 2;
}

.form-group {
  display: flex;
  flex-direction: column;
}

.form-group label {
  font-weight: 600;
  margin-bottom: 4px;
}

.form-group input,
.form-group select {
  padding: 8px;
  border: 1px solid #ccc;
  border-radius: 8px;
  color: black;
}

.id-card-input {
  display: flex;
  gap: 8px;
}

.id-card-input input {
  flex: 1;
}

.id-card-input button {
  white-space: nowrap;
  padding: 8px 16px;
  background: #ea9c00;
  border: none;
  border-radius: 8px;
  color: white;
  cursor: pointer;
}



.form-actions {
  display: flex;
  justify-content: space-between;
  margin-top: 20px;
}

button {
  background: #ea9c00;
  color: white;
  padding: 10px 16px;
  border: none;
  border-radius: 8px;
  cursor: pointer;
}

button:hover {
  background: #d48806;
}
</style>
