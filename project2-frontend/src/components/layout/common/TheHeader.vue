<template>
  <div class="header">
    <div class="left-header">
      <img id="menu" src="../../../assets/img/toggle.png" alt="menu" />
      <img
        id="logo"
        src="../../../assets/img/vncdc.gov.jpg"
        alt="cukcuk-logo"
      />
    </div>
    <div class="center-header">
      <div id="namecompany">
        <p>Website quản lý thông tin tiêm chủng</p>
      </div>
    </div>
    <div class="right-header">
      <img
        id="avatar"
        src="../../../assets/icon/avatar-default.png"
        alt="avatar"
      />
      <span id="name">{{ $cookies.get("fullname") }}</span>
      <img id="option" src="../../../assets/icon/option.png" alt="option" />
    </div>
  </div>
</template>
<style scoped>
@import "./../../css/page/header.css";
</style>
<script>
export default {
  name: "TheHeader",
};
</script>
