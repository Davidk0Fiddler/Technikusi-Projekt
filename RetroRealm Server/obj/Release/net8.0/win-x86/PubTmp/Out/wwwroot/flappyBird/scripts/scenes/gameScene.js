import k from "../kaplayCtx.js";
import { Background } from "../objects/Background.js";
import { Ground } from "../objects/Ground.js";
import { Bird } from "../objects/Bird.js";
import { PipeGroup } from "../objects/Pipes.js";
import { ScoreBoard } from "../objects/Score.js";
import endScreen from "../scenes/endScreen.js";

// Get Ready felirat
function DisplayGetReadyLabel() {
  getReadyLabel = k.add([
    sprite("getReadyLabel"),
    anchor("center"),
    pos(k.width() / 2, k.height() / 2),
    z(10),
  ]);
}

// Tap ikon (indítás előtt)
function DisplayTapTapIcon() {
  tapTapIcon = k.add([
    sprite("tapTapIcon"),
    anchor("center"),
    pos(k.width() / 2, k.height() / 2),
    z(10),
    scale(0),
  ]);
}

// Scene alapállapot
function SceneInit() {
  time = 0;
  countDownNumber = 3;
  isStarting = true;
  k.setGravity(0);
}

function Game() {
  k.setGravity(300);

  gameClick = k.onClick(() => {
    bird.BirdJump(pipes, background, ground, scoreBoard);
  });

  pipes.DisplayPipes();

  k.onUpdate(() => {
    pipes.MovePipes();
    scoreBoard.DisplayScore();
  });

  k.onCollide("bird", "scoreZone", (zone) => {
    if (!zone.passed) scoreBoard.IncreaseScore();
  });

  k.onCollide("bird", "pipe", () => {
    bird.DisableJump();
    pipes.SetPipeSpeed(0);
    background.SetBackgroundSpeed(0);
    ground.SetGroundSpeed(0);
    scoreBoard.SetStateScore();
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
  SceneInit();
  document.querySelector("canvas").style.cursor = "none";

  // Objektumok létrehozása
  background = new Background();
  ground = new Ground();
  bird = new Bird();
  pipes = new PipeGroup();
  scoreBoard = new ScoreBoard();

  DisplayGetReadyLabel();
  DisplayTapTapIcon();
  bird.BirdInit();

  // Háttér és ground animáció
  k.onUpdate(() => {
    ground.DisplayGround();
    background.DisplayBackground();
  });

  // Indulás előtti countdown
  if (isStarting) {
    loop(1, () => {
      time += 1;

      if (time < 1) {
        DisplayGetReadyLabel();
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
        tapTapIcon.scale = isTapTapBig ? vec2(1.0, 1.0) : vec2(1.01, 1.01);
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
