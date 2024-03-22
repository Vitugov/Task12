using System.Windows;
using Task12.Model.Accounts;
using Task12.Model.Clients;
using Task12.Model.Users;
using Task12.ViewModel.Accounts;

namespace Task12.View.Accounts
{
    /// <summary>
    /// Логика взаимодействия для CreditAccountView.xaml
    /// </summary>
    public partial class CreditAccountView : Window
    {
        public CreditAccountView(User user, Client client, Account account)
        {
            InitializeComponent();
            DataContext = new CreditAccountVM(user, client, account);
        }
    }
}
