// Importing the token refresh helper used for authentication renewal
import refreshToken from "../scripts/tokenRefresher.js";

/*
    ---------------------------------------------
    DATA DEFINITIONS
    ---------------------------------------------
*/

// List of fruits and vegetables with translations and image references

let fruits = [
    {"eng": "apple",            "hun": "alma",                  "esp": "manzana",           "img": "apple.jpg"},
    {"eng": "banana",           "hun": "banán",                 "esp": "banana",            "img": "banana.jpg"},
    {"eng": "beetroot",         "hun": "cékla",                 "esp": "remolacha",         "img": "beetroot.jpg"},
    {"eng": "bell pepper",      "hun": "kaliforniai paprika",   "esp": "pimiento morrón",   "img": "bellpepper.jpg"},
    {"eng": "cabbage",          "hun": "káposzta",              "esp": "repollo",           "img": "cabbage.jpg"},
    {"eng": "carrot",           "hun": "sárgarépa",             "esp": "zanahoria",         "img": "carrot.jpg"},
    {"eng": "cauliflower",      "hun": "karfiol",               "esp": "coliflor",          "img": "cauliflower.jpg"},
    {"eng": "chili pepper",     "hun": "chili paprika",         "esp": "guindilla",         "img": "chilipepper.jpg"},
    {"eng": "corn",             "hun": "kukorica",              "esp": "maíz",              "img": "corn.jpg"},
    {"eng": "cucumber",         "hun": "uborka",                "esp": "pepino",            "img": "cucumber.jpg"},
    {"eng": "eggplant",         "hun": "padlizsán",             "esp": "berenjena",         "img": "eggplant.jpg"},
    {"eng": "garlic",           "hun": "fokhagyma",             "esp": "ajo",               "img": "garlic.jpg"},
    {"eng": "ginger",           "hun": "gyömbér",               "esp": "jengibre",          "img": "ginger.jpg"},
    {"eng": "grapes",           "hun": "szőlő",                 "esp": "uvas",              "img": "grapes.jpg"},
    {"eng": "jalapeno",         "hun": "jalapeno",              "esp": "jalapeño",          "img": "jalapeno.jpg"},
    {"eng": "kiwi",             "hun": "kiwi",                  "esp": "kiwi",              "img": "kiwi.jpg"},
    {"eng": "lemon",            "hun": "citrom",                "esp": "limón",             "img": "lemon.jpg"},
    {"eng": "lettuce",          "hun": "saláta",                "esp": "lechuga",           "img": "lettuce.jpg"},    
    {"eng": "mango",            "hun": "mangó",                 "esp": "mango",             "img": "mango.jpg"},
    {"eng": "onion",            "hun": "vöröshagyma",           "esp": "cebolla",           "img": "onion.jpg"},
    {"eng": "orange",           "hun": "narancs",               "esp": "naranja",           "img": "orange.jpg"},
    {"eng": "pear",             "hun": "körte",                 "esp": "pera",              "img": "pear.jpg"},
    {"eng": "peas",             "hun": "borsó",                 "esp": "guisantes",         "img": "peas.jpg"},
    {"eng": "pineapple",        "hun": "ananász",               "esp": "piña",              "img": "pineapple.jpg"},
    {"eng": "pomegranate",      "hun": "gránátalma",            "esp": "granada",           "img": "pomegranate.jpg"},
    {"eng": "potato",           "hun": "burgonya",              "esp": "papa",              "img": "potato.jpg"},
    {"eng": "raddish",          "hun": "retek",                 "esp": "rábano",            "img": "raddish.jpg"},
    {"eng": "soy beans",        "hun": "szójabab",              "esp": "frijoles de soja",  "img": "soybeans.jpg"},
    {"eng": "spinach",          "hun": "spenót",                "esp": "espinaca",          "img": "spinach.jpg"},
    {"eng": "tomato",           "hun": "paradicsom",            "esp": "tomate",            "img": "tomato.jpg"},
    {"eng": "turnip",           "hun": "fehérrépa",             "esp": "nabo",              "img": "turnip.jpg"},
    {"eng": "watermelon",       "hun": "görögdinnye",           "esp": "sandía",            "img": "watermelon.jpg"}
];

/*
    Text resources used throughout the UI
    Indexed access is used for language switching
*/

