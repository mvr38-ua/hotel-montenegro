<template>
  <div class="login-container">
    <h1>Login</h1>
    <form class="login-form" @submit.prevent="login">
      <div class="form-group">
        <label for="email">Email:</label>
        <input type="email" id="email" v-model="email" required />
      </div>
      <div class="form-group">
        <label for="password">Password:</label>
        <input type="password" id="password" v-model="password" required />
      </div>
      <button type="submit" class="login-button">Login</button>
      <p v-if="errorMessage" class="error">{{ errorMessage }}</p>
      <p v-if="!errorMessage" class="acierto">{{ aciertoMessage }}</p>

      
    </form>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import api from '../services/api';

const email = ref('');
const password = ref('');
const errorMessage = ref('');
const aciertoMessage = ref('');


const login = async () => {
  try {
    const response = await api.post('/Usuario/login', {
      email: email.value,
      password: password.value,
    });

    const token = response.data.token;
    localStorage.setItem('token', token); // Guardamos el token en localStorage
    aciertoMessage.value = 'Login correcto.';
    window.location.href = '/'; // Redirigir a la página de inicio
  } catch (error) {
    errorMessage.value = 'Email o password Incorrecto.';
  }
};
</script>

<style scoped>
/* Estructura principal */
.login-container {
  max-width: 400px;
  margin: 50px auto;
  padding: 20px;
  background: #f9f9f9;
  border-radius: 8px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  font-family: Arial, sans-serif;
}

/* Título */
h1 {
  text-align: center;
  color: #333;
  margin-bottom: 20px;
}

/* Formularios */
.login-form {
  display: flex;
  flex-direction: column;
  gap: 15px;
}

.form-group {
  display: flex;
  flex-direction: column;
}

label {
  font-size: 14px;
  color: #555;
  margin-bottom: 5px;
}

input {
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 5px;
  font-size: 14px;
  color: #333;
}

/* Botón */
.login-button {
  padding: 10px;
  background: #007BFF;
  color: #fff;
  font-size: 16px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.login-button:hover {
  background: #0056b3;
}

/* Mensajes de error */
.error {
  text-align: center;
  color: red;
  font-size: 14px;
  margin-top: 10px;
}
.acierto {
  text-align: center;
  color: rgb(89, 255, 0);
  font-size: 14px;
  margin-top: 10px;
}
</style>
