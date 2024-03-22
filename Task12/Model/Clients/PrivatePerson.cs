namespace Task12.Model.Clients
{
    public class PrivatePerson : Client
    {
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Surname { get; set; }
        public string INN { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public override string Type => "Частное лицо";

        public override string Name => Surname + " " + FirstName + " " + Patronymic;

        public override string Phone => PhoneNumber;
    }
}
