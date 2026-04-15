async function logout() {
  const refreshToken = sessionStorage.getItem("RefreshToken");
  const response = await fetch("https://localhost:7234/api/Logout", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      token: refreshToken,
    }),
  });

  if (response.ok) {
    sessionStorage.removeItem("Token");
    sessionStorage.removeItem("RefreshToken");
    window.location.href = "../index.html";
  }

  console.log(response.status);
}

export default logout;
