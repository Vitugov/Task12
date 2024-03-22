using System.Windows;
using Task12.Model.Accounts;
using Task12.Model.Clients;
using Task12.Model.Users;
using Task12.ViewModel.Accounts;

namespace Task12.View.Accounts
{
    /// <summary>
    /// Логика взаимодействия для CurrentAccountView.xaml
    /// </summary>
    public partial class CurrentAccountView : Window
    {
        public CurrentAccountView(User user, Client client, Account account)
        {
            InitializeComponent();
            DataContext = new CurrentAccountVM(user, client, account);
        }
    }
}
