using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TECHNOSYNCERP.Models;

namespace TECHNOSYNCERP.Controllers
{
    public class MasterController : Controller
    {

        private readonly IConfiguration _configuration;
        public MasterController(ILogger<MasterController> logger, IConfiguration configuration)
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
        public async Task<IActionResult> Inventory()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("IM");
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
        public async Task<IActionResult> BusinessPartner()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("BP");
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
        public IActionResult GETBPPARTNER(string type)
        {
           
            var connStr = _configuration.GetConnectionString("ErpConnection");
            List<Dictionary<string, string>> List = new List<Dictionary<string, string>>();

            try
            {
                using (var con = new SqlConnection(connStr))
                {
                    string query = @$"EXEC GET_BusinessPartner '{type}'";
                    using (var cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var obj = new Dictionary<string, string>();
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        obj[reader.GetName(i)] = reader.GetValue(i).ToString();
                                    }
                                    List.Add(obj);
                                }
                            }
                        }
                    }
                }
                return Json(List);
            }
            catch (Exception ex)
            {
                // Log exception here if using ILogger
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> Ledger()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("LED");
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
        public async Task<IActionResult>  WarehouseSetup()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("WARE");
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
        public async Task<IActionResult> TaxMaster()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("TM");
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
        public async Task<IActionResult> TaxCombination()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("TC");
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
        public async Task<IActionResult> Company()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("COM");
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
        public async Task<IActionResult> Country()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("COU");
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
        public async Task<IActionResult> State()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("STATE");
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
        public async Task<IActionResult> Employee()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("MEMP");
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
        public async Task<IActionResult> Banks()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("BANK");
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
        public async Task<IActionResult> Tds()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("TDSM");
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
        public async Task<IActionResult> Freight()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce =await GETBTNAUTH("FRE");
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
        public async Task<IActionResult> FinancialYear()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("FYI");
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
      
        public async Task<IActionResult> GETWHS(string userId)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var resultList = new List<Dictionary<string, string>>();
            string query = @"SELECT * FROM WhsMaster";

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
     
        public async Task<IActionResult> GETWAREHOUSE(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var resultList = new List<Dictionary<string, string>>();
            string query = @"EXEC Item_WhsDetails @ItemID";

            try
            {
                await using (var con = new SqlConnection(connStr))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ItemID", id ?? (object)DBNull.Value);

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
                }

                return Json(resultList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETTAXSETUP()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var resultList = new List<Dictionary<string, string>>();
            string query = @"SELECT * FROM TaxTyp";

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
        public async Task<IActionResult> GETAXMASTER(string userId)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var resultList = new List<Dictionary<string, string>>();
            string query = @"SELECT * FROM TaxTyp";

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
        public async Task<IActionResult> GETTAXFORMDATA(string Id)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = @$"EXEC Get_TaxCombDetails @Id ";

                var dataList = new List<Dictionary<string, object>>();

                await using (var con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@Id", Id ?? (object)DBNull.Value);

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object? value = await rdr.IsDBNullAsync(i) ? "" : rdr.GetValue(i);

                                    // Convert DATE values to string format without time
                                    if (value is DateTime dateValue && dateValue.TimeOfDay == TimeSpan.Zero)
                                    {
                                        row[columnName] = dateValue.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        row[columnName] = value!;
                                    }
                                }

                                dataList.Add(row);
                            }
                        }
                    }
                }

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETTAXCOMBINATIONS()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = @"SELECT * FROM TaxCode_Mst";

                var dataList = new List<Dictionary<string, object>>();

                await using (var con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object? value = await rdr.IsDBNullAsync(i) ? "" : rdr.GetValue(i);

                                    // Convert DATE values to string format without time
                                    if (value is DateTime dateValue && dateValue.TimeOfDay == TimeSpan.Zero)
                                    {
                                        row[columnName] = dateValue.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        row[columnName] = value!;
                                    }
                                }

                                dataList.Add(row);
                            }
                        }
                    }
                }

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETTAXCODE()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = @"
            SELECT 
                TaxCodeId,
                TaxCode,
                TaxCalRate,
                (TaxCalRate / 2) AS CGST,
                (TaxCalRate / 2) AS SGST,
                TaxCalRate AS UTGST,
                TaxCalRate AS IGST
            FROM 
                TaxCode_Mst 
            WHERE 
                IsActive = 'Y';
        ";

                var dataList = new List<Dictionary<string, object>>();

                await using (var con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object? value = await rdr.IsDBNullAsync(i) ? "" : rdr.GetValue(i);

                                    // Convert DATE values to string format without time
                                    if (value is DateTime dateValue && dateValue.TimeOfDay == TimeSpan.Zero)
                                    {
                                        row[columnName] = dateValue.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        row[columnName] = value!;
                                    }
                                }

                                dataList.Add(row);
                            }
                        }
                    }
                }

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETFREIGHTTAXCODE()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = @"
            SELECT 
                TaxCodeId,
                TaxCode,
                TaxCalRate,
                (TaxCalRate / 2) AS CGST,
                (TaxCalRate / 2) AS SGST,
                TaxCalRate AS UTGST,
                TaxCalRate AS IGST
            FROM 
                TaxCode_Mst 
            WHERE 
                IsActive = 'Y' AND Freight_app = 'Y';
        ";

                var dataList = new List<Dictionary<string, object>>();

                await using (var con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object? value = await rdr.IsDBNullAsync(i) ? "" : rdr.GetValue(i);

                                    // Convert DATE values to string format without time
                                    if (value is DateTime dateValue && dateValue.TimeOfDay == TimeSpan.Zero)
                                    {
                                        row[columnName] = dateValue.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        row[columnName] = value!;
                                    }
                                }

                                dataList.Add(row);
                            }
                        }
                    }
                }

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETHSNMASTER()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = @"
            SELECT T1.TaxCode, * 
            FROM [HSNMaster] T0
            INNER JOIN TaxCode_Mst T1 ON T1.TaxCodeId = T0.TaxCodeId
        ";

                var dataList = new List<Dictionary<string, object>>();

                await using (var con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object? value = await rdr.IsDBNullAsync(i) ? "" : rdr.GetValue(i);

                                    // Convert DATE values to string format without time
                                    if (value is DateTime dateValue && dateValue.TimeOfDay == TimeSpan.Zero)
                                    {
                                        row[columnName] = dateValue.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        row[columnName] = value!;
                                    }
                                }

                                dataList.Add(row);
                            }
                        }
                    }
                }

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETHSN(string flag)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = @"SELECT * FROM [HSNMaster]";

                var dataList = new List<Dictionary<string, object>>();

                await using (var con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object? value = await rdr.IsDBNullAsync(i) ? "" : rdr.GetValue(i);

                                    // Convert DATE values to string format without time
                                    if (value is DateTime dateValue && dateValue.TimeOfDay == TimeSpan.Zero)
                                    {
                                        row[columnName] = dateValue.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        row[columnName] = value!;
                                    }
                                }

                                dataList.Add(row);
                            }
                        }
                    }
                }

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETUOMLIST()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = @"SELECT * FROM [UomTyp]";
                var dataList = new List<Dictionary<string, object>>();

                await using (var con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object? value = await rdr.IsDBNullAsync(i) ? "" : rdr.GetValue(i);

                                    // Convert DATE values to string format without time
                                    if (value is DateTime dateValue && dateValue.TimeOfDay == TimeSpan.Zero)
                                    {
                                        row[columnName] = dateValue.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        row[columnName] = value!;
                                    }
                                }

                                dataList.Add(row);
                            }
                        }
                    }
                }

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETUOM()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = @"SELECT * FROM [UomTyp] WHERE IsActive = 'A'";
                var dataList = new List<Dictionary<string, object>>();

                await using (var con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object? value = await rdr.IsDBNullAsync(i) ? "" : rdr.GetValue(i);

                                    // Convert DATE values to string format without time
                                    if (value is DateTime dateValue && dateValue.TimeOfDay == TimeSpan.Zero)
                                    {
                                        row[columnName] = dateValue.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        row[columnName] = value!;
                                    }
                                }

                                dataList.Add(row);
                            }
                        }
                    }
                }

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETNEXTITEM()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = "EXEC Item_NextItemCode";
                var dataList = new List<Dictionary<string, object>>();

                await using (var con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object? value = await rdr.IsDBNullAsync(i) ? "" : rdr.GetValue(i);

                                    // Convert DATE values to string format without time
                                    if (value is DateTime dateValue && dateValue.TimeOfDay == TimeSpan.Zero)
                                    {
                                        row[columnName] = dateValue.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        row[columnName] = value!;
                                    }
                                }

                                dataList.Add(row);
                            }
                        }
                    }
                }

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETITEMGRPLIST()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = "SELECT * FROM [ItGrpMaster]";
                var dataList = new List<Dictionary<string, object>>();

                await using (var con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object? value = await rdr.IsDBNullAsync(i) ? "" : rdr.GetValue(i);

                                    // Convert DATE values to string format without time
                                    if (value is DateTime dateValue && dateValue.TimeOfDay == TimeSpan.Zero)
                                    {
                                        row[columnName] = dateValue.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        row[columnName] = value!;
                                    }
                                }

                                dataList.Add(row);
                            }
                        }
                    }
                }

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETITEMGRP()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = "SELECT * FROM [ItGrpMaster] WHERE IsActive = 'A'";
                var dataList = new List<Dictionary<string, object>>();

                await using (var con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object? value = await rdr.IsDBNullAsync(i) ? "" : rdr.GetValue(i);

                                    // Convert DATE values to string format without time
                                    if (value is DateTime dateValue && dateValue.TimeOfDay == TimeSpan.Zero)
                                    {
                                        row[columnName] = dateValue.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        row[columnName] = value!;
                                    }
                                }

                                dataList.Add(row);
                            }
                        }
                    }
                }

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETITEMLIST()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = "EXEC ItemMasterList ''";
                var dataList = new List<Dictionary<string, object>>();

                await using (var con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object? value = await rdr.IsDBNullAsync(i) ? "" : rdr.GetValue(i);

                                    // Convert DATE values to string format without time
                                    if (value is DateTime dateValue && dateValue.TimeOfDay == TimeSpan.Zero)
                                    {
                                        row[columnName] = dateValue.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        row[columnName] = value!;
                                    }
                                }

                                dataList.Add(row);
                            }
                        }
                    }
                }

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETITEMBYID(string id)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = @$"EXEC ItemMasterList '','{id}'";
                var dataList = new List<Dictionary<string, object>>();

                await using (var con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object? value = await rdr.IsDBNullAsync(i) ? "" : rdr.GetValue(i);

                                    // Convert DATE values to string format without time
                                    if (value is DateTime dateValue && dateValue.TimeOfDay == TimeSpan.Zero)
                                    {
                                        row[columnName] = dateValue.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        row[columnName] = value!;
                                    }
                                }

                                dataList.Add(row);
                            }
                        }
                    }
                }

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETPRICESETUPBYID(string id)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = @$"EXEC GetItemPriceData '{id}'";
                var dataList = new List<Dictionary<string, object>>();

                await using (var con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object? value = await rdr.IsDBNullAsync(i) ? "" : rdr.GetValue(i);

                                    // Convert DATE values to string format without time
                                    if (value is DateTime dateValue && dateValue.TimeOfDay == TimeSpan.Zero)
                                    {
                                        row[columnName] = dateValue.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        row[columnName] = value!;
                                    }
                                }

                                dataList.Add(row);
                            }
                        }
                    }
                }

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETCOUNTRY()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = "SELECT * FROM [CounMaster]";
                var dataList = new List<Dictionary<string, object>>();

                await using (var con = new SqlConnection(connectionString))
                {
                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;
                        await con.OpenAsync();

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object value = rdr.IsDBNull(i) ? "" : rdr.GetValue(i);

                                    // Convert DATE values to string format without time
                                    if (value is DateTime dateValue && dateValue.TimeOfDay == TimeSpan.Zero)
                                    {
                                        row[columnName] = dateValue.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        row[columnName] = value;
                                    }
                                }

                                dataList.Add(row);
                            }
                        }
                    }
                }

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETCOUNTRYBYID(string countryId)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = "SELECT * FROM [CounMaster] WHERE COUN_ID = @CountryId";
                var dataList = new List<Dictionary<string, object>>();

                await using (var con = new SqlConnection(connectionString))
                {
                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@CountryId", countryId);

                        await con.OpenAsync();

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object value = rdr.IsDBNull(i) ? "" : rdr.GetValue(i);

                                    // Convert DATE values to string format without time
                                    if (value is DateTime dateValue && dateValue.TimeOfDay == TimeSpan.Zero)
                                    {
                                        row[columnName] = dateValue.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        row[columnName] = value;
                                    }
                                }

                                dataList.Add(row);
                            }
                        }
                    }
                }

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETSTATE()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = @"
            SELECT 
                T1.Code AS CountryCode,
                T1.Name AS CountryName,
                T0.*
            FROM StateMaster T0
            INNER JOIN CounMaster T1 ON T0.Coun_Id = T1.Coun_Id";

                var dataList = new List<Dictionary<string, object>>();

                await using (var con = new SqlConnection(connectionString))
                {
                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;
                        await con.OpenAsync();

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object value = rdr.IsDBNull(i) ? "" : rdr.GetValue(i);

                                    // Convert DATE values to string format without time
                                    if (value is DateTime dateValue && dateValue.TimeOfDay == TimeSpan.Zero)
                                    {
                                        row[columnName] = dateValue.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        row[columnName] = value;
                                    }
                                }

                                dataList.Add(row);
                            }
                        }
                    }
                }

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETSTATEBYID(string id)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = @$"
            SELECT T1.Code AS CountryCode, T1.Name AS CountryName, * 
            FROM StateMaster T0
            INNER JOIN CounMaster T1 ON T0.Coun_Id = T1.Coun_Id 
            WHERE Stat_Id = @id";

                var dataList = new List<Dictionary<string, object>>();

                await using (var con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@id", id);

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object? value = await rdr.IsDBNullAsync(i) ? "" : rdr.GetValue(i);

                                    // Convert DATE values to string format without time
                                    if (value is DateTime dateValue && dateValue.TimeOfDay == TimeSpan.Zero)
                                        row[columnName] = dateValue.ToString("dd/MM/yyyy");
                                    else
                                        row[columnName] = value!;
                                }

                                dataList.Add(row);
                            }
                        }
                    }
                }

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETEMPLOYEE()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = @"
            SELECT
            T1.Code AS CountryCode,
            T1.Name AS CountryName,
            T2.RoleName,
            *
            FROM [Master_EmployeeMaster] T0
            INNER JOIN CounMaster T1 ON T0.Coun_Id = T1.Coun_Id
            LEFT JOIN Master_Employeerolemaster T2 ON  T2.RoleID =T0.RoleID
             ";

                var dataList = new List<Dictionary<string, object>>();

                await using (var con = new SqlConnection(connectionString))
                {
                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;
                        await con.OpenAsync();

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object value = rdr.IsDBNull(i) ? "" : rdr.GetValue(i);

                                    // Convert DATE values to string format without time
                                    if (value is DateTime dateValue && dateValue.TimeOfDay == TimeSpan.Zero)
                                    {
                                        row[columnName] = dateValue.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        row[columnName] = value;
                                    }
                                }

                                dataList.Add(row);
                            }
                        }
                    }
                }

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETEMPLOYEEBYID(string id)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = @$"
            SELECT T1.Code AS CountryCode, T1.Name AS CountryName, * 
            FROM Master_EmployeeMaster T0
            INNER JOIN CounMaster T1 ON T0.Coun_Id = T1.Coun_Id 
            WHERE EmployeeID = @id";

                var dataList = new List<Dictionary<string, object>>();

                await using (var con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@id", id);

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object? value = await rdr.IsDBNullAsync(i) ? "" : rdr.GetValue(i);

                                    // Convert DATE values to string format without time
                                    if (value is DateTime dateValue && dateValue.TimeOfDay == TimeSpan.Zero)
                                        row[columnName] = dateValue.ToString("dd/MM/yyyy");
                                    else
                                        row[columnName] = value!;
                                }

                                dataList.Add(row);
                            }
                        }
                    }
                }

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public async Task<IActionResult> GETBANK()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = @"
            SELECT T1.Code AS CountryCode, T1.Name AS CountryName, * 
            FROM BankMaster T0
            INNER JOIN CounMaster T1 ON T0.Coun_Id = T1.Coun_Id";

                var dataList = new List<Dictionary<string, object>>();

                await using (var con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object? value = await rdr.IsDBNullAsync(i) ? "" : rdr.GetValue(i);

                                    // Convert DATE values to string format without time
                                    if (value is DateTime dateValue && dateValue.TimeOfDay == TimeSpan.Zero)
                                        row[columnName] = dateValue.ToString("dd/MM/yyyy");
                                    else
                                        row[columnName] = value!;
                                }

                                dataList.Add(row);
                            }
                        }
                    }
                }

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETBANKBYID(string id)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = @$"
            SELECT T1.Code AS CountryCode, T1.Name AS CountryName, *
            FROM BankMaster T0
            INNER JOIN CounMaster T1 ON T0.Coun_Id = T1.Coun_Id
            WHERE BankID = @BankID";

                var dataList = new List<Dictionary<string, object>>();

                await using (var con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@BankID", id);

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object? value = await rdr.IsDBNullAsync(i) ? "" : rdr.GetValue(i);

                                    if (value is DateTime dateValue && dateValue.TimeOfDay == TimeSpan.Zero)
                                        row[columnName] = dateValue.ToString("dd/MM/yyyy");
                                    else
                                        row[columnName] = value!;
                                }

                                dataList.Add(row);
                            }
                        }
                    }
                }

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETSTATEBYCOUNTRY(string id)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = @"SELECT * FROM StateMaster WHERE Coun_Id = @CounId";
                var dataList = new List<Dictionary<string, object>>();

                await using (var con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@CounId", id);

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object? value = await rdr.IsDBNullAsync(i) ? "" : rdr.GetValue(i);

                                    if (value is DateTime dateValue && dateValue.TimeOfDay == TimeSpan.Zero)
                                        row[columnName] = dateValue.ToString("dd/MM/yyyy");
                                    else
                                        row[columnName] = value!;
                                }

                                dataList.Add(row);
                            }
                        }
                    }
                }

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETCOMPANY()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = @"
            SELECT 
                T1.Code AS 'StateCode',
                T1.Name AS 'StateName',
                T2.Code AS 'CountryCode',
                T2.Name AS 'CountryName',
                T0.* 
            FROM CompanyMaster T0
            INNER JOIN StateMaster T1 ON T0.Stat_Id = T1.Stat_Id
            INNER JOIN CounMaster T2 ON T0.Coun_Id = T2.Coun_Id";

                var dataList = new List<Dictionary<string, object>>();

                await using (var con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object? value = await rdr.IsDBNullAsync(i) ? "" : rdr.GetValue(i);

                                    // Convert DATE values to string format without time
                                    if (value is DateTime dateValue && dateValue.TimeOfDay == TimeSpan.Zero)
                                        row[columnName] = dateValue.ToString("dd/MM/yyyy");
                                    else
                                        row[columnName] = value!;
                                }

                                dataList.Add(row);
                            }
                        }
                    }
                }

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETCOMPANYBYID(string id)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = @"
            SELECT 
                T1.Code AS 'StateCode',
                T1.Name AS 'StateName',
                T2.Code AS 'CountryCode',
                T2.Name AS 'CountryName',
                T0.* 
            FROM CompanyMaster T0
            INNER JOIN StateMaster T1 ON T0.Stat_Id = T1.Stat_Id
            INNER JOIN CounMaster T2 ON T0.Coun_Id = T2.Coun_Id
            WHERE T0.CompanyID = @CompanyID";

                var dataList = new List<Dictionary<string, object>>();

                await using (var con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@CompanyID", id);

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object? value = await rdr.IsDBNullAsync(i) ? "" : rdr.GetValue(i);

                                    // Convert DATE values to string format without time
                                    if (value is DateTime dateValue && dateValue.TimeOfDay == TimeSpan.Zero)
                                        row[columnName] = dateValue.ToString("dd/MM/yyyy");
                                    else
                                        row[columnName] = value!;
                                }

                                dataList.Add(row);
                            }
                        }
                    }
                }

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETLEDGER()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var resultList = new List<Dictionary<string, string>>();

            await using (var con = new SqlConnection(connStr))
            {
                string query = @"
            SELECT 
                (SELECT CASE 
                        WHEN T11.ControlAcc IS NULL THEN T0.ControlAcc
                        WHEN T11.ControlAcc = 'N' THEN T0.ControlAcc
                        ELSE 'Y' 
                    END AS 'ControlAcc' 
                 FROM LedgerGroup T11 
                 WHERE T11.Id IN (T0.BaseID)
                ) AS ControlAccc,
                *
            FROM LedgerGroup T0";

                await using (var cmd = new SqlCommand(query, con))
                {
                    await con.OpenAsync();

                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var obj = new Dictionary<string, string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                obj[reader.GetName(i)] = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString();
                            }
                            resultList.Add(obj);
                        }
                    }
                }
            }

            return Json(resultList);
        }
        public async Task<IActionResult> GETLEDGERBYID(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var resultList = new List<Dictionary<string, string>>();

            await using (var con = new SqlConnection(connStr))
            {
                string query = @$"
            SELECT *,
                (SELECT CASE 
                        WHEN T11.ControlAcc IS NULL THEN T0.ControlAcc
                        WHEN T11.ControlAcc = 'N' THEN T0.ControlAcc
                        ELSE 'Y' 
                    END AS 'ControlAccc' 
                 FROM LedgerGroup T11 
                 WHERE T11.Id IN (T0.BaseID)
                ) AS ControlAcc
            FROM LedgerGroup T0
            WHERE T0.ID = @ID";

                await using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    await con.OpenAsync();

                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var obj = new Dictionary<string, string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                obj[reader.GetName(i)] = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString();
                            }
                            resultList.Add(obj);
                        }
                    }
                }
            }

            return Json(resultList);
        }
        public async Task<IActionResult> GETBPPARTNERBYID(string id)
        {
            try
            {
                var connStr = _configuration.GetConnectionString("ErpConnection");
                var Ledgerdata = new List<Dictionary<string, string>>();
                var Addressdata = new List<Dictionary<string, string>>();
                var BankData = new List<Dictionary<string, string>>();
                var Balance = new List<Dictionary<string, string>>();

                await using (var con = new SqlConnection(connStr))
                {
                    await con.OpenAsync();

                    async Task<List<Dictionary<string, string>>> GetDataAsync(string query)
                    {
                        var result = new List<Dictionary<string, string>>();
                        await using (var cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@ID", id);

                            await using (var reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    var obj = new Dictionary<string, string>();
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        obj[reader.GetName(i)] = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString();
                                    }
                                    result.Add(obj);
                                }
                            }
                        }
                        return result;
                    }

                    Ledgerdata = await GetDataAsync("SELECT * FROM LedgerGroup WHERE ID = @ID");
                    Addressdata = await GetDataAsync("SELECT * FROM LedgerAddress WHERE LedgerID = @ID");
                    BankData = await GetDataAsync("SELECT * FROM LedgerBankDetails WHERE LedgerID = @ID");
                    string query1 =@$"
                                 SELECT SUM(T1.Debit - T1.Credit) AS 'BALANCE' FROM Accounts_JournalEntry_Head T0 
                                 INNER JOIN Accounts_JournalEntry_Row T1 ON T0.DocEntry = T1.DocEntry WHERE T1.LedgerID = @ID ";
                     Balance = await GetDataAsync(query1);


                }

                return Json(new { Ledgerdata, Addressdata, BankData, Balance });
            }
            catch (SqlException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
           
        }
        public async Task<IActionResult> GETADDRES(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var resultList = new List<Dictionary<string, string>>();

            await using var con = new SqlConnection(connStr);
            string query = @"
        SELECT 
            T1.Code AS StateCode,
            T1.Name AS StateName,
            T2.Code AS CountryCode,
            T2.Name AS CountryName,
            *
        FROM LedgerAddress T0
        INNER JOIN StateMaster T1 ON T0.Stat_Id = T1.Stat_Id
        INNER JOIN CounMaster T2 ON T0.Coun_Id = T2.Coun_Id
        WHERE LedgerID = @LedgerID";

            await using var cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@LedgerID", id);

            await con.OpenAsync();

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var obj = new Dictionary<string, string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    obj[reader.GetName(i)] = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString();
                }
                resultList.Add(obj);
            }

            return Json(resultList);
        }
        public async Task<IActionResult> GETTERMSBYID(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var resultList = new List<Dictionary<string, string>>();

            await using var con = new SqlConnection(connStr);
            string query = "SELECT * FROM CreditDaysMaster WHERE CredID = @CredID";

            await using var cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@CredID", id);

            await con.OpenAsync();

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var obj = new Dictionary<string, string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    obj[reader.GetName(i)] = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString();
                }
                resultList.Add(obj);
            }

            return Json(resultList);
        }
        public async Task<IActionResult> GETNEXTLEDGERCODE(string flag)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var resultList = new List<Dictionary<string, string>>();

            await using var con = new SqlConnection(connStr);
            string query = "EXEC Ledger_NextLedgerCode @Flag";

            await using var cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Flag", flag);

            await con.OpenAsync();

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var obj = new Dictionary<string, string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    obj[reader.GetName(i)] = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString();
                }
                resultList.Add(obj);
            }

            return Json(resultList);
        }
        public async Task<IActionResult> GETTERMS()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var resultList = new List<Dictionary<string, string>>();

            await using var con = new SqlConnection(connStr);
            string query = "SELECT * FROM CreditDaysMaster";

            await using var cmd = new SqlCommand(query, con);

            await con.OpenAsync();

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var obj = new Dictionary<string, string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    obj[reader.GetName(i)] = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString();
                }
                resultList.Add(obj);
            }

            return Json(resultList);
        }
        public async Task<IActionResult> GETBANKACCOUNTLEDGERBYID(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var resultList = new List<Dictionary<string, string>>();

            await using var con = new SqlConnection(connStr);
            string query = @"
        SELECT T0.*, T2.GroupName, T1.BankCode, T1.BankName
        FROM LedgerBankDetails T0
        LEFT JOIN BankMaster T1 ON T0.BankID = T1.BankID
        INNER JOIN LedgerGroup T2 ON T0.LedgerID = T2.ID
        WHERE T0.LedgerID = @LedgerID";

            await using var cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@LedgerID", id);

            await con.OpenAsync();
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var obj = new Dictionary<string, string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    obj[reader.GetName(i)] = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString();
                }
                resultList.Add(obj);
            }

            return Json(resultList);
        }
        public async Task<IActionResult> GETBANKACCOUNTLEDGER()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var resultList = new List<Dictionary<string, string>>();

            await using var con = new SqlConnection(connStr);
            string query = "SELECT * FROM LedgerBankDetails";

            await using var cmd = new SqlCommand(query, con);
            await con.OpenAsync();
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var obj = new Dictionary<string, string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    obj[reader.GetName(i)] = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString();
                }
                resultList.Add(obj);
            }

            return Json(resultList);
        }
        public async Task<IActionResult> GETTDS()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var resultList = new List<Dictionary<string, string>>();

            await using var con = new SqlConnection(connStr);
            string query = "SELECT * FROM Master_TDS";

            await using var cmd = new SqlCommand(query, con);
            await con.OpenAsync();
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var obj = new Dictionary<string, string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    obj[reader.GetName(i)] = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString();
                }
                resultList.Add(obj);
            }

            return Json(resultList);
        }
        public async Task<IActionResult> GETTDSBYASSETYP(string type)
        {
            try
            {
                var connStr = _configuration.GetConnectionString("ErpConnection");
                var resultList = new List<Dictionary<string, string>>();

                await using var con = new SqlConnection(connStr);
                string query = "SELECT * FROM Master_TDS WHERE Assessee = @Assessee";

                await using var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Assessee", type);

                await con.OpenAsync();
                await using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var obj = new Dictionary<string, string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        obj[reader.GetName(i)] = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString();
                    }
                    resultList.Add(obj);
                }

                return Json(resultList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }
        public async Task<IActionResult> GETTDSBYID(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var resultList = new List<Dictionary<string, string>>();

            await using var con = new SqlConnection(connStr);
            string query = "SELECT * FROM Master_TDS WHERE TDSID = @TDSID";
            await using var cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@TDSID", id);

            await con.OpenAsync();
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var obj = new Dictionary<string, string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    obj[reader.GetName(i)] = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString();
                }
                resultList.Add(obj);
            }

            return Json(resultList);
        }
        public async Task<IActionResult> GETFYYEAR()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var resultList = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = @"
            SELECT 
                [FYearId],
                [FYear],
                CONVERT(VARCHAR(10), [StarDate], 120) AS [StarDate],
                CONVERT(VARCHAR(10), [EndDate], 120) AS [EndDate],
                [Year],
                [Status],
                [CretedByUId],
                [CretedByUName],
                CONVERT(VARCHAR(19), [CreatedDate], 120) AS [CreatedDate],
                [UpdatedByUId],
                [UpdatedByUName],
                CONVERT(VARCHAR(19), [UpdatedDate], 120) AS [UpdatedDate]
            FROM Master_FYear";

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();
                await using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var obj = new Dictionary<string, string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        obj[reader.GetName(i)] = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString();
                    }
                    resultList.Add(obj);
                }

                return Json(resultList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETFYEARBYID(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var resultList = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = @"
            SELECT 
                [FYearId],
                [FYear],
                CONVERT(VARCHAR(10), [StarDate], 120) AS [StarDate],
                CONVERT(VARCHAR(10), [EndDate], 120) AS [EndDate],
                [Year],
                [Status],
                [CretedByUId],
                [CretedByUName],
                CONVERT(VARCHAR(19), [CreatedDate], 120) AS [CreatedDate],
                [UpdatedByUId],
                [UpdatedByUName],
                CONVERT(VARCHAR(19), [UpdatedDate], 120) AS [UpdatedDate]
            FROM Master_FYear
            WHERE FYearId = @FYearId";

                await using var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FYearId", id);

                await con.OpenAsync();
                await using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var obj = new Dictionary<string, string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        obj[reader.GetName(i)] = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString();
                    }
                    resultList.Add(obj);
                }

                return Json(resultList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETACTIVEBPLADGER(string flag)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var resultList = new List<Dictionary<string, string>>();

            await using var con = new SqlConnection(connStr);
            string query = flag == "C"
                ? "SELECT TOP(1) CUSTOMERLID AS LegerID FROM CompanyMaster"
                : "SELECT TOP(1) SUPPLIERLID AS LegerID FROM CompanyMaster";

            await using var cmd = new SqlCommand(query, con);
            await con.OpenAsync();
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var obj = new Dictionary<string, string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    obj[reader.GetName(i)] = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString();
                }
                resultList.Add(obj);
            }

            return Json(resultList);
        }
        public async Task<IActionResult> GETFREIGHT()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var resultList = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = "SELECT * FROM Master_Freight";
                await using var cmd = new SqlCommand(query, con);

                await con.OpenAsync();
                await using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var obj = new Dictionary<string, string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        obj[reader.GetName(i)] = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString();
                    }
                    resultList.Add(obj);
                }

                return Json(resultList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETFREIGHTBYID(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var resultList = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = "SELECT * FROM Master_Freight WHERE FreID = @FreID";
                await using var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FreID", id);

                await con.OpenAsync();
                await using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var obj = new Dictionary<string, string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        obj[reader.GetName(i)] = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString();
                    }
                    resultList.Add(obj);
                }

                return Json(resultList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETACTIVEFREIGHT()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var resultList = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = "SELECT * FROM Master_Freight WHERE IsActive = 'A'";
                await using var cmd = new SqlCommand(query, con);

                await con.OpenAsync();
                await using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var obj = new Dictionary<string, string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        obj[reader.GetName(i)] = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString();
                    }
                    resultList.Add(obj);
                }

                return Json(resultList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


        [HttpPost]
        public IActionResult Warehouses([FromBody] WhsMaster data)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var item = data;
            using (var con = new SqlConnection(connStr))
            {
                con.Open();
                using (var transaction = con.BeginTransaction())
                {
                    try
                    {
                        if (string.IsNullOrEmpty(item.ID))
                        {
                            // Insert new warehouse
                            var insertQuery = @"
                                INSERT INTO [WhsMaster] 
                                ([WhsCode], [WhsName], [Locked], 
                                 [CretedByUId], [CretedByUName], [CreatedDate],
                                 [UpdatedByUId], [UpdatedByUName], [UpdatedDate]) 
                                VALUES 
                                (@WhsCode, @WhsName, @Locked,
                                 @CretedByUId, @CretedByUName, @CreatedDate,
                                 @UpdatedByUId, @UpdatedByUName, @UpdatedDate);
                                SELECT SCOPE_IDENTITY();";

                            using (var cmd = new SqlCommand(insertQuery, con, transaction))
                            {
                                cmd.Parameters.AddWithValue("@WhsCode", item.WhsCode);
                                cmd.Parameters.AddWithValue("@WhsName", (object)item.WhsName ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Locked", "N");
                                cmd.Parameters.AddWithValue("@CretedByUId", HttpContext.Session.GetString("UserID"));
                                cmd.Parameters.AddWithValue("@CretedByUName", HttpContext.Session.GetString("UserName"));
                                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                                cmd.Parameters.AddWithValue("@UpdatedByUId", HttpContext.Session.GetString("UserID"));
                                cmd.Parameters.AddWithValue("@UpdatedByUName", HttpContext.Session.GetString("UserName"));
                                cmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);

                                // Execute and get the new ID
                                var newId = cmd.ExecuteScalar();
                                item.WhsCode = newId.ToString();
                            }
                        }
                        else
                        {
                            // Update existing warehouse
                            var updateQuery = @"
                        UPDATE [WhsMaster] SET 
                        [WhsName] = @WhsName,
                        [UpdatedByUId] = @UpdatedByUId,
                        [UpdatedByUName] = @UpdatedByUName,
                        [UpdatedDate] = @UpdatedDate
                        WHERE WhsCode = @WhsCode";

                            using (var cmd = new SqlCommand(updateQuery, con, transaction))
                            {
                                cmd.Parameters.AddWithValue("@WhsName", (object)item.WhsName ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@UpdatedByUId", HttpContext.Session.GetString("UserID"));
                                cmd.Parameters.AddWithValue("@UpdatedByUName", HttpContext.Session.GetString("UserName"));
                                cmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                                cmd.Parameters.AddWithValue("@WhsCode", item.WhsCode);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        return Json(new { success = true, message = "Warehouse updated successfully.", warehouse = data });
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        return Json(new { success = false, message = "Database error: " + ex.Message });
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return Json(new { success = false, message = "Unexpected error: " + ex.Message });
                    }
                }
            }
        }
        public IActionResult DeleteWarehouse([FromBody] WhsMaster data)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var item = data;

            if (string.IsNullOrEmpty(item.ID))
            {
                return Json(new { success = false, message = "Warehouse ID is required for deletion." });
            }

            using (var con = new SqlConnection(connStr))
            {
                con.Open();
                using (var transaction = con.BeginTransaction())
                {
                    try
                    {
                        var deleteQuery = @"DELETE FROM [WhsMaster] WHERE ID = @ID";

                        using (var cmd = new SqlCommand(deleteQuery, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@ID", item.ID);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected == 0)
                            {
                                transaction.Rollback();
                                return Json(new { success = false, message = "No warehouse found with the specified ID." });
                            }
                        }

                        transaction.Commit();
                        return Json(new { success = true, message = "Warehouse deleted successfully." });
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        return Json(new { success = false, message = "Database error: " + ex.Message });
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return Json(new { success = false, message = "Unexpected error: " + ex.Message });
                    }
                }
            }
        }

        #region Tax Master
        public async Task<IActionResult> ADDTAXMASTER(Taxtype_mst Data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");
            try
            {
                // Set timestamps and session info
                Data.CreatedDate = DateTime.Now;
                Data.UpdatedDate = DateTime.Now;
                Data.CretedByUName = HttpContext.Session.GetString("UserName");
                Data.UpdatedByUName = HttpContext.Session.GetString("UserName");
                Data.CretedByUId = HttpContext.Session.GetString("UserID");
                Data.UpdatedByUId = HttpContext.Session.GetString("UserID");

                // Generate insert query
                Genrate_Query genrate = new Genrate_Query();
                string insertQuery = genrate.GenerateInsertQuery(Data, "[TaxTyp]", "TaxTypeId");

                // Execute query asynchronously
                await using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    await using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { Success = true, Message = "Tax Type Added Successfully..!" });
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    return Conflict("A record with the same value already exists.");
                }
                return StatusCode(500, ex.Message);
            }
        }

        public async Task<IActionResult> UPDATETAXMASTER(Taxtype_mst Data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");
            try
            {
                // Set update info
                Data.UpdatedDate = DateTime.Now;
                Data.UpdatedByUId = HttpContext.Session.GetString("UserID");
                Data.UpdatedByUName = HttpContext.Session.GetString("UserName");

                // Generate update query
                Genrate_Query genrate = new Genrate_Query();
                string query = genrate.GenerateUpdateQuery(Data, "[TaxTyp]", "TaxTypeId", Data.TaxTypeId, "");

                // Execute query asynchronously
                await using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    await using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { Success = true, Message = "Tax Type Updated Successfully..!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public async Task<IActionResult> DELETETAXMASTER(string Id)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");
            try
            {
                string query = "DELETE FROM [TaxTyp] WHERE TaxTypeId = @TaxTypeId";

                await using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    await using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@TaxTypeId", Id);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { success = true, message = "Tax Type Deleted Successfully..!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        #endregion
        #region Tax Combination
        public async Task<IActionResult> ADDTAXCOMBINATION(TaxCombinationSetup Data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");
            try
            {
                Data.CreatedDate = DateTime.Now;
                Data.UpdatedDate = DateTime.Now;
                Data.CretedByUName = HttpContext.Session.GetString("UserName");
                Data.UpdatedByUName = HttpContext.Session.GetString("UserName");
                Data.CretedByUId = HttpContext.Session.GetString("UserID");
                Data.UpdatedByUId = HttpContext.Session.GetString("UserID");

                Genrate_Query genrate = new Genrate_Query();
                string insertQuery = genrate.GenerateInsertQuery(Data, "[TaxCode_Mst]", "TaxCodeId");
                string lastId = "";

                await using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();
                    await using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();

                        cmd.CommandText = "SELECT MAX(TaxCodeId) AS TaxCodeId FROM TaxCode_Mst;";
                        await using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                        {
                            if (await rdr.ReadAsync())
                            {
                                lastId = rdr["TaxCodeId"].ToString();
                            }
                        }
                    }
                }

                return Json(new { Success = true, LastId = lastId, Message = "Tax Combination Details Added Successfully..!" });
            }
            catch (SqlException ex)
            {
                int errorCode = ex.ErrorCode;
                if (errorCode == -2146232060)
                {
                    return StatusCode(500, "This Tax Code Is Already In System. Please Define New..!");
                }
                else if (ex.Number == 2627 || ex.Number == 2601)
                {
                    return Conflict("A record with the same value already exists.");
                }
                else
                {
                    return StatusCode(500, ex.Message);
                }
            }
        }
        public async Task<IActionResult> ADDFORMDT([FromBody] List<TaxCombibnationForm> Data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");
            try
            {
                foreach (var item in Data)
                {
                    if (!string.IsNullOrEmpty(item.TaxCodeFormId) && item.TaxCodeFormId != "0")
                    {
                        Genrate_Query genrate = new Genrate_Query();
                        string updateQuery = genrate.GenerateUpdateQuery(item, "[TaxCodeForm_Det]", "TaxCodeFormId", item.TaxCodeFormId, "");
                        if (!string.IsNullOrEmpty(item.TaxTypeId))
                        {
                            await using (SqlConnection con = new SqlConnection(connectionString))
                            {
                                await con.OpenAsync();
                                await using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                                {
                                    cmd.CommandTimeout = 300;
                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }
                        }
                    }
                    else
                    {
                        item.TaxCodeFormId = null;
                        Genrate_Query genrate = new Genrate_Query();
                        if (!string.IsNullOrEmpty(item.TaxTypeId))
                        {
                            string insertQuery = genrate.GenerateInsertQuery(item, "[TaxCodeForm_Det]", "TaxCodeFormId");
                            await using (SqlConnection con = new SqlConnection(connectionString))
                            {
                                await con.OpenAsync();
                                await using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                                {
                                    cmd.CommandTimeout = 300;
                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }
                        }
                    }
                }

                return Json(new { Success = true, Message = "Row Data Saved Successfully..!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> UPDATETAXCOMBINATION(TaxCombinationSetup Data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");
            try
            {
                Data.UpdatedDate = DateTime.Now;
                Data.UpdatedByUName = HttpContext.Session.GetString("UserName");
                Data.UpdatedByUId = HttpContext.Session.GetString("UserId");

                Genrate_Query genrate = new Genrate_Query();
                string updateQuery = genrate.GenerateUpdateQuery(Data, "[TaxCode_Mst]", "TaxCodeId", Data.TaxCodeId, "");

                await using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();
                    await using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { Success = true, LastId = Data.TaxCodeId, Message = "Tax Details Updated Successfully..!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> DELETETAXCOMBINATION(string Id)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");
            try
            {
                string query = "DELETE FROM [TaxCodeForm_Det] WHERE TaxCodeId = @TaxCodeId; " +
                               "DELETE FROM [TaxCode_Mst] WHERE TaxCodeId = @TaxCodeId;";

                await using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();
                    await using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@TaxCodeId", Id);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { success = true, message = "Tax Details Deleted Successfully..!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region Tax HSN Master

        public async Task<IActionResult> CREATEHSN([FromBody] HSN Data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                // Set metadata
                Data.CreatedDate = DateTime.Now;
                Data.UpdatedDate = DateTime.Now;
                Data.CretedByUName = HttpContext.Session.GetString("UserName");
                Data.UpdatedByUName = HttpContext.Session.GetString("UserName");
                Data.CretedByUId = HttpContext.Session.GetString("UserID");
                Data.UpdatedByUId = HttpContext.Session.GetString("UserID");

                Genrate_Query generator = new Genrate_Query();
                string insertQuery = generator.GenerateInsertQuery(Data, "[HSNMaster]", "HSNID");

                string lastInsertedId = "";

                await using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    // Append SCOPE_IDENTITY() to get last inserted ID
                    insertQuery += "; SELECT SCOPE_IDENTITY();";

                    await using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.CommandTimeout = 300;

                        object result = await cmd.ExecuteScalarAsync();
                        lastInsertedId = result?.ToString();
                    }
                }

                return Json(new
                {
                    Success = true,
                    LastId = lastInsertedId,
                    Message = "Tax HSN Added Successfully..!"
                });
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    return Conflict("A record with the same value already exists.");
                }
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> UPDATEHSN([FromBody] HSN Data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                Data.UpdatedDate = DateTime.Now;
                Data.UpdatedByUName = HttpContext.Session.GetString("UserName");
                Data.UpdatedByUId = HttpContext.Session.GetString("UserId");

                Genrate_Query genrate = new Genrate_Query();
                string updateQuery = genrate.GenerateUpdateQuery(Data, "[HSNMaster]", "HSNID", Data.HSNID, "");

                await using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();
                    await using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { Success = true, LastId = Data.HSNID, Message = "HSN Details Updated Successfully..!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> DELETEHSN([FromBody] HSN Data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                string deleteQuery = "DELETE FROM [HSNMaster] WHERE HSNID = @HSNID";

                await using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();
                    await using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@HSNID", Data.HSNID);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { success = true, message = "HSN Details Deleted Successfully..!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #endregion
        #region UOM Master
        public async Task<IActionResult> CREATEUOM([FromBody] UOM Data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                // Set metadata
                Data.CreatedDate = DateTime.Now;
                Data.UpdatedDate = DateTime.Now;
                Data.CretedByUName = HttpContext.Session.GetString("UserName");
                Data.UpdatedByUName = HttpContext.Session.GetString("UserName");
                Data.CretedByUId = HttpContext.Session.GetString("UserID");
                Data.UpdatedByUId = HttpContext.Session.GetString("UserID");

                Genrate_Query generator = new Genrate_Query();
                string insertQuery = generator.GenerateInsertQuery(Data, "[UomTyp]", "UomID");

                string lastInsertedId = "";

                await using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    // Append SCOPE_IDENTITY() to get last inserted ID
                    insertQuery += "; SELECT SCOPE_IDENTITY();";

                    await using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.CommandTimeout = 300;
                        object result = await cmd.ExecuteScalarAsync();
                        lastInsertedId = result?.ToString();
                    }
                }

                return Json(new
                {
                    Success = true,
                    LastId = lastInsertedId,
                    Message = "UOM Details Added Successfully..!"
                });
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    return Conflict("A record with the same value already exists.");
                }
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> UPDATEUOM([FromBody] UOM Data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                Data.UpdatedDate = DateTime.Now;
                Data.UpdatedByUName = HttpContext.Session.GetString("UserName");
                Data.UpdatedByUId = HttpContext.Session.GetString("UserId");

                Genrate_Query genrate = new Genrate_Query();
                string updateQuery = genrate.GenerateUpdateQuery(Data, "[UomTyp]", "UomID", Data.UomID, "");

                await using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();
                    await using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { Success = true, LastId = Data.UomID, Message = "UOM Updated Successfully..!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> DELETEUOM([FromBody] UOM Data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                string deleteQuery = "DELETE FROM [UomTyp] WHERE UomID = @UomID";

                await using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();
                    await using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@UomID", Data.UomID);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { Success = true, message = "UOM Deleted Successfully..!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #endregion

        #region Item Group

        public async Task<IActionResult> CREATEITEMGRP([FromBody] ITEMGRP Data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                // Set metadata
                Data.CreatedDate = DateTime.Now;
                Data.UpdatedDate = DateTime.Now;
                Data.CretedByUName = HttpContext.Session.GetString("UserName");
                Data.UpdatedByUName = HttpContext.Session.GetString("UserName");
                Data.CretedByUId = HttpContext.Session.GetString("UserID");
                Data.UpdatedByUId = HttpContext.Session.GetString("UserID");

                Genrate_Query generator = new Genrate_Query();
                string insertQuery = generator.GenerateInsertQuery(Data, "[ItGrpMaster]", "ItmGrpID");

                string lastInsertedId = "";

                await using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    // Append SCOPE_IDENTITY() to get last inserted ID
                    insertQuery += "; SELECT SCOPE_IDENTITY();";

                    await using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.CommandTimeout = 300;
                        object result = await cmd.ExecuteScalarAsync();
                        lastInsertedId = result?.ToString();
                    }
                }

                return Json(new
                {
                    Success = true,
                    LastId = lastInsertedId,
                    Message = "Item Group Added Successfully..!"
                });
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    return Conflict("A record with the same value already exists.");
                }
                return StatusCode(500, ex.Message);
            }
        }

        public async Task<IActionResult> UPDATEITEMGRP([FromBody] ITEMGRP Data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                Data.UpdatedDate = DateTime.Now;
                Data.UpdatedByUName = HttpContext.Session.GetString("UserName");
                Data.UpdatedByUId = HttpContext.Session.GetString("UserId");

                Genrate_Query genrate = new Genrate_Query();
                string updateQuery = genrate.GenerateUpdateQuery(Data, "[ItGrpMaster]", "ItmGrpID", Data.ItmGrpID, "");

                await using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();
                    await using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { Success = true, LastId = Data.ItmGrpID, Message = "Item Group Updated Successfully..!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public async Task<IActionResult> DELETEITEMGRP([FromBody] ITEMGRP Data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                string deleteQuery = "DELETE FROM [ItGrpMaster] WHERE ItmGrpID = @ItmGrpID";

                await using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();
                    await using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@ItmGrpID", Data.ItmGrpID);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { Success = true, message = "Item Group Deleted Successfully..!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #endregion

        #region Item Master
        public IActionResult CREATEITEM([FromBody] ITEMMASTER Data)
        {
            if (Data == null)
            {
                return BadRequest("Item data is required");
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlConnection connection = null;
            SqlTransaction transaction = null;

            try
            {
                // Validate session data first
                var (userName, userId) = GetSessionUserData();
                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("User session is invalid");
                }

                // Validate item duplication
                var validationResponse = VALIDATEITEMDUPLICATION(Data.itemData.ItemName, Data.itemData.EanCode,Data.itemData.ItmGrpID);
                if (!validationResponse.Success)
                {
                    return BadRequest(validationResponse.Message);
                }

                // Start transaction
                connection = new SqlConnection(connectionString);
                connection.Open();
                transaction = connection.BeginTransaction();

                // Create item
                var itemResult = CREATEITEM(Data.itemData, userName, userId, connection, transaction);
                if (!itemResult.Success)
                {
                    transaction.Rollback();
                    return StatusCode(500, itemResult.Message);
                }

                // Create warehouse inventory setups
                if (Data.WhsDetails != null && Data.WhsDetails.Any())
                {
                    var whsSetupResult = CREATEWHSINV(Data.WhsDetails, itemResult.ItemId, userName, userId, connection, transaction);
                    if (!whsSetupResult.Success)
                    {
                        transaction.Rollback();
                        return StatusCode(500, whsSetupResult.Message);
                    }
                }

                // Create price setups
                if (Data.PriceSetup != null && Data.PriceSetup.Any())
                {
                    var priceSetupResult = CREATEPRICESETUP(Data.PriceSetup, itemResult.ItemId, userName, userId, connection, transaction);
                    if (!priceSetupResult.Success)
                    {
                        transaction.Rollback();
                        return StatusCode(500, priceSetupResult.Message);
                    }
                }

                // Commit transaction if all operations succeeded
                transaction.Commit();

                return Ok(new
                {
                    Success = true,
                    Message = "Item Details Added Successfully",
                    ItemId = itemResult.ItemId
                });
            }
            catch (SqlException ex)
            {
                transaction?.Rollback();
                return HandleSqlException(ex);
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
            finally
            {
                transaction?.Dispose();
                connection?.Dispose();
            }
        }
        public IActionResult UPDATEITEM([FromBody] ITEMMASTER Data)
        {
            if (Data == null)
            {
                return BadRequest("Item data is required");
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlConnection connection = null;
            SqlTransaction transaction = null;

            try
            {
                // Validate session data first
                var (userName, userId) = GetSessionUserData();
                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("User session is invalid");
                }

                // Validate item ID exists for update operation
                if (string.IsNullOrEmpty(Data.itemData?.ItemID))
                {
                    return BadRequest("Item ID is required for update");
                }

                // Start transaction
                connection = new SqlConnection(connectionString);
                connection.Open();
                transaction = connection.BeginTransaction();

                // Update item
                var itemResult = UPDATEITEM(Data.itemData, userName, userId, connection, transaction);
                if (!itemResult.Success)
                {
                    transaction.Rollback();
                    return StatusCode(500, itemResult.Message);
                }

                // Update warehouse inventory setups
                if (Data.WhsDetails != null && Data.WhsDetails.Any())
                {
                    var whsSetupResult = CREATEWHSINV(Data.WhsDetails, Data.itemData.ItemID, userName, userId, connection, transaction);
                    if (!whsSetupResult.Success)
                    {
                        transaction.Rollback();
                        return StatusCode(500, whsSetupResult.Message);
                    }
                }

                // Update price setups
                if (Data.PriceSetup != null && Data.PriceSetup.Any())
                {
                    var priceSetupResult = UPDATEPRICESETUP(Data.PriceSetup, Data.itemData.ItemID, userName, userId, connection, transaction);
                    if (!priceSetupResult.Success)
                    {
                        transaction.Rollback();
                        return StatusCode(500, priceSetupResult.Message);
                    }
                }

                // Commit transaction if all operations succeeded
                transaction.Commit();

                return Ok(new
                {
                    Success = true,
                    Message = "Item Details Updated Successfully",
                    ItemId = Data.itemData.ItemID
                });
            }
            catch (SqlException ex)
            {
                transaction?.Rollback();
                return HandleSqlException(ex);
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
            finally
            {
                transaction?.Dispose();
                connection?.Dispose();
            }
        }
        private (bool Success, string Message, string ItemId) CREATEITEM(ITEM Data, string userName, string userId, SqlConnection connection, SqlTransaction transaction)
        {
            try
            {
                // Set metadata
                Data.CreatedDate = DateTime.Now;
                Data.UpdatedDate = DateTime.Now;
                Data.CretedByUName = userName;
                Data.UpdatedByUName = userName;
                Data.CretedByUId = userId;
                Data.UpdatedByUId = userId;

                // Generate parameterized query
                var generator = new Genrate_Query();
                string insertQuery = generator.GenerateInsertQuery(Data, "[ItemMaster]", "ItemID");

                if (string.IsNullOrEmpty(insertQuery))
                {
                    return (false, "Failed to generate insert query", null);
                }

                insertQuery += "; SELECT SCOPE_IDENTITY();";

                using (var cmd = new SqlCommand(insertQuery, connection, transaction))
                {
                    cmd.CommandTimeout = 300;
                    var result = cmd.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                    {
                        return (false, "Failed to create item - no ID returned", null);
                    }

                    string newItemId = result.ToString();
                    return (true, "Item created successfully", newItemId);
                }
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                return (false, $"A record with the same value already exists.", null);
            }
            catch (Exception ex)
            {
                return (false, $"Failed to create item: {ex.Message}", null);
            }
        }
        private (bool Success, string Message) CREATEPRICESETUP(PRISESETUP[] Data, string ItemId, string userName, string userId, SqlConnection connection, SqlTransaction transaction)
        {
            if (Data == null || !Data.Any())
            {
                return (true, "No price setups to create");
            }

            try
            {
                foreach (var item in Data)
                {
                    item.UpdatedDate = DateTime.Now;
                    item.UpdatedByUName = userName;
                    item.UpdatedByUId = userId;
                    item.ItemID = ItemId;

                    Genrate_Query generator = new Genrate_Query();
                    string query;

                    if (string.IsNullOrEmpty(item.PriceSetID))
                    {
                        // New record
                        item.CreatedDate = DateTime.Now;
                        item.CretedByUName = userName;
                        item.CretedByUId = userId;
                        item.PriceSetID = null;

                        query = generator.GenerateInsertQuery(item, "[ItemPriceSetup]", "PriceSetID");
                    }
                    else
                    {
                        // Update record
                        query = generator.GenerateUpdateQuery(item, "[ItemPriceSetup]", "PriceSetID", item.PriceSetID, "");
                    }

                    using (SqlCommand cmd = new SqlCommand(query, connection, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.ExecuteNonQuery();
                    }
                }

                return (true, "Price setups created successfully");
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                return (false, $"A record with the same value already exists.");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to create price setups: {ex.Message}");
            }
        }
        private (bool Success, string Message) CREATEWHSINV(WHSDETAILS[] Data, string ItemId, string userName, string userId, SqlConnection connection, SqlTransaction transaction)
        {
            if (Data == null || !Data.Any())
            {
                return (true, "No warehouse details to create"); // Changed to success since it's optional
            }

            try
            {
                foreach (var item in Data)
                {
                    string Query = "";
                    var generator = new Genrate_Query();
                    var now = DateTime.Now;

                    if (!string.IsNullOrEmpty(item.InvtDetId))
                    {
                        item.UpdatedDate = now;
                        item.UpdatedByUName = userName;
                        item.UpdatedByUId = userId;
                        item.ItemId = ItemId;

                        Query = generator.GenerateUpdateQuery(item, "[Item_Invnt_det]", "InvtDetId", item.InvtDetId, "");

                    }
                    else
                    {
                        item.CretedDate = now;
                        item.CretedByUName = userName;
                        item.CretedByUId = userId;
                        item.UpdatedDate = now;
                        item.UpdatedByUName = userName;
                        item.UpdatedByUId = userId;
                        item.ItemId = ItemId;
                        item.InvtDetId = null;

                        Query = generator.GenerateInsertQuery(item, "[Item_Invnt_det]", "InvtDetId");
                    }

                    if (string.IsNullOrEmpty(Query))
                    {
                        return (false, "Failed to generate warehouse setup query");
                    }

                    Query += "; SELECT SCOPE_IDENTITY();";

                    using (var cmd = new SqlCommand(Query, connection, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        var result = cmd.ExecuteScalar();

                       //if (result == null || result == DBNull.Value || string.IsNullOrEmpty(item.InvtDetId))
                       //{
                       //    return (false, "Failed to create warehouse setup - no ID returned");
                       //}
                    }
                }

                return (true, "Warehouse setups created successfully");
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                return (false, "A record with the same value already exists.");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to create warehouse setups: {ex.Message}");
            }
        }
        private (bool Success, string Message) VALIDATEITEMDUPLICATION(string itemName, string eanCode, string grpId)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                string query = @"
                    SELECT TOP 1 ItemName, EanCode, ItmGrpID 
                    FROM ItemMaster
                    WHERE (ItemName = @ItemName AND ItmGrpID = @ItmGrpID)
                       OR (EanCode = @EanCode)
                ";

                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ItemName", itemName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@EanCode", eanCode ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ItmGrpID", grpId ?? (object)DBNull.Value);

                    con.Open();

                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            string existingItemName = rdr["ItemName"]?.ToString();
                            string existingEanCode = rdr["EanCode"]?.ToString();
                            string existingGrpId = rdr["ItmGrpID"]?.ToString();

                            // Check for ItemName + Group duplication
                            if (existingItemName?.Equals(itemName, StringComparison.OrdinalIgnoreCase) == true &&
                                existingGrpId?.Equals(grpId, StringComparison.OrdinalIgnoreCase) == true)
                            {
                                return (false, "Item Name duplication is not allowed within the same group.");
                            }

                            // Check for EAN duplication
                            if (!string.IsNullOrEmpty(eanCode) &&
                                existingEanCode?.Equals(eanCode, StringComparison.OrdinalIgnoreCase) == true)
                            {
                                return (false, "EAN Code duplication is not allowed.");
                            }
                        }
                    }
                }

                return (true, "No duplicate record found.");
            }
            catch (Exception ex)
            {
                return (false, $"Validation error: {ex.Message}");
            }
        }
        private (string UserName, string UserId) GetSessionUserData()
        {
            try
            {
                // Get user name from session
                string userName = HttpContext.Session.GetString("UserName");

                // Get user ID from session
                string userId = HttpContext.Session.GetString("UserID");

                // Validate the retrieved values
                if (string.IsNullOrWhiteSpace(userName))
                {
                    throw new InvalidOperationException("User name not found in session");
                }

                if (string.IsNullOrWhiteSpace(userId))
                {
                    throw new InvalidOperationException("User ID not found in session");
                }

                return (userName.Trim(), userId.Trim());
            }
            catch (Exception ex)
            {
                // Log the error (you should have a logging mechanism)
                // _logger.LogError(ex, "Failed to retrieve user session data");

                // Return empty values to indicate failure
                return (null, null);
            }
        }
        private IActionResult HandleSqlException(SqlException ex)
        {
            if (ex.Number == 2627 || ex.Number == 2601)
            {
                return Conflict("A record with the same value already exists.");
            }
            return StatusCode(500, $"Database error: {ex.Message}");
        }
        private (bool Success, string Message, string ItemId) UPDATEITEM(ITEM Data, string userName, string userId, SqlConnection connection, SqlTransaction transaction)
        {
            try
            {
                // Set metadata - Only update relevant fields for an update operation
                Data.UpdatedDate = DateTime.Now;
                Data.UpdatedByUName = userName;
                Data.UpdatedByUId = userId;

                // Prevent updating these fields
                Data.ItemCode = null;
                Data.CreatedDate = default;
                Data.CretedByUName = null;
                Data.CretedByUId = null;

                // Generate update query
                var generator = new Genrate_Query();
                string updateQuery = generator.GenerateUpdateQuery(Data, "[ItemMaster]", "ItemID", Data.ItemID, "");

                if (string.IsNullOrEmpty(updateQuery))
                {
                    return (false, "Failed to generate update query", null);
                }

                using (var cmd = new SqlCommand(updateQuery, connection, transaction))
                {
                    cmd.CommandTimeout = 300;
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return (false, "No records were updated - item may not exist", null);
                    }

                    return (true, "Item updated successfully", Data.ItemID);
                }
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                return (false, "A record with the same unique value already exists", null);
            }
            catch (Exception ex)
            {
                return (false, $"Failed to update item: {ex.Message}", null);
            }
        }
        private (bool Success, string Message) UPDATEPRICESETUP(PRISESETUP[] Data, string ItemId, string userName, string userId, SqlConnection connection, SqlTransaction transaction)
        {
            if (Data == null || !Data.Any())
            {
                return (true, "No price setups provided - skipping update");
            }

            try
            {
                // First delete all existing price setups for this item
                string deleteQuery = $"DELETE FROM [ItemPriceSetup] WHERE ItemID = '{ItemId}'";
                using (var deleteCmd = new SqlCommand(deleteQuery, connection, transaction))
                {
                    deleteCmd.CommandTimeout = 300;
                    deleteCmd.ExecuteNonQuery();
                }

                foreach (var item in Data)
                {
                    // Set metadata
                    item.UpdatedDate = DateTime.Now;
                    item.UpdatedByUName = userName;
                    item.UpdatedByUId = userId;
                    item.ItemID = ItemId;
                    item.CreatedDate = DateTime.Now;
                    item.CretedByUName = userName;
                    item.CretedByUId = userId;
                    item.PriceSetID = null;

                    var generator = new Genrate_Query();
                    string insertQuery = generator.GenerateInsertQuery(item, "[ItemPriceSetup]", "PriceSetID");

                    if (string.IsNullOrEmpty(insertQuery))
                    {
                        return (false, "Failed to generate price setup query");
                    }

                    using (var cmd = new SqlCommand(insertQuery, connection, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            return (false, $"Failed to insert price setup record");
                        }
                    }
                }
                return (true, "Price setups updated successfully");
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                return (false, "A record with the same unique value already exists");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to update price setups: {ex.Message}");
            }
        }
        public IActionResult DELETEITEM( string ItemID)
        {
            string ConnectionString = _configuration.GetConnectionString("ErpConnection");
            try
            {

                string Query = @$"Delete from [Item_Invnt_det] where ItemID='{ItemID}'
                                Delete from[ItemPriceSetup] where ItemID = '{ItemID }'
                                Delete from[ItemMaster] where ItemID = '{ ItemID }' ";
                List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        con.Open();
                        cmd.CommandText = Query;
                        cmd.CommandTimeout = 300;
                        cmd.ExecuteNonQuery();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        con.Close();
                    }
                }
                return Json(new { success = true, message = " Item  Deleted Successfully..!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        } 
        public IActionResult DELETEPPRICESETUP( string ItemID)
        {
            string ConnectionString = _configuration.GetConnectionString("ErpConnection");
            try
            {

                string Query = @$"
                                Delete from [ItemPriceSetup] where PriceSetID = '{ItemID }'
                                 ";
                List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        con.Open();
                        cmd.CommandText = Query;
                        cmd.CommandTimeout = 300;
                        cmd.ExecuteNonQuery();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        con.Close();
                    }
                }
                return Json(new { success = true, message = " Item  Deleted Successfully..!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }
        #endregion
        #region COUNTRY
        public async Task<IActionResult> CREATECOUNTRY([FromBody] COUNTRY data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                // Common audit fields
                data.UpdatedDate = DateTime.Now;
                data.UpdatedByUName = HttpContext.Session.GetString("UserName");
                data.UpdatedByUId = HttpContext.Session.GetString("UserID");

                Genrate_Query generator = new Genrate_Query();
                string query;

                if (string.IsNullOrEmpty(data.Coun_Id))
                {
                    // New record
                    data.CreatedDate = DateTime.Now;
                    data.CretedByUId = data.UpdatedByUId;
                    data.CretedByUName = data.UpdatedByUName;
                    data.Coun_Id = null;

                    query = generator.GenerateInsertQuery(data, "[CounMaster]", "Coun_Id");
                }
                else
                {
                    // Update record
                    query = generator.GenerateUpdateQuery(data, "[CounMaster]", "Coun_Id", data.Coun_Id, "");
                }

                await using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();
                    await using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { Success = true, Message = "Country saved successfully." });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                return Conflict("A record with the same value already exists.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        public async Task<IActionResult> DELETECOUNTRY(string id)
        {
            string ConnectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                string Query = "DELETE FROM [CounMaster] WHERE Coun_Id = @Coun_Id";

                await using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    await using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.Parameters.AddWithValue("@Coun_Id", id);
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { Success = true, Message = "Country deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region STATE

        public async Task<IActionResult> CREATESTATE([FromBody] STATE data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                // Common audit fields
                data.UpdatedDate = DateTime.Now;
                data.UpdatedByUName = HttpContext.Session.GetString("UserName");
                data.UpdatedByUId = HttpContext.Session.GetString("UserID");

                Genrate_Query generator = new Genrate_Query();
                string query;

                if (string.IsNullOrEmpty(data.Stat_Id))
                {
                    // New record
                    data.CreatedDate = DateTime.Now;
                    data.CretedByUId = data.UpdatedByUId;
                    data.CretedByUName = data.UpdatedByUName;
                    data.Stat_Id = null;

                    query = generator.GenerateInsertQuery(data, "[StateMaster]", "Stat_Id");
                }
                else
                {
                    // Update record
                    query = generator.GenerateUpdateQuery(data, "[StateMaster]", "Stat_Id", data.Stat_Id, "");
                }

                await using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();
                    await using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { Success = true, Message = "State saved successfully." });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                return Conflict("A record with the same value already exists.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        public async Task<IActionResult> DELETESTATE(string id)
        {
            string ConnectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                string Query = "DELETE FROM [StateMaster] WHERE Stat_Id = @Stat_Id";

                await using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    await using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.Parameters.AddWithValue("@Stat_Id", id);
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { Success = true, Message = "State deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #endregion

        #region Employee Master

        public async Task<IActionResult> CREATEEMPLOYEE([FromBody] EmployeeMaster data)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");

            await using var con = new SqlConnection(connStr);
            await con.OpenAsync();

            SqlTransaction tran = (SqlTransaction)await con.BeginTransactionAsync();

            try
            {
                string userId = null;

                if (data.LinkUser == "Y")
                {
                    var last4 = data.MobilePhone?.Length >= 4
                        ? data.MobilePhone[^4..]
                        : data.MobilePhone;

                    var user = new User
                    {
                        UserID = null,
                        EmpId = null,
                        FullName = $"{data.FirstName} {data.LastName}".Trim(),
                        Username = data.FirstName.Trim() + data.LastName.Trim(),
                        Password = data.FirstName.Trim() + last4,
                        Mobile = data.MobilePhone,
                        Role = "U",
                        IsActive = data.IsActive
                    };

                    // MUST pass same connection + transaction
                    userId = await CreateOrUpdateUserAsync(user, con, tran);
                }

                data.LinkUserID = userId;
                data.UpdatedDate = DateTime.Now;
                data.UpdatedByUId = HttpContext.Session.GetString("UserID");
                data.UpdatedByUName = HttpContext.Session.GetString("UserName");

                var generator = new Genrate_Query();
                string query;

                if (string.IsNullOrEmpty(data.EmployeeID))
                {
                    data.CreatedDate = DateTime.Now;
                    data.CretedByUId = data.UpdatedByUId;
                    data.CretedByUName = data.UpdatedByUName;
                    data.EmployeeID = null;

                    query = generator.GenerateInsertQuery(
                        data, "[Master_EmployeeMaster]", "EmployeeID");
                }
                else
                {
                    query = generator.GenerateUpdateQuery(
                        data, "[Master_EmployeeMaster]", "EmployeeID", data.EmployeeID, "");
                }

                using (var cmd = new SqlCommand(query, con, tran))
                {
                    await cmd.ExecuteNonQueryAsync();

                    if (string.IsNullOrEmpty(data.EmployeeID))
                    {
                        cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                        data.EmployeeID = (await cmd.ExecuteScalarAsync())?.ToString();
                    }
                }

                if (!string.IsNullOrEmpty(userId))
                {
                    var updateUser = @"UPDATE [Users] 
                               SET EmpId = @EmpId 
                               WHERE UserID = @UserID";

                    using var cmd = new SqlCommand(updateUser, con, tran);
                    cmd.Parameters.AddWithValue("@EmpId", data.EmployeeID);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    await cmd.ExecuteNonQueryAsync();
                }

                await tran.CommitAsync();   // ✅ NOW THIS WORKS
                return Json(new { Success = true, EmployeeID = data.EmployeeID });
            }
            catch (Exception ex)
            {
                if (tran != null)
                    await tran.RollbackAsync();   // ✅ Safe rollback

                return StatusCode(500, ex.Message);
            }
        }

        private async Task<string> CreateOrUpdateUserAsync(
    User item,
    SqlConnection con,
    SqlTransaction tran)
        {
            if (string.IsNullOrEmpty(item.UserID))
            {
                var insertQuery = @"
        INSERT INTO [Users]
        ([Username],[Password],[FullName],[Email],[Mobile],[Role],[IsActive],
         [CreatedBy],[UpdatedBy],[LicenseValidFrom],[LicenseValidTo],
         [LicenseStatus],[LicenseGenDate],[Licencekey],[EmpId])
        VALUES
        (@Username,@Password,@FullName,@Email,@Mobile,@Role,@IsActive,
         @CreatedBy,@UpdatedBy,@LicenseValidFrom,@LicenseValidTo,
         @LicenseStatus,@LicenseGenDate,@Licencekey,@EmpId);
        SELECT SCOPE_IDENTITY();";

                using var cmd = new SqlCommand(insertQuery, con, tran);
                cmd.Parameters.AddWithValue("@Username", item.Username ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Password", item.Password ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FullName", item.FullName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", item.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Mobile", item.Mobile ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Role", item.Role ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@IsActive", item.IsActive);
                cmd.Parameters.AddWithValue("@CreatedBy", HttpContext.Session.GetString("UserID"));
                cmd.Parameters.AddWithValue("@UpdatedBy", HttpContext.Session.GetString("UserID"));
                cmd.Parameters.AddWithValue("@LicenseValidFrom", (object)item.LicenseValidFrom ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LicenseValidTo", (object)item.LicenseValidTo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LicenseStatus", (object)item.LicenseStatus ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LicenseGenDate", (object)item.LicenseGenDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Licencekey", (object)item.Licencekey ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@EmpId", (object)item.EmpId ?? DBNull.Value);

                return (await cmd.ExecuteScalarAsync())?.ToString();
            }

            return item.UserID;
        }
               
        public async Task<IActionResult> DELETEEMPLOYEE(string id)
        {
            string ConnectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                string Query = "DELETE FROM [Master_EmployeeMaster] WHERE EmployeeID = @EmployeeID";

                await using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    await using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeID", id);
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { Success = true, Message = "Employee deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #endregion
        #region Employee Role Master
        public async Task<IActionResult> CREATEROLE([FromBody] Role Data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                // Set metadata
                Data.CreatedDate = DateTime.Now;
                Data.UpdatedDate = DateTime.Now;
                Data.CreatedByUName = HttpContext.Session.GetString("UserName");
                Data.UpdatedByUName = HttpContext.Session.GetString("UserName");
                Data.CreatedByUId = HttpContext.Session.GetString("UserID");
                Data.UpdatedByUId = HttpContext.Session.GetString("UserID");

                Genrate_Query generator = new Genrate_Query();
                string insertQuery = generator.GenerateInsertQuery(Data, "[Master_EmployeeRoleMaster]", "RoleID");

                string lastInsertedId = "";

                await using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    // Append SCOPE_IDENTITY() to get last inserted ID
                    insertQuery += "; SELECT SCOPE_IDENTITY();";

                    await using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.CommandTimeout = 300;
                        object result = await cmd.ExecuteScalarAsync();
                        lastInsertedId = result?.ToString();
                    }
                }

                return Json(new
                {
                    Success = true,
                    LastId = lastInsertedId,
                    Message = "Role Details Added Successfully..!"
                });
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    return Conflict("A record with the same value already exists.");
                }
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> UPDATEROLE([FromBody] Role Data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                Data.UpdatedDate = DateTime.Now;
                Data.UpdatedByUName = HttpContext.Session.GetString("UserName");
                Data.UpdatedByUId = HttpContext.Session.GetString("UserId");

                Genrate_Query genrate = new Genrate_Query();
                string updateQuery = genrate.GenerateUpdateQuery(Data, "[Master_EmployeeRoleMaster]", "RoleID", Data.RoleID, "");

                await using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();
                    await using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { Success = true, LastId = Data.RoleID, Message = "Role Updated Successfully..!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> DELETEROLE([FromBody] Role Data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                string deleteQuery = "DELETE FROM [Master_EmployeeRoleMaster] WHERE RoleID = @RoleID";

                await using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();
                    await using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@RoleID", Data.RoleID);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { Success = true, message = "Role Deleted Successfully..!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public async Task<IActionResult> GETROLELIST()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = @"SELECT * FROM [Master_EmployeeRoleMaster]";
                var dataList = new List<Dictionary<string, object>>();

                await using (var con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object? value = await rdr.IsDBNullAsync(i) ? "" : rdr.GetValue(i);

                                    // Convert DATE values to string format without time
                                    if (value is DateTime dateValue && dateValue.TimeOfDay == TimeSpan.Zero)
                                    {
                                        row[columnName] = dateValue.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        row[columnName] = value!;
                                    }
                                }

                                dataList.Add(row);
                            }
                        }
                    }
                }

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETROLE()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = @"SELECT * FROM [Master_EmployeeRoleMaster] WHERE IsActive = 'A'";
                var dataList = new List<Dictionary<string, object>>();

                await using (var con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object? value = await rdr.IsDBNullAsync(i) ? "" : rdr.GetValue(i);

                                    // Convert DATE values to string format without time
                                    if (value is DateTime dateValue && dateValue.TimeOfDay == TimeSpan.Zero)
                                    {
                                        row[columnName] = dateValue.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        row[columnName] = value!;
                                    }
                                }

                                dataList.Add(row);
                            }
                        }
                    }
                }

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #endregion


        #region BANKS

        public async Task<IActionResult> CREATEBANK([FromBody] BAKNS data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                // Common audit fields
                data.UpdatedDate = DateTime.Now;
                data.UpdatedByUName = HttpContext.Session.GetString("UserName");
                data.UpdatedByUId = HttpContext.Session.GetString("UserID");

                Genrate_Query generator = new Genrate_Query();
                string query;

                if (string.IsNullOrEmpty(data.BankID))
                {
                    // New record
                    data.CretedDate = DateTime.Now;
                    data.CretedByUId = data.UpdatedByUId;
                    data.CretedByUName = data.UpdatedByUName;
                    data.BankID = null;

                    query = generator.GenerateInsertQuery(data, "[BankMaster]", "BankID");
                }
                else
                {
                    // Update record
                    query = generator.GenerateUpdateQuery(data, "[BankMaster]", "BankID", data.BankID, "");
                }

                await using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();
                    await using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { Success = true, Message = "Bank saved successfully." });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                return Conflict("A record with the same value already exists.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        public async Task<IActionResult> DELETEBANK(string id)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                string query = "DELETE FROM [BankMaster] WHERE BankID = @BankID";

                await using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();
                    await using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@BankID", id);
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { Success = true, Message = "Bank deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #endregion

        #region COMPANY

        public async Task<IActionResult> CREATECOMPANY([FromBody] COMPANY data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                // Common audit fields
                data.UpdatedDate = DateTime.Now;
                data.UpdatedByUName = HttpContext.Session.GetString("UserName");
                data.UpdatedByUId = HttpContext.Session.GetString("UserID");

                Genrate_Query generator = new Genrate_Query();
                string query;

                if (string.IsNullOrEmpty(data.CompanyID))
                {
                    // New record
                    data.CreatedDate = DateTime.Now;
                    data.CretedByUId = data.UpdatedByUId;
                    data.CretedByUName = data.UpdatedByUName;
                    data.CompanyID = null;

                    query = generator.GenerateInsertQuery(data, "[CompanyMaster]", "CompanyID");
                }
                else
                {
                    // Update record
                    query = generator.GenerateUpdateQuery(data, "[CompanyMaster]", "CompanyID", data.CompanyID, "");
                }

                await using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();
                    await using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { Success = true, Message = "Company saved successfully." });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                return Conflict("A record with the same value already exists.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        public async Task<IActionResult> DELETECOMPANY(string id)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                string query = "DELETE FROM [CompanyMaster] WHERE CompanyID = @CompanyID";

                await using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();
                    await using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@CompanyID", id);
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { Success = true, Message = "Company deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #endregion

        #region LEDGER

        public async Task<IActionResult> CREATELEDGER([FromBody] LEDGER data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");
            await using SqlConnection con = new SqlConnection(connectionString);
            SqlTransaction transaction = null;

            try
            {
                await con.OpenAsync();
                transaction = con.BeginTransaction();

                string userName = HttpContext.Session.GetString("UserName");
                string userId = HttpContext.Session.GetString("UserID");

                // Process Ledgers
                foreach (var item in data.Ledgers)
                {
                    item.UpdatedDate = DateTime.Now;
                    item.UpdatedByUName = userName;
                    item.UpdatedByUId = userId;

                    Genrate_Query generator = new Genrate_Query();
                    string query;
                    int count = 0;
                    if (string.IsNullOrEmpty(item.ID))
                    {

                        // New record
                        item.CreatedDate = DateTime.Now;
                        item.CretedByUId = userId;
                        item.CretedByUName = userName;
                        item.ID = null;
                        if (item.LedgerType !="" && item.LedgerType != null)
                        {
                            string query2 = @$"Select DefLedgerId FROM DefaultConfiguration Where ObjType ='BP' AND GroupType='{item.LedgerType}'";
                            await using SqlCommand cmd1 = new SqlCommand(query2, con, transaction);
                            await cmd1.ExecuteNonQueryAsync();
                            item.BaseID = (await cmd1.ExecuteScalarAsync()).ToString();
                        }


                        string query1 = @$"Select Count(*) as HasRow FROM LedgerGroup Where GroupName ='{item.GroupName}' AND Postable ='{item.Postable}' ;";
                        await using SqlCommand cmd = new SqlCommand(query1, con, transaction);
                        await cmd.ExecuteNonQueryAsync();
                        count = Convert.ToInt16(await cmd.ExecuteScalarAsync());
                        if (count >0)
                        {
                           return StatusCode(500, $"Business Partner with name '{item.GroupName}' already exists.");
                        }
                        query = generator.GenerateInsertQuery(item, "[LedgerGroup]", "ID");
                     
                    }
                    else
                    {
                        if (item.LedgerType != "" && item.LedgerType != null)
                        {
                            string query2 = @$"Select DefLedgerId FROM DefaultConfiguration Where ObjType ='BP' AND GroupType='{item.LedgerType}'";
                            await using SqlCommand cmd1 = new SqlCommand(query2, con, transaction);
                            await cmd1.ExecuteNonQueryAsync();
                            item.BaseID = (await cmd1.ExecuteScalarAsync()).ToString();
                        }
                        // Update record
                        string query1 = @$"Select Count(*) as HasRow FROM LedgerGroup Where GroupName ='{item.GroupName}' AND ID <> '{item.ID}'  AND Postable ='{item.Postable}' ";
                        await using SqlCommand cmd = new SqlCommand(query1, con, transaction);
                        await cmd.ExecuteNonQueryAsync();
                        count = Convert.ToInt16(await cmd.ExecuteScalarAsync());
                        if (count > 0)
                        {
                            return StatusCode(500, $"Business Partner with name '{item.GroupName}' already exists.");
                        }
                        query = generator.GenerateUpdateQuery(item, "[LedgerGroup]", "ID", item.ID, "");
                    }

                    await using (SqlCommand cmd = new SqlCommand(query, con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();

                        if (string.IsNullOrEmpty(item.ID))
                        {
                            cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                            item.ID = (await cmd.ExecuteScalarAsync()).ToString();
                        }
                    }
                }

                // Process Ledger Addresses
                if (data.Address?.Length > 0)
                {
                    foreach (var item in data.Address)
                    {
                        item.UpdatedDate = DateTime.Now;
                        item.UpdatedByUName = userName;
                        item.UpdatedByUId = userId;
                        item.LedgerID = data.Ledgers[0].ID;

                        Genrate_Query generator = new Genrate_Query();
                        string query;

                        if (string.IsNullOrEmpty(item.AddID))
                        {
                            item.CreatedDate = DateTime.Now;
                            item.CretedByUId = userId;
                            item.CretedByUName = userName;
                            item.AddID = null;
                            query = generator.GenerateInsertQuery(item, "[LedgerAddress]", "AddID");
                        }
                        else
                        {
                            query = generator.GenerateUpdateQuery(item, "[LedgerAddress]", "AddID", item.AddID, "");
                        }

                        await using (SqlCommand cmd = new SqlCommand(query, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                }

                // Process Ledger Bank Info
                if (data.BankInfo?.Length > 0)
                {
                    foreach (var item in data.BankInfo)
                    {
                        item.UpdatedDate = DateTime.Now;
                        item.UpdatedByUName = userName;
                        item.UpdatedByUId = userId;
                        item.LedgerID = data.Ledgers[0].ID;

                        Genrate_Query generator = new Genrate_Query();
                        string query;

                        if (string.IsNullOrEmpty(item.ID))
                        {
                            item.CreatedDate = DateTime.Now;
                            item.CretedByUId = userId;
                            item.CretedByUName = userName;
                            item.ID = null;
                            query = generator.GenerateInsertQuery(item, "[LedgerBankDetails]", "ID");
                        }
                        else
                        {
                            query = generator.GenerateUpdateQuery(item, "[LedgerBankDetails]", "ID", item.ID, "");
                        }

                        await using (SqlCommand cmd = new SqlCommand(query, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                }

                await transaction.CommitAsync();
                return Json(new { Success = true, Message = "Document saved successfully.", Id= data.Ledgers[0].ID});
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
                await con.CloseAsync();
            }
        }

        public async Task<IActionResult> DELETELEDGER(string id)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                string query = "DELETE FROM [LedgerGroup] WHERE ID = @ID";

                await using SqlConnection con = new SqlConnection(connectionString);
                await con.OpenAsync();
                await using SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.CommandTimeout = 300;
                await cmd.ExecuteNonQueryAsync();

                return Json(new { Success = true, Message = "Ledger deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public async Task<IActionResult> DELETELEDGERBANK(string id)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                string query = "DELETE FROM [LedgerBankDetails] WHERE ID = @ID";

                await using SqlConnection con = new SqlConnection(connectionString);
                await con.OpenAsync();
                await using SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.CommandTimeout = 300;
                await cmd.ExecuteNonQueryAsync();

                return Json(new { Success = true, Message = "Ledger bank info deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public async Task<IActionResult> DELETELEDGERADDRESS(string id)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                string query = "DELETE FROM [LedgerAddress] WHERE AddID = @AddID";

                await using SqlConnection con = new SqlConnection(connectionString);
                await con.OpenAsync();
                await using SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@AddID", id);
                cmd.CommandTimeout = 300;
                await cmd.ExecuteNonQueryAsync();

                return Json(new { Success = true, Message = "Ledger address deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #endregion


        #region PAYTERM

        public async Task<IActionResult> CREATEPAYTERMS([FromBody] PAYPERMS data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                string userName = HttpContext.Session.GetString("UserName");
                string userId = HttpContext.Session.GetString("UserID");

                // Common audit fields
                data.UpdatedDate = DateTime.Now;
                data.UpdatedByUName = userName;
                data.UpdatedByUId = userId;

                Genrate_Query generator = new Genrate_Query();
                string query;

                if (string.IsNullOrEmpty(data.CredID))
                {
                    // New record
                    data.CreatedDate = DateTime.Now;
                    data.CretedByUId = userId;
                    data.CretedByUName = userName;
                    data.CredID = null;

                    query = generator.GenerateInsertQuery(data, "[CreditDaysMaster]", "CredID");
                }
                else
                {
                    // Update record
                    query = generator.GenerateUpdateQuery(data, "[CreditDaysMaster]", "CredID", data.CredID, "");
                }

                await using SqlConnection con = new SqlConnection(connectionString);
                await con.OpenAsync();
                await using SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandTimeout = 300;
                await cmd.ExecuteNonQueryAsync();

                return Json(new { Success = true, Message = "Term saved successfully." });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                // Duplicate key error
                return Conflict("A record with the same value already exists.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        public async Task<IActionResult> DELETEPAYTERMS(string id)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                string query = "DELETE FROM [CreditDaysMaster] WHERE CredID = @CredID";

                await using SqlConnection con = new SqlConnection(connectionString);
                await con.OpenAsync();
                await using SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CredID", id);
                cmd.CommandTimeout = 300;
                await cmd.ExecuteNonQueryAsync();

                return Json(new { Success = true, Message = "Term deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #endregion

        #region TDS

        public async Task<IActionResult> CREATETDS([FromBody] TDS data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                string userName = HttpContext.Session.GetString("UserName");
                string userId = HttpContext.Session.GetString("UserID");

                // Common audit fields
                data.UpdatedDate = DateTime.Now;
                data.UpdatedByUName = userName;
                data.UpdatedByUId = userId;

                Genrate_Query generator = new Genrate_Query();
                string query;

                if (string.IsNullOrEmpty(data.TDSID))
                {
                    // New record
                    data.CreatedDate = DateTime.Now;
                    data.CretedByUId = userId;
                    data.CretedByUName = userName;
                    data.TDSID = null;

                    query = generator.GenerateInsertQuery(data, "[Master_TDS]", "TDSID");
                }
                else
                {
                    // Update record
                    query = generator.GenerateUpdateQuery(data, "[Master_TDS]", "TDSID", data.TDSID, "");
                }

                await using SqlConnection con = new SqlConnection(connectionString);
                await con.OpenAsync();
                await using SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandTimeout = 300;
                await cmd.ExecuteNonQueryAsync();

                return Json(new { Success = true, Message = "Term saved successfully." });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                return Conflict("A record with the same value already exists.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        public async Task<IActionResult> DELETETDS(string id)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                string query = "DELETE FROM [Master_TDS] WHERE TDSID = @TDSID";

                await using SqlConnection con = new SqlConnection(connectionString);
                await con.OpenAsync();
                await using SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TDSID", id);
                cmd.CommandTimeout = 300;
                await cmd.ExecuteNonQueryAsync();

                return Json(new { Success = true, Message = "Term deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #endregion

        #region Fy Year

        public async Task<IActionResult> CREATEFYEAR([FromBody] FYYEAR data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                string userName = HttpContext.Session.GetString("UserName");
                string userId = HttpContext.Session.GetString("UserID");

                // Common audit fields
                data.UpdatedDate = DateTime.Now;
                data.UpdatedByUName = userName;
                data.UpdatedByUId = userId;

                Genrate_Query generator = new Genrate_Query();
                string query;

                if (string.IsNullOrEmpty(data.FYearId))
                {
                    // New record
                    data.CreatedDate = DateTime.Now;
                    data.CretedByUId = userId;
                    data.CretedByUName = userName;
                    data.FYearId = null;

                    query = generator.GenerateInsertQuery(data, "[Master_FYear]", "FYearId");
                }
                else
                {
                    // Update record
                    query = generator.GenerateUpdateQuery(data, "[Master_FYear]", "FYearId", data.FYearId, "");
                }

                await using SqlConnection con = new SqlConnection(connectionString);
                await con.OpenAsync();
                await using SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandTimeout = 300;
                await cmd.ExecuteNonQueryAsync();

                return Json(new { Success = true, Message = "Term saved successfully." });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                return Conflict("A record with the same value already exists.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        public async Task<IActionResult> DELETEFYEAR(string id)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                string query = "DELETE FROM [Master_FYear] WHERE FYearId = @FYearId";

                await using SqlConnection con = new SqlConnection(connectionString);
                await con.OpenAsync();
                await using SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FYearId", id);
                cmd.CommandTimeout = 300;
                await cmd.ExecuteNonQueryAsync();

                return Json(new { Success = true, Message = "Term deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #endregion

        #region FREIGHT

        public async Task<IActionResult> CREATEFREIGHT([FromBody] FREIGHT data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                string userName = HttpContext.Session.GetString("UserName");
                string userId = HttpContext.Session.GetString("UserID");

                // Common audit fields
                data.UpdatedDate = DateTime.Now;
                data.UpdatedByUName = userName;
                data.UpdatedByUId = userId;

                Genrate_Query generator = new Genrate_Query();
                string query;

                if (string.IsNullOrEmpty(data.FreID))
                {
                    // New record
                    data.CreatedDate = DateTime.Now;
                    data.CretedByUId = userId;
                    data.CretedByUName = userName;
                    data.FreID = null;

                    query = generator.GenerateInsertQuery(data, "[Master_Freight]", "FreID");
                }
                else
                {
                    // Update record
                    query = generator.GenerateUpdateQuery(data, "[Master_Freight]", "FreID", data.FreID, "");
                }

                await using SqlConnection con = new SqlConnection(connectionString);
                await con.OpenAsync();
                await using SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandTimeout = 300;
                await cmd.ExecuteNonQueryAsync();

                return Json(new { Success = true, Message = "Freight saved successfully." });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                return Conflict("A record with the same value already exists.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        public async Task<IActionResult> DELETEFREIGHT(string id)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                string query = "DELETE FROM [Master_Freight] WHERE FreID = @FreID";

                await using SqlConnection con = new SqlConnection(connectionString);
                await con.OpenAsync();
                await using SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FreID", id);
                cmd.CommandTimeout = 300;
                await cmd.ExecuteNonQueryAsync();

                return Json(new { Success = true, Message = "Freight deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion
        #region ITEM BULK UPLOAD
        public IActionResult ITEMBULKUPLOAD([FromBody] BulkItemUploadModel data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            // Initialize response object
            var response = new
            {
                items = new List<object>()
            };

            try
            {
                // Validate input
                if (data?.Items == null || data.Items.Length == 0)
                {
                    return BadRequest(new { success = false, error = "No items provided for upload" });
                }

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        // Create dictionaries for quick lookup of foreign keys
                        var uomCache = new Dictionary<string, string>();
                        var itemGroupCache = new Dictionary<string, string>();
                        var hsnCache = new Dictionary<string, string>();
                        var taxCache = new Dictionary<string, string>();
                        var warehouseCache = new Dictionary<string, string>();

                        // Preload lookup data to minimize database queries
                        PreloadLookupData(connection, transaction, uomCache, itemGroupCache, hsnCache, taxCache, warehouseCache);

                        // Process each item
                        foreach (var item in data.Items)
                        {

                            try
                            {
                                var itemData = item.ItemData;

                                // Initialize error message for itemData
                                string itemDataError = null;

                                // Validate required fields
                                if (string.IsNullOrWhiteSpace(itemData.ItemCode))
                                    throw new Exception("ItemCode is required");
                                if (string.IsNullOrWhiteSpace(itemData.ItemName))
                                    throw new Exception("ItemName is required");
                                if (string.IsNullOrWhiteSpace(itemData.UomCode))
                                    throw new Exception("UomCode is required");

                                // Set audit fields
                                SetAuditFields(itemData);

                                try
                                {
                                    // Get foreign key IDs from cache
                                    itemData.UomID = GetIdFromCache(uomCache, itemData.UomCode.Trim(), "UOM");
                                    itemData.ItmGrpID = GetIdFromCache(itemGroupCache, itemData.ItmGrpName.Trim(), "Item Group");
                                    itemData.HSNID = GetIdFromCache(hsnCache, itemData.HSNCode.Trim(), "HSN");
                                    itemData.TaxCodeId = GetIdFromCache(taxCache, itemData.TaxCode.Trim(), "Tax Code");

                                    // Clear the codes since we're using IDs now
                                    itemData.UomCode = null;
                                    itemData.ItmGrpName = null;
                                    itemData.HSNCode = null;
                                    itemData.TaxCode = null;

                                    // Insert item master
                                    string insertQuery = GenerateInsertQuery(itemData, "[ItemMaster]", "ItemID");
                                    ExecuteNonQuery(connection, transaction, insertQuery);

                                    // Get the newly inserted ItemID
                                    string itemId = GetIdFromLookup(connection, transaction, "ItemMaster", "ItemID", "ItemCode", itemData.ItemCode);

                                    // Process price setups if they exist
                                    if (item.PriceSetups != null && item.PriceSetups.Length > 0)
                                    {
                                        foreach (var priceSetup in item.PriceSetups)
                                        {
                                            var priceSetupDict = new Dictionary<string, object>();
                                            string priceSetupError = null;

                                            try
                                            {
                                                //// Skip if item codes don't match
                                                //if (!itemData.ItemCode.Equals(priceSetup.ItemCode, StringComparison.OrdinalIgnoreCase))
                                                //    continue;

                                                priceSetup.ItemID = itemId;
                                                priceSetup.UomID = GetIdFromCache(uomCache, priceSetup.ConversionCode.Trim(), "UOM");
                                                priceSetup.BaseUomID = itemData.UomID;
                                                priceSetup.ConversionCode = null;
                                                priceSetup.BaseUom = null;
                                                priceSetup.UpdatedDate = DateTime.Now;
                                                priceSetup.UpdatedByUName = HttpContext.Session.GetString("UserName");
                                                priceSetup.UpdatedByUId = HttpContext.Session.GetString("UserID");
                                                priceSetup.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd");
                                                priceSetup.CretedByUId = HttpContext.Session.GetString("UserID");
                                                priceSetup.CretedByUName = HttpContext.Session.GetString("UserName");
                                                priceSetup.PriceSetID = null;
                                                string priceSetupQuery = GenerateInsertQuery(priceSetup, "[ItemPriceSetup]", "PriceSetID");
                                                ExecuteNonQuery(connection, transaction, priceSetupQuery);

                                            }
                                            catch (Exception ex)
                                            {
                                                transaction.Rollback();
                                                return StatusCode(500, ex.Message.ToString()+" For Item Code "+ itemData.ItemCode);
                                            }

                                        }
                                    }

                                    // Process inventory details if they exist
                                    if (item.InventoryDetails != null && item.InventoryDetails.Length > 0)
                                    {
                                        foreach (var inventory in item.InventoryDetails)
                                        {
                                            var inventoryDict = new Dictionary<string, object>();
                                            string inventoryError = null;

                                            try
                                            {
                                                // Skip if item codes don't match
                                                //if (!item.ItemData.ItemCode.Equals(inventory.ItemCode, StringComparison.OrdinalIgnoreCase))
                                                //    continue;

                                                inventory.ItemId = itemId;
                                                inventory.WhsId = GetIdFromCache(warehouseCache, inventory.WhsCode.Trim(), "Warehouse");
                                                inventory.UpdatedDate = DateTime.Now;
                                                inventory.UpdatedByUName = HttpContext.Session.GetString("UserName");
                                                inventory.UpdatedByUId = HttpContext.Session.GetString("UserID");
                                                inventory.CretedDate = DateTime.Now;
                                                inventory.CretedByUId = HttpContext.Session.GetString("UserID");
                                                inventory.CretedByUName = HttpContext.Session.GetString("UserName");
                                                inventory.InvtDetId = null;
                                                inventory.WhsCode = null;
                                                string inventoryQuery = GenerateInsertQuery(inventory, "[Item_Invnt_det]", "InvtDetId");
                                                ExecuteNonQuery(connection, transaction, inventoryQuery);
                                            }
                                            catch (Exception ex)
                                            {
                                                transaction.Rollback();
                                                return StatusCode(500, ex.Message.ToString() + " For Item Code " + itemData.ItemCode);
                                            }
                                        }
                                    }

                                    
                                }
                                catch (Exception ex)
                                {
                                    transaction.Rollback();
                                    return StatusCode(500, ex.Message.ToString() + " For Item Code " + itemData.ItemCode);
                                }
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                return StatusCode(500, ex.Message.ToString());
                            }
                        }
                        transaction.Commit();
                        return Ok(new { success = true, message="Data Uploded Successfully.", data = response});
                    }
                }
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                return Conflict(new { success = false, error = $"Duplicate item found: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = $"An error occurred: {ex.Message}" });
            }
        }
        // Helper Methods
        private string GetIdFromLookup(SqlConnection connection, SqlTransaction transaction, string table, string idColumn, string lookupColumn, string lookupValue)
        {
            if (string.IsNullOrWhiteSpace(lookupValue)) return null;

            string query = $"SELECT {idColumn} FROM {table} WHERE {lookupColumn} = @lookupValue";
            using (var cmd = new SqlCommand(query, connection, transaction))
            {
                cmd.Parameters.AddWithValue("@lookupValue", lookupValue.Trim());
                var result = cmd.ExecuteScalar();
                return result?.ToString();
            }
        }
        private void PreloadLookupData(SqlConnection connection, SqlTransaction transaction,
            Dictionary<string, string> uomCache,
            Dictionary<string, string> itemGroupCache,
            Dictionary<string, string> hsnCache,
            Dictionary<string, string> taxCache,
            Dictionary<string, string> warehouseCache)
        {
            // Load UOM data
            LoadLookupData(connection, transaction, "UomTyp", "Code", "UomID", uomCache);

            // Load Item Group data
            LoadLookupData(connection, transaction, "ItGrpMaster", "ItmGrpNam", "ItmGrpID", itemGroupCache);

            // Load HSN data
            LoadLookupData(connection, transaction, "HSNMaster", "Code", "HSNID", hsnCache);

            // Load Tax data
            LoadLookupData(connection, transaction, "TaxCode_Mst", "TaxCode", "TaxCodeId", taxCache);

            // Load Warehouse data
            LoadLookupData(connection, transaction, "WhsMaster", "WhsCode", "ID", warehouseCache);
        }
        private void LoadLookupData(SqlConnection connection, SqlTransaction transaction,
            string tableName, string keyColumn, string valueColumn,
            Dictionary<string, string> cache)
        {
            string query = $"SELECT {keyColumn}, {valueColumn} FROM {tableName}";
            using (var cmd = new SqlCommand(query, connection, transaction))
            {
                cmd.CommandTimeout = 300;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string key = reader[keyColumn].ToString();
                        string value = reader[valueColumn].ToString();
                        if (!cache.ContainsKey(key))
                        {
                            cache.Add(key, value);
                        }
                    }
                }
            }
        }
        private string GetIdFromCache(Dictionary<string, string> cache, string key, string lookupName)
        {
            if (string.IsNullOrWhiteSpace(key)) return null;

            if (cache.TryGetValue(key, out string value))
            {
                return value;
            }
            throw new Exception($"{lookupName} '{key}' not found in the system");
        }
        private void SetAuditFields(ItemData itemData)
        {
            string userId = HttpContext.Session.GetString("UserID");
            string userName = HttpContext.Session.GetString("UserName");
            DateTime now = DateTime.Now;

            itemData.CreatedDate = now;
            itemData.UpdatedDate = now;
            itemData.CretedByUId = userId;
            itemData.CretedByUName = userName;
            itemData.UpdatedByUId = userId;
            itemData.UpdatedByUName = userName;
        }
        private string GenerateInsertQuery(object entity, string tableName, string identityColumn)
        {
            var properties = entity.GetType().GetProperties();
            var columns = new List<string>();
            var values = new List<string>();

            foreach (var prop in properties)
            {
                // Skip identity columns and null values
                if (prop.Name.Equals(identityColumn, StringComparison.OrdinalIgnoreCase))
                    continue;

                var value = prop.GetValue(entity);
                if (value == null)
                    continue;

                columns.Add($"[{prop.Name}]");

                if (value is string strValue)
                {
                    values.Add($"'{strValue.Replace("'", "''")}'");
                }
                else if (value is DateTime dateValue)
                {
                    values.Add($"'{dateValue:yyyy-MM-dd HH:mm:ss}'");
                }
                else if (value is bool boolValue)
                {
                    values.Add(boolValue ? "1" : "0");
                }
                else
                {
                    values.Add(value.ToString());
                }
            }

            return $"INSERT INTO {tableName} ({string.Join(", ", columns)}) VALUES ({string.Join(", ", values)});";
        }
        private void ExecuteNonQuery(SqlConnection connection, SqlTransaction transaction, string query)
        {
            using (var cmd = new SqlCommand(query, connection, transaction))
            {
                cmd.CommandTimeout = 300;
                cmd.ExecuteNonQuery();
            }
        }
        #endregion
    }
}
