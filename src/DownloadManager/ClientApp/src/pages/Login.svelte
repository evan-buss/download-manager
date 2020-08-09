<script lang="ts">
  import { user } from "../stores/auth.store";
  import { push } from "svelte-spa-router";
  import AlertMessage from "../components/AlertMessage.svelte";

  let username: string = "";
  let password: string = "";
  let loginFailed = false;

  async function handleSubmit(): Promise<void> {
    const success = await user.login(username, password);
    console.log(success);
    if (success) push("/");
    else loginFailed = true;
  }

  $: invalidUsername = username.includes("evan");
  $: invalidPassword = password.length < 8;
</script>

<main class="h-screen flex justify-center items-center">
  {#if loginFailed}
    <div class="fixed top-0 mt-6">
      <AlertMessage type="error" message="Invalid credentials. Login Failed." />
    </div>
  {/if}

  <div
    class="bg-white w-full max-w-sm rounded-lg shadow-lg hover:shadow-xl
    overflow-hidden mx-auto">
    <div class="py-4 px-6">
      <div class="text-center font-bold text-gray-700 text-3xl">
        Download Manager
      </div>
      <div class="mt-1 text-center text-gray-600">Login or create account</div>
      <form on:submit={handleSubmit}>
        <div class="mt-4 w-full">
          <input
            bind:value={username}
            placeholder="Username"
            class="w-full mt-2 py-2 px-4 bg-gray-100 text-gray-700 border
            border-gray-300 rounded block appearance-none placeholder-gray-500
            focus:outline-none focus:bg-white" />
          {#if invalidUsername}
            <p class="text-red-600">Invalid username.</p>
          {/if}
        </div>
        <div class="mt-4 w-full">
          <input
            bind:value={password}
            type="password"
            placeholder="Password"
            class="w-full mt-2 py-2 px-4 bg-gray-100 text-gray-700 border
            border-gray-300 rounded block appearance-none placeholder-gray-500
            focus:outline-none focus:bg-white" />
          {#if invalidPassword}
            <p class="text-red-600">Invalid password.</p>
          {/if}
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
        on:click={() => console.log('wowee')}
        class="text-blue-600 font-bold mx-2 text-sm hover:text-blue-500">
        Register
      </div>
    </div>
  </div>
</main>
