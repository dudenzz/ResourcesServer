using System;
using System.Net;
using System.Net.Sockets;
 
public class HelloWorld
{
    static public void Main ()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                   Console.WriteLine(ip.ToString());
                }
            }
    }
}
