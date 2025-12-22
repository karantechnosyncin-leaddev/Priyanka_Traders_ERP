namespace TECHNOSYNCERP.Models
{
    public class Auth
    {
        public AuthData[] Data { get; set; }
        public string UserId { get; set; }

    }
    public class AuthData { 
        public string MenuId { get; set; }    
        public string UserId { get; set; }    
        public string Auth { get; set; }    
    }
    public class User { 
        public string UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Role { get; set; }
        public string LicenseValidFrom { get; set; }
        public string LicenseValidTo { get; set; }
        public string Licencekey { get; set; }
        public string LicenseStatus { get; set; }
        public string LicenseGenDate { get; set; }
        public string CreatedBy { get; set; }
        public string LastLogin { get; set; }
        public string IsActive { get; set; }
    }
    public class LicenseData
    { 
        public string UserID { get; set; }
        public string LicenseValidFrom { get; set; }
        public string LicenseValidTo { get; set; }
        public string Licencekey { get; set; }
    }
}
