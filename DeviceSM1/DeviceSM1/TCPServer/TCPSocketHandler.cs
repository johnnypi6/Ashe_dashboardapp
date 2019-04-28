using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceSM1.TCPServer
{
    public interface TCPSocketHandler
    {
        void OnClientConnected(int clientNumber);
        void OnClientDisconnected(int clientNumber);
        void OnReceivedData(int clientNumber, byte[] message, int messageSize);
    }
}
