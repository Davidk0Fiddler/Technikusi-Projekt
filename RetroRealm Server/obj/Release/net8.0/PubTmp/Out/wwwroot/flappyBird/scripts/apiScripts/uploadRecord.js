import baseURL from "../../../scripts/baseURL.js";
import getAuthData from "../../../scripts/getAuthData.js";
import refreshToken from "../../../scripts/tokenRefresher.js";

export default async function UploadRecord(currentRecord) {
  let isTried = false;

  // Aktuális auth adatok
  let authData = await getAuthData();

  while (true) {
    const response = await fetch(`${baseURL}/api/FlappyBirdStatus`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${authData.token}`,
      },
      body: JSON.stringify(currentRecord),
    });

    // Sikeres feltöltés
    if (response.ok) {
      return true;
    }

    // Token lejárt => egyszeri frissítés és retry
    if (!isTried && response.status === 401) {
      await refreshToken(authData.refreshToken);
      authData = await getAuthData();
      isTried = true;
      continue;
    }

    // Egyéb hiba
    throw new Error("HTTP error: " + response.status);
  }
}
