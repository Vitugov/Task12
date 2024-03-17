using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Task12.Commands;
using Task12.Model.Accounts;
using Task12.Model.Clients;
using Task12.Model.Users;
using Task12.View.Accounts;

namespace Task12.ViewModel.Accounts
{
    class TypeSelectionVM : BaseVM
    {
        private readonly User _User;
        private readonly Client _Client;
        private readonly Dictionary<string, Type> _AccountTypesDic;
        private ObservableCollection<string> _AccountTypes;
        public ObservableCollection<string> AccountTypes
        {
            get => _AccountTypes;
            set => Set(ref _AccountTypes, value);
        }

        private string _SelectedType;
        public string SelectedType
        {
            get => _SelectedType;
            set => Set(ref _SelectedType, value);
        }

        public TypeSelectionVM(User user, AccountTypeSelection window, Client client)
        {
            _AccountTypesDic = new Dictionary<string, Type>()
            {
                { "Текущий счет", typeof(CurrentAccount) },
                { "Кредитный счет", typeof(CreditAccount) },
                { "Сберегательный счет", typeof(SavingsAccount) }
            };
            _AccountTypes = new ObservableCollection<string>(_AccountTypesDic.Keys);
            _SelectedType = "Текущий счет";
            _Client = client;
            _User = user;
            CancelCommand = new RelayCommand(x => window.Close());
            OkCommand = new RelayCommand(x => Execute_OkCommand(window), x => SelectedType != null);
        }

        public RelayCommand OkCommand { get; }
        public RelayCommand CancelCommand { get; }

        private void Execute_OkCommand(Window window)
        {
            Window newWindow;
            var manager = _User as Manager;
            Account account;
            switch (SelectedType)
            {
                case "Текущий счет":
                    account = manager.OpenAccount<CurrentAccount>(_Client);
                    newWindow = new CurrentAccountView(_User, _Client, account);
                    newWindow.Show();
                    break;
                    
                case "Кредитный счет":
                    account = manager.OpenAccount<CreditAccount>(_Client);
                    newWindow = new CreditAccountView(_User, _Client, account);
                    newWindow.Show();
                    break;

                case "Сберегательный счет":
                    account = manager.OpenAccount<SavingsAccount>(_Client);
                    newWindow = new SavingAccountView(_User, _Client, account);
                    newWindow.Show();
                    break;
            }
            window.Close();
        }

    }
}
