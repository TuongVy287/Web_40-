<template>
  <div class="container" :class="{ 'sign-up-mode': isSignUp }">
    <!-- Sign In Form -->
    <div class="form-container sign-in-container">
      <form @submit.prevent="handleLogin">
        <h2>Đăng Nhập</h2>
        <input placeholder="Mã nhân viên" v-model="MaNV" required />
        <input type="password" placeholder="Mật khẩu" v-model="password" required />
        <a href="#">Quên mật khẩu?</a>
        <button type="submit">Đăng Nhập</button>
      </form>
    </div>

    <!-- Sign Up Form -->
    <!-- <div class="form-container sign-up-container">
      <form @submit.prevent="handleRegister">
        <h2>Đăng Ký</h2>
        <input type="text" placeholder="Tên người dùng" v-model="username" required />
        <input type="email" placeholder="Email" v-model="email" required />
        <input type="password" placeholder="Mật khẩu" v-model="password" required />
        <button type="submit">Đăng Ký</button>
      </form>
    </div> -->

    <!-- Overlay -->
    <!-- <div class="overlay-container">
      <div class="overlay">
        <div class="overlay-panel overlay-left">
          <h2>Xin Chào Bạn!</h2>
          <p>Đã có tài khoản? Vui lòng đăng nhập!</p>
          <button class="ghost" @click="isSignUp = false">Đăng Nhập</button>
        </div>
        <div class="overlay-panel overlay-right">
          <h2>Xin Chào Bạn!</h2>
          <p>Vui lòng đăng ký thông tin cá nhân của bạn</p>
          <button class="ghost" @click="isSignUp = true">Đăng Ký</button>
        </div>
      </div>
    </div> -->
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: 'SignIn',
  setup() {
    return {
      isSignUp: false,
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
          password: this.password
        })
        console.log('Login with:', response.data)
        if (response.data) {
          localStorage.setItem('user', JSON.stringify(response.data))
          this.$router.push({ name: 'Home' })
        } else {
          console.log('Login failed');
        }
      } catch (error) {
        console.error('Error during login:', error);
      }
    },
    handleRegister() {
      console.log('Register with:', this.username, this.MaNV, this.password)
    }

  }
}

</script>
<style scoped>
/* General styles */
* {
  box-sizing: border-box;
}

body {
  font-family: 'Poppins', sans-serif;
  background-color: #f6f5f7;
  margin: 0;
  padding: 0;
}

.container {
  background: #fff;
  border-radius: 10px;
  box-shadow: 0 14px 28px rgba(0, 0, 0, 0.25),
    0 10px 10px rgba(0, 0, 0, 0.22);
  position: relative;
  overflow: hidden;
  width: 768px;
  max-width: 100%;
  min-height: 480px;
  margin: 50px auto;
}

.form-container {
  position: absolute;
  top: 0;
  height: 100%;
  transition: all 0.6s ease-in-out;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  padding: 0 50px;
  text-align: center;
}

.sign-in-container {
  left: 0;
  width: 50%;
  z-index: 2;
}

.sign-up-container {
  left: 0;
  width: 50%;
  opacity: 0;
  z-index: 1;
}

.container.sign-up-mode .sign-in-container {
  transform: translateX(100%);
  opacity: 0;
  z-index: 1;
}

.container.sign-up-mode .sign-up-container {
  transform: translateX(100%);
  opacity: 1;
  z-index: 2;
}

/* Overlay styles */
.overlay-container {
  position: absolute;
  top: 0;
  left: 50%;
  width: 50%;
  height: 100%;
  overflow: hidden;
  transition: transform 0.6s ease-in-out;
  z-index: 100;
}

.overlay {
  background: linear-gradient(to right, #f39c12, #f1c40f);
  background-repeat: no-repeat;
  background-size: cover;
  color: white;
  position: relative;
  left: -100%;
  height: 100%;
  width: 200%;
  transform: translateX(0);
  transition: transform 0.6s ease-in-out;
}

.container.sign-up-mode .overlay-container {
  transform: translateX(-100%);
}

.container.sign-up-mode .overlay {
  transform: translateX(50%);
}

.overlay-panel {
  position: absolute;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 0 40px;
  text-align: center;
  top: 0;
  height: 100%;
  width: 50%;
  transition: all 0.6s ease-in-out;
}

.overlay-left {
  transform: translateX(-20%);
  left: 0;
}

.overlay-right {
  right: 0;
  transform: translateX(0);
}

.container.sign-up-mode .overlay-left {
  transform: translateX(0);
}

.container.sign-up-mode .overlay-right {
  transform: translateX(20%);
}

/* Inputs & buttons */
form {
  background: #ffffff;
  display: flex;
  flex-direction: column;
  padding: 0 20px;
}

input {
  background-color: #eee;
  color: #000000;
  border: none;
  padding: 12px 15px;
  margin: 8px 0;
  width: 100%;
  border-radius: 5px;
}

button {
  border-radius: 20px;
  border: 1px solid #f39c12;
  background-color: #f39c12;
  color: #ffffff;
  font-size: 14px;
  font-weight: bold;
  padding: 12px 45px;
  margin-top: 10px;
  letter-spacing: 1px;
  text-transform: uppercase;
  transition: transform 80ms ease-in;
  cursor: pointer;
}

button:hover {
  background-color: #e67e22;
  border-color: #e67e22;
}

button:active {
  transform: scale(0.95);
}

button.ghost {
  background-color: transparent;
  border-color: #ffffff;
  color: #ffffff;
}

button.ghost:hover {
  background-color: rgba(255, 255, 255, 0.1);
}
</style>
