using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Task12.Model;
using Task12.Model.Accounts;
using Task12.Model.Clients;
using Task12.Model.Users;

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
            set => SetWithFilter(ref _CompaniesEnabled, value, FilterClients);
        }

        private bool _PrivatePersonsEnabled = true;
        public bool PrivatePersonsEnabled
        {
            get => _PrivatePersonsEnabled;
            set => SetWithFilter(ref _PrivatePersonsEnabled, value, FilterClients);
        }

        private string _TextFilterString = "";
        public string TextFilterString
        {
            get => _TextFilterString;
            set => SetWithFilter(ref _TextFilterString, value, FilterClients);
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
            set => Set(ref _SelectedClient, value);
        }

        private Account? _SelectedAccount;
        public Account? SelectedAccount
        {
            get => _SelectedAccount;
            set => Set(ref _SelectedAccount, value);
        }

        public MainVM(User user)
        {
            User = user;
            Accounts = [];
            Clients = [];
            if (user.GetType() == typeof(Manager))
            {

                var manager = User as Manager;
                Initialization.Start(manager);

                Clients = new ObservableCollection<Client>(manager.GetClientsList());
                FilteredClients = CollectionViewSource.GetDefaultView(Clients);
                FilteredClients.Filter = ClientFilter;
            }
            
            CompaniesEnabled = true;
            PrivatePersonsEnabled = true;
            TextFilterString = "";

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
    }
}