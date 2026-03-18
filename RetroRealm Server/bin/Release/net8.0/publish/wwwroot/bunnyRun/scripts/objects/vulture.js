import k from "../kaplayCtx.js";
import { vultureSheet } from "../sprites.js";

// Repülő ellenségek (keselyűk) kezelése
export class Vultures {
    constructor() {
        // Aktív keselyűk listája
        this.vultures = [];

        // Vízszintes mozgási sebesség
        this.speed = 400;
    }

    // Új keselyű spawnolása a képernyőn kívülről
    generateVulture() {
        let vulture = k.add([
            sprite("vulture"),

            // Jobbról repül be, a talaj fölött
            pos(k.width() + 100, k.height() - 400),

            // Hitbox kisebb, mint a sprite a fair ütközéshez
            area({
                shape: new k.Rect(k.vec2(5, 5), 60, 40),
            }),

            scale(3),
            z(5),

            // Ellenség tag (collision / query miatt)
            "enemy"
        ]);

        // Alap repülő animáció indítása
        vulture.play("fly");

        // Elmentjük későbbi kezeléshez
        this.vultures.push(vulture);
    }

    // Összes aktív keselyű mozgatása balra
    moveVultures() {
        this.vultures.forEach(vulture => {
            vulture.move(-this.speed, 0);
        });
    }

    // Képernyőről kifutott keselyűk törlése
    deleteVultures() {
        for (let i = 0; i < this.vultures.length; i++) {
            const vulture = this.vultures[i];

            if (vulture.pos.x < -100) {
                vulture.destroy();
                this.vultures.splice(i, 1);
                i--; // splice után visszalépünk, hogy ne maradjon ki elem
            }
        }
    }
}
