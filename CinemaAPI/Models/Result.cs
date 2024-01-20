namespace CinemaApi.Models
{
	public class Result<TData>
	{
			public Result(
				int status,
				TData data)
			{
				Status = status;
				Data = data;
				Errors = new List<string>();
			}

			public Result(
				int status,
				TData data,
				List<string> errors)
			{
				Status = status;
				Data = data;
				Errors = errors;
			}

			public int Status { get; set; }
			public TData Data { get; set; }
			public List<string> Errors { get; set; }
	}
}