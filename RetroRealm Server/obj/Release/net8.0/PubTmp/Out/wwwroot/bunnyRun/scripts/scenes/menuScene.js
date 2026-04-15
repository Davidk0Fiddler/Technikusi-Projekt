import k from "../kaplayCtx.js";
import {
  background,
  spriteSheet,
  bunnySpriteSheet,
  foxSheet,
} from "../sprites.js";
import gameScene from "./gameScene.js";
import baseURL from "../../../scripts/baseURL.js";

// Menü háttér kirajzolása
function DisplayBackground() {
  k.add([sprite("background"), pos(0, 0), scale(0.8), z(0)]);
}

// Játék cím / logó megjelenítése
function DisplayBunnyRunLabel() {
  k.add([
    sprite("bunnyRunLabel"),
    anchor("center"),
    scale(10),
    z(3),
    pos(k.width() / 2, 200),
  ]);
}

// Felső sarokban lévő menü ikon (jövőbeli funkcióknak)
function DisplayMenuButton() {
  const menuButton = k.add([
    sprite("menuButton"),
    scale(4),
    z(3),
    pos(5, 5),
    area(),
  ]);

  // Hover effekt + kurzor csere
  menuButton.onHoverUpdate(() => {
    k.setCursor('url("./assets/pointer.png") 11 13, auto');
    menuButton.scale = vec2(4.05, 4.05);
  });

  menuButton.onHoverEnd(() => {
    k.setCursor('url("./assets/cursor.png") 11 13, auto');
    menuButton.scale = vec2(4, 4);
  });

  menuButton.onClick(() => {
    window.location.href = baseURL;
  });
}

// Play gomb – játék indítása
function DisplayPlayButton() {
  const playButton = k.add([
    sprite("playButton"),
    scale(4),
    z(3),
    anchor("botright"),
    pos(k.width() / 2 - 50, k.height() / 2 - 30),
    area(),
  ]);

  playButton.onHoverUpdate(() => {
    k.setCursor('url("./assets/pointer.png") 11 13, auto');
    playButton.scale = vec2(4.05, 4.05);
  });

  playButton.onHoverEnd(() => {
    k.setCursor('url("./assets/cursor.png") 11 13, auto');
    playButton.scale = vec2(4, 4);
  });

  playButton.onClick(() => {
    k.go("gameScene");
  });
}

// Leaderboard gomb (későbbi funkció)
function DisplayLeaderBoardButton() {
  const leaderBoardButton = k.add([
    sprite("leaderBoardButton"),
    scale(4),
    z(3),
    anchor("botleft"),
    pos(k.width() / 2 + 50, k.height() / 2 - 30),
    area(),
  ]);

  leaderBoardButton.onHoverUpdate(() => {
    k.setCursor('url("./assets/pointer.png") 11 13, auto');
    leaderBoardButton.scale = vec2(4.05, 4.05);
  });

  leaderBoardButton.onHoverEnd(() => {
    k.setCursor('url("./assets/cursor.png") 11 13, auto');
    leaderBoardButton.scale = vec2(4, 4);
  });

  leaderBoardButton.onClick(() => {
    window.location.href = `${baseURL}/leaderboard/leaderboard.html?leaderboard=bunnyrun`;
  });
}

// Dekoratív nyúl animáció a menüben
function DisplayBunny() {
  const bunny = k.add([
    sprite("bunny"),
    scale(3),
    z(3),
    anchor("center"),
    pos(k.width() / 2, k.height() - 175),
  ]);

  bunny.play("run");
}

export default scene("menuScene", () => {
  // Menü elemek felépítése
  DisplayBackground();
  DisplayBunnyRunLabel();
  DisplayMenuButton();
  DisplayPlayButton();
  DisplayLeaderBoardButton();
  DisplayBunny();
});
