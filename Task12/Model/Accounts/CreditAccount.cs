using Task12.Model.Clients;

namespace Task12.Model.Accounts
{
    public class CreditAccount : Account
    {
        public override string Name { get => "Кредитный счет №" + Code; }
        public decimal Limit { get; set; }

        public decimal InterestRateInMonth { get; set; }

        public override decimal Minimum => Limit;

        public CreditAccount() : base() { }

        public CreditAccount(Client client) : base(client) { }
    }
}
