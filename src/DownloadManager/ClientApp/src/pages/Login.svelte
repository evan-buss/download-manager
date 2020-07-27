<script lang="ts">
  import { user } from "../stores/auth.store";
  import { push } from "svelte-spa-router";

  let username: string;
  let password: string;

  function handleSubmit(): void {
    user.login();
    push("/");
  }
</script>

<main class="h-screen flex justify-center items-center">
  <div
    class="bg-white w-full max-w-sm rounded-lg hover:shadow-md overflow-hidden
    mx-auto">
    <div class="py-4 px-6">
      <div class="text-center font-bold text-gray-700 text-3xl">
        Mega Server
      </div>
      <div class="mt-1 text-center text-gray-600">Login or create account</div>
      <form on:submit={handleSubmit}>
        <div class="mt-4 w-full">
          <input
            bind:value={username}
            required
            placeholder="Username"
            class="w-full mt-2 py-2 px-4 bg-gray-100 text-gray-700 border
            border-gray-300 rounded block appearance-none placeholder-gray-500
            focus:outline-none focus:bg-white" />
          <div class="text-red-500">Username is required.</div>
        </div>
        <div class="mt-4 w-full">
          <input
            required
            bind:value={password}
            type="password"
            placeholder="Password"
            class="w-full mt-2 py-2 px-4 bg-gray-100 text-gray-700 border
            border-gray-300 rounded block appearance-none placeholder-gray-500
            focus:outline-none focus:bg-white" />
          <div class="text-red-500">Password is required.</div>
        </div>
        <div class="flex justify-between items-center mt-6">
          <a href="#" class="text-gray-600 text-sm hover:text-gray-500">
            Forgot Password?
          </a>
          <button
            type="submit"
            class="py-2 px-4 bg-gray-700 text-white rounded hover:bg-gray-600
            focus:outline-none">
            Login
          </button>
        </div>
      </form>
    </div>
    <div class="flex items-center justify-center py-4 bg-gray-100 text-center">
      <h1 class="text-gray-600 text-sm">Dont`t have an account?</h1>
      <div
        on:click={() => console.log(user.test())}
        class="text-blue-600 font-bold mx-2 text-sm hover:text-blue-500">
        Register
      </div>
    </div>
  </div>
</main>
