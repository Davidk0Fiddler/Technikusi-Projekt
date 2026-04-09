import { texts } from "./lang_achiev.js";
import GetAchievements from "./GetAchievements.js";

function SetLanguage(lang_achiev) {
  localStorage.setItem("lang", lang_achiev);
  const nyelv = document.getElementById("lang-button");
  nyelv.textContent = texts.langselection[lang_achiev];
  const cim = document.getElementById("title");
  cim.textContent = texts.merfold[lang_achiev];

  DisplayAchievements();
}

const langbutton = document.getElementById("lang-button");
langbutton.addEventListener("click", () => {
  document.getElementById("lang-selection").style.display = "block";
  document
    .getElementById("lang-selection")
    .addEventListener("mouseover", () => {
      document.getElementById("lang-selection").style.display = "block";
    });
  document.getElementById("lang-selection").addEventListener("mouseout", () => {
    document.getElementById("lang-selection").style.display = "none";
  });
});

const hunbutton = document.getElementById("hun");
hunbutton.addEventListener("click", () => {
  SetLanguage("hun");
  document.getElementById("lang-selection").style.display = "none";
});

const engbutton = document.getElementById("eng");
engbutton.addEventListener("click", () => {
  SetLanguage("eng");
  document.getElementById("lang-selection").style.display = "none";
});

const espbutton = document.getElementById("esp");
espbutton.addEventListener("click", () => {
  SetLanguage("esp");
  document.getElementById("lang-selection").style.display = "none";
});

