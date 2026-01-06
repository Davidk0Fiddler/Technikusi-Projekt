using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using RetroRealm_Server.DTOs;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services.Interfaces;
using System.Dynamic;
using static RetroRealm_Server.Services.UserService;

namespace RetroRealm_Server.Services
{
    public class UserService : IUserService
    {
        private readonly RetroRealmDatabaseContext _context;
        private readonly ILogService _logService;
        private readonly IBunnyRunStatusService _bunnyRunStatusService;
        private readonly IFlappyBirdStatusService _flappyBirdStatusService;
        private readonly IMemoryGameStatusService _memoryGameStatusService;
        private readonly IWordleStatusService _wordleStatusService;

        public class StatusCreationErrorHandlerListItem
        {
            public int UserId { get; set; }
            public int Tryings { get; set; }

            public StatusCreationErrorHandlerListItem(int userId, int tryings) { 
                this.UserId = userId;
                this.Tryings = tryings;
            }
        }

        private List<StatusCreationErrorHandlerListItem> statusCreationErrorHandlingIds = new List<StatusCreationErrorHandlerListItem>(); 

        public UserService(RetroRealmDatabaseContext context, ILogService logService, IBunnyRunStatusService bunnyRunStatusService, IFlappyBirdStatusService flappyBirdStatusService, IMemoryGameStatusService memoryGameStatusService, IWordleStatusService wordleStatusService)
        {
            _context = context;
            _logService = logService;
            _bunnyRunStatusService = bunnyRunStatusService;
            _flappyBirdStatusService = flappyBirdStatusService;
            _memoryGameStatusService = memoryGameStatusService;
            _wordleStatusService = wordleStatusService;
        }

        public async Task<Result<User>> CreateNewUserAsync(RegisterDTO model)
        {
            if (await _context.Users.AnyAsync(u => u.Username == model.Username))
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, $"User already exists! ({model.Username})", DateTime.Now, null);
                return Result<User>.Fail("User already exists");
            }

            var newUser = new User
            {
                Username = model.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                RoleId = 2
            };

