using System.IO;
using System.Net;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        ASCIIEncoding encoding = new ASCIIEncoding();

        WebClient client = new WebClient();
        client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

        // post data
        Stream requestStream = client.OpenWrite("http://localhost:19683/default.aspx", "POST");

        byte[] postByteArray = encoding.GetBytes("val1=hello&val2=fellows!");
        requestStream.Write(postByteArray, 0, postByteArray.Length);

        // the data is sent to the server when the stream is closed
        requestStream.Close();

        // get data
        requestStream = client.OpenRead("http://localhost:19683/default.aspx");

        StreamReader sr = new StreamReader(requestStream);
        string data = sr.ReadToEnd();
        requestStream.Close();
    }
}
