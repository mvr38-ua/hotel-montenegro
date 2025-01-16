<template>
  <div id="app" class="container mt-4">
    <!-- Contenedor Principal -->
    <div class="row">
      <!-- Imagen y Descripción -->
      <div class="col-md-6">
        <img
            class="img-fluid rounded"
            :src="imageSrc"
            alt="Room"
        />
      </div>
      <div class="col-md-6">
        <h1 class="mb-3">Habitación Estándar</h1>
        <div class="d-flex mb-3">
          <i class="fas fa-bed me-3"></i>
          <i class="fas fa-wifi me-3"></i>
          <i class="fas fa-tv me-3"></i>
          <i class="fas fa-shower me-3"></i>
        </div>
        <p class="fw-bold">
          Ocupación: 2 Adultos, 0 Niños, 0 Bebés (Máximo 2 ocupantes)
        </p>
        <p>
          Estas habitaciones no tienen balcón y sus ventanas ofrecen vistas a un
          pequeño jardín interior. Disponen de dos camas de 1.05m o una cama de
          2x2m. Algunos baños cuentan con ducha y otros con bañera.
        </p>
        <div class="mb-3">
          <label for="guests" class="form-label">Ocupantes:</label>
          <select id="guests" class="form-select w-auto">
            <option>1</option>
            <option>2</option>
          </select>
        </div>
      </div>
    </div>
    <!-- Lista de Precios -->
    <div class="mt-5">
      <h2 class="mb-4">Precios</h2>
      <ul class="list-group">
        <li
            class="list-group-item d-flex justify-content-between align-items-center"
        >
          <div>
            <strong>Sólo Alojamiento</strong>
            <p class="mb-0 text-muted">132,06 € / noche</p>
          </div>
          <button class="btn btn-primary">Reservar</button>
        </li>
        <li
            class="list-group-item d-flex justify-content-between align-items-center"
        >
          <div>
            <strong>Alojamiento y desayuno</strong>
            <p class="mb-0 text-muted">155,36 € / noche</p>
          </div>
          <button class="btn btn-primary">Reservar</button>
        </li>
        <li
            class="list-group-item d-flex justify-content-between align-items-center"
        >
          <div>
            <strong>Media pensión</strong>
            <p class="mb-0 text-muted">224,42 € / noche</p>
          </div>
          <button class="btn btn-primary">Reservar</button>
        </li>
        <li
            class="list-group-item d-flex justify-content-between align-items-center"
        >
          <div>
            <strong>Pensión completa</strong>
            <p class="mb-0 text-muted">293,48 € / noche</p>
          </div>
          <button class="btn btn-primary">Reservar</button>
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
import estandarImage from '../assets/estandar.png';

export default {
  data() {
    return {
      imageSrc: estandarImage
    };
  }
};
</script>

<style>
</style>