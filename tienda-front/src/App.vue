<template>
 <div class="container">
  <nav>
    <router-link to="/">
      <div class="nav-wrapper">
        <a href="#!" class="brand-logo">Tienda</a>
      </div>
    </router-link>
  </nav>
  <ul>
    
    <li class="waves-effect waves-light btn white-text" style="margin-right: 20px;">
      <router-link to="/loginUsuario" style="color: white;">Login</router-link>
    </li>
    <li class="waves-effect waves-light btn white-text" style="margin-right: 20px;">
      <router-link to="/addUsuario" style="color: white;">Añadir Usuario
        <i class="material-icons" style="margin-left: 10px;">add_circles</i>
      </router-link>
    </li>
  </ul>

  <!--Para la autenticacion se realiza igual mediante storage creado en el login, 
    en este caso si es True me muestra el contenedor con las credencial del usuario-->
  <div class="container" v-if="$store.state.isAuthenticated">
    <div>
        <label>Usuario: {{$store.state.userName }}</label><!--llama a la funcion que contiene el nombre del usuario en localstore-->
    </div>
    <div>
        <label>ID: {{ $store.state.id}}</label><!--llama a la funcion que contiene el id del usuario en localstore-->
    </div>
  </div>

  
 </div>

 <div class="container">
    <Router-view></Router-view>
  </div>
</template>

<script>

export default {
  name: 'App',
  
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
  }

</script>


