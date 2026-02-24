using Microsoft.EntityFrameworkCore;
using RetroRealm_Server.DTOs.GetAvatarsForUserDTO;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services.LogService;

namespace RetroRealm_Server.Services.GetAvatarsForUserService
{
    public class GetAvatarsForUserService : IGetAvatarsForUserService
    {
        private readonly RetroRealmDatabaseContext _context;
        private readonly ILogService _logService;
        public GetAvatarsForUserService(RetroRealmDatabaseContext context, ILogService logService)
        {
            _context = context;
            _logService = logService;
        }

        public async Task<Result<List<GetAvatarForUserDTO>>> GetAvatarsForUserAsync(string userName)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == userName);

            if (user == null)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, $"User not found for getting avatars! - {userName}", DateTime.Now, null);
                return Result<List<GetAvatarForUserDTO>>.Fail("User not found");
            }

            var avatars = await _context.Avatars.ToListAsync();

            var ownedAvatars = await GetAvatarsForUserDTOMapper(avatars, user.OwnedAvatarsId);

            await _logService.CreateLogAsync(LogType.Get.ToString(), null, $"Avatars of user-{user.Id} have been requested!", DateTime.Now, null);
            return Result<List<GetAvatarForUserDTO>>.Ok(ownedAvatars);
        }

        private async Task<List<GetAvatarForUserDTO>> GetAvatarsForUserDTOMapper(List<Avatar> allAvatars, List<int> ownedAvatars) {
            var mappedAvatars = new List<GetAvatarForUserDTO>();
            
            foreach (var avatar in allAvatars)
            {
                if (ownedAvatars.Contains(avatar.Id)) {
                    var mappedAvatar = new GetAvatarForUserDTO
                    {
                        Name = avatar.Name,
                        Price = avatar.Price,
                    };
                }
            }

            return mappedAvatars;
        }
    }
}
