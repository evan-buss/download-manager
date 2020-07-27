<script lang="ts">
  import Router, { wrap, replace } from "svelte-spa-router";
  import { user } from "./stores/auth.store";

  import Login from "./pages/Login.svelte";
  import Home from "./pages/Home.svelte";

  function conditionsFailed(event: any): void {
    replace("/login");
  }

  $: isLoggedIn = $user.refreshToken && $user.accessToken;

  // $: let isLoggedIn = user.

  const routes = {
    "/": wrap(Home, (detail: any) => {
      console.log($user);
      return isLoggedIn;
    }),
    "/login": Login,
  };
</script>

<Router {routes} on:conditionsFailed={conditionsFailed} />

<!-- Hook up actual user store. Store username, token, refresh token -->
<!-- Hook up stronly typed configuration. Need a way to determine if we are running in a conatiner or not -->
