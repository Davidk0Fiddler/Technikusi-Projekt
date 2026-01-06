using Microsoft.EntityFrameworkCore;
using RetroRealm_Server.DTOs;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services.Interfaces;

namespace RetroRealm_Server.Services
{
    public class FlappyBirdService : IFlappyBirdStatusService
    {
        private readonly RetroRealmDatabaseContext _context;
        private readonly ILogService _logService;
        private readonly IAuthService _authService;

        public FlappyBirdService(RetroRealmDatabaseContext context, ILogService logService, IAuthService authService)
        {
            _context = context;
            _logService = logService;
            _authService = authService;
        }

        #region Get FlappyBirdStatus by Id
        public async Task<Result<ReadFlappyBirdStatusDTO>> GetFlappyBirdStatusAsync(RefreshTokenDto model) {
            var result = await _authService.CheckExpireDateAsync(model);
            if (!result) return Result<ReadFlappyBirdStatusDTO>.Fail("RefreshToken expired or does not exists!");

            var userId = await _authService.GetUserIdFromRefreshTokenAsync(model);

            var status = await _context.Flappy_Bird_Status.FirstOrDefaultAsync(s => s.UserId == userId);

            if (status == null) 
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, $"FlappyBirdStatus has not found for GET! User-{userId}", DateTime.Now, userId);
                return Result<ReadFlappyBirdStatusDTO>.Fail("Status not found!");
            }
            await _logService.CreateLogAsync(LogType.Get.ToString(), null, $"FlappyBirdStatus has been requested by user-{userId}!", DateTime.Now, userId);
            return Result<ReadFlappyBirdStatusDTO>.Ok(ToReadFlappyBirdStatusDTO(status));
        }
        #endregion

        #region Create FlappyBirdStatus 
        public async Task<Result<FlappyBirdStatus>> CreateFlappyBirdStatusAsync(int userId) {
            var status = new FlappyBirdStatus
            {
                UserId = userId,
                MaxPassedPipes = 0,
            };

            _context.Flappy_Bird_Status.Add(status);
            try
            {
                await _context.SaveChangesAsync();
                await _logService.CreateLogAsync(LogType.Create.ToString(), null, $"New FlappyBirdStatus has been created for user-{userId}", DateTime.Now, userId);
                return Result<FlappyBirdStatus>.Ok(status);
            }
            catch (DbUpdateException ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Database error while creating a FlappyBirdStatus! User-{userId}", DateTime.Now, userId);
                return Result<FlappyBirdStatus>.Fail("Database error!");
            }
            catch (Exception ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Error while creating a FlappyBirdStatus! User-{userId}", DateTime.Now, userId);
                return Result<FlappyBirdStatus>.Fail("Error!");
            }
        }
        #endregion

        #region Update FlappyBirdStatus
        public async Task<Result<bool>> UpdateFlappyBirdStatusAsync(UpdateFlappyBirdStatusDTO updatedStatus, RefreshTokenDto model) {
            var result = await _authService.CheckExpireDateAsync(model);
            if (!result) return Result<bool>.Fail("RefreshToken expired or does not exists!");

            var userId = await _authService.GetUserIdFromRefreshTokenAsync(model);

            var currentStatus = await _context.Flappy_Bird_Status.FirstOrDefaultAsync(s => s.UserId == userId);

            if (currentStatus != null) {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, $"Status not found for UPDATE! User-{userId}", DateTime.Now, userId);
                return Result<bool>.Fail("Status not found");
            }

            currentStatus.MaxPassedPipes = updatedStatus.MaxPassedPipes;
            try
            {
                await _context.SaveChangesAsync();
                await _logService.CreateLogAsync(LogType.Update.ToString(), null, $"FlappyBirdStatus has been updated for User-{userId}!", DateTime.Now, userId);
                return Result<bool>.Ok(true);
            }
            catch (DbUpdateException ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Database error during updating FlappyBirdStatus for User-{userId}!", DateTime.Now, userId);
                return Result<bool>.Fail("Database error");
            }
            catch (Exception ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Error during updating FlappyBirdStatus for User-{userId}!", DateTime.Now, userId);
                return Result<bool>.Fail("Error");
            }
        }
        #endregion
        private static ReadFlappyBirdStatusDTO ToReadFlappyBirdStatusDTO(FlappyBirdStatus b)
        {
            return new ReadFlappyBirdStatusDTO
            {
                MaxPassedPipes = b.MaxPassedPipes,
            };
        }
    }
}
