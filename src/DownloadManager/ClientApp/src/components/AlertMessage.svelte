<script lang="ts">
  import { fade } from "svelte/transition";
  import { onMount } from "svelte";

  export let message: string = "";
  export let type: string = "";

  let tag: string;
  let bgColor: string;

  onMount(() => {
    switch (type) {
      case "error":
        tag = "Error";
        bgColor = "bg-red-500";
        break;
      case "warning":
        tag = "Warning";
        bgColor = "bg-yellow-400";
        break;
      default:
        tag = "Alert";
        bgColor = "bg-gray-400";
        break;
    }
  });
</script>

<div transition:fade class="p-2">
  <div
    class="inline-flex items-center bg-white leading-none rounded-full p-2
    shadow text-gray-700 text-sm">
    <span
      class="inline-flex {bgColor} text-white rounded-full h-6 px-3
      justify-center items-center">
      Error
    </span>
    <span class="inline-flex px-2">{message}</span>
  </div>
</div>
