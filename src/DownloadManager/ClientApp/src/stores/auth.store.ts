import { writable, Writable } from "svelte/store";

export interface User {
  username: string;
  isLoggedIn: boolean;
}

export const user = writable<boolean>(false);

// function createCurrentUser() {
//   const { subscribe, set, update } = writable<User>({
//     username: null,
//     isLoggedIn: false,
//   });

//   return {
//     subscribe,
//     login: () => {
//       update((user) => {
//         user.isLoggedIn = true;
//         return user;
//       });
//     },
//     logout: () => {
//       update((user) => {
//         user.isLoggedIn = false;
//         return user;
//       });
//     },
//     reset: () => {
//       set({ isLoggedIn: false, username: null });
//     },
//   };
// }

// export const user = createCurrentUser();
