import k from "./scripts/kaplayCtx.js";
import { gameScene } from "./scripts/gameScene.js";
import baseURL from "../scripts/baseURL.js";

function checkScreenSize() {
  if (window.innerWidth < 1024) {
    window.location.href = baseURL;
  }
}

checkScreenSize();

window.addEventListener("resize", checkScreenSize);

k.go("gameScene");
