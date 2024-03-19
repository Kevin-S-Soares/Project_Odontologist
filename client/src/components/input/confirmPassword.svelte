<script lang="ts">
  import Eye from "../svg/eye.svelte";
  import ClosedEye from "../svg/closedEye.svelte";
  import { errorText } from "../text/errorText";

  export let id: string;
  export let idConfirm: string;
  export let label: string = "Password";
  export let confirmLabel: string = "Confirm password";
  export let value: string;
  export let confirmValue = "";
  export let errorMessage: string = "Password must be...";
  export let confirmErrorMessage: string = "Passwords do not match.";
  export let emptyMessage: string = "Password cannot be empty.";
  export let invalidCallBack: (arg: boolean) => any = () => 0;

  const successOutline = "outline-teal-600";
  const errorOutline = "outline-rose-600";

  export let pattern: string = ".*";

  let details = {
    pressed: false,
    color: "#000000",
    show: false,
    message: "",
  };

  let detailsConfirm = {
    pressed: false,
    color: "#000000",
    show: false,
    message: "",
  };

  const setError = (
    element: HTMLInputElement,
    arg: { show: boolean; color: string },
  ) => {
    if (element.classList.contains(successOutline)) {
      element.classList.remove(successOutline);
    }
    if (!element.classList.contains(errorOutline)) {
      element.classList.add(errorOutline);
    }
    arg.show = true;
    arg.color = "#e11d48";
  };

  const setSuccess = (
    element: HTMLInputElement,
    arg: { show: boolean; color: string },
  ) => {
    if (element.classList.contains(errorOutline)) {
      element.classList.remove(errorOutline);
    }
    if (!element.classList.contains(successOutline)) {
      element.classList.add(successOutline);
    }
    arg.show = false;
    arg.color = "#0d9488";
  };

  const changeType = (event: any) => {
    event.preventDefault();
    const input = document.getElementById(id) as HTMLInputElement;
    if (details.pressed) {
      input.type = "password";
    } else {
      input.type = "text";
    }
    details.pressed = !details.pressed;
    input.focus();
  };

  const confirmChangeType = (event: any) => {
    event.preventDefault();
    const input = document.getElementById(idConfirm) as HTMLInputElement;
    if (detailsConfirm.pressed) {
      input.type = "password";
    } else {
      input.type = "text";
    }
    detailsConfirm.pressed = !detailsConfirm.pressed;
    input.focus();
  };

  const verifyPattern = () => {
    const input = document.getElementById(id) as HTMLInputElement;
    if (value === "") {
      if (
        input.classList.contains(successOutline) ||
        input.classList.contains(errorOutline)
      ) {
        if (input.classList.contains(successOutline)) {
          input.classList.remove(successOutline);
        } else {
          input.classList.remove(errorOutline);
        }
      }
      details.show = false;
      details.color = "#000000";
      invalidCallBack(true);
      return;
    }
    if (!input.classList.contains("outline")) {
      input.classList.add("outline");
    }
    details.show = value.match(pattern) === null;

    if (!details.show) {
      setSuccess(input, details);
      invalidCallBack(value !== confirmValue);
    } else {
      setError(input, details);
      details.message = errorMessage;
      invalidCallBack(true);
    }
  };

  const confirmVerifyPattern = () => {
    const input = document.getElementById(idConfirm) as HTMLInputElement;
    if (confirmValue === "") {
      if (
        input.classList.contains(successOutline) ||
        input.classList.contains(errorOutline)
      ) {
        if (input.classList.contains(successOutline)) {
          input.classList.remove(successOutline);
        } else {
          input.classList.remove(errorOutline);
        }
      }
      detailsConfirm.show = false;
      detailsConfirm.color = "#000000";
      invalidCallBack(true);
      return;
    }
    if (!input.classList.contains("outline")) {
      input.classList.add("outline");
    }
    detailsConfirm.show = value !== confirmValue;

    if (!detailsConfirm.show) {
      setSuccess(input, detailsConfirm);
      invalidCallBack(value !== confirmValue);
    } else {
      setError(input, detailsConfirm);
      detailsConfirm.message = confirmErrorMessage;
      invalidCallBack(true);
    }
  };

  const showErrorMessage = () => {
    const confirmInput = document.getElementById(idConfirm) as HTMLInputElement;
    if (confirmValue !== "") {
      if (value !== confirmValue) {
        setError(confirmInput, detailsConfirm);
        errorMessage = confirmErrorMessage;
        invalidCallBack(true);
      } else {
        setSuccess(confirmInput, detailsConfirm);
        invalidCallBack(value !== confirmValue);
      }
      detailsConfirm = detailsConfirm;
    }

    const input = document.getElementById(id) as HTMLInputElement;
    if (value === "") {
      setError(input, details);
      details.message = emptyMessage;
      if (!input.classList.contains("outline")) {
        input.classList.add("outline");
      }
      invalidCallBack(true);
      return;
    }
    details.show = value.match(pattern) === null;
    if (details.show) {
      setError(input, details);
      details.message = errorMessage;
      invalidCallBack(true);
    } else {
      setSuccess(input, details);
      invalidCallBack(value !== confirmValue);
    }
  };

  const confirmShowErrorMessage = () => {
    const input = document.getElementById(idConfirm) as HTMLInputElement;
    if (value === "") {
      setError(input, detailsConfirm);
      detailsConfirm.message = emptyMessage;
      if (!input.classList.contains("outline")) {
        input.classList.add("outline");
      }
      invalidCallBack(true);
      return;
    }
    detailsConfirm.show = value !== confirmValue;
    if (detailsConfirm.show) {
      setError(input, detailsConfirm);
      detailsConfirm.message = confirmErrorMessage;
      invalidCallBack(true);
    } else {
      setSuccess(input, detailsConfirm);
      invalidCallBack(value !== confirmValue);
    }
  };

  const clickInput = () => {
    if (value === "") {
      details.show = false;
      const input = document.getElementById(id) as HTMLInputElement;
      if (input.classList.contains(errorOutline)) {
        input.classList.remove(errorOutline);
      }
      details.color = "#000000";
    }
  };
  const confirmClickInput = () => {
    if (confirmValue === "") {
      detailsConfirm.show = false;
      const input = document.getElementById(idConfirm) as HTMLInputElement;
      if (input.classList.contains(errorOutline)) {
        input.classList.remove(errorOutline);
      }
      detailsConfirm.color = "#000000";
    }
  };
