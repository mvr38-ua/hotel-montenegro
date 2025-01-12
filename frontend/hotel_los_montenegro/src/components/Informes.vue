<template>
    <div class="informes">
        <h1>Informes</h1>
        <p>Bienvenido a la sección de informes del Hotel Los Montenegro.</p>
    </div>
</template>

<script>
export default {
    name: 'Informes',
    data() {
        return {
            message: 'Esta es la sección de informes.'
        };
    }
};
</script>

<style scoped>
.informes {
    text-align: center;
    margin-top: 50px;
}

h1 {
    color: #2c3e50;
}

p {
    color: #34495e;
}
</style>