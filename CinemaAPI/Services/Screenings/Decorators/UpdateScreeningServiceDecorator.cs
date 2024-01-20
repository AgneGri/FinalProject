using CinemaApi.Exceptions;
using CinemaApi.Models;
using CinemaApi.Models.Screenings.DTOs;
using CinemaApi.Models.Screenings.Parameters;

namespace CinemaApi.Services.Screenings.Decorators
{
	public class UpdateScreeningServiceDecorator : IService<UpdateScreeningParameter, ScreeningDto>
	{
		private readonly IService<UpdateScreeningParameter, ScreeningDto> _updateScreeningService;
		private readonly ILogger<UpdateScreeningServiceDecorator> _logger;

		public UpdateScreeningServiceDecorator(
			IService<UpdateScreeningParameter, ScreeningDto> updateScreeningService,
			ILogger<UpdateScreeningServiceDecorator> logger)
		{
			_updateScreeningService = updateScreeningService;
			_logger = logger;
		}

		public async Task<Result<ScreeningDto>> CallAsync(UpdateScreeningParameter parameter)
		{
			try
			{
				_logger.LogInformation("Calling service with {@Parameter}", parameter);

				return await _updateScreeningService.CallAsync(parameter);
			}
			catch (ArgumentNullException ex)
			{
				_logger.LogError(
					ex,
					"Null argument provided for UpdateScreeningService." + ex.ParamName
				);

				return new Result<ScreeningDto>(
					400,
					null,
					new List<string> { "Invalid request: " + ex.ParamName }
				);
			}
			catch (DataValidationException ex)
			{
				_logger.LogError(ex, "Validation error occurred with provided Id {ScreeningId}.");

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
					"An Error occurred while updating the screening record with a provided Id",
					parameter.Screening?.Id
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
				_logger.LogInformation("Calling UpdateScreeningService ended.");
			}
		}
	}
}