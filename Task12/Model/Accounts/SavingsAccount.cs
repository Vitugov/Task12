using Task12.Model.Clients;

namespace Task12.Model.Accounts
{
    public class SavingsAccount : Account
    {
        public override string Name { get => "Сберегательный счет №" + Code; }

        public decimal MinSum { get; set; }
        public decimal InterestRateInMonth { get; set; }
        public override decimal Minimum => MinSum;
        public SavingsAccount() : base() { }

        public SavingsAccount(Client client) : base(client) { }
    }
}
