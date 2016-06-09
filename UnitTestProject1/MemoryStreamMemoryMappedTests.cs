using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class MemoryStreamMemoryMappedTests
    {
        [TestMethod]
        public void MemoryStream_Basics_Test()
        {
            // backing store is in-memory buffer
            // very fast: no need for I/O read/write operations
            // caution: not suitable for large data size
            // defies an important streaming advantage
            // no reading data in chunks (all data is already in memory!)
            // only a good choice for non-persistent small-size data
            //----random access to data chunk from a non-seekable stream
            //----read data chunk and store in an array
            //----wrap a MemoryStream around the array
            //----MemoryStream is seekable
            // random access to data from I/O call; ex:web service or database
            //----wrap data in MemoryStream for fast random access
            // Flush method has no implementation in MemoryStream, data entirely in memory so makes sense
        }

        [TestMethod]
        public void MemoryStream_Example_Test()
        {
            #region default constructor

            byte[] arr1 = { 1, 2, 3 };
            byte[] arr2 = new byte[253];

            // will automatically grow in size
            MemoryStream ms = new MemoryStream();

            // sets initial capacity to 256 bytes
            ms.Write(arr1, 0, arr1.Length);

            // length is still <= 256
            ms.Write(arr2, 0, arr2.Length);

            // capacity is doubled to 512 bytes
            ms.WriteByte(1);

            #endregion default constructor

            #region provide capacity

            // set initial capacity to 1 byte
            MemoryStream ms1 = new MemoryStream(1);
            ms1.WriteByte(234);

            // capacity is incremented to 256 bytes
            ms1.WriteByte(234);

            #endregion provide capacity

            #region provide an array

            byte[] arr = { 66, 77, 88 };

            // stream cannot be resized anymore
            MemoryStream ms2 = new MemoryStream(arr);

            // reads 66, position = 1
            int x = ms2.ReadByte();

            // replaces 77 with 55
            ms2.WriteByte(55);

            // reset the position
            ms2.Position = 0;
            byte[] data = ReadBytes(ms2);

            #endregion provide an array

            #region misc

            byte[] arr3 = { 66, 77, 88};

            // set CanWrite to false
            MemoryStream ms3 = new MemoryStream(arr, false);

            // to do: read data
            byte[] a = ms3.ToArray();

            #endregion misc
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

        [TestMethod]
        public void MemoryMapped_Basics_Test()
        {
            // memory-mapped files are not stream types
            // provide similar features to files and pipes
            // feature 1: fast random access to files
            //----FileStream allows random access to files
            //----Memory-mapped files vs FileStream
            //--------FileStream optimized for sequential file access
            //--------FileStream I/O required to access data
            //--------FileStream not thread-safe because pointer moves while writing and reading
            //--------Memory-mapped files better performance for random file access
            //--------Memory-mapped files faster memory access
            //--------Memory-mapped files data can be read in a multi-threaded fashion
            // feature 2: shared memory between processes on same machine
            //----Pipes provide a shared memory between processes
            //----Memory-mapped files vs pipes and PipeStream
            //--------Pipes allow cross-machine communication
            //--------Pipes are stream based
            //--------Memory-mapped files allow same-machine communication only
            //--------Memory-mapped files not stream-based, shared memory block
            //--------Memory-mapped files most efficient for single machine communication
        }

        [TestMethod]
        public void MemoryMapped_Example_Test()
        {
            ReadWrite();
            Compare();
            TestMemoryLocation();

            Console.WriteLine("Done");
        }

        static void ReadWrite()
        {
            using (MemoryMappedFile mmf = MemoryMappedFile.CreateFromFile(@"C:\temp\data.txt",
                FileMode.CreateNew,
                "map1",
                1000))
            {
                using (MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor())
                {
                    accessor.Write(0, (byte)88);
                    Console.WriteLine(accessor.ReadByte(0));

                    byte[] data = Encoding.UTF8.GetBytes("test data");
                    accessor.WriteArray(1, data, 0, data.Length);
                }
            }
        }

        static void Compare()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            // simulate writing at random location, video editing software for example 
            using (FileStream fs = new FileStream(@"C:\temp\data1.txt", FileMode.Create))
            {
                for (int i = 0; i <= 200000; i++)
                {
                    fs.Position = 10;
                    fs.WriteByte(100);
                    fs.Position = 5;
                    fs.WriteByte(100);
                    fs.Position = 15;
                    fs.WriteByte(100);
                    fs.Position = 3;
                    fs.WriteByte(100);
                }
            }

            sw.Stop();
            Console.WriteLine("FileStream: " + sw.Elapsed);
            sw.Reset();

            sw.Start();

            // this will be much faster for rapid random access
            using (MemoryMappedFile mmf = MemoryMappedFile.CreateFromFile(@"C:\temp\data2.txt",
                FileMode.CreateNew,
                "map1",
                1000))
            {
                using (MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor())
                {
                    for (int i = 0; i <= 200000; i++)
                    {
                        accessor.Write(10, (byte)100);
                        accessor.Write(5, (byte)100);
                        accessor.Write(15, (byte)100);
                        accessor.Write(3, (byte)100);
                    }
                }
            }

            sw.Stop();
            Console.WriteLine("Memory-Mapped File: " + sw.Elapsed);
            sw.Reset();
        }

        static void TestMemoryLocation()
        {
            // server process
            using (MemoryMappedFile mmf = MemoryMappedFile.CreateNew("MemoryLocation1", 1000))
            {
                using (MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor())
                {
                    accessor.Write(0, 100);
                }
            }

            // client process
            using (MemoryMappedFile mmf = MemoryMappedFile.OpenExisting("MemoryLocation1"))
            {
                using (MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor())
                {
                    Console.WriteLine(accessor.ReadInt32(0)); // returns 100
                }
            }
        }
    }
}