const Texts = [
    {"eng": "fruits and vegetables", "hun": "gyümölcsök és zöldségek", "esp": "frutas y verduras"},
    {"eng": "START", "hun": "START", "esp": "START"},
    {"eng": "Game finished", "hun": "Játék vége", "esp": "Juego terminado"},
    {"eng": "Time", "hun": "Idő", "esp": "Tiempo"},
    {"eng": "Total flips", "hun": "Összes fordítás", "esp": "volteos totales"},
    {"eng": "Difficulty", "hun": "Nehézség", "esp": "Dificultad"},
    {"eng": "Play again", "hun": "Új játék", "esp": "Jugar de nuevo"},
    {"eng": "Back to menu", "hun": "Vissza a menübe", "esp": "Volver al menú"},
    {"eng": "This game is a memory game developed for the technician exam. The theme of the game is fruits and vegetables. <br><br>During the game, the memory cards have pictures and names of fruits and vegetables. These must be paired with each other. <br><br>In addition to entertainment, there is also an educational opportunity.  Young children can learn the names of fruits and vegetables in 3 languages ​​while playing."
     ,"hun": "Ez a játék egy memóriajáték, amelyet technikus vizsgára fejlesztettek ki. A játék témája a gyümölcsök és zöldségek. <br><br> A játék során a memória kártyákon gyümölcs és zöldség képek és nevek szerepelnek. Ezeket kell párosítsani egymással.  <br><br> A szórakozás mellett oktatási lehetőség is adott. A kis gyermekek játék során tanulhatják a gyümölcsök és zöldségek nevét 3 nyelven."
     , "esp": "Este juego es un juego de memoria desarrollado para el examen de técnico. El tema del juego son las frutas y verduras. <br><br> Durante el juego, las tarjetas de memoria contienen imágenes y nombres de frutas y verduras. Debes emparejarlas entre sí. <br> <br> Además de entretener, también ofrece una oportunidad educativa. Los niños pequeños pueden aprender los nombres de frutas y verduras en 3 idiomas mientras juegan."
    },
    {
        "eng": "The size of the game can be set in the menu using the button next to the START button. You can change the size of the game by clicking on the button. The current size can be read from the button. <br><br> <img src='./assets/help-images/game-size.png' class='game-size-image'>",
        "hun": "A játék nagyságát a menüben lehet beállítani a START gomb meletti gombbal. A gombra kattintva lehet váltani a játék méretét. Az aktuális méretet a gombról lehet leolvasni. <br><br> <img src='./assets/help-images/game-size.png' class='game-size-image'>",
        "esp": "El tamaño del juego se puede ajustar en el menú mediante el botón situado junto al botón INICIO. Para cambiar el tamaño del juego, basta con hacer clic en dicho botón. El tamaño actual se muestra en el mismo. <br><br> <img src='./assets/help-images/game-size.png' class='game-size-image'>"
    },
    {
        "eng": "The language of the game can be set in the menu. The right-hand button of the bottom buttons can be used to bring up the selection options. If you move the cursor towards the button, the selectable languages ​​will appear above it. While you hold the cursor over these two elements, this will appear. As soon as you move the cursor away from it, the selection element will disappear. <br><br> <img src='./assets/help-images/language-image.png' class='language-image'>"
        ,"hun": "A játék nyelvezét a menüben lehet állítani. Az alsó gombok közül a jobb oldalival lehet előhívni a választási lehetőségeket. Amennyiben a kurzort a gomb felé visszük megjelenik felette a a választható nyelvek. Míg ezen a kettő elemen tartjuk a kurzort addik jelenik ez meg. Amint elvisszük róla, eltűnik a választhatási elem. <br><br> <img src='./assets/help-images/language-image.png' class='language-image'>"
        ,"esp": "El idioma del juego se puede configurar en el menú. El botón derecho de la barra inferior permite acceder a las opciones de idioma. Al mover el cursor hacia este botón, aparecerán los idiomas disponibles. Mientras mantengas el cursor sobre estos dos elementos, el menú de idiomas permanecerá visible. Al alejar el cursor, el menú desaparecerá. <br><br> <img src='./assets/help-images/language-image.png' class='language-image'>"
    },
    {
        "eng": "The player selects the cards by clicking on them. After the second choice, the system checks whether they match. If they do, they turn green and are fixed, but if they don't, they turn red, vibrate, and turn back. <br><br> <img src='./assets/help-images/how-to-play-image.png' class='language-image'>",
        "hun": "A kártyákra kattintva kiválasztja őket a játékos. A második választás után a rendszer leellenőrzi hogy egyeznek-e, amennyiben igen, abban az esetben zöldek lesznek, és rögzülnek, de ha nem, akkor pirosra váltanak, rezegnek, és visszafordulnak. <br><br> <img src='./assets/help-images/how-to-play-image.png' class='language-image'>",
        "esp": "El jugador selecciona las cartas haciendo clic sobre ellas. Tras la segunda selección, el sistema comprueba si coinciden. Si coinciden, se vuelven verdes y permanecen fijas; si no, se vuelven rojas, vibran y vuelven a su posición original. <br><br> <img src='./assets/help-images/how-to-play-image.png' class='language-image'>"
    },
    {"eng": "RESTART", "hun": "ÚJRAINDÍTÁS", "esp": "REANUDAR"},
    {"eng": "MENU", "hun": "MENÜ", "esp": "MENÚ"},
    {"eng": "About the game", "hun": "A játékról", "esp": "Acerca del juego"},
    {"eng": "Set Game Size", "hun": "Játékméret váltás", "esp": "Establecer el tamaño del juego"},
    {"eng": "Change language", "hun": "Nyelv megváltoztatása", "esp": "Cambiar idioma"},
    {"eng": "How to play the game", "hun": "Hogyan kell játszani", "esp": "Cómo jugar el juego"},
    {"eng": "Welcome", "hun": "Üdvözöllek", "esp": "Hola"},
    {"eng": "New Record!", "hun": "Új Rekord!", "esp": "¡Nuevo récord!"}
];

