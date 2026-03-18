import getAuthData from "../../../scripts/getAuthData.js";
import refreshToken from "../../../scripts/tokenRefresher.js";


export default async function GetLogs() {
    const ENDPOINT = "https://localhost:7234/logs";
    let authData = await getAuthData();
    let isTried = false;

    const fetchLogs = async () => {
      try {
        const response = await fetch(ENDPOINT, {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${authData.token}`
          }
        });

        if(!response.ok){
          if(!isTried){
            isTried = true;
            await refreshToken(authData.refreshToken);
            authData = await getAuthData();
            return fetchLogs();
          }
          return null;
        }

        const data = await response.json();
        return data
    }
    catch(e){
      console.error("ERROR:", e);
      return null;
    }
  }
  return await fetchLogs();
}