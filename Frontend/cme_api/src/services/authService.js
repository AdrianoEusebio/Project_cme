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

      const { token, username: user, email, idGroup, idUser } = response.data;
      localStorage.setItem("token", token);
      localStorage.setItem("userId", idUser)
      localStorage.setItem("username", user);
      localStorage.setItem("email", email);
      localStorage.setItem("role", idGroup);

      this.getUserCredentials();

      return response.data;
    } catch (error) {
      throw new Error(error.response?.data?.message || "Erro ao fazer login");
    }
  },

  async getUserCredentials() {
    try {
      const token = localStorage.getItem("token");
      const userId = localStorage.getItem("userId");
      console.log("token:", token);
      console.log("ID:", userId);

      if (!token || !userId) {
        alert("Usuário não está logado.");
        return;
      }

      const response = await api.get(`/api/User/${userId}`, {
        headers: { Authorization: `Bearer ${token}` },
      });

      const user = response.data;
      alert(`
        📋 Informações da Conta:
        👤 Usuário: ${user.username}
        📧 Email: ${user.email}
        🔑 Grupo: ${user.idGroup === 1 ? "Administrador" : "Enfermeiro"}
      `);
    } catch (error) {
      console.error("Erro ao buscar credenciais:", error);
      alert("Erro ao recuperar informações da conta.");
    }
  },
};
