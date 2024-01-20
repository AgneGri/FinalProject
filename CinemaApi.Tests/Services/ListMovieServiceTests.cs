using CinemaApi.Models.Movies.DTOs;
using CinemaApi.Models.Movies.Parameters;
using CinemaApi.Services.Movies.Converters;
using CinemaApi.Services.Movies.Services;
using DataAccess;
using DataAccess.Entities;
using Moq;
using System.Linq.Expressions;

namespace CinemaApi.Tests.Services
{
	[TestClass]
	public class ListMovieServiceTests
	{
		private ListMovieParameter parameter1 = null!;
		private ListMovieParameter parameter2 = null!;
		private List<Movie> movieRepositoryResult = null!;
		private Mock<IRepository<Movie>> moviesRepositoryMock = null!;
		private Mock<MovieDtoConverter> movieDtoConverterMock = null!;

		[TestInitialize]
		public void Initialize()
		{
			parameter1 = new ListMovieParameter(3);
			parameter2 = new ListMovieParameter(
			releaseYear: 2023,
			genre: "Trileris");

			movieRepositoryResult = CreateMovieList();
			moviesRepositoryMock = new Mock<IRepository<Movie>>();
			moviesRepositoryMock
				.Setup(repo => repo.ListAsync(It.IsAny<Expression<Func<Movie, bool>>>()))
				.ReturnsAsync(movieRepositoryResult);
			
			movieDtoConverterMock = new Mock<MovieDtoConverter>();
			movieDtoConverterMock
				.Setup(converter => converter.Convert(It.IsAny<Movie>()))
				.Returns((Movie movie) => new MovieDto
				{
					Id = movie.Id,
					TitleInLT = movie.TitleInLT,
					TitleInOriginalLanguage = movie.TitleInOriginalLanguage,
					Description = movie.Description,
					ReleaseYear = movie.ReleaseYear,
					Genre = movie.Genre,
					RatingLabelValue = Enum.GetName(typeof(RatingType), movie.RatingLabelValueId),
					Language = Enum.GetName(typeof(LanguageType), movie.LanguageId),
					SubtitleLanguage = Enum.GetName(typeof(LanguageType), movie.SubtitleLanguageId),
					RatingsInStars = movie.RatingsInStars
				}
			);
		}

		[TestMethod]
		public async Task ThrowsArgumentNullException()
		{
			// arrange
			var service = CreateService();

			// act and assert
			await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => service.CallAsync(null));
		}

		[TestMethod]
		public async Task ReturnsListOfMoviesWhenLimitIsSet()
		{
			// arrange
			var service = CreateService();
			int expectedMovieCount = 3;

			// act
			var result = await service.CallAsync(parameter1);

			// assert
			Assert.AreEqual(200, result.Status);
			Assert.IsNotNull(result.Data);
			Assert.AreEqual(expectedMovieCount, result.Data.Count);
			for (int i = 0; i < expectedMovieCount; i++)
			{
				Assert.AreEqual(movieRepositoryResult[i].Id, result.Data[i].Id);
				Assert.AreEqual(movieRepositoryResult[i].TitleInLT, result.Data[i].TitleInLT);
				Assert.AreEqual(movieRepositoryResult[i].TitleInOriginalLanguage, result.Data[i].TitleInOriginalLanguage);
				Assert.AreEqual(movieRepositoryResult[i].Description, result.Data[i].Description);
				Assert.AreEqual(movieRepositoryResult[i].ReleaseYear, result.Data[i].ReleaseYear);
				Assert.AreEqual(movieRepositoryResult[i].Genre, result.Data[i].Genre);
				Assert.AreEqual(Enum.GetName(typeof(RatingType), movieRepositoryResult[i].RatingLabelValueId), result.Data[i].RatingLabelValue);
				Assert.AreEqual(Enum.GetName(typeof(LanguageType), movieRepositoryResult[i].LanguageId), result.Data[i].Language);
				Assert.AreEqual(movieRepositoryResult[i].RatingsInStars, result.Data[i].RatingsInStars);
				Assert.AreEqual(Enum.GetName(typeof(LanguageType), movieRepositoryResult[i].SubtitleLanguageId), result.Data[i].SubtitleLanguage);
			}
		}

