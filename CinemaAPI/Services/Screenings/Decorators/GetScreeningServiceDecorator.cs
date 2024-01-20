using CinemaApi.Models;
using CinemaApi.Models.Screenings.Parameters;
using CinemaApi.Models.Screenings.DTOs;
using CinemaApi.Exceptions;

namespace CinemaApi.Services.Screenings.Decorators
{
	public class GetScreeningServiceDecorator : IService<GetScreeningParameter, ScreeningDto>
	{
		private readonly IService<GetScreeningParameter, ScreeningDto> _getScreeningService;
		private readonly ILogger<GetScreeningServiceDecorator> _logger;

		public GetScreeningServiceDecorator(
			IService<GetScreeningParameter, ScreeningDto> getScreeningService,
			ILogger<GetScreeningServiceDecorator> logger)
		{
			_getScreeningService = getScreeningService;
			_logger = logger;
		}

		public async Task<Result<ScreeningDto>> CallAsync(GetScreeningParameter parameter)
		{
			try
			{
				_logger.LogInformation("Calling service with {@Parameter}", parameter);

				return await _getScreeningService.CallAsync(parameter);
			}
			catch (ArgumentNullException ex)
			{
				_logger.LogError(
					ex,
					"Null argument provided for GetScreeningService." + ex.ParamName
				);

				return new Result<ScreeningDto>(
					400,
					null,
					new List<string> 
					{ 
						"Invalid request: Parameter for fetching screening record is required." 
					}
				);
			}
			catch (DataValidationException ex)
			{
				_logger.LogError(ex, "Validation error occurred with provided Id {ScreeningId}");

				return new Result<ScreeningDto>(
					400,
					null,
					new List<string>() { ex.Message }
				);
			}
			catch (NotFoundException ex)
			{
				_logger.LogError(
					ex,
					"Screening record not found. Requested Screening Id: {ScreeningId}",
					parameter?.Id
				);

				return new Result<ScreeningDto>(
					404,
					null,
					new List<string> { ex.Message }
				);
			}
			catch (Exception ex)
			{
				_logger.LogError(
					ex,
					"An Error occurred while searching for the screening record with a provided Id",
					parameter.Id
				);

				return new Result<ScreeningDto>(
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
				_logger.LogInformation("Calling GetScreeningService ended.");
			}
		}
	}
}