/*
    ---------------------------------------------
    LANGUAGE HANDLING
    ---------------------------------------------
*/

// Default language
let lang = "eng";

// Switches language and updates static menu texts
function switchLang(newLang) {
    lang = newLang;
    document.getElementById("h5").textContent = Texts[0][lang];
    document.getElementById("play-button").textContent = Texts[1][lang];
}

/*
    ---------------------------------------------
    USER DATA HANDLING
    ---------------------------------------------
*/

// Load user data from backend (if authenticated)
const userData = await getUserData();
let user;
if (userData != undefined) user = userData;

/*
    ---------------------------------------------
    DOM ELEMENT REFERENCES
    ---------------------------------------------
*/

// Main containers and controls
const gameContainer = document.getElementById("game-container");
const menuContainer = document.getElementById("menu");
const gameModeBtn = document.getElementById("gamemode-button");
const mainMenuBtn = document.getElementById("main-menu");
const leaderboardBtn = document.getElementById("leaderboard");
const helpBtn = document.getElementById("help");
const languageBtn = document.getElementById("language");
const languageButtonsContainer = document.getElementById("language-buttons");

// End screen elements
const endScreen = document.getElementById("end-screen");
const endScreenTimeArea = document.getElementById("end-screen-time");
const endScreenTotalFlipsArea = document.getElementById("end-screen-total-flips");
const endScreenDifficultyArea = document.getElementById("end-screen-difficulty");

// Info panel during gameplay
const infoPanel = document.getElementById("info-panel");
const infoTimeArea = document.getElementById("info-panel-time");
const infoFlippingArea = document.getElementById("info-panel-total-flips");
const infoDifficultyArea = document.getElementById("info-panel-difficulty");

// Buttons
const playAgainBtn = document.getElementById("end-screen-play-again");
const menuBtn = document.getElementById("end-screen-menu");
const restartBtn = document.getElementById("restart-button");
const infoMenuBtn = document.getElementById("menu-button");

// Help panel elements
const helpPanel = document.getElementById("help-panel");
const helpAboutBtn = document.getElementById("help-about-button");
const helpGameSizeBtn = document.getElementById("help-game-size-button");
const helpChangeLanguageBtn = document.getElementById("help-change-language-button");
const helpHowToPlayBtn = document.getElementById("help-how-to-play-button");
const backdropElement = document.getElementById("backdrop-element");
const helpRightSide = document.getElementById("help-right-side");

// New record indicator
const newRecordBlock = document.getElementById("newRecordBlock");

/*
    ---------------------------------------------
    GAME STATE VARIABLES
    ---------------------------------------------
*/

// Game size (board dimension)
let gameSize = 2;

// Runtime state
let gameCards = [];
let cardTypes = [];
let selectedCards = [];
let seconds = 0;
let minutes = 0;
let hours = 0;
let playing = false;
let flipping = 0;
let timerId = null;

/*
    ---------------------------------------------
    GAME LOGIC
    ---------------------------------------------
*/

// Resets all runtime variables for starting a new game
function reset() {
    playing = true;        // Enables the game loop
    flipping = 0;          // Reset flip counter
    seconds = 0;           // Reset time
    minutes = 0;
    hours = 0;
    selectedCards = [];    // Clear currently selected cards
}

