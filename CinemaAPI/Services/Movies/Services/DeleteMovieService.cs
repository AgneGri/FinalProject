using CinemaApi.Exceptions;
using CinemaApi.Models;
using CinemaApi.Models.Movies.DTOs;
using CinemaApi.Models.Movies.Parameters;
using DataAccess;
using DataAccess.Entities;

namespace CinemaApi.Services.Movies.Services
{
	public class DeleteMovieService : IService<DeleteMovieParameter, MovieDto>
	{
		private readonly IRepository<Movie> _moviesRepository;

		public DeleteMovieService(IRepository<Movie> moviesRepository)
		{
			_moviesRepository = moviesRepository;
		}

		public async Task<Result<MovieDto>> CallAsync(DeleteMovieParameter parameter)
		{
			if (parameter == null)
			{
				throw new ArgumentNullException(nameof(parameter));
			}

			var movie = await _moviesRepository.GetAsync(parameter.Id);

			if (movie == null)
			{
				throw new NotFoundException($"The movie with the provided Id {parameter.Id} is not found. Please check the Id and try again.");
			}

			if (DateTime.Now - movie.CreatedAt < TimeSpan.FromDays(14))
			{
				throw new BadRequestException("Cannot delete movies which were created within 14 days.");
			}

			var result = await _moviesRepository.DeleteAsync(parameter.Id);

			if (result)
			{
				return new Result<MovieDto>(200, new MovieDto());
			}

			throw new Exception("Deletion failed.");
		}
	}
}