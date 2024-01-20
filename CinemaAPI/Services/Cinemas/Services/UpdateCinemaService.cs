using CinemaApi.Models;
using DataAccess;
using CinemaApi.Models.Cinemas.Parameters;
using CinemaApi.Models.Cinemas.DTOs;
using CinemaApi.Services.Cinemas.Converters;
using CinemaApi.Models.Cinemas.Validators;
using CinemaApi.Exceptions;

namespace CinemaApi.Services.Cinemas.Services
{
	public class UpdateCinemaService : IService <UpdateCinemaParameter, CinemaDto>
	{
		private readonly ICinemaRepository _cinemasRepository;
		private readonly CinemaConverter _cinemaConverter;
		private readonly CinemaDtoConverter _cinemaDtoConverter;
		private readonly UpdateCinemaParameterValidator _updateCinemaParameterValidator;

		public UpdateCinemaService(
			ICinemaRepository cinemasRepository,
			CinemaConverter cinemaConverter, 
			CinemaDtoConverter cinemaDtoConverter,
			UpdateCinemaParameterValidator updateCinemaParameterValidator)
		{
			_cinemasRepository = cinemasRepository;
			_cinemaConverter = cinemaConverter;
			_cinemaDtoConverter = cinemaDtoConverter;
			_updateCinemaParameterValidator = updateCinemaParameterValidator;
		}

		public async Task<Result<CinemaDto>> CallAsync(UpdateCinemaParameter parameter)
		{
			if (parameter == null)
			{
				throw new ArgumentNullException(nameof(parameter));
			}

			_updateCinemaParameterValidator.ValidateParameters(parameter.Id, parameter.Cinema);

			var cinemaRecordExistById = await _cinemasRepository
				.DoesCinemaRecordExistByIdAsync(parameter.Id);

			if (!cinemaRecordExistById)
			{
				throw new NotFoundException("The cinema record with the specified Id is not found.");
			}

			var cinemaToUpdate = await _cinemasRepository.GetAsync(parameter.Cinema.Id);
			var updatedCinema = _cinemaConverter.Update(parameter.Cinema, cinemaToUpdate);
			var updatedEntity = await _cinemasRepository.UpdateAsync(updatedCinema);
			var cinemaDto = _cinemaDtoConverter.Convert(updatedEntity);

			return new Result<CinemaDto>(200, cinemaDto);
		}
	}
}