<template>
    <div class="gestion-temporadas">
      <div class="header-container">
        <h2>Listado de Temporadas</h2>
        <button class="add-button" @click="nuevaTemporada">Añadir nueva</button>
      </div>
      <table class="table">
        <thead>
          <tr>
            <th>Nombre</th>
            <th>Fecha Inicio</th>
            <th>Fecha Fin</th>
            <th>Tarifa</th>
            <th>Acciones</th>

          </tr>
        </thead>
        <tbody>
          <tr v-for="temporada in temporadas" :key="temporada.id">
            <td>{{ temporada.nombre }}</td>
            <td>{{ temporada.fechaInicio }}</td>
            <td>{{ temporada.fechaFin }}</td>
            <td>{{ temporada.tarifa }}</td>

            <td>
              <button class="edit-button" @click="editarTemporada(temporada)">✏️</button>
              <button class="delete-button" @click="eliminarTemporadaHandler(temporada.id)">🗑️</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </template>
  
  <script setup>
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import authService from '../services/authService';
import temporadaService from '../services/temporadaService';

const router = useRouter();
const isAuthorized = ref(false); // Controla si el usuario tiene permisos
const temporadas = ref([]);

// Verificar si el usuario tiene el rol adecuado
const verificarPermiso = async () => {
  try {
    const rolUsuario = await authService.obtenerRolDelUsuario();
    if (rolUsuario === 1002) {
      isAuthorized.value = true; // Permitir acceso
      cargarTemporadas(); // Cargar temporadas solo si es admin
    } else {
      isAuthorized.value = false; // Denegar acceso
      router.push('/'); // Redirigir al inicio
    }
  } catch (error) {
    console.error('Error al verificar permisos:', error);
    router.push('/'); // Redirigir al inicio en caso de error
  }
};

// Cargar temporadas
const cargarTemporadas = async () => {
  try {
    const response = await temporadaService.obtenerTemporadas();
    const temporadasData = response.$values || [];
    temporadas.value = temporadasData.map(temporada => ({
      ...temporada,
      fechaInicio: new Date(temporada.fechaIni).toISOString().split("T")[0],
      fechaFin: new Date(temporada.fechaFin).toISOString().split("T")[0],
      tarifa: temporada.tarifa,
    }));
  } catch (error) {
    console.error('Error al cargar temporadas:', error);
  }
};

// Funciones de CRUD (sin cambios)
const eliminarTemporadaHandler = async (id) => {
  if (confirm('¿Estás seguro de que quieres eliminar esta temporada?')) {
    try {
      await temporadaService.eliminarTemporada(id);
      await cargarTemporadas();
    } catch (error) {
      console.error('Error al eliminar temporada:', error);
    }
  }
};

const nuevaTemporada = () => {
  const nombre = prompt('Ingrese el nombre de la nueva temporada');
  const fechaInicio = prompt('Ingrese la fecha de inicio (YYYY-MM-DD)');
  const fechaFin = prompt('Ingrese la fecha de fin (YYYY-MM-DD)');
  const tarifa = parseFloat(prompt('Ingrese la tarifa (por ejemplo, 1.5)'));

  if (nombre && fechaInicio && fechaFin && !isNaN(tarifa)) {
    const nueva = { nombre, fechaInicio, fechaFin, tarifa };

    temporadaService.crearTemporada(nueva)
      .then(() => cargarTemporadas())
      .catch((error) => console.error('Error al crear temporada:', error));
  } else {
    alert('Por favor, ingrese todos los datos de la temporada correctamente');
  }
};

const editarTemporada = (temporada) => {
  const nombre = prompt('Editar nombre de la temporada', temporada.nombre);
  const fechaInicio = prompt('Editar fecha de inicio (YYYY-MM-DD)', temporada.fechaInicio);
  const fechaFin = prompt('Editar fecha de fin ', temporada.fechaFin);
  const tarifa = parseFloat(prompt('Editar tarifa (por ejemplo, 1.5)', temporada.tarifa));

  if (nombre && fechaInicio && fechaFin && tarifa) {
    const temporadaActualizada = { ...temporada, nombre, fechaInicio, fechaFin, tarifa };
    temporadaService.actualizarTemporada(temporada.id, temporadaActualizada)
      .then(() => cargarTemporadas())
      .catch((error) => console.error('Error al actualizar temporada:', error));
  } else {
    alert('Por favor, ingrese todos los datos de la temporada');
  }
};

// Comprobar permisos al montar el componente
onMounted(verificarPermiso);
</script>
  
  <style scoped>
  .gestion-temporadas {
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
  </style>
  