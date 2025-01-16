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
  
};
