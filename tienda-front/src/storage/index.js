import {createStore} from 'vuex';//guarda los valores del usuario para poder usarlos mas adelante sin tener que crear métodos
//cada vez que se quieran llamar
import VuexPersistence from 'vuex-persist';

const storage = createStore({//variables para guardar el valor
    state: {
        isAuthenticated: false,
        userName: '',
        id:''
    },
    // mutaciónes métodos que cambia los valores de los estados
    mutations: {
        setAuthenticated(state,value){
            state.isAuthenticated = value;
        },
        setUsername(state,userName){
            state.userName = userName;
        },
        setEmail(state,email){
            state.email = email;
        }, 
        setId(state,id){
            state.id = id;
        }, 
        logout(state, router){//le entra un estado y el router 
            state.isAuthenticated = false;//si esta autenticado que pase a false
            //si hay token hay que borrarlo pero como en este caso es una cookies esta ya viene sola
            state.userName = '';//el nombre del usuario se elimina 
            state.id = null;//el id del usuario se elimina
            state.email = '';//el id del usuario se elimina
            router.push('/loginUsuario')//redirige al componente login para que se vuelva a logar 
        }
    },
    //es storage acepta plugins para definir el localStorage
    plugins:[
        new VuexPersistence({
            storage:window.localStorage//se almacena la información
        }).plugin //al poner la palabra clave plugin se establece como un nuevo plugin
    ]
});

export default storage//se importa para llamarlo en el main.js