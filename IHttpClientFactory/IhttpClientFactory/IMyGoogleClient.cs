namespace WebApplication2
{
    public interface IMyGoogleClient
    {
        Task<int> GetRootLengthAsync();
    }

    public class MyGoogleClient2 : IMyGoogleClient
    {
        private readonly HttpClient _httpClient;

        public MyGoogleClient2(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetRootLengthAsync()
        {
            var result = await _httpClient.GetStringAsync("/");
            return result.Length;
        }
    }
}
