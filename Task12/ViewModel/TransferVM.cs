using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Task12.Commands;
using Task12.Model;
using Task12.Model.Accounts;
using Task12.Model.Clients;
using Task12.Model.Users;
using Task12.ViewModel.Accounts;

namespace Task12.ViewModel
{
    internal class TransferVM : BaseVM
    {
        private readonly User _User;

        private bool _IsAcountToAcountTransfer;
        private Client _SenderClient;
        private Account _SenderAccount;
        private Client _AcceptorClient;
        private Account _AcceptorAccount;

        public User User => _User;
        public bool IsAcountToAcountTransfer
        {
            get => _IsAcountToAcountTransfer;
            set => Set(ref _IsAcountToAcountTransfer, value);
        }

        public Client SenderClient
        {
            get => _SenderClient;
            set => SetWithAction(ref _SenderClient, value, SenderAccountsAndNotificationRefresh);
        }

        public Account SenderAccount
        {
            get => _SenderAccount;
            set => SetWithAction(ref _SenderAccount, value, UpdateNotification);
        }

        public Client AcceptorClient
        {
            get => _AcceptorClient;
            set => SetWithAction(ref _AcceptorClient, value, AcceptorAccountsAndNotificationRefresh);
        }

        public Account AcceptorAccount
        {
            get => _AcceptorAccount;
            set => SetWithAction(ref _AcceptorAccount, value, UpdateNotification);
        }

        private ObservableCollection<Client> _Clients;
        public ObservableCollection<Client> Clients
        {
            get => _Clients;
            set => Set(ref _Clients, value);
        }

        private ObservableCollection<Account> _SenderAccounts;
        public ObservableCollection<Account> SenderAccounts
        {
            get => _SenderAccounts;
            set => Set(ref _SenderAccounts, value);
        }

        private ObservableCollection<Account> _AcceptorAccounts;
        public ObservableCollection<Account> AcceptorAccounts
        {
            get => _AcceptorAccounts;
            set => Set(ref _AcceptorAccounts, value);
        }

        private decimal _Sum;
        public decimal Sum
        {
            get => _Sum;
            set => SetWithAction(ref _Sum, value, UpdateNotification);
        }

        private string _Notification;
        public string Notification
        {
            get => _Notification;
            set => Set(ref _Notification, value);
        }

        public RelayCommand OkCommand { get; }
        public RelayCommand CancelCommand { get; }

        public TransferVM(User user, Client client, Account account, bool isAcountToAcountTransfer)
        {
            _IsAcountToAcountTransfer = isAcountToAcountTransfer;
            _User = user;
            _SenderClient = client;
            _SenderAccount = account;

            SenderAccountsRefresh();
            AcceptorAccounts = [];
            Clients = [];
            if (user.GetType() == typeof(Manager))
            {
                var manager = User as Manager;

                Clients = new ObservableCollection<Client>(manager.GetClientsList());
            }

            OkCommand = new RelayCommand(win => ExecuteOkCommand(win), win => Notification == "" || Notification == "У отправителя недостаточно средств!");
            CancelCommand = new RelayCommand(win => CloseWindow(win));
            UpdateNotification();
        }

        private ObservableCollection<Account> GetAccounts(Client client)
        {
            if (client == null)
                return [];
            return new ObservableCollection<Account>(User.GetClientAccount(client));
        }

        private void SenderAccountsRefresh() => SenderAccounts = GetAccounts(SenderClient);

        private void AcceptorAccountsRefresh() => AcceptorAccounts = GetAccounts(AcceptorClient);

        private void UpdateNotification() => Notification = GetNotification();

        private void SenderAccountsAndNotificationRefresh()
        {
            SenderAccountsRefresh();
            UpdateNotification();
        }

        private void AcceptorAccountsAndNotificationRefresh()
        {
            AcceptorAccountsRefresh();
            UpdateNotification();
        }

        private void ExecuteOkCommand(object obj)
        {
            var manager = User as Manager;
            if (IsAcountToAcountTransfer)
            {
                manager.Transfer(Sum, SenderAccount, AcceptorAccount);
            }
            else
            {
                manager.TransferBetweenClients(Sum, SenderClient, AcceptorClient);
            }
            CloseWindow(obj);
        }

        private void CloseWindow(object obj)
        {
            (obj as Window).Close();
        }
        private string GetNotification()
        {
            var manager = User as Manager;
            
            if (SenderClient == null)
            {
                return "Выберите отправителя";
            }

            if (AcceptorClient == null)
            {
                return "Выберите получателя";
            }
            if (IsAcountToAcountTransfer)
            {
                if (SenderAccount == null)
                {
                    return "Выберите счет отправителя!";
                }
                if (!manager.IsMoneyEnought(Sum, SenderAccount))
                {
                    return "Недостаточно средств на счете!";
                }
                if (AcceptorAccount == null)
                {
                    return "Выберите счет получателя!";
                }
            }
            else
            {
                if (!manager.IsMoneyEnought(Sum, SenderClient, out var accountsList))
                {
                    return "У отправителя недостаточно средств!";
                }
            }
            return "";
        }

    }
}
