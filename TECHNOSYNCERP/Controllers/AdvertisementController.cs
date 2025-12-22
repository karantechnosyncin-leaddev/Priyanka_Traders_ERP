using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace TECHNOSYNCERP.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly IConfiguration _configuration;
        public AdvertisementController(ILogger<AdvertisementController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GETERPAD(string FYearId)
        {
            var connStr = _configuration.GetConnectionString("Addvertisingurl");
            var list = new List<Dictionary<string, string>>();
            try
            {
                string type = "RETAILSYNC";
                var apiUrl = _configuration.GetValue<string>("AppSettings:AdvertisingUrl");
                string Url = $"{apiUrl}api/ErpAdvertising?type={type}";
                using var client = new HttpClient();
                var response = await client.GetAsync(Url);

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, "Error calling ad API");
                }

                var result = await response.Content.ReadAsStringAsync();
                return Json(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }

}
