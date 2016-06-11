using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        int intVal = 3;
        string stringVal = "abcde";

        using (FileStream fs = new FileStream(@"c:\temp\binary.text", FileMode.Create))
        {
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                bw.Write(intVal);
                bw.Write(stringVal);
            }
        }

        using (FileStream fs = new FileStream(@"c:\temp\binary.text", FileMode.Open))
        {
            using (BinaryReader bw = new BinaryReader(fs))
            {
                int var = bw.ReadInt16();           // int types reserve 32 bits, here you get the right int value but reading below is corrupt
                //int var = bw.ReadInt32();         // uncomment then ReadString below will correctly return "abcde"
                //byte b = bw.ReadByte();           // this will return 5 for length of "abcde", ReadString will fail below though since position is now corrupt
                string var2 = bw.ReadString();
            }
        }

        Console.WriteLine("Done");
        Console.ReadLine();
    }
}

