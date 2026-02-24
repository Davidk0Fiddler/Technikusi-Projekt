import k from "../kaplayCtx.js";

export class Background {
    constructor() {
        this.k = k;

        // Két háttérelem a folyamatos scrollhoz
        this.backgroundElement1 = this.k.add([
            sprite("background"),
            pos(0, 0),
            z(0),
        ]);

        this.backgroundElement2 = this.k.add([
            sprite("background"),
            pos(this.k.width() - 1, 0),
            z(0),
        ]);

        // X pozíciók kezelése külön a pontos mozgatáshoz
        this.bg1X = 0;
        this.bg2X = this.k.width() - 1;

        // Scroll sebesség
        this.backgroundElementSpeed = 20;
    }

    setBackgroundSpeed(speed) {
        this.backgroundElementSpeed = speed;
    }

    displayBackground() {
        // Frame-alapú elmozdulás
        const dx = this.backgroundElementSpeed * this.k.dt();

        this.bg1X -= dx;
        this.bg2X -= dx;

        // Ha kiment balra, visszaugrik jobbra
        if (this.bg1X <= -this.k.width()) {
            this.bg1X = this.k.width() - 1;
        }

        if (this.bg2X <= -this.k.width()) {
            this.bg2X = this.k.width() - 1;
        }

        // Sprite pozíciók frissítése
        this.backgroundElement1.pos.x = this.bg1X;
        this.backgroundElement2.pos.x = this.bg2X;
    }
}