var list = [
  {
    nameEng: "Little Hopper",
    nameEsp: "Pequeño Saltador",
    nameHun: "Kis Ugráló",
    requirementEng: "Reach 5 points in Bunny Run!",
    requirementEsp: "¡Alcanza los 5 puntos en Bunny Run!",
    requirementHun: "Érj el 5 pontot a Bunny Run játékban!",
    prizeCoin: 10,
  },
  {
    nameEng: "Forest Runner",
    nameEsp: "Corredor del Bosque",
    nameHun: "Erdő Futója",
    requirementEng: "Reach 10 points in Bunny Run!",
    requirementEsp: "¡Alcanza los 10 puntos en Bunny Run!",
    requirementHun: "Érj el 10 pontot a Bunny Run játékban!",
    prizeCoin: 20,
  },
  {
    nameEng: "Predator Dodger",
    nameEsp: "Esquivador de Depredadores",
    nameHun: "Ragadozókerülő",
    requirementEng: "Reach 15 points in Bunny Run!",
    requirementEsp: "¡Alcanza los 15 puntos en Bunny Run!",
    requirementHun: "Érj el 15 pontot a Bunny Run játékban!",
    prizeCoin: 30,
  },
  {
    nameEng: "Legendary Bunny",
    nameEsp: "Conejo Legendario",
    nameHun: "Legendás Nyúl",
    requirementEng: "Reach 20 points in Bunny Run!",
    requirementEsp: "¡Alcanza los 20 puntos en Bunny Run!",
    requirementHun: "Érj el 20 pontot a Bunny Run játékban!",
    prizeCoin: 40,
  },
  {
    nameEng: "First Flap",
    nameEsp: "Primer Aleteo",
    nameHun: "Első Szárnycsapás",
    requirementEng: "Reach 5 points in FlappyBird!",
    requirementEsp: "¡Alcanza los 5 puntos en FlappyBird!",
    requirementHun: "Érj el 5 pontot a FlappyBird játékban!",
    prizeCoin: 10,
  },
  {
    nameEng: "Pipe Explorer",
    nameEsp: "Explorador de Tuberías",
    nameHun: "Cső Felfedező",
    requirementEng: "Reach 10 points in FlappyBird!",
    requirementEsp: "¡Alcanza los 10 puntos en FlappyBird!",
    requirementHun: "Érj el 10 pontot a FlappyBird játékban!",
    prizeCoin: 20,
  },
  {
    nameEng: "Sky Navigator",
    nameEsp: "Navegante del Cielo",
    nameHun: "Ég Navigátora",
    requirementEng: "Reach 15 points in FlappyBird!",
    requirementEsp: "¡Alcanza los 15 puntos en FlappyBird!",
    requirementHun: "Érj el 15 pontot a FlappyBird játékban!",
    prizeCoin: 30,
  },
  {
    nameEng: "Master of the Pipes",
    nameEsp: "Maestro de las Tuberías",
    nameHun: "Csövek Mestere",
    requirementEng: "Reach 20 points in FlappyBird!",
    requirementEsp: "¡Alcanza los 20 puntos en FlappyBird!",
    requirementHun: "Érj el 20 pontot a FlappyBird játékban!",
    prizeCoin: 40,
  },
  {
    nameEng: "Curious Mind",
    nameEsp: "Mente Curiosa",
    nameHun: "Kíváncsi Elme",
    requirementEng: "Complete the Memory Game in 110 flips!",
    requirementEsp: "¡Completa el juego de memoria en 110 turnos!",
    requirementHun: "Teljesítsd a Memória játékot 110 felfordítás alatt!",
    prizeCoin: 10,
  },
  {
    nameEng: "Memory Apprentice",
    nameEsp: "Aprendiz de Memoria",
    nameHun: "Memória Tanonc",
    requirementEng: "Complete the Memory Game in 100 flips!",
    requirementEsp: "¡Completa el juego de memoria en 100 turnos!",
    requirementHun: "Teljesítsd a Memória játékot 100 felfordítás alatt!",
    prizeCoin: 20,
  },
  {
    nameEng: "Pattern Finder",
    nameEsp: "Buscador de Patrones",
    nameHun: "Mintavadász",
    requirementEng: "Complete the Memory Game in 90 flips!",
    requirementEsp: "¡Completa el juego de memoria en 90 turnos!",
    requirementHun: "Teljesítsd a Memória játékot 90 felfordítás alatt!",
    prizeCoin: 30,
  },
  {
    nameEng: "Master of Memory",
    nameEsp: "Maestro de la Memoria",
    nameHun: "Memória Mestere",
    requirementEng: "Complete the Memory Game in 80 flips!",
    requirementEsp: "¡Completa el juego de memoria en 80 turnos!",
    requirementHun: "Teljesítsd a Memória játékot 80 felfordítás alatt!",
    prizeCoin: 40,
  },
  {
    nameEng: "First Guess",
    nameEsp: "Primer Acierto",
    nameHun: "Első Találat",
    requirementEng: "Complete 1 Wordle games!",
    requirementEsp: "¡Completa 1 juegos de Wordle!",
    requirementHun: "Fejezz be 1 Wordle játékot!",
    prizeCoin: 10,
  },
  {
    nameEng: "Word Hunter",
    nameEsp: "Cazador de Palabras",
    nameHun: "Szóvadász",
    requirementEng: "Complete 5 Wordle games!",
    requirementEsp: "¡Completa 5 juegos de Wordle!",
    requirementHun: "Fejezz be 5 Wordle játékot!",
    prizeCoin: 20,
  },
  {
    nameEng: "Vocabulary Expert",
    nameEsp: "Experto en Vocabulario",
    nameHun: "Szókincs Szakértő",
    requirementEng: "Complete 10 Wordle games!",
    requirementEsp: "¡Completa 10 juegos de Wordle!",
    requirementHun: "Fejezz be 10 Wordle játékot!",
    prizeCoin: 30,
  },
  {
    nameEng: "Word Legend",
    nameEsp: "Leyenda de las Palabras",
    nameHun: "Szavak Legendája",
    requirementEng: "Complete 15 Wordle games!",
    requirementEsp: "¡Completa 15 juegos de Wordle!",
    requirementHun: "Fejezz be 15 Wordle játékot!",
    prizeCoin: 40,
  },
];

async function DisplayAchievements() {
  const itemList = document.getElementById("item-list");
  itemList.innerHTML = " ";
  var lang = localStorage.getItem("lang") ?? "Eng";
  var langa = lang.charAt(0).toUpperCase() + lang.slice(1);
  list.forEach((achievement) => {
    itemList.innerHTML += `
        <div class="item">
    <p class="achievement-name">${achievement[`name${langa}`]}</p>
    <p class="achievement-requirement">${achievement[`requirement${langa}`]}</p>
    <p class="achievement-prize">Coins: ${achievement.prizeCoin}</p>
        </div>`;
  });
}
DisplayAchievements();
