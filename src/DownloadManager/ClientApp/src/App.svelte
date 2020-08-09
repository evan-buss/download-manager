<script lang="ts">
  import Router, { wrap, replace } from "svelte-spa-router";
  import { user } from "./stores/auth.store";

  import Login from "./pages/Login.svelte";
  import Dashboard from "./pages/Dashboard.svelte";
  import Settings from "./pages/Settings.svelte";

  function conditionsFailed(event: any): void {
    replace(event.detail.redirectUrl);
  }

  function requiresAuth(detail: any): boolean {
    detail.redirectUrl = "/login";
    return user.isLoggedIn();
  }

  const routes = {
    "/": wrap(Dashboard, (detail: any) => {
      return requiresAuth(detail);
    }),
    "/login": wrap(Login, (detail: any) => {
      detail.redirectUrl = "/dashboard";
      return !user.isLoggedIn();
    }),
    "/settings": wrap(Settings, (detail: any) => {
      return requiresAuth(detail);
    }),
    "*": wrap(Login, (detail: any) => {
      return !user.isLoggedIn();
    }),
  };
</script>

<Router {routes} on:conditionsFailed={conditionsFailed} />
