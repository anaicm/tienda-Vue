import { createApp } from 'vue'
import App from './App.vue'
import 'materialize-css/dist/css/materialize.min.css'
import 'material-design-icons/iconfont/material-icons.css'
import axios from 'axios'
import VueAxios from 'vue-axios'
import router from './router'
import storage from './storage/'//importa el storage con las variables que contiene los valores del usuario

axios.defaults.baseURL = 'http://localhost:5021/'
axios.defaults.withCredentials = true;
createApp(App).use(router).use(VueAxios,axios).use(storage).mount('#app')
