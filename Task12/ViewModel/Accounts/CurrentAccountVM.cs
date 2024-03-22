using Task12.Model.Accounts;
using Task12.Model.Clients;
using Task12.Model.Users;
using WPFUsefullThings;

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

        private void SaveCommand() { }
    }
}
