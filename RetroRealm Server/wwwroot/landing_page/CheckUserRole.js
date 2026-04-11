import refreshToken from "../scripts/tokenRefresher.js";
import baseUrl from "../scripts/baseURL.js";

export default async function CheckUserRole() {
  const token = sessionStorage.getItem("Token");
  if (!token) {
    return 401;
  }

  await refreshToken(sessionStorage.getItem("RefreshToken"));

  const response = await fetch(`${baseUrl}/getName`, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  switch (response.status) {
    case 401:
      console.log("nincs user");
      return 401;

    case 403:
      console.log("rossz user");
      return 403;

    case 200:
      console.log("jó user");
      return 200;

    default:
      console.log(response.status);
      return 401;
  }
}
