using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RetroRealm_Server.DTOs;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services.Interfaces;

namespace RetroRealm_Server.Services
{
    public class BunnyRunService : IBunnyRunStatusService
    {
        private readonly RetroRealmDatabaseContext _context;
        private readonly ILogService _logService;
        //private readonly IRefreshTokenService _refreshTokenService;

        public BunnyRunService(RetroRealmDatabaseContext context, ILogService logService
            //,IRefreshTokenService refreshTokenService
            ) {
            _context = context;
            _logService = logService;
            //_refreshTokenService = refreshTokenService;
        }

        #region Get BunnyRunStatus by Id
        public async Task<Result<ReadBunnyRunStatusDTO>>GetBunnyRunStatusAsync(string username) {
            //var result = await _refreshTokenService.CheckExpireDateAsync(model);
            //if (!result) return Result<ReadBunnyRunStatusDTO>.Fail("RefreshToken expired or does not exists!");

            //var UserId = await _refreshTokenService.GetUserIdFromRefreshTokenAsync(model);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            var UserId = user.Id;


            var status = await _context.Bunny_Run_Status.FirstOrDefaultAsync(r => r.UserId == UserId);

            if (status == null)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, $"BunnyRunStatus has not found for GET! User-{UserId}", DateTime.Now, UserId);
                return Result<ReadBunnyRunStatusDTO>.Fail("Status not found!");
            }
            await _logService.CreateLogAsync(LogType.Get.ToString(), null, $"BunnyRunStatus has been requested by user-{UserId}!", DateTime.Now, UserId);
            return Result<ReadBunnyRunStatusDTO>.Ok(ToReadBunnyRunStatusDTO(status));
        }
        #endregion

        #region Create BunnyRunStatus
        public async Task<Result<BunnyRunStatus>> CreateBunnyRunStatusAsync(int userId)
        {
            var status = new BunnyRunStatus
            {
                UserId = userId,
                MaxDistance = 0
            };

            _context.Bunny_Run_Status.Add(status);
            try
            {
                await _context.SaveChangesAsync();
                await _logService.CreateLogAsync(LogType.Create.ToString(),null, $"New BunnyRunStatus has been created for user-{userId}!",DateTime.Now, userId);
                return Result<BunnyRunStatus>.Ok(status);
            }
            catch (DbUpdateException ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Database error while creating a BunnyRunStatus! User-{userId}", DateTime.Now, userId);
                return Result<BunnyRunStatus>.Fail("Database error!");
            }
            catch (Exception ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Error while creating a BunnyRunStatus! User-{userId}", DateTime.Now, userId);
                return Result<BunnyRunStatus>.Fail("Error");
            }
        }
        #endregion

        #region Update BunnyRunStatus
        public async Task<Result<UpdateBunnyRunStatusDTO>> UpdateBunnyRunStatusAsync(UpdateBunnyRunStatusDTO updatedStatus, string username) {
            //var result = await _refreshTokenService.CheckExpireDateAsync(model);
            //if (!result) return Result<UpdateBunnyRunStatusDTO>.Fail("RefreshToken expired or does not exists!");

            //var UserId = await _refreshTokenService.GetUserIdFromRefreshTokenAsync(model);


            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            var UserId = user.Id;

            var currentStatus = await _context.Bunny_Run_Status.FirstOrDefaultAsync(s => s.UserId == UserId);

            if (currentStatus == null) {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, $"Status not found for UPDATE! User-{UserId} ", DateTime.Now, UserId);
                return Result<UpdateBunnyRunStatusDTO>.Fail("Status not found");
            }

            currentStatus.MaxDistance = updatedStatus.MaxDistance;
            try
            {
                await _context.SaveChangesAsync();
                await _logService.CreateLogAsync(LogType.Update.ToString(), null, $"BunnyRunStatus has been updated for User-{UserId}!", DateTime.Now, UserId);
                return Result<UpdateBunnyRunStatusDTO>.Ok(updatedStatus);
            }
            catch (DbUpdateException ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Database error during updating BunnyRunStatus for User-{UserId}!", DateTime.Now, UserId);
                return Result<UpdateBunnyRunStatusDTO>.Fail("Database error");
            }
            catch (Exception ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Error during updating BunnyRunStatus for User-{UserId}!", DateTime.Now, UserId);
                return Result<UpdateBunnyRunStatusDTO>.Fail("Error");
            }
        }
        #endregion

        private static ReadBunnyRunStatusDTO ToReadBunnyRunStatusDTO(BunnyRunStatus b) {
            return new ReadBunnyRunStatusDTO
            {
                MaxDistance = b.MaxDistance
            };
        }
    }
}
