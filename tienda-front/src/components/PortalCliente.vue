<template>
  <div>
    <h3>Bienvenid@ {{ mostrarUsuario() }}</h3>
    <div class="">
      <div
        class="waves-effect waves-light btn white-text"
        style="margin-right: 20px"
        @click="abrirModalAniadirEstablecimiento"
      >
        Nuevo establecimiento
        <i class="material-icons" style="margin-left: 10px">add_circles</i>
      </div>
    </div>
    <div>
      <table>
        <thead>
          <th>Nombre</th>
          <th>Dirección</th>
          <th>Teléfono</th>
          <th>Actualizar</th>
          <th>Eliminar</th>
        </thead>
        <tbody
          v-for="establecimiento in establecimientos"
          :key="establecimiento.id"
        >
          <td>{{ establecimiento.nombre }}</td>
          <td>{{ establecimiento.direccion }}</td>
          <td>{{ establecimiento.telefono }}</td>
          <td class="center" @click="abrirModalModificar(establecimiento)">
            <i class="material-icons">edit</i>
          </td>
          <td
            class="center"
            @click="eliminarEstablecimiento(establecimiento.id)"
          >
            <i class="material-icons">delete</i>
          </td>
        </tbody>
      </table>
    </div>
    <!--Modal añadir  tienda ---------------------------------------->
    <div id="modalAniadirEstablecimiento" class="modal">
      <div class="modal-content">
        <label for="nombre">Nombre Establecimiento:</label>
        <input
          type="text"
          id="nombre"
          v-model="nombre"
        /><!--v-model="Establecimiento.nombre"-->
        <label for="direccion">Dirección:</label>
        <input type="text" id="direccion" v-model="direccion" />
        <label for="telefono">telefono:</label>
        <input type="text" id="email" v-model="telefono" />
      </div>
      <div class="modal-footer">
        <!-- Botones para cerrar y guardar cambios -->
        <a class="modal-close waves-effect waves-green btn-flat">Cerrar</a>
        <a
          class="modal-close waves-effect waves-green btn-flat"
          @click="guardarCambios"
          >Guardar Cambios</a
        >
      </div>
    </div>
    <!--Modal actualizar---------------------------------------->
    <div id="modalModificar" class="modal">
      <div class="modal-content">
        <label for="nombre">Nombre Establecimiento:</label>
        <input
          type="text"
          id="nombre"
          v-model="establecimientoSeleccionado.nombre"
        /><!--v-model="Establecimiento.nombre"-->
        <label for="direccion">Dirección:</label>
        <input
          type="text"
          id="direccion"
          v-model="establecimientoSeleccionado.direccion"
        />
        <label for="telefono">telefono:</label>
        <input
          type="text"
          id="email"
          v-model="establecimientoSeleccionado.telefono"
        />
      </div>
      <div class="modal-footer">
        <!-- Botones para cerrar y guardar cambios -->
        <a class="modal-close waves-effect waves-green btn-flat">Cerrar</a>
        <a
          class="modal-close waves-effect waves-green btn-flat"
          @click="guardarCambiosActualizar"
          >Guardar Cambios</a
        >
      </div>
    </div>
  </div>
</template>

<script>
import M from "materialize-css";
export default {
  name: "portalCliente",
  data() {
    return {
      idUsuarioSeleccionado: this.$store.state.idUsuarioSeleccionado,
      //nameUsuarioSeleccionado: this.$store.state.nameUsuarioSeleccionado, 
      nombre: "",
      direccion: "",
      telefono: "",
      establecimientos: [],
      establecimientoSeleccionado: {
        id: -1,
        nombre: "",
        direccion: "",
        telefono: "",
      },
    };
  },
  methods: {
    mostrarUsuario(){
      alert(this.$store.state.nameUsuarioSeleccionado);
      return this.$store.state.nameUsuarioSeleccionado;
    },
    abrirModalAniadirEstablecimiento() {
      const modal = M.Modal.init(
        document.getElementById("modalAniadirEstablecimiento")
      );
      modal.open();
    },
    async guardarCambios() {
      //cambios para añadir un nuevo establecimiento
      // Enviar los datos del establecimiento a la API
      const confirmacion = window.confirm(
        "Se va a añadir un nuevo establecimiento, ¿Estás seguro?"
      );
      if (!confirmacion) {
        // El usuario canceló la eliminación
        return;
      }
      const establecimiento = {
        nombre: this.nombre,
        direccion: this.direccion,
        telefono: this.telefono,
        usuario_id: this.idUsuarioSeleccionado, //estado del storage que guarda el id del usuario
      };
      const response = await this.axios.post(
        "/establecimientos",
        establecimiento
      );
      // Si la creación se realizó correctamente, cerrar el modal
      if (response.status === 200) {
        console.log("Se ha guardado correctamente");
          this.establecimientos.push(response.data);//añade el nuevo registro a la tabla y lo muestra
      }
    },
    async guardarCambiosActualizar() {
      // Realizar la solicitud para actualizar el usuario
      try {
        const response = await this.axios.put(
          `/Establecimientos`,
          this.establecimientoSeleccionado
        );
        console.log("Usuario actualizado con éxito:", response);
        // Buscar y actualizar el usuario en la lista existente
        const index = this.establecimientos.findIndex(
          (establecimiento) =>
            establecimiento.id === this.establecimientoSeleccionado.id
        );
        if (index !== -1) {
          // Actualizar el usuario en la lista
          this.establecimientos[index] = {
            nombre: this.establecimientoSeleccionado.nombre,
            direccion: this.establecimientoSeleccionado.direccion,
            telefono: this.establecimientoSeleccionado.telefono,
          };
        }
       
      } catch (error) {
        console.error("Error al actualizar usuario:", error);
      }
    },
    abrirModalModificar(establecimiento) {
      // Abrir el modal de edición y asignar el usuario seleccionado
      this.establecimientoSeleccionado = {
        id: establecimiento.id,
        nombre: establecimiento.nombre,
        direccion: establecimiento.direccion,
        telefono: establecimiento.telefono,
      };
      const modal = M.Modal.init(document.getElementById("modalModificar"));
      modal.open();
    },
    async eliminarEstablecimiento(idEstablecimiento) {
      const confirmacion = window.confirm(
        "¿Estás seguro de que deseas eliminar este Establecimiento?"
      );
      if (!confirmacion) {
        // El usuario canceló la eliminación
        return;
      }

      // Realizar la solicitud para eliminar el usuario por ID
      try {
        const response = await this.axios.delete(
          `/Establecimientos/${idEstablecimiento}`
        );
        console.log("Usuario eliminado con éxito:", response);

        // Filtrar la lista para quitar el usuario eliminado
        this.establecimientos = this.establecimientos.filter(
          (user) => idEstablecimiento !== user.id
        );
      
      } catch (error) {
        console.error("Error al eliminar usuario:", error);
      }
    },
  }, //----------------------fin del metodo
  async mounted() {
    try {
      //this.isLoading = true; //cuando empieza a cargar los datos se pone en true
      const response = await this.axios.get(
        "/Establecimientos/" + this.idUsuarioSeleccionado
      );
      this.establecimientos = response.data;
    } catch (error) {
      console.error("Error al obtener usuarios:", error);
    }
    //this.isLoading = false; //cuando finaliza vuelve a estado inicial de false
  },
};
</script>
