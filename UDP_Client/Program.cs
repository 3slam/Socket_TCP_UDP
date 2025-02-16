using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("UDP Client Started...");
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
            while (true)
            {
                Console.Write("Enter message (type 'exit' to quit): ");
                string message = Console.ReadLine();

                if (message.ToLower() == "exit") break;

                byte[] sendData = Encoding.UTF8.GetBytes(message);
 
                clientSocket.SendTo(sendData, serverEndPoint);
                Console.WriteLine($"Sent: {message}");
  
            }
            clientSocket.Close();
            Console.WriteLine("UDP Client Closed.");
        }
    }
}