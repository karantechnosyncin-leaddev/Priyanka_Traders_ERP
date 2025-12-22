using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace TECHNOSYNCERP.Middleware
{
    public class SessionValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public SessionValidationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Ensure session is available
                if (context.Features.Get<ISessionFeature>()?.Session != null)
                {
                    var userId = context.Session.GetString("UserID");
                    var sessionId = context.Session.GetString("SessionId");

                    if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(sessionId))
                    {
                        using (var con = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                        {
                            string query = @"SELECT SessionId, LastLogin 
                                 FROM Users 
                                 WHERE UserID = @UserId AND LoginStatus = 'A'";
                            using (var cmd = new SqlCommand(query, con))
                            {
                                cmd.Parameters.AddWithValue("@UserId", userId);
                                con.Open();
                                using (var reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        var dbSessionId = reader["SessionId"].ToString();
                                        var lastLogin = Convert.ToDateTime(reader["LastLogin"]);

                                        if ((DateTime.Now - lastLogin).TotalMinutes > 60 ||
                                            dbSessionId != sessionId)
                                        {
                                            context.Session.Clear();
                                            var redirectUrl = $"{context.Request.PathBase}/Home/Index?sessionExpired=true";
                                            context.Response.Redirect(redirectUrl);
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                var redirectUrl = $"{context.Request.PathBase}/Home/Index?sessionExpired=true";
                context.Response.Redirect(redirectUrl);
                throw;
            }
          
        }
    }
}
