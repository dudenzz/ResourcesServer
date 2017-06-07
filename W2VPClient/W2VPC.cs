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
        public delegate void RecieveModelDelegate(string modelName);
        public delegate void UpdateReadingProgressDelegate(int current, int size);
        #endregion
        #region events
        public event AuthDelegate Auth;
        public event LogoutDelegate Logout_event;
        public event RecieveModelDelegate RecieveModel;
        public event RecieveModelDelegate RecieveQuestion;
        public event UpdateReadingProgressDelegate UpdateReadingProgress;
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
                                    mainClient = new TcpClient();
                                    break;
                                case "MODEL":
                                    RecieveModel(message.Split()[1]);
                                    break;
                                case "QUESTIONS":
                                    RecieveQuestion(message.Split()[1]);
                                    break;
                                case "READING":
                                    UpdateReadingProgress(int.Parse(message.Split(' ')[1]), int.Parse(message.Split(' ')[2]));
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
        public void GetModels()
        {
            byte[] buffer = new byte[255];
            stream.Write(toByteArray("LISTMODELS"), 0, 255);
        }
        public void ReadModel(string modelName)
        {
            byte[] buffer = new byte[255];
            stream.Write(toByteArray("READMODEL "+modelName), 0, 255);
        }

        public void SendCustomMessage(string Message)
        {
            stream.Write(toByteArray(Message), 0, 255);
        }

        public void GetQuestionSets()
        {
            byte[] buffer = new byte[255];
            stream.Write(toByteArray("LISTQUESTIONS"), 0, 255);
        }
    }
}
