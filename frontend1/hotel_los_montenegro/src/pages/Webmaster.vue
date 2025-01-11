<template>
    <div class="admin-panel">
      <Sidebar />
      <div class="content">
        <router-view />
      </div>
    </div>
</template>
  
<script setup>
import Sidebar from '../components/Sidebar.vue';

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