		[TestMethod]
		public async Task ReturnsListOfMoviesWhenCorrectReleaseDataAndGenreIsSet()
		{
			// arrange
			var service = CreateService();
			var expectedMovieCount = 2;

			// act
			var result = await service.CallAsync(parameter2);

			// assert
			Assert.AreEqual(200, result.Status);
			Assert.IsNotNull(result.Data);
			Assert.AreEqual(expectedMovieCount, result.Data.Count);
			for (int i = 0; i < expectedMovieCount; i++)
			{
				Assert.AreEqual(movieRepositoryResult[i].Id, result.Data[i].Id);
				Assert.AreEqual(movieRepositoryResult[i].TitleInLT, result.Data[i].TitleInLT);
				Assert.AreEqual(movieRepositoryResult[i].TitleInOriginalLanguage, result.Data[i].TitleInOriginalLanguage);
				Assert.AreEqual(movieRepositoryResult[i].Description, result.Data[i].Description);
				Assert.AreEqual(movieRepositoryResult[i].ReleaseYear, result.Data[i].ReleaseYear);
				Assert.AreEqual(movieRepositoryResult[i].Genre, result.Data[i].Genre);
				Assert.AreEqual(Enum.GetName(typeof(RatingType), movieRepositoryResult[i].RatingLabelValueId), result.Data[i].RatingLabelValue);
				Assert.AreEqual(Enum.GetName(typeof(LanguageType), movieRepositoryResult[i].LanguageId), result.Data[i].Language);
				Assert.AreEqual(movieRepositoryResult[i].RatingsInStars, result.Data[i].RatingsInStars);
				Assert.AreEqual(Enum.GetName(typeof(LanguageType), movieRepositoryResult[i].SubtitleLanguageId), result.Data[i].SubtitleLanguage);
			}
		}

		[TestMethod]
		public async Task CallsMoviesRepositoryListOnce()
		{
			// arrange
			var service = CreateService();

			// act
			var result = await service.CallAsync(parameter1);

			// assert
			moviesRepositoryMock
				.Verify(m => m.ListAsync(null), 
				Times.Once
			);
		}

		private ListMovieService CreateService()
		{
			return new ListMovieService(moviesRepositoryMock.Object, movieDtoConverterMock.Object);
		}

		private List<Movie> CreateMovieList()
		{
			var movies = new List<Movie>
	{
		new Movie
		{
			TitleInLT = "Mėnulio Šviesa",
			TitleInOriginalLanguage = "Moonlight",
			Description = "A touching story about growth and self-discovery.",
			ReleaseYear = 2024,
			Genre = "Drama",
			RatingLabelValueId = 1,
			LanguageId = 1,
			RatingsInStars = 5,
			CreatedAt = DateTime.Now,
			SubtitleLanguageId = 2
		},
		new Movie
		{
			TitleInLT = "Amžinos Žvaigždės",
			TitleInOriginalLanguage = "Eternal Stars",
			Description = "An epic journey through space and time, exploring the mysteries of the universe.",
			ReleaseYear = 2023,
			Genre = "Fantastinis",
			RatingLabelValueId = 2,
			LanguageId = 1,
			RatingsInStars = 4,
			CreatedAt = DateTime.Now,
			SubtitleLanguageId = 2
		},
		new Movie
		{
			TitleInLT = "Nakties Paslaptis",
			TitleInOriginalLanguage = "Secrets of the Night",
			Description = "An intriguing thriller that weaves a tale of mystery and suspense in a small town.",
			ReleaseYear = 2023,
			Genre = "Trileris",
			RatingLabelValueId = 3,
			LanguageId = 1,
			RatingsInStars = 2,
			CreatedAt = DateTime.Now,
			SubtitleLanguageId = 2
		},
		new Movie
		{
			TitleInLT = "Šalta naktis",
			TitleInOriginalLanguage = "Cold night",
			Description = "An intriguing thriller about 2 family lives in a small town.",
			ReleaseYear = 2023,
			Genre = "Trileris",
			RatingLabelValueId = 3,
			LanguageId = 1,
			RatingsInStars = 4,
			CreatedAt = DateTime.Now,
			SubtitleLanguageId = 2
		}
	};

			return movies;
		}
	}
}