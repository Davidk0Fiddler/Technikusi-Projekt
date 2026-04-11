import baseURL from "../scripts/baseURL.js";
import GetLeaderboards from "./GetLeaderBoards.js";
import { texts } from "./lang.js";

const leaderboards = await GetLeaderboards();
const contentArea = document.getElementById("content");
const dropdownBtn = document.getElementById("dropbtn");

var currentLeaderboard = " ";

const params = new URLSearchParams(window.location.search);
const inputLeaderboardName = params.get("leaderboard") ?? undefined;

switch (inputLeaderboardName) {
  case "bunnyrun":
    DisplayBunnyRunLeaderBoard();
    break;
  case "flappybird":
    DisplayFlappyBirdLeaderBoard();
    break;
  case "memorygametime":
    DisplayMemoryGameTimeLeaderBoard();
    break;
  case "memorygameflips":
    DisplayMemoryGameFlipsLeaderBoard();
    break;
  case "wordle":
    DisplayWordleLeaderBoard();
    break;
  case "achievement":
    DisplayAchievementLeaderBoard();
    break;
  default:
    break;
}

function setLanguage(lang) {
  localStorage.setItem("lang", lang);
  document.getElementById("lang-selection").style.display = "none";

  const leadboardTitle = document.getElementById("leaderboard-title");
  const langButton = document.getElementById("lang-button");

  document.getElementById("dropbtn").textContent =
    texts.chooseLeaderboard[lang];

  document.getElementById("bunny-run-btn").textContent =
    texts.bunnyRunLeaderboard[localStorage.getItem("lang")];

  document.getElementById("flappy-bird-btn").textContent =
    texts.flappyBirdLeaderboard[localStorage.getItem("lang")];

  document.getElementById("memory-game-flips-btn").textContent =
    texts.memoryGameFlipsLeaderboard[localStorage.getItem("lang")];

  document.getElementById("memory-game-time-btn").textContent =
    texts.memoryGameTimeLeaderboard[localStorage.getItem("lang")];

  document.getElementById("wordle-btn").textContent =
    texts.wordleLeaderboard[localStorage.getItem("lang")];

  document.getElementById("achievements-btn").textContent =
    texts.achievementLeaderboard[localStorage.getItem("lang")];

  leadboardTitle.textContent = texts.leaderboardTitle[lang];
  langButton.textContent = texts.langButton[lang];

  switch (currentLeaderboard) {
    case "bunnyrun":
      DisplayBunnyRunLeaderBoard();
      break;
    case "flappybird":
      DisplayFlappyBirdLeaderBoard();
      break;
    case "memorygametime":
      DisplayMemoryGameTimeLeaderBoard();
      break;
    case "memorygameflips":
      DisplayMemoryGameFlipsLeaderBoard();
      break;
    case "wordle":
      DisplayWordleLeaderBoard();
      break;
    case "achievement":
      DisplayAchievementLeaderBoard();
      break;
    default:
      break;
  }
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
  .getElementById("bunny-run-btn")
  .addEventListener("click", DisplayBunnyRunLeaderBoard);

document
  .getElementById("flappy-bird-btn")
  .addEventListener("click", DisplayFlappyBirdLeaderBoard);

document
  .getElementById("memory-game-time-btn")
  .addEventListener("click", DisplayMemoryGameTimeLeaderBoard);

document
  .getElementById("memory-game-flips-btn")
  .addEventListener("click", DisplayMemoryGameFlipsLeaderBoard);

document
  .getElementById("wordle-btn")
  .addEventListener("click", DisplayWordleLeaderBoard);

document
  .getElementById("achievements-btn")
  .addEventListener("click", DisplayAchievementLeaderBoard);

document
  .getElementById("exit-page-btn")
  .addEventListener("click", () => (window.location.href = baseURL));

function DisplayBunnyRunLeaderBoard() {
  const bunnyRunLeaderboard = leaderboards.BunnyRunLeaderboard;
  currentLeaderboard = "bunnyrun";

  dropdownBtn.textContent =
    texts.bunnyRunLeaderboard[localStorage.getItem("lang")];

  contentArea.innerHTML = " ";

  contentArea.innerHTML += `
        <div class="item">
          <div class="username"><u>${texts.username[localStorage.getItem("lang")]}</u></div>
          <div class="record"><u>${texts.maxDistance[localStorage.getItem("lang")]}</u></div>
        </div>`;

  bunnyRunLeaderboard.forEach((element) => {
    contentArea.innerHTML += `
        <div class="item">
          <div class="username">${element.userName}</div>
          <div class="record">${element.maxDistance}</div>
        </div>`;
  });
}

function DisplayFlappyBirdLeaderBoard() {
  const flappybirdLeaderboard = leaderboards.FlappyBirdLeaderboard;
  currentLeaderboard = "flappybird";

  dropdownBtn.textContent =
    texts.flappyBirdLeaderboard[localStorage.getItem("lang")];

  contentArea.innerHTML = " ";

  contentArea.innerHTML += `
        <div class="item">
          <div class="username"><u>${texts.username[localStorage.getItem("lang")]}</u></div>
          <div class="record"><u>${texts.maxPassedPipes[localStorage.getItem("lang")]}</u></div>
        </div>`;

  flappybirdLeaderboard.forEach((element) => {
    contentArea.innerHTML += `
        <div class="item">
          <div class="username">${element.userName}</div>
          <div class="record">${element.maxPassedPipes}</div>
        </div>`;
  });
}

function DisplayMemoryGameTimeLeaderBoard() {
  const memoryGameTimeLeaderboard = leaderboards.MemoryGameTimeLeaderboard;
  currentLeaderboard = "memorygametime";

  dropdownBtn.textContent =
    texts.memoryGameTimeLeaderboard[localStorage.getItem("lang")];

  contentArea.innerHTML = " ";

  contentArea.innerHTML += `
        <div class="item">
          <div class="username"><u>${texts.username[localStorage.getItem("lang")]}</u></div>
          <div class="record"><u>${texts.time[localStorage.getItem("lang")]}</u></div>
        </div>`;

  memoryGameTimeLeaderboard.forEach((element) => {
    contentArea.innerHTML += `
        <div class="item">
          <div class="username">${element.userName}</div>
          <div class="record">${element.minTime[0]}${texts.hours[localStorage.getItem("lang")]} : ${element.minTime[1]}${texts.minutes[localStorage.getItem("lang")]} : ${element.minTime[2]}${texts.seconds[localStorage.getItem("lang")]}</div>
        </div>`;
  });
}

function DisplayMemoryGameFlipsLeaderBoard() {
  const memoryGameTimeLeaderboard = leaderboards.MemoryGameFlipsLeaderboard;
  currentLeaderboard = "memorygameflips";

  dropdownBtn.textContent =
    texts.memoryGameFlipsLeaderboard[localStorage.getItem("lang")];

  contentArea.innerHTML = " ";

  contentArea.innerHTML += `
        <div class="item">
          <div class="username"><u>${texts.username[localStorage.getItem("lang")]}</u></div>
          <div class="record"><u>${texts.flips[localStorage.getItem("lang")]}</u></div>
        </div>`;

  memoryGameTimeLeaderboard.forEach((element) => {
    contentArea.innerHTML += `
        <div class="item">
          <div class="username">${element.userName}</div>
          <div class="record">${element.minFlipping} ${texts.flips[localStorage.getItem("lang")]}</div>
        </div>`;
  });
}

function DisplayWordleLeaderBoard() {
  const wordleLeaderboard = leaderboards.WordleLeaderboard;
  currentLeaderboard = "wordle";

  dropdownBtn.textContent =
    texts.wordleLeaderboard[localStorage.getItem("lang")];

  contentArea.innerHTML = " ";

  contentArea.innerHTML += `
        <div class="item">
          <div class="username"><u>${texts.username[localStorage.getItem("lang")]}</u></div>
          <div class="record"><u>${texts.completedWords[localStorage.getItem("lang")]}</u></div>
        </div>`;

  wordleLeaderboard.forEach((element) => {
    contentArea.innerHTML += `
        <div class="item">
          <div class="username">${element.userName}</div>
          <div class="record">${element.completedWords}</div>
        </div>`;
  });
}

function DisplayAchievementLeaderBoard() {
  const achievementLeaderboard = leaderboards.AchievementLeaderboard;
  currentLeaderboard = "achievement";

  dropdownBtn.textContent =
    texts.achievementLeaderboard[localStorage.getItem("lang")];

  contentArea.innerHTML = " ";

  contentArea.innerHTML += `
        <div class="item">
          <div class="username"><u>${texts.username[localStorage.getItem("lang")]}</u></div>
          <div class="record"><u>${texts.completedAchievements[localStorage.getItem("lang")]}</u></div>
        </div>`;

  achievementLeaderboard.forEach((element) => {
    contentArea.innerHTML += `
        <div class="item">
          <div class="username">${element.userName}</div>
          <div class="record">${element.completedAchievements}</div>
        </div>`;
  });
}

setLanguage(localStorage.getItem("lang") ?? "eng");
