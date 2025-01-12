<template>
    <div class="gestion-habitaciones">
      <h2>Listado de habitaciones</h2>
      <button class="add-button" @click="nuevaHabitacion">Añadir nueva</button>
      <table class="table">
        <thead>
          <tr>
            <th>Número</th>
            <th>Precio Base</th>
            <th>Capacidad</th>
            <th>Bloqueada</th>
            <th>Adaptada</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="habitacion in habitaciones" :key="habitacion.id">
            <td>{{ habitacion.numero }}</td>
            <td>{{ habitacion.precioBase }}</td>
            <td>{{ habitacion.capacidad }}</td>
            <td>{{ habitacion.bloqueada }}</td>
            <td>{{ habitacion.adaptada }}</td>
            <td>
              <button class="edit-button" @click="editarHabitacion(habitacion.id)">✏️</button>
              <button class="delete-button" @click="eliminarHabitacionHandler(habitacion.id)">🗑️</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
</template>
  
<script setup >
    import { ref, onMounted } from 'vue';
    import habitacionesService from '../services/habitacionesService';
    
    const habitaciones = ref([]);
  
    const cargarHabitaciones = async () => {
    try {
        const response = await habitacionesService.obtenerHabitaciones();
        // Asegúrate de acceder a los valores correctos dentro de la respuesta
        habitaciones.value = response.$values;
    } catch (error) {
        console.error('Error al cargar habitaciones:', error);
    }
    };

    const eliminarHabitacionHandler = async (id) => {
    if (confirm('¿Estás seguro de que quieres eliminar esta habitación?')) {
        try {
        await habitacionesService.eliminarHabitacion(id);
        await cargarHabitaciones(); // Actualizar la lista después de eliminar
        } catch (error) {
        console.error('Error al eliminar habitación:', error);
        }
    }
    };

    const nuevaHabitacion = () => {
        
    };

    const editarHabitacion = (id) => {
    alert(`Funcionalidad de edición aún no implementada para habitación con ID: ${id}`);
    };

    onMounted(cargarHabitaciones);

  
</script>

  <style>
  .gestion-habitaciones {
    padding: 20px;
  }
  
  h2 {
    margin-bottom: 20px;
    font-size: 1.5rem;
  }
  
  .add-button {
    background-color: #007bff;
    color: white;
    border: none;
    padding: 10px 20px;
    border-radius: 4px;
    font-size: 14px;
    margin-bottom: 15px;
    cursor: pointer;
  }
  
  .add-button:hover {
    background-color: #0056b3;
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
  }
  
  .table th {
    font-weight: bold;
  }
  
  .edit-button,
  .delete-button {
    background: none;
    border: none;
    cursor: pointer;
    font-size: 16px;
  }
  
  .edit-button {
    color: #007bff;
  }
  
  .delete-button {
    color: #dc3545;
  }
  
  .edit-button:hover {
    color: #0056b3;
  }
  
  .delete-button:hover {
    color: #a71d2a;
  }
  </style>
  