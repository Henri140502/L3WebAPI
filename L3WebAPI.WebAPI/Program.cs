using L3WebApi.Business.Implementations;
using L3WebApi.Business.Interfaces;
using L3WebAPI.DataAccess.Implementations;
using L3WebAPI.DataAccess.Interfaces;
using NLog;
using NLog.Web;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json.Serialization;

public class Program {

	public static void Main(string[] args) {
		var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
		logger.Debug("Starting up app ...");

		try {
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddTransient<IGamesDataAccess, GamesDataAccess>();
			builder.Services.AddTransient<IGamesService, GamesService>();

			builder.Services.AddControllers().AddJsonOptions(options => {
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
			});
			builder.Services.AddOpenApi();

			builder.Logging.ClearProviders();
			builder.Host.UseNLog();

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
					Name = "L3 Web API",
					Url = "/openapi/v1.json"
				}];
			});

			app.Run();

		} catch (Exception exception) {
			// NLog: catch setup errors
			logger.Error(exception, "Stopped program because of exception");
			throw;
		} finally {
			// Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
			NLog.LogManager.Shutdown();
		}
	}
}
