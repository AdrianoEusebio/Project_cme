import axios from "axios";

console.log("authService.js foi carregado!");

const api = axios.create({
  baseURL: "http://localhost:8000",
});
console.log("Variavels:", api);

export default {
  async login(username, password) {
    try {
      const response = await api.post(`/api/User/login`, {
        username: username,
        password: password,
      });

      return response.data;
    } catch (error) {
      throw new Error(error.response?.data?.message || "Erro ao fazer login");
    }
  },
};
