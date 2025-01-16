<template>
  <div class="about-container">
    <h1>Acerca de Los Montenegro</h1>
    <p>Los Montenegro es un hotel de lujo que ofrece una experiencia única y confortable para todos sus huéspedes.</p>
    <section>
      <h2>Nuestra Historia</h2>
      <p>Fundado en 1990, Los Montenegro ha sido un símbolo de elegancia y sofisticación. Con más de 30 años de experiencia, hemos perfeccionado el arte de la hospitalidad.</p>
    </section>
    <section>
      <h2>Nuestros Servicios</h2>
      <div class="services-grid">
        <div class="service-item">
          <h3>Habitaciones de lujo</h3>
          <p>Con vistas panorámicas que te dejarán sin aliento.</p>
        </div>
        <div class="service-item">
          <h3>Restaurantes gourmet</h3>
          <p>Disfruta de una experiencia culinaria única.</p>
        </div>
        <div class="service-item">
          <h3>Spa y bienestar</h3>
          <p>Relájate y rejuvenece en nuestro spa de clase mundial.</p>
        </div>
        <div class="service-item">
          <h3>Piscinas y recreación</h3>
          <p>Áreas recreativas para toda la familia.</p>
        </div>
        <div class="service-item">
          <h3>Salones para eventos</h3>
          <p>Perfectos para conferencias y celebraciones.</p>
        </div>
      </div>
    </section>
    <section>
      <h2>Compromiso con la Sostenibilidad</h2>
      <p>En Los Montenegro, estamos comprometidos con prácticas sostenibles y responsables. Implementamos iniciativas ecológicas para reducir nuestro impacto ambiental.</p>
    </section>
    <section>
      <h2>Testimonios</h2>
      <blockquote>
        <p>"Una experiencia inolvidable. El servicio es excepcional y las instalaciones son de primera clase." - Juan Pérez</p>
      </blockquote>
      <blockquote>
        <p>"El mejor hotel en el que me he hospedado. Definitivamente regresaré." - María García</p>
      </blockquote>
    </section>
  </div>
</template>

<style scoped>
.about-container {
  max-width: 800px;
  margin: 0 auto;
  padding: 20px;
  background-color: #f9f9f9;
  border-radius: 10px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

h1 {
  color: #2c3e50;
  font-size: 2.5rem;
  margin-bottom: 20px;
}

h2 {
  color: #34495e;
  font-size: 1.8rem;
  margin-top: 20px;
}

p {
  font-size: 1.1rem;
  color: #7f8c8d;
  line-height: 1.6;
}

.services-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
  gap: 20px;
  margin-top: 20px;
}

.service-item {
  background-color: #ffffff;
  padding: 15px;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  text-align: center;
}

.service-item h3 {
  color: #2c3e50;
  font-size: 1.5rem;
  margin-bottom: 10px;
}

.service-item p {
  font-size: 1rem;
  color: #7f8c8d;
}
</style>