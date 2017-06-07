using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class BasicsTests
    {
        [TestMethod]
        public void Stream_Basics_Test()
        {
            // Backing Stores
            //----FileStream, file system
            //----MemoryStream, memory
            //----NetworkStream, TCP, sockets
            // Decorator Streams
            //----GZipStream
            //----CryptoStream
            //----BufferedStream
            //----DeflateStream
            //----AuthenticatedStream
            // Stream Adapters
            //----StreamReader/StreamWriter
            //----BinaryReader/BinaryWriter
            //----XmlReader/XmlWriter
        }

        [TestMethod]
        public void Unreliable_Read_Test()
        {
            MemoryStream stream = new MemoryStream();
            byte[] dataToRead = new byte[stream.Length];

            // there is no guarantee that the stream will actually read all data at once
            // as specified in the count parameter (dataToRead.Length
            int bytesRead = stream.Read(dataToRead, 0, dataToRead.Length);
        }

        [TestMethod]
        public void Proper_Read_Test()
        {
            // dataToRead will hold the data read from the stream
            MemoryStream stream = new MemoryStream();
            byte[] dataToRead = new byte[stream.Length];

            // this is the total number of bytes read. this will be incremented
            // and eventually will equal the bytes size held by the stream
            int totalBytesRead = 0;

            // this is the number of bytes read in each iteration (i.e. chuck size0
            int chunkBytesRead = 1;

            while (totalBytesRead < dataToRead.Length && chunkBytesRead > 0)
            {
                chunkBytesRead = stream.Read(dataToRead, totalBytesRead, dataToRead.Length - totalBytesRead);
                totalBytesRead = totalBytesRead + chunkBytesRead;
            }

            //return dataToRead;
        }

        [TestMethod]
        public void Get_Length_Of_NonSeekable_Stream_Test()
        {
            // Read the entire stream
            // Store the stream in a buffer (ex: memory)
            // Query the length of this buffer
        }

        [TestMethod]
        public void Position_Test()
        {
            MemoryStream stream = new MemoryStream();
            stream.Seek(300, SeekOrigin.Begin);
            long position = stream.Position; // 300
            stream.Seek(200, SeekOrigin.Current);
            position = stream.Position; // 500
        }

        [TestMethod]
        public void Position_Example_Test()
        {
            using (FileStream fs = File.Create(@"C:\temp\testPosition.txt"))
            {
                // position is 0
                long pos = fs.Position;

                // sets the position to 1
                fs.Position = 1;

                byte[] arrbytes = { 100, 101 };
                // writes the content of arrbytes into current position - which is 1
                fs.Write(arrbytes, 0, arrbytes.Length);
                // position is now 3 as its advanced by write
                pos = fs.Position;

                // YOU NEED TO EXPLICITLY SET POSITION BACK TO 0 HERE
                // ELSE READBYTES WILL START READING AT POSITION 3
                // HERE THOUGH, YOU NEED TO SET POSITION TO 1
                // SINCE WE STARTED WRITING AT 1
                fs.Position = 0;
                byte[] readdata1 = ReadBytes(fs);
            }
        }

        private byte[] ReadBytes(Stream stream)
        {
            byte[] dataToRead = new byte[stream.Length];

            // this is the total number of bytes read. this will be incremented
            // and eventually will equal the bytes size held by the stream
            int totalBytesRead = 0;

            // this is the number of bytes read in each iteration (i.e. chuck size0
            int chunkBytesRead = 1;

            while (totalBytesRead < dataToRead.Length && chunkBytesRead > 0)
            {
                chunkBytesRead = stream.Read(dataToRead, totalBytesRead, dataToRead.Length - totalBytesRead);
                totalBytesRead = totalBytesRead + chunkBytesRead;
            }

            return dataToRead;
        }

        public void Implement_Buffer_For_Streams_That_Dont_Support_It_Test()
        {
            // Use the BufferedStream decorator stream
        }

        public void Multithreading_Streams_Test()
        {
            // Multithreading can be used to read/write into stream in parallel
            // However, as a rule, streams are not thread-safe
            // Therefore, explicityly code for thread-safety
            // Stream class has a static Synchronized method
            // Returns a thread-safe wrapper around the stream
        }

        public void Stream_With_No_Backing_Store_Test()
        {
            // Stream.Null returns a stream with no backing store
            // Might be useful for testing code to write huge data without actually consuming resources
            // Write or WriteBytes on a Null stream do nothing
            // Reading from a Null stream returns 0

            Stream s = Stream.Null; // return a null stream
            s.WriteByte(66); // data is ignored
            // my core functionality test goes here
        }

        [TestMethod]
        public void Chunk_Memory_Test()
        {
            long bytes = GC.GetTotalMemory(false);
            string filePath = @"C:\temp\input.pdf";
            string outputfilePath = String.Format(@"C:\temp\output-{0}.pdf", Guid.NewGuid());

            Console.WriteLine("TotalMemory: {0}", bytes);

            //byte[] data = File.ReadAllBytes(filePath);
            //File.Copy(filePath, outputfilePath);

            const int chunkSize = 1024; // read the file by chunks of 1KB
            using (var file = File.OpenRead(filePath))
            {
                int bytesRead;
                var buffer = new byte[chunkSize];

                using (var fs = File.Create(outputfilePath))
                {
                    while ((bytesRead = file.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fs.Write(buffer, 0, bytesRead);
                    }
                }
            }

            /*
            int MAX_BUFFER = 1024;
            byte[] Buffer = new byte[MAX_BUFFER];
            int bytesRead;
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                while ((bytesRead = fileStream.Read(Buffer, 0, MAX_BUFFER)) != 0)
                {
                    // Process this chunk starting from offset 0 
                    // and continuing for bytesRead bytes!
                }
            }
            */

            long bytes1 = GC.GetTotalMemory(false);
            Console.WriteLine("TotalMemory: {0}", bytes1);
            Console.WriteLine("TotalMemory Difference: {0}", bytes1 - bytes);
            Console.ReadLine();
        }
    }
}
