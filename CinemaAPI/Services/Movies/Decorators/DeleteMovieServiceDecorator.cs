using CinemaApi.Exceptions;
using CinemaApi.Models;
using CinemaApi.Models.Movies.DTOs;
using CinemaApi.Models.Movies.Parameters;

namespace CinemaApi.Services.Movies.Decorators
{
	public class DeleteMovieServiceDecorator : IService<DeleteMovieParameter, MovieDto>
	{
		private readonly IService<DeleteMovieParameter, MovieDto> _deleteMovieService;
		private readonly ILogger<DeleteMovieServiceDecorator> _logger;

		public DeleteMovieServiceDecorator(
			IService<DeleteMovieParameter, MovieDto> deleteMovieService,
			ILogger<DeleteMovieServiceDecorator> logger)
		{
			_deleteMovieService = deleteMovieService;
			_logger = logger;
		}

		public async Task<Result<MovieDto>> CallAsync(DeleteMovieParameter parameter)
		{
			try
			{
				_logger.LogInformation("Calling service with {@Parameter}", parameter);

				return await _deleteMovieService.CallAsync(parameter);
			}
			catch (ArgumentNullException ex)
			{
				_logger.LogError(
					ex,
					"Null argument provided for DeleteMovieService."
				);

				return new Result<MovieDto>(
					400,
					null,
					new List<string> { "Invalid request: Movie Id is required." }
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
			catch (BadRequestException ex)
			{
				_logger.LogError(
					  ex,
					"Attempt to delete a recently created movie. Movie Id: {MovieId}",
					parameter.Id
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
					"Error occurred while deleting a movie record with provided Id {MovieId}",
					parameter?.Id
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
				_logger.LogInformation("Calling DeleteMovieService ended.");
			}
		}
	}
}