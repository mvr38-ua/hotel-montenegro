
import { reactive } from 'vue';

export const reservaGlobal = reactive({
  formulario: null,
  habitacion: null,
  servicios: [],
});

export const limpiarReserva = () => {
  reservaGlobal.formulario = null;
  reservaGlobal.habitacion = null;
  reservaGlobal.servicios = [];
};