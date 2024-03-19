using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using Task12.Commands;
using Task12.Model.Accounts;
using Task12.Model.Clients;
using Task12.Model.Users;
using Task12.View.Accounts;
using Task12.ViewModel.Accounts;
using Task12.View;
using Task12.Model.Serialization;
using System.IO;
using Task12.Model;

namespace Task12.ViewModel
{
    public class MainVM : BaseVM

    {
        private User _User;
        public User User
        {
            get => _User;
            set => Set(ref _User, value);
        }

        private bool _CompaniesEnabled = true;
        public bool CompaniesEnabled
        {
            get => _CompaniesEnabled;
            set => SetWithAction(ref _CompaniesEnabled, value, FilterClients);
        }

        private bool _PrivatePersonsEnabled = true;
        public bool PrivatePersonsEnabled
        {
            get => _PrivatePersonsEnabled;
            set => SetWithAction(ref _PrivatePersonsEnabled, value, FilterClients);
        }

        private string _TextFilterString = "";
        public string TextFilterString
        {
            get => _TextFilterString;
            set => SetWithAction(ref _TextFilterString, value, FilterClients);
        }

        private ObservableCollection<Client> _Clients;
        public ObservableCollection<Client> Clients
        {
            get => _Clients;
            set => Set(ref _Clients, value);
        }

        private ICollectionView _filteredClients;
        public ICollectionView FilteredClients
        {
            get => _filteredClients;
            set => Set(ref _filteredClients, value);

        }

        private ObservableCollection<Account> _Accounts;
        public ObservableCollection<Account> Accounts
        {
            get => _Accounts;
            set => Set(ref _Accounts, value);
        }

        private Client? _SelectedClient;
        public Client? SelectedClient
        {
            get => _SelectedClient;
            set => SetWithAction(ref _SelectedClient, value, RefreshAccounts);
        }

        private Account? _SelectedAccount;
        public Account? SelectedAccount
        {
            get => _SelectedAccount;
            set => Set(ref _SelectedAccount, value);
        }

        //public RelayCommand EditClientCommand { get; }
        //public RelayCommand AddClientCommand { get; }
        public RelayCommand MoneyTransferClientToClientCommand { get; }

        public RelayCommand CloseAccountCommand { get; }
        public RelayCommand EditAccountCommand { get; }
        public RelayCommand AddAccountCommand { get; }
        public RelayCommand TopUpAccountCommand { get; }
        public RelayCommand MoneyTransferAccountToAccountCommand { get; }
        public MainVM(User user)
        {
            User = user;
            Accounts = [];
            Clients = [];
            if (user.GetType() == typeof(Manager))
            {
                var manager = User as Manager;
                Serialization.Load(manager);

                Clients = new ObservableCollection<Client>(manager.GetClientsList());
                FilteredClients = CollectionViewSource.GetDefaultView(Clients);
                FilteredClients.Filter = ClientFilter;
            }
            
            CompaniesEnabled = true;
            PrivatePersonsEnabled = true;
            TextFilterString = "";

            CloseAccountCommand = new RelayCommand(CloseAccount, CanCloseAccount);
            AddAccountCommand = new RelayCommand(n => (new AccountTypeSelection(user, SelectedClient)).Show(),
                n => SelectedClient != null);
            EditAccountCommand = new RelayCommand(n => EditAccount(SelectedAccount), n => SelectedAccount != null);
            TopUpAccountCommand = new RelayCommand(n => (new TopUpAccountView(user, SelectedClient, SelectedAccount)).Show(),
                n => SelectedAccount != null);
            MoneyTransferClientToClientCommand = new RelayCommand(n => (new TransferView(user, SelectedClient, SelectedAccount, false)).Show());
            MoneyTransferAccountToAccountCommand = new RelayCommand(n => (new TransferView(user, SelectedClient, SelectedAccount, true)).Show());
        }
        private void FilterClients()
        {
            FilteredClients.Refresh();
        }

        private bool ClientFilter(object item)
        {
            var typeFilter = TypeFilter(item.GetType());
            var textFilter = TextFilter(item as Client);
            return typeFilter && textFilter;
        }

        private bool TypeFilter(Type type)
        {
            List<Type> types = [];
            if (CompaniesEnabled)
                types.Add(typeof(Company));
            if (PrivatePersonsEnabled)
                types.Add(typeof(PrivatePerson));
            return types.Contains(type);
        }

        private bool TextFilter(Client client)
        {
            return client.Name.ToLower().Contains(TextFilterString.ToLower());
        }

        private void RefreshAccounts()
        {
            if (SelectedClient == null)
                Accounts = [];
            else
                Accounts = new ObservableCollection<Account>(User.GetClientAccount(SelectedClient));
        }

        private void CloseAccount(object obj)
        {
            var account = obj as Account;
            var manager = User as Manager;
            manager.CloseAccount(account);
            RefreshAccounts();
        }

        private bool CanCloseAccount(object obj)
        {
            return 
                obj != null &&
                (User.GetType() == typeof(Manager)) &&
                ((obj as Account).Sum == 0);
        }

        private void EditAccount(object obj)
        {
            var accountType = obj.GetType();
            var account = obj as Account;
            Window window;
            
            if (accountType == typeof(CurrentAccount))
            {
                window = new CurrentAccountView(_User, SelectedClient, account);
                window.Show();
            }
            else if (accountType == typeof(CreditAccount))
            {
                window = new CreditAccountView(_User, SelectedClient, account);
                window.Show();
            }
            else if (accountType == typeof(SavingsAccount))
            {
                window = new SavingAccountView(_User, SelectedClient, account);
                window.Show();
            }
        }
        public void WindowActivated(object sender, System.EventArgs e)
        {
            RefreshAccounts();
        }

        public void OnWindowClosing()
        {
            Serialization.Serialize();
        }
    }
}