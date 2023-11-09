<template>
    <div class="container" style="margin-top: 100px;">
        <div class="clo m12 card-panel">
            <form @submit.prevent="iniciarSesion"> 
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
                <button class="btn waves-effect waves-light" v-show="!loading" style="margin-right: 20px;" type="submit" name="action">
                    <router-link to="/addUsuario" style="color: white;">Añadir Usuario
                        <i class="material-icons right">send</i>
                    </router-link>    
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
                    var result = await this.axios.post('/Login', datos);
                    console.log(result);
                    console.log('¡Conectado correctamente!');
                    // isAuthenticated = true;
                    this.$router.push('/portalUsuario');//cuando devuelve OK redirige al componente 'portalUsuario'
                } catch (error) {
                    console.error(error); 
                } finally {
                this.loading = false;
            }
            }
        },
    }

</script>