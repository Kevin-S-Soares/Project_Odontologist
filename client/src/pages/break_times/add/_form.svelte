<script lang="ts">
  import { BreakTime } from "../../../models/break_time";
  import { Status } from "../../../models/enums";
  import { add } from "../../../models/APIAdapters/break_time/add";

  export let scheduleId: number;
  export let token: string;

  const breakTime: BreakTime = new BreakTime();
  breakTime.scheduleId = scheduleId;
  let status: Status = Status.NONE;
  const sendForm = async () => {
    await add(breakTime, token);
    status = Status.SUCCESS;
    setTimeout(() => {
      window.location.replace(`/break_times?scheduleId=${scheduleId}`);
    }, 2000);
  };
</script>

{#if status === Status.NONE}
  <div class="flex w-1/4 flex-col">
    <label class="font-medium dark:text-white" for="Name"> Name: </label>
    <input
      class="mt-2 rounded border-2 border-black"
      name="Name"
      type="text"
      bind:value={breakTime.name}
    />
  </div>
  <div class="mt-4 flex w-1/4 flex-col">
    <label class="font-medium dark:text-white" for="StartDay">
      Start day:
    </label>
    <select
      class="mt-2 rounded border-2 border-black"
      name="StartDay"
      bind:value={breakTime.startDay}
    >
      <option value={0}>Sunday</option>
      <option value={1}>Monday</option>
      <option value={2}>Tuesday</option>
      <option value={3}>Wednesday</option>
      <option value={4}>Thursday</option>
      <option value={5}>Friday</option>
      <option value={6}>Saturday</option>
    </select>
  </div>
  <div class="mt-4 flex w-1/4 flex-col">
    <label class="font-medium dark:text-white" for="StartTime">
      Start time:
    </label>
    <input
      class="mt-2 rounded border-2 border-black"
      name="StartTime"
      type="time"
      step="1"
      bind:value={breakTime.startTime}
    />
  </div>
  <div class="mt-4 flex w-1/4 flex-col">
    <label class="font-medium dark:text-white" for="EndDay"> End day: </label>
    <select
      class="mt-2 rounded border-2 border-black"
      name="EndDay"
      bind:value={breakTime.endDay}
    >
      <option value={0}>Sunday</option>
      <option value={1}>Monday</option>
      <option value={2}>Tuesday</option>
      <option value={3}>Wednesday</option>
      <option value={4}>Thursday</option>
      <option value={5}>Friday</option>
      <option value={6}>Saturday</option>
    </select>
  </div>
  <div class="mt-4 flex w-1/4 flex-col">
    <label class="font-medium dark:text-white" for="EndTime"> End time: </label>
    <input
      class="mt-2 rounded border-2 border-black"
      name="EndTime"
      type="time"
      step="1"
      bind:value={breakTime.endTime}
    />
  </div>

  <div>
    <button
      class="mt-4 rounded-md bg-teal-400 px-3 py-3 font-bold text-white transition-all hover:bg-teal-500"
      on:click={sendForm}>Add break time</button
    >
  </div>
{/if}
{#if status === Status.SUCCESS}
  <div class="mt-4">
    <p class="text-center text-xl dark:text-white">
      Break time added successfully!
    </p>
    <p class="text-center text-xl dark:text-white">Returning to index.</p>
  </div>
{/if}
