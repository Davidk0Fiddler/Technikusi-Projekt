import GetLogs from "./API/GetLogs.js";
import getUserName from "./API/GetUserName.js";
import GetUsers from "./API/GetUsers.js";
import DisplayDetailedLogPanel from "./DisplayDetailedLogPanel.js";
import DisplayDetailedUserPanel from "./DisplayDetailedUserPanel.js";
import LandingPanel from "./Panels/LandingPanel.js";
import LogsPanel from "./Panels/LogsPanel.js";
import UsersPanel from "./Panels/UsersPanel.js";

var users = [];
var logs = [];
var adminName;

function DisplayUsers() {
  document.body.innerHTML = UsersPanel();

  const adminNameArea = document.getElementById("admin-name");
  adminNameArea.textContent = adminName;

  const hamburgerBtn = document.getElementById("hamburger-icon");
  const closeNavigationElementBtn = document.getElementById(
    "close-navigation-element-btn",
  );
  const navigationElement = document.getElementById("navigation-element");

  let isNavigationElementOpened = false;

  function HandleNavigationElementMoves(isOpened) {
    let animation = isOpened
      ? "navigation-element-open"
      : "navigation-element-close";
    navigationElement.style.animation = `${animation} 1s forwards`;
  }

  hamburgerBtn.addEventListener("click", () => {
    isNavigationElementOpened = true;
    HandleNavigationElementMoves(isNavigationElementOpened);
  });

  closeNavigationElementBtn.addEventListener("click", () => {
    isNavigationElementOpened = false;
    HandleNavigationElementMoves(isNavigationElementOpened);
  });

  const userPanelBtn = document.getElementById("users-btn");
  userPanelBtn.addEventListener("click", DisplayUsers);

  const logPanelBtn = document.getElementById("logs-btn");
  logPanelBtn.addEventListener("click", DisplayLogs);

  const refreshedAtArea = document.getElementById("refreshed-at");

  let date = new Date();

  refreshedAtArea.textContent = `Refreshed at ${date.toLocaleString("hu-HU", {
    year: "numeric",
    month: "2-digit",
    day: "2-digit",
    hour: "2-digit",
    minute: "2-digit",
  })}`;

  const currentAllUsersArea = document.getElementById("all-users-number");
  currentAllUsersArea.textContent = users.length;

  const allAdminsArea = document.getElementById("all-admins-number");
  allAdminsArea.textContent = users.filter((u) => u.roleName == "Admin").length;

  const allUsersArea = document.getElementById("all-actual-users-number");
  allUsersArea.textContent = users.filter((u) => u.roleName == "User").length;
  const userRecordsArea = document.getElementById("user-records");
  userRecordsArea.innerHTML = " ";
  users.forEach((u) => {
    let record = document.createElement("div");
    record.className = "user-record";
    record.addEventListener("click", () => DisplayDetailedUserPanel(u));

    record.innerHTML = `
            <div class="data">
                    <div style="border-left: none;">Username: ${u.username}</div>
                    <div>Role: ${u.roleName}</div>
                    <div>Current Avatar: ${u.currentAvatarName}</div>
                    <div>Completed Achievements: ${u.completedChallangesName.length}</div>
            </div>
        `;

    userRecordsArea.appendChild(record);
  });
}

