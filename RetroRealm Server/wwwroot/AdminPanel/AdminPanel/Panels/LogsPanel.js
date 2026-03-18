export default function LogsPanel() {
  return `
    <div class="navbar">
        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" id="hamburger-icon" class="bi bi-list" viewBox="0 0 16 16">
            <path fill-rule="evenodd" d="M2.5 12a.5.5 0 0 1 .5-.5h10a.5.5 0 0 1 0 1H3a.5.5 0 0 1-.5-.5m0-4a.5.5 0 0 1 .5-.5h10a.5.5 0 0 1 0 1H3a.5.5 0 0 1-.5-.5m0-4a.5.5 0 0 1 .5-.5h10a.5.5 0 0 1 0 1H3a.5.5 0 0 1-.5-.5"/>
        </svg>

        <span id="admin-name"> Placeholder Admin name</span>
    </div>

    <div class="navigation-element" id="navigation-element">
        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" id="close-navigation-element-btn" class="bi bi-x-lg" viewBox="0 0 16 16">
            <path d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8z"/>
        </svg>

        <h4 id="navigation-element-h4">admin panel</h4>
        <div class="navigation-button" id="users-btn">Users Panel</div>
        <div class="navigation-button" id="logs-btn">Logs Panel</div>
    </div>

    <div class="container-log">
        <h2>Logs Panel</h2>
        <div class="logs-base-infos">
            <div style="border-left: none;">Current Logs: <span id="all-logs-number">0</span></div>
            <div>Error Logs: <span id="all-error-logs-number">0</span></div>
        </div>

        <div class="log-records" id="log-records">
            <div class="log-record">
                <div class="data">
                    <div style="border-left: none; width: 10%">GET</div>
                    <div style="width: 65%;"><p>Description: All users have been requested for the admin panel</p></div>
                    <div style="width: 25%;">DateTime: 2026.03.16 08:02</div>
                </div>
            </div>
        </div>
        <div class="refreshed-at" id="refreshed-at">Refreshed at 2026.03.06 12:25</div>
    </div>

    <div class="black-overlay" id="black-overlay">
        <div class="detailed-log-panel">
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" id="close-detailed-log-panel-btn" class="bi bi-x-lg" viewBox="0 0 16 16">
                <path d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8z"/>
            </svg>

            <div class="log-grid">
                <div class="log-item log-type">
                    <h3>LogType</h3>
                    <p id="log-type"> </p>
                </div>

                <div class="log-item log-datetime">
                    <h3>DateTime</h3>
                    <p id="log-time"> </p>
                </div>

                <div class="log-item log-user">
                    <h3>User Name</h3>
                    <p id="user-name"> </p>
                </div>

                <div class="log-item log-description">
                    <h3>Description</h3>
                    <p id="log-description"> </p>
                </div>

                <div class="log-item log-error">
                    <h3>Error</h3>
                    <p id="log-error"> </p>
                </div>
            </div>
        </div>
    </div>
       `;
}
