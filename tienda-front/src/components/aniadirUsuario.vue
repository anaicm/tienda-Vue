<template>
  <div class="container" style="margin-top: 100px">
    <div class="clo m12 card-panel">
      <a
        class="btn-floating btn-large waves-effect waves-light right"
        href=""
        @click="cerrarAddUsuario()"
        ><i class="material-icons">close</i></a
      >

      <div class="row">
        <div class="col m3">
          <label>Usuario</label>
          <input type="text" v-model="usuario" />
        </div>
      </div>
      <div class="row">
        <div class="col m3">
          <label>Email:</label>
          <input type="email" v-model="email" />
        </div>
      </div>
      <div class="row">
        <div class="col m3">
          <label>Contraseña</label>
          <input type="password" v-model="clave" />
        </div>
      </div>
      <!--2=> mostrar el loading en vez de el botón agregar-->
      <button
        class="btn waves-effect waves-light"
        type="submit"
        name="action"
        v-show="!loading"
        @click="crearUsuario"
      >
        Aceptar
        <i class="material-icons right">send</i>
      </button>
    </div>
    <!--1=> añadir el div para mostrar loading-->
    <div v-show="loading" class="progress">
      <div class="indeterminate"></div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
export default {
  name: "addUsuario",
  data() {
    return {
      usuario: "",
      email: "",
      clave: "",
      loading: false, //elemento de recarga, de primeras siempe no estoy cargando
    };
  },
  methods: {
    async crearUsuario() {
      try {
        //3- Cambiar a true login para empezar la carga
        this.loading = true; //voy a empezar la carga y se quita el botón del formulario
        const response = await axios.post("/Users", {
          Username: this.usuario,
          Email: this.email,
          Password: this.clave,
        });
        if (response.status === 200) {
          // Usuario agregado con éxito
          // Limpia el formulario
          this.usuario = "";
          this.email = "";
          this.clave = "";
          console.log("Usuario agregado con éxito");
          //4- función para parar el loading con un tiempo de vida
          setTimeout(() => {
            this.loading = false; // Detener la carga
          }, 1000);
          this.$router.push("/loginUsuario");
        } else {
          // Manejar otros casos aquí
          console.error("Error al agregar el usuario");
        }
      } catch (error) {
        // Manejar errores de la solicitud
        console.error("Error al hacer la solicitud:", error);
        this.loading = false;
      }
    },
    cerrarAddUsuario() {
      // Verifica si el usuario está autenticado
      if (this.$store.state.isAuthenticated) {
        // Redirige al portal del usuario
        this.$router.push("/portalUsuario");
      } else {
        // Redirige al inicio
        this.$router.push("/");
      }
    },
  },
};
</script>
