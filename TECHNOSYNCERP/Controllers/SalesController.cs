using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using TECHNOSYNCERP.Models;

namespace TECHNOSYNCERP.Controllers
{
    public class SalesController : Controller
    {
        private readonly IConfiguration _configuration;
        public SalesController(ILogger<SalesController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> GETLAYCOFIG(string objtype)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = @" SELECT  ID,  ObjType, MenuName,LayoutName,Size,
                    Icon,FileName FROM Layout_Configuration WHERE ObjType = @ObjType";

                var dataList = new List<Dictionary<string, object>>();

                await using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ObjType", objtype);
                        cmd.CommandTimeout = 300;

                        await con.OpenAsync();
                        await using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
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
        public async Task<IActionResult> GETBTNAUTH(string objtype)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string userId = HttpContext.Session.GetString("UserID") ?? "";
                string query = @$"
            SELECT *
            FROM BtnAuth T0
            INNER JOIN Btn T1 ON T1.BtnID = T0.BtnID
            WHERE T0.ObjType = @ObjType AND T0.UserID = @UserID";

                var dataList = new List<Dictionary<string, object>>();

                await using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ObjType", objtype);
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        cmd.CommandTimeout = 300;

                        await con.OpenAsync();
                        await using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
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
        public async Task<IActionResult> SalesQuotation()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("SQ");
                if (responce is JsonResult jsonResult && jsonResult.Value is List<Dictionary<string, object>> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        ViewBag.BtnAuth = dataList;
                    }
                }
                ViewBag.DLCData = null;
                // Get DLC (Layout Configuration) data
                var dlcResponse = await GETLAYCOFIG("SQ");
                if (dlcResponse is JsonResult dlcJson && dlcJson.Value is List<Dictionary<string, object>> dlcList)
                {
                    if (dlcList.Count > 0)
                    {
                        ViewBag.DLCData = dlcList;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> SalesOrder()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("SO");
                if (responce is JsonResult jsonResult && jsonResult.Value is List<Dictionary<string, object>> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        ViewBag.BtnAuth = dataList;
                    }
                }
                ViewBag.DLCData = null;
                // Get DLC (Layout Configuration) data
                var dlcResponse = await GETLAYCOFIG("SO");
                if (dlcResponse is JsonResult dlcJson && dlcJson.Value is List<Dictionary<string, object>> dlcList)
                {
                    if (dlcList.Count > 0)
                    {
                        ViewBag.DLCData = dlcList;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> SalesInvoice()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("SI");
                if (responce is JsonResult jsonResult && jsonResult.Value is List<Dictionary<string, object>> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        ViewBag.BtnAuth = dataList;
                    }
                }
                ViewBag.DLCData = null;
                // Get DLC (Layout Configuration) data
                var dlcResponse = await GETLAYCOFIG("SI");
                if (dlcResponse is JsonResult dlcJson && dlcJson.Value is List<Dictionary<string, object>> dlcList)
                {
                    if (dlcList.Count > 0)
                    {
                        ViewBag.DLCData = dlcList;
                    }
                }
                return View();

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> SalesTaxInvoice()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("STI");
                if (responce is JsonResult jsonResult && jsonResult.Value is List<Dictionary<string, object>> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        ViewBag.BtnAuth = dataList;
                    }
                }
                ViewBag.DLCData = null;
                // Get DLC (Layout Configuration) data
                var dlcResponse = await GETLAYCOFIG("STI");
                if (dlcResponse is JsonResult dlcJson && dlcJson.Value is List<Dictionary<string, object>> dlcList)
                {
                    if (dlcList.Count > 0)
                    {
                        ViewBag.DLCData = dlcList;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> DeliveryNote()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("SD");
                if (responce is JsonResult jsonResult && jsonResult.Value is List<Dictionary<string, object>> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        ViewBag.BtnAuth = dataList;
                    }
                }
                ViewBag.DLCData = null;
                // Get DLC (Layout Configuration) data
                var dlcResponse = await GETLAYCOFIG("SD");
                if (dlcResponse is JsonResult dlcJson && dlcJson.Value is List<Dictionary<string, object>> dlcList)
                {
                    if (dlcList.Count > 0)
                    {
                        ViewBag.DLCData = dlcList;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> SalesReturn()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("SR");
                if (responce is JsonResult jsonResult && jsonResult.Value is List<Dictionary<string, object>> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        ViewBag.BtnAuth = dataList;
                    }
                }
                ViewBag.DLCData = null;
                // Get DLC (Layout Configuration) data
                var dlcResponse = await GETLAYCOFIG("SR");
                if (dlcResponse is JsonResult dlcJson && dlcJson.Value is List<Dictionary<string, object>> dlcList)
                {
                    if (dlcList.Count > 0)
                    {
                        ViewBag.DLCData = dlcList;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> CreditNote()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("SCN");
                if (responce is JsonResult jsonResult && jsonResult.Value is List<Dictionary<string, object>> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        ViewBag.BtnAuth = dataList;
                    }
                }
                ViewBag.DLCData = null;
                // Get DLC (Layout Configuration) data
                var dlcResponse = await GETLAYCOFIG("SCN");
                if (dlcResponse is JsonResult dlcJson && dlcJson.Value is List<Dictionary<string, object>> dlcList)
                {
                    if (dlcList.Count > 0)
                    {
                        ViewBag.DLCData = dlcList;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        private async Task<List<Dictionary<string, string>>> GetNextDocNumberAsync(string id, string docType)
        {
            var list = new List<Dictionary<string, string>>();
            var connStr = _configuration.GetConnectionString("ErpConnection");

            try
            {
                await using var con = new SqlConnection(connStr);
                string query = "EXEC GET_NextDocNumber @Id, @DocType";

                await using var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@DocType", docType);

                await con.OpenAsync();
                await using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var obj = new Dictionary<string, string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        obj[reader.GetName(i)] = reader.GetValue(i).ToString();
                    }
                    list.Add(obj);
                }
            }
            catch
            {
                throw; // Let the controller handle the error
            }

            return list;
        }
        public async Task<IActionResult> GETSALEQUOTATIONDOCNUM(string id)
        {
            try
            {
                var result = await GetNextDocNumberAsync(id, "Sale_SaleQuotation_Head");
                return Json(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETSALERETURNDOCNUM(string id)
        {
            try
            {
                var result = await GetNextDocNumberAsync(id, "Sale_SaleReturn_Head");
                return Json(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETSALEORDERDOCNUM(string id)
        {
            try
            {
                var result = await GetNextDocNumberAsync(id, "Sale_SaleOrder_Head");
                return Json(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETSALEDELIVERYDOCNUM(string id)
        {
            try
            {
                var result = await GetNextDocNumberAsync(id, "Sale_SaleDelivery_Head");
                return Json(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETSALEINVOICEDOCNUM(string id)
        {
            try
            {
                var result = await GetNextDocNumberAsync(id, "Sale_SaleInvoice_Head");
                return Json(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETSALETAXINVOICEDOCNUM(string id)
        {
            try
            {
                var result = await GetNextDocNumberAsync(id, "Sale_SalesTaxInvoice_Head");
                return Json(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETSALECREADITENOTEDOCNUM(string id)
        {
            try
            {
                var result = await GetNextDocNumberAsync(id, "Sale_SaleCreditNote_Head");
                return Json(result);
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
            var payment = new List<Dictionary<string, string>>();

            var newobj = new
            {
                header,
                row,
                payment,
                freight
            };

            try
            {
                await using var con = new SqlConnection(connStr);
                await con.OpenAsync();

                // FindDocHeader
                string query = @$"EXEC [FindDocHeader] '{doctype}','{docEntry}','{docid}','{refno}','{fromdate}','{todate}','',''";
                await using (var cmd = new SqlCommand(query, con))
                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var obj = new Dictionary<string, string>();
                        for (int i = 0; i < reader.FieldCount; i++)
                            obj[reader.GetName(i)] = reader.GetValue(i).ToString();
                        header.Add(obj);
                    }
                }

                // Payments based on doc type
                if (!string.IsNullOrWhiteSpace(docEntry))
                {
                    if (doctype == "STI")
                    {
                        string query1 = @$" SELECT * FROM [Accounts_Receipt_Head] T2 INNER JOIN [Accounts_Receipt_Row] T1 
                                    ON T1.DocEntry = T2.DocEntry AND T1.BaseDocEntry = '{docEntry}' AND T1.BaseObjType = '{doctype}';";
                        await using (var cmd = new SqlCommand(query1, con))
                        await using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var obj = new Dictionary<string, string>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                    obj[reader.GetName(i)] = reader.GetValue(i).ToString();
                                payment.Add(obj);
                            }
                        }
                    }
                    else if (doctype == "SR")
                    {
                        string query1 = @$" SELECT * FROM [Accounts_Payment_Head] T2 INNER JOIN [Accounts_Payment_Row] T1 
                                    ON T1.DocEntry = T2.DocEntry AND T1.BaseDocEntry = '{docEntry}' AND T1.BaseObjType = '{doctype}';";
                        await using (var cmd = new SqlCommand(query1, con))
                        await using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var obj = new Dictionary<string, string>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                    obj[reader.GetName(i)] = reader.GetValue(i).ToString();
                                payment.Add(obj);
                            }
                        }
                    }
                    else if (doctype == "SCN")
                    {
                        string query1 = @$" SELECT * FROM [Accounts_Payment_Head] T2 INNER JOIN [Accounts_Payment_Row] T1 
                        ON T1.DocEntry = T2.DocEntry AND T1.BaseDocEntry = '{docEntry}' AND T1.BaseObjType = '{doctype}';";
                        await using (var cmd = new SqlCommand(query1, con))
                        await using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var obj = new Dictionary<string, string>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                    obj[reader.GetName(i)] = reader.GetValue(i).ToString();
                                payment.Add(obj);
                            }
                        }
                    }
                }

                // Rows
                if (!string.IsNullOrWhiteSpace(docEntry))
                {
                    string query1 = @$"EXEC [FindDocRow] '{doctype}','{docEntry}'";
                    await using (var cmd = new SqlCommand(query1, con))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var obj = new Dictionary<string, string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                                obj[reader.GetName(i)] = reader.GetValue(i).ToString();
                            row.Add(obj);
                        }
                    }
                }

                // Freight
                if (!string.IsNullOrWhiteSpace(docEntry))
                {
                    string query1 = @$"EXEC [FindDocFreight] '{doctype}','{docEntry}'";
                    await using (var cmd = new SqlCommand(query1, con))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var obj = new Dictionary<string, string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                                obj[reader.GetName(i)] = reader.GetValue(i).ToString();
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
            if (status == null)
                status = "";

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

                // Document_CopyToHeader
                string query = @$"EXEC [Document_CopyToHeader] '{doctype}','{docEntry}','{docid}','{refno}','{fromdate}','{todate}','{HttpContext.Session.GetString("UserID")}','','{ledgercode}','{status}'";
                await using (var cmd = new SqlCommand(query, con))
                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var obj = new Dictionary<string, string>();
                        for (int i = 0; i < reader.FieldCount; i++)
                            obj[reader.GetName(i)] = reader.GetValue(i).ToString();
                        header.Add(obj);
                    }
                }

                // Document_CopyToRow
                if (!string.IsNullOrWhiteSpace(docEntry))
                {
                    string query1 = @$"EXEC [Document_CopyToRow] '{doctype}','{docEntry}','O'";
                    await using (var cmd = new SqlCommand(query1, con))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var obj = new Dictionary<string, string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                                obj[reader.GetName(i)] = reader.GetValue(i).ToString();
                            row.Add(obj);
                        }
                    }
                }

                // FindDocFreight
                if (!string.IsNullOrWhiteSpace(docEntry))
                {
                    string query1 = @$"EXEC [FindDocFreight] '{doctype}','{docEntry}'";
                    await using (var cmd = new SqlCommand(query1, con))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var obj = new Dictionary<string, string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                                obj[reader.GetName(i)] = reader.GetValue(i).ToString();
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
        public async Task<string> GETBILLTOSTATECODE(string id)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");

                await using var con = new SqlConnection(connectionString);
                string query = @"
                    SELECT T3.GstCode
                    FROM LedgerGroup T1
                    INNER JOIN LedgerAddress T2 ON T2.LedgerID = T1.ID
                    INNER JOIN StateMaster T3 ON T2.Stat_Id = T3.Stat_Id
                    WHERE T2.AdresType = 'B' AND T1.ID= @Id";

                await using var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);

                await con.OpenAsync();

                await using var reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    // Use the column alias or name directly (no table prefix needed)
                    return reader["GstCode"]?.ToString() ?? string.Empty;
                }

                // If no record is found, return null or empty string
                return string.Empty;
            }
            catch
            {
                throw; // Let the controller handle the error
            }
        }
        public async Task<string> GSTCOMPSTATECODE()
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("ErpConnection");

                await using var con = new SqlConnection(connectionString);
                const string query = @"
                       SELECT TOP 1 T1.GstCode
                       FROM CompanyMaster T0
                       INNER JOIN StateMaster T1 ON T0.Stat_Id = T1.Stat_Id";

                await using var cmd = new SqlCommand(query, con);
                await con.OpenAsync();
                var result = await cmd.ExecuteScalarAsync();

                return result?.ToString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #region SALES QUOTATION

        public async Task<IActionResult> CREATESALESQUOT([FromBody] SALESQUOT data, string flag, string doctype)
        {
            if (flag == "P")
            {
                return await PARK(data, doctype);
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(connectionString);
                await con.OpenAsync();
                transaction = con.BeginTransaction();
         


                var compstateid = ""; 
                string query1 = "Select Stat_Id FROM CompanyMaster";
                using (SqlCommand cmd = new SqlCommand(query1, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();
                    cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                    compstateid = (await cmd.ExecuteScalarAsync()).ToString();
                }

                var bpstateid = "";
                string query2 = "Select Stat_Id FROM CompanyMaster";
                using (SqlCommand cmd = new SqlCommand(query1, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();
                    cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                    compstateid = (await cmd.ExecuteScalarAsync()).ToString();
                }

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

                    using (var cmd = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Sale_SaleQuotation_Head'", con, transaction))
                    {
                        await using var reader = await cmd.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            header.Docnum = reader["Message"].ToString();
                        }
                    }

                    query = generator.GenerateInsertQuery(header, " [Sale_SaleQuotation_Head]", "DocEntry");
                }
                else
                {
                    query = generator.GenerateUpdateQuery(header, "[Sale_SaleQuotation_Head]", "DocEntry", header.DocEntry, "");
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


                string billtostatecode = await GETBILLTOSTATECODE(header.LedgerID);
                string companystatecode = await GSTCOMPSTATECODE();
                // Process Lines
                if (data.lines != null && data.lines.Length > 0)
                {
                    using (SqlCommand cmd = new SqlCommand($"Delete from [Sale_SaleQuotation_Row] where DocEntry = '{header.DocEntry}'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }

                    int lineNumber = 1;
                    foreach (var line in data.lines)
                    {
                        if (!string.IsNullOrWhiteSpace(line.TaxCode))
                        {
                            if (billtostatecode == companystatecode)
                            {
                                // For state code 27: only GST@ is valid
                                if (!line.TaxCode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{line.TaxCode}' for Item Code {line.ItemCode} (Line No: {lineNumber}). Only GST is allowed. ");
                                }
                            }
                            else
                            {
                                // For all other state codes: GST@ not allowed, IGST@ or UTGST@ is fine
                                if (line.TaxCode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{line.TaxCode}' for Item Code {line.ItemCode} (Line No: {lineNumber}). Only IGST or UTGST Allowed.");
                                }
                            }
                        }
                        else
                        {
                            if (transaction != null) await transaction.RollbackAsync();
                            return StatusCode(500,
                                $"Missing Tax Code for Item Code {line.ItemCode} (Line No: {lineNumber}).");
                        }



                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = header.DocEntry;
                        line.LineNum = lineNumber;
                        line.CreatedDate = DateTime.Now;
                        line.CretedByUId = HttpContext.Session.GetString("UserID");
                        line.CretedByUName = HttpContext.Session.GetString("UserName");
                        line.ID = null;

                        string lineQuery = generator.GenerateInsertQuery(line, "[Sale_SaleQuotation_Row]", "ID");
                        using (SqlCommand cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }
                        lineNumber++;
                    }
                }

                // Process Freight
                if (data.freight != null && data.freight.Length > 0 && data.header.FreightTotal > 0)
                {
                    using (SqlCommand cmd = new SqlCommand($"Delete from [Sale_SaleQuotation_Freight] where DocEntry = '{header.DocEntry}'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }


                    foreach (var line in data.freight)
                    {
                        string taxcode = "";
                        using (var cmd = new SqlCommand(@$"select TaxCode from TaxCode_Mst where TaxCodeId = '{line.TaxCodeId}'", con, transaction))
                        {
                            await using var reader = await cmd.ExecuteReaderAsync();
                            while (await reader.ReadAsync())
                            {
                                taxcode = reader["TaxCode"].ToString();
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(taxcode))
                        {
                            if (billtostatecode == companystatecode)
                            {
                                // For state code 27: only GST@ is valid
                                if (!taxcode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{taxcode}' for  {line.Name}  Only GST is allowed. ");
                                }
                            }
                            else
                            {
                                // For all other state codes: GST@ not allowed, IGST@ or UTGST@ is fine
                                if (taxcode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{taxcode}' for  {line.Name}  Only IGST or UTGST Allowed.");
                                }
                            }
                        }
                        else
                        {
                            if (transaction != null) await transaction.RollbackAsync();
                            return StatusCode(500,
                                $"Missing Tax Code for  {line.Name} .");
                        }
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = header.DocEntry;
                        line.Name = null;
                        line.CreatedDate = DateTime.Now;
                        line.CretedByUId = HttpContext.Session.GetString("UserID");
                        line.CretedByUName = HttpContext.Session.GetString("UserName");
                        line.ID = null;

                        string lineQuery = generator.GenerateInsertQuery(line, "[Sale_SaleQuotation_Freight]", "ID");
                        using (SqlCommand cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                }

                await transaction.CommitAsync();
                return Json(new { Success = true, DocEntry = header.DocEntry, Message = "Document saved successfully." });
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
                transaction?.Dispose();
                if (con != null) await con.DisposeAsync();
            }
        }
        public async Task<IActionResult> DELETESALECHQUOT(string id)
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

                int validation = 0;
                int DocNum = 0;
                string message = "This Document Has Reference ";

                string[] tables = new[]
                {
                    "Sale_SaleOrder_Row,Sale_SaleOrder_Head,SQ,Sales Order",
                    "Sale_SaleDelivery_Row,Sale_SaleDelivery_Head,SQ,Sales Delivery Note",
                    "Sale_SaleInvoice_Row,Sale_SaleInvoice_Head,SQ,Sales Invoice"
                };

                foreach (var t in tables)
                {
                    var parts = t.Split(',');
                    string query = $@"exec [Document_CheckBaseDocEntry] '{parts[0]}','{parts[1]}','{id}','{parts[2]}'";

                    await using (var cmd = new SqlCommand(query, con))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            validation = Convert.ToInt16(reader["RESULT"]);
                            DocNum = Convert.ToInt16(reader["Docnum"]);
                            if (validation > 0)
                            {
                                message += $" In {parts[3]}, Document No {DocNum} !";
                                return Json(new { success = false, message });
                            }
                        }
                    }
                }

                string deleteQuery = @"
                    DELETE from [Sale_SaleQuotation_Freight] where DocEntry = @id;
                    DELETE FROM [Sale_SaleQuotation_Row] WHERE DocEntry = @id;
                    DELETE FROM [Sale_SaleQuotation_Head] WHERE DocEntry = @id;
                ";
                await using (var cmd = new SqlCommand(deleteQuery, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();
                }

                return Json(new { success = true, message = "Document Deleted Successfully!" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
            }
        }
        #endregion
        #region SALES ORDER
        public async Task<IActionResult> CREATESALESORDER([FromBody] SALESORDER data, string flag, string doctype)
        {
            if (flag == "P")
            {
                return await PARK(data, doctype);
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(connectionString);
                await con.OpenAsync();
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

                    using (var cmd = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Sale_SaleOrder_Head'", con, transaction))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            header.Docnum = reader["Message"].ToString();
                        }
                    }

                    query = generator.GenerateInsertQuery(header, " [Sale_SaleOrder_Head]", "DocEntry");
                }
                else
                {
                    query = generator.GenerateUpdateQuery(header, "[Sale_SaleOrder_Head]", "DocEntry", header.DocEntry, "");
                }

                using (var cmd = new SqlCommand(query, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();

                    if (string.IsNullOrEmpty(header.DocEntry))
                    {
                        cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                        header.DocEntry = (await cmd.ExecuteScalarAsync()).ToString();
                    }
                }
                string billtostatecode = await GETBILLTOSTATECODE(header.LedgerID);
                string companystatecode = await GSTCOMPSTATECODE();
                // Process Lines
                if (data.lines != null && data.lines.Length > 0)
                {
                    using (var cmd = new SqlCommand($"Delete from [Sale_SaleOrder_Row] where DocEntry = '{header.DocEntry}'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }
                  
                    int lineNumber = 1;
                    foreach (var line in data.lines)
                    {

                        if (!string.IsNullOrWhiteSpace(line.TaxCode))
                        {
                            if (billtostatecode == companystatecode)
                            {
                                // For state code 27: only GST@ is valid
                                if (!line.TaxCode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{line.TaxCode}' for Item Code {line.ItemCode} (Line No: {lineNumber}). Only GST is allowed. ");
                                }
                            }
                            else
                            {
                                // For all other state codes: GST@ not allowed, IGST@ or UTGST@ is fine
                                if (line.TaxCode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{line.TaxCode}' for Item Code {line.ItemCode} (Line No: {lineNumber}). Only IGST or UTGST Allowed.");
                                }
                            }
                        }
                        else
                        {
                            if (transaction != null) await transaction.RollbackAsync();
                            return StatusCode(500,
                                $"Missing Tax Code for Item Code {line.ItemCode} (Line No: {lineNumber}).");
                        }
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = header.DocEntry;
                        line.LineNum =lineNumber;

                        line.CreatedDate = DateTime.Now;
                        line.CretedByUId = HttpContext.Session.GetString("UserID");
                        line.CretedByUName = HttpContext.Session.GetString("UserName");
                        line.ID = null;

                        string lineQuery = generator.GenerateInsertQuery(line, "[Sale_SaleOrder_Row]", "ID");
                        using (var cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }
                        lineNumber++;
                    }
                }

                // Process Freight
                if (data.freight != null && data.freight.Length > 0 && data.header.FreightTotal > 0)
                {
                    using (var cmd = new SqlCommand($"Delete from [Sale_SaleOrder_Freight] where DocEntry = '{header.DocEntry}'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }

                    foreach (var line in data.freight)
                    {
                        string taxcode = "";
                        using (var cmd = new SqlCommand(@$"select TaxCode from TaxCode_Mst where TaxCodeId = '{line.TaxCodeId}'", con, transaction))
                        {
                            await using var reader = await cmd.ExecuteReaderAsync();
                            while (await reader.ReadAsync())
                            {
                                taxcode = reader["TaxCode"].ToString();
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(taxcode))
                        {
                            if (billtostatecode == companystatecode)
                            {
                                // For state code 27: only GST@ is valid
                                if (!taxcode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{taxcode}' for  {line.Name}  Only GST is allowed. ");
                                }
                            }
                            else
                            {
                                // For all other state codes: GST@ not allowed, IGST@ or UTGST@ is fine
                                if (taxcode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{taxcode}' for  {line.Name}  Only IGST or UTGST Allowed.");
                                }
                            }
                        }
                        else
                        {
                            if (transaction != null) await transaction.RollbackAsync();
                            return StatusCode(500,
                                $"Missing Tax Code for  {line.Name} .");
                        }
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = header.DocEntry;
                        line.Name = null;

                        line.CreatedDate = DateTime.Now;
                        line.CretedByUId = HttpContext.Session.GetString("UserID");
                        line.CretedByUName = HttpContext.Session.GetString("UserName");
                        line.ID = null;

                        string lineQuery = generator.GenerateInsertQuery(line, "[Sale_SaleOrder_Freight]", "ID");
                        using (var cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                }

                string Query = @$"EXEC UpdateOpenQuantity 'Sale_SaleQuotation_Row','Sale_SaleQuotation_Head','Sale_SaleOrder_Head','Sale_SaleOrder_Row','{header.DocEntry}'";
                using (var cmd = new SqlCommand(Query, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();
                }

                await transaction.CommitAsync();
                return Json(new { Success = true, DocEntry = header.DocEntry, Message = "Document saved successfully." });
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
                transaction?.Dispose();
                if (con != null) await con.DisposeAsync();
            }
        }

        public async Task<IActionResult> DELETESALECHORDER(string id)
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

                int validation = 0;
                int DocNum = 0;
                string message = "This Document Has Reference ";

                string[] tables = new[]
                {
                    "Sale_SaleDelivery_Row,Sale_SaleDelivery_Head,SO,Sales Delivery Note",
                    "Sale_SaleInvoice_Row,Sale_SaleInvoice_Head,SO,Sales Invoice"
                };

                foreach (var t in tables)
                {
                    var parts = t.Split(',');
                    string query = $@"exec [Document_CheckBaseDocEntry] '{parts[0]}','{parts[1]}','{id}','{parts[2]}'";

                    await using (var cmd = new SqlCommand(query, con))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            validation = Convert.ToInt16(reader["RESULT"]);
                            DocNum = Convert.ToInt16(reader["Docnum"]);
                            if (validation > 0)
                            {
                                message += $" In {parts[3]}, Document No {DocNum} !";
                                return Json(new { success = false, message });
                            }
                        }
                    }
                }

                string queryDelete = @$"EXEC [Delete_SalesOrder] {id}";
                await using (var cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.CommandTimeout = 300;
                    cmd.Parameters.AddWithValue("@id", id);
                    await cmd.ExecuteNonQueryAsync();
                }

                return Json(new { success = true, message = "Document Deleted Successfully!" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
            }
        }

        #endregion
        #region SALES DELIVERY
        public async Task<IActionResult> CREATESALESDELIVERY([FromBody] SALESDELIVERY data, string flag, string doctype)
        {
            if (flag == "P")
            {
                return await PARK(data, doctype);
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(connectionString);
                await con.OpenAsync();
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

                    using var cmdGet = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Sale_SaleDelivery_Head'", con, transaction);
                    await using var reader = await cmdGet.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        header.Docnum = reader["Message"].ToString();
                    }

                    query = generator.GenerateInsertQuery(header, " [Sale_SaleDelivery_Head]", "DocEntry");
                }
                else
                {
                    query = generator.GenerateUpdateQuery(header, "[Sale_SaleDelivery_Head]", "DocEntry", header.DocEntry, "");
                }

                using (var cmd = new SqlCommand(query, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();

                    if (string.IsNullOrEmpty(header.DocEntry))
                    {
                        cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                        header.DocEntry = (await cmd.ExecuteScalarAsync()).ToString();
                    }
                }
                string billtostatecode = await GETBILLTOSTATECODE(header.LedgerID);
                string companystatecode = await GSTCOMPSTATECODE();
                // Process Lines
                if (data.lines != null && data.lines.Length > 0)
                {
                    using (var cmd = new SqlCommand($@"
                             DELETE FROM Inventory_Stock where ObjType ='SD' AND DocEntry ='{header.DocEntry}'
                             Delete from [Sale_SaleDelivery_Row] where DocEntry = '{header.DocEntry}'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }


                    int lineNumber = 1;
                    foreach (var line in data.lines)
                    {
                        if (!string.IsNullOrWhiteSpace(line.TaxCode))
                        {
                            if (billtostatecode == companystatecode)
                            {
                                // For state code 27: only GST@ is valid
                                if (!line.TaxCode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{line.TaxCode}' for Item Code {line.ItemCode} (Line No: {lineNumber}). Only GST is allowed. ");
                                }
                            }
                            else
                            {
                                // For all other state codes: GST@ not allowed, IGST@ or UTGST@ is fine
                                if (line.TaxCode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{line.TaxCode}' for Item Code {line.ItemCode} (Line No: {lineNumber}). Only IGST or UTGST Allowed.");
                                }
                            }
                        }
                        else
                        {
                            if (transaction != null) await transaction.RollbackAsync();
                            return StatusCode(500,
                                $"Missing Tax Code for Item Code {line.ItemCode} (Line No: {lineNumber}).");
                        }
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = header.DocEntry;
                        line.LineNum = lineNumber;
                        line.CreatedDate = DateTime.Now;
                        line.CretedByUId = HttpContext.Session.GetString("UserID");
                        line.CretedByUName = HttpContext.Session.GetString("UserName");
                        line.ID = null;

                        string lineQuery = generator.GenerateInsertQuery(line, "[Sale_SaleDelivery_Row]", "ID");
                        using (var cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }
                        lineNumber++;
                    }
                }

                // Process Freight
                if (data.freight != null && data.freight.Length > 0 && data.header.FreightTotal > 0)
                {
                    using (var cmd = new SqlCommand($@"
                           
                            Delete from [Sale_SaleDelivery_Freight] where DocEntry = '{header.DocEntry}'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }

                    foreach (var line in data.freight)
                    {
                        string taxcode = "";
                        using (var cmd = new SqlCommand(@$"select TaxCode from TaxCode_Mst where TaxCodeId = '{line.TaxCodeId}'", con, transaction))
                        {
                            await using var reader = await cmd.ExecuteReaderAsync();
                            while (await reader.ReadAsync())
                            {
                                taxcode = reader["TaxCode"].ToString();
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(taxcode))
                        {
                            if (billtostatecode == companystatecode)
                            {
                                // For state code 27: only GST@ is valid
                                if (!taxcode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{taxcode}' for  {line.Name}  Only GST is allowed. ");
                                }
                            }
                            else
                            {
                                // For all other state codes: GST@ not allowed, IGST@ or UTGST@ is fine
                                if (taxcode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{taxcode}' for  {line.Name}  Only IGST or UTGST Allowed.");
                                }
                            }
                        }
                        else
                        {
                            if (transaction != null) await transaction.RollbackAsync();
                            return StatusCode(500,
                                $"Missing Tax Code for  {line.Name} .");
                        }
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = header.DocEntry;
                        line.Name = null;
                        line.CreatedDate = DateTime.Now;
                        line.CretedByUId = HttpContext.Session.GetString("UserID");
                        line.CretedByUName = HttpContext.Session.GetString("UserName");
                        line.ID = null;

                        string lineQuery = generator.GenerateInsertQuery(line, "[Sale_SaleDelivery_Freight]", "ID");
                        using (var cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                }

                string query1 = @$"EXEC UpdateOpenQuantity 'Sale_SaleQuotation_Row','Sale_SaleQuotation_Head','Sale_SaleDelivery_Head','Sale_SaleDelivery_Row','{header.DocEntry}'";
                using (var cmd = new SqlCommand(query1, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();
                }

                string query2 = @$"EXEC UpdateOpenQuantity 'Sale_SaleOrder_Row','Sale_SaleOrder_Head','Sale_SaleDelivery_Head','Sale_SaleDelivery_Row','{header.DocEntry}'";
                using (var cmd = new SqlCommand(query2, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();
                }
                // Delivery Stock
                await using (var cmdStock = new SqlCommand($"EXEC [Insert_InvtoryStock] '{header.DocEntry}','SD'", con, transaction))
                await using (var rdr = await cmdStock.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        string success = rdr["success"].ToString();
                        string message = rdr["message"].ToString();
                        if (!string.Equals(success, "true", StringComparison.OrdinalIgnoreCase))
                        {
                            await transaction.RollbackAsync();
                            return StatusCode(500, $"An error occurred: {message}");
                        }
                    }
                }
                await transaction.CommitAsync();
                return Json(new { Success = true, DocEntry = header.DocEntry, Message = "Document saved successfully." });
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
                transaction?.Dispose();
                if (con != null) await con.DisposeAsync();
            }
        }

        public async Task<IActionResult> DELETESALECHDELIVERY(string id)
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

                int validation = 0;
                int DocNum = 0;
                string message = "This Document Has Reference ";

                var checks = new[]
                {
            new { RowTable = "Sale_SaleInvoice_Row", HeadTable = "Sale_SaleInvoice_Head", Type = "SD", Desc = "Sales Invoice" },
            new { RowTable = "Sale_SaleReturn_Row", HeadTable = "Sale_SaleReturn_Head", Type = "SD", Desc = "Sales Return" }
        };

                foreach (var check in checks)
                {
                    string query = $@"exec [Document_CheckBaseDocEntry] '{check.RowTable}','{check.HeadTable}','{id}','{check.Type}'";
                    await using var cmd = new SqlCommand(query, con);
                    await using var reader = await cmd.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        validation = Convert.ToInt16(reader["RESULT"]);
                        DocNum = Convert.ToInt16(reader["Docnum"]);
                        if (validation > 0)
                        {
                            message += $" In {check.Desc}, Document No {DocNum} !";
                            return Json(new { success = false, message });
                        }
                    }
                }

                string deleteQuery = @$"EXEC [Delete_DeleveryNote] {id}";
                await using (var cmd = new SqlCommand(deleteQuery, con))
                {
                    cmd.CommandTimeout = 300;
                    cmd.Parameters.AddWithValue("@id", id);
                    await cmd.ExecuteNonQueryAsync();
                }

                return Json(new { success = true, message = "Document Deleted Successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion
        #region SALES INVOICE
          public async Task<IActionResult> CREATESALESINVOICE([FromBody] SALESINVOICE data, string flag, string doctype)
          {
            if (flag == "P")
            {
                return await PARK(data, doctype);
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(connectionString);
                await con.OpenAsync();
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

                    await using (var cmd = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Sale_SaleInvoice_Head'", con, transaction))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            header.Docnum = reader["Message"].ToString();
                        }
                    }

                    query = generator.GenerateInsertQuery(header, " [Sale_SaleInvoice_Head]", "DocEntry");
                }
                else
                {
                    query = generator.GenerateUpdateQuery(header, "[Sale_SaleInvoice_Head]", "DocEntry", header.DocEntry, "");
                }

                using (var cmd = new SqlCommand(query, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();

                    if (string.IsNullOrEmpty(header.DocEntry))
                    {
                        cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                        header.DocEntry = (await cmd.ExecuteScalarAsync()).ToString();
                    }
                }
                string billtostatecode = await GETBILLTOSTATECODE(header.LedgerID);
                string companystatecode = await GSTCOMPSTATECODE();
                // Process Lines
                if (data.lines != null && data.lines.Length > 0)
                {
                    using (var cmd = new SqlCommand($"Delete from [Sale_SaleInvoice_Row] where DocEntry = '{header.DocEntry}'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }
                  
                    int linenum = 1;
                    foreach (var line in data.lines)
                    {
                        if (!string.IsNullOrWhiteSpace(line.TaxCode))
                        {
                            if (billtostatecode == companystatecode)
                            {
                                // For state code 27: only GST@ is valid
                                if (!line.TaxCode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{line.TaxCode}' for Item Code {line.ItemCode} (Line No: {linenum}). Only GST is allowed  ");
                                }
                            }
                            else
                            {
                                // For all other state codes: GST@ not allowed, IGST@ or UTGST@ is fine
                                if (line.TaxCode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{line.TaxCode}' for Item Code {line.ItemCode} (Line No: {linenum}). Only IGST or UTGST Allowed.");
                                }
                            }
                        }
                        else
                        {
                            if (transaction != null) await transaction.RollbackAsync();
                            return StatusCode(500,
                                $"Missing Tax Code for Item Code {line.ItemCode} (Line No: {linenum}).");
                        }
                        line.LineNum = linenum;
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = header.DocEntry;
                        line.CreatedDate = DateTime.Now;
                        line.CretedByUId = HttpContext.Session.GetString("UserID");
                        line.CretedByUName = HttpContext.Session.GetString("UserName");
                        line.ID = null;

                        string lineQuery = generator.GenerateInsertQuery(line, "[Sale_SaleInvoice_Row]", "ID");
                        using (var cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }
                        linenum++;
                    }
                }

                // Process Freight
                if (data.freight != null && data.freight.Length > 0 && data.header.FreightTotal > 0)
                {
                    using (var cmd = new SqlCommand($"Delete from [Sale_SaleInvoice_Freight] where DocEntry = '{header.DocEntry}'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }

                    foreach (var line in data.freight)
                    {
                        string taxcode = "";
                        using (var cmd = new SqlCommand(@$"select TaxCode from TaxCode_Mst where TaxCodeId = '{line.TaxCodeId}'", con, transaction))
                        {
                            await using var reader = await cmd.ExecuteReaderAsync();
                            while (await reader.ReadAsync())
                            {
                                taxcode = reader["TaxCode"].ToString();
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(taxcode))
                        {
                            if (billtostatecode == companystatecode)
                            {
                                // For state code 27: only GST@ is valid
                                if (!taxcode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{taxcode}' for  {line.Name}  Only GST is allowed. ");
                                }
                            }
                            else
                            {
                                // For all other state codes: GST@ not allowed, IGST@ or UTGST@ is fine
                                if (taxcode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{taxcode}' for  {line.Name}  Only IGST or UTGST Allowed.");
                                }
                            }
                        }
                        else
                        {
                            if (transaction != null) await transaction.RollbackAsync();
                            return StatusCode(500,
                                $"Missing Tax Code for  {line.Name} .");
                        }
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = header.DocEntry;
                        line.Name = null;
                        line.CreatedDate = DateTime.Now;
                        line.CretedByUId = HttpContext.Session.GetString("UserID");
                        line.CretedByUName = HttpContext.Session.GetString("UserName");
                        line.ID = null;

                        string lineQuery = generator.GenerateInsertQuery(line, "[Sale_SaleInvoice_Freight]", "ID");
                        using (var cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                }

                // Update open quantities
                string[] updateQueries =
                {
                    @$"EXEC UpdateOpenQuantity 'Sale_SaleQuotation_Row','Sale_SaleQuotation_Head','Sale_SaleInvoice_Head','Sale_SaleInvoice_Row','{header.DocEntry}'",
                    @$"EXEC UpdateOpenQuantity 'Sale_SaleOrder_Row','Sale_SaleOrder_Head','Sale_SaleInvoice_Head','Sale_SaleInvoice_Row','{header.DocEntry}'",
                    @$"EXEC UpdateOpenQuantity 'Sale_SaleDelivery_Row','Sale_SaleDelivery_Head','Sale_SaleInvoice_Head','Sale_SaleInvoice_Row','{header.DocEntry}'"
                };

                foreach (var uq in updateQueries)
                {
                    using var cmd = new SqlCommand(uq, con, transaction);
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();
                }

                // Insert Inventory Stock
                await using (var cmd = new SqlCommand($"EXEC [Insert_InvtoryStock] '{header.DocEntry}','SI'", con, transaction))
                await using (var rdr = await cmd.ExecuteReaderAsync())
                {
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

                await transaction.CommitAsync();
                return Json(new { Success = true, DocEntry = header.DocEntry, Message = "Document saved successfully." });
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
                transaction?.Dispose();
                if (con != null) await con.DisposeAsync();
            }
        }
          public async Task<IActionResult> DELETESALECHINVOICE(string id)
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

                string query = @"
                    DELETE from [Sale_SaleInvoice_Freight] where DocEntry = @id;
                    DELETE FROM [Sale_SaleInvoice_Row] WHERE DocEntry = @id;
                    DELETE FROM [Sale_SaleInvoice_Head] WHERE DocEntry = @id;
                ";

                await using (var cmd = new SqlCommand(query, con))
                {
                    cmd.CommandTimeout = 300;
                    cmd.Parameters.AddWithValue("@id", id);
                    await cmd.ExecuteNonQueryAsync();
                }

                return Json(new { success = true, message = "Document Deleted Successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        #endregion
        #region SALES TAX INVOICE
        public async Task<IActionResult> CREATESALESTAXINVOICE([FromBody] SALESTAXINVOICE data, string flag, string doctype)
        {
            if (flag == "P")
            {
                return await PARK(data, doctype);
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(connectionString);
                await con.OpenAsync();
                transaction = con.BeginTransaction();

                var header = data.header;
                var receipt = data.payment;
                header.AppliedAmount = 0;
                header.BalanceDue = header.NETTotal;
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

                    await using (var cmd = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Sale_SalesTaxInvoice_Head'", con, transaction))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            header.Docnum = reader["Message"].ToString();
                        }
                    }

                    query = generator.GenerateInsertQuery(header, " [Sale_SalesTaxInvoice_Head]", "DocEntry");
                }
                else
                {
                    query = generator.GenerateUpdateQuery(header, "[Sale_SalesTaxInvoice_Head]", "DocEntry", header.DocEntry, "");
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
                string billtostatecode = await GETBILLTOSTATECODE(header.LedgerID);
                string companystatecode = await GSTCOMPSTATECODE();
                // Process Lines
                if (data.lines != null && data.lines.Length > 0)
                {
                    await using (var cmd = new SqlCommand($@"
                            DELETE FROM Inventory_Stock where ObjType ='STI' AND DocEntry ='{header.DocEntry}'
                            DELETE FROM [Sale_SalesTaxInvoice_Row] where DocEntry = '{header.DocEntry}'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }

                    int linenum = 1;
                  
                    foreach (var line in data.lines)
                    {
                        if (!string.IsNullOrWhiteSpace(line.TaxCode))
                        {
                            if (billtostatecode == companystatecode)
                            {
                                // For state code 27: only GST@ is valid
                                if (!line.TaxCode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{line.TaxCode}' for Item Code {line.ItemCode} (Line No: {linenum}). Only GST is allowed. ");
                                }
                            }
                            else
                            {
                                // For all other state codes: GST@ not allowed, IGST@ or UTGST@ is fine
                                if (line.TaxCode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{line.TaxCode}' for Item Code {line.ItemCode} (Line No: {linenum}). Only IGST or UTGST Allowed.");
                                }
                            }
                        }
                        else
                        {
                            if (transaction != null) await transaction.RollbackAsync();
                            return StatusCode(500,
                                $"Missing Tax Code for Item Code {line.ItemCode} (Line No: {linenum}).");
                        }
                        line.LineNum = linenum;
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = header.DocEntry;
                        line.CreatedDate = DateTime.Now;
                        line.CretedByUId = HttpContext.Session.GetString("UserID");
                        line.CretedByUName = HttpContext.Session.GetString("UserName");
                        line.ID = null;

                        string lineQuery = generator.GenerateInsertQuery(line, "[Sale_SalesTaxInvoice_Row]", "ID");
                        await using (var cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }
                        linenum++;
                    }
                }

                // Process Freight
                if (data.freight != null && data.freight.Length > 0 && data.header.FreightTotal > 0)
                {
                    await using (var cmd = new SqlCommand($"Delete from [Sale_SalesTaxInvoice_Freight] where DocEntry = '{header.DocEntry}'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }

                    foreach (var line in data.freight)
                    {
                        string taxcode = "";
                        using (var cmd = new SqlCommand(@$"select TaxCode from TaxCode_Mst where TaxCodeId = '{line.TaxCodeId}'", con, transaction))
                        {
                            await using var reader = await cmd.ExecuteReaderAsync();
                            while (await reader.ReadAsync())
                            {
                                taxcode = reader["TaxCode"].ToString();
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(taxcode))
                        {
                            if (billtostatecode == companystatecode)
                            {
                                // For state code 27: only GST@ is valid
                                if (!taxcode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{taxcode}' for  {line.Name}  Only GST is allowed. ");
                                }
                            }
                            else
                            {
                                // For all other state codes: GST@ not allowed, IGST@ or UTGST@ is fine
                                if (taxcode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{taxcode}' for  {line.Name}  Only IGST or UTGST Allowed.");
                                }
                            }
                        }
                        else
                        {
                            if (transaction != null) await transaction.RollbackAsync();
                            return StatusCode(500,
                                $"Missing Tax Code for  {line.Name} .");
                        }
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = header.DocEntry;
                        line.Name = null;
                        line.CreatedDate = DateTime.Now;
                        line.CretedByUId = HttpContext.Session.GetString("UserID");
                        line.CretedByUName = HttpContext.Session.GetString("UserName");
                        line.ID = null;

                        string lineQuery = generator.GenerateInsertQuery(line, "[Sale_SalesTaxInvoice_Freight]", "ID");
                        await using (var cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                }

                // Update Open Quantity
                string[] updateQueries =
                {
                    @$"EXEC UpdateOpenQuantity 'Sale_SaleQuotation_Row','Sale_SaleQuotation_Head','Sale_SalesTaxInvoice_Head','Sale_SalesTaxInvoice_Row','{header.DocEntry}'",
                    @$"EXEC UpdateOpenQuantity 'Sale_SaleOrder_Row','Sale_SaleOrder_Head','Sale_SalesTaxInvoice_Head','Sale_SalesTaxInvoice_Row','{header.DocEntry}'",
                    @$"EXEC UpdateOpenQuantity 'Sale_SaleDelivery_Row','Sale_SaleDelivery_Head','Sale_SalesTaxInvoice_Head','Sale_SalesTaxInvoice_Row','{header.DocEntry}'"
                };

                foreach (var uq in updateQueries)
                {
                    await using var cmd = new SqlCommand(uq, con, transaction);
                    cmd.CommandTimeout = 300;
                    await cmd.ExecuteNonQueryAsync();
                }

                // Inventory Stock
                await using (var cmdStock = new SqlCommand($"EXEC [Insert_InvtoryStock] '{header.DocEntry}','STI'", con, transaction))
                await using (var rdr = await cmdStock.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        string success = rdr["success"].ToString();
                        string message = rdr["message"].ToString();
                        if (!string.Equals(success, "true", StringComparison.OrdinalIgnoreCase))
                        {
                            await transaction.RollbackAsync();
                            return StatusCode(500, $"An error occurred: {message}");
                        }
                    }
                }

                // Journal Entry
                using (SqlCommand cmd = new SqlCommand(
                   @"SELECT DocEntry 
                      FROM Accounts_JournalEntry_Head 
                      WHERE BaseObjType = 'STI' 
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
                await using (var cmdJournal = new SqlCommand($"EXEC [Create_SalesJournalEntry] '{header.DocEntry}','STI'", con, transaction))
                await using (var rdr = await cmdJournal.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        string success = rdr["success"].ToString();
                        string message = rdr["message"].ToString();
                        if (!string.Equals(success, "true", StringComparison.OrdinalIgnoreCase))
                        {
                            await transaction.RollbackAsync();
                            return StatusCode(500, $"An error occurred: {message}");
                        }
                    }
                }
                // Process Payment if applicable
                if (receipt.ChequeAmt + receipt.CashAmt + receipt.BankAmt>0)
                {
                    string DocNum = "";
                    await using (var cmd = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Accounts_Receipt_Head'", con, transaction))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            DocNum = reader["Message"].ToString();
                        }
                    }
                    await using (var cmd = new SqlCommand(@$"Select DefLedgerId FROm DefaultConfiguration where [Name]='CHEQUEACCOUNT'", con, transaction))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            receipt.ChequeLedgerId = reader["DefLedgerId"].ToString();
                        }
                    }
                  
                    if (string.IsNullOrEmpty(receipt.DocEntry))
                    {
                        if (receipt.ChequeAmt == 0)
                        {
                            receipt.ChequeLedgerId = "";
                        }
                        else if (receipt.CashAmt == 0)
                        {
                            receipt.CashLedgerId = "";
                        }
                        else if (receipt.BankAmt == 0)
                        {
                            receipt.BankLedgerId = "";
                        }
                        receipt.DocStatus = "O";
                        receipt.Docnum = DocNum;
                        receipt.FYearId = header.FYearId;
                        receipt.DocumentDate = header.DocumentDate.ToString();
                        receipt.PostingDate = header.PostingDate.ToString();
                        receipt.DueDate = header.DueDate.ToString();
                        receipt.NETTotal = receipt.ChequeAmt+ receipt.CashAmt+ receipt.BankAmt;
                        receipt.OpenBalance = 0;
                        receipt.PayOnAccAmt = 0;
                        receipt.PayOnAccCheck = "N";
                        receipt.LedgerID = header.LedgerID;
                        receipt.LedgerCode = header.LedgerCode;
                        receipt.LedgerName = header.LedgerName;
                        receipt.LedgerType = "B";
                        receipt.RefNo = header.RefNo;
                        receipt.Remarks = header.Remarks;
                        receipt.LedgerBalance = 0;

                        receipt.CreatedDate = DateTime.Now;
                        receipt.CretedByUId = HttpContext.Session.GetString("UserID");
                        receipt.CretedByUName = HttpContext.Session.GetString("UserName");
                        receipt.UpdatedDate = DateTime.Now;
                        receipt.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        receipt.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        receipt.DocEntry = null;

                        query = generator.GenerateInsertQuery(receipt, " [Accounts_Receipt_Head]", "DocEntry");
                    }
                    else
                    {
                        query = generator.GenerateUpdateQuery(receipt, "[Accounts_Receipt_Head]", "DocEntry", receipt.DocEntry, "");
                    }

                    await using (var cmd = new SqlCommand(query, con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();

                        if (string.IsNullOrEmpty(receipt.DocEntry))
                        {
                            cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                            receipt.DocEntry = (await cmd.ExecuteScalarAsync()).ToString();
                        }
                    }

                    // Payment row
                    ACCOUNTSRECEIPTROW payrow = new ACCOUNTSRECEIPTROW
                    {
                        DocEntry = receipt.DocEntry,
                        DocName = "Sales Tax Invoice",
                        DocNum = header.Docnum,
                        DocumentDate = header.DocumentDate.ToString(),
                        DueDate = header.DueDate.ToString(),
                        TotalPayment = receipt.CashAmt + receipt.BankAmt + receipt.ChequeAmt,
                        BalanceDue = header.NETTotal -(receipt.CashAmt +receipt.BankAmt+receipt.ChequeAmt),
                        LedgerID = header.LedgerID,
                        LedgerName = header.LedgerName,
                        BaseDocEntry = header.DocEntry,
                        BaseObjType = "STI",
                        CreatedDate = DateTime.Now,
                        CretedByUId = HttpContext.Session.GetString("UserID"),
                        CretedByUName = HttpContext.Session.GetString("UserName"),
                        UpdatedDate = DateTime.Now,
                        UpdatedByUName = HttpContext.Session.GetString("UserName"),
                        UpdatedByUId = HttpContext.Session.GetString("UserID")
                    };

                    await using (var cmd = new SqlCommand($"Delete from [Accounts_Receipt_Row] where DocEntry = '{receipt.DocEntry}'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }

                    string lineQuery = generator.GenerateInsertQuery(payrow, "[Accounts_Receipt_Row]", "ID");
                    await using (var cmdInsert = new SqlCommand(lineQuery, con, transaction))
                    {
                        cmdInsert.CommandTimeout = 300;
                        await cmdInsert.ExecuteNonQueryAsync();
                    }

                    string updateStatusQuery = @$"
                        Update Sale_SalesTaxInvoice_Head Set DocStatus ='C' Where DocEntry ='{header.DocEntry}';
                        Update Sale_SalesTaxInvoice_Row Set [Status] ='C' Where DocEntry ='{header.DocEntry}';
                    ";
                    await using (var cmdUpdate = new SqlCommand(updateStatusQuery, con, transaction))
                    {
                        cmdUpdate.CommandTimeout = 300;
                        await cmdUpdate.ExecuteNonQueryAsync();
                    }
                    using (SqlCommand cmd = new SqlCommand( @"SELECT DocEntry FROM Accounts_JournalEntry_Head 
                                          WHERE BaseObjType = 'REC'    AND BaseDocEntry = @BaseDocEntry", con, transaction))
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
                    await using (var cmdPaymentJE = new SqlCommand($"exec [Create_PaymentJournalEntry] '{receipt.DocEntry}','REC'", con, transaction))
                    {
                        cmdPaymentJE.CommandTimeout = 300;
                        await cmdPaymentJE.ExecuteNonQueryAsync();
                    }
                }
                await transaction.CommitAsync();
                return Json(new { Success = true, DocEntry = header.DocEntry, Message = "Document saved successfully." });
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
                transaction?.Dispose();
                if (con != null) await con.DisposeAsync();
            }
        }
        public async Task<IActionResult> DELETESALETAXINVOICE([FromBody] SALESTAXINVOICE data)
        {
            if (string.IsNullOrEmpty(data.header.DocEntry))
                return Json(new { success = false, message = "Please Select Valid Document." });

            string connectionString = _configuration.GetConnectionString("ErpConnection");

            await using var con = new SqlConnection(connectionString);
            await con.OpenAsync();
            await using var transaction = con.BeginTransaction();

            try
            {
                await using (var cmd = new SqlCommand("Delete_SalesTaxInvoice", con, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DocEntry", data.header.DocEntry);
                    await cmd.ExecuteNonQueryAsync();
                }

                await transaction.CommitAsync();
                return Json(new { success = true, message = "Document Deleted Successfully!" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request.", error = ex.Message });
            }
        }
        #endregion
        #region SALES RETURN
        public async Task<IActionResult> CREATESALESRETURN([FromBody] SALESRETURN data, string flag, string doctype)
        {
            if (flag == "P")
                return await PARK(data, doctype);

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(connectionString);
                await con.OpenAsync();
                transaction = con.BeginTransaction();

                var header = data.header;

                // Common audit fields
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

                    await using (var cmd = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Sale_SaleReturn_Head'", con, transaction))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            header.Docnum = reader["Message"].ToString();
                        }
                    }

                    query = generator.GenerateInsertQuery(header, " [Sale_SaleReturn_Head]", "DocEntry");
                }
                else
                {
                    query = generator.GenerateUpdateQuery(header, "[Sale_SaleReturn_Head]", "DocEntry", header.DocEntry, "");
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
                string billtostatecode = await GETBILLTOSTATECODE(header.LedgerID);
                string companystatecode = await GSTCOMPSTATECODE();
                // Process Lines
                if (data.lines != null && data.lines.Length > 0)
                {
                    await using (var cmd = new SqlCommand($@"
                        DELETE FROM Inventory_Stock where ObjType ='SR' AND DocEntry ='{header.DocEntry}'
                        DELETE FROM [Sale_SaleReturn_Row] WHERE DocEntry = '{header.DocEntry}'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }
                 
                    int linenum = 1;
                    foreach (var line in data.lines)
                    {
                        if (!string.IsNullOrWhiteSpace(line.TaxCode))
                        {
                            if (billtostatecode == companystatecode)
                            {
                                // For state code 27: only GST@ is valid
                                if (!line.TaxCode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{line.TaxCode}' for Item Code {line.ItemCode} (Line No: {linenum}). Only GST is allowed. ");
                                }
                            }
                            else
                            {
                                // For all other state codes: GST@ not allowed, IGST@ or UTGST@ is fine
                                if (line.TaxCode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{line.TaxCode}' for Item Code {line.ItemCode} (Line No: {linenum}). Only IGST or UTGST Allowed.");
                                }
                            }
                        }
                        else
                        {
                            if (transaction != null) await transaction.RollbackAsync();
                            return StatusCode(500,
                                $"Missing Tax Code for Item Code {line.ItemCode} (Line No: {linenum}).");
                        }
                        line.LineNum = linenum;
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = header.DocEntry;
                        line.CreatedDate = DateTime.Now;
                        line.CretedByUId = HttpContext.Session.GetString("UserID");
                        line.CretedByUName = HttpContext.Session.GetString("UserName");
                        line.ID = null;

                        string lineQuery = generator.GenerateInsertQuery(line, "[Sale_SaleReturn_Row]", "ID");
                        await using (var cmdLine = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmdLine.CommandTimeout = 300;
                            await cmdLine.ExecuteNonQueryAsync();
                        }
                        linenum++;
                    }
                }

                // Process Freight
                if (data.freight != null && data.freight.Length > 0 && data.header.FreightTotal > 0)
                {
                    await using (var cmd = new SqlCommand($"DELETE FROM [Sale_SaleReturn_Freight] WHERE DocEntry = '{header.DocEntry}'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }

                    foreach (var line in data.freight)
                    {
                        string taxcode = "";
                        using (var cmd = new SqlCommand(@$"select TaxCode from TaxCode_Mst where TaxCodeId = '{line.TaxCodeId}'", con, transaction))
                        {
                            await using var reader = await cmd.ExecuteReaderAsync();
                            while (await reader.ReadAsync())
                            {
                                taxcode = reader["TaxCode"].ToString();
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(taxcode))
                        {
                            if (billtostatecode == companystatecode)
                            {
                                // For state code 27: only GST@ is valid
                                if (!taxcode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{taxcode}' for  {line.Name}  Only GST is allowed. ");
                                }
                            }
                            else
                            {
                                // For all other state codes: GST@ not allowed, IGST@ or UTGST@ is fine
                                if (taxcode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{taxcode}' for  {line.Name}  Only IGST or UTGST Allowed.");
                                }
                            }
                        }
                        else
                        {
                            if (transaction != null) await transaction.RollbackAsync();
                            return StatusCode(500,
                                $"Missing Tax Code for  {line.Name} .");
                        }
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = header.DocEntry;
                        line.Name = null;
                        line.CreatedDate = DateTime.Now;
                        line.CretedByUId = HttpContext.Session.GetString("UserID");
                        line.CretedByUName = HttpContext.Session.GetString("UserName");
                        line.ID = null;

                        string lineQuery = generator.GenerateInsertQuery(line, "[Sale_SaleReturn_Freight]", "ID");
                        await using (var cmdLine = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmdLine.CommandTimeout = 300;
                            await cmdLine.ExecuteNonQueryAsync();
                        }
                    }
                }

                // Update Open Quantity for Delivery
                string queryUpdateQty = @$"EXEC UpdateOpenQuantity 'Sale_SaleDelivery_Row','Sale_SaleDelivery_Head','Sale_SaleReturn_Head','Sale_SaleReturn_Row','{header.DocEntry}'";
                await using (var cmdUpdate = new SqlCommand(queryUpdateQty, con, transaction))
                {
                    cmdUpdate.CommandTimeout = 300;
                    await cmdUpdate.ExecuteNonQueryAsync();
                }

                // Inventory Stock
                await using (var cmdStock = new SqlCommand($"EXEC [Insert_InvtoryStock] '{header.DocEntry}','SR'", con, transaction))
                await using (var rdr = await cmdStock.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        string success = rdr["success"].ToString();
                        string message = rdr["message"].ToString();

                        if (!string.Equals(success, "true", StringComparison.OrdinalIgnoreCase))
                        {
                            await transaction.RollbackAsync();
                            return StatusCode(500, $"An error occurred: {message}");
                        }
                    }
                }

                await transaction.CommitAsync();
                return Json(new { Success = true, DocEntry = header.DocEntry, Message = "Document saved successfully." });
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
                transaction?.Dispose();
                if (con != null) await con.DisposeAsync();
            }
        }
        public async Task<IActionResult> DELETESALESRETURN(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Json(new { success = false, message = "ID cannot be empty" });

            string connectionString = _configuration.GetConnectionString("ErpConnection");

            await using var con = new SqlConnection(connectionString);
            await con.OpenAsync();

            try
            {
                string query = @"Exec Delete_SalesReturn @id";
                await using var cmd = new SqlCommand(query, con);
                cmd.CommandTimeout = 300;
                cmd.Parameters.AddWithValue("@id", id);
                await cmd.ExecuteNonQueryAsync();

                return Json(new { success = true, message = "Document Deleted Successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        #endregion
        #region SALES CREDIT NOTE
         public async Task<IActionResult> CREATESALESCREDITNOTE([FromBody] SALESCREDITNOTE data, string flag, string doctype)
        {
            if (flag == "P")
                return await PARK(data, doctype);

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(connectionString);
                await con.OpenAsync();
                transaction = con.BeginTransaction();

                var header = data.header;
                var payment = data.payment;
                header.AppliedAmount = 0;
                header.BalanceDue = header.NETTotal;
                // Common audit fields
                header.UpdatedDate = DateTime.Now;
                header.UpdatedByUName = HttpContext.Session.GetString("UserName");
                header.UpdatedByUId = HttpContext.Session.GetString("UserID");

                Genrate_Query generator = new Genrate_Query();
                string query;

                // Header Insert or Update
                if (string.IsNullOrEmpty(header.DocEntry))
                {
                    header.CreatedDate = DateTime.Now;
                    header.CretedByUId = HttpContext.Session.GetString("UserID");
                    header.CretedByUName = HttpContext.Session.GetString("UserName");
                    header.DocEntry = null;

                    await using (var cmd = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Sale_SaleCreditNote_Head'", con, transaction))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            header.Docnum = reader["Message"].ToString();
                        }
                    }

                    query = generator.GenerateInsertQuery(header, " [Sale_SaleCreditNote_Head]", "DocEntry");
                }
                else
                {
                    query = generator.GenerateUpdateQuery(header, "[Sale_SaleCreditNote_Head]", "DocEntry", header.DocEntry, "");
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
                string billtostatecode = await GETBILLTOSTATECODE(header.LedgerID);
                string companystatecode = await GSTCOMPSTATECODE();
                // Process Lines
                if (data.lines != null && data.lines.Length > 0)
                {
                    await using (var cmd = new SqlCommand($@"
                    DELETE FROM Inventory_Stock where ObjType ='SCN' AND DocEntry ='{header.DocEntry}'
                    DELETE FROM [Sale_SaleCreditNote_Row] WHERE DocEntry = '{header.DocEntry}'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }
                 
                    int linenum = 1;
                    foreach (var line in data.lines)
                    {
                        if (!string.IsNullOrWhiteSpace(line.TaxCode))
                        {
                            if (billtostatecode == companystatecode)
                            {
                                // For state code 27: only GST@ is valid
                                if (!line.TaxCode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{line.TaxCode}' for Item Code {line.ItemCode} (Line No: {linenum}). Only GST is allowed. ");
                                }
                            }
                            else
                            {
                                // For all other state codes: GST@ not allowed, IGST@ or UTGST@ is fine
                                if (line.TaxCode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{line.TaxCode}' for Item Code {line.ItemCode} (Line No: {linenum}). Only IGST or UTGST Allowed.");
                                }
                            }
                        }
                        else
                        {
                            if (transaction != null) await transaction.RollbackAsync();
                            return StatusCode(500,
                                $"Missing Tax Code for Item Code {line.ItemCode} (Line No: {linenum}).");
                        }
                        line.LineNum = linenum;
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = header.DocEntry;
                        line.CreatedDate = DateTime.Now;
                        line.CretedByUId = HttpContext.Session.GetString("UserID");
                        line.CretedByUName = HttpContext.Session.GetString("UserName");
                        line.ID = null;

                        string lineQuery = generator.GenerateInsertQuery(line, "[Sale_SaleCreditNote_Row]", "ID");
                        await using (var cmdLine = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmdLine.CommandTimeout = 300;
                            await cmdLine.ExecuteNonQueryAsync();
                        }
                        linenum++;
                    }
                }

                // Process Freight
                if (data.freight != null && data.freight.Length > 0 && data.header.FreightTotal > 0)
                {
                    await using (var cmd = new SqlCommand($"DELETE FROM [Sale_SaleCreditNote_Freight] WHERE DocEntry = '{header.DocEntry}'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }

                    foreach (var line in data.freight)
                    {
                        string taxcode = "";
                        using (var cmd = new SqlCommand(@$"select TaxCode from TaxCode_Mst where TaxCodeId = '{line.TaxCodeId}'", con, transaction))
                        {
                            await using var reader = await cmd.ExecuteReaderAsync();
                            while (await reader.ReadAsync())
                            {
                                taxcode = reader["TaxCode"].ToString();
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(taxcode))
                        {
                            if (billtostatecode == companystatecode)
                            {
                                // For state code 27: only GST@ is valid
                                if (!taxcode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{taxcode}' for  {line.Name}  Only GST is allowed. ");
                                }
                            }
                            else
                            {
                                // For all other state codes: GST@ not allowed, IGST@ or UTGST@ is fine
                                if (taxcode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{taxcode}' for  {line.Name}  Only IGST or UTGST Allowed.");
                                }
                            }
                        }
                        else
                        {
                            if (transaction != null) await transaction.RollbackAsync();
                            return StatusCode(500,
                                $"Missing Tax Code for  {line.Name} .");
                        }
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = header.DocEntry;
                        line.Name = null;
                        line.CreatedDate = DateTime.Now;
                        line.CretedByUId = HttpContext.Session.GetString("UserID");
                        line.CretedByUName = HttpContext.Session.GetString("UserName");
                        line.ID = null;

                        string lineQuery = generator.GenerateInsertQuery(line, "[Sale_SaleCreditNote_Freight]", "ID");
                        await using (var cmdLine = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmdLine.CommandTimeout = 300;
                            await cmdLine.ExecuteNonQueryAsync();
                        }
                    }
                }

                // Update Open Quantity
                string queryUpdateQty = @$"EXEC UpdateOpenQuantity 'Sale_SaleReturn_Row','Sale_SaleReturn_Head','Sale_SaleCreditNote_Head','Sale_SaleCreditNote_Row','{header.DocEntry}'";
                await using (var cmdUpdate = new SqlCommand(queryUpdateQty, con, transaction))
                {
                    cmdUpdate.CommandTimeout = 300;
                    await cmdUpdate.ExecuteNonQueryAsync();
                }

                // Inventory Stock
                string deleteStock = @$"DELETE FROM Inventory_Stock WHERE DocEntry ='{header.DocEntry}' AND ObjType ='SCN'";
                await using (var cmdDelStock = new SqlCommand(deleteStock, con, transaction))
                {
                    cmdDelStock.CommandTimeout = 300;
                    await cmdDelStock.ExecuteNonQueryAsync();
                }

                await using (var cmdStock = new SqlCommand($"EXEC [Insert_InvtoryStock] '{header.DocEntry}','SCN'", con, transaction))
                await using (var rdrStock = await cmdStock.ExecuteReaderAsync())
                {
                    while (await rdrStock.ReadAsync())
                    {
                        if (!string.Equals(rdrStock["success"].ToString(), "true", StringComparison.OrdinalIgnoreCase))
                        {
                            await transaction.RollbackAsync();
                            return StatusCode(500, $"An error occurred: {rdrStock["message"]}");
                        }
                    }
                }

                // Create Sales Journal Entry
                using (SqlCommand cmd = new SqlCommand(
                @"SELECT DocEntry 
                      FROM Accounts_JournalEntry_Head 
                      WHERE BaseObjType = 'SCN' 
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
                await using (var cmdJournal = new SqlCommand($"EXEC [Create_SalesJournalEntry] '{header.DocEntry}','SCN'", con, transaction))
                await using (var rdrJournal = await cmdJournal.ExecuteReaderAsync())
                {
                    while (await rdrJournal.ReadAsync())
                    {
                        if (!string.Equals(rdrJournal["success"].ToString(), "true", StringComparison.OrdinalIgnoreCase))
                        {
                            await transaction.RollbackAsync();
                            return StatusCode(500, $"An error occurred: {rdrJournal["message"]}");
                        }
                    }
                }

                // Payment Transaction
                if (payment.ChequeAmt + payment.CashAmt + payment.BankAmt > 0)
                {
                    string DocNum = "";
                    string queryNext = @$"EXEC GET_NextDocNumber '{header.FYearId}','Accounts_Payment_Head'";
                    await using (var cmd = new SqlCommand(queryNext, con, transaction))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                            DocNum = reader["Message"].ToString();
                    }
                    await using (var cmd = new SqlCommand(@$"Select DefLedgerId FROm DefaultConfiguration where [Name]='CHEQUEACCOUNT'", con, transaction))
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            payment.ChequeLedgerId = reader["DefLedgerId"].ToString();
                        }
                    }
                    if (string.IsNullOrEmpty(payment.DocEntry))
                    {
                        if (payment.ChequeAmt == 0)
                        {
                            payment.ChequeLedgerId = "";
                        }
                        else if (payment.CashAmt == 0)
                        {
                            payment.CashLedgerId = "";
                        }
                        else if (payment.BankAmt == 0)
                        {
                            payment.BankLedgerId = "";
                        }
                        payment.DocStatus = "O";
                        payment.Docnum = DocNum;
                        payment.FYearId = header.FYearId;
                        payment.DocumentDate = header.DocumentDate.ToString();
                        payment.PostingDate = header.PostingDate.ToString();
                        payment.DueDate = header.DueDate.ToString();
                        payment.NETTotal = payment.ChequeAmt + payment.CashAmt + payment.BankAmt;
                        payment.OpenBalance = 0;
                        payment.PayOnAccAmt = 0;
                        payment.PayOnAccCheck = "N";
                        payment.LedgerID = header.LedgerID;
                        payment.LedgerCode = header.LedgerCode;
                        payment.LedgerName = header.LedgerName;
                        payment.LedgerType = "B";
                        payment.RefNo = header.RefNo;
                        payment.Remarks = header.Remarks;
                        payment.LedgerBalance = 0;
                        payment.CreatedDate = DateTime.Now;
                        payment.CretedByUId = HttpContext.Session.GetString("UserID");
                        payment.CretedByUName = HttpContext.Session.GetString("UserName");
                        payment.UpdatedDate = DateTime.Now;
                        payment.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        payment.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        payment.DocEntry = null;
                        query = generator.GenerateInsertQuery(payment, " [Accounts_Payment_Head]", "DocEntry");
                    }
                    else
                    {
                        query = generator.GenerateUpdateQuery(payment, "[Accounts_Payment_Head]", "DocEntry", payment.DocEntry, "");
                    }

                    await using (var cmdPay = new SqlCommand(query, con, transaction))
                    {
                        cmdPay.CommandTimeout = 300;
                        await cmdPay.ExecuteNonQueryAsync();
                        if (string.IsNullOrEmpty(payment.DocEntry))
                        {
                            cmdPay.CommandText = "SELECT SCOPE_IDENTITY()";
                            payment.DocEntry = (await cmdPay.ExecuteScalarAsync()).ToString();
                        }
                    }

                    ACCOUNTSRECEIPTROW payrow = new ACCOUNTSRECEIPTROW
                    {
                        DocEntry = payment.DocEntry,
                        DocName = "Credit Note",
                        DocNum = header.Docnum,
                        DocumentDate = header.DocumentDate.ToString(),
                        DueDate = header.DueDate.ToString(),
                        TotalPayment = payment.CashAmt + payment.BankAmt + payment.ChequeAmt,
                        BalanceDue = header.NETTotal - payment.CashAmt + payment.BankAmt + payment.ChequeAmt,
                        LedgerID = header.LedgerID,
                        LedgerName = header.LedgerName,
                        Remarks = "",
                        BaseDocEntry = header.DocEntry,
                        BaseObjType = "SCN",
                        CreatedDate = DateTime.Now,
                        CretedByUId = HttpContext.Session.GetString("UserID"),
                        CretedByUName = HttpContext.Session.GetString("UserName"),
                        UpdatedDate = DateTime.Now,
                        UpdatedByUName = HttpContext.Session.GetString("UserName"),
                        UpdatedByUId = HttpContext.Session.GetString("UserID"),
                        ID = null
                    };

                    await using (var cmdDeletePayRow = new SqlCommand($"DELETE FROM [Accounts_Payment_Row] WHERE DocEntry = '{payment.DocEntry}'", con, transaction))
                    {
                        cmdDeletePayRow.CommandTimeout = 300;
                        await cmdDeletePayRow.ExecuteNonQueryAsync();
                    }

                    string lineQuery1 = generator.GenerateInsertQuery(payrow, "[Accounts_Payment_Row]", "ID");
                    await using (var cmdPayRow = new SqlCommand(lineQuery1, con, transaction))
                    {
                        cmdPayRow.CommandTimeout = 300;
                        await cmdPayRow.ExecuteNonQueryAsync();
                    }

                    string lineQuery3 = @$"UPDATE Sale_SaleCreditNote_Head SET DocStatus ='C' WHERE DocEntry ='{header.DocEntry}';
                                   UPDATE Sale_SaleCreditNote_Row SET [Status] ='C' WHERE DocEntry ='{header.DocEntry}'";
                    await using (var cmdUpdateStatus = new SqlCommand(lineQuery3, con, transaction))
                    {
                        cmdUpdateStatus.CommandTimeout = 300;
                        await cmdUpdateStatus.ExecuteNonQueryAsync();
                    }
                    using (SqlCommand cmd = new SqlCommand(@"SELECT DocEntry FROM Accounts_JournalEntry_Head 
                       WHERE BaseObjType = 'PAY'    AND BaseDocEntry = @BaseDocEntry", con, transaction))
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
                    await using (var cmdJournalEntry = new SqlCommand($"EXEC [Create_PaymentJournalEntry] '{payment.DocEntry}','PAY'", con, transaction))
                    {
                        cmdJournalEntry.CommandTimeout = 300;
                        await cmdJournalEntry.ExecuteNonQueryAsync();
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
                transaction?.Dispose();
                if (con != null) await con.DisposeAsync();
            }
        }
         public async Task<IActionResult> DELETESALESCREDITNOTE(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Json(new { success = false, message = "ID cannot be empty" });

            string connectionString = _configuration.GetConnectionString("ErpConnection");

            await using var con = new SqlConnection(connectionString);
            await con.OpenAsync();

            try
            {
                string query = @"EXEC [Delete_SaleCreditNote] @id";
                await using var cmd = new SqlCommand(query, con);
                cmd.CommandTimeout = 300;
                cmd.Parameters.AddWithValue("@id", id);
                await cmd.ExecuteNonQueryAsync();

                return Json(new { success = true, message = "Document Deleted Successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        #endregion
        #region PARK DOC
        public async Task<IActionResult> PARK([FromBody] dynamic data, string OBJType)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                var parkdata = new Park
                {
                    OBJType = OBJType,
                    DocEntry = null,
                    CreatedDate = DateTime.Now,
                    CretedByUId = HttpContext.Session.GetString("UserID"),
                    CretedByUName = HttpContext.Session.GetString("UserName"),
                    Payload = JsonSerializer.Serialize(data),
                    RefNo = data.header?.RefNo ?? string.Empty,
                    DocumentDate = DateTime.Now
                };

                ParkDoc generator = new ParkDoc();

                // Assuming ParKDocument can be awaited if async
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
