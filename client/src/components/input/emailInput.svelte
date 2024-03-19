<script lang="ts">
  import { errorText } from "../text/errorText";

  export let id: string;
  export let label: string = "Email";
  export let value: string;
  export let errorMessage: string = "Invalid email.";
  export let emptyMessage: string = "Email cannot be empty.";
  export let invalidCallBack: (arg: boolean) => any = (arg) => 0;

  const successOutline = "outline-teal-600";
  const errorOutline = "outline-rose-600";

  let pattern = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
  let show = false;
  let message = "";

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
      show = false;
      invalidCallBack(true);
      return;
    }
    if (!input.classList.contains("outline")) {
      input.classList.add("outline");
    }
    show = value.match(pattern) === null;

    if (!show) {
      if (!input.classList.contains(successOutline)) {
        input.classList.add(successOutline);
      }
      if (input.classList.contains(errorOutline)) {
        input.classList.remove(errorOutline);
      }
      invalidCallBack(false);
    } else {
      if (!input.classList.contains(errorOutline)) {
        input.classList.add(errorOutline);
      }
      if (input.classList.contains(successOutline)) {
        input.classList.remove(successOutline);
      }
      message = errorMessage;
      invalidCallBack(true);
    }
  };

  const showErrorMessage = () => {
    const input = document.getElementById(id) as HTMLInputElement;
    if (value === "") {
      show = true;
      message = emptyMessage;
      invalidCallBack(true);
      if (!input.classList.contains("outline")) {
        input.classList.add("outline");
      }
      if (!input.classList.contains(errorOutline)) {
        input.classList.add(errorOutline);
      }
      return;
    }
    show = value.match(pattern) === null;
    if (show) {
      if (!input.classList.contains(errorOutline)) {
        input.classList.add(errorOutline);
      }
      message = errorMessage;
      invalidCallBack(true);
    } else {
      if (!input.classList.contains(successOutline)) {
        input.classList.add(successOutline);
      }
      invalidCallBack(false);
    }
  };

  const clickInput = () => {
    if (value === "") {
      show = false;
      const input = document.getElementById(id) as HTMLInputElement;
      if (input.classList.contains(errorOutline)) {
        input.classList.remove(errorOutline);
      }
    }
  };
</script>

<div class="mt-2 flex flex-col">
  <label class="font-medium dark:text-white" for={id}>{label}</label>
  <input
    class="mt-2 rounded-md border-2 p-2 focus:outline"
    {id}
    type="email"
    bind:value
    on:input={verifyPattern}
    on:focusin={clickInput}
    on:focusout={showErrorMessage}
    pattern={"^\\w+([\\.\\+\\-']\\w+)*@\\w+([\\-\\.]\\w+)*\\.\\w+([\\-\\.]\\w+)$"}
  />
  <p class="mt-2 {errorText} {show ? 'block' : 'hidden'}">{message}</p>
</div>
