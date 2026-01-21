async function refreshToken(refreshToken) {

  if (!refreshToken) {
    // console.error("No refresh token");
    return false;
  }

  const response = await fetch("https://localhost:7234/api/Auth/refreshToken", {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify({
      Token: refreshToken
    })
  });

  if (!response.ok) {
    const errorData = await response.json();

    if (errorData.errors) {
      const firstKey = Object.keys(errorData.errors)[0];
      const message = errorData.errors[firstKey][0];
      // console.error("Refresh token error:", message);
    } else {
      // console.error("Refresh token error:", errorData.title);
    }

    return false;
  }

  const data = await response.json();
  sessionStorage.setItem("Token", data.token);
  sessionStorage.setItem("RefreshToken", data.refreshToken);

  return true;
}

export default refreshToken;
