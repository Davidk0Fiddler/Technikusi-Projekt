export default function UsersPanel() {
    return (
        `
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

    <div class="container-user">
        <h2>Users Panel</h2>
        <div class="users-base-infos">
            <div style="border-left: none;">Current Users: <span id="all-users-number">0</span></div>
            <div>Actual Users: <span id="all-actual-users-number">0</span></div>
            <div>Actual Admins: <span id="all-admins-number">0</span></div>            
        </div>

        <div class="user-records" id="user-records">
            <div class="user-record">
                <div class="data">
                    <div style="border-left: none;">Username: Test</div>
                    <div>Role: User</div>
                    <div>Current Avatar: Base Character</div>
                    <div>Completed Achievements: 3</div>
                </div>
            </div>
        </div>
        <div class="refreshed-at" id="refreshed-at">Refreshed at 2026.03.06 12:25</div>
    </div>

    <div class="black-overlay" id="black-overlay">
        <div class="detailed-user-panel">
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" id="close-detailed-user-panel-btn" class="bi bi-x-lg" viewBox="0 0 16 16">
                <path d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8z"/>
            </svg>

            <div class="user-panel">
                <div class="profile">

                    <img src="baseAvatar.png" class="main-avatar" id="character-avatar">

                    <div class="user-name" id="user-name-role">
                    <h2>PlayerName</h2> (RoleName)
                    </div>

                    <div class="coins">
                    Coins: <span id="coin-count">0</span>
                    </div>

                </div>

                <div class="game-status">

                    <div class="game">
                    <h4>Wordle</h4>
                    <span class="status" id="wordle-status">Total words : 0</span>
                    </div>

                    <div class="game">
                    <h4>Memory Game</h4>
                    <span class="status" id="memory-game-time-status">Min-Time: 00:00:00</span>
                    <span class="status" id="memory-game-flips-status">Min-Flips: 0</span>
                    </div>

                    <div class="game">
                    <h4>Bunny Run</h4>
                    <span class="status" id="bunny-run-status">Max-Distance: 0</span>
                    </div>

                    <div class="game">
                    <h4>Flappy Bird</h4>
                    <span class="status" id="flappy-bird-status">Max-Passed-Pipes: 0</span>
                    </div>

                </div>

                <div class="avatar-gallery" id="avatar-gallery">
                    <img src="baseAvatar.png">
                    <img src="healerAvatar.png">
                    <img src="ninjaAvatar.png">
                    <img src="rangerAvatar.png">
                    <img src="oldAvatar.png">
                    <img src="warriorAvatar.png">
                </div>

                <div class="achievements">
                    <h3>Completed Achievements</h3>

                    <div class="achievement-grid" id="achievement-grid">
                    <div class="achievement">First Win</div>
                    <div class="achievement">100 Matches</div>
                    <div class="achievement">Top Rank</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
        `
    );
}