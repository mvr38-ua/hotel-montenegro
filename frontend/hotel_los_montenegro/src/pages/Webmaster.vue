<template>
  <div v-if="isAuthorized" class="admin-panel">
    <Sidebar />
    <div class="content">
      <router-view />
    </div>
  </div>
  <div v-else>
    <p>No tienes permiso para acceder a esta página.</p>
  </div>
</template>

<script setup>
import Sidebar from '../components/Sidebar.vue';
import { onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';
import authService from '../services/authService';

// Variables reactivas
const isAuthorized = ref(false);
const router = useRouter(); // Acceder al router

const verificarPermiso = async () => {
  try {
    const rolUsuario = await authService.obtenerRolDelUsuario();
    if (rolUsuario === 1002) {
      isAuthorized.value = true; // Permitir acceso
    } else {
      isAuthorized.value = false; // Denegar acceso
      router.push('/'); // Redirigir al inicio
    }
  } catch (error) {
    console.error('Error al verificar permisos:', error);
    router.push('/'); // Redirigir al inicio en caso de error
  }
};

// Usar la función verificarPermiso cuando el componente se monta
onMounted(verificarPermiso);
</script>

<style>
.admin-panel {
  display: flex;
  background-color: #f9f9f9;
  height: 100vh;
}

.content {
  flex: 1;
  padding: 20px;
  background-color: #fff;
  border-left: 1px solid #ddd;
  overflow-y: auto;
}
</style>
