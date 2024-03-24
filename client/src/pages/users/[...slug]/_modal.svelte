<script lang="ts">
  import { remove } from "../../../models/APIAdapters/user/remove";
  import Trash from "../../../components/svg/trash.svelte";

  export let id: string;
  export let token: string;
  export let isDarkTheme: boolean;

  let isModalVisible = false;
  const showModal = () => {
    const modal = document.getElementById("modal") as HTMLDivElement;
    isModalVisible = !isModalVisible;
    if (isModalVisible) {
      window.onclick = (event: any) => {
        if (event.target == modal) {
          isModalVisible = false;
          window.onclick = null;
        }
      };
    }
  };
  const submit = async () => {
    await remove(id, token);
    window.location.replace("/users/");
  };
</script>

<button
  aria-label="delete user"
  class="relative h-10 w-10 rounded-full border bg-rose-600 transition-all hover:bg-rose-700 dark:border-neutral-900 dark:bg-rose-400 dark:hover:bg-rose-500"
  on:click={showModal}
  ><span class="absolute -translate-x-1/2 -translate-y-1/2"
    ><Trash color={isDarkTheme ? "#404040" : "#e5e7eb"} /></span
  ></button
>
<div
  id="modal"
  class="{isModalVisible
    ? 'block'
    : 'hidden'} fixed left-0 top-0 z-10 h-full w-full bg-gray-300 transition-all"
  style="background-color: rgba(0,0,0,0.4)"
>
  <div class="relative">
    <div
      class="absolute left-1/2 grid grid-rows-3 p-4 {isModalVisible
        ? '-translate-y-1/2 opacity-100'
        : '-translate-y-0 opacity-0'} top-96 h-[16rem] w-96 -translate-x-1/2 rounded-md bg-white transition-all duration-1000 dark:bg-neutral-700"
    >
      <div class="place-self-start justify-self-end">
        <button
          class="text-4xl dark:text-white"
          on:click={() => {
            isModalVisible = false;
            window.onclick = null;
          }}>&#215;</button
        >
      </div>
      <div class="">
        <p class="text-center text-lg dark:text-white">
          Are you sure that you want to delete this account?
        </p>
      </div>
      <div class="flex items-center justify-around">
        <button
          class="rounded-md border bg-rose-600 p-2 font-bold text-white transition-all hover:bg-rose-700 dark:border-neutral-900"
          on:click={submit}>Submit</button
        >
        <button
          class="rounded-md border p-2 font-medium transition-all hover:bg-gray-100 dark:border-neutral-900 dark:text-white dark:hover:bg-neutral-600"
          on:click={() => {
            isModalVisible = false;
            window.onclick = null;
          }}>Cancel</button
        >
      </div>
      <div></div>
      <div></div>
    </div>
  </div>
</div>
