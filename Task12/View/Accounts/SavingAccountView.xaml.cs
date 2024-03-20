﻿using System;
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
