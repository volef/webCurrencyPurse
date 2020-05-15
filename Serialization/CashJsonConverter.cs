using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace webCurrencyPurse.Serialization
{
    internal class CashJsonConverter : JsonConverter<decimal>
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal);
        }

        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("0.####").Replace(',', '.'));
        }
    }
}