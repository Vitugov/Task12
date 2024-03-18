using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task12.Model.Clients;
using Newtonsoft.Json;
using Task12.Model.Accounts;
using System.IO;
using Task12.ViewModel.Accounts;

namespace Task12.Model.Serialization
{
    internal static class Serialization
    {
        internal static string Path = "_Data.json";
        internal static void Serialize()
        {
            var serializingData = DataStorage.Current.Accounts.ToList()
                .Select(pair => new KeyValuePair<Client, List<Account>>(pair.Key, pair.Value))
                .ToList();

            string jsonString = JsonConvert.SerializeObject(serializingData,
                Formatting.Indented, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
                });
            File.WriteAllText(Path, jsonString);
        }

        internal static void Deserialize()
        {
            var jsonString = File.ReadAllText(Path);
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new ClientConverter());
            settings.Converters.Add(new AccountConverter());
            settings.Formatting = Formatting.Indented;

            // Для десериализации:
            var deserializedData = JsonConvert.DeserializeObject<List<KeyValuePair<Client, List<Account>>>>(jsonString, settings);
            DataStorage.Current.Accounts = deserializedData.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
