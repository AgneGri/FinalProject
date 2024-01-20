using CinemaApi.Exceptions;
using CinemaApi.Models;
using CinemaApi.Models.Cinemas.Parameters;
using CinemaApi.Models.Cinemas.DTOs;

namespace CinemaApi.Services.Cinemas.Decorators
{
	public class UpdateCinemaServiceDecorator : IService<UpdateCinemaParameter, CinemaDto>
	{
		private readonly IService<UpdateCinemaParameter, CinemaDto> _updateCinemaService;
		private readonly ILogger<UpdateCinemaServiceDecorator> _logger;

		public UpdateCinemaServiceDecorator(
			IService<UpdateCinemaParameter, CinemaDto> updateCinemaService,
			ILogger<UpdateCinemaServiceDecorator> logger)
		{
			_updateCinemaService = updateCinemaService;
			_logger = logger;
		}

		public async Task<Result<CinemaDto>> CallAsync(UpdateCinemaParameter parameter)
		{
			try
			{
				_logger.LogInformation("Calling service with {@Parameter}", parameter);

				return await _updateCinemaService.CallAsync(parameter);
			}
			catch (ArgumentNullException ex)
			{
				_logger.LogError(
					ex,
					"Null argument provided for UpdateCinemaService." + ex.ParamName
				);

				return new Result<CinemaDto>(
					400,
					null,
					new List<string> { "Invalid request." + ex.ParamName}
				);
			}
			catch (DataValidationException ex)
			{
				_logger.LogError(
					ex,
					"Validation error occurred with provided Id {CinemaId}",
					parameter?.Id
				);

				return new Result<CinemaDto>(
					400,
					null,
					new List<string> { ex.Message }
				);
			}
			catch (NotFoundException ex)
			{
				_logger.LogError(
					ex,
					"Cinema record not found. Requested cinema Id: {CinemaId}",
					parameter?.Id
				);

				return new Result<CinemaDto>(
					404,
					null,
					new List<string> { ex.Message }
				);
			}
			catch (Exception ex)
			{
				_logger.LogError(
					ex,
					"An Error occurred while updating cinema record with a provided Id"
				);

				return new Result<CinemaDto>(
					500,
					null,
					new List<string>
					{
						"An Error occurred. Please contact the system administrator."
					}
				);
			}
			finally
			{
				_logger.LogInformation("Calling UpdateCinemaService ended.");
			}
		}
	}
}