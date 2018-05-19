using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (HttpListener listener = new HttpListener())
            {
                listener.Prefixes.Add("http://localhost:8888/");
                listener.Start();
                Console.WriteLine("Whait...");
                while (true)
                {
                    HttpListenerContext context = await listener.GetContextAsync();
                    ThreadPool.QueueUserWorkItem((obj) =>
                    {
                        HttpListenerResponse response = context.Response;
                        string responseString = "Hello";
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                        response.ContentLength64 = buffer.Length;
                        response.ContentEncoding = Encoding.UTF8;
                        Stream output = response.OutputStream;
                        output.Write(buffer, 0, buffer.Length);
                        output.Close();
                    });
                }
            }
        }
    }
}
