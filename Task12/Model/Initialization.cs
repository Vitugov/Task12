using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Task12.Model.Accounts;
using Task12.Model.Clients;
using Task12.Model.Users;

namespace Task12.Model
{
    internal static class Initialization
    {
        internal static void Start(Manager manager)
        {
            for (int i = 0; i < 10; i++)
            {
                var client = manager.AddClient<Company>();
                client.ShortName = "Company_" + i;
                client.PhoneNumber = "+7913773" + i + "33" + i;
                AddAccounts(client);
            }

            for (int i = 0; i < 10; i++)
            {
                var client = manager.AddClient<PrivatePerson>();
                client.FirstName = "Имя_" + i;
                client.Patronymic = "Отчество_" + i;
                client.Surname = "Фамилия_" + i;
                client.PhoneNumber = "+791377" + i + "132" + i;
                AddAccounts(client);
            }
        }

        private static void AddAccounts(Client client)
        {
            var rnd = new Random();
            DataStorage.Current.AddAccount(new CurrentAccount() { Client = client, Sum = rnd.Next() / 1000});
            DataStorage.Current.AddAccount(new SavingsAccount() { Client = client, Sum = rnd.Next() / 1000 });
            DataStorage.Current.AddAccount(new CreditAccount()  { Client = client, Sum = rnd.Next() / 1000 });
        }
    }
}
