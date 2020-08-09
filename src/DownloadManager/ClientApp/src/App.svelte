<script lang="ts">
  import Router, { wrap, replace } from "svelte-spa-router";
  import { user } from "./stores/auth.store";

  import Login from "./pages/Login.svelte";
  import Dashboard from "./pages/Dashboard.svelte";

  function conditionsFailed(event: any): void {
    replace(event.detail.redirectUrl);
  }

  const routes = {
    "/": wrap(Dashboard, (detail: any) => {
      detail.redirectUrl = "/login";
      return user.isLoggedIn();
    }),
    "/login": wrap(Login, (detail: any) => {
      detail.redirectUrl = "/";
      return !user.isLoggedIn();
    }),
  };
</script>

<Router {routes} on:conditionsFailed={conditionsFailed} />
