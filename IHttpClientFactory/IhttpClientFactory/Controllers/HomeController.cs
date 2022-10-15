using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly MyGoogleClient _myGoogleClient;
        private readonly IMyGoogleClient _myGoogleClient2;

        //Named Client
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        //Typed Client
        public HomeController(ILogger<HomeController> logger, MyGoogleClient myGoogleClient)
        {
            _logger = logger;
            _myGoogleClient = myGoogleClient;
        }

        //wrapper over HttpClient
        public HomeController(ILogger<HomeController> logger, IMyGoogleClient myGoogleClient2)
        {
            _logger = logger;
            _myGoogleClient2 = myGoogleClient2;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient(NamedHttpClients.GoogleClient);
            var result = await client.GetAsync(client.BaseAddress);
            return Content(result);
        }

        public async Task<IActionResult> Privacy()
        {
            var result = await _myGoogleClient.Client.GetAsync("/");
            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _myGoogleClient2.GetRootLengthAsync();
            return Ok(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}