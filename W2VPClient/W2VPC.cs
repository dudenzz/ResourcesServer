using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace W2VPClient
{
    public class W2VPC
    {
        TcpClient mainClient;
        IPAddress serverIP;
        NetworkStream stream;
        int port;
        string clientName;

        #region delegates
        public delegate void AuthDelegate(string login);
        public delegate void LogoutDelegate();
        #endregion
        #region events
        public event AuthDelegate Auth;
        public event LogoutDelegate Logout_event;
        #endregion
        public W2VPC(IPAddress ip, int port)
        {
            serverIP = ip;
            this.port = port;
            mainClient = new TcpClient();  
        }
        public bool Connect()
        {
            
            try
            {
                mainClient.Connect(serverIP,port);
                stream = mainClient.GetStream();
                Thread listener = new Thread(() =>
                {
                    bool alive = true;
                    byte[] buffer = new byte[255];
                    while (alive)
                    {
                        if (stream.DataAvailable)
                        {
                            stream.Read(buffer, 0, 255);
                            string message = byteToString(buffer);
                            switch (message.Split(' ')[0])
                            {
                                case "AUTH":
                                    Auth(message.Split(' ')[1]);
                                    clientName = message.Split(' ')[1];
                                    break;
                                case "NO_AUTH":
                                    Auth("guest");
                                    clientName = "guest";
                                    break;
                                case "LOGOUT":
                                    Logout_event();
                                    alive = false;
                                    stream.Close();
                                    mainClient.Close();
                                    break;
                            }
                        }
                    }
                });
                listener.Start();
                return true;
            }
            catch
            {
                return false;
            }
        }
        byte[] toByteArray(string message)
        {
            byte[] buffer = new byte[255];
            for (int i = 0; i < 255; i++)
                if (i < message.Length)
                    buffer[i] = (byte)message[i];
                else
                    buffer[i] = 0;
            return buffer;
        }
        string byteToString(byte[] array)
        {
            string ret = "";
            for (int i = 0; i < 255; i++)
                if (array[i] != 0)
                    ret += (char)array[i];
            return ret;
        }
        public void Login(string login, string password)
        {
            
            MD5 hasher = MD5.Create();
            byte[] pwd = hasher.ComputeHash(toByteArray(password));
            byte[] buffer = new byte[255];
            buffer = toByteArray("LOGIN "+login+" ");
            for (int i = 155; i < 155 + 16; i++)
                buffer[i] = pwd[i - 155];
            stream.Write(buffer,0,255);
 
        }
        public void Logout()
        {
            byte[] buffer = new byte[255];
            stream.Write(toByteArray("LOGOUT"),0,255);

        }
    }
}
