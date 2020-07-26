<script lang="ts">
  import Router, { wrap, replace } from "svelte-spa-router";
  import { user } from "./stores/auth.store";

  import Login from "./pages/Login.svelte";
  import Home from "./pages/Home.svelte";

  function conditionsFailed(event: any): void {
    replace("/login");
  }

  const routes = {
    "/": wrap(Home, (detail: any) => {
      console.log($user);
      return $user;
    }),
    "/login": Login,
  };
</script>

<Router {routes} on:conditionsFailed={conditionsFailed} />
