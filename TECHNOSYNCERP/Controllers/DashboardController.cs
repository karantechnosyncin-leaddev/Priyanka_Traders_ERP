using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace TECHNOSYNCERP.Controllers
{
    public class DashboardController : Controller
    {

        private readonly IConfiguration _configuration;
        public DashboardController(ILogger<DashboardController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> GETDASHCARDS(string FromDate, string ToDate)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query =@$"EXEC [GET_DASHBOARD_CARDS] '{FromDate}','{ToDate}'"; // keep your query as is

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SequentialAccess); // helps with large data
                while (await reader.ReadAsync())
                {
                    var obj = new Dictionary<string, string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        obj[reader.GetName(i)] = reader.GetValue(i)?.ToString();
                    }
                    list.Add(obj);
                }

                return Json(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
