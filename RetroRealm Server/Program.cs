using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services._NotUserServices;
using RetroRealm_Server.Services._NotUserServices.Interfaces;
using RetroRealm_Server.Services.AvatarService;
using RetroRealm_Server.Services.BunnyRunService;
using RetroRealm_Server.Services.CheckAchievementsService;
using RetroRealm_Server.Services.DeleteAllRefreshTokenService;
using RetroRealm_Server.Services.FlappyBirdService;
using RetroRealm_Server.Services.GetAchievementsByUserService;
using RetroRealm_Server.Services.GetAvatarsForUserService;
using RetroRealm_Server.Services.Jwt_Service;
using RetroRealm_Server.Services.LeaderboardService;
using RetroRealm_Server.Services.LeaderBoardService;
using RetroRealm_Server.Services.Login_Service;
using RetroRealm_Server.Services.LogoutService;
using RetroRealm_Server.Services.LogService;
using RetroRealm_Server.Services.MemoryGameService;
using RetroRealm_Server.Services.RefreshTokenService;
using RetroRealm_Server.Services.Register_Service;
using RetroRealm_Server.Services.UserService;
using RetroRealm_Server.Services.WorldeStatusService;
using Serilog;
using System.Security.Claims;
using System.Text;

JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();
var builder = WebApplication.CreateBuilder(args);


//// Pasting the login system 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ClockSkew = TimeSpan.FromMinutes(2),
            NameClaimType = ClaimTypes.Name,
            RoleClaimType = ClaimTypes.Role
        };
    });


builder.Services.AddSingleton<IAuthorizationHandler, RoleOrRoleIdHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.Requirements.Add(new RoleOrRoleIdRequirement("Admin", 1)));

    options.AddPolicy("UserOnly", policy =>
        policy.Requirements.Add(new RoleOrRoleIdRequirement("User", 2)));
});


// Add services to the container.
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
builder.Services.AddScoped<ILogOutService, LogoutService>();
builder.Services.AddScoped<IDeleteAllRefreshTokenService, DeleteAllRefreshTokenService>();
builder.Services.AddScoped<IGetAchievementsByUserService, GetAchievementsByUserService>();
builder.Services.AddScoped<IGetAvatarsForUserService, GetAvatarsForUserService>();
builder.Services.AddScoped<IBunnyRunStatusService, BunnyRunService>();
builder.Services.AddScoped<IFlappyBirdStatusService, FlappyBirdService>();
builder.Services.AddScoped<IMemoryGameStatusService, MemoryGameService>();
builder.Services.AddScoped<IWordleStatusService, WordleStatusService>();
builder.Services.AddScoped<ILeaderboardService, LeaderboardService>();
builder.Services.AddScoped<ICheckAchievementService, CheckAchievementService>();


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAvatarService, AvatarService>();

builder.Services.AddScoped<IAchievementsService, AchievementService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "RetroRealm API",
        Version = "v1"
    });

    //  JWT Bearer definition
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Írd be így: Bearer {JWT token}"
    });

    //  Apply globally
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var logger = new LoggerConfiguration()
.ReadFrom.Configuration(builder.Configuration)
.Enrich.FromLogContext()
.CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);



builder.Services.AddDbContext<RetroRealmDatabaseContext>(

db => db.UseSqlite(builder.Configuration.GetConnectionString("RetroRealmDatabaseContex")));


builder.Services.AddDbContext<LogDatabaseContext>(

db => db.UseSqlite(builder.Configuration.GetConnectionString("RetroRealmLogDatabaseContex")));




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<RetroRealmDatabaseContext>();

    // RefreshTokens tábla kiürítése
    db.Database.ExecuteSqlRaw("DELETE FROM RefreshTokens;");

    // AUTOINCREMENT nullázása (ha van)
    db.Database.ExecuteSqlRaw("DELETE FROM sqlite_sequence WHERE name='RefreshTokens';");
}


app.UseDefaultFiles();
string currentDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(currentDir),
    RequestPath = ""
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
