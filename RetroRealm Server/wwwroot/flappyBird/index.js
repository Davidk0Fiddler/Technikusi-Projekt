import k from "./scripts/kaplayCtx.js";
import kaplayLoadingScreen from "./scripts/scenes/kaplayLoadingScene.js";

function checkScreenSize() {
  if (window.innerWidth < 1024) {
    window.location.href = "/landingpage";
  }
}

checkScreenSize();

window.addEventListener("resize", checkScreenSize);

k.setCursor('url("./assets/cursor.png") 11 13, auto');
k.go("kaplayLoadingScene");
