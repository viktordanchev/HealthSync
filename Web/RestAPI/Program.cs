using Server.Extensions;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddIdentity();
builder.Services.Configure<JsonSerializerOptions>(options => new JsonSerializerOptions(JsonSerializerDefaults.Web));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

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
app.UseCors(x =>
{
    x.WithOrigins("https://localhost:5173");
    x.AllowAnyHeader();
    x.AllowAnyMethod();
});

app.MapFallbackToFile("/index.html");

app.Run();
