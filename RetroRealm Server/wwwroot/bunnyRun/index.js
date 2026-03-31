import k from "./scripts/kaplayCtx.js";
import loadingScene from "./scripts/scenes/kaplayLoadingScene.js";

document.querySelector("canvas").style.cursor =
  'url("./assets/cursor.png") 11 13, auto';

function CheckScreenSize() {
  if (window.innerWidth < 1024) {
    window.location.href = "/landingpage";
  }
}

CheckScreenSize();

window.addEventListener("resize", CheckScreenSize);

k.go("loadingScene");
