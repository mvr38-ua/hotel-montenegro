<template>
    <div class="gestion-reservas">
      <div class="header-container">
        <h2>Listado de Reservas</h2>
      </div>
      <table class="table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Usuario</th>
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
            <td>{{ reserva.usuario.nombre }}</td>
            <td>{{ reserva.habitacion.numero }}</td>
            <td>{{ reserva.fechaInicio }}</td>
            <td>{{ reserva.fechaFinal }}</td>
            <td>{{ obtenerNombresServicios(reserva.servicios) }}</td>
            <td>{{ reserva.precioTotal }}</td>
            <td>
              <button class="botonBorrar" @click="eliminarReservaHandler(reserva.id)">🗑️</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </template>
  
  <script setup>
  import { ref, onMounted } from "vue";
  import reservasService from "../services/reservasService";
  
  const reservas = ref([]);
  
  const obtenerNombresServicios = (servicios) => {
  if (!servicios || !Array.isArray(servicios.$values)) return "N/A";
  return servicios.$values.map(servicio => servicio.nombre).join(", ");
 };

  const cargarReservas = async () => {
    try {
      const response = await reservasService.obtenerReservas();
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
  
  const eliminarReservaHandler = async (id) => {
    if (confirm("¿Estás seguro de que quieres eliminar esta reserva?")) {
      try {
        await reservasService.eliminarReserva(id);
        cargarReservas();
      } catch (error) {
        console.error("Error al eliminar reserva:", error);
      }
    }
  };
  
  onMounted(() => {
    cargarReservas();
  });
  </script>
  
  <style>
  .gestion-reservas {
  padding: 20px;
  display: flex;
  flex-direction: column;
}

.header-container {
  display: flex;
  align-items: center;
  margin-bottom: 20px;
  justify-content: space-between;
}

h2 {
  margin-bottom: 20px;
  font-size: 1.5rem;
  font-family: 'Montserrat', sans-serif;
  font-weight: 800;
  display: inline-block;
  margin-right: 20px; /* Espacio entre el título y el botón */
}

.table {
  width: 100%;
  border-collapse: collapse;
  background-color: #f9f9f9;
}

.table thead {
  background-color: #ccc;
}

.table th,
.table td {
  text-align: left;
  padding: 10px;
  border: 1px solid #ddd;
  vertical-align: middle; /* Alinea verticalmente */
}

.table th {
  font-weight: bold;
}

.botonBorrar {
  display: flex;
  justify-content: center; /* Centra horizontalmente el botón */
  align-items: center; /* Alinea verticalmente si es necesario */
  width: 100%; /* Asegura que el contenido ocupe toda la celda */
}

.botonBorrar {
  background: none;
  border: none;
  cursor: pointer;
  font-size: 16px;
  color: #dc3545;
}

.botonBorrar:hover {
  color: #a71d2a;
}
  </style>
  