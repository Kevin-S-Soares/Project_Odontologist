<script lang="ts">
  import { Odontologist } from "../../../models/odontologist";
  import { Status } from "../../../models/enums";
  import { add } from "../../../models/APIAdapters/odontologist/add";

  const odontologist: Odontologist = new Odontologist();
  let status: Status = Status.NONE;
  const sendForm = async () => {
    await add(odontologist);
    status = Status.SUCCESS;
    setTimeout(() => {
      window.location.replace("/odontologists/");
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
      bind:value={odontologist.name}
    />
  </div>
  <div class="mt-4 flex w-1/4 flex-col">
    <label class="font-medium" for="Phone"> Phone: </label>
    <input
      class="mt-2 rounded border-2 border-black"
      name="Phone"
      type="tel"
      bind:value={odontologist.phone}
    />
  </div>
  <div class="mt-4 flex w-1/4 flex-col">
    <label class="font-medium" for="Email"> Email: </label>
    <input
      class="mt-2 rounded border-2 border-black"
      name="Email"
      type="email"
      bind:value={odontologist.email}
    />
  </div>

  <div>
    <button
      class="mt-4 rounded-md bg-teal-400 px-3 py-3 font-bold text-white transition-all hover:bg-teal-500"
      on:click={sendForm}>Add odontologist</button
    >
  </div>
{/if}
{#if status === Status.SUCCESS}
  <div class="mt-4">
    <p class="text-center text-xl">Odontologist added successfully!</p>
    <p class="text-center text-xl">Returning to index.</p>
  </div>
{/if}
