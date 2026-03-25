import { texts } from "./lang.js";
import GetAvatars from "./GetAvatars.js";

var list = await GetAvatars();

const langButton = document.getElementById("lang-button");
function setLanguage(lang) {
  langButton.textContent = texts.langButton[lang];
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

function DisplayItems() {
  const itemList = document.getElementById("item-list");
  console.log(itemList);
  itemList.innerHTML = ` `;

  list.forEach((avatar) => {
    console.log(avatar);
    itemList.innerHTML += `<div class="item">
          <img src="${avatar.avatarName}.png" alt="${avatar.avatarName}" />
          <h2 class="item-name">${avatar.avatarName}</h2>
          <p class="item-price">${avatar.price} coins</p>
          <button class="buy" onclick="Purchase(${avatar.avatarName})">Buy</button>
        </div>`;
  });
}

DisplayItems();
