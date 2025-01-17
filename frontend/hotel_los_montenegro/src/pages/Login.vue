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
      <button type="submit" class="btn btn-custom">Login</button>
      <p v-if="errorMessage" class="error">{{ errorMessage }}</p>
      <p v-if="!errorMessage" class="acierto">{{ aciertoMessage }}</p>
    </form>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { jwtDecode } from 'jwt-decode'; // Instala jwt-decode: npm install jwt-decode
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

    // Decodificar el token JWT
    const decodedToken: { TipoUsuario: int } = jwtDecode(token);
    console.log('Tipo de Usuario antes de conversión:', decodedToken.TipoUsuario);

    // Convertir a número si es necesario
    const tipoUsuario = Number(decodedToken.TipoUsuario);
    console.log('Tipo de Usuario después de conversión:', tipoUsuario);

    // Redirigir basado en el tipo de usuario
    switch (tipoUsuario) {
      case 1: // Usuario normal
        console.log('USUARIO NORMAL');
        window.location.href = '/';
        break;
      case 2: // Recepcionista
        window.location.href = '/recepcionista';
        break;
      case 1002: // Administrador
        console.log('ADMINISTRADOR');
        window.location.href = '/webmaster';
        break;
      case 3:
        window.location.href = '/webmaster';
        break;
      default:
        console.log('Tipo de Usuario no reconocido:', tipoUsuario);
        window.location.href = '/';
        break;
    }
  } catch (error) {
    errorMessage.value = 'Email o password incorrecto.';
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
.btn-custom {
  background-color: lightgray;
  color: black;
  border: 1px solid black;
  border-radius: 5px;
  padding: 10px 20px;
  transition: background-color 0.3s ease;
  text-align: center;
}

.btn-custom:hover {
  background-color: darkgray;
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