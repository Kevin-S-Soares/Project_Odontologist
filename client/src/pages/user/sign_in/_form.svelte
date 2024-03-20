<script lang="ts">
  import FormPanel from "../../../components/panel/formPanel.svelte";
  import EmailInput from "../../../components/input/emailInput.svelte";
  import PasswordInput from "../../../components/input/passwordInput.svelte";
  import SubmitButton from "../../../components/button/submitButton.svelte";
  import { authenticate } from "../../../models/APIAdapters/user/authenticate";
  import { Status } from "../../../models/enums";
  import { errorText } from "../../../components/text/errorText";

  const form = {
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
    const response = await authenticate(form);
    if (response === "") {
      status = Status.ERROR;
      isLoading = false;
      form.email = "";
      form.password = "";
      setTimeout(() => {
        status = Status.NONE;
      }, 2000);
      return;
    }
    document.cookie = `Authorization=bearer ${response}; SameSite=Strict; Path=/;`;
    document.location.replace("/");
  };
</script>

<FormPanel>
  <EmailInput
    id="email"
    bind:value={form.email}
    invalidCallBack={form.setInvalidEmail}
  />
  <PasswordInput
    id="password"
    bind:value={form.password}
    invalidCallBack={form.setInvalidPassword}
  />
  <div class="mt-4 flex items-center justify-between">
    <SubmitButton
      id="submit"
      {isLoading}
      {clickSubmit}
      disabled={form.invalidEmail || form.invalidPassword || isLoading}
    />
    <a
      data-astro-prefetch="hover"
      class="text-cyan-700 underline hover:text-cyan-800 dark:text-cyan-400 dark:hover:text-cyan-300"
      href="/user/forget_password">Forgot password?</a
    >
  </div>

  {#if status == Status.ERROR}
    <p class="mt-4 text-xl {errorText}">Email or password invalid!</p>
  {/if}
</FormPanel>
