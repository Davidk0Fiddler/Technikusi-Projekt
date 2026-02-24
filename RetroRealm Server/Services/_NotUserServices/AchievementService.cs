using Microsoft.EntityFrameworkCore;
using RetroRealm_Server.DTOs;
using RetroRealm_Server.DTOs._NotUserDTOS;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services._NotUserServices.Interfaces;
using RetroRealm_Server.Services.LogService;

namespace RetroRealm_Server.Services._NotUserServices
{
    public class AchievementService: IAchievementsService
    {
        private readonly RetroRealmDatabaseContext _context;
        private readonly ILogService _logService;

        public AchievementService(RetroRealmDatabaseContext context, ILogService logService)
        {
            _context = context;
            _logService = logService;
        }

        #region Get All Achievements
        public async Task<Result<List<ReadAchievementDTO>>> GetAllAchievementsAsync()
        {
            var allAchievements = await _context.Achievements.Select(r => ToReadAchievementsDTO(r)).ToListAsync();

            await _logService.CreateLogAsync(LogType.Get.ToString(), null, "All achievements have been requested!", DateTime.Now, null);
            return Result<List<ReadAchievementDTO>>.Ok(allAchievements);
        }
        #endregion

        #region Get One Achievement
        public async Task<Result<ReadAchievementDTO>> GetAchievementAsync(int id)
        {
            var achievement = await _context.Achievements.FirstOrDefaultAsync(r => r.Id == id);

            if (achievement == null)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, $"Achievement ({achievement}) has not found for GET!", DateTime.Now, null);
                return Result<ReadAchievementDTO>.Fail("Achievement not found!");
            }

            await _logService.CreateLogAsync(LogType.Get.ToString(), null, $"Achievement ({achievement.NameEng} has been requested!", DateTime.Now, null);
            return Result<ReadAchievementDTO>.Ok(ToReadAchievementsDTO(achievement));
        }
        #endregion

        #region Create Achievements
        public async Task<Result<Achievement>> CreateAchievementAsync(CreateAchievementDTO newAchievement)
        {
            var achievement = new Achievement
            {
                NameEng = newAchievement.Name,
                DescriptionEng = newAchievement.Description,
                Requirement = newAchievement.Requirement,
            };

            _context.Achievements.Add(achievement);
            try
            {
                await _context.SaveChangesAsync();
                await _logService.CreateLogAsync(LogType.Create.ToString(), null, $"Achievement has been created! ({achievement})", DateTime.Now, null);
                return Result<Achievement>.Ok(achievement);
            }
            catch (DbUpdateException ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Database error while creating an achievement! {achievement.NameEng}", DateTime.Now, null);
                return Result<Achievement>.Fail("Database Error");
            }
            catch (Exception ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Error while creating an achievement! {achievement.NameEng}", DateTime.Now, null);
                return Result<Achievement>.Fail("Error");
            }
        }
        #endregion

        #region Update Achievements
        public async Task<Result<UpdateAchievementDTO>> UpdateAchievementAsync(int id, UpdateAchievementDTO updatedAchievement)
        {
            var currentAchievement = await _context.Achievements.FindAsync(id);

            if (currentAchievement == null)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, $"Achievement ({id}) not found for UPDATE!", DateTime.Now, null);
                return Result<UpdateAchievementDTO>.Fail("Achievement not found!");
            }


            currentAchievement.NameEng = currentAchievement.NameEng;
            currentAchievement.DescriptionEng = currentAchievement.DescriptionEng;
            currentAchievement.Requirement = currentAchievement.Requirement;
            try
            {
                await _context.SaveChangesAsync();
                await _logService.CreateLogAsync(LogType.Update.ToString(), null, $"Achievement (ID: {currentAchievement.Id}) has been updated!", DateTime.Now, null);
                return Result<UpdateAchievementDTO>.Ok(updatedAchievement);
            }
            catch (DbUpdateException ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Database error during updating achievement! {currentAchievement.NameEng}", DateTime.Now, null);
                return Result<UpdateAchievementDTO>.Fail("Database error");
            }
            catch (Exception ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Error during updating achievement! {currentAchievement.NameEng}", DateTime.Now, null);
                return Result<UpdateAchievementDTO>.Fail("Error");
            }
        }
        #endregion

        #region Delete Achievements
        public async Task<Result<int>> DeleteAchievementByIdAsync(int id)
        {
            var DeletingItem = await _context.Achievements.FindAsync(id);

            if (DeletingItem == null)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, $"Achievement not found for DELETE! {id}", DateTime.Now, null);
                return Result<int>.Fail("Achievement not found");
            }

            _context.Achievements.Remove(DeletingItem);

            try
            {
                await _context.SaveChangesAsync();
                await _logService.CreateLogAsync(LogType.Delete.ToString(), null, $"Achievements ({DeletingItem.NameEng}) has been deleted!", DateTime.Now, null);
                return Result<int>.Ok(id);
            }
            catch (DbUpdateException ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Database error during deleting achievements! {DeletingItem.NameEng}", DateTime.Now, null);
                return Result<int>.Fail("Database error");
            }
            catch (Exception ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Error during deleting achievements! {DeletingItem.NameEng}", DateTime.Now, null);
                return Result<int>.Fail("Not Registered Error");
            }
        }
        #endregion

        private static ReadAchievementDTO ToReadAchievementsDTO(Achievement a)
        {
            return new ReadAchievementDTO
            {
                Name = a.NameEng,
                Description = a.DescriptionEng,
                Requirement = a.Requirement,
            };
        }
    }
}