using CinemaApi.Exceptions;
using CinemaApi.Models;
using CinemaApi.Models.Cinemas.DTOs;
using CinemaApi.Models.Cinemas.Parameters;

namespace CinemaApi.Services.Cinemas.Decorators
{
	public class DeleteCinemaServiceDecorator : IService<DeleteCinemaParameter, CinemaDto>
	{
		private readonly IService<DeleteCinemaParameter, CinemaDto> _deleteCinemaService;
		private readonly ILogger<DeleteCinemaServiceDecorator> _logger;

		public DeleteCinemaServiceDecorator(
			IService<DeleteCinemaParameter, CinemaDto> deleteCinemaService,
			ILogger<DeleteCinemaServiceDecorator> logger)
		{
			_deleteCinemaService = deleteCinemaService;
			_logger = logger;
		}

		public async Task<Result<CinemaDto>> CallAsync(DeleteCinemaParameter parameter)
		{
			try
			{
				_logger.LogInformation("Calling service with {@Parameter}", parameter);

				return await _deleteCinemaService.CallAsync(parameter);
			}
			catch (ArgumentNullException ex)
			{
				_logger.LogError(
					ex,
					"Null argument provided for DeleteCinemaService."
				);

				return new Result<CinemaDto>(
					400,
					null,
					new List<string> { "Invalid request: cinema record Id is required." }
				);
			}
			catch (DataValidationException ex)
			{
				_logger.LogError(
					ex,
					"Data validation error occurred while deleting a cinema record."
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
					"Error occurred while deleting the cinema record with provided cinema record Id {CinemaId}",
					parameter?.Id
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
				_logger.LogInformation("Calling DeleteCinemaService ended.");
			}
		}
	}
}