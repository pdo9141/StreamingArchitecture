using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class PipeStreamInterprocessCommunicationTests
    {
        [TestMethod]
        public void PipeStreamInterprocess_Basics_Test()
        {
            // A windows pipe is a section of shared memory that processes use for communication
            // One process writes to memory location while other reads from it, this is call Interprocess Communication (IPC)
            // There's two ways to communicate
            //----One-way, process A writes, process B reads
            //----Two-way (Duplex), process A read/write, process B read/write
            // One process must create (instantiate) the pipe, this process is called Pipe Server
            // The other process is called the Pipe Client
            // There are two types of pipes
            //----Named
            //--------has a name
            //--------one-way or Duplex
            //--------pipe server with multiple pipe clients
            //--------communication over same/different machine(s)
            //----Anonymous
            //--------has no name
            //--------one-way
            //--------pipe server/pipe client communication
            //--------communcation over same machine 
            // Although named pipes support cross-machine process communication,
            // Same machine communication yields best performance
            //----no network dependency == better performance
            // Pipes are stream-based
            //----the sending process sends data in a streamed fashion
            //----the receiving process receives data in chucks as a series of bytes
            // Remember as a general rule are not thread-safe,
            // PipeStream however is thread-safe
            //----no need to explicitly use Synchronized method
            //----read and write operations block the thread
        }
    }
}
