using Task12.Model.Accounts;
using Task12.Model.Clients;

namespace Task12.Model.Users
{
    public class User
    {
        internal string? Name { get; set; }
        internal List<Client> GetClientsList()
        {
            return DataStorage.Current.GetClients();
        }

        internal List<Account> GetClientAccount(Client client)
        {
            return client.GetAccounts();
        }
    }
}
