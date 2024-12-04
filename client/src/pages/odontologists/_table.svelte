
<script lang="ts">
    import { Odontologist } from "../../models/odontologist"
    import { remove } from "../../models/APIAdapters/odontologist/remove"
    export let odontologists: Odontologist[]
    export let token: string

    const getRow = (arg: number) => {
        let rows = "";
          if (arg === 0) {
            rows = "row-start-2 row-end-3 ";
          }
          if (arg === 1) {
            rows = "row-start-3 row-end-4 ";
          }
          if (arg === 2) {
            rows = "row-start-4 row-end-5 ";
          }
          if (arg === 3) {
            rows = "row-start-5 row-end-6 ";
          }
          if (arg === 4) {
            rows = "row-start-6 row-end-7 ";
          }
          return rows
    }

    let isModalVisible = false;
    let odontologistToDelete = new Odontologist();
    const showModal = () => {
    const modal = document.getElementById("modal") as HTMLDivElement;
    isModalVisible = !isModalVisible;
    if (isModalVisible) {
      window.onclick = (event: any) => {
        if (event.target == modal) {
          isModalVisible = false;
          window.onclick = null;
        }
      };
    }
  };
  const submit = async () => {
    console.log(odontologistToDelete.id)
      await remove(odontologistToDelete.id, token);
      window.location.replace("/odontologists/");
    };
</script>

<a
class="mt-4 block w-1/6 cursor-pointer rounded-md bg-teal-400 px-1 py-3 text-center font-bold text-white transition-all hover:bg-teal-500"
href="/odontologists/add">Add odontologist</a
>

    {#if odontologists.length > 0 }
    <div id="modal" style="background-color: rgba(0,0,0,0.4)" class="{isModalVisible? "block" : "hidden" } fixed w-full h-full bg-gray-300 z-10 left-0 top-0">
      <div class="relative">
        <div class="bg-white h-[16rem] w-96 absolute left-1/2 -translate-x-1/2 translate-y-1/2 top-1/2 rounded-md p-4 grid grid-rows-3">
          <button
          class="place-self-start justify-self-end text-4xl dark:text-white"
          on:click={() => {
            isModalVisible = false;
            window.onclick = null;
          }}>&#215;</button
        >
          <div>
            <p class="text-center text-lg dark:text-white">
              Are you sure that you want to delete {odontologistToDelete.name}
            </p> 
          </div>
          <div class="flex items-center justify-around">
            <button on:click={submit} class="rounded-md border bg-rose-600 p-2 font-bold text-white transition-all hover:bg-rose-700 dark:border-neutral-900">Submit</button>
            <button class="rounded-md border p-2 font-medium transition-all hover:bg-gray-100 dark:border-neutral-900 dark:text-white dark:hover:bg-neutral-600"
            on:click={() => {
              isModalVisible = false;
              window.onclick = null
            }}>Cancel</button>
          </div>

        </div>
      </div>
    </div>
      <div class="mt-4 grid w-full grid-cols-[repeat(6,_1fr)] grid-rows-auto rounded-md border">
        <div class="col-start-1 col-end-2 row-start-1 row-end-2">
          <p class="text-center font-medium">Name</p>
        </div>
        <div class="col-start-2 col-end-3 row-start-1 row-end-2">
          <p class="text-center font-medium">Phone</p>
        </div>
        <div class="col-start-3 col-end-4 row-start-1 row-end-2">
          <p class="text-center font-medium">Email</p>
        </div>
        <div class="col-start-4 col-end-5 row-start-1 row-end-2">
          <p class="text-center font-medium">Schedules</p>
        </div>
        <div class="col-start-5 col-end-7 row-start-1 row-end-2">
          <p class="text-center font-medium">Actions</p>
        </div>

        {#each odontologists as item, index}
              <div class={getRow(index) + "col-start-1 col-end-2"}>
                <p class="block text-center">{item.name}</p>
              </div>
              <div class={getRow(index) + "col-start-2 col-end-3"}>
                <p class="block text-center">{item.phone}</p>
              </div>
              <div class={getRow(index) + "col-start-3 col-end-4 justify-self-center"}>
                <a
                  href={`mailto:${item.email}`}
                  class="cursor-pointer text-center underline"
                >
                  {item.email}
                </a>
              </div>
              <div class={getRow(index) + "col-start-4 col-end-5 justify-self-center"}>
                <a
                  href={`/schedules/${item.id}`}
                  class="cursor-pointer text-center hover:underline"
                >
                  details
                </a>
              </div>
              <div class={getRow(index) + "col-start-5 col-end-6 justify-self-center"}>
                <a
                  href={`/odontologists/edit/${item.id}`}
                  class="cursor-pointer text-center hover:underline"
                >
                  edit
                </a>
              </div>
              <div
                class={
                  getRow(index) +
                  "col-start-6 col-end-7 justify-self-center hover:underline"
                }>
              
                <button on:click={() => {
                  odontologistToDelete = item;
                  showModal()
                }} class="cursor-pointer text-center">delete</button>

              </div>
          
        {/each}
      </div>
    {:else}
      <div class="mt-4">
        <p class="text-center text-3xl">No odontologists registered.</p>
      </div>
   {/if}