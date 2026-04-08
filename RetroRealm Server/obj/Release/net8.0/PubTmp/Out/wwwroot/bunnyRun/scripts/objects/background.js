import k from "../kaplayCtx.js";

// Két sprite-ból álló, végtelenül scrollozó háttér
export class Background {
    constructor() {
        // Alap scroll sebesség (px / mp)
        this.speed = 100;

        // Első háttér – ez látszik induláskor
        this.backgroundElement1 = k.add([
            sprite("background"),
            pos(0, 0),
            z(0),
            scale(0.8),
        ]);

        // Második háttér – közvetlenül az első mögé rakva
        // Így tudjuk folyamatosan körbeforgatni őket
        this.backgroundElement2 = k.add([
            sprite("background"),
            pos(1840, 0),
            z(0),
            scale(0.8),
        ]);

        // Az X pozíciókat külön tároljuk,
        // hogy ne közvetlenül a sprite-ot piszkáljuk logikailag
        this.background1X = 0;
        this.background2X = 1840;
    }

    // Külső vezérléshez (pl. gyorsítás, lassítás játék közben)
    setBackgroundSpeed(speed) {
        this.speed = speed;
    }

    // Ezt frame-enként kell hívni (pl. update loopból)
    displayBackground() {
        // Delta time miatt framerate-független lesz a mozgás
        const dx = this.speed * k.dt();

        // Balra toljuk mindkét hátteret
        this.background1X -= dx;
        this.background2X -= dx;

        // Ha az egyik teljesen kiment a képernyőről,
        // átdobjuk a másik mögé
        if (this.background1X <= -1843) {
            this.background1X = 1840;
        }

        if (this.background2X <= -1843) {
            this.background2X = 1840;
        }

        // Végül frissítjük a sprite-ok pozícióját
        this.backgroundElement1.pos.x = this.background1X;
        this.backgroundElement2.pos.x = this.background2X;
    }
}
