import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:8000",
});

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
