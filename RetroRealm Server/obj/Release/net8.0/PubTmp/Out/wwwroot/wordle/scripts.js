// Importing the list of valid words for the Wordle game
import words from "./words.js";

// Importing localized UI texts (multi-language support)
import texts from "./texts.js";

// Importing token refresh logic for authenticated backend calls
import refreshToken from "../scripts/tokenRefresher.js";

/* ================================================================
   DOM REFERENCES – GAME GRID
================================================================ */

// Each variable represents a row in the Wordle grid
let firstRow = document.getElementById('first-row');
let secondRow = document.getElementById('second-row');
let thirdRow = document.getElementById('third-row');
let fourthRow = document.getElementById('fourth-row');
let fifthRow = document.getElementById('fifth-row');
let sixthRow = document.getElementById('sixth-row');

/* ================================================================
   GLOBAL GAME STATE
================================================================ */

// The randomly selected target word
let word;

// Prevents input during animations
let isAnimating = false;

// Current language setting
let lang = "eng";

/* ================================================================
   BACKEND STATISTICS UPDATE
================================================================ */

// Updates the player's statistics on the backend
// Uses stored JWT tokens and attempts token refresh on failure
function changeStat() {
    const sessionRefreshToken = sessionStorage.getItem("RefreshToken");
    const token = sessionStorage.getItem("Token");

    // Abort if authentication data is missing
    if (sessionRefreshToken != undefined && token != undefined) {

        // Send statistics update request
        let response = fetch("https://localhost:7234/api/WordleStatus", {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
            body: JSON.stringify({
                CompletedWords: 0
            })
        });

        // If request fails, try refreshing the token and retry
        if (!response.ok) {
            refreshToken(sessionRefreshToken);

            fetch("https://localhost:7234/api/WordleStatus", {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${token}`
                },
                body: JSON.stringify({
                    CompletedWords: 0
                })
            });
        }

        return true;
    }

    return false;
}

/* ================================================================
   GAME START / MENU HANDLING
================================================================ */

let gameEnded = false;

const playBtn = document.getElementById("play-button");

const menu = document.getElementById("menu");

const backToMainMenuBtn = document.getElementById("main-menu");
backToMainMenuBtn.addEventListener("click", () => {
    window.location.href = "../htmls/pc.html"
});

const gameContainer = document.getElementById("game-container");

// Start game from menu
playBtn.addEventListener("click", () => {
    menu.style.display = "none";
    gameContainer.style.display = "block";
    startGame();
});

/* ================================================================
   WORD SELECTION
================================================================ */

// Returns a random element from an array
function getRandomElement(arr) {
    const randomIndex = Math.floor(Math.random() * arr.length);
    return arr[randomIndex];
}

/* ================================================================
   KEYBOARD CONFIGURATION
================================================================ */

// Allowed input characters
const allowedChars = [
    'a','b','c','d','e','f','g','h','i','j','k','l','m',
    'n','o','p','q','r','s','t','u','v','w','x','y','z'
];

// Stores the color state for each keyboard key
let allowedCharsIdColors = Array(26).fill("");

// Updates a key's color using priority rules
function setKeyColor(char, newColor) {
    const index = allowedChars.indexOf(char);

    const priority = {
        "": 0,
        grey: 1,
        yellow: 2,
        green: 3
    };

    const currentColor = allowedCharsIdColors[index];

    if (priority[newColor] > priority[currentColor]) {
        allowedCharsIdColors[index] = newColor;
    }
}

/* ================================================================
   INPUT STATE
================================================================ */

let inputword = '';
let rownum = 1;

/* ================================================================
   PHYSICAL KEYBOARD INPUT
================================================================ */

document.body.addEventListener('keydown', async (event) => {
    if (gameEnded) return;

    // Add character
    if (allowedChars.includes(event.key.toLowerCase()) && inputword.length < 5) {
        inputword += event.key.toLowerCase();
        updateNextRow(rownum, inputword);
    }
    // Remove character
    else if (event.key === "Backspace" && inputword.length > 0) {
        inputword = inputword.slice(0, -1);
        updateNextRow(rownum, inputword);
    }
    // Submit word
    else if (
        event.key === 'Enter' &&
        rownum < 7 &&
        inputword.length === 5 &&
        words.includes(inputword)
    ) {
        const finished = await checkrow(rownum, inputword, word);
        if (!finished) rownum++;
        inputword = '';
    }
});

/* ================================================================
   ROW RENDERING
================================================================ */

// Updates the current row display
function updateNextRow(row, inputword) {
    const rowElement = getRowElement(row);
    if (!rowElement) return;

    const chars = inputword
        .split('')
        .concat(Array(5).fill(''))
        .slice(0, 5);

    rowElement.innerHTML = chars
        .map(char => `<div class="card"><p>${char}</p></div>`)
        .join('');
}

// Returns the DOM element for a given row index
function getRowElement(row) {
    switch (row) {
        case 1: return firstRow;
        case 2: return secondRow;
        case 3: return thirdRow;
        case 4: return fourthRow;
        case 5: return fifthRow;
        case 6: return sixthRow;
        default: return null;
    }
}

/* ================================================================
   WORD EVALUATION
================================================================ */

// Evaluates a guess and applies Wordle rules
async function checkrow(row, inputword, word) {
    if (isAnimating) return true;
    isAnimating = true;

    const rowElement = getRowElement(row);
    if (!rowElement) {
        isAnimating = false;
        return true;
    }

    const targetChars = word.split('');
    const guessChars = inputword.split('');
    let ids = Array(5).fill("");

    // Count target letter occurrences
    const letterCount = {};
    for (let i = 0; i < 5; i++) {
        letterCount[targetChars[i]] = (letterCount[targetChars[i]] || 0) + 1;
    }

    // Correct position (green)
    for (let i = 0; i < 5; i++) {
        if (guessChars[i] === targetChars[i]) {
            ids[i] = "green";
            letterCount[guessChars[i]]--;
        }
    }

    // Wrong position (yellow) or not present (grey)
    for (let i = 0; i < 5; i++) {
        if (ids[i]) continue;

        if (letterCount[guessChars[i]] > 0) {
            ids[i] = "yellow";
            letterCount[guessChars[i]]--;
        } else {
            ids[i] = "grey";
        }
    }

    // Update keyboard colors
    for (let i = 0; i < 5; i++) {
        setKeyColor(inputword[i], ids[i]);
    }

    await animateRow(rowElement, guessChars, ids);
    loadkeys();
    isAnimating = false;

    const isWin = ids.every(id => id === "green");
    if (isWin || row === 6) {
        endGame(isWin);
        return true;
    }

    return false;
}

/* ================================================================
   ROW ANIMATION
================================================================ */

// Animates the card flip and color reveal
async function animateRow(rowElement, chars, ids, delay = 500) {
    for (let i = 0; i < 5; i++) {
        rowElement.innerHTML = chars.map((char, j) => {
            if (j < i) {
                return `<div class="card" id="${ids[j]}"><p>${char}</p></div>`;
            } else if (j === i) {
                return `<div class="card flipping"><p>${char}</p></div>`;
            } else {
                return `<div class="card"><p>${char}</p></div>`;
            }
        }).join('');

        await new Promise(r => setTimeout(r, delay / 2));

        const flippingCard = rowElement.querySelector('.card.flipping');
        if (flippingCard) flippingCard.id = ids[i];

        await new Promise(r => setTimeout(r, delay / 2));

        if (flippingCard) flippingCard.classList.remove('flipping');
    }
}

/* ================================================================
   VIRTUAL KEYBOARD
================================================================ */

function clickKey(key) {
    if (gameEnded || isAnimating) return;

    if (allowedChars.includes(key.toLowerCase()) && inputword.length < 5) {
        inputword += key.toLowerCase();
        updateNextRow(rownum, inputword);
    }
}

async function clickEnter() {
    if (gameEnded || isAnimating) return;

    if (rownum < 7 && inputword.length === 5 && words.includes(inputword)) {
        const finished = await checkrow(rownum, inputword, word);
        if (!finished) rownum++;
        inputword = '';
    }
}

function clickBackspace() {
    if (gameEnded || isAnimating) return;

    if (inputword.length > 0) {
        inputword = inputword.slice(0, -1);
        updateNextRow(rownum, inputword);
    }
}

// Renders the on-screen keyboard
function loadkeys() {
    let keyboard = document.getElementById('keys');
    keyboard.innerHTML = '';

    let id = 0;
    allowedChars.forEach(char => {
        keyboard.innerHTML += `
            <div class="${allowedCharsIdColors[id]} key"
                 id="key-${char}"
                 onclick='clickKey("${char}")'>${char}</div>
        `;
        id++;
    });

    keyboard.innerHTML += `
        <div class="key enter" onclick="clickEnter()">Enter</div>
        <div class="key backspace" onclick="clickBackspace()">Backspace</div>
    `;
}

loadkeys();

/* ================================================================
   GAME RESET / START / END
================================================================ */

function resetGameData() {
    allowedCharsIdColors = Array(26).fill("");
    rownum = 1;
    inputword = '';
}

function startGame() {
    menu.style.display = "none";
    endScreen.style.display = "none";
    backToMenuFromaGameBtn.style.display = "block";
    gameContainer.style.display = "block";

    word = getRandomElement(words);
    resetGameData();
    gameEnded = false;

    [firstRow, secondRow, thirdRow, fourthRow, fifthRow, sixthRow].forEach(row => {
        row.innerHTML = Array(5).fill('<div class="card"></div>').join('');
    });

    loadkeys();
}

function endGame(win) {
    gameEnded = true;
    setTimeout(() => displayEndScreen(win, word), 1200);
}

/* ================================================================
   LANGUAGE SELECTION
================================================================ */

let languageBtn = document.getElementById("language");
let languageButtonsContainer = document.getElementById("language-buttons");

languageBtn.addEventListener("mouseover", () => {
    languageButtonsContainer.style.display = "flex";
    languageButtonsContainer.style.position = "absolute";

    languageButtonsContainer.addEventListener("mouseover", () => {
        languageButtonsContainer.style.display = "flex";
        languageButtonsContainer.style.position = "absolute";
    });

    languageButtonsContainer.addEventListener("mouseout", () => {
        languageButtonsContainer.style.display = "none";
        languageButtonsContainer.style.position = "relative";
    });

    languageBtn.addEventListener("mouseout", () => {
        languageButtonsContainer.style.display = "none";
        languageButtonsContainer.style.position = "relative";
    });
});



document.getElementById("eng").addEventListener("click", () => lang = "eng");
document.getElementById("hun").addEventListener("click", () => lang = "hun");
document.getElementById("esp").addEventListener("click", () => lang = "esp");

/* ================================================================
   END SCREEN
================================================================ */

const endScreen = document.getElementById("end-screen");
const endScreenH1 = document.getElementById("end-screen-h1");
const endScreenWord = document.getElementById("end-screen-word");
const endScreenH5 = document.getElementById("end-screen-h5");
const playAgainBtn = document.getElementById("play-again-button");
const backToMenuBtn = document.getElementById("back-to-menu-button");

function displayEndScreen(win, word) {
    gameContainer.style.display = "none";
    endScreen.style.display = "block";
    backToMenuFromaGameBtn.style.display = "none";

    endScreenH1.textContent = win
        ? texts.endScreenH1WIN[lang]
        : texts.endScreenH1[lang];

    if (win && changeStat()) {
        endScreenH5.textContent = texts.endScreenH5[lang];
        endScreenH5.style.animation = "3s fadeInOut";
        endScreenH5.style.visibility = "visible";
        setTimeout(() => endScreenH5.style.visibility = "hidden", 3000);
    }

    endScreenWord.textContent = `${texts.endScreenWord[lang]} ${word}`;
    playAgainBtn.textContent = texts.endScreenPlayAgain[lang];
    backToMenuBtn.textContent = texts.endScreenBackToMenu[lang];

    playAgainBtn.addEventListener("click", startGame);
    backToMenuBtn.addEventListener("click", backToMenu);
}

function backToMenu() {
    gameContainer.style.display = "none";
    menu.style.display = "block";
    endScreen.style.display = "none";
    backToMenuFromaGameBtn.style.display = "none";
}

/* ================================================================
   HELP PANEL
================================================================ */

const helpBtn = document.getElementById("help");
const backdropElement = document.getElementById("backdrop-element");
const helpPanel = document.getElementById("help-panel");
const helpAboutTheGameBtn = document.getElementById("help-about-button");
const helpChangeLanguageBtn = document.getElementById("help-change-language-button");
const helpHowToPlayBtn = document.getElementById("help-how-to-play-button");
const helpRightSide = document.getElementById("help-right-side");

helpBtn.addEventListener("click", displayHelp);

function updateHelp(page) {
    helpAboutTheGameBtn.textContent = texts.helpAboutTheGameBtn[lang];
    helpChangeLanguageBtn.textContent = texts.helpChangeLanguageBtn[lang];
    helpHowToPlayBtn.textContent = texts.helpHowToPlayBtn[lang];

    if (page === 0) helpRightSide.innerHTML = texts.helpAboutTheGame[lang];
    if (page === 1) helpRightSide.innerHTML = texts.helpChangeLanguage[lang];
    if (page === 2) helpRightSide.innerHTML = texts.helpHowToPlay[lang];
}

function displayHelp() {
    backdropElement.style.display = "block";
    helpPanel.style.display = "flex";

    let page = 0;
    updateHelp(page);

    backdropElement.addEventListener("click", () => {
        backdropElement.style.display = "none";
        helpPanel.style.display = "none";
    });

    helpAboutTheGameBtn.onclick = () => updateHelp(page = 0);
    helpChangeLanguageBtn.onclick = () => updateHelp(page = 1);
    helpHowToPlayBtn.onclick = () => updateHelp(page = 2);
}

/* ================================================================
   BACK TO MENU BUTTON (IN-GAME)
================================================================ */

const backToMenuFromaGameBtn = document.getElementById("back-to-menu-btn");
backToMenuFromaGameBtn.addEventListener("click", backToMenu);