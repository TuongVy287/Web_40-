<template>
    <!-- Nền tối mờ phía sau modal -->
    <div class="modal-overlay">
      <!-- Khung chính của modal -->
      <div class="modal-container">
        <h2 class="modal-title">Add Rate</h2>
  
        <!-- Form nhập liệu -->
        <div class="form-group">
          <label for="roomType">Room type</label>
          <input
            type="text"
            id="roomType"
            v-model="formData.roomType"
            placeholder="Enter room type"
          />
        </div>
  
        <div class="form-group">
          <label for="cancellationPolicy">Cancellation policy</label>
          <input
            type="text"
            id="cancellationPolicy"
            v-model="formData.cancellationPolicy"
            placeholder="Enter cancellation policy"
          />
        </div>
  
        <div class="form-group">
          <label for="rooms">Rooms</label>
          <input
            type="number"
            id="rooms"
            v-model.number="formData.rooms"
            placeholder="Enter total number of rooms"
          />
        </div>
  
        <div class="form-group">
          <label for="price">Price</label>
          <input
            type="number"
            id="price"
            v-model.number="formData.price"
            placeholder="Enter room price"
          />
        </div>
  
        <!-- Khu vực nút hành động -->
        <div class="modal-actions">
          <button class="cancel-button" @click="onCancel">Cancel</button>
          <button class="save-button" @click="onSave">Save</button>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  import axios from 'axios';
  
  export default {
    name: "RateModal",
    data() {
      return {
        // Khởi tạo dữ liệu form
        formData: {
          roomType: "",
          cancellationPolicy: "",
          rooms: 0,
          price: 0,
        },
      };
    },
    methods: {
      onCancel() {
        // Emit sự kiện để component cha đóng modal
        this.$emit("close");
      },
      async onSave() {
        try {
          // Gửi dữ liệu form lên server (thay đổi URL cho phù hợp)
          await axios.post("http://localhost:3000/api/rates", {
            roomType: this.formData.roomType,
            cancellationPolicy: this.formData.cancellationPolicy,
            rooms: this.formData.rooms,
            price: this.formData.price
          });
  
          // Sau khi lưu thành công, ta có thể emit sự kiện để cha fetch lại dữ liệu
          // và đóng modal
          this.$emit("close");
        } catch (error) {
          console.error("Error saving rate:", error);
        }
      },
    },
  };
  </script>
  
  <style scoped>
  /* Nền tối mờ phía sau modal */
  .modal-overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0,0,0,0.4);
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 999;
  }
  
  /* Khung chính của modal */
  .modal-container {
    background-color: #fff;
    padding: 20px;
    width: 400px;
    max-width: 90%;
    border-radius: 8px;
    box-shadow: 0 5px 15px rgba(0,0,0,0.3);
    animation: fadeInDown 0.3s ease;
  }
  
  /* Tiêu đề modal */
  .modal-title {
    margin-bottom: 20px;
    color: #f39c12;
    text-align: center;
  }
  
  /* Các nhóm form input */
  .form-group {
    margin-bottom: 15px;
  }
  
  .form-group label {
    display: block;
    margin-bottom: 6px;
    font-weight: 600;
  }
  
  .form-group input {
    width: 100%;
    padding: 8px;
    font-size: 14px;
    box-sizing: border-box;
  }
  
  /* Nút hành động */
  .modal-actions {
    display: flex;
    justify-content: flex-end;
    gap: 10px;
    margin-top: 20px;
  }
  
  .cancel-button,
  .save-button {
    padding: 10px 16px;
    border: none;
    border-radius: 4px;
    cursor: pointer;
    font-size: 14px;
  }
  
  .cancel-button {
    background-color: #ccc;
    color: #333;
  }
  
  .save-button {
    background-color: #f39c12;
    color: #fff;
    transition: background-color 0.3s ease;
  }
  
  .save-button:hover {
    background-color: #e67e22;
  }
  
  /* Hiệu ứng xuất hiện modal */
  @keyframes fadeInDown {
    from {
      opacity: 0;
      transform: translateY(-10%);
    }
    to {
      opacity: 1;
      transform: translateY(0);
    }
  }
  </style>
  