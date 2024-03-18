<script lang="ts">
  export let id: string;
  export let label: string = "Email";
  export let value: string;
  export let errorMessage: string = "Invalid email.";
  export let emptyMessage: string = "Email cannot be empty.";
  export let invalidCallBack: (arg: boolean) => any = (arg) => 0;

  let pattern = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
  let show = false;
  let message = "";

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
      show = false;
      invalidCallBack(true);
      return;
    }
    if (!input.classList.contains("outline")) {
      input.classList.add("outline");
    }
    show = value.match(pattern) === null;

    if (!show) {
      if (!input.classList.contains("outline-teal-500")) {
        input.classList.add("outline-teal-500");
      }
      if (input.classList.contains("outline-rose-500")) {
        input.classList.remove("outline-rose-500");
      }
      invalidCallBack(false);
    } else {
      if (!input.classList.contains("outline-rose-500")) {
        input.classList.add("outline-rose-500");
      }
      if (input.classList.contains("outline-teal-500")) {
        input.classList.remove("outline-teal-500");
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
      if (!input.classList.contains("outline-rose-500")) {
        input.classList.add("outline-rose-500");
      }
      return;
    }
    show = value.match(pattern) === null;
    if (show) {
      if (!input.classList.contains("outline-rose-500")) {
        input.classList.add("outline-rose-500");
      }
      message = errorMessage;
      invalidCallBack(true);
    } else {
      if (!input.classList.contains("outline-teal-500")) {
        input.classList.add("outline-teal-500");
      }
      invalidCallBack(false);
    }
  };

  const clickInput = () => {
    if (value === "") {
      show = false;
      const input = document.getElementById(id) as HTMLInputElement;
      if (input.classList.contains("outline-rose-500")) {
        input.classList.remove("outline-rose-500");
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
  <p class="mt-2 text-rose-500 {show ? 'block' : 'hidden'}">{message}</p>
</div>
