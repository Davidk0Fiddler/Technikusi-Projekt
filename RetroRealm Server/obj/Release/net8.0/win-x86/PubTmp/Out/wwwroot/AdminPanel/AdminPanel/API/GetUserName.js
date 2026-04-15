import getAuthData from "../../../scripts/getAuthData.js";
import refreshToken from "../../../scripts/tokenRefresher.js";
import baseURL from "../../../scripts/baseURL.js";

export default async function getUserName() {
  let authData = await getAuthData();
  let isTried = false;

  const fetchName = async () => {
    try {
      const response = await fetch(`${baseURL}/getName`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${authData.token}`,
        },
      });

      if (!response.ok) {
        if (!isTried) {
          isTried = true;
          await refreshToken(authData.refreshToken);
          authData = await getAuthData();
          return fetchName();
        }
        return null;
      }

      const data = await response.json();
      return data.userName ?? null;
    } catch (e) {
      console.error("ERROR:", e);
      return null;
    }
  };

  return await fetchName();
}
