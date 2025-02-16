using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP_Server;
 
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, I am a UDP Server");
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        serverSocket.Bind(new IPEndPoint(IPAddress.Loopback, 8080));
        Console.WriteLine("Server is listening on port 8080...");
        byte[] buffer = new byte[1024];
        EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
        while (true)
        {
             
            int receivedBytes = serverSocket.ReceiveFrom(buffer, ref remoteEP);
            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
            IPEndPoint senderEndPoint = (IPEndPoint) remoteEP;
            string senderIP = senderEndPoint.Address.ToString();
            int senderPort = senderEndPoint.Port;
            Console.WriteLine($"Received: {receivedMessage} From: {senderIP}:{senderPort}");
        }
    }
}
 