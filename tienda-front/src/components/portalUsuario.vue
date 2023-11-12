<template>
    <div>
    <div>
      <h2>Este es el portal del usuario</h2>
      <!--Si esta autenticado saldra el boton para cerrar sesion-->
      <button class="btn waves-effect waves-light" v-show="!loading" style="margin-right: 20px;" type="submit" name="action"
      v-if="$store.state.isAuthenticated" @click="$store.commit('logout', $router);cerrarSesion();">
          Cerrar sesión
          <i class="material-icons right">send</i>       
      </button>
      <div class="progress" v-show="isLoading"><!--Se va a mostrar cuando este cargando los datos-->
        <div class="indeterminate"></div>
      </div>
      <!-- Mostrar la tabla si hay usuarios -->
      <table v-if="usuarios.length > 0" v-show="!isLoading"><!--Se muestra cuando no este cargando los datos-->
        <thead>
          <tr>
            <th>ID</th>
            <th>Username</th>
            <th>Email</th>
            <!-- Agrega más columnas según las propiedades de tus usuarios -->
          </tr>
        </thead>
        <tbody>
          <tr v-for="usuario in usuarios" :key="usuario.id">
            <td>{{ usuario.id }}</td>
            <td>{{ usuario.username }}</td>
            <td>{{ usuario.email }}</td>
            <td class="center"><a href="#!" class="material-icons">edit</a></td>
            <td class="center"><a href="#!" class="material-icons">delete</a></td>
          </tr>
        </tbody>
      </table>
      
      <!-- Mostrar mensaje si no hay usuarios -->
      <div v-else>
        No hay usuarios para mostrar.
      </div>
    </div>
    </div>
  </template>
  
  <script>

  export default {
    name: 'portalUsuario',
    data() {
      return {
        usuarios: [],
        isLoading: false,// se inicializa en false para la carga
       
      };
    },
    methods:{
      cerrarSesion(){//cerrar sesion en el back
        try {
        this.axios.post('/Login/logout');
        } catch (error) {
          console.error('Error al obtener usuarios:', error);
        }
      }
    },
    async mounted() {
      // Realizar la solicitud a la API para obtener los usuarios
      try {
        this.isLoading = true;//cuando empieza a cargar los datos se pone en true 
        const response = await this.axios.get('/Users', {
          withCredentials: true,
          
        });
      // Asignar los usuarios a la propiedad 'usuarios'
        this.usuarios = response.data;
        //para la paginación
      } catch (error) {
        console.error('Error al obtener usuarios:', error);
      }
      this.isLoading = false;//cuando finaliza vuelve a estado inicial de false 
    },
   
  };
  </script>
  