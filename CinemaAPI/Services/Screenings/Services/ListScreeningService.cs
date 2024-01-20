using DataAccess;
using CinemaApi.Models.Screenings.Parameters;
using CinemaApi.Models.Screenings.DTOs;
using CinemaApi.Services.Screenings.Converters;
using CinemaApi.Models;

namespace CinemaApi.Services.Screenings.Services
{
	public class ListScreeningService : IService<ListScreeningParameter, List<ScreeningDto>>
	{
		private readonly IScreeningRepository _screeningsRepository;
		private readonly ScreeningDtoConverter _screeningDtoConverter;

		public ListScreeningService(
			IScreeningRepository screeningsRepository,
			ScreeningDtoConverter screeningDtoConverter)
		{
			_screeningsRepository = screeningsRepository;
			_screeningDtoConverter = screeningDtoConverter;
		}

		public async Task<Result<List<ScreeningDto>>> CallAsync(ListScreeningParameter parameter)
		{
			if (parameter == null)
			{
				throw new ArgumentNullException(nameof(parameter));
			}

			var screenings = await _screeningsRepository.ListScreeningsWithDetailsAsync();

			if (parameter.Limit > 0)
			{
				screenings = screenings.Take(parameter.Limit.Value).ToList();
			}

			var screeningDtos = screenings.Select(_screeningDtoConverter.Convert).ToList();

			return new Result<List<ScreeningDto>>(200, screeningDtos);
		}
	}
}