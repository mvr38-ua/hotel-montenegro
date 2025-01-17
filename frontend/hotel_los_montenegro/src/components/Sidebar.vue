<template>
  <div class="sidebar">
    <router-link to="/webmaster/habitaciones" class="sidebar-item">
      Gestión de habitaciones
    </router-link>
    <router-link to="/webmaster/usuarios" class="sidebar-item">
      Gestión de usuarios
    </router-link>
    <router-link to="/webmaster/servicio" class="sidebar-item">
      Gestión servicios
    </router-link>
    <router-link to="/webmaster/categoria" class="sidebar-item">
      Gestión categorías
    </router-link>
    <router-link to="/webmaster/temporada" class="sidebar-item">
      Gestión temporadas
    </router-link>
    <router-link to="/webmaster/informes" class="sidebar-item">
      Informes
    </router-link>
  </div>
</template>

<style>
.sidebar {
  width: 250px;
  background-color: #f5f5f5;
  display: flex;
  flex-direction: column;
  padding: 20px;
  border-right: 1px solid #ddd;
}

.sidebar-item {
  display: block;
  padding: 10px 15px;
  margin-bottom: 10px;
  text-decoration: none;
  color: #333;
  background-color: #e6e6e6;
  border-radius: 4px;
  font-weight: bold;
  text-align: center;
}

.sidebar-item:hover {
  background-color: #d4d4d4;
}

.router-link-active {
  background-color: #333;
  color: #fff;
}
</style>
