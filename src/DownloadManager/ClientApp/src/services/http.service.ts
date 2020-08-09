import axios from "axios";

export const BASE_URL = "https://localhost:44355/";

export const http = axios.create({
  baseURL: BASE_URL,
});
