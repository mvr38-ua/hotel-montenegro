<template>
  <div class="container mt-4">
    <h1 class="mb-4">Listado de Habitaciones Disponibles</h1>
    <div v-if="categoriasCargadas" class="row" v-for="habitacion in habitacionesFiltradas" :key="habitacion.id">
      <div class="col-md-12">
        <div class="card mb-4">
          <div class="row no-gutters">
            <div class="col-md-4">
              <img :src="getImage(habitacion.categoriaId)" class="card-img" alt="Imagen de la habitación">
            </div>
            <div class="col-md-8">
              <div class="card-body">
                <h5 class="card-title">Habitación {{ getCategoriaNombre(habitacion.categoriaId) }}</h5>
                <p class="card-text"><strong>Fechas:</strong> {{ formatDate(fechaInicio) }} - {{ formatDate(fechaFinal) }}</p>
                <p class="card-text"><strong>Precio desde:</strong> {{ habitacion.precioBase }} €/noche</p>
                <button class="btn btn-custom" @click="reservar(habitacion)">Reservar</button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div v-else>
      <p>Cargando categorías...</p>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import { useRouter } from 'vue-router';
import categoria1 from '../assets/estandar.png';
import categoria2 from '../assets/economy.png';
import categoria3 from '../assets/deluxe.png';

export default {
  data() {
    return {
      habitaciones: [],
      categorias: {},
      categoriasCargadas: false,
      fechaInicio: '',
      fechaFinal: ''
    };
  },
  computed: {
    habitacionesFiltradas() {
      const categorias = new Set();
      return this.habitaciones.filter(habitacion => {
        if (!categorias.has(habitacion.categoriaId)) {
          categorias.add(habitacion.categoriaId);
          return true;
        }
        return false;
      });
    }
  },
  created() {
    const disponibles = this.$route.query.disponibles;
    if (disponibles) {
      try {
        const parsedDisponibles = JSON.parse(disponibles);
        if (Array.isArray(parsedDisponibles)) {
          this.habitaciones = parsedDisponibles;
        } else {
          console.error('Parsed disponibles is not an array:', parsedDisponibles);
          this.habitaciones = [];
        }
      } catch (e) {
        console.error('Error parsing habitaciones:', e);
        this.habitaciones = [];
      }
      this.fechaInicio = this.$route.query.fechaInicio;
      this.fechaFinal = this.$route.query.fechaFinal;
    }
    this.fetchCategorias();
  },
  methods: {
    async fetchCategorias() {
      try {
        const response = await axios.get('http://localhost:5288/api/Categoriums');
        if (response.data && Array.isArray(response.data.$values)) {
          this.categorias = response.data.$values.reduce((acc, categoria) => {
            acc[categoria.id] = categoria.nombre;
            return acc;
          }, {});
          this.categoriasCargadas = true;
        } else {
          console.error('API response does not contain $values array:', response.data);
        }
      } catch (error) {
        console.error('Error fetching categories:', error);
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
      return this.categorias[categoriaId] || 'Desconocida';
    },
    formatDate(date) {
      const d = new Date(date);
      const day = String(d.getDate()).padStart(2, '0');
      const month = String(d.getMonth() + 1).padStart(2, '0');
      const year = d.getFullYear();
      return `${day}/${month}/${year}`;
    },
    reservar(habitacion) {
      this.$router.push({
        path: '/detalles',
        query: {
          habitacion: JSON.stringify(habitacion),
          fechaInicio: this.fechaInicio,
          fechaFinal: this.fechaFinal
        }
      });
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
</style>