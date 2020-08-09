<script lang="ts">
  export let disabled: boolean = false;
</script>

<style>

</style>

<button {disabled} class="btn">
  <slot />
</button>