// Selects random unique card types for the board
function getCardTypes() {
    cardTypes = [];
    let cardNumber = (gameSize ** 2) / 2; // Number of unique pairs needed

    for (let i = 0; i < cardNumber; i++) {
        // Pick a random fruit
        let newCard = fruits[Math.floor(Math.random() * fruits.length)];

        // Ensure uniqueness (no duplicate card types)
        while (cardTypes.includes(newCard)) {
            newCard = fruits[Math.floor(Math.random() * fruits.length)];
        }

        cardTypes.push(newCard);
    }
}

// Creates the gameCards array by duplicating each card type (pairing)
function fillGameArray() {
    gameCards = [];
    selectedCards = [];

    // Each card type is added twice to form a pair
    for (let i in cardTypes) {
        gameCards.push(cardTypes[i]);
        gameCards.push(cardTypes[i]);
    }
}

// Shuffles the cards using the Fisher–Yates algorithm
function shuffleArray() {
    for (let i = gameCards.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [gameCards[i], gameCards[j]] = [gameCards[j], gameCards[i]];
    }
}

// Triggered when the selected cards do NOT match
function badPairing() {
    setTimeout(() => {
        // Get DOM elements of the selected cards
        let firstelement = document.getElementById(`${selectedCards[0][1]}`);
        let secondelement = document.getElementById(`${selectedCards[1][1]}`);
        let firstname = document.getElementById(`n${selectedCards[0][1]}`);
        let secondname = document.getElementById(`n${selectedCards[1][1]}`);

        // Shake animation + red overlay
        firstelement.style.animation = "shake 0.5s forwards";
        secondelement.style.animation = "shake 0.5s forwards";
        firstelement.style.backgroundImage =
            `linear-gradient(rgba(255, 0, 0, 0.4), rgba(255, 0, 0, 0.5)), url(./assets/fruit-images/${selectedCards[0][0].img})`;
        secondelement.style.backgroundImage =
            `linear-gradient(rgba(255, 0, 0, 0.4), rgba(255, 0, 0, 0.5)), url(./assets/fruit-images/${selectedCards[1][0].img})`;

        setTimeout(() => {
            // Flip cards back
            firstelement.style.animation = "rotation-back 0.5s forwards";
            secondelement.style.animation = "rotation-back 0.5s forwards";

            setTimeout(() => {
                // Reset visuals
                firstelement.style.backgroundImage = "none";
                secondelement.style.backgroundImage = "none";
                firstname.style.display = "none";
                secondname.style.display = "none";
                firstelement.style.background = "linear-gradient(to right, darkgray,lightslategray);";
                secondelement.style.background = "linear-gradient(to right, darkgray,lightslategray);";
            }, 150);

            // Remove both selected cards
            selectedCards.pop();
            selectedCards.pop();
        }, 300);
    }, 500);

    // Increase flip counter
    flipping += 1;
}

// Triggered when the selected cards DO match
function goodPairing() {
    if (gameCards.length == 0) playing = false;

    setTimeout(() => {
        // Green overlay for correct match
        document.getElementById(`${selectedCards[0][1]}`).style.backgroundImage =
            `linear-gradient(rgba(0, 255, 0, 0.4), rgba(0, 255, 0, 0.5)), url(./assets/fruit-images/${selectedCards[0][0].img})`;
        document.getElementById(`${selectedCards[1][1]}`).style.backgroundImage =
            `linear-gradient(rgba(0, 255, 0, 0.4), rgba(0, 255, 0, 0.5)), url(./assets/fruit-images/${selectedCards[1][0].img})`;

        // Remove matched cards from gameCards to disable further selection
        gameCards = gameCards.filter(obj => obj !== selectedCards[0][0]);
        gameCards = gameCards.filter(obj => obj !== selectedCards[1][0]);

        // Clear selected cards
        selectedCards.pop();
        selectedCards.pop();

        // End game if no cards remain
        if (gameCards.length == 0) {
            playing = false;
            infoPanel.style.animation = "gameFadeOut 2s forwards";
            gameContainer.style.animation = "gameFadeOut 2s forwards";
            setTimeout(displayEndScreen, 1500);
        }
    }, 300);

    flipping += 1;
}

// Compares the two selected cards
function checkCards() {
    if (selectedCards[0][0].eng == selectedCards[1][0].eng) {
        goodPairing();
    } else {
        badPairing();
    }
}

// Sets CSS classes based on the selected board size
function setBoardStyles() {
    switch (gameSize) {
        case 4:
            gameContainer.classList.remove("two", "six", "eight");
            gameContainer.classList.add("four");
            break;
        case 2:
            gameContainer.classList.remove("four", "six", "eight");
            gameContainer.classList.add("two");
            break;
        case 6:
            gameContainer.classList.remove("two", "four", "eight");
            gameContainer.classList.add("six");
            break;
        case 8:
            gameContainer.classList.remove("two", "four", "six");
            gameContainer.classList.add("eight");
            break;
    }
}

// Renders the game board and creates all card elements
function displayBoard() {
    setBoardStyles();
    infoPanel.style.display = "block";
    gameContainer.innerHTML = "";
    gameContainer.style.display = "flex";

    // Create card DOM elements
    gameCards.forEach((element, index) => {
        const newCard = document.createElement("div");

        // Assign card size class
        newCard.className = `card${gameSize}`;
        newCard.id = index;

        // Name label for the card
        let newName = document.createElement("div");
        newName.className = "name";
        newName.id = `n${index}`;
        newName.textContent = element[lang];

        // Card click handler
        newCard.addEventListener("click", () => {
            function choosingCard() {
                // Only allow valid selections
                if (
                    selectedCards.length < 2 &&
                    gameCards.includes(element) &&
                    !selectedCards.some(([el, idx]) => el === element && idx === index)
                ) {
                    selectedCards.push([element, index]);
                    newCard.style.animation = "rotation 0.5s forwards";
                    newCard.style.background = "linear-gradient(to right, darkgray,lightslategray);";

                    setTimeout(() => {
                        newCard.style.backgroundImage = `url(./assets/fruit-images/${element.img})`;
                        newName.style.display = "block";
                    }, 100);

                    // When two cards are selected, check for match
                    if (selectedCards.length == 2)
                        setTimeout(checkCards(), 3000);
                }
            }

            choosingCard();
        });

        newCard.appendChild(newName);
        gameContainer.appendChild(newCard);
    });
}

// Updates the information panel during gameplay (time, flips, buttons text)
function updateInfo() {

    // Display full time format if hours, minutes and seconds are present
    if (hours > 0 && minutes > 0 && seconds > 0) {
        infoTimeArea.textContent = `${Texts[3][lang]}: ${hours}:${minutes}:${seconds}`;
    }
    // Display minutes and seconds if hours are zero
    else if (hours == 0 && minutes > 0 && seconds > 0) {
        infoTimeArea.textContent = `${Texts[3][lang]}: ${minutes}:${seconds}`;
    }
    // Display only seconds if minutes and hours are zero
    else {
        infoTimeArea.textContent = `${Texts[3][lang]}: ${seconds}`;
    }

    // Update flip counter
    infoFlippingArea.textContent = `${Texts[4][lang]}: ${flipping}`;

    // Update button texts based on current language
    restartBtn.textContent = Texts[12][lang];
    infoMenuBtn.textContent = Texts[13][lang];
}

// Generates a new game and initializes all required systems
function makeGame() {

    // Stop the existing game updater loop if it exists
    if (timerId !== null) {
        clearTimeout(timerId);
    }

    // Reset animations to prevent conflicts
    gameContainer.style.animation = "none";
    infoPanel.style.animation = "none";

    // Generate the game structure
    getCardTypes();     // Select random card types
    fillGameArray();    // Create card pairs
    shuffleArray();     // Shuffle cards
    displayBoard();     // Render cards on screen
    reset();            // Reset runtime values
    updateInfo();       // Update UI

    // Start the game loop updater
    timerId = setTimeout(updateGame, 1000);
}

// Infinite game updater responsible for time, UI updates and difficulty display
function updateGame() {

    // Increase time only if there are still cards remaining
    if (gameCards.length != 0) seconds += 1;

    // Convert seconds to minutes
    if (seconds % 60 == 0 && seconds != 0) {
        seconds = 0;
        minutes += 1;
    }

    // Convert minutes to hours
    if (minutes % 60 == 0 && minutes != 0) {
        minutes = 0;
        hours += 1;
    }

    // Refresh info panel
    updateInfo();

    // Display current difficulty
    switch (gameSize) {
        case 2:
            infoDifficultyArea.textContent = `${Texts[5][lang]}: 2x2`;
            break;
        case 4:
            infoDifficultyArea.textContent = `${Texts[5][lang]}: 4x4`;
            break;
        case 6:
            infoDifficultyArea.textContent = `${Texts[5][lang]}: 6x6`;
            break;
        case 8:
            infoDifficultyArea.textContent = `${Texts[5][lang]}: 8x8`;
            break;
    }

    // Continue updating if the game is still active
    if (playing) {
        timerId = setTimeout(updateGame, 1000);
    }
}

