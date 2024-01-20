using CinemaApi.Exceptions;
using CinemaApi.Models;
using CinemaApi.Models.Movies.DTOs;
using CinemaApi.Models.Movies.Parameters;

namespace CinemaApi.Services.Movies.Decorators
{
	public class CreateMovieServiceDecorator : IService<CreateMovieParameter, MovieDto>
	{
		private readonly IService<CreateMovieParameter, MovieDto> _createMovieService;
		private readonly ILogger<CreateMovieServiceDecorator> _logger;

		public CreateMovieServiceDecorator(
			IService<CreateMovieParameter, MovieDto> createMovieService,
			ILogger<CreateMovieServiceDecorator> logger)
		{
			_createMovieService = createMovieService;
			_logger = logger;
		}

		public async Task<Result<MovieDto>> CallAsync(CreateMovieParameter parameter)
		{
			try
			{
				_logger.LogInformation("Calling service with {@Parameter}", parameter);

				return await _createMovieService.CallAsync(parameter);
			}
			catch (ArgumentNullException ex)
			{
				_logger.LogError(
					ex,
					"Null argument provided for CreateMovieService." + ex.ParamName
				);

				return new Result<MovieDto>(
					400,
					null,
					new List<string> { "Invalid request: " + ex.Message}
				);
			}
			catch (DataValidationException ex)
			{
				_logger.LogError(
					ex,
					"Data validation error occurred while creating a movie."
				);

				return new Result<MovieDto>(
					400,
					null,
					new List<string> { ex.Message }
				);
			}
			catch (RecordAlreadyExistsException ex)
			{
				_logger.LogError(
					ex,
					"Data validation error occurred while creating a movie. " +
					"Title in LT: {TitleInLT}, Title in Original Language: {TitleInOriginalLanguage}",
					parameter.Movie.TitleInLT,
					parameter.Movie.TitleInOriginalLanguage
				);

				return new Result<MovieDto>(
					409,
					null,
					new List<string> { ex.Message }
				);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while creating a movie record.");

				return new Result<MovieDto>(
					500,
					null,
					new List<string>() { "An error occurred. Please contact the administrator." }
				);
			}
			finally
			{
				_logger.LogInformation("Calling CreateMovieService ended.");
			}
		}
	}
}