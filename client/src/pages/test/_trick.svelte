<script lang="ts">
  import EmailInput from "../../components/input/emailInput.svelte";
  import ConfirmPassword from "../../components/input/confirmPassword.svelte";
  const config = {
    email: "",
    password: "",
    confirmPassword: "",
  };
</script>

<EmailInput
  label="Email"
  id="email"
  errorMessage="Invalid email."
  emptyMessage="Email cannot be empty."
  bind:value={config.email}
/>
<ConfirmPassword
  label="Password"
  confirmLabel="Confirm password"
  id="password"
  idConfirm="confirm-password"
  errorMessage="Invalid password."
  confirmErrorMessage="Passwords do not match."
  emptyMessage="Password cannot be empty."
  bind:value={config.password}
/>
<button on:click={() => console.log(config)}>abc</button>
