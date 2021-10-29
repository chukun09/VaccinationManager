<template>
  <div class="content">
    <div class="headcontent">
      <p id="title-content">Quản lý lịch tiêm</p>
      <IconButton
        icon="icon/check-solid.svg"
        title="Xác nhận tiêm"
        style="margin-left: auto"
        :action="confirmAppointments"
      />
    </div>
    <div class="appointment">
      <div class="vaccination-header" style="display: flex">
        <span class="title" style="margin-top: 12px">Danh sách lịch hẹn</span>
        <button
          id="delete-employee"
          type="button"
          style="margin-left: auto"
          @click="deleteAppointments"
        >
          <img
            id="delete"
            style="height: 20px; width: 20px"
            src="../../../assets/icon/delete.png"
          />
        </button>
        <button id="btnrefresh" type="button">
          <img id="refresh" src="../../../assets/icon/refresh.png" />
        </button>
      </div>
      <strong id="alert" v-show="dataAppointments.length === 0"
        >Hiện không có dữ liệu</strong
      >
      <div id="tablecontainer" v-show="dataAppointments.length != 0">
        <table class="table table-responsive w-100 d-block d-md-table">
          <thead class="thead-inverse">
            <tr>
              <th>
                <input type="checkbox" @click="select" v-model="select_all" />
              </th>
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
            <tr
              v-for="(data, index) in dataAppointments"
              :key="index"
              @click="
                checked.includes(data.vaccination_appointmentId)
                  ? checked.pop(data.vaccination_appointmentId)
                  : checked.push(data.vaccination_appointmentId)
              "
            >
              <td>
                <input
                  type="checkbox"
                  v-model="checked"
                  :value="data.vaccination_appointmentId"
                />
              </td>
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
                {{ timeFromNow(data.vaccination_appointment_date) }}
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
    <div class="vaccination">
      <p class="title" style="margin-top: 12px">
        Danh sách thông tin tiêm chủng
      </p>
      <strong id="alert" v-show="dataVaccinations.length === 0"
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
              <th>Tình trạng sức khỏe sau tiêm</th>
            </tr>
          </thead>
          <tbody id="tbodytable">
            <tr v-for="(data, index) in dataVaccinations" :key="index">
              <td scope="row">{{ data.fullname }}</td>
              <td>{{ data.gender }}</td>
              <td>{{ $filters.formatDateOfBirth(data.dateofbirth) }}</td>
              <td>{{ data.phonenumber }}</td>
              <td>{{ data.address }}</td>
              <td>{{ data.vaccine_name }}</td>
              <td>
                {{ $filters.formatDateOfBirth(data.vaccination_date) }}
              </td>
              <td>{{ data.health_condition }}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>
<script>
import dayjs from "dayjs";
import IconButton from "./../common/BaseIconButton.vue";
export default {
  components: {
    IconButton,
  },
  data() {
    return {
      select_all: false,
      checked: [],
      dataAppointments: [],
      dataVaccinations: [],
      appointment: {},
      newUID: "",
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
    select() {
      this.checked = [];
      if (!this.select_all) {
        for (let i in this.dataAppointments) {
          this.checked.push(this.dataAppointments[i].vaccination_appointmentId);
        }
      }
    },
    async deleteAppointments() {
      if (this.checked.length == 0) {
        alert("Vui lòng chọn lịch muốn xóa!");
      } else {
        if (confirm("Bạn có muốn xóa những lịch hẹn đã chọn ?")) {
          for (let i in this.checked) {
            await this.axios
              .delete(
                "https://localhost:44336/api/v1/vaccinationappointment/" +
                  this.checked[i]
              )
              .then(() => {
                this.loadData();
              })
              .catch((e) => {
                console.log(e);
                alert("Đã xảy ra lỗi vui lòng kiểm tra lại!");
                return;
              });
          }
          alert("Xóa lịch hẹn thành công!");
        } else return;
      }
    },
    async loadData() {
      await this.axios
        .get("https://localhost:44336/api/v1/vaccinationinformation")
        .then((res) => {
          this.dataVaccinations = res.data;
        })
        .catch((e) => {
          console.log(e);
        });
      await this.axios
        .get("https://localhost:44336/api/v1/vaccinationappointment")
        .then((res) => {
          this.dataAppointments = res.data;
        })
        .catch((e) => {
          console.log(e);
        });
    },
    async confirmAppointments() {
      if (this.checked.length == 0) {
        alert("Vui lòng chọn lịch hẹn!");
      } else {
        if (
          confirm(
            "Bạn có muốn xác nhận rằng những lịch đã chọn là đã được tiêm rồi không ?"
          )
        ) {
          for (let i in this.checked) {
           await this.getNewUID();
            await this.axios
              .get(
                "https://localhost:44336/api/v1/vaccinationappointment/" +
                  this.checked[i]
              )
              .then((res) => {
                this.appointment = {
                  vaccination_informationId: this.newUID,
                  vaccination_date: this.formatDateOfBirth(
                    res.data.vaccination_appointment_date
                  ),
                  vaccine_name: res.data.vaccine_name,
                  userId: res.data.userId,
                  health_condition: "Hiện tại bình thường",
                };
                console.log(this.appointment);
              })
              .catch((e) => {
                console.log(e);
                alert("Đã xảy ra lỗi vui lòng kiểm tra lại!");
                return;
              });
            await this.axios
              .post(
                "https://localhost:44336/api/v1/vaccinationinformation",
                JSON.stringify(this.appointment),
                {
                  headers: {
                    Authorization: `Bearer ${this.$cookies.get("JWT")}`,
                    "Content-Type": "application/json",
                  },
                }
              )
              .then(() => {
                this.loadData();
              })
              .catch((e) => {
                console.log(e);
                alert("Đã xảy ra lỗi vui lòng kiểm tra lại!");
                return;
              });
            await this.axios
              .delete(
                "https://localhost:44336/api/v1/vaccinationappointment/" +
                  this.checked[i]
              )
              .then(() => {
                this.loadData();
              })
              .catch((e) => {
                console.log(e);
                alert("Đã xảy ra lỗi vui lòng kiểm tra lại!");
                return;
              });
          }
          alert("Xác nhận thành công!");
        } else return;
      }
    },
    async getNewUID() {
      await this.axios
        .get("https://localhost:44336/api/v1/users/GetNewUID", {
          headers: {
            Authorization: `Bearer ${this.$cookies.get("JWT")}`,
          },
        })
        .then((res) => {
          this.newUID = res.data;
        })
        .catch((error) => {
          alert(error);
        });
    },
  },
};
</script>
<style scoped>
.modal-content {
  width: 600px !important;
}
#title-content {
  font-size: 30px;
  font-family: "googleblack";
  margin-left: 20px;
  margin-bottom: 0px;
}
.headcontent {
  margin-top: 20px;
  display: flex;
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