<template>
  <div class="addform">
    <div class="contentform">
      <p id="inforemployee" style="text-align: left">THÔNG TIN LỊCH TIÊM</p>
      <div class="avatarform">
        <img id="defaultavatar" src="./../../../assets/img/vaccination.png" />
      </div>
    </div>
    <div class="form">
      <p style="text-align: left; font-family: 'regular'">
        A. THÔNG TIN CHUNG:
      </p>
      <div id="linediv"></div>
      <BaseInput
        titleinput="Mã lịch tiêm"
        typeofinput="text"
        placeholder="Mã lịch tiêm"
        v-model="data.vaccination_appointmentId"
        :required="true"
        validated="1"
      />
      <BaseInput
        :validated="isEdit"
        titleinput="Ngày hẹn tiêm"
        typeofinput="date"
        placeholder="Ngày hẹn tiêm"
        v-model="data.vaccination_appointment_date"
      />
      <BaseComboBox
        :customSelect="data.vaccine_name"
        @select="selected($event)"
        itemName="vaccines"
        nameValue="vaccine_name"
        type="select"
        nameText="vaccine_name"
        titleinput="Loại vaccine"
        :required="true"
      />
      <BaseInput
        :validated="isEdit"
        titleinput="Tình trạng sức khỏe"
        typeofinput="text"
        placeholder="tình trạng sức khỏe"
        v-model="data.health_condition"
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
    appointmentDetail: {
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
    appointmentDetail: function () {
      this.data = this.appointmentDetail;
      this.data.vaccination_appointment_date = this.formatDateOfBirth(
        this.data.vaccination_appointment_date
      );
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
      .get("https://localhost:44336/api/v1/vaccine")
      .then((res) => {
        let vaccines = [];
        let data = res.data;
        if (data) {
          data.map((item) => {
            let tmp = {};
            tmp["value"] = item.vaccine_Name;
            tmp["text"] = item.vaccine_Name;
            vaccines.push(tmp);
          });
        }
        localStorage.setItem("vaccines", JSON.stringify(vaccines));
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