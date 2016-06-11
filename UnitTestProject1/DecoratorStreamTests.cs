using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.IO.Compression;

namespace UnitTestProject1
{
    [TestClass]
    public class DecoratorStreamTests
    {
        [TestMethod]
        public void DecoratorStream_Basics_Test()
        {
            // Decorator is wrapper class that implements the Decorator design pattern
            // Adds runtime capabilities to the wrapped class
            // Closing a decorator stream also closes the decorated backing store stream
            // In a chain, closing the stream at the start of the chain, also closes the entire chain
            // Chaining FileStream > GZipStream > BufferedStream is useful for large data
            //----reason you might want to do this is the true value compression stream can only be apparent when compressing large
            //----enough data so that the size saved will outweigh the compression overhead
            /*
            FileStream fs = new FileStream(@"c:\temp\text.txt", FileMode.Open);
            BufferedStream bs = new BufferedStream(new GZipStream(fs, CompressionMode.Compress));
            */
        }

        [TestMethod]
        public void BufferedStream_Test()
        {
            // Enhances the performance of reading/writing by reducing number of I/O operations
            // Implemented via an in-memory buffer
            // FileStream implements an internal buffer
            // Q: How do streams without internal buffers benefit from buffering
            // A: BufferedStream decorator stream, default buffer size is 4096 bytes (1KB)
            // Write
            //----data is written to buffer instead of backing store
            //----when buffer is full, data is flushed to the backing store
            //----single I/O operation instead of muliple ones
            // Read
            //----more data is read than what is actually requested
            //----extra data is stored in buffer and used for futer reads
            //----number of I/O operations reduced
        }

        [TestMethod]
        public void CompressionStream_Test()
        {
            // DeflateStream and GZipStream use the same compression algorithm
            // GZipStream uses DeflateStream
            // GZipStream adds Cyclic Redundancy Check (CRC) to compressed data
            // GZipStream is more reliable than DeflateStream
            // GZipStream slower than DeflateStream and produces larger size
        }

        [TestMethod]
        public void CryptoStream_Test()
        {
            // Provides cryptographic operations over stream data
            // Requires understanding of cryptographic concepts
        }
    }
}
