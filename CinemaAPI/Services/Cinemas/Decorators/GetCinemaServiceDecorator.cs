using CinemaApi.Models;
using CinemaApi.Models.Cinemas.Parameters;
using CinemaApi.Models.Cinemas.DTOs;
using CinemaApi.Exceptions;

namespace CinemaApi.Services.Cinemas.Decorators
{
	public class GetCinemaServiceDecorator : IService<GetCinemaParameter, CinemaDto>
	{
		private readonly IService<GetCinemaParameter, CinemaDto> _getCinemaService;
		private readonly ILogger<GetCinemaServiceDecorator> _logger;

		public GetCinemaServiceDecorator(
			IService<GetCinemaParameter, CinemaDto> getCinemaService,
			ILogger<GetCinemaServiceDecorator> logger)
		{
			_getCinemaService = getCinemaService;
			_logger = logger;
		}

		public async Task<Result<CinemaDto>> CallAsync(GetCinemaParameter parameter)
		{
			try
			{
				_logger.LogInformation("Calling service with {@Parameter}", parameter);

				return await _getCinemaService.CallAsync(parameter);
			}
			catch (ArgumentNullException ex)
			{
				_logger.LogError(
					ex,
					"Null argument provided for GetCinemaService. Parameter required for " +
					"fetching a cinema record is missing."
				);

				return new Result<CinemaDto>(
					400,
					null,
					new List<string> { "Parameter for fetching the cinema record is required." }
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
					"An Error occurred while searching for cinema with a provided Id: {CinemaId}",
					parameter.Id
				);

				return new Result<CinemaDto>(
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
				_logger.LogInformation("Calling GetCinemaService ended.");
			}
		}
	}
}