<script lang="ts">
  import { BreakTime } from "../../../models/break_time";
  import { Status } from "../../../models/enums";
  import { edit } from "../../../models/APIAdapters/break_time/edit"


  const breakTime: BreakTime = new BreakTime();
  let status: Status = Status.NONE;
  const sendForm = async () => {
    await edit(breakTime);
    status = Status.SUCCESS;
    setTimeout(() => {
      window.location.replace("/break_times/")
    }, 2000)
  }
</script>

{#if status === Status.NONE}
<div class="flex w-1/4 flex-col">
    <label class="font-medium" for="Name"> Name: </label>
    <input
      class="mt-2 rounded border-2 border-black"
      name="Name"
      type="text"
      bind:value={breakTime.name}
    />
  </div>
  <div class="mt-4 flex w-1/4 flex-col">
    <label class="font-medium"  for="StartDay"> Start day: </label>
    <input
      class="mt-2 rounded border-2 border-black"
      name="StartDay"
      type="number"
      bind:value={breakTime.startDay}
    />
  </div>
  <div class="mt-4 flex w-1/4 flex-col">
    <label class="font-medium"  for="StartTime"> Start day: </label>
    <input
      class="mt-2 rounded border-2 border-black"
      name="StartTime"
      type="time"
      bind:value={breakTime.startTime}
    />
  </div>
  <div class="mt-4 flex w-1/4 flex-col">
    <label class="font-medium"  for="StartDay"> End day: </label>
    <input
      class="mt-2 rounded border-2 border-black"
      name="EndDay"
      type="number"
      bind:value={breakTime.endDay}
    />
  </div>
  <div class="mt-4 flex w-1/4 flex-col">
    <label class="font-medium"  for="StartTime"> End time: </label>
    <input
      class="mt-2 rounded border-2 border-black"
      name="EndTime"
      type="time"
      bind:value={breakTime.endTime}
    />
  </div>
  
  <div>
    <button class="mt-4 py-3 px-3 text-white font-bold bg-teal-400 rounded-md hover:bg-teal-500 transition-all"
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
