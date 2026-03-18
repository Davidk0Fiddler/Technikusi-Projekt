export default function LandingPanel() {
    return( `<div class="navbar">
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


    <div class="container" id="container">
        <div class="left">welcome to the admin page</div>

        <div class="right-top">
            <div class="user-container">
                <h5>Users</h5>
                <p> There are <span id="current-user-number">0</span> users currently in the database!</p>
                <div class="go-to-users-panel-button" id="go-to-users-panel-btn">
                    Go to Users Panel   
                </div>
            </div>
        </div>

        <div class="right-bottom">
            <div class="logs-container">
                <h5>Logs</h5>
                <p>There are <span id="current-logs-number">0</span> logs currently in the database!</p>
                <div class="go-to-logs-panel-button" id="go-to-logs-panel-btn">
                    Go to Logs Panel
                </div>
            </div>
        </div>
    </div>`); 
}