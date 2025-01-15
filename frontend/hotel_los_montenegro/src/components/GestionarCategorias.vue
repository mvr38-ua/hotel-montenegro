<template>
    <div class="gestion-categorias">
      <div class="header-container">
        <h2>Listado de Categorías</h2>
        <button class="add-button" @click="abrirModal(null)">Añadir nueva</button>
      </div>
      <table class="table">
        <thead>
          <tr>
            <th>Nombre</th>
            <th style="text-align: center;">Acciones</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="categoria in categorias" :key="categoria.id">
            <td>{{ categoria.nombre }}</td>
            <td>
                <div class="acciones"> 
                    <button class="edit-button" @click="abrirModal(categoria)">✏️</button>
                    <button class="delete-button" @click="eliminarCategoriaHandler(categoria.id)">🗑️</button>
                </div>
            </td>
          </tr>
        </tbody>
      </table>
  
      <!-- Modal para agregar o editar categoría -->
      <div v-if="mostrarModal" class="modal">
        <div class="modal-content">
          <h3>{{ categoriaIdEdicion ? 'Editar categoría' : 'Crear nueva categoría' }}</h3>
          <form @submit.prevent="guardarCategoria">
            <label>
              Nombre:
              <input v-model="nuevaCategoriaData.nombre" type="text" required />
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
  import categoriasService from '../services/categoriasService'; // Asegúrate de tener este servicio.
  
  const categorias = ref([]);
  const nuevaCategoriaData = ref({ nombre: '' });
  const mostrarModal = ref(false);
  const categoriaIdEdicion = ref(null);
  
  const cargarCategorias = async () => {
    try {
      const response = await categoriasService.obtenerCategorias();
      categorias.value = response.$values; // Ajusta esto según la respuesta de tu API
    } catch (error) {
      console.error('Error al cargar categorías:', error);
    }
  };
  
  const eliminarCategoriaHandler = async (id) => {
    if (confirm('¿Estás seguro de que quieres eliminar esta categoría?')) {
      try {
        await categoriasService.eliminarCategoria(id);
        await cargarCategorias(); // Actualizar la lista después de eliminar
      } catch (error) {
        console.error('Error al eliminar categoría:', error);
      }
    }
  };
  
  const abrirModal = (categoria) => {
    if (categoria) {
      // Estamos editando, cargar los datos de la categoría
      categoriaIdEdicion.value = categoria.id;
      nuevaCategoriaData.value = {
        nombre: categoria.nombre,
      };
    } else {
      // Nueva categoría
      categoriaIdEdicion.value = null;
      nuevaCategoriaData.value = { nombre: '' };
    }
    mostrarModal.value = true;
  };
  
  const cerrarModal = () => {
    mostrarModal.value = false;
  };
  
  const guardarCategoria = async () => {
    if (!nuevaCategoriaData.value.nombre) {
      alert('Por favor, ingresa un nombre para la categoría.');
      return;
    }
  
    const categoria = {
      nombre: nuevaCategoriaData.value.nombre,
    };
  
    if (categoriaIdEdicion.value) {
      categoria.id = categoriaIdEdicion.value;
    }
  
    try {
      if (categoriaIdEdicion.value) {
        // Actualizar categoría existente
        await categoriasService.actualizarCategoria(categoriaIdEdicion.value, categoria);
      } else {
        // Crear nueva categoría
        await categoriasService.crearCategoria(categoria);
      }
      await cargarCategorias(); // Actualizar la lista
      cerrarModal(); // Cerrar el modal
    } catch (error) {
      console.error('Error al guardar categoría:', error);
    }
  };
  
  onMounted(() => {
    cargarCategorias();
  });
  </script>
  
  <style>
  .gestion-categorias {
    padding: 20px;
    display: flex;
    flex-direction: column;
  }
  
  h2 {
    margin-bottom: 20px;
    font-size: 1.5rem;
    font-family: 'Montserrat', sans-serif;
    font-weight: 800;
    display: inline-block;
    margin-right: 20px;
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
  .acciones {
  display: flex;
  justify-content: center; /* Centra horizontalmente el botón */
  align-items: center; /* Alinea verticalmente si es necesario */
  width: 100%; /* Asegura que el contenido ocupe toda la celda */
}
  </style>
  