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
        //private readonly IRefreshTokenService _refreshTokenService; 

        public MemoryGameService(RetroRealmDatabaseContext context, ILogService logService
            //, IRefreshTokenService refreshTokenService
            )
        {
            _context = context;
            _logService = logService;
            //_refreshTokenService = refreshTokenService;
        }

        #region Get MemoryGameStatus by Id
        public async Task<Result<ReadMemoryGameStatusDTO>> GetMemoryGameStatusAsync(string username) {
            //var result = await _refreshTokenService.CheckExpireDateAsync(model);
            //if (!result) return Result<ReadMemoryGameStatusDTO>.Fail("RefreshToken expired or does not exists!");

            //var userId = await _refreshTokenService.GetUserIdFromRefreshTokenAsync(model);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            var userId = user.Id;

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
        public async Task<Result<bool>> UpdateMemoryGameStatusAsync(UpdateMemoryGameStatusDTO updatedStatus, string username) {
            //var result = await _refreshTokenService.CheckExpireDateAsync(model);
            //if (!result) return Result<bool>.Fail("RefreshToken expired or does not exists!");

            //var userId = await _refreshTokenService.GetUserIdFromRefreshTokenAsync(model);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            var userId = user.Id;

            var currentStatus = await _context.Memory_Game_Status.FirstOrDefaultAsync(s => s.UserId == userId);

            if (currentStatus == null)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, $"Status not found for UPDATE! User-{userId}", DateTime.Now, userId);
                return Result<bool>.Fail("Status not found");
            }

            if (currentStatus.MinFlipping > updatedStatus.MinFlipping) currentStatus.MinFlipping = updatedStatus.MinFlipping;
            
            if ((currentStatus.MinTime[0]*60*60 + currentStatus.MinTime[0] * 60 + currentStatus.MinTime[0]) > (updatedStatus.MinTime[0] * 60 * 60 + updatedStatus.MinTime[0] * 60 + updatedStatus.MinTime[0])) currentStatus.MinTime = updatedStatus.MinTime;
            
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
