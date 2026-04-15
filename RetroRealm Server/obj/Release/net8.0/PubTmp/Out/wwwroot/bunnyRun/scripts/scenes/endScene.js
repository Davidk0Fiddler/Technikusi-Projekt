import k from "../kaplayCtx.js";
import scoreState from "../scoreNum.js";
import { spriteSheet } from "../sprites.js";
import menuScene from "./menuScene.js";
import gameScene from "./gameScene.js";
import CheckRecord from "../api/CheckRecord.js";

// Bitmap font betöltése a score szöveghez
loadBitmapFont("font", "./assets/font.png", 36, 45);

// Statikus háttér kirajzolása az end screenhez
function DisplayBackground() {
  k.add([sprite("background"), pos(0, 0), scale(0.8), z(0)]);
}

// Középső panel (vizuális keret az end screenhez)
function DisplayEndScreenPanel() {
  k.add([
    sprite("endScreenPanel"),
    pos(k.width() / 2, k.height() / 2),
    anchor("center"),
    scale(8),
    z(4),
  ]);
}

// Game Over felirat megjelenítése
function DisplayGameOver() {
  k.add([
    sprite("gameOverLabel"),
    pos(k.width() / 2, k.height() / 2 - 300),
    anchor("center"),
    z(4),
    scale(5),
  ]);
}

// Végső pontszám kiírása bitmap fonttal
function DisplayFinalScore() {
  k.add([
    text(`Score: ${scoreState.score}`, {
      size: 60,
      font: "font",
    }),
    pos(k.width() / 2, k.height() / 2 - 125),
    anchor("center"),
    z(5),
  ]);
}

// Menü gomb (visszalépés főmenübe)
function DisplayMenuButton() {
  const menuButton = k.add([
    sprite("menuButton"),
    pos(k.width() / 2 - 150, k.height() / 2),
    anchor("center"),
    z(5),
    scale(6),
    area(),
  ]);

  // Hover effekt – enyhe nagyítás + kurzor csere
  menuButton.onHoverUpdate(() => {
    k.setCursor('url("./assets/pointer.png") 11 13, auto');
    menuButton.scale = vec2(6.05, 6.05);
  });

  menuButton.onHoverEnd(() => {
    k.setCursor('url("./assets/cursor.png") 11 13, auto');
    menuButton.scale = vec2(6, 6);
  });

  // Vissza a menü scene-re
  menuButton.onClick(() => {
    k.go("menuScene");
  });
}

// Újraindítás gomb (az aktuális játék újratöltése)
function DisplayRestartButton() {
  const restartButton = k.add([
    sprite("restartButton"),
    pos(k.width() / 2 + 150, k.height() / 2),
    anchor("center"),
    z(5),
    scale(6),
    area(),
  ]);

  restartButton.onHoverUpdate(() => {
    k.setCursor('url("./assets/pointer.png") 11 13, auto');
    restartButton.scale = vec2(6.05, 6.05);
  });

  restartButton.onHoverEnd(() => {
    k.setCursor('url("./assets/cursor.png") 11 13, auto');
    restartButton.scale = vec2(6, 6);
  });

  // Játék újraindítása
  restartButton.onClick(() => {
    k.go("gameScene");
  });
}

// Rekord ellenőrzése és visszajelzés megjelenítése
async function RecordDisplayer() {
  const isNewRecord = await CheckRecord();

  if (isNewRecord) {
    k.add([
      sprite("newRecordLabel"),
      anchor("center"),
      pos(k.width() / 2, k.height() / 2 - 175),
      z(10),
      scale(5),
    ]);
  }
}

// End scene felépítése
export default scene("endScene", () => {
  DisplayBackground();
  DisplayEndScreenPanel();
  DisplayFinalScore();
  DisplayGameOver();
  DisplayMenuButton();
  DisplayRestartButton();
  RecordDisplayer();

  k.setCursor('url("./assets/cursor.png") 11 13, auto');
});
