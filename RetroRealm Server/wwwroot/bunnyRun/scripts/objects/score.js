import { spriteSheet } from "../sprites.js";
import k from "../kaplayCtx.js";
import scoreState from "../scoreNum.js";

// Pontszám megjelenítése sprite-okból (nem sima text)
export class ScoreBoard {
    constructor() {
        this.k = k;

        // Aktuális pontszám
        this.score = 0;

        // Kirajzolt számjegyek referenciái
        // (minden frissítésnél újraépítjük)
        this.scoreSprites = [];

        // Egy számjegy alap szélessége
        this.DIGIT_WIDTH = 60;
    }

    // Pontszám növelése (pl. akadály átugrásnál)
    increaseScore() {
        this.score += 1;
    }

    // Előző frame-ben kirajzolt számjegyek törlése
    clearScoreSprites() {
        this.scoreSprites.forEach(s => s.destroy());
        this.scoreSprites = [];
    }

    // Aktuális pontszám kirajzolása a képernyőre
    displayScore() {
        // Előbb letakarítjuk a régi számjegyeket
        this.clearScoreSprites();

        // A pontszámot stringként kezeljük,
        // így könnyű számjegyenként kirajzolni
        const scoreString = this.score.toString();

        // Bal felső saroktól indulunk (HUD elem)
        const startX = 20;

        for (let i = 0; i < scoreString.length; i++) { 
            const number = scoreString[i];

            // Egy számjegy kirajzolása
            const digitSprite = k.add([
                sprite(`digit${number}`),
                pos(startX + i * (this.DIGIT_WIDTH + 1), 20),
                fixed(),   // kamera mozgástól független (HUD)
                z(10),     // mindig minden fölött
                scale(4),
            ]); 

            this.scoreSprites.push(digitSprite); 
        }
    }

    // Pontszám átadása globális state-nek
    // (pl. game over képernyőnél)
    setStateScore() {
        scoreState.score = this.score;
    }
}
