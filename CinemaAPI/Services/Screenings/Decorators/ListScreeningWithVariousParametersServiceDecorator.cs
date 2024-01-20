using CinemaApi.Models.Screenings.DTOs;
using CinemaApi.Models.Screenings.Parameters;
using CinemaApi.Models;
using CinemaApi.Exceptions;

namespace CinemaApi.Services.Screenings.Decorators
{
	public class ListScreeningWithVariousParametersServiceDecorator : IService<ListScreeningParameter, List<ScreeningShowingsDto>>
	{
		private readonly IService<ListScreeningParameter, List<ScreeningShowingsDto>> _listScreeningShowingsService;
		private readonly ILogger<ListScreeningServiceDecorator> _logger;

		public ListScreeningWithVariousParametersServiceDecorator(
			IService<ListScreeningParameter, List<ScreeningShowingsDto>> listScreeningShowingsService, 
			ILogger<ListScreeningServiceDecorator> logger)
		{
			_listScreeningShowingsService = listScreeningShowingsService;
			_logger = logger;
		}

		public async Task<Result<List<ScreeningShowingsDto>>> CallAsync(ListScreeningParameter parameter)
		{
			try
			{
				_logger.LogInformation("Calling service with {@Parameter}", parameter);

				return await _listScreeningShowingsService.CallAsync(parameter);
			}
			catch (ArgumentNullException ex)
			{
				_logger.LogError(
					ex,
					"Null argument provided for ListScreeningWithVariousParametersService." 
					+ ex.ParamName
				);

				return new Result<List<ScreeningShowingsDto>>(
					400,
					null,
					new List<string>
					{
						"Invalid request: Parameters for fetching screening records are required."
					}
				);
			}
			catch (NotFoundException ex)
			{
				_logger.LogError(
					ex,
					"Screening record not found. "
					+ ex.Message
				);

				return new Result<List<ScreeningShowingsDto>>(
					404,
					null,
					new List<string> { ex.Message }
				);
			}
			catch (Exception exception)
			{
				_logger.LogError("Error occurred while listing the screening records", exception);

				return new Result<List<ScreeningShowingsDto>>(
					500,
					null,
					new List<string>() { "An error occurred. Please contact the system administrator." }
				);
			}
			finally
			{
				_logger.LogInformation("Calling ListScreeningWithVariousParametersService ended.");
			}
		}
	}
}