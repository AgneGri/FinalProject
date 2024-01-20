using CinemaApi.Exceptions;
using CinemaApi.Models;
using CinemaApi.Models.Screenings.DTOs;
using CinemaApi.Models.Screenings.Parameters;
using DataAccess;

namespace CinemaApi.Services.Screenings.Services
{
	public class DeleteScreeningService : IService<DeleteScreeningParameter, ScreeningDto>
	{
		private readonly IScreeningRepository _screeningsRepository;

		public DeleteScreeningService(IScreeningRepository screeningsRepository)
		{
			_screeningsRepository = screeningsRepository;
		}

		public async Task<Result<ScreeningDto>> CallAsync(DeleteScreeningParameter parameter)
		{
			if (parameter == null)
			{
				throw new ArgumentNullException(nameof(parameter));
			}

			var screening = await _screeningsRepository.GetScreeningWithDetailsAsync(parameter.Id);

			if (screening == null)
			{
				throw new NotFoundException($"The screening record with the provided Id is not found. Please check the Id and try again.");
			}

			var result = await _screeningsRepository.DeleteAsync(parameter.Id);

			if (result)
			{
				return new Result<ScreeningDto>(200, new ScreeningDto());
			}

			throw new Exception("Deletion failed.");
		}
	}
}