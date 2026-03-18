export default function DisplayDetailedUserPanel(user) {
  const blackOverlay = document.getElementById("black-overlay");
  blackOverlay.style.display = "flex";

  const closeDetailedUserPanelBtn = document.getElementById(
    "close-detailed-user-panel-btn",
  );
  closeDetailedUserPanelBtn.addEventListener(
    "click",
    () => (blackOverlay.style.display = "none"),
  );

  const userCurrentCharacter = document.getElementById("character-avatar");
  userCurrentCharacter.src = `./assets/${user.currentAvatarName}.png`;

  const userNameRole = document.getElementById("user-name-role");
  userNameRole.innerHTML = `<h2>${user.username}</h2> (${user.roleName})`;

  const coinCountArea = document.getElementById("coin-count");
  coinCountArea.textContent = user.coins;

  const wordleStatusArea = document.getElementById("wordle-status");
  wordleStatusArea.textContent = `Total words: ${user.wordleStatus.completedWords}`;

  const memoryGameTimeStatus = document.getElementById(
    "memory-game-time-status",
  );
  memoryGameTimeStatus.textContent = `Min-Time: ${user.memoryGameStatus.minTime[0]}h:${user.memoryGameStatus.minTime[1]}m:${user.memoryGameStatus.minTime[2]}s`;

  const memoryGameFlipsStatus = document.getElementById(
    "memory-game-flips-status",
  );
  memoryGameFlipsStatus.textContent = `Min-Flips: ${user.memoryGameStatus.minFlipping}`;

  const bunnyRunStatus = document.getElementById("bunny-run-status");
  bunnyRunStatus.textContent = `Max-Distance: ${user.bunnyRunStatus.maxDistance}`;

  const flappyBirdStatus = document.getElementById("flappy-bird-status");
  flappyBirdStatus.textContent = `Max-Passed-Pipes: ${user.flappyBirdStatus.maxPassedPipes}`;

  const avatarGallery = document.getElementById("avatar-gallery");
  console.log(user);
  avatarGallery.innerHTML = " ";
  user.ownedAvatarsNames.forEach((avatar) => {
    avatarGallery.innerHTML += `<img src="./assets/${avatar}.png" alt="${avatar}">`;
  });

  const achievementGrid = document.getElementById("achievement-grid");
  achievementGrid.innerHTML = " ";
  user.completedChallangesName.forEach((challange) => {
    achievementGrid.innerHTML += `<div class="achievement">${challange}</div>`;
  });
}
