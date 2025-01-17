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
  max-width: 900px;
  margin: 0 auto;
  padding: 30px;
  background-color: #f4f4f9;
  border-radius: 12px;
  box-shadow: 0 6px 12px rgba(0, 0, 0, 0.1);
  font-family: 'Georgia', serif;
}

h1 {
  color: #2c3e50; /* Dark gray */
  font-size: 3rem;
  margin-bottom: 25px;
  text-align: center;
  font-weight: bold;
}

h2 {
  color: #34495e; /* Medium dark gray */
  font-size: 2rem;
  margin-top: 25px;
  margin-bottom: 15px;
  border-bottom: 2px solid #34495e;
  padding-bottom: 5px;
}

p {
  font-size: 1.2rem;
  color: #7f8c8d; /* Medium gray */
  line-height: 1.8;
  margin-bottom: 20px;
}

.services-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
  gap: 25px;
  margin-top: 25px;
}

.service-item {
  background-color: #ffffff;
  padding: 20px;
  border-radius: 10px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  text-align: center;
  transition: transform 0.3s ease, box-shadow 0.3s ease;
}

.service-item:hover {
  transform: translateY(-5px);
  box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
}

.service-item h3 {
  color: #2c3e50; /* Dark gray */
  font-size: 1.8rem;
  margin-bottom: 15px;
}

.service-item p {
  font-size: 1.1rem;
  color: #7f8c8d; /* Medium gray */
}

blockquote {
  font-size: 1.2rem;
  color: #555; /* Medium dark gray */
  border-left: 4px solid #34495e;
  padding-left: 15px;
  margin: 20px 0;
  font-style: italic;
}
</style>