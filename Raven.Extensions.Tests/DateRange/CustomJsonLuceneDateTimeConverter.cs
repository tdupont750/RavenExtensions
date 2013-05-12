using System;
using Raven.Abstractions.Linq;
using Raven.Imports.Newtonsoft.Json;

namespace Raven.Extensions.Tests.DateRange
{
    public class CustomJsonLuceneDateTimeConverter : JsonConverter
    {
        public static readonly CustomJsonLuceneDateTimeConverter Instance = new CustomJsonLuceneDateTimeConverter();

        public static string DateToString(DateTime value)
        {
            return DateTools.DateToString(value, DateTools.Resolution.MILLISECOND);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var input = reader.Value as long?;

            if (input.HasValue)
            {
                var stringToDate = DateTools.StringToDate(input.Value.ToString());
                return DateTime.SpecifyKind(stringToDate, DateTimeKind.Local);
            }

            return reader.Value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            object writeValue;
            if (value is DateTime)
            {
                var dateString = DateToString((DateTime)value);
                writeValue = Int64.Parse(dateString);
            }
            else
                writeValue = value;

            writer.WriteValue(writeValue);
        }
    }
}