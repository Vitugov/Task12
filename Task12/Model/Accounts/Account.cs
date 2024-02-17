using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task12.Model.Clients;

namespace Task12.Model.Accounts
{
    public class Account
    {
        internal Client? Client { get; set; }
        internal decimal Sum { get; set; }

        internal Account(Client client)
        {
            Client = client;
            Sum = 0;
        }

        internal Account()
        {
            Sum = 0;
        }
    }
}
