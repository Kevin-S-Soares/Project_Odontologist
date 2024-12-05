<script lang="ts">
  import { BreakTime } from "../../models/break_time";
  import { DateHandler } from "../../models/date_handler";
  import { remove } from "../../models/APIAdapters/break_time/remove";
  export let breakTimes: BreakTime[];
  export let token: string;
  export let scheduleId: string;

  const getRow = (arg: number) => {
    let rows = "";
    if (arg === 0) {
      rows = "row-start-2 row-end-3 ";
    }
    if (arg === 1) {
      rows = "row-start-3 row-end-4 ";
    }
    if (arg === 2) {
      rows = "row-start-4 row-end-5 ";
    }
    if (arg === 3) {
      rows = "row-start-5 row-end-6 ";
    }
    if (arg === 4) {
      rows = "row-start-6 row-end-7 ";
    }
    return rows;
  };

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
    await remove(breakTimeToDelete.id, token);
    window.location.replace(`/break_times?scheduleId=${scheduleId}`);
  };
  let isModalVisible = false;
  let breakTimeToDelete = new BreakTime();
</script>

<a
  class="mt-4 line-clamp-1 inline-block cursor-pointer rounded-md bg-teal-400 px-1 py-3 text-center font-bold text-white transition-all hover:bg-teal-500 md:w-1/6 dark:border dark:border-neutral-900"
  href="/break_times/add?scheduleId={scheduleId}">Add break time</a
>

{#if breakTimes.length > 0}
  <div
    id="modal"
    style="background-color: rgba(0,0,0,0.4)"
    class="{isModalVisible
      ? 'block'
      : 'hidden'} fixed left-0 top-0 z-10 h-full w-full bg-gray-300"
  >
    <div class="relative">
      <div
        class="absolute left-1/2 top-1/2 grid h-[16rem] w-96 -translate-x-1/2 translate-y-1/2 grid-rows-3 rounded-md bg-white p-4"
      >
        <button
          class="place-self-start justify-self-end text-4xl dark:text-white"
          on:click={() => {
            isModalVisible = false;
            window.onclick = null;
          }}>&#215;</button
        >
        <div>
          <p class="text-center text-lg">
            Are you sure that you want to delete {breakTimeToDelete.name}?
          </p>
        </div>
        <div class="flex items-center justify-around">
          <button
            on:click={submit}
            class="rounded-md border bg-rose-600 p-2 font-bold text-white transition-all hover:bg-rose-700 dark:border-neutral-900"
            >Submit</button
          >
          <button
            class="rounded-md border p-2 font-medium transition-all hover:bg-gray-100 dark:border-neutral-900 dark:hover:bg-neutral-600"
            on:click={() => {
              isModalVisible = false;
              window.onclick = null;
            }}>Cancel</button
          >
        </div>
      </div>
    </div>
  </div>
  <div
    class="grid-rows-auto mt-4 grid w-full grid-cols-[10rem_10rem_10rem_10rem_10rem] overflow-x-scroll rounded-md border p-4 md:grid-cols-[repeat(6,_1fr)] dark:border-neutral-900"
  >
    <div class="col-start-1 col-end-2 row-start-1 row-end-2">
      <p class="text-center font-medium dark:text-white">Name</p>
    </div>
    <div class="col-start-2 col-end-3 row-start-1 row-end-2">
      <p class="text-center font-medium dark:text-white">Start</p>
    </div>
    <div class="col-start-3 col-end-4 row-start-1 row-end-2">
      <p class="text-center font-medium dark:text-white">End</p>
    </div>
    <div class="col-start-4 col-end-6 row-start-1 row-end-2">
      <p class="text-center font-medium dark:text-white">Actions</p>
    </div>

    {#each breakTimes as item, index}
      <div class={getRow(index) + "col-start-1 col-end-2"}>
        <p class="block text-center dark:text-white">{item.name}</p>
      </div>
      <div class={getRow(index) + "col-start-2 col-end-3"}>
        <p class="block text-center dark:text-white">
          {DateHandler.getDayOfTheWeek(item.startDay) + " - " + item.startTime}
        </p>
      </div>
      <div class={getRow(index) + "col-start-3 col-end-4"}>
        <p class="block text-center dark:text-white">
          {DateHandler.getDayOfTheWeek(item.endDay) + " - " + item.endTime}
        </p>
      </div>
      <div class={getRow(index) + "col-start-4 col-end-5 justify-self-center"}>
        <a
          href={`/break_times/edit/${item.id}`}
          class="cursor-pointer text-center hover:underline dark:text-white"
        >
          edit
        </a>
      </div>
      <div
        class={getRow(index) +
          "col-start-5 col-end-6 justify-self-center hover:underline"}
      >
        <button
          on:click={() => {
            breakTimeToDelete = item;
            showModal();
          }}
          class="cursor-pointer text-center dark:text-white">delete</button
        >
      </div>
    {/each}
  </div>
{:else}
  <div class="mt-4">
    <p class="text-center text-3xl dark:text-white">
      No break times registered.
    </p>
  </div>
{/if}
