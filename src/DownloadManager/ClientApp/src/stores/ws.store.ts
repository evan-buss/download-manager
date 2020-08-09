import { writable } from "svelte/store";
import * as signalR from "@microsoft/signalr";
import { BASE_URL } from "../services/http.service";

function createConnection() {
  console.log("create connection");
  const connection = new signalR.HubConnectionBuilder()
    .withUrl(BASE_URL + "api/hubs/downloads")
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
    console.log("disocnnecting");
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

export const connection = createConnection();
