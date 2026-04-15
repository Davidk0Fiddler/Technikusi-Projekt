async function refreshToken(refreshToken) {
  if (!refreshToken || refreshToken == "undefined") {
    // console.error("No refresh token");
    return false;
  }
  console.log(refreshToken);

  const response = await fetch("https://localhost:7234/api/Refreshtoken", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      Token: refreshToken,
    }),
  });

  if (!response.ok) {
    return false;
  }

  const data = await response.json();

  console.log(data.refreshToken);
  console.log(data.refreshToken.token);

  sessionStorage.setItem("Token", data.token);
  sessionStorage.setItem("RefreshToken", data.refreshToken.token);

  return true;
}

export default refreshToken;
