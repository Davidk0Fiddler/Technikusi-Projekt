import { texts } from "./lang.js";
import GetAvatars from "./GetAvatars.js";
import PurchaseAvatar from "./PurchaseAvatar.js";
import baseURL from "../scripts/baseURL.js";

var list = await GetAvatars();

const langButton = document.getElementById("lang-button");
function setLanguage(lang) {
  localStorage.setItem("lang", lang);

  document.getElementById("lang-selection").style.display = "none";
  langButton.textContent = texts.langButton[lang];
  list.forEach((avatar) => {
    document.getElementById(`${avatar.avatarName}-id`).textContent =
      texts[avatar.avatarName][lang];
  });

  document
    .querySelectorAll(".buy")
    .forEach((button) => (button.textContent = texts.buyButtonText[lang]));
}

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

document
  .getElementById("eng")
  .addEventListener("click", () => setLanguage("eng"));

document
  .getElementById("hun")
  .addEventListener("click", () => setLanguage("hun"));

document
  .getElementById("esp")
  .addEventListener("click", () => setLanguage("esp"));

document
  .getElementById("exit-page-btn")
  .addEventListener("click", () => (window.location.href = baseURL));

function DisplayItems() {
  const itemList = document.getElementById("item-list");
  itemList.innerHTML = ` `;

  list.forEach((avatar) => {
    itemList.innerHTML += `<div class="item">
          <img src="${avatar.avatarName}.png" alt="${avatar.avatarName}" />
          <h2 class="item-name" id='${avatar.avatarName}-id'>${avatar.avatarName}</h2>
          <p class="item-price">${avatar.price} coins</p>
          <button class="buy" onclick="Purchase('${avatar.avatarName}')">Buy</button>
        </div>`;
  });
}

window.Purchase = async function (avatarName) {
  alert(await PurchaseAvatar(avatarName));
};

DisplayItems();
setLanguage(localStorage.getItem("lang") ?? "eng");