function DisplayLogs() {
  document.body.innerHTML = LogsPanel();

  const adminNameArea = document.getElementById("admin-name");
  adminNameArea.textContent = adminName;

  const hamburgerBtn = document.getElementById("hamburger-icon");
  const closeNavigationElementBtn = document.getElementById(
    "close-navigation-element-btn",
  );
  const navigationElement = document.getElementById("navigation-element");

  let isNavigationElementOpened = false;

  function HandleNavigationElementMoves(isOpened) {
    let animation = isOpened
      ? "navigation-element-open"
      : "navigation-element-close";
    navigationElement.style.animation = `${animation} 1s forwards`;
  }

  hamburgerBtn.addEventListener("click", () => {
    isNavigationElementOpened = true;
    HandleNavigationElementMoves(isNavigationElementOpened);
  });

  closeNavigationElementBtn.addEventListener("click", () => {
    isNavigationElementOpened = false;
    HandleNavigationElementMoves(isNavigationElementOpened);
  });

  const userPanelBtn = document.getElementById("users-btn");
  userPanelBtn.addEventListener("click", DisplayUsers);

  const logPanelBtn = document.getElementById("logs-btn");
  logPanelBtn.addEventListener("click", DisplayLogs);

  let date = new Date();

  const refreshedAtArea = document.getElementById("refreshed-at");
  refreshedAtArea.textContent = `Refreshed at ${date.toLocaleString("hu-HU", {
    year: "numeric",
    month: "2-digit",
    day: "2-digit",
    hour: "2-digit",
    minute: "2-digit",
  })}`;

  const allLogsNumberArea = document.getElementById("all-logs-number");
  allLogsNumberArea.textContent = logs.length;

  const allErrorLogsNumberArea = document.getElementById(
    "all-error-logs-number",
  );
  allErrorLogsNumberArea.textContent = logs.filter(
    (l) => l.logType == "Error",
  ).length;

  const logRecordsArea = document.getElementById("log-records");
  logRecordsArea.innerHTML = " ";

  logs.forEach((log) => {
    let logRecord = document.createElement("div");
    logRecord.className = "log-record";

    let logTime = log.dateTime.slice(0, 23).replace("T", " ");

    logRecord.innerHTML = `
            <div class="data">
                    <div style="border-left: none; width: 10%">${log.logType}</div>
                    <div style="width: 65%;"><p style="color: ${log.logType == "Error" ? "red" : "black"}">Description: ${log.description}</p></div>
                    <div style="width: 25%;">DateTime: ${logTime}</div>
            </div>
        `;

    logRecord.addEventListener("click", () => DisplayDetailedLogPanel(log));

    logRecordsArea.appendChild(logRecord);
  });
}

async function Init() {
  adminName = await getUserName();

  if (!adminName) {
    window.location.href = "../../htmls/pc.html";
    return;
  }

  document.body.innerHTML = LandingPanel();

  const adminNameArea = document.getElementById("admin-name");
  adminNameArea.textContent = adminName;

  const hamburgerBtn = document.getElementById("hamburger-icon");
  const closeNavigationElementBtn = document.getElementById(
    "close-navigation-element-btn",
  );
  const navigationElement = document.getElementById("navigation-element");

  let isNavigationElementOpened = false;

  function HandleNavigationElementMoves(isOpened) {
    let animation = isOpened
      ? "navigation-element-open"
      : "navigation-element-close";
    navigationElement.style.animation = `${animation} 1s forwards`;
  }

  hamburgerBtn.addEventListener("click", () => {
    isNavigationElementOpened = true;
    HandleNavigationElementMoves(isNavigationElementOpened);
  });

  closeNavigationElementBtn.addEventListener("click", () => {
    isNavigationElementOpened = false;
    HandleNavigationElementMoves(isNavigationElementOpened);
  });

  const userPanelBtn = document.getElementById("users-btn");
  userPanelBtn.addEventListener("click", DisplayUsers);

  const logPanelBtn = document.getElementById("logs-btn");
  logPanelBtn.addEventListener("click", DisplayLogs);

  let getUsersResponse = await GetUsers();

  users = getUsersResponse ?? [];

  const currentUserNumber = document.getElementById("current-user-number");
  currentUserNumber.textContent = users.length;

  let getLogsResponse = await GetLogs();

  logs = getLogsResponse ?? [];

  const currentLogsNumber = document.getElementById("current-logs-number");
  currentLogsNumber.textContent = logs.length;

  const goToUsersPanelBtn = document.getElementById("go-to-users-panel-btn");
  goToUsersPanelBtn.addEventListener("click", DisplayUsers);

  const goToLogsPanelBtn = document.getElementById("go-to-logs-panel-btn");
  goToLogsPanelBtn.addEventListener("click", DisplayLogs);
}

Init();
