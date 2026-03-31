import k from "./scripts/kaplayCtx.js";
import { gameScene } from "./scripts/gameScene.js";

function checkScreenSize() {
    if (window.innerWidth < 1024) { 
        window.location.href = "/landingpage";
    }
}

checkScreenSize();

window.addEventListener("resize", checkScreenSize);

k.go("gameScene");