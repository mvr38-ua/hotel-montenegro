<template>
  <div class="container mt-4">
    <!-- Buscador -->
    <div class="search-bar mb-4 bg-gray p-4 rounded shadow">
      <div class="row">
        <div class="col-md-3 col-sm-6 mb-2">
          <input type="date" v-model="fechaInicio" class="form-control" placeholder="Fecha de llegada" />
        </div>
        <div class="col-md-3 col-sm-6 mb-2">
          <input type="date" v-model="fechaFinal" class="form-control" placeholder="Fecha de salida" />
        </div>
        <div class="col-md-3 col-sm-6 mb-2">
          <input type="number" v-model="numPersonas" class="form-control" placeholder="Número de personas" />
        </div>
        <div class="col-md-3 col-sm-6 mb-2">
          <button class="btn btn-elegant w-100" @click="buscarHabitaciones">Buscar</button>
        </div>
      </div>
    </div>

    <!-- Imagen grande -->
    <div class="main-image mb-4">
      <img src="../assets/imagen_hotel.png" class="img-fluid" alt="Main Image" />
    </div>

    <!-- Iconos con frases -->
    <div class="row mb-4">
      <div class="col-md-4 col-sm-6 text-center mb-2">
        <i class="fas fa-tag fa-2x"></i>
        <p>Mejor precio garantizado</p>
      </div>
      <div class="col-md-4 col-sm-6 text-center mb-2">
        <i class="fas fa-hand-holding-usd fa-2x"></i>
        <p>Si encuentras un precio mejor te lo mejoramos</p>
      </div>
      <div class="col-md-4 col-sm-6 text-center mb-2">
        <i class="fas fa-umbrella-beach fa-2x"></i>
        <p>En 1ª línea de playa de Calpe</p>
      </div>
      <div class="col-md-4 col-sm-6 text-center mb-2">
        <i class="fas fa-recycle fa-2x"></i>
        <p>Tu reserva 1kg de plástico del mar</p>
      </div>
      <div class="col-md-4 col-sm-6 text-center mb-2">
        <i class="fas fa-credit-card fa-2x"></i>
        <p>Pago en tu llegada al hotel</p>
      </div>
      <div class="col-md-4 col-sm-6 text-center mb-2">
        <i class="fas fa-glass-cheers fa-2x"></i>
        <p>Descuentos en bares y restaurantes</p>
      </div>
    </div>

    <!-- Segunda imagen -->
    <div class="secondary-image mb-4">
      <img src="../assets/principal.png" class="img-fluid" alt="Secondary Image" />
    </div>

    <!-- Texto final -->
    <div class="final-text">
      <h2><strong>Descubre el Lujo y Confort de Nuestras Habitaciones</strong></h2>
      <p>
        Las habitaciones de nuestro hotel están diseñadas para ofrecer el máximo confort y lujo. Cada habitación cuenta con una televisión Ultra HD para disfrutar de tus programas favoritos con la mejor calidad de imagen. Los espacios son amplios, proporcionando un ambiente relajante y cómodo. Además, todas las habitaciones incluyen un minibar bien surtido para tu conveniencia.
      </p>
      <p>
        Disfruta de una amplia terraza individual donde podrás relajarte y deleitarte con la vista. Para tu comodidad, las habitaciones están equipadas con calefacción y aire acondicionado, asegurando una temperatura agradable en cualquier época del año. Nuestra atención a los detalles y el diseño elegante hacen que cada estancia sea una experiencia inolvidable.
      </p>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import { useRouter } from 'vue-router';

export default {
  name: 'BuscarHabitaciones',
  data() {
    return {
      fechaInicio: '',
      fechaFinal: '',
      numPersonas: 1,
    };
  },
  setup() {
    const router = useRouter();
    return { router };
  },
  methods: {
    async buscarHabitaciones() {
      try {
        const reservasResponse = await axios.get('http://localhost:5288/api/Reserva');
        const habitacionesResponse = await axios.get('http://localhost:5288/api/Habitaciones');

        const reservas = reservasResponse.data.$values;
        const habitaciones = habitacionesResponse.data.$values;

        if (Array.isArray(reservas) && Array.isArray(habitaciones)) {
          const habitacionesDisponibles = habitaciones.filter(habitacion => {
            return habitacion.capacidad >= this.numPersonas && !reservas.some(reserva => {
              const reservaInicio = new Date(reserva.fechaInicio);
              const reservaFinal = new Date(reserva.fechaFinal);
              const fechaInicio = new Date(this.fechaInicio);
              const fechaFinal = new Date(this.fechaFinal);

              return reserva.habitacionId === habitacion.id &&
                  ((fechaInicio >= reservaInicio && fechaInicio <= reservaFinal) ||
                      (fechaFinal >= reservaInicio && fechaFinal <= reservaFinal) ||
                      (reservaInicio >= fechaInicio && reservaInicio <= fechaFinal) ||
                      (reservaFinal >= fechaInicio && reservaFinal <= fechaFinal));
            });
          });

          this.router.push({
            path: '/habitaciones',
            query: {
              disponibles: JSON.stringify(habitacionesDisponibles),
              fechaInicio: this.fechaInicio,
              fechaFinal: this.fechaFinal
            }
          });
        } else {
          console.error('Error: reservas or habitaciones is not an array');
        }
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    }
  }
};
</script>

<style scoped>
.search-bar {
  background-color: #f0f0f0; /* Gray background */
  padding: 20px;
  border-radius: 10px; /* Rounded corners */
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); /* Shadow */
}

.search-bar input, .search-bar button {
  margin-bottom: 10px;
}

.btn-elegant {
  background-color: #2c3e50; /* Dark blue color */
  color: white;
  border: none;
  border-radius: 5px;
  padding: 10px 20px;
  transition: background-color 0.3s ease;
}

.btn-elegant:hover {
  background-color: #1a252f; /* Darker shade on hover */
}

.main-image img, .secondary-image img {
  width: 100%;
  height: auto;
}

.text-center i {
  display: block;
  margin-bottom: 10px;
}

.final-text {
  text-align: center;
  margin-top: 20px;
}

@media (max-width: 768px) {
  .main-image img, .secondary-image img {
    width: 100%;
  }
}
</style>