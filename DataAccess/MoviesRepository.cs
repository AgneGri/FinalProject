using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
	public class MoviesRepository : BaseRepository<Movie>, IMovieRepository
	{
		public MoviesRepository(CinemaDbContext context) : base(context)
		{
		}

		public async Task<bool> DoesMovieExistAsync(string titleInLT, string titleInOriginalLanguage)
		{
			return await _context.Movies
				.AnyAsync(
					m =>
						m.TitleInLT == titleInLT
						|| m.TitleInOriginalLanguage == titleInOriginalLanguage
				);
		}

		public async Task<bool> DoesMovieExistByIdAsync(int Id)
		{
			return await _context.Movies.AnyAsync(m => m.Id == Id);
		}
	}
}