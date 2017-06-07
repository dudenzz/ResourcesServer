using ResourcesServerGit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class W2VP
    {
        static Dictionary<string, string> logins;
        static Dictionary<string, string> models;

        public static Dictionary<string, string> Models
        {
            get { return W2VP.models; }
            set { W2VP.models = value; }
        }
        static bool blockModelRead = false;
        static Model model;
        static W2VP()
        {
            model = new Model();
            logins = new Dictionary<string, string>();
            foreach (string l in File.ReadAllLines("logins.txt"))
                logins.Add(l.Split(' ')[0], l.Split(' ')[1]);
            models = new Dictionary<string, string>();
            foreach (string l in File.ReadAllLines("models.txt"))
                models.Add(l.Split(' ')[0], l.Split(' ')[1]);
        }
        public enum MessageInterpretations
        {
            AUTH,
            NOAUTH,
            UNAUTH,
            INVALID,
            END,
            LOGOUT,
            READ,
            NOREAD,
            LIST
        }
        static byte[] toByteArray(string message)
        {
            byte[] buffer = new byte[255];
            for (int i = 0; i < 255; i++)
                if (i < message.Length)
                    buffer[i] = (byte)message[i];
                else
                    buffer[i] = 0;
            return buffer;
        }
        public static MessageInterpretations interpretMessage(string message, byte[] message_bytes, bool auth)
        {
            switch(message.Split(' ')[0])
            {
                case "LOGIN":
                    Console.WriteLine(message.Split(' ')[1] + " logging in");
                    if (logins.Keys.Contains(message.Split(' ')[1]))
                    {

                        byte[] password = new byte[16];
                        for (int i = 0; i < 16; i++)
                            if (i < 16) password[i] = (byte)message_bytes[i + 155];
                            else password[i] = 0;
                        MD5 hasher = MD5.Create();
                        byte[] act_password = hasher.ComputeHash(toByteArray(logins[message.Split(' ')[1]]));
                        bool correct = true;
                        for (int i = 0; i < 16; i++)
                            if (act_password[i] != password[i])
                            {
                                correct = false;
                                return MessageInterpretations.NOAUTH;
                            }
                        if (correct)
                            return MessageInterpretations.AUTH;
                    }
                    else
                        return MessageInterpretations.NOAUTH;
                    break;
                case "END":
                    return MessageInterpretations.END;
                case "LOGOUT":
                    return MessageInterpretations.LOGOUT;
                case "READMODEL":
                    if (!auth) return MessageInterpretations.UNAUTH;
                    if (blockModelRead) return MessageInterpretations.NOREAD;
                    blockModelRead = true;
                    new Thread(() =>
                    {
                        model.read10000 += model_read10000;
                        model.read(models[message.Split(' ')[1]]);               
                        blockModelRead = false;
                    }).Start();
                    return MessageInterpretations.READ;
                case "LISTMODELS":
                    return MessageInterpretations.LIST;
                case "READQUESTIONS":
                    QuestionBase qb = new QuestionBase("../../data/questions", QuestionBase.QuestionTypes.ESL);
                    break;
                default:
                    return MessageInterpretations.INVALID;
            }
            return MessageInterpretations.INVALID;
        }
        static public long messageCount = 0;
        static public string messageToAll = "";
        static void model_read10000(int current, int size)
        {
            messageCount += 1;
            messageToAll = "READING " + current.ToString() + " " + size.ToString();
        }
        
    }
}

