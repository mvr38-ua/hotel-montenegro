<template>
  <div class="register-container">
    <h1>Register</h1>
    <form class="register-form" @submit.prevent="register">
      <div class="form-group">
        <label for="name">Name:</label>
        <input type="text" id="name" v-model="name" required />
      </div>
      <div class="form-group">
        <label for="lastname">Last Name:</label>
        <input type="text" id="lastname" v-model="lastname" required />
      </div>
      <div class="form-group">
        <label for="dni">DNI:</label>
        <input type="text" id="dni" v-model="dni" required />
      </div>
      <div class="form-group">
        <label for="email">Email:</label>
        <input type="email" id="email" v-model="email" required />
      </div>
      <div class="form-group">
        <label for="password">Password:</label>
        <input type="password" id="password" v-model="password" required />
      </div>
      <div class="form-group">
        <label for="phone">Phone:</label>
        <input type="text" id="phone" v-model="phone" required />
      </div>
      <div class="form-group">
        <label for="mobile">Mobile:</label>
        <input type="text" id="mobile" v-model="mobile" required />
      </div>
      <div class="form-group">
        <label for="gender">Gender:</label>
        <select id="gender" v-model="gender" required>
          <option value="M">Male</option>
          <option value="F">Female</option>
        </select>
      </div>
      <div class="form-group">
        <label for="birthdate">Birth Date:</label>
        <input type="date" id="birthdate" v-model="birthdate" required />
      </div>
      <button type="submit" class="btn btn-custom">Register</button>
      <p v-if="errorMessage" class="error">{{ errorMessage }}</p>
      <p v-if="successMessage" class="success">{{ successMessage }}</p>
    </form>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import authService from '../services/authService';

const name = ref('');
const lastname = ref('');
const dni = ref('');
const email = ref('');
const password = ref('');
const phone = ref('');
const mobile = ref('');
const gender = ref('');
const birthdate = ref('');
const errorMessage = ref('');
const successMessage = ref('');

const register = async () => {
  try {
    await authService.register({
      name: name.value,
      lastname: lastname.value,
      dni: dni.value,
      email: email.value,
      password: password.value,
      phone: phone.value,
      mobile: mobile.value,
      gender: gender.value,
      birthdate: birthdate.value,
      addressId: 1,
      userTypeId: 1,
    });
    successMessage.value = 'Registration successful!';
    errorMessage.value = '';
    setTimeout(() => (window.location.href = '/login'), 2000); // Redirigir a login después de 2s
  } catch (error) {
    successMessage.value = '';
    errorMessage.value = 'Registration failed. Please try again.';
  }
};
</script>

<style scoped>
/* Estructura principal */
.register-container {
  max-width: 500px;
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
.register-form {
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

input,
select {
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

/* Mensajes */
.error {
  text-align: center;
  color: red;
  font-size: 14px;
  margin-top: 10px;
}

.success {
  text-align: center;
  color: rgb(89, 255, 0);
  font-size: 14px;
  margin-top: 10px;
}
</style>