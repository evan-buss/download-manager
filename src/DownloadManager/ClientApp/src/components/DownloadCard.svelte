<script lang="ts">
  import type { Download } from "../services/download-manager.service";
  import moment from "moment";

  import { tweened, spring } from "svelte/motion";
  import { cubicOut } from "svelte/easing";
  export let download: Download;

  const progress = tweened(0, {
    duration: 200,
    easing: cubicOut,
  });
  // const progress = spring(0, {
  //   stiffness: 0.1,
  //   damping: 0.8,
  // });

  $: progress.set(download.progress);
</script>

<style>
  .stripes div {
    background-size: 30px 30px;
    background-image: linear-gradient(
      135deg,
      rgba(255, 255, 255, 0.15) 25%,
      transparent 25%,
      transparent 50%,
      rgba(255, 255, 255, 0.15) 50%,
      rgba(255, 255, 255, 0.15) 75%,
      transparent 75%,
      transparent
    );
    animation: animate-stripes 3s linear infinite;
  }

  @keyframes animate-stripes {
    0% {
      background-position: 0 0;
    }
    100% {
      background-position: 60px 0;
    }
  }
</style>

<div
  class="flex bg-white shadow hover:shadow-md cursor-pointer rounded-lg
  md:mx-auto my-6">
  <div class="flex items-center justify-center px-4 py-4 w-full">
    <div class="w-full">
      <div class="flex items-center justify-between">
        <h2 class="text-lg font-semibold text-gray-900 mt-1">
          {download.fileName}
        </h2>
        <p
          class="rounded-sm px-1 hover:shadow-md text-xs text-white bg-red-600">
          {download.type.toUpperCase()}
        </p>
      </div>
      <p class="text-sm text-gray-700">
        {moment(download.startDate).fromNow()}
      </p>
      <div
        class="shadow-sm w-full bg-gray-100 mt-3 rounded-full stripes"
        role="progressbar"
        aria-valuemin="0"
        aria-valuemax="100"
        aria-valuenow={$progress}>
        <div
          class="bg-teal-400 text-sm leading-none py-1 text-center text-white
          rounded-full"
          style="width: {$progress}%">
          {Math.floor($progress)}%
        </div>
      </div>
    </div>
  </div>
</div>
