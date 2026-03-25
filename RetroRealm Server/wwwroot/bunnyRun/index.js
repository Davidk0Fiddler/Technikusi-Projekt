import k from "./scripts/kaplayCtx.js";
import loadingScene from "./scripts/scenes/kaplayLoadingScene.js";

document.querySelector("canvas").style.cursor = 'url("./assets/cursor.png") 11 13, auto';

function checkScreenSize() {
    if (window.innerWidth < 1024) { 
        window.location.href = "/landingpage";
    }
}

checkScreenSize();

window.addEventListener("resize", checkScreenSize);

k.go("loadingScene");