using CinemaApi.Models.Screenings.DTOs;
using CinemaApi.Models.Screenings.Parameters;
using CinemaApi.Models;
using CinemaApi.Services.Screenings.Converters;
using DataAccess;
using CinemaApi.Exceptions;

namespace CinemaApi.Services.Screenings.Services
{
	public class ListScreeningWithVariousParametersService : IService<ListScreeningParameter, List<ScreeningShowingsDto>>
	{
		private readonly IScreeningRepository _screeningsRepository;
		private readonly ScreeningShowingsDtoConverter _screeningShowingsDtoConverter;

		public ListScreeningWithVariousParametersService(
			IScreeningRepository screeningsRepository,
			ScreeningShowingsDtoConverter screeningShowingsDtoConverter)
		{
			_screeningsRepository = screeningsRepository;
			_screeningShowingsDtoConverter = screeningShowingsDtoConverter;
		}

		public async Task<Result<List<ScreeningShowingsDto>>> CallAsync(ListScreeningParameter parameter)
		{
			if (parameter == null)
			{
				throw new ArgumentNullException(nameof(parameter));
			}

			var screenings = await _screeningsRepository.ListScreeningsWithDetailsAsync();

			if (!string.IsNullOrEmpty(parameter.City))
			{
				screenings = screenings
					.Where(s => s.Cinema.City.ToLower() == parameter.City.ToLower())
					.ToList();
			}

			if (parameter.ShowDate.HasValue)
			{
				screenings = screenings
					.Where(s => s.ShowDate.Date == parameter.ShowDate.Value.Date)
					.ToList();
			}

			if (!string.IsNullOrEmpty(parameter.Auditorium))
			{
				screenings = screenings
					.Where(s => s.Cinema.Auditorium.ToLower() == parameter.Auditorium.ToLower())
					.ToList();
			}

			if (!string.IsNullOrEmpty(parameter.Genre))
			{
				screenings = screenings
					.Where(s => s.Movie.Genre.ToLower().Contains(parameter.Genre.ToLower()))
					.ToList();
			}

			if (!screenings.Any())
			{
				throw new NotFoundException($"No match found for the provided search criteria.");
			}

			var screeningShowingsDtos = screenings.Select(_screeningShowingsDtoConverter.Convert).ToList();

			return new Result<List<ScreeningShowingsDto>>(200, screeningShowingsDtos);
		}
	}
}