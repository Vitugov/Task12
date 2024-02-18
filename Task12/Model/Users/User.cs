using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task12.Model.Clients;

namespace Task12.Model.Users
{
    public class User
    {
        internal List<Client> GetClientsList()
        {
            return DataStorage.Current.GetClients();
        }
    }
}
