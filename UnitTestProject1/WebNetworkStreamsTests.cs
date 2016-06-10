using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class WebNetworkStreamsTests
    {
        [TestMethod]
        public void WebNetworkStreams_Basics_Test()
        {
            // WebRequest/WebResponse provide low-level control over web requests/responses
            // WebClient provides common operations for simpler web requests (interally uses WebRequest)
            // WebClient provides choices to work with strings, byte arrays, files, or streams
            // WebClient does not support reading a POST response using stream
            // Stream support in WebClient
            //----OpenRead: opens a readable stream for data downloaded from a resource
            //----OpenWrite: opens a stream for writing data to the specified resource
            // HttpClient provides base class for HTTP requests/responses (interally uses HttpWebRequest)
            //----this is the recommended approach
            //----http://blogs.msdn.com/b/henrikn/archive/2012/02/11/httpclient-is-here.aspx
            //----only supports asynchronous operations
            // WebRequest and WebResponse support stream-based data uploads and downloads      
            // Application Layer includes WebRequest, WebResponse, WebClient, HttpClient
            // Transport Layer includes TcpListener, TcpClient, Socket
            //----greater flexibility
            //----slightly better performance
            //----more details to handle
            //----communication with Transport Layer might be blocked    
            // Socket is an endpoint for interprocess communication over a network (System.Net.Sockets)
            // NetworkStream
            //----unseekable stream, has a socket object as its backing store
            //----allows interprocess communication in a streamed manner
            //----supports connection-oriented protocols such as TCP/IP, UDP is not supported
        }
    }
}
