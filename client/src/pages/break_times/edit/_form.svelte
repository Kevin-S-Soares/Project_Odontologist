<script lang="ts">
  import { BreakTime } from "../../../models/break_time";
  import { Status } from "../../../models/enums";
  import { edit } from "../../../models/APIAdapters/break_time/edit";

  export let breakTime: BreakTime;
  export let token: string;
  let status: Status = Status.NONE;
  const sendForm = async () => {
    await edit(breakTime, token);
    status = Status.SUCCESS;
    setTimeout(() => {
      window.location.replace(
        `/break_times?scheduleId=${breakTime.scheduleId}`,
      );
    }, 2000);
  };
  let name = breakTime.schedule === undefined ? "" : breakTime.schedule.name;
</script>

{#if status === Status.NONE}
  <div class="flex w-1/4 flex-col">
    <label class="font-medium" for="ScheduleName"> Schedule Name: </label>
    <input
      class="mt-2 rounded border-2 border-black bg-gray-200"
      name="ScheduleName"
      type="text"
      disabled={true}
      bind:value={name}
    />
  </div>
  <div class="mt-4 flex w-1/4 flex-col">
    <label class="font-medium" for="Name"> Name: </label>
    <input
      class="mt-2 rounded border-2 border-black"
      name="Name"
      type="text"
      bind:value={breakTime.name}
    />
  </div>
  <div class="mt-4 flex w-1/4 flex-col">
    <label class="font-medium" for="StartDay"> Start day: </label>
    <select
      class="mt-2 rounded border-2 border-black bg-white"
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
    <label class="font-medium" for="StartTime"> Start time: </label>
    <input
      class="mt-2 rounded border-2 border-black"
      name="StartTime"
      type="time"
      step="1"
      bind:value={breakTime.startTime}
    />
  </div>
  <div class="mt-4 flex w-1/4 flex-col">
    <label class="font-medium" for="EndDay"> End day: </label>
    <select
      class="mt-2 rounded border-2 border-black bg-white"
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
    <label class="font-medium" for="EndTime"> End time: </label>
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
      on:click={sendForm}>Edit break time</button
    >
  </div>
{/if}
{#if status === Status.SUCCESS}
  <div class="mt-4">
    <p class="text-center text-xl">Break time edited successfully!</p>
    <p class="text-center text-xl">Returning to index.</p>
  </div>
{/if}
