<template>
  <div id="app">
    <Navbar></Navbar>
    <router-view></router-view> 
  </div>
</template>

<script setup lang="ts">
import Navbar from './components/Navbar.vue';


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
