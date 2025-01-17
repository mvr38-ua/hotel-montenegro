<script>
import axios from 'axios';
import authService from '../services/authService';

export default {
  data() {
    return {
      user: null,
      newPassword: '', // Almacenar la nueva contraseña
    };
  },
  created() {
    this.fetchUser();
  },
  methods: {
    async fetchUser() {
      try {
        const token = localStorage.getItem('token');
        if (!token) {
          console.error('No token found');
          return;
        }
        const userId = authService.obtenerUsuarioDelToken();
        const response = await axios.get(`http://localhost:5288/api/Usuario/${userId}`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });
        if (response.data) {
          this.user = response.data;
        }
      } catch (error) {
        console.error('Error fetching user data:', error);
      }
    },

    async changePassword() {
      if (!this.newPassword) {
        alert('Por favor ingresa una nueva contraseña');
        return;
      }

      try {
        const token = localStorage.getItem('token');
        const userId = authService.obtenerUsuarioDelToken();

        const response = await axios.put(
          `http://localhost:5288/api/ContraseñasUsuarios/${userId}`, // Actualizar la contraseña
          { Contraseña: this.newPassword }, // Enviar la nueva contraseña
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );

        if (response.status === 204) {
          alert('Contraseña cambiada con éxito');
          this.newPassword = ''; // Limpiar el campo de la nueva contraseña
        }
      } catch (error) {
        console.error('Error cambiando la contraseña:', error);
        alert('Hubo un error al cambiar la contraseña');
      }
    },

    formatDate(dateString) {
      const options = { year: 'numeric', month: 'long', day: 'numeric' };
      return new Date(dateString).toLocaleDateString(undefined, options);
    },
  },
};
</script>
