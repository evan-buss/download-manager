<script lang="ts">
  import DownloadCard from "../components/DownloadCard.svelte";
  import { tryDecodeLink } from "../services/download-manager.service";
  import Navbar from "../components/Navbar.svelte";

  let items: string[] = ["test", "test2"];
  let downloadLink: string;

  function onBlur() {
    downloadLink = tryDecodeLink(downloadLink);
  }
</script>

<Navbar />
<div class="flex justify-center w-screen mt-20">
  <main class="flex-col items-center w-1/2">
    <div class="flex flex-row justify-center">
      <input
        class="w-full mt-2 py-2 px-4 bg-white text-gray-700 rounded
        appearance-none placeholder-gray-500 focus:outline-none focus:bg-white
        mr-4 border-2 border-black hover:shadow-sm"
        type="text"
        bind:value={downloadLink}
        on:blur={onBlur}
        placeholder="Download Link" />
      <button
        class="mt-2 py-2 px-4 bg-white text-gray-700 rounded block
        appearance-none placeholder-gray-500 border-2 border-teal-300
        focus:outline-none hover:bg-teal-400 hover:text-white">
        Submit
      </button>
    </div>
    <div class="mx-auto">
      {#each items as item}
        <DownloadCard {item} />
      {:else}
        <p class="text-white">No downloads.</p>
      {/each}
    </div>
  </main>
</div>
