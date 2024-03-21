using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Task12.Commands;
using Task12.Model.Accounts;
using Task12.Model.Clients;
using Task12.Model.Users;
using Task12.View.Accounts;

namespace Task12.ViewModel.Accounts
{
    class CurrentAccountVM : AccountVM
    {
        public CurrentAccount CurrentAccount => Account as CurrentAccount;
        public string Name => Account.Name;

        public CurrentAccountVM(User user, Client client, Account account)
            : base(user, client, account)
        {
            OkCommand = new RelayCommand(obj => { SaveCommand(); CloseWindow(obj); });
        }

        private void SaveCommand() {}
    }
}
