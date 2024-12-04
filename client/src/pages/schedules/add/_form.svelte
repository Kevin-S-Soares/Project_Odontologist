<script lang="ts">
  import { Schedule } from "../../../models/schedule";
  import { Status } from "../../../models/enums";
  import { add } from "../../../models/APIAdapters/schedule/add";

  export let odontologistId: number;
  export let token: string;

  const schedule: Schedule = new Schedule();
  schedule.odontologistId = odontologistId;
  let status: Status = Status.NONE;
  const sendForm = async () => {
    await add(schedule, token);
    status = Status.SUCCESS;
    setTimeout(() => {
      window.location.replace(`/schedules?odontologistId=${odontologistId}`);
    }, 2000);
  };
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
    <label class="font-medium" for="StartDay"> Start day: </label>
    <select
      class="mt-2 rounded border-2 border-black"
      name="StartDay"
      bind:value={schedule.startDay}
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
      bind:value={schedule.startTime}
    />
  </div>
  <div class="mt-4 flex w-1/4 flex-col">
    <label class="font-medium" for="EndDay"> End day: </label>
    <select
      class="mt-2 rounded border-2 border-black"
      name="EndDay"
      bind:value={schedule.endDay}
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
    <label class="font-medium" for="StartTime"> End time: </label>
    <input
      class="mt-2 rounded border-2 border-black"
      name="EndTime"
      type="time"
      step="1"
      bind:value={schedule.endTime}
    />
  </div>

  <div>
    <button
      class="mt-4 rounded-md bg-teal-400 px-3 py-3 font-bold text-white transition-all hover:bg-teal-500"
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
