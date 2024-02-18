using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task12.Model.Accounts
{
    public class CreditAccount : Account
    {
        public decimal Limit { get; set; }
        public override string Name { get => "Credit account"; }
    }
}
