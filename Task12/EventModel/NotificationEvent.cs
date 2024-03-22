using Task12.Model.Accounts;
using Task12.Model.Clients;
using Task12.Model.Users;

namespace Task12.EventModel
{
    //internal static class NotificationEvent
    //{

    //    //internal event Action CloseAccountEvent; // время, юзер, клиент, аккаунт
    //    //internal event Action TopUpEvent;       // время, юзер, клиент, аккаунт, сумма пополнения
    //    //internal event Action FundsExpenditureEvent;    // время, юзер, клиент, аккаунт, контрагент, аккаунт контрагента, сумма превода
    //    //internal event Action FundsReceiptEvent;        // время, юзер, клиент, аккаунт, контрагент, аккаунт контрагента, сумма превода
    //    //internal event Action ChangeAccountDetailsEvent;    //время, юзер, клиент, аккаунт
    //}

    public class AccountEventArgs : EventArgs
    {
        public DateTime DateTime { get; }
        public AccountOperation Operation { get; }
        public User User { get; }
        public Client Client { get; }
        public Account Account { get; }
        public decimal? Sum { get; }
        public Client? CounterpartyClient { get; }
        public Account? CounterpartyAccount { get; }
        public AccountEventArgs(
            AccountOperation operation,
            User user,
            Client client,
            Account account,
            decimal? sum = null,
            Client? counterpartyClient = null,
            Account? counterpartyAccount = null)
        {
            DateTime = DateTime.Now;
            Operation = operation;
            User = user;
            Client = client;
            Account = account;
            Sum = sum;
            CounterpartyClient = counterpartyClient;
            CounterpartyAccount = counterpartyAccount;
        }
    }
    public class NotificationMessage
    {
        public DateTime DateTime { get; set; }
        public AccountOperation Operation { get; set; }
        public string Message { get; set; }

        public NotificationMessage() { }
        public NotificationMessage(AccountEventArgs args)
        {
            DateTime = args.DateTime;
            Operation = args.Operation;
            switch (Operation)
            {
                case AccountOperation.Open:
                    Message = args.User.Name + ": " + DateTime.ToString("dd.MM.yyyy HH:mm") + ". Для клиента: " + args.Client.Name +
                        " открыт счет: " + args.Account.Name + ".";
                    break;
                case AccountOperation.Close:
                    Message = args.User.Name + ": " + DateTime.ToString("dd.MM.yyyy HH:mm") + ". Для клиента: " + args.Client.Name +
                        " закрыт счет: " + args.Account.Name + ".";
                    break;
                case AccountOperation.Change:
                    Message = args.User.Name + ": " + DateTime.ToString("dd.MM.yyyy HH:mm") + ". Для клиента: " + args.Client.Name +
                        " изменен счет: " + args.Account.Name + ".";
                    break;
                case AccountOperation.TopUp:
                    Message = args.User.Name + ": " + DateTime.ToString("dd.MM.yyyy HH:mm") + ". Для клиента: " + args.Client.Name +
                        " пополнен счет: " + args.Account.Name + " на сумму: " + args.Sum.ToString() + ".";
                    break;
                case AccountOperation.Expenditure:
                    Message = args.User.Name + ": " + DateTime.ToString("dd.MM.yyyy HH:mm") + ". Для клиента: " + args.Client.Name +
                        " выполнено перечисление со счета: " + args.Account.Name + " на сумму: " + args.Sum.ToString() +
                        ", на имя: " + args.CounterpartyClient.Name + "(" + args.Account.Name + ").";
                    break;
                case AccountOperation.Receipt:
                    Message = args.User.Name + ": " + DateTime.ToString("dd.MM.yyyy HH:mm") + ". Для клиента: " + args.Client.Name +
                        " получены средства на счета: " + args.Account.Name + " на сумму: " + args.Sum.ToString() +
                        ", от: " + args.CounterpartyClient.Name + "(" + args.Account.Name + ").";
                    break;
                default:
                    throw new ArgumentException("Unknown operation");
            }
        }


    }

    public enum AccountOperation
    {
        Open,
        Close,
        TopUp,
        Expenditure,
        Receipt,
        Change
    }
}
