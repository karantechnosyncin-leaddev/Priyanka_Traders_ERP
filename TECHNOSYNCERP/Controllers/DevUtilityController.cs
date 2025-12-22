using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TECHNOSYNCERP.Models;

namespace TECHNOSYNCERP.Controllers
{
    public class DevUtilityController : Controller
    {
        private readonly IConfiguration _configuration;
        public DevUtilityController(ILogger<DevUtilityController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult MenuCreation()
        {
            if  (HttpContext.Session.GetString("UserID") !="")
            {
               return View();
            }
            else
            {
                return RedirectToAction("Index", "Dashboard");
            }
        }
        public IActionResult ButtonMaster()
        {
            if (HttpContext.Session.GetString("UserID") != "" && HttpContext.Session.GetString("Role") == "A" || HttpContext.Session.GetString("Role") == "D")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> GLAccountMapping()
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
        public async Task<IActionResult> LayoutConfiguration()
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
        public async Task<IActionResult> GETBUTTONS()
        {
            try
            {
                string ConnectionString = _configuration.GetConnectionString("ErpConnection");
                string Query = "SELECT * FROM [btn]";
                List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();

                await using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    await con.OpenAsync();

                    await using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandTimeout = 300;

                        await using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                Dictionary<string, object> row = new Dictionary<string, object>();
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
        public async Task<IActionResult> GETBUTTONSBYID(string id)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var resultList = new List<Dictionary<string, string>>();
            string query = "SELECT * FROM [btn] WHERE BtnId = @BtnId";

            try
            {
                await using var con = new SqlConnection(connStr);
                await con.OpenAsync();

                await using var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@BtnId", id ?? (object)DBNull.Value);

                await using var rdr = await cmd.ExecuteReaderAsync();
                while (await rdr.ReadAsync())
                {
                    var dict = new Dictionary<string, string>();
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        dict[rdr.GetName(i)] = await rdr.IsDBNullAsync(i) ? "" : rdr[i]?.ToString();
                    }
                    resultList.Add(dict);
                }

                return Json(resultList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETMENU()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var menuList = new List<Dictionary<string, string>>();
            string query = "SELECT * FROM Menu";

            try
            {
                await using var con = new SqlConnection(connStr);
                await con.OpenAsync();

                await using var cmd = new SqlCommand(query, con);

                await using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var obj = new Dictionary<string, string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        obj[reader.GetName(i)] = await reader.IsDBNullAsync(i) ? "" : reader.GetValue(i)?.ToString();
                    }
                    menuList.Add(obj);
                }

                return Json(menuList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETLEDGER()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var menuList = new List<Dictionary<string, string>>();
            string query = "Select ID,GroupName FROM LedgerGroup where LedgerType =''";

            try
            {
                await using var con = new SqlConnection(connStr);
                await con.OpenAsync();

                await using var cmd = new SqlCommand(query, con);

                await using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var obj = new Dictionary<string, string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        obj[reader.GetName(i)] = await reader.IsDBNullAsync(i) ? "" : reader.GetValue(i)?.ToString();
                    }
                    menuList.Add(obj);
                }

                return Json(menuList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETGL()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var menuList = new List<Dictionary<string, string>>();
            string query = "Select * FROM [DefaultConfiguration]";

            try
            {
                await using var con = new SqlConnection(connStr);
                await con.OpenAsync();

                await using var cmd = new SqlCommand(query, con);

                await using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var obj = new Dictionary<string, string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        obj[reader.GetName(i)] = await reader.IsDBNullAsync(i) ? "" : reader.GetValue(i)?.ToString();
                    }
                    menuList.Add(obj);
                }

                return Json(menuList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> GETDLC()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var menuList = new List<Dictionary<string, string>>();
            string query = "Select * FROM [Layout_Configuration]";

            try
            {
                await using var con = new SqlConnection(connStr);
                await con.OpenAsync();

                await using var cmd = new SqlCommand(query, con);

                await using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var obj = new Dictionary<string, string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        obj[reader.GetName(i)] = await reader.IsDBNullAsync(i) ? "" : reader.GetValue(i)?.ToString();
                    }
                    menuList.Add(obj);
                }

                return Json(menuList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> UPDATEMENU([FromBody] MenuList data)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");

            await using var con = new SqlConnection(connStr);
            await con.OpenAsync();
            await using var transaction = await con.BeginTransactionAsync();

            try
            {
                foreach (var item in data.Menus)
                {
                    if (string.IsNullOrEmpty(item.MenuID))
                    {
                        // Insert new menu
                        var insertQuery = @"
                    INSERT INTO [Menu] 
                    ([MenuOrderID], [MenuName], [ParentID], [Controller], [Action], [Icon], [IsActive], [Role]) 
                    VALUES 
                    (@MenuOrderID, @MenuName, @ParentID, @Controller, @Action, @Icon, @IsActive, @Role)";

                        await using var cmd = new SqlCommand(insertQuery, con, (SqlTransaction)transaction);
                        cmd.Parameters.AddWithValue("@MenuOrderID", item.MenuOrderID);
                        cmd.Parameters.AddWithValue("@MenuName", (object)item.MenuName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ParentID", (object)item.ParentID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Controller", (object)item.Controller ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Action", (object)item.Action ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Icon", (object)item.Icon ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@IsActive", item.IsActive);
                        cmd.Parameters.AddWithValue("@Role", item.Role);

                        await cmd.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        // Update existing menu
                        var updateQuery = @"
                    UPDATE [Menu] SET 
                        [MenuOrderID] = @MenuOrderID,
                        [MenuName] = @MenuName,
                        [ParentID] = @ParentID,
                        [Controller] = @Controller,
                        [Action] = @Action,
                        [Icon] = @Icon,
                        [IsActive] = @IsActive,
                        [Role] = @Role
                    WHERE MenuID = @MenuID";

                        await using var cmd = new SqlCommand(updateQuery, con, (SqlTransaction)transaction);
                        cmd.Parameters.AddWithValue("@MenuOrderID", item.MenuOrderID);
                        cmd.Parameters.AddWithValue("@MenuName", (object)item.MenuName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ParentID", (object)item.ParentID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Controller", (object)item.Controller ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Action", (object)item.Action ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Icon", (object)item.Icon ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@IsActive", item.IsActive);
                        cmd.Parameters.AddWithValue("@Role", item.Role);
                        cmd.Parameters.AddWithValue("@MenuID", item.MenuID);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                await transaction.CommitAsync();
                return Json(new { success = true, message = "Menu updated successfully." });
            }
            catch (SqlException ex)
            {
                await transaction.RollbackAsync();
                return Json(new { success = false, message = "Database error: " + ex.Message });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Json(new { success = false, message = "Unexpected error: " + ex.Message });
            }
        }
        #region BUTTON
        public async Task<IActionResult> CREATEBTN([FromBody] BTN data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");
            Genrate_Query generator = new Genrate_Query();

            try
            {
                string query = "";
                if (string.IsNullOrEmpty(data.BtnID))
                {
                    data.BtnID = null;
                    query = generator.GenerateInsertQuery(data, "[Btn]", "BtnID");

                }
                else
                {
                    query = generator.GenerateUpdateQuery(data, "[Btn]", "BtnID", data.BtnID, "");
                }

                await using var con = new SqlConnection(connectionString);
                await con.OpenAsync();

                await using var cmd = new SqlCommand(query, con)
                {
                    CommandTimeout = 300
                };

                await cmd.ExecuteNonQueryAsync();

                return Json(new { success = true, message = "Document saved successfully." });
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

        public async Task<IActionResult> DELETEBTN(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Json(new { success = false, message = "BtnID is required." });

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            string query = "DELETE FROM [Btn] WHERE BtnID = @BtnID";

            try
            {
                await using var con = new SqlConnection(connectionString);
                await con.OpenAsync();

                await using var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@BtnID", id);

                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                if (rowsAffected == 0)
                    return Json(new { success = false, message = "No record found with the specified BtnID." });

                return Json(new { success = true, message = "Document deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion
        #region GL MAPING 
        public async Task<IActionResult> CREATEGL([FromBody] MainGl data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");
            Genrate_Query generator = new Genrate_Query();

            try
            {
                await using var con = new SqlConnection(connectionString);
                await con.OpenAsync();

                for (int i = 0; i < data.List.Length; i++)
                {
                    var item = data.List[i]; // ✅ Fixed

                    string query = string.IsNullOrEmpty(item.ID)
                        ? generator.GenerateInsertQuery(item, "[DefaultConfiguration]", "ID")
                        : generator.GenerateUpdateQuery(item, "[DefaultConfiguration]", "ID", item.ID, "");

                    await using var cmd = new SqlCommand(query, con)
                    {
                        CommandTimeout = 300
                    };

                    await cmd.ExecuteNonQueryAsync();
                }

                return Json(new { success = true, message = "Document saved successfully." });
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


        public async Task<IActionResult> DELETEGL(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Json(new { success = false, message = "ID is required." });

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            string query = "DELETE FROM [DefaultConfiguration] WHERE ID = @ID";

            try
            {
                await using var con = new SqlConnection(connectionString);
                await con.OpenAsync();

                await using var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", id);

                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                if (rowsAffected == 0)
                    return Json(new { success = false, message = "No record found with the specified ID." });

                return Json(new { success = true, message = "Document deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion
        #region Layout Configuration
        public async Task<IActionResult> CREATEDLC([FromBody] MainDLC data)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");
            Genrate_Query generator = new Genrate_Query();

            try
            {
                await using var con = new SqlConnection(connectionString);
                await con.OpenAsync();

                for (int i = 0; i < data.List.Length; i++)
                {
                    var item = data.List[i]; // ✅ Fixed

                    string query = string.IsNullOrEmpty(item.ID)
                        ? generator.GenerateInsertQuery(item, "[Layout_Configuration]", "ID")
                        : generator.GenerateUpdateQuery(item, "[Layout_Configuration]", "ID", item.ID, "");

                    await using var cmd = new SqlCommand(query, con)
                    {
                        CommandTimeout = 300
                    };

                    await cmd.ExecuteNonQueryAsync();
                }

                return Json(new { success = true, message = "Document saved successfully." });
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


        public async Task<IActionResult> DELETEDLC(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Json(new { success = false, message = "ID is required." });

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            string query = "DELETE FROM [Layout_Configuration] WHERE ID = @ID";

            try
            {
                await using var con = new SqlConnection(connectionString);
                await con.OpenAsync();

                await using var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", id);

                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                if (rowsAffected == 0)
                    return Json(new { success = false, message = "No record found with the specified ID." });

                return Json(new { success = true, message = "Document deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion
    }
}
