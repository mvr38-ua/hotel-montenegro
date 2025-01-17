import axios from 'axios';


const API_URL = 'http://localhost:5288/api/Usuario'; 

export default {
  async login(email: string, password: string) {
    const response = await fetch(`${API_URL}/login`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ email, password })
    });

    if (!response.ok) {
      throw new Error('Login failed');
    }

    const data = await response.json();
    return data; // Devuelve el token u otros datos
  },

  async register(userData: {
    name: string;
    lastname: string;
    dni: string;
    email: string;
    password: string;
    phone: string;
    mobile: string;
    gender: string;
    birthdate: string;
    addressId: number;
    userTypeId: number;
  }) {
    const response = await fetch(`${API_URL}/register`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        Nombre: userData.name,
        Apellidos: userData.lastname,
        Dni: userData.dni,
        Email: userData.email,
        Password: userData.password,
        Telefono: userData.phone,
        Movil: userData.mobile,
        Genero: userData.gender,
        FechaNac: userData.birthdate,
        DireccionId: userData.addressId,
        TipoUsuarioId: userData.userTypeId,
      }),
    });
  
    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(`Registration failed: ${errorText}`);
    }
  
    return await response.json();
  },

  obtenerRolDelUsuario() {
    const token = localStorage.getItem('token'); // Recuperar el token del almacenamiento
    if (!token) {
      throw new Error('No se encontró un token, el usuario no está autenticado');
    }

    // Decodificar el payload del token (asumiendo que es un JWT)
    const payload = JSON.parse(atob(token.split('.')[1]));
    console.log('Payload del token:', payload);
    return payload.TipoUsuario; // Suponiendo que el rol está en la propiedad "role"
  },

  // Opcional: Método para cerrar sesión
  logout() {
    localStorage.removeItem('token'); // Eliminar el token
  },

  obtenerUsuarioDelToken() {
    const token = localStorage.getItem('token'); // Recuperar el token del almacenamiento
    if (!token) {
      throw new Error('No se encontró un token, el usuario no está autenticado');
    }
  
    try {
      // Decodificar el payload del token (asumiendo que es un JWT)
      const payload = JSON.parse(atob(token.split('.')[1]));
      // Extraer y devolver el ID del usuario
      return payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
    } catch (error) {
      console.error('Error al decodificar el token:', error);
      throw new Error('Token no válido');
    }
  },

  async changePassword(newPassword, passwordId) {  // Añadí 'passwordId' como parámetro
    if (!newPassword) {
      alert('Por favor ingresa una nueva contraseña');
      return;
    }
  
    try {
      const token = localStorage.getItem('token');
      
      // Ya no necesitas obtener el 'userId' desde el token, sino el 'passwordId' que se pasa como argumento
      if (!passwordId) {
        alert('No se encontró el Id de la contraseña');
        return;
      }
  
      const response = await axios.put(
        `http://localhost:5288/api/ContraseñasUsuarios/${passwordId}`,  // Usamos 'passwordId' en la URL
        { Id:passwordId, Contraseña: newPassword, UltimaModi: new Date().toISOString() }, // Nueva contraseña y fecha
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
  
      if (response.status === 204) {
        alert('Contraseña cambiada con éxito');
      }
    } catch (error) {
      console.error('Error cambiando la contraseña:', error);
      if (error.response) {
        console.error('Detalles del error:', error.response.data);
        alert('Hubo un error al cambiar la contraseña: ' + error.response.data);
      } else {
        alert('Hubo un error al cambiar la contraseña');
      }
    }
  },
  
  async existeUsuario(email: string) {
    const response = await fetch(`${API_URL}/existeUsuario/${email}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    });

    if (!response.ok) {
      if (response.status === 404) {
        return { existe: false }; // Usuario no encontrado
      }
      throw new Error('Error verificando el usuario');
    }

    const data = await response.json();
    return data; // { existe: true, id: userId } o { existe: false }
  },
  
};
