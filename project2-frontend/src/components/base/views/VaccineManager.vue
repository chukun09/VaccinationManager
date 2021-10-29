<template>
  <div class="content">
    <div class="head-content">
      <p id="employee-list">Danh sách Vaccine</p>
      <IconButton
        v-show="grantPermission"
        :action="addVaccine"
        icon="icon/add.png"
        title="Thêm Vaccine"
        style="margin-left: auto"
      />
    </div>
    <div id="myModal" class="modal" v-show="show_modal" v-if="grantPermission">
      <div class="modal-content">
        <span class="close" accesskey="c" @click="show_modal = false"
          >&times;</span
        >
        <BaseFormVaccine
          id="addform"
          :vaccineDetail="vaccineData"
          :isEdit="isEdit"
        />
        <div class="endform" v-show="isShow">
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
            @click="addNewVaccine"
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
    <div class="finder">
      <div class="finder-findtext">
        <input
          accesskey="s"
          type="text"
          class="findtext"
          id="finder-findtext"
          placeholder="Tìm kiếm theo Mã hoặc Tên"
          onfocus="this.placeholder = ''"
          onblur="this.placeholder = 'Tìm kiếm theo Mã hoặc tên'"
          style="width: 300px; height: 40px"
        />
        <div><i class="fa fa-times"></i></div>
      </div>
      <button
        id="delete-employee"
        type="button"
        @click="deleteVaccine"
        v-show="grantPermission"
      >
        <img
          id="delete"
          style="height: 20px; width: 20px"
          src="../../../assets/icon/delete.png"
        />
      </button>
      <button id="btnrefresh" type="button" v-show="grantPermission">
        <img id="refresh" src="../../../assets/icon/refresh.png" />
      </button>
    </div>
    <div id="tablecontainer">
      <table class="table table-responsive w-100 d-block d-md-table">
        <thead class="thead-inverse">
          <tr>
            <th v-if="grantPermission">
              <input type="checkbox" @click="select" v-model="select_all" />
            </th>
            <th>Tên vaccine</th>
            <th>Số lượng</th>
            <th>Xuất xứ</th>
            <th>Ngày sản xuất</th>
          </tr>
        </thead>
        <tbody id="tbodytable">
          <tr
            v-for="(data, index) in dataTables"
            :key="index"
            @click="
              checked.includes(data.vaccineId)
                ? checked.pop(data.vaccineId)
                : checked.push(data.vaccineId)
            "
            @dblclick="showUser(data.vaccineId)"
          >
            <td v-if="grantPermission">
              <input
                type="checkbox"
                v-model="checked"
                :value="data.vaccineId"
              />
            </td>
            <td>{{ data.vaccine_Name }}</td>
            <td>{{ data.vaccine_Amount }}</td>
            <td>{{ data.vaccine_Origin }}</td>
            <td>{{ $filters.formatDateOfBirth(data.date_Of_Manufacture) }}</td>
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
import IconButton from "./../../base/common/BaseIconButton.vue";
import BaseFormVaccine from "./../../base/common/BaseFormVaccine.vue";
export default {
  name: "VaccineManager",
  components: {
    IconButton,
    BaseFormVaccine,
  },
  data() {
    return {
      select_all: false,
      checked: [],
      show_modal: false,
      dataTables: [],
      vaccineData: {},
      newID: "",
      isShow: false,
      isEdit: "0",
    };
  },
  props: {
    grantPermission: {
      default: true,
    },
  },
  created() {
    this.loadData();
  },
  methods: {
    async loadData() {
      await this.axios
        .get("https://localhost:44336/api/v1/vaccine")
        .then((res) => {
          this.dataTables = res.data;
        })
        .catch((error) => {
          alert(error);
        });
    },
    async addVaccine() {
      this.isEdit = "0";
      this.isShow = true;
      this.show_modal = this.show_modal ? false : true;
      await this.getNewUID();
      this.vaccineData = { vaccineId: this.newID };
    },
    async getNewUID() {
      await this.axios
        .get("https://localhost:44336/api/v1/users/GetNewUID", {
          headers: {
            Authorization: `Bearer ${this.$cookies.get("JWT")}`,
          },
        })
        .then((res) => {
          this.newID = res.data;
        })
        .catch((error) => {
          alert(error);
        });
    },
    async getVaccineById(id) {
      await this.axios
        .get("https://localhost:44336/api/v1/vaccine/" + id)
        .then((res) => {
          this.vaccineData = res.data;
        })
        .catch((e) => {
          alert(e);
        });
    },
    showUser(id) {
      this.getVaccineById(id);
      this.show_modal = true;
      this.isShow = false;
      this.isEdit = "1";
    },
    select() {
      this.checked = [];
      if (!this.select_all) {
        for (let i in this.dataTables) {
          this.checked.push(this.dataTables[i].vaccineId);
        }
      }
    },
    async addNewVaccine() {
      await this.axios
        .post(
          "https://localhost:44336/api/v1/vaccine",
          JSON.stringify(this.vaccineData),
          {
            headers: {
              Authorization: `Bearer ${this.$cookies.get("JWT")}`,
              "Content-Type": "application/json",
            },
          }
        )
        .then(() => {
          alert("Thêm Vaccine mới thành công!");
          this.show_modal = false;
          this.loadData();
        })
        .catch((e) => {
          console.error(e);
          alert("Vui lòng kiểm tra lại dữ liệu");
        });
    },
    async deleteVaccine() {
      if (this.checked.length != 0) {
        if (confirm("Bạn có chắc muốn xóa thông tin vaccine đã chọn?")) {
          for (let i in this.checked) {
            await this.axios
              .delete(
                "https://localhost:44336/api/v1/vaccine/" + this.checked[i]
              )
              .then(() => {
                this.loadData();
              })
              .catch((e) => {
                console.log(e);
              });
          }
        } else return;
      } else alert("Chưa có vaccine nào được chọn để xóa");
    },
  },
};
</script>

<style scoped>
@import url("./../../css/page/content.css");
.modal-content {
  width: 600px !important;
}
</style>