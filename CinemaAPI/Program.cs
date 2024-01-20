using CinemaApi.Extensions;
using CinemaApi.Middlewares;
using CinemaApi.Models.Screenings.Converters;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CinemaAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

			//turime susieti AppDbContext is DataAccess projekto su connectionStringu
			builder.Services.AddDbContext<CinemaDbContext>(options =>
				options.UseMySql(connectionString, 
				ServerVersion.AutoDetect(connectionString))
			);

			Log.Logger = new LoggerConfiguration()
				.WriteTo.Console()
				.WriteTo.File("logs/logs.txt", rollingInterval: RollingInterval.Hour)
				.CreateLogger();

			builder.Host.UseSerilog();

			// Add services to the container.

			builder.Services.AddApplicationServices();

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.Converters.Add(new DateConverter());
				options.JsonSerializerOptions.Converters.Add(new TimeConverter());
			});
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			//if (app.Environment.IsDevelopment())
			//{
				app.UseSwagger();
				app.UseSwaggerUI();
			//}

			app.UseHttpsRedirection();

			app.UseMiddleware<ErrorHandlerMiddleware>();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}