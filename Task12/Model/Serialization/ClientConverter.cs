using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Task12.Model.Clients;

namespace Task12.Model.Serialization
{
    public class ClientConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Client));
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            switch (jo["Type"]?.Value<string>())
            {
                case "Компания":
                    return jo.ToObject<Company>(serializer);
                case "Частное лицо":
                    return jo.ToObject<PrivatePerson>(serializer);
                default:
                    throw new Exception("Unknown type");
            }
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
