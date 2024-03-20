<script lang="ts">
  export let isDarkTheme: boolean;

  import Gear from "../components/svg/gear.svelte";
  import Exit from "../components/svg/exit.svelte";
  import Tool from "../components/svg/tool.svelte";
  import Tooth from "../components/svg/tooth.svelte";
  import User from "../components/svg/user.svelte";
  import Calendar from "../components/svg/calendar.svelte";
  import { Role } from "../models/enums";

  export let role: Role;

  let isDropDownActive = false;

  const switchDropDown = () => {
    const clicked = () => {
      isDropDownActive = !isDropDownActive;
      if (!isDropDownActive) {
        window.removeEventListener("click", clicked);
      }
    };
    if (!isDropDownActive) {
      window.addEventListener("click", clicked);
    }
  };

  const logout = () => {
    document.cookie = "Authorization=; Max-age=0; Path=/;";
    document.location.replace("/");
  };
</script>

<div class="">
  <button
    class="{isDropDownActive
      ? ''
      : 'rotate-90'} rounded-full bg-gray-200 p-1 transition-all dark:bg-neutral-600"
    aria-label={"Dropdown button"}
    on:click={switchDropDown}
  >
    <Gear color={isDarkTheme ? "#FFFFFF" : "#1f2937"} />
  </button>

  <div
    id="dropdown"
    class="absolute left-1/2 top-[8.5rem] z-10 -translate-x-1/2 -translate-y-1/2 lg:top-[6rem] {isDropDownActive
      ? 'flex'
      : 'hidden'} w-96 flex-col rounded-md border bg-white py-1 dark:border-neutral-900 dark:bg-neutral-800"
  >
    {#if role === Role.ADMIN || role === Role.VISITOR}
      <a
        class="grid grid-cols-2 hover:underline lg:hidden dark:decoration-white"
        href="/appointments/"
        ><span class="mr-12 place-self-end self-center"
          ><Calendar color={isDarkTheme ? "#FFFFFF" : "#1f2937"} /></span
        ><span class="ml-4 place-self-start self-center text-lg dark:text-white"
          >Appointments</span
        ></a
      >
      <a
        class="grid grid-cols-2 hover:underline lg:hidden dark:decoration-white"
        href="/odontologists/"
        ><span class="mr-12 place-self-end self-center"
          ><Tooth color={isDarkTheme ? "#FFFFFF" : "#1f2937"} /></span
        ><span class="ml-4 place-self-start self-center text-lg dark:text-white"
          >Odontologists</span
        ></a
      >
      <a
        class="grid grid-cols-2 hover:underline lg:hidden dark:decoration-white"
        href="/users/"
        ><span class="mr-12 place-self-end self-center dark:text-white"
          ><User color={isDarkTheme ? "#FFFFFF" : "#1f2937"} /></span
        ><span class="ml-4 place-self-start self-center text-lg dark:text-white"
          >Users</span
        ></a
      >
    {/if}
    {#if role === Role.ODONTOLOGIST}
      <a
        class="grid grid-cols-2 hover:underline lg:hidden dark:decoration-white"
        href="/appointments/"
        ><span class="mr-12 place-self-end self-center"
          ><Calendar color={isDarkTheme ? "#FFFFFF" : "#1f2937"} /></span
        ><span class="ml-4 place-self-start self-center text-lg dark:text-white"
          >Appointments</span
        ></a
      >
      <a
        class="grid grid-cols-2 hover:underline lg:hidden dark:decoration-white"
        href="/odontologists/"
        ><span class="mr-12 place-self-end self-center"
          ><Tooth color={isDarkTheme ? "#FFFFFF" : "#1f2937"} /></span
        ><span class="ml-4 place-self-start self-center text-lg dark:text-white"
          >Dashboard</span
        ></a
      >
    {/if}
    {#if role === Role.ATTENDANT}
      <a
        class="grid grid-cols-2 hover:underline lg:hidden dark:decoration-white"
        href="/appointments/"
        ><span class="mr-12 place-self-end self-center"
          ><Calendar color={isDarkTheme ? "#FFFFFF" : "#1f2937"} /></span
        ><span class="ml-4 place-self-start self-center text-lg dark:text-white"
          >Appointments</span
        ></a
      >
      <a
        class="grid grid-cols-2 hover:underline lg:hidden dark:decoration-white"
        href="/odontologists/"
        ><span class="mr-12 place-self-end self-center"
          ><Tooth color={isDarkTheme ? "#FFFFFF" : "#1f2937"} /></span
        ><span class="ml-4 place-self-start self-center text-lg dark:text-white"
          >Odontologists</span
        ></a
      >
    {/if}

    <a
      class="grid grid-cols-2 hover:underline dark:decoration-white"
      href="/user/settings/appearance"
      ><span class="mr-12 place-self-end self-center"
        ><Tool color={isDarkTheme ? "#FFFFFF" : "#1f2937"} /></span
      ><span class="ml-4 place-self-start self-center text-lg dark:text-white"
        >Settings</span
      ></a
    >
    <button
      on:click={logout}
      class="grid grid-cols-2 hover:underline dark:decoration-white"
      ><span class="mr-12 place-self-end self-center"
        ><Exit color={isDarkTheme ? "#FFFFFF" : "#1f2937"} /></span
      ><span class="ml-4 place-self-start self-center text-lg dark:text-white"
        >Log out</span
      ></button
    >
  </div>
</div>
