using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task12.Model.Accounts;
using Task12.Model.Clients;

namespace Task12.Model
{
    public static class DataStorageExtension
    {
        public static void Save(this Client client)
        {
            DataStorage.Current.AddClient(client);
        }

        public static void Save(this Account account)
        {
            DataStorage.Current.AddAccount(account);
        }

        public static void Delete(this Account account)
        {
            DataStorage.Current.RemoveAccount(account);
        }

        public static List<Account> GetAccounts(this Client client)
        {
            return DataStorage.Current.GetAccounts(client);
        }
    }
}
