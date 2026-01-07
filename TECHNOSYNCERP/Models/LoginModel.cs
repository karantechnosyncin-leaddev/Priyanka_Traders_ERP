namespace TECHNOSYNCERP.Models
{
    public class LoginModel
    {
        public string User { get; set; }
        public string Password { get; set; }
    }
    public class UserModel
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string CompName { get; set; }
        public string EmpId { get; set; }
    }

    public class AuthModel
    {
        public List<Dictionary<string, string>> MenuAuth { get; set; }
        public List<Dictionary<string, string>> BtnAuth { get; set; }
    }
}
