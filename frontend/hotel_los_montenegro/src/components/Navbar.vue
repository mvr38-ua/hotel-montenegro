<template>
  <div>
    <nav class="navbar navbar-expand-lg navbar-dark" :style="{ backgroundColor: '#000' }">
      <div class="container-fluid">
        <div class="navbar-nav">
          <div class="nav-item">
            <button class="btn btn-link nav-link no-arrow" @click="toggleSidebar">
              <i class="fas fa-bars"></i> <!-- Three-bar icon -->
            </button>
          </div>
        </div>
        <a class="navbar-brand" href="/">Los Montenegro</a>
        <button class="navbar-toggler" type="button" @click="toggleNavbar">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" :class="{ show: navbarOpen }">
          <ul class="navbar-nav ms-auto">
            <li class="nav-item" v-if="isAuthenticated">
              <div class="dropdown">
                <button class="btn profile-btn" @click="toggleDropdown">
                  <i class="fas fa-user-circle"></i> <!-- Profile icon only -->
                </button>
                <div class="dropdown-menu" :class="{ show: dropdownOpen }">
                  <button class="dropdown-item" @click="goToProfile">Ver Perfil</button>
                  <button class="dropdown-item" @click="logout">Cerrar sesión</button>
                </div>
              </div>
            </li>
            <li class="nav-item" v-if="!isAuthenticated">
              <router-link to="/login" class="nav-link text-white" @click="closeMenus">Iniciar sesión</router-link>
            </li>
            <li class="nav-item" v-if="!isAuthenticated">
              <router-link to="/register" class="nav-link text-white" @click="closeMenus">Registrarse</router-link>
            </li>
          </ul>
        </div>
      </div>
    </nav>
    <div class="sidebar" :class="{ open: sidebarOpen }">
      <button class="close-btn" @click="toggleSidebar">&times;</button>
      <hr class="separator">
      <ul>
        <li><router-link to="/" @click="closeMenus">Home</router-link></li>
        <li><router-link to="/about" @click="closeMenus">About</router-link></li>
        <li><router-link to="/servicios" @click="closeMenus">Servicios</router-link></li>
        <li><router-link to="/listado" @click="closeMenus">Habitaciones</router-link></li>
      </ul>
    </div>
    <div class="overlay" v-if="sidebarOpen" @click="toggleSidebar"></div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      isAuthenticated: false,
      navbarOpen: false,
      sidebarOpen: false,
      dropdownOpen: false,
    };
  },
  mounted() {
    this.isAuthenticated = !!localStorage.getItem("token");
  },
  methods: {
    toggleNavbar() {
      this.navbarOpen = !this.navbarOpen;
    },
    toggleSidebar() {
      this.sidebarOpen = !this.sidebarOpen;
    },
    toggleDropdown() {
      this.dropdownOpen = !this.dropdownOpen;
    },
    goToProfile() {
      this.closeMenus();
      this.$router.push("/profile");
    },
    logout() {
      localStorage.removeItem("token");
      this.isAuthenticated = false;
      this.closeMenus();
      this.$router.push("/login");
    },
    closeMenus() {
      this.sidebarOpen = false;
      this.dropdownOpen = false;
    },
  },
};
</script>

<style scoped>
.navbar-brand {
  position: absolute;
  left: 50%;
  transform: translateX(-50%);
  font-family: 'Playfair Display', serif;
  font-size: 2rem;
}

.navbar-nav {
  margin-left: auto;
}

.no-arrow::after {
  display: none !important;
}

.sidebar {
  position: fixed;
  top: 0;
  left: 0;
  width: 250px;
  height: 100%;
  background-color: #fff; /* White background */
  color: #000; /* Black text color */
  padding: 20px;
  transform: translateX(-100%);
  transition: transform 0.3s ease;
  z-index: 1000;
}

.sidebar.open {
  transform: translateX(0);
}

.sidebar ul {
  list-style: none;
  padding: 0;
}

.sidebar ul li {
  margin: 15px 0;
}

.sidebar ul li a {
  color: #000; /* Black text color */
  text-decoration: none;
  background-color: #d3d3d3; /* Light gray background */
  padding: 10px;
  border-radius: 5px;
  display: block;
}

.sidebar ul li a:hover {
  background-color: #b0b0b0; /* Slightly darker gray on hover */
}

.close-btn {
  background: none;
  border: none;
  font-size: 1.5rem;
  cursor: pointer;
  color: #000;
  margin-bottom: 10px;
  text-align: left; /* Align the button to the left */
}

.separator {
  border: 0;
  height: 1px;
  background: #d3d3d3; /* Light gray line */
  margin: 10px 0;
}

.overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  z-index: 999;
}

.profile-btn {
  background: none;
  border: none;
  padding: 0;
}

.profile-btn i {
  font-size: 2rem; /* Increase the icon size */
  color: #fff; /* Ensure the icon color is white */
}

.dropdown {
  position: relative;
}

.dropdown-menu {
  display: none;
  position: absolute;
  right: 0;
  background-color: #fff;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  border-radius: 5px;
  overflow: hidden;
  z-index: 1000;
  width: 200px; /* Set a fixed width */
  white-space: nowrap; /* Prevent text wrapping */
  transform: translateX(-75%); /* Align with the profile icon */
}

.dropdown-menu.show {
  display: block;
}

.dropdown-item {
  padding: 10px 20px;
  color: #000;
  text-align: left;
  cursor: pointer;
  background-color: #fff;
  border: none;
  width: 100%;
}

.dropdown-item:hover {
  background-color: #f1f1f1;
}

.nav-link.text-white {
  color: #fff !important;
}
</style>