using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Client
    {
        bool authenticated = false;

        public bool Authenticated
        {
            get { return authenticated; }
            set { authenticated = value; }
        }
        string login = "guest";
        public string Login
        {
            get { return login; }
            set { login = value; }
        }
        public Client() { }
        public void authenticate(string message)
        {
            authenticated = true;
            login = message.Split(' ')[1];
        }
    }
}

