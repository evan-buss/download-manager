<script lang="ts">
  import { user } from "../stores/auth.store";
  import { push } from "svelte-spa-router";
  import AlertMessage from "../components/AlertMessage.svelte";

  let username: string = "";
  let password: string = "";
  let passwordTouched = false;
  let loginFailed = false;

  async function handleSubmit(): Promise<void> {
    const success = await user.login(username, password);
    console.log(success);
    if (success) push("/");
    else loginFailed = true;
  }

  let invalidUsername = false;
  $: invalidPassword = password.length < 8 && passwordTouched;
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
      <div class="text-center font-bold text-black text-3xl">
        Download Manager
      </div>
      <div class="mt-1 text-center text-gray-600">Login or create account</div>
      <form on:submit={handleSubmit}>
        <div class="mt-4 w-full">
          <input
            bind:value={username}
            placeholder="Username"
            class="w-full mt-2 py-2 px-4 bg-gray-100 text-gray-700 border-2
            border-gray-400 rounded block appearance-none placeholder-gray-500
            focus:outline-none focus:bg-white focus:border-black" />
          {#if invalidUsername}
            <p class="text-red-600 mt-1">Invalid username.</p>
          {/if}
        </div>
        <div class="mt-4 w-full">
          <input
            bind:value={password}
            on:blur={() => (passwordTouched = true)}
            type="password"
            placeholder="Password"
            class="w-full mt-2 py-2 px-4 bg-gray-100 text-gray-700 border-2
            border-gray-400 rounded block appearance-none placeholder-gray-500
            focus:outline-none focus:bg-white focus:border-black" />
          {#if invalidPassword}
            <p class="text-red-600 mt-1">Invalid password.</p>
          {/if}
        </div>
        <div class="flex justify-between items-center mt-1">
          <a href="/reset" class="text-gray-600 text-sm hover:text-gray-500">
            Forgot Password?
          </a>
          <button type="submit" class="btn">Login</button>
        </div>
      </form>
    </div>
    <div class="flex items-center justify-center py-4 bg-gray-100 text-center">
      <h1 class="text-gray-600 text-sm">Dont`t have an account?</h1>
      <div
        on:click={() => console.log('wowee')}
        class="text-teal-400 font-bold mx-2 text-sm hover:text-blue-500">
        Register
      </div>
    </div>
  </div>
</main>
