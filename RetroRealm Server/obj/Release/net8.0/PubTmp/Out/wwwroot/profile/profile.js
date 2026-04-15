import { texts } from "./lang.js";
import GetUserData from "./GetUserData.js";
import baseURL from "../scripts/baseURL.js";
import SwitchAvatar from "./SwitchAvatar.js";

function setLanguage(lang) {
  localStorage.setItem("lang", lang);
  document.getElementById("lang-selection").style.display = "none";

  const langButton = document.getElementById("lang-button");
  langButton.textContent = texts.langButton[lang];
  const achievementsTitle = document.getElementById("achievements-head");
  achievementsTitle.textContent = texts.achievementsTitle[lang];
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

function displayUserData(userData) {
  const picture = document.getElementById("profile-pic");
  picture.src = `${userData.currentAvatarName}.png`;
  const name = document.getElementById("name");
  name.textContent = `${userData.userName}`;
  const coins = document.getElementById("coins");
  coins.textContent = `Coins: ${userData.coins}`;

  const ownedAvatars = document.getElementById("avatars");
  ownedAvatars.innerHTML = ``;

  userData.ownedAvatars.forEach((avatar) => {
    ownedAvatars.innerHTML += `
    <img src="${avatar}.png" alt="${avatar}" class="owned-avatar"
    onclick="SwitchAvatar('${avatar}')">`;
  });

  window.SwitchAvatar = async function (avatar) {
    await SwitchAvatar(avatar);
    await Start();
  };

  const words = document.getElementById("words");
  words.textContent = `Words: ${userData.wordleStatus.completedWords}`;

  const mintime = document.getElementById("min-time");
  mintime.textContent = `Min-Time: ${userData.memoryGameStatus.minTime[0]} hours ${userData.memoryGameStatus.minTime[1]} minutes ${userData.memoryGameStatus.minTime[2]} seconds`;
  const minflips = document.getElementById("min-flips");
  minflips.textContent = `Min-Flips: ${userData.memoryGameStatus.minFlipping}`;

  const maxdistance = document.getElementById("max-distance");
  maxdistance.textContent = `Max-Distance: ${userData.bunnyRunStatus.maxDistance}`;

  const maxpassedpipes = document.getElementById("max-passed-pipes");
  maxpassedpipes.textContent = `Max-Passed-Pipes: ${userData.flappyBirdStatus.maxPassedPipes}`;

  const achievements = document.getElementById("achievements");
  achievements.innerHTML = ``;

  userData.completedAchievements.forEach((achievement) => {
    achievements.innerHTML += `
    <p>${achievement.nameEng}</p>`;
  });
}
async function Start() {
  const userData = await GetUserData();

  userData != undefined
    ? await displayUserData(userData)
    : (window.location.href = baseURL);
}

await Start();
