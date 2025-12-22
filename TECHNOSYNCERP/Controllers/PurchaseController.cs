using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using TECHNOSYNCERP.Models;

namespace TECHNOSYNCERP.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IConfiguration _configuration;
        public PurchaseController(ILogger<PurchaseController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> GETBTNAUTH(string objtype)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string userId = HttpContext.Session.GetString("UserID") ?? "";

                string query = @"
                 SELECT * 
                 FROM BtnAuth T0
                 INNER JOIN Btn T1 ON T1.BtnID = T0.BtnID
                 WHERE T0.ObjType = @ObjType AND T0.UserID = @UserID";

                var dataList = new List<Dictionary<string, object>>();

                await using var con = new SqlConnection(connectionString);
                await con.OpenAsync();

                await using var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ObjType", objtype ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@UserID", userId);

                await using var rdr = await cmd.ExecuteReaderAsync();
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

                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public async Task<IActionResult> PurchaseQuotation()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("PURQ");
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
        public async Task<IActionResult> PurchaseOrder()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("PURO");
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
        public async Task<IActionResult> GRPO()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("GRPO");
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
        public async Task<IActionResult> GoodsReturn()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("PGR");
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
        public async Task<IActionResult> DebitNote()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("PDN");
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
        public async Task<IActionResult> PurchaseInvoice()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH("PURI");
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
        public async Task<IActionResult> GETLEDGERGROUP()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var resultList = new List<Dictionary<string, string>>();

            try
            {
                await using var con = new SqlConnection(connStr);
                await con.OpenAsync();

                string query = "EXEC GET_LedgerGroup";
                await using var cmd = new SqlCommand(query, con);

                await using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var obj = new Dictionary<string, string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        obj[reader.GetName(i)] = await reader.IsDBNullAsync(i) ? "" : reader.GetValue(i)?.ToString();
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
        private async Task<List<Dictionary<string, string>>> ExecuteNextDocNumberAsync(string id, string tableName)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var resultList = new List<Dictionary<string, string>>();

            await using var con = new SqlConnection(connStr);
            await con.OpenAsync();

            string query = $@"EXEC GET_NextDocNumber '{id}','{tableName}'";
            await using var cmd = new SqlCommand(query, con);

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var obj = new Dictionary<string, string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    obj[reader.GetName(i)] = await reader.IsDBNullAsync(i) ? "" : reader.GetValue(i)?.ToString();
                }
                resultList.Add(obj);
            }

            return resultList;
        }
        public async Task<IActionResult> GETPURCHASEINVOICEDOCNUM(string id)
        {
            try
            {
                var result = await ExecuteNextDocNumberAsync(id, "Purchase_PurchaseInvoiceHead");
                return Json(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETPURCHASEQUOTATIONDOCNUM(string id)
        {
            try
            {
                var result = await ExecuteNextDocNumberAsync(id, "Purchase_PurchaseQuotation_Head");
                return Json(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETPURCHASEORDERDOCNUM(string id)
        {
            try
            {
                var result = await ExecuteNextDocNumberAsync(id, "Purchase_PurchaseOrder_Head");
                return Json(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETPURCHASEDEBITDOCNUM(string id)
        {
            try
            {
                var result = await ExecuteNextDocNumberAsync(id, "Purchase_PurchaseDebitNote_Head");
                return Json(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETPurchaseGoodsReturnDOCNUM(string id)
        {
            try
            {
                var result = await ExecuteNextDocNumberAsync(id, "Purchase_PurchaseGoodsReturn_Head");
                return Json(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GETPURCHASEGRPODOCNUM(string id)
        {
            try
            {
                var result = await ExecuteNextDocNumberAsync(id, "Purchase_GRPO_Head");
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

            var newobj = new
            {
                header = header,
                row = row,
                freight = freight,
            };

            try
            {
                await using var con = new SqlConnection(connStr);
                await con.OpenAsync();

                // Fetch Header
                string queryHeader = @$"EXEC [FindDocHeader] '{doctype}','{docEntry}','{docid}','{refno}','{fromdate}','{todate}','',''";
                await using (var cmd = new SqlCommand(queryHeader, con))
                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var obj = new Dictionary<string, string>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            obj[reader.GetName(i)] = await reader.IsDBNullAsync(i) ? "" : reader.GetValue(i)?.ToString();
                        }
                        header.Add(obj);
                    }
                }

                if (!string.IsNullOrEmpty(docEntry))
                {
                    // Fetch Row
                    string queryRow = @$"EXEC [FindDocRow] '{doctype}','{docEntry}'";
                    await using (var cmdRow = new SqlCommand(queryRow, con))
                    await using (var readerRow = await cmdRow.ExecuteReaderAsync())
                    {
                        while (await readerRow.ReadAsync())
                        {
                            var obj = new Dictionary<string, string>();
                            for (int i = 0; i < readerRow.FieldCount; i++)
                            {
                                obj[readerRow.GetName(i)] = await readerRow.IsDBNullAsync(i) ? "" : readerRow.GetValue(i)?.ToString();
                            }
                            row.Add(obj);
                        }
                    }

                    // Fetch Freight
                    string queryFreight = @$"EXEC [FindDocFreight] '{doctype}','{docEntry}'";
                    await using (var cmdFreight = new SqlCommand(queryFreight, con))
                    await using (var readerFreight = await cmdFreight.ExecuteReaderAsync())
                    {
                        while (await readerFreight.ReadAsync())
                        {
                            var obj = new Dictionary<string, string>();
                            for (int i = 0; i < readerFreight.FieldCount; i++)
                            {
                                obj[readerFreight.GetName(i)] = await readerFreight.IsDBNullAsync(i) ? "" : readerFreight.GetValue(i)?.ToString();
                            }
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
            status ??= "";

            var connStr = _configuration.GetConnectionString("ErpConnection");
            var header = new List<Dictionary<string, string>>();
            var row = new List<Dictionary<string, string>>();
            var freight = new List<Dictionary<string, string>>();

            var newobj = new
            {
                header = header,
                row = row,
                freight = freight,
            };

            try
            {
                await using var con = new SqlConnection(connStr);
                await con.OpenAsync();

                // Fetch Header
                string queryHeader = @$"EXEC [Document_CopyToHeader] '{doctype}','{docEntry}','{docid}','{refno}','{fromdate}','{todate}','{HttpContext.Session.GetString("UserID")}','','{ledgercode}','{status}'";
                await using (var cmdHeader = new SqlCommand(queryHeader, con))
                await using (var readerHeader = await cmdHeader.ExecuteReaderAsync())
                {
                    while (await readerHeader.ReadAsync())
                    {
                        var obj = new Dictionary<string, string>();
                        for (int i = 0; i < readerHeader.FieldCount; i++)
                        {
                            obj[readerHeader.GetName(i)] = await readerHeader.IsDBNullAsync(i) ? "" : readerHeader.GetValue(i)?.ToString();
                        }
                        header.Add(obj);
                    }
                }

                if (!string.IsNullOrEmpty(docEntry))
                {
                    // Fetch Row
                    string queryRow = @$"EXEC [Document_CopyToRow] '{doctype}','{docEntry}','O'";
                    await using (var cmdRow = new SqlCommand(queryRow, con))
                    await using (var readerRow = await cmdRow.ExecuteReaderAsync())
                    {
                        while (await readerRow.ReadAsync())
                        {
                            var obj = new Dictionary<string, string>();
                            for (int i = 0; i < readerRow.FieldCount; i++)
                            {
                                obj[readerRow.GetName(i)] = await readerRow.IsDBNullAsync(i) ? "" : readerRow.GetValue(i)?.ToString();
                            }
                            row.Add(obj);
                        }
                    }

                    // Fetch Freight
                    string queryFreight = @$"EXEC [FindDocFreight] '{doctype}','{docEntry}'";
                    await using (var cmdFreight = new SqlCommand(queryFreight, con))
                    await using (var readerFreight = await cmdFreight.ExecuteReaderAsync())
                    {
                        while (await readerFreight.ReadAsync())
                        {
                            var obj = new Dictionary<string, string>();
                            for (int i = 0; i < readerFreight.FieldCount; i++)
                            {
                                obj[readerFreight.GetName(i)] = await readerFreight.IsDBNullAsync(i) ? "" : readerFreight.GetValue(i)?.ToString();
                            }
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
        #region PURCHASE QUOTATION
        public async Task<IActionResult> CREATEPURCHQUOT([FromBody] PURCHASEQUOT data, string flag, string doctype)
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
                transaction = con.BeginTransaction(); // must be synchronous
               

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

                    using (var cmd = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Purchase_PurchaseQuotation_Head'", con, transaction))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (await reader.ReadAsync())
                                {
                                    header.Docnum = reader["Message"].ToString();
                                }
                            }
                        }
                    }

                    query = generator.GenerateInsertQuery(header, " [Purchase_PurchaseQuotation_Head]", "DocEntry");
                }
                else
                {
                    // Update record
                    query = generator.GenerateUpdateQuery(header, "[Purchase_PurchaseQuotation_Head]", "DocEntry", header.DocEntry, "");
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
                    using (SqlCommand cmd = new SqlCommand($"DELETE FROM [Purchase_PurchaseQuotation_Row] WHERE DocEntry = '{header.DocEntry}'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }

                    int lineNum = 1;
                    for (int i = 0; i < data.lines.Length; i++)
                    {
                        var line = data.lines[i];
                        if (!string.IsNullOrWhiteSpace(line.TaxCode))
                        {
                            if (billtostatecode == companystatecode)
                            {
                                // For state code 27: only GST@ is valid
                                if (!line.TaxCode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{line.TaxCode}' for Item Code {line.ItemCode} (Line No: {lineNum}). Only GST is allowed. ");
                                }
                            }
                            else
                            {
                                // For all other state codes: GST@ not allowed, IGST@ or UTGST@ is fine
                                if (line.TaxCode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{line.TaxCode}' for Item Code {line.ItemCode} (Line No: {lineNum}). Only IGST or UTGST Allowed.");
                                }
                            }
                        }
                        else
                        {
                            if (transaction != null) await transaction.RollbackAsync();
                            return StatusCode(500,
                                $"Missing Tax Code for Item Code {line.ItemCode} (Line No: {lineNum}).");
                        }
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = data.header.DocEntry;
                        line.LineNum = lineNum;
                        line.CreatedDate = DateTime.Now;
                        line.CretedByUId = HttpContext.Session.GetString("UserID");
                        line.CretedByUName = HttpContext.Session.GetString("UserName");
                        line.ID = null;

                        string lineQuery = generator.GenerateInsertQuery(line, "[Purchase_PurchaseQuotation_Row]", "ID");

                        using (SqlCommand cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }

                        lineNum++;
                    }
                }

                // Process Freight
                if (data.freight != null && data.freight.Length > 0 && data.header.FreightTotal > 0)
                {
                    using (SqlCommand cmd = new SqlCommand($"DELETE FROM [Purchase_PurchaseQuotation_Freight] WHERE DocEntry = '{header.DocEntry}'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }

                    for (int i = 0; i < data.freight.Length; i++)
                    {
                        var line = data.freight[i];
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
                        line.DocEntry = data.header.DocEntry;
                        line.Name = null;
                        line.CreatedDate = DateTime.Now;
                        line.CretedByUId = HttpContext.Session.GetString("UserID");
                        line.CretedByUName = HttpContext.Session.GetString("UserName");
                        line.ID = null;

                        string lineQuery = generator.GenerateInsertQuery(line, "[Purchase_PurchaseQuotation_Freight]", "ID");

                        using (SqlCommand cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                }

                await transaction.CommitAsync(); // Async commit
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
                if (con != null)
                {
                    await con.CloseAsync();
                    con.Dispose();
                }
            }
        }
        public async Task<IActionResult> DELETEPURCHQUOT(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(new { success = false, message = "ID cannot be empty" });
            }

            string ConnectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    int validation = 0;
                    int DocNum = 0;
                    string message = "This Document Has Reference ";

                    string[] queries = new string[]
                    {
                         $@"exec [Document_CheckBaseDocEntry] 'Purchase_PurchaseOrder_Row','Purchase_PurchaseOrder_Head','{id}','PQ'",
                         $@"exec [Document_CheckBaseDocEntry] 'Purchase_PurchaseInvoiceRow','Purchase_PurchaseInvoiceHead','{id}','PQ'",
                         $@"exec [Document_CheckBaseDocEntry] 'Purchase_GRPO_Row','Purchase_GRPO_Head','{id}','PQ'"
                    };

                    string[] messages = new string[]
                    {
                         " In Purchase Order, Document No ",
                         " In Purchase Invoice!, Document No ",
                         " In GRPO!, Document No "
                    };

                    for (int i = 0; i < queries.Length; i++)
                    {
                        using (SqlCommand cmd = new SqlCommand(queries[i], con))
                        {
                            cmd.CommandTimeout = 300;
                            using (var reader = await cmd.ExecuteReaderAsync())
                            {
                                if (await reader.ReadAsync())
                                {
                                    validation = Convert.ToInt16(reader["RESULT"]);
                                    DocNum = Convert.ToInt16(reader["Docnum"]);
                                    if (validation > 0)
                                    {
                                        message += messages[i] + DocNum + " !";
                                        return Json(new { success = false, message = message });
                                    }
                                }
                            }
                        }
                    }

                    string query = @"
                        DELETE FROM [Purchase_PurchaseQuotation_Freight] WHERE DocEntry = @id;
                        DELETE FROM [Purchase_PurchaseQuotation_Row] WHERE DocEntry = @id;
                        DELETE FROM [Purchase_PurchaseQuotation_Head] WHERE DocEntry = @id;
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
                // Consider logging the exception here
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
            }
        }
        #endregion
        #region PURCHASE ORDER
          public async Task<IActionResult> CREATEPURCHORDR([FromBody] PURCHASEORDER data, string flag, string doctype)
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
                transaction = con.BeginTransaction(); // transaction must remain synchronous

                // Process Header
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

                    using (var cmd = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Purchase_PurchaseOrder_Head'", con, transaction))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                                header.Docnum = reader["Message"].ToString();
                        }
                    }

                    query = generator.GenerateInsertQuery(header, " [Purchase_PurchaseOrder_Head]", "DocEntry");
                }
                else
                {
                    query = generator.GenerateUpdateQuery(header, "[Purchase_PurchaseOrder_Head]", "DocEntry", header.DocEntry, "");
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
                    using (SqlCommand cmd = new SqlCommand($"DELETE FROM [Purchase_PurchaseOrder_Row] WHERE DocEntry = '{header.DocEntry}'", con, transaction))
                        await cmd.ExecuteNonQueryAsync();

                    int lineNum = 1;
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
                                        $"Invalid Tax Code '{line.TaxCode}' for Item Code {line.ItemCode} (Line No: {lineNum }). Only GST is allowed. ");
                                }
                            }
                            else
                            {
                                // For all other state codes: GST@ not allowed, IGST@ or UTGST@ is fine
                                if (line.TaxCode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{line.TaxCode}' for Item Code {line.ItemCode} (Line No: {lineNum }). Only IGST or UTGST Allowed.");
                                }
                            }
                        }
                        else
                        {
                            if (transaction != null) await transaction.RollbackAsync();
                            return StatusCode(500,
                                $"Missing Tax Code for Item Code {line.ItemCode} (Line No: {lineNum + 1}).");
                        }
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = header.DocEntry;
                        line.LineNum = lineNum;
                        line.CreatedDate = DateTime.Now;
                        line.CretedByUId = HttpContext.Session.GetString("UserID");
                        line.CretedByUName = HttpContext.Session.GetString("UserName");
                        line.ID = null;

                        string lineQuery = generator.GenerateInsertQuery(line, "[Purchase_PurchaseOrder_Row]", "ID");
                        using (SqlCommand cmd = new SqlCommand(lineQuery, con, transaction))
                            await cmd.ExecuteNonQueryAsync();

                        lineNum++;
                    }
                }

                // Process Freight
                if (data.freight != null && data.freight.Length > 0 && data.header.FreightTotal > 0)
                {
                    using (SqlCommand cmd = new SqlCommand($"DELETE FROM [Purchase_PurchaseOrder_Freight] WHERE DocEntry = '{header.DocEntry}'", con, transaction))
                        await cmd.ExecuteNonQueryAsync();

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

                        string lineQuery = generator.GenerateInsertQuery(line, "[Purchase_PurchaseOrder_Freight]", "ID");
                        using (SqlCommand cmd = new SqlCommand(lineQuery, con, transaction))
                            await cmd.ExecuteNonQueryAsync();
                    }
                }

                string Query = @$"EXEC UpdateOpenQuantity 'Purchase_PurchaseQuotation_Row','Purchase_PurchaseQuotation_Head','Purchase_PurchaseOrder_Head','Purchase_PurchaseOrder_Row','{header.DocEntry}'";
                using (SqlCommand cmd = new SqlCommand(Query, con, transaction))
                    await cmd.ExecuteNonQueryAsync();

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
          public async Task<IActionResult> DELETEPURCHORDR(string id)
          {
            if (string.IsNullOrEmpty(id))
                return Json(new { success = false, message = "ID cannot be empty" });

            string ConnectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    int validation = 0;
                    int DocNum = 0;
                    string message = "This Document Has Reference ";

                    string[] queries = new string[]
                    {
                       $@"exec [Document_CheckBaseDocEntry] 'Purchase_GRPO_Row','Purchase_GRPO_Head','{id}','PO'",
                       $@"exec [Document_CheckBaseDocEntry] 'Purchase_PurchaseInvoiceRow','Purchase_PurchaseInvoiceHead','{id}','PO'"
                    };

                    string[] messages = new string[]
                    {
                      " In GRPO!, Document No ",
                      " In Purchase Invoice!, Document No "
                    };

                    for (int i = 0; i < queries.Length; i++)
                    {
                        using (SqlCommand cmd = new SqlCommand(queries[i], con))
                        {
                            cmd.CommandTimeout = 300;
                            using (var reader = await cmd.ExecuteReaderAsync())
                            {
                                if (await reader.ReadAsync())
                                {
                                    validation = Convert.ToInt16(reader["RESULT"]);
                                    DocNum = Convert.ToInt16(reader["Docnum"]);
                                    if (validation > 0)
                                    {
                                        message += messages[i] + DocNum + " !";
                                        return Json(new { success = false, message = message });
                                    }
                                }
                            }
                        }
                    }

                    string query = @$"Exec [Delete_PurchaseOrder] '{id}'";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }

                    return Json(new { success = true, message = "Document Deleted Successfully!" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
            }
        }
        #endregion
        #region PURCHASE GRPO
        public async Task<IActionResult> CREATEGRPO([FromBody] PURCHASEGRPO data, string flag, string doctype)
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
                transaction = con.BeginTransaction(); // Transaction remains synchronous

                // Process Header
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

                    using (var cmd = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Purchase_GRPO_Head'", con, transaction))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                                header.Docnum = reader["Message"].ToString();
                        }
                    }

                    query = generator.GenerateInsertQuery(header, " [Purchase_GRPO_Head]", "DocEntry");
                }
                else
                {
                    query = generator.GenerateUpdateQuery(header, "[Purchase_GRPO_Head]", "DocEntry", header.DocEntry, "");
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
                    using (SqlCommand cmd = new SqlCommand($"" +
                        $@"
                        DELETE FROM Inventory_Stock where ObjType ='GRPO' AND DocEntry ='{header.DocEntry}'
                        DELETE FROM [Purchase_GRPO_Row] WHERE DocEntry = '{header.DocEntry}'

                        ", con, transaction))
                        await cmd.ExecuteNonQueryAsync();

                    int lineNum = 1;
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
                                        $"Invalid Tax Code '{line.TaxCode}' for Item Code {line.ItemCode} (Line No: {lineNum}). Only GST is allowed. ");
                                }
                            }
                            else
                            {
                                // For all other state codes: GST@ not allowed, IGST@ or UTGST@ is fine
                                if (line.TaxCode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{line.TaxCode}' for Item Code {line.ItemCode} (Line No: {lineNum}). Only IGST or UTGST Allowed.");
                                }
                            }
                        }
                        else
                        {
                            if (transaction != null) await transaction.RollbackAsync();
                            return StatusCode(500,
                                $"Missing Tax Code for Item Code {line.ItemCode} (Line No: {lineNum + 1}).");
                        }
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = header.DocEntry;
                        line.LineNum = lineNum;
                        line.CreatedDate = DateTime.Now;
                        line.CretedByUId = HttpContext.Session.GetString("UserID");
                        line.CretedByUName = HttpContext.Session.GetString("UserName");
                        line.ID = null;

                        string lineQuery = generator.GenerateInsertQuery(line, "[Purchase_GRPO_Row]", "ID");
                        using (SqlCommand cmd = new SqlCommand(lineQuery, con, transaction))
                            await cmd.ExecuteNonQueryAsync();

                        lineNum++;
                    }
                }

                // Process Freight
                if (data.freight != null && data.freight.Length > 0 && data.header.FreightTotal > 0)
                {
                    using (SqlCommand cmd = new SqlCommand($"DELETE FROM [Purchase_GRPO_Freight] WHERE DocEntry = '{header.DocEntry}'", con, transaction))
                        await cmd.ExecuteNonQueryAsync();

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

                        string lineQuery = generator.GenerateInsertQuery(line, "[Purchase_GRPO_Freight]", "ID");
                        using (SqlCommand cmd = new SqlCommand(lineQuery, con, transaction))
                            await cmd.ExecuteNonQueryAsync();
                    }
                }

                string Query = @$"EXEC UpdateOpenQuantity 'Purchase_PurchaseQuotation_Row','Purchase_PurchaseQuotation_Head','Purchase_GRPO_Head','Purchase_GRPO_Row','{header.DocEntry}'";
                using (SqlCommand cmd = new SqlCommand(Query, con, transaction))
                    await cmd.ExecuteNonQueryAsync();

                string Query1 = @$"EXEC UpdateOpenQuantity 'Purchase_PurchaseOrder_Row','Purchase_PurchaseOrder_Head','Purchase_GRPO_Row','Purchase_GRPO_Row','{header.DocEntry}'";
                using (SqlCommand cmd = new SqlCommand(Query1, con, transaction))
                    await cmd.ExecuteNonQueryAsync();

                await using (var cmdStock = new SqlCommand($"EXEC [Insert_InvtoryStock] '{header.DocEntry}','GRPO'", con, transaction))
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
        public async Task<IActionResult> DELETEPURCHGRPO(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Json(new { success = false, message = "ID cannot be empty" });

            string ConnectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    await con.OpenAsync();

                    int validation = 0;
                    int DocNum = 0;
                    string message = "This Document Has Reference ";

                    string[] queries = new string[]
                    {
                $@"exec [Document_CheckBaseDocEntry] 'Purchase_PurchaseGoodsReturn_Row','Purchase_PurchaseGoodsReturn_Head','{id}','GRPO'",
                $@"exec [Document_CheckBaseDocEntry] 'Purchase_PurchaseGoodsReturn_Row','Purchase_PurchaseGoodsReturn_Head','{id}','GRPO'",
                $@"exec [Document_CheckBaseDocEntry] 'Purchase_PurchaseInvoiceRow','Purchase_PurchaseInvoiceHead','{id}','GRPO'"
                    };

                    string[] messages = new string[]
                    {
                " In Purchase Return!, Document No ",
                " In Goods Return!, Document No ",
                " In Purchase Invoice!, Document No "
                    };

                    for (int i = 0; i < queries.Length; i++)
                    {
                        using (SqlCommand cmd = new SqlCommand(queries[i], con))
                        {
                            cmd.CommandTimeout = 300;
                            using (var reader = await cmd.ExecuteReaderAsync())
                            {
                                if (await reader.ReadAsync())
                                {
                                    validation = Convert.ToInt16(reader["RESULT"]);
                                    DocNum = Convert.ToInt16(reader["Docnum"]);
                                    if (validation > 0)
                                    {
                                        message += messages[i] + DocNum + " !";
                                        return Json(new { success = false, message = message });
                                    }
                                }
                            }
                        }
                    }

                    string query = @$"Exec [Delete_PurchaseGRPO]  '{id}'";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;
                        await cmd.ExecuteNonQueryAsync();
                    }

                    return Json(new { success = true, message = "Document Deleted Successfully!" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
            }
        }
        #endregion
        #region PURCHASE GOODS RETURN
        public async Task<IActionResult> CREATEPURRETURN([FromBody] PURCHASERETURN data, string flag, string doctype)
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

                // Process Header
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

                    using (var cmd = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Purchase_PurchaseGoodsReturn_Head'", con, transaction))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                                header.Docnum = reader["Message"].ToString();
                        }
                    }

                    query = generator.GenerateInsertQuery(header, " [Purchase_PurchaseGoodsReturn_Head]", "DocEntry");
                }
                else
                {
                    query = generator.GenerateUpdateQuery(header, "[Purchase_PurchaseGoodsReturn_Head]", "DocEntry", header.DocEntry, "");
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
                    using (SqlCommand cmd = new SqlCommand($@"
                            DELETE FROM Inventory_Stock where ObjType ='PGR' AND DocEntry ='{header.DocEntry}'
                            DELETE FROM [Purchase_PurchaseGoodsReturn_Row] WHERE DocEntry = '{header.DocEntry}'", con, transaction))
                        await cmd.ExecuteNonQueryAsync();

                    int lineNum = 1;
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
                                        $"Invalid Tax Code '{line.TaxCode}' for Item Code {line.ItemCode} (Line No: {lineNum}). Only GST is allowed. ");
                                }
                            }
                            else
                            {
                                // For all other state codes: GST@ not allowed, IGST@ or UTGST@ is fine
                                if (line.TaxCode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{line.TaxCode}' for Item Code {line.ItemCode} (Line No: {lineNum}). Only IGST or UTGST Allowed.");
                                }
                            }
                        }
                        else
                        {
                            if (transaction != null) await transaction.RollbackAsync();
                            return StatusCode(500,
                                $"Missing Tax Code for Item Code {line.ItemCode} (Line No: {lineNum + 1}).");
                        }
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = header.DocEntry;
                        line.LineNum = lineNum;
                        line.CreatedDate = DateTime.Now;
                        line.CretedByUId = HttpContext.Session.GetString("UserID");
                        line.CretedByUName = HttpContext.Session.GetString("UserName");
                        line.ID = null;

                        string lineQuery = generator.GenerateInsertQuery(line, "[Purchase_PurchaseGoodsReturn_Row]", "ID");
                        using (SqlCommand cmd = new SqlCommand(lineQuery, con, transaction))
                            await cmd.ExecuteNonQueryAsync();

                        lineNum++;
                    }
                }

                // Process Freight
                if (data.freight != null && data.freight.Length > 0 && data.header.FreightTotal > 0)
                {
                    using (SqlCommand cmd = new SqlCommand($"DELETE FROM [Purchase_PurchaseGoodsReturn_Freight] WHERE DocEntry = '{header.DocEntry}'", con, transaction))
                        await cmd.ExecuteNonQueryAsync();

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

                        string lineQuery = generator.GenerateInsertQuery(line, "[Purchase_PurchaseGoodsReturn_Freight]", "ID");
                        using (SqlCommand cmd = new SqlCommand(lineQuery, con, transaction))
                            await cmd.ExecuteNonQueryAsync();
                    }
                }

                string Query3 = @$"EXEC UpdateOpenQuantity 'Purchase_GRPO_Row','Purchase_GRPO_Head','Purchase_PurchaseGoodsReturn_Row','Purchase_PurchaseGoodsReturn_Row','{header.DocEntry}'";
                using (SqlCommand cmd = new SqlCommand(Query3, con, transaction))
                    await cmd.ExecuteNonQueryAsync();
                await using (var cmdStock = new SqlCommand($"EXEC [Insert_InvtoryStock] '{header.DocEntry}','PGR'", con, transaction))
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
         public async Task<IActionResult> DELETEPURCHRETURN(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Json(new { success = false, message = "ID cannot be empty" });

            string ConnectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    await con.OpenAsync();

                    int validation = 0;
                    int DocNum = 0;
                    string message = "This Document Has Reference ";

                    string Q1 = $@"exec [Document_CheckBaseDocEntry] 'Purchase_PurchaseDebitNote_Row','Purchase_PurchaseDebitNote_Head','{id}','PGR'";
                    using (SqlCommand cmd = new SqlCommand(Q1, con))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            validation = Convert.ToInt16(reader["RESULT"]);
                            DocNum = Convert.ToInt16(reader["Docnum"]);
                            if (validation > 0)
                            {
                                message += " In Purchase Invoice!, Document No " + DocNum + " !";
                                return Json(new { success = false, message = message });
                            }
                        }
                    }

                    string query = @$"  EXEC [Delete_PurchaseGoodsReturn] @id ";

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
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
            }
        }
        #endregion
        #region PURCHASE DEBIT NOTE
        public async Task<IActionResult> CREATEDEBITNOTE([FromBody] PURCHASEDEBITENOTE data, string flag, string doctype)
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

                // Process Header
                var header = data.header;
                header.UpdatedDate = DateTime.Now;
                header.UpdatedByUName = HttpContext.Session.GetString("UserName");
                header.UpdatedByUId = HttpContext.Session.GetString("UserID");
                header.AppliedAmount = 0;
                header.BalanceDue = header.NETTotal;
                Genrate_Query generator = new Genrate_Query();
                string query;

                if (string.IsNullOrEmpty(header.DocEntry))
                {
                    header.CreatedDate = DateTime.Now;
                    header.CretedByUId = HttpContext.Session.GetString("UserID");
                    header.CretedByUName = HttpContext.Session.GetString("UserName");
                    header.DocEntry = null;

                    using (var cmd = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Purchase_PurchaseDebitNote_Head'", con, transaction))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                                header.Docnum = reader["Message"].ToString();
                        }
                    }

                    query = generator.GenerateInsertQuery(header, " [Purchase_PurchaseDebitNote_Head]", "DocEntry");
                }
                else
                {
                    query = generator.GenerateUpdateQuery(header, "[Purchase_PurchaseDebitNote_Head]", "DocEntry", header.DocEntry, "");
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
                    using (SqlCommand cmd = new SqlCommand($@"
                            DELETE FROM Inventory_Stock where ObjType ='PDN' AND DocEntry ='{header.DocEntry}'
                            DELETE FROM [Purchase_PurchaseDebitNote_Row] WHERE DocEntry = '{header.DocEntry}'", con, transaction))
                        await cmd.ExecuteNonQueryAsync();

                    int lineNum = 1;
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
                                        $"Invalid Tax Code '{line.TaxCode}' for Item Code {line.ItemCode} (Line No: {lineNum}). Only GST is allowed. ");
                                }
                            }
                            else
                            {
                                // For all other state codes: GST@ not allowed, IGST@ or UTGST@ is fine
                                if (line.TaxCode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{line.TaxCode}' for Item Code {line.ItemCode} (Line No: {lineNum}). Only IGST or UTGST Allowed.");
                                }
                            }
                        }
                        else
                        {
                            if (transaction != null) await transaction.RollbackAsync();
                            return StatusCode(500,
                                $"Missing Tax Code for Item Code {line.ItemCode} (Line No: {lineNum + 1}).");
                        }
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = header.DocEntry;
                        line.LineNum = lineNum;
                        line.CreatedDate = DateTime.Now;
                        line.CretedByUId = HttpContext.Session.GetString("UserID");
                        line.CretedByUName = HttpContext.Session.GetString("UserName");
                        line.ID = null;

                        string lineQuery = generator.GenerateInsertQuery(line, "[Purchase_PurchaseDebitNote_Row]", "ID");
                        using (SqlCommand cmd = new SqlCommand(lineQuery, con, transaction))
                            await cmd.ExecuteNonQueryAsync();

                        lineNum++;
                    }
                }

                // Process Freight
                if (data.freight != null && data.freight.Length > 0 && data.header.FreightTotal > 0)
                {
                    using (SqlCommand cmd = new SqlCommand($"DELETE FROM [Purchase_PurchaseDebitNote_Freight] WHERE DocEntry = '{header.DocEntry}'", con, transaction))
                        await cmd.ExecuteNonQueryAsync();

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

                        string lineQuery = generator.GenerateInsertQuery(line, "[Purchase_PurchaseDebitNote_Freight]", "ID");
                        using (SqlCommand cmd = new SqlCommand(lineQuery, con, transaction))
                            await cmd.ExecuteNonQueryAsync();
                    }
                }

                string Query3 = @$"EXEC UpdateOpenQuantity 'Purchase_PurchaseGoodsReturn_Row','Purchase_PurchaseGoodsReturn_Head','Purchase_PurchaseDebitNote_Row','Purchase_PurchaseDebitNote_Row','{header.DocEntry}'";
                using (SqlCommand cmd = new SqlCommand(Query3, con, transaction))
                    await cmd.ExecuteNonQueryAsync();
                await using (var cmdStock = new SqlCommand($"EXEC [Insert_InvtoryStock] '{header.DocEntry}','PDN'", con, transaction))
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
                string jedocentry = "";
                // Use parameterized query to prevent SQL injection
                string query1 = @"SELECT DocEntry FROM Accounts_JournalEntry_Head  
                 WHERE BaseObjType = 'PDN' AND BaseDocEntry = @BaseDocEntry";

                using (SqlCommand cmd = new SqlCommand(query1, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    cmd.Parameters.AddWithValue("@BaseDocEntry", header.DocEntry);

                    // Use ExecuteScalarAsync for SELECT queries that return a single value
                    var result = await cmd.ExecuteScalarAsync();

                    if (result != null && result != DBNull.Value)
                    {
                        jedocentry = result.ToString();
                    }
                }

                // Only proceed with deletion if we found a DocEntry
                if (!string.IsNullOrEmpty(jedocentry))
                {
                    string query2 = @"
                     DELETE FROM Accounts_JournalEntry_Row WHERE DocEntry = @DocEntry
                     DELETE FROM Accounts_JournalEntry_Head WHERE DocEntry = @DocEntry;
                    ";

                    using (SqlCommand cmd = new SqlCommand(query2, con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@DocEntry", jedocentry);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                // Journal Entry
                await using (var cmdJournal = new SqlCommand($"EXEC [Create_PurchaseJournalEntry] '{header.DocEntry}','PDN'", con, transaction))
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
        public async Task<IActionResult> DELETEDEBITNOTE(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Json(new { success = false, message = "ID cannot be empty" });

            string ConnectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    await con.OpenAsync();

                    string query = @" Exec [Delete_PurchaseDebiteNote] @id";

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
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
            }
        }
        #endregion
        #region PURCHASE TAX INVOICE
        public async Task<IActionResult> CREATEPURCHINVOICE([FromBody] PURCHASEINVOICE data, string flag, string doctype)
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
                header.UpdatedDate = DateTime.Now;
                header.UpdatedByUName = HttpContext.Session.GetString("UserName");
                header.UpdatedByUId = HttpContext.Session.GetString("UserID");
                header.AppliedAmount = 0;
                header.BalanceDue = header.NETTotal;
                Genrate_Query generator = new Genrate_Query();
                string query;

                if (string.IsNullOrEmpty(header.DocEntry))
                {
                    header.CreatedDate = DateTime.Now;
                    header.CretedByUId = HttpContext.Session.GetString("UserID");
                    header.CretedByUName = HttpContext.Session.GetString("UserName");
                    header.DocEntry = null;

                    using (var cmd = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Purchase_PurchaseInvoiceHead'", con, transaction))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                                header.Docnum = reader["Message"].ToString();
                        }
                    }

                    query = generator.GenerateInsertQuery(header, "[Purchase_PurchaseInvoiceHead]", "DocEntry");
                }
                else
                {
                    query = generator.GenerateUpdateQuery(header, "[Purchase_PurchaseInvoiceHead]", "DocEntry", header.DocEntry, "");
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
                    using (SqlCommand cmd = new SqlCommand($@"
                            DELETE FROM Inventory_Stock where ObjType ='PI' AND DocEntry ='{header.DocEntry}'
                            DELETE FROM [Purchase_PurchaseInvoiceRow] WHERE DocEntry = '{header.DocEntry}'", con, transaction))
                        await cmd.ExecuteNonQueryAsync();

                    int lineNum =1;
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
                                        $"Invalid Tax Code '{line.TaxCode}' for Item Code {line.ItemCode} (Line No: {lineNum}). Only GST is allowed. ");
                                }
                            }
                            else
                            {
                                // For all other state codes: GST@ not allowed, IGST@ or UTGST@ is fine
                                if (line.TaxCode.StartsWith("GST@", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (transaction != null) await transaction.RollbackAsync();
                                    return StatusCode(500,
                                        $"Invalid Tax Code '{line.TaxCode}' for Item Code {line.ItemCode} (Line No: {lineNum}). Only IGST or UTGST Allowed.");
                                }
                            }
                        }
                        else
                        {
                            if (transaction != null) await transaction.RollbackAsync();
                            return StatusCode(500,
                                $"Missing Tax Code for Item Code {line.ItemCode} (Line No: {lineNum + 1}).");
                        }
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = header.DocEntry;
                        line.LineNum = lineNum;
                        line.CreatedDate = DateTime.Now;
                        line.CretedByUId = HttpContext.Session.GetString("UserID");
                        line.CretedByUName = HttpContext.Session.GetString("UserName");
                        line.ID = null;

                        string lineQuery = generator.GenerateInsertQuery(line, "[Purchase_PurchaseInvoiceRow]", "ID");
                        using (SqlCommand cmd = new SqlCommand(lineQuery, con, transaction))
                            await cmd.ExecuteNonQueryAsync();

                        lineNum++;
                    }
                }
                // Process Freight
                if (data.freight != null && data.freight.Length > 0 && data.header.FreightTotal > 0)
                {
                    using (SqlCommand cmd = new SqlCommand($"DELETE FROM [Purchase_PurchaseInvoiceFreight] WHERE DocEntry = '{header.DocEntry}'", con, transaction))
                        await cmd.ExecuteNonQueryAsync();

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

                        string lineQuery = generator.GenerateInsertQuery(line, "[Purchase_PurchaseInvoiceFreight]", "ID");
                        using (SqlCommand cmd = new SqlCommand(lineQuery, con, transaction))
                            await cmd.ExecuteNonQueryAsync();
                    }
                }
                string jedocentry = "";

                // Use parameterized query to prevent SQL injection
                string query1 = @"SELECT DocEntry FROM Accounts_JournalEntry_Head  
                 WHERE BaseObjType = 'PI' AND BaseDocEntry = @BaseDocEntry";

                using (SqlCommand cmd = new SqlCommand(query1, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    cmd.Parameters.AddWithValue("@BaseDocEntry", header.DocEntry);

                    // Use ExecuteScalarAsync for SELECT queries that return a single value
                    var result = await cmd.ExecuteScalarAsync();

                    if (result != null && result != DBNull.Value)
                    {
                        jedocentry = result.ToString();
                    }
                }

                // Only proceed with deletion if we found a DocEntry
                if (!string.IsNullOrEmpty(jedocentry))
                {
                    string query2 = @"
                     DELETE FROM Accounts_JournalEntry_Row WHERE DocEntry = @DocEntry
                     DELETE FROM Accounts_JournalEntry_Head WHERE DocEntry = @DocEntry;
                    "; 

                    using (SqlCommand cmd = new SqlCommand(query2, con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@DocEntry", jedocentry);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                // Update Open Quantity
                string[] updateQueries = new string[]
                {
                    @$"EXEC UpdateOpenQuantity 'Purchase_PurchaseQuotation_Row','Purchase_PurchaseQuotation_Head','Purchase_PurchaseInvoiceHead','Purchase_PurchaseInvoiceRow','{header.DocEntry}'",
                    @$"EXEC UpdateOpenQuantity 'Purchase_PurchaseOrder_Row','Purchase_PurchaseOrder_Head','Purchase_PurchaseInvoiceHead','Purchase_PurchaseInvoiceRow','{header.DocEntry}'",
                    @$"EXEC UpdateOpenQuantity 'Purchase_GRPO_Row','Purchase_GRPO_Head','Purchase_PurchaseInvoiceHead','Purchase_PurchaseInvoiceRow','{header.DocEntry}'"
                };
                foreach (var uq in updateQueries)
                {
                    using (SqlCommand cmd = new SqlCommand(uq, con, transaction))
                        await cmd.ExecuteNonQueryAsync();
                }
                if (header.ItemType == "I")
                {
                    await using (var cmdStock = new SqlCommand($"EXEC [Insert_InvtoryStock] '{header.DocEntry}','PI'", con, transaction))
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
                }
                // Journal Entry
                await using (var cmdJournal = new SqlCommand($"EXEC [Create_PurchaseJournalEntry] '{header.DocEntry}','PI'", con, transaction))
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
        public async Task<IActionResult> DELETEPURCHINVOICE(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Json(new { success = false, message = "ID cannot be empty" });

            string ConnectionString = _configuration.GetConnectionString("ErpConnection");

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    await con.OpenAsync();

                    int validation = 0;
                    int DocNum = 0;
                    string message = "This Document Has Reference ";
                    string Q1 = $@"exec [Document_CheckBaseDocEntry] 'Purchase_PurchaseGoodsReturn_Row','Purchase_PurchaseGoodsReturn_Head','{id}','GRPO'";
                    using (SqlCommand cmd = new SqlCommand(Q1, con))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            validation = Convert.ToInt16(reader["RESULT"]);
                            DocNum = Convert.ToInt16(reader["Docnum"]);
                            if (validation > 0)
                                return Json(new { success = false, message = message + " In Goods Return!, Document No " + DocNum + " !" });
                        }
                    }

                    string query = @"Exec [Delete_PurchaseTaxInvoice] @id  ";

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
                return StatusCode(500, new { success = false, message = ex.Message });
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

                var parkData = new Park
                {
                    OBJType = OBJType,
                    DocEntry = null,
                    CreatedDate = DateTime.Now,
                    CretedByUId = HttpContext.Session.GetString("UserID"),
                    CretedByUName = HttpContext.Session.GetString("UserName"),
                    Payload = JsonSerializer.Serialize(data),
                    RefNo = data.header?.RefNo,
                    DocumentDate = DateTime.Now
                };

                // Assuming ParKDocument can be made async; if not, you can wrap it in Task.Run
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
