import sprites from "../sprites.js";
import k from "../kaplayCtx.js";
import scoreState from "../scoreNum.js";

export class ScoreBoard {
    constructor() {
        this.k = k;
        this.score = 0;
        this.scoreSprites = [];
        this.DIGIT_WIDTH = 10;
    }

    increaseScore() {
        // Score növelése
        this.score += 1;
    }

    clearScoreSprites() {
        // Korábbi számjegyek eltávolítása
        this.scoreSprites.forEach(s => s.destroy());
        this.scoreSprites = [];
    }

    displayScore() {
        // Score újrarajzolása
        this.clearScoreSprites();

        const scoreString = this.score.toString();
        const totalWidth =
            scoreString.length * this.DIGIT_WIDTH + (scoreString.length - 1);

        const startX = width() / 2 - totalWidth / 2;

        for (let i = 0; i < scoreString.length; i++) {
            const number = scoreString[i];

            const digitSprite = add([
                sprite(`digit${number}`),
                pos(startX + i * (this.DIGIT_WIDTH + 1), this.k.height() - 26),
                fixed(),
                z(10),
            ]);

            this.scoreSprites.push(digitSprite);
        }
    }

    setStateScore() {
        // Score mentése globális state-be
        scoreState.score = this.score;
    }
}
