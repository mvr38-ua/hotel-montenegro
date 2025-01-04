<template>
    <div>
      <h2>Register</h2>
      <form @submit.prevent="register">
        <div>
          <label for="name">Name:</label>
          <input type="text" id="name" v-model="name" required />
        </div>
        <div>
          <label for="email">Email:</label>
          <input type="email" id="email" v-model="email" required />
        </div>
        <div>
          <label for="password">Password:</label>
          <input type="password" id="password" v-model="password" required />
        </div>
        <button type="submit">Register</button>
      </form>
    </div>
  </template>
  
  <script>
  import authService from '../services/authService';
  
  export default {
    name: 'Register',
    data() {
      return {
        name: '',
        email: '',
        password: ''
      };
    },
    methods: {
      async register() {
        try {
          const response = await authService.register(this.name, this.email, this.password);
          console.log('Registration successful:', response);
          // Redirigir a la página de login o home
        } catch (error) {
          console.error('Registration failed:', error);
        }
      }
    }
  };
  </script>
  
  <style scoped>
  form {
    max-width: 400px;
    margin: auto;
  }
  
  div {
    margin-bottom: 10px;
  }
  
  label {
    display: block;
    margin-bottom: 5px;
  }
  
  input {
    width: 100%;
    padding: 8px;
    margin-top: 5px;
  }
  
  button {
    padding: 10px 15px;
    background-color: #3498db;
    border: none;
    color: white;
    cursor: pointer;
  }
  
  button:hover {
    background-color: #2980b9;
  }
  </style>
  