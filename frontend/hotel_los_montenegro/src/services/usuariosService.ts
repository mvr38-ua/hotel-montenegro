const API_URL = 'http://localhost:5288/api/Usuario'; // Cambia la URL si tu API está alojada en otro lugar

export default {
  async obtenerUsuarios() {
    const response = await fetch(API_URL, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      }
    });

    if (!response.ok) {
      throw new Error('Error al obtener Usuario');
    }

    const data = await response.json();
    return data;
  },

  async actualizarUsuario(id: string, usuario: any) {
    console.log(usuario);
    const response = await fetch(`${API_URL}/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(usuario)
    });

    if (!response.ok) {
      throw new Error('Error al actualizar habitación');
    }
  },

  async eliminarUsuario(id: string) {
    const response = await fetch(`${API_URL}/${id}`, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
      }
    });

    if (!response.ok) {
      throw new Error('Error al eliminar habitación');
    }
  }, 

  async obtenerTiposUsuario() {
    const response = await fetch('http://localhost:5288/api/TipoUsuarios',{
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    });
    if (!response.ok) {
      throw new Error('Error al obtener categorías');
    }
    return await response.json();
  }
  
};
