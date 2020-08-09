<script lang="ts">
  import ProfileMenu from "./ProfileMenu.svelte";
  import { link } from "svelte-spa-router";
  import active from "svelte-spa-router/active";
</script>

<style>
  :global(a.active) {
    color: #81e6d9;
  }
</style>

<header class="h-16 fixed top-0 bg-black w-screen">
  <div
    class="mx-4 lg:mx-auto lg:w-8/12 h-full flex items-center justify-between">
    <!-- <h1 class="text-2xl text-gray-400">Download Manager</h1> -->
    <a href="/" use:link use:active class="text-white">
      <svg viewBox="0 0 20 20" fill="currentColor" class="w-8 h-8">
        <path
          fill-rule="evenodd"
          d="M3 17a1 1 0 011-1h12a1 1 0 110 2H4a1 1 0 01-1-1zm3.293-7.707a1 1 0
          011.414 0L9 10.586V3a1 1 0 112 0v7.586l1.293-1.293a1 1 0 111.414
          1.414l-3 3a1 1 0 01-1.414 0l-3-3a1 1 0 010-1.414z"
          clip-rule="evenodd" />
      </svg>
    </a>
    <ProfileMenu />
  </div>
</header>
