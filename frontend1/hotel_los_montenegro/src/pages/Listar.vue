<template>
  <div class="webmaster-page">
    <!-- Contenido principal -->
    <div class="content">
      <!-- Menú lateral -->
      <aside class="sidebar">
        <button class="sidebar-button">Gestión de habitaciones</button>
        <button class="sidebar-button">Gestión de tarifas</button>
        <button class="sidebar-button">Gestión de usuarios</button>
        <button class="sidebar-button">Informes</button>
      </aside>

      <!-- Lista de habitaciones -->
      <main class="main">
        <div class="header">
          <h1>Listado de habitaciones</h1>
          <button class="add-button">Añadir nueva</button>
        </div>
        <table class="habitaciones-table">
          <thead>
          <tr>
            <th>Número</th>
            <th>Precio Base</th>
            <th>Capacidad</th>
            <th>Bloqueada</th>
            <th>Adaptada</th>
            <th>Categoria</th>
            <th>Acciones</th>
          </tr>
          </thead>
          <tbody>
          <tr v-for="habitacion in habitaciones" :key="habitacion.id">
            <td>{{ habitacion.numero }}</td>
            <td>{{ habitacion.precioBase }}</td>
            <td>{{ habitacion.capacidad }}</td>
            <td>{{ habitacion.bloqueada ? 'Sí' : 'No' }}</td>
            <td>{{ habitacion.adaptada ? 'Sí' : 'No' }}</td>
            <td>{{ habitacion.categoria ? habitacion.categoria.nombre : 'N/A' }}</td>
            <td>
              <button @click="editar(habitacion)">✏️</button>
              <button @click="eliminar(habitacion)">🗑️</button>
            </td>
          </tr>
          </tbody>
        </table>
      </main>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import api from '../services/api';

const habitaciones = ref([]);

const fetchHabitaciones = async () => {
  try {
    const response = await api.get('/habitaciones');
    console.log('API response:', response.data); // Log the API response
    habitaciones.value = response.data.$values;
  } catch (error) {
    console.error('Error fetching habitaciones:', error);
  }
};

const editar = (habitacion) => {
  // Lógica para editar habitación
};

const eliminar = (habitacion) => {
  // Lógica para eliminar habitación
};

onMounted(() => {
  fetchHabitaciones();
});
</script>

<style scoped>
.webmaster-page {
  display: flex;
  flex-direction: column;
  height: 100vh;
}

.content {
  display: flex;
  flex: 1;
}

.sidebar {
  width: 200px;
  background-color: #f5f5f5;
  padding: 10px;
  height: 100%;
}

.sidebar-button {
  width: 100%;
  padding: 10px;
  margin-bottom: 10px;
  background-color: #d3d3d3; /* Light gray color */
  color: black;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 16px;
  transition: background-color 0.3s, transform 0.3s;
}

.sidebar-button:hover {
  background-color: #a9a9a9; /* Darker gray color */
  transform: scale(1.05);
}

.main {
  flex: 1;
  padding: 20px;
  overflow-y: auto;
}

.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
}

.habitaciones-table {
  width: 100%;
  border-collapse: collapse;
}

.habitaciones-table th, .habitaciones-table td {
  border: 1px solid #ccc;
  padding: 10px;
  text-align: left;
}

.habitaciones-table th {
  background-color: #f2f2f2;
}

.add-button {
  background-color: #808080;
  color: white;
  padding: 10px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}
</style>