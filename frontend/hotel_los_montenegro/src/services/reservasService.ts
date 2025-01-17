const API_URL = 'http://localhost:5288/api/Reserva'; 

export default {
  async obtenerReservas() {
    const response = await fetch(API_URL, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    });

    if (!response.ok) {
      throw new Error('Error al obtener reservas');
    }

    return await response.json();
  },

  async obtenerReserva(id: number) {
    const response = await fetch(`${API_URL}/${id}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    });

    if (!response.ok) {
      throw new Error('Error al obtener la reserva');
    }

    return await response.json();
  },

  async obtenerReservasPorUsuario(usuarioId: number) {
    const response = await fetch(`${API_URL}/Usuario/${usuarioId}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    });

    if (!response.ok) {
      throw new Error('Error al obtener reservas del usuario');
    }

    return await response.json();
  },

  async obtenerReservasPorFecha(fechaInicio: string, fechaFinal: string) {
    const response = await fetch(`${API_URL}/Fecha?fechaInicio=${fechaInicio}&fechaFinal=${fechaFinal}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    });

    if (!response.ok) {
      throw new Error('Error al obtener reservas por rango de fechas');
    }

    return await response.json();
  },

  async crearReserva(reserva: any) {
    const response = await fetch(API_URL, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(reserva),
    });

    if (!response.ok) {
      throw new Error('Error al crear reserva');
    }

    return await response.json();
  },

  async actualizarReserva(id: number, reserva: any) {
    const response = await fetch(`${API_URL}/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(reserva),
    });

    if (!response.ok) {
      throw new Error('Error al actualizar reserva');
    }
  },

  async eliminarReserva(id: number) {
    const response = await fetch(`${API_URL}/${id}`, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
      },
    });

    if (!response.ok) {
      throw new Error('Error al eliminar reserva');
    }
  },
  
  // Método para obtener las habitaciones disponibles
  async obtenerHabitacionesDisponibles(fechaInicio: string, fechaFin: string, capacidadMinima: number | null, categoriaId: number | null) {
    const requestBody = {
      fechaInicio: fechaInicio,
      fechaFin: fechaFin,
      capacidadMinima: capacidadMinima,
      categoriaId: categoriaId
    };

    const response = await fetch(`${API_URL}/HabitacionesDisponibles`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(requestBody),
    });

    if (!response.ok) {
      throw new Error('Error al obtener habitaciones disponibles');
    }

    return await response.json();
  },
  
};
