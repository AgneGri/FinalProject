using CinemaApi.Models;

namespace CinemaApi.Services
{
	public interface IService<TParameter, TData>
	{
		Task<Result<TData>> CallAsync(TParameter parameter);
	}
}