// Displays the end screen after finishing the game
function displayEndScreen() {

    // Hide game UI
    infoPanel.style.display = "none";
    infoPanel.style.animation = " ";
    gameContainer.style.display = "none";
    gameContainer.style.animation = " ";

    // Show end screen
    endScreen.style.display = "block";
    endScreen.style.animation = "scaling 1s forwards";
    endScreenTimeArea.style.animation = " ";

    // Display final time
    if (hours > 0 && minutes > 0 && seconds > 0) {
        endScreenTimeArea.textContent = `${Texts[3][lang]}: ${hours}:${minutes}:${seconds}`;
    }
    else if (hours == 0 && minutes > 0 && seconds > 0) {
        endScreenTimeArea.textContent = `${Texts[3][lang]}: ${minutes}:${seconds}`;
    }
    else {
        endScreenTimeArea.textContent = `${Texts[3][lang]}: ${seconds}`;
    }

    // Display localized end screen texts
    document.getElementById("end-screen-h1").textContent = `${Texts[2][lang]}`;
    endScreenTotalFlipsArea.style.animation = " ";
    endScreenTotalFlipsArea.textContent = `${Texts[4][lang]}: ${flipping}`;
    playAgainBtn.textContent = `${Texts[6][lang]}!`;
    menuBtn.textContent = `${Texts[7][lang]}!`;

    // Display difficulty on end screen
    switch (gameSize) {
        case 2:
            endScreenDifficultyArea.textContent = `${Texts[5][lang]}: 2x2`;
            break;
        case 4:
            endScreenDifficultyArea.textContent = `${Texts[5][lang]}: 4x4`;
            break;
        case 6:
            endScreenDifficultyArea.textContent = `${Texts[5][lang]}: 6x6`;
            break;
        case 8:
            endScreenDifficultyArea.textContent = `${Texts[5][lang]}: 8x8`;
            break;
    }

    // Update statistics only for the hardest difficulty
    if (gameSize == 8) {
        changeStat();
    }
}

// Updates the help panel content based on the selected page
function updateHelp(page) {

    // Update help menu button labels
    helpAboutBtn.textContent = `${Texts[14][lang]}`;
    helpGameSizeBtn.textContent = `${Texts[15][lang]}`;
    helpChangeLanguageBtn.textContent = `${Texts[16][lang]}`;
    helpHowToPlayBtn.textContent = `${Texts[17][lang]}`;

    // Switch between help pages
    switch (page) {
        case 0:
            helpRightSide.innerHTML = `${Texts[8][lang]}`;
            helpAboutBtn.style.borderRight = "0px solid black";
            helpGameSizeBtn.style.borderRight = "2px solid black";
            helpChangeLanguageBtn.style.borderRight = "2px solid black";
            helpHowToPlayBtn.style.borderRight = "2px solid black";
            break;
        case 1:
            helpRightSide.innerHTML = `${Texts[9][lang]}`;
            helpGameSizeBtn.style.borderRight = "0px solid black";
            helpAboutBtn.style.borderRight = "2px solid black";
            helpChangeLanguageBtn.style.borderRight = "2px solid black";
            helpHowToPlayBtn.style.borderRight = "2px solid black";
            break;
        case 2:
            helpRightSide.innerHTML = `${Texts[10][lang]}`;
            helpChangeLanguageBtn.style.borderRight = "0px solid black";
            helpGameSizeBtn.style.borderRight = "2px solid black";
            helpAboutBtn.style.borderRight = "2px solid black";
            helpHowToPlayBtn.style.borderRight = "2px solid black";
            break;
        case 3:
            helpRightSide.innerHTML = `${Texts[11][lang]}`;
            helpHowToPlayBtn.style.borderRight = "0px solid black";
            helpGameSizeBtn.style.borderRight = "2px solid black";
            helpAboutBtn.style.borderRight = "2px solid black";
            helpChangeLanguageBtn.style.borderRight = "2px solid black";
            break;
    }
}

