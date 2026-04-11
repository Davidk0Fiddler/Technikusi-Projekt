import baseURL from "../scripts/baseURL.js";

export default async function SwitchAvatar(avatarName) {
  const response = await fetch(`${baseURL}/api/setCharacter`, {
    method: "POST",
    headers: {
      Authorization: `Bearer ${sessionStorage.getItem("Token")}`,
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      characterName: avatarName,
    }),
  });
}
