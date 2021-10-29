import { createApp } from 'vue'
import App from './App.vue'
import router from './router/appRoute.js'
import  VueCookies from 'vue3-cookies'
import store from './store/index'
import axios from 'axios'
import VueAxios from 'vue-axios'
const myV3App = createApp(App)
myV3App.config.globalProperties.$filters ={
    formatDateOfBirth(date) {
        let dateOrigin = new Date(date).toLocaleDateString("en-GB");
        return dateOrigin;
      },
}
myV3App.use(router).use(store).use(VueCookies).use(VueAxios, axios);
myV3App.config.globalProperties.axios = axios;
myV3App.mount('#app')