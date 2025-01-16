<template>
    <div class="recepcion-container">
      <!-- Listado de Reservas -->
      <div class="reservas-section">
        <h2>Listado de Reservas</h2>
        <ul>
          <li v-for="reserva in reservas" :key="reserva.id" class="reserva-item">
            <span>{{ reserva.id }} - {{ reserva.usuarioNombre }}</span>
            <button @click="confirmarCancelar(reserva.id)">Cancelar</button>
          </li>
        </ul>
      </div>
  
      <!-- Listado de Habitaciones -->
      <div class="habitaciones-section">
        <h2>Listado de Habitaciones</h2>
        <ul>
          <li v-for="habitacion in habitaciones" :key="habitacion.id" class="habitacion-item">
            <span>{{ habitacion.numero }}</span>
            <button v-if="!habitacion.bloqueada" @click="bloquearHabitacion(habitacion.id)">Bloquear</button>
            <button v-else @click="desbloquearHabitacion(habitacion.id)">Desbloquear</button>
          </li>
        </ul>
      </div>
  
      <!-- Formulario de Reservas -->
      <div class="formulario-section">
        <h2>Formulario de Reservas</h2>
        <form @submit.prevent="crearReserva">
          <div class="form-group" v-for="(valor, campo) in formulario" :key="campo">
            <label :for="campo">{{ campo | capitalize }}</label>
            <input
              v-if="campo !== 'capacidadMinima' && campo !== 'tipoHabitacion'"
              :id="campo"
              v-model="formulario[campo]"
              :type="campo.includes('fecha') ? 'date' : 'text'"
              :required="campo !== 'capacidadMinima' && campo !== 'tipoHabitacion'"
            />
            <input
              v-else
              :id="campo"
              v-model="formulario[campo]"
              type="text"
            />
          </div>
          <button type="submit">Reservar</button>
        </form>
      </div>
    </div>
  
    <!-- Modal para Confirmación -->
    <div v-if="mostrarModal" class="modal-overlay">
      <div class="modal">
        <p>¿Estás seguro de que deseas cancelar esta reserva?</p>
        <button @click="cancelarReserva(reservaIdModal)">Confirmar</button>
        <button @click="cerrarModal">Cerrar</button>
      </div>
    </div>
  </template>
  
  <script setup lang="ts">
  import { ref } from 'vue';
  import reservaService from '../services/reservaService';
  import habitacionService from '../services/habitacionService';
  
  const reservas = ref([]);
  const habitaciones = ref([]);
  const formulario = ref({
    nombre: '',
    apellidos: '',
    correo: '',
    movil: '',
    fechaEntrada: '',
    fechaSalida: '',
    capacidadMinima: '',
    tipoHabitacion: '',
  });
  const mostrarModal = ref(false);
  const reservaIdModal = ref(null);
  
  // Carga inicial de datos
  const cargarDatos = async () => {
    reservas.value = await reservaService.obtenerReservas();
    habitaciones.value = await habitacionService.obtenerHabitaciones();
  };
  
  // Confirmar cancelación
  const confirmarCancelar = (id) => {
    reservaIdModal.value = id;
    mostrarModal.value = true;
  };
  
  // Cerrar modal
  const cerrarModal = () => {
    reservaIdModal.value = null;
    mostrarModal.value = false;
  };
  
  // Cancelar reserva
  const cancelarReserva = async (id) => {
    await reservaService.cancelarReserva(id);
    cerrarModal();
    cargarDatos();
  };
  
  // Bloquear habitación
  const bloquearHabitacion = async (id) => {
    await habitacionService.bloquearHabitacion(id);
    cargarDatos();
  };
  
  // Desbloquear habitación
  const desbloquearHabitacion = async (id) => {
    await habitacionService.desbloquearHabitacion(id);
    cargarDatos();
  };
  
  // Crear reserva
  const crearReserva = async () => {
    await reservaService.crearReserva(formulario.value);
    alert('Reserva creada exitosamente');
    cargarDatos();
    Object.keys(formulario.value).forEach((key) => (formulario.value[key] = ''));
  };
  
  // Inicialización
  cargarDatos();
  </script>
 <style scoped>
 /* Ajuste de contenedores específicos */
 .reserva-recepcion-container {
   display: grid;
   grid-template-columns: 1fr 2fr;
   gap: 20px;
   padding: 1rem;
 }
 
 /* Listado de reservas */
 .reservas-listado {
   display: flex;
   flex-direction: column;
   gap: 1rem;
 }
 
 .reserva-item {
   display: flex;
   justify-content: space-between;
   align-items: center;
   background: var(--background-light, #f5f5f5);
   padding: 0.5rem;
   border-radius: 5px;
 }
 
 .reserva-item button {
   background-color: #d9534f;
   color: white;
 }
 
 .reserva-item button:hover {
   background-color: #c9302c;
 }
 
 /* Listado de habitaciones */
 .habitaciones-listado {
   display: flex;
   flex-direction: column;
   gap: 1rem;
 }
 
 .habitacion-item {
   display: flex;
   justify-content: space-between;
   align-items: center;
   background: var(--background-light, #f5f5f5);
   padding: 0.5rem;
   border-radius: 5px;
 }
 
 .habitacion-item .bloquear-btn {
   background-color: #f0ad4e;
   color: white;
 }
 
 .habitacion-item .bloquear-btn:hover {
   background-color: #ec971f;
 }
 
 .habitacion-item .desbloquear-btn {
   background-color: #5cb85c;
   color: white;
 }
 
 .habitacion-item .desbloquear-btn:hover {
   background-color: #4cae4c;
 }
 
 /* Formulario de reserva */
 .formulario-reserva {
   background: var(--background-light, #ffffff);
   padding: 1rem;
   border-radius: 5px;
   box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
 }
 
 .formulario-reserva .form-group {
   margin-bottom: 1rem;
 }
 
 .formulario-reserva label {
   display: block;
   font-size: 0.9rem;
   margin-bottom: 0.3rem;
 }
 
 .formulario-reserva input,
 .formulario-reserva select {
   width: 100%;
   padding: 0.5rem;
   font-size: 1rem;
   border: 1px solid #ddd;
   border-radius: 4px;
 }
 
 .formulario-reserva button {
   margin-top: 1rem;
   background-color: #007bff;
   color: white;
 }
 
 .formulario-reserva button:hover {
   background-color: #0056b3;
 }
 </style>
 