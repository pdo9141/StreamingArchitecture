using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class StreamAdapterTests
    {
        [TestMethod]
        public void StreamAdapter_Basics_Test()
        {
            // These classes are not streams, they do not implement the Stream class
            // StreamReader has an internal buffer
            //----more data can be read then what is requested
            //----future reads are then served from buffer and not from backing store stream
            // StreamWriter
            //----writes characters into the backing store stream
            // Default encoding used is UTF-8
            // ASCII is simpliest and most efficient since it uses 1 byte for each character
            //----can represent only the first 127 characters of the Unicode set
            //----cannot represent special and non-English characters
            //----for such special and non-English characters use Unicode encoding
            // Unicode Bytes Allocation
            //----UTF-8: represents the first 127 characters with 1 byte each (for ASCII compatibility)
            //--------remaining characters encoded into variable number of bytes (must take care to account for this)
            //----UTF-16: c# char type (16-bits) will always be represented by 2 bytes, seeks will be easier
            // When to use Binary Adapters
            //----reading and writing primitive types from/into a stream
            //----reading and writing binary files such as JPEG
            //----how about reading and writing strings? so how is this different than StreamReader and StreamWriter
            //--------text adapters perform better when dealing with strings (up to 40%)
            //--------because they inherit TextReader/TextWriter, Text adapters seems to be optimized for string reading/writing
            // XML Adapters
            //----XmlRead and XmlWriter are high performance adapters
            //----Forward-only cursors to read/write an XML stream
            //--------forward-only: navigation can be done onwards only
            //----Xml adapters are used when the stream contains XML data
            //----Knowledge of XML is required
            // Closing stream adapters
            //----closing a stream adapter closes the backing store stream
            //----be careful not to resuse a stream after the corresponding adapter is closed
            //----in .NET 4.5 there is an option to keep the stream open (bool leaveOpen) after reader/writer is disposed
        }
    }
}
