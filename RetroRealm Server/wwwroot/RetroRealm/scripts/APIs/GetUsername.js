import baseURL from "../../../scripts/baseURL.js";
import refreshToken from "../../../scripts/tokenRefresher.js";
export default async function GetUserName() {
  const token = sessionStorage.getItem("Token");
  if (!token) return "";

  async function request(tokenToUse) {
    return fetch(`${baseURL}/api/GetUserName`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${tokenToUse}`,
      },
    });
  }

  try {
    let response = await request(token);

    if (response.status === 401) {
      const refreshed = await refreshToken(
        sessionStorage.getItem("RefreshToken"),
      );
      if (!refreshed) return "";

      const newToken = sessionStorage.getItem("Token");
      if (!newToken) return "";

      response = await request(newToken);
    }

    if (!response.ok) return "";

    const userName = await response.text();
    return userName;
  } catch (err) {
    // console.error("GetUserName error:", err);
    return "";
  }
}
