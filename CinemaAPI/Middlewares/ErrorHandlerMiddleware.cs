using System.Net;
using System.Text;
using System.Text.Json;

namespace CinemaApi.Middlewares
{
	public class ErrorHandlerMiddleware
	{
		private readonly RequestDelegate _next;

		public ErrorHandlerMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				if (context.Request.ContentType == "application/json")
				{
					var isValidJson = await IsValidJson(context.Request);
					if (!isValidJson)
					{
						throw new ClientException("Invalid JSON format.");
					}
				}

				await _next(context);
			}
			catch (Exception error)
			{
				var response = context.Response;
				response.ContentType = "application/json";

				switch (error)
				{
					case JsonException jsonException:
						response.StatusCode = (int)HttpStatusCode.BadRequest;
						break;
					case ClientException e:
						response.StatusCode = (int)HttpStatusCode.BadRequest;
						break;
					default:
						response.StatusCode = (int)HttpStatusCode.InternalServerError;
						break;
				}

				var result = System.Text.Json.JsonSerializer.Serialize(new { message = error?.Message });
				await response.WriteAsync(result);
			}
		}

		private async Task<bool> IsValidJson(HttpRequest request)
		{
			try
			{

				var requestBody = await new StreamReader(request.Body).ReadToEndAsync();

				System.Text.Json.JsonSerializer.Deserialize<object>(requestBody);

				request.Body = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));

				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}