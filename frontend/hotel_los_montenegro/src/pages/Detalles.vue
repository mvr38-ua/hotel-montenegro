<template>
  <div id="app" class="container mt-4">
    <!-- Contenedor Principal -->
    <div class="row">
      <!-- Imagen y Descripción -->
      <div class="col-md-6">
        <img
            class="img-fluid rounded"
            :src="imageSrc"
            alt="Room"
        />
      </div>
      <div class="col-md-6">
        <h1 class="mb-3">Habitación {{ getCategoriaNombre(habitacion.categoriaId) }}</h1>
        <div class="d-flex justify-content-center mb-3 icon-container">
          <i class="fas fa-bed me-3"></i>
          <i class="fas fa-wifi me-3"></i>
          <i class="fas fa-tv me-3"></i>
          <i class="fas fa-shower me-3"></i>
        </div>
        <p class="fw-bold">
          Ocupación: {{ habitacion.capacidad }} Adultos (Máximo por Habitación)
        </p>
        <p>{{ getCategoriaDescripcion(habitacion.categoriaId) }}</p>
        <p><strong>Fechas:</strong> {{ formatDate(fechaInicio) }} - {{ formatDate(fechaFinal) }}</p>
        <div class="mb-3 text-center">
          <label for="guests" class="form-label">Ocupantes:</label>
          <input type="number" id="guests" class="form-control w-auto mx-auto" :max="habitacion.capacidad" min="1" v-model="selectedOccupants">
        </div>
      </div>
    </div>
    <!-- Lista de Precios -->
    <div class="mt-5">
      <h2 class="mb-4">Precios</h2>
      <ul class="list-group">
        <li
            class="list-group-item d-flex justify-content-between align-items-center"
        >
          <div class="text-left">
            <strong>Sólo Alojamiento</strong>
            <p class="mb-0 text-muted">{{ calcularPrecioTotal(habitacion.precioBase) }} €</p>
          </div>
          <button class="btn btn-custom" @click="toggleServiceSection">Reservar</button>
        </li>
        <li
            class="list-group-item d-flex justify-content-between align-items-center"
        >
          <div class="text-left">
            <strong>Alojamiento y desayuno</strong>
            <p class="mb-0 text-muted">{{ calcularPrecioTotal(habitacion.precioBase) }} € + {{ calcularPrecioServicio(servicePrices.desayuno) }} € /Total</p>
          </div>
          <button class="btn btn-custom" @click="toggleServiceSection">Reservar</button>
        </li>
        <li
            class="list-group-item d-flex justify-content-between align-items-center"
        >
          <div class="text-left">
            <strong>Media pensión</strong>
            <p class="mb-0 text-muted">{{ calcularPrecioTotal(habitacion.precioBase) }} € + {{ calcularPrecioServicio(servicePrices.mediaPension) }} € /Total</p>
          </div>
          <button class="btn btn-custom" @click="toggleServiceSection">Reservar</button>
        </li>
        <li
            class="list-group-item d-flex justify-content-between align-items-center"
        >
          <div class="text-left">
            <strong>Pensión completa</strong>
            <p class="mb-0 text-muted">{{ calcularPrecioTotal(habitacion.precioBase) }} € + {{ calcularPrecioServicio(servicePrices.pensionCompleta) }} € /Total</p>
          </div>
          <button class="btn btn-custom" @click="toggleServiceSection">Reservar</button>
        </li>
      </ul>
    </div>
    <!-- Sección de Servicios -->
    <div v-if="showServiceSection" class="service-section mt-4">
      <h3>Seleccione los servicios que desea para completar la reserva</h3>
      <div v-if="loading">Loading services...</div>
      <div v-else>
        <div v-for="service in filteredServices" :key="service.id" class="service-item">
          <label>
            <input type="checkbox" v-model="service.selected" />
            <span>{{ service.nombre }} - {{ service.precioServicio }}€</span>
          </label>
        </div>
      </div>
      <div class="service-buttons mt-3">
        <button class="btn btn-custom" @click="confirmSelection">Confirmar Reserva</button>
      </div>
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
      habitacion: null,
      fechaInicio: '',
      fechaFinal: '',
      imageSrc: '',
      servicePrices: {
        desayuno: 0,
        mediaPension: 0,
        pensionCompleta: 0
      },
      selectedOccupants: 1,
      showServiceSection: false,
      services: [],
      filteredServices: [],
      loading: true
    };
  },
  created() {
    const habitacion = this.$route.query.habitacion;
    if (habitacion) {
      this.habitacion = JSON.parse(habitacion);
      this.fechaInicio = this.$route.query.fechaInicio;
      this.fechaFinal = this.$route.query.fechaFinal;
      this.imageSrc = this.getImage(this.habitacion.categoriaId);
      this.fetchServicePrices();
      this.fetchAdditionalServices();
    }
  },
  methods: {
    async fetchServicePrices() {
      try {
        const response = await axios.get('http://localhost:5288/api/Servicios');
        if (response.data && response.data.$values) {
          const services = response.data.$values;
          this.servicePrices = {
            desayuno: services.find(service => service.nombre === 'Desayuno')?.precioServicio || 0,
            mediaPension: services.find(service => service.nombre === 'Media Pensión')?.precioServicio || 0,
            pensionCompleta: services.find(service => service.nombre === 'Pensión Completa')?.precioServicio || 0
          };
        }
      } catch (error) {
        console.error('Error fetching service prices:', error);
      }
    },
    async fetchAdditionalServices() {
      try {
        const response = await axios.get('http://localhost:5288/api/Servicios');
        if (response.data && response.data.$values) {
          this.services = response.data.$values;
          this.filteredServices = this.services.filter(service =>
              !['Desayuno', 'Media Pensión', 'Pensión Completa'].includes(service.nombre)
          );
          this.loading = false;
        }
      } catch (error) {
        console.error('Error fetching additional services:', error);
      }
    },
    toggleServiceSection() {
      this.showServiceSection = !this.showServiceSection;
    },
    confirmSelection() {
      const selectedServices = this.filteredServices.filter(service => service.selected);
      console.log('Selected additional services:', selectedServices);
      this.showServiceSection = false;
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
    },
    formatDate(date) {
      const d = new Date(date);
      const day = String(d.getDate()).padStart(2, '0');
      const month = String(d.getMonth() + 1).padStart(2, '0');
      const year = d.getFullYear();
      return `${day}/${month}/${year}`;
    },
    calcularPrecioTotal(precioBase) {
      const fechaInicio = new Date(this.fechaInicio);
      const fechaFinal = new Date(this.fechaFinal);
      const diffTime = Math.abs(fechaFinal - fechaInicio);
      const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
      return (precioBase * diffDays).toFixed(2);
    },
    calcularPrecioServicio(precioServicio) {
      const fechaInicio = new Date(this.fechaInicio);
      const fechaFinal = new Date(this.fechaFinal);
      const diffTime = Math.abs(fechaFinal - fechaInicio);
      const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
      return (precioServicio * diffDays).toFixed(2);
    }
  }
};
</script>

<style>
.card {
  border: 1px solid #ddd;
  border-radius: 4px;
}

.card-title {
  font-size: 1.25rem;
  font-weight: bold;
  text-align: left;
}

.card-text {
  text-align: left;
}

.card-body {
  text-align: left;
}

.card-img {
  width: 100%;
  height: auto;
}

.btn-custom {
  background-color: lightgray;
  color: black;
  border: 1px solid black;
  border-radius: 5px;
  padding: 10px 20px;
  transition: background-color 0.3s ease;
  text-align: left;
}

.btn-custom:hover {
  background-color: darkgray;
}

.text-center {
  text-align: center;
}

.text-left {
  text-align: left;
}

.icon-container {
  justify-content: center;
}

.icon-container i {
  font-size: 1.5rem;
  margin-right: 15px;
  color: lightgray;
}

.service-section {
  background-color: #f8f9fa; /* Light background color */
  color: #343a40; /* Dark text color for better visibility */
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 0 15px rgba(0, 0, 0, 0.1);
}

.service-item {
  margin-bottom: 10px;
}

.service-buttons {
  display: flex;
  justify-content: flex-end;
  margin-top: 20px;
}

.service-buttons button {
  background-color: lightgray;
  color: black;
  border: 1px solid black;
  border-radius: 5px;
  padding: 10px 20px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.service-buttons button:hover {
  background-color: #0056b3;
}
</style>