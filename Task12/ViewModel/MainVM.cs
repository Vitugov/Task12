using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            get =>  _User; 
            set => Set(ref _User, value);
        }
        
        private ObservableCollection<Client> _Clients;
        public ObservableCollection<Client> Clients
        {
            get => _Clients;
            set => Set(ref _Clients, value);
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
            }
        }
    }
}
