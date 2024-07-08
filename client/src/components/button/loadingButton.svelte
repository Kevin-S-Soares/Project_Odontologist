<script lang="ts">
  import Loading from "../svg/loading.svelte";

  export let clickSubmit: (event: any) => any = () => 0;
  export let message = "Submit";
  export let id: string;
  export let disabled: boolean;
  export let isLoading = false;
</script>

<button
  {id}
  {disabled}
  class="mt-4 flex w-full justify-center rounded-md border bg-gradient-to-b from-teal-500 to-emerald-500 p-2 font-semibold text-white transition-all hover:bg-emerald-500 disabled:cursor-not-allowed disabled:opacity-40 dark:border dark:border-neutral-900"
  on:click={clickSubmit}
  ><span>{message}</span><span
    class="{isLoading ? 'inline' : 'hidden'} ml-2 place-self-end self-center"
    ><Loading /></span
  ></button
>
