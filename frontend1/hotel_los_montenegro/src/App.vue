<template>
  <div id="app">
    <router-view></router-view> <!-- Este es el punto donde se renderiza el contenido de las rutas -->
  </div>
</template>

<script setup lang="ts">
  // Aquí podrías importar otros componentes si los necesitas, aunque para este caso no es necesario.
</script>

<style scoped>
  #app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
  }
</style>
