import { texttranslate } from "./landing_lang.js";

function SetLanguage(landing_lang) {
  const nyelv = document.getElementById("nyelvvalt");
  nyelv.textContent = texttranslate.nyelvvalt[landing_lang];

  const szoveg = document.getElementById("pe1");
  szoveg.textContent = texttranslate.plepjbe[landing_lang];

  const achiev = document.getElementById("belepbtnmerfold");
  achiev.textContent = texttranslate.merfold[landing_lang];

  const belepes = document.getElementById("belepbtnbelep");
  belepes.textContent = texttranslate.belep[landing_lang];

  const leader = document.getElementById("belepbtnranglista");
  leader.textContent = texttranslate.ranglist[landing_lang];

  const miaz = document.getElementById("ha2");
  miaz.textContent = texttranslate.hmiazaretro[landing_lang];

  const desc = document.getElementById("pe2");
  desc.textContent = texttranslate.magyarazat[landing_lang];

  const kattints1 = document.getElementById("katt1");
  kattints1.textContent = texttranslate.kattint[landing_lang];

  const kattints2 = document.getElementById("katt2");
  kattints2.textContent = texttranslate.kattint[landing_lang];

  const kattints3 = document.getElementById("katt3");
  kattints3.textContent = texttranslate.kattint[landing_lang];

  const kattints4 = document.getElementById("katt4");
  kattints4.textContent = texttranslate.kattint[landing_lang];

  const kattints5 = document.getElementById("kattretro");
  kattints5.textContent = texttranslate.kattint[landing_lang];
}

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

const hunbutton = document.getElementById("hun");
hunbutton.addEventListener("click", () => {
  SetLanguage("hu");
  document.getElementById("langselection").style.display = "none";
});

const engbutton = document.getElementById("eng");
engbutton.addEventListener("click", () => {
  SetLanguage("en");
  document.getElementById("langselection").style.display = "none";
});

const espbutton = document.getElementById("esp");
espbutton.addEventListener("click", () => {
  SetLanguage("esp");
  document.getElementById("langselection").style.display = "none";
});
