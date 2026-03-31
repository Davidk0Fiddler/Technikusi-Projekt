import { texts } from "./lang.js";

function setLanguage(lang) {
  const leadboardTitle = document.getElementById("leaderboard-title");
  const langButton = document.getElementById("lang-button");
  leadboardTitle.textContent = texts.leaderboardTitle[lang];
  langButton.textContent = texts.langButton[lang];
}

const langButton = document.getElementById("lang-button");
langButton.addEventListener("click", () => {
  document.getElementById("lang-selection").style.display = "block";
  document
    .getElementById("lang-selection")
    .addEventListener("mouseover", () => {
      document.getElementById("lang-selection").style.display = "block";
    });
  document.getElementById("lang-selection").addEventListener("mouseout", () => {
    document.getElementById("lang-selection").style.display = "none";
  });
});

const engButton = document.getElementById("eng");
engButton.addEventListener("click", () => {
  setLanguage("en");
  document.getElementById("lang-selection").style.display = "none";
});
const hunButton = document.getElementById("hun");
hunButton.addEventListener("click", () => {
  setLanguage("hu");
  document.getElementById("lang-selection").style.display = "none";
});
const espButton = document.getElementById("esp");
espButton.addEventListener("click", () => {
  setLanguage("esp");
  document.getElementById("lang-selection").style.display = "none";
});
