<script lang="ts">
  import FormPanel from "../../../../../components/panel/formPanel.svelte";
  import ConfirmPasswords from "../../../../../components/input/confirmPassword.svelte";
  import SubmitButton from "../../../../../components/button/submitButton.svelte";
  import { changePassword } from "../../../../../models/APIAdapters/user/changePassword.ts";
  import { Status } from "../../../../../models/enums";

  export let token: string;

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
    const response = await changePassword(form.password, token);
    status = response ? Status.SUCCESS : Status.ERROR;
    isLoading = false;
  };
</script>

<FormPanel bg_color="white">
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
    <p class="text-center text-lg dark:text-white">
      Password changed successfully.
    </p>
  {/if}
</FormPanel>
