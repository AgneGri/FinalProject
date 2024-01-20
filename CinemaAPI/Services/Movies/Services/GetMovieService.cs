using CinemaApi.Exceptions;
using CinemaApi.Models;
using CinemaApi.Models.Movies.DTOs;
using CinemaApi.Models.Movies.Parameters;
using CinemaApi.Services.Movies.Converters;
using DataAccess;

namespace CinemaApi.Services.Movies.Services
{
	public class GetMovieService : IService<GetMovieParameter, MovieDto>
	{
		private readonly IMovieRepository _moviesRepository;
		private readonly MovieDtoConverter _movieDtoConverter;

		public GetMovieService(
			IMovieRepository moviesRepository,
			MovieDtoConverter movieDtoConverter)
		{
			_moviesRepository = moviesRepository;
			_movieDtoConverter = movieDtoConverter;
		}

		public async Task<Result<MovieDto>> CallAsync(GetMovieParameter parameter)
		{
			if (parameter == null)
			{
				throw new ArgumentNullException(nameof(parameter));
			}

			var movieExistById = await _moviesRepository.DoesMovieExistByIdAsync(parameter.Id);

			if (!movieExistById)
			{
				throw new NotFoundException("The movie with the specified Id is not found in the database. " +
					"Please check the Id and try again.");
			}

			var result = await _moviesRepository.GetAsync(parameter.Id);
			var movieDto = _movieDtoConverter.Convert(result);

			return new Result<MovieDto>(200, movieDto);
		}
	}
}