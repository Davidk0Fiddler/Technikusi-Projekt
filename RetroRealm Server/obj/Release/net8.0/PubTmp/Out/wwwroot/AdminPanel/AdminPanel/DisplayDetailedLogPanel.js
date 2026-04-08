export default function DisplayDetailedLogPanel(log) {
  const blackOverlay = document.getElementById("black-overlay");
  blackOverlay.style.display = "flex";

  const closeDetailedLogPanelBtn = document.getElementById(
    "close-detailed-log-panel-btn",
  );
  closeDetailedLogPanelBtn.addEventListener(
    "click",
    () => (blackOverlay.style.display = "none"),
  );

  const logTypeArea = document.getElementById("log-type");
  logTypeArea.textContent = log.logType;

  const logTimeArea = document.getElementById("log-time");
  const logTime = log.dateTime.toLocaleString("hu-HU", {
    year: "numeric",
    month: "2-digit",
    day: "2-digit",
    hour: "2-digit",
    minute: "2-digit",
  });

  logTimeArea.textContent = logTime;

  const logUserNameArea = document.getElementById("user-name");
  logUserNameArea.textContent = log.userName;

  const logDescriptionArea = document.getElementById("log-description");
  logDescriptionArea.textContent = log.description;

  const logErrorArea = document.getElementById("log-error");
  logErrorArea.textContent = log.error;
}
