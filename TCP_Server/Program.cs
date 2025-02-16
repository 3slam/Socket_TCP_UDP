using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCP_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, I am a TCP Server");
 
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Loopback, 9090));
            serverSocket.Listen(10);
            Console.WriteLine("Server is listening on port 9090...");

            while (true)
            {
                Socket clientSocket = serverSocket.Accept();
                IPEndPoint clientEndPoint = (IPEndPoint)clientSocket.RemoteEndPoint;
                Console.WriteLine($"Client connected: {clientEndPoint.Address}:{clientEndPoint.Port}");
                byte[] buffer = new byte[1024];
                int receivedBytes = clientSocket.Receive(buffer);
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
                Console.WriteLine($"Received: {receivedMessage} From: {clientEndPoint.Address}:{clientEndPoint.Port}");
                clientSocket.Close();
                Console.WriteLine("Client disconnected.");
            }
        }
    }
}