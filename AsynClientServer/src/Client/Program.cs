using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                while (true)
                {
                    try
                    {
                        var response = await client.GetAsync("http://localhost:8888/");

                        if (response.IsSuccessStatusCode)
                        {
                            Console.WriteLine(await response.Content.ReadAsStringAsync());
                        }
                        else
                        {
                            Console.WriteLine(response.StatusCode.ToString("G"));
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                    Console.ReadLine();
                }
            }
        }
    }

}
