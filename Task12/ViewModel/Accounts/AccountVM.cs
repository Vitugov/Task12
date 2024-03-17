using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Task12.Model.Accounts;
using Task12.Model.Clients;
using Task12.Model.Users;
using Task12.View.Accounts;

namespace Task12.ViewModel.Accounts
{
    public class AccountVM : BaseVM
    {
        private readonly User _User;
        private readonly Client _Client;
        private readonly Account _Account;
        private readonly Window _Window;

        public User User => _User;
        public Client Client => _Client;
        public Account Account => _Account;
        public Window Window => _Window;
        public AccountVM(User user, Client client, Account account, Window window)
        {
            _User = user;
            _Client = client;
            _Account = account;
            _Window = window;
        }
    }
}
