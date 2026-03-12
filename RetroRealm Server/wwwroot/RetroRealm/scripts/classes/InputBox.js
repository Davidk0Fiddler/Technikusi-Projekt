import k from "../kaplayCtx.js";
import { TextRect } from "./TextRect.js";

export class InputBox {
  constructor(playerObject) {
    this.MAX_CHARS = 20;
    this.text = "";
    this.visible = false;
    this.cursorVisible = true;
    this.playerObject = playerObject;
    this.onSend = null;

    this.container = add([
      rect(500, 80),
      pos(center()),
      anchor("center"),
      color(0, 0, 0),
      outline(4, rgb(255, 255, 255)),
      z(1000),
      fixed(),
      opacity(0),
    ]);

    this.label = add([
      text("", { size: 32 }),
      pos(this.container.pos.x - 230, this.container.pos.y),
      anchor("left"),
      z(1001),
      fixed(),
      opacity(0),
    ]);

    this.counter = add([
      text("", { size: 20 }),
      pos(width() - 20, 20),
      anchor("topright"),
      z(1001),
      fixed(),
      opacity(0),
    ]);

    loop(0.5, () => {
      if (this.visible) {
        this.cursorVisible = !this.cursorVisible
      }
    });

    onCharInput((char) => {
      if (!this.visible) return
      if (this.text.length >= this.MAX_CHARS) return

      if (isKeyDown("shift")) {
        this.text += char.toUpperCase()
      } else {
        this.text += char
      }
    });

    onKeyPress("backspace", () => {
      if (!this.visible) return
      this.text = this.text.slice(0, -1)
    });

    onKeyPress("escape", () => {
      if (!this.visible) return
      this.hide()
    });

    onKeyPress("enter", () => {
      if (!this.visible) return
      if (this.text.length > 0) {
        this.onSend(this.text);
      }
      this.hide()
    });

    onUpdate(() => this.update())
  }

  show() {
    this.visible = true
    this.text = ""
    this.container.opacity = 1
    this.label.opacity = 1
    this.counter.opacity = 1
  };

  hide() {
    this.visible = false
    this.text = ""
    this.container.opacity = 0
    this.label.opacity = 0
    this.counter.opacity = 0;

    if (this.onClose) {
        this.onClose();
    }
  };

  update() {
    if (!this.visible) return

    const cursor = this.cursorVisible ? "|" : ""
    this.label.text = this.text + cursor
    this.counter.text = `${this.text.length}/${this.MAX_CHARS}`
  };
}