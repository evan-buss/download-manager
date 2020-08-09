<script lang="ts">
  import DownloadCard from "../components/DownloadCard.svelte";
  import { tryDecodeLink } from "../services/download-manager.service";
  import Navbar from "../components/Navbar.svelte";

  let downloads: string[] = [];
  let downloadLink: string;

  function onBlur() {
    console.log("blur");
    downloadLink = tryDecodeLink(downloadLink) ?? downloadLink;
  }

  function isValidHttpUrl(link: string) {
    let url: URL;

    try {
      url = new URL(link);
    } catch (_) {
      return false;
    }

    console.log(url);

    return (
      (url.protocol === "http:" || url.protocol === "https:") &&
      url.host === "mega.nz"
    );
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
