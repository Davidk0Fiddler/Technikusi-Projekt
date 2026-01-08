async function checkForm() {
    const username = document.getElementById("usernameInput").value;
    const password = document.getElementById("passwordInput").value;

    const response = await fetch("https://localhost:7234/api/Auth/login", {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(
      {
        Username: username,
        Password: password
      }
    )
    });

    if (!response.ok) {
      console.log("error");
        
    }
    else {
        const data = await response.json();
        console.log("succes");
        sessionStorage.setItem("Token", data.token);
        sessionStorage.setItem("RefreshToken", data.refreshToken.token);
        window.location.href = "../htmls/pc.html";
    }


}

const submitBtn = document.getElementById("submitBtn");
submitBtn.addEventListener("click", checkForm);