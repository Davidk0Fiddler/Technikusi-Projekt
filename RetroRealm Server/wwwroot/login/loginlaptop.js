import { texts } from "./lang.js";

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
  const loginh1 = document.getElementById("login-h1");
  loginh1.textContent = texts.loginH1[lang];
  const inputtext = document.getElementById("inputtext");
  inputtext.placeholder = texts.inputtext[lang];
  const inputtext2 = document.getElementById("inputpass");
  inputtext2.placeholder = texts.inputpass[lang];
  const gomb = document.getElementById("belepes");
  gomb.textContent = texts.belepes[lang];
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

const hunbutton = document.getElementById("hun");
hunbutton.addEventListener("click", () => {
  setLanguage("hu");
  document.getElementById("langselection").style.display = "none";
});

const engbutton = document.getElementById("eng");
engbutton.addEventListener("click", () => {
  setLanguage("en");
  document.getElementById("langselection").style.display = "none";
});

const espbutton = document.getElementById("esp");
espbutton.addEventListener("click", () => {
  setLanguage("esp");
  document.getElementById("langselection").style.display = "none";
});
