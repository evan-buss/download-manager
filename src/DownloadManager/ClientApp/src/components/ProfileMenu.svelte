<script lang="ts">
  import { onDestroy, onMount } from "svelte/internal";
  import { user } from "../stores/auth.store";
  import { replace } from "svelte-spa-router";

  let isOpen = false;

  function handleEscape(e: KeyboardEvent): void {
    if (e.key === "Esc" || e.key === "Escape") {
      isOpen = false;
    }
  }

  function logOut(): void {
    user.logout();
    replace("/login");
  }
  onMount(() => {
    document.addEventListener("keydown", handleEscape);
  });

  onDestroy(() => {
    document.removeEventListener("keydown", handleEscape);
  });
</script>

<div class="relative mt-2">
  <button class="relative z-10" on:click={() => (isOpen = !isOpen)}>
    <svg
      class="h-8 w-8 text-white"
      xmlns="http://www.w3.org/2000/svg"
      fill="none"
      viewBox="0 0 24 24"
      stroke="currentColor">
      <path
        stroke-linecap="round"
        stroke-linejoin="round"
        stroke-width="2"
        d="M5.121 17.804A13.937 13.937 0 0112 16c2.5 0 4.847.655 6.879 1.804M15
        10a3 3 0 11-6 0 3 3 0 016 0zm6 2a9 9 0 11-18 0 9 9 0 0118 0z" />
    </svg>
  </button>
  {#if isOpen}
    <div
      on:click={() => (isOpen = false)}
      class="fixed w-screen h-screen inset-0 bg-black opacity-25" />
    <div class="absolute right-0 py-2 w-40 bg-white shadow-lg rounded-lg">
      <!-- <p class="text-gray-700 px-4 py-2 hover:bg-teal-300">Link1</p> -->

      <div class="text-gray-700 px-4 py-2 hover:bg-teal-300 cursor-pointer">
        <svg
          viewBox="0 0 20 20"
          fill="currentColor"
          class="inline w-6 h-6 mr-4">
          <path
            d="M5 4a1 1 0 00-2 0v7.268a2 2 0 000 3.464V16a1 1 0 102 0v-1.268a2 2
            0 000-3.464V4zM11 4a1 1 0 10-2 0v1.268a2 2 0 000 3.464V16a1 1 0 102
            0V8.732a2 2 0 000-3.464V4zM16 3a1 1 0 011 1v7.268a2 2 0 010
            3.464V16a1 1 0 11-2 0v-1.268a2 2 0 010-3.464V4a1 1 0 011-1z" />
        </svg>
        Settings
      </div>
      <div
        class="text-gray-700 px-4 py-2 hover:bg-teal-300 cursor-pointer"
        on:click={logOut}>
        <svg
          viewBox="0 0 20 20"
          fill="currentColor"
          class="inline w-6 h-6 mr-4">
          <path
            fill-rule="evenodd"
            d="M3 3a1 1 0 00-1 1v12a1 1 0 102 0V4a1 1 0 00-1-1zm10.293 9.293a1 1
            0 001.414 1.414l3-3a1 1 0 000-1.414l-3-3a1 1 0 10-1.414 1.414L14.586
            9H7a1 1 0 100 2h7.586l-1.293 1.293z"
            clip-rule="evenodd" />
        </svg>
        Log Out
      </div>
    </div>
  {/if}

</div>
