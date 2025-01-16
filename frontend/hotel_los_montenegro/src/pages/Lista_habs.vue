<template>
  <div class="container mt-4">
    <h1 class="mb-4">Listado de Habitaciones</h1>
    <div v-if="habitaciones.length">
      <div v-for="habitacion in habitaciones" :key="habitacion.id" class="row mb-4">
        <div class="col-md-12">
          <div class="card d-flex flex-row">
            <img :src="getImage(habitacion.categoriaId)" class="card-img-left" alt="Imagen de la habitación">
            <div class="card-body">
              <h5 class="card-title">Habitación {{ getCategoriaNombre(habitacion.categoriaId) }}</h5>
              <p class="card-text">{{ habitacion.descripcion }}</p>
              <p class="card-text"><strong>Precio:</strong> {{ habitacion.precioBase }} €/noche</p>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div v-else>
      <p>No hay habitaciones disponibles.</p>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import categoria1 from '../assets/estandar.png';
import categoria2 from '../assets/economy.png';
import categoria3 from '../assets/deluxe.png';

export default {
  data() {
    return {
      habitaciones: []
    };
  },
  created() {
    this.fetchHabitaciones();
  },
  methods: {
    async fetchHabitaciones() {
      try {
        const response = await axios.get('http://localhost:5288/api/Habitaciones');
        if (response.data && response.data.$values) {
          this.habitaciones = response.data.$values;
        }
      } catch (error) {
        console.error('Error fetching habitaciones:', error);
      }
    },
    getImage(categoriaId) {
      switch (categoriaId) {
        case 1:
          return categoria1;
        case 2:
          return categoria2;
        case 3:
          return categoria3;
        default:
          return '';
      }
    },
    getCategoriaNombre(categoriaId) {
      switch (categoriaId) {
        case 1:
          return 'Economy';
        case 2:
          return 'Estandar';
        case 3:
          return 'Deluxe';
        default:
          return 'Desconocida';
      }
    }
  }
};
</script>

<style scoped>
.card {
  border: 1px solid #ddd;
  border-radius: 4px;
  display: flex;
  flex-direction: row;
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
  flex: 1;
}

.card-img-left {
  width: 150px;
  height: auto;
  object-fit: cover;
  margin-right: 20px;
}
</style>