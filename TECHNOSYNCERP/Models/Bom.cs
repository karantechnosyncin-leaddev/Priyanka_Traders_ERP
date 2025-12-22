namespace TECHNOSYNCERP.Models
{
    public class BOM
    {
        public BOMHEDER header { get; set; }
        public BOMROW[] lines { get; set; }
    }
    public class BOMHEDER {
        public string BOMID { get; set; }
        public string ItemID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Quantity { get; set; }
        public string WhsCode { get; set; }
        public string WhsID { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class BOMROW
    {
        public string ID { get; set; }
        public string BOMID { get; set; }
        public string ItemID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string WhsCode { get; set; }
        public string WhsID { get; set; }
        public string UomID { get; set; }
        public string UomCode { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }

    }

    public class PRODUCTIONORDER {
        public PRODORDRHEAD header { get; set; }
        public PRODORDRROW[] lines { get; set; }
    }
    public class PRODORDRHEAD 
    {
        public string? DocEntry { get; set; }
        public string? Docnum { get; set; }
        public string? FYearId { get; set; }
        public string? OBJType { get; set; }
        public string? OrderDate { get; set; }
        public string? StartDate { get; set; }
        public string? DueDate { get; set; }
        public string? LedgerID { get; set; }
        public string? LedgerCode { get; set; }
        public string? DocStatus { get; set; }
        public string? Status { get; set; }
        public string? ItemID { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? UOMCode { get; set; }
        public string? UOMID { get; set; }
        public string? Quantity { get; set; }
        public string? OpenQty { get; set; }
        public string? WhsID { get; set; }
        public string? WhsCode { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class PRODORDRROW
    {
        public string? ID { get; set; }
        public string? DocEntry { get; set; }
        public string? ItemID { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? BaseQty { get; set; }
        public string? PlanQty { get; set; }
        public string? OpenQty { get; set; }
        public string? WhsCode { get; set; }
        public string? WhsID { get; set; }
        public string? UomID { get; set; }
        public string? UomCode { get; set; }
        public string? StockInWhs { get; set; }
        public string? IssuMth { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
}
