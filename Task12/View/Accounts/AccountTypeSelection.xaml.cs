using System.Windows;
using Task12.Model.Clients;
using Task12.Model.Users;
using Task12.ViewModel.Accounts;

namespace Task12.View.Accounts
{
    /// <summary>
    /// Логика взаимодействия для AccountTypeSelection.xaml
    /// </summary>
    public partial class AccountTypeSelection : Window
    {
        public AccountTypeSelection(User user, Client client)
        {
            InitializeComponent();
            DataContext = new TypeSelectionVM(user, client);
        }
    }
}
