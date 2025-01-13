<template>
  <header class="header">
    <div class="menu-icon">
      <button @click="toggleMenu">
        <i class="fas fa-bars"></i>
      </button>
    </div>
    <a href="/" class="logo">
      Los Montenegro
    </a>
    <div class="auth-links">
      <template v-if="isAuthenticated">
        <!-- Si el usuario está autenticado -->
        <button @click="goToProfile">
          <i class="fas fa-user-circle"></i>
        </button>
      </template>
      <template v-else>
        <!-- Si el usuario no está autenticado -->
        <router-link to="/login" class="link">Iniciar sesión</router-link>
        <router-link to="/register" class="link">Registrarse</router-link>
      </template>
    </div>
  </header>
</template>

<script>
export default {
  data() {
    return {
      isAuthenticated: false, // Cambiar según el estado real de autenticación
    };
  },
  mounted() {
    // Verifica el estado de autenticación (puede ser desde Vuex o una API)
    this.isAuthenticated = !!localStorage.getItem("authToken"); // Ejemplo básico
  },
  methods: {
    toggleMenu() {
      console.log("Menú desplegado");
    },
    goToProfile() {
      this.$router.push("/profile");
    },
  },
};
</script>

<style scoped>
.header {
  background-color: black;
  color: white;
  padding: 10px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  position: relative;
}

.menu-icon {
  position: absolute;
  left: 10px;
}

.logo {
  font-family: "Cursive", sans-serif;
  font-size: 1.8em;
  text-align: center;
  flex-grow: 1;
  color: white;
  text-decoration: none;
}

.auth-links {
  position: absolute;
  right: 10px;
}

.auth-links .link {
  color: white;
  text-decoration: none;
  margin-right: 15px;
}

.auth-links .link:hover {
  text-decoration: underline;
}

.auth-links button {
  background: none;
  border: none;
  color: white;
  font-size: 1.5em;
}
</style>