async function refreshToken() {
  const refreshToken = localStorage.getItem("RefreshToken");

  if (!refreshToken) {
    return false;
  }

  const response = await fetch("https://localhost:7234/api/Auth/refreshToken", {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify({
      model: refreshToken.token
    })
  });

  if (!response.ok) {
    return false;
  }

  const data = await response.json();

  localStorage.setItem("Token", data.token);
  localStorage.setItem("RefreshToken", data.refreshToken.token);

  return true;
}

export default TokenRefresher;