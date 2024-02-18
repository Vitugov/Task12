using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task12.Model.Clients;

namespace Task12.Model.Accounts
{
    public class CurrentAccount : Account
    {
        public override string Name { get => "Текущий счет №" + Code; }

        public CurrentAccount() : base() { }

        public CurrentAccount(Client client) : base(client) { }
    }
}
