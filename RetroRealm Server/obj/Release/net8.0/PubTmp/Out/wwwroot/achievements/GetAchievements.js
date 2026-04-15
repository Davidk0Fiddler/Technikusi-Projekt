import baseURL from "../scripts/baseURL.js";

export default async function GetAchievements() {
  const response = await fetch(`${baseURL}/api/GetAchievements`);
  if (response.ok) {
    return await response.json();
  }
  return [];
}
