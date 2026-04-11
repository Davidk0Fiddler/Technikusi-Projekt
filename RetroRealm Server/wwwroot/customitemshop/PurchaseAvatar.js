import baseUrl from "../scripts/baseURL.js";
import refreshToken from "../scripts/tokenRefresher.js";

export default async function PurchaseAvatar(avatarName) {
  if (!sessionStorage.getItem("Token")) {
    return "User not logged in!";
  }

  console.log(sessionStorage.getItem("RefreshToken"));
  await refreshToken(sessionStorage.getItem("RefreshToken"));

  console.log(sessionStorage.getItem("Token"));
  const response = await fetch(`${baseUrl}/api/PurchaseAvatar`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${sessionStorage.getItem("Token")}`,
    },
    body: JSON.stringify({
      avatarName: avatarName,
    }),
  });

  let responseData;
  const contentType = response.headers.get("content-type");

  if (contentType && contentType.includes("application/json")) {
    responseData = await response.json();
  } else {
    responseData = await response.text();
  }

  if (response.ok) return "Avatar purchased!";

  if (response.status === 404) return "User!";

  if (response.status === 400) return responseData;

  return "Unexpected error occurred!";
}
