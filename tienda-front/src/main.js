import { createApp } from 'vue'
import App from './App.vue'
import 'materialize-css/dist/css/materialize.min.css'
import 'material-design-icons/iconfont/material-icons.css'
import axios from 'axios'
import VueAxios from 'vue-axios'
import router from './router'
axios.defaults.baseURL = 'http://localhost:5021/'
axios.defaults.withCredentials = true;
createApp(App).use(router).use(VueAxios,axios).mount('#app')
