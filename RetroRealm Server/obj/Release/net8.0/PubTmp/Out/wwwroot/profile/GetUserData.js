import baseURL from "../scripts/baseURL.js";
import refreshToken from "../scripts/tokenRefresher.js";

async function GetName() {
  await refreshToken(sessionStorage.getItem("RefreshToken"));
  const response = await fetch(`${baseURL}/api/GetUserName`, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${sessionStorage.getItem("Token")}`,
    },
  });

  if (response.ok) {
    const data = await response.text();
    return data;
  }
}

export default async function GetUserData() {
  let userName = await GetName();
  const response = await fetch(`${baseURL}/api/GetUserData`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      userName: userName,
    }),
  });

  if (!response.ok) {
    return undefined;
  }
  const data = await response.json();
  return data;
}
