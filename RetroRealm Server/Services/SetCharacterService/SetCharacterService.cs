using Microsoft.EntityFrameworkCore;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services.LogService;

namespace RetroRealm_Server.Services.SetCharacterService
{
    public class SetCharacterService : ISetCharacterService
    {
        private readonly RetroRealmDatabaseContext _context;
        private readonly ILogService _logService;
        public SetCharacterService(RetroRealmDatabaseContext context, ILogService logService)
        {
            _context = context;
            _logService = logService;
        }

        public async Task<Result<bool>> SetCharacter(string characterName, string userName) { 
            
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == userName);
            if (user == null)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), $"Username ({userName}) not found for setting character.", $"Username ({userName}) not found for setting character.", DateTime.Now, null);
                return Result<bool>.Fail("User not found.");
            }
            
            var character = await _context.Avatars.FirstOrDefaultAsync(a => a.Name == characterName);
            if (character == null)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), $"Character ({characterName}) not found for setting character.", $"Character ({characterName}) not found for setting character.", DateTime.Now, null);
                return Result<bool>.Fail("character not found.");
            }

            if (!user.OwnedAvatarsId.Contains(character.Id)) { 
                await _logService.CreateLogAsync(LogType.Error.ToString(), $"User ({userName}) does not own character ({characterName}) for setting character.", $"User ({userName}) does not own character ({characterName}) for setting character.", DateTime.Now, null);
                return Result<bool>.Fail("User does not own this character.");
            }

            user.CurrentAvatarId = character.Id;

            try
            {
                await _context.SaveChangesAsync();
                await _logService.CreateLogAsync(LogType.Success.ToString(), $"User ({userName}) set character ({characterName}) successfully.", $"User ({userName}) set character ({characterName}) successfully.", DateTime.Now, null);
                return Result<bool>.Ok(true);
            }
            catch { 
                await _logService.CreateLogAsync(LogType.Error.ToString(), $"Error occurred while saving changes for user ({userName}) setting character ({characterName}).", $"Error occurred while saving changes for user ({userName}) setting character ({characterName}).", DateTime.Now, null);
                return Result<bool>.Fail("An error occurred while saving changes.");
            }
        }
    }
}
