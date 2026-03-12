using Microsoft.EntityFrameworkCore;
using RetroRealm_Server.DTOs.AdminPanelDTOs;
using RetroRealm_Server.DTOs.BunnyRunDTOs;
using RetroRealm_Server.DTOs.FlappyBirdDTOs;
using RetroRealm_Server.DTOs.MemoryGameDTOs;
using RetroRealm_Server.DTOs.WorldeStatusDTOs;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services.LogService;
using System.Formats.Asn1;
using System.Linq;

namespace RetroRealm_Server.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly ILogService _logService;
        private readonly RetroRealmDatabaseContext _context;
        private readonly LogDatabaseContext _logContext;
        public AdminService(ILogService logService, RetroRealmDatabaseContext context, LogDatabaseContext logContext)
        {
            _logService = logService;
            _context = context;
            _logContext = logContext;
        }

        public async Task<Result<List<ReadUserForAdminDTO>>> GetAllUsersForAdminPanel()
        {
            var users = await _context.Users
                .Include(u => u.MemoryGameStatus)
                .Include(u => u.FlappyBirdStatus)
                .Include(u => u.WorldeStatus)
                .Include(u => u.BunnyRunStatus)
                .Include(u => u.CurrentAvatar)
                .Include(u => u.Role)
                .ToListAsync();

            var avatars = await _context.Avatars.ToListAsync();
            var achievements = await _context.Achievements.ToListAsync();

            var result = users.Select(u => new ReadUserForAdminDTO
            {
                CurrentAvatarName = u.CurrentAvatar?.Name,
                Coins = u.Coins,
                RoleName = u.Role?.Name,
                Username = u.Username,

                OwnedAvatarsNames = avatars
                    .Where(a => u.OwnedAvatarsId.Contains(a.Id))
                    .Select(a => a.Name)
                    .ToList(),

                CompletedChallangesName = achievements
                    .Where(a => u.CompletedChallangesId.Contains(a.Id))
                    .Select(a => a.NameEng)
                    .ToList(),

                MemoryGameStatus = new ReadMemoryGameStatusDTO
                {
                    MinTime = u.MemoryGameStatus.MinTime,
                    MinFlipping = u.MemoryGameStatus.MinFlipping
                },

                FlappyBirdStatus = new ReadFlappyBirdStatusDTO
                {
                    MaxPassedPipes = u.FlappyBirdStatus.MaxPassedPipes
                },

                WorldeStatus = new ReadWordleStatusDTO
                {
                    CompletedWords = u.WorldeStatus.CompletedWords
                },

                BunnyRunStatus = new ReadBunnyRunStatusDTO
                {
                    MaxDistance = u.BunnyRunStatus.MaxDistance
                }

            }).ToList();

            await _logService.CreateLogAsync(
                LogType.Get.ToString(),
                null,
                "All users have been requested for the admin panel",
                DateTime.Now,
                null);

            return Result<List<ReadUserForAdminDTO>>.Ok(result);
        }

        public async Task<Result<List<ReadLogDTO>>> GetAllLogsForAdminPanel() {
            var logs = await _logContext.Logs.ToListAsync();

            var users = await _context.Users
                .ToDictionaryAsync(u => u.Id, u => u.Username);

            var result = logs.Select(l => new ReadLogDTO
            {
                DateTime = l.DateTime,
                Description = l.Description,
                Error = l.Error ?? " ",
                LogType = l.LogType,
                UserName = l.UserId != null && users.ContainsKey(l.UserId.Value)
                    ? users[l.UserId.Value]
                    : "Unknown User"
            }).ToList();

            await _logService.CreateLogAsync(LogType.Get.ToString(), null, "All logs have been requested for the admin panel", DateTime.Now, null);
            return Result<List<ReadLogDTO>>.Ok(result);
        }
    }
}
