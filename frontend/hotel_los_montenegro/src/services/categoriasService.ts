const API_URL_CATEGORIAS = 'http://localhost:5288/api/Categoriums'; // URL de la API para las categorías

export default {
  async obtenerCategorias() {
    const response = await fetch(API_URL_CATEGORIAS, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    });

    if (!response.ok) {
      throw new Error('Error al obtener categorías');
    }

    const data = await response.json();
    return data;
  },

  async crearCategoria(categoria: any) {
    const response = await fetch(API_URL_CATEGORIAS, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(categoria),
    });

    if (!response.ok) {
      throw new Error('Error al crear categoría');
    }

    const data = await response.json();
    return data;
  },

  async actualizarCategoria(id: number, categoria: any) {
    const response = await fetch(`${API_URL_CATEGORIAS}/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(categoria),
    });

    if (!response.ok) {
      throw new Error('Error al actualizar categoría');
    }
  },

  async eliminarCategoria(id: number) {
    const response = await fetch(`${API_URL_CATEGORIAS}/${id}`, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
      },
    });

    if (!response.ok) {
      throw new Error('Error al eliminar categoría');
    }
  }
};
