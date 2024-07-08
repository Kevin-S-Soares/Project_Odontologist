<script lang="ts">
  import FormPanel from "../../../../../components/panel/formPanel.svelte";
  import EmailInput from "../../../../../components/input/emailInput.svelte";
  import SubmitButton from "../../../../../components/button/submitButton.svelte";
  import { changeEmail } from "../../../../../models/APIAdapters/user/changeEmail.ts";
  import { Status } from "../../../../../models/enums";

  export let token: string;
  export let email: string;

  const form = {
    email: email,
    invalidEmail: true,
    setInvalidEmail: (arg: boolean) => {
      form.invalidEmail = arg;
    },
  };

  let status = Status.NONE;
  let isLoading = false;
  const clickSubmit = async (event: any) => {
    isLoading = true;
    const response = await changeEmail(form.email, token);
    status = response ? Status.SUCCESS : Status.ERROR;
    isLoading = false;
  };
</script>

<FormPanel bg_color="white">
  {#if status === Status.NONE}
    <EmailInput
      id="email"
      bind:value={form.email}
      invalidCallBack={form.setInvalidEmail}
    />
    <div class="mt-4">
      <SubmitButton
        {clickSubmit}
        id={"submit"}
        disabled={isLoading || form.invalidEmail}
        {isLoading}
      />
    </div>
  {/if}
  {#if status === Status.ERROR}
    <p class="text-center text-lg text-rose-500">Something went wrong!</p>
  {/if}
  {#if status === Status.SUCCESS}
    <p class="text-center text-lg dark:text-white">
      Please verify your email and follow its instructions.
    </p>
  {/if}
</FormPanel>
