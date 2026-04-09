export default async function GetAvatars() {
  const response = await fetch("https://localhost:7234/api/GetAchievements");
  if (response.ok) {
    return await response.json();
  }
  return [];
}
