using System.Windows;
using Task12.Model.Accounts;
using Task12.Model.Clients;
using Task12.Model.Users;
using Task12.ViewModel;

namespace Task12.View
{
    /// <summary>
    /// Логика взаимодействия для TopUpAccountView.xaml
    /// </summary>
    public partial class TopUpAccountView : Window
    {
        public TopUpAccountView(User user, Client client, Account account)
        {
            InitializeComponent();
            DataContext = new TopUpAccountVM(user, client, account);
        }
    }
}
