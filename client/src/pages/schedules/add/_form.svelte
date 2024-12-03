<script lang="ts">
  import { Schedule } from "../../../models/schedule";
  import { Status } from "../../../models/enums";
  import { add } from "../../../models/APIAdapters/schedule/add"


  const schedule: Schedule = new Schedule();
  let status: Status = Status.NONE;
  const sendForm = async () => {
    await add(schedule);
    status = Status.SUCCESS;
    setTimeout(() => {
      window.location.replace("/schedules/")
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
      bind:value={schedule.name}
    />
  </div>
  <div class="mt-4 flex w-1/4 flex-col">
    <label class="font-medium"  for="StartDay"> Start day: </label>
    <input
      class="mt-2 rounded border-2 border-black"
      name="StartDay"
      type="number"
      bind:value={schedule.startDay}
    />
  </div>
  <div class="mt-4 flex w-1/4 flex-col">
    <label class="font-medium"  for="StartTime"> Start day: </label>
    <input
      class="mt-2 rounded border-2 border-black"
      name="StartTime"
      type="time"
      bind:value={schedule.startTime}
    />
  </div>
  <div class="mt-4 flex w-1/4 flex-col">
    <label class="font-medium"  for="StartDay"> End day: </label>
    <input
      class="mt-2 rounded border-2 border-black"
      name="EndDay"
      type="number"
      bind:value={schedule.endDay}
    />
  </div>
  <div class="mt-4 flex w-1/4 flex-col">
    <label class="font-medium"  for="StartTime"> End time: </label>
    <input
      class="mt-2 rounded border-2 border-black"
      name="EndTime"
      type="time"
      bind:value={schedule.endTime}
    />
  </div>
  
  <div>
    <button class="mt-4 py-3 px-3 text-white font-bold bg-teal-400 rounded-md hover:bg-teal-500 transition-all"
      on:click={sendForm}>Add schedule</button
    >
  </div>
  
{/if}
{#if status === Status.SUCCESS}
<div class="mt-4">
  <p class="text-center text-xl">Schedule added successfully!</p>
  <p class="text-center text-xl">Returning to index.</p>
</div>
{/if}
