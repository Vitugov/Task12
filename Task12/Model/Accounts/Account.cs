using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task12.Model.Clients;

namespace Task12.Model.Accounts
{
    public abstract class Account
    {
        public abstract string Name { get; }
        internal Client? Client { get; set; }
        public decimal Sum { get; set; }

        

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
