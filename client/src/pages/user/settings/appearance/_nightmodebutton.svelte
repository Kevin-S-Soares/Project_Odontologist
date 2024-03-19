<script lang="ts">
  export let isDarkTheme: boolean;

  import Sun from "../../../../components/svg/sun.svelte";
  import Moon from "../../../../components/svg/moon.svelte";

  let isLightTheme = isDarkTheme;

  const changeTheme = () => {
    isLightTheme = !isLightTheme;
    const body = document.getElementById("body") as HTMLBodyElement;
    console.log("chegou aqui");
    if (isLightTheme) {
      body.classList.remove("dark");
      document.cookie = "Theme=; Max-age=0; Path=/;";
      document.location.reload();
    } else {
      body.classList.add("dark");
      document.cookie = "Theme=Dark; Path=/;";
      document.location.reload();
    }
  };
</script>

<div class="mt-2">
  <div
    class="relative h-10 w-20 items-center rounded-3xl border bg-gray-200 p-1 dark:border-neutral-900 dark:bg-neutral-600"
  >
    <button
      class="absolute top-1/2 flex h-8 w-8 -translate-x-1/2 -translate-y-1/2 items-center justify-center rounded-full bg-white transition-all dark:bg-neutral-800 {isLightTheme
        ? ' translate-x-0'
        : ' translate-x-10'} duration-200"
        aria-label={isDarkTheme? "Switch to light mode": "Switch to dark mode"}
      on:click={changeTheme}
    >
      <span class="relative">
        {#if isLightTheme}
          <span class="absolute -translate-x-1/2 -translate-y-1/2"
            ><Sun color={isDarkTheme ? "#1f2937" : "#FFFFFF"} /></span
          >
        {:else}
          <span class="absolute -translate-x-1/2 -translate-y-1/2"
            ><Moon color={isDarkTheme ? "#1f2937" : "#FFFFFF"} /></span
          >
        {/if}
      </span>
    </button>
  </div>
</div>
