<script lang="ts">
  import FormPanel from "../../../components/panel/formPanel.svelte";
  import EmailInput from "../../../components/input/emailInput.svelte";
  import SubmitButton from "../../../components/button/submitButton.svelte";
  import { forgetPassword } from "../../../models/APIAdapters/user/forgetPassword";
  import { Status } from "../../../models/enums";

  const form = {
    email: "",
    invalidEmail: true,
    setInvalidEmail: (arg: boolean) => {
      form.invalidEmail = arg;
    },
  };

  let status = Status.NONE;
  let isLoading = false;
  const clickSubmit = async () => {
    isLoading = true;
    await forgetPassword(form.email);
    status = Status.SUCCESS;
    isLoading = false;
  };
</script>

<FormPanel>
  {#if status === Status.NONE}
    <EmailInput
      id="email"
      bind:value={form.email}
      invalidCallBack={form.setInvalidEmail}
    />
    <div class="mt-4 flex items-center justify-between">
      <SubmitButton
        id="submit"
        {isLoading}
        {clickSubmit}
        disabled={form.invalidEmail || isLoading}
      />
    </div>
  {/if}

  {#if status == Status.SUCCESS}
    <p class="text-center text-xl font-normal">
      Verify your email and follows its instructions.
    </p>
  {/if}
</FormPanel>
