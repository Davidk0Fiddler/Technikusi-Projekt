import k from "../kaplayCtx.js";
import sprites from "../sprites.js";
import gameScene from "./gameScene.js";

// Háttér
function setBackground() {
    k.add([
        sprite("background"),
        pos(0, 0),
        z(0),
    ]);
}

// Talaj
function setGround() {
    k.add([
        sprite("ground"),
        pos(0, k.height() - 56),
        z(1),
    ]);
}

// Flappy Bird cím
function displayFlappyBirdLabel() {
    k.add([
        sprite("flappyBirdLabel"),
        anchor("center"),
        pos(k.width() / 2, 75),
        z(1),
    ]);
}

// Animált madár
function displayBird() {
    const birdElement = k.add([
        sprite("bird"),
        anchor("center"),
        pos(k.width() / 2, 150),
        z(1),
    ]);

    birdElement.play("moving");
}

// Play gomb
function displayPlayButton() {
    const playButton = k.add([
        sprite("playButton"),
        anchor("center"),
        pos(k.width() / 3, 235),
        z(2),
        scale(0.8),
        area(),
        "playButton",
    ]);

    playButton.onHoverUpdate(() => {
        k.setCursor('url("./assets/pointer.png") 11 13, auto');
        playButton.scale = vec2(0.81, 0.81);
    });

    playButton.onHoverEnd(() => {
        k.setCursor('url("./assets/cursor.png") 11 13, auto');
        playButton.scale = vec2(0.8, 0.8);
    });
}

// Leaderboard gomb
function displayLeaderboardButton() {
    const leaderboardButton = k.add([
        sprite("leaderboardButton"),
        anchor("center"),
        pos((k.width() / 3) * 2, 235),
        z(2),
        scale(0.8),
        area(),
    ]);

    leaderboardButton.onHoverUpdate(() => {
        k.setCursor('url("./assets/pointer.png") 11 13, auto');
        leaderboardButton.scale = vec2(0.81, 0.81);
    });

    leaderboardButton.onHoverEnd(() => {
        k.setCursor('url("./assets/cursor.png") 11 13, auto');
        leaderboardButton.scale = vec2(0.8, 0.8);
    });
}

// Menü ikon gomb
function displayMenuButton() {
    const menuButton = k.add([
        sprite("menuButton"),
        pos(1, 1),
        z(1),
        scale(0.8),
        area(),
    ]);

    menuButton.onHoverUpdate(() => {
        k.setCursor('url("./assets/pointer.png") 11 13, auto');
        menuButton.scale = vec2(0.85, 0.85);
    });

    menuButton.onHoverEnd(() => {
        k.setCursor('url("./assets/cursor.png") 11 13, auto');
        menuButton.scale = vec2(0.8, 0.8);
    });
}

export default scene("menuScene", () => {
    setBackground();
    setGround();
    displayFlappyBirdLabel();
    displayBird();
    displayPlayButton();
    displayLeaderboardButton();
    displayMenuButton();

    // Játék indítása
    onClick("playButton", () => go("gameScene"));
});
