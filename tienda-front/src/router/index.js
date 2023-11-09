import { createWebHistory, createRouter } from "vue-router";
import addUsuario from '../components/aniadirUsuario.vue';//componente usuario
import App from '../App.vue';
import loginUsuario from '../components/login.vue'
import portalUsuario from '../components/portalUsuario.vue'

const routes = [//se crea un array de rutas 
    
    {
        path: '/addUsuario',// en las rutas se pueden acceder por la path o por el nombre 
        name: 'addUsuario',
        component: addUsuario //al componente que se refiere 
    },
    {
        path: '/App',// en las rutas se pueden acceder por la path o por el nombre 
        name: 'App',
        component: App //al componente que se refiere 
    },
    {
        path: '/loginUsuario',// en las rutas se pueden acceder por la path o por el nombre 
        name: 'loginUsuario',
        component: loginUsuario //al componente que se refiere 
    },
    {
        path: '/portalUsuario',// en las rutas se pueden acceder por la path o por el nombre 
        name: 'portalUsuario',
        component: portalUsuario //al componente que se refiere 
    },
   
  
];

//una vez definido se crea el router propio
const router = createRouter({
    history: createWebHistory(),
    routes
});
// despues se exporta el router
export default router;