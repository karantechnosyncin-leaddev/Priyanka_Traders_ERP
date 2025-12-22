using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Text.Json;
using TECHNOSYNCERP.Models;

namespace TECHNOSYNCERP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString("UserID");
            if (!string.IsNullOrEmpty(userId))
            {
                UpdateLoginStatus(userId, "N", null);
                HttpContext.Session.Clear();
            }
            return View();
        }
        public IActionResult Home()
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
        public IActionResult Logout()
        {
            var userId = HttpContext.Session.GetString("UserID");

            if (!string.IsNullOrEmpty(userId))
            {
                UpdateLoginStatus(userId, "N", null);
                HttpContext.Session.Clear();
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult Index(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var user = ValidateUser(model.User, model.Password);

                if (user != null)
                {
                    if (IsUserAlreadyLoggedIn(user.UserID))
                    {
                        ViewBag.Error = "User is already logged in from another device.";
                        return View(model);
                    }

                    var sessionId = Guid.NewGuid().ToString();
                    UpdateLoginStatus(user.UserID, "A", sessionId);
                    SetSession(user, sessionId);

                    return RedirectToAction("Home", "Home");
                }
                else
                {
                    ViewBag.Error = "Wrong Username or Password.";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login Error");
                ViewBag.Error = ex.Message;
                return View(model);
            }
        }
        private UserModel ValidateUser(string username, string password)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");

            using (var con = new SqlConnection(connStr))
            {
                string query = @"SELECT (Select TOP 1 ComName FROM CompanyMaster) as CompName,* 
                                 FROM Users 
                                 WHERE UserName = @UserName 
                                   AND Password = @Password 
                                   AND IsActive = '1' 
                                   AND LicenseStatus = 'A'";

                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserName", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserModel
                            {
                                UserID = reader["UserID"].ToString(),
                                UserName = reader["UserName"].ToString(),
                                Role = reader["Role"].ToString(),
                                Mobile = reader["Mobile"].ToString(),
                                Email = reader["Email"].ToString(),
                                CompName = reader["CompName"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }
        private void SetSession(UserModel user, string sessionId)
        {
            HttpContext.Session.SetString("UserID", user.UserID);
            HttpContext.Session.SetString("UserName", user.UserName);
            HttpContext.Session.SetString("Role", user.Role);
            HttpContext.Session.SetString("Mobile", user.Mobile);
            HttpContext.Session.SetString("Email", user.Email);
            HttpContext.Session.SetString("CompName", user.CompName);
            HttpContext.Session.SetString("SessionId", sessionId);

            var authData = GetAuth(user.UserID);
            var authBytes = JsonSerializer.SerializeToUtf8Bytes(authData);
            HttpContext.Session.Set("Auth", authBytes);
        }
        private bool IsUserAlreadyLoggedIn(string userId)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            using (var con = new SqlConnection(connStr))
            {
                    string query = @"SELECT LoginStatus, LastLogin 
                                 FROM Users 
                                 WHERE UserID = @UserId";
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var status = reader["LoginStatus"].ToString();
                            string time = reader["LastLogin"].ToString();
                            if (!string.IsNullOrEmpty(time))
                            {
                                var lastLogin = Convert.ToDateTime(reader["LastLogin"]);

                                if (status == "A" && (DateTime.Now - lastLogin).TotalMinutes < 1)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                            
                        }
                    }
                }
            }
            return false;
        }
        private void UpdateLoginStatus(string userId, string status, string? sessionId)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            using (var con = new SqlConnection(connStr))
            {
                string query = @"UPDATE Users 
                                 SET LastLogin = GETDATE(),
                                     LoginStatus = @Status,
                                     SessionId = @SessionId
                                 WHERE UserID = @UserId";
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@SessionId", (object?)sessionId ?? DBNull.Value);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private AuthModel GetAuth(string userId)
        {
            var connStr = _configuration.GetConnectionString("ErpConnection");
            using (var con = new SqlConnection(connStr))
            {
                con.Open();
                return new AuthModel
                {
                    MenuAuth = GetUserAuth(userId, con),
                    BtnAuth = GetBtnAuth(userId, con)
                };
            }
        }
        private List<Dictionary<string, string>> GetUserAuth(string userId, SqlConnection con)
        {
            var result = new List<Dictionary<string, string>>();
            string query = @"SELECT T2.MenuName, T2.MenuID, T2.ParentID, T2.Controller, 
                                    T2.Action, T2.Icon, T2.MenuOrderID,
                                    T1.Username, T1.Role, COALESCE(T0.Auth, 'N') AS Auth
                             FROM Menu T2
                             LEFT JOIN MenuAuth T0 ON T0.MenuId = T2.MenuId AND T0.UserID = @UserID
                             LEFT JOIN Users T1 ON T0.UserID = T1.UserID
                             WHERE T2.IsActive='Y'";

            using (var cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@UserID", userId);
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var dict = new Dictionary<string, string>();
                        for (int i = 0; i < rdr.FieldCount; i++)
                            dict[rdr.GetName(i)] = rdr[i]?.ToString();
                        result.Add(dict);
                    }
                }
            }
            return result;
        }
        private List<Dictionary<string, string>> GetBtnAuth(string userId, SqlConnection con)
        {
            var result = new List<Dictionary<string, string>>();
            string query = @"SELECT * FROM BtnAuth WHERE UserID = @UserID";

            using (var cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@UserID", userId);
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var dict = new Dictionary<string, string>();
                        for (int i = 0; i < rdr.FieldCount; i++)
                            dict[rdr.GetName(i)] = rdr[i]?.ToString();
                        result.Add(dict);
                    }
                }
            }
            return result;
        }
    }
}
