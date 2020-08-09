<script lang="ts">
  export let item: string;
</script>

<div
  class="flex bg-white shadow hover:shadow-md cursor-pointer rounded-lg mx-4
  md:mx-auto my-6">
  <div class="flex items-center justify-center px-4 py-4 w-full">
    <div class="w-full">
      <div class="flex items-center justify-between">
        <h2 class="text-lg font-semibold text-gray-900 -mt-1">{item}</h2>

        <p
          class="rounded-sm px-1 hover:shadow-md text-xs text-white bg-red-600">
          MEGA
        </p>
      </div>
      <p class="text-sm text-gray-700">22h ago</p>
      <div
        class="border border-teal-400 w-full bg-grey-light mt-3 rounded-full">
        <div
          class="bg-teal-400 text-sm leading-none py-1 text-center text-white
          rounded-full"
          style="width: 55%">
          55%
        </div>
      </div>
      <div />
    </div>
  </div>
</div>
