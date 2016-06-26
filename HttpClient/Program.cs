using System;
using System.Net.Http;

class Program
{
    static void Main(string[] args)
    {
        Get();

        Console.WriteLine("I am a free thread!");
        Console.ReadLine();
    }

    private static async void Get()
    {
        HttpClient client = new HttpClient();

        HttpResponseMessage response = await
            client.GetAsync("http://localhost:19683/default.aspx");

        response.EnsureSuccessStatusCode();

        Console.WriteLine(await response.Content.ReadAsStringAsync());
    }
}
