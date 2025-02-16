using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
 

class UDPExample
{
    static void Main()
    {
        Thread serverThread = new Thread(new ThreadStart(UDPServer));
        serverThread.Start();

        Thread.Sleep(1000);  

        UDPClient();
    }

    // UDP Server
    static void UDPServer()
    {
        // Create UDP socket
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        // Bind to a local endpoint (IP + Port)
        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 8080);
        serverSocket.Bind(serverEndPoint);

        Console.WriteLine("UDP Server is listening on port 8080...");

        byte[] buffer = new byte[1024];
        EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

        while (true)
        {
            int receivedBytes = serverSocket.ReceiveFrom(buffer, ref remoteEP);
            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
            Console.WriteLine($"Received from {remoteEP}: {receivedMessage}");

            // Send response
            string response = "Hello from UDP Server!";
            byte[] responseData = Encoding.UTF8.GetBytes(response);
            serverSocket.SendTo(responseData, remoteEP);
        }
    }

    // UDP Client
    static void UDPClient()
    {
        // Create UDP socket
        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        // Server address
        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 8080);

        string message = "Hello from UDP Client!";
        byte[] sendData = Encoding.UTF8.GetBytes(message);

        // Send data to the server
        clientSocket.SendTo(sendData, serverEndPoint);
        Console.WriteLine("Client sent: " + message);

        // Receive response
        byte[] buffer = new byte[1024];
        EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
        int receivedBytes = clientSocket.ReceiveFrom(buffer, ref remoteEP);
        string receivedMessage = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
        Console.WriteLine("Client received: " + receivedMessage);

        clientSocket.Close();
    }
}