            _context.Users.Add(newUser);
            try
            {
                await _context.SaveChangesAsync();
                await _logService.CreateLogAsync(LogType.Create.ToString(), null, $"User has been created! ({newUser.Username})", DateTime.Now, null);
                return Result<User>.Ok(newUser);
            }
            catch (DbUpdateException ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, "Database error while registering!", DateTime.Now, null);
                return Result<User>.Fail("Database error");
            }
            catch (Exception ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, "Error while registering!", DateTime.Now, null);
                return Result<User>.Fail("Error");

            }
        }

        public async Task<Result<bool>> AddStatusesToUserAsync(string username)
        {
            var user = await _context.Users.Include(x => x.BunnyRunStatus)
                                           .Include(x => x.FlappyBirdStatus)
                                           .Include(x => x.WorldeStatus)
                                           .Include(x => x.MemoryGameStatus)
                                           .SingleOrDefaultAsync(x => x.Username == username);

            // A státuszok elkészítése
            await _bunnyRunStatusService.CreateBunnyRunStatusAsync(user.Id);
            await _flappyBirdStatusService.CreateFlappyBirdStatusAsync(user.Id);
            await _memoryGameStatusService.CreateMemoryGameStatusAsync(user.Id);
            await _wordleStatusService.CreateWordleStatusAsync(user.Id);

            // A státuszok betöltése a user-be
            var bunnyRunStatus = await _context.Bunny_Run_Status.SingleOrDefaultAsync(x => x.UserId == user.Id);
            var flappyBirdStatus = await _context.Flappy_Bird_Status.SingleOrDefaultAsync(x => x.UserId == user.Id);
            var memoryGameStatus = await _context.Memory_Game_Status.SingleOrDefaultAsync(x => x.UserId == user.Id);
            var wordleStatus = await _context.Wordle_Status.SingleOrDefaultAsync(x => x.UserId == user.Id);

            // Ha a bármelyik legenerálás hibás
            if (bunnyRunStatus == null || flappyBirdStatus == null || memoryGameStatus == null || wordleStatus == null)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, "Error during creating statuses!", DateTime.Now, user.Id);
                statusCreationErrorHandling(user);                
                    return Result<bool>.Fail("Error during creating statuses!");
            }

            // Státusz id-k behelyezése a User objektumba
            user.BunnyRunStatusId = bunnyRunStatus.Id;
            user.FlappyBirdStatusId = flappyBirdStatus.Id;
            user.MemoryCardStatusId = memoryGameStatus.Id;
            user.WordleStatusId = wordleStatus.Id;

            try
            {
                await _context.SaveChangesAsync();
                await _logService.CreateLogAsync(LogType.Create.ToString(), null, $"Statuses has been added! ({user.Username})", DateTime.Now, user.Id);
                return Result<bool>.Ok(true);
            }
            catch (DbUpdateException ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, "Database error while adding statuses!", DateTime.Now, user.Id);
                return Result<bool>.Fail("Database error");
            }
            catch (Exception ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, "Error while adding statuses!", DateTime.Now, user.Id);
                return Result<bool>.Fail("Error");
            }

        }

        public async Task<Result<ReadUserDTO>> GetUserData(string username)
        {
            var user = await _context.Users.Include(x => x.Role)
                                           .Include(x => x.BunnyRunStatus)
                                           .Include(x => x.FlappyBirdStatus)
                                           .Include(x => x.WorldeStatus)
                                           .Include(x => x.MemoryGameStatus)
                                           .SingleOrDefaultAsync(x => x.Username == username);

            if (user == null)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, "User not found for data request!", DateTime.Now, null);
                return Result<ReadUserDTO>.Fail("User not found!");
            }

            await _logService.CreateLogAsync(LogType.Get.ToString(), null, $"User (id-{user.Id}) data has been requested!", DateTime.Now, user.Id);
            return Result<ReadUserDTO>.Ok(ToReadUserDTO(user));
        }

        private ReadUserDTO ToReadUserDTO(User user) 
        { 
            return new ReadUserDTO {
                UserName = user.Username,
                Coins = user.Coins,
                RoleName = user.Role.Name,
                CurrentAvatarId = user.CurrentAvatarId,
                IsOnline = user.IsOnline,
                BunnyRunStatus = user.BunnyRunStatus,
                FlappyBirdStatus = user.FlappyBirdStatus,
                MemoryGameStatus = user.MemoryGameStatus,
                WordleStatus = user.WorldeStatus,
            }; 
        }

        private async void statusCreationErrorHandling(User user)
        {
            bool IsFixed = false;
            statusCreationErrorHandlingIds.Add(new StatusCreationErrorHandlerListItem(user.Id, 1));

            await _bunnyRunStatusService.CreateBunnyRunStatusAsync(user.Id);
            await _flappyBirdStatusService.CreateFlappyBirdStatusAsync(user.Id);
            await _memoryGameStatusService.CreateMemoryGameStatusAsync(user.Id);
            await _wordleStatusService.CreateWordleStatusAsync(user.Id);

            // A státuszok betöltése a user-be
            var bunnyRunStatus = await _context.Bunny_Run_Status.SingleOrDefaultAsync(x => x.UserId == user.Id);
            var flappyBirdStatus = await _context.Flappy_Bird_Status.SingleOrDefaultAsync(x => x.UserId == user.Id);
            var memoryGameStatus = await _context.Memory_Game_Status.SingleOrDefaultAsync(x => x.UserId == user.Id);
            var wordleStatus = await _context.Wordle_Status.SingleOrDefaultAsync(x => x.UserId == user.Id);

            // Ha a bármelyik legenerálás hibás
            if (bunnyRunStatus == null || flappyBirdStatus == null || memoryGameStatus == null || wordleStatus == null)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, $"Error during creating statuses in the Error Handling! (Tryings: {statusCreationErrorHandlingIds.SingleOrDefault(x => x.UserId == user.Id).Tryings})", DateTime.Now, user.Id);
                statusCreationErrorHandlingIds.SingleOrDefault(x => x.UserId == user.Id).Tryings++;
                while (!IsFixed && statusCreationErrorHandlingIds.SingleOrDefault(x => x.UserId == user.Id).Tryings < 5)
                {
                    await _bunnyRunStatusService.CreateBunnyRunStatusAsync(user.Id);
                    await _flappyBirdStatusService.CreateFlappyBirdStatusAsync(user.Id);
                    await _memoryGameStatusService.CreateMemoryGameStatusAsync(user.Id);
                    await _wordleStatusService.CreateWordleStatusAsync(user.Id);

                    // A státuszok betöltése a user-be
                    bunnyRunStatus = await _context.Bunny_Run_Status.SingleOrDefaultAsync(x => x.UserId == user.Id);
                    flappyBirdStatus = await _context.Flappy_Bird_Status.SingleOrDefaultAsync(x => x.UserId == user.Id);
                    memoryGameStatus = await _context.Memory_Game_Status.SingleOrDefaultAsync(x => x.UserId == user.Id);
                    wordleStatus = await _context.Wordle_Status.SingleOrDefaultAsync(x => x.UserId == user.Id);

                    // Ha a bármelyik legenerálás hibás
                    if (bunnyRunStatus == null || flappyBirdStatus == null || memoryGameStatus == null || wordleStatus == null)
                    {
                        await _logService.CreateLogAsync(LogType.Error.ToString(), null, $"Error during creating statuses in the Error Handling! (Tryings: {statusCreationErrorHandlingIds.SingleOrDefault(x => x.UserId == user.Id).Tryings})", DateTime.Now, user.Id);
                        statusCreationErrorHandlingIds.SingleOrDefault(x => x.UserId == user.Id).Tryings++;
                    }
                    else
                    {
                        await _logService.CreateLogAsync(LogType.Succes.ToString(), null, $"Error  during creating statuses FIXED in the Error Handling! (Tryings: {statusCreationErrorHandlingIds.SingleOrDefault(x => x.UserId == user.Id).Tryings})", DateTime.Now, user.Id);
                        IsFixed = true;
                        statusCreationErrorHandlingIds.Remove(statusCreationErrorHandlingIds.SingleOrDefault(x => x.UserId == user.Id));
                    }
                }
            }
            else
            {
                await _logService.CreateLogAsync(LogType.Succes.ToString(), null, $"Error  during creating statuses FIXED in the Error Handling! (Tryings: {statusCreationErrorHandlingIds.SingleOrDefault(x => x.UserId == user.Id).Tryings})", DateTime.Now, user.Id);
                IsFixed = true;
                statusCreationErrorHandlingIds.Remove(statusCreationErrorHandlingIds.SingleOrDefault(x => x.UserId == user.Id));
            }
        }
    }
}
