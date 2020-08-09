import { writable } from "svelte/store";
import { http } from "../services/http.service";
import type { AxiosResponse } from "axios";

export interface User {
  accessToken: string;
  refreshToken: string;
}

function createCurrentUser() {
  const { subscribe, set, update } = writable<User>({
    accessToken: localStorage.getItem("access-token"),
    refreshToken: localStorage.getItem("refresh-token"),
  });

  return {
    subscribe,
    login: async (username: string, password: string): Promise<boolean> => {
      const tokens = await login(username, password);
      if (tokens) {
        update((user) => {
          user.accessToken = tokens.accessToken;
          user.refreshToken = tokens.refreshToken;
          return user;
        });
        return true;
      }
      return false;
    },
    logout: () => {
      localStorage.removeItem("access-token");
      localStorage.removeItem("refresh-token");
      set(null);
    },
    isLoggedIn: () => {
      let user: User;
      subscribe((curr) => (user = curr));
      return (
        user !== null && user.accessToken !== null && user.refreshToken !== null
      );
    },
  };
}

async function login(
  username: string,
  password: string
): Promise<TokenResponse> {
  return await http
    .post<LoginRequest, AxiosResponse<TokenResponse>>("api/auth/login", {
      username,
      password,
    })
    .then((tokens) => {
      localStorage.setItem("access-token", tokens.data.accessToken);
      localStorage.setItem("refresh-token", tokens.data.refreshToken);
      return tokens.data;
    })
    .catch((error) => {
      console.log(error);
      console.log("catching");
      return null;
    });
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
