export default {
    name: 'Principal',
    template: `
        <div>
            <h1>Bienvenido al Hotel Montenegro</h1>
            <button @click="$router.push('/login')">Iniciar Sesión</button>
        </div>
    `,
};
