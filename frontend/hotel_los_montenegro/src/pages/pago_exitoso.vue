<template>
    <div class="container mt-4">
      <h1 class="mb-4">Pago Exitoso</h1>
      <div class="alert alert-success" role="alert">
        <h4 class="alert-heading">¡Felicidades!</h4>
        <p>Tu pago ha sido procesado exitosamente. Gracias por reservar con nosotros.</p>
        <hr>
        <p class="mb-0">Puedes verificar los detalles de tu reserva en la sección de "Mis Reservas".</p>
      </div>
      <div class="text-center mt-4">
        <button class="btn btn-primary" @click="irAReservas">Ver Mis Reservas</button>
      </div>
    </div>
  </template>
  
  <script>
  import { useRouter } from 'vue-router';
  
  export default {
    data() {
      return {
        pagoExitoso: true,
      };
    },
    methods: {
      irAReservas() {
        // Redirige a la página de reservas o detalles de usuario
        this.$router.push({ name: 'misReservas' });  // Asegúrate de tener una ruta llamada 'misReservas'
      }
    }
  };
  </script>
  
  <style scoped>
  .container {
    max-width: 800px;
    margin: 0 auto;
  }
  
  .alert {
    text-align: center;
  }
  
  .btn-primary {
    background-color: #007bff;
    border-color: #007bff;
  }
  
  .btn-primary:hover {
    background-color: #0056b3;
    border-color: #004085;
  }
  </style>
  