using CinemaApi.Exceptions;
using CinemaApi.Models;
using CinemaApi.Models.Movies.DTOs;
using CinemaApi.Models.Movies.Parameters;
using CinemaApi.Models.Movies.Validators;
using CinemaApi.Services.Movies.Converters;
using DataAccess;

namespace CinemaApi.Services.Movies.Services
{
	public class UpdateMovieService : IService<UpdateMovieParameter, MovieDto>
	{
		private readonly IMovieRepository _moviesRepository;
		private readonly MovieConverter _movieConverter;
		private readonly MovieDtoConverter _movieDtoConverter;
		private readonly UpdateMovieParameterValidator _updateMovieParameterValidator;

		public UpdateMovieService(
			IMovieRepository moviesRepository,
			MovieConverter movieConverter,
			MovieDtoConverter movieDtoConverter,
			UpdateMovieParameterValidator updateMovieParameterValidator)
		{
			_moviesRepository = moviesRepository;
			_movieConverter = movieConverter;
			_movieDtoConverter = movieDtoConverter;
			_updateMovieParameterValidator = updateMovieParameterValidator;
		}

		public async Task<Result<MovieDto>> CallAsync(UpdateMovieParameter parameter)
		{
			if (parameter == null)
			{
				throw new ArgumentNullException(nameof(parameter));
			}

			_updateMovieParameterValidator.ValidateParameters(parameter.Id, parameter.Movie);

			var movieExistsById = await _moviesRepository.DoesMovieExistByIdAsync(parameter.Movie.Id);


			if (!movieExistsById)
			{
				throw new NotFoundException("The movie with the specified Id is not found.");
			}

			var currentYear = DateTime.Now.Year;

			if (parameter.Movie.ReleaseYear > currentYear)
			{
				throw new BadRequestException("The release year cannot be in the future.");
			}

			var movieToUpdate = await _moviesRepository.GetAsync(parameter.Movie.Id);
			var updatedMovie = _movieConverter.Update(parameter.Movie, movieToUpdate);
			var updatedEntity = await _moviesRepository.UpdateAsync(updatedMovie);
			var movieDto = _movieDtoConverter.Convert(updatedEntity);

			return new Result<MovieDto>(200, movieDto);
		}
	}
}