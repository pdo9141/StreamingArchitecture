using System;
using System.IO;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        byte[] data = Encoding.UTF8.GetBytes("ABCDE");
        using (Stream ms = new MemoryStream(data))
        {
            using (StreamReader sr = new StreamReader(ms))
            {
                char[] c = new char[3];
                sr.Read(c, 0, c.Length);                // more data is read and stored in reader's buffer
                Console.WriteLine(c);                   // this will print ABC       

                ms.Position = 4;                        // you're not setting position in stream here, it's actually from in-memory buffer!
                //sr.DiscardBufferedData();             // call discard method to work directly with stream to get accurate results
                Console.WriteLine((char)sr.Read());     // you expect to see "E" here but unless you call DiscardBufferedData(), you'll see "D" since reading from buffer
            }
        }

        Console.WriteLine("Done");
        Console.ReadLine();
    }
}

