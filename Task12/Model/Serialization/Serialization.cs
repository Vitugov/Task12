using Newtonsoft.Json;
using System.IO;
using Task12.EventModel;
using Task12.Model.Accounts;
using Task12.Model.Clients;
using Task12.Model.Users;

namespace Task12.Model.Serialization
{
    internal static class Serialization
    {
        internal static string Path = "_Data.json";
        internal static string LogPath = "_Log.json";
        internal static void Serialize()
        {
            var serializingData = DataStorage.Current.Accounts.ToList()
                .Select(pair => new KeyValuePair<Client, List<Account>>(pair.Key, pair.Value))
                .ToList();

            var serializingLog = DataStorage.Current.Log;

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            };
            var dataString = JsonConvert.SerializeObject(serializingData, Formatting.Indented, settings);
            File.WriteAllText(Path, dataString);
            var logString = JsonConvert.SerializeObject(serializingLog, Formatting.Indented, settings);
            File.WriteAllText(LogPath, logString);
        }

        internal static void Deserialize()
        {
            var dataString = File.ReadAllText(Path);
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new ClientConverter());
            settings.Converters.Add(new AccountConverter());
            settings.Formatting = Formatting.Indented;

            var deserializedData = JsonConvert.DeserializeObject<List<KeyValuePair<Client, List<Account>>>>(dataString, settings);
            deserializedData.ForEach(pair => pair.Value.ForEach(acc => acc.Client = pair.Key));
            DataStorage.Current.Accounts = deserializedData.ToDictionary(x => x.Key, x => x.Value);

            if (!File.Exists(LogPath))
                return;
            var logString = File.ReadAllText(LogPath);
            var logSettings = new JsonSerializerSettings();
            var deserializedLog = JsonConvert.DeserializeObject<List<NotificationMessage>>(logString, logSettings);
            DataStorage.Current.Log = deserializedLog;
        }

        internal static void Load(Manager manager)
        {
            if (File.Exists("_Data.json"))
            {
                Deserialize();
            }
            else
            {
                Initialization.Start(manager);
            }
        }
    }
}
