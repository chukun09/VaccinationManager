<template>
  <div class="personalform">
    <div class="addform">
      <div class="contentform">
        <p id="inforemployee" style="text-align: left">THÔNG TIN NGƯỜI DÙNG</p>
        <div class="avatarform">
          <img
            id="defaultavatar"
            src="./../../../assets/img/default-avatar.jpg"
          />
          <p id="imgpara">
            (vui lòng chọn ảnh có dạng .jpg, .jpeg, .png, .gif.)
          </p>
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
          titleinput="Ngày sinh"
          typeofinput="date"
          v-model="data.dateOfBirth"
        />
        <!-- Identity Number -->
        <BaseInput
          titleinput="Giới tính"
          typeofinput="text"
          placeholder="Giới tính"
          v-model="data.gender"
        />
        <!-- Identity Place -->
        <BaseInput
          titleinput="Tài khoản"
          typeofinput="text"
          placeholder="Tài khoản"
          v-model="data.account"
        />
      </div>
      <div class="form2" style="margin-top: 117px">
        <!-- Full Name -->
        <BaseInput
          placeholder="Họ và Tên"
          titleinput="Họ và tên"
          typeofinput="text"
          v-model="data.fullName"
          :required="true"
        />
        <BaseInput
          titleinput="Số điện thoại"
          typeofinput="number"
          v-model="data.phoneNumber"
          placeholder="Số điện thoại"
        />
        <BaseInput
          titleinput="Địa chỉ"
          typeofinput="input"
          v-model="data.address"
          placeholder="Địa chỉ"
        />
        <!-- Phone Number -->
        <BaseInput
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
    <div class="endform">
      <button
        class="btnend"
        style="background-color: #e5e5e5; border: none"
        id="cancelform"
        type="button"
        @click="cancel()"
      >
        Hủy
      </button>
      <button
        class="btnend"
        style="background-color: #019160; color: white"
        id="save-employee"
        @click="editUser()"
        type="button"
      >
        <i class="fa fa-floppy-o" style="margin-right: 8px; font-size: 16px"></i
        >Lưu
      </button>
    </div>
  </div>
</template>

<script>
import dayjs from "dayjs";
import BaseInput from "./../common/BaseInput.vue";
import BaseComboBox from "./../common/BaseCombobox.vue";
export default {
  data() {
    return {
      data: {},
    };
  },
  components: {
    BaseInput,
    BaseComboBox,
  },
  created() {
    this.getPersonalInformation(this.$cookies.get("id"));
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
  methods: {
    formatDateOfBirth: function (date) {
      let dateOrigin = dayjs(date).format("YYYY-MM-DD");
      return dateOrigin;
    },
    selected(event) {
      this.data[event.name] = event.value;
    },
    cancel() {
      if (confirm("Bạn có chắc muốn hủy bỏ thay đổi ?"))
        this.getPersonalInformation(this.$cookies.get("id"));
      return;
    },
    async editUser(){
         await this.axios
        .put(
          "https://localhost:44336/api/v1/users/" + this.$cookies.get("id"),
          JSON.stringify(this.data),
          {
            headers: {
              Authorization: `Bearer ${this.$cookies.get("JWT")}`,
              "Content-Type": "application/json",
            },
          }
        )
        .then(() => {
          alert("Thay đổi thông tin người dùng thành công! Vui lòng đăng nhập lại!");
          this.$router.push('/');
        })
        .catch((e) => {
          console.error(e);
        });
    },
    async getPersonalInformation(id) {
      await this.axios
        .get("https://localhost:44336/api/v1/users/" + id)
        .then((res) => {
          res.data.dateOfBirth = this.formatDateOfBirth(res.data.dateOfBirth);
          this.data = res.data;
          console.log(this.data);
        })
        .catch((e) => {
          console.log(e);
        });
    },
  },
};
</script>

<style scoped>
@import url("./../../css/common/addform.css");
.personalform {
  display: inline-block;
  width: 100%;
}
.addform {
  height: 83%;
  margin-left: 300px;
}
</style>