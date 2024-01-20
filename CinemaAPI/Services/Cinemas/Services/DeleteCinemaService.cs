using CinemaApi.Models;
using DataAccess.Entities;
using DataAccess;
using CinemaApi.Models.Cinemas.Parameters;
using CinemaApi.Models.Cinemas.DTOs;
using CinemaApi.Exceptions;

namespace CinemaApi.Services.Cinemas.Services
{
	public class DeleteCinemaService : IService<DeleteCinemaParameter, CinemaDto>
	{
		private readonly IRepository<Cinema> _cinemasRepository;

		public DeleteCinemaService(IRepository<Cinema> cinemasRepository)
		{
			_cinemasRepository = cinemasRepository;
		}

		public async Task<Result<CinemaDto>> CallAsync(DeleteCinemaParameter parameter)
		{
			if (parameter == null)
			{
				throw new ArgumentNullException(nameof(parameter));
			}

			var cinema = await _cinemasRepository.GetAsync(parameter.Id);

			if (cinema == null)
			{
				throw new NotFoundException("The cinema with a provided Id {CinemaId} is not found." +
					"Please check the cinema record Id and try again.");
			}

			var result = await _cinemasRepository.DeleteAsync(parameter.Id);

			if (result)
			{
				return new Result<CinemaDto>(200, new CinemaDto());
			}

			throw new Exception("Deletion failed.");
		}
	}
}