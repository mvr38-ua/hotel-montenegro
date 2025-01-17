<template>
    <div class="resumen">
      <div class="contenedor">
        <!-- Caja izquierda -->
        <div class="caja">
          <h2>Resumen de tu Reserva</h2>
          <h4>Datos del formulario</h4>
          <p>Nombre: {{ formulario.nombre }}</p>
          <p>Apellidos: {{ formulario.apellidos }}</p>
          <p>Correo: {{ formulario.correo }}</p>
          <p>Móvil: {{ formulario.movil }}</p>
  
          <h4>Estancia</h4>
          <p>Fecha Entrada: {{ formulario.fechaEntrada }}</p>
          <p>Fecha Salida: {{ formulario.fechaSalida }}</p>
  
          <h4>Habitación Seleccionada</h4>
          <p>Número: {{ habitacion.numero }}</p>
          <p>Precio por día: {{ habitacion.precioBase }} €</p>
          <p>Número: {{ habitacion.numero }}</p>
          <p>Capacidad máxima: {{ habitacion.capacidad }}</p>
  
          <h4>Servicios Seleccionados</h4>
          <ul>
            <li v-for="servicio in servicios" :key="servicio.id">
              {{ servicio.nombre }}: {{ servicio.precioServicio }} €
            </li>
          </ul>
        </div>
  
        <!-- Caja derecha -->
        <div class="caja">
          <h2>Resumen del Pago</h2>
          <p><strong>Precio habitación:</strong> {{ precioHabitacion }} €</p>
          <p><strong>Días alojado:</strong> {{ diasEstancia }} </p>
          <p><strong>Servicios adicionales:</strong> {{ precioServicios }} €</p>
          <hr>
          <p><strong>Precio total:</strong> {{ precioTotal }} €</p>
          <button class="btn-proceder" @click="procederPago">Proceder con el pago</button>
        </div>
      </div>
    </div>
  </template>
  
  <script setup lang="ts">
  import { reservaGlobal, limpiarReserva } from '../store/reservaGlobal';
  import { useRouter } from 'vue-router';
  import { computed } from 'vue';
  import reservaService from '../services/reservasService';
  
  // Accede a los datos globales
  const formulario = reservaGlobal.formulario;
  const habitacion = reservaGlobal.habitacion;
  const servicios = reservaGlobal.servicios;
  
  // Cálculo de días entre fechas
  const calcularDias = (fechaInicio: string, fechaFin: string) => {
    const fecha1 = new Date(fechaInicio);
    const fecha2 = new Date(fechaFin);
    const diferencia = fecha2.getTime() - fecha1.getTime();
    return Math.ceil(diferencia / (1000 * 60 * 60 * 24)); // Conversión a días
  };
  
  // Cálculos
  const diasEstancia = computed(() =>
    formulario.fechaEntrada && formulario.fechaSalida
      ? calcularDias(formulario.fechaEntrada, formulario.fechaSalida)
      : 0
  );
  
  const precioHabitacion = computed(() =>
    habitacion && habitacion.precioBase ? diasEstancia.value * habitacion.precioBase : 0
  );
  
  const precioServicios = computed(() =>
    servicios && servicios.length
      ? servicios.reduce((total, servicio) => total + servicio.precioServicio, 0)
      : 0
  );
  
  const precioTotal = computed(() => precioHabitacion.value + precioServicios.value);
  
  // Función para proceder con el pago
  const router = useRouter();

  const procederPago = /*async*/ () => {
    try{
        // Preparar el JSON
        const reserva = {
            fechaInicio: formulario.fechaEntrada,
            fechaFinal: formulario.fechaSalida,
            precioTotal: precioTotal,
            usuarioId: 1,
            habitacionId: habitacion.id,
            servicios: servicios,
        };

        // Realizar el POST
        //const response = await reservaService.crearReserva(reserva);
        //console.log('Reserva realizada con éxito:', response);

        // Limpiar reserva global y redirigir
        limpiarReserva();
        alert('Reserva realizada con éxito');
        router.push('/pago');
        
    } catch (error) {
        console.error('Error al realizar la reserva:', error);
        alert('Hubo un problema al realizar la reserva');
    }
};

  </script>
  
  <style scoped>
  .contenedor {
    display: flex;
    justify-content: space-between;
    gap: 20px;
    margin-top: 20px;
    padding: 0 20px; /* Añadimos margen lateral */
  }
  
  .caja {
    background-color: #f7f7f7;
    padding: 20px;
    border: 1px solid #ddd;
    border-radius: 8px;
    width: 45%;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  }
  
  h2 {
    margin-bottom: 15px;
  }
  
  h4 {
    margin-top: 20px;
    margin-bottom: 10px;
  }
  
  ul {
    list-style-type: none;
    padding: 0;
  }
  
  li {
    margin-bottom: 5px;
  }
  
  .btn-proceder {
    background-color: #4caf50;
    color: white;
    border: none;
    padding: 10px 15px;
    border-radius: 5px;
    cursor: pointer;
    margin-top: 20px;
    width: 100%;
    font-size: 16px;
  }
  
  .btn-proceder:hover {
    background-color: #45a049;
  }
  </style>
  
  