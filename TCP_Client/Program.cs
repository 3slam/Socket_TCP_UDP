using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCP_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("TCP Client Started...");
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9090));
            Console.WriteLine("Connected to the server.");
            while (true)
            {
                Console.Write("Enter message (type 'exit' to quit): ");
                string message = Console.ReadLine();
                if (message.ToLower() == "exit") break;
                byte[] sendData = Encoding.UTF8.GetBytes(message);
                clientSocket.Send(sendData);
                Console.WriteLine($"Sent: {message}");
            }
            clientSocket.Close();
            Console.WriteLine("TCP Client Closed.");
        }
    }
}