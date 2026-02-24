import k from "../kaplayCtx.js";
import sprites from "../sprites.js";
import menuScene from "./menuScene.js";

k.loadSprite("kaplay_dino-o", "./assets/kaplay_dino-o.png");

export default scene("kaplayLoadingScene", () => {
    setCamScale(0.3);

    let scaling = 0;

    // Kaplay logo
    const kaplayLabel = k.add([
        sprite("kaplay_dino-o"),
        anchor("center"),
        pos(k.center()),
        scale(scaling),
    ]);

    k.onUpdate(() => {
        // Scale-in animáció
        if (scaling < 1) {
            scaling += 0.04;
        } else {
            // Tovább a menübe
            wait(1, () => k.go("menuScene"));
        }

        kaplayLabel.scale = vec2(scaling, scaling);
    });
});
