using System.Windows;
using Task12.Model.Accounts;
using Task12.Model.Clients;
using Task12.Model.Users;
using Task12.ViewModel.Accounts;

namespace Task12.View.Accounts
{
    /// <summary>
    /// Логика взаимодействия для SavingAccountView.xaml
    /// </summary>
    public partial class SavingAccountView : Window
    {
        public SavingAccountView(User user, Client client, Account account)
        {
            InitializeComponent();
            DataContext = new SavingAccountVM(user, client, account);
        }
    }
}
