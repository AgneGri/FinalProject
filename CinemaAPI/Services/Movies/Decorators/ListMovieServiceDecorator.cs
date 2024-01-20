using CinemaApi.Exceptions;
using CinemaApi.Models;
using CinemaApi.Models.Movies.DTOs;
using CinemaApi.Models.Movies.Parameters;
using System.Text.Json;

namespace CinemaApi.Services.Movies.Decorators
{
	public class ListMovieServiceDecorator : IService<ListMovieParameter, List<MovieDto>>
	{
		private readonly IService<ListMovieParameter, List<MovieDto>> _listMovieService;
		private readonly ILogger<ListMovieServiceDecorator> _logger;

		public ListMovieServiceDecorator(
			IService<ListMovieParameter, List<MovieDto>> listMovieService,
			ILogger<ListMovieServiceDecorator> logger)
		{
			_listMovieService = listMovieService;
			_logger = logger;
		}

		public async Task<Result<List<MovieDto>>> CallAsync(ListMovieParameter parameter)
		{
			try
			{
				_logger.LogInformation("Calling service with {@Parameter}", parameter);

				return await _listMovieService.CallAsync(parameter);
			}
			catch (ArgumentNullException ex)
			{
				_logger.LogError(
					ex,
					"Null argument provided for ListMovieService. Parameter required for fetching movies is missing."
				);

				return new Result<List<MovieDto>>(
					400,
					null,
					new List<string> { "Invalid request: Parameter for fetching movies is required." }
				);
			}
			catch (DataValidationException ex)
			{
				_logger.LogError(
					ex,
					"Validation error occurred with provided limit {Limit}",
					parameter?.Limit
				);

				return new Result<List<MovieDto>>(
					400,
					null,
					new List<string> { ex.Message }
				);
			}
			catch (NotFoundException ex)
			{
				_logger.LogError(
					ex,
					"No movies found matching the provided search criteria. Criteria: {Criteria}",
					 JsonSerializer.Serialize(parameter)
				 );

				return new Result<List<MovieDto>>(
					404,
					null,
					new List<string> { ex.Message }
				);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while listing the movies");

				return new Result<List<MovieDto>>(
					500,
					null,
					new List<string>() { "An error occurred. Please contact the system administrator." }
				);
			}
			finally
			{
				_logger.LogInformation("Calling ListMovieService ended.");
			}
		}
	}
}