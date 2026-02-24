using Microsoft.EntityFrameworkCore;
using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.LogService
{
    public class LogService : ILogService
    {
        private readonly LogDatabaseContext _context;
        private readonly ILogger<LogService> _logger;
        public LogService(LogDatabaseContext context, ILogger<LogService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateLogAsync(string logType, string? error, string description, DateTime dateTime, int? userId) {
            var newLog = new Log
            {
                LogType = logType,
                Error = error,
                Description = description,
                DateTime = dateTime,
                UserId = userId
            };
            _context.Logs.Add(newLog);

            switch (logType) {
                case "Error":
                    _logger.LogError(description);
                    break;
                default:
                    _logger.LogInformation(description);
                    break;
            }

            await _context.SaveChangesAsync();

        }
        #region Get All Logs
        public async Task<Result<List<Log>>> GetAllLogsAsync()
        {
            var allLogs = await _context.Logs.ToListAsync();

            return Result<List<Log>>.Ok(allLogs);
        }
        #endregion

        #region Get One Log
        public async Task<Result<Log>> GetLogAsync(int id) { 
            var log = await _context.Logs.FirstOrDefaultAsync(l => l.Id == id);
            if (log == null) {
                return Result<Log>.Fail("Log not found!");
            }

            return Result<Log>.Ok(log);
        }
        #endregion
    }
}
