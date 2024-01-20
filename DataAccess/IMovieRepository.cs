using DataAccess.Entities;

namespace DataAccess
{
	public interface IMovieRepository : IRepository<Movie>
	{
		Task<bool> DoesMovieExistAsync(string titleInLT, string titleInOriginalLanguage);
		Task<bool> DoesMovieExistByIdAsync(int id);
	}
}