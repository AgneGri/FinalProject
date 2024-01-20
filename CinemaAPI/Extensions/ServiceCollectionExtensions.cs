using CinemaApi.Models.Movies.DTOs;
using CinemaApi.Models.Movies.Parameters;
using CinemaApi.Services.Movies.Decorators;
using CinemaApi.Services;
using DataAccess;
using DataAccess.Entities;
using CinemaApi.Models.Screenings.DTOs;
using CinemaApi.Models.Screenings.Parameters;
using CinemaApi.Services.Screenings.Services;
using CinemaApi.Services.Movies.Converters;
using CinemaApi.Services.Screenings.Converters;
using CinemaApi.Services.Screenings.Decorators;
using CinemaApi.Services.Movies.Services;
using CinemaApi.Models.Cinemas.Parameters;
using CinemaApi.Models.Cinemas.DTOs;
using CinemaApi.Services.Cinemas.Services;
using CinemaApi.Services.Cinemas.Converters;
using CinemaApi.Services.Cinemas.Decorators;
using CinemaApi.Models.Movies.Validators;
using CinemaApi.Models.Cinemas.Validators;
using CinemaApi.Models.Screenings.Validators;

namespace CinemaApi.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			RegisterRepositories(services);

			RegisterServices(services);

			RegisterConverters(services);

			return services;
		}

		private static void RegisterRepositories(IServiceCollection services)
		{
			services.AddTransient<IMovieRepository, MoviesRepository>();
			services.AddTransient<IRepository<Movie>, MoviesRepository>();
			services.AddTransient<IRepository<Screening>, ScreeningsRepository>();
			services.AddTransient<IScreeningRepository, ScreeningsRepository> ();
			services.AddTransient<IRepository<Cinema>, CinemasRepository>();
			services.AddTransient<ICinemaRepository, CinemasRepository>();
		}

		private static void RegisterServices (IServiceCollection services) 
		{
			#region Movie
			services.AddTransient<IService<CreateMovieParameter, MovieDto>, CreateMovieService>();
			services.Decorate<IService<CreateMovieParameter, MovieDto>, CreateMovieServiceDecorator>();
			services.AddTransient<CreateMovieParameterValidator>();

			services.AddTransient<IService<UpdateMovieParameter, MovieDto>, UpdateMovieService>();
			services.Decorate<IService<UpdateMovieParameter, MovieDto>, UpdateMovieServiceDecorator>();
			services.AddTransient<UpdateMovieParameterValidator>();

			services.AddTransient<IService<GetMovieParameter, MovieDto>, GetMovieService>();
			services.Decorate<IService<GetMovieParameter, MovieDto>, GetMovieServiceDecorator>();

			services.AddTransient<IService<ListMovieParameter, List<MovieDto>>, ListMovieService>();
			services.Decorate<IService<ListMovieParameter, List<MovieDto>>, ListMovieServiceDecorator>();

			services.AddTransient<IService<DeleteMovieParameter, MovieDto>, DeleteMovieService>();
			services.Decorate<IService<DeleteMovieParameter, MovieDto>, DeleteMovieServiceDecorator>();
			#endregion

			#region Screening
			services.AddTransient<IService<CreateScreeningParameter, ScreeningDto>, CreateScreeningService>();
			services.Decorate<IService<CreateScreeningParameter, ScreeningDto>, CreateScreeningServiceDecorator>();
			services.AddTransient<CreateScreeningParameterValidator>();

			services.AddTransient<IService<UpdateScreeningParameter, ScreeningDto>, UpdateScreeningService>();
			services.Decorate<IService<UpdateScreeningParameter, ScreeningDto>, UpdateScreeningServiceDecorator>();
			services.AddTransient<UpdateScreeningParameterValidator>();

			services.AddTransient<IService<GetScreeningParameter, ScreeningDto>, GetScreeningService>();
			services.Decorate<IService<GetScreeningParameter, ScreeningDto>, GetScreeningServiceDecorator>();

			services.AddTransient<IService<ListScreeningParameter, List<ScreeningDto>>, ListScreeningService>();
			services.Decorate<IService<ListScreeningParameter, List<ScreeningDto>>, ListScreeningServiceDecorator>();
			services.AddTransient<IService<ListScreeningParameter, List<ScreeningShowingsDto>>, ListScreeningWithVariousParametersService>();
			services.Decorate<IService<ListScreeningParameter, List<ScreeningShowingsDto>>, ListScreeningWithVariousParametersServiceDecorator>();

			services.AddTransient<IService<DeleteScreeningParameter, ScreeningDto>, DeleteScreeningService>();
			services.Decorate<IService<DeleteScreeningParameter, ScreeningDto>, DeleteScreeningServiceDecorator>();
			#endregion

			#region Cinema
			services.AddTransient<IService<CreateCinemaParameter, CinemaDto>, CreateCinemaService>();
			services.Decorate<IService<CreateCinemaParameter, CinemaDto>, CreateCinemaServiceDecorator>();
			services.AddTransient<CreateCinemaParameterValidator>();

			services.AddTransient<IService<UpdateCinemaParameter, CinemaDto>, UpdateCinemaService>();
			services.Decorate<IService<UpdateCinemaParameter, CinemaDto>, UpdateCinemaServiceDecorator>();
			services.AddTransient<UpdateCinemaParameterValidator>();

			services.AddTransient<IService<GetCinemaParameter, CinemaDto>, GetCinemaService>();
			services.Decorate<IService<GetCinemaParameter, CinemaDto>, GetCinemaServiceDecorator>();

			services.AddTransient<IService<ListCinemaParameter, List<CinemaDto>>, ListCinemaService>();
			services.Decorate<IService<ListCinemaParameter, List<CinemaDto>>, ListCinemaServiceDecorator>();

			services.AddTransient<IService<DeleteCinemaParameter, CinemaDto>, DeleteCinemaService>();
			services.Decorate<IService<DeleteCinemaParameter, CinemaDto>, DeleteCinemaServiceDecorator>();
			#endregion
		}

		private static void RegisterConverters(IServiceCollection services)
		{
			services.AddTransient<MovieConverter>();
			services.AddTransient<MovieDtoConverter>();
			services.AddTransient<ScreeningConverter>();
			services.AddTransient<ScreeningDtoConverter>();
			services.AddTransient<ScreeningShowingsDtoConverter>();
			services.AddTransient<CinemaConverter>();
			services.AddTransient<CinemaDtoConverter>();
		}
	}
}