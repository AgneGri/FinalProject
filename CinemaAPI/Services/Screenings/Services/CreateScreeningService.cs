using CinemaApi.Models;
using DataAccess;
using CinemaApi.Services.Screenings.Converters;
using CinemaApi.Models.Screenings.DTOs;
using CinemaApi.Models.Screenings.Parameters;
using CinemaApi.Models.Screenings.Validators;

namespace CinemaApi.Services.Screenings.Services
{
	public class CreateScreeningService : IService<CreateScreeningParameter, ScreeningDto>
	{
		private readonly IScreeningRepository _screeningsRepository;
		private readonly ScreeningConverter _screeningConverter;
		private readonly ScreeningDtoConverter _screeningDtoConverter;
		private readonly CreateScreeningParameterValidator _createScreeningParameterValidator;

		public CreateScreeningService(
			IScreeningRepository screeningsRepository,
			ScreeningConverter screeningConverter, 
			ScreeningDtoConverter screeningDtoConverter,
			CreateScreeningParameterValidator createScreeningParameterValidator)
		{
			_screeningsRepository = screeningsRepository;
			_screeningConverter = screeningConverter;
			_screeningDtoConverter = screeningDtoConverter;
			_createScreeningParameterValidator = createScreeningParameterValidator;
		}

		public async Task<Result<ScreeningDto>> CallAsync(CreateScreeningParameter parameter)
		{
			if (parameter == null)
			{
				throw new ArgumentNullException(nameof(parameter));
			}

			_createScreeningParameterValidator.ValidateParameters(parameter.Screening);

			var screeningEntity = _screeningConverter.Create(parameter.Screening);
			var createdScreening = await _screeningsRepository.CreateAsync(screeningEntity);
			var detailedScreening = await _screeningsRepository.GetScreeningWithDetailsAsync(createdScreening.Id);
			var screeningDto = _screeningDtoConverter.Convert(detailedScreening);

			return new Result<ScreeningDto>(200, screeningDto);
		}
	}
}