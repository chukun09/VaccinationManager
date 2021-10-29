<template>
  <div class="addform">
    <div class="contentform">
      <p id="inforemployee" style="text-align: left">THÔNG TIN NGƯỜI DÙNG</p>
      <div class="avatarform">
        <img
          id="defaultavatar"
          src="./../../../assets/img/default-avatar.jpg"
        />
        <p id="imgpara">(vui lòng chọn ảnh có dạng .jpg, .jpeg, .png, .gif.)</p>
      </div>
    </div>
    <div class="form">
      <p style="text-align: left; font-family: 'regular'">
        A. THÔNG TIN CHUNG:
      </p>
      <div id="linediv"></div>
      <!-- EmployeeCode -->
      <BaseInput
        titleinput="UID"
        typeofinput="text"
        placeholder="Mã người dùng"
        v-model="data.userId"
        :required="true"
        validated="1"
      />
      <!-- Date of Birth -->
      <BaseInput
        :validated="isEdit"
        titleinput="Ngày sinh"
        typeofinput="date"
        v-model="data.dateOfBirth"
      />
      <!-- Identity Number -->
      <BaseInput
        :validated="isEdit"
        titleinput="Giới tính"
        typeofinput="text"
        placeholder="Giới tính"
        v-model="data.gender"
      />
      <!-- Identity Place -->
      <BaseInput
        :validated="isEdit"
        titleinput="Tài khoản"
        typeofinput="text"
        placeholder="Tài khoản"
        v-model="data.account"
      />
    </div>
    <div class="form2" style="margin-top: 117px">
      <!-- Full Name -->
      <BaseInput
        :validated="isEdit"
        placeholder="Họ và Tên"
        titleinput="Họ và tên"
        typeofinput="text"
        v-model="data.fullName"
        :required="true"
      />
      <BaseInput
        :validated="isEdit"
        titleinput="Số điện thoại"
        typeofinput="number"
        v-model="data.phoneNumber"
        placeholder="Số điện thoại"
      />
      <BaseInput
        :validated="isEdit"
        titleinput="Địa chỉ"
        typeofinput="input"
        v-model="data.address"
        placeholder="Địa chỉ"
      />
      <!-- Phone Number -->
      <BaseInput
        :validated="isEdit"
        titleinput="Mật khẩu"
        placeholder="Mật khẩu"
        typeofinput="password"
        v-model="data.password"
        required="true"
      />
      <BaseComboBox
        :customSelect="data.authority_Name"
        @select="selected($event)"
        itemName="authorities"
        nameValue="authority_Name"
        type="select"
        nameText="authority_Name"
        defaultValue="Quản trị viên"
        titleinput="Vai trò"
        :required="true"
      />
    </div>
  </div>
</template>

<script>
import dayjs from "dayjs";
import BaseInput from "./BaseInput.vue";
import BaseComboBox from "./BaseCombobox.vue";
export default {
  props: {
    isEdit: {
      default: "0",
    },
    userDetail: {
      default: {},
    },
  },
  data() {
    return {
      data: {},
    };
  },
  components: {
    BaseInput,
    BaseComboBox,
  },
  watch: {
    userDetail: function () {
      this.data = this.userDetail;
      this.data.dateOfBirth = this.formatDateOfBirth(this.data.dateOfBirth);
    },
  },
  methods: {
    selected(event) {
      this.data[event.name] = event.value;
      
    },
    formatDateOfBirth: function (date) {
      let dateOrigin = dayjs(date).format("YYYY-MM-DD");
      return dateOrigin;
    },
  },
  created() {
    this.axios
      .get("https://localhost:44336/api/v1/Authority")
      .then((res) => {
        let authorities = [];
        let data = res.data;
        if (data) {
          data.map((item) => {
            let tmp = {};
            tmp["value"] = item.name;
            tmp["text"] = item.name;
            authorities.push(tmp);
          });
        }
        localStorage.setItem("authorities", JSON.stringify(authorities));
      })
      .catch((e) => {
        console.log(e);
      });
  },
};
</script>

<style scoped>
@import url("./../../css/common/addform.css");
</style>