using CinemaApi.Exceptions;
using CinemaApi.Models;
using CinemaApi.Models.Screenings.DTOs;
using CinemaApi.Models.Screenings.Parameters;

namespace CinemaApi.Services.Screenings.Decorators
{
	public class CreateScreeningServiceDecorator : IService<CreateScreeningParameter, ScreeningDto>
	{
		private readonly IService<CreateScreeningParameter, ScreeningDto> _createScreeningService;
		private readonly ILogger<CreateScreeningServiceDecorator> _logger;

		public CreateScreeningServiceDecorator(
			IService<CreateScreeningParameter, ScreeningDto> createScreeningService, 
			ILogger<CreateScreeningServiceDecorator> logger)
		{
			_createScreeningService = createScreeningService;
			_logger = logger;
		}

		public async Task<Result<ScreeningDto>> CallAsync(CreateScreeningParameter parameter)
		{
			try
			{
				_logger.LogInformation("Calling service with {@Parameter}", parameter);

				return await _createScreeningService.CallAsync(parameter);
			}
			catch (ArgumentNullException ex)
			{
				_logger.LogError(
					ex,
					"Null argument provided for CreateScreeningService." + ex.ParamName
				);

				return new Result<ScreeningDto>(
					400,
					null,
					new List<string> { "Invalid request: " + ex.Message }
				);
			}
			catch (DataValidationException ex)
			{
				_logger.LogError(ex, "Error occurred while creating a screening record");
				
				return new Result<ScreeningDto>(
					400,
					null,
					new List<string>() { ex.Message }
				);
			}
			catch (Exception ex)
			{
				_logger.LogError("Error occurred while creating a screening record", ex);

				return new Result<ScreeningDto>(
					500,
					null,
					new List<string>() { "An error occurred. Please contact the administrator." }
				);
			}
			finally
			{
				_logger.LogInformation("Calling CreateScreeningService ended.");
			}
		}
	}
}