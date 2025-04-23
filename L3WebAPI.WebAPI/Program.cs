using L3WebApi.Business.Implementations;
using L3WebApi.Business.Interfaces;
using L3WebAPI.DataAccess;
using L3WebAPI.DataAccess.Implementations;
using L3WebAPI.DataAccess.Interfaces;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using NLog;
using NLog.Web;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;

public class Program {
	private static Task WriteHealthCheckResponse(HttpContext context, HealthReport healthReport) {
		context.Response.ContentType = "application/json; charset=utf-8";

		var options = new JsonWriterOptions { Indented = true };

		using var memoryStream = new MemoryStream();
		using (var jsonWriter = new Utf8JsonWriter(memoryStream, options)) {
			jsonWriter.WriteStartObject();
			jsonWriter.WriteString("status", healthReport.Status.ToString());
			jsonWriter.WriteStartObject("results");

			foreach (var healthReportEntry in healthReport.Entries) {
				jsonWriter.WriteStartObject(healthReportEntry.Key);
				jsonWriter.WriteString("status",
					healthReportEntry.Value.Status.ToString());
				jsonWriter.WriteString("description",
					healthReportEntry.Value.Description);
				jsonWriter.WriteStartObject("data");

				foreach (var item in healthReportEntry.Value.Data) {
					jsonWriter.WritePropertyName(item.Key);

					JsonSerializer.Serialize(jsonWriter, item.Value,
						item.Value?.GetType() ?? typeof(object));
				}

				jsonWriter.WriteEndObject();
				jsonWriter.WriteEndObject();
			}

			jsonWriter.WriteEndObject();
			jsonWriter.WriteEndObject();
		}

		return context.Response.WriteAsync(
			Encoding.UTF8.GetString(memoryStream.ToArray()));
	}


	public static void Main(string[] args) {
		var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
		logger.Debug("Starting up app ...");

		try {
			var builder = WebApplication.CreateBuilder(args);

			// Config
			builder.Configuration
				.AddUserSecrets<Program>(true)
				.Build();

			builder.Services.AddDbContext<GameDbContext>(opt =>
				opt.UseNpgsql(
					builder.Configuration.GetConnectionString("GamesDb")
				)
			);

			// Add services to the container.
			builder.Services.AddTransient<IGamesDataAccess, GamesDataAccess>();
			builder.Services.AddTransient<IGamesService, GamesService>();

			builder.Services.AddControllers().AddJsonOptions(options => {
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
			});
			builder.Services.AddOpenApi();

			builder.Services.AddHealthChecks()
				.AddNpgSql(builder.Configuration.GetConnectionString("GamesDb"));

			builder.Logging.ClearProviders();
			builder.Host.UseNLog();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment()) {
				app.MapOpenApi();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapHealthChecks("/health", new HealthCheckOptions {
				ResponseWriter = WriteHealthCheckResponse
			});

			app.MapControllers();

			app.UseSwaggerUI(options => {
				options.ConfigObject.Urls = [new UrlDescriptor {
					Name = "L3 Web API",
					Url = "/openapi/v1.json"
				}];
			});

			using (var scope = app.Services.CreateScope()) {
				var dbContext = scope.ServiceProvider.GetRequiredService<GameDbContext>();

				// Here is the migration executed
				dbContext.Database.Migrate();
			}

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
