using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TECHNOSYNCERP.Models;

namespace TECHNOSYNCERP.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IConfiguration _configuration;
        public ReportsController(ILogger<ReportsController> logger, IConfiguration configuration)
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
        public async Task<IActionResult> Reports()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("R");
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
        public async Task<IActionResult> SalesRegister()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await     GETBTNAUTH("RSR");
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
        public async Task<IActionResult> InventoryPostingList()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("RIPL");
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
        public async Task<IActionResult> PurchaseRegister()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("RSR");
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
        public async Task<IActionResult> AccountStatement()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("AS");
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
        public async Task<IActionResult> LedgerReport()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("LEDR");
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
        public async Task<IActionResult> GETREPORTS()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();
            string userId = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not logged in");
            }

            try
            {
                using (var con = new SqlConnection(connStr))
                {
                    string query = @"
                SELECT T0.ReportID, T0.Title, T0.Icon, T0.ObjType, T0.IsActive, T0.ControllerName, T0.ViewName,
                       T1.ID, T1.IsFav
                FROM Report_Definitions T0
                LEFT JOIN [Report_Configuration] T1
                       ON T1.ReportID = T0.ReportID AND T1.UserId = @UserId
            ";

                    await con.OpenAsync();

                    using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var obj = new Dictionary<string, string>();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    obj[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i).ToString();
                                }

                                list.Add(obj);
                            }
                        }
                    }
                }

                return Json(list);
            }
            catch (Exception ex)
            {
                // Optionally log exception here using ILogger
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETBPPARTNER(string type)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                using (var con = new SqlConnection(connStr))
                {
                    string query = $"EXEC GET_BusinessPartner '{type}'";

                    await con.OpenAsync();

                    using (var cmd = new SqlCommand(query, con))
                    {

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var obj = new Dictionary<string, string>();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    obj[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i).ToString();
                                }

                                list.Add(obj);
                            }
                        }
                    }
                }

                return Json(list);
            }
            catch (Exception ex)
            {
                // Optionally log exception here
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETCONDIGDATA(string ObjType)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var result = new List<Dictionary<string, string>>();
            string userId = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not logged in");
            }

            try
            {
                using (var con = new SqlConnection(connStr))
                {
                    await con.OpenAsync();

                    string query = @"
                SELECT * 
                FROM Report_Col_Configuration 
                WHERE ObjType = @ObjType AND UserID = @UserID
            ";

                    using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ObjType", ObjType ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@UserID", userId);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var obj = new Dictionary<string, string>();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    obj[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i).ToString();
                                }

                                result.Add(obj);
                            }
                        }
                    }
                }

                return Json(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async IAsyncEnumerable<Dictionary<string, string>> GETSFILTERDATA(
            string LedgerCode, string FromDate, string ToDate, string Docnum, string Docstatus, string CreatedUser, string ItemID, string Basedon)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            using (var con = new SqlConnection(connStr))
            {
                await con.OpenAsync();
                string Query = @$"Exec Report_SalesRegister '{LedgerCode}','{FromDate}','{ToDate}','{Docnum}','{Docstatus}','{CreatedUser}','{ItemID}'";
                using (var cmd = new SqlCommand(Query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (var rdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await rdr.ReadAsync())
                        {
                            var dict = new Dictionary<string, string>();
                            for (int i = 0; i < rdr.FieldCount; i++)
                            {
                                dict[rdr.GetName(i)] = rdr.IsDBNull(i) ? "" : rdr[i].ToString();
                            }
                            yield return dict; // 🚀 Stream row by row
                        }
                    }
                }
            }
        }
        public async IAsyncEnumerable<Dictionary<string, string>> GETPFILTERDATA(
         string LedgerCode, string FromDate, string ToDate, string Docnum, string Docstatus, string CreatedUser, string ItemID,string Basedon)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            using (var con = new SqlConnection(connStr))
            {
                await con.OpenAsync();
                string Query = @$"Exec Report_PurchaseRegister '{LedgerCode}','{FromDate}','{ToDate}','{Docnum}','{Docstatus}','{CreatedUser}','{ItemID}'";
                using (var cmd = new SqlCommand(Query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (var rdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await rdr.ReadAsync())
                        {
                            var dict = new Dictionary<string, string>();
                            for (int i = 0; i < rdr.FieldCount; i++)
                            {
                                dict[rdr.GetName(i)] = rdr.IsDBNull(i) ? "" : rdr[i].ToString();
                            }
                            yield return dict; // 🚀 Stream row by row
                        }
                    }

                }
            }
        }
        public async IAsyncEnumerable<Dictionary<string, string>> GETINVTORYDATA(
          string FromDate, string ToDate, string ItemID,string ItemGrp)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            using (var con = new SqlConnection(connStr))
            {
                await con.OpenAsync();
                string Query = @$"Exec Report_InventoryPostList '{FromDate}','{ToDate}','{ItemID}','{ItemGrp}'";
                using (var cmd = new SqlCommand(Query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (var rdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await rdr.ReadAsync())
                        {
                            var dict = new Dictionary<string, string>();
                            for (int i = 0; i < rdr.FieldCount; i++)
                            {
                                dict[rdr.GetName(i)] = rdr.IsDBNull(i) ? "" : rdr[i].ToString();
                            }
                            yield return dict; // 🚀 Stream row by row
                        }
                    }

                }
            }
        }
        public async IAsyncEnumerable<Dictionary<string, string>> GETACCOUNTDATA(
         string LedgerCode, string FromDate, string ToDate)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            using (var con = new SqlConnection(connStr))
            {
                await con.OpenAsync();
                string Query = @$"Exec Report_AccountStatement '{LedgerCode}','{FromDate}','{ToDate}'";
                using (var cmd = new SqlCommand(Query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (var rdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await rdr.ReadAsync())
                        {
                            var dict = new Dictionary<string, string>();
                            for (int i = 0; i < rdr.FieldCount; i++)
                            {
                                dict[rdr.GetName(i)] = rdr.IsDBNull(i) ? "" : rdr[i].ToString();
                            }
                            yield return dict; // 🚀 Stream row by row
                        }
                    }

                }
            }
        }
        [HttpPost]
        #region CERATE REPORT
        public async Task<IActionResult> CREATEREPORT([FromBody] REPORTS data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;

            try
            {
                await using var con = new SqlConnection(connectionString);
                await con.OpenAsync();

                // BeginTransaction is synchronous
                transaction = con.BeginTransaction();

                Genrate_Query generator = new Genrate_Query();
                string query;

                if (string.IsNullOrEmpty(data.ReportID))
                {
                    data.ReportID = null;
                    query = generator.GenerateInsertQuery(data, " [Report_Definitions]", "ReportID");
                }
                else
                {
                    query = generator.GenerateUpdateQuery(data, "[Report_Definitions]", "ReportID", data.ReportID, "");
                }

                await using (var cmd = new SqlCommand(query, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();

                    if (string.IsNullOrEmpty(data.ReportID))
                    {
                        cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                        var result = await cmd.ExecuteScalarAsync();
                        data.ReportID = result?.ToString();
                    }
                }

                // Commit transaction (still synchronous in older ADO.NET)
                transaction.Commit();

                return Json(new { Success = true, DocEntry = data.ReportID, Message = "Document saved successfully." });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                transaction?.Rollback();
                return Conflict("A record with the same value already exists.");
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        public async Task<IActionResult> DELETEREPORT(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(new { success = false, message = "ID cannot be empty" });
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                await using var con = new SqlConnection(connectionString);
                await con.OpenAsync();

                string query = "DELETE FROM Report_Definitions WHERE ReportID = @ReportID";

                await using (var cmd = new SqlCommand(query, con))
                {
                    cmd.CommandTimeout = 300;
                    cmd.Parameters.AddWithValue("@ReportID", id);
                    await cmd.ExecuteNonQueryAsync();
                }

                return Json(new { success = true, message = "Document Deleted Successfully!" });
            }
            catch (Exception ex)
            {
                // Optionally log exception using ILogger
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
            }
        }
        public async Task<IActionResult> SETFAV(string ReportID, string flag, string id)
        {
            if (string.IsNullOrEmpty(ReportID))
            {
                return Json(new { success = false, message = "ID cannot be empty" });
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            string userId = HttpContext.Session.GetString("UserID");

            try
            {
                await using var con = new SqlConnection(connectionString);
                await con.OpenAsync();

                if (!string.IsNullOrEmpty(id) && id != null && id != "null")
                {
                    string query = @"
                        UPDATE [Report_Configuration] 
                        SET IsFav = @IsFav, UserId = @UserId 
                        WHERE ReportID = @ReportID AND ID = @ID
                    ";

                    await using var cmd = new SqlCommand(query, con);
                    cmd.CommandTimeout = 300;
                    cmd.Parameters.AddWithValue("@IsFav", flag);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@ReportID", ReportID);
                    cmd.Parameters.AddWithValue("@ID", id);

                    await cmd.ExecuteNonQueryAsync();
                }
                else
                {
                    string query = @"
                INSERT INTO [Report_Configuration] (ReportID, UserId, IsFav) 
                VALUES (@ReportID, @UserId, @IsFav)
            ";

                    await using var cmd = new SqlCommand(query, con);
                    cmd.CommandTimeout = 300;
                    cmd.Parameters.AddWithValue("@ReportID", ReportID);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@IsFav", flag);

                    await cmd.ExecuteNonQueryAsync();
                }

                return Json(new { success = true, message = "Document Updated Successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
            }
        }
        #endregion
        #region
        public async Task<IActionResult> ADDCOLCONFIGURATION([FromBody] List<ReportConfiguration> dataList)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;

            try
            {
                await using var con = new SqlConnection(connectionString);
                await con.OpenAsync();

                // Begin transaction (still synchronous)
                transaction = con.BeginTransaction();

                foreach (var data in dataList)
                {
                    Genrate_Query generator = new Genrate_Query();
                    string query;

                    data.UserID = HttpContext.Session.GetString("UserID");

                    if (string.IsNullOrEmpty(data.ID))
                    {
                        data.ID = null;
                        query = generator.GenerateInsertQuery(data, "[Report_Col_Configuration]", "ID");
                    }
                    else
                    {
                        query = generator.GenerateUpdateQuery(data, "[Report_Col_Configuration]", "ID", data.ID, "");
                    }

                    await using var cmd = new SqlCommand(query, con, transaction);
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();
                }

                transaction.Commit();

                return Json(new { Success = true, Message = "Setting added successfully." });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                transaction?.Rollback();
                return Conflict("A record with the same value already exists.");
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        #endregion
    }
}
