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
        static void Main(string[] args)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54698//api//values");
                //HTTP POST
                string meterReadingFile = "~\\MeterReaderWebAPI\\Files\\Meter_Reading.csv";

                var parameters = new Dictionary<string, string> { { "value", meterReadingFile } };
                var encodedContent = new FormUrlEncodedContent(parameters);

                var responseTask = client.PostAsync(client.BaseAddress, encodedContent);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    Console.WriteLine("The file has imported successfully");

                }
            }
            Console.ReadLine();

        }
    }
}

