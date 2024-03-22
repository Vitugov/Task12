using System.Windows;
using Task12.Model.Accounts;
using Task12.Model.Clients;
using Task12.Model.Users;
using Task12.ViewModel;

namespace Task12.View
{
    /// <summary>
    /// Логика взаимодействия для TransferView.xaml
    /// </summary>
    public partial class TransferView : Window
    {
        public TransferView(User user, Client client, Account account, bool isAccountToAccountTransfer)
        {
            InitializeComponent();
            DataContext = new TransferVM(user, client, account, isAccountToAccountTransfer);
        }
    }
}
