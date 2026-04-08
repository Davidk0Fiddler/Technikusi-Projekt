import k from "./kaplayCtx.js";

let sprites;

// Sprite atlas betöltése
sprites = await k.loadSpriteAtlas("./assets/spritesheet.png", {
    // Background
    "background": {
        x: 0,
        y: 0,
        width: 144,
        height: 256,
    },

    // Ground
    "ground": {
        x: 292,
        y: 0,
        width: 144,
        height: 56,
    },

    // Bird (animált)
    "bird": {
        x: 0,
        y: 483,
        width: 84,
        height: 28,
        sliceX: 3,
        anims: {
            "moving": { from: 0, to: 2, loop: true, speed: 10 },
        },
    },

    // Pipes
    "pipe_top": {
        x: 56,
        y: 323,
        width: 26,
        height: 160,
    },
    "pipe_bottom": {
        x: 84,
        y: 323,
        width: 26,
        height: 160,
    },

    // Digits
    "digit0": { x: 496, y: 60, width: 12, height: 19 },
    "digit1": { x: 136, y: 455, width: 8, height: 19 },
    "digit2": { x: 292, y: 160, width: 12, height: 19 },
    "digit3": { x: 306, y: 160, width: 12, height: 19 },
    "digit4": { x: 320, y: 160, width: 12, height: 19 },
    "digit5": { x: 333, y: 160, width: 14, height: 19 },
    "digit6": { x: 292, y: 184, width: 13, height: 19 },
    "digit7": { x: 306, y: 184, width: 13, height: 19 },
    "digit8": { x: 320, y: 184, width: 13, height: 19 },
    "digit9": { x: 334, y: 184, width: 13, height: 19 },

    // UI elemek
    "flappyBirdLabel": {
        x: 351,
        y: 91,
        width: 90,
        height: 24,
    },
    "playButton": {
        x: 354,
        y: 117,
        width: 53,
        height: 31,
    },
    "leaderboardButton": {
        x: 414,
        y: 117,
        width: 53,
        height: 31,
    },
    "menuButton": {
        x: 462,
        y: 26,
        width: 40,
        height: 13,
    },
    "restartButton": {
        x: 462,
        y: 42,
        width: 39,
        height: 13,
    },
    "getReadyLabel": {
        x: 295,
        y: 59,
        width: 92,
        height: 25,
    },
    "tapTapIcon": {
        x: 292,
        y: 90,
        width: 57,
        height: 50,
    },
    "gameOverLabel": {
        x: 395,
        y: 59,
        width: 96,
        height: 21,
    },
    "gameOverScorePanel": {
        x: 3,
        y: 259,
        width: 113,
        height: 57,
    },
    "newRecordLabel": {
        x: 150,
        y: 263,
        width: 48,
        height: 7,
    },
});

export default sprites;
