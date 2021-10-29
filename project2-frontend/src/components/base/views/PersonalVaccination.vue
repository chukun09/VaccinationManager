<template>
  <div class="content">
    <div class="headcontent">
      <p id="title-content">Thông tin tiêm chủng cá nhân</p>
    </div>
    <div class="vaccination">
      <p class="title">Danh sách lịch đã tiêm</p>
      <strong id="alert" v-show="dataVaccinations.length == 0"
        >Hiện không có dữ liệu</strong
      >
      <div id="tablecontainer" v-show="dataVaccinations.length != 0">
        <table class="table table-responsive w-100 d-block d-md-table">
          <thead class="thead-inverse">
            <tr>
              <th>Họ và tên</th>
              <th>Giới tính</th>
              <th>Ngày sinh</th>
              <th>Điện thoại</th>
              <th>Địa chỉ</th>
              <th>Loại vaccine</th>
              <th>Ngày tiêm</th>
              <th>Tình trạng sức khỏe</th>
            </tr>
          </thead>
          <tbody id="tbodytable">
            <tr
              v-for="(data, index) in dataVaccinations"
              :key="index"
              @dblclick="editHealthCondition(data)"
            >
              <td scope="row">{{ data.fullname }}</td>
              <td>{{ data.gender }}</td>
              <td>{{ $filters.formatDateOfBirth(data.dateofbirth) }}</td>
              <td>{{ data.phonenumber }}</td>
              <td>{{ data.address }}</td>
              <td>{{ data.vaccine_name }}</td>
              <td>{{ $filters.formatDateOfBirth(data.vaccination_date) }}</td>
              <td>{{ data.health_condition }}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
    <div class="appointment">
      <p class="title">Danh sách lịch hẹn</p>
      <strong id="alert" v-show="dataAppointments.length === 0"
        >Hiện không có dữ liệu</strong
      >
      <div id="tablecontainer" v-show="dataAppointments.length != 0">
        <table class="table table-responsive w-100 d-block d-md-table">
          <thead class="thead-inverse">
            <tr>
              <th>Họ và tên</th>
              <th>Giới tính</th>
              <th>Ngày sinh</th>
              <th>Điện thoại</th>
              <th>Địa chỉ</th>
              <th>Loại vaccine</th>
              <th>Ngày hẹn tiêm</th>
              <th>Tình trạng sức khỏe trước tiêm</th>
              <th>Thời gian còn lại</th>
            </tr>
          </thead>
          <tbody id="tbodytable">
            <tr v-for="(data, index) in dataAppointments" :key="index">
              <td scope="row">{{ data.fullname }}</td>
              <td>{{ data.gender }}</td>
              <td>{{ $filters.formatDateOfBirth(data.dateofbirth) }}</td>
              <td>{{ data.phonenumber }}</td>
              <td>{{ data.address }}</td>
              <td>{{ data.vaccine_name }}</td>
              <td>
                {{
                  $filters.formatDateOfBirth(data.vaccination_appointment_date)
                }}
              </td>
              <td>{{ data.health_condition }}</td>
              <td style="text-align: center; color: red">
                {{ this.timeFromNow(data.vaccination_appointment_date) }}
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>
<script>
import dayjs from "dayjs";
export default {
  data() {
    return {
      dataAppointments: [],
      dataVaccinations: [],
      id: this.$cookies.get("id"),
    };
  },
  created() {
    this.loadData();
  },
  methods: {
    formatDateOfBirth: function (date) {
      let dateOrigin = dayjs(date).format("YYYY-MM-DD");
      return dateOrigin;
    },
    timeFromNow(date) {
      let currentDate = new Date();
      currentDate = this.formatDateOfBirth(currentDate);
      let remainDays = dayjs(date).diff(currentDate, "day");
      if (remainDays >= 0) {
        return `Còn lại ${remainDays} ngày`;
      } else return `Đã quá ngày hẹn ${Math.abs(remainDays)} ngày`;
    },
    async loadData() {
      await this.axios
        .get("https://localhost:44336/api/v1/vaccinationinformation/" + this.id)
        .then((res) => {
          this.dataVaccinations = res.data;
        })
        .catch((e) => {
          console.log(e);
        });
      await this.axios
        .get(
          "https://localhost:44336/api/v1/vaccinationappointment/user/" +
            this.id
        )
        .then((res) => {
          this.dataAppointments = res.data;
        })
        .catch((e) => {
          console.log(e);
        });
    },
    async editHealthCondition(data) {
      let store = {
        userId: data.userId,
        vaccination_informationId: data.vaccination_informationId,
        vaccination_date: data.vaccination_date,
        vaccine_name: data.vaccine_name,
        health_condition: data.health_condition,
      };
      let updateHealth = prompt("Mời bạn cập nhật tình trạng sức khỏe!");
      if (updateHealth === null) {
        return;
      } else {
        store.health_condition = updateHealth;
        await this.axios
          .put(
            "https://localhost:44336/api/v1/vaccinationinformation/" +
              store.vaccination_informationId,
            JSON.stringify(store),
            {
              headers: {
                "Content-Type": "application/json",
              },
            }
          )
          .then(() => {
            alert("Thông tin sức khỏe của bạn đã được cập nhật!");
            this.loadData();
          })
          .catch((e) => {
            console.log(e);
          });
      }
    },
  },
};
</script>

<style>
#title-content {
  font-size: 30px;
  font-family: "googleblack";
  margin-left: 20px;
}
.headcontent {
  margin-top: 20px;
}
.title {
  font-size: 20px;
  font-family: "regular";
  margin-left: 20px;
}
th,
td {
  text-align: center;
}
.vaccination,
.appointment {
  height: 350px;
  position: relative;
}
#alert {
  position: absolute;
  left: 42%;
  top: 150px;
  z-index: 1;
}
</style>