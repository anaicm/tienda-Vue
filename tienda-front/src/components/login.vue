<template>
    <div class="container" style="margin-top: 100px;">
        <div class="clo m12 card-panel">
            <form @submit.prevent="iniciarSesion"> 
                <a class="btn-floating btn-large waves-effect waves-light right" href="" @click="cerrarLogin()"><i class="material-icons">close</i></a>
            <div class="row">   
                <div class="col m3">
                    <label>Usuario</label>
                    <input type="text" v-model="username"/>
                </div> 
            </div>
                <div class="row">
                    <div class="col m3">
                        <label>Contraseña</label>
                        <input type="password" v-model="password"/>
                    </div> 
                </div>
                <button class="btn waves-effect waves-light" style="margin-right: 20px; margin-bottom: 20px;" type="submit" name="action">Iniciar sesión
                    <i class="material-icons right">send</i>
                </button> 
            </form>
        </div>       
    </div>
</template>

<script>
    export default
    {
        name: 'loginUsuario', 
        data(){
            return{
                username:'',
                password:'',
                loading: false
            }    
        },
        methods: {
            async iniciarSesion() {
                var datos = {
                    username: this.username,
                    password: this.password,
                };
                this.loading = true;
                try {
                    var result = await this.axios.post('/Login', datos);//guarda el json que hay en el loginController para los datos del usuario
                    console.log('¡Conectado correctamente!');
                    //localStorage.setItem('userName', result.data.userData.userName);//guarda el nombre del usuario en localhost
                    //localStorage.setItem('id', result.data.userData.id);//guarda el id del usuario
                    this.$store.commit('setAuthenticated', true);//llama a la funcion de store para ver si esta autenticado
                    this.$store.commit('setUsername', result.data.userData.userName);//llama a la funcion de store
                    this.$store.commit('setEmail', result.data.userData.email);//llama a la funcion de store
                    this.$store.commit('setId', result.data.userData.id);//llama a la funcion de store
                    // isAuthenticated = true;
                    this.$router.push('/portalUsuario');//cuando devuelve OK redirige al componente 'portalUsuario'
                } catch (error) {
                    console.error(error); 
                } finally {
                this.loading = false;
            }
            },
            cerrarLogin() {
             // Redirecciona a la página de inicio
                this.$router.push('/');
            }
        },
       

       
    }

</script>