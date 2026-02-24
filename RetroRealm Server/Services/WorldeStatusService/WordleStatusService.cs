using Microsoft.EntityFrameworkCore;
using RetroRealm_Server.DTOs.WorldeStatusDTOs;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services.CheckAchievementsService;
using RetroRealm_Server.Services.LogService;

namespace RetroRealm_Server.Services.WorldeStatusService
{
    public class WordleStatusService : IWordleStatusService
    {
        private readonly RetroRealmDatabaseContext _context;
        private readonly ILogService _logService;
        private readonly ICheckAchievementService _checkAchievementService;

        public WordleStatusService(RetroRealmDatabaseContext context, ILogService logService, ICheckAchievementService checkAchievementService)
        {
            _context = context;
            _logService = logService;
            _checkAchievementService = checkAchievementService;
        }

        #region Get WordleStatus by Id 
        public async Task<Result<ReadWordleStatusDTO>> GetWordleStatusAsync(string username)
        {
            //var result = await _refreshTokenService.CheckExpireDateAsync(model);
            //if (!result) return Result<ReadWordleStatusDTO>.Fail("RefreshToken expired or does not exists!");

            //var userId = await _refreshTokenService.GetUserIdFromRefreshTokenAsync(model);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            var userId = user.Id;

            var status = await _context.Wordle_Status.FirstOrDefaultAsync(r => r.UserId == userId);

            if (status == null)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, $"WordleStatus has not found for GET! User-{userId}", DateTime.Now, userId);
                return Result<ReadWordleStatusDTO>.Fail("Status not found!");
            }
            await _logService.CreateLogAsync(LogType.Get.ToString(), null, $"WordleStatus has been requested by user-{userId}!", DateTime.Now, userId);
            return Result<ReadWordleStatusDTO>.Ok(ToReadWordleStatusDTO(status));
        }
        #endregion

        #region Create WordleStatus
        public async Task<Result<WordleStatus>> CreateWordleStatusAsync(int userId)
        {
            var status = new WordleStatus
            {
                UserId = userId,
                CompletedWords = 0
            };

            _context.Wordle_Status.Add(status);
            try
            {
                await _context.SaveChangesAsync();
                await _logService.CreateLogAsync(LogType.Create.ToString(), null, $"New WordleStatus has been created for user-{userId}", DateTime.Now, userId);
                return Result<WordleStatus>.Ok(status);
            }
            catch (DbUpdateException ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Database error while creating a WordleStatus! User-{userId}", DateTime.Now, userId);
                return Result<WordleStatus>.Fail("Database error!");
            }
            catch (Exception ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message,$"Error while creating a WordleStatus! User-{userId}", DateTime.Now, userId);
                return Result<WordleStatus>.Fail("Error!");
            }
        }
        #endregion

        #region Update WordleStatus
        public async Task<Result<bool>> UpdateWordleStatusAsync(UpdateWordleStatusDTO updatedStatus, string username)
        { 
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            var userId = user.Id;

            var currentStatus = await _context.Wordle_Status.FirstOrDefaultAsync(s => s.UserId == userId);

            if (currentStatus == null)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, "Status not found for UPDATE!", DateTime.Now, userId);
                return Result<bool>.Fail("Status not found");
            }

            currentStatus.CompletedWords = currentStatus.CompletedWords + 1;
            await _checkAchievementService.CheckWordleAchievementsAsync(userId, currentStatus.CompletedWords);

            try
            {
                await _context.SaveChangesAsync();
                await _logService.CreateLogAsync(LogType.Update.ToString(), null, $"WordleStatus has been updated for User-{userId}!", DateTime.Now, userId);
                return Result<bool>.Ok(true);
            }
            catch (DbUpdateException ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Database error during updating WordleStatus for User-{userId}!", DateTime.Now, userId);
                return Result<bool>.Fail("Database error!");
            }
            catch (Exception ex)
            {   
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Error during updating WordleStatus for User-{userId}!", DateTime.Now, userId);
                return Result<bool>.Fail("Error");
            }
        }
        #endregion
        private static ReadWordleStatusDTO ToReadWordleStatusDTO(WordleStatus w)
        {
            return new ReadWordleStatusDTO
            {
                CompletedWords = w.CompletedWords,
            };
        }
    }
}
