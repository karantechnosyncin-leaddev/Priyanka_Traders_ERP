using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TECHNOSYNCERP.Models;

namespace TECHNOSYNCERP.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        public AuthController(ILogger<AuthController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> GETBTNAUTH1(string objtype)
        {
            try
            {
                string ConnectionString = _configuration.GetConnectionString("ErpConnection");
                string Query = @$"SELECT * 
                          FROM BtnAuth T0
                          INNER JOIN Btn T1 ON T1.BtnID = T0.BtnID
                          WHERE T0.ObjType = @ObjType 
                            AND T0.UserID = @UserID";

                List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();

                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.Parameters.AddWithValue("@ObjType", objtype ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@UserID", HttpContext.Session.GetString("UserID") ?? (object)DBNull.Value);

                        await con.OpenAsync();
                        cmd.CommandTimeout = 300;

                        using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
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
                return StatusCode(500, ex.Message.ToString());
            }
        }

        public async Task<IActionResult> Users()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH1("USER");
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
        public async Task<IActionResult> MenuAuth()
        {
            if (HttpContext.Session.GetString("UserID") != "")
            {
                ViewBag.BtnAuth = null;
                var responce = await GETBTNAUTH1("MA");
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
        public IActionResult GenLicense()
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
        public IActionResult ButtonAuth()
        {
            if (HttpContext.Session.GetString("UserID") != "" )
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> GETUSERS()
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            List<Dictionary<string, string>> List = new List<Dictionary<string, string>>();

            try
            {
                await using (var con = new SqlConnection(connStr))
                {
                    string query;
                    if (HttpContext.Session.GetString("Role") == "U")
                    {
                        query = @"
                    SELECT  
                        [UserID],
                        [Username],
                        [Password],
                        [FullName],
                        [Email],
                        [Mobile],
                        [Role],
                        [IsActive],
                        CONVERT(varchar, [LastLogin], 23) AS [LastLogin],
                        [CreatedBy],
                        CONVERT(varchar, [CreatedDate], 23) AS [CreatedDate],
                        [UpdatedBy],
                        CONVERT(varchar, [UpdatedDate], 23) AS [UpdatedDate],
                        CONVERT(varchar, [LicenseValidFrom], 23) AS [LicenseValidFrom],
                        CONVERT(varchar, [LicenseValidTo], 23) AS [LicenseValidTo],
                        [LicenseStatus],
                        CONVERT(varchar, [LicenseGenDate], 23) AS [LicenseGenDate],
                        [Licencekey],
                        [LoginStatus]
                    FROM [Users] 
                    WHERE Role = 'U'";
                    }
                    else
                    {
                        query = @"
                    SELECT  
                        [UserID],
                        [Username],
                        [Password],
                        [FullName],
                        [Email],
                        [Mobile],
                        [Role],
                        [IsActive],
                        CONVERT(varchar, [LastLogin], 23) AS [LastLogin],
                        [CreatedBy],
                        CONVERT(varchar, [CreatedDate], 23) AS [CreatedDate],
                        [UpdatedBy],
                        CONVERT(varchar, [UpdatedDate], 23) AS [UpdatedDate],
                        CONVERT(varchar, [LicenseValidFrom], 23) AS [LicenseValidFrom],
                        CONVERT(varchar, [LicenseValidTo], 23) AS [LicenseValidTo],
                        [LicenseStatus],
                        CONVERT(varchar, [LicenseGenDate], 23) AS [LicenseGenDate],
                        [Licencekey],
                        [LoginStatus]
                    FROM [Users]";
                    }

                    await using (var cmd = new SqlCommand(query, con))
                    {
                        await con.OpenAsync();

                        await using (var reader = await cmd.ExecuteReaderAsync())
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
                                    List.Add(obj);
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                return Json(new { success = false, message = "Database error: " + ex.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Unexpected error: " + ex.Message });
            }

            return Json(List);
        }
        public async Task<IActionResult> GetUsersAuth(string userId)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var resultList = new List<Dictionary<string, string>>();

            string query;
            if (HttpContext.Session.GetString("Role") == "U")
            {
                query = @"
            SELECT 
                T2.MenuName,
                T2.MenuID, 
                T2.ParentID, 
                T2.Controller, 
                T2.Action, 
                T2.Icon, 
                T2.MenuOrderID,
                T1.Username, 
                T1.Role, 
                COALESCE(T0.Auth, 'N') AS Auth
            FROM Menu T2
            LEFT JOIN MenuAuth T0 ON T0.MenuId = T2.MenuId AND T0.UserID = @UserID
            LEFT JOIN Users T1 ON T0.UserID = T1.UserID
            WHERE T2.Role = 'B'";
            }
            else
            {
                query = @"
            SELECT 
                T2.MenuName,
                T2.MenuID, 
                T2.ParentID, 
                T2.Controller, 
                T2.Action, 
                T2.Icon, 
                T2.MenuOrderID,
                T1.Username, 
                T1.Role, 
                COALESCE(T0.Auth, 'N') AS Auth
            FROM Menu T2
            LEFT JOIN MenuAuth T0 ON T0.MenuId = T2.MenuId AND T0.UserID = @UserID 
            LEFT JOIN Users T1 ON T0.UserID = T1.UserID AND T1.Role = 'U'";
            }

            try
            {
                await using (var con = new SqlConnection(connStr))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = userId;

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var dict = new Dictionary<string, string>();
                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    dict[rdr.GetName(i)] = await rdr.IsDBNullAsync(i) ? null : rdr[i]?.ToString();
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
        public async Task<IActionResult> GETBTNAUTH(string userId, string menuId)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var resultList = new List<Dictionary<string, string>>();

            string query;
            if (HttpContext.Session.GetString("Role") == "U")
            {
                query = @"
            SELECT 
                T0.BtnID,
                T0.Icon,
                T0.ColorClass,
                T0.BtnName,
                T1.AuthID,
                ISNULL(T1.Auth, 'N') AS Auth,
                T1.MenuID
            FROM Btn T0
            LEFT JOIN BtnAuth T1 
                ON T0.BtnID = T1.BtnID 
               AND T1.UserID = @UserID 
               AND T1.MenuID = @MenuID
            WHERE T0.Role = 'B'";
            }
            else
            {
                query = @"
            SELECT 
                T0.BtnID,
                T0.Icon,
                T0.ColorClass,
                T0.BtnName,
                T1.AuthID,
                ISNULL(T1.Auth, 'N') AS Auth,
                T1.MenuID
            FROM Btn T0
            LEFT JOIN BtnAuth T1 
                ON T0.BtnID = T1.BtnID 
               AND T1.UserID = @UserID 
               AND T1.MenuID = @MenuID";
            }

            try
            {
                await using (var con = new SqlConnection(connStr))
                {
                    await con.OpenAsync();

                    await using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = userId;
                        cmd.Parameters.Add("@MenuID", SqlDbType.NVarChar).Value = menuId;

                        await using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var dict = new Dictionary<string, string>();
                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    dict[rdr.GetName(i)] = await rdr.IsDBNullAsync(i) ? null : rdr[i]?.ToString();
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

        public async Task<IActionResult> UPDATEKEY(string Key, string UserId)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");

            await using (var con = new SqlConnection(connStr))
            {
                await con.OpenAsync();
                await using (var transaction = await con.BeginTransactionAsync())
                {
                    var updateQuery = @"
                UPDATE Users 
                SET Licencekey = @Key,
                    LicenseStatus = 'I',
                    LicenseGenDate = GETDATE() 
                WHERE UserID = @UserId";

                    try
                    {
                        await using (var cmd = new SqlCommand(updateQuery, con, (SqlTransaction)transaction))
                        {
                            cmd.Parameters.AddWithValue("@UserId", UserId ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Key", Key ?? (object)DBNull.Value);

                            await cmd.ExecuteNonQueryAsync();
                        }

                        await transaction.CommitAsync();
                        return Json(new { success = true, message = "Authorization saved successfully." });
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
            }
        }
        public async Task<IActionResult> GETBTNS()
        {
            try
            {
                string ConnectionString = _configuration.GetConnectionString("ErpConnection");
                string Query = "SELECT * FROM [Btn]";
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
        public async Task<IActionResult> GETMENU()
        {
            try
            {
                string ConnectionString = _configuration.GetConnectionString("ErpConnection");
                string Query = "SELECT * FROM [Menu] T1 WHERE IsActive = 'Y' AND ParentID <> '0'";
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
        public async Task<IActionResult> GETMENUBYUSER(string id)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ErpConnection");
                string query = @"
            SELECT 
                T2.MenuName,
                T2.MenuID, 
                T2.ParentID, 
                T2.Controller, 
                T2.Action, 
                T2.Icon, 
                T2.MenuOrderID,
                T2.ObjType, 
                T1.Username, 
                T1.Role, 
                T0.Auth
            FROM MenuAuth T0
            INNER JOIN Users T1 ON T0.UserID = T1.UserID
            INNER JOIN Menu T2 ON T0.MenuId = T2.MenuId
            WHERE T1.UserID = @UserID AND T0.Auth = 'Y' AND T2.IsActive = 'Y' AND ParentID <> '0'";

                List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();

                await using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", id);
                        cmd.CommandTimeout = 300;

                        await con.OpenAsync();

                        await using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                Dictionary<string, object> row = new Dictionary<string, object>();
                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    string columnName = rdr.GetName(i);
                                    object? value = rdr.IsDBNull(i) ? "" : rdr.GetValue(i);

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
        [HttpPost]
        public async Task<IActionResult> Users([FromBody] User data)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            var item = data;

            await using (var con = new SqlConnection(connStr))
            {
                await con.OpenAsync();
                await using (var transaction = await con.BeginTransactionAsync())
                {
                    try
                    {
                        if (string.IsNullOrEmpty(item.UserID))
                        {
                            // Insert new user
                            var insertQuery = @"
                        INSERT INTO [Users] 
                        ([Username], [Password], [FullName], [Email], [Mobile], [Role], [IsActive], 
                         [CreatedBy], [UpdatedBy], [LicenseValidFrom], [LicenseValidTo], [LicenseStatus], 
                         [LicenseGenDate], [Licencekey],[EmpId]) 
                        VALUES 
                        (@Username, @Password, @FullName, @Email, @Mobile, @Role, @IsActive,
                         @CreatedBy, @UpdatedBy, @LicenseValidFrom, @LicenseValidTo, @LicenseStatus,
                         @LicenseGenDate, @Licencekey ,@empid );
                        SELECT SCOPE_IDENTITY();";

                            await using (var cmd = new SqlCommand(insertQuery, con, (SqlTransaction)transaction))
                            {
                                cmd.Parameters.AddWithValue("@Username", item.Username ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@Password", (object)item.Password ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@FullName", (object)item.FullName ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Email", (object)item.Email ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Mobile", (object)item.Mobile ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Role", (object)item.Role ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@IsActive", item.IsActive);
                                cmd.Parameters.AddWithValue("@CreatedBy", HttpContext.Session.GetString("UserID") ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@UpdatedBy", HttpContext.Session.GetString("UserID") ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@LicenseValidFrom", (object)item.LicenseValidFrom ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@LicenseValidTo", (object)item.LicenseValidTo ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@LicenseStatus", (object)item.LicenseStatus ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@LicenseGenDate", (object)item.LicenseGenDate ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Licencekey", (object)item.Licencekey ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@empid ", (object)item.EmpId ?? DBNull.Value);

                                // Execute and get the new UserID
                                var newId = await cmd.ExecuteScalarAsync();
                                item.UserID = newId?.ToString();
                            }
                        }
                        else
                        {
                            // Update existing user
                            var updateQuery = @"
                        UPDATE [Users] SET 
                            [Username] = @Username,
                            [Password] = @Password,
                            [FullName] = @FullName,
                            [Email] = @Email,
                            [Mobile] = @Mobile,
                            [Role] = @Role,
                            [IsActive] = @IsActive,
                            [UpdatedBy] = @UpdatedBy,
                            [UpdatedDate] = @UpdatedDate,
                            [LicenseValidFrom] = @LicenseValidFrom,
                            [LicenseValidTo] = @LicenseValidTo,
                            [Licencekey] = @Licencekey,
                            [EmpId] = @empid
                        WHERE UserID = @UserID";

                            await using (var cmd = new SqlCommand(updateQuery, con, (SqlTransaction)transaction))
                            {
                                cmd.Parameters.AddWithValue("@Username", item.Username ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@Password", (object)item.Password ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@FullName", (object)item.FullName ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Email", (object)item.Email ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Mobile", (object)item.Mobile ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Role", (object)item.Role ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@IsActive", item.IsActive);
                                cmd.Parameters.AddWithValue("@UpdatedBy", HttpContext.Session.GetString("UserID") ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                                cmd.Parameters.AddWithValue("@LicenseValidFrom", (object)item.LicenseValidFrom ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@LicenseValidTo", (object)item.LicenseValidTo ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Licencekey", (object)item.Licencekey ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@UserID", item.UserID);
                                cmd.Parameters.AddWithValue("@empid ", (object)item.EmpId ?? DBNull.Value);
                                await cmd.ExecuteNonQueryAsync();
                            }
                        }

                        await transaction.CommitAsync();
                        return Json(new { success = true, message = "User updated successfully.", users = data });
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
            }
        }
        public async Task<IActionResult> SAVEAUTH([FromBody] Auth model)
        {
            if (model == null || model.Data == null)
            {
                return Json(new { success = false, message = "Invalid data" });
            }

            var connStr = _configuration.GetConnectionString("ErpConnection");

            await using (var con = new SqlConnection(connStr))
            {
                await con.OpenAsync();

                await using (var transaction = await con.BeginTransactionAsync())
                {
                    var deleteQuery = @"DELETE FROM MenuAuth WHERE UserID = @UserId";

                    try
                    {
                        // Delete existing records
                        await using (var cmd = new SqlCommand(deleteQuery, con, (SqlTransaction)transaction))
                        {
                            cmd.Parameters.AddWithValue("@UserId", model.UserId ?? (object)DBNull.Value);
                            await cmd.ExecuteNonQueryAsync();
                        }

                        // Insert new records
                        foreach (var item in model.Data)
                        {
                            if (!string.IsNullOrEmpty(item.MenuId))
                            {
                                var insertQuery = @"
                            INSERT INTO [MenuAuth] 
                            ([MenuID], [UserID], [Auth]) 
                            VALUES 
                            (@MenuID, @UserID, @Auth)";

                                await using (var cmd = new SqlCommand(insertQuery, con, (SqlTransaction)transaction))
                                {
                                    cmd.Parameters.AddWithValue("@MenuID", (object)item.MenuId ?? DBNull.Value);
                                    cmd.Parameters.AddWithValue("@UserID", (object)model.UserId ?? DBNull.Value);
                                    cmd.Parameters.AddWithValue("@Auth", (object)item.Auth ?? DBNull.Value);

                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }
                        }

                        await transaction.CommitAsync();
                        return Json(new { success = true, message = "Authorization saved successfully." });
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
            }
        }
        #region License
        public async Task<IActionResult> APPLYLICENCE([FromBody] LicenseData data)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");

            await using var con = new SqlConnection(connStr);
            await con.OpenAsync();

            await using var transaction = await con.BeginTransactionAsync();
            var updateQuery = @"
        UPDATE Users 
        SET LicenseValidFrom = @LicenseValidFrom,
            LicenseStatus = @LicenseStatus,
            LicenseValidTo = @LicenseValidTo 
        WHERE UserID = @UserId";

            try
            {
                await using var cmd = new SqlCommand(updateQuery, con, (SqlTransaction)transaction);
                cmd.Parameters.AddWithValue("@LicenseValidFrom", data.LicenseValidFrom ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@LicenseValidTo", data.LicenseValidTo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@LicenseStatus", "A");
                cmd.Parameters.AddWithValue("@UserId", data.UserID ?? (object)DBNull.Value);

                await cmd.ExecuteNonQueryAsync();
                await transaction.CommitAsync();

                return Json(new { success = true, message = "Licence applied successfully." });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion
        #region Menu
        public async Task<IActionResult> DeleteMenu([FromBody] Menu data)
        {
            if (string.IsNullOrEmpty(data.MenuID))
                return Json(new { success = false, message = "Menu ID is required for deletion." });

            var connStr = _configuration.GetConnectionString("ErpConnection");
            await using var con = new SqlConnection(connStr);
            await con.OpenAsync();
            await using var transaction = await con.BeginTransactionAsync();

            try
            {
                var deleteQuery = @"
            DELETE FROM MenuAuth WHERE MenuID = @MenuID;
            DELETE FROM [Menu] WHERE MenuID = @MenuID;
            DELETE FROM [Menu] WHERE ParentID = @MenuID;";

                await using var cmd = new SqlCommand(deleteQuery, con, (SqlTransaction)transaction);
                cmd.Parameters.AddWithValue("@MenuID", data.MenuID);

                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                if (rowsAffected == 0)
                {
                    await transaction.RollbackAsync();
                    return Json(new { success = false, message = "No Menu found with the specified ID." });
                }

                await transaction.CommitAsync();
                return Json(new { success = true, message = "Menu deleted successfully." });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion
        #region BtnAuth
        public async Task<IActionResult> CREATEBTNAUTH([FromBody] BTNAUTHLIST itemdata)
        {
            string connectionString = _configuration.GetConnectionString("ErpConnection");
            Genrate_Query generator = new Genrate_Query();

            try
            {
                for (int i = 0; i < itemdata.authorizations.Length; i++)
                {
                    var item = itemdata.authorizations[i];
                    string query = string.IsNullOrEmpty(item.AuthID)
                        ? generator.GenerateInsertQuery(item, "[BtnAuth]", "AuthID")
                        : generator.GenerateUpdateQuery(item, "[BtnAuth]", "AuthID", item.AuthID, "");

                    await using var con = new SqlConnection(connectionString);
                    await con.OpenAsync();
                    await using var cmd = new SqlCommand(query, con)
                    {
                        CommandTimeout = 300
                    };

                    await cmd.ExecuteNonQueryAsync();
                }

                return Json(new { success = true, message = "Authorization saved successfully." });
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

        public async Task<IActionResult> DELETEBTNAUTH(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Json(new { success = false, message = "MenuID is required." });

            string connectionString = _configuration.GetConnectionString("ErpConnection");
            var query = "DELETE FROM [BtnAuth] WHERE MenuID = @MenuID";

            try
            {
                await using var con = new SqlConnection(connectionString);
                await con.OpenAsync();
                await using var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@MenuID", id);

                await cmd.ExecuteNonQueryAsync();

                return Json(new { success = true, message = "Authorization deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

    }
}
