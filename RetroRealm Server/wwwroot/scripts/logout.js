async function logout() {
  const refreshToken = sessionStorage.getItem("RefreshToken");
  console.log(refreshToken);
  const response = await fetch("https://localhost:7234/api/Auth/logout", {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify({
      token: refreshToken
    })
  });

  if (response.ok)
  {
    sessionStorage.removeItem("Token");
    sessionStorage.removeItem("RefreshToken");
    window.location.href = "../index.html";
  }

}

export default logout;