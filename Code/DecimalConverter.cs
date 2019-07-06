using System;
using Newtonsoft.Json;

namespace CustomerService
{
    public class DecimalConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(decimal));
        }
        public override bool CanRead => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(((decimal)value).ToString("0.00"));
        }
    }
}