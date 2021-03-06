﻿using System;
using System.IO;
using System.Xml;

class Program
{
    static void Main(string[] args)
    {
        XmlReaderSettings settings = new XmlReaderSettings();
        settings.IgnoreWhitespace = true;

        using (FileStream fs = new FileStream(@"c:\temp\test.xml", FileMode.Open))
        {
            using (XmlReader xr = XmlReader.Create(fs, settings))
            {
                while (xr.Read())
                {
                    switch (xr.NodeType)
                    {
                        case XmlNodeType.XmlDeclaration:
                            Console.WriteLine(xr.Value);
                            break;
                        case XmlNodeType.Text:
                            Console.WriteLine(xr.Value);
                            break;
                        case XmlNodeType.Element:
                            Console.WriteLine(xr.Name);
                            if (xr.HasAttributes)
                                Console.WriteLine("id= " + xr.GetAttribute(0));
                            break;
                    }
                }
            }
        }

        Console.WriteLine("Done");
        Console.ReadLine();
    }
}

