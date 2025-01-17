<template>
  <div class="container mt-4">
    <h1 class="mb-4">Perfil de Usuario</h1>
    <div v-if="user">
      <div class="card mb-4">
        <div class="card-body">
          <h5 class="card-title">Nombre: {{ user.nombre }} {{ user.apellidos }}</h5>
          <p class="card-text"><strong>Email:</strong> {{ user.email }}</p>
          <p class="card-text"><strong>Teléfono:</strong> {{ user.telefono }}</p>
          <p class="card-text"><strong>Móvil:</strong> {{ user.movil }}</p>
          <p class="card-text"><strong>Género:</strong> {{ user.genero }}</p>
          <p class="card-text"><strong>Fecha de Nacimiento:</strong> {{ formatDate(user.fechaNac) }}</p>
          <p class="card-text"><strong>Dirección:</strong> {{ user.direccion.direccion }}, {{ user.direccion.municipio }}, {{ user.direccion.provincia }}, {{ user.direccion.pais }}</p>
        </div>
      </div>
    </div>
    <div v-else>
      <p>No se ha podido cargar la información del usuario.</p>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import authService from '../services/authService';

export default {
  data() {
    return {
      user: null
    };
  },
  created() {
    this.fetchUser();
  },
  methods: {
    async fetchUser() {
      try {
        const token = localStorage.getItem('token');
        if (!token) {
          console.error('No token found');
          return;
        }
        const userId = authService.obtenerUsuarioDelToken();
        const response = await axios.get(`http://localhost:5288/api/Usuario/${userId}`, {
          headers: {
            Authorization: `Bearer ${token}`
          }
        });
        if (response.data) {
          this.user = response.data;
        }
      } catch (error) {
        console.error('Error fetching user data:', error);
      }
    },
    formatDate(dateString) {
      const options = {year: 'numeric', month: 'long', day: 'numeric'};
      return new Date(dateString).toLocaleDateString(undefined, options);
    }
  }
};
</script>

<style scoped>
.card {
  border: 1px solid #ddd;
  border-radius: 4px;
}

.card-title {
  font-size: 1.25rem;
  font-weight: bold;
}

.card-text {
  text-align: left;
}

.card-body {
  text-align: left;
}
</style>