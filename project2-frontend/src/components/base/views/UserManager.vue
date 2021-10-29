<template>
  <div class="content">
    <div class="head-content">
      <p id="employee-list">Danh sách người dùng</p>
      <IconButton
        :action="addUser"
        icon="icon/add.png"
        title="Thêm Người Dùng"
        style="margin-left: auto"
      />
    </div>
    <div id="myModal" class="modal" v-show="show_modal">
      <div class="modal-content">
        <span class="close" accesskey="c" @click="show_modal = false"
          >&times;</span
        >
        <BaseForm id="addform" :userDetail="userData" :isEdit="isEdit" />
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
            @click="addNewUser"
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
          v-model="filterText"
          @input="filter(dataTables, filterText)"
          accesskey="s"
          type="text"
          class="findtext"
          id="finder-findtext"
          placeholder="Tìm kiếm theo Mã, Tên hoặc Số điện thoại"
          onfocus="this.placeholder = ''"
          onblur="this.placeholder = 'Tìm kiếm theo Mã, Tên hoặc Số điện thoại'"
          style="width: 300px; height: 40px"
        />
        <div><i class="fa fa-times"></i></div>
      </div>
      <button id="delete-employee" type="button" @click="deleteUser">
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
    <div id="tablecontainer">
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
            <th>Tài khoản</th>
            <th>Mật khẩu</th>
            <th>Địa chỉ</th>
            <th>Vai trò</th>
          </tr>
        </thead>
        <tbody id="tbodytable">
          <tr
            v-for="(data, index) in filterData"
            :key="index"
            @click="
              checked.includes(data.userId)
                ? checked.pop(data.userId)
                : checked.push(data.userId)
            "
            @dblclick="showUser(data.userId)"
          >
            <td>
              <input type="checkbox" v-model="checked" :value="data.userId" />
            </td>
            <td scope="row">{{ data.fullName }}</td>
            <td>{{ data.gender }}</td>
            <td>{{ $filters.formatDateOfBirth(data.dateOfBirth) }}</td>
            <td>{{ data.phoneNumber }}</td>
            <td>{{ data.account }}</td>
            <td>{{ data.password }}</td>
            <td>{{ data.address }}</td>
            <td>{{ data.authority_Name }}</td>
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
import BaseForm from "./../../base/common/BaseForm.vue";
export default {
  name: "UserManager",
  components: {
    IconButton,
    BaseForm,
  },
  data() {
    return {
      select_all: false,
      checked: [],
      show_modal: false,
      dataTables: [],
      filterData: [],
      userData: {},
      newUID: "",
      isShow: false,
      filterText: "",
      isEdit: "0",
    };
  },
  created() {
    this.loadData();
  },
  methods: {
    async loadData() {
      await this.axios
        .get("https://localhost:44336/api/v1/users")
        .then((res) => {
          this.dataTables = res.data;
          this.filterData = this.dataTables;
        })
        .catch((error) => {
          alert(error);
        });
    },
    filter(arr, searchKey) {
      if (searchKey == "" || searchKey == null) {
        this.filterData = this.dataTables;
      } else {
        this.filterData = arr.filter((obj) =>
          Object.keys(obj).some((key) => obj[key].startsWith(searchKey))
        );
      }
    },
    async addUser() {
      this.isEdit = "0";
      this.isShow = true;
      this.show_modal = this.show_modal ? false : true;
      await this.getNewUID();
      this.userData = { userId: this.newUID };
    },
    async getUserById(id) {
      await this.axios
        .get("https://localhost:44336/api/v1/users/" + id)
        .then((res) => {
          this.userData = res.data;
        })
        .catch((e) => {
          alert(e);
        });
    },
    showUser(id) {
      this.getUserById(id);
      this.show_modal = true;
      this.isShow = false;
      this.isEdit = "1";
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
    select() {
      this.checked = [];
      if (!this.select_all) {
        for (let i in this.dataTables) {
          this.checked.push(this.dataTables[i].userId);
        }
      }
    },
    async addNewUser() {
      await this.axios
        .post(
          "https://localhost:44336/api/v1/users",
          JSON.stringify(this.userData),
          {
            headers: {
              Authorization: `Bearer ${this.$cookies.get("JWT")}`,
              "Content-Type": "application/json",
            },
          }
        )
        .then(() => {
          alert("Thêm người dùng mới thành công!");
          this.show_modal = false;
          this.loadData();
        })
        .catch((e) => {
          console.error(e);
          alert("Vui lòng kiểm tra lại dữ liệu nhập!");
        });
    },
    async deleteUser() {
      if (this.checked.length != 0) {
        if (confirm("Bạn có muốn xóa những người dùng đã chọn?")) {
          for (let i in this.checked) {
            await this.axios
              .delete(
                "https://localhost:44336/api/v1/users/" + this.checked[i],
                {
                  headers: {
                    Authorization: `Bearer ${this.$cookies.get("JWT")}`,
                  },
                }
              )
              .then(() => {
                this.loadData();
              })
              .catch((e) => {
                console.log(e);
              });
          }
        } else return;
      } else alert("Chưa có người dùng được chọn!");
    },
  },
};
</script>

<style scoped>
@import url("./../../css/page/content.css");
</style>