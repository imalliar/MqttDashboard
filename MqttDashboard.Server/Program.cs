using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MqttDashboard.Dal.Identity;
using MqttDashboard.Dal.Model;
using MqttDashboard.Dal.Settings;
using MqttDashboard.Server.Framework;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MqttSettings>(builder.Configuration.GetSection("Mqtt"));

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}


builder.Services.AddDbContext<MqttContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MqttConnection"));
});

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Host.UseNLog();

builder.Services
    .AddIdentity<ApplicationUser, ApplicationRole>()
    .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<MqttContext>();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddCors(options =>
    options.AddPolicy(name: "AngularPolicy",
        cfg =>
        {
            cfg.AllowAnyHeader();
            cfg.AllowAnyMethod();
            cfg.WithOrigins(builder.Configuration["AllowedCORS"]);
        }));

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

var supportedCultures = new[] { "en-US" };
var localizationOptions = new RequestLocalizationOptions { ApplyCurrentCultureToResponseHeaders = true }.SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
