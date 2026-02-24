import sprites from "../sprites.js";
import k from "../kaplayCtx.js";

export class PipeGroup {
    constructor() {
        this.k = k;
        this.pipes = [];

        // Pipe mozgás és generálás paraméterek
        this.pipeSpeed = 40;
        this.minY = 50;
        this.maxY = this.k.height() - 136;
        this.minGap = 50;
        this.maxGap = 70;
    }

    setPipeSpeed(speed) {
        this.pipeSpeed = speed;
    }

    generatePipe() {
        const y = rand(this.minY, this.maxY);
        const gap = rand(this.minGap, this.maxGap);

        // Felső cső (parent)
        const pipe = this.k.add([
            sprite("pipe_top"),
            anchor("botleft"),
            pos(this.k.width(), y),
            body({ isStatic: true }),
            area(),
            z(2),
            "pipe",
        ]);

        // Alsó cső
        pipe.add([
            sprite("pipe_bottom"),
            pos(0, gap),
            body({ isStatic: true }),
            area(),
            z(2),
            "pipe",
        ]);

        // Láthatatlan score trigger
        pipe.add([
            rect(10, gap),
            pos(30, 0),
            area(),
            z(2),
            opacity(0),
            "scoreZone",
            { passed: false },
        ]);

        this.pipes.push(pipe);
    }

    deletePipe() {
        // Képernyőn kívüli pipe-ok törlése
        for (let i = 0; i < this.pipes.length; i++) {
            const pipe = this.pipes[i];

            if (pipe.pos.x + 30 < 0) {
                pipe.destroy();
                this.pipes.splice(i, 1);
            }
        }
    }

    movePipes() {
        // Pipe-ok mozgatása frame-alapon
        for (let i = 0; i < this.pipes.length; i++) {
            const pipe = this.pipes[i];
            pipe.pos.x -= this.pipeSpeed * this.k.dt();
        }
    }

    displayPipes() {
        // Időzített pipe generálás
        loop(2.5, () => {
            this.generatePipe();
            this.deletePipe();
        });
    }

    deleteAllPipes() {
        // Összes pipe eltávolítása
        for (let i = 0; i < this.pipes.length; i++) {
            this.pipes[i].destroy();
            this.pipes.splice(i, 1);
        }
    }
}
