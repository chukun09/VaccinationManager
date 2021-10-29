<template>
  <div class="addform">
    <div class="contentform">
      <p id="inforemployee" style="text-align: left">THÔNG TIN VACCINE</p>
      <div class="avatarform">
        <img
          id="defaultavatar"
          src="./../../../assets/img/vaccine.png"
        />
      </div>
    </div>
    <div class="form">
      <p style="text-align: left; font-family: 'regular'">
        A. THÔNG TIN CHUNG:
      </p>
      <div id="linediv"></div>
      <BaseInput
        titleinput="Mã Vaccine"
        typeofinput="text"
        placeholder="Mã Vaccine"
        v-model="data.vaccineId"
        :required="true"
        validated="1"
      />
      <!-- EmployeeCode -->
      <BaseInput
        titleinput="Tên Vaccine"
        typeofinput="text"
        placeholder="Tên Vaccine"
        v-model="data.vaccine_Name"
        :required="true"
        :validated="isEdit"
      />
      <!-- Date of Birth -->
      <BaseInput
        :validated="isEdit"
        titleinput="Số lượng"
        typeofinput="number"
        v-model="data.vaccine_Amount"
      />
      <!-- Identity Number -->
      <BaseInput
        :validated="isEdit"
        titleinput="Xuất xứ"
        typeofinput="text"
        placeholder="Xuất xứ"
        v-model="data.vaccine_Origin"
      />
      <!-- Identity Place -->
      <BaseInput
        :validated="isEdit"
        titleinput="Ngày sản xuất"
        typeofinput="date"
        placeholder="Ngày sản xuất"
        v-model="data.date_Of_Manufacture"
      />
    </div>
  </div>
</template>

<script>
import dayjs from "dayjs";
import BaseInput from "./BaseInput.vue";
export default {
  props: {
    isEdit: {
      default: "0",
    },
    vaccineDetail: {
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
  },
  watch: {
    vaccineDetail: function () {
      this.data = this.vaccineDetail;
      this.data.date_Of_Manufacture = this.formatDateOfBirth(this.data.date_Of_Manufacture);
    },
  },
  methods: {
    formatDateOfBirth: function (date) {
      let dateOrigin = dayjs(date).format("YYYY-MM-DD");
      return dateOrigin;
    },
  },
};
</script>

<style scoped>
@import url("./../../css/common/addform.css");
</style>