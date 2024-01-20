using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
	public class CinemaDbContext : DbContext
	{
		public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options)
		{
		}

		public DbSet<Movie> Movies{ get; set; }
		public DbSet<Cinema> Cinemas { get; set; }
		public DbSet<Screening> Screenings { get; set; }
		public DbSet<Language> languages { get; set; }
		public DbSet<RatingLabelValue> RatingLabelValues { get; set; }
	}
}