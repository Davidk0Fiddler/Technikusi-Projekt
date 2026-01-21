using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using RetroRealm_Server.DTOs;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



[ApiController]

[Route("api/[controller]")]

public class AuthController : ControllerBase

{
    private readonly IAuthService _authService;


    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO model){
        var result = await _authService.RegisterAsync(model);

        if (result.Success) return Created();

        if (result.Error == "User already exists") return BadRequest("User already exists!");

        return BadRequest();
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO model)
    {
        var result = await _authService.LoginAsync(model);

        if (result.Success) return Ok(result.Data);

        return Unauthorized("Invalid credentials.");

    }


    [HttpPost("refreshToken")]

    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto model)
    {
        var result = await _authService.RefreshTokenAsync(model);

        if (result.Success) return Ok(result.Data);

        if (result.Error == "Invalid refresh token") return Unauthorized("Invalid refresh token");

        return Unauthorized("Expired refresh token");

    }


    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] RefreshTokenDto model)
    {
        var result = _authService.LogoutAsync(model);
        return Ok(new { message = "Logged out successfully." });
    }

    [HttpDelete("allRefreshTokens")]
    public async Task<IActionResult> DeleteAllRefreshTokens() {
        _authService.DeleteAllRefreshTokenns();
        return NoContent();
    }
}   