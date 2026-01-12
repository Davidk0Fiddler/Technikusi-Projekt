async function getUserData() {
    const refreshToken = sessionStorage.getItem("RefreshToken");
    const token = sessionStorage.getItem("Token")
    let userData;
    for (let i = 0; i < 5; i++) {
        const response = await fetch("https://localhost:7234/api/Users/getusersdata", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
            body: JSON.stringify({
                Token: refreshToken.token
            })
        });

        if (response.ok) {
            userData = await response.json();
        }
    }

    return userData;
}

export default getUserData;