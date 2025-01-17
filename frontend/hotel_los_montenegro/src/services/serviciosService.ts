const API_URL = 'http://localhost:5288/api/Servicios';

export default {
  async obtenerServicios() {
    const response = await fetch(API_URL, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      }
    });

    if (!response.ok) {
      throw new Error('Error al obtener servicios');
    }

    const data = await response.json();
    return data;
  },

  async obtenerServicio(id: number) {
    const response = await fetch(`${API_URL}/${id}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      }
    });

    if (!response.ok) {
      throw new Error('Error al obtener el servicio');
    }

    const data = await response.json();
    return data;
  },

  async crearServicio(servicio: any) {
    const response = await fetch(API_URL, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(servicio)
    });

    if (!response.ok) {
      throw new Error('Error al crear servicio');
    }

    const data = await response.json();
    return data;
  },

  async actualizarServicio(id: number, servicio: any) {
    const response = await fetch(`${API_URL}/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(servicio)
    });

    if (!response.ok) {
      throw new Error('Error al actualizar servicio');
    }
  },

  async eliminarServicio(id: number) {
    const response = await fetch(`${API_URL}/${id}`, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
      }
    });

    if (!response.ok) {
      throw new Error('Error al eliminar servicio');
    }
  }
};
