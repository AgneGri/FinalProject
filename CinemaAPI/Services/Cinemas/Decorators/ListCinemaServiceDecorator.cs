using CinemaApi.Exceptions;
using CinemaApi.Models;
using CinemaApi.Models.Cinemas.DTOs;
using CinemaApi.Models.Cinemas.Parameters;

namespace CinemaApi.Services.Cinemas.Decorators
{
	public class ListCinemaServiceDecorator : IService<ListCinemaParameter, List<CinemaDto>>
	{
		private readonly IService<ListCinemaParameter, List<CinemaDto>> _listCinemaService;
		private readonly ILogger<ListCinemaServiceDecorator> _logger;

		public ListCinemaServiceDecorator(
			IService<ListCinemaParameter, List<CinemaDto>> listCinemaService,
			ILogger<ListCinemaServiceDecorator> logger)
		{
			_listCinemaService = listCinemaService;
			_logger = logger;
		}

		public async Task<Result<List<CinemaDto>>> CallAsync(ListCinemaParameter parameter)
		{
			try
			{
				_logger.LogInformation("Calling service with {@Parameter}", parameter);

				return await _listCinemaService.CallAsync(parameter);
			}
			catch (ArgumentNullException ex)
			{
				_logger.LogError(
					ex,
					"Null argument provided for ListCinemaService. Parameter required for " +
					"fetching a cinema record is missing."
				);

				return new Result<List<CinemaDto>>(
					400,
					null,
					new List<string> { "Parameter for fetching cinema records is required." }
				);
			}
			catch (DataValidationException ex)
			{
				_logger.LogError(
					ex,
					"Validation error occurred with provided limit {Limit}",
					parameter?.Limit
				);

				return new Result<List<CinemaDto>>(
					400,
					null,
					new List<string> { ex.Message }
				);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while listing the cinemas");

				return new Result<List<CinemaDto>>(
					500,
					null,
					new List<string>() { "An error occurred. Please contact the system administrator." }
				);
			}
			finally
			{
				_logger.LogInformation("Calling ListCinemaService ended.");
			}
		}
	}
}