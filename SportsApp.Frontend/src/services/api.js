import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost/api'  // Punto de entrada único
});

export default api;