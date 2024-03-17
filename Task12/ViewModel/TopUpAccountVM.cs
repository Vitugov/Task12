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
using Task12.ViewModel.Accounts;

namespace Task12.ViewModel
{
    public class TopUpAccountVM : AccountVM
    {
        private decimal _Sum;
        public decimal Sum
        {
            get => _Sum;
            set => Set(ref _Sum, value);
        }

        public TopUpAccountVM(User user, Client client, Account account, Window window)
            : base(user, client, account, window)
        {
            OkCommand = new RelayCommand(x => OkCommandExecute());
            CancelCommand = new RelayCommand(x => Window.Close());
        }

        public RelayCommand OkCommand { get; }
        public RelayCommand CancelCommand { get; }

        private void OkCommandExecute()
        {
            var manager = User as Manager;
            manager.Replenishment(Sum, Account);
            Window.Close();
        }
    }
}
