import { createApp } from "vue";
import { createRouter, createWebHistory } from "vue-router";
import App from './App.vue';
import Home from './pages/Home.vue';
import Login from './pages/Login.vue';
import Register from './pages/Register.vue';

const routes = [
  { path: '/', component: Home },
  { path: '/login', component: Login },
  { path: '/register', component: Register },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

createApp(App).use(router).mount('#app');
