import k from "../kaplayCtx.js";
import menuScene from "./menuScene.js";
import { kaplayDino } from "../sprites.js";

// Egyszerű loading / intro scene (logo animációval)
export default scene("loadingScene", () => {
    // Skálázási érték az animációhoz
    let scaling = 0;

    // Alap kurzor visszaállítása
    k.setCursor("../../assets/cursor.png");

    // Kaplay logó kirajzolása középre
    const kaplayLabel = k.add([
        sprite("kaplay_dino-o"),
        anchor("center"),
        pos(k.center()),
        scale(scaling),
    ]);

    // Frame-alapú animáció
    k.onUpdate(() => {
        // Fokozatos nagyítás
        if (scaling < 1.5) {
            scaling += 0.04;
        } else {
            // Kis késleltetés után továbblépés a menübe
            k.wait(1, () => {
                k.go("menuScene");
            });
        }

        // Aktuális skála alkalmazása a sprite-ra
        kaplayLabel.scale = vec2(scaling, scaling);
    });
});
