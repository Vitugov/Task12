using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Task12.EventModel;
using Task12.Model.Serialization;
using Task12.Model.Users;
using Task12.ViewModel;

namespace Task12
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var manager = new Manager();
            var vm = new MainVM(manager);
            DataContext = vm;
            this.Activated += vm.WindowActivated;
            this.Closing += MainWindow_Closing;
            manager.AccountEvent += OnAccountEvent;

        }
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            (this.DataContext as MainVM)?.OnWindowClosing();
        }

        private void OnAccountEvent(object sender, AccountEventArgs e)
        {
            var message = new NotificationMessage(e);
            Dispatcher.Invoke(() =>
            {
                MessageBox.Show(this, message.Message, "Message Received", MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }
    }
}