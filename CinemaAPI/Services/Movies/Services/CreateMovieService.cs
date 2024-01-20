using CinemaApi.Exceptions;
using CinemaApi.Models;
using CinemaApi.Models.Movies.DTOs;
using CinemaApi.Models.Movies.Parameters;
using CinemaApi.Models.Movies.Validators;
using CinemaApi.Services.Movies.Converters;
using DataAccess;

namespace CinemaApi.Services.Movies.Services
{
	public class CreateMovieService : IService<CreateMovieParameter, MovieDto>
	{
		private readonly IMovieRepository _moviesRepository;
		private readonly MovieConverter _movieConverter;
		private readonly MovieDtoConverter _movieDtoConverter;
		private readonly CreateMovieParameterValidator _createMovieParameterValidator;

		public CreateMovieService(
			IMovieRepository moviesRepository, 
			MovieConverter movieConverter,
			MovieDtoConverter movieDtoConverter,
			CreateMovieParameterValidator createMovieParameterValidator)
		{
			_moviesRepository = moviesRepository;
			_movieConverter = movieConverter;
			_movieDtoConverter = movieDtoConverter;
			_createMovieParameterValidator = createMovieParameterValidator;
		}

		public async Task<Result<MovieDto>> CallAsync(CreateMovieParameter parameter)
		{
			if (parameter == null)
			{
				throw new ArgumentNullException(nameof(parameter));
			}

			_createMovieParameterValidator.ValidateParameters(parameter.Movie);

			var movieExists = await _moviesRepository.DoesMovieExistAsync(
				parameter.Movie.TitleInLT,
				parameter.Movie.TitleInOriginalLanguage
			);

			if (movieExists)
			{
				throw new RecordAlreadyExistsException("A movie with the same title already exists.");
			}

			var movieEntity = _movieConverter.Create(parameter.Movie);
			var createdMovie = await _moviesRepository.CreateAsync(movieEntity);
			var movieDto = _movieDtoConverter.Convert(createdMovie);

			return new Result<MovieDto>(200, movieDto);
		}
	}
}