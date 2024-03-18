<script lang="ts">
  import FormPanel from "../../../components/panel/formPanel.svelte";
  import TextInput from "../../../components/input/textInput.svelte";
  import EmailInput from "../../../components/input/emailInput.svelte";
  import ConfirmPasswordInputs from "../../../components/input/confirmPassword.svelte";
  import SubmitButton from "../../../components/button/submitButton.svelte";
  import { register } from "../../../models/APIAdapters/user/register";
  import { Status } from "../../../models/enums";

  const form = {
    name: "",
    invalidName: true,
    setInvalidName: (arg: boolean) => {
      form.invalidName = arg;
    },

    email: "",
    invalidEmail: true,
    setInvalidEmail: (arg: boolean) => {
      form.invalidEmail = arg;
    },

    password: "",
    invalidPassword: true,
    setInvalidPassword: (arg: boolean) => {
      form.invalidPassword = arg;
    },
  };

  let status = Status.NONE;
  let isLoading = false;
  const clickSubmit = async () => {
    isLoading = true;
    const response = await register(form);
    status = response ? Status.SUCCESS : Status.ERROR;
    isLoading = false;
  };
</script>

<FormPanel>
  {#if status === Status.NONE || status === Status.ERROR}
    <TextInput
      id="name"
      label="Name"
      bind:value={form.name}
      invalidCallBack={form.setInvalidName}
    />
    <EmailInput
      id="email"
      bind:value={form.email}
      invalidCallBack={form.setInvalidEmail}
    />
    <ConfirmPasswordInputs
      id="password"
      idConfirm="password-confirm"
      bind:value={form.password}
      invalidCallBack={form.setInvalidPassword}
    />
    <div class="mt-4">
      <SubmitButton
        id="submit"
        {isLoading}
        {clickSubmit}
        disabled={form.invalidName ||
          form.invalidEmail ||
          form.invalidPassword ||
          isLoading}
      />
    </div>
    {#if status === Status.ERROR}
      <p class="mt-2 text-xl text-rose-500">Something went wrong!</p>
    {/if}
  {/if}
  {#if status === Status.SUCCESS}
    <p class="text-center text-xl dark:text-white">
      Verify your email and follows its instructions.
    </p>
  {/if}
</FormPanel>
