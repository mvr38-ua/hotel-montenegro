<template>
    <div class="gestion-servicios">
      <div class="header-container">
        <h2>Listado de servicios</h2>
        <button class="add-button" @click="abrirModal(null)">Añadir nuevo</button>
      </div>
      <table class="table">
        <thead>
          <tr>
            <th>Nombre</th>
            <th>Descripción</th>
            <th>Precio</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="servicio in servicios" :key="servicio.id">
            <td>{{ servicio.nombre }}</td>
            <td>{{ servicio.descripcion || 'No disponible' }}</td>
            <td>{{ servicio.precioServicio + " €"  }}</td>
            <td>
              <button class="edit-button" @click="abrirModal(servicio)">✏️</button>
              <button class="delete-button" @click="eliminarServicioHandler(servicio.id)">🗑️</button>
            </td>
          </tr>
        </tbody>
      </table>
  
      <div v-if="mostrarModal" class="modal">
        <div class="modal-content">
          <h3>{{ servicioIdEdicion ? 'Editar servicio' : 'Crear nuevo servicio' }}</h3>
          <form @submit.prevent="guardarServicio">
            <label>
              Nombre:
              <input v-model="nuevoServicioData.nombre" type="text" required />
            </label>
            <label>
              Descripción:
              <textarea v-model="nuevoServicioData.descripcion"></textarea>
            </label>
            <label>
              Precio:
              <input v-model="nuevoServicioData.precioServicio" type="number" step="0.01" required />
            </label>
            <div class="modal-actions">
              <button type="submit" class="save-button">Guardar</button>
              <button type="button" class="cancel-button" @click="cerrarModal">Cancelar</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </template>
  
  <script setup>
  import { ref, onMounted } from 'vue';
  import serviciosService from '../services/serviciosService';
  
  const servicios = ref([]);
  const nuevoServicioData = ref({
    nombre: '',
    descripcion: '',
    precioServicio: '',
  });
  const mostrarModal = ref(false);
  
  const servicioIdEdicion = ref(null);
  
  const cargarServicios = async () => {
    try {
      const response = await serviciosService.obtenerServicios();
      servicios.value = response.$values;
    } catch (error) {
      console.error('Error al cargar servicios:', error);
    }
  };
  
  const eliminarServicioHandler = async (id) => {
    if (confirm('¿Estás seguro de que quieres eliminar este servicio?')) {
      try {
        await serviciosService.eliminarServicio(id);
        await cargarServicios(); // Actualizar la lista después de eliminar
      } catch (error) {
        console.error('Error al eliminar servicio:', error);
      }
    }
  };
  
  const abrirModal = (servicio) => {
    if (servicio) {
      // Estamos editando, cargar los datos del servicio
      servicioIdEdicion.value = servicio.id;
      nuevoServicioData.value = {
        nombre: servicio.nombre,
        descripcion: servicio.descripcion || '',
        precioServicio: servicio.precioServicio,
      };
    } else {
      // Nuevo servicio
      servicioIdEdicion.value = null;
      nuevoServicioData.value = {
        nombre: '',
        descripcion: '',
        precioServicio: '',
      };
    }
    mostrarModal.value = true;
  };
  
  const cerrarModal = () => {
    mostrarModal.value = false;
  };
  
  const guardarServicio = async () => {
    if (!nuevoServicioData.value.nombre || isNaN(nuevoServicioData.value.precioServicio)) {
      alert('Por favor, completa todos los campos correctamente.');
      return;
    }
  
    const servicio = {
      nombre: nuevoServicioData.value.nombre,
      descripcion: nuevoServicioData.value.descripcion,
      precioServicio: parseFloat(nuevoServicioData.value.precioServicio),
    };
  
    if (servicioIdEdicion.value) {
      servicio.id = servicioIdEdicion.value;
    }
  
    try {
      if (servicioIdEdicion.value) {
        // Actualizar servicio existente
        await serviciosService.actualizarServicio(servicioIdEdicion.value, servicio);
      } else {
        // Crear nuevo servicio
        await serviciosService.crearServicio(servicio);
      }
      await cargarServicios(); // Actualizar la lista
      cerrarModal(); // Cerrar el modal
    } catch (error) {
      console.error('Error al guardar el servicio:', error);
    }
  };
  
  onMounted(() => {
    cargarServicios();
  });
  </script>
  
  <style>
  .gestion-servicios {
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
  