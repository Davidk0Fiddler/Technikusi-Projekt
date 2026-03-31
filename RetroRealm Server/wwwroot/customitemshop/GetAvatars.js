export default async function GetAvatars() {
  const response = await fetch("https://localhost:7234/api/Avatars");
  if (response.ok) {
    return await response.json();
  }
  return [];
}
