using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;


namespace TECHNOSYNCERP.Controllers
{

    public class AttachmentController : Controller
    {
        private readonly IConfiguration _configuration;
        public AttachmentController(ILogger<AttachmentController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<ContentResult> GETATTACHMENTS(string docId, string ObjType)
        {
            try
            {
                var list = new List<Dictionary<string, object>>();
                string conStr = _configuration.GetConnectionString("ErpConnection");

                using (SqlConnection con = new SqlConnection(conStr))
                {
                    string query = @"
                SELECT SrNo, FileName, Extension, base64data, CreateDate, UpdateDate, DocId, ObjType, UserId, CounterCode
                FROM Attachments
                WHERE DocId = @DocId AND ObjType = @ObjType
            ";

                    await con.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@DocId", docId);
                        cmd.Parameters.AddWithValue("@ObjType", ObjType);

                        using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                var obj = new Dictionary<string, object>();

                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    obj[rdr.GetName(i)] = rdr.IsDBNull(i) ? null : rdr.GetValue(i);
                                }

                                list.Add(obj);
                            }
                        }
                    }
                }

                string json = JsonConvert.SerializeObject(list, new JsonSerializerSettings
                {
                    MaxDepth = int.MaxValue,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                return Content(json, "application/json");
            }
            catch (SqlException ex)
            {
                string errorJson = JsonConvert.SerializeObject(new { success = false, error = "Database error", message = ex.Message });
                return Content(errorJson, "application/json");
            }
            catch (Exception ex)
            {
                string errorJson = JsonConvert.SerializeObject(new { success = false, error = "Server error", message = ex.Message });
                return Content(errorJson, "application/json");
            }
        }

        public async Task<JsonResult> DELETE(string srno)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    string query = "DELETE FROM Attachments WHERE SrNo = @srno";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@srno", srno);
                        await command.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { message = "Attachment Deleted Successfully", success = true });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message, success = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ADD([FromBody] List<Dictionary<string, object>> list)
        {
            SqlTransaction transaction = null;
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
                await connection.OpenAsync();
                transaction = connection.BeginTransaction();

                foreach (var file in list)
                {
                    string query = @"
                        INSERT INTO Attachments 
                        (DocId, ObjType, FileName, Extension, base64data, CounterCode, UserId, CreateDate, UpdateDate) 
                        VALUES (@DocId, @ObjType, @FileName, @Extension, @base64data, @Counter, @UserId, GETDATE(), GETDATE())
                    ";

                    using (var command = new SqlCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@DocId", Convert.ToInt32(file["DocId"].ToString()));
                        command.Parameters.AddWithValue("@ObjType", file["ObjType"].ToString());
                        command.Parameters.AddWithValue("@FileName", file["FileName"].ToString());
                        command.Parameters.AddWithValue("@Extension", file["Extension"].ToString());
                        command.Parameters.AddWithValue("@Counter", file["Counter"].ToString());
                        command.Parameters.AddWithValue("@UserId", HttpContext.Session.GetString("UserID"));

                        string base64String = file["FileData"].ToString();
                        if (string.IsNullOrWhiteSpace(base64String) || !IsBase64String(base64String))
                        {
                            await transaction.RollbackAsync();
                            return Json(new { error = "Invalid Base64 file data.", success = false });
                        }
                        command.Parameters.AddWithValue("@base64data", base64String);

                        await command.ExecuteNonQueryAsync();
                    }
                }

                await transaction.CommitAsync();
                return Json(new { message = "Attachments Uploaded Successfully", success = true });
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    try
                    {
                        await transaction.RollbackAsync();
                    }
                    catch (Exception rollbackEx)
                    {
                        return Json(new { error = $"Error: {ex.Message}. Rollback failed: {rollbackEx.Message}", success = false });
                    }
                }

                return Json(new { error = ex.Message, success = false });
            }
            finally
            {
                if (connection != null)
                {
                    await connection.CloseAsync();
                    connection.Dispose();
                }
            }
        }

        private bool IsBase64String(string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out _);
        }

    }
}