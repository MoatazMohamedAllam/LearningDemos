namespace WebApplication2
{
    public class MyGoogleClient
    {
        public MyGoogleClient(HttpClient client)
        {
            Client = client;
        }

        public HttpClient Client { get; set; }
    }
}
