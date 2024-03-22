namespace Task12.Model.Clients
{
    public class Company : Client
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }
        public string ShortDescription { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public override string Type => "Компания";

        public override string Name => ShortName;

        public override string Phone => PhoneNumber;
    }
}
