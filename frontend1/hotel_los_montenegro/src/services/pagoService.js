const API_URL = 'https://sandbox.green-sys.es/sales'; // URL del entorno de pruebas

const API_KEY = 'sk_u0flwbjphgm5xrcazr'; // Tu API Key

export default {
  async procesarPago(pagoData) {
    const response = await fetch(API_URL, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'x-api-key': API_KEY, // Incluye tu API Key en el header
      },
      body: JSON.stringify(pagoData),
    });

    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(`Error al procesar el pago: ${errorText}`);
    }

    return await response.json(); // Retorna la URL del TPV
  }
};
