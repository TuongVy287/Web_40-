<template>
  <div class="login-page">
    <div v-if="!forgotpassword">
      <div class="login-box">
        <h2>Đăng Nhập</h2>
        <input type="text" v-model="MaNV" placeholder="Mã nhân viên" required />
        <input type="password" v-model="password" placeholder="Mật khẩu" required />
        <a href="#" class="forgot-password" @click="forgotpassword = true">Quên mật khẩu?</a>
        <button type="submit" @click="handleLogin">ĐĂNG NHẬP</button>
      </div>
    </div>
    <div v-else>
      <div class="login-box">
        <h2>Quên mật khẩu</h2>
        <input type="text" v-model="MaNV" placeholder="Mã nhân viên" required />
        <input type="password" v-model="password" placeholder="Mật khẩu" required />
        <a href="#" class="forgot-password" @click="forgotpassword = false">Quay lại</a>
        <button type="submit">Gửi</button>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
export default {
  name: 'SignIn',
  data() {
    return {
      forgotpassword: false,
      username: [],
      MaNV: '',
      password: '',
    }
  },
  methods: {
    async handleLogin() {
      console.log('Login with:', this.MaNV, this.password)
      try {
        const response = await axios.post('http://localhost:5250/api/Auth/login', {
          MaNV: this.MaNV,
          MatKhau: this.password
        })
        console.log('Login with:', response.data)
        if (response.data) {
          localStorage.setItem('user', JSON.stringify(response.data))
          window.dispatchEvent(new Event('user-changed')); // thủ công gửi sự kiện
        } else {
          console.log('Login failed');
        }
      } catch (error) {
        console.error('Error during login:', error);
      }
    },
  }
};
</script>

<style scoped>
.login-page {
  width: 100vw;
  height: 100vh;
  background-color: #f3f4f6;
  display: flex;
  justify-content: center;
  align-items: center;
}

.login-box {
  background: white;
  padding: 40px;
  border-radius: 12px;
  box-shadow: 0 8px 20px rgba(0, 0, 0, 0.1);
  width: 320px;
  display: flex;
  flex-direction: column;
  gap: 15px;
  text-align: center;
}

.login-box h2 {
  margin-bottom: 10px;
  font-size: 22px;
  font-weight: bold;
}

.login-box input {
  padding: 10px;
  font-size: 14px;
  border: 1px solid #ccc;
  border-radius: 6px;
  outline: none;
  color: black;
}

.login-box .forgot-password {
  font-size: 13px;
  color: #6a1b9a;
  text-decoration: none;
  display: inline-block;
  margin: 5px 0;
}

.login-box button {
  padding: 10px;
  background-color: #f9a825;
  color: white;
  font-weight: 600;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  transition: background 0.3s ease;
}

.login-box button:hover {
  background-color: #f57f17;
}
</style>
