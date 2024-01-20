using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
	public class CinemasRepository : BaseRepository<Cinema>, ICinemaRepository
	{
		public CinemasRepository(CinemaDbContext context) : base(context)
		{
		}

		public async Task<bool> DoesCinemaRecordExistbyAuditoriumAsync(string auditorium)
		{
			return await _context.Cinemas.AnyAsync(c => c.Auditorium == auditorium);
		}

		public async Task<bool> DoesCinemaRecordExistByIdAsync(int Id)
		{
			return await _context.Cinemas.AnyAsync(c => c.Id == Id);
		}
	}
}