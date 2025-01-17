<template>
  <div class="container mt-4">
    <h1 class="mb-4">Listado de Servicios</h1>
    <div v-if="servicios.length">
      <div v-for="servicio in servicios" :key="servicio.id" class="card mb-4">
        <div class="card-body">
          <h5 class="card-title">{{ servicio.nombre }}</h5>
          <p class="card-text">{{ servicio.descripcion }}</p>
          <p class="card-text"><strong>Precio:</strong> {{ servicio.precioServicio }} €</p>
        </div>
      </div>
    </div>
    <div v-else>
      <p>No hay servicios disponibles.</p>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      servicios: []
    };
  },
  created() {
    this.fetchServicios();
  },
  methods: {
    async fetchServicios() {
      try {
        const response = await axios.get('http://localhost:5288/api/Servicios');
        if (response.data && response.data.$values) {
          this.servicios = response.data.$values.filter(servicio =>
              !['Desayuno', 'Media Pensión', 'Pensión Completa'].includes(servicio.nombre)
          );
        }
      } catch (error) {
        console.error('Error fetching servicios:', error);
      }
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