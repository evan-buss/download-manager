<script lang="ts">
  import { fetchAllUsers, createUser } from "../services/users.service";
  import { onMount } from "svelte";
  import { user } from "../stores/auth.store";
  import { push } from "svelte-spa-router";

  export let name: string = "Evan";

  let newName: string;
  let userList: string[] = [];

  onMount(() => {
    loadUserList();
  });

  async function createUserHandler() {
    userList = await createUser(newName);
  }

  async function loadUserList() {
    userList = await fetchAllUsers();
  }

  function logout() {
    user.set(false);
    push("/login");
  }
</script>

<h1 class="font-mono text-4xl text-gray-300">Welcome home {name}</h1>

<div class="flex flex-row space-x-4">
  <input
    type="text"
    class="border border-gray-600 shadow-md"
    bind:value={newName} />
  <button
    class="rounded-md bg-gray-400 shadow-md p-2"
    on:click={createUserHandler}>
    Submit
  </button>
  <button
    class="rounded-lg bg-gray-200 p-4 border shadow-lg border-blue-400"
    on:click={logout}>
    Logout
  </button>
</div>

{#each userList as name}
  <h3 class="text-white">{name}</h3>
{/each}
