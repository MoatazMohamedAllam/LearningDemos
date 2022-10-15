using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemsWithHttpClient
{
    internal class DemoClass
    {
        //this will cause a dns issue 
        //private readonly static HttpClient _httpClient = new HttpClient();

        //solution
        private readonly static HttpClient _httpClient = new HttpClient(new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(1),
        });

        //private readonly static HttpClient _httpClient = new HttpClient(new SocketsHttpHandler
        //{
        //    PooledConnectionLifetime = TimeSpan.FromMinutes(1),
        //}, disposeHandler: false);

        public void DO()
        {
            _httpClient.GetAsync("https://google.com");
        }
    }
}
