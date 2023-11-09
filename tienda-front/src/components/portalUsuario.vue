<template>
    <div>
      <h2>Este es el portal del usuario</h2>
      
      <!-- Mostrar la tabla si hay usuarios -->
      <table v-if="usuarios.length > 0">
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
            <!-- Agrega más celdas según las propiedades de tus usuarios -->
          </tr>
        </tbody>
      </table>
  
      <!-- Mostrar mensaje si no hay usuarios -->
      <div v-else>
        No hay usuarios para mostrar.
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: 'portalUsuario',
    data() {
      return {
        usuarios: [],
      };
    },
    async mounted() {
        // if(!isAuthenticated)
        //     this.$router.push('/Login');
      // Realizar la solicitud a la API para obtener los usuarios
      try {
        const response = await this.axios.get('/Users', {
          withCredentials: true,
        });
      // Asignar los usuarios a la propiedad 'usuarios'
        this.usuarios = response.data;
      } catch (error) {
        console.error('Error al obtener usuarios:', error);
      }
    },
  };
  </script>
  