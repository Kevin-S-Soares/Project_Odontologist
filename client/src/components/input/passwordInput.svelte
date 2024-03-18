<script lang="ts">
  import Eye from "../svg/eye.svelte";
  import ClosedEye from "../svg/closedEye.svelte";

  export let id: string;
  export let label: string = "Password";
  export let value: string;
  export let errorMessage: string = "Password must be...";
  export let emptyMessage: string = "Password cannot be empty.";
  export let invalidCallBack: (arg: boolean) => any = () => 0;

  export let pattern: string = ".*";

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
    if (element.classList.contains("outline-teal-500")) {
      element.classList.remove("outline-teal-500");
    }
    if (!element.classList.contains("outline-rose-500")) {
      element.classList.add("outline-rose-500");
    }
    arg.show = true;
    arg.color = "#f43f5e";
  };

  const setSuccess = (
    element: HTMLInputElement,
    arg: { show: boolean; color: string },
  ) => {
    if (element.classList.contains("outline-rose-500")) {
      element.classList.remove("outline-rose-500");
    }
    if (!element.classList.contains("outline-teal-500")) {
      element.classList.add("outline-teal-500");
    }
    arg.show = false;
    arg.color = "#14b8a6";
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
        input.classList.contains("outline-teal-500") ||
        input.classList.contains("outline-rose-500")
      ) {
        if (input.classList.contains("outline-teal-500")) {
          input.classList.remove("outline-teal-500");
        } else {
          input.classList.remove("outline-rose-500");
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
      if (input.classList.contains("outline-rose-500")) {
        input.classList.remove("outline-rose-500");
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

  <p class="mt-2 text-rose-500 {details.show ? 'block' : 'hidden'}">
    {details.message}
  </p>
</div>
