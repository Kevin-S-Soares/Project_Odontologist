<script lang="ts">
  import { errorText } from "../text/errorText.ts";
  import Eye from "../svg/eye.svelte";
  import ClosedEye from "../svg/closedEye.svelte";

  export let id: string;
  export let label: string = "Password";
  export let value: string;
  export let errorMessage: string = "Password must be...";
  export let emptyMessage: string = "Password cannot be empty.";
  export let invalidCallBack: (arg: boolean) => any = () => 0;

  export let pattern: string = ".*";

  const successOutline = "outline-teal-600";
  const errorOutline = "outline-rose-600";

  let details = {
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
      invalidCallBack(false);
    } else {
      setError(input, details);
      details.message = errorMessage;
      invalidCallBack(true);
    }
  };

  const showErrorMessage = () => {
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
      invalidCallBack(false);
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
