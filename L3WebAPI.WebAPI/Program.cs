using L3WebAPI.Buisness.Implementations;
using L3WebAPI.Buisness.Interfaces;
using L3WebAPI.DataAcces.Implementaions;
using L3WebAPI.DataAcces.Interfaces;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IGamesService, GamesService>();
builder.Services.AddTransient<IGameDataAccess, GameDataAccess>();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwaggerUI(options => {
	options.ConfigObject.Urls = [new UrlDescriptor {
		Name = "L3 Webp API",
		Url = "/openapi/v1.json"
	}];
});

app.Run();
