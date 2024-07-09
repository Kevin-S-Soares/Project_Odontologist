<script lang="ts">
  export let bg_color = "bg-gray-50 dark:bg-neutral-700";
</script>

<div
  class="w-11/12 rounded {bg_color} p-4 text-lg sm:w-1/2 md:w-6/12 lg:w-3/12"
>
  <slot />
</div>
