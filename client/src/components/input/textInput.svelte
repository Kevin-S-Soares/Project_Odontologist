<script lang="ts">
  import { errorText } from "../text/errorText.ts";

  export let value: string;
  export let id: string;
  export let label: string;
  export let emptyMessage = `${label} cannot be empty.`;
  export let invalidCallBack: (arg: boolean) => any = (arg) => 0;

  const successOutline = "outline-teal-600";
  const errorOutline = "outline-rose-600";

  let show = false;
  const clickInput = () => {
    if (value === "") {
      show = false;
      const input = document.getElementById(id) as HTMLInputElement;
      if (input.classList.contains(errorOutline)) {
        input.classList.remove(errorOutline);
      }
    }
  };

  const showErrorMessage = () => {
    const input = document.getElementById(id) as HTMLInputElement;
    if (value === "") {
      show = true;
      invalidCallBack(true);
      if (!input.classList.contains("outline")) {
        input.classList.add("outline");
      }
      if (!input.classList.contains(errorOutline)) {
        input.classList.add(errorOutline);
      }
    } else {
      show = false;
      invalidCallBack(false);
      if (!input.classList.contains("outline")) {
        input.classList.add("outline");
      }
      if (input.classList.contains(errorOutline)) {
        input.classList.remove(errorOutline);
      }
      if (!input.classList.contains(successOutline)) {
        input.classList.add(successOutline);
      }
    }
  };

  const verifyValue = () => {
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
    } else {
      invalidCallBack(false);
    }
    if (!input.classList.contains("outline")) {
      input.classList.add("outline");
    }
  };
</script>

<div class="mt-2 flex flex-col">
  <label class="font-medium dark:text-white" for={id}>{label}</label>
  <input
    class="mt-2 h-12 rounded-md border-2 p-2 focus:outline focus:{successOutline}"
    {id}
    type="text"
    bind:value
    on:focusin={clickInput}
    on:focusout={showErrorMessage}
    on:input={verifyValue}
  />
  <p class="mt-2 {errorText} {show ? 'block' : 'hidden'}">{emptyMessage}</p>
</div>
