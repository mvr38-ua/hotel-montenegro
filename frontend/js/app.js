import { createApp } from 'https://unpkg.com/vue@3.2.47/dist/vue.esm-browser.js';
import { createRouter, createWebHashHistory } from 'https://unpkg.com/vue-router@4.2.2/dist/vue-router.esm-browser.js';

import Principal from './components/Principal.js';
import LoginForm from './components/LoginForm.js';
import HotelDetails from './components/HotelDetails.js';

// Configurar rutas
const routes = [
    { path: '/', component: Principal },
    { path: '/login', component: LoginForm },
    { path: '/hotel-details', component: HotelDetails },
];

// Crear el router
const router = createRouter({
    history: createWebHashHistory(),
    routes,
});

// Crear y montar la aplicación Vue
const app = createApp({});
app.use(router);
app.mount('#app');
