<template>
  <div class="recepcion-container">
    <!-- Listado de Reservas -->
    <div class="reservas-section">
      <h2>Listado de Reservas</h2>
      <table>
        <thead>
          <tr>
            <th>Numero de Reserva</th>
            <th>Nombre</th>
            <th>Acción</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="reserva in reservas" :key="reserva.id" class="reserva-item">
            <td>{{ reserva.id }}</td>
            <td>{{ reserva.usuario.nombre}}</td>
            <td><button @click="confirmarCancelar(reserva.id)">Cancelar</button></td>
          </tr>
        </tbody>
      </table>
    </div>

    <!-- Listado de Habitaciones -->
    <div class="habitaciones-section">
      <h2>Listado de Habitaciones</h2>
      <table>
        <thead>
          <tr>
            <th>Número de Habitación</th>
            <th>Estado</th>
            <th>Acción</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="habitacion in habitaciones" :key="habitacion.id" class="habitacion-item">
            <td>{{ habitacion.numero }}</td>
            <td>{{ habitacion.bloqueada ? 'Bloqueada' : 'Disponible' }}</td>
            <td>
              <button v-if="!habitacion.bloqueada" @click="bloquearHabitacion(habitacion.id)">Bloquear</button>
              <button v-else @click="desbloquearHabitacion(habitacion.id)">Desbloquear</button>
            </td>
          </tr>
        </tbody>
      </table>
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
import reservaService from '../services/reservasService';
import habitacionService from '../services/habitacionesService';
import { onMounted } from 'vue';
import authService from '../services/authService';
import { useRouter } from 'vue-router';

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
  try {
    // Cargar datos de reservas y habitaciones
    const response1 = await reservaService.obtenerReservas();
    reservas.value = response1.$values;

    const response2 = await habitacionService.obtenerHabitaciones();
    habitaciones.value = response2.$values;

    // Verifica si los datos están llegando
    console.log('Reservas:', reservas.value);
    console.log('Habitaciones:', habitaciones.value);
  } catch (error) {
    console.error('Error al cargar los datos:', error);
  }
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

// Variables reactivas
const isAuthorized = ref(false);
const router = useRouter(); // Acceder al router

const verificarPermiso = async () => {
  console.log('Verificando permisos...');
  try {
    const rolUsuario = await authService.obtenerRolDelUsuario();
    console.log('Rol del usuario:', rolUsuario);
    if (rolUsuario === "3") {       // CAMBIAR AL ROL DE RECEPCIONISTA SI CAMBIA EN TU DB
      isAuthorized.value = true; // Permitir acceso
      console.log('Tienes permiso para acceder a esta página.');
    } else {
      isAuthorized.value = false; // Denegar acceso
      console.log('No tienes permiso para acceder a esta página.');
      router.push('/'); // Redirigir al inicio
    }
  } catch (error) {
    console.error('Error al verificar permisos:', error);
    router.push('/'); // Redirigir al inicio en caso de error
  }
};

// Inicialización
onMounted(() => {
  cargarDatos();

  // Usar la función verificarPermiso cuando el componente se monta
  verificarPermiso();
});

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
table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 1rem;
}

th, td {
  padding: 8px;
  text-align: left;
  border: 1px solid #ddd;
}

th {
  background-color: #f4f4f4;
}

/* Listado de habitaciones */
button {
  padding: 0.4em 0.8em;
  margin: 0.2em;
  background-color: #007bff;
  color: white;
  border: none;
  cursor: pointer;
}

button:hover {
  background-color: #0056b3;
}

.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
}

.modal {
  background: white;
  padding: 1rem;
  border-radius: 5px;
  text-align: center;
}

form {
  display: grid;
  gap: 1rem;
  margin-top: 1rem;
}

input {
  padding: 0.5rem;
  border: 1px solid #ddd;
  border-radius: 5px;
}
</style>
