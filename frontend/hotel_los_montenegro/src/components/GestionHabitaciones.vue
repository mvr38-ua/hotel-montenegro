<template>
    <div class="gestion-habitaciones">
      <div class="header-container">
        <h2>Listado de habitaciones</h2>
        <button class="add-button" @click="abrirModal(null)">Añadir nueva</button>
      </div>
      <table class="table">
        <thead>
          <tr>
            <th>Número</th>
            <th>Precio Base</th>
            <th>Capacidad</th>
            <th>Bloqueada</th>
            <th>Adaptada</th>
            <th>Categoría</th>
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
            <td>{{ habitacion.categoria.nombre }}</td>
            <td>
              <button class="edit-button" @click="abrirModal(habitacion)">✏️</button>
              <button class="delete-button" @click="eliminarHabitacionHandler(habitacion.id)">🗑️</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    
      <div v-if="mostrarModal" class="modal">
        <div class="modal-content">
          <h3>{{ habitacionIdEdicion ? 'Editar habitación' : 'Crear nueva habitación' }}</h3>
          <form @submit.prevent="guardarHabitacion">
            <label>
              Número:
              <input v-model="nuevaHabitacionData.numero" type="text" required />
            </label>
            <label>
              Precio Base:
              <input v-model="nuevaHabitacionData.precioBase" type="number" step="0.01" required />
            </label>
            <label>
              Capacidad:
              <input v-model="nuevaHabitacionData.capacidad" type="number" required />
            </label>
            <label>
              Bloqueada:
              <input v-model="nuevaHabitacionData.bloqueada" type="checkbox" />
            </label>
            <label>
              Adaptada:
              <input v-model="nuevaHabitacionData.adaptada" type="checkbox" />
            </label>
            <label>
              Categoría:
              <select v-model="categoriaSeleccionada" required>
                <option value="" disabled>Seleccione una categoría</option>
                <option v-for="categoria in categorias" :key="categoria.id" :value="categoria.id">
                  {{ categoria.nombre }}
                </option>
              </select>
            </label>
            <div class="modal-actions">
              <button type="submit" class="save-button">Guardar</button>
              <button type="button" class="cancel-button" @click="cerrarModal">Cancelar</button>
            </div>
          </form>
        </div>
      </div>
   
</template>
  
<script setup >
    import { ref, onMounted } from 'vue';
    import habitacionesService from '../services/habitacionesService';
    
    const habitaciones = ref([]);
    const nuevaHabitacionData = ref({
      numero: '',
      precioBase: '',
      capacidad: '',
      bloqueada: false,
      adaptada: false,
    });
    const mostrarModal = ref(false);
  
    const categorias = ref([]);
    const categoriaSeleccionada = ref(null);
    const habitacionIdEdicion = ref(null);

    const cargarCategorias = async () => {
      try {
        const response = await habitacionesService.obtenerCategorias();
        categorias.value = response.$values;
      } catch (error) {
        console.error('Error al cargar categorías:', error);
      }
    };

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
    const abrirModal = (habitacion) => {
  if (habitacion) {
    // Estamos editando, cargar los datos de la habitación
    habitacionIdEdicion.value = habitacion.id;
    nuevaHabitacionData.value = {
      numero: habitacion.numero,
      precioBase: habitacion.precioBase,
      capacidad: habitacion.capacidad,
      bloqueada: habitacion.bloqueada,
      adaptada: habitacion.adaptada,
    };
    categoriaSeleccionada.value = habitacion.categoria.id;
  } else {
    // Nueva habitación
    habitacionIdEdicion.value = null;
    nuevaHabitacionData.value = {
      numero: '',
      precioBase: '',
      capacidad: '',
      bloqueada: false,
      adaptada: false,
    };
    categoriaSeleccionada.value = null;
  }
  mostrarModal.value = true;
};

  const cerrarModal = () => {
    mostrarModal.value = false;
  };

  const guardarHabitacion = async () => {
    if (
    !nuevaHabitacionData.value.numero ||
    isNaN(nuevaHabitacionData.value.precioBase) ||
    isNaN(nuevaHabitacionData.value.capacidad) ||
    !categoriaSeleccionada.value
  ) {
    alert('Por favor, completa todos los campos correctamente.');
    return;
  }

  const habitacion = {
    numero: parseInt(nuevaHabitacionData.value.numero),
    precioBase: parseFloat(nuevaHabitacionData.value.precioBase),
    capacidad: parseInt(nuevaHabitacionData.value.capacidad),
    bloqueada: Boolean(nuevaHabitacionData.value.bloqueada),
    adaptada: Boolean(nuevaHabitacionData.value.adaptada),
    categoriaId: categoriaSeleccionada.value,
  };
  if (habitacionIdEdicion.value) {
    habitacion.id = habitacionIdEdicion.value;
  }
  try {
    if (habitacionIdEdicion.value) {
      // Actualizar habitación existente
      await habitacionesService.actualizarHabitacion(habitacionIdEdicion.value, habitacion);
    } else {
      // Crear nueva habitación
      await habitacionesService.crearHabitacion(habitacion);
    }
    await cargarHabitaciones(); // Actualizar la lista
    cerrarModal(); // Cerrar el modal
  } catch (error) {
    console.error('Error al guardar la habitación:', error);
  }
  };


  onMounted(() => {
    cargarHabitaciones();
    cargarCategorias();
  });

  
</script>

  <style>
 .gestion-habitaciones {
  padding: 20px;
  display: flex;
  flex-direction: column;
}

h2 {
  margin-bottom: 20px;
  font-size: 1.5rem;
  font-family: 'Montserrat', sans-serif; /* Asegúrate de que la fuente esté aplicada */
  font-weight: 800;
  display: inline-block;
  margin-right: 20px; /* Espacio entre el título y el botón */
}

.add-button {
  background-color: #707377;
  color: white;
  border: none;
  padding: 10px 20px;
  border-radius: 4px;
  font-size: 14px;
  cursor: pointer;
  font-family: 'Montserrat', sans-serif; 
}


.header-container {
  display: flex;
  align-items: center;
  margin-bottom: 20px;
  justify-content: space-between;
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

  .modal {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}

.modal-content {
  background: white;
  padding: 20px;
  border-radius: 8px;
  width: 400px;
  max-width: 90%;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
}

.modal-content h3 {
  margin-top: 0;
}

.modal-content label {
  display: block;
  margin: 10px 0;
}

.modal-actions {
  display: flex;
  justify-content: flex-end;
  gap: 10px;
}

.save-button {
  background-color: #007bff;
  color: white;
  border: none;
  padding: 10px 20px;
  border-radius: 4px;
  cursor: pointer;
}

.save-button:hover {
  background-color: #0056b3;
}

.cancel-button {
  background-color: #dc3545;
  color: white;
  border: none;
  padding: 10px 20px;
  border-radius: 4px;
  cursor: pointer;
}

.cancel-button:hover {
  background-color: #a71d2a;
}
select {
  width: 100%;
  padding: 10px;
  margin: 10px 0;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 14px;
}

</style>
  