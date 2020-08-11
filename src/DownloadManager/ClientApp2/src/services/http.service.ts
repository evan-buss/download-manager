import axios from "axios";

export const BASE_URL = "https://localhost:44355/";
// export const BASE_URL = window.location.origin;

export const httpClient = axios.create({
  baseURL: BASE_URL,
});
