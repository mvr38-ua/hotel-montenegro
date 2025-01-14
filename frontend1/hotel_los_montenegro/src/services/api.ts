import axios from 'axios';

// Aquí definimos la URL base de la API
const api = axios.create({
  baseURL: 'http://localhost:5288/api',  // Tu API está corriendo en localhost:5288
  headers: {
    'Content-Type': 'application/json',
  },
});

// Agregar un interceptor para incluir el token de autorización en las cabeceras
api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');  // Asumimos que el token está en localStorage
  if (token) {
    config.headers['Authorization'] = `Bearer ${token}`;  // Incluir el token JWT en las peticiones
  }
  return config;
});

export default api;
