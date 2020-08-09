<script lang="ts">
  import { is_promise, onDestroy, onMount } from "svelte/internal";

  let isOpen = false;

  function handleEscape(e: KeyboardEvent) {
    if (e.key === "Esc" || e.key === "Escape") {
      isOpen = false;
    }
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
      <p class="text-gray-700 px-4 py-2 hover:bg-teal-300">Link1</p>
      <p class="text-gray-700 px-4 py-2 hover:bg-teal-300">Link2</p>
    </div>
  {/if}

</div>
