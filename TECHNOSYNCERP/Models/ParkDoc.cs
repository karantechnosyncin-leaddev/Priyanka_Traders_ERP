using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace TECHNOSYNCERP.Models
{
    public class ParkDoc
    {
        public dynamic ParKDocument(Park data, string connectionString)
        {
            SqlConnection con = null;
            SqlTransaction transaction = null;

            try
            {
                Genrate_Query generator = new Genrate_Query();
                con = new SqlConnection(connectionString);
                con.Open();
                transaction = con.BeginTransaction();

                string query = generator.GenerateInsertQuery(data, "[Document_Park]", "DocEntry");

                using (SqlCommand cmd = new SqlCommand(query, con, transaction))
                {
                    cmd.CommandTimeout = 300;
                    cmd.ExecuteNonQuery();

                    // Get the ID if it was an insert
                    if (string.IsNullOrEmpty(data.DocEntry))
                    {
                        cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                        data.DocEntry = cmd.ExecuteScalar().ToString();
                    }
                }

                transaction.Commit();
                return new { Success = true, DocEntry = data.DocEntry, Message = "Document saved successfully." };
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                try
                {
                    transaction?.Rollback();
                    return new { Success = false, DocEntry = string.Empty, Message = "A record with the same value already exists." };
                }
                catch
                {
                    // Additional error handling if rollback fails
                    return new { Success = false, DocEntry = string.Empty, Message = "Conflict occurred and couldn't complete rollback." };
                }
            }
            catch (Exception ex)
            {
                try
                {
                    transaction?.Rollback();
                    return new { Success = false, DocEntry = string.Empty, Message = ex.Message };
                }
                catch
                {
                    // Additional error handling if rollback fails
                    return new { Success = false, DocEntry = string.Empty, Message = "Error occurred and couldn't complete rollback." };
                }
            }
            finally
            {
                if (transaction != null)
                {
                    transaction.Dispose();
                }

                if (con != null)
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
    }
    public class Park
    {
        public string DocEntry { get; set; }
        public string OBJType { get; set; }
        public DateTime DocumentDate { get; set; }
        public string Payload { get; set; }
        public string RefNo { get; set; }
        public string CretedByUId { get; set; }
        public string CretedByUName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}