using System;
using System.IO;
using System.Xml;

class Program
{
    static void Main(string[] args)
    {
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;

        using (FileStream fs = new FileStream(@"c:\temp\test1.xml", FileMode.Create))
        {
            using (XmlWriter xw = XmlWriter.Create(fs, settings))
            {
                xw.WriteStartElement("Books");
                xw.WriteStartElement("Book");
                xw.WriteAttributeString("id", "1");
                xw.WriteElementString("Title", "Streaming in .NET");
                xw.WriteElementString("Category", "IT");
            }
        }

        Console.WriteLine("Done");
        Console.ReadLine();
    }
}

