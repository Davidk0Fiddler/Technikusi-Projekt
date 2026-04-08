import k from "../kaplayCtx.js";

export class TextRect {
    constructor(object, textString) {
        this.object = object;
        this.textString = textString;
    
        this.textRect = this.object.add([
            rect(this.textString.length * 8 ,17.5),
            anchor("center"),
            pos(0,-40),
            scale(1),
            z(6),
            outline(2, rgb(0,0,0)),
        ]);

        this.textLabel = this.textRect.add([
            text(this.textString, { size: 14, font: "sans-serif", letterSpacing: 0.1 }),
            anchor("center"),
            color(0,0,0),
            outline(2, rgb(0,0,0)),
        ]);
    }
}