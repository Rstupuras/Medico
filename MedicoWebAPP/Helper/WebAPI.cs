using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MedicoWebAPP.Helper
{
    public class WebAPI
    {
        public static HttpClient Initializatiion()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:5001");
            return Client;
        }
        public HttpClient Initial()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:5001");
            return Client;
        }
        
        
    }
}