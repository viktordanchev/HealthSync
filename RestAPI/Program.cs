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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHub<ChatHub>("/chat");

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
