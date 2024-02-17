using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Task12.Model.Accounts;
using Task12.Model.Clients;

namespace Task12.Model.Users
{
    internal class Manager : User
    {
        internal string Name { get; set; }
        
        internal T AddClient<T>()
            where T : Client, new()
        {
            var newClient = new T();
            DataStorage.Current.AddClient(newClient);
            return newClient;
        }
        
        internal void OpenAccount<T>(Client client)
            where T : Account, new()
        {
            var newAccount = new T
            {
                Client = client,
                Sum = 0
            };
            DataStorage.Current.AddAccount(newAccount);
        }
        internal void CloseAccount<T>(T account)
            where T : Account
        {
            if (account.Sum != 0)
                throw new InvalidOperationException("Couldn't close account becouse Account Sum does not equal 0");
            DataStorage.Current.RemoveAccount(account);
        }
        internal void Transfer<TSender, TAcceptor>(decimal sum, TSender senderAcount, TAcceptor acceptorAccount)
            where TSender : Account
            where TAcceptor : Account
        {
            if (senderAcount.Sum < sum)
                throw new InvalidOperationException("There is no eought money to transfer");
            senderAcount.Sum -= sum;
            acceptorAccount.Sum += sum;
        }

        internal void Replenishment<T>(decimal sum, T account)
            where T : Account
        {
            account.Sum += sum;
        }

        internal List<Client> GetClientsList()
        {
            return DataStorage.Current.GetClients();
        }
    }
}
