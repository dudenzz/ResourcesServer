using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class W2VServer
    {
        Thread listeningThread;
        TcpListener listener;

        public bool isAlive;
        IPAddress getLocalIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }
            throw new Exception("Could not retrieve a local IP Address");
        }
        void listen()
        {

        }
        void send(NetworkStream stream, string message)
        {
            byte[] buffer = new byte[255];
            for(int i = 0; i<255; i++)
                if (i<message.Length)
                    buffer[i] = (byte)message[i];
                else
                    buffer[i] = 0;
            stream.Write(buffer, 0, 255);
        }
        public W2VServer()
        {
            isAlive = true;
            listener = new TcpListener(getLocalIP(), 8080);
            listeningThread = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                listener.Start();
                Console.WriteLine("Listening on " +getLocalIP().ToString() + " : 8080");
                while (isAlive)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    new Thread(new ParameterizedThreadStart(HandleClient)).Start(client);
                }
            });
            listeningThread.Start();
        }
        public void Stop()
        {
            listener.Stop();
            isAlive = false;
        }

        delegate void HandleClientDelegate(TcpClient client);
        void HandleClient(object wrappedClient)
        {
            TcpClient client = (TcpClient)wrappedClient;
            NetworkStream stream = client.GetStream();
            Console.WriteLine("Connected");
            bool clientAlive = true;
            Client client_abs = new Client();
            while(clientAlive)
            {
                if(stream.DataAvailable)
                {
                    byte[] buffer = new byte[255];
                    stream.Read(buffer, 0, 255);
                    string backMessage;
                    W2VP.MessageInterpretations message = W2VP.interpretMessage(convertData(buffer),buffer,client_abs.Authenticated);
                    switch(message)
                    {
                        case W2VP.MessageInterpretations.UNAUTH:
                            Console.WriteLine("Someone tried to perform an unauthorized command");
                            backMessage = "UNAUTH";
                            send(stream, backMessage);
                            break;
                        case W2VP.MessageInterpretations.AUTH:
                            client_abs.authenticate(convertData(buffer));
                            Console.WriteLine(client_abs.Login + " has successfully logged in");
                            backMessage = "AUTH";
                            send(stream, backMessage);
                            break;
                        case W2VP.MessageInterpretations.NOAUTH:
                            Console.WriteLine("Someone tried to login with login " + convertData(buffer).Split(' ')[1] + " but failed");
                            backMessage = "NO_AUTH";
                            send(stream, backMessage);
                            break;
                        case W2VP.MessageInterpretations.INVALID:
                            Console.WriteLine("Recieved invalid message");
                            backMessage = "INVALID";
                            send(stream, backMessage);
                            break;
                        case W2VP.MessageInterpretations.END:
                            if (client_abs.Authenticated)
                            {
                                Console.WriteLine(client_abs.Login + " forced server termination");
                                clientAlive = false;
                                this.isAlive = false;
                                backMessage = "ACK";
                            }
                            else
                            {
                                Console.WriteLine("guest tried to force server termination");
                                backMessage = "NO_AUTH";
                            }
                            send(stream, backMessage);
                            break;
                        case W2VP.MessageInterpretations.LOGOUT:
                            backMessage = "ACK";
                            Console.WriteLine(client_abs.Login + " logged out");
                            clientAlive = false;
                            send(stream, backMessage);
                            break;
                        case W2VP.MessageInterpretations.LIST:
                            backMessage = "MODELLIST ";
                            foreach (string model in W2VP.Models.Keys)
                                backMessage += model + " ";
                            send(stream, backMessage);
                            break;
                        case W2VP.MessageInterpretations.READ:
                            backMessage = "READING";
                            Console.Write("Started reading a corpus, blocked reading command");
                            send(stream, backMessage);
                            break;
                        case W2VP.MessageInterpretations.NOREAD:
                            Console.Write("Someone tried to start a new model reading, but that command is blocked");
                            backMessage = "BLOCKED";
                            send(stream, backMessage);
                            break;
                        default:
                            Console.WriteLine("Unimplemented request recieved");
                            break;
                    }
                }
                Thread.Sleep(1000);
            }
        }

        string convertData(byte [] table)
        {
            string recieved = "";
            foreach(byte b in table)
            {
                if(b!=0)
                    recieved += (char)(b);
            }
            return recieved;
        }
    }
}

