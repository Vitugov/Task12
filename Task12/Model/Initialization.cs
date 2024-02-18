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
            Random rand = new Random();

            // Генерация записей для компаний
            string[] companyNames = { "Электрозавод", "Текстильщик", "Красный Октябрь", "Госстрах", "Пролетарий", "Металлург", "Автостроитель", "Химик", "Энергетик", "Строитель" };
            for (int i = 0; i < 10; i++)
            {
                var client = manager.AddClient<Company>();
                client.ShortName = companyNames[i];
                // Генерация псевдослучайного телефона
                client.PhoneNumber = $"+7913{rand.Next(100, 999)}{rand.Next(1000, 9999)}";
                AddAccounts(client);
            }

            // Генерация записей для частных лиц
            string[] firstNames = { "Ярослав", "Всеволод", "Святослав", "Мстислав", "Ростислав", "Владислав", "Станислав", "Борислав", "Любомир", "Владимир" };
            string[] patronymics = { "Владимирович", "Русланович", "Богданович", "Ярославович", "Святославович", "Мстиславович", "Ростиславович", "Дмитриевич", "Васильевич", "Игоревич" };
            string[] surnames = { "Князев", "Волконский", "Рюрикович", "Драгомиров", "Святогоров", "Боянов", "Городецкий", "Добрынин", "Заболотный", "Измайлов" };

            for (int i = 0; i < 10; i++)
            {
                var client = manager.AddClient<PrivatePerson>();
                client.FirstName = firstNames[i];
                client.Patronymic = patronymics[i];
                client.Surname = surnames[i];
                // Генерация псевдослучайного телефона
                client.PhoneNumber = $"+7913{rand.Next(100, 999)}{rand.Next(1000, 9999)}";
                AddAccounts(client);
            }
        }

        private static void AddAccounts(Client client)
        {
            var rnd = new Random();
            DataStorage.Current.AddAccount(new CurrentAccount(client) { Sum = rnd.Next() / 1000 });
            DataStorage.Current.AddAccount(new SavingsAccount(client) { Sum = rnd.Next() / 1000 });
            DataStorage.Current.AddAccount(new CreditAccount(client)  { Sum = rnd.Next() / 1000 });
        }
    }
}
