using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroRealm_Server.DTOs;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services.Interfaces;
using System.Data;

namespace RetroRealm_Server.Services
{
    public class RoleService : IRoleService
    {
        private readonly RetroRealmDatabaseContext _context;
        private readonly ILogService _logService;

        public RoleService(RetroRealmDatabaseContext context, ILogService logService) { 
            _context = context;
            _logService = logService;
        }

        #region Get All Roles 
        public async Task<Result<List<ReadRoleDTO>>> GetAllRolesAsync() { 
            var allRoles = await _context.Roles.Select(r => ToReadRoleDTO(r)).ToListAsync();

            await _logService.CreateLogAsync(LogType.Get.ToString(), null, "All roles have been requested!", DateTime.Now, null);
            return Result<List<ReadRoleDTO>>.Ok(allRoles);
        }
        #endregion

        #region Get One Role
        public async Task<Result<ReadRoleDTO>> GetRoleAsync(int id) { 
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);

            if (role == null) {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, $"Role has not found for GET! Id-{id}", DateTime.Now, null);
                return Result<ReadRoleDTO>.Fail("Role not found!");
            }

            await _logService.CreateLogAsync(LogType.Get.ToString(), null, $"Role ({role.Name} has been requested!", DateTime.Now, null);
            return Result<ReadRoleDTO>.Ok(ToReadRoleDTO(role));
        }
        #endregion

        #region Create Role
        public async Task<Result<Role>> CreateRoleAsync(CreateRoleDTO newRole)
        {
            var role = new Role
            {
                Name = newRole.RoleName
            };

            _context.Roles.Add(role);
            try
            {
                await _context.SaveChangesAsync();
                await _logService.CreateLogAsync(LogType.Create.ToString(),null, $"Role has been created! ({role.Name})",DateTime.Now, null);
                return Result<Role>.Ok(role);
            }
            catch (DbUpdateException ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Database error while creating a role! ({role.Name})" , DateTime.Now, null);
                return Result<Role>.Fail("Database error!");
            }
            catch (Exception ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Error while creating a role! ({role.Name})", DateTime.Now, null);
                return Result<Role>.Fail("Error!");
            }
        }

        #endregion

        #region Update Role
        public async Task<Result<bool>> UpdateRoleAsync(int id, UpdateRoleDTO UpdatedRole) {
            var CurrentRole = await _context.Roles.FindAsync(id);

            if (CurrentRole == null)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, $"Role not found for UPDATE! Id-{id}", DateTime.Now, null);
                return Result<bool>.Fail("Role not found");
            }

            CurrentRole.Name = UpdatedRole.RoleName;

            try
            {
                await _context.SaveChangesAsync();
                await _logService.CreateLogAsync(LogType.Update.ToString(), null, $"Role (ID: {CurrentRole.Id}) has been updated!", DateTime.Now, null);
                return Result<bool>.Ok(true);
            }
            catch (DbUpdateException ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Database error during updating role! (ID: {CurrentRole.Id})", DateTime.Now, null);
                return Result<bool>.Fail("Database error");
            }
            catch (Exception ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Error during updating role! (ID: {CurrentRole.Id})", DateTime.Now, null);
                return Result<bool>.Fail("Error");
            }
        }
        #endregion

        #region Delete Role
        public async Task<Result<bool>> DeleteRoleByIdAsync(int id) {
            var deletingItem = await _context.Roles.FindAsync(id);

            if (deletingItem == null)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, "Role not found for DELETE!", DateTime.Now, null);
                return Result<bool>.Fail("Role not found");
            }

            _context.Roles.Remove(deletingItem);

            try
            {
                await _context.SaveChangesAsync();
                await _logService.CreateLogAsync(LogType.Delete.ToString(), null, $"Role ({deletingItem.Name}) has been deleted!", DateTime.Now, null);
                return Result<bool>.Ok(true);
            }
            catch (DbUpdateException ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Database error during deleting role! ({deletingItem.Name})", DateTime.Now, null);
                return Result<bool>.Fail("Database error");
            }
            catch (Exception ex)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), ex.Message, $"Error during deleting role! ({deletingItem.Name})", DateTime.Now, null);
                return Result<bool>.Fail("Error");
            }
        }
        #endregion
        private static ReadRoleDTO ToReadRoleDTO(Role r)
        {
            return new ReadRoleDTO
            {
                RoleName = r.Name
            };
        }
    }
}
