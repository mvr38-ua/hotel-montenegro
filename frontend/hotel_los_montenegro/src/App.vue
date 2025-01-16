<template>
  <div id="app">
    <Navbar></Navbar>
    <div class="content">
      <router-view></router-view>
    </div>
    <Footer></Footer>
  </div>
</template>

<script setup lang="ts">
import Navbar from './components/Navbar.vue';
import Footer from './components/Footer.vue';
</script>

<style scoped>
/* Hacer que el contenedor principal ocupe todo el alto de la ventana */
#app {
  display: flex;
  flex-direction: column; /* Organiza los elementos en columna */
  min-height: 100vh; /* Asegura que el contenedor principal ocupe toda la altura de la pantalla */
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

/* Permite que el contenido principal crezca para ocupar el espacio restante */
.content {
  flex: 1;
  padding: 20px 0; /* Ajusta el espaciado según sea necesario */
}
</style>
