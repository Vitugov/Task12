using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
