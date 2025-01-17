import { createApp } from 'vue';
import { createRouter, createWebHistory } from "vue-router";
import App from './App.vue';
import Home from './pages/Principal.vue';
import Login from './pages/Login.vue';
import Register from './pages/Register.vue';
import About from './pages/About.vue';
import Detail from './pages/Detalles.vue';
import Habitacionesdisp from './pages/Habitaciones.vue';
import listado from './pages/Lista_habs.vue';
import Servicios from './pages/Servicios.vue';
import Webmaster from './pages/Webmaster.vue';
import Profile from './pages/perfil.vue';
import GestionHabitaciones from './components/GestionHabitaciones.vue';
import GestionTemporada from './components/GestionarTemporada.vue';
import ReservaRecepcion from './pages/ReservaRecepcion.vue';
import Resumen from './pages/Resumen.vue';

import Pago from './pages/Pago.vue';


import GestionUsuarios from './components/GestionUsuarios.vue';
import Informes from './components/Informes.vue';
import WelcomeWebmaster from './components/WelcomeWebmaster.vue';
import GestionServicio from './components/GestionarServicios.vue';
import GestionCategoria from './components/GestionarCategorias.vue';
import PagoExitoso from './pages/pago_exitoso.vue';


const routes = [
  { path: '/', component: Home },
  { path: '/login', component: Login },
  { path: '/register', component: Register },
  { path: '/pago', name: 'pago', component: Pago,},
  { path: '/about', component: About},
  { path: '/servicios', component: Servicios },
  { path: '/detalles', component: Detail},
  { path: '/habitaciones', component: Habitacionesdisp},
  { path: '/listado', component: listado},
  { path: '/profile', component: Profile },
  { path: '/pago_exitoso', component: PagoExitoso },

  {
    path: '/webmaster',
    component: Webmaster,
    children: [
      { path: '', component: WelcomeWebmaster },
      { path: 'habitaciones', component: GestionHabitaciones },
      { path: 'usuarios', component: GestionUsuarios },
      { path: 'informes', component: Informes },
      { path: 'temporada', component: GestionTemporada },
      { path: 'servicio', component: GestionServicio },
      { path: 'categoria', component: GestionCategoria },

    ],
  },
  { path: '/recepcionista', component: ReservaRecepcion },
  { path: '/resumen', component: Resumen },

];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

createApp(App).use(router).mount('#app');
