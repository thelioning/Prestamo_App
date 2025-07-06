const fetch = require('node-fetch');

exports.handler = async function(event, context) {
  console.log("Método HTTP:", event.httpMethod);
  
  if (event.httpMethod !== 'POST') {
    console.log("Método no permitido");
    return {
      statusCode: 405,
      body: JSON.stringify({ error: 'Método no permitido, use POST' }),
    };
  }

  try {
    const data = JSON.parse(event.body);
    console.log("Datos recibidos:", data);

    const { token, nuevaClave } = data;

    if (!token || !nuevaClave) {
      console.log("Faltan token o nuevaClave");
      return {
        statusCode: 400,
        body: JSON.stringify({ error: 'Faltan token o nuevaClave' }),
      };
    }

    const apiUrl = process.env.API_URL || 'NO_API_URL';
    console.log("API URL:", apiUrl);

    if (apiUrl === 'NO_API_URL') {
      console.log("Variable API_URL no configurada");
      return {
        statusCode: 500,
        body: JSON.stringify({ error: 'API_URL no está configurada en el entorno' }),
      };
    }

    const apiResponse = await fetch(apiUrl, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ Token: token, NuevaClave: nuevaClave }),
    });

    console.log("Respuesta de la API - Status:", apiResponse.status);

    const apiResult = await apiResponse.json();
    console.log("Respuesta de la API - Body:", apiResult);

    if (!apiResponse.ok) {
      return {
        statusCode: apiResponse.status,
        body: JSON.stringify({ error: apiResult.error || 'Error desconocido' }),
      };
    }

    return {
      statusCode: 200,
      body: JSON.stringify({ message: 'Contraseña actualizada correctamente' }),
    };

  } catch (error) {
    console.log("Excepción capturada:", error);
    return {
      statusCode: 500,
      body: JSON.stringify({ error: 'Error interno', details: error.message }),
    };
  }
};