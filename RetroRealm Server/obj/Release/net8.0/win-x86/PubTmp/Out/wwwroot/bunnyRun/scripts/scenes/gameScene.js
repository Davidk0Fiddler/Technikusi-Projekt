import k from "../kaplayCtx.js";
import { Background } from "../objects/background.js";
import { Bunny } from "../objects/bunny.js";
import { Fox } from "../objects/fox.js";
import { ScoreBoard } from "../objects/score.js";
import { Vultures } from "../objects/vulture.js";
import endScene from "./endScene.js";

// Statikus talaj létrehozása (amin a játékos állhat)
function SetGround() {
  k.add([
    pos(0, k.height() - 130),

    // Szélesebb, mint a képernyő, hogy biztosan lefedje a játékteret
    rect(k.width() * 2, 130, { fill: false }),
    area(),

    // Statikus fizikai test (nem mozog, nem esik)
    body({ isStatic: true }),
  ]);
}

export default scene("gameScene", () => {
  // Alap gravitáció beállítása
  k.setGravity(300);

  SetGround();

  // Játék közben a kurzor ne látszódjon
  k.setCursor("none");

  // Egyszerű game state flag
  let isInGame = true;

  // Játék objektumok inicializálása
  const background = new Background();
  const bunny = new Bunny();
  const foxes = new Fox();
  const vultures = new Vultures();
  const scoreBoard = new ScoreBoard();

  // Alap animáció induláskor
  bunny.PlayAnim("run");

  // Kattintás = ugrás (csak ha talajon van és él a játék)
  k.onClick(() => {
    if (bunny.object.isGrounded() && isInGame) {
      bunny.object.jump(300);
      bunny.PlayAnim("jump");

      // Egyszerű időzített animáció váltások
      k.wait(1, () => bunny.PlayAnim("fall"));
      k.wait(2, () => bunny.PlayAnim("run"));
    }
  });

  // Ellenségek spawnolása időközönként
  k.loop(2, () => {
    // Véletlenszerűen földi vagy légi ellenfél
    k.rand(1) < 0.5 ? foxes.GenerateFox() : vultures.GenerateVulture();

    // Régi, kifutott ellenfelek takarítása
    foxes.DeleteFoxes();
    vultures.DeleteVultures();
  });

  // Pontszám növelése időalapon
  k.loop(1, () => {
    scoreBoard.IncreaseScore();
  });

  // Játékos–ellenség ütközés = game over
  k.onCollide("player", "enemy", () => {
    isInGame = false;

    // Score átadása az end scene-nek
    scoreBoard.SetStateScore();

    k.go("endScene");
  });

  // Fő update loop
  k.onUpdate(() => {
    if (isInGame) {
      background.DisplayBackground();
      foxes.MoveFoxes();
      vultures.MoveVultures();
      scoreBoard.DisplayScore();
    }
  });
});