// Displays the help panel and initializes page navigation
function displayHelp(){

    // Show the dark background and the help panel
    backdropElement.style.display = "block";
    helpPanel.style.display = "flex";

    // Default help page index
    let page = 0;

    // Load the initial help page
    updateHelp(page);

    // Switch to the "About" help page
    helpAboutBtn.addEventListener("click", () => {
        page = 0;
        updateHelp(page);
    });

    // Switch to the "Game Size" help page
    helpGameSizeBtn.addEventListener("click", () => {
        page = 1;
        updateHelp(page);
    });

    // Switch to the "Change Language" help page
    helpChangeLanguageBtn.addEventListener("click", () => {
        page = 2;
        updateHelp(page);
    });

    // Switch to the "How To Play" help page
    helpHowToPlayBtn.addEventListener("click", () => {
        page = 3;
        updateHelp(page);
    });

    // Close the help panel when clicking on the background
    backdropElement.addEventListener("click", () => {
        backdropElement.style.display = "none";
        helpPanel.style.display = "none";
    })
}

// Start the game when the Play button is clicked
document.getElementById("play-button").addEventListener("click", () => {
    makeGame();
    menuContainer.style.display = "none";
    gameContainer.style.display = "flex";
    gameContainer.style.animation = "scaling 1s forwards";
});

// Available game modes (grid sizes)
let gameModes = [2,4,6,8];
let index = 0;

// Cycle through game modes when clicking the game mode button
gameModeBtn.addEventListener("click", () => {
    index = index + 1;
    if (index > 3) index = 0;

    gameSize = gameModes[index];

    // Update button text based on selected mode
    switch (index) {
        case 0:
            gameModeBtn.textContent = "2x2";
            break;
        case 1:
            gameModeBtn.textContent = "4x4";
            break;
        case 2:
            gameModeBtn.textContent = "6x6";
            break;
        case 3:
            gameModeBtn.textContent = "8x8";
            break;
        default:
            break;
    }
});

// Show language selection buttons when hovering the language button
languageBtn.addEventListener("mouseover", () => {
    languageButtonsContainer.style.display = "flex";
    languageButtonsContainer.style.position = "absolute";

    // Keep the language menu visible while hovering it
    languageButtonsContainer.addEventListener("mouseover", ()=> {
        languageButtonsContainer.style.display = "flex";
        languageButtonsContainer.style.position = "absolute";
    });

    // Hide language menu when mouse leaves it
    languageButtonsContainer.addEventListener("mouseout", () => {
        languageButtonsContainer.style.display = "none";
        languageButtonsContainer.style.position = "relative";
    });
});

// Hide language selection when mouse leaves the language button
languageBtn.addEventListener("mouseout", () => {
    languageButtonsContainer.style.display = "none";
    languageButtonsContainer.style.position = "relative";
});

// Select English language
document.getElementById("eng").addEventListener("click", () => {
    switchLang("eng");
});

// Select Hungarian language
document.getElementById("hun").addEventListener("click", () => {
    switchLang("hun");
});

// Select Spanish language
document.getElementById("esp").addEventListener("click", () => {
    switchLang("esp");
});

// Return to the main menu from the end screen
menuBtn.addEventListener("click", () => {
    endScreen.style.animation = "gameFadeOut 2s forwards";
    newRecordBlock.style.display = "none";

    setTimeout(() => {
        endScreen.style.display = "none";
        menuContainer.style.display = "block";
        menuContainer.style.animation = "scaling 1s forwards";
    }, 1500);  
});

// Start a new game from the end screen
playAgainBtn.addEventListener("click", ()=>{
    endScreen.style.animation = "gameFadeOut 2s forwards";
    newRecordBlock.style.display = "none";

    setTimeout(() => {
        endScreen.style.display = "none";
        makeGame();
    }, 1500);
});

// Open the help panel
helpBtn.addEventListener("click", displayHelp);

// Restart the game during gameplay
restartBtn.addEventListener("click", makeGame);

// Return to the main menu from the game
infoMenuBtn.addEventListener("click", () => {
    playing = false;
    gameContainer.style.animation = "gameFadeOut 2s forwards";
    infoPanel.style.animation = "gameFadeOut 2s forwards";

    setTimeout(() => {
        gameContainer.style.display = "none";
        infoPanel.style.display = "none";
        menuContainer.style.display = "block";
        menuContainer.style.animation = "scaling 1s forwards";
    }, 1500); 
});

