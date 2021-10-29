<template>
  <div class="navbar">
    <div class="navbar-list">
      <ul id="menu-list" style="list-style-type: none">
        <li v-show="a[0]">
          <button
            class="list-button"
            :class="{
              minisize: changewidth,
            }"
            @click="routing('UserManager')"
          >
            <img
              class="icon-list"
              src="../../../assets/icon/dic-employee.png"
            /><span :class="{ hide: minispan }">Quản lý người dùng</span>
          </button>
        </li>
        <li v-show="a[1]">
          <button
            class="list-button"
            :class="{
              minisize: changewidth,
            }"
            @click="routing('VaccinationManager')"
          >
            <img
              class="icon-list"
              src="../../../assets/img/vaccination.png"
            /><span :class="{ hide: minispan }">Thông tin tiêm chủng</span>
          </button>
        </li>
        <li v-show="a[2]">
          <button
            class="list-button"
            :class="{
              minisize: changewidth,
            }"
            @click="routing('VaccineManager')"
          >
            <img class="icon-list" src="../../../assets/img/vaccine.png" /><span
              :class="{ hide: minispan }"
              >Thông tin Vaccine</span
            >
          </button>
        </li>
        <li v-show="a[3]">
          <button
            class="list-button"
            :class="{
              minisize: changewidth,
            }"
            @click="routing('AppointmentManager')"
          >
            <img
              class="icon-list"
              src="../../../assets/img/calendar.jpg"
            /><span :class="{ hide: minispan }">Quản lý lịch tiêm</span>
          </button>
        </li>
        <li v-show="a[4]">
          <button
            class="list-button"
            :class="{
              minisize: changewidth,
            }"
            @click="routing('PersonalInformation')"
          >
            <img
              class="icon-list"
              src="../../../assets/img/default-avatar.jpg"
            /><span :class="{ hide: minispan }">Thông tin cá nhân</span>
          </button>
        </li>
        <li>
          <button
            class="list-button"
            :class="{
              minisize: changewidth,
            }"
            @click="logOut"
          >
            <img class="icon-list" src="../../../assets/icon/exit.png" /><span
              :class="{ hide: minispan }"
              >Đăng xuất</span
            >
          </button>
        </li>
      </ul>
      <button type="button" accesskey="m" id="mini" @click="changeNavBar">
        <i id="icon" :class="anglechange"></i>
      </button>
    </div>
  </div>
</template>
<style scoped>
@import "./../../css/page/navbar.css";
</style>
<script>
export default {
  name: "TheNavBar",
  data() {
    return {
      changewidth: false,
      anglechange: "fa fa-angle-left",
      minispan: false,
      userId: this.$cookies.get("id"),
    };
  },
  props:{
    page:{},
    a:{
      type: Array,
    },
  },
  methods: {
    routing(tab) {
      this.$router.push({ path: `/${this.page}/${this.userId}/${tab}` });
    },
    logOut() {
      this.$cookies.keys().forEach((cookie) => this.$cookies.remove(cookie));
      sessionStorage.setItem("authentication", false);
      this.$router.push("/");
    },
    changeNavBar: function () {
      this.changewidth = !this.changewidth;
      this.minispan = !this.minispan;
      if (this.anglechange == "fa fa-angle-left") {
        this.anglechange = "fa fa-angle-right";
      } else this.anglechange = "fa fa-angle-left";
      this.$emit("changeWidthContent", this.minispan);
    },
  },
};
</script>
