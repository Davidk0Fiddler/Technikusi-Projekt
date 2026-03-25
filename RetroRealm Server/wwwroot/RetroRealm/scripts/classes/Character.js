import k from "../kaplayCtx.js";
import { TextRect } from "./TextRect.js";

export class Character {
  constructor(
    spriteName,
    userName,
    role,
    posX = k.width() / 2,
    posY = k.height() / 2,
  ) {
    this.speed = 64;
    this.userName = userName;
    this.spriteName = spriteName;
    this.role = role;
    this.facing = "down";
    this.currentAnim = null;
    this.colorRole =
      this.role == "Admin"
        ? [255, 0, 0]
        : this.role == "User"
          ? [0, 255, 0]
          : [255, 255, 255];
    ((this.posX = posX), (this.posY = posY), (this.text = null));
    this.textRect = null;
    this.textTimeout = null;

    this.object = k.add([
      sprite(this.spriteName),
      pos(this.posX, this.posY),
      area(),
      body(),
      "player",
      z(3),
      scale(1.05),
      anchor("center"),
    ]);
    this.object.add([
      text(userName, { size: 12, font: "sans-serif", letterSpacing: 0.1 }),
      anchor("center"),
      color(0, 0, 0),
      pos(1, -19),
      z(3),
    ]);

    this.object.add([
      text(userName, { size: 11, font: "sans-serif", letterSpacing: 0.1 }),
      anchor("center"),
      color(this.colorRole[0], this.colorRole[1], this.colorRole[2]),
      pos(0, -20),
      z(3),
    ]);
  }

  play(anim) {
    if (this.currentAnim !== anim) {
      this.currentAnim = anim;
      this.object.play(anim);
    }
  }

  update(dx, dy) {
    const moving = dx !== 0 || dy !== 0;

    if (moving) {
      this.object.moveBy(dx * this.speed * k.dt(), dy * this.speed * k.dt());
    }

    if (!moving) {
      this.play(
        this.facing === "up"
          ? "idleUp"
          : this.facing === "down"
            ? "idleDown"
            : this.facing === "left"
              ? "idelLeft"
              : "idleRight",
      );
      return;
    }

    if (dx > 0) {
      this.facing = "right";
      this.play("walkRight");
    } else if (dx < 0) {
      this.facing = "left";
      this.play("walkLeft");
    } else if (dy < 0) {
      this.facing = "up";
      this.play("walkUp");
    } else if (dy > 0) {
      this.facing = "down";
      this.play("walkDown");
    }
  }

  setName(charName, userName, role) {
    this.userName = userName;
    this.role = role;

    const colorRole2 =
      this.role === "Admin"
        ? [255, 0, 0]
        : this.role === "User"
          ? [0, 255, 0]
          : [255, 255, 255];

    // régi feliratok törlése
    this.object.children
      .filter((c) => c.is("text"))
      .forEach((c) => c.destroy());

    if (this.spriteName != charName) {
      this.spriteName = charName;
      this.object.use(sprite(this.spriteName));
    }

    // fekete árnyék
    this.object.add([
      text(userName, { size: 12, font: "sans-serif", letterSpacing: 0.1 }),
      anchor("center"),
      color(0, 0, 0),
      pos(1, -19),
      z(3),
    ]);

    // színes név
    this.object.add([
      text(userName, { size: 11, font: "sans-serif", letterSpacing: 0.1 }),
      anchor("center"),
      color(colorRole2[0], colorRole2[1], colorRole2[2]),
      pos(0, -20),
      z(3),
    ]);
  }

  setPosition(x, y) {
    this.object.pos.x = x;
    this.object.pos.y = y;
  }

  setText(text) {
    if (this.textRect) {
      k.destroy(this.textRect.textRect);
      this.textRect = null;
    }
    this.text = text;

    if (text) {
      this.textRect = new TextRect(this.object, text);
    }
  }
}
