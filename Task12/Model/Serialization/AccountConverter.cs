using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Task12.Model.Accounts;

namespace Task12.Model.Serialization
{
    public class AccountConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Account));
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            string accountType = jo["Name"]?.Value<string>().Split(' ')[0] ?? "";

            switch (accountType)
            {
                case "Кредитный":
                    return jo.ToObject<CreditAccount>(serializer);
                case "Текущий":
                    return jo.ToObject<CurrentAccount>(serializer);
                case "Сберегательный":
                    return jo.ToObject<SavingsAccount>(serializer);
                default:
                    throw new Exception("Unknown account type");
            }
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
