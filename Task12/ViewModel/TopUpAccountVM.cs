using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Task12.EventModel;
using Task12.Model.Accounts;
using Task12.Model.Clients;
using Task12.Model.Users;
using Task12.ViewModel.Accounts;
using WPFUsefullThings;

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

        public TopUpAccountVM(User user, Client client, Account account)
            : base(user, client, account)
        {

            OkCommand = new RelayCommand(obj => { SaveCommand(); CloseWindow(obj); });
        }

        private void SaveCommand()
        {
            var manager = User as Manager;
            manager.Replenishment(Sum, Account);
        }
    }
}
