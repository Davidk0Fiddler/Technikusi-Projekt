using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.SetCharacterService
{
    public interface ISetCharacterService
    {
        Task<Result<bool>> SetCharacter(string characterName, string userName);
    }
}
