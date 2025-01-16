<template>
  <div class="recepcion-container">
    <!-- Sección Izquierda: Listados -->
    <div class="listados-section">
      <!-- Listado de Reservas -->
      <div class="reservas-section caja-gris">
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
              <td>{{ reserva.usuario.nombre }}</td>
              <td><button @click="confirmarCancelar(reserva.id)">Cancelar</button></td>
            </tr>
          </tbody>
        </table>
      </div>

      <!-- Listado de Habitaciones -->
      <div class="habitaciones-section caja-gris">
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
    </div>

    <!-- Sección Derecha: Formulario -->
    <div class="formulario-section caja-gris">
      <h2>Formulario de Reservas</h2>
      <form @submit.prevent="crearReserva">
        <div class="form-group">
          <label for="nombre">Nombre</label>
          <input id="nombre" v-model="formulario.nombre" type="text" required />
        </div>
        <div class="form-group">
          <label for="apellidos">Apellidos</label>
          <input id="apellidos" v-model="formulario.apellidos" type="text" required />
        </div>
        <div class="form-group">
          <label for="correo">Correo</label>
          <input id="correo" v-model="formulario.correo" type="email" required />
        </div>
        <div class="form-group">
          <label for="movil">Móvil</label>
          <input id="movil" v-model="formulario.movil" type="tel" required />
        </div>
        <div class="form-group">
          <label for="fechaEntrada">Fecha de Entrada</label>
          <input id="fechaEntrada" v-model="formulario.fechaEntrada" type="date" required />
        </div>
        <div class="form-group">
          <label for="fechaSalida">Fecha de Salida</label>
          <input id="fechaSalida" v-model="formulario.fechaSalida" type="date" required />
        </div>
        <div class="form-group">
          <label for="capacidadMinima">Capacidad Mínima</label>
          <input id="capacidadMinima" v-model="formulario.capacidadMinima" type="number" />
        </div>
        <div class="form-group">
          <label for="tipoHabitacion">Tipo de Habitación</label>
          <input id="tipoHabitacion" v-model="formulario.tipoHabitacion" type="text" />
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
    const response1 = await reservaService.obtenerReservas();
    reservas.value = response1.$values;

    const response2 = await habitacionService.obtenerHabitaciones();
    habitaciones.value = response2.$values;

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
  try {
    await reservaService.crearReserva(formulario.value);
    alert('Reserva creada exitosamente');
    cargarDatos();
    formulario.value = {
      nombre: '',
      apellidos: '',
      correo: '',
      movil: '',
      fechaEntrada: '',
      fechaSalida: '',
      capacidadMinima: '',
      tipoHabitacion: '',
    };
  } catch (error) {
    console.error('Error al crear la reserva:', error);
  }
};

const isAuthorized = ref(false);
const router = useRouter();

const verificarPermiso = async () => {
  try {
    const rolUsuario = await authService.obtenerRolDelUsuario();
    if (rolUsuario === "3") {
      isAuthorized.value = true;
    } else {
      router.push('/');
    }
  } catch (error) {
    console.error('Error al verificar permisos:', error);
    router.push('/');
  }
};

// Inicialización
onMounted(() => {
  cargarDatos();
  verificarPermiso();
});
</script>

<style scoped>
/* Contenedor principal con dos columnas iguales */
.recepcion-container {
  display: grid;
  grid-template-columns: 1fr 1fr; /* Dos columnas iguales */
  gap: 20px;
  padding: 20px;
}

/* Estilo para las cajas grises */
.caja-gris {
  background-color: #f0f0f0; /* Fondo gris claro */
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1); /* Sombra ligera */
}

/* Márgenes entre las secciones */
.listados-section,
.formulario-section {
  margin: 0 auto;
}

/* Tabla */
table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 1rem;
}

th,
td {
  padding: 10px;
  text-align: left;
  border: 1px solid #ddd;
}

th {
  background-color: #e0e0e0;
}

/* Botones */
button {
  padding: 0.5em 1em;
  margin: 0.2em;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}

button:hover {
  background-color: #0056b3;
}

/* Modal */
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

/* Formulario */
form {
  display: grid;
  gap: 1rem;
}

input {
  padding: 0.5rem;
  border: 1px solid #ddd;
  border-radius: 5px;
  width: 100%;
}
</style>