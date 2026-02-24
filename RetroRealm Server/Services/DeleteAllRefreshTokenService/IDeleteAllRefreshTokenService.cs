using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.DeleteAllRefreshTokenService
{
    public interface IDeleteAllRefreshTokenService
    {
        Task<Result<bool>> DeleteAllRefreshTokenns();
    }
}
