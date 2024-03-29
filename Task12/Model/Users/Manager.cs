﻿using Task12.EventModel;
using Task12.Model.Accounts;
using Task12.Model.Clients;

namespace Task12.Model.Users
{
    internal class Manager : User
    {
        internal Manager()
        {
            Name = "Менеджер";
            AccountEvent += DataStorage.Current.UpdateLog;
        }

        internal event Action<object, AccountEventArgs>? AccountEvent;
        internal T AddClient<T>()
            where T : Client, new()
        {
            var newClient = new T();
            newClient.Save();
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
            newAccount.Save();

            var accountEventArgs = new AccountEventArgs(AccountOperation.Open, this, client, newAccount);
            AccountEvent?.Invoke(this, accountEventArgs);
            return newAccount;
        }
        internal void CloseAccount<T>(T account)
            where T : Account
        {
            if (account.Sum != 0)
                throw new InvalidOperationException("Couldn't close account becouse Account Sum does not equal 0");
            account.Delete();

            var accountEventArgs = new AccountEventArgs(AccountOperation.Close, this, account.Client, account);
            AccountEvent?.Invoke(this, accountEventArgs);
        }
        internal void Transfer<TSender, TAcceptor>(decimal sum, TSender senderAccount, TAcceptor acceptorAccount)
            where TSender : Account
            where TAcceptor : Account
        {

            try
            {
                if (!IsMoneyEnought(sum, senderAccount))
                    throw new InvalidOperationException("There is no enought money to transfer");
            }
            catch
            {
                return;
            }

            senderAccount.Sum -= sum;
            acceptorAccount.Sum += sum;

            var accountEventArgs1 = new AccountEventArgs(AccountOperation.Expenditure, this,
                senderAccount.Client, senderAccount, sum, acceptorAccount.Client, acceptorAccount);
            AccountEvent?.Invoke(this, accountEventArgs1);
            var accountEventArgs2 = new AccountEventArgs(AccountOperation.Receipt, this,
                acceptorAccount.Client, acceptorAccount, sum, senderAccount.Client, senderAccount);
            AccountEvent?.Invoke(this, accountEventArgs2);
        }

        internal void Replenishment<T>(decimal sum, T account)
            where T : Account
        {
            account.Sum += sum;

            var accountEventArgs = new AccountEventArgs(AccountOperation.TopUp, this, account.Client, account, sum);
            AccountEvent?.Invoke(this, accountEventArgs);
        }

        internal bool IsMoneyEnought(decimal sum, Account account) => account.Sum - account.Minimum >= sum;

        internal bool IsMoneyEnought(decimal sum, Client client, out List<Account> accountsList)
        {
            accountsList = client.GetAccounts();
            return accountsList.Select(acc => acc.Sum - acc.Minimum).Sum() >= sum;
        }


        internal void TransferBetweenClients<TSender, TAcceptor>(decimal sum, TSender sender, TAcceptor acceptor)
            where TSender : Client
            where TAcceptor : Client
        {

            try
            {
                if (!IsMoneyEnought(sum, sender, out var senderAccountsExt))
                    throw new InvalidOperationException("There is no enought money to transfer");
            }
            catch
            {
                return;
            }

            var k = IsMoneyEnought(sum, sender, out var senderAccounts);
            var acceptorAccount = SelectAcceptorAccount(acceptor);

            decimal sumToDebit = sum;
            foreach (var senderAccount in senderAccounts)
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
            var acceptorAccounts = client.GetAccounts();
            var acceptorAccount = acceptorAccounts
                .Where(acc => acc.GetType() == typeof(CurrentAccount))
                .FirstOrDefault();

            if (acceptorAccount == null)
                acceptorAccount = acceptorAccounts.First();

            return acceptorAccount;
        }

        public void ChangeAccountData(Account account, decimal? limit = null, decimal? interestRateInMonth = null)
        {
            var type = account.GetType();
            var changes = 0;
            if (type == typeof(CurrentAccount))
            {
                return;
            }

            if (type == typeof(CreditAccount))
            {
                var creditAccount = account as CreditAccount;

                if (limit != null && creditAccount.Limit != (decimal)limit)
                {
                    creditAccount.Limit = (decimal)limit;
                    changes += 1;
                }
                if (interestRateInMonth != null && creditAccount.InterestRateInMonth != (decimal)interestRateInMonth)
                {
                    creditAccount.InterestRateInMonth = (decimal)interestRateInMonth;
                    changes += 1;
                }
            }

            if (type == typeof(SavingsAccount))
            {
                var savingAccount = account as SavingsAccount;

                if (limit != null && savingAccount.MinSum != (decimal)limit)
                {
                    savingAccount.MinSum = (decimal)limit;
                    changes += 1;
                }
                if (interestRateInMonth != null && savingAccount.InterestRateInMonth != (decimal)interestRateInMonth)
                {
                    savingAccount.InterestRateInMonth = (decimal)interestRateInMonth;
                    changes += 1;
                }
            }

            if (changes > 0)
            {
                var accountEventArgs = new AccountEventArgs(AccountOperation.Change, this, account.Client, account);
                AccountEvent?.Invoke(this, accountEventArgs);
            }

        }
    }
}
