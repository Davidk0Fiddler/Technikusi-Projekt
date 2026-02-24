import k from "../kaplayCtx.js";

export class Ground {
    constructor() {
        this.k = k;

        // Két ground elem a folyamatos scrollhoz
        this.groundElement1 = this.k.add([
            sprite("ground"),
            pos(0, this.k.height() - 56),
            z(3),
            body({ isStatic: true }),
            area(),
        ]);

        this.groundElement2 = this.k.add([
            sprite("ground"),
            pos(this.k.width() - 1, this.k.height() - 56),
            z(3),
            body({ isStatic: true }),
            area(),
        ]);

        // X pozíciók külön kezelve
        this.g1X = 0;
        this.g2X = this.k.width() - 1;

        // Scroll sebesség
        this.groundElementSpeed = 40;
    }

    setGroundSpeed(speed) {
        this.groundElementSpeed = speed;
    }

    displayGround() {
        // Frame-alapú elmozdulás
        const dx = this.groundElementSpeed * this.k.dt();

        this.g1X -= dx;
        this.g2X -= dx;

        // Ha kiment balra, visszaugrik jobbra
        if (this.g1X <= -this.k.width()) {
            this.g1X = this.k.width() - 1;
        }

        if (this.g2X <= -this.k.width()) {
            this.g2X = this.k.width() - 1;
        }

        // Sprite pozíciók frissítése
        this.groundElement1.pos.x = this.g1X;
        this.groundElement2.pos.x = this.g2X;
    }
}
