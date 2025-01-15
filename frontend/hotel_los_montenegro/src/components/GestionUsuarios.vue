<template>
    <div class="gestion-usuarios">
      <div class="header-container">
        <h2>Listado de usuarios</h2>
        <button class="add-button" @click="abrirModal(null)">Añadir nuevo</button>
      </div>
      <table class="table">
        <thead>
          <tr>
            <th>Email</th>
            <th>Nombre</th>
            <th>DNI</th>
            <th>Teléfono</th>
            <th>Activo</th>
            <th>Fecha de nacimiento</th>
            <th>Direccion</th>
            <th>Tipo de usuario</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="usuario in usuarios" :key="usuario.id">
            <td>{{ usuario.email }}</td>
            <td>{{ usuario.nombre + ' ' + usuario.apellidos }}</td>
            <td>{{ usuario.dni }}</td>
            <td>{{ usuario.movil + '\n' + usuario.telefono }}</td>
            <td>{{ usuario.activo ? 'Sí' : 'No' }}</td>
            <td>{{ usuario.fechaNac }}</td>
            <td>{{ usuario.direccion.direccion }}</td>
            <td>{{ usuario.tipoUsuario.tipo }}</td>
            <td>
              <button class="edit-button" @click="abrirModal(usuario)">✏️</button>
              <button class="delete-button" @click="eliminarUsuarioHandler(usuario.id)">🗑️</button>
            </td>
          </tr>
        </tbody>
      </table>
  
      <!-- Modal -->
      <div v-if="mostrarModal" class="modal">
        <div class="modal-content">
          <h3>{{ usuarioIdEdicion ? 'Editar usuario' : 'Crear nuevo usuario' }}</h3>
          <form @submit.prevent="guardarUsuario">
            <label>
              Email:
              <input v-model="nuevoUsuarioData.email" type="email" required />
            </label>
            <label>
              Nombre:
              <input v-model="nuevoUsuarioData.nombre" type="text" required />
            </label>
            <label>
              Apellidos:
              <input v-model="nuevoUsuarioData.apellidos" type="text" required />
            </label>
            <label>
              DNI:
              <input v-model="nuevoUsuarioData.dni" type="text" required />
            </label>
            <label>
              Teléfono:
              <input v-model="nuevoUsuarioData.telefono" type="text" />
            </label>
            <label>
              Movil:
              <input v-model="nuevoUsuarioData.movil" type="text" />
            </label>
            <label>
              Activo:
              <input v-model="nuevoUsuarioData.activo" type="checkbox" />
            </label>
            <label>
              Fecha de nacimiento:
              <input v-model="nuevoUsuarioData.fechaNac" type="date" />
            </label>
            <label>Genero:
              <select v-model="nuevoUsuarioData.genero" required>
                <option value="M">Male</option>
                <option value="F">Female</option>
              </select>
            </label>
            <label>
              Tipo de Usuario:
              <select v-model="tipoUsuarioSeleccionado" required>
                <option value="" disabled>Seleccione el tipo de usuario</option>
                <option v-for="tipoUsuario in tiposUsuario" :key="tipoUsuario.id" :value="tipoUsuario.id">
                  {{ tipoUsuario.tipo }}
                </option>
              </select>
            </label>
            <div class="modal-actions">
              <button type="submit" class="save-button">Guardar</button>
              <button type="button" class="cancel-button" @click="cerrarModal">Cancelar</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </template>
  
  <script setup>
  import { ref, onMounted } from 'vue';
  import usuariosService from '../services/usuariosService';
  import authService from '../services/authService';
  
  const usuarios = ref([]);
  const nuevoUsuarioData = ref({
    email: '',
    nombre: '',
    apellidos: '',
    dni: '',
    telefono: '',
    movil: '',
    activo: false,
    fechaNac: '',
    tipoUsuario: '',
    genero: '',
  });
  const mostrarModal = ref(false);
  const usuarioIdEdicion = ref(null);
  const tiposUsuario = ref([]);
  const tipoUsuarioSeleccionado = ref(null);
  
  const cargarTiposUsuario = async () => {
      try {
        const response = await usuariosService.obtenerTiposUsuario();
        tiposUsuario.value = response.$values;
      } catch (error) {
        console.error('Error al cargar categorías:', error);
      }
    };

  const cargarUsuarios = async () => {
    try {
      const response = await usuariosService.obtenerUsuarios();
      usuarios.value = response.$values.map(usuario => ({
      ...usuario,
      fechaNac: usuario.fechaNac 
        ? new Date(usuario.fechaNac).toISOString().split("T")[0] // Formatear fecha
        : null, // Manejar fechas nulas si es necesario
    }));

    } catch (error) {
      console.error('Error al cargar usuarios:', error);
    }
  };
  
  const eliminarUsuarioHandler = async (id) => {
    if (confirm('¿Estás seguro de que quieres eliminar este usuario?')) {
      try {
        await usuariosService.eliminarUsuario(id);
        await cargarUsuarios(); // Actualizar la lista después de eliminar
      } catch (error) {
        console.error('Error al eliminar usuario:', error);
      }
    }
  };
  
  const abrirModal = (usuario) => {
    if (usuario) {
      // Estamos editando, cargar los datos del usuario
      usuarioIdEdicion.value = usuario.id;
      nuevoUsuarioData.value = { ...usuario };
    } else {
      // Nueva habitación
      usuarioIdEdicion.value = null;
      nuevoUsuarioData.value = {
        email: '',
        nombre: '',
        apellidos: '',
        dni: '',
        telefono: '',
        movil: '',
        activo: false,
        fechaNac: '',
        tipoUsuario: '',
        genero: '',
      };
    }
    mostrarModal.value = true;
  };
  
  const cerrarModal = () => {
    mostrarModal.value = false;
  };
  
  const guardarUsuario = async () => {
    if (!nuevoUsuarioData.value.email || !nuevoUsuarioData.value.nombre || !nuevoUsuarioData.value.apellidos || !nuevoUsuarioData.value.dni || !nuevoUsuarioData.value.movil) {
      alert('Por favor, completa todos los campos correctamente.');
      return;
    }
  
    const usuario = {
        email: nuevoUsuarioData.value.email,
        name: nuevoUsuarioData.value.nombre,
        lastname: nuevoUsuarioData.value.apellidos,
        dni: nuevoUsuarioData.value.dni,
        phone: nuevoUsuarioData.value.telefono,
        mobile: nuevoUsuarioData.value.movil,
        birthdate: nuevoUsuarioData.value.fechaNac,
        addressId: 1,
        userTypeId: tipoUsuarioSeleccionado.value,
        password: '123456',
        gender: nuevoUsuarioData.value.genero,
    };
    const usuarioAct = {
        id: usuarioIdEdicion.value,
        email: nuevoUsuarioData.value.email,
        nombre: nuevoUsuarioData.value.nombre,
        apellidos: nuevoUsuarioData.value.apellidos,
        dni: nuevoUsuarioData.value.dni,
        telefono: nuevoUsuarioData.value.telefono,
        movil: nuevoUsuarioData.value.movil,
        fechaNac: nuevoUsuarioData.value.fechaNac,
        direccionId: 1,
        tipoUsuarioId: tipoUsuarioSeleccionado.value,
        genero: nuevoUsuarioData.value.genero,
    };
  
    /* if (usuarioIdEdicion.value) {
      usuario.id = usuarioIdEdicion.value;
    } */
  
    try {
      if (usuarioIdEdicion.value) {
        // Actualizar usuario existente
        await usuariosService.actualizarUsuario(usuarioIdEdicion.value, usuarioAct);
      } else {
        // Crear nuevo usuario
        await authService.register(usuario);
      }
      await cargarUsuarios(); // Actualizar la lista
      cerrarModal(); // Cerrar el modal
    } catch (error) {
      console.error('Error al guardar usuario:', error);
    }
  };
  
  onMounted(() => {
    cargarUsuarios();
    cargarTiposUsuario();
  });
  </script>
  
  <style scoped>
  .gestion-usuarios {
    padding: 20px;
    display: flex;
    flex-direction: column;
  }
  
  h2 {
    margin-bottom: 20px;
    font-size: 1.5rem;
    font-weight: 800;
  }
  
  .add-button {
    background-color: #707377;
    color: white;
    border: none;
    padding: 10px 20px;
    border-radius: 4px;
    font-size: 14px;
    cursor: pointer;
  }
  
  .header-container {
    display: flex;
    align-items: center;
    margin-bottom: 20px;
    justify-content: space-between;
  }
  
  .table {
    width: 100%;
    border-collapse: collapse;
    background-color: #f9f9f9;
  }
  
  .table thead {
    background-color: #ccc;
  }
  
  .table th,
  .table td {
    text-align: left;
    padding: 10px;
    border: 1px solid #ddd;
  }
  
  .table th {
    font-weight: bold;
  }
  
  .edit-button,
  .delete-button {
    background: none;
    border: none;
    cursor: pointer;
    font-size: 16px;
  }
  
  .edit-button {
    color: #007bff;
  }
  
  .delete-button {
    color: #dc3545;
  }
  
  .edit-button:hover {
    color: #0056b3;
  }
  
  .delete-button:hover {
    color: #a71d2a;
  }
  
  .modal {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: rgba(0, 0, 0, 0.5);
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 1000;
  }
  
  .modal-content {
    background: white;
    padding: 20px;
    border-radius: 8px;
    width: 400px;
    max-width: 90%;
    box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  }
  
  .modal-content h3 {
    margin-top: 0;
  }
  
  .modal-content label {
    display: block;
    margin: 10px 0;
  }
  
  .modal-actions {
    display: flex;
    justify-content: flex-end;
    gap: 10px;
  }
  
  .save-button {
    background-color: #007bff;
    color: white;
    border: none;
    padding: 10px 20px;
    border-radius: 4px;
    cursor: pointer;
  }
  
  .save-button:hover {
    background-color: #0056b3;
  }
  
  .cancel-button {
    background-color: #dc3545;
    color: white;
    border: none;
    padding: 10px 20px;
    border-radius: 4px;
    cursor: pointer;
  }
  
  .cancel-button:hover {
    background-color: #a71d2a;
  }
  </style>
  