import baseURL from "../scripts/baseURL.js";
import logout from "../scripts/logout.js";
import CheckUserRole from "./CheckUserRole.js";
import { textTranslate } from "./landing_lang.js";

function SetLanguage(landing_lang) {
  localStorage.setItem("lang", landing_lang);

  document.getElementById("langselection").style.display = "none";

  const nyelv = document.getElementById("nyelvvalt");
  nyelv.textContent = textTranslate.nyelvvalt[landing_lang];

  const szoveg = document.getElementById("pe1");
  szoveg.textContent = textTranslate.plepjbe[landing_lang];

  const achiev = document.getElementById("belep-btn-merfold");
  achiev.textContent = textTranslate.merfold[landing_lang];

  const kijelentbutton = document.getElementById("kijelentbutton");
  if (role == 401)
    kijelentbutton.textContent = textTranslate.belep[landing_lang];
  else kijelentbutton.textContent = textTranslate.kilep[landing_lang];

  const profileBtn = document.getElementById("profile-btn");
  profileBtn.textContent = textTranslate.profil[landing_lang];

  const leader = document.getElementById("belepbtnranglista");
  leader.textContent = textTranslate.ranglist[landing_lang];

  const miaz = document.getElementById("ha2");
  miaz.textContent = textTranslate.hmiazaretro[landing_lang];

  const desc = document.getElementById("pe2");
  desc.textContent = textTranslate.magyarazat[landing_lang];

  const kattints1 = document.getElementById("katt1");
  kattints1.textContent = textTranslate.kattint[landing_lang];

  const kattints2 = document.getElementById("katt2");
  kattints2.textContent = textTranslate.kattint[landing_lang];

  const kattints3 = document.getElementById("katt3");
  kattints3.textContent = textTranslate.kattint[landing_lang];

  const kattints4 = document.getElementById("katt4");
  kattints4.textContent = textTranslate.kattint[landing_lang];

  const kattints5 = document.getElementById("kattretro");
  kattints5.textContent = textTranslate.kattint[landing_lang];
}

document.getElementById("profile-btn").style.display = "none";
document.getElementById("admin-panel-btn").style.display = "none";

const langbutton = document.getElementById("nyelvvalt");
langbutton.addEventListener("click", () => {
  document.getElementById("langselection").style.display = "block";
  document.getElementById("langselection").addEventListener("mouseover", () => {
    document.getElementById("langselection").style.display = "block";
  });
  document.getElementById("langselection").addEventListener("mouseout", () => {
    document.getElementById("langselection").style.display = "none";
  });
});

document
  .getElementById("hun")
  .addEventListener("click", () => SetLanguage("hun"));

document
  .getElementById("eng")
  .addEventListener("click", () => SetLanguage("eng"));

document
  .getElementById("esp")
  .addEventListener("click", () => SetLanguage("esp"));

document
  .getElementById("flappy-bird-play-btn")
  .addEventListener(
    "click",
    () => (window.location.href = "../flappyBird/index.html"),
  );

document
  .getElementById("bunny-run-play-btn")
  .addEventListener(
    "click",
    () => (window.location.href = "../bunnyRun/index.html"),
  );

document
  .getElementById("memory-game-play-btn")
  .addEventListener(
    "click",
    () => (window.location.href = "../memory-cards/index.html"),
  );

document
  .getElementById("admin-panel-btn")
  .addEventListener(
    "click",
    () => (window.location.href = "../AdminPanel/AdminPanel/index.html"),
  );

document
  .getElementById("wordle-play-btn")
  .addEventListener(
    "click",
    () => (window.location.href = "../wordle/index.html"),
  );

document
  .getElementById("retrorealm-btn")
  .addEventListener(
    "click",
    () => (window.location.href = "../RetroRealm/index.html"),
  );

document
  .getElementById("belep-btn-merfold")
  .addEventListener(
    "click",
    () => (window.location.href = "../achievements/achievements.html"),
  );

document
  .getElementById("profile-btn")
  .addEventListener(
    "click",
    () => (window.location.href = "../profile/profile.html"),
  );

document
  .getElementById("customitemshop-btn")
  .addEventListener(
    "click",
    () => (window.location.href = "../customitemshop/customitemshop.html"),
  );

document
  .getElementById("belepbtnranglista")
  .addEventListener(
    "click",
    () => (window.location.href = "../leaderboard/leaderboard.html"),
  );

const role = await CheckUserRole();
const kijelentkezesBtn = document.getElementById("kijelentbutton");

console.log(role);

if (role == 403) {
  SetLanguage(localStorage.getItem("lang") ?? "eng");
  kijelentkezesBtn.addEventListener("click", async () => {
    await logout();
    setTimeout(() => (window.location.href = baseURL), 3000);
  });

  console.log("asd");
  document.getElementById("profile-btn").style.display = "block";
  document.getElementById("admin-panel-btn").style.display = "none";
} else if (role == 200) {
  SetLanguage(localStorage.getItem("lang") ?? "eng");
  kijelentkezesBtn.addEventListener("click", async () => {
    await logout();
    setTimeout(() => (window.location.href = baseURL), 3000);
  });

  document.getElementById("profile-btn").style.display = "block";
  document.getElementById("admin-panel-btn").style.display = "block";
} else {
  console.log(sessionStorage.getItem("Token"));
  kijelentkezesBtn.addEventListener(
    "click",
    () => (window.location.href = "../login/index.html"),
  );

  document.getElementById("profile-btn").style.display = "none";
  document.getElementById("admin-panel-btn").style.display = "none";

  SetLanguage(localStorage.getItem("lang") ?? "eng");
}
