import baseUrl from "../scripts/baseURL.js";

export default async function Login(username, password) {
  return await fetch(`${baseUrl}/api/Login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      Username: username,
      Password: password,
    }),
  })
    .then((response) => {
      if (!response.ok) {
        return response.status;
      }

      return response.json().then((data) => {
        sessionStorage.setItem("Token", data.token);
        sessionStorage.setItem("RefreshToken", data.refreshToken.token);
        return response.status;
      });
    })
    .catch((error) => {
      console.error("Hálózati hiba:", error);
      throw error;
    });
}
