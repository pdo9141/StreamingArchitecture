using System;
using System.IO;
using System.Net.Sockets;

class Program
{
    static void Main(string[] args)
    {
        TcpClient tcpClient = new TcpClient("localhost", 5200);

        NetworkStream networkStream = tcpClient.GetStream();
        BufferedStream bs = new BufferedStream(networkStream);

        // send data to listener
        byte[] dataToSend = new byte[100];
        new Random().NextBytes(dataToSend);
        for (int i = 0; i < 100; i++)
            networkStream.Write(dataToSend, 0, dataToSend.Length);

        // when the network stream is closed, it also shuts down the connection
        networkStream.Close();

        Console.WriteLine("Done");
        Console.ReadLine();
    }
}
