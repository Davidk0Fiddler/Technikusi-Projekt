async function refreshToken() {
  const refreshToken = sessionStorage.getItem("RefreshToken");

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
  sessionStorage.setItem("Token", data.token);
  sessionStorage.setItem("RefreshToken", data.refreshToken);

  return true;
}

export default refreshToken;