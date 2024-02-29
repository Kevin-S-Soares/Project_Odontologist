<script lang="ts">
  import { onMount } from "svelte";
  import Loading from "../svg/loading.svelte";

  export let clickSubmit: (event: any) => any = () => 0;
  export let message = "Submit";
  export let id: string;
  export let disabled: boolean;
  export let isLoading = false;

  onMount(() => {
    document.addEventListener("keypress", (event: any) => {
      if (event.which === 13) {
        const button = document.getElementById(id) as HTMLButtonElement;
        button.click();
      }
    });
  });
</script>

<button
  {id}
  {disabled}
  class="flex rounded-md border bg-gradient-to-b from-teal-500 to-emerald-500 p-2 font-semibold text-white transition-all hover:bg-emerald-500 disabled:cursor-not-allowed disabled:opacity-40"
  on:click={clickSubmit}
  ><span>{message}</span><span
    class="{isLoading ? 'inline' : 'hidden'} ml-2 place-self-end self-center"
    ><Loading /></span
  ></button
>
