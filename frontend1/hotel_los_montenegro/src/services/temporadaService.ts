const API_URL = 'http://localhost:5288/api/Temporadums'; // Cambia la URL si tu API está alojada en otro lugar

export default {
  async obtenerTemporadas() {
    const response = await fetch(API_URL, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    });

    if (!response.ok) {
      throw new Error('Error al obtener temporadas');
    }

    const data = await response.json();
    return data;
  },

  async crearTemporada(temporada) {
    const response = await fetch(API_URL, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(temporada),
    });

    if (!response.ok) {
      throw new Error('Error al crear temporada');
    }

    const data = await response.json();
    return data;
  },

  async actualizarTemporada(id, temporada) {
    const response = await fetch(`${API_URL}/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(temporada),
    });

    if (!response.ok) {
      throw new Error('Error al actualizar temporada');
    }
  },

  async eliminarTemporada(id) {
    const response = await fetch(`${API_URL}/${id}`, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
      },
    });

    if (!response.ok) {
      throw new Error('Error al eliminar temporada');
    }
  }
};
