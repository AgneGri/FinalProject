using CinemaApi.Exceptions;
using CinemaApi.Models;
using CinemaApi.Models.Screenings.DTOs;
using CinemaApi.Models.Screenings.Parameters;

namespace CinemaApi.Services.Screenings.Decorators
{
	public class DeleteScreeningServiceDecorator : IService<DeleteScreeningParameter, ScreeningDto>
	{
		private readonly IService<DeleteScreeningParameter, ScreeningDto> _deleteScreeningService;
		private readonly ILogger<DeleteScreeningServiceDecorator> _logger;

		public DeleteScreeningServiceDecorator(
			IService<DeleteScreeningParameter, ScreeningDto> deleteScreeningService, 
			ILogger<DeleteScreeningServiceDecorator> logger)
		{
			_deleteScreeningService = deleteScreeningService;
			_logger = logger;
		}

		public async Task<Result<ScreeningDto>> CallAsync(DeleteScreeningParameter parameter)
		{
			try
			{
				_logger.LogInformation("Calling service with {@Parameter}", parameter);

				return await _deleteScreeningService.CallAsync(parameter);
			}
			catch (ArgumentNullException ex)
			{
				_logger.LogError(
					ex,
					"Null argument provided for DeleteScreeningService." + ex.ParamName
				);

				return new Result<ScreeningDto>(
					400,
					null,
					new List<string> { "Invalid request: Screening record Id is required." }
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
					"Error occurred while deleting the screening record with provided Id {Id}",
					parameter?.Id
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
				_logger.LogInformation("Calling DeleteScreeningService ended.");
			}
		}
	}
}