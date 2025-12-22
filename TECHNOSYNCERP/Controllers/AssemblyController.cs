using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TECHNOSYNCERP.Models;

namespace TECHNOSYNCERP.Controllers
{
    public class AssemblyController : Controller
    {
        private readonly IConfiguration _configuration;
        public AssemblyController(ILogger<AssemblyController> logger, IConfiguration configuration)
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
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult FirstVisualData()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = GETBTNAUTH("AFVD");
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
        public IActionResult FinalVisualData()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = GETBTNAUTH("AFIVD");
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
        public IActionResult InprogressRejectionRecord()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = GETBTNAUTH("AIRR");
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
        public IActionResult DeflashingProcessRejection()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = GETBTNAUTH("AIRR");
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
        public IActionResult GETFIRSTVISUALROW(string FromDate, string Todate)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            List<Dictionary<string, string>> List = new List<Dictionary<string, string>>();

            try
            {
                using (var con = new SqlConnection(connStr))
                {
                    string query = @$"SELECT *, FORMAT([Date], 'yyyy-MM-dd') AS [Date]  FROM [Assembly_FirstVisualData_Row] WHERE [Date] BETWEEN '{FromDate}' AND '{Todate}';";
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
        public IActionResult GETFINALVISUALDATAROW(string FromDate, string Todate)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            List<Dictionary<string, string>> List = new List<Dictionary<string, string>>();

            try
            {
                using (var con = new SqlConnection(connStr))
                {
                    string query = @$"SELECT *, FORMAT([Date], 'yyyy-MM-dd') AS [Date]  FROM [Assembly_FinalVisualData_Row] WHERE [Date] BETWEEN '{FromDate}' AND '{Todate}';";
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
        public IActionResult GETINPROGRESSREJROW(string FromDate, string Todate)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            List<Dictionary<string, string>> List = new List<Dictionary<string, string>>();

            try
            {
                using (var con = new SqlConnection(connStr))
                {
                    string query = @$"SELECT *, FORMAT([Date], 'yyyy-MM-dd') AS [Date]  FROM [Assembly_FinalVisualData_Row] WHERE [Date] BETWEEN '{FromDate}' AND '{Todate}';";
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
        public IActionResult GETDEFLASHINGPROROW(string FromDate, string Todate)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            List<Dictionary<string, string>> List = new List<Dictionary<string, string>>();

            try
            {
                using (var con = new SqlConnection(connStr))
                {
                    string query = @$"SELECT *, FORMAT([Date], 'yyyy-MM-dd') AS [Date]  FROM [Assembly_DeflashProcesRejRecord_Row] WHERE [Date] BETWEEN '{FromDate}' AND '{Todate}';";
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
        public IActionResult GETINPROGRESSROW(string FromDate, string Todate)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            List<Dictionary<string, string>> List = new List<Dictionary<string, string>>();

            try
            {
                using (var con = new SqlConnection(connStr))
                {
                    string query = @$"SELECT *, FORMAT([Date], 'yyyy-MM-dd') AS [Date]  FROM [Assembly_InprogressProcesRejRecord_Row] WHERE [Date] BETWEEN '{FromDate}' AND '{Todate}';";
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
        public IActionResult COPYFROM(string FromDate, string Todate)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            List<Dictionary<string, string>> List = new List<Dictionary<string, string>>();

            try
            {
                using (var con = new SqlConnection(connStr))
                {
                    string query = @$"Exec [GET_FirstVisualData] '{FromDate}' , '{Todate}'";
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
        [HttpPost]

        #region FIRST VISUAL DATA
        public IActionResult CREATEFIRSTVISUALDATA([FromBody] AssemblyFirstVisualData data)
        {
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

                    query = generator.GenerateInsertQuery(header, " [Assembly_FirstVisualData_Head]", "DocEntry");
                }
                else
                {
                    query = generator.GenerateUpdateQuery(header, "[Assembly_FirstVisualData_Head]", "DocEntry", header.DocEntry, "");
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
                            line.CreatedDate = DateTime.Now;
                            line.CretedByUId = HttpContext.Session.GetString("UserID");
                            line.CretedByUName = HttpContext.Session.GetString("UserName");
                            line.ID = null;
                            lineQuery = generator.GenerateInsertQuery(line, "[Assembly_FirstVisualData_Row]", "ID");
                        }
                        else
                        {
                            lineQuery = generator.GenerateUpdateQuery(line, "[Assembly_FirstVisualData_Row]", "ID", line.ID, "");
                        }
                        using (SqlCommand cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                transaction.Commit();
                return Json(new { Success = true, DocEntry = data.header.DocEntry, Message = "Document saved successfully." });
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
        public IActionResult DELETEFIRSTVISUALDATA(string id)
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
                    con.Open();
                    int validation = 0;
                    int DocNum = 0;
                    string message = "This Document Has Reference ";

                    string query = @"
                        DELETE from [Assembly_FirstVisualData_Row] where ID = @id;
                    ";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                    return Json(new { success = true, message = "Record Deleted Successfully!" });
                }
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
            }
        }
        #endregion
        #region FINAL VISUAL DATA
        public IActionResult CREATEFINELVISUALDATA([FromBody] AssemblyFinalVisualData data)
        {
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

                    query = generator.GenerateInsertQuery(header, " [Assembly_FinalVisualData_Head]", "DocEntry");
                }
                else
                {
                    query = generator.GenerateUpdateQuery(header, "[Assembly_FinalVisualData_Head]", "DocEntry", header.DocEntry, "");
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
                            line.CreatedDate = DateTime.Now;
                            line.CretedByUId = HttpContext.Session.GetString("UserID");
                            line.CretedByUName = HttpContext.Session.GetString("UserName");
                            line.ID = null;
                            lineQuery = generator.GenerateInsertQuery(line, "[Assembly_FinalVisualData_Row]", "ID");
                        }
                        else
                        {
                            lineQuery = generator.GenerateUpdateQuery(line, "[Assembly_FinalVisualData_Row]", "ID", line.ID, "");
                        }
                        using (SqlCommand cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                transaction.Commit();
                return Json(new { Success = true, DocEntry = data.header.DocEntry, Message = "Document saved successfully." });
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
        public IActionResult DELETEFINELVISUALDATA(string id)
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
                    con.Open();
                    int validation = 0;
                    int DocNum = 0;
                    string message = "This Document Has Reference ";

                    string query = @"
                        DELETE from [Assembly_FinalVisualData_Row] where ID = @id;
                    ";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                    return Json(new { success = true, message = "Record Deleted Successfully!" });
                }
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
            }
        }
        #endregion
        #region DEFLASHING PROCESS REJECTION 
        public IActionResult CREATEDEFLASHINGREJEC([FromBody] DEFPROGRESSREJECTIONRECORD data)
        {
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

                    query = generator.GenerateInsertQuery(header, " [Assembly_DeflashProcesRejRecord_Head]", "DocEntry");
                }
                else
                {
                    query = generator.GenerateUpdateQuery(header, "[Assembly_DeflashProcesRejRecord_Head]", "DocEntry", header.DocEntry, "");
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
                            line.CreatedDate = DateTime.Now;
                            line.CretedByUId = HttpContext.Session.GetString("UserID");
                            line.CretedByUName = HttpContext.Session.GetString("UserName");
                            line.ID = null;
                            lineQuery = generator.GenerateInsertQuery(line, "[Assembly_DeflashProcesRejRecord_Row]", "ID");
                        }
                        else
                        {
                            lineQuery = generator.GenerateUpdateQuery(line, "[Assembly_DeflashProcesRejRecord_Row]", "ID", line.ID, "");
                        }
                        using (SqlCommand cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                transaction.Commit();
                return Json(new { Success = true, DocEntry = data.header.DocEntry, Message = "Document saved successfully." });
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
        public IActionResult DELETEDEFREJREC(string id)
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
                    con.Open();
                    int validation = 0;
                    int DocNum = 0;
                    string message = "This Document Has Reference ";

                    string query = @"
                        DELETE from [Assembly_DeflashProcesRejRecord_Row] where ID = @id;
                    ";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                    return Json(new { success = true, message = "Record Deleted Successfully!" });
                }
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
            }
        }
        #endregion
        #region INPROGRESS PROCESS REJECTION 
        public IActionResult CREATEINPROGRESSREJEC([FromBody] INFPROGRESSREJECTIONRECORD data)
        {
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

                    query = generator.GenerateInsertQuery(header, " [Assembly_InprogressProcesRejRecord_Head]", "DocEntry");
                }
                else
                {
                    query = generator.GenerateUpdateQuery(header, "[Assembly_InprogressProcesRejRecord_Head]", "DocEntry", header.DocEntry, "");
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
                            line.CreatedDate = DateTime.Now;
                            line.CretedByUId = HttpContext.Session.GetString("UserID");
                            line.CretedByUName = HttpContext.Session.GetString("UserName");
                            line.ID = null;
                            lineQuery = generator.GenerateInsertQuery(line, "[Assembly_InprogressProcesRejRecord_Row]", "ID");
                        }
                        else
                        {
                            lineQuery = generator.GenerateUpdateQuery(line, "[Assembly_InprogressProcesRejRecord_Row]", "ID", line.ID, "");
                        }
                        using (SqlCommand cmd = new SqlCommand(lineQuery, con, transaction))
                        {
                            cmd.CommandTimeout = 300;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                transaction.Commit();
                return Json(new { Success = true, DocEntry = data.header.DocEntry, Message = "Document saved successfully." });
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
        public IActionResult DELETEINPROGRESSREJREC(string id)
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
                    con.Open();
                    int validation = 0;
                    int DocNum = 0;
                    string message = "This Document Has Reference ";

                    string query = @"
                        DELETE from [Assembly_InprogressProcesRejRecord_Row] where ID = @id;
                    ";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                    return Json(new { success = true, message = "Record Deleted Successfully!" });
                }
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
            }
        }
        #endregion
    }
}
