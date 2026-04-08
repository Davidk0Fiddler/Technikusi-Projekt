const adminNameInputElement = document.getElementById("admin-name-input");
const adminPasswordInputElement = document.getElementById("admin-password-input");
const submitButton = document.getElementById("submit-button");

var isLoading = false;

function LoadingAnimation(isLoading) {
    const loadingContainer = document.getElementById("loading-bg");

    isLoading ? loadingContainer.style.display = "flex" : loadingContainer.style.display = "none";
};

submitButton.addEventListener("click", async () => {
    const username = adminNameInputElement.value;
    const password = adminPasswordInputElement.value;
    
    isLoading = true;
    LoadingAnimation(isLoading);
    
    try {
        const response = await fetch("https://localhost:7234/api/Login", {
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
            alert("Something went of during the login!");
        }
        else {
            const data = await response.json();
            sessionStorage.setItem("Token", data.token);
            sessionStorage.setItem("RefreshToken", data.refreshToken.token);
            window.location.href = "./AdminPanel/index.html";
        }
    } catch(err) {
        console.log(err);
        alert("Something went of during the login!");
    }

    isLoading = false;
    LoadingAnimation(isLoading)
});