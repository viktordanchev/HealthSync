using Infrastructure;
using Microsoft.EntityFrameworkCore;
using RestAPI.SignalR;
using Server.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersExtension();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextExtension(builder.Configuration);
builder.Services.AddIdentityExtension();
builder.Services.AddJWTAuthentication(builder.Configuration);
builder.Services.AddCorsExtension(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddMemoryCache();
builder.Services.AddConfigs(builder.Configuration);
builder.Services.AddSignalR();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(o =>
{
    o.SwaggerEndpoint("/swagger/v1/swagger.json", "HealthSync API v1");
    o.RoutePrefix = string.Empty;
});

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<HealthSyncDbContext>();
await db.Database.MigrateAsync();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.MapHub<ChatHub>("/chat");

app.Run();
