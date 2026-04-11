import { texts } from "./lang_achiev.js";
import GetAchievements from "./GetAchievements.js";
import baseUrl from "../scripts/baseURL.js";

function SetLanguage(lang_achiev) {
  localStorage.setItem("lang", lang_achiev);

  document.getElementById("lang-selection").style.display = "none";

  const nyelv = document.getElementById("lang-button");
  nyelv.textContent = texts.langselection[lang_achiev];
  const cim = document.getElementById("title");
  cim.textContent = texts.merfold[lang_achiev];

  DisplayAchievements();
}

const langbutton = document.getElementById("lang-button");
langbutton.addEventListener("click", () => {
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

document
  .getElementById("hun")
  .addEventListener("click", () => SetLanguage("hun"));

document
  .getElementById("eng")
  .addEventListener("click", () => SetLanguage("eng"));

document
  .getElementById("esp")
  .addEventListener("click", () => SetLanguage("esp"));

async function DisplayAchievements() {
  var list = await GetAchievements();

  const itemList = document.getElementById("item-list");
  itemList.innerHTML = " ";
  var lang = localStorage.getItem("lang") ?? "Eng";
  var langa = lang.charAt(0).toUpperCase() + lang.slice(1);
  list.forEach((achievement) => {
    itemList.innerHTML += `
        <div class="item">
    <p class="achievement-name">${achievement[`name${langa}`]}</p>
    <p class="achievement-requirement">${achievement[`requirement${langa}`]}</p>
    <p class="achievement-prize">${texts.prize[localStorage.getItem("lang") ?? "eng"]}: ${achievement.prizeCoin} C</p>
        </div>`;
  });
}
DisplayAchievements();

document.getElementById("exit-page-btn").addEventListener("click", () => {
  window.location.href = baseUrl;
});

SetLanguage(localStorage.getItem("lang") ?? "eng");
