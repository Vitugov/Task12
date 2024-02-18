using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task12.Model.Clients;

namespace Task12.Model.Accounts
{
    public class CreditAccount : Account
    {
        public decimal Limit { get; set; }
        public override string Name { get => "Кредитный счет №" + Code; }

        public CreditAccount() : base() { }

        public CreditAccount(Client client) : base(client) { }
    }
}
