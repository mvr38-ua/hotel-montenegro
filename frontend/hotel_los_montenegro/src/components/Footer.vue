<template>
  <footer class="footer">
    <div class="footer-content">
      <div class="footer-section">
        <h3>Los Montenegro</h3>
        <p>&copy; 2025. Todos los derechos reservados.</p>
      </div>
      <div class="footer-section">
        <h4>Enlaces útiles</h4>
        <ul>
          <li><router-link to="/">Inicio</router-link></li>
          <li><router-link to="/about">Acerca de</router-link></li>
        </ul>
      </div>
      <div class="footer-section">
        <h4>Síguenos</h4>
        <div class="social-icons">
          <a href="https://www.facebook.com" target="_blank" rel="noopener noreferrer">
            <i class="fab fa-facebook-f"></i>
          </a>
          <a href="https://www.twitter.com" target="_blank" rel="noopener noreferrer">
            <i class="fab fa-twitter"></i>
          </a>
          <a href="https://www.instagram.com" target="_blank" rel="noopener noreferrer">
            <i class="fab fa-instagram"></i>
          </a>
        </div>
      </div>
    </div>
  </footer>
</template>

<script>
export default {};
</script>

<style scoped>
.footer {
  background-color: black;
  color: white;
  padding: 20px 10%;
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  flex-wrap: wrap;
}

.footer-content {
  width: 100%;
  display: flex;
  justify-content: space-between;
  flex-wrap: wrap;
}

.footer-section {
  flex: 1 1 30%;
  margin: 10px;
}

.footer-section h3,
.footer-section h4 {
  margin-bottom: 10px;
  font-size: 1.2em;
  font-weight: bold;
}

.footer-section p {
  font-size: 0.9em;
}

.footer-section ul {
  list-style: none;
  padding: 0;
}

.footer-section ul li {
  margin-bottom: 5px;
}

.footer-section ul li a {
  text-decoration: none;
  color: white;
  font-size: 0.9em;
}

.footer-section ul li a:hover {
  text-decoration: underline;
}

.social-icons a {
  margin-right: 15px;
  color: white;
  font-size: 1.5em;
  transition: color 0.3s;
}

.social-icons a:hover {
  color: #f0a500;
}
</style>
