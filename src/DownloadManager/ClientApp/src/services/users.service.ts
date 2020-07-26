export async function fetchAllUsers(): Promise<string[]> {
  return await fetch(
    `${window.location.origin}/weatherforecast/db`
  ).then((response) => response.json());
}

export async function createUser(name: string): Promise<string[]> {
  return await fetch(
    `${window.location.origin}/weatherforecast/createname?name=${name}`
  ).then((response) => response.json());
}

export const name = "Evan Buss";
