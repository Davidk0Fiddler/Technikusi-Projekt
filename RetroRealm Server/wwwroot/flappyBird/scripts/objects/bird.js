import sprites from "../sprites.js";
import k from "../kaplayCtx.js";
import endScreen from "../scenes/endScreen.js";

export class Bird {
    constructor() {
        this.k = k;

        // Bird game object
        this.object = this.k.add([
            sprite("bird"),
            pos(this.k.width() / 4, (this.k.height() - 56) / 2),
            z(5),
            body(),
            area({
                shape: new Rect(vec2(4, 8), 14, 12),
            }),
            "bird",
        ]);

        // Ugrás tiltása ütközés után
        this.isNotCollided = true;
    }

    birdInit() {
        // Alap animáció
        this.object.play("moving");
    }   

    birdJump(pipes, background, ground, scoreBoard) {
        // Ugrás csak aktív állapotban
        if (this.isNotCollided) {            
            this.object.jump(100);
        }

        this.object.onUpdate(() => {
            // Halál feltételek: kirepül vagy földre esik
            if (this.object.pos.y < -20 || this.object.isGrounded()) {
                this.disableJump();

                // Játék megállítása
                pipes.setPipeSpeed(0);
                background.setBackgroundSpeed(0);
                ground.setGroundSpeed(0);

                // Score rögzítése és end screen
                scoreBoard.setStateScore();
                k.wait(2, () => this.k.go("endScreen"));
            }
        });
    }

    disableJump() {
        // Ütközés utáni állapot
        this.isNotCollided = false;
        this.object.angle = 45;
    }
}
