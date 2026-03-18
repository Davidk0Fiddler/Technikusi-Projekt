using Microsoft.EntityFrameworkCore;
using RetroRealm_Server.DTOs.GetAchievementsDTO;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services.LogService;

namespace RetroRealm_Server.Services.GetAchievementsService.cs
{
    public class GetAchievementsService : IGetAchievementsService
    {
        private readonly RetroRealmDatabaseContext _context;
        private readonly ILogService _logService;
        public GetAchievementsService(RetroRealmDatabaseContext context, ILogService logService)
        {
            _context = context;
            _logService = logService;
        }

        public async Task<Result<List<GetAchievementDTO>>> GetAchievementsAsync()
        {
            var achievements = await _context.Achievements.ToListAsync();

            var result = new List<GetAchievementDTO>();

            achievements.ForEach(a => {
                var requirementEng = "";
                var requirementEsp = "";
                var requirementHun = "";

                switch (a.GameId) {
                    case 1:
                        requirementEng = $"Reach {a.Requirement} points in Bunny Run!";
                        requirementEsp = $"¡Alcanza los {a.Requirement} puntos en Bunny Run!";
                        requirementHun = $"Érj el {a.Requirement} pontot a Bunny Run játékban!";
                        break;
                    case 2:
                        requirementEng = $"Reach {a.Requirement} points in FlappyBird!";
                        requirementEsp = $"¡Alcanza los {a.Requirement} puntos en FlappyBird!";
                        requirementHun = $"Érj el {a.Requirement} pontot a FlappyBird játékban!";
                        break;
                    case 3:
                        requirementEng = $"Complete the Memory Game in {a.Requirement} flips!";
                        requirementEsp = $"¡Completa el juego de memoria en {a.Requirement} turnos!";
                        requirementHun = $"Teljesítsd a Memória játékot {a.Requirement} felfordítás alatt!";
                        break;
                    case 4:
                        requirementEng = $"Complete {a.Requirement} Wordle games!";
                        requirementEsp = $"¡Completa {a.Requirement} juegos de Wordle!";
                        requirementHun = $"Fejezz be {a.Requirement} Wordle játékot!";
                        break;
                }

                var mappedAchievement = new GetAchievementDTO
                {
                    NameEng = a.NameEng,
                    NameEsp = a.NameEsp,
                    NameHun = a.NameHun,
                    PrizeCoin = a.PrizeCoin,
                    RequirementEng = requirementEng,
                    RequirementEsp = requirementEsp,
                    RequirementHun = requirementHun,
                };

                result.Add(mappedAchievement);
            });

            await _logService.CreateLogAsync(LogType.Get.ToString(),null,"Achievements have been requested!", DateTime.Now, null);
            return Result<List<GetAchievementDTO>>.Ok(result);
        }
    }
}
