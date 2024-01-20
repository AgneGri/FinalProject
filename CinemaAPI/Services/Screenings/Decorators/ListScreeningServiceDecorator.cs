using CinemaApi.Exceptions;
using CinemaApi.Models;
using CinemaApi.Models.Screenings.DTOs;
using CinemaApi.Models.Screenings.Parameters;

namespace CinemaApi.Services.Screenings.Decorators
{
	public class ListScreeningServiceDecorator : IService<ListScreeningParameter, List<ScreeningDto>>
	{
		private readonly IService<ListScreeningParameter, List<ScreeningDto>> _listScreeningService;
		private readonly ILogger<ListScreeningServiceDecorator> _logger;

		public ListScreeningServiceDecorator(
			IService<ListScreeningParameter, List<ScreeningDto>> listScreeningService, 
			ILogger<ListScreeningServiceDecorator> logger)
		{
			_listScreeningService = listScreeningService;
			_logger = logger;
		}

		public async Task<Result<List<ScreeningDto>>> CallAsync(ListScreeningParameter parameter)
		{
			try
			{
				_logger.LogInformation("Calling service with {@Parameter}", parameter);

				return await _listScreeningService.CallAsync(parameter);
			}
			catch (ArgumentNullException ex)
			{
				_logger.LogError(
					ex,
					"Null argument provided for ListScreeningService." + ex.ParamName
				);

				return new Result<List<ScreeningDto>>(
					400,
					null,
					new List<string>
					{
						"Invalid request: Parameter for fetching screening records is required."
					}
				);
			}
			catch (DataValidationException ex)
			{
				_logger.LogError(
					ex, 
					"Validation error occurred with provided limit {Limit}",
					parameter?.Limit
				);

				return new Result<List<ScreeningDto>>(
					400,
					null,
					new List<string>() { ex.Message }
				);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while listing the screening records.");

				return new Result<List<ScreeningDto>>(
					500,
					null,
					new List<string>() { "An error occurred. Please contact the system administrator." }
				);
			}
			finally
			{
				_logger.LogInformation("Calling ListScreeningService ended.");
			}
		}
	}
}