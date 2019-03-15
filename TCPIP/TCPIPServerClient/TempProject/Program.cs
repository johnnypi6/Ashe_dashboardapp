using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using TCPServer;

namespace TempProject
{
    class Program
    {
        static Server svr;

        static void Main(string[] args)
        {
            //Create HostServer
            svr = new Server();
            int MyPort = 9999;

            svr.Listen(MyPort);
            svr.OnReceiveData += new Server.ReceiveDataCallback(OnDataReceived);
            svr.OnClientConnect += new Server.ClientConnectCallback(NewClientConnected);
            svr.OnClientDisconnect += new Server.ClientDisconnectCallback(ClientDisconnect);

            Console.ReadLine();
        }

        private static void OnDataReceived(int clientNumber, byte[] message, int messageSize)
        {
            string str = Encoding.ASCII.GetString(message).Trim(new char[] { '\0' });
            Console.WriteLine(clientNumber);
            Console.WriteLine(str);
            svr.SendMessage(Encoding.ASCII.GetBytes(str));
        }

        private static void NewClientConnected(int ConnectionID)
        {
            int i = 0;
        }

        private static void ClientDisconnect(int clientNumber)
        {
            int i = 0;
        }
    }
}
