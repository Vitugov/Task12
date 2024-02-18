﻿using System;
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
        internal long Code { get; set; }
        public decimal Sum { get; set; }

        

        internal Account(Client client) : this()
        {
            Client = client;
        }

        internal Account()
        {
            var rnd = new Random();
            Sum = 0;
            Code = rnd.NextInt64();
        }
    }
}
