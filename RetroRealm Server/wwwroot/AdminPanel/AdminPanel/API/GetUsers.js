import getAuthData from "../../../scripts/getAuthData.js";
import refreshToken from "../../../scripts/tokenRefresher.js";


export default async function GetUsers() {
    const ENDPOINT = "https://localhost:7234/users";
    let authData = await getAuthData();
    let isTried = false;

    const fetchUsers = async () => {
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
            return fetchUsers();
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
  return await fetchUsers();
}