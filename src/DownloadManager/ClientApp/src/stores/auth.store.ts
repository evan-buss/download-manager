import { writable } from "svelte/store";
import { httpClient } from "../services/http.service";
import type { AxiosResponse } from "axios";
import { connection } from "./ws.store";

export interface User {
  accessToken: string;
  refreshToken: string;
}

function createCurrentUser() {
  const { subscribe, set, update } = writable<User>({
    accessToken: localStorage.getItem("access-token"),
    refreshToken: localStorage.getItem("refresh-token"),
  });

  if (isLoggedIn()) {
    connection.connect();
  }

  async function login(username: string, password: string): Promise<boolean> {
    const loginSuccess = await httpClient
      .post<LoginRequest, AxiosResponse<TokenResponse>>("api/auth/login", {
        username,
        password,
      })
      .then((resp) => {
        console.log(resp.status);
        localStorage.setItem("access-token", resp.data.accessToken);
        localStorage.setItem("refresh-token", resp.data.refreshToken);
        return resp.data;
      })
      .then((tokens) => {
        set({
          accessToken: tokens.accessToken,
          refreshToken: tokens.refreshToken,
        });
        return true;
      })
      .catch((error) => {
        console.log(error);
        console.log("catching");
        return false;
      });

    connection.connect();

    return loginSuccess;
  }

  function logout() {
    localStorage.removeItem("access-token");
    localStorage.removeItem("refresh-token");
    connection.disconnect();
    set(null);
  }

  function isLoggedIn() {
    let user: User;
    subscribe((curr) => (user = curr));
    return (
      user !== null && user.accessToken !== null && user.refreshToken !== null
    );
  }

  return {
    subscribe,
    login,
    logout,
    isLoggedIn,
  };
}

export const user = createCurrentUser();

export interface LoginRequest {
  username: string;
  password: string;
}

export interface TokenResponse {
  accessToken: string;
  refreshToken: string;
}
