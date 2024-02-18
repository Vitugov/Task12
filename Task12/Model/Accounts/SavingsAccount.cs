using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task12.Model.Clients;

namespace Task12.Model.Accounts
{
    public class SavingsAccount : Account
    {
        public override string Name { get => "Сберегательный счет №" + Code; }

        public SavingsAccount() : base() { }

        public SavingsAccount(Client client) : base(client) { }
    }
}
