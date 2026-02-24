using RetroRealm_Server.DTOs._NotUserDTOS;
using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.AvatarService
{
    public interface IAvatarService
    {
        Task<Result<List<ReadAvatarDTO>>> GetAllAvatarsAsync(); // Read (All)
        Task<Result<ReadAvatarDTO>> GetAvatarAsync(int id); // Read (One)
        Task<Result<Avatar>> CreateAvatarAsync(CreateAvatarDTO newAvatar); // Create
        Task<Result<UpdateAvatarDTO>> UpdateAvatarAsync(int id, UpdateAvatarDTO updatedAvatar); // Update
        Task<Result<int>> DeleteAvatarByIdAsync(int id); // Delete
        Task<Result<bool>> PurchaseAvatarAsync(string username, string avatarName);
    }
}
