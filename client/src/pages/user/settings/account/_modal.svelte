<script lang="ts">
  import { remove } from "../../../../models/APIAdapters/user/remove";
  export let id: string;
  export let token: string;

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
    const result = await remove(id, token);
    if (result) {
      document.cookie = "Authorization=; Max-age=0; Path=/;";
      window.location.replace("/");
    } else {
      console.log("ops");
    }
  };
</script>

<button
  class="mt-2 line-clamp-1 inline-block text-lg text-rose-600 transition-all hover:underline dark:text-rose-400"
  data-astro-prefetch="hover"
  on:click={showModal}
>
  Delete Account
</button>
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
          Are you sure that you want to delete your account?
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
