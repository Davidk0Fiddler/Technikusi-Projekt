import k from "../kaplayCtx.js";
import { Background } from "../objects/background.js";
import { Bunny } from "../objects/bunny.js";
import { Fox } from "../objects/fox.js";
import { ScoreBoard } from "../objects/score.js";
import { Vultures } from "../objects/vulture.js";
import endScene from "./endScene.js";

// Statikus talaj létrehozása (amin a játékos állhat)
function setGround() {
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

    setGround();

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
    bunny.playAnim("run");

    // Kattintás = ugrás (csak ha talajon van és él a játék)
    k.onClick(() => {
        if (bunny.object.isGrounded() && isInGame) {
            bunny.object.jump(300);
            bunny.playAnim("jump");

            // Egyszerű időzített animáció váltások
            k.wait(1, () => bunny.playAnim("fall"));
            k.wait(2, () => bunny.playAnim("run"));
        }
    });

    // Ellenségek spawnolása időközönként
    k.loop(2, () => { 
        // Véletlenszerűen földi vagy légi ellenfél
        k.rand(1) < 0.5 
            ? foxes.generateFox() 
            : vultures.generateVulture();

        // Régi, kifutott ellenfelek takarítása
        foxes.deleteFoxes();
        vultures.deleteVultures();
    });

    // Pontszám növelése időalapon
    k.loop(1, () => {
        scoreBoard.increaseScore();
    });

    // Játékos–ellenség ütközés = game over
    k.onCollide("player", "enemy", () => {
        isInGame = false;

        // Score átadása az end scene-nek
        scoreBoard.setStateScore();

        k.go("endScene");
    });

    // Fő update loop
    k.onUpdate(() => {
        if (isInGame) {
            background.displayBackground();
            foxes.moveFoxes();
            vultures.moveVultures();
            scoreBoard.displayScore();
        }
    });
});
