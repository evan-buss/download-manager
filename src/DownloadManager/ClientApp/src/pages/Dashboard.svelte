<script lang="ts">
  import DownloadCard from "../components/DownloadCard.svelte";
  import {
    tryDecodeLink,
    isValidHttpUrl,
  } from "../services/download-manager.service";
  import type { Download } from "../services/download-manager.service";

  import Navbar from "../components/Navbar.svelte";
  import { connection } from "../stores/ws.store";
  import { onMount, onDestroy } from "svelte";

  let downloads: Download[] = [];
  let downloadLink: string = "";

  function progressHandler(val: Download) {
    if (downloads.some((x) => x.fileName === val.fileName)) {
      const index = downloads.findIndex((x) => x.fileName === val.fileName);
      downloads = [
        ...downloads.slice(0, index),
        val,
        ...downloads.slice(index + 1),
      ];
    } else {
      console.log("sorting");
      downloads = [...downloads, val];
      downloads = downloads.sort((a: Download, b: Download) => {
        return a.startDate.getTime() - b.startDate.getTime();
      });
    }
  }

  onMount(() => {
    console.log("adding handler");
    $connection.on("progress", progressHandler);
  });

  onDestroy(() => {
    console.log("removing handler");
    $connection.off("progress", progressHandler);
  });

  function onBlur() {
    console.log("blur");
    downloadLink = tryDecodeLink(downloadLink) ?? downloadLink;
  }

  $: disabled = !isValidHttpUrl(downloadLink);
</script>

<Navbar />
<div class="flex justify-center w-screen mt-20">
  <main class="flex-col items-center w-full mx-4 lg:w-1/2">
    <form
      class="flex flex-row justify-center"
      on:submit={() => console.log('submitted')}>
      <input
        class="w-full mt-2 py-2 px-4 bg-white text-gray-700 rounded
        appearance-none placeholder-gray-500 focus:outline-none focus:bg-white
        mr-4 border-2 border-black hover:shadow-sm"
        type="text"
        bind:value={downloadLink}
        on:blur={onBlur}
        placeholder="Download Link" />
      <button type="submit" class="btn" {disabled}>Download</button>
    </form>
    <div class="mx-auto">
      {#each downloads as download}
        <DownloadCard {download} />
      {:else}
        <h4 class="text-center p-12 text-xl font-light">
          No downloads in progress...
        </h4>
      {/each}
    </div>
  </main>
</div>
