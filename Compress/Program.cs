using System;
using System.IO;
using System.IO.Compression;

namespace Compress
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Compress

            using (FileStream fs = new FileStream(@"C:\temp\data.txt", FileMode.Open))
            {
                byte[] dataToRead = ReadBytes(fs);
                using (FileStream fs1 = new FileStream(@"C:\temp\data1.txt", FileMode.CreateNew))
                    using (Stream gs = new GZipStream(fs1, CompressionMode.Compress))
                        gs.Write(dataToRead, 0, dataToRead.Length);
            }

            Console.WriteLine("compression completed");
            Console.WriteLine();

            #endregion Compress

            #region Decompress

            Console.WriteLine("press enter to decompress...");
            Console.ReadLine();

            using (FileStream fs = new FileStream(@"C:\temp\data1.txt", FileMode.Open))
            {
                using (Stream gs = new GZipStream(fs, CompressionMode.Decompress))
                {
                    StreamReader sr = new StreamReader(gs);
                    string data = sr.ReadToEnd();
                    Console.WriteLine(data);
                }
            }

            #endregion Decompress

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        static byte[] ReadBytes(Stream s)
        {
            byte[] dataToRead = new byte[s.Length];

            // this is the total number of bytes read. this will be incremented
            // and eventually will equal the bytes size held by the stream
            int totalBytesRead = 0;

            // this is the number of bytes read in each iteration (i.e. chuck size0
            int chunkBytesRead = 1;

            while (totalBytesRead < dataToRead.Length && chunkBytesRead > 0)
            {
                chunkBytesRead = s.Read(dataToRead, totalBytesRead, dataToRead.Length - totalBytesRead);
                totalBytesRead = totalBytesRead + chunkBytesRead;
            }

            return dataToRead;
        }
    }
}
