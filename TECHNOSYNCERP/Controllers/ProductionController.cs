using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using TECHNOSYNCERP.Models;

namespace TECHNOSYNCERP.Controllers
{
    public class ProductionController : Controller
    {
        private readonly IConfiguration _configuration;
        public ProductionController(ILogger<ProductionController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult GETBTNAUTH(string objtype)
        {
            try
            {
                string ConnectionString = _configuration.GetConnectionString("ErpConnection");
                string Query = @$"Select * from BtnAuth T0
inner join Btn T1 on T1 .BtnID =T0.BtnID
Where  T0.ObjType ='{objtype}' and T0.UserID='{HttpContext.Session.GetString("UserID")}'";
                List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        con.Open();
                        cmd.CommandText = Query;
                        cmd.CommandTimeout = 300;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        {
                            while (rdr.Read())
                            {
                                Dictionary<string, object> row = new Dictionary<string, object>();
                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object? value = rdr.IsDBNull(i) ? "" : rdr.GetValue(columnName);
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
                        con.Close();
                    }
                }
                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }
        public IActionResult Bom()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = GETBTNAUTH("BOM");
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
        public IActionResult ProductionOrder()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = GETBTNAUTH("PO");
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
        public IActionResult BOMFINDMODE()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            List<Dictionary<string, string>> List = new List<Dictionary<string, string>>();

            try
            {
                using (var con = new SqlConnection(connStr))
                {
                    string query = @"Select * FROM [Master_BillOfMaterialHead] order by BOMID desc";
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
                return StatusCode(500, new { error = ex.Message });
            }
        } 
        public IActionResult GETBOMROW(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            List<Dictionary<string, string>> headlist = new List<Dictionary<string, string>>();
            List<Dictionary<string, string>> rowlist = new List<Dictionary<string, string>>();

            var data = new {
                head = headlist,
                row= rowlist
            };

            try
            {
                using (var con = new SqlConnection(connStr))
                {
                    con.Open();
                    
                    using (var cmd = new SqlCommand($"Select * FROM [Master_BillOfMaterialHead] where BOMID='{id}'", con))
                    {
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
                                    headlist.Add(obj);
                                }
                            }
                        }
                    }
                    string query = @$"Select * FROM [Master_BillOfMaterialRow] where BOMID='{id}' ";
                    using (var cmd = new SqlCommand(query, con))
                    {
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
                                    rowlist.Add(obj);
                                }
                            }
                        }
                    }
                }
                return Json(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        public IActionResult GETITEMBYID(string id)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = @$"EXEC ItemMasterList '', '{id}'";
                var dataList = new List<Dictionary<string, object>>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // 1. Check if item exists in BOM
                    using (SqlCommand checkCmd = new SqlCommand("SELECT 1 FROM [Master_BillOfMaterialHead] WHERE [ItemID] = @ItemID", con))
                    {
                        checkCmd.Parameters.AddWithValue("@ItemID", id);
                        using (SqlDataReader rdr = checkCmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                return Json(new { success = false, message = "This Item Already In Bill Of Material." });
                            }
                        }
                    }

                    // 2. If not in BOM, execute stored procedure
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var row = new Dictionary<string, object>();
                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object value = rdr.IsDBNull(i) ? "" : rdr.GetValue(i);

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

                return Json(new { success = true, message = dataList });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public IActionResult GETITEMFROMBOM(string id)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = @$"  Select * FROM  Master_BillOfMaterialHead where BOMID ='{id}'";
                var dataList = new List<Dictionary<string, object>>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // 2. If not in BOM, execute stored procedure
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var row = new Dictionary<string, object>();
                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object value = rdr.IsDBNull(i) ? "" : rdr.GetValue(i);

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

                return Json(new { success = true, message = dataList });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public IActionResult GETITEMPRODUCTIONORDER()
        {
            try
            {
                string ConnectionString = _configuration.GetConnectionString("ErpConnection");
                string Query = @$"SELECT * FROM Master_BillOfMaterialHead T0";
                List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        con.Open();
                        cmd.CommandText = Query;
                        cmd.CommandTimeout = 300;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        {
                            while (rdr.Read())
                            {
                                Dictionary<string, object> row = new Dictionary<string, object>();
                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object? value = rdr.IsDBNull(i) ? "" : rdr.GetValue(columnName);
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
                        con.Close();
                    }
                }
                return Json(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }           
        public IActionResult GETPRODORDERDOCNUM(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            List<Dictionary<string, string>> List = new List<Dictionary<string, string>>();

            try
            {
                using (var con = new SqlConnection(connStr))
                {
                    string query = @$"EXEC GET_NextDocNumber '{id}','Prodution_ProdutionOrderHead'";
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
        public IActionResult FINDMODE(string doctype, string docid, string refno, string fromdate, string todate, string docEntry)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            List<Dictionary<string, string>> header = new List<Dictionary<string, string>>();
            List<Dictionary<string, string>> row = new List<Dictionary<string, string>>();
            var newobj = new
            {
                header = header,
                row = row,
            };
            try
            {
                using (var con = new SqlConnection(connStr))
                {
                    string query = @$"EXEC [FindDocHeader] '{doctype}','{docEntry}','{docid}','{refno}','{fromdate}','{todate}','',''";
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
                                    header.Add(obj);
                                }
                            }
                        }
                    }
                    if (docEntry != "" || docEntry != null)
                    {
                        string query1 = @$"EXEC [FindDocRow] '{doctype}','{docEntry}'";
                        using (var cmd = new SqlCommand(query1, con))
                        {
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
                                        row.Add(obj);
                                    }
                                }
                            }
                        }
                    }
                }
                return Json(newobj);
            }
            catch (Exception ex)
            {
                // Log exception here if using ILogger
                return StatusCode(500, new { error = ex.Message });
            }
        }
        [HttpPost]

        #region CREATE BOM
        public IActionResult CREATEBOM([FromBody] BOM data, string flag, string doctype)
        {
            if (flag == "P")
            {
                return PARK(data, doctype);
            }

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                transaction = con.BeginTransaction();

                // Process Header
                var header = data.header;
                // Common audit fields
                header.UpdatedDate = DateTime.Now;
                header.UpdatedByUName = HttpContext.Session.GetString("UserName");
                header.UpdatedByUId = HttpContext.Session.GetString("UserID");

                Genrate_Query generator = new Genrate_Query();
                string query;

                if (string.IsNullOrEmpty(header.BOMID))
                {
                    // New record
                    header.CreatedDate = DateTime.Now;
                    header.CretedByUId = HttpContext.Session.GetString("UserID");
                    header.CretedByUName = HttpContext.Session.GetString("UserName");
                    header.BOMID = null;
                    query = generator.GenerateInsertQuery(header, "[Master_BillOfMaterialHead]", "BOMID");
                }
                else
                {
                    // Update record
                    query = generator.GenerateUpdateQuery(header, "[Master_BillOfMaterialHead]", "BOMID", header.BOMID, "");
                }

                using (SqlCommand cmd = new SqlCommand(query, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    cmd.ExecuteNonQuery();

                    // Get the ID if it was an insert
                    if (string.IsNullOrEmpty(header.BOMID))
                    {
                        cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                        header.BOMID = cmd.ExecuteScalar().ToString();
                    }
                }

                // Process Lines only if they exist
                if (data.lines != null && data.lines.Length > 0)
                {
                    for (int i = 0; i < data.lines.Length; i++)
                    {
                        var line = data.lines[i];
                        // Common audit fields
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.BOMID = data.header.BOMID;

                        string lineQuery;

                        if (string.IsNullOrEmpty(line.ID))
                        {
                            // New record
                            line.CreatedDate = DateTime.Now;
                            line.CretedByUId = HttpContext.Session.GetString("UserID");
                            line.CretedByUName = HttpContext.Session.GetString("UserName");
                            line.ID = null;
                            lineQuery = generator.GenerateInsertQuery(line, "[Master_BillOfMaterialRow]", "ID");
                        }
                        else
                        {
                            lineQuery = generator.GenerateUpdateQuery(line, "[Master_BillOfMaterialRow]", "ID", line.ID, "");
                        }

                        using (SqlCommand cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                transaction.Commit();
                return Json(new { Success = true, DocEntry = data.header.BOMID, Message = "Document Saved Succesfully...!" });
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
                con?.Close();
                con?.Dispose();
            }
        }
        public IActionResult DELETEBOM(string id)
        {
            string ConnectionString = _configuration.GetConnectionString("ErpConnection");
            try
            {


                string Query = "Delete from [Master_BillOfMaterialRow] where BOMID='" + id + "' Delete from [Master_BillOfMaterialHead] where BOMID='" + id + "' ";
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
                return Json(new { success = true, message = " BOM  Deleted Successfully..!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }
        public IActionResult DELETEBOMITEM(string id)
        {
            string ConnectionString = _configuration.GetConnectionString("ErpConnection");
            try
            {

                string Query = "Delete from [Master_BillOfMaterialRow] where ID='" + id + "' ";
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
        #region CREATE PRODUCTION ORDER 
        public IActionResult CREATEPRODUCTIONORDER([FromBody] PRODUCTIONORDER data, string flag, string doctype)
        {
            if (flag == "P")
            {
                return PARK(data, doctype);
            }
            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                transaction = con.BeginTransaction();

                // Process Header
                var header = data.header;
                // Common audit fields
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

                    using (var cmd = new SqlCommand(@$"EXEC GET_NextDocNumber '{header.FYearId}','Prodution_ProdutionOrderHead'", con, transaction))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    header.Docnum = reader["Message"].ToString();
                                }
                            }
                        }
                    }
                    query = generator.GenerateInsertQuery(header, "[Prodution_ProdutionOrderHead]", "DocEntry");
                }
                else
                {
                    // Update record
                    query = generator.GenerateUpdateQuery(header, "[Prodution_ProdutionOrderHead]", "DocEntry", header.DocEntry, "");
                }

                using (SqlCommand cmd = new SqlCommand(query, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    cmd.ExecuteNonQuery();

                    // Get the ID if it was an insert
                    if (string.IsNullOrEmpty(header.DocEntry))
                    {
                        cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                        header.DocEntry = cmd.ExecuteScalar().ToString();
                    }
                }

                // Process Lines only if they exist
                if (data.lines != null && data.lines.Length > 0)
                {
                    for (int i = 0; i < data.lines.Length; i++)
                    {
                        var line = data.lines[i];
                        // Common audit fields
                        line.UpdatedDate = DateTime.Now;
                        line.UpdatedByUName = HttpContext.Session.GetString("UserName");
                        line.UpdatedByUId = HttpContext.Session.GetString("UserID");
                        line.DocEntry = data.header.DocEntry;

                        string lineQuery;

                        if (string.IsNullOrEmpty(line.ID))
                        {
                            // New record
                            line.CreatedDate = DateTime.Now;
                            line.CretedByUId = HttpContext.Session.GetString("UserID");
                            line.CretedByUName = HttpContext.Session.GetString("UserName");
                            line.ID = null;
                            lineQuery = generator.GenerateInsertQuery(line, "[Prodution_ProdutionOrderRow]", "ID");
                        }
                        else
                        {
                            lineQuery = generator.GenerateUpdateQuery(line, "[Prodution_ProdutionOrderRow]", "ID", line.ID, "");
                        }

                        using (SqlCommand cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                if(data.header.Status=="R"){
                    using (SqlCommand cmd = new SqlCommand($"EXEC [Insert_InvtoryStock] '{data.header.DocEntry}','PROD'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        using (var rdr = cmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    // Assuming column names are "Success" and "Message"
                                    string success = rdr["Success"].ToString();
                                    string message = rdr["Message"].ToString();

                                    if (!string.Equals(success, "true", StringComparison.OrdinalIgnoreCase))
                                    {
                                        transaction?.Rollback();
                                        return StatusCode(500, $"An error occurred: {message}");
                                    }
                                }
                            }
                        }
                    }
                    using (SqlCommand cmd = new SqlCommand($"Update  Prodution_ProdutionOrderHead  Set DocStatus='C' ,Status ='C'", con, transaction))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.ExecuteNonQuery();
                    }
                }
                
                transaction.Commit();
                return Json(new { Success = true, DocEntry = data.header.DocEntry, Message = "Document Saved Succesfully...!" });
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
                con?.Close();
                con?.Dispose();
            }
        }
        public IActionResult DELETEPRODUCTIONORDER(string id)
        {
            string ConnectionString = _configuration.GetConnectionString("ErpConnection");
            try
            {
                string Query = "Delete From Attachments where ObjType ='PROD' and DocId='" + id + "' Delete from [Prodution_ProdutionOrderRow] where DocEntry='" + id + "' Delete from [Prodution_ProdutionOrderHead] where DocEntry='" + id + "' ";
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
                return Json(new { success = true, message = " Production Order  Deleted Successfully..!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }
        public IActionResult DELETEPRODUCTIONORDERITEM(string id)
        {
            string ConnectionString = _configuration.GetConnectionString("ErpConnection");
            try
            {

                string Query = "Delete from [Prodution_ProdutionOrderRow] where ID='" + id + "' ";
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
        #region PARK DOC
        public IActionResult PARK([FromBody] dynamic data, string OBJType)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");
            SqlTransaction transaction = null;
            SqlConnection con = null;
            try
            {
                ParkDoc Gnrator = new ParkDoc();
                var parkdata = new Park() { };
                parkdata.OBJType = OBJType;
                parkdata.DocEntry = null;
                parkdata.CreatedDate = DateTime.Now;
                parkdata.CretedByUId = HttpContext.Session.GetString("UserID");
                parkdata.CretedByUName = HttpContext.Session.GetString("UserName");
                parkdata.Payload = JsonSerializer.Serialize(data).ToString();
                parkdata.RefNo = data.header.ItemCode;
                parkdata.DocumentDate = DateTime.Now;
                return Json(Gnrator.ParKDocument(parkdata, connectionString));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        #endregion
    }
}
