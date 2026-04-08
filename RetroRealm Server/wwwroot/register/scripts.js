import { texts } from "./lang.js";
import Register from "./Register.js";

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

  const nyelv = document.getElementById("nyelvvalt");
  nyelv.textContent = texts.nyelvvalt[lang];

  const beleph1 = document.getElementById("regist-h1");
  beleph1.textContent = texts.registH1[lang];

  const usernameInputArea = document.getElementById("username-input");
  usernameInputArea.placeholder = texts.usernameInput[lang];

  const passwordInputArea = document.getElementById("password-input");
  passwordInputArea.placeholder = texts.passwrodInput[lang];

  const passwordAgainInputArea = document.getElementById(
    "password-again-input",
  );
  passwordAgainInputArea.placeholder = texts.passwordAgainInput[lang];

  const gomb = document.getElementById("register-btn");
  gomb.textContent = texts.regisztracio[lang];

  const regszov = document.getElementById("szin2");
  regszov.innerHTML = texts.szin2[lang];

  const link2 = document.getElementById("link");
  link2.textContent = texts.link[lang];
}

const langbutton = document.getElementById("nyelvvalt");
langbutton.addEventListener("click", () => {
  console.log("click");
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

setLanguage(localStorage.getItem("lang") ?? "eng");

document
  .getElementById("register-form")
  .addEventListener("submit", async (event) => {
    event.preventDefault();

    const username = document.getElementById("username-input").value;
    const password = document.getElementById("password-input").value;
    const passwordAgain = document.getElementById("password-again-input").value;

    if (password != passwordAgain) {
      alert("A jelszavak nem egyeznek!");
    } else {
      await Register(username, password).then((status) => {
        if (status === 204) {
          alert(
            texts.successfullRegisterAlertMessage[
              localStorage.getItem("lang") ?? "eng"
            ],
          );

          window.location.href = "../login/index.html";
        } else {
          alert(
            texts.unSuccessfullRegisterAlertMessage[
              localStorage.getItem("lang") ?? "eng"
            ],
          );
        }
      });
    }
  });
