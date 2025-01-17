const API_URL = 'http://localhost:5288/api/Habitaciones';

export default {
  async obtenerHabitaciones() {
    const response = await fetch(API_URL, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      }
    });

    if (!response.ok) {
      throw new Error('Error al obtener habitaciones');
    }

    const data = await response.json();
    return data;
  },

  async crearHabitacion(habitacion: any) {
    const response = await fetch(API_URL, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(habitacion)
    });

    if (!response.ok) {
      throw new Error('Error al crear habitación');
    }

    const data = await response.json();
    return data;
  },

  async actualizarHabitacion(id: string, habitacion: any) {
    const response = await fetch(`${API_URL}/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(habitacion)
    });

    if (!response.ok) {
      throw new Error('Error al actualizar habitación');
    }
  },

  async eliminarHabitacion(id: string) {
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

  async obtenerCategorias() {
    const response = await fetch('http://localhost:5288/api/Categoriums',{
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    });
    if (!response.ok) {
      throw new Error('Error al obtener categorías');
    }
    return await response.json();
  },

  async bloquearHabitacion(id: string) {
    const response = await fetch(`${API_URL}/bloquear/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
    });

    if (!response.ok) {
      throw new Error('Error al bloquear habitación');
    }
  },

  async desbloquearHabitacion(id: string) {
    const response = await fetch(`${API_URL}/desbloquear/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
    });

    if (!response.ok) {
      throw new Error('Error al desbloquear habitación');
    }
  },
  
};
