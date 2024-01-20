using CinemaApi.Exceptions;
using CinemaApi.Models;
using CinemaApi.Models.Movies.DTOs;
using CinemaApi.Models.Movies.Parameters;
using CinemaApi.Services.Movies.Converters;
using DataAccess;
using DataAccess.Entities;

namespace CinemaApi.Services.Movies.Services
{
	public class ListMovieService : IService<ListMovieParameter, List<MovieDto>>
	{
		private readonly IRepository<Movie> _moviesRepository;
		private readonly MovieDtoConverter _movieDtoConverter;

		public ListMovieService(
			IRepository<Movie> moviesRepository,
			MovieDtoConverter movieDtoConverter)
		{
			_moviesRepository = moviesRepository;
			_movieDtoConverter = movieDtoConverter;
		}

		public async Task<Result<List<MovieDto>>> CallAsync(ListMovieParameter parameter)
		{
			if (parameter == null)
			{
				throw new ArgumentNullException(nameof(parameter));
			}

			if (parameter.ReleaseYear.HasValue && parameter.ReleaseYear.Value > DateTime.Now.Year)
			{
				throw new DataValidationException("ReleaseYear cannot be higher than the current year.");
			}

			var movies = await _moviesRepository.ListAsync();

			if (parameter.Limit.HasValue && parameter.Limit > 0)
			{
				movies = movies.Take(parameter.Limit.Value).ToList();
			}

			if (!string.IsNullOrEmpty(parameter.Genre))
			{
				movies = movies
					.Where(m => m.Genre.ToLower().Contains(parameter.Genre.ToLower()))
					.ToList();
			}

			if (!string.IsNullOrEmpty(parameter.TitleInLt))
			{
				movies = movies
					.Where(m => m.TitleInLT.ToLower() == parameter.TitleInLt.ToLower())
					.ToList();
			}

			if (!string.IsNullOrEmpty(parameter.TitleInOriginalLanguage))
			{
				movies = movies
					.Where(m => m.TitleInOriginalLanguage.ToLower() == parameter.TitleInOriginalLanguage.ToLower())
					.ToList();
			}

			if (parameter.ReleaseYear.HasValue)
			{
				movies = movies
					.Where(m => m.ReleaseYear == parameter.ReleaseYear.Value)
					.ToList();
			}

			if (!movies.Any())
			{
				throw new NotFoundException("No movies found matching the provided search criteria.");
			}

			var movieDtos = movies.Select(_movieDtoConverter.Convert).ToList();

			return new Result<List<MovieDto>>(200, movieDtos);
		}
	}
}