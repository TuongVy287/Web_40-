<template>
    <article>
      <div class="container"
        :class="{ 'sign-up-active': view === 'signUp', 'forgot-password-active': view === 'forgotPassword' }">
        <div class="overlay-container" v-if="!isMobileView">
          <div class="overlay">
            <div class="overlay-left">
              <h2>Chào Mừng Trở Lại!</h2>
              <p>Vui lòng nhập thông tin cá nhân của bạn</p>
              <button class="invert" id="signIn" @click="switchToSignIn">Đăng nhập</button>
            </div>
            <div class="overlay-right">
              <h2>Xin Chào Bạn!</h2>
              <p>Vui lòng đăng ký thông tin cá nhân của bạn</p>
              <button class="invert" id="signUp" @click="switchToSignUp">Đăng ký</button>
            </div>
          </div>
        </div>

        <!-- Đăng Nhập -->
        <form class="sign-in" v-if="view === 'signIn'" @submit.prevent="attemptLogin">
          <h2>Đăng Nhập</h2>
          <div>Sử dụng tài khoản của bạn</div>
          <input type="email" v-model="email" placeholder="Email" />
          <input type="password" v-model="password" placeholder="Mật khẩu" />
          <a href="#" @click.prevent="switchToForgotPassword">Quên mật khẩu của bạn?</a>
          <button type="submit">Đăng nhập</button>
          <button v-if="isMobileView" class="switch-button" @click="switchToSignUp">Chưa có tài khoản? Đăng ký</button>
        </form>

        <!-- Đăng Ký -->
        <form class="sign-up" v-if="view === 'signUp'" @submit.prevent="attemptSignUp">
          <h2>Đăng Ký</h2>
          <div>Tạo tài khoản mới</div>
          <input type="email" v-model="newEmail" placeholder="Email" />
          <input type="password" v-model="newPassword" placeholder="Mật khẩu" />
          <button type="submit">Đăng ký</button>
          <button v-if="isMobileView" class="switch-button" @click="switchToSignIn">Đã có tài khoản? Đăng nhập</button>
        </form>

        <!-- Quên Mật Khẩu -->
        <form class="forgot-password" v-if="view === 'forgotPassword'" @submit.prevent="attemptPasswordReset">
          <h2>Quên Mật Khẩu</h2>
          <div>Nhập email và mật khẩu mới của bạn</div>
          <input type="email" v-model="resetEmail" placeholder="Email" />
          <input type="password" v-model="resetPassword" placeholder="Mật khẩu mới" />
          <button type="submit">Đặt lại mật khẩu</button>
          <button v-if="isMobileView" class="switch-button" @click="switchToSignIn">Đăng nhập</button>
        </form>
      </div>
    </article>
  </template>

  <script lang="ts">
  export default {
    data() {
      return {
        view: 'signIn',
        isMobileView: window.innerWidth <= 768,
        email: '',
        password: '',
        newEmail: '',
        newPassword: '',
        resetEmail: '',
        resetPassword: '',
        accounts: JSON.parse(localStorage.getItem('accounts'))
      };
    },
    methods: {
      switchToSignIn() {
        this.view = 'signIn';
      },
      switchToSignUp() {
        this.view = 'signUp';
      },
      switchToForgotPassword() {
        this.view = 'forgotPassword';
      },
      attemptLogin() {
        const account = this.accounts.find(
          (account) => account.email === this.email && account.password === this.password
        );
        if (account) {
          localStorage.setItem('loggedIn', "true");
          localStorage.setItem('userRole', account.type);
          alert(`Chào mừng ${account.email.split('@')[0]}!`);
          this.$router.push('/Dashboard'); // Chuyển hướng sau khi đăng nhập thành công
        } else {
          alert('Email hoặc mật khẩu không đúng.');
        }
      },
      attemptSignUp() {
        if (this.accounts.find((account) => account.email === this.newEmail)) {
          alert('Email đã tồn tại.');
        } else if (this.newPassword.length < 8) {
          alert('Mật khẩu phải đủ ít nhất 8 ký tự.');
        } else {
          const newAccount = { email: this.newEmail, password: this.newPassword, type: 3 };
          this.accounts.push(newAccount);
          localStorage.setItem('accounts', JSON.stringify(this.accounts));
          alert('Đăng ký thành công!');
          this.switchToSignIn();
        }
      },
      attemptPasswordReset() {
        const account = this.accounts.find((account) => account.email === this.resetEmail);
        if (!account) {
          alert('Email không tồn tại.');
        } else if (this.resetPassword.length < 8) {
          alert('Mật khẩu phải đủ ít nhất 8 ký tự.');
        } else {
          account.password = this.resetPassword;
          localStorage.setItem('accounts', JSON.stringify(this.accounts));
          alert('Đặt lại mật khẩu thành công!');
          this.switchToSignIn();
        }
      },
      handleResize() {
        this.isMobileView = window.innerWidth <= 768;
      }
    },
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
    @include overlays(-20%);
  }

  .overlay-right {
    @include overlays(0);
    right: 0;
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
    padding: 90px 60px;
    width: calc(50% - 120px);
    height: calc(100% - 180px);
    text-align: center;
    background: linear-gradient(to bottom, #ece2e2, #ffffff);
    transition: all .5s ease-in-out;

    div {
      font-size: 1rem;
    }

    input {
      background-color: #eee;
      border: none;
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

  .sign-in {
    left: 0;
    z-index: 2;
  }

  .sign-up {
    left: 0;
    z-index: 1;
    opacity: 0;
  }

  .forgot-password {
    left: 0;
    z-index: 1;
    opacity: 0;
  }

  .sign-up-active,
  .forgot-password-active {
    .sign-in {
      transform: translateX(100%);
    }

    .sign-up,
    .forgot-password {
      transform: translateX(100%);
      opacity: 1;
      z-index: 5;
      animation: show .5s;
    }

    .overlay-container {
      transform: translateX(-100%);
    }

    .overlay {
      transform: translateX(50%);
    }

    .overlay-left {
      transform: translateX(0);
    }

    .overlay-right {
      transform: translateX(20%);
    }
  }

  @keyframes show {
    0% {
      opacity: 0;
      z-index: 1;
    }

    49% {
      opacity: 0;
      z-index: 1;
    }

    50% {
      opacity: 1;
      z-index: 10;
    }
  }

  /* Responsive CSS for small screens */
  @media screen and (max-width: 768px) {
    .container {
      width: 100%;
      height: auto;
      max-width: none;
      min-height: 100vh;
    }

    .overlay-container {
      display: none;
    }

    form {
      position: relative;
      width: 90%;
      padding: 20px;
      height: auto;
    }

    button,
    input {
      width: 100%;
    }

    .sign-up-active,
    .forgot-password-active {
      .sign-in {
        transform: none;
      }

      .sign-up,
      .forgot-password {
        transform: none;
        opacity: 1;
      }
    }
  }
  </style>

