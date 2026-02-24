import k from "../kaplayCtx.js";

// A játékos karakter (nyúl) kezelése
export class Bunny {
    constructor() {
        // A nyúl game object létrehozása és eltárolása
        this.object = k.add([ 
            sprite("bunny"), 

            // Alap pozíció: bal oldalról indul, talaj közelében
            pos(k.width() / 5, k.height() - 150), 

            // Ütközési terület – kisebb, mint a sprite,
            // hogy ne legyen "túl érzékeny" a collision
            area({
                shape: new k.Rect(k.vec2(0, 3), 30, 30),
            }), 

            // Fizika (gravitáció, ugrás, esés stb.)
            body(), 

            // Sprite felnagyítása
            scale(3), 

            // Rétegmélység – háttér elé kerül
            z(4), 

            // Tag, hogy könnyű legyen hivatkozni rá máshol
            "player"
        ]);
    }

    // Animáció lejátszása kívülről (pl. idle, run, jump)
    playAnim(anim) {
        this.object.play(anim);
    }
}
