<script lang="ts">
  import MagnifierGlass from "../../components/svg/magnifierGlass.svelte";
  import { getAllOthers } from "../../models/APIAdapters/users/getAllOthers";
  export let body: Array<any>;

  const form = {
    search: "",
  };

  let isSearching = false;
  const search = () => {
    if (!isSearching) {
      isSearching = true;
      setTimeout(async () => {
        body = await getAllOthers(form.search);
        isSearching = false;
      }, 500);
    }
  };
</script>

<div class="mt-2 h-96 w-full overflow-y-auto lg:w-1/4">
  <div class="relative">
    <input
      type="text"
      name="search"
      class="h-10 w-full rounded-md border px-4 text-right placeholder:font-medium placeholder:text-black dark:border-neutral-900"
      placeholder="Search for an user"
      on:input={search}
      bind:value={form.search}
    />
    <div class="absolute left-2 top-2.5">
      <span>
        <MagnifierGlass />
      </span>
    </div>
  </div>
  {#each body as item}
    <a
      class="my-2 flex h-20 items-center justify-between rounded-md border bg-gray-100 px-4 py-2 dark:border-neutral-900 dark:bg-neutral-700 dark:text-white"
      href={`/users/${item.id}`}
    >
      <span class="">
        <img
          width={32}
          height={32}
          class="rounded-full border border-neutral-900"
          src={"/placeholder.png"}
          alt={`user ${item.id}`}
          loading={"eager"}
        />{" "}
      </span>
      <span class="ml-2 line-clamp-1 dark:text-white">
        {item.name}
      </span>
    </a>
  {/each}
</div>
