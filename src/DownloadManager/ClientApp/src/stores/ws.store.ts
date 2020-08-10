import { writable } from "svelte/store";
import * as signalR from "@microsoft/signalr";
import { BASE_URL } from "../services/http.service";

function createConnection() {
  console.log("create connection");
  const connection = new signalR.HubConnectionBuilder()
    .withUrl(BASE_URL + "api/hubs/downloads", {
      accessTokenFactory: () => {
        const token = localStorage.getItem("access-token");
        console.log(localStorage.getItem("access-token"));
        return token;
      },
    })
    .withAutomaticReconnect()
    .build();

  const { subscribe, set, update } = writable<signalR.HubConnection>(
    connection
  );

  function connect() {
    console.log("connecting");
    update((conn) => {
      conn.start().catch((err) => console.log(err));
      return conn;
    });
  }

  function disconnect() {
    console.log("disconnecting");
    update((conn) => {
      conn?.stop().catch((err) => console.log(err));
      return conn;
    });
  }

  return {
    subscribe,
    connect,
    disconnect,
  };
}
import type { format } from "path";

export const connection = createConnection();
