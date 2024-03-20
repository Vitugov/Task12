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

namespace Task12.ViewModel.Accounts
{
    class CreditAccountVM : AccountVM
    {
        private decimal _Sum;
        private decimal _Limit;
        private decimal _InterestRateInMonth;

        public CreditAccount CreditAccount => Account as CreditAccount;
        public string Name => Account.Name;
        public decimal Sum
        {
            get => _Sum; 
            set => Set(ref _Sum,value);
        }

        public decimal Limit
        {
            get => _Limit;
            set => Set(ref _Limit, value);
        }

        public decimal InterestRateInMonth
        {
            get => _InterestRateInMonth;
            set => Set(ref _InterestRateInMonth, value);
        }
        public CreditAccountVM(User user, Client client, Account account)
            : base(user, client, account)
        {
            Sum = CreditAccount.Sum;
            Limit = CreditAccount.Limit;
            InterestRateInMonth = CreditAccount.InterestRateInMonth;
            OkCommand = new RelayCommand(obj => { SaveCommand(); CloseWindow(obj); });
        }

        private void SaveCommand()
        {
            CreditAccount.Sum = Sum;
            CreditAccount.Limit = Limit;
            CreditAccount.InterestRateInMonth = InterestRateInMonth;
            ChangeAccountEventCall();
        }
    }
}
