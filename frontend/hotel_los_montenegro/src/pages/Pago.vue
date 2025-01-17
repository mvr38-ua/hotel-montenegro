<template>
    <div class="pago">
      <button @click="realizarPago">Realizar Pago</button>
      <div v-if="urlPago">
        <p>Por favor, completa el pago en: <a :href="urlPago" target="_blank">Ir al TPV</a></p>
      </div>
    </div>
  </template>
  
  <script setup>
  import { ref } from 'vue';
  import pagoService from '../services/pagoService';
  
  const urlPago = ref('');
  
  const realizarPago = async () => {
    const pagoData = {
      amount: 29.99,
      currency: 'EUR',
      description: 'Compra de prueba',
      reference: `ORDER-${Math.floor(Math.random() * 10000)}`, // Generar una referencia única
      url_callback: 'http://localhost:5173/pago', // URL donde recibirás la respuesta
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
  