using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using TECHNOSYNCERP.Models;

namespace TECHNOSYNCERP.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IConfiguration _configuration;
        public InventoryController(ILogger<InventoryController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> GETBTNAUTH(string objType)
        {
            try
            {
                string userId = HttpContext.Session.GetString("UserID");
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("User not logged in");

                string query = @"
            SELECT  *
            FROM BtnAuth T0
            INNER JOIN Btn T1 ON T1.BtnID = T0.BtnID
            WHERE T0.ObjType = @ObjType AND T0.UserID = @UserID";

                var dataList = new List<Dictionary<string, object>>();

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ObjType", objType);
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    await con.OpenAsync();
                    using (var rdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await rdr.ReadAsync())
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < rdr.FieldCount; i++)
                            {
                                row[rdr.GetName(i)] = rdr.IsDBNull(i) ? "" : rdr.GetValue(i);
                            }
                            dataList.Add(row);
                        }
                    }
                }

                return Json(dataList);
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        public async Task<IActionResult> OpeningBalance()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("IOB");
                if (responce is JsonResult jsonResult && jsonResult.Value is List<Dictionary<string, object>> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        ViewBag.BtnAuth = dataList;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> GoodsReceipt()
        {

            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("GR");
                if (responce is JsonResult jsonResult && jsonResult.Value is List<Dictionary<string, object>> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        ViewBag.BtnAuth = dataList;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> GoodsIssue()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("GI");
                if (responce is JsonResult jsonResult && jsonResult.Value is List<Dictionary<string, object>> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        ViewBag.BtnAuth = dataList;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> InventoryTransfer()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("IIT");
                if (responce is JsonResult jsonResult && jsonResult.Value is List<Dictionary<string, object>> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        ViewBag.BtnAuth = dataList;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> InventoryRequest()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("IREQ");
                if (responce is JsonResult jsonResult && jsonResult.Value is List<Dictionary<string, object>> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        ViewBag.BtnAuth = dataList;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> InventoryCounting()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("IIC");
                if (responce is JsonResult jsonResult && jsonResult.Value is List<Dictionary<string, object>> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        ViewBag.BtnAuth = dataList;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> InventoryCountingNew()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("IICN");
                if (responce is JsonResult jsonResult && jsonResult.Value is List<Dictionary<string, object>> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        ViewBag.BtnAuth = dataList;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> InventoryPostingNew()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("IIPN");
                if (responce is JsonResult jsonResult && jsonResult.Value is List<Dictionary<string, object>> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        ViewBag.BtnAuth = dataList;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> InventoryPosting()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("IIP");
                if (responce is JsonResult jsonResult && jsonResult.Value is List<Dictionary<string, object>> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        ViewBag.BtnAuth = dataList;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> InventoryAssignment()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("IIA");
                if (responce is JsonResult jsonResult && jsonResult.Value is List<Dictionary<string, object>> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        ViewBag.BtnAuth = dataList;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> BarcodePrint()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("IBP");
                if (responce is JsonResult jsonResult && jsonResult.Value is List<Dictionary<string, object>> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        ViewBag.BtnAuth = dataList;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> InventoryRequestIn()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("IRI");
                if (responce is JsonResult jsonResult && jsonResult.Value is List<Dictionary<string, object>> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        ViewBag.BtnAuth = dataList;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> InventoryRequestOut()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("IRO");
                if (responce is JsonResult jsonResult && jsonResult.Value is List<Dictionary<string, object>> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        ViewBag.BtnAuth = dataList;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> StockCheck()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("ISC");
                if (responce is JsonResult jsonResult && jsonResult.Value is List<Dictionary<string, object>> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        ViewBag.BtnAuth = dataList;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> MaterialIn()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("MI");
                if (responce is JsonResult jsonResult && jsonResult.Value is List<Dictionary<string, object>> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        ViewBag.BtnAuth = dataList;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        } 
        public async Task<IActionResult> MaterialOut()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("MO");
                if (responce is JsonResult jsonResult && jsonResult.Value is List<Dictionary<string, object>> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        ViewBag.BtnAuth = dataList;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> PriceList()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("IPL");
                if (responce is JsonResult jsonResult && jsonResult.Value is List<Dictionary<string, object>> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        ViewBag.BtnAuth = dataList;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> GETOPENFYEAR()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = @"SELECT [FYearId],[FYear] FROM [Master_FYear] 
                         WHERE Status ='O' 
                         ORDER BY FYearId DESC";
                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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
        public async Task<IActionResult> GETWHSUNLOKED(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = $"exec [Document_WhsDetails] '{id}'";
                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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
        public async Task<IActionResult> GETWHSSTOCK(string id, string FromWhs)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = $"exec [GET_WhsSTOCK] '{id}' ,'{FromWhs}'";
                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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
        public async Task<IActionResult> GETACTIVEITEMS()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = "EXEC ItemMasterList 'A'"; // keep your query as is

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
        public async Task<IActionResult> GETACTIVEITEMSBYID(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = $"EXEC ItemMasterList 'A','{id}'"; // keep query as is

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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

        public async Task<IActionResult> GETACTIVEITEMSSALE()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = "EXEC ItemMasterList_Sale 'A'"; // keep your query as is

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
        public async Task<IActionResult> GETACTIVEITEMSBYIDSALE(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = $"EXEC ItemMasterList_Sale 'A','{id}'"; // keep query as is

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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

        public async Task<IActionResult> GETACTIVEITEMSPURCHASE()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = "EXEC ItemMasterList_Purchase 'A'"; // keep your query as is

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
        public async Task<IActionResult> GETACTIVEITEMSBYIDPURCHASE(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = $"EXEC ItemMasterList_Purchase 'A','{id}'"; // keep query as is

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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

        public async Task<IActionResult> GETOBITEMINVENTORYLIST()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = "EXEC  ItemMasterList_Inventory_OB 'A' "; // keep your query as is

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

        public async Task<IActionResult> GETACTIVEITEMSINVENTORY()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = "EXEC ItemMasterList_Inventory 'A'"; // keep your query as is

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
        public async Task<IActionResult> GETACTIVEITEMSBYIDINVENTORY(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = $"EXEC ItemMasterList_Inventory 'A','{id}'"; // keep query as is

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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

        #region GET INVENTORY REQUEST IN ITEM
        public async Task<IActionResult> GETACTIVEITEMSINVENTORYIN(string fwhs, string twhs)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = @$"EXEC ItemMasterList_InventoryRequestIn 'A','','{fwhs}','{twhs}'"; // keep your query as is

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
        public async Task<IActionResult> GETACTIVEITEMSINVENTORYINBYID(string fwhs, string twhs,string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = @$"EXEC ItemMasterList_InventoryRequestIn 'A','{id}','{fwhs}','{twhs}'"; // keep your query as is

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
        #endregion

        #region GET INVENTORY REQUEST OUT ITEM
        public async Task<IActionResult> GETACTIVEITEMSINVENTORYOUT(string fwhs, string twhs)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = @$"EXEC ItemMasterList_InventoryRequestOut 'A','','{fwhs}','{twhs}'"; // keep your query as is

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
        public async Task<IActionResult> GETACTIVEITEMSINVENTORYOUTBYID(string fwhs, string twhs, string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = @$"EXEC ItemMasterList_InventoryRequestOut 'A','{id}','{fwhs}','{twhs}'"; // keep your query as is

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
        #endregion

        public async Task<IActionResult> GETACTIVEITEMSBYWHSIDINVENTORY(string id,string FromWhsId, string ToWhsId)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = $"EXEC ItemMasterList_InventoryRequestIn 'A','{id}','{FromWhsId}','{ToWhsId}'"; // keep query as is

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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

        public async Task<IActionResult> GETALLUOMBYITEM(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = $"EXEC GET_UOMPrice '{id}'"; // keep query as is

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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
        public async Task<IActionResult> GETALLUOMBYITEMPURCHASE(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = $"EXEC GET_UOMPrice_Purchase '{id}'"; // keep query as is

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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
        public async Task<IActionResult> GETUOMBYITEM(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = @$"SELECT T0.UomID, T1.Code AS 'UomCode', T0.MRP, T0.Discount, T0.NetAmount
                          FROM ItemPriceSetup T0
                          INNER JOIN UomTyp T1 ON T0.UomID = T1.UomID
                          INNER JOIN ItemMaster T2 ON T0.ItemID = T2.ItemID AND T2.UomID = T0.UomID
                          WHERE T0.ItemID = '{id}'";

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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
        public async Task<IActionResult> GETGOODSRECEIPTDOCNUM(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = @$"EXEC GET_NextDocNumber '{id}', 'Inventory_GoodsReceipt_Head'";

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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
        public async Task<IActionResult> GETGOODSISSUEDOCNUM(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = @$"EXEC GET_NextDocNumber '{id}', 'Inventory_GoodsIssue_Head'";

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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
        public async Task<IActionResult> GETMATERIALINDOCNUM(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = @$"EXEC GET_NextDocNumber '{id}', 'Inventory_MaterialIn_Head'";

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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
        public async Task<IActionResult> GETMATERIALOUTDOCNUM(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = @$"EXEC GET_NextDocNumber '{id}', 'Inventory_MaterialOut_Head'";

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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
        public async Task<IActionResult> GETINVENTORYCOUNTINGDOCNUMNEW(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = @$"EXEC GET_NextDocNumber '{id}', 'Inventory_Counting_Head_NEW'";

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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
        public async Task<IActionResult> VALIDATEITMS(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = @$"EXEC GET_NextDocNumber '{id}', 'Inventory_GoodsIssue_Head'";

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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
        public async Task<IActionResult> GETADDBYLEDGER(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = $"exec GET_LedgerAddress '{id}' ,'S'";

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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
        public async Task<IActionResult> GETPRICELISTITEMS(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = $"exec [Inventory_GetPriceList] '{id}'";

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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
        public async Task<IActionResult> GETUOMDETEAILS(string id, string uomid)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = $"Select * FROM ItemPriceSetup where UomID ='{uomid}' and ItemID='{id}'";

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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
        public async Task<IActionResult> GETOPENINGBALDOCNUM(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = $"EXEC GET_NextDocNumber '{id}', 'Inventory_OpeningBalanceHead'";

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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
        public async Task<IActionResult> FINDMODE(string doctype, string docid, string refno, string fromdate, string todate, string docEntry)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var header = new List<Dictionary<string, string>>();
            var row = new List<Dictionary<string, string>>();
            var freight = new List<Dictionary<string, string>>();

            var newobj = new
            {
                header = header,
                row = row,
                freight = freight
            };

            try
            {
                await using var con = new SqlConnection(connStr);
                await con.OpenAsync();

                // Header
                string queryHeader = @$"EXEC [FindDocHeader] '{doctype}','{docEntry}','{docid}','{refno}','{fromdate}','{todate}','',''";
                await using (var cmd = new SqlCommand(queryHeader, con))
                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var obj = new Dictionary<string, string>();
                        for (int i = 0; i < reader.FieldCount; i++)
                            obj[reader.GetName(i)] = reader.GetValue(i)?.ToString();
                        header.Add(obj);
                    }
                }

                if (!string.IsNullOrEmpty(docEntry))
                {
                    // Row
                    string queryRow = @$"EXEC [FindDocRow] '{doctype}','{docEntry}'";
                    await using (var cmd = new SqlCommand(queryRow, con))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var obj = new Dictionary<string, string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                                obj[reader.GetName(i)] = reader.GetValue(i)?.ToString();
                            row.Add(obj);
                        }
                    }

                    // Freight
                    string queryFreight = @$"EXEC [FindDocFreight] '{doctype}','{docEntry}'";
                    await using (var cmd = new SqlCommand(queryFreight, con))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var obj = new Dictionary<string, string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                                obj[reader.GetName(i)] = reader.GetValue(i)?.ToString();
                            freight.Add(obj);
                        }
                    }
                }

                return Json(newobj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> COPYFROM(string doctype, string docid, string refno, string fromdate, string todate, string docEntry, string ledgercode, string status)
        {
            status ??= ""; // default to empty string if null
            var connStr = _configuration.GetConnectionString("ErpConnection");

            var header = new List<Dictionary<string, string>>();
            var row = new List<Dictionary<string, string>>();
            var freight = new List<Dictionary<string, string>>();

            var newobj = new
            {
                header,
                row,
                freight
            };

            try
            {
                await using var con = new SqlConnection(connStr);
                await con.OpenAsync();

                // Header
                string queryHeader = @$"EXEC [Document_CopyToHeader] 
                                '{doctype}','{docEntry}','{docid}','{refno}','{fromdate}','{todate}',
                                '{HttpContext.Session.GetString("UserID")}','','{ledgercode}','{status}'";
                await using (var cmd = new SqlCommand(queryHeader, con))
                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var obj = new Dictionary<string, string>();
                        for (int i = 0; i < reader.FieldCount; i++)
                            obj[reader.GetName(i)] = reader.GetValue(i)?.ToString();
                        header.Add(obj);
                    }
                }

                if (!string.IsNullOrEmpty(docEntry))
                {
                    // Row
                    string queryRow = @$"EXEC [Document_CopyToRow] '{doctype}','{docEntry}','O'";
                    await using (var cmd = new SqlCommand(queryRow, con))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var obj = new Dictionary<string, string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                                obj[reader.GetName(i)] = reader.GetValue(i)?.ToString();
                            row.Add(obj);
                        }
                    }

                    // Freight
                    string queryFreight = @$"EXEC [FindDocFreight] '{doctype}','{docEntry}'";
                    await using (var cmd = new SqlCommand(queryFreight, con))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var obj = new Dictionary<string, string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                                obj[reader.GetName(i)] = reader.GetValue(i)?.ToString();
                            freight.Add(obj);
                        }
                    }
                }

                return Json(newobj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> COPYFROMMATERIALOUT(string doctype, string itemid)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                await con.OpenAsync();

                string query = @$"EXEC [dbo].[Document_CopyFromMatOut] '{doctype}','{itemid}'";

                await using var cmd = new SqlCommand(query, con);
                await using var reader = await cmd.ExecuteReaderAsync();
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
        public async Task<IActionResult> RESUMEPARK(string doctype, string docid, string refno, string fromdate, string todate, string docEntry, string Userid)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();
            try
            {
                await using var con = new SqlConnection(connStr);
                await con.OpenAsync();

                string query = @$"EXEC [FindDocHeader] '{doctype}','{docEntry}','{docid}','{refno}','{fromdate}','{todate}','{Userid}','P'";

                await using var cmd = new SqlCommand(query, con);
                await using var reader = await cmd.ExecuteReaderAsync();
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
        public async Task<IActionResult> DELETEPARKDOC(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");

            try
            {
                await using var con = new SqlConnection(connStr);
                await con.OpenAsync();

                string query = "DELETE FROM [Document_Park] WHERE DocEntry = @DocEntry";

                await using var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DocEntry", id);

                int affectedRows = await cmd.ExecuteNonQueryAsync();

                return Json(new { deletedRows = affectedRows });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETWHSDROPDOWN()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var resultList = new List<Dictionary<string, string>>();
            string query = @"Select ID,WhsCode,WhsName ,(Select ISNULL(SUM(Quantity),0) from [Inventory_Stock]  where  WhsID=T0.ID)as StockInWhs FROM WhsMaster T0 Where T0.Locked ='N'";

            try
            {
                await using (var con = new SqlConnection(connStr))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    await using (var rdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await rdr.ReadAsync())
                        {
                            var dict = new Dictionary<string, string>();
                            for (int i = 0; i < rdr.FieldCount; i++)
                            {
                                dict[rdr.GetName(i)] = await rdr.IsDBNullAsync(i) ? "" : rdr[i].ToString();
                            }
                            resultList.Add(dict);
                        }
                    }
                }

                return Json(resultList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETEMPLOYEEDROPDOWN()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var resultList = new List<Dictionary<string, string>>();
            string query = @"SELECT * FROM [Master_EmployeeMaster] T0 WHERE IsActive ='A'";

            try
            {
                await using (var con = new SqlConnection(connStr))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    await using (var rdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await rdr.ReadAsync())
                        {
                            var dict = new Dictionary<string, string>();
                            for (int i = 0; i < rdr.FieldCount; i++)
                            {
                                dict[rdr.GetName(i)] = await rdr.IsDBNullAsync(i) ? "" : rdr[i].ToString();
                            }
                            resultList.Add(dict);
                        }
                    }
                }

                return Json(resultList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETINVREQDOCNUM(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = @$"EXEC GET_NextDocNumber '{id}', 'Inventory_Request_Head'";

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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
        public async Task<IActionResult> GETINVREQINDOCNUM(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = @$"EXEC GET_NextDocNumber '{id}', 'Inventory_RequestIn_Head'";

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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
        public async Task<IActionResult> GETINVREQOUTDOCNUM(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = @$"EXEC GET_NextDocNumber '{id}', 'Inventory_RequestOut_Head'";

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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
        public async Task<IActionResult> GETINVTRANSFERDOCNUM(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = @$"EXEC GET_NextDocNumber '{id}', 'Inventory_Transfer_Head'";

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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
        public async Task<IActionResult> GETINVCOUNTINGDOCNUM(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = @$"EXEC GET_NextDocNumber '{id}', 'Inventory_Counting_Head'";

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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
        public async Task<IActionResult> GETINVPOSTINGDOCNUM(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = @$"EXEC GET_NextDocNumber '{id}', 'Inventory_Posting_Head'";

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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
        public async Task<IActionResult> GETROWDATAINVENTORYPOSTINGNEW()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                var empid = HttpContext.Session.GetString("EmpId");

                if (string.IsNullOrEmpty(empid))
                    return Ok(list);

                await using var con = new SqlConnection(connStr);

                string query = @"Select * FROM Inventory_Counting_Row_NEW 
                         Where [Status] ='O' AND EmployeeID = @EmployeeID";

                await using var cmd = new SqlCommand(query, con);

                // IMPORTANT FIX
                cmd.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = int.Parse(empid);

                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var obj = new Dictionary<string, string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        obj[reader.GetName(i)] =
                            reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString();
                    }
                    list.Add(obj);
                }

                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


        [HttpPost]

        #region OPENING BALANCE
        public async Task<IActionResult> CREATEOPNINGBALANCE([FromBody] OPENINGBALANCE data, string flag, string doctype)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            if (flag == "P")
            {
                return await PARK(data, doctype);
            }

            SqlTransaction transaction = null;

            await using var con = new SqlConnection(connectionString);
            await con.OpenAsync();

            try
            {
                transaction = con.BeginTransaction();

                // Process Header
                var header = data.header;
                header.UpdatedDate = DateTime.Now;
                header.UpdatedByUName = HttpContext.Session.GetString("UserName");
                header.UpdatedByUId = HttpContext.Session.GetString("UserID");

                Genrate_Query generator = new Genrate_Query();
                string query;

                if (string.IsNullOrEmpty(header.DocEntry))
                {
                    // New record
                    header.CreatedDate = DateTime.Now;
                    header.CretedByUId = HttpContext.Session.GetString("UserID");
                    header.CretedByUName = HttpContext.Session.GetString("UserName");
                    header.DocEntry = null;

                    await using (var cmd = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Inventory_OpeningBalanceHead'", con, transaction))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            header.Docnum = reader["Message"].ToString();
                        }
                    }

                    query = generator.GenerateInsertQuery(header, "[Inventory_OpeningBalanceHead]", "DocEntry");
                }
                else
                {
                    // Update record
                    query = generator.GenerateUpdateQuery(header, "[Inventory_OpeningBalanceHead]", "DocEntry", header.DocEntry, "");
                }

                await using (var cmd = new SqlCommand(query, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();

                    if (string.IsNullOrEmpty(header.DocEntry))
                    {
                        cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                        header.DocEntry = (await cmd.ExecuteScalarAsync()).ToString();
                    }
                }

                // Process Lines
                if (data.lines != null && data.lines.Length > 0)
                {
                    int linenum = 1;
                    foreach (var line in data.lines)
                    {
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = data.header.DocEntry;
                        line.LineNum = linenum;

                        string lineQuery;
                        if (string.IsNullOrEmpty(line.ID))
                        {
                            line.CreatedDate = DateTime.Now;
                            line.CretedByUId = HttpContext.Session.GetString("UserID");
                            line.CretedByUName = HttpContext.Session.GetString("UserName");
                            line.ID = null;
                            lineQuery = generator.GenerateInsertQuery(line, "[Inventory_OpeningBalanceRow]", "ID");
                        }
                        else
                        {
                            lineQuery = generator.GenerateUpdateQuery(line, "[Inventory_OpeningBalanceRow]", "ID", line.ID, "");
                        }

                        await using (var cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }
                        linenum++;
                    }

                    await using (var cmd = new SqlCommand($"EXEC [Insert_InvtoryStock] '{data.header.DocEntry}','IOB'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await using var rdr = await cmd.ExecuteReaderAsync();
                        while (await rdr.ReadAsync())
                        {
                            string success = rdr["Success"].ToString();
                            string message = rdr["Message"].ToString();

                            if (!string.Equals(success, "true", StringComparison.OrdinalIgnoreCase))
                            {
                                await transaction.RollbackAsync();
                                return StatusCode(500, $"An error occurred: {message}");
                            }
                        }
                    }
                }

                await transaction.CommitAsync();
                return Json(new { Success = true, DocEntry = data.header.DocEntry, Message = "Document saved successfully." });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return Conflict("A record with the same value already exists.");
            }
            catch (Exception ex)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            finally
            {
                if (transaction != null) await transaction.DisposeAsync();
                await con.CloseAsync();
            }
        }
        #endregion
        #region Goods Receipt
        public async Task<IActionResult> CREATERECEIPT([FromBody] GOODSRECEIPT data, string flag, string doctype)
        {
            if (flag == "P")
            {
                return await PARK(data, doctype);
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;

            await using var con = new SqlConnection(connectionString);
            await con.OpenAsync();

            try
            {
                transaction = con.BeginTransaction();

                // Process Header
                var header = data.header;
                header.UpdatedDate = DateTime.Now;
                header.UpdatedByUName = HttpContext.Session.GetString("UserName");
                header.UpdatedByUId = HttpContext.Session.GetString("UserID");

                Genrate_Query generator = new Genrate_Query();
                string query;

                if (string.IsNullOrEmpty(header.DocEntry))
                {
                    // New record
                    header.CreatedDate = DateTime.Now;
                    header.CretedByUId = HttpContext.Session.GetString("UserID");
                    header.CretedByUName = HttpContext.Session.GetString("UserName");
                    header.DocEntry = null;

                    await using (var cmd = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Inventory_GoodsReceipt_Head'", con, transaction))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            header.Docnum = reader["Message"].ToString();
                        }
                    }

                    query = generator.GenerateInsertQuery(header, "[Inventory_GoodsReceipt_Head]", "DocEntry");
                }
                else
                {
                    // Update record
                    query = generator.GenerateUpdateQuery(header, "[Inventory_GoodsReceipt_Head]", "DocEntry", header.DocEntry, "");
                }

                await using (var cmd = new SqlCommand(query, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();

                    if (string.IsNullOrEmpty(header.DocEntry))
                    {
                        cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                        header.DocEntry = (await cmd.ExecuteScalarAsync()).ToString();
                    }
                }

                // Process Lines
                if (data.lines != null && data.lines.Length > 0)
                {
                    int linenum = 1;
                    foreach (var line in data.lines)
                    {
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = data.header.DocEntry;
                        line.LineNum = linenum;

                        string lineQuery;
                        if (string.IsNullOrEmpty(line.ID))
                        {
                            line.CreatedDate = DateTime.Now;
                            line.CretedByUId = HttpContext.Session.GetString("UserID");
                            line.CretedByUName = HttpContext.Session.GetString("UserName");
                            line.ID = null;
                            lineQuery = generator.GenerateInsertQuery(line, "[Inventory_GoodsReceipt_Row]", "ID");
                        }
                        else
                        {
                            lineQuery = generator.GenerateUpdateQuery(line, "[Inventory_GoodsReceipt_Row]", "ID", line.ID, "");
                        }

                        await using (var cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }

                        linenum++;
                    }

                    await using (var cmd = new SqlCommand($"EXEC [Insert_InvtoryStock] '{data.header.DocEntry}','GR'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await using var rdr = await cmd.ExecuteReaderAsync();
                        while (await rdr.ReadAsync())
                        {
                            string success = rdr["Success"].ToString();
                            string message = rdr["Message"].ToString();

                            if (!string.Equals(success, "true", StringComparison.OrdinalIgnoreCase))
                            {
                                await transaction.RollbackAsync();
                                return StatusCode(500, $"An error occurred: {message}");
                            }
                        }
                    }
                }

                await transaction.CommitAsync();
                return Json(new { Success = true, DocEntry = data.header.DocEntry, Message = "Goods Receipt saved successfully." });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return Conflict("A record with the same value already exists.");
            }
            catch (Exception ex)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            finally
            {
                if (transaction != null) await transaction.DisposeAsync();
                await con.CloseAsync();
            }
        }
        #endregion


        #region Inventroy Transfer
        public async Task<IActionResult> CREATEINVENTORYTRANSFER([FromBody] INVENTORYTRANSFER data, string flag, string doctype)
        {
            if (flag == "P")
            {
                return await PARK(data, doctype);
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;

            await using var con = new SqlConnection(connectionString);
            await con.OpenAsync();

            try
            {
                transaction = con.BeginTransaction();

                // Process Header
                var header = data.header;
                header.UpdatedDate = DateTime.Now;
                header.UpdatedByUName = HttpContext.Session.GetString("UserName");
                header.UpdatedByUId = HttpContext.Session.GetString("UserID");

                Genrate_Query generator = new Genrate_Query();
                string query;

                if (string.IsNullOrEmpty(header.DocEntry))
                {
                    // New record
                    header.CreatedDate = DateTime.Now;
                    header.CretedByUId = HttpContext.Session.GetString("UserID");
                    header.CretedByUName = HttpContext.Session.GetString("UserName");
                    header.DocEntry = null;

                    await using (var cmd = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Inventory_Transfer_Head'", con, transaction))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            header.Docnum = reader["Message"].ToString();
                        }
                    }

                    query = generator.GenerateInsertQuery(header, "[Inventory_Transfer_Head]", "DocEntry");
                }
                else
                {
                    // Update record
                    query = generator.GenerateUpdateQuery(header, "[Inventory_Transfer_Head]", "DocEntry", header.DocEntry, "");
                }

                await using (var cmd = new SqlCommand(query, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();

                    if (string.IsNullOrEmpty(header.DocEntry))
                    {
                        cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                        header.DocEntry = (await cmd.ExecuteScalarAsync()).ToString();
                    }
                }
                //Delete
                using (SqlCommand cmd = new SqlCommand($"DELETE FROM [Inventory_Transfer_Row] WHERE DocEntry = '{header.DocEntry}'", con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();
                }
                // Process Lines
                if (data.lines != null && data.lines.Length > 0)
                {
                    int linenum = 1;
                    foreach (var line in data.lines)
                    {
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = data.header.DocEntry;
                        line.LineNum = linenum;
                        line.ToWhsCode = data.header.ToWhsCode;
                        line.ToWhsID = data.header.ToWhsId;
                        line.FromWhsID = data.header.FromWhsId;
                        line.FromWhsCode = data.header.FromWhsCode;
                        string lineQuery;
                        if (string.IsNullOrEmpty(line.ID))
                        {
                            line.CreatedDate = DateTime.Now;
                            line.CretedByUId = HttpContext.Session.GetString("UserID");
                            line.CretedByUName = HttpContext.Session.GetString("UserName");
                            line.ID = null;
                            lineQuery = generator.GenerateInsertQuery(line, "[Inventory_Transfer_Row]", "ID");
                        }
                        else
                        {
                            lineQuery = generator.GenerateUpdateQuery(line, "[Inventory_Transfer_Row]", "ID", line.ID, "");
                        }

                        await using (var cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }

                        linenum++;
                    }

                    await using (var cmd = new SqlCommand($"EXEC [Insert_InvtoryStock] '{data.header.DocEntry}','IIT'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await using var rdr = await cmd.ExecuteReaderAsync();
                        while (await rdr.ReadAsync())
                        {
                            string success = rdr["Success"].ToString();
                            string message = rdr["Message"].ToString();

                            if (!string.Equals(success, "true", StringComparison.OrdinalIgnoreCase))
                            {
                                await transaction.RollbackAsync();
                                return StatusCode(500, $"An error occurred: {message}");
                            }
                        }
                    }
                }

                await transaction.CommitAsync();
                return Json(new { Success = true, DocEntry = data.header.DocEntry, Message = "Inventory Transfer saved successfully." });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return Conflict("A record with the same value already exists.");
            }
            catch (Exception ex)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            finally
            {
                if (transaction != null) await transaction.DisposeAsync();
                await con.CloseAsync();
            }
        }
        #endregion
        #region Inventroy Request
        public async Task<IActionResult> CREATEINVENTOROREQUEST([FromBody] INVENTORYREQUEST data, string flag, string doctype)
        {
            if (flag == "P")
            {
                return await PARK(data, doctype);
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;

            await using var con = new SqlConnection(connectionString);
            await con.OpenAsync();

            try
            {
                transaction = con.BeginTransaction();

                // Process Header
                var header = data.header;
                header.UpdatedDate = DateTime.Now;
                header.UpdatedByUName = HttpContext.Session.GetString("UserName");
                header.UpdatedByUId = HttpContext.Session.GetString("UserID");

                Genrate_Query generator = new Genrate_Query();
                string query;

                if (string.IsNullOrEmpty(header.DocEntry))
                {
                    // New record
                    header.CreatedDate = DateTime.Now;
                    header.CretedByUId = HttpContext.Session.GetString("UserID");
                    header.CretedByUName = HttpContext.Session.GetString("UserName");
                    header.DocEntry = null;

                    await using (var cmd = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Inventory_Request_Head'", con, transaction))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            header.Docnum = reader["Message"].ToString();
                        }
                    }

                    query = generator.GenerateInsertQuery(header, "[Inventory_Request_Head]", "DocEntry");
                }
                else
                {
                    // Update record
                    query = generator.GenerateUpdateQuery(header, "[Inventory_Request_Head]", "DocEntry", header.DocEntry, "");
                }

                await using (var cmd = new SqlCommand(query, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();
                                                                                             
                    if (string.IsNullOrEmpty(header.DocEntry)) 
                    {
                        cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                        header.DocEntry = (await cmd.ExecuteScalarAsync()).ToString();
                    }
                }
                //Delete
                using (SqlCommand cmd = new SqlCommand($"DELETE FROM [Inventory_Request_Row] WHERE DocEntry = '{header.DocEntry}'", con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();
                }
                // Process Lines
                if (data.lines != null && data.lines.Length > 0)
                {
                    int linenum = 1;
                    foreach (var line in data.lines)
                    {

                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = data.header.DocEntry;
                        line.LineNum = linenum;
                        line.ToWhsCode = data.header.ToWhsCode;
                        line.ToWhsID = data.header.ToWhsId;
                        line.FromWhsID = data.header.FromWhsId;
                        line.FromWhsCode = data.header.FromWhsCode;
                        line.TargetEntry = "0";
                        line.TargetObj = "";
                        line.TargetLine= 0;
                        string lineQuery;
                        if (string.IsNullOrEmpty(line.ID))
                        {
                            line.CreatedDate = DateTime.Now;
                            line.CretedByUId = HttpContext.Session.GetString("UserID");
                            line.CretedByUName = HttpContext.Session.GetString("UserName");
                            line.ID = null;
                            lineQuery = generator.GenerateInsertQuery(line, "[Inventory_Request_Row]", "ID");
                        }
                        else
                        {
                            lineQuery = generator.GenerateUpdateQuery(line, "[Inventory_Request_Row]", "ID", line.ID, "");
                        }

                        await using (var cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }

                        linenum++;
                    }

                    await using (var cmd = new SqlCommand($"EXEC [Insert_InvtoryStock] '{data.header.DocEntry}','IREQ'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await using var rdr = await cmd.ExecuteReaderAsync();
                        while (await rdr.ReadAsync())
                        {
                            string success = rdr["Success"].ToString();
                            string message = rdr["Message"].ToString();

                            if (!string.Equals(success, "true", StringComparison.OrdinalIgnoreCase))
                            {
                                await transaction.RollbackAsync();
                                return StatusCode(500, $"An error occurred: {message}");
                            }
                        }
                    }
                }

                await transaction.CommitAsync();
                return Json(new { Success = true, DocEntry = data.header.DocEntry, Message = "Inventory Request saved successfully." });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return Conflict("A record with the same value already exists.");
            }
            catch (Exception ex)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            finally
            {
                if (transaction != null) await transaction.DisposeAsync();
                await con.CloseAsync();
            }
        }
        #endregion
        #region Inventroy Request IN
       
        public async Task<IActionResult> CREATEINVENTOROREQUESTIN([FromBody] INVENTORYREQUESTIN data, string flag, string doctype)
        {
            if (flag == "P")
            {
                return await PARK(data, doctype);
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;

            await using var con = new SqlConnection(connectionString);
            await con.OpenAsync();

            try
            {
                transaction = con.BeginTransaction();

                // Process Header
                var header = data.header;
                header.UpdatedDate = DateTime.Now;
                header.UpdatedByUName = HttpContext.Session.GetString("UserName");
                header.UpdatedByUId = HttpContext.Session.GetString("UserID");

                Genrate_Query generator = new Genrate_Query();
                string query;

                if (string.IsNullOrEmpty(header.DocEntry))
                {
                    // New record
                    header.CreatedDate = DateTime.Now;
                    header.CretedByUId = HttpContext.Session.GetString("UserID");
                    header.CretedByUName = HttpContext.Session.GetString("UserName");
                    header.DocEntry = null;

                    await using (var cmd = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Inventory_RequestIn_Head'", con, transaction))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            header.Docnum = reader["Message"].ToString();
                        }
                    }

                    query = generator.GenerateInsertQuery(header, "[Inventory_RequestIn_Head]", "DocEntry");
                }
                else
                {
                    // Update record
                    query = generator.GenerateUpdateQuery(header, "[Inventory_RequestIn_Head]", "DocEntry", header.DocEntry, "");
                }

                await using (var cmd = new SqlCommand(query, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();

                    if (string.IsNullOrEmpty(header.DocEntry))
                    {
                        cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                        header.DocEntry = (await cmd.ExecuteScalarAsync()).ToString();
                    }
                }
                //Delete
                using (SqlCommand cmd = new SqlCommand($"DELETE FROM [Inventory_RequestIn_Row] WHERE DocEntry = '{header.DocEntry}'", con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();
                }
                // Process Lines
                if (data.lines != null && data.lines.Length > 0)
                {
                    int linenum = 1;
                    foreach (var line in data.lines)
                    {
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = data.header.DocEntry;
                        line.LineNum = linenum;
                        line.ToWhsCode = data.header.ToWhsCode;
                        line.ToWhsID = data.header.ToWhsId;
                        line.FromWhsID = data.header.FromWhsId;
                        line.FromWhsCode = data.header.FromWhsCode;
                        string lineQuery;
                        if (string.IsNullOrEmpty(line.ID))
                        {
                            line.CreatedDate = DateTime.Now;
                            line.CretedByUId = HttpContext.Session.GetString("UserID");
                            line.CretedByUName = HttpContext.Session.GetString("UserName");
                            line.ID = null;
                            lineQuery = generator.GenerateInsertQuery(line, "[Inventory_RequestIn_Row]", "ID");
                        }
                        else
                        {
                            lineQuery = generator.GenerateUpdateQuery(line, "[Inventory_RequestIn_Row]", "ID", line.ID, "");
                        }

                        await using (var cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }
                        List<(int Id, decimal OpenQty)> rows = new List<(int, decimal)>();
                        string query45 = @$"SELECT ID, OpenQty 
                                                FROM Inventory_RequestOut_Row 
                                                WHERE ItemID = @ItemID AND [Status] = 'O' AND FromWhsID ='{line.FromWhsID}'";

                        using (SqlCommand cmd = new SqlCommand(query45, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@ItemID", line.ItemID);

                            await using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    rows.Add((
                                        reader.GetInt32(reader.GetOrdinal("ID")),
                                        reader.GetDecimal(reader.GetOrdinal("OpenQty"))
                                    ));
                                }
                            }
                        }

                        // Step 2: After reader is closed, you are free to update rows safely
                        decimal remainingQty = line.Qty;
                        foreach (var row in rows)
                        {
                            if (remainingQty <= 0)
                                break;

                            decimal openQty = row.OpenQty;

                            if (openQty <= 0)
                                continue;

                            if (remainingQty >= openQty)
                            {
                                string closeQuery = @$"UPDATE Inventory_RequestOut_Row 
                                      SET OpenQty = 0,  [Status] = 'C' ,TargetLine ='{line.LineNum}',TargetObj='IRI ',TargetEntry='{header.DocEntry}'
                                      WHERE ID = @ID AND FromWhsID ='{line.FromWhsID}'";

                                using (SqlCommand cmd = new SqlCommand(closeQuery, con, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@ID", row.Id);
                                    await cmd.ExecuteNonQueryAsync();
                                }
                                remainingQty -= openQty;
                            }
                            else
                            {
                                string updateQuery = @$"UPDATE Inventory_RequestOut_Row 
                                       SET OpenQty = @Qty ,TargetLine ='{line.LineNum}',TargetObj='IRI',TargetEntry='{header.DocEntry}'
                                       WHERE ID = @ID  AND FromWhsID ='{line.FromWhsID}'";

                                using (SqlCommand cmd = new SqlCommand(updateQuery, con, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@ID", row.Id);
                                    cmd.Parameters.AddWithValue("@Qty", openQty - remainingQty);
                                    await cmd.ExecuteNonQueryAsync();
                                }
                                remainingQty = 0;
                                break;
                            }
                        }
                        linenum++;
                    }

                    await using (var cmd = new SqlCommand($"EXEC [Insert_InvtoryStock] '{data.header.DocEntry}','IREQI'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await using var rdr = await cmd.ExecuteReaderAsync();
                        while (await rdr.ReadAsync())
                        {
                            string success = rdr["Success"].ToString();
                            string message = rdr["Message"].ToString();

                            if (!string.Equals(success, "true", StringComparison.OrdinalIgnoreCase))
                            {
                                await transaction.RollbackAsync();
                                return StatusCode(500, $"An error occurred: {message}");
                            }
                        }
                    }
                }

                await transaction.CommitAsync();
                return Json(new { Success = true, DocEntry = data.header.DocEntry, Message = "Inventory Request In saved successfully." });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return Conflict("A record with the same value already exists.");
            }
            catch (Exception ex)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            finally
            {
                if (transaction != null) await transaction.DisposeAsync();
                await con.CloseAsync();
            }
        }
        //validation for Quantity
        public async Task<IActionResult> VALIDATEINQUANTITY(string totalQty, string ItemID)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = $"EXEC VALIDATE_INVREQOPENQTY '{ItemID}','{totalQty}','Inventory_RequestOut_Row'";

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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
        #endregion
        #region Inventroy Request Out
        public async Task<IActionResult> CREATEINVENTOROREQUESTOUT([FromBody] INVENTORYREQUESTOUT data, string flag, string doctype)
        {
            if (flag == "P")
            {
                return await PARK(data, doctype);
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;

            await using var con = new SqlConnection(connectionString);
            await con.OpenAsync();

            try
            {
                transaction = con.BeginTransaction();

                // Process Header
                var header = data.header;
                header.UpdatedDate = DateTime.Now;
                header.UpdatedByUName = HttpContext.Session.GetString("UserName");
                header.UpdatedByUId = HttpContext.Session.GetString("UserID");

                Genrate_Query generator = new Genrate_Query();
                string query;

                if (string.IsNullOrEmpty(header.DocEntry))
                {
                    // New record
                    header.CreatedDate = DateTime.Now;
                    header.CretedByUId = HttpContext.Session.GetString("UserID");
                    header.CretedByUName = HttpContext.Session.GetString("UserName");
                    header.DocEntry = null;

                    await using (var cmd = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Inventory_RequestOut_Head'", con, transaction))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            header.Docnum = reader["Message"].ToString();
                        }
                    }

                    query = generator.GenerateInsertQuery(header, "[Inventory_RequestOut_Head]", "DocEntry");
                }
                else
                {
                    // Update record
                    query = generator.GenerateUpdateQuery(header, "[Inventory_RequestOut_Head]", "DocEntry", header.DocEntry, "");
                }

                await using (var cmd = new SqlCommand(query, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();

                    if (string.IsNullOrEmpty(header.DocEntry))
                    {
                        cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                        header.DocEntry = (await cmd.ExecuteScalarAsync()).ToString();
                    }
                }
                //Delete
                using (SqlCommand cmd = new SqlCommand($"DELETE FROM [Inventory_RequestOut_Row] WHERE DocEntry = '{header.DocEntry}'", con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();
                }
                // Process Lines
                if (data.lines != null && data.lines.Length > 0)
                {
                    int linenum = 1;
                    foreach (var line in data.lines)
                    {
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = data.header.DocEntry;
                        line.LineNum = linenum;
                        line.ToWhsCode = data.header.ToWhsCode;
                        line.ToWhsID = data.header.ToWhsId;
                        line.FromWhsID = data.header.FromWhsId;
                        line.FromWhsCode = data.header.FromWhsCode;
                        line.TargetEntry = "0";
                        line.TargetObj = "";
                        line.TargetLine = 0;
                        string lineQuery;
                        if (string.IsNullOrEmpty(line.ID))
                        {
                            line.CreatedDate = DateTime.Now;
                            line.CretedByUId = HttpContext.Session.GetString("UserID");
                            line.CretedByUName = HttpContext.Session.GetString("UserName");
                            line.ID = null;
                            lineQuery = generator.GenerateInsertQuery(line, "[Inventory_RequestOut_Row]", "ID");
                        }
                        else
                        {
                            lineQuery = generator.GenerateUpdateQuery(line, "[Inventory_RequestOut_Row]", "ID", line.ID, "");
                        }

                        await using (var cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }
                        List<(int Id, decimal OpenQty)> rows = new List<(int, decimal)>();
                        string query45 = @$"SELECT ID, OpenQty 
                                                FROM Inventory_Request_Row 
                                                WHERE ItemID = @ItemID AND [Status] = 'O' AND FromWhsID ='{line.FromWhsID}'";

                        using (SqlCommand cmd = new SqlCommand(query45, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@ItemID", line.ItemID);

                            await using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    rows.Add((
                                        reader.GetInt32(reader.GetOrdinal("ID")),
                                        reader.GetDecimal(reader.GetOrdinal("OpenQty"))
                                    ));
                                }
                            }
                        }

                        // Step 2: After reader is closed, you are free to update rows safely
                        decimal remainingQty = line.Qty;
                        foreach (var row in rows)
                        {
                            if (remainingQty <= 0)
                                break;

                            decimal openQty = row.OpenQty;

                            if (openQty <= 0)
                                continue;

                            if (remainingQty >= openQty)
                            {
                                string closeQuery = @$"UPDATE Inventory_Request_Row 
                                      SET OpenQty = 0,  [Status] = 'C' ,TargetLine ='{line.LineNum}',TargetObj='IRO',TargetEntry='{header.DocEntry}'
                                      WHERE ID = @ID AND FromWhsID ='{line.FromWhsID}'";

                                using (SqlCommand cmd = new SqlCommand(closeQuery, con, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@ID", row.Id);
                                    await cmd.ExecuteNonQueryAsync();
                                }

                                remainingQty -= openQty;
                            }
                            else
                            {
                                string updateQuery = @$"UPDATE Inventory_Request_Row 
                                       SET OpenQty = @Qty   ,TargetLine ='{line.LineNum}',TargetObj='IRO',TargetEntry='{header.DocEntry}'
                                       WHERE ID = @ID  AND FromWhsID ='{line.FromWhsID}'";

                                using (SqlCommand cmd = new SqlCommand(updateQuery, con, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@ID", row.Id);
                                    cmd.Parameters.AddWithValue("@Qty", openQty - remainingQty);
                                    await cmd.ExecuteNonQueryAsync();
                                }
                                remainingQty = 0;
                                break;
                            }
                        }
                        linenum++;
                    }

                    await using (var cmd = new SqlCommand($"EXEC [Insert_InvtoryStock] '{data.header.DocEntry}','IRO'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await using var rdr = await cmd.ExecuteReaderAsync();
                        while (await rdr.ReadAsync())
                        {
                            string success = rdr["Success"].ToString();
                            string message = rdr["Message"].ToString();

                            if (!string.Equals(success, "true", StringComparison.OrdinalIgnoreCase))
                            {
                                await transaction.RollbackAsync();
                                return StatusCode(500, $"An error occurred: {message}");
                            }
                        }
                    }
                }

                await transaction.CommitAsync();
                return Json(new { Success = true, DocEntry = data.header.DocEntry, Message = "Inventory Request Out saved successfully." });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return Conflict("A record with the same value already exists.");
            }
            catch (Exception ex)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            finally
            {
                if (transaction != null) await transaction.DisposeAsync();
                await con.CloseAsync();
            }
        }
        //validation for Quantity
        public async Task<IActionResult> VALIDATEQUANTITY( string totalQty, string ItemID)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = $"EXEC VALIDATE_INVREQOPENQTY '{ItemID}','{totalQty}','Inventory_Request_Row'";

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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
        
        #endregion 
        #region Inventroy Counting
        public async Task<IActionResult> CREATEINVENTORYCOUNTING([FromBody] INVENTORYCOUNTING data, string flag, string doctype)
        {
            if (flag == "P")
            {
                return await PARK(data, doctype);
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;

            await using var con = new SqlConnection(connectionString);
            await con.OpenAsync();

            try
            {
                transaction = con.BeginTransaction();

                // Process Header
                var header = data.header;
                header.UpdatedDate = DateTime.Now;
                header.UpdatedByUName = HttpContext.Session.GetString("UserName");
                header.UpdatedByUId = HttpContext.Session.GetString("UserID");

                Genrate_Query generator = new Genrate_Query();
                string query;

                if (string.IsNullOrEmpty(header.DocEntry))
                {
                    // New record
                    header.CreatedDate = DateTime.Now;
                    header.CretedByUId = HttpContext.Session.GetString("UserID");
                    header.CretedByUName = HttpContext.Session.GetString("UserName");
                    header.DocEntry = null;

                    await using (var cmd = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Inventory_Counting_Head'", con, transaction))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            header.Docnum = reader["Message"].ToString();
                        }
                    }

                    query = generator.GenerateInsertQuery(header, "[Inventory_Counting_Head]", "DocEntry");
                }
                else
                {
                    // Update record
                    query = generator.GenerateUpdateQuery(header, "[Inventory_Counting_Head]", "DocEntry", header.DocEntry, "");
                }

                await using (var cmd = new SqlCommand(query, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();

                    if (string.IsNullOrEmpty(header.DocEntry))
                    {
                        cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                        header.DocEntry = (await cmd.ExecuteScalarAsync()).ToString();
                    }
                }
                //Delete
                using (SqlCommand cmd = new SqlCommand($"DELETE FROM [Inventory_Counting_Row] WHERE DocEntry = '{header.DocEntry}'", con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();
                }
                // Process Lines
                if (data.lines != null && data.lines.Length > 0)
                {
                    int linenum = 1;
                    foreach (var line in data.lines)
                    {
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = data.header.DocEntry;
                        line.LineNum = linenum;
                        line.WhsCode = data.header.WhsCode;
                        line.WhsID = data.header.WhsId;
                        string lineQuery;
                        if (string.IsNullOrEmpty(line.ID))
                        {
                            line.CreatedDate = DateTime.Now;
                            line.CretedByUId = HttpContext.Session.GetString("UserID");
                            line.CretedByUName = HttpContext.Session.GetString("UserName");
                            line.ID = null;
                            lineQuery = generator.GenerateInsertQuery(line, "[Inventory_Counting_Row]", "ID");
                        }
                        else
                        {
                            lineQuery = generator.GenerateUpdateQuery(line, "[Inventory_Counting_Row]", "ID", line.ID, "");
                        }

                        await using (var cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }

                        linenum++;
                    }

                    await using (var cmd = new SqlCommand($"EXEC [Insert_InvtoryStock] '{data.header.DocEntry}','IIC'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await using var rdr = await cmd.ExecuteReaderAsync();
                        while (await rdr.ReadAsync())
                        {
                            string success = rdr["Success"].ToString();
                            string message = rdr["Message"].ToString();

                            if (!string.Equals(success, "true", StringComparison.OrdinalIgnoreCase))
                            {
                                await transaction.RollbackAsync();
                                return StatusCode(500, $"An error occurred: {message}");
                            }
                        }
                    }
                }

                await transaction.CommitAsync();
                return Json(new { Success = true, DocEntry = data.header.DocEntry, Message = "Inventory Counting saved successfully." });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return Conflict("A record with the same value already exists.");
            }
            catch (Exception ex)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            finally
            {
                if (transaction != null) await transaction.DisposeAsync();
                await con.CloseAsync();
            }
        }
        #endregion
        #region Inventroy Counting New
        public async Task<IActionResult> CREATEINVENTORYCOUNTINGNEW([FromBody] INVENTORYCOUNTINGNEW data, string flag, string doctype)
        {
            if (flag == "P")
            {
                return await PARK(data, doctype);
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;

            await using var con = new SqlConnection(connectionString);
            await con.OpenAsync();

            try
            {
                transaction = con.BeginTransaction();

                // Process Header
                var header = data.header;
                header.UpdatedDate = DateTime.Now;
                header.UpdatedByUName = HttpContext.Session.GetString("UserName");
                header.UpdatedByUId = HttpContext.Session.GetString("UserID");

                Genrate_Query generator = new Genrate_Query();
                string query;

                if (string.IsNullOrEmpty(header.DocEntry))
                {
                    // New record
                    header.CreatedDate = DateTime.Now;
                    header.CretedByUId = HttpContext.Session.GetString("UserID");
                    header.CretedByUName = HttpContext.Session.GetString("UserName");
                    header.DocEntry = null;

                    await using (var cmd = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Inventory_Counting_Head_NEW'", con, transaction))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            header.Docnum = reader["Message"].ToString();
                        }
                    }

                    query = generator.GenerateInsertQuery(header, "[Inventory_Counting_Head_NEW]", "DocEntry");
                }
                else
                {
                    // Update record
                    query = generator.GenerateUpdateQuery(header, "[Inventory_Counting_Head_NEW]", "DocEntry", header.DocEntry, "");
                }

                await using (var cmd = new SqlCommand(query, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();

                    if (string.IsNullOrEmpty(header.DocEntry))
                    {
                        cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                        header.DocEntry = (await cmd.ExecuteScalarAsync()).ToString();
                    }
                }
                //Delete
                using (SqlCommand cmd = new SqlCommand($"DELETE FROM [Inventory_Counting_Row_NEW] WHERE DocEntry = '{header.DocEntry}'", con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();
                }
                // Process Lines
                if (data.lines != null && data.lines.Length > 0)
                {
                    int linenum = 1;
                    foreach (var line in data.lines)
                    {
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = data.header.DocEntry;
                        line.LineNum = linenum;
                        string lineQuery;
                        if (string.IsNullOrEmpty(line.ID))
                        {
                            line.CreatedDate = DateTime.Now;
                            line.CretedByUId = HttpContext.Session.GetString("UserID");
                            line.CretedByUName = HttpContext.Session.GetString("UserName");
                            line.ID = null;
                            lineQuery = generator.GenerateInsertQuery(line, "[Inventory_Counting_Row_NEW]", "ID");
                        }
                        else
                        {
                            lineQuery = generator.GenerateUpdateQuery(line, "[Inventory_Counting_Row_NEW]", "ID", line.ID, "");
                        }

                        await using (var cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }

                        linenum++;
                    }

                    await using (var cmd = new SqlCommand($"EXEC [Insert_InvtoryStock] '{data.header.DocEntry}','IICN'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await using var rdr = await cmd.ExecuteReaderAsync();
                        while (await rdr.ReadAsync())
                        {
                            string success = rdr["Success"].ToString();
                            string message = rdr["Message"].ToString();

                            if (!string.Equals(success, "true", StringComparison.OrdinalIgnoreCase))
                            {
                                await transaction.RollbackAsync();
                                return StatusCode(500, $"An error occurred: {message}");
                            }
                        }
                    }
                }

                await transaction.CommitAsync();
                return Json(new { Success = true, DocEntry = data.header.DocEntry, Message = "Inventory Counting New saved successfully." });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return Conflict("A record with the same value already exists.");
            }
            catch (Exception ex)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            finally
            {
                if (transaction != null) await transaction.DisposeAsync();
                await con.CloseAsync();
            }
        }
        #endregion
        #region Inventroy Posting
        public async Task<IActionResult> CREATEINVENTORYPOSTING([FromBody] INVENTORYPOSTING data, string flag, string doctype)
        {
            if (flag == "P")
            {
                return await PARK(data, doctype);
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;

            await using var con = new SqlConnection(connectionString);
            await con.OpenAsync();

            try
            {
                transaction = con.BeginTransaction();

                // Process Header
                var header = data.header;
                header.UpdatedDate = DateTime.Now;
                header.UpdatedByUName = HttpContext.Session.GetString("UserName");
                header.UpdatedByUId = HttpContext.Session.GetString("UserID");

                Genrate_Query generator = new Genrate_Query();
                string query;

                if (string.IsNullOrEmpty(header.DocEntry))
                {
                    // New record
                    header.CreatedDate = DateTime.Now;
                    header.CretedByUId = HttpContext.Session.GetString("UserID");
                    header.CretedByUName = HttpContext.Session.GetString("UserName");
                    header.DocEntry = null;

                    await using (var cmd = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Inventory_Posting_Head'", con, transaction))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            header.Docnum = reader["Message"].ToString();
                        }
                    }

                    query = generator.GenerateInsertQuery(header, "[Inventory_Posting_Head]", "DocEntry");
                }
                else
                {
                    // Update record
                    query = generator.GenerateUpdateQuery(header, "[Inventory_Posting_Head]", "DocEntry", header.DocEntry, "");
                }

                await using (var cmd = new SqlCommand(query, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();

                    if (string.IsNullOrEmpty(header.DocEntry))
                    {
                        cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                        header.DocEntry = (await cmd.ExecuteScalarAsync()).ToString();
                    }
                }
                //Delete
                using (SqlCommand cmd = new SqlCommand($"DELETE FROM [Inventory_Posting_Row] WHERE DocEntry = '{header.DocEntry}'", con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();
                }
                // Process Lines
                if (data.lines != null && data.lines.Length > 0)
                {
                    int linenum = 1;
                    foreach (var line in data.lines)
                    {
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = data.header.DocEntry;
                        line.LineNum = linenum;
                        line.WhsCode = data.header.WhsCode;
                        line.WhsID = data.header.WhsId;
                        string lineQuery;
                        if (string.IsNullOrEmpty(line.ID))
                        {
                            line.CreatedDate = DateTime.Now;
                            line.CretedByUId = HttpContext.Session.GetString("UserID");
                            line.CretedByUName = HttpContext.Session.GetString("UserName");
                            line.ID = null;
                            lineQuery = generator.GenerateInsertQuery(line, "[Inventory_Posting_Row]", "ID");
                        }
                        else
                        {
                            lineQuery = generator.GenerateUpdateQuery(line, "[Inventory_Posting_Row]", "ID", line.ID, "");
                        }

                        await using (var cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }

                        linenum++;
                    }

                    await using (var cmd = new SqlCommand($"EXEC [Insert_InvtoryStock] '{data.header.DocEntry}','IIP'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await using var rdr = await cmd.ExecuteReaderAsync();
                        while (await rdr.ReadAsync())
                        {
                            string success = rdr["Success"].ToString();
                            string message = rdr["Message"].ToString();

                            if (!string.Equals(success, "true", StringComparison.OrdinalIgnoreCase))
                            {
                                await transaction.RollbackAsync();
                                return StatusCode(500, $"An error occurred: {message}");
                            }
                        }
                    }
                }

                await transaction.CommitAsync();
                return Json(new { Success = true, DocEntry = data.header.DocEntry, Message = "Inventory Posting saved successfully." });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return Conflict("A record with the same value already exists.");
            }
            catch (Exception ex)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            finally
            {
                if (transaction != null) await transaction.DisposeAsync();
                await con.CloseAsync();
            }
        }
        #endregion
        #region Inventroy Posting New
        public async Task<IActionResult> CREATEINVENTORYPOSTINGNEW([FromBody] INVENTORYPOSTINGNEW data)
        {
            

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;

            await using var con = new SqlConnection(connectionString);
            await con.OpenAsync();

            try
            {
                transaction = con.BeginTransaction();

                // Process Header
                var header = data.header;
                header.UpdatedDate = DateTime.Now;
                header.UpdatedByUName = HttpContext.Session.GetString("UserName");
                header.UpdatedByUId = HttpContext.Session.GetString("UserID");

                Genrate_Query generator = new Genrate_Query();
                string query;

                if (string.IsNullOrEmpty(header.DocEntry))
                {
                    // New record
                    header.CreatedDate = DateTime.Now;
                    header.CretedByUId = HttpContext.Session.GetString("UserID");
                    header.CretedByUName = HttpContext.Session.GetString("UserName");
                    header.DocEntry = null;

                    await using (var cmd = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Inventory_Posting_Head_New'", con, transaction))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            header.Docnum = reader["Message"].ToString();
                        }
                    }

                    query = generator.GenerateInsertQuery(header, "[Inventory_Posting_Head_New]", "DocEntry");
                }
                else
                {
                    // Update record
                    query = generator.GenerateUpdateQuery(header, "[Inventory_Posting_Head_New]", "DocEntry", header.DocEntry, "");
                }

                await using (var cmd = new SqlCommand(query, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();

                    if (string.IsNullOrEmpty(header.DocEntry))
                    {
                        cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                        header.DocEntry = (await cmd.ExecuteScalarAsync()).ToString();
                    }
                }
                //Delete
                using (SqlCommand cmd = new SqlCommand($"DELETE FROM [Inventory_Posting_Row_New] WHERE DocEntry = '{header.DocEntry}'", con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();
                }
                // Process Lines
                if (data.lines != null && data.lines.Length > 0)
                {
                    int linenum = 1;
                    foreach (var line in data.lines)
                    {
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = data.header.DocEntry;
                        line.LineNum = linenum;
                        string lineQuery;
                        if (string.IsNullOrEmpty(line.ID))
                        {
                            line.CreatedDate = DateTime.Now;
                            line.CretedByUId = HttpContext.Session.GetString("UserID");
                            line.CretedByUName = HttpContext.Session.GetString("UserName");
                            line.ID = null;
                            lineQuery = generator.GenerateInsertQuery(line, "[Inventory_Posting_Row_New]", "ID");
                        }
                        else
                        {
                            lineQuery = generator.GenerateUpdateQuery(line, "[Inventory_Posting_Row_New]", "ID", line.ID, "");
                        }

                        await using (var cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }

                        linenum++;
                        await using (var cmd = new SqlCommand($"UPDATE Inventory_Counting_Row_NEW SET [Status] ='C' Where   ID='{line.BaseLine}';", con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }
                        //Unfreese Logic Heare 
                    }

                    await using (var cmd = new SqlCommand($"EXEC [Insert_InvtoryStock] '{data.header.DocEntry}','IIPN'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await using var rdr = await cmd.ExecuteReaderAsync();
                        while (await rdr.ReadAsync())
                        {
                            string success = rdr["Success"].ToString();
                            string message = rdr["Message"].ToString();

                            if (!string.Equals(success, "true", StringComparison.OrdinalIgnoreCase))
                            {
                                await transaction.RollbackAsync();
                                return StatusCode(500, $"An error occurred: {message}");
                            }
                        }
                    }
                  


                }

                await transaction.CommitAsync();
                return Json(new { Success = true, DocEntry = data.header.DocEntry, Message = "Inventory Counting New saved successfully." });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return Conflict("A record with the same value already exists.");
            }
            catch (Exception ex)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            finally
            {
                if (transaction != null) await transaction.DisposeAsync();
                await con.CloseAsync();
            }
        }
        public async Task<IActionResult> GETINVPOSTINGDOCNUMNEW(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = @$"EXEC GET_NextDocNumber '{id}', 'Inventory_Posting_Head_New'";

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();
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

        

        #endregion


        #region Goods Issue
        public async Task<IActionResult> CREATEISSUE([FromBody] GOODSISSUE data, string flag, string doctype)
        {
            if (flag == "P")
            {
                return await PARK(data, doctype);
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;

            await using var con = new SqlConnection(connectionString);
            await con.OpenAsync();

            try
            {
                transaction = con.BeginTransaction();

                // Process Header
                var header = data.header;
                header.UpdatedDate = DateTime.Now;
                header.UpdatedByUName = HttpContext.Session.GetString("UserName");
                header.UpdatedByUId = HttpContext.Session.GetString("UserID");

                Genrate_Query generator = new Genrate_Query();
                string query;

                if (string.IsNullOrEmpty(header.DocEntry))
                {
                    // New record
                    header.CreatedDate = DateTime.Now;
                    header.CretedByUId = HttpContext.Session.GetString("UserID");
                    header.CretedByUName = HttpContext.Session.GetString("UserName");
                    header.DocEntry = null;

                    await using (var cmd = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Inventory_GoodsIssue_Head'", con, transaction))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            header.Docnum = reader["Message"].ToString();
                        }
                    }

                    query = generator.GenerateInsertQuery(header, "[Inventory_GoodsIssue_Head]", "DocEntry");
                }
                else
                {
                    // Update record
                    query = generator.GenerateUpdateQuery(header, "[Inventory_GoodsIssue_Head]", "DocEntry", header.DocEntry, "");
                }

                await using (var cmd = new SqlCommand(query, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();

                    if (string.IsNullOrEmpty(header.DocEntry))
                    {
                        cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                        header.DocEntry = (await cmd.ExecuteScalarAsync()).ToString();
                    }
                }

                // Process Lines
                if (data.lines != null && data.lines.Length > 0)
                {
                    int linenum = 1;
                    foreach (var line in data.lines)
                    {
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = data.header.DocEntry;
                        line.LineNum = linenum;

                        string lineQuery;
                        if (string.IsNullOrEmpty(line.ID))
                        {
                            line.CreatedDate = DateTime.Now;
                            line.CretedByUId = HttpContext.Session.GetString("UserID");
                            line.CretedByUName = HttpContext.Session.GetString("UserName");
                            line.ID = null;
                            lineQuery = generator.GenerateInsertQuery(line, "[Inventory_GoodsIssue_Row]", "ID");
                        }
                        else
                        {
                            lineQuery = generator.GenerateUpdateQuery(line, "[Inventory_GoodsIssue_Row]", "ID", line.ID, "");
                        }

                        await using (var cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }

                        linenum++;
                    }

                    await using (var cmd = new SqlCommand($"EXEC [Insert_InvtoryStock] '{data.header.DocEntry}','GI'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await using var rdr = await cmd.ExecuteReaderAsync();
                        while (await rdr.ReadAsync())
                        {
                            string success = rdr["Success"].ToString();
                            string message = rdr["Message"].ToString();
                   
                            if (!string.Equals(success, "true", StringComparison.OrdinalIgnoreCase))
                            {
                                await transaction.RollbackAsync();
                                return StatusCode(500, $"An error occurred: {message}");
                            }
                        }
                    }
                }

                await transaction.CommitAsync();
                return Json(new { Success = true, DocEntry = data.header.DocEntry, Message = "Goods Issue saved successfully." });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return Conflict("A record with the same value already exists.");
            }
            catch (Exception ex)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            finally
            {
                if (transaction != null) await transaction.DisposeAsync();
                await con.CloseAsync();
            }
        }
        #endregion
        #region MATERIAL IN
        public async Task<IActionResult> CREATEMATERIALIN([FromBody] MATERIALIN data, string flag, string doctype)
        {
            if (flag == "P")
                return await PARK(data, doctype);

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;

            await using var con = new SqlConnection(connectionString);
            await con.OpenAsync();

            try
            {
                transaction = con.BeginTransaction();

                // Process Header
                var header = data.header;
                header.UpdatedDate = DateTime.Now;
                header.UpdatedByUName = HttpContext.Session.GetString("UserName");
                header.UpdatedByUId = HttpContext.Session.GetString("UserID");

                Genrate_Query generator = new Genrate_Query();
                string query;

                if (string.IsNullOrEmpty(header.DocEntry))
                {
                    // New record
                    header.CreatedDate = DateTime.Now;
                    header.CretedByUId = HttpContext.Session.GetString("UserID");
                    header.CretedByUName = HttpContext.Session.GetString("UserName");
                    header.DocEntry = null;

                    await using (var cmd = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Inventory_MaterialIn_Head'", con, transaction))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                            header.Docnum = reader["Message"].ToString();
                    }

                    query = generator.GenerateInsertQuery(header, "[Inventory_MaterialIn_Head]", "DocEntry");
                }
                else
                {
                    query = generator.GenerateUpdateQuery(header, "[Inventory_MaterialIn_Head]", "DocEntry", header.DocEntry, "");
                }

                await using (var cmd = new SqlCommand(query, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();

                    if (string.IsNullOrEmpty(header.DocEntry))
                    {
                        cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                        header.DocEntry = (await cmd.ExecuteScalarAsync()).ToString();
                    }
                }

                // Process Lines
                if (data.lines != null && data.lines.Length > 0)
                {
                    int linenum = 1;
                    foreach (var line in data.lines)
                    {
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = data.header.DocEntry;
                        line.LineNum = linenum;

                        string lineQuery;
                        if (string.IsNullOrEmpty(line.ID))
                        {
                            line.CreatedDate = DateTime.Now;
                            line.CretedByUId = HttpContext.Session.GetString("UserID");
                            line.CretedByUName = HttpContext.Session.GetString("UserName");
                            line.ID = null;
                            lineQuery = generator.GenerateInsertQuery(line, "[Inventory_MaterialIn_Row]", "ID");
                        }
                        else
                        {
                            lineQuery = generator.GenerateUpdateQuery(line, "[Inventory_MaterialIn_Row]", "ID", line.ID, "");
                        }

                        await using (var cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }
                        linenum++;
                    }

                    await using (var cmd = new SqlCommand($"EXEC [Insert_InvtoryStock] '{data.header.DocEntry}','IMI'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await using var rdr = await cmd.ExecuteReaderAsync();
                        while (await rdr.ReadAsync())
                        {
                            string success = rdr["Success"].ToString();
                            string message = rdr["Message"].ToString();

                            if (!string.Equals(success, "true", StringComparison.OrdinalIgnoreCase))
                            {
                                await transaction.RollbackAsync();
                                return StatusCode(500, $"An error occurred: {message}");
                            }
                        }
                    }
                }

                await transaction.CommitAsync();
                return Json(new { Success = true, DocEntry = data.header.DocEntry, Message = "Document saved successfully." });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return Conflict("A record with the same value already exists.");
            }
            catch (Exception ex)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            finally
            {
                if (transaction != null) await transaction.DisposeAsync();
                await con.CloseAsync();
            }
        }
        public async Task<IActionResult> DELETEMATERIALIN(string id)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            string query = $@"
        DELETE FROM [Inventory_Stock] WHERE DocEntry = '{id}' AND ObjType='IMI';
        DELETE FROM [Inventory_MaterialIn_Row] WHERE DocEntry = '{id}';
        DELETE FROM [Inventory_MaterialIn_Head] WHERE DocEntry = '{id}';";

            await using var con = new SqlConnection(connectionString);
            await con.OpenAsync();

            try
            {
                await using var cmd = new SqlCommand(query, con);
                cmd.CommandTimeout = 300;
                await cmd.ExecuteNonQueryAsync();

                return Json(new { success = true, message = "Document deleted successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
            finally
            {
                await con.CloseAsync();
            }
        }
        #endregion
        #region MATERIAL OUT
        public async Task<IActionResult> CREATEMATERIALOUT([FromBody] MATERIALOUT data, string flag, string doctype)
        {
            if (flag == "P")
                return  await PARK(data, doctype);

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;

            await using var con = new SqlConnection(connectionString);
            await con.OpenAsync();

            try
            {
                transaction = con.BeginTransaction();

                var header = data.header;
                header.UpdatedDate = DateTime.Now;
                header.UpdatedByUName = HttpContext.Session.GetString("UserName");
                header.UpdatedByUId = HttpContext.Session.GetString("UserID");

                Genrate_Query generator = new Genrate_Query();
                string query;

                if (string.IsNullOrEmpty(header.DocEntry))
                {
                    header.CreatedDate = DateTime.Now;
                    header.CretedByUId = HttpContext.Session.GetString("UserID");
                    header.CretedByUName = HttpContext.Session.GetString("UserName");
                    header.DocEntry = null;

                    await using (var cmd = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Inventory_MaterialOut_Head'", con, transaction))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                            header.Docnum = reader["Message"].ToString();
                    }

                    query = generator.GenerateInsertQuery(header, "[Inventory_MaterialOut_Head]", "DocEntry");
                }
                else
                {
                    query = generator.GenerateUpdateQuery(header, "[Inventory_MaterialOut_Head]", "DocEntry", header.DocEntry, "");
                }

                await using (var cmd = new SqlCommand(query, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();

                    if (string.IsNullOrEmpty(header.DocEntry))
                    {
                        cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                        header.DocEntry = (await cmd.ExecuteScalarAsync()).ToString();
                    }
                }

                // Process lines
                if (data.lines != null && data.lines.Length > 0)
                {
                    int linenum = 1;
                    foreach (var line in data.lines)
                    {
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = data.header.DocEntry;
                        line.LineNum = linenum;

                        string lineQuery;
                        if (string.IsNullOrEmpty(line.ID))
                        {
                            line.CreatedDate = DateTime.Now;
                            line.CretedByUId = HttpContext.Session.GetString("UserID");
                            line.CretedByUName = HttpContext.Session.GetString("UserName");
                            line.ID = null;
                            lineQuery = generator.GenerateInsertQuery(line, "[Inventory_MaterialOut_Row]", "ID");
                        }
                        else
                        {
                            lineQuery = generator.GenerateUpdateQuery(line, "[Inventory_MaterialOut_Row]", "ID", line.ID, "");
                        }

                        await using (var cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }
                        linenum++;
                    }

                    await using (var cmd = new SqlCommand($"EXEC [Insert_InvtoryStock] '{data.header.DocEntry}','IMO'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await using var rdr = await cmd.ExecuteReaderAsync();
                        while (await rdr.ReadAsync())
                        {
                            string success = rdr["Success"].ToString();
                            string message = rdr["Message"].ToString();

                            if (!string.Equals(success, "true", StringComparison.OrdinalIgnoreCase))
                            {
                                await transaction.RollbackAsync();
                                return StatusCode(500, $"An error occurred: {message}");
                            }
                        }
                    }
                }

                string updateQtyQuery = @$"EXEC UpdateOpenQuantity 'Sale_SaleOrder_Row','Sale_SaleOrder_Head','Inventory_MaterialOut_Head','Inventory_MaterialOut_Row','{data.header.DocEntry}'";
                await using (var cmd = new SqlCommand(updateQtyQuery, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();
                }

                await transaction.CommitAsync();
                return Json(new { Success = true, DocEntry = data.header.DocEntry, Message = "Document saved successfully." });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return Conflict("A record with the same value already exists.");
            }
            catch (Exception ex)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            finally
            {
                if (transaction != null) await transaction.DisposeAsync();
                await con.CloseAsync();
            }
        }
        public async Task<IActionResult> DELETEMATERIALOUT(string id)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            await using var con = new SqlConnection(connectionString);
            await con.OpenAsync();

            try
            {
                int validation = 0;
                int docNum = 0;
                string message = "This Document Has Reference ";

                string checkQuery = $@"exec [Document_CheckBaseDocEntry] 'Sale_SaleOrder_Row','Sale_SaleOrder_Head','{id}','IMO'";
                await using (var cmd = new SqlCommand(checkQuery, con))
                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        validation = Convert.ToInt16(reader["RESULT"]);
                        docNum = Convert.ToInt16(reader["Docnum"]);
                        if (validation > 0)
                        {
                            message += " In Sales Order, Document No " + docNum + "!";
                            return Json(new { success = false, message });
                        }
                    }
                }

                string deleteQuery = $@"
            DELETE FROM [Inventory_Stock] WHERE DocEntry = '{id}' AND ObjType='IMO';
            DELETE FROM [Inventory_MaterialOut_Row] WHERE DocEntry = '{id}';
            DELETE FROM [Inventory_MaterialOut_Head] WHERE DocEntry = '{id}';";

                await using (var cmd = new SqlCommand(deleteQuery, con))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();
                }

                return Json(new { success = true, message = "Document deleted successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
            finally
            {
                await con.CloseAsync();
            }
        }
        #endregion
        #region PRICE LIST
        public async Task<IActionResult> BULKPRICELISTUPDATE([FromBody] PRISELIST data, string flag, string doctype)
        {
            if (flag == "P")
                return await PARK(data, doctype);

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;

            await using var con = new SqlConnection(connectionString);
            await con.OpenAsync();

            try
            {
                transaction = con.BeginTransaction();
                Genrate_Query generator = new Genrate_Query();

                if (data.lines != null && data.lines.Length > 0)
                {
                    foreach (var line in data.lines)
                    {
                        // Common audit fields
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");

                        string lineQuery;
                        if (string.IsNullOrEmpty(line.PriceSetID))
                        {
                            // New record
                            line.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd");
                            line.CretedByUId = HttpContext.Session.GetString("UserID");
                            line.CretedByUName = HttpContext.Session.GetString("UserName");
                            line.PriceSetID = null;
                            lineQuery = generator.GenerateInsertQuery(line, "[ItemPriceSetup]", "PriceSetID");
                        }
                        else
                        {
                            lineQuery = generator.GenerateUpdateQuery(line, "[ItemPriceSetup]", "PriceSetID", line.PriceSetID, "");
                        }

                        await using var cmd = new SqlCommand(lineQuery, con, transaction);
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                await transaction.CommitAsync();
                return Json(new { Success = true, DocEntry = "", Message = "Document saved successfully." });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return Conflict("A record with the same value already exists.");
            }
            catch (Exception ex)
            {
                if (transaction != null) await transaction.RollbackAsync();
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            finally
            {
                if (transaction != null) await transaction.DisposeAsync();
                await con.CloseAsync();
            }
        }
        #endregion
        #region PARK DOC
        public async Task<IActionResult> PARK([FromBody] dynamic data, string OBJType)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                ParkDoc generator = new ParkDoc();

                var parkdata = new Park
                {
                    OBJType = OBJType,
                    DocEntry = null,
                    CreatedDate = DateTime.Now,
                    CretedByUId = HttpContext.Session.GetString("UserID"),
                    CretedByUName = HttpContext.Session.GetString("UserName"),
                    Payload = JsonSerializer.Serialize(data),
                    RefNo = data.header.RefNo,
                    DocumentDate = DateTime.Now
                };

                // Assuming ParKDocument can be made async, otherwise keep as sync
                var result = await Task.Run(() => generator.ParKDocument(parkdata, connectionString));

                return Json(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        #endregion
    }
}



