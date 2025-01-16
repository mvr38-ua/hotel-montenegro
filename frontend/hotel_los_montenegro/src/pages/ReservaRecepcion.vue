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
      <form @submit.prevent="abrirModalServicios">
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
      <!-- Botón para abrir modal de selección de servicios -->
      <button @click="abrirModalServicios" class="btn-servicios">Servicios a elegir</button>
    </div>
  </div>

  <!-- Modal para Confirmación -->
  <div v-if="mostrarModal" class="modal-overlay">
    <div class="modal">
      <p>¿Estás seguro de que deseas cancelar esta reserva?</p>
      <button @click="cancelarReserva(reservaIdModal)">Confirmar</button>
      <button @click="cerrarModal" class="cerrar-modal">X</button>
    </div>
  </div>

  <!-- Modal de Servicios -->
  <div v-if="mostrarModalServicios" class="modal-overlay">
    <div class="modal">
      <button class="cerrar-modal" @click="cerrarModalServicios">X</button>
      <h3>Elija nuestros servicios disponibles...</h3>
      <div v-if="cargandoServicios">Cargando servicios...</div>
      <div v-else class="servicios-list">
        <div v-for="(servicio, index) in servicios" :key="index" class="servicio-item">
          <label>
            <input type="checkbox" v-model="servicio.seleccionado" />
            <span>{{ servicio.nombre }} - {{ servicio.precioServicio }}€</span>
          </label>
        </div>
      </div>
      <button @click="continuarConReserva" :disabled="cargandoServicios">Continuar con la reserva</button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import reservaService from '../services/reservasService';
import habitacionService from '../services/habitacionesService';
import serviciosService from '../services/serviciosService';
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
const mostrarModalServicios = ref(false);
const servicios = ref([]);
const cargandoServicios = ref(false);

// Cargar los datos de reservas, habitaciones y servicios
const cargarDatos = async () => {
  try {
    // Cargar reservas
    const response1 = await reservaService.obtenerReservas();
    reservas.value = response1.$values;

    // Cargar habitaciones
    const response2 = await habitacionService.obtenerHabitaciones();
    habitaciones.value = response2.$values;

    // Cargar servicios
    const serviciosCargados = await serviciosService.obtenerServicios();
    servicios.value = serviciosCargados.$values;

    console.log('Reservas:', reservas.value);
    console.log('Habitaciones:', habitaciones.value);
    console.log('Servicios:', servicios.value);
  } catch (error) {
    console.error('Error al cargar los datos:', error);
  }
};

// Mostrar modal de servicios
const abrirModalServicios = async () => {
  mostrarModalServicios.value = true;
};

// Cerrar modal de servicios
const cerrarModalServicios = () => {
  mostrarModalServicios.value = false;
};

// Continuar con la reserva (siguiente paso)
const continuarConReserva = () => {
  const serviciosSeleccionados = servicios.value.filter((s) => s.seleccionado);
  console.log('Servicios seleccionados:', serviciosSeleccionados);

  // Aquí puedes hacer algo más, como redirigir a un resumen de reserva
  cerrarModalServicios();
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
/* Estilos previos no modificados */

.recepcion-container {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 20px;
  padding: 20px;
}

.caja-gris {
  background-color: #f0f0f0;
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.listados-section,
.formulario-section {
  margin: 0 auto;
}

/* Botón de Servicios a elegir */
.btn-servicios {
  background-color: #28a745;
  color: white;
  padding: 0.5em 1em;
  border-radius: 5px;
  cursor: pointer;
  margin-top: 20px;
}

.btn-servicios:hover {
  background-color: #218838;
}

/* Modales */
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
  padding: 20px;
  border-radius: 8px;
  max-width: 600px;
  width: 80%;
  box-shadow: 0 0 15px rgba(0, 0, 0, 0.1);
  display: flex;
  flex-direction: column;
  justify-content: space-between; /* Esto permite distribuir el contenido */
  height: auto;              /* Asegura que el modal tenga el tamaño adecuado */
  position: relative;        /* Para posicionar correctamente el botón de cerrar */
}
.cerrar-modal {
  position: absolute;
  top: 5px;
  right: 5px;
  background-color: rgb(126, 17, 17);
  color: white;
  border: none;
  border-radius: 3px;
  padding: 8px;
  cursor: pointer;
  font-size: 14px;
}

.modal button:last-child {
  margin-top: auto; /* Empuja el botón de continuar hacia abajo */
}

.cerrar-modal:hover {
  background-color: rgb(184, 13, 13);
}

input {
  padding: 0.5rem;
  border: 1px solid #ddd;
  border-radius: 5px;
  width: 100%;
}

/* Lista de servicios */
.servicios-list {
  display: flex;
  flex-direction: column;
  gap: 10px;
}

.servicio-item label {
  display: flex;
  align-items: center;
  gap: 10px;
}

.servicio-item input {
  margin-right: 10px;
}
</style>
