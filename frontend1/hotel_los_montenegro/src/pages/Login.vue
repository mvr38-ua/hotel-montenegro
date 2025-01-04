<template>
  <div>
    <h1>Login</h1>
    <form @submit.prevent="login">
      <div>
        <label for="email">Email:</label>
        <input type="email" id="email" v-model="email" required />
      </div>
      <div>
        <label for="password">Password:</label>
        <input type="password" id="password" v-model="password" required />
      </div>
      <button type="submit">Login</button>
      <p v-if="errorMessage" class="error">{{ errorMessage }}</p>
    </form>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import api from '../services/api'; // Importamos la instancia de Axios

const email = ref('');
const password = ref('');
const errorMessage = ref('');

const login = async () => {
  try {
    const response = await api.post('/Usuario/login', {
      email: email.value,
      password: password.value,
    });

    const token = response.data.token;
    localStorage.setItem('token', token);  // Guardamos el token en localStorage

    // Redirigir a la página de inicio o alguna otra página
    window.location.href = '/';  // O puedes usar el router de Vue para redirigir
  } catch (error) {
    errorMessage.value = 'Invalid email or password.';
  }
};
</script>

<style scoped>
.error {
  color: red;
}
</style>
