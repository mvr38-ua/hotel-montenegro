<template>
  <header class="header">
    <div class="menu-icon">
      <button @click="toggleMenu">
        <i class="fas fa-bars"></i>
      </button>
      <div v-if="menuOpen" class="dropdown-menu">
        <router-link to="/listar" class="dropdown-item" @click="closeMenu">Listar</router-link>
      </div>
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
      menuOpen: false,
    };
  },
  mounted() {
    // Verifica el estado de autenticación (puede ser desde Vuex o una API)
    this.isAuthenticated = !!localStorage.getItem("authToken"); // Ejemplo básico
  },
  methods: {
    toggleMenu() {
      this.menuOpen = !this.menuOpen;
    },
    closeMenu() {
      this.menuOpen = false;
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

.dropdown-menu {
  position: absolute;
  top: 40px;
  left: 10px;
  background-color: #333; /* Dark gray background */
  color: white;
  border: 1px solid #555; /* Slightly lighter gray border */
  border-radius: 5px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  z-index: 1000;
}

.dropdown-item {
  display: block;
  padding: 10px;
  text-decoration: none;
  color: white;
  transition: background-color 0.3s;
}

.dropdown-item:hover {
  background-color: #555; /* Darker gray on hover */
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