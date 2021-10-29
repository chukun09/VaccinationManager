<template>
  <div class="content">
    <div class="head-content">
      <p id="employee-list">Danh sách lịch hẹn tiêm chủng</p>
    </div>
    <div class="finder">
      <div class="finder-findtext">
        <input
          accesskey="s"
          type="text"
          class="findtext"
          id="finder-findtext"
          placeholder="Tìm kiếm"
          onfocus="this.placeholder = ''"
          onblur="this.placeholder = 'Tìm kiếm'"
          style="width: 300px; height: 40px"
        />
        <div><i class="fa fa-times"></i></div>
      </div>
    </div>
    <div id="tablecontainer">
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
            <th>Kết quả khám sức khỏe</th>
          </tr>
        </thead>
        <tbody id="tbodytable">
          <tr
            v-for="(data, index) in dataTables"
            :key="index"
          >
            <td scope="row">{{ data.fullname }}</td>
            <td>{{ data.gender }}</td>
            <td>{{ $filters.formatDateOfBirth(data.dateofbirth) }}</td>
            <td>{{ data.phonenumber }}</td>
            <td>{{ data.address }}</td>
            <td>{{ data.vaccine_name }}</td>
            <td>{{ $filters.formatDateOfBirth(data.vaccination_appointment_date) }}</td>
            <td>{{ data.health_condition }}</td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="pagination">
      <ul id="listpage" style="list-style-type: none">
        <li class="list-page">
          <img src="../../../assets/icon/btn-firstpage.svg" alt="firstpage" />
        </li>
        <li class="list-page">
          <img src="../../../assets/icon/btn-prev-page.svg" alt="prevpage" />
        </li>
        <li class="list-page"><a class="page-link" href="#">1</a></li>
        <li class="list-page"><a class="page-link" href="#">2</a></li>
        <li class="list-page"><a class="page-link" href="#">3</a></li>
        <li class="list-page"><a class="page-link" href="#">4</a></li>
        <li class="list-page">
          <img src="../../../assets/icon/btn-next-page.svg" alt="firstpage" />
        </li>
        <li class="list-page">
          <img src="../../../assets/icon/btn-lastpage.svg" alt="prevpage" />
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
export default {
  name: 'AppointmentManager',
  data() {
    return {
      dataTables: [],
    };
  },
  created() {
    this.loadData();
  },
  methods: {
    async loadData() {
      await this.axios
        .get("https://localhost:44336/api/v1/vaccinationappointment")
        .then((res) => {
          this.dataTables = res.data;
        })
        .catch((error) => {
          alert(error);
        });
    },
  }
}
</script>

<style scoped>
@import url("./../../css/page/content.css");
</style>