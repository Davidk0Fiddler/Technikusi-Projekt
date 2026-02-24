import getAuthData from "../../../scripts/getAuthData.js";
import refreshToken from "../../../scripts/tokenRefresher.js";

export default async function getRecord() {
    // Visszatérési objektum
    const record = {
        success: false,
        data: {}
    };

    const endPoint = "https://localhost:7234/api/FlappyBirdStatus";
    let isTried = false;

    // Aktuális auth adatok
    let authData = await getAuthData();

    const fetchRecord = async () => {
        try {
            const response = await fetch(endPoint, {
                method: "GET",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${authData.token}`
                }
            });

            // Token lejárt => egyszeri frissítés és újrapróbálás
            if (!response.ok) {
                if (!isTried) {
                    isTried = true;
                    await refreshToken(authData.refreshToken);
                    authData = await getAuthData();
                    return await fetchRecord();
                }
                throw new Error("HTTP error: " + response.status);
            }

            // Sikeres válasz
            record.success = true;
            record.data = await response.json();

        } catch (e) {
            console.error("ERROR:", e);
        }
    };

    await fetchRecord();
    return record;
}
