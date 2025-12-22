using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Transactions;
using TECHNOSYNCERP.Models;

namespace TECHNOSYNCERP.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IConfiguration _configuration;
        public AccountsController(ILogger<AccountsController> logger, IConfiguration configuration)
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
        public async Task<IActionResult> Payment()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserID")))
            {
                ViewBag.BtnAuth = null;

                var response = await GETBTNAUTH("PAY");

                if (response is JsonResult jsonResult && jsonResult.Value is List<Dictionary<string, object>> dataList)
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
        public async Task<IActionResult> Receipt()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("REC");
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
        public async Task<IActionResult> ChequeDeposit()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("ACD");
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
        public async Task<IActionResult> LedgerOpeningBalance()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("LOB");
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
        public async Task<IActionResult> JournalEntry()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("JE");
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
        public async Task<IActionResult> Contra()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("ACON");
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
        public async Task<IActionResult> GETPAYMENTDOCNUM(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                using (var con = new SqlConnection(connStr))
                using (var cmd = new SqlCommand("GET_NextDocNumber", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@DocumentName", "Accounts_Payment_Head");

                    await con.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                var obj = new Dictionary<string, string>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    obj[reader.GetName(i)] = reader.GetValue(i)?.ToString() ?? "";
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
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETCONTRADOCNUM(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                using (var con = new SqlConnection(connStr))
                using (var cmd = new SqlCommand("GET_NextDocNumber", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@DocumentName", "Accounts_Contra_Head");

                    await con.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                var obj = new Dictionary<string, string>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    obj[reader.GetName(i)] = reader.GetValue(i)?.ToString() ?? "";
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
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETLOBDOCNUM(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                using (var con = new SqlConnection(connStr))
                using (var cmd = new SqlCommand("GET_NextDocNumber", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@DocumentName", "Account_LedgerOpeningBalance_Head");

                    await con.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                var obj = new Dictionary<string, string>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    obj[reader.GetName(i)] = reader.GetValue(i)?.ToString() ?? "";
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
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETCHEQUEDEPOSITDOCNUM(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                using (var con = new SqlConnection(connStr))
                using (var cmd = new SqlCommand("GET_NextDocNumber", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@DocumentName", "Accounts_ChequeDeposit_Head");

                    await con.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                var obj = new Dictionary<string, string>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    obj[reader.GetName(i)] = reader.GetValue(i)?.ToString() ?? "";
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
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETLOBLEDGERS(string FYearId)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();
            try
            {
                using (var con = new SqlConnection(connStr))
                using (var cmd = new SqlCommand("GET_OBLegder", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue( "@FYearId" ,FYearId);
                    await con.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                var obj = new Dictionary<string, string>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    obj[reader.GetName(i)] = reader.GetValue(i)?.ToString() ?? "";
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
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETCONTRALEDGER()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();
            try
            {
                using (var con = new SqlConnection(connStr))
                using (var cmd = new SqlCommand("Select ID,GroupName from LedgerGroup where Postable='A'  AND IsActive='A' AND (CashAcc ='Y' OR BankAcc ='Y' )", con))
                {
                    cmd.CommandType = CommandType.Text;
                    await con.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                var obj = new Dictionary<string, string>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    obj[reader.GetName(i)] = reader.GetValue(i)?.ToString() ?? "";
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
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETDEFALLOBLEDG()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();
            try
            {
                using (var con = new SqlConnection(connStr))
                using (var cmd = new SqlCommand("Select DefLedgerName,DefLedgerId FROM DefaultConfiguration where [Name]= 'LEDGEROPNINGBALANCE'", con))
                {
                    cmd.CommandType = CommandType.Text;
                    await con.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                var obj = new Dictionary<string, string>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    obj[reader.GetName(i)] = reader.GetValue(i)?.ToString() ?? "";
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
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETCHEQUEDEPOSITEROW()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();
            try
            {
                using (var con = new SqlConnection(connStr))
                using (var cmd = new SqlCommand("Exec GET_CheqeDeposit_List", con))
                {
                    cmd.CommandType = CommandType.Text;
                    await con.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                var obj = new Dictionary<string, string>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    obj[reader.GetName(i)] = reader.GetValue(i)?.ToString() ?? "";
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
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETRECEIPTDOCNUM(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                using (var con = new SqlConnection(connStr))
                using (var cmd = new SqlCommand("GET_NextDocNumber", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@DocumentName", "Accounts_Receipt_Head");

                    await con.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                var obj = new Dictionary<string, string>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    obj[reader.GetName(i)] = reader.GetValue(i)?.ToString() ?? "";
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
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETJEDOCNUM(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                using (var con = new SqlConnection(connStr))
                using (var cmd = new SqlCommand("GET_NextDocNumber", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@DocumentName", "Accounts_JournalEntry_Head");

                    await con.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                var obj = new Dictionary<string, string>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    obj[reader.GetName(i)] = reader.GetValue(i)?.ToString() ?? "";
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
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETLEDGERS(string type)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                using (var con = new SqlConnection(connStr))
                using (var cmd = new SqlCommand("Exec GET_GLACCOUNT @LedgerType", con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@LedgerType", type); // parameterized

                    await con.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                var obj = new Dictionary<string, string>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    obj[reader.GetName(i)] = reader.GetValue(i)?.ToString() ?? "";
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
                // Optionally log using ILogger
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETCASHLEDGER(string type)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                string query = @"SELECT * 
                         FROM LedgerGroup 
                         WHERE CashAcc = 'Y' AND Postable = 'A' AND IsActive = 'A'";

                using (var con = new SqlConnection(connStr))
                using (var cmd = new SqlCommand(query, con))
                {
                    await con.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                var obj = new Dictionary<string, string>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    obj[reader.GetName(i)] = reader.GetValue(i)?.ToString() ?? "";
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
                // Log exception if using ILogger
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETBANKLEDGER(string type)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                string query = @"SELECT * 
                         FROM LedgerGroup 
                         WHERE BankAcc = 'Y' AND Postable = 'A' AND IsActive = 'A'";

                using (var con = new SqlConnection(connStr))
                using (var cmd = new SqlCommand(query, con))
                {
                    await con.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                var obj = new Dictionary<string, string>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    obj[reader.GetName(i)] = reader.GetValue(i)?.ToString() ?? "";
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
                // Log exception if using ILogger
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETBANKS(string type)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                string query = @"SELECT * FROM BankMaster WHERE IsActive = 'A'";

                using (var con = new SqlConnection(connStr))
                using (var cmd = new SqlCommand(query, con))
                {
                    await con.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                var obj = new Dictionary<string, string>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    obj[reader.GetName(i)] = reader.GetValue(i)?.ToString() ?? "";
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
                // Log exception if using ILogger
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETBANKSLEDGERS(string type)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                string query = @"Select ID,GroupName FROm LedgerGroup where BankAcc ='Y'";

                using (var con = new SqlConnection(connStr))
                using (var cmd = new SqlCommand(query, con))
                {
                    await con.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                var obj = new Dictionary<string, string>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    obj[reader.GetName(i)] = reader.GetValue(i)?.ToString() ?? "";
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
                // Log exception if using ILogger
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETLEDGERPAYMENTDETAILS(string Id, string type, string flag)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var list = new List<Dictionary<string, string>>();

            try
            {
                using (var con = new SqlConnection(connStr))
                using (var cmd = new SqlCommand("Accounts_Payment_LedgerDet", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LedgerID", Id);
                    cmd.Parameters.AddWithValue("@ObjType", type);
                    cmd.Parameters.AddWithValue("@Flag", flag);

                    await con.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                var obj = new Dictionary<string, string>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    obj[reader.GetName(i)] = reader.GetValue(i)?.ToString() ?? "";
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
                // Log exception using ILogger if available
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

        [HttpPost]
        #region ACCOUNT PAYMENT
          public async Task<IActionResult> CREATEPAYMENT([FromBody] ACCOUNTSPAYMENT data, string flag, string doctype)
          {
            if (flag == "P")
            {
                return await PARK(data, doctype); // keep this sync if PARK is sync
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(connectionString);
                await con.OpenAsync();
                transaction = con.BeginTransaction();

                // Process Header
                var header = data.header;
                header.UpdatedDate = DateTime.Now;
                header.UpdatedByUName = HttpContext.Session.GetString("UserName");
                header.UpdatedByUId = HttpContext.Session.GetString("UserID");
                await using (var cmd = new SqlCommand(@$"Select DefLedgerId FROm DefaultConfiguration where [Name]='CHEQUEACCOUNT'", con, transaction))
                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        header.ChequeLedgerId = reader["DefLedgerId"].ToString();
                    }
                }
                Genrate_Query generator = new Genrate_Query();
                string query;

                if (string.IsNullOrEmpty(header.DocEntry))
                {
                    // New record
                    header.CreatedDate = DateTime.Now;
                    header.CretedByUId = HttpContext.Session.GetString("UserID");
                    header.CretedByUName = HttpContext.Session.GetString("UserName");
                    header.DocEntry = null;
                    if (header.ChequeAmt == 0)
                    {
                        header.ChequeLedgerId = "";
                    }
                    else if (header.CashAmt == 0)
                    {
                        header.CashLedgerId = "";
                    }
                    else if (header.BankAmt == 0)
                    {
                        header.BankLedgerId = "";
                    }
                    query = generator.GenerateInsertQuery(header, " [Accounts_Payment_Head]", "DocEntry");
                }
                else
                {
                    query = generator.GenerateUpdateQuery(header, "[Accounts_Payment_Head]", "DocEntry", header.DocEntry, "");
                }

                using (SqlCommand cmd = new SqlCommand(query, con, transaction))
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
                if (data.lines != null && data.lines.Length > 0 )
                {
                    using (SqlCommand cmd = new SqlCommand($"DELETE FROM [Accounts_Payment_Row] WHERE DocEntry = @DocEntry", con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@DocEntry", header.DocEntry);
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }

                    foreach (var line in data.lines)
                    {
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = header.DocEntry;

                        line.CreatedDate = DateTime.Now;
                        line.CretedByUId = HttpContext.Session.GetString("UserID");
                        line.CretedByUName = HttpContext.Session.GetString("UserName");
                        line.ID = null;

                        string lineQuery = generator.GenerateInsertQuery(line, "[Accounts_Payment_Row]", "ID");

                        using (SqlCommand cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }

                        if (line.BaseObjType == "SR" || line.BaseObjType == "SCN")
                        {
                            string headTable = "Sale_SaleCreditNote_Head";
                            string rowTable = "Sale_SaleCreditNote_Row";

                            string updateQuery = $@"
                                UPDATE {headTable}
                                SET BalanceDue = @NewBalanceDue,
                                    AppliedAmount = @AppliedAmount
                                WHERE DocEntry = @BaseDocEntry;";

                            if ((line.BalanceDue - line.TotalPayment) == 0)
                            {
                                updateQuery += $@"
                                UPDATE {headTable} SET DocStatus = 'C' WHERE DocEntry = @BaseDocEntry;
                                UPDATE {rowTable} SET [Status] = 'C' WHERE DocEntry = @BaseDocEntry;";
                            }

                            using (SqlCommand cmd = new SqlCommand(updateQuery, con, transaction))
                            {
                                cmd.Parameters.AddWithValue("@NewBalanceDue", line.BalanceDue - line.TotalPayment);
                                cmd.Parameters.AddWithValue("@AppliedAmount", line.TotalPayment);
                                cmd.Parameters.AddWithValue("@BaseDocEntry", line.BaseDocEntry);
                                cmd.CommandTimeout = 300;
                                await cmd.ExecuteNonQueryAsync();
                            }
                        }
                        else if (line.BaseObjType == "PI")
                        {
                            string headTable = "Purchase_PurchaseInvoiceHead";
                            string rowTable = "Purchase_PurchaseInvoiceRow";

                            string updateQuery = $@"
                                UPDATE {headTable}
                                SET BalanceDue = @NewBalanceDue,
                                    AppliedAmount = @AppliedAmount
                                WHERE DocEntry = @BaseDocEntry;";

                            if ((line.BalanceDue - line.TotalPayment) == 0)
                            {
                                updateQuery += $@"
                                UPDATE {headTable} SET DocStatus = 'C' WHERE DocEntry = @BaseDocEntry;
                                UPDATE {rowTable} SET [Status] = 'C' WHERE DocEntry = @BaseDocEntry;";
                            }

                            using (SqlCommand cmd = new SqlCommand(updateQuery, con, transaction))
                            {
                                cmd.Parameters.AddWithValue("@NewBalanceDue", line.BalanceDue - line.TotalPayment);
                                cmd.Parameters.AddWithValue("@AppliedAmount", line.TotalPayment);
                                cmd.Parameters.AddWithValue("@BaseDocEntry", line.BaseDocEntry);
                                cmd.CommandTimeout = 300;
                                await cmd.ExecuteNonQueryAsync();
                            }
                        }

                    }
                }

                // Execute Journal Entry stored procedure
                using (SqlCommand cmd = new SqlCommand("Create_PaymentJournalEntry", con, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InvoiceDocEntry", header.DocEntry);
                    cmd.Parameters.AddWithValue("@Objtype", "PAY");
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();
                }

                transaction.Commit();
                return Json(new { Success = true, DocEntry = header.DocEntry, Message = "Document saved successfully." });
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
            finally
            {
                transaction?.Dispose();
                if (con != null)
                {
                    await con.CloseAsync();
                    con.Dispose();
                }
            }
        }
        public async Task<IActionResult> DELETEPAYMENT([FromBody] ACCOUNTSPAYMENT data)
        {
            if (string.IsNullOrEmpty(data.header.DocEntry))
            {
                return Json(new { success = false, message = "Please Select Valid Document." });
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();
                    transaction = con.BeginTransaction();

                    // Delete header and rows
                    string deleteQuery = @"
                        DELETE FROM [Accounts_Payment_Row] WHERE DocEntry = @id;
                        DELETE FROM [Accounts_Payment_Head] WHERE DocEntry = @id;
                    ";

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@id", data.header.DocEntry);
                        await cmd.ExecuteNonQueryAsync();
                    }

                    // Update referenced SR documents if any
                    if (data.lines != null)
                    {
                        foreach (var line in data.lines)
                        {
                            if (line.BaseObjType == "SR")
                            {
                                string updateQuery = @"
                            UPDATE Sale_SaleCreditNote_Head SET DocStatus = 'O' WHERE DocEntry = @BaseDocEntry;
                            UPDATE Sale_SaleCreditNote_Row SET [Status] = 'O' WHERE DocEntry = @BaseDocEntry;
                        ";

                                using (SqlCommand cmd = new SqlCommand(updateQuery, con, transaction))
                                {
                                    cmd.CommandTimeout = 300;
                                    cmd.Parameters.AddWithValue("@BaseDocEntry", line.BaseDocEntry);
                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }
                        }
                    }

                    transaction.Commit();
                    return Json(new { success = true, message = "Document Deleted Successfully!" });
                }
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }

                // Optionally log exception here
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
            }
            finally
            {
                transaction?.Dispose();
            }
        }
        #endregion
        #region ACCOUNT RECEIPT
            public async Task<IActionResult> CREATERECEIPT([FromBody] ACCOUNTSRECEIPT data, string flag, string doctype)
            {
                if (flag == "P")
                {
                    return await PARK(data, doctype); // Keep sync if PARK is sync
                }

                string connectionString = _configuration.GetConnectionString("ErpConnection");
                SqlTransaction transaction = null;
                SqlConnection con = null;

                try
                {
                    con = new SqlConnection(connectionString);
                    await con.OpenAsync();
                    transaction = con.BeginTransaction();

                    // Process Header
                    var header = data.header;
                    header.UpdatedDate = DateTime.Now;
                    header.UpdatedByUName = HttpContext.Session.GetString("UserName");
                    header.UpdatedByUId = HttpContext.Session.GetString("UserID");
                    await using (var cmd = new SqlCommand(@$"Select DefLedgerId FROm DefaultConfiguration where [Name]='CHEQUEACCOUNT'", con, transaction))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            header.ChequeLedgerId = reader["DefLedgerId"].ToString();
                        }
                    }
                    Genrate_Query generator = new Genrate_Query();
                    string query;

                    if (string.IsNullOrEmpty(header.DocEntry))
                    {
                        // New record
                        header.CreatedDate = DateTime.Now;
                        header.CretedByUId = HttpContext.Session.GetString("UserID");
                        header.CretedByUName = HttpContext.Session.GetString("UserName");
                        header.DocEntry = null;
                        if (header.ChequeAmt==0)
                        {
                            header.ChequeLedgerId = "";
                        }
                        else if(header.CashAmt==0)
                        {
                            header.CashLedgerId= "";
                        }
                        else if (header.BankAmt==0)
                        {
                            header.BankLedgerId = "";
                        }
                    query = generator.GenerateInsertQuery(header, "[Accounts_Receipt_Head]", "DocEntry");
                    }
                    else
                    {
                        query = generator.GenerateUpdateQuery(header, "[Accounts_Receipt_Head]", "DocEntry", header.DocEntry, "");
                    }

                    using (SqlCommand cmd = new SqlCommand(query, con, transaction))
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
                    if (data.lines != null && data.lines.Length > 0 )
                    {
                        using (SqlCommand cmd = new SqlCommand($"DELETE FROM [Accounts_Receipt_Row] WHERE DocEntry = @DocEntry", con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@DocEntry", header.DocEntry);
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }

                        foreach (var line in data.lines)
                        {
                            line.UpdatedDate = DateTime.Now;
                            line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                            line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                            line.DocEntry = header.DocEntry;

                            line.CreatedDate = DateTime.Now;
                            line.CretedByUId = HttpContext.Session.GetString("UserID");
                            line.CretedByUName = HttpContext.Session.GetString("UserName");
                            line.ID = null;

                            string lineQuery = generator.GenerateInsertQuery(line, "[Accounts_Receipt_Row]", "ID");

                            using (SqlCommand cmd = new SqlCommand(lineQuery, con, transaction))
                            {
                                cmd.CommandTimeout = 300;
                                await cmd.ExecuteNonQueryAsync();
                            }

                        if (line.BaseObjType == "STI")
                        {
                            string headTable = "Sale_SalesTaxInvoice_Head";
                            string rowTable = "Sale_SalesTaxInvoice_Row";

                            string updateQuery = $@"
                                UPDATE {headTable}
                                SET BalanceDue = @NewBalanceDue,
                                    AppliedAmount = @AppliedAmount
                                WHERE DocEntry = @BaseDocEntry;";

                            if ((line.BalanceDue - line.TotalPayment) == 0)
                            {
                                updateQuery += $@"
                                UPDATE {headTable} SET DocStatus = 'C' WHERE DocEntry = @BaseDocEntry;
                                UPDATE {rowTable} SET [Status] = 'C' WHERE DocEntry = @BaseDocEntry;";
                            }

                            using (SqlCommand cmd = new SqlCommand(updateQuery, con, transaction))
                            {
                                cmd.Parameters.AddWithValue("@NewBalanceDue", line.BalanceDue - line.TotalPayment);
                                cmd.Parameters.AddWithValue("@AppliedAmount", line.TotalPayment);
                                cmd.Parameters.AddWithValue("@BaseDocEntry", line.BaseDocEntry);
                                cmd.CommandTimeout = 300;
                                await cmd.ExecuteNonQueryAsync();
                            }
                        }
                        if (line.BaseObjType == "PDN")
                        {
                            string headTable = "Purchase_PurchaseDebitNote_Head";
                            string rowTable = "Purchase_PurchaseDebitNote_Row";

                            string updateQuery = $@"
                                UPDATE {headTable}
                                SET BalanceDue = @NewBalanceDue,
                                    AppliedAmount = @AppliedAmount
                                WHERE DocEntry = @BaseDocEntry;";

                            if ((line.BalanceDue - line.TotalPayment) == 0)
                            {
                                updateQuery += $@"
                                UPDATE {headTable} SET DocStatus = 'C' WHERE DocEntry = @BaseDocEntry;
                                UPDATE {rowTable} SET [Status] = 'C' WHERE DocEntry = @BaseDocEntry;";
                            }

                            using (SqlCommand cmd = new SqlCommand(updateQuery, con, transaction))
                            {
                                cmd.Parameters.AddWithValue("@NewBalanceDue", line.BalanceDue - line.TotalPayment);
                                cmd.Parameters.AddWithValue("@AppliedAmount", line.TotalPayment);
                                cmd.Parameters.AddWithValue("@BaseDocEntry", line.BaseDocEntry);
                                cmd.CommandTimeout = 300;
                                await cmd.ExecuteNonQueryAsync();
                            }
                        }
                      }
                    }

                    // Execute Journal Entry stored procedure
                    using (SqlCommand cmd = new SqlCommand("Create_PaymentJournalEntry", con, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@InvoiceDocEntry", header.DocEntry);
                        cmd.Parameters.AddWithValue("@Objtype", "REC");
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }

                    transaction.Commit();
                    return Json(new { Success = true, DocEntry = header.DocEntry, Message = "Document saved successfully." });
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
                finally
                {
                    transaction?.Dispose();
                    if (con != null)
                    {
                        await con.CloseAsync();
                        con.Dispose();
                    }
                }
            }
            public async Task<IActionResult> DELETERECEIPT([FromBody] ACCOUNTSRECEIPT data)
            {
                if (string.IsNullOrEmpty(data.header.DocEntry))
                {
                    return Json(new { success = false, message = "Please Select Valid Document." });
                }

                string connectionString = _configuration.GetConnectionString("ErpConnection");
                SqlTransaction transaction = null;

                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        await con.OpenAsync();
                        transaction = con.BeginTransaction();

                        // Delete header and row records
                        string deleteQuery = @"
                            DELETE FROM [Accounts_Receipt_Row] WHERE DocEntry = @id;
                            DELETE FROM [Accounts_Receipt_Head] WHERE DocEntry = @id;
                        ";

                        using (SqlCommand cmd = new SqlCommand(deleteQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            cmd.Parameters.AddWithValue("@id", data.header.DocEntry);
                            await cmd.ExecuteNonQueryAsync();
                        }

                        // Update referenced STI documents if any
                        if (data.lines != null)
                        {
                            foreach (var line in data.lines)
                            {
                                if (line.BaseObjType == "STI")
                                {
                                    string updateQuery = @"
                                        UPDATE Sale_SaleReturn_Head SET DocStatus = 'O' WHERE DocEntry = @BaseDocEntry;
                                        UPDATE Sale_SaleReturn_Row SET [Status] = 'O' WHERE DocEntry = @BaseDocEntry;
                                    ";

                                    using (SqlCommand cmd = new SqlCommand(updateQuery, con, transaction))
                                    {
                                        cmd.CommandTimeout = 300;
                                        cmd.Parameters.AddWithValue("@BaseDocEntry", line.BaseDocEntry);
                                        await cmd.ExecuteNonQueryAsync();
                                    }
                                }
                            }
                        }

                        transaction.Commit();
                        return Json(new { success = true, message = "Document Deleted Successfully!" });
                    }
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    // Optionally log exception here
                    return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
                }
                finally
                {
                    transaction?.Dispose();
                }
            }
        #endregion
        #region LEDGER OPENING BALANCE
        public async Task<IActionResult> CREATEOPNINGBALANCE([FromBody] LEDGEROPNING data, string flag, string doctype)
        {
            if (flag == "P")
            {
                return await PARK(data, doctype); // Keep sync if PARK is sync
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(connectionString);
                await con.OpenAsync();
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

                    query = generator.GenerateInsertQuery(header, "[Account_LedgerOpeningBalance_Head]", "DocEntry");
                }
                else
                {
                    query = generator.GenerateUpdateQuery(header, "[Account_LedgerOpeningBalance_Head]", "DocEntry", header.DocEntry, "");
                }

                using (SqlCommand cmd = new SqlCommand(query, con, transaction))
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
                    using (SqlCommand cmd = new SqlCommand($" DELETE FROM [Account_LedgerOpeningBalance_Row] WHERE DocEntry = @DocEntry", con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@DocEntry", header.DocEntry);
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }

                    foreach (var line in data.lines)
                    {
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = header.DocEntry;

                        line.CreatedDate = DateTime.Now;
                        line.CretedByUId = HttpContext.Session.GetString("UserID");
                        line.CretedByUName = HttpContext.Session.GetString("UserName");
                        line.ID = null;

                        string lineQuery = generator.GenerateInsertQuery(line, "[Account_LedgerOpeningBalance_Row]", "ID");

                        using (SqlCommand cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                }

                // Execute Journal Entry stored procedure
                using (SqlCommand cmd = new SqlCommand(
                    @"SELECT DocEntry 
                      FROM Accounts_JournalEntry_Head 
                      WHERE BaseObjType = 'LOB' 
                        AND BaseDocEntry = @BaseDocEntry", con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    cmd.Parameters.AddWithValue("@BaseDocEntry", header.DocEntry);

                    using (var rdr = await cmd.ExecuteReaderAsync())
                    {
                        if (await rdr.ReadAsync())
                        {
                            var baseDocEntry = rdr["DocEntry"].ToString();
                            rdr.Close(); // ensure reader is closed before new command executes

                            using (SqlCommand cmd1 = new SqlCommand(
                                @"DELETE FROM Accounts_JournalEntry_Row WHERE DocEntry = @DocEntry;
                                  DELETE FROM Accounts_JournalEntry_Head WHERE DocEntry = @DocEntry;", con, transaction))
                            {
                                cmd1.CommandTimeout = 300;
                                cmd1.Parameters.AddWithValue("@DocEntry", baseDocEntry);
                                await cmd1.ExecuteNonQueryAsync();
                            }
                        }
                        else
                        {
                            
                        }
                    }
                }

                using (SqlCommand cmd = new SqlCommand("Create_AccountsJournalEntry", con, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InvoiceDocEntry", header.DocEntry);
                    cmd.Parameters.AddWithValue("@Objtype", "LOB");
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();
                }

                transaction.Commit();
                return Json(new { Success = true, DocEntry = header.DocEntry, Message = "Document saved successfully." });
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
            finally
            {
                transaction?.Dispose();
                if (con != null)
                {
                    await con.CloseAsync();
                    con.Dispose();
                }
            }
        }
        public async Task<IActionResult> DELETELEDGEROPNING(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(new { success = false, message = "Please Select Valid Document." });
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();
                    transaction = con.BeginTransaction();

                    using (SqlCommand cmd = new SqlCommand(
                  @"SELECT DocEntry 
                      FROM Accounts_JournalEntry_Head 
                      WHERE BaseObjType = 'LOB' 
                        AND BaseDocEntry = @BaseDocEntry", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@BaseDocEntry", id);

                        using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            if (await rdr.ReadAsync())
                            {
                                var baseDocEntry = rdr["DocEntry"].ToString();
                                rdr.Close(); // ensure reader is closed before new command executes

                                using (SqlCommand cmd1 = new SqlCommand(
                                    @"DELETE FROM Accounts_JournalEntry_Row WHERE DocEntry = @DocEntry;
                                  DELETE FROM Accounts_JournalEntry_Head WHERE DocEntry = @DocEntry;", con, transaction))
                                {
                                    cmd1.CommandTimeout = 300;
                                    cmd1.Parameters.AddWithValue("@DocEntry", baseDocEntry);
                                    await cmd1.ExecuteNonQueryAsync();
                                }
                            }
                            else
                            {

                            }
                        }
                    }
                    // Delete header and row records
                    string deleteQuery = @"
                            DELETE FROM [Account_LedgerOpeningBalance_Row] WHERE DocEntry = @id;
                            DELETE FROM [Account_LedgerOpeningBalance_Head] WHERE DocEntry = @id;
                        ";

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@id", id);
                        await cmd.ExecuteNonQueryAsync();
                    }

                    transaction.Commit();
                    return Json(new { success = true, message = "Document Deleted Successfully!" });
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                // Optionally log exception here
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
            }
            finally
            {
                transaction?.Dispose();
            }
        }
        #endregion
        #region JOURNAL ENTRY
        public async Task<IActionResult> CREATEJE([FromBody] JOURNALENTRY data, string flag, string doctype)
        {
            if (flag == "P")
            {
                return await PARK(data, doctype); // keep sync if PARK is sync
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(connectionString);
                await con.OpenAsync();
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

                    query = generator.GenerateInsertQuery(header, "[Accounts_JournalEntry_Head]", "DocEntry");
                }
                else
                {
                    query = generator.GenerateUpdateQuery(header, "[Accounts_JournalEntry_Head]", "DocEntry", header.DocEntry, "");
                }

                using (SqlCommand cmd = new SqlCommand(query, con, transaction))
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
                    using (SqlCommand cmd = new SqlCommand($"DELETE FROM [Accounts_JournalEntry_Row] WHERE DocEntry = @DocEntry", con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@DocEntry", header.DocEntry);
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }

                    foreach (var line in data.lines)
                    {
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = header.DocEntry;

                        line.CreatedDate = DateTime.Now;
                        line.CretedByUId = HttpContext.Session.GetString("UserID");
                        line.CretedByUName = HttpContext.Session.GetString("UserName");
                        line.ID = null;

                        string lineQuery = generator.GenerateInsertQuery(line, "[Accounts_JournalEntry_Row]", "ID");

                        using (SqlCommand cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                }

                transaction.Commit();
                return Json(new { Success = true, DocEntry = header.DocEntry, Message = "Document saved successfully." });
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
            finally
            {
                transaction?.Dispose();
                if (con != null)
                {
                    await con.CloseAsync();
                    con.Dispose();
                }
            }
        }
         public async Task<IActionResult> DELETEJE(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(new { success = false, message = "ID cannot be empty" });
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    string query = @"
                        DELETE FROM [Accounts_JournalEntry_Row] WHERE DocEntry = @id;
                        DELETE FROM [Accounts_JournalEntry_Head] WHERE DocEntry = @id;
                    ";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@id", id);
                        await cmd.ExecuteNonQueryAsync();
                    }

                    return Json(new { success = true, message = "Document Deleted Successfully!" });
                }
            }
            catch (Exception ex)
            {
                // Optionally log exception here
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
            }
        }
        #endregion
        #region CREATECONTRA
        public async Task<IActionResult> CREATECONTRA([FromBody] CONTRA data, string flag, string doctype)
        {
            if (flag == "P")
            {
                return await PARK(data, doctype); // keep sync if PARK is sync
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(connectionString);
                await con.OpenAsync();
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

                    query = generator.GenerateInsertQuery(header, "[Accounts_Contra_Head]", "DocEntry");
                }
                else
                {
                    query = generator.GenerateUpdateQuery(header, "[Accounts_Contra_Head]", "DocEntry", header.DocEntry, "");
                }

                using (SqlCommand cmd = new SqlCommand(query, con, transaction))
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
                    using (SqlCommand cmd = new SqlCommand($"DELETE FROM [Accounts_Contra_Row] WHERE DocEntry = @DocEntry", con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@DocEntry", header.DocEntry);
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }

                    foreach (var line in data.lines)
                    {
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = header.DocEntry;

                        line.CreatedDate = DateTime.Now;
                        line.CretedByUId = HttpContext.Session.GetString("UserID");
                        line.CretedByUName = HttpContext.Session.GetString("UserName");
                        line.ID = null;

                        string lineQuery = generator.GenerateInsertQuery(line, "[Accounts_Contra_Row]", "ID");

                        using (SqlCommand cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                }
                // Execute Journal Entry stored procedure
                using (SqlCommand cmd = new SqlCommand(
                    @"SELECT DocEntry 
                      FROM Accounts_JournalEntry_Head 
                      WHERE BaseObjType = 'ACON' 
                        AND BaseDocEntry = @BaseDocEntry", con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    cmd.Parameters.AddWithValue("@BaseDocEntry", header.DocEntry);

                    using (var rdr = await cmd.ExecuteReaderAsync())
                    {
                        if (await rdr.ReadAsync())
                        {
                            var baseDocEntry = rdr["DocEntry"].ToString();
                            rdr.Close(); // ensure reader is closed before new command executes

                            using (SqlCommand cmd1 = new SqlCommand(
                                @"DELETE FROM Accounts_JournalEntry_Row WHERE DocEntry = @DocEntry;
                                  DELETE FROM Accounts_JournalEntry_Head WHERE DocEntry = @DocEntry;", con, transaction))
                            {
                                cmd1.CommandTimeout = 300;
                                cmd1.Parameters.AddWithValue("@DocEntry", baseDocEntry);
                                await cmd1.ExecuteNonQueryAsync();
                            }
                        }
                        else
                        {

                        }
                    }
                }
                using (SqlCommand cmd = new SqlCommand("Create_AccountsJournalEntry", con, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InvoiceDocEntry", header.DocEntry);
                    cmd.Parameters.AddWithValue("@Objtype", "ACON");
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();
                }
                transaction.Commit();
                return Json(new { Success = true, DocEntry = header.DocEntry, Message = "Document saved successfully." });
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
            finally
            {
                transaction?.Dispose();
                if (con != null)
                {
                    await con.CloseAsync();
                    con.Dispose();
                }
            }
        }
        public async Task<IActionResult> DELETECONTRA(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(new { success = false, message = "ID cannot be empty" });
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync(); // ✅ Open connection at start

                    // ✅ 1: Get Journal Entry linked to deposit
                    string getBaseDocQuery = @"
                SELECT DocEntry 
                FROM Accounts_JournalEntry_Head 
                WHERE BaseObjType = 'ACON' 
                  AND BaseDocEntry = @BaseDocEntry";

                    string baseDocEntry = null;

                    using (SqlCommand cmd = new SqlCommand(getBaseDocQuery, con))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@BaseDocEntry", id);

                        using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            if (await rdr.ReadAsync())
                            {
                                baseDocEntry = rdr["DocEntry"].ToString();
                            }
                        }
                    }

                    // ✅ 2: If journal entry exists → delete it
                    if (!string.IsNullOrEmpty(baseDocEntry))
                    {
                        string deleteJournalQuery = @"
                    DELETE FROM Accounts_JournalEntry_Row WHERE DocEntry = @DocEntry;
                    DELETE FROM Accounts_JournalEntry_Head WHERE DocEntry = @DocEntry;";

                        using (SqlCommand cmd1 = new SqlCommand(deleteJournalQuery, con))
                        {
                            cmd1.CommandTimeout = 300;
                            cmd1.Parameters.AddWithValue("@DocEntry", baseDocEntry);
                            await cmd1.ExecuteNonQueryAsync();
                        }
                    }

                    // ✅ 3: Delete cheque deposit record
                    string deleteDepositQuery = @"
                DELETE FROM Accounts_Contra_Row WHERE DocEntry = @id;
                DELETE FROM Accounts_Contra_Head WHERE DocEntry = @id;";

                    using (SqlCommand cmd = new SqlCommand(deleteDepositQuery, con))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@id", id);
                        await cmd.ExecuteNonQueryAsync();
                    }

                    return Json(new { success = true, message = "Document Deleted Successfully!" });
                }
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
            }
        }
        #endregion
        #region CHEQUE DEPOSIT
        public async Task<IActionResult> CEREATECHEQUEDEPOSIT([FromBody] CHEQUEDEPOSIT data, string flag, string doctype)
        {
            if (flag == "P")
            {
                return await PARK(data, doctype); // keep sync if PARK is sync
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(connectionString);
                await con.OpenAsync();
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

                    query = generator.GenerateInsertQuery(header, "[Accounts_ChequeDeposit_Head]", "DocEntry");
                }
                else
                {
                    query = generator.GenerateUpdateQuery(header, "[Accounts_ChequeDeposit_Head]", "DocEntry", header.DocEntry, "");
                }

                using (SqlCommand cmd = new SqlCommand(query, con, transaction))
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
                    using (SqlCommand cmd = new SqlCommand($"DELETE FROM [Accounts_ChequeDeposit_Row] WHERE DocEntry = @DocEntry", con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@DocEntry", header.DocEntry);
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }

                    foreach (var line in data.lines)
                    {
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = header.DocEntry;

                        line.CreatedDate = DateTime.Now;
                        line.CretedByUId = HttpContext.Session.GetString("UserID");
                        line.CretedByUName = HttpContext.Session.GetString("UserName");
                        line.ID = null;

                        string lineQuery = generator.GenerateInsertQuery(line, "[Accounts_ChequeDeposit_Row]", "ID");

                        using (SqlCommand cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                }
                // Execute Journal Entry stored procedure
                using (SqlCommand cmd = new SqlCommand(
                    @"SELECT DocEntry 
                      FROM Accounts_JournalEntry_Head 
                      WHERE BaseObjType = 'ACD' 
                        AND BaseDocEntry = @BaseDocEntry", con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    cmd.Parameters.AddWithValue("@BaseDocEntry", header.DocEntry);

                    using (var rdr = await cmd.ExecuteReaderAsync())
                    {
                        if (await rdr.ReadAsync())
                        {
                            var baseDocEntry = rdr["DocEntry"].ToString();
                            rdr.Close(); // ensure reader is closed before new command executes

                            using (SqlCommand cmd1 = new SqlCommand(
                                @"DELETE FROM Accounts_JournalEntry_Row WHERE DocEntry = @DocEntry;
                                  DELETE FROM Accounts_JournalEntry_Head WHERE DocEntry = @DocEntry;", con, transaction))
                            {
                                cmd1.CommandTimeout = 300;
                                cmd1.Parameters.AddWithValue("@DocEntry", baseDocEntry);
                                await cmd1.ExecuteNonQueryAsync();
                            }
                        }
                        else
                        {

                        }
                    }
                }
               using (SqlCommand cmd = new SqlCommand("Create_AccountsJournalEntry", con, transaction))
               {
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.Parameters.AddWithValue("@InvoiceDocEntry", header.DocEntry);
                   cmd.Parameters.AddWithValue("@Objtype", "ACD");
                   cmd.CommandTimeout = 300;
                   await cmd.ExecuteNonQueryAsync();
               }
                transaction.Commit();
                return Json(new { Success = true, DocEntry = header.DocEntry, Message = "Document saved successfully." });
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
            finally
            {
                transaction?.Dispose();
                if (con != null)
                {
                    await con.CloseAsync();
                    con.Dispose();
                }
            }
        }
        public async Task<IActionResult> DELETEQUEDEPOSIT(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(new { success = false, message = "ID cannot be empty" });
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync(); // ✅ Open connection at start

                    // ✅ 1: Get Journal Entry linked to deposit
                    string getBaseDocQuery = @"
                SELECT DocEntry 
                FROM Accounts_JournalEntry_Head 
                WHERE BaseObjType = 'ACD' 
                  AND BaseDocEntry = @BaseDocEntry";

                    string baseDocEntry = null;

                    using (SqlCommand cmd = new SqlCommand(getBaseDocQuery, con))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@BaseDocEntry", id);

                        using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            if (await rdr.ReadAsync())
                            {
                                baseDocEntry = rdr["DocEntry"].ToString();
                            }
                        }
                    }

                    // ✅ 2: If journal entry exists → delete it
                    if (!string.IsNullOrEmpty(baseDocEntry))
                    {
                        string deleteJournalQuery = @"
                    DELETE FROM Accounts_JournalEntry_Row WHERE DocEntry = @DocEntry;
                    DELETE FROM Accounts_JournalEntry_Head WHERE DocEntry = @DocEntry;";

                        using (SqlCommand cmd1 = new SqlCommand(deleteJournalQuery, con))
                        {
                            cmd1.CommandTimeout = 300;
                            cmd1.Parameters.AddWithValue("@DocEntry", baseDocEntry);
                            await cmd1.ExecuteNonQueryAsync();
                        }
                    }

                    // ✅ 3: Delete cheque deposit record
                    string deleteDepositQuery = @"
                DELETE FROM Accounts_ChequeDeposit_Row WHERE DocEntry = @id;
                DELETE FROM Accounts_ChequeDeposit_Head WHERE DocEntry = @id;";

                    using (SqlCommand cmd = new SqlCommand(deleteDepositQuery, con))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@id", id);
                        await cmd.ExecuteNonQueryAsync();
                    }

                    return Json(new { success = true, message = "Document Deleted Successfully!" });
                }
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
            }
        }

        #endregion
        #region PARK DOC
        public async Task<IActionResult> PARK([FromBody] dynamic data, string OBJType)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                ParkDoc generator = new ParkDoc();
                var parkData = new Park
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

                // Wrap synchronous operation in Task.Run for async behavior
                var result = await Task.Run(() => generator.ParKDocument(parkData, connectionString));

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
