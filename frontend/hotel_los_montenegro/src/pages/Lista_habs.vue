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
              <div class="amenities">
                <i class="fas fa-wifi" v-if="habitacion.wifi"></i>
                <i class="fas fa-bath" v-if="habitacion.bano"></i>
                <!-- Add more icons as needed -->
              </div>
              <p class="card-text">{{ habitacion.descripcion }}</p>
              <p class="card-text"><strong>Precio:</strong> {{ habitacion.precioBase }} €/noche</p>
              <p class="card-description">{{ getCategoriaDescripcion(habitacion.categoriaId) }}</p>
            </div>
          </div>
          <hr class="separator">
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
    },
    getCategoriaDescripcion(categoriaId) {
      switch (categoriaId) {
        case 1:
          return 'Habitación Economy: Estas habitaciones tienen balcón y sus ventanas ofrecen vistas a un pequeño jardín interior. Disponen de dos camas de 1.05m o una cama de 2x2m. Algunos baños cuentan con ducha y otros con bañera.';
        case 2:
          return 'Habitación Estandar: Estas habitaciones ofrecen vistas al jardín principal y cuentan con un balcón privado. Disponen de una cama de 2x2m y un baño con bañera.';
        case 3:
          return 'Habitación Deluxe: Estas habitaciones ofrecen vistas panorámicas al mar y cuentan con un balcón privado. Disponen de una cama de 2x2m, una sala de estar y un baño con bañera de hidromasaje.';
        default:
          return 'Descripción no disponible.';
      }
    }
  }
};
</script>

<style scoped>
.card {
  border: none;
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

.amenities {
  margin-top: 10px;
}

.amenities i {
  margin-right: 10px;
  font-size: 1.5rem;
  color: #555;
}

.card-description {
  margin-top: 10px;
  font-style: italic;
}

.separator {
  border: none;
  border-top: 1px solid #d3d3d3;
  margin: 20px 0;
}
</style>