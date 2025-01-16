import { createApp } from 'vue';
import { createRouter, createWebHistory } from "vue-router";
import App from './App.vue';
import Home from './pages/Home.vue';
import Login from './pages/Login.vue';
import Register from './pages/Register.vue';
import Webmaster from './pages/Webmaster.vue';
import GestionHabitaciones from './components/GestionHabitaciones.vue';
import GestionTarifas from './components/GestionTarifas.vue';
import GestionTemporada from './components/GestionarTemporada.vue';
import Pago from './pages/Pago.vue';

import GestionUsuarios from './components/GestionUsuarios.vue';
import Informes from './components/Informes.vue';

const routes = [
  { path: '/', component: Home },
  { path: '/login', component: Login },
  { path: '/register', component: Register },
  { path: '/pago', name: 'Pago', component: Pago,},
  {
    path: '/webmaster',
    component: Webmaster,
    children: [
      { path: 'habitaciones', component: GestionHabitaciones },
      { path: 'tarifas', component: GestionTarifas },
      { path: 'usuarios', component: GestionUsuarios },
      { path: 'informes', component: Informes },
      { path: 'temporada', component: GestionTemporada },
      

    ],
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

createApp(App).use(router).mount('#app');
