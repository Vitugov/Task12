using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task12.Model.Clients
{
    public abstract class Client
    {
        public abstract string Type { get; }
        public abstract string Name { get; }
        public abstract string Phone { get; }

        public Client()
        {

        }

        public override string ToString()
        {
            return Name;
        }
    }
}
