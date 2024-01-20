using System.Text.Json.Serialization;
using System.Text.Json;
using System.Globalization;

namespace CinemaApi.Models.Screenings.Converters
{
	public class DateConverter : JsonConverter<DateTime>
	{
		public override DateTime Read(
			ref Utf8JsonReader reader, 
			Type typeToConvert, 
			JsonSerializerOptions options)
		{
			var dateString = reader.GetString();

			try
			{
				return DateTime
					.ParseExact(dateString, "yyyy-MM-ddTHH:mm:ss.fffZ", 
					CultureInfo.InvariantCulture, 
					DateTimeStyles.AdjustToUniversal);
			}
			catch (FormatException)
			{
				throw new JsonException("Wrong date format.");
			}
		}

		public override void Write(
			Utf8JsonWriter writer, 
			DateTime value, 
			JsonSerializerOptions options)
		{
			writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
		}
	}
}