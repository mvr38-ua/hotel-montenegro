<template>
    <div class="reservas">
      <h1>Mis reservas</h1>
      <table class="tabla-reservas">
        <thead>
          <tr>
            <th>Número de reserva</th>
            <th>Habitación</th>
            <th>Fecha Inicio</th>
            <th>Fecha Fin</th>
            <th>Servicios</th>
            <th>Total</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="reserva in reservas" :key="reserva.id">
            <td>{{ reserva.id }}</td>
            <td>{{ reserva.habitacion.numero }}</td>
            <td>{{ reserva.fechaInicio }}</td>
            <td>{{ reserva.fechaFinal }}</td>
            <td>{{ obtenerNombresServicios(reserva.servicios) }}</td>
            <td>{{ reserva.precioTotal  + " €" }}</td>
            <td>
                <button class="btn-cancelar" @click="cancelarReserva(reserva.id)">Cancelar</button>
            </td>
        </tr>
        <tr v-if="reservas.length === 0">
          <td colspan="10">No se encontraron reservas.</td>
        </tr>
      </tbody>
    </table>
    <div class="paginacion">
      <button @click="cambiarPagina(paginaActual - 1)" :disabled="paginaActual === 1">Anterior</button>
      <span>Página {{ paginaActual }} de {{ totalPaginas }}</span>
      <button @click="cambiarPagina(paginaActual + 1)" :disabled="paginaActual === totalPaginas">Siguiente</button>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from "vue";
import reservasService from '../services/reservasService';
import authService from "../services/authService";

// Variables reactivas
const reservas = ref([]);
const usuarioId = authService.obtenerUsuarioDelToken(); 
const paginaActual = ref(1);
const filasPorPagina = ref(10);

// Variables calculadas
const totalPaginas = computed(() => Math.ceil(reservas.value.length / filasPorPagina.value));
const reservasPaginadas = computed(() =>
  reservas.value.slice(
    (paginaActual.value - 1) * filasPorPagina.value,
    paginaActual.value * filasPorPagina.value
  )
);
const obtenerNombresServicios = (servicios) => {
  if (!servicios || !Array.isArray(servicios.$values)) return "N/A";
  return servicios.$values.map(servicio => servicio.nombre).join(", ");
 };

// Función para cargar reservas del usuario
const cargarReservas = async () => {
    try {
      const response = await reservasService.obtenerReservasPorUsuario(usuarioId);
      reservas.value = response.$values.map(reserva => ({
      ...reserva,
      fechaInicio: reserva.fechaInicio 
        ? new Date(reserva.fechaInicio).toISOString().split("T")[0] // Formatear fechaInicio
        : null, // Manejar fechas nulas si es necesario
      fechaFinal: reserva.fechaFinal 
        ? new Date(reserva.fechaFinal).toISOString().split("T")[0] // Formatear fechaFinal
        : null, // Manejar fechas nulas si es necesario
    }));
    } catch (error) {
      console.error("Error al cargar reservas:", error);
    }
  };

// Función para cancelar una reserva
const cancelarReserva = async (id) => {
  if (confirm("¿Estás seguro de que deseas cancelar esta reserva?")) {
    try {
      await reservasService.eliminarReserva(id);
      alert("Reserva cancelada con éxito");
      cargarReservas();
    } catch (error) {
      console.error("Error al cancelar reserva:", error.message);
      alert("Hubo un error al cancelar la reserva.");
    }
  }
};

// Función para cambiar de página
const cambiarPagina = (nuevaPagina) => {
  if (nuevaPagina >= 1 && nuevaPagina <= totalPaginas.value) {
    paginaActual.value = nuevaPagina;
  }
};

// Cargar reservas al montar el componente
onMounted(() => {
  cargarReservas();
});
</script>

<style>
.reservas {
  padding: 20px;
}

.tabla-reservas {
  width: 100%;
  border-collapse: collapse;
  margin-top: 20px;
}

.tabla-reservas th,
.tabla-reservas td {
  border: 1px solid #ddd;
  padding: 8px;
  text-align: center;
}

.tabla-reservas th {
  background-color: #f4f4f4;
}

.btn-cancelar {
  background-color: #f44336;
  color: white;
  border: none;
  padding: 5px 10px;
  cursor: pointer;
  border-radius: 3px;
}

.btn-cancelar:hover {
  background-color: #d32f2f;
}

.paginacion {
  margin-top: 20px;
  display: flex;
  justify-content: center;
  align-items: center;
}

.paginacion button {
  margin: 0 5px;
  padding: 5px 10px;
  border: none;
  background-color: #007bff;
  color: white;
  border-radius: 3px;
  cursor: pointer;
}

.paginacion button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}

.paginacion span {
  margin: 0 10px;
}
</style>
