import logout from "./logout.js";

const loginBtn = document.getElementById("loginBtn");
loginBtn.addEventListener("click", () => {window.location.href = "../htmls/login.html"});

const memoryGame = document.getElementById("memoryGame");
memoryGame.addEventListener("click", () => {window.location.href = "../memory-cards/index.html"});

const wordleGame = document.getElementById("wordle");
wordleGame.addEventListener("click", () => { window.location.href = "../wordle/index.html" }); 

const flappyBird = document.getElementById("flappyBird");
flappyBird.addEventListener("click", () => { window.location.href = "../flappyBird/index.html" });

const bunnyRun = document.getElementById("bunnyRun");
bunnyRun.addEventListener("click", () => { window.location.href = "../bunnyRun/index.html" });

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