</script>

<div class="relative mt-2 flex flex-col">
  <label class="font-medium dark:text-white" for={id}>{label}</label>

  <input
    class="mt-2 rounded-md border-2 p-2 focus:outline"
    {id}
    type="password"
    bind:value
    on:input={verifyPattern}
    on:focusin={clickInput}
    on:focusout={showErrorMessage}
    {pattern}
  />
  <button
    tabindex="-1"
    id="show-{id}"
    class="absolute right-6 top-[3.12rem]"
    on:mousedown={changeType}
    >{#if details.pressed}
      <ClosedEye color={details.color} />
    {:else}
      <Eye color={details.color} />
    {/if}
  </button>

  <p class="mt-2 {errorText} {details.show ? 'block' : 'hidden'}">
    {details.message}
  </p>
</div>

<div class="relative mt-2 flex flex-col">
  <label class="font-medium dark:text-white" for={idConfirm}
    >{confirmLabel}</label
  >
  <input
    class="mt-2 rounded-md border-2 p-2 focus:outline"
    id={idConfirm}
    type="password"
    bind:value={confirmValue}
    on:input={confirmVerifyPattern}
    on:focusin={confirmClickInput}
    on:focusout={confirmShowErrorMessage}
  />
  <button
    tabindex="-1"
    id="show-{idConfirm}"
    class="absolute right-6 top-[3.12rem]"
    on:mousedown={confirmChangeType}
    >{#if detailsConfirm.pressed}
      <ClosedEye color={detailsConfirm.color} />
    {:else}
      <Eye color={detailsConfirm.color} />
    {/if}
  </button>

  <p class="mt-2 {errorText} {detailsConfirm.show ? 'block' : 'hidden'}">
    {detailsConfirm.message}
  </p>
</div>
