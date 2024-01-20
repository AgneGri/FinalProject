using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
namespace DataAccess
{
	public class ScreeningsRepository : BaseRepository<Screening>, IScreeningRepository
	{
		public ScreeningsRepository(CinemaDbContext context) : base(context)
		{
		}

		public async Task<Screening?> GetScreeningWithDetailsAsync(int id)
		{
			return await _context.Screenings
				.Include(s => s.Cinema)
				.Include(s => s.Movie)
				.FirstOrDefaultAsync(s => s.Id == id);
		}

		public async Task<List<Screening>> ListScreeningsWithDetailsAsync(int? limit = null)
		{
			IQueryable<Screening> query = _context.Screenings
				.Include(s => s.Cinema)
				.Include(s => s.Movie);

			if (limit.HasValue && limit > 0)
			{
				query = query.Take(limit.Value);
			}

			return await query.ToListAsync();
		}

		public async Task<bool> DoesScreeningRecordExistByIdAsync(int Id)
		{
			return await _context.Screenings.AnyAsync(s => s.Id == Id);
		}
	}
}