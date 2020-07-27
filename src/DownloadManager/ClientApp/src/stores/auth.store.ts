import { writable } from "svelte/store";

export interface User {
  accessToken: string;
  refreshToken: string;
}

function createCurrentUser() {
  const { subscribe, set, update } = writable<User>({
    accessToken: null,
    refreshToken: null,
  });

  return {
    subscribe,
    login: () => {
      update((user) => {
        user.accessToken = "token";
        user.refreshToken = "refreshToken";
        return user;
      });
    },
    logout: () => {
      update((user) => {
        user = null;
        return user;
      });
    },
  };
}

async function login(): Promise<boolean> {
  await fetch();
}

export const user = createCurrentUser();
