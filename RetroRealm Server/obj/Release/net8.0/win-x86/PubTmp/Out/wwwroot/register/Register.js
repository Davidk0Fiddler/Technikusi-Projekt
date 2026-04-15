import baseUrl from "../scripts/baseURL.js";

export default async function Register(username, password) {
  return await fetch(`${baseUrl}/api/Register`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      username: username,
      password: password,
    }),
  })
    .then((response) => {
      return response.status;
    })
    .catch((error) => {
      console.error("Hálózati hiba:", error);
      throw error;
    });
}
