import k from "../kaplayCtx.js";
import { foxSheet } from "../sprites.js";

// Ellenség (róka) kezelés – spawn, mozgatás, takarítás
export class Fox {
    constructor() {
        // Aktív rókák listája
        this.foxes = [];

        // Alap mozgási sebesség (px / mp)
        this.speed = 400;
    }

    // Új róka létrehozása a képernyő jobb szélén kívül
    generateFox() {
        let fox = k.add([ 
            sprite("fox"), 

            // Jobbról fut be, talaj közelében
            pos(k.width() + 100, k.height() - 200), 

            // Fizika komponens (gravity + collision)
            body(), 

            // Szűkebb hitbox a pontosabb ütközéshez
            area({
                shape: new k.Rect(k.vec2(5, 6), 40, 20),
            }), 

            // Sprite felnagyítása
            scale(4), 

            // A játékos és a háttér fölé kerül
            z(5), 

            // Tag: könnyű ütközés- és csoportkezeléshez
            "enemy"
        ]);

        // Alap animáció indítása spawn után
        fox.play("run");

        // Elmentjük, hogy később tudjuk mozgatni / törölni
        this.foxes.push(fox);
    }

    // Összes aktív róka balra mozgatása
    // (játék loopból hívva)
    moveFoxes() {
        this.foxes.forEach(fox => {
            fox.move(-this.speed, 0);
        });
    }

    // Képernyőről kifutott rókák eltávolítása
    deleteFoxes() {
        for (let i = 0; i < this.foxes.length; i++) {
            const fox = this.foxes[i];

            // Ha már nem látható, nem tartjuk tovább memóriában
            if (fox.pos.x < -100) {
                fox.destroy();
                this.foxes.splice(i, 1);
                i--; // splice miatt visszalépünk egyet
            }
        }
    }
}
