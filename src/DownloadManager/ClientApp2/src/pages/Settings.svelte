<script lang="ts">
  import Navbar from "../components/Navbar.svelte";
  import { onMount } from "svelte";
  import { connection } from "../stores/ws.store";

  onMount(() => {
    $connection.on("progress", (val: number) => {
      console.log("Progress", val);
    });
  });
</script>

<Navbar />
<main class="mt-20">
  <h2>Hello world</h2>
</main>
