using System;
using System.IO.Pipes;
using System.Text;
using System.Threading;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //ByteTransmission();
            MessageTransmission();
        }

        private static void MessageTransmission()
        {
            using (NamedPipeServerStream s = new NamedPipeServerStream("n1", 
                PipeDirection.InOut,
                1,
                PipeTransmissionMode.Message))
            {
                s.WaitForConnection();

                int counter = 1;
                StringBuilder message = new StringBuilder();
                string chunk = "";

                byte[] buffer = new byte[10]; // Read in blocks

                do {
                    s.Read(buffer, 0, buffer.Length);

                    chunk = Encoding.UTF8.GetString(buffer);
                    message.Append(chunk);

                    Console.WriteLine("chunk " + counter.ToString()
                        + " read: " + chunk);
                    counter = counter + 1;

                    Array.Clear(buffer, 0, buffer.Length);

                    Thread.Sleep(3000);
                }
                while (!s.IsMessageComplete);

                Console.WriteLine(message.ToString());
                Console.WriteLine("");
                Console.WriteLine("press enter to finish");
                Console.ReadLine();    
            }
        }

        private static void ByteTransmission()
        {
            using (NamedPipeServerStream server = new NamedPipeServerStream("pipe1"))
            {
                server.WaitForConnection();

                Console.WriteLine("connection established");
                Console.WriteLine("press enter to write a byte");
                Console.ReadLine();

                server.WriteByte(234);

                Console.WriteLine("server wrote byte into pipe");
                Console.WriteLine("press enter to read incoming byte from client");
                Console.ReadLine();

                Console.WriteLine(server.ReadByte());
            }

            Console.WriteLine("end");
            Console.ReadLine();
        }
    }
}
