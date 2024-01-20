using CinemaApi.Models;
using DataAccess;
using CinemaApi.Models.Cinemas.DTOs;
using CinemaApi.Models.Cinemas.Parameters;
using CinemaApi.Services.Cinemas.Converters;
using CinemaApi.Models.Cinemas.Validators;
using CinemaApi.Exceptions;

namespace CinemaApi.Services.Cinemas.Services
{
	public class CreateCinemaService : IService<CreateCinemaParameter, CinemaDto>
	{
		private readonly ICinemaRepository _cinemasRepository;
		private readonly CinemaConverter _cinemaConverter;
		private readonly CinemaDtoConverter _cinemaDtoConverter;
		private readonly CreateCinemaParameterValidator _createCinemaParameterValidator;

		public CreateCinemaService(
			ICinemaRepository cinemasRepository, 
			CinemaConverter cinemaConverter, 
			CinemaDtoConverter cinemaDtoConverter,
			CreateCinemaParameterValidator createCinemaParameterValidator)
		{
			_cinemasRepository = cinemasRepository;
			_cinemaConverter = cinemaConverter;
			_cinemaDtoConverter = cinemaDtoConverter;
			_createCinemaParameterValidator = createCinemaParameterValidator;
		}

		public async Task<Result<CinemaDto>> CallAsync(CreateCinemaParameter parameter)
		{
			if (parameter == null)
			{
				throw new ArgumentNullException(nameof(parameter));
			}

			_createCinemaParameterValidator.ValidateParameters(parameter.Cinema);

			var cinemaRecordExists = await _cinemasRepository
				.DoesCinemaRecordExistbyAuditoriumAsync(parameter.Cinema.Auditorium);

			if (cinemaRecordExists)
			{
				throw new RecordAlreadyExistsException("A cinema record with the same auditorium name already exists.");
			}

			var screeningEntity = _cinemaConverter.Create(parameter.Cinema);
			var createdScreening = await _cinemasRepository.CreateAsync(screeningEntity);
			var screeningDto = _cinemaDtoConverter.Convert(createdScreening);

			return new Result<CinemaDto>(200, screeningDto);
		}
	}
}