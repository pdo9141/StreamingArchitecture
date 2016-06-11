using System;
using System.IO;
using System.Diagnostics;
using System.Net.Sockets;

namespace DecoratorClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
                 
            TcpClient tcpClient = new TcpClient("localhost", 5200);
            NetworkStream networkStream = tcpClient.GetStream();

            Console.WriteLine("Connected");
            Console.WriteLine("");

            // create random data to send to the server
            byte[] dataToSend = new byte[100];
            new Random().NextBytes(dataToSend);

            Console.WriteLine("Sending non-buffered data... ");
            sw.Start();

            for (int i = 0; i < 400000; i++)
                networkStream.Write(dataToSend, 0, dataToSend.Length);

            sw.Stop();
            Console.WriteLine("Non-buffered: {0}", sw.Elapsed);
            sw.Reset();

            // send buffered data
            Console.WriteLine("Sending buffered data... ");

            BufferedStream bs = new BufferedStream(networkStream);
            sw.Start();

            for (int i = 0; i < 400000; i++)
                bs.Write(dataToSend, 0, dataToSend.Length);

            sw.Stop();
            Console.WriteLine("Buffered: {0}", sw.Elapsed);

            bs.Close();
            
            Console.ReadLine();
        }
    }
}
