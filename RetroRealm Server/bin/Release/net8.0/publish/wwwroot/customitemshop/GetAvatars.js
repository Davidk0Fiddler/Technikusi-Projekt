import baseURL from "../scripts/baseURL.js";

export default async function GetAvatars() {
  const response = await fetch(`${baseURL}/api/Avatars`);
  if (response.ok) {
    return await response.json();
  }
  return [];
}
