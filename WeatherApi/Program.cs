using WeatherApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables
DotNetEnv.Env.Load("../.env");

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddSingleton<IWeatherService, WeatherService>();

// Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Log environment variables for debugging
var env = builder.Configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT");
Console.WriteLine($"Environment: {env}");
Console.WriteLine($"ASPNETCORE_URLS: {Environment.GetEnvironmentVariable("ASPNETCORE_URLS")}");

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather API V1");
});

// ❌ Removed HTTPS redirection (OpenShift handles it)
app.UseAuthorization();

app.MapControllers();

app.Run();
