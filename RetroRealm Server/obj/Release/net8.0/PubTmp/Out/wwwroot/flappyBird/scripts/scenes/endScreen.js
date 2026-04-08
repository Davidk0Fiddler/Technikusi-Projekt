import k from "../kaplayCtx.js";
import sprites from "../sprites.js";
import scoreState from "../scoreNum.js";
import gameScene from "./gameScene.js";
import menuScene from "./menuScene.js";
import checkRecord from "../apiScripts/checkRecord.js";

loadBitmapFont("font", "./assets/font.png", 36, 45);

// Háttér kirajzolása
function setBackground() {
    k.add([
        sprite("background"),
        pos(0, 0),
        z(0),
    ]);
}

// Talaj kirajzolása
function setGround() {
    k.add([
        sprite("ground"),
        pos(0, k.height() - 56),
        z(1),
    ]);
}

// Game Over felirat
function setGameOverLabel() {
    k.add([
        sprite("gameOverLabel"),
        pos(k.width() / 2, k.height() / 2 - 50),
        anchor("center"),
        z(2),
    ]);
}

// Score panel
function setGameOverScorePanel() {
    k.add([
        sprite("gameOverScorePanel"),
        pos(k.width() / 2, k.height() / 2),
        anchor("center"),
        z(2),
    ]);
}

// Aktuális score szöveg
function setScoreText() {
    k.add([
        text(`Score: ${scoreState.score}`, {
            size: 10,
            font: "font",
        }),
        pos(k.width() / 2, k.height() / 2 - 15),
        anchor("center"),
        z(3),
    ]);
}

// Menü gomb
function displayMenuButton() {
    const menuButton = k.add([
        sprite("menuButton"),
        anchor("center"),
        pos(k.width() / 2 - 25, k.height() / 2 + 5),
        z(3),
        area(),
    ]);

    menuButton.onHoverUpdate(() => {
        k.setCursor('url("./assets/pointer.png") 11 13, auto');
        menuButton.scale = vec2(1.01, 1.01);
    });

    menuButton.onHoverEnd(() => {
        k.setCursor('url("./assets/cursor.png") 11 13, auto');
        menuButton.scale = vec2(1, 1);
    });

    menuButton.onClick(() => k.go("menuScene"));
}

// Restart gomb
function displayRestartButton() {
    const playButton = k.add([
        sprite("restartButton"),
        anchor("center"),
        pos(k.width() / 2 + 25, k.height() / 2 + 5),
        z(3),
        area(),
    ]);

    playButton.onHoverUpdate(() => {
        k.setCursor('url("./assets/pointer.png") 11 13, auto');
        playButton.scale = vec2(1.01, 1.01);
    });

    playButton.onHoverEnd(() => {
        k.setCursor('url("./assets/cursor.png") 11 13, auto');
        playButton.scale = vec2(1, 1);
    });

    playButton.onClick(() => k.go("gameScene"));
}

// Rekord ellenőrzése és megjelenítése
async function recordDisplayer() {
    const isNewRecord = await checkRecord();

    if (isNewRecord) {
        k.add([
            sprite("newRecordLabel"),
            anchor("center"),
            pos(k.width() / 2, k.height() / 2 - 25),
            z(10),
        ]);
    }
}

export default scene("endScreen", async () => {
    k.setCursor('url("./assets/cursor.png") 11 13, auto');

    setBackground();
    setGround();
    setGameOverLabel();
    setGameOverScorePanel();
    setScoreText();
    displayRestartButton();
    displayMenuButton();

    // Async rekord ellenőrzés
    await recordDisplayer();
});
