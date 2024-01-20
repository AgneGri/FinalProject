using System.Linq.Expressions;

namespace DataAccess
{
	public interface IRepository<TEntity>
	{
		Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>>? filter = null);

		Task<TEntity> CreateAsync(TEntity entity);

		Task<TEntity?> GetAsync(int id);

		Task<TEntity> UpdateAsync(TEntity entity);

		Task<bool> DeleteAsync(int id);
	}
}