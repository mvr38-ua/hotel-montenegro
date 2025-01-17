<template>
    <div class="pago">
      <button @click="realizarPago">Realizar Pago</button>
      <div v-if="urlPago">
        <p>Por favor, completa el pago en: <a :href="urlPago" target="_blank">Ir al TPV</a></p>
      </div>
    </div>
  </template>
  
  <script setup>
import { ref, onMounted } from 'vue';
import pagoService from '../services/pagoService';
import { useRoute } from 'vue-router';

const urlPago = ref('');
const route = useRoute();  // Para acceder a los parámetros de la URL

// Obtener el precio total de la ruta (pasado como query)
const precioTotal = ref(parseFloat(route.query.precioTotal));  // Convertimos el valor a float

const realizarPago = async () => {
  if (!precioTotal.value) {
    alert('No se ha proporcionado un precio válido');
    return;
  }

  const pagoData = {
    amount: precioTotal.value,  // Usamos el precio total que viene de resumen.vue
    currency: 'EUR',
    description: 'Pago de reserva en hotel',
    reference: `ORDER-${Math.floor(Math.random() * 10000)}`,  // Generar una referencia única
    url_callback: 'http://localhost:5173/pago_exitoso', // URL donde recibirás la respuesta
  };

  try {
    const response = await pagoService.procesarPago(pagoData);
    urlPago.value = response.url; // Guardar la URL del TPV
  } catch (error) {
    console.error('Error al realizar el pago:', error);
  }
};
</script>

  
  <style>
  .pago {
    padding: 20px;
    text-align: center;
  }
  
  button {
    background-color: #28a745;
    color: white;
    border: none;
    padding: 10px 20px;
    cursor: pointer;
  }
  
  button:hover {
    background-color: #218838;
  }
  </style>
  