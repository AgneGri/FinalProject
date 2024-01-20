using CinemaApi.Models;
using CinemaApi.Models.Cinemas.Parameters;
using CinemaApi.Models.Cinemas.DTOs;
using CinemaApi.Exceptions;

namespace CinemaApi.Services.Cinemas.Decorators
{
	public class CreateCinemaServiceDecorator : IService<CreateCinemaParameter, CinemaDto>
	{
		private readonly IService<CreateCinemaParameter, CinemaDto> _createCinemaService;
		private readonly ILogger<CreateCinemaServiceDecorator> _logger;

		public CreateCinemaServiceDecorator(
			IService<CreateCinemaParameter, CinemaDto> createCinemaService, 
			ILogger<CreateCinemaServiceDecorator> logger)
		{
			_createCinemaService = createCinemaService;
			_logger = logger;
		}

		public async Task<Result<CinemaDto>> CallAsync(CreateCinemaParameter parameter)
		{
			try
			{
				_logger.LogInformation("Calling service with {@Parameter}", parameter);

				return await _createCinemaService.CallAsync(parameter);
			}
			catch (ArgumentNullException ex)
			{
				_logger.LogError(
					ex,
					"Null argument provided for CreateCinemaService." + ex.ParamName
				);

				return new Result<CinemaDto>(
					400,
					null,
					new List<string> { "Invalid request: " + ex.Message }
				);
			}
			catch (DataValidationException ex)
			{
				_logger.LogError(
					ex,
					"Data validation error occurred while creating a cinema record."
				);

				return new Result<CinemaDto>(
					400,
					null,
					new List<string> { ex.Message }
				);
			}
			catch (RecordAlreadyExistsException ex)
			{
				_logger.LogError(
					ex,
					"Data validation error occurred while creating a cinema record. " +
					"Auditorium: {Auditorium}",
					parameter.Cinema.Auditorium
				);

				return new Result<CinemaDto>(
					409,
					null,
					new List<string> { ex.Message }
				);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while creating a cinema record");

				return new Result<CinemaDto>(
					500,
					null,
					new List<string>() { "An error occurred. Please contact the administrator." }
				);
			}
			finally
			{
				_logger.LogInformation("Calling CreateCinemaService ended.");
			}
		}
	}
}