using Task12.EventModel;
using Task12.Model.Accounts;
using Task12.Model.Clients;

namespace Task12.Model
{
    internal class DataStorage
    {
        static internal DataStorage Current { get; set; }
        internal Dictionary<Client, List<Account>> Accounts { get; set; }
        internal List<NotificationMessage> Log { get; set; }

        public static DataStorage operator +(DataStorage dataStorage, Client client)
        {
            client.Save();
            return dataStorage;
        }

        public static DataStorage operator +(DataStorage dataStorage, Account account)
        {
            account.Save();
            return dataStorage;
        }

        public static DataStorage operator -(DataStorage dataStorage, Account account)
        {
            account.Delete();
            return dataStorage;
        }

        static DataStorage()
        {
            Current = new DataStorage();
        }
        internal DataStorage()
        {
            Accounts = [];
            Log = [];
        }
        internal void AddClient(Client client)
        {
            if (Accounts.ContainsKey(client))
                throw new InvalidOperationException("Client already is in the DataStorage");

            Accounts.Add(client, []);
        }

        internal void AddAccount(Account account)
        {
            var accountsList = account.Client.GetAccounts();
            if (accountsList.Contains(account))
                throw new InvalidOperationException("Account already is in the DataStorage");

            Accounts[account.Client].Add(account);
        }

        internal void RemoveAccount(Account account)
        {
            var accountsList = account.Client.GetAccounts();
            if (!accountsList.Contains(account))
                throw new NullReferenceException("There is no Account in the DataStorage");

            Accounts[account.Client].Remove(account);
        }

        internal List<Account> GetAccounts(Client client)
        {
            if (!Accounts.TryGetValue(client, out var accountsList))
                throw new NullReferenceException("There is no Client in the DataStorage");
            return accountsList;
        }

        internal List<Client> GetClients()
        {
            return [.. Accounts.Keys];
        }

        internal void UpdateLog(object sender, AccountEventArgs e)
        {
            var note = new NotificationMessage(e);
            Log.Add(note);
        }
    }
}
