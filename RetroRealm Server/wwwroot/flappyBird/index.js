import k from "./scripts/kaplayCtx.js";
import kaplayLoadingScreen from "./scripts/scenes/kaplayLoadingScene.js";

function CheckScreenSize() {
  if (window.innerWidth < 1024) {
    window.location.href = "/landingpage";
  }
}

CheckScreenSize();

window.addEventListener("resize", CheckScreenSize);

k.setCursor('url("./assets/cursor.png") 11 13, auto');
k.go("kaplayLoadingScene");
