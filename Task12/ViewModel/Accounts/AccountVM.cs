﻿using System.Windows;
using Task12.Model.Accounts;
using Task12.Model.Clients;
using Task12.Model.Users;
using WPFUsefullThings;

namespace Task12.ViewModel.Accounts
{
    public class AccountVM : INotifyPropertyChangedPlus
    {
        private readonly User _User;
        private readonly Client _Client;
        private readonly Account _Account;
        private readonly Window _Window;

        public User User => _User;
        public Client Client => _Client;
        public Account Account => _Account;
        public Window Window => _Window;
        public RelayCommand OkCommand { get; set; }
        public RelayCommand CancelCommand { get; }

        public AccountVM(User user, Client client, Account account)
        {
            _User = user;
            _Client = client;
            _Account = account;
            CancelCommand = new RelayCommand(CloseWindow);

        }

        internal void CloseWindow(object obj) => (obj as Window).Close();
    }
}
