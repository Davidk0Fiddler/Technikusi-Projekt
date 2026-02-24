using Microsoft.EntityFrameworkCore;
using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.LogService
{
    public interface ILogService
    {
        Task CreateLogAsync(string logType, string? error, string description, DateTime dateTime, int? userId);
        Task<Result<List<Log>>> GetAllLogsAsync();
        Task<Result<Log>> GetLogAsync(int id);
    }
}
