using Server.Extensions;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddIdentity();
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddCorsExtension(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddMemoryCache();
builder.Services.Configure<JsonSerializerOptions>(options => new JsonSerializerOptions(JsonSerializerDefaults.Web));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Cors");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
