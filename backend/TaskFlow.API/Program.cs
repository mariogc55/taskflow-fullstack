using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskFlow.Application.Interfaces;
using TaskFlow.Application.Services;
using TaskFlow.Domain.Entities;
using TaskFlow.Infrastructure.Data;
using TaskFlow.Infrastructure.Repositories;
using TaskFlow.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
var envConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
var appSettingsConn = builder.Configuration.GetConnectionString("DefaultConnection");

var connectionString = string.Empty;

if (!string.IsNullOrEmpty(databaseUrl))
{
    var databaseUri = new Uri(databaseUrl);
    var userInfo = databaseUri.UserInfo.Split(':');
    connectionString = $"Host={databaseUri.Host};Port={databaseUri.Port};Database={databaseUri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true;";
}
else if (!string.IsNullOrEmpty(envConnectionString) && envConnectionString.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase))
{
    var databaseUri = new Uri(envConnectionString);
    var userInfo = databaseUri.UserInfo.Split(':');
    connectionString = $"Host={databaseUri.Host};Port={databaseUri.Port};Database={databaseUri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true;";
}
else if (!string.IsNullOrEmpty(envConnectionString))
{
    connectionString = envConnectionString;
}
else
{
    connectionString = appSettingsConn;
}

if (!string.IsNullOrEmpty(connectionString))
{
    connectionString = connectionString.Replace("Trusted_Connection=true;", "", StringComparison.OrdinalIgnoreCase)
                                     .Replace("Trusted_Connection=True;", "", StringComparison.OrdinalIgnoreCase)
                                     .Replace("Server=(localdb)\\mssqllocaldb;", "", StringComparison.OrdinalIgnoreCase);
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy.WithOrigins("http://localhost:4200", "https://localhost:4200", "https://taskflow-fullstack-chi.vercel.app")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
});

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<TokenService>();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["Secret"] ?? "LlavePorDefectoSuperSegura1234567890";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey))
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{port}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    
    int maxRetries = 5;
    int delaySeconds = 4;
    bool migrationSuccess = false;

    for (int i = 1; i <= maxRetries; i++)
    {
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            logger.LogInformation($"Intentando conectar y aplicar migraciones a PostgreSQL (Intento {i}/{maxRetries})...");
            
            context.Database.Migrate();
            logger.LogInformation("¡Migraciones aplicadas correctamente y tablas creadas con éxito!");

            if (!context.Set<User>().Any())
            {
                var defaultUser = new User
                {
                    Username = "admin",
                    Email = "admin@taskflow.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")
                };
                
                context.Set<User>().Add(defaultUser);
                context.SaveChanges();
                logger.LogInformation("Usuario administrador creado por defecto.");
            }

            migrationSuccess = true;
            break;
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, $"Intento {i} fallido al aplicar migraciones. Reintentando en {delaySeconds} segundos...");
            if (i == maxRetries)
            {
                logger.LogError(ex, "Se agotaron todos los intentos para conectar con la base de datos.");
            }
            Thread.Sleep(TimeSpan.FromSeconds(delaySeconds));
        }
    }
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAngular");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();