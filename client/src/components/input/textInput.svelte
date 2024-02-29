<script lang="ts">
  export let value: string;
  export let id: string;
  export let label: string;
  export let emptyMessage = `${label} cannot be empty.`;
  export let invalidCallBack: (arg: boolean) => any = (arg) => 0;

  let show = false;
  const clickInput = () => {
    if (value === "") {
      show = false;
      const input = document.getElementById(id) as HTMLInputElement;
      if (input.classList.contains("outline-rose-500")) {
        input.classList.remove("outline-rose-500");
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
      if (!input.classList.contains("outline-rose-500")) {
        input.classList.add("outline-rose-500");
      }
    } else {
      show = false;
      invalidCallBack(false);
      if (!input.classList.contains("outline")) {
        input.classList.add("outline");
      }
      if (input.classList.contains("outline-rose-500")) {
        input.classList.remove("outline-rose-500");
      }
      if (!input.classList.contains("outline-teal-500")) {
        input.classList.add("outline-teal-500");
      }
    }
  };

  const verifyValue = () => {
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
  <label class="font-medium" for={id}>{label}</label>
  <input
    class="mt-2 rounded-md border-2 p-2 focus:outline focus:outline-teal-500"
    {id}
    type="text"
    bind:value
    on:focusin={clickInput}
    on:focusout={showErrorMessage}
    on:input={verifyValue}
  />
  <p class="mt-2 text-rose-500 {show ? 'block' : 'hidden'}">{emptyMessage}</p>
</div>
