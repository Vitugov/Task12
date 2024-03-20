using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    internal class AccountEventArgs : EventArgs
    {
        internal DateTime Time { get; }
        internal AccountOperation Operation { get; }
        internal User User { get; }
        internal Client Client { get; }
        internal Account Account { get; }
        internal decimal? Sum { get; }
        internal Client? CounterpartyClient { get; }
        internal Account? CounterpartyAccount { get; }
        internal AccountEventArgs(
            AccountOperation operation,
            User user,
            Client client,
            Account account,
            decimal? sum = null,
            Client? counterpartyClient = null,
            Account? counterpartyAccount = null)
        {
            Time = DateTime.Now;
            Operation = operation;
            User = user;
            Client = client;
            Account = account;
            Sum = sum;
            CounterpartyClient = counterpartyClient;
            CounterpartyAccount = counterpartyAccount;
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
