using CinemaApi.Exceptions;
using CinemaApi.Models;
using CinemaApi.Models.Movies.DTOs;
using CinemaApi.Models.Movies.Parameters;

namespace CinemaApi.Services.Movies.Decorators
{
	public class UpdateMovieServiceDecorator : IService<UpdateMovieParameter, MovieDto>
	{
		private readonly IService<UpdateMovieParameter, MovieDto> _updateMovieService;
		private readonly ILogger<UpdateMovieServiceDecorator> _logger;

		public UpdateMovieServiceDecorator(
			IService<UpdateMovieParameter, MovieDto> updateMovieService,
			ILogger<UpdateMovieServiceDecorator> logger)
		{
			_updateMovieService = updateMovieService;
			_logger = logger;
		}

		public async Task<Result<MovieDto>> CallAsync(UpdateMovieParameter parameter)
		{
			try
			{
				_logger.LogInformation("Calling service with {@Parameter}", parameter);

				return await _updateMovieService.CallAsync(parameter);
			}
			catch (ArgumentNullException ex)
			{
				_logger.LogError(
					ex,
					"Null argument provided for ListMovieService: " + ex.ParamName
				);

				return new Result<MovieDto>(
					400,
					null,
					new List<string> { "Invalid request." + ex.ParamName}
				);
			}
			catch (DataValidationException ex)
			{
				_logger.LogError(ex, "Validation error occurred with provided Id {MovieId}.");
				
				return new Result<MovieDto>(
						400,
						null,
						new List<string>() { ex.Message }
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
			catch (BadRequestException ex)
			{
				_logger.LogError(
					  ex,
					 "Invalid operation attempted on movie due to release year. Movie Id: {MovieId}, Release Year: {ReleaseYear}",
					parameter.Id,
					parameter.Movie.ReleaseYear
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
					"An Error occurred while updating a movie record with a provided Id: {MovieId}",
					parameter.Movie?.Id
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
				_logger.LogInformation("Calling UpdateMovieService ended.");
			}
		}
	}
}