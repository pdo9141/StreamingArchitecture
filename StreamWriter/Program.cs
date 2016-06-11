using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        using (Stream ms = new MemoryStream())
        {
            using (StreamWriter sw = new StreamWriter(ms))
            {
                sw.Write('A');
                sw.Write('B');

                long x = sw.BaseStream.Position;    // you would expect pointer to be == 2 here but it is not
                sw.Flush();                         // you need to call Flush
                x = sw.BaseStream.Position;         // now position is 2
            }
        }

        Console.WriteLine("Done");
        Console.ReadLine();
    }
}

