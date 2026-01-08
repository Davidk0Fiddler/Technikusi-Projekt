import logout from "./logout.js";

const loginBtn = document.getElementById("loginBtn");
loginBtn.addEventListener("click", () => {window.location.href = "../htmls/login.html"});

// const memoryGame = document.getElementById("memoryGame");
// memoryGame.addEventListener("click", () => {window.location.href = "memoryGame"});

const logoutBtn = document.getElementById("logoutBtn");
logoutBtn.addEventListener("click", () => {
    logout();
    sessionStorage.clear("Token");
});



if (sessionStorage.getItem("Token")) {
    loginBtn.style.display = "none";
    logoutBtn.style.display = "block";
}
else {
    logoutBtn.style.display = "none";
    loginBtn.style.display = "block";
}

console.log(sessionStorage.getItem("Token"));
console.log(sessionStorage.getItem("RefreshToken"));