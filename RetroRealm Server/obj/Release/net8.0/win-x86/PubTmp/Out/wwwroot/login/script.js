import { texts } from "./lang.js";
import Login from "./Login.js";

const container = document.getElementById("particles");

for (let i = 0; i < 60; i++) {
  const p = document.createElement("div");
  p.className = "particle";
  p.style.left = Math.random() * 100 + "vw";
  p.style.animationDuration = 5 + Math.random() * 10 + "s";
  p.style.animationDelay = Math.random() * 10 + "s";
  container.appendChild(p);
}

function setLanguage(lang) {
  localStorage.setItem("lang", lang);
  document.getElementById("langselection").style.display = "none";

  const loginh1 = document.getElementById("login-h1");
  loginh1.textContent = texts.loginH1[lang];

  const userNameInputArea = document.getElementById("username-input");
  userNameInputArea.placeholder = texts.usernameInput[lang];

  const passwordInputArea = document.getElementById("password-input");
  passwordInputArea.placeholder = texts.passwordInput[lang];

  const loginBtn = document.getElementById("belepes-btn");
  loginBtn.textContent = texts.belepes[lang];

  const regszov = document.getElementById("szin");
  regszov.innerHTML = texts.szin[lang];

  const link2 = document.getElementById("link");
  link2.textContent = texts.link[lang];

  const nyelv = document.getElementById("nyelvvalt");
  nyelv.textContent = texts.nyelvvalt[lang];
}

const langbutton = document.getElementById("nyelvvalt");
langbutton.addEventListener("click", () => {
  document.getElementById("langselection").style.display = "block";

  document.getElementById("langselection").addEventListener("mouseover", () => {
    document.getElementById("langselection").style.display = "block";
  });

  document.getElementById("langselection").addEventListener("mouseout", () => {
    document.getElementById("langselection").style.display = "none";
  });
});

document
  .getElementById("hun")
  .addEventListener("click", () => setLanguage("hun"));

document
  .getElementById("eng")
  .addEventListener("click", () => setLanguage("eng"));

document
  .getElementById("esp")
  .addEventListener("click", () => setLanguage("esp"));

document
  .getElementById("login-form")
  .addEventListener("submit", async (event) => {
    event.preventDefault();

    const username = document.getElementById("username-input").value;
    const password = document.getElementById("password-input").value;

    await Login(username, password).then((status) => {
      if (status === 200) {
        alert(
          texts.successFullLoginAlertMessage[
            localStorage.getItem("lang") ?? "eng"
          ],
        );

        window.location.href = "../landing_page/landing.html";
      } else {
        alert(
          texts.unSuccessFullLoginAlertMessage[
            localStorage.getItem("lang") ?? "eng"
          ],
        );
      }
    });
  });

setLanguage(localStorage.getItem("lang") ?? "eng");
