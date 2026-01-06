using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services;
using RetroRealm_Server.Services.Interfaces;
using Serilog;
using System.Data;
using System.Text;

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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
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
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAvatarService, AvatarService>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<IBunnyRunStatusService, BunnyRunService>();
builder.Services.AddScoped<IFlappyBirdStatusService, FlappyBirdService>();
builder.Services.AddScoped<IMemoryGameStatusService, MemoryGameService>();
builder.Services.AddScoped<IWordleStatusService, WordleStatusService>();
builder.Services.AddScoped<IAchievementsService, AchievementService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
