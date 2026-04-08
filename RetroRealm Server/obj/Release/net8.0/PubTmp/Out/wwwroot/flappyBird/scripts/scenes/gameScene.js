import k from "../kaplayCtx.js";
import { Background } from "../objects/background.js";
import { Ground } from "../objects/ground.js";
import { Bird } from "../objects/bird.js";
import { PipeGroup } from "../objects/pipes.js";
import { ScoreBoard } from "../objects/score.js";
import endScreen from "../scenes/endScreen.js";

// Get Ready felirat
function displayGetReadyLabel() {
    getReadyLabel = k.add([
        sprite("getReadyLabel"),
        anchor("center"),
        pos(k.width() / 2, k.height() / 2),
        z(10),
    ]);
}

// Tap ikon (indítás előtt)
function displayTapTapIcon() {
    tapTapIcon = k.add([
        sprite("tapTapIcon"),
        anchor("center"),
        pos(k.width() / 2, k.height() / 2),
        z(10),
        scale(0),
    ]);
}

// Scene alapállapot
function sceneInit() {
    time = 0;
    countDownNumber = 3;
    isStarting = true;
    k.setGravity(0);
}

// Játék logika indítása
function Game() {
    k.setGravity(300);

    // Bird jump input
    gameClick = k.onClick(() => {
        bird.birdJump(pipes, background, ground, scoreBoard);
    });

    // Pipe generálás
    pipes.displayPipes();

    k.onUpdate(() => {
        pipes.movePipes();
        scoreBoard.displayScore();
    });

    // Score növelés
    k.onCollide("bird", "scoreZone", (zone) => {
        if (!zone.passed) scoreBoard.increaseScore();
    });

    // Ütközés csővel → game over
    k.onCollide("bird", "pipe", () => {
        bird.disableJump();
        pipes.setPipeSpeed(0);
        background.setBackgroundSpeed(0);
        ground.setGroundSpeed(0);
        scoreBoard.setStateScore();
        k.wait(2, () => k.go("endScreen"));
    });
}

// Scene state változók
var gameClick;
var startClick;
var getReadyLabel;
var tapTapIcon;
var countDownDigitSprite;
var isStarting = true;
var time = 0;
var countDownNumber = 3;
var isTapTapBig = false;

// Game objektumok
var bird;
var ground;
var background;
var pipes;
var scoreBoard;

export default scene("gameScene", () => {
    sceneInit();
    document.querySelector("canvas").style.cursor = "none";

    // Objektumok létrehozása
    background = new Background();
    ground = new Ground();
    bird = new Bird();
    pipes = new PipeGroup();
    scoreBoard = new ScoreBoard();

    displayGetReadyLabel();
    displayTapTapIcon();
    bird.birdInit();

    // Háttér és ground animáció
    k.onUpdate(() => {
        ground.displayGround();
        background.displayBackground();
    });

    // Indulás előtti countdown
    if (isStarting) {
        loop(1, () => {
            time += 1;

            if (time < 1) {
                displayGetReadyLabel();
            }

            if (time > 1 && time < 5) {
                getReadyLabel.destroy();

                if (countDownNumber < 3) {
                    countDownDigitSprite.destroy();
                }

                countDownDigitSprite = k.add([
                    sprite(`digit${countDownNumber}`),
                    pos(k.width() / 2, k.height() / 2),
                    anchor("center"),
                    z(10),
                ]);

                countDownNumber -= 1;
            }

            if (time >= 5) {
                countDownDigitSprite.destroy();
            }
        });

        // Tap ikon pulzálás
        loop(0.3, () => {
            if (isStarting && time >= 5) {
                tapTapIcon.scale = isTapTapBig
                    ? vec2(1.0, 1.0)
                    : vec2(1.01, 1.01);
                isTapTapBig = !isTapTapBig;
            }
        });

        // Játék indítása clickre
        startClick = k.onClick(() => {
            if (time >= 5) {
                isStarting = false;
                tapTapIcon.destroy();
                startClick.cancel();
                Game();
            }
        });
    }
});
