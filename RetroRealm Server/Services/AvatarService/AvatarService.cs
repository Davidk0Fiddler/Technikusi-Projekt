using Microsoft.EntityFrameworkCore;
using RetroRealm_Server.DTOs._NotUserDTOS;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services.LogService;

namespace RetroRealm_Server.Services.AvatarService
{
    public class AvatarService : IAvatarService
    {
        private readonly RetroRealmDatabaseContext _context;
        private readonly ILogService _logService;
        //private readonly IRefreshTokenService _refreshTokenService;


        public AvatarService(RetroRealmDatabaseContext context , ILogService logService, ILogger<AvatarService> logger
            //, IRefreshTokenService refreshTokenService
            )
        {
            _context = context;
            _logService = logService;
            //_refreshTokenService = refreshTokenService;
        }

        #region Get All Avatars
        public async Task<Result<List<ReadAvatarDTO>>> GetAllAvatarsAsync() {
            var allAvatars = await _context.Avatars.Select(r => ToReadAvatarDTO(r)).ToListAsync();

            await _logService.CreateLogAsync(LogType.Get.ToString(), null, "All avatars have been requested!",DateTime.Now,null);
            return Result<List<ReadAvatarDTO>>.Ok(allAvatars);
        }
        #endregion

        #region Get One Avatar
        public async Task<Result<ReadAvatarDTO>> GetAvatarAsync(int id)
        {
            var avatar = await _context.Avatars.FirstOrDefaultAsync(r => r.Id == id);

            if (avatar == null) {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, "Avatar has not found for GET!", DateTime.Now, null);
                return Result<ReadAvatarDTO>.Fail("Avatar not found!");
            }
            await _logService.CreateLogAsync(LogType.Get.ToString(), null, $" Avatar ({avatar.Name} has been requested!", DateTime.Now, null);
            return Result<ReadAvatarDTO>.Ok(ToReadAvatarDTO(avatar));
        }

        #endregion

        #region Create Avatar
        public async Task<Result<Avatar>> CreateAvatarAsync(CreateAvatarDTO newAvatar) {
            var avatar = new Avatar
            {
                Name = newAvatar.AvatarName,
                Price = newAvatar.Price
            };

            _context.Avatars.Add(avatar);
            try
            {
                await _context.SaveChangesAsync();
                await _logService.CreateLogAsync(LogType.Create.ToString(), null, $"Avatar has been created! ({avatar})",DateTime.Now,null);
                return Result<Avatar>.Ok(avatar) ;
            }
            catch (DbUpdateException ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message,"Database error while creating an avatar!", DateTime.Now, null);
                return Result<Avatar>.Fail("Database error");
            }
            catch (Exception ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, "Error while creating an avatar!", DateTime.Now, null);
                return Result<Avatar>.Fail("Error");
            }
        }
        #endregion

        #region Update Avatar
        public async Task<Result<UpdateAvatarDTO>> UpdateAvatarAsync(int id, UpdateAvatarDTO updatedAvatar)
        {
            var CurrentAvatar = await _context.Avatars.FindAsync(id);

            if (CurrentAvatar == null)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, $"Avatar not found for UPDATE! {updatedAvatar.Id}", DateTime.Now, null);
                return Result<UpdateAvatarDTO>.Fail("Avatar not found!"); 
            }
            

            CurrentAvatar.Name = updatedAvatar.AvatarName;
            CurrentAvatar.Price = updatedAvatar.Price;
            try
            {
                await _context.SaveChangesAsync();
                await _logService.CreateLogAsync(LogType.Update.ToString(), null, $"Avatar (ID: {CurrentAvatar.Id}) has been updated!", DateTime.Now, null);
                return Result<UpdateAvatarDTO>.Ok(updatedAvatar);
            }
            catch (DbUpdateException ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Database error during updating avatar! {CurrentAvatar.Id}", DateTime.Now, null);
                return Result<UpdateAvatarDTO>.Fail("Database error");
            }
            catch (Exception ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Error during updating avatar! {CurrentAvatar.Id}", DateTime.Now, null);
                return Result<UpdateAvatarDTO>.Fail("Error");
            }
        }
        #endregion

        #region Delete Avatar
        public async Task<Result<int>> DeleteAvatarByIdAsync(int id)
        {
            var DeletingItem = await _context.Avatars.FindAsync(id);

            if (DeletingItem == null)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, $"Avatar not found for DELETE! - {id}", DateTime.Now, null);
                return Result<int>.Fail("Avatar not found");
            }

            _context.Avatars.Remove(DeletingItem);

            try
            {
                await _context.SaveChangesAsync();
                await _logService.CreateLogAsync(LogType.Delete.ToString(), null, $"Avatar ({DeletingItem.Name}) has been deleted!", DateTime.Now, null);
                return Result<int>.Ok(id);
            }
            catch (DbUpdateException ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Database error during deleting avatar! {DeletingItem.Id}", DateTime.Now, null);
                return Result<int>.Fail("Database error");
            }
            catch (Exception ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Error during deleting avatar! {DeletingItem.Id}", DateTime.Now, null);
                return Result<int>.Fail("Error");
            }
        }
        #endregion

        #region Purchase Avatar

        public async Task<Result<bool>> PurchaseAvatarAsync(string username, string avatarName) {
            //var result = await _refreshTokenService.CheckExpireDateAsync(model);

            //if (!result) return Result<bool>.Fail("Refreshtoken expired or does not exists");

            //var userId = await _refreshTokenService.GetUserIdFromRefreshTokenAsync(model);
		    var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
		    if (user == null) 
            {
               return Result<bool>.Fail("Not valid User!");
		    }

		    var avatar = await _context.Avatars.FirstOrDefaultAsync(a => a.Name == avatarName);
		    if (avatar == null) 
		    {
		    	return Result<bool>.Fail("Not valid avatarId");	
		    }
		    
		    if (user.Coins < avatar.Price) {
		    	return Result<bool>.Fail("User does not have enough coins!");
		    }

		    user.OwnedAvatarsId.Add(avatar.Id);

            try
            {
                await _context.SaveChangesAsync();
                await _logService.CreateLogAsync(LogType.Succes.ToString(), null, $"Avatar ({avatarName}) has been succesfully purchased by User-{user.Id}!", DateTime.Now, user.Id);
                return Result<bool>.Ok(true);
            }
            catch (DbUpdateException ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Database error during purchasing avatar! User-{user.Id} | Avatar-{avatarName}", DateTime.Now, null);
                return Result<bool>.Fail("Database error");
            }
            catch (Exception ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Error during purchasing avatar! User-{user.Id} | Avatar-{avatarName}", DateTime.Now, null);
                return Result<bool>.Fail("Error");
            }
	    }
        #endregion


        private static ReadAvatarDTO ToReadAvatarDTO(Avatar a)
            {
            return new ReadAvatarDTO
            {
                AvatarName = a.Name,
                Price = a.Price,
            };
        }
    }
}
