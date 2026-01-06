using RetroRealm_Server.DTOs;
using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.Interfaces
{
    public interface IAvatarService
    {
        Task<Result<List<ReadAvatarDTO>>> GetAllAvatarsAsync(); // Read (All)
        Task<Result<ReadAvatarDTO>> GetAvatarAsync(int id); // Read (One)
        Task<Result<Avatar>> CreateAvatarAsync(CreateAvatarDTO newAvatar); // Create
        Task<Result<UpdateAvatarDTO>> UpdateAvatarAsync(int id, UpdateAvatarDTO updatedAvatar); // Update
        Task<Result<int>> DeleteAvatarByIdAsync(int id); // Delete
        Task<Result<bool>> PurchaseAvatarAsync(RefreshTokenDto model, int avatarId);
    }
}
