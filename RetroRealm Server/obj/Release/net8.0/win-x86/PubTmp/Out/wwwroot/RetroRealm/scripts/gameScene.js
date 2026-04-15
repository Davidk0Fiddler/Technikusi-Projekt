import { Character } from "./classes/Character.js";
import { InputBox } from "./classes/InputBox.js";
import { CreateMap } from "./CreateMap.js";
import GetUserName from "./APIs/GetUsername.js";
import k from "./kaplayCtx.js";

export const gameScene = k.scene("gameScene", async () => {
  let movementEnabled = true;

  const ownUserName = await GetUserName();
  CreateMap();

  const player = new Character("baseCharacter", ownUserName, "player");
  const inputBox = new InputBox(player.object);

  let currentText = null;
  let currentTextDate = null;

  inputBox.onSend = (text) => {
    currentText = text;
    currentTextDate = new Date().toISOString();
    player.setText(text);
  };

  const dir = { x: 0, y: 0 };
  const otherPlayers = [];

  /* =========================
       WEBSOCKET
    ========================= */

  const SOCKET = new WebSocket("wss://localhost:7234/ws");

  SOCKET.onopen = () => {
    sendUpdate();
  };

  SOCKET.onmessage = (event) => {
    const data = JSON.parse(event.data);

    const serverUserNames = new Set(data.Players.map((p) => p.UserName));

    data.Players.forEach((p) => {
      console.log(p);

      /* =========================
            SAJÁT JÁTÉKOS
            ========================= */
      if (p.UserName === data.YourUserName) {
        player.setName(p.CharacterName, p.UserName, p.Role);

        // Szöveg kezelése a saját játékosnál is
        if (p.LastTextDate === null) {
          if (player.text) player.setText(null);
        } else if (p.Text && p.LastTextDate) {
          const now = Date.now();
          const textTime = new Date(p.LastTextDate).getTime();
          if (now - textTime < 10000) {
            if (player.text !== p.Text) player.setText(p.Text);
          } else {
            if (player.text) player.setText(null);
          }
        }
        return; // Fontos: itt return, hogy ne folytatódjon a többi játékos kezelése erre az elemre
      }

      /* =========================
            MÁSIK JÁTÉKOS
            ========================= */
      let other = otherPlayers.find((o) => o.userName === p.UserName);

      if (!other) {
        other = {
          userName: p.UserName,
          char: new Character(
            p.CharacterName,
            p.UserName,
            p.Role,
            p.posX,
            p.posY,
          ),
          dir: { x: 0, y: 0 },
        };
        otherPlayers.push(other);
      }

      if (p.LastTextDate === null) {
        // Szerver szerint nincs szöveg
        if (other.char.text) {
          other.char.setText(null);
        }
      } else if (p.Text && p.LastTextDate) {
        const now = Date.now();
        const textTime = new Date(p.LastTextDate).getTime();
        if (now - textTime < 10000) {
          // Még érvényes
          if (other.char.text !== p.Text) {
            other.char.setText(p.Text);
          }
        } else {
          // Lejárt, de szerver még küldi (átmeneti állapot) - töröljük
          if (other.char.text) {
            other.char.setText(null);
          }
        }
      }

      other.dir.x = p.moveX;
      other.dir.y = p.moveY;

      other.char.posX = p.posX;
      other.char.posY = p.posY;

      other.lastSend = Number(p.LastSend);
    });

    for (let i = otherPlayers.length - 1; i >= 0; i--) {
      const o = otherPlayers[i];

      if (!serverUserNames.has(o.userName)) {
        // opcionális: cleanup / destroy
        k.destroy(o.char.object);

        otherPlayers.splice(i, 1);
      }
    }
  };

  /* =========================
       INPUT
    ========================= */

  k.onKeyPress("t", () => {
    if (inputBox && !inputBox.visible) {
      inputBox.show();

      movementEnabled = false;

      dir.x = 0;
      dir.y = 0;
    }
  });

  inputBox.onClose = () => {
    movementEnabled = true;
  };

  k.onKeyDown(["d", "right"], () => {
    if (!movementEnabled) return;
    dir.x = 1;
  });

  k.onKeyDown(["a", "left"], () => {
    if (!movementEnabled) return;
    dir.x = -1;
  });

  k.onKeyDown(["w", "up"], () => {
    if (!movementEnabled) return;
    dir.y = -1;
  });

  k.onKeyDown(["s", "down"], () => {
    if (!movementEnabled) return;
    dir.y = 1;
  });

  k.onKeyRelease(["d", "right", "a", "left"], () => (dir.x = 0));
  k.onKeyRelease(["w", "up", "s", "down"], () => (dir.y = 0));

  /* =========================
       SERVER UPDATE
    ========================= */

  function sendUpdate() {
    SOCKET.send(
      JSON.stringify({
        Type: "UPDATE",
        Data: {
          UserName: ownUserName,
          moveX: dir.x,
          moveY: dir.y,

          posX: player.object.pos.x,
          posY: player.object.pos.y,

          Text: currentText,
          LastTextDate: currentTextDate,

          LastSend: new Date(),
        },
      }),
    );
  }

  k.loop(0.1, () => {
    if (SOCKET.readyState === WebSocket.OPEN) {
      sendUpdate();
    }
  });

  /* =========================
       GAME LOOP
    ========================= */

  k.onUpdate(() => {
    if (player && movementEnabled) {
      player.update(dir.x, dir.y);
    }

    otherPlayers.forEach((o) => {
      o.char.update(o.dir.x, o.dir.y);
    });

    // npc.update(
    //     npcRoute[npcRouteId].x,
    //     npcRoute[npcRouteId].y
    // );
  });
});
