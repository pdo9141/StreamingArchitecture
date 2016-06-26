using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class AsynchronousSupportTests
    {
        [TestMethod]
        public void AsynchronousSupport_Basics_Test()
        {
            // Operation Types
            //----CPU intensive (computer-bound)
            //--------Executed on a thread pool thread
            //--------Ex: mathematic operations, image processing, compression
            //----I/O bound operations
            //--------Ex: database calls and web service calls, file access, network access
            //--------Executed by hardware devices controlled by Windows device driver
            // Backing store streams, decorator streams, and stream adapters (except binary adapters)
            // expose asynchronous operations
            // Decision factors for decorator streams and MemoryStream CPU operations:
            //----Does the program logic benefit from asynchronous execution?
            //--------Ex: Does your program need the result of a GZipStream operation before it carries
            //--------on? If yes, then asynchronous model does not make sense
            //----Even if logic benefits from asynchronous model, asynchronous execution incurs threading overhead (thread switching)
            //--------Ex: Time saved by excuting compression asynchronously must outweigh threading overhead
            // Decision factors for backing store streams and stream adapters I/O operations:
            //----Does the program logic benefit from asynchronous execution?
            //----How long does the I/O operation block?
            //--------If long time then scalability gain outweighs threading overhead
            //--------If I/O operation is fast then asynchronous model might not be appropriate
            // Pre .NET 4.5 you would use methods prefixed with "Begin" and "End" to leverage async
            // .NET 4.5 simplifies programming model, methods are appended with "Async", async/await
        }
    }
}
