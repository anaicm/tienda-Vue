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
      <router-link to="/addUsuario" style="color: white;">Añadir Usuario</router-link>
    </li>
  </ul>

 

  
 </div>

 <div class="container">
    <Router-view></Router-view>
  </div>
</template>

<script>

export default {
  name: 'App',
  
}
</script>


