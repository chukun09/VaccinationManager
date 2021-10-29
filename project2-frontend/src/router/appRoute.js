import Login from './../components/layout/page/Login.vue';
import AdminPage from './../views/AdminPage.vue'
import UserPage from './../views/UserPage.vue'
import DoctorPage from './../views/DoctorPage.vue'
import NursePage from './../views/NursePage.vue'
import { createRouter, createWebHistory } from 'vue-router'
import NotFound from './../components/layout/page/NotFoundPage.vue'
import UserManager from './../components/base/views/UserManager.vue';
import VaccineManager from './../components/base/views/VaccineManager.vue';
import AppointmentManager from './../components/base/views/AppointmentManager.vue';
import VaccinationManager from './../components/base/views/VaccinationManager.vue';
import PersonalInformation from './../components/base/views/PersonalInformation.vue';
import PersonalVaccination from './../components/base/views/PersonalVaccination.vue';
import DoctorAppointment from './../components/base/views/DoctorAppointment.vue';
import NurseAppointment from './../components/base/views/NurseAppointment.vue';
const routes = [
  { path: "/", component: Login },
  { path: "/login", component: Login, meta: { auth: false } },
  {
    path: "/UserPage/:id", component: UserPage, meta: { auth: true, role: "Người dùng" },
    children: [
      {
        path: "VaccineManager",
        component: VaccineManager,
        props: { grantPermission: false }
      },
      {
        path: "VaccinationManager",
        component: PersonalVaccination
      },
      {
        path: "",
        component: PersonalVaccination
      },
      {
        path: "PersonalInformation",
        component: PersonalInformation
      }
    ]
  },
  {
    path: "/AdminPage/:id", component: AdminPage, meta: { auth: true, role: "Quản trị viên" },
    children: [{
      path: '',
      component: UserManager
    },
    {
      path: "UserManager",
      component: UserManager
    },
    {
      path: "VaccineManager",
      component: VaccineManager
    },
    {
      path: "AppointmentManager",
      component: AppointmentManager
    },
    {
      path: "VaccinationManager",
      component: VaccinationManager
    },
    {
      path: "PersonalInformation",
      component: PersonalInformation
    }
    ]
  },
  {
    path: "/DoctorPage/:id", component: DoctorPage, meta: { auth: true, role: "Bác sĩ" },
    children: [{
      path: "VaccineManager",
      component: VaccineManager,
      props: { grantPermission: true }
    },
    {
      path: "VaccinationManager",
      component: DoctorAppointment
    },
    {
      path: "",
      component: DoctorAppointment
    },
    {
      path: "PersonalInformation",
      component: PersonalInformation
    }]
  },
  {
    path: "/NursePage/:id", component: NursePage, meta: { auth: true, role: "Y tá" },
    children: [{
      path: "VaccineManager",
      component: VaccineManager,
      props: { grantPermission: false }
    },
    {
      path: "VaccinationManager",
      component: NurseAppointment
    },
    {
      path: "",
      component: NurseAppointment
    },
    {
      path: "PersonalInformation",
      component: PersonalInformation
    }]
  },
  { path: "/:pathMatch(.*)*", component: NotFound, meta: { auth: false } }
]
const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})
router.beforeEach((to, from, next) => {
  if (sessionStorage.getItem("authentication") != to.meta.role && to.meta.auth) {
    sessionStorage.setItem("authentication", false);
    next('/login');
  }
  else next();
})
export default router;