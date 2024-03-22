using Task12.Model.Clients;

namespace Task12.Model.Accounts
{
    public class CurrentAccount : Account
    {
        public override string Name { get => "Текущий счет №" + Code; }

        public override decimal Minimum => 0;
        public CurrentAccount() : base() { }

        public CurrentAccount(Client client) : base(client) { }
    }
}
