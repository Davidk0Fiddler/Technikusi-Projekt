import baseURL from "../scripts/baseURL.js";

async function GetBunnyRunLeaderboard() {
  const response = await fetch(`${baseURL}/bunnyrun`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  });

  if (response.ok) {
    return response.json();
  }

  return [];
}

async function GetFlappyBirdLeaderboard() {
  const response = await fetch(`${baseURL}/flappybird`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  });

  if (response.ok) {
    return response.json();
  }

  return [];
}

async function GetMemoryGameFlipsLeaderboard() {
  const response = await fetch(`${baseURL}/memorygame/flips`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  });

  if (response.ok) {
    return response.json();
  }

  return [];
}

async function GetMemoryGameTimeLeaderboard() {
  const response = await fetch(`${baseURL}/memorygame/time`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  });

  if (response.ok) {
    return response.json();
  }

  return [];
}

async function GetWordleLeaderboard() {
  const response = await fetch(`${baseURL}/wordle`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  });

  if (response.ok) {
    return response.json();
  }

  return [];
}

async function GetAchievementLeaderboard() {
  const response = await fetch(`${baseURL}/achievement`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  });

  if (response.ok) {
    return response.json();
  }

  return [];
}

export default async function GetLeaderboards() {
  const leaderboards = {
    BunnyRunLeaderboard: await GetBunnyRunLeaderboard(),
    FlappyBirdLeaderboard: await GetFlappyBirdLeaderboard(),
    MemoryGameFlipsLeaderboard: await GetMemoryGameFlipsLeaderboard(),
    MemoryGameTimeLeaderboard: await GetMemoryGameTimeLeaderboard(),
    WordleLeaderboard: await GetWordleLeaderboard(),
    AchievementLeaderboard: await GetAchievementLeaderboard(),
  };

  return leaderboards;
}
