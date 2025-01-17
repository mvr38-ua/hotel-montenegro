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
              <td><button @click="confirmarCancelar(reserva.id)" class="btn-servicios">Cancelar</button></td>
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
                <button v-if="!habitacion.bloqueada" @click="bloquearHabitacion(habitacion.id)" class="btn-servicios">Bloquear</button>
                <button v-else @click="desbloquearHabitacion(habitacion.id)" class="btn-servicios">Desbloquear</button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>

    <!-- Sección Derecha: Formulario -->
    <div class="formulario-section caja-gris">
      <h2>Formulario de Reservas</h2>
      <form @submit.prevent="irAPaginaResumen">
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
        <div>
          <!-- Botón para abrir modal de selección de servicios -->
          <button type="button" @click="abrirModalServicios" class="btn-servicios">Servicios a elegir</button>
        </div>
        <div>
          <button type="button" @click="abrirModalHabitaciones" class="btn-servicios">Buscar Habitaciones</button>
        </div>
        <div>
          <button type="submit" class="btn-servicios">Reservar</button>
        </div>
      </form>
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

  <!-- Modal de habitaciones disponibles -->
  <div v-if="mostrarModalHabitaciones" class="modal-overlay">
    <div class="modal">
      <button class="cerrar-modal" @click="cerrarModalHabitaciones">X</button>
      <h3>Habitaciones Disponibles</h3>
      <ul class="lista-habitaciones">
        <li v-for="habitacion in habitacionesDisponibles" :key="habitacion.id" class="habitacion-item">
          <div>
            <strong>{{ habitacion.numero }}</strong>
            <p>Capacidad: {{ habitacion.capacidad }}</p>
            <p>Precio por día: {{ habitacion.precioBase }}€</p>
          </div>
        <button @click="seleccionarHabitacion(habitacion)" class="btn-seleccionar">Seleccionar</button>
        </li>
      </ul>
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
import { reservaGlobal } from '../store/reservaGlobal';


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
const mostrarModalHabitaciones = ref(false);
const habitacionesDisponibles = ref([]);
const habitacionSeleccionada = ref(null);



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

// Función para abrir el modal de habitaciones disponibles
const abrirModalHabitaciones = () => {
  try {
    // Llamada al servicio para obtener las habitaciones disponibles
    buscarHabitaciones(); // Esta función ahora debería obtener las habitaciones disponibles

    // Abre el modal 
    mostrarModalHabitaciones.value = true;

  } catch (error) {
    console.error('Error al abrir el modal de habitaciones:', error);
  }
};

// Cerrar modal de servicios
const cerrarModalHabitaciones = () => {
  mostrarModalHabitaciones.value = false;
};
// Función para buscar habitaciones disponibles
const buscarHabitaciones = async () => {
  try {
    // Formateamos las fechas al formato ISO
    const fechaEntradaFormateada = new Date(formulario.value.fechaEntrada).toISOString();
    const fechaSalidaFormateada = new Date(formulario.value.fechaSalida).toISOString();

    // Llamamos al servicio con las fechas formateadas y parámetros adicionales
    const habitacionesDisponiblesData = await reservaService.obtenerHabitacionesDisponibles(
      fechaEntradaFormateada,
      fechaSalidaFormateada,
      formulario.value.capacidadMinima || null, // Si no se especifica, pasamos null
      formulario.value.tipoHabitacion || null // Si no se especifica, pasamos null
    );

    // Guardamos las habitaciones obtenidas
    habitacionesDisponibles.value = habitacionesDisponiblesData.$values;
    console.log(habitacionesDisponibles.value)
  } catch (error) {
    alert('Debe introducir los datos necesarios para poder buscar la habitacion');
  }
};

// Función para seleccionar una habitación
const seleccionarHabitacion = (habitacion: any) => {
  habitacionSeleccionada.value = habitacion;
  alert(`Has seleccionado la habitación: ${habitacion.numero}`);
  console.log(habitacion)
  mostrarModalHabitaciones.value = false; // Cierra el modal después de seleccionar
};

// Continuar con la reserva
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

const irAPaginaResumen = () => {
  // Validar campos obligatorios del formulario
  const { nombre, apellidos, correo, movil, fechaEntrada, fechaSalida } = formulario.value;

  if (!nombre || !apellidos || !correo || !movil || !fechaEntrada || !fechaSalida) {
    alert('Por favor, completa todos los campos obligatorios del formulario.');
    return;
  }

  // Validar que se haya seleccionado una habitación
  if (!habitacionSeleccionada.value) {
    alert('Por favor, selecciona una habitación.');
    return;
  }

  reservaGlobal.formulario = formulario.value;
  reservaGlobal.habitacion = habitacionSeleccionada.value;
  reservaGlobal.servicios = servicios.value.filter((s) => s.seleccionado);

  console.log(reservaGlobal);

  router.push('/resumen');

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

.lista-habitaciones {
  list-style: none;
  padding: 0;
}

.habitacion-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px;
  margin: 10px 0;
  border: 1px solid #ddd;
  border-radius: 5px;
  background-color: #f9f9f9;
}

.habitacion-item div {
  max-width: 70%;
}

.btn-seleccionar {
  padding: 5px 10px;
  background-color: #007bff;
  color: #fff;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}

.btn-seleccionar:hover {
  background-color: #0056b3;
}

</style>