// Displays a welcome message for logged-in users
function DisplayWelcomeBox() {
    if (user != undefined) {
        const welcomeDiv = document.getElementById("welcome-div");
        const welcomeP = document.getElementById("welcome-p");

        let displayText;
        var userLanguage = sessionStorage.getItem("user-language");

        // Choose welcome text language
        if (userLanguage == "eng" || userLanguage == "hun" || userLanguage == "esp")
            displayText = `${Texts[18][userLanguage]} ${user.userName}!`;
        else
            displayText = `${Texts[18][lang]} ${user.userName}!`;

        welcomeP.textContent = displayText;

        // Play welcome animation
        welcomeDiv.style.animation = "rollingDown 1s forwards";
        setTimeout(() => {
            welcomeDiv.style.animation = "rollingUp 1s forwards";
        }, 1500);
    }
}

// Show welcome message on page load
DisplayWelcomeBox(); 

// Navigate to main menu page
mainMenuBtn.addEventListener("click", () => {
    window.location.href = "https://localhost:7234"; 
});

// Navigate to leaderboard page
leaderboardBtn.addEventListener("click", () => {
    // Leaderboard navigation placeholder
});

// Fetch logged-in user's data from the backend
async function getUserData() {

    var sessionRefreshToken = sessionStorage.getItem("RefreshToken");
    var token = sessionStorage.getItem("Token");
    let userData;

    // Only attempt request if tokens exist
    if(sessionRefreshToken != undefined && token != undefined){

        // Retry up to 5 times
        for (let i = 0; i < 5; i++) {
            const response = await fetch("https://localhost:7234/api/Users/getuserdata", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${token}`
                },
                body: JSON.stringify({
                    Token: sessionRefreshToken
                })
            });

            if (response.ok) {
                userData = await response.json();
            }
        }
        return userData;
    }
}

// Updates the user's best game statistics
async function changeStat() {

    // Checks if the new time is better (lower) than the stored time
    function isNewTimeLesser(newTime, oldTime) {
        let newTimeSecs = newTime[2] + newTime[1]*60 + newTime[0]*60*60;
        let oldTimeSecs = oldTime[2] + oldTime[1]*60 + oldTime[0]*60*60;
        return oldTimeSecs > newTimeSecs;
    }

    // Checks if the new flip count is better (lower)
    function isNewFlipsLesser (newSlips, oldSlips) {
        return oldSlips > newSlips;
    }

    // Fetch user's best game statistics
    async function getUserStatusData(token) {
        const response =  await fetch("https://localhost:7234/api/MemoryGameStatus", {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}` 
            }
        });

        if (response.ok){
            let data = await response.json();
            return data;
        }
    }

    // Upload updated statistics to the backend
    function uploadNewStatus(newStatus) {
        var response = fetch("https://localhost:7234/api/MemoryGameStatus", {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
            body: JSON.stringify(newStatus)
        });

        // Retry after refreshing token if request fails
        if(!response.ok) {
            refreshToken(sessionRefreshToken);
            fetch("https://localhost:7234/api/MemoryGameStatus", {
                method: "GET",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${token}` 
                },
                body: JSON.stringify(newStatus)
            });
        }
    }

    var sessionRefreshToken = sessionStorage.getItem("RefreshToken");
    var token = sessionStorage.getItem("Token");

    let userBestGameStatus;

    // Only proceed if authentication tokens exist
    if (sessionRefreshToken != undefined && token != undefined) {

        // Get stored best results
        userBestGameStatus = await getUserStatusData(token);
        userBestGameStatus = userBestGameStatus.data;

        let newTimeRecord = false;
        let newFlipRecord = false;

        // Default new status object
        let newStatus = {
            minTime: [0,0,0],
            minFlipping: 0
        };

        endScreenTimeArea.style.animation = " ";
        endScreenTotalFlipsArea.style.animation = " ";

        // Check for new time record
        if (isNewTimeLesser([hours, minutes, seconds], userBestGameStatus.minTime)) { 
            newStatus.minTime = [hours, minutes, seconds];
            endScreenTimeArea.style.animation = "newRecordBlockAnimation 1s infinite";  
            newTimeRecord = true;
        }

        // Check for new flip record
        if (isNewFlipsLesser(flipping, userBestGameStatus.minFlipping)) {
            newStatus.minFlipping = flipping;
            endScreenTotalFlipsArea.style.animation = "newRecordBlockAnimation 1s infinite";
            newFlipRecord = true;
        }

        // Upload new records if any were beaten
        if (newFlipRecord || newTimeRecord) {
            uploadNewStatus(newStatus);
            newRecordBlock.textContent = Texts[19][lang];
            newRecordBlock.style.animation = "newRecordBlockAnimation 1s infinite";

            setTimeout(() => {
                newRecordBlock.style.display = "block";
            },1000);
        }
    }
}
