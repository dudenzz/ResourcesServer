using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Client class contains basic informations about connected Client such as his current authentification status or his name
    /// </summary>
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

