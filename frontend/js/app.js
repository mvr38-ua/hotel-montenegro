import { createApp } from './libs/vue.esm-browser.js';  // Ruta local a Vue
import { createRouter, createWebHashHistory } from './libs/vue-router.esm-browser.js';  // Ruta local a Vue Router

// Define los componentes de tu aplicación
const Principal = {
  template: `
    <div>
      <h1>Bienvenido al Hotel Montenegro</h1>
      <p>Esta es la página principal.</p>
    </div>
  `
};

const LoginForm = {
  template: `
    <div>
      <h1>Iniciar Sesión</h1>
      <form>
        <label for="username">Usuario:</label>
        <input type="text" id="username" name="username">
        <br>
        <label for="password">Contraseña:</label>
        <input type="password" id="password" name="password">
        <br>
        <button type="submit">Entrar</button>
      </form>
    </div>
  `
};

const HotelDetails = {
  template: `
    <div>
      <h1>Detalles del Hotel</h1>
      <p>Aquí encontrarás información sobre nuestras instalaciones y servicios.</p>
    </div>
  `
};

// Configura las rutas
const routes = [
  { path: '/', component: Principal },
  { path: '/login', component: LoginForm },
  { path: '/hotel-details', component: HotelDetails }
];

// Configura Vue Router
const router = createRouter({
  history: createWebHashHistory(),
  routes
});

// Crea y monta la aplicación Vue
const app = createApp({});
app.use(router);
app.mount('#app');
