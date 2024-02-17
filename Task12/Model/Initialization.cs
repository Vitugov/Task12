using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task12.Model.Clients;
using Task12.Model.Users;

namespace Task12.Model
{
    internal static class Initialization
    {
        internal static void Start(Manager manager)
        {
            for (int i = 0; i < 10; i++)
            {
                var client = manager.AddClient<Client>();
                client.Name = "Client_" + i;
            }
        }
    }
}
