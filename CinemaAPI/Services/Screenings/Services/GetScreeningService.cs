using CinemaApi.Exceptions;
using CinemaApi.Models;
using CinemaApi.Models.Screenings.DTOs;
using CinemaApi.Models.Screenings.Parameters;
using CinemaApi.Services.Screenings.Converters;
using DataAccess;

namespace CinemaApi.Services.Screenings.Services
{
	public class GetScreeningService : IService<GetScreeningParameter, ScreeningDto>
	{
		private readonly IScreeningRepository _screeningsRepository;
		private readonly ScreeningDtoConverter _screeningDtoConverter;

		public GetScreeningService(
			IScreeningRepository screeningsRepository, 
			ScreeningDtoConverter screeningDtoConverter)
		{
			_screeningsRepository = screeningsRepository;
			_screeningDtoConverter = screeningDtoConverter;
		}

		public async Task<Result<ScreeningDto>> CallAsync(GetScreeningParameter parameter)
		{
			if (parameter == null)
			{
				throw new ArgumentNullException(nameof(parameter));
			}

			var screeningRecordExistById = await _screeningsRepository.DoesScreeningRecordExistByIdAsync(parameter.Id);

			if (!screeningRecordExistById)
			{
				throw new NotFoundException($"The screening record with the specified Id is not found in the database. " +
					$"Please check the Id and try again.");
			}

			var result = await _screeningsRepository.GetScreeningWithDetailsAsync(parameter.Id);
			var screeningDto = _screeningDtoConverter.Convert(result);

			return new Result<ScreeningDto>(200, screeningDto);
		}
	}
}