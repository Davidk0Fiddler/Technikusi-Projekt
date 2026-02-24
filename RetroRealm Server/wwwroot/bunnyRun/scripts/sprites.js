import k from "./kaplayCtx.js";

// Kaplay logó / intro sprite
export var kaplayDino = k.loadSprite(
    "kaplay_dino-o",
    "./assets/kaplay_dino-o.png"
);

// Háttérkép (több scene-ben újrahasznált)
export var background = k.loadSprite(
    "background",
    "./assets/bg.png"
);

// UI + statikus elemek sprite atlasza
export var spriteSheet = k.loadSpriteAtlas(
    "./assets/spriteSheet.png",
    {
        // Talaj sprite
        "ground": {
            x: 0,
            y: 858,
            width: 1536,
            height: 166,
        },

        // UI gombok
        "restartButton": { x: 0,  y: 16, width: 37, height: 12 },
        "menuButton":    { x: 0,  y: 29, width: 37, height: 13 },
        "playButton":    { x: 40, y: 16, width: 52, height: 29 },
        "leaderBoardButton": { x: 93, y: 16, width: 52, height: 29 },

        // Számjegyek (score kijelzéshez)
        "digit1": { x: 0,   y: 0, width: 13, height: 14 },
        "digit2": { x: 14,  y: 0, width: 13, height: 14 },
        "digit3": { x: 28,  y: 0, width: 13, height: 14 },
        "digit4": { x: 42,  y: 0, width: 13, height: 14 },
        "digit5": { x: 56,  y: 0, width: 13, height: 14 },
        "digit6": { x: 70,  y: 0, width: 13, height: 14 },
        "digit7": { x: 84,  y: 0, width: 13, height: 14 },
        "digit8": { x: 98,  y: 0, width: 13, height: 14 },
        "digit9": { x: 112, y: 0, width: 13, height: 14 },
        "digit0": { x: 126, y: 0, width: 13, height: 14 },

        // Feliratok / panelek
        "getReadyLabel": {
            x: 0,
            y: 46,
            width: 92,
            height: 26,
        },
        "gameOverLabel": {
            x: 0,
            y: 72,
            width: 96,
            height: 21,
        },
        "endScreenPanel": {
            x: 0,
            y: 94,
            width: 113,
            height: 57,
        },
        "newRecordLabel": {
            x: 0,
            y: 152,
            width: 49,
            height: 8,
        },
        "bunnyRunLabel": {
            x: 0,
            y: 161,
            width: 88,
            height: 24,
        },
    }
);

// Nyúl sprite + animációk
export var bunnySpriteSheet = k.loadSprite(
    "bunny",
    "./assets/bunnySheet.png",
    {
        sliceX: 8,
        sliceY: 2,
        anims: {
            run:  { from: 8, to: 10, loop: true, speed: 7.5 },
            jump: { from: 1, to: 1, loop: true },
            fall: { from: 3, to: 3, loop: true },
        },
    }
);

// Róka ellenség sprite
export var foxSheet = k.loadSprite(
    "fox",
    "./assets/foxSheet.png",
    {
        sliceX: 3,
        sliceY: 1,
        anims: {
            run: { from: 0, to: 2, loop: true, speed: 10 },
        },
    }
);

// Repülő ellenség (keselyű)
export var vultureSheet = k.loadSprite(
    "vulture",
    "./assets/vultureSheet.png",
    {
        sliceX: 4,
        sliceY: 1,
        anims: {
            fly: { from: 0, to: 3, loop: true, speed: 10 },
        },
    }
);
