//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using RetroRealm_Server.DTOs;
//using RetroRealm_Server.Models;
//using RetroRealm_Server.Services.Interfaces;

//namespace RetroRealm_Server.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class RolesController : ControllerBase
//    {
//        private readonly RetroRealmDatabaseContext _context;
//        private readonly IRoleService _roleService;

//        public RolesController(RetroRealmDatabaseContext context, IRoleService roleService)
//        {
//            _context = context;
//            _roleService = roleService;
//        }

//        // GET: api/Roles
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Result<List<ReadRoleDTO>>>>> GetRoles()
//        {
//            var result= await _roleService.GetAllRolesAsync();
//            return Ok(result.Data);
//        }

//        // GET: api/Roles/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<IEnumerable<Result<ReadRoleDTO>>>> GetRole(int id) {
//            var result = await _roleService.GetRoleAsync(id);

//            if (!result.Success) return NotFound();

//            return Ok(result.Data);
//        }

//        // PUT: api/Roles/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutRole(int id, UpdateRoleDTO role)
//        {
//            if (id != role.Id)
//            {
//                return BadRequest();
//            }

//            var result = await _roleService.UpdateRoleAsync(id, role);

//            if (result.Success) return NoContent();

//            if (result.Error == "Role not found") return NotFound();

//            return BadRequest();
//        }

//        // POST: api/Roles
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Result<Role>>> PostRole(CreateRoleDTO role)
//        {
//            var result = await _roleService.CreateRoleAsync(role);

//            if (result.Success) return CreatedAtAction("GetRole", new { id = result.Data }, result.Data);

//            return BadRequest();
//        }

//        // DELETE: api/Roles/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteRole(int id)
//        {
//            var result = await _roleService.DeleteRoleByIdAsync(id);

//            if (result.Success) return NoContent();

//            if (result.Error == "Role not found");
            
//            return BadRequest();
//        }
//    }
//}
