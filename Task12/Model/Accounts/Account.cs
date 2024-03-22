using Task12.Model.Clients;

namespace Task12.Model.Accounts
{
    public abstract class Account
    {
        public abstract string Name { get; }
        internal Client? Client { get; set; }
        internal long Code { get; set; }
        public decimal Sum { get; set; }
        public abstract decimal Minimum { get; }



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

        public override string ToString()
        {
            return Name;
        }
    }
}
