using CinemaApi.Exceptions;
using CinemaApi.Models;
using CinemaApi.Models.Cinemas.DTOs;
using CinemaApi.Models.Cinemas.Parameters;
using CinemaApi.Services.Cinemas.Converters;
using DataAccess;

namespace CinemaApi.Services.Cinemas.Services
{
	public class GetCinemaService : IService<GetCinemaParameter, CinemaDto>
	{
		private readonly ICinemaRepository _cinemasRepository;
		private readonly CinemaDtoConverter _cinemaDtoConverter;

		public GetCinemaService(
			ICinemaRepository cinemasRepository, 
			CinemaDtoConverter cinemaDtoConverter)
		{
			_cinemasRepository = cinemasRepository;
			_cinemaDtoConverter = cinemaDtoConverter;
		}

		public async Task<Result<CinemaDto>> CallAsync(GetCinemaParameter parameter)
		{
			if (parameter == null)
			{
				throw new ArgumentNullException(nameof(parameter));
			}

			var cinemaRecordExistById = await _cinemasRepository
				.DoesCinemaRecordExistByIdAsync(parameter.Id);

			if (!cinemaRecordExistById)
			{
				throw new NotFoundException("The cinema record with the specified Id is not " +
					"found in the database. Please check the Id and try again.");
			}

			var result = await _cinemasRepository.GetAsync(parameter.Id);
			var cinemaDto = _cinemaDtoConverter.Convert(result);

			return new Result<CinemaDto>(200, cinemaDto);
		}
	}
}