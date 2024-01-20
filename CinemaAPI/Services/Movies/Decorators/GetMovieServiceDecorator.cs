using CinemaApi.Exceptions;
using CinemaApi.Models;
using CinemaApi.Models.Movies.DTOs;
using CinemaApi.Models.Movies.Parameters;

namespace CinemaApi.Services.Movies.Decorators
{
	public class GetMovieServiceDecorator : IService<GetMovieParameter, MovieDto>
	{
		private readonly IService<GetMovieParameter, MovieDto> _getMovieService;
		private readonly ILogger<GetMovieServiceDecorator> _logger;

		public GetMovieServiceDecorator(
			IService<GetMovieParameter, MovieDto> getMovieService,
			ILogger<GetMovieServiceDecorator> logger)
		{
			_getMovieService = getMovieService;
			_logger = logger;
		}

		public async Task<Result<MovieDto>> CallAsync(GetMovieParameter parameter)
		{
			try
			{
				_logger.LogInformation("Calling service with {@Parameter}", parameter);

				return await _getMovieService.CallAsync(parameter);
			}
			catch (ArgumentNullException ex)
			{
				_logger.LogError(
					ex,
					"Null argument provided for GetMovieService. Parameter required " +
					"for fetching a movie is missing."
				);

				return new Result<MovieDto>(
					400,
					null,
					new List<string> { "Invalid request: Parameter for fetching the " +
					"movie is required." }
				);
			}
			catch (DataValidationException ex)
			{
				_logger.LogError(
					ex,
					"Validation error occurred with provided Id {MovieId}",
					parameter?.Id
				);

				return new Result<MovieDto>(
					400,
					null,
					new List<string> { ex.Message }
				);
			}
			catch (NotFoundException ex)
			{
				_logger.LogError(
					ex,
					"Movie not found. Requested Movie Id: {MovieId}",
					parameter?.Id
				);

				return new Result<MovieDto>(
					404,
					null,
					new List<string> { ex.Message }
				);
			}
			catch (Exception ex)
			{
				_logger.LogError(
					ex,
					"An Error occurred while searching for the movie record with a provided Id: {MovieId}",
					parameter.Id
				);

				return new Result<MovieDto>(
					500,
					null,
					new List<string>
					{
						"An Error occurred. Please contact the system administrator. "
					}
				);
			}
			finally
			{
				_logger.LogInformation("Calling GetMovieService ended.");
			}
		}
	}
}