<script lang="ts">
  import TextInput from "../../../../../components/input/textInput.svelte";
  import FormPanel from "../../../../../components/panel/formPanel.svelte";
  import SubmitButton from "../../../../../components/button/submitButton.svelte";
  import { Role, Status } from "../../../../../models/enums";
  import { update } from "../../../../../models/APIAdapters/user/update";
  import { errorText } from "../../../../../components/text/errorText";

  export let id: string;
  export let name: string;
  export let role: Role;
  export let token: string;

  let isLoading = false;
  let status = Status.WAITING;
  const form = {
    id: id,
    name: name,
    invalidName: false,
    setInvalidName: (arg: boolean) => {
      form.invalidName = arg;
    },

    role: role,
    invalidRole: false,
    setInvalidRole: (arg: boolean) => {
      form.invalidRole = arg;
    },
  };
  const updateUser = async () => {
    isLoading = true;
    const updated = await update(form, token);
    status = updated ? Status.SUCCESS : Status.ERROR;
    isLoading = false;
  };
</script>

<FormPanel bg_color="white">
  {#if status === Status.WAITING}
    <TextInput id="name" label="Name" bind:value={form.name} />
    <div class="mt-2 flex flex-col">
      <label for="role" class="font-medium dark:text-white">Role</label>
      <select
        bind:value={form.role}
        id="role"
        name="role"
        class="mt-2 rounded-md border-2 bg-white p-2 dark:border-neutral-900"
      >
        <option value={Role.ODONTOLOGIST}>Odontologist</option>
        <option value={Role.ATTENDANT}>Attendant</option>
        <option value={Role.VISITOR}>Visitor</option>
      </select>
    </div>

    <div class="mt-2">
      <SubmitButton
        id="submit"
        clickSubmit={updateUser}
        disabled={form.invalidName || isLoading}
      />
    </div>
  {/if}

  {#if status === Status.ERROR}
    <div>
      <p class={errorText}>Something went wrong while updating user.</p>
    </div>
  {/if}
  {#if status === Status.SUCCESS}
    <div class="pl-6">
      <p class="text-lg dark:text-white">Details updated.</p>
    </div>
  {/if}
</FormPanel>
