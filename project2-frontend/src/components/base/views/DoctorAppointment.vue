<template>
  <div class="content">
    <div class="headcontent">
      <p id="title-content">Quản lý lịch tiêm</p>
    </div>
    <div id="myModal" class="modal" v-show="show_modal">
      <div class="modal-content">
        <span class="close" accesskey="c" @click="show_modal = false"
          >&times;</span
        >
        <BaseFormAppointment
          id="addform"
          :appointmentDetail="appointmentData"
        />
        <div class="endform">
          <button
            class="btnend"
            style="background-color: #e5e5e5; border: none"
            id="cancelform"
            type="button"
            @click="show_modal = !show_modal"
          >
            Hủy
          </button>
          <button
            class="btnend"
            style="background-color: #019160; color: white"
            id="save-employee"
            type="button"
            @click="addNewAppointment"
          >
            <i
              class="fa fa-floppy-o"
              style="margin-right: 8px; font-size: 16px"
            ></i
            >Lưu
          </button>
        </div>
      </div>
    </div>
    <div class="vaccination">
      <div class="vaccination-header" style="display: flex">
        <span class="title" style="margin-top: 12px">Danh sách người dùng</span>
        <IconButton
          icon="icon/add.png"
          title="Thêm lịch tiêm"
          style="margin-left: auto"
          :action="addApointment"
        />
      </div>
      <strong id="alert" v-show="dataUsers.length == 0"
        >Hiện không có dữ liệu</strong
      >
      <div id="tablecontainer" v-show="dataUsers.length != 0">
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
              <th>Vai trò</th>
            </tr>
          </thead>
          <tbody id="tbodytable">
            <tr
              v-for="(data, index) in dataUsers"
              :key="index"
              @click="
                checked.includes(data.userId)
                  ? checked.pop(data.userId)
                  : checked.push(data.userId)
              "
            >
              <td>
                <input type="checkbox" v-model="checked" :value="data.userId" />
              </td>
              <td scope="row">{{ data.fullName }}</td>
              <td>{{ data.gender }}</td>
              <td>{{ $filters.formatDateOfBirth(data.dateOfBirth) }}</td>
              <td>{{ data.phoneNumber }}</td>
              <td>{{ data.address }}</td>
              <td>{{ data.authority_Name }}</td>
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
                {{ timeFromNow(data.vaccination_appointment_date) }}
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
import IconButton from "./../../base/common/BaseIconButton.vue";
import BaseFormAppointment from "./../../base/common/BaseFormAppointment.vue";
export default {
  components: {
    IconButton,
    BaseFormAppointment,
  },
  data() {
    return {
      select_all: false,
      checked: [],
      dataAppointments: [],
      dataUsers: [],
      show_modal: false,
      appointmentData: {},
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
        for (let i in this.dataUsers) {
          this.checked.push(this.dataUsers[i].userId);
        }
      }
    },
    async addApointment() {
      if (this.checked.length == 0) {
        alert("Vui lòng chọn đối tượng tiêm!");
      } else {
        this.show_modal = this.show_modal ? false : true;
        await this.getNewUID();
        this.appointmentData = { vaccination_appointmentId: this.newUID };
      }
    },
    async addNewAppointment() {
      for (let i in this.checked) {
        this.appointmentData.userId = this.checked[i];
        await this.getNewUID();
        this.appointmentData.vaccination_appointmentId = this.newUID;
        await this.axios
          .post(
            "https://localhost:44336/api/v1/vaccinationappointment",
            JSON.stringify(this.appointmentData),
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
            alert("Đã xảy ra lỗi");
            return;
          });
      }
      alert("Thêm lịch tiêm thành công!");
      this.show_modal = false;
      this.checked = [];
    },
    async loadData() {
      await this.axios
        .get("https://localhost:44336/api/v1/users")
        .then((res) => {
          this.dataUsers = res.data;
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