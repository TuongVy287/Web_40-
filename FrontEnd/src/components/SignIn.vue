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
    mounted() {
      window.addEventListener('resize', this.handleResize);
    },
    beforeDestroy() {
      window.removeEventListener('resize', this.handleResize);
    }
  };
  </script>

  <style lang="scss" scoped>
  .container {
    position: absolute; // Đảm bảo nó chiếm toàn bộ màn hình
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%); // Di chuyển về chính giữa
    width: 100%;
    max-width: 768px;
    min-height: 480px;
    border-radius: 10px;
    overflow: hidden;
    box-shadow: 0 15px 30px rgba(0, 0, 0, .2),
      0 10px 10px rgba(0, 0, 0, .2);
    background: linear-gradient(to bottom, #efefef, #ccc);

    display: flex;
    justify-content: center;
    align-items: center;
  }

  .overlay-container {
    position: absolute;
    top: 0;
    left: 50%;
    width: 50%;
    height: 100%;
    overflow: hidden;
    transition: transform .5s ease-in-out;
    z-index: 100;
  }

  .overlay {
    position: relative;
    left: -100%;
    height: 100%;
    width: 200%;
    background: linear-gradient(to bottom right, #4464c5, #4169E1);
    color: #fff;
    transform: translateX(0);
    transition: transform .5s ease-in-out;
  }

  @mixin overlays($property) {
    position: absolute;
    top: 0;
    display: flex;
    align-items: center;
    justify-content: center;
    flex-direction: column;
    padding: 70px 40px;
    width: calc(50% - 80px);
    height: calc(100% - 140px);
    text-align: center;
    transform: translateX($property);
    transition: transform .5s ease-in-out;
  }

  .overlay-left {
    @include overlays(0);
    left: 30px;
    top:30px;
  }

  .overlay-right {
    @include overlays(0);
    right: 30px;
        top:30px;

  }

  h2 {
    margin: 0;
  }

  p {
    margin: 20px 0 30px;
  }

  a {
    color: #222;
    text-decoration: none;
    margin: 15px 0;
    font-size: 1rem;
  }

  button {
    border-radius: 20px;
    border: 1px solid #d9dce7;
    background-color: #4169E1;
    color: #fff;
    font-size: 1rem;
    font-weight: bold;
    padding: 10px 40px;
    letter-spacing: 1px;
    text-transform: uppercase;
    cursor: pointer;
    transition: transform .1s ease-in;

    &:active {
      transform: scale(.9);
    }

    &:focus {
      outline: none;
    }
  }

  button.invert {
    background-color: transparent;
    border-color: #fff;
  }

  button.switch-button {
    margin-top: 20px;
    padding: 10px 20px;
    background-color: transparent;
    border: none;
    color: #4169E1;
    cursor: pointer;

    &:focus {
      outline: none;
    }
  }

  form {
    position: absolute;
    top: 0;
    display: flex;
    align-items: center;
    justify-content: center;
    flex-direction: column;
    width: calc(50% - 00px);
    height: calc(100% - 00px);
    text-align: center;
    background: linear-gradient(to bottom, #ece2e2, #ffffff);
    transition: all .5s ease-in-out;
  padding: 40px 30px;
  max-width: 400px;
  margin: auto;
  border-radius: 10px;
  box-shadow: 0 10px 25px rgba(0, 0, 0, 0.1);

    div {
      font-size: 1rem;
    }

    input {
      background-color: #eee;
      border: none;
      color: #000;
      padding: 8px 15px;
      margin: 6px 0;
      width: calc(100% - 30px);
      border-radius: 15px;
      border-bottom: 1px solid #ddd;
      box-shadow: inset 0 1px 2px rgba(0, 0, 0, .4),
        0 -1px 1px #fff,
        0 1px 0 #fff;
      overflow: hidden;

      &:focus {
        outline: none;
        background-color: #fff;
      }
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
