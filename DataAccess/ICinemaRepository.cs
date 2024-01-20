using DataAccess.Entities;

namespace DataAccess
{
	public interface ICinemaRepository : IRepository<Cinema>
	{
		Task<bool> DoesCinemaRecordExistbyAuditoriumAsync(string auditorium);

		Task<bool> DoesCinemaRecordExistByIdAsync(int Id);
	}
}