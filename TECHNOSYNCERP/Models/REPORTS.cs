namespace TECHNOSYNCERP.Models
{
    public class REPORTS
    {
      public string? ReportID { get; set; }
      public string? Title { get; set; }
      public string? Icon { get; set; }
      public string? UserId { get; set; }
      public string? IsFav { get; set; }
      public string? ObjType { get; set; }
      public string? IsActive { get; set; }
      public string? ControllerName { get; set; }
      public string? ViewName { get; set; }
    }
    public class ReportConfiguration
    {
        public string? ID { get; set; }
        public string? Name { get; set; }
        public string? UserID { get; set; }
        public string? DName { get; set; }
        public string? IsVisible { get; set; }
        public string? ObjType { get; set; }
    }
}
