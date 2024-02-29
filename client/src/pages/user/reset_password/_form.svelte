<script lang="ts">
  import FormPanel from "../../../components/panel/formPanel.svelte";
  import ConfirmPasswords from "../../../components/input/confirmPassword.svelte";
  import SubmitButton from "../../../components/button/submitButton.svelte";
  import { resetPassword } from "../../../models/APIAdapters/user/resetPassword";
  import { Status } from "../../../models/enums";

  const form = {
    password: "",
    invalidPassword: true,
    setInvalidPassword: (arg: boolean) => {
      form.invalidPassword = arg;
    },
  };

  let status = Status.NONE;
  let isLoading = false;
  const clickSubmit = async (event: any) => {
    isLoading = true;
    const expression = /\?hash=(\w+)&userId=(.+)&operation=(\d+)/;
    const match = window.location.search.match(expression);
    if (match === null || match.length !== 4) {
      status = Status.ERROR;
      return;
    }

    const request = {
      password: form.password,
      hash: match[1],
      userId: match[2],
      operation: parseInt(match[3]),
    };

    const response = await resetPassword(request);
    status = response ? Status.SUCCESS : Status.ERROR;
    isLoading = false;
  };
</script>

<FormPanel>
  {#if status === Status.NONE}
    <ConfirmPasswords
      id="password"
      idConfirm="password-confirm"
      bind:value={form.password}
      invalidCallBack={form.setInvalidPassword}
    />
    <div class="mt-4">
      <SubmitButton
        {clickSubmit}
        id={"submit"}
        disabled={isLoading || form.invalidPassword}
        {isLoading}
      />
    </div>
  {/if}
  {#if status === Status.ERROR}
    <p class="text-center text-lg text-rose-500">Something went wrong!</p>
  {/if}
  {#if status === Status.SUCCESS}
    <p class="text-center text-lg">Password changed successfully.</p>
  {/if}
</FormPanel>
