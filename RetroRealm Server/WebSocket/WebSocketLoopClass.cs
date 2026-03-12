    using Microsoft.EntityFrameworkCore;
    using RetroRealm_Server.DTOs.WebSocketDTOs;
    using RetroRealm_Server.Models;
    using RetroRealm_Server.WebSocket;
    using System.Net.WebSockets;
    using System.Text;
    using System.Text.Json;
    public class WebSocketLoopClass
    {
        private static List<WebSocketOutputDTO> inGameData = new(); 
        public static int NextId = 1;
        private readonly RetroRealmDatabaseContext _context;
        private readonly string _connectionId = Guid.NewGuid().ToString("N");
        private PlayerState? _playerState;
        public string? _currentUsername;
        public string? _anonymousUserName;

        public WebSocketLoopClass(RetroRealmDatabaseContext context)
        {
            _context = context;
        }
        public async Task WebSocketLoop(WebSocket socket)
        {
            var buffer = new byte[4096];

            while (socket.State == WebSocketState.Open)
            {
                try
                {
                    var result = await socket.ReceiveAsync(
                        new ArraySegment<byte>(buffer),
                        CancellationToken.None
                    );

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        RemovePlayer();

                        await socket.CloseAsync(
                            WebSocketCloseStatus.NormalClosure,
                            "Client closed",
                            CancellationToken.None
                        );
                        break;
                    }

                    var json = Encoding.UTF8.GetString(buffer, 0, result.Count);

                    var inputMessage = JsonSerializer.Deserialize<WsMessage<WebSocketInputDTO>>(
                        json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (inputMessage?.Data == null)
                    {
                        continue;
                    }

                    List<WebSocketOutputDTO> output;

                    switch (inputMessage.Type)
                    {
                        case "UPDATE":
                            output = await SendOutput(inputMessage.Data);
                            break;

                        default:
                            output = new List<WebSocketOutputDTO>();
                            break;
                    }

                    var response = new WebSocketOutputWrapperDTO
                    {
                        YourUserName = _currentUsername,
                        Players = output
                    };

                    var responseJson = JsonSerializer.Serialize(response);

                    inGameData.RemoveAll(data =>
                        DateTime.UtcNow - data.LastSend > TimeSpan.FromSeconds(1)
                    );

                    foreach (var player in inGameData)
                    {
                        if (player.LastTextDate.HasValue &&
                            DateTime.UtcNow - player.LastTextDate.Value > TimeSpan.FromSeconds(10))
                        {
                            player.Text = null;
                            player.LastTextDate = null;
                        }
                    }

                    await socket.SendAsync(
                        Encoding.UTF8.GetBytes(responseJson),
                        WebSocketMessageType.Text,
                        true,
                        CancellationToken.None
                    );
                }
                catch (WebSocketException ex)
                {
                    // Kliens hirtelen megszakította a kapcsolatot
                    Console.WriteLine($"WebSocket hiba: {ex.Message}");
                    RemovePlayer();
                    break; // Kilépés a ciklusból
                }
                catch (Exception ex)
                {
                    // Egyéb váratlan hiba
                    Console.WriteLine($"Váratlan hiba: {ex}");
                    RemovePlayer();
                    break;
                }
            }
        }
    
    private async Task<List<WebSocketOutputDTO>> SendOutput(WebSocketInputDTO input)
    {
        var output = await CreateOutputFromServerLogic(input);

        _currentUsername = output.UserName;

        var existing = inGameData.FirstOrDefault(x => x.UserName == output.UserName);

        if (existing != null)
        {
            existing.Role = _playerState!.Role;
            existing.CharacterName = _playerState.CharacterName;
            existing.moveX = (int)_playerState.X;
            existing.moveY = (int)_playerState.Y;
            existing.posX = _playerState.posX;
            existing.posY = _playerState.posY;
            existing.Text = _playerState.Text;
            existing.LastTextDate = _playerState.LastTextDate;
            existing.LastSend = _playerState.LastSend;
        }
        else
        {
            inGameData.Add(output);
        }

        return inGameData;
    }
    private async Task<bool> TryInitializePlayerAsync(string username)
    {
        var user = await _context.Users
            .AsNoTracking()
            .Include(u => u.Role)
            .Include(u => u.CurrentAvatar)
            .FirstOrDefaultAsync(u => u.Username == username);

        if (user == null)
            return false;

        _playerState.Id = user.Id;
        _playerState.UserName = user.Username;
        _playerState.Role = user.Role!.Name;
        _playerState.CharacterName = user.CurrentAvatar.Name;
        _playerState.LastTextDate = DateTime.UtcNow.AddMilliseconds(-5000);

        inGameData.RemoveAll(p =>
            p.UserName.StartsWith("anon_") &&
            p.UserName.Contains(_connectionId[..8])
        );

        return true;
    }
    // anon ghost eltakarítása
    private async Task<WebSocketOutputDTO> CreateOutputFromServerLogic(WebSocketInputDTO input)
    {
        if (_playerState == null)
        {
            InitializeAnonymousPlayer();
        }

        if (!string.IsNullOrWhiteSpace(input.UserName) &&
            _playerState.Role == "anonymous")
        {
            await TryInitializePlayerAsync(input.UserName);
        }

        _playerState.X = (int)input.moveX;
        _playerState.Y = (int)input.moveY;
        _playerState.posX = input.posX;
        _playerState.posY = input.posY;
        _playerState.LastSend = input.LastSend;

        if (!string.IsNullOrWhiteSpace(input.Text))
        {
            // Csak új szövegnél frissítsd az időt
            if (_playerState.Text != input.Text)
            {
                _playerState.Text = input.Text;
                _playerState.LastTextDate = DateTime.UtcNow;
            }
        }
        else
        {
            // Ha üres, töröld a szöveget
            _playerState.Text = null;
            _playerState.LastTextDate = null;
        }

        return new WebSocketOutputDTO
        {
            UserName = _playerState.UserName,
            Role = _playerState.Role,
            CharacterName = _playerState.CharacterName,
            moveX = (int)_playerState.X,
            moveY = (int)_playerState.Y,
            posX = _playerState.posX,
            posY = _playerState.posY,
            Text = _playerState.Text,
            LastTextDate = _playerState.LastTextDate,
            LastSend = _playerState.LastSend
        };
    }

    private void InitializeAnonymousPlayer()
    {
        _playerState = new PlayerState
        {
            UserName = $"anon_{_connectionId[..8]}",
            Role = "anonymous",
            CharacterName = "baseCharacter",
            LastSend = DateTime.UtcNow
        };
    }

    private void RemovePlayer()
        {
            if (_currentUsername == null)
                return;

            inGameData.RemoveAll(p => p.UserName == _currentUsername);

            Console.WriteLine($"Player removed: {_currentUsername}");
        }
}