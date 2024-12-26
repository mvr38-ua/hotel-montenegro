export default {
    name: 'LoginForm',
    data() {
        return {
            email: '',
            password: '',
            errorMessage: '',
        };
    },
    methods: {
        login() {
            fetch('https://tu-api-url/login', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    email: this.email,
                    password: this.password,
                }),
            })
                .then((response) => {
                    if (!response.ok) {
                        throw new Error('Credenciales incorrectas');
                    }
                    return response.json();
                })
                .then((data) => {
                    const token = data.token; // Suponiendo que la API devuelve el token en la propiedad 'token'
                    if (token) {
                        localStorage.setItem('authToken', token); // Almacena el token
                        alert('¡Inicio de sesión exitoso!');
                        this.$emit('login-success'); // Notifica al componente padre si es necesario
                    } else {
                        throw new Error('No se recibió el token de autenticación');
                    }
                })
                .catch((error) => {
                    this.errorMessage = error.message;
                });
        },
        logout() {
            // Método opcional para cerrar sesión
            localStorage.removeItem('authToken');
            alert('Sesión cerrada');
        },
    },
    template: `
        <div class="login-form">
            <h2>Iniciar Sesión</h2>
            <div v-if="errorMessage" class="error">{{ errorMessage }}</div>
            <form @submit.prevent="login">
                <div>
                    <label for="email">Correo Electrónico:</label>
                    <input type="email" id="email" v-model="email" required />
                </div>
                <div>
                    <label for="password">Contraseña:</label>
                    <input type="password" id="password" v-model="password" required />
                </div>
                <button type="submit">Ingresar</button>
            </form>
        </div>
    `,
};
