<script lang="ts">
  import { Appointment } from "../../../models/appointment";
  import { Status } from "../../../models/enums";
  import { edit } from "../../../models/APIAdapters/appointment/edit";
  import type { Odontologist } from "../../../models/odontologist";
  import type { Schedule } from "../../../models/schedule";

  export let appointment: Appointment;
  export let token: string;
  export let odontologists: Odontologist[];
  export let schedules: Schedule[];

  let filteredSchedules: Schedule[] = schedules.filter(
    (item) => item.odontologistId === odontologists[0].id,
  );

  let status: Status = Status.NONE;
  const sendForm = async () => {
    await edit(appointment, token);
    status = Status.SUCCESS;
    setTimeout(() => {
      window.location.replace(`/appointments`);
    }, 2000);
  };
  const changeOdontologist = () => {
    const selectNode = document.getElementById(
      "odontologist_name",
    ) as HTMLSelectElement;
    const aux = parseInt(selectNode.value);
    filteredSchedules = schedules.filter((item) => item.odontologistId === aux);
  };
  let aux =
    appointment.schedule === undefined
      ? 0
      : appointment.schedule.odontologistId;
</script>

{#if status === Status.NONE}
  <div class="flex w-48 flex-col lg:w-1/4">
    <label class="font-medium dark:text-white" for="OdontologistName">
      Odontologist name:
    </label>
    <select
      id="odontologist_name"
      class="mt-2 rounded border-2 border-black bg-white"
      bind:value={aux}
      on:change={changeOdontologist}
    >
      {#each odontologists as item}
        <option value={item.id}>{item.name}</option>
      {/each}
    </select>
  </div>
  <div class="mt-4 flex w-48 flex-col lg:w-1/4">
    <label class="font-medium dark:text-white" for="ScheduleName">
      Schedule name:
    </label>
    <select
      class="mt-2 rounded border-2 border-black bg-white"
      bind:value={appointment.scheduleId}
    >
      {#each filteredSchedules as item}
        <option value={item.id}>{item.name}</option>
      {/each}
    </select>
  </div>
  <div class="mt-4 flex w-48 flex-col lg:w-1/4">
    <label class="font-medium dark:text-white" for="Start"> Start: </label>
    <input
      class="mt-2 rounded border-2 border-black"
      name="Start"
      type="datetime-local"
      step="1"
      bind:value={appointment.start}
    />
  </div>
  <div class="mt-4 flex w-48 flex-col lg:w-1/4">
    <label class="font-medium dark:text-white" for="End"> End: </label>
    <input
      class="mt-2 rounded border-2 border-black"
      name="End"
      type="datetime-local"
      step="1"
      bind:value={appointment.end}
    />
  </div>
  <div class="mt-4 flex w-48 flex-col lg:w-1/4">
    <label class="font-medium dark:text-white" for="PatientName">
      Patient name:
    </label>
    <input
      class="mt-2 rounded border-2 border-black"
      name="PatientName"
      type="text"
      bind:value={appointment.patientName}
    />
  </div>
  <div class="mt-4 flex w-48 flex-col lg:w-1/4">
    <label class="font-medium dark:text-white" for="Description">
      Description:
    </label>
    <input
      class="mt-2 rounded border-2 border-black"
      name="Description"
      type="text"
      bind:value={appointment.description}
    />
  </div>
  <div>
    <button
      class="mt-4 rounded-md bg-teal-400 px-3 py-3 font-bold text-white transition-all hover:bg-teal-500"
      on:click={sendForm}>Edit appointment</button
    >
  </div>
{/if}
{#if status === Status.SUCCESS}
  <div class="mt-4">
    <p class="text-center text-xl dark:text-white">
      Appointment edited successfully!
    </p>
    <p class="text-center text-xl dark:text-white">Returning to index.</p>
  </div>
{/if}
