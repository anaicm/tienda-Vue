<template>
  <div>
    <div class="container">
      <nav>
        <div class="nav-wrapper">
          <router-link to="/">
            <i
              class="brand-logo"
              style="display: flex; justify-content: center; align-self: center"
            >
              Tienda
            </i>
          </router-link>
        </div>
        <div>
          <ul class="right" style="margin-right: 20px">
            <!--si el usuario está autenticado el botón del login se oculta-->
            <li
              v-if="!$store.state.isAuthenticated"
              class="waves-effect waves-light btn white-text"
              style="margin-right: 20px"
            >
              <router-link
                to="/loginUsuario"
                style="color: white"
                @click="mostrarLogin = true"
                >Login</router-link
              >
            </li>
            <li
              class="waves-effect waves-light btn white-text"
              style="margin-right: 20px"
            >
              <router-link to="/addUsuario" style="color: white"
                >Añadir Usuario
                <i class="material-icons" style="margin-left: 10px"
                  >add_circles</i
                >
              </router-link>
            </li>
            <!--si el usuario no esta logado no sale el botón cerrar sesión-->
            <li
              class="btn waves-effect waves-light"
              v-show="!loading"
              style="margin-right: 20px"
              name="action"
              v-if="$store.state.isAuthenticated"
            >
              <router-link to="/portalCliente" style="color: white"
                >Portal Cliente
                <i class="material-icons" style="margin-left: 10px"
                  >add_circles</i
                >
              </router-link>
            </li>
            <!--si el usuario no esta logado no sale el botón cerrar sesión-->
            <li
              class="btn waves-effect waves-light"
              v-show="!loading"
              style="margin-right: 20px"
              name="action"
              v-if="$store.state.isAuthenticated"
              @click="
                $store.commit('logout', $router);
                cerrarSesion();
              "
            >
              Cerrar sesión
            </li>
          </ul>
        </div>
      </nav>
      <!--Para la autenticacion se realiza igual mediante storage creado en el login, 
    en este caso si es True me muestra el contenedor con las credencial del usuario-->
      <div class="container" v-if="$store.state.isAuthenticated">
        <div>
          <label>Usuario: {{ $store.state.userName }}</label
          ><!--llama a la funcion que contiene el nombre del usuario en localstore-->
        </div>
        <div>
          <label>ID: {{ $store.state.id }}</label
          ><!--llama a la funcion que contiene el id del usuario en localstore-->
        </div>
      </div>
    </div>
    <!----------------------------------------------------------------------------->
    <div class="container">
      <Router-view></Router-view>
    </div>
  </div>
</template>

<script>
export default {
  name: "App",
  methods: {
    data() {
      return {
        mostrarLogin: false,
      };
    },
    cerrarSesion() {
      //cerrar sesion en el back
      try {
        this.axios.post("/Login/logout");
      } catch (error) {
        console.error("Error al obtener usuarios:", error);
      }
    },
    mounted() {
      // Busca el elemento con la clase `box`
      const box = document.querySelector(".box");
      // Agrega un evento `mousemove` al documento
      window.onmousemove = function (e) {
        // Calcula el ángulo de rotación
        const x = e.clientX / 3;
        // Aplica el efecto de rotación al elemento `box`
        box.style.transform = "perspective(1000px) rotate(" + x + "deg)";
      };
    },
  },

  /*
  methods: {
   
        usuario() {//devuelve el valor que contiene el nombre del usuario en localStore
         
            return localStorage.getItem('userName');
        },
        id() {//devuelve el valor que contiene el id del usuario en localStore
            return localStorage.getItem('id');
        }, 
        isAuthenticated(){
          return localStorage.getItem('isAuthenticated') == 'S';
        } 
        },
    */
  //En vez de tener los valores aquí creados, directamente se pueden traer de storage
};
</script>
