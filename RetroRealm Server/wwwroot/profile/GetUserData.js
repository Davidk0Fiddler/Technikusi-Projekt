export default async function GetUserData() {
  const response = await fetch("https://localhost:7234/api/GetUserData", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      userName: "admin1",
    }),
  });
  const data = await response.json();
  return data;
}
