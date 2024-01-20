using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace CinemaApi.Models.Screenings.Converters
{
	public class TimeConverter : JsonConverter<DateTime>
	{
		public override DateTime Read(
			ref Utf8JsonReader reader, 
			Type typeToConvert, 
			JsonSerializerOptions options)
		{
			var timeString = reader.GetString();

			try
			{
				return DateTime
					.ParseExact(timeString, "HH:mm", 
					CultureInfo.InvariantCulture, 
					DateTimeStyles.None);
			}
			catch (FormatException)
			{
				throw new JsonException("Wrong time format.");
			}
		}

		public override void Write(
			Utf8JsonWriter writer, 
			DateTime value, 
			JsonSerializerOptions options)
		{
			writer
				.WriteStringValue(value.ToUniversalTime()
				.ToString("HH:mm"));
		}
	}
}