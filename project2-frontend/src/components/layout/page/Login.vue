<template>
  <div class="loginPage">
    <div class="container">
      <div class="d-flex justify-content-center h-100">
        <div class="card">
          <div class="card-header">
            <h3>Sign In</h3>
            <div class="d-flex justify-content-end social_icon">
              <span><i class="fab fa-facebook-square"></i></span>
              <span><i class="fab fa-google-plus-square"></i></span>
              <span><i class="fab fa-twitter-square"></i></span>
            </div>
          </div>
          <div class="card-body">
            <form>
              <div class="input-group form-group">
                <div class="input-group-prepend">
                  <span class="input-group-text"
                    ><i class="fas fa-user"></i
                  ></span>
                </div>
                <input
                  type="text"
                  v-model.trim="v$.account.$model"
                  class="form-control"
                  :class="{
                    'is-invalid': v$.account.$error,
                    'is-valid': !v$.account.$invalid,
                  }"
                  placeholder="username"
                />
              </div>
              <div class="input-group form-group">
                <div class="input-group-prepend">
                  <span class="input-group-text"
                    ><i class="fas fa-key"></i
                  ></span>
                </div>
                <input
                  type="password"
                  v-model.trim="v$.password.$model"
                  :class="{
                    'is-invalid': v$.password.$error,
                    'is-valid': !v$.password.$invalid,
                  }"
                  class="form-control"
                  placeholder="password"
                />
              </div>
              <div class="row align-items-center remember">
                <input type="checkbox" v-model="check" />Remember Me
              </div>
              <div class="form-group">
                <input
                  type="submit"
                  value="Login"
                  @click.prevent="login"
                  class="btn float-right login_btn"
                />
              </div>
            </form>
          </div>
          <div class="card-footer">
            <div class="d-flex justify-content-center links">
              Don't have an account?<a href="#">Sign Up</a>
            </div>
            <div class="d-flex justify-content-center">
              <a href="#">Forgot your password?</a>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<style scoped>
@import url("./../../css/page/Login.css");
.is-valid {
  border: 1px solid #33ff00 !important;
}
.is-invalid {
  border: 1px solid #ff0000 !important;
}
</style>
<script>
import useVuelidate from "@vuelidate/core";
import { required } from "@vuelidate/validators";
export default {
  setup() {
    return { v$: useVuelidate() };
  },
  created(){
    this.account = this.$cookies.get("username");
    this.password = this.$cookies.get("password");
    sessionStorage.setItem("authentication", false);
  },
  data() {
    return {
      check: false,
      account: "",
      password: "",
      role: "",
      userId: "",
    };
  },
  validations() {
    return {
      account: { required },
      password: { required },
    };
  },
  methods: {
    async login() {
      this.v$.$validate();
      if (!this.v$.$error) {
        await this.axios
          .get("https://localhost:44336/api/v1/users/Login", {
            params: {
              username: this.account,
              password: this.password,
            },
          })
          .then((response) => {
            this.$cookies.set("JWT", response.data);
            this.checkRoleLogin();
          })
          .catch(() => {
            alert("Sai tên đăng nhập hoặc mật khẩu. Vui lòng kiểm tra lại!");
            return;
          });
      } else alert("Form failed validation!");
    },
    async checkRoleLogin() {
      await this.axios
        .get("https://localhost:44336/api/v1/users/LoginRole", {
          headers: {
            Authorization: `Bearer ${this.$cookies.get("JWT")}`,
          },
        })
        .then((res) => {
          // this.$router.params.check = true;
          this.userId = res.data.id;
          this.role = res.data.role;
          this.$cookies.set("fullname", res.data.fullName);
          this.$cookies.set("id", res.data.id);
          sessionStorage.setItem("authentication", this.role);
          if (this.check) {
            this.$cookies.set("username", this.account);
            this.$cookies.set("password", this.password);
          }
          this.RoutingLogin();
        })
        .catch(() => {
          alert("Oops, Has an error please try again later!");
        });
    },
    RoutingLogin() {
      let role = this.role;
      if (role === "Bác sĩ") {
        this.$router.push({ path: `/DoctorPage/${this.userId}` });
      } else if (role == "Y tá") {
        this.$router.push({ path: `/NursePage/${this.userId}` });
      } else if (role == "Người dùng") {
        this.$router.push({ path: `/UserPage/${this.userId}` });
      } else this.$router.push({ path: `/AdminPage/${this.userId}` });
    },
  },
};
</script>
