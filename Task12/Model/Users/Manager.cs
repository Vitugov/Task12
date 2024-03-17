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
        
        internal T OpenAccount<T>(Client client)
            where T : Account, new()
        {
            var newAccount = new T
            {
                Client = client,
                Sum = 0
            };
            DataStorage.Current.AddAccount(newAccount);
            return newAccount;
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
            if (!IsMoneyEnought(sum, senderAcount))
                throw new InvalidOperationException("There is no enought money to transfer");
            senderAcount.Sum -= sum;
            acceptorAccount.Sum += sum;
        }

        internal void Replenishment<T>(decimal sum, T account)
            where T : Account
        {
            account.Sum += sum;
        }

        internal bool IsMoneyEnought(decimal sum, Account account) => account.Sum - account.Minimum >= sum;

        internal bool IsMoneyEnought(decimal sum, Client client, out List<Account> accountsList)
        {
            accountsList = DataStorage.Current.GetAccounts(client);
            return accountsList.Select(acc => acc.Sum - acc.Minimum).Sum() >= sum;
        }

        
        internal void TransferBetweenClients<TSender, TAcceptor>(decimal sum, TSender sender, TAcceptor acceptor)
            where TSender : Client
            where TAcceptor : Client
        {
            if (!IsMoneyEnought(sum, sender, out var senderAccounts))
                throw new InvalidOperationException("There is no enought money to transfer");

            var acceptorAccount = SelectAcceptorAccount(acceptor);
            
            decimal sumToDebit = sum;
            foreach ( var senderAccount in senderAccounts)
            {
                var freeMoney = senderAccount.Sum - senderAccount.Minimum;

                if (freeMoney >= sumToDebit)
                {
                    Transfer(sumToDebit, senderAccount, acceptorAccount);
                    break;
                }
                else
                {
                    Transfer(freeMoney, senderAccount, acceptorAccount);
                    sumToDebit -= freeMoney;
                }
            }
        }

        private Account SelectAcceptorAccount(Client client)
        {
            var acceptorAccounts = DataStorage.Current.GetAccounts(client);
            var acceptorAccount = acceptorAccounts
                .Where(acc => acc.GetType() == typeof(CurrentAccount))
                .FirstOrDefault();

            if (acceptorAccount == null)
                acceptorAccount = acceptorAccounts.First();

            return acceptorAccount;
        }
    }
}
