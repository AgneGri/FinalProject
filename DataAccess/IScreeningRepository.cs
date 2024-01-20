using DataAccess.Entities;

namespace DataAccess
{
	public interface IScreeningRepository : IRepository<Screening>
	{
		Task<Screening?> GetScreeningWithDetailsAsync(int id);

		Task<List<Screening>> ListScreeningsWithDetailsAsync(int? limit = null);

		Task<bool> DoesScreeningRecordExistByIdAsync(int Id);
	}
}