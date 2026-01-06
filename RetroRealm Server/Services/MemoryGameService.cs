using Microsoft.EntityFrameworkCore;
using RetroRealm_Server.DTOs;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services.Interfaces;

namespace RetroRealm_Server.Services
{
    public class MemoryGameService : IMemoryGameStatusService
    {
        private readonly RetroRealmDatabaseContext _context;
        private readonly ILogService _logService;
        private readonly IAuthService _authService; 

        public MemoryGameService(RetroRealmDatabaseContext context, ILogService logService, IAuthService authService)
        {
            _context = context;
            _logService = logService;
            _authService = authService;
        }

        #region Get MemoryGameStatus by Id
        public async Task<Result<ReadMemoryGameStatusDTO>> GetMemoryGameStatusAsync(RefreshTokenDto model) {
            var result = await _authService.CheckExpireDateAsync(model);
            if (!result) return Result<ReadMemoryGameStatusDTO>.Fail("RefreshToken expired or does not exists!");
            
            var userId = await _authService.GetUserIdFromRefreshTokenAsync(model);

            var status = await _context.Memory_Game_Status.FirstOrDefaultAsync(m => m.UserId == userId);

            if (status == null)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, $"MemoryGameStatus has not found for GET! User-{userId}", DateTime.Now, userId);
                return Result<ReadMemoryGameStatusDTO>.Fail("Status not found!");
            }
            await _logService.CreateLogAsync(LogType.Get.ToString(), null, $"MemoryGameStatus has been requested by user-{userId}!", DateTime.Now, userId);
            return Result<ReadMemoryGameStatusDTO>.Ok(ToReadMemoryGameStatusDTO(status));
        }
        #endregion

        #region Create MemoryGameStatus
        public async Task<Result<MemoryGameStatus>> CreateMemoryGameStatusAsync(int userId)
        { 
            var status = new MemoryGameStatus 
            { 
                UserId = userId,
                MinFlipping = 0,
                MinTime = [0,0,0],
            };

            _context.Memory_Game_Status.Add(status);
            try
            {
                await _context.SaveChangesAsync();
                await _logService.CreateLogAsync(LogType.Create.ToString(), null, $"New MemoryGameStatus has been created for user-{userId}", DateTime.Now, userId);
                return Result<MemoryGameStatus>.Ok(status);
            }
            catch (DbUpdateException ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Database error while creating a MemoryGameStatus! User-{userId}", DateTime.Now, userId);
                return Result<MemoryGameStatus>.Fail("Database error!");
            }
            catch (Exception ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Error while creating a MemoryGameStatus! User-{userId}", DateTime.Now, userId);
                return Result<MemoryGameStatus>.Fail("Error!");
            }

        }
        #endregion

        #region Update MemoryGameStatus 
        public async Task<Result<bool>> UpdateMemoryGameStatusAsync(UpdateMemoryGameStatusDTO updatedStatus, RefreshTokenDto model) {
            var result = await _authService.CheckExpireDateAsync(model);
            if (!result) return Result<bool>.Fail("RefreshToken expired or does not exists!");

            var userId = await _authService.GetUserIdFromRefreshTokenAsync(model);

            var currentStatus = await _context.Memory_Game_Status.FirstOrDefaultAsync(s => s.UserId == userId);

            if (currentStatus == null)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, $"Status not found for UPDATE! User-{userId}", DateTime.Now, userId);
                return Result<bool>.Fail("Status not found");
            }

            currentStatus.MinFlipping = updatedStatus.MinFlipping;
            currentStatus.MinTime = updatedStatus.MinTime;
            try
            {
                await _context.SaveChangesAsync();
                await _logService.CreateLogAsync(LogType.Update.ToString(), null, $"MemoryGameStatus has been updated for User-{userId}!", DateTime.Now, userId);
                return Result<bool>.Ok(true);
            }
            catch (DbUpdateException ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Database error during updating MemoryGameStatus for User-{userId}!", DateTime.Now, userId);
                return Result<bool>.Fail("Database error");
            }
            catch (Exception ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Error during updating MemoryGameStatus for User-{userId}!", DateTime.Now, userId);
                return Result<bool>.Fail("Error");
            }
        }
        #endregion
        private static ReadMemoryGameStatusDTO ToReadMemoryGameStatusDTO(MemoryGameStatus m) {
            return new ReadMemoryGameStatusDTO
            {
                MinFlipping = m.MinFlipping,
                MinTime = m.MinTime,
            };
        }
    }
}
