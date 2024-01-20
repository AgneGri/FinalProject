using CinemaApi.Models;
using CinemaApi.Models.Screenings.DTOs;
using CinemaApi.Services.Screenings.Converters;
using DataAccess;
using CinemaApi.Models.Screenings.Parameters;
using CinemaApi.Models.Screenings.Validators;

namespace CinemaApi.Services.Screenings.Services
{
	public class UpdateScreeningService : IService<UpdateScreeningParameter, ScreeningDto>
	{
		private readonly IScreeningRepository _screeningsRepository;
		private readonly ScreeningConverter _screeningConverter;
		private readonly ScreeningDtoConverter _screeningDtoConverter;
		private readonly UpdateScreeningParameterValidator _updateScreeningParameterValidator;

		public UpdateScreeningService(
			IScreeningRepository screeningsRepository, 
			ScreeningConverter screeningConverter, 
			ScreeningDtoConverter screeningDtoConverter,
			UpdateScreeningParameterValidator updateScreeningParameterValidator)
		{
			_screeningsRepository = screeningsRepository;
			_screeningConverter = screeningConverter;
			_screeningDtoConverter = screeningDtoConverter;
			_updateScreeningParameterValidator = updateScreeningParameterValidator;
		}

		public async Task<Result<ScreeningDto>> CallAsync(UpdateScreeningParameter parameter)
		{
			if (parameter == null)
			{
				throw new ArgumentNullException(nameof(parameter));
			}

			_updateScreeningParameterValidator.ValidateParameters(parameter.Id, parameter.Screening);

			var screeningRecordExistsById = await _screeningsRepository
				.DoesScreeningRecordExistByIdAsync(parameter.Screening.Id);


			if (!screeningRecordExistsById)
			{
				return new Result<ScreeningDto>(
					404,
					null,
					new List<string> { "The screening record with the specified Id is not found." }
				);
			}

			var screeningToUpdate = await _screeningsRepository.GetAsync(parameter.Screening.Id);
			var updatedScreening = _screeningConverter.Update(parameter.Screening, screeningToUpdate);
			var updatedEntity = await _screeningsRepository.UpdateAsync(updatedScreening);
			var detailedUpdatedScreening = await _screeningsRepository.GetScreeningWithDetailsAsync(updatedEntity.Id);
			var screeningDto = _screeningDtoConverter.Convert(detailedUpdatedScreening);

			return new Result<ScreeningDto>(200, screeningDto);
		}
	}
}