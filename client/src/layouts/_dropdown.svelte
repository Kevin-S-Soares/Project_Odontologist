<script lang="ts">
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
    document.cookie = "Authorization=; Max-age=0;";
    document.location.reload();
  };
</script>

<div class="">
  <button
    class="{isDropDownActive
      ? ''
      : 'rotate-90'} rounded-full bg-gray-200 p-1 transition-all"
    id="arrow"
    on:click={switchDropDown}
  >
    <Gear />
  </button>

  <div
    id="dropdown"
    class="absolute z-10 left-0 top-16 {isDropDownActive
      ? 'flex'
      : 'hidden'} w-96 flex-col rounded-md border bg-white py-1"
  >
    {#if role === Role.ADMIN}
      <a
        class="grid grid-cols-2 hover:underline lg:hidden"
        href="/appointments/"
        ><span class="mr-12 place-self-end self-center"><Calendar /></span><span
          class="ml-4 place-self-start self-center text-lg">Appointments</span
        ></a
      >
      <a
        class="grid grid-cols-2 hover:underline lg:hidden"
        href="/odontologists/"
        ><span class="mr-12 place-self-end self-center"><Tooth /></span><span
          class="ml-4 place-self-start self-center text-lg">Odontologists</span
        ></a
      >
      <a
        class="grid grid-cols-2 hover:underline lg:hidden"
        href="/appointments/"
        ><span class="mr-12 place-self-end self-center"><User /></span><span
          class="ml-4 place-self-start self-center text-lg">Users</span
        ></a
      >
    {/if}
    {#if role === Role.ODONTOLOGIST}
      <a
        class="grid grid-cols-2 hover:underline lg:hidden"
        href="/appointments/"
        ><span class="mr-12 place-self-end self-center"><Calendar /></span><span
          class="ml-4 place-self-start self-center text-lg">Appointments</span
        ></a
      >
      <a
        class="grid grid-cols-2 hover:underline lg:hidden"
        href="/odontologists/"
        ><span class="mr-12 place-self-end self-center"><Tooth /></span><span
          class="ml-4 place-self-start self-center text-lg">Dashboard</span
        ></a
      >
    {/if}
    {#if role === Role.ATTENDANT}
      <a
        class="grid grid-cols-2 hover:underline lg:hidden"
        href="/appointments/"
        ><span class="mr-12 place-self-end self-center"><Calendar /></span><span
          class="ml-4 place-self-start self-center text-lg">Appointments</span
        ></a
      >
      <a
        class="grid grid-cols-2 hover:underline lg:hidden"
        href="/odontologists/"
        ><span class="mr-12 place-self-end self-center"><Tooth /></span><span
          class="ml-4 place-self-start self-center text-lg">Odontologists</span
        ></a
      >
    {/if}

    <a class="grid grid-cols-2 hover:underline" href="/settings/"
      ><span class="mr-12 place-self-end self-center"><Tool /></span><span
        class="ml-4 place-self-start self-center text-lg">Settings</span
      ></a
    >
    <button on:click={logout} class="grid grid-cols-2 hover:underline"
      ><span class="mr-12 place-self-end self-center"><Exit /></span><span
        class="ml-4 place-self-start self-center text-lg">Log out</span
      ></button
    >
  </div>
</div>
