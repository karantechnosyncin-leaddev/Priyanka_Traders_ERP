namespace TECHNOSYNCERP.Models
{
    public class SALESQUOT
    {
        public SALESQUOTHEAD header { get; set; }
        public SALESQUOTITEM[] lines { get; set; }
        public SALESQUOTFREIGHT[] freight { get; set; }
    }
    public class SALESQUOTHEAD
    {
        public string DocEntry { get; set; }
        public string ItemType { get; set; }
        public string Docnum { get; set; }
        public string DocStatus { get; set; }
        public string PostingDate { get; set; }
        public string DeliveryDate { get; set; }
        public string DocumentDate { get; set; }
        public string DueDate { get; set; }
        public string FYearId { get; set; }
        public string RefNo { get; set; }
        public string Remarks { get; set; }
        public string LedgerID { get; set; }
        public string LedgerName { get; set; }
        public string LedgerCode { get; set; }
        public string AddID { get; set; }
        public string AddressId { get; set; }
        public string PONo { get; set; }
        public string VehicleNo { get; set; }
        public string TransMode { get; set; }
        public string VendorCode { get; set; }
        public decimal TotalWeight { get; set; }
        public decimal GrossTotal { get; set; }
        public decimal GSTTotal { get; set; }
        public decimal NETTotal { get; set; }
        public decimal FreightTotal { get; set; }
        public decimal TotalAftBillDisc { get; set; }
        public decimal DisPer { get; set; }
        public decimal DisAmount { get; set; }
        public decimal Rounding { get; set; }


        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class SALESQUOTITEM
    {
        public string ID { get; set; }
        public int LineNum { get; set; }
        public string DocEntry { get; set; }
        public string? ItemID { get; set; }
        public string? EanCode { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string LedgerID { get; set; }
        public string LedgerName { get; set; }
        public decimal Quantity { get; set; }
        public decimal OpenQuantity { get; set; }
        public decimal BaseQuantity { get; set; }

        public decimal ItemCost { get; set; }
        public decimal UnitePrice { get; set; }
        public decimal MRP { get; set; }
        public decimal NetAmount { get; set; }
        public decimal DisPer { get; set; }
        public decimal DisAmount { get; set; }
        public decimal TaxableAmt { get; set; }
        public decimal AftBillDisc { get; set; }
        public string TaxCodeId { get; set; }
        public string TaxCode { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal CGSTRate { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal SGSTRate { get; set; }
        public decimal IGSTAmt { get; set; }
        public decimal IGSTRate { get; set; }
        public decimal UTGSTAmt { get; set; }
        public decimal UTGSTRate { get; set; }
        public decimal Weight1 { get; set; }
        public string? UOMID { get; set; }
        public string? UOMCode { get; set; }
        public string HSNID { get; set; }
        public string? HSNCode { get; set; }
        public string WHSID { get; set; }
        public string? WhsCode { get; set; }
        public string? StockInWhs { get; set; }
        public string? AcctCode { get; set; }
        public string? Remarks { get; set; }

        public string? Status { get; set; }
        public string? BaseObjType { get; set; }
        public string? BaseDocEntry { get; set; }
        public string? BaseLine { get; set; }


        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }

    }
    public class SALESQUOTFREIGHT
    {
        public string ID { get; set; }
       
        public string DocEntry { get; set; }
        public string FreID { get; set; }
        public string Name { get; set; }

        public decimal NetAmt { get; set; }
        public string TaxCodeId { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal CGSTRate { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal SGSTRate { get; set; }
        public decimal IGSTAmt { get; set; }
        public decimal IGSTRate { get; set; }
        public decimal UTGSTAmt { get; set; }
        public decimal UTGSTRate { get; set; }
        public string? Remark { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }

    }
    public class SALESORDER
    {
        public SALESORDERHEAD header { get; set; }
        public SALESORDERITEM[] lines { get; set; }
        public SALESORDERFREIGHT[] freight { get; set; }
    }
    public class SALESORDERHEAD
    {
        public string DocEntry { get; set; }
        public string ItemType { get; set; }
        public string Docnum { get; set; }
        public string DocStatus { get; set; }
        public string PostingDate { get; set; }
        public string DeliveryDate { get; set; }
        public string DocumentDate { get; set; }
        public string DueDate { get; set; }
        public string FYearId { get; set; }
        public string RefNo { get; set; }
        public string Remarks { get; set; }
        public string LedgerID { get; set; }
        public string LedgerName { get; set; }
        public string LedgerCode { get; set; }
        public string AddID { get; set; }
        public string AddressId { get; set; }
        public string PONo { get; set; }
        public string VehicleNo { get; set; }
        public string TransMode { get; set; }
        public string VendorCode { get; set; }
        public decimal TotalWeight { get; set; }
        public decimal GrossTotal { get; set; }
        public decimal GSTTotal { get; set; }
        public decimal NETTotal { get; set; }
        public decimal FreightTotal { get; set; }
        public decimal TotalAftBillDisc { get; set; }
        public decimal DisPer { get; set; }
        public decimal DisAmount { get; set; }
        public decimal Rounding { get; set; }


        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class SALESORDERITEM
    {
        public string ID { get; set; }
        public int LineNum { get; set; }
        public string DocEntry { get; set; }
        public string? ItemID { get; set; }
        public string? EanCode { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string LedgerID { get; set; }
        public string LedgerName { get; set; }
        public decimal Quantity { get; set; }
        public decimal OpenQuantity { get; set; }
        public decimal BaseQuantity { get; set; }

        public decimal ItemCost { get; set; }
        public decimal UnitePrice { get; set; }
        public decimal MRP { get; set; }
        public decimal NetAmount { get; set; }
        public decimal DisPer { get; set; }
        public decimal DisAmount { get; set; }
        public decimal TaxableAmt { get; set; }
        public decimal AftBillDisc { get; set; }
        public string TaxCodeId { get; set; }
        public string TaxCode { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal CGSTRate { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal SGSTRate { get; set; }
        public decimal IGSTAmt { get; set; }
        public decimal IGSTRate { get; set; }
        public decimal UTGSTAmt { get; set; }
        public decimal UTGSTRate { get; set; }
        public decimal Weight1 { get; set; }
        public string? UOMID { get; set; }
        public string? UOMCode { get; set; }
        public string HSNID { get; set; }
        public string? HSNCode { get; set; }
        public string WHSID { get; set; }
        public string? WhsCode { get; set; }
        public string? StockInWhs { get; set; }
        public string? AcctCode { get; set; }
        public string? Remarks { get; set; }

        public string? Status { get; set; }
        public string? BaseObjType { get; set; }
        public string? BaseDocEntry { get; set; }
        public string? BaseLine { get; set; }


        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }

    }
    public class SALESORDERFREIGHT
    {
        public string ID { get; set; }
        public string DocEntry { get; set; }
        public string FreID { get; set; }
        public string Name { get; set; }

        public decimal NetAmt { get; set; }
        public string TaxCodeId { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal CGSTRate { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal SGSTRate { get; set; }
        public decimal IGSTAmt { get; set; }
        public decimal IGSTRate { get; set; }
        public decimal UTGSTAmt { get; set; }
        public decimal UTGSTRate { get; set; }
        public string? Remark { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }

    }
    public class SALESDELIVERY
    {
        public SALESDELIVERYHEAD header { get; set; }
        public SALESDELIVERYITEM[] lines { get; set; }
        public SALESDELIVERYFREIGHT[] freight { get; set; }
    }
    public class SALESDELIVERYHEAD
    {
        public string DocEntry { get; set; }
        public string ItemType { get; set; }
        public string Docnum { get; set; }
        public string DocStatus { get; set; }
        public string PostingDate { get; set; }
        public string DeliveryDate { get; set; }
        public string DocumentDate { get; set; }
        public string DueDate { get; set; }
        public string FYearId { get; set; }
        public string RefNo { get; set; }
        public string Remarks { get; set; }
        public string LedgerID { get; set; }
        public string LedgerName { get; set; }
        public string LedgerCode { get; set; }
        public string AddID { get; set; }
        public string AddressId { get; set; }
        public string PONo { get; set; }
        public string VehicleNo { get; set; }
        public string TransMode { get; set; }
        public string VendorCode { get; set; }
        public decimal TotalWeight { get; set; }
        public decimal GrossTotal { get; set; }
        public decimal GSTTotal { get; set; }
        public decimal NETTotal { get; set; }
        public decimal FreightTotal { get; set; }
        public decimal TotalAftBillDisc { get; set; }
        public decimal DisPer { get; set; }
        public decimal DisAmount { get; set; }
        public decimal Rounding { get; set; }


        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class SALESDELIVERYITEM
    {
        public string ID { get; set; }
        public int LineNum { get; set; }
        public string DocEntry { get; set; }
        public string? ItemID { get; set; }
        public string? EanCode { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string LedgerID { get; set; }
        public string LedgerName { get; set; }
        public decimal Quantity { get; set; }
        public decimal OpenQuantity { get; set; }
        public decimal BaseQuantity { get; set; }

        public decimal ItemCost { get; set; }
        public decimal UnitePrice { get; set; }
        public decimal MRP { get; set; }
        public decimal NetAmount { get; set; }
        public decimal DisPer { get; set; }
        public decimal DisAmount { get; set; }
        public decimal TaxableAmt { get; set; }
        public decimal AftBillDisc { get; set; }
        public string TaxCodeId { get; set; }
        public string TaxCode { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal CGSTRate { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal SGSTRate { get; set; }
        public decimal IGSTAmt { get; set; }
        public decimal IGSTRate { get; set; }
        public decimal UTGSTAmt { get; set; }
        public decimal UTGSTRate { get; set; }
        public decimal Weight1 { get; set; }
        public string? UOMID { get; set; }
        public string? UOMCode { get; set; }
        public string HSNID { get; set; }
        public string? HSNCode { get; set; }
        public string WHSID { get; set; }
        public string? WhsCode { get; set; }
        public string? StockInWhs { get; set; }
        public string? AcctCode { get; set; }
        public string? Remarks { get; set; }

        public string? Status { get; set; }
        public string? BaseObjType { get; set; }
        public string? BaseDocEntry { get; set; }
        public string? BaseLine { get; set; }


        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }

    }
    public class SALESDELIVERYFREIGHT
    {
        public string ID { get; set; }
        public string DocEntry { get; set; }
        public string FreID { get; set; }
        public string Name { get; set; }

        public decimal NetAmt { get; set; }
        public string TaxCodeId { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal CGSTRate { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal SGSTRate { get; set; }
        public decimal IGSTAmt { get; set; }
        public decimal IGSTRate { get; set; }
        public decimal UTGSTAmt { get; set; }
        public decimal UTGSTRate { get; set; }
        public string? Remark { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }

    }
    public class SALESINVOICE
    {
        public SALESINVOICEHEAD header { get; set; }
        public SALESINVOICEITEM[] lines { get; set; }
        public SALESINVOICEFREIGHT[] freight { get; set; }
    }
    public class SALESINVOICEHEAD
    {
        public string DocEntry { get; set; }
        public string ItemType { get; set; }
        public string Docnum { get; set; }
        public string DocStatus { get; set; }
        public string PostingDate { get; set; }
        public string DeliveryDate { get; set; }
        public string DocumentDate { get; set; }
        public string DueDate { get; set; }
        public string FYearId { get; set; }
        public string RefNo { get; set; }
        public string Remarks { get; set; }
        public string LedgerID { get; set; }
        public string LedgerName { get; set; }
        public string LedgerCode { get; set; }
        public string AddID { get; set; }
        public string AddressId { get; set; }
        public string PONo { get; set; }
        public string VehicleNo { get; set; }
        public string TransMode { get; set; }
        public string VendorCode { get; set; }
        public decimal TotalWeight { get; set; }
        public decimal GrossTotal { get; set; }
        public decimal GSTTotal { get; set; }
        public decimal NETTotal { get; set; }
        public decimal FreightTotal { get; set; }
        public decimal TotalAftBillDisc { get; set; }
        public decimal DisPer { get; set; }
        public decimal DisAmount { get; set; }
        public decimal Rounding { get; set; }


        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class SALESINVOICEITEM
    {
        public string ID { get; set; }
        public int LineNum { get; set; }
        public string DocEntry { get; set; }
        public string? ItemID { get; set; }
        public string? EanCode { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string LedgerID { get; set; }
        public string LedgerName { get; set; }
        public decimal Quantity { get; set; }
        public decimal OpenQuantity { get; set; }
        public decimal BaseQuantity { get; set; }

        public decimal ItemCost { get; set; }
        public decimal UnitePrice { get; set; }
        public decimal MRP { get; set; }
        public decimal NetAmount { get; set; }
        public decimal DisPer { get; set; }
        public decimal DisAmount { get; set; }
        public decimal TaxableAmt { get; set; }
        public decimal AftBillDisc { get; set; }
        public string TaxCodeId { get; set; }
        public string TaxCode { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal CGSTRate { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal SGSTRate { get; set; }
        public decimal IGSTAmt { get; set; }
        public decimal IGSTRate { get; set; }
        public decimal UTGSTAmt { get; set; }
        public decimal UTGSTRate { get; set; }
        public decimal Weight1 { get; set; }
        public string? UOMID { get; set; }
        public string? UOMCode { get; set; }
        public string HSNID { get; set; }
        public string? HSNCode { get; set; }
        public string WHSID { get; set; }
        public string? WhsCode { get; set; }
        public string? StockInWhs { get; set; }
        public string? AcctCode { get; set; }
        public string? Remarks { get; set; }

        public string? Status { get; set; }
        public string? BaseObjType { get; set; }
        public string? BaseDocEntry { get; set; }
        public string? BaseLine { get; set; }


        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }

    }
    public class SALESINVOICEFREIGHT
    {
        public string ID { get; set; }
        public string DocEntry { get; set; }
        public string FreID { get; set; }
        public string Name { get; set; }

        public decimal NetAmt { get; set; }
        public string TaxCodeId { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal CGSTRate { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal SGSTRate { get; set; }
        public decimal IGSTAmt { get; set; }
        public decimal IGSTRate { get; set; }
        public decimal UTGSTAmt { get; set; }
        public decimal UTGSTRate { get; set; }
        public string? Remark { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }

    }
    public class SALESTAXINVOICE
    {
        public SALESTAXINVOICEHEAD header { get; set; }
        public ACCOUNTSRECEIPTHEAD payment { get; set; }
        public SALESTAXINVOICEITEM[] lines { get; set; }
        public SALESTAXINVOICEFREIGHT[] freight { get; set; }
    }

    public class SALESTAXINVOICEHEAD
    {
        public string DocEntry { get; set; }
        public string ItemType { get; set; }
        public string Docnum { get; set; }
        public string DocStatus { get; set; }
        public string PostingDate { get; set; }
        public string DeliveryDate { get; set; }
        public string DocumentDate { get; set; }
        public string DueDate { get; set; }
        public string FYearId { get; set; }
        public string RefNo { get; set; }
        public string Remarks { get; set; }
        public string LedgerID { get; set; }
        public string LedgerName { get; set; }
        public string LedgerCode { get; set; }
        public string AddID { get; set; }
        public string AddressId { get; set; }
        public string PONo { get; set; }
        public string VehicleNo { get; set; }
        public string TransMode { get; set; }
        public string VendorCode { get; set; }
        public string PayTrans { get; set; }
        public string PayLater { get; set; }
        public string PayReco { get; set; }

        public decimal TotalWeight { get; set; }
        public decimal GrossTotal { get; set; }
        public decimal GSTTotal { get; set; }
        public decimal NETTotal { get; set; }
        public decimal AppliedAmount { get; set; }
        public decimal BalanceDue { get; set; }
        public decimal FreightTotal { get; set; }
        public decimal TotalAftBillDisc { get; set; }
        public decimal DisPer { get; set; }
        public decimal DisAmount { get; set; }
        public decimal Rounding { get; set; }


        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class SALESTAXINVOICEITEM
    {
        public string ID { get; set; }
        public int LineNum { get; set; }
        public string DocEntry { get; set; }
        public string? ItemID { get; set; }
        public string? EanCode { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string LedgerID { get; set; }
        public string LedgerName { get; set; }
        public decimal Quantity { get; set; }
        public decimal OpenQuantity { get; set; }
        public decimal BaseQuantity { get; set; }

        public decimal ItemCost { get; set; }
        public decimal UnitePrice { get; set; }
        public decimal MRP { get; set; }
        public decimal NetAmount { get; set; }
        public decimal DisPer { get; set; }
        public decimal DisAmount { get; set; }
        public decimal TaxableAmt { get; set; }
        public decimal AftBillDisc { get; set; }
        public string TaxCodeId { get; set; }
        public string TaxCode { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal CGSTRate { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal SGSTRate { get; set; }
        public decimal IGSTAmt { get; set; }
        public decimal IGSTRate { get; set; }
        public decimal UTGSTAmt { get; set; }
        public decimal UTGSTRate { get; set; }
        public decimal Weight1 { get; set; }
        public string? UOMID { get; set; }
        public string? UOMCode { get; set; }
        public string HSNID { get; set; }
        public string? HSNCode { get; set; }
        public string WHSID { get; set; }
        public string? WhsCode { get; set; }
        public string? StockInWhs { get; set; }
        public string? AcctCode { get; set; }
        public string? Remarks { get; set; }

        public string? Status { get; set; }
        public string? BaseObjType { get; set; }
        public string? BaseDocEntry { get; set; }
        public string? BaseLine { get; set; }


        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }

    }
    public class SALESTAXINVOICEFREIGHT
    {
        public string ID { get; set; }
        public string DocEntry { get; set; }
        public string FreID { get; set; }
        public string Name { get; set; }

        public decimal NetAmt { get; set; }
        public string TaxCodeId { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal CGSTRate { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal SGSTRate { get; set; }
        public decimal IGSTAmt { get; set; }
        public decimal IGSTRate { get; set; }
        public decimal UTGSTAmt { get; set; }
        public decimal UTGSTRate { get; set; }
        public string? Remark { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }

    }
    public class SALESRETURN
    {
        public SALESRETURNHEAD header { get; set; }
        
        public SALESRETURNITEM[] lines { get; set; }
        public SALESRETURNFREIGHT[] freight { get; set; }
    }
    public class SALESRETURNHEAD
    {
        public string DocEntry { get; set; }
        public string ItemType { get; set; }
        public string Docnum { get; set; }
        public string DocStatus { get; set; }
        public string PostingDate { get; set; }
        public string DeliveryDate { get; set; }
        public string DocumentDate { get; set; }
        public string DueDate { get; set; }
        public string FYearId { get; set; }
        public string RefNo { get; set; }
        public string Remarks { get; set; }
        public string LedgerID { get; set; }
        public string LedgerName { get; set; }
        public string LedgerCode { get; set; }
        public string AddID { get; set; }
        public string AddressId { get; set; }
        public string PONo { get; set; }
        public string VehicleNo { get; set; }
        public string TransMode { get; set; }
        public string VendorCode { get; set; }
        public string PayTrans { get; set; }
        public string PayLater { get; set; }
        public string PayReco { get; set; }
        public decimal TotalWeight { get; set; }
        public decimal GrossTotal { get; set; }
        public decimal GSTTotal { get; set; }
        public decimal NETTotal { get; set; }
  
        public decimal FreightTotal { get; set; }
        public decimal TotalAftBillDisc { get; set; }
        public decimal DisPer { get; set; }
        public decimal DisAmount { get; set; }
        public decimal Rounding { get; set; }


        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class SALESRETURNITEM
    {
        public string ID { get; set; }
        public int LineNum { get; set; }
        public string DocEntry { get; set; }
        public string? ItemID { get; set; }
        public string? EanCode { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string LedgerID { get; set; }
        public string LedgerName { get; set; }
        public decimal Quantity { get; set; }
        public decimal OpenQuantity { get; set; }
        public decimal BaseQuantity { get; set; }

        public decimal ItemCost { get; set; }
        public decimal UnitePrice { get; set; }
        public decimal MRP { get; set; }
        public decimal NetAmount { get; set; }
        public decimal DisPer { get; set; }
        public decimal DisAmount { get; set; }
        public decimal TaxableAmt { get; set; }
        public decimal AftBillDisc { get; set; }
        public string TaxCodeId { get; set; }
        public string TaxCode { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal CGSTRate { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal SGSTRate { get; set; }
        public decimal IGSTAmt { get; set; }
        public decimal IGSTRate { get; set; }
        public decimal UTGSTAmt { get; set; }
        public decimal UTGSTRate { get; set; }
        public decimal Weight1 { get; set; }
        public string? UOMID { get; set; }
        public string? UOMCode { get; set; }
        public string HSNID { get; set; }
        public string? HSNCode { get; set; }
        public string WHSID { get; set; }
        public string? WhsCode { get; set; }
        public string? StockInWhs { get; set; }
        public string? AcctCode { get; set; }
        public string? Remarks { get; set; }

        public string? Status { get; set; }
        public string? BaseObjType { get; set; }
        public string? BaseDocEntry { get; set; }
        public string? BaseLine { get; set; }


        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }

    }
    public class SALESRETURNFREIGHT
    {
        public string ID { get; set; }
        public string DocEntry { get; set; }
        public string FreID { get; set; }
        public string Name { get; set; }

        public decimal NetAmt { get; set; }
        public string TaxCodeId { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal CGSTRate { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal SGSTRate { get; set; }
        public decimal IGSTAmt { get; set; }
        public decimal IGSTRate { get; set; }
        public decimal UTGSTAmt { get; set; }
        public decimal UTGSTRate { get; set; }
        public string? Remark { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }

    }
    public class SALESCREDITNOTE
    {
        public SALESCREDITNOTEHEAD header { get; set; }
        public ACCOUNTSPAYMENTHEAD payment { get; set; }
        public SALESCREDITNOTEITEM[] lines { get; set; }
        public SALESCREDITNOTEFREIGHT[] freight { get; set; }
    }
    public class SALESCREDITNOTEHEAD
    {
        public string DocEntry { get; set; }
        public string ItemType { get; set; }
        public string Docnum { get; set; }
        public string DocStatus { get; set; }
        public string PostingDate { get; set; }
        public string DeliveryDate { get; set; }
        public string DocumentDate { get; set; }
        public string DueDate { get; set; }
        public string FYearId { get; set; }
        public string RefNo { get; set; }
        public string Remarks { get; set; }
        public string LedgerID { get; set; }
        public string LedgerName { get; set; }
        public string LedgerCode { get; set; }
        public string AddID { get; set; }
        public string AddressId { get; set; }
        public string PONo { get; set; }
        public string VehicleNo { get; set; }
        public string TransMode { get; set; }
        public string VendorCode { get; set; }
        public decimal TotalWeight { get; set; }
        public decimal GrossTotal { get; set; }
        public decimal GSTTotal { get; set; }
        public decimal NETTotal { get; set; }
        public decimal BalanceDue { get; set; }
        public decimal AppliedAmount { get; set; }
        public decimal FreightTotal { get; set; }
        public decimal TotalAftBillDisc { get; set; }
        public decimal DisPer { get; set; }
        public decimal DisAmount { get; set; }
        public decimal Rounding { get; set; }
        public string PayTrans { get; set; }
        public string PayLater { get; set; }
        public string PayReco { get; set; }

        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class SALESCREDITNOTEITEM
    {
        public string ID { get; set; }
        public int LineNum { get; set; }
        public string DocEntry { get; set; }
        public string? ItemID { get; set; }
        public string? EanCode { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string LedgerID { get; set; }
        public string LedgerName { get; set; }
        public decimal Quantity { get; set; }
        public decimal OpenQuantity { get; set; }
        public decimal BaseQuantity { get; set; }

        public decimal ItemCost { get; set; }
        public decimal UnitePrice { get; set; }
        public decimal MRP { get; set; }
        public decimal NetAmount { get; set; }
        public decimal DisPer { get; set; }
        public decimal DisAmount { get; set; }
        public decimal TaxableAmt { get; set; }
        public decimal AftBillDisc { get; set; }
        public string TaxCodeId { get; set; }
        public string TaxCode { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal CGSTRate { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal SGSTRate { get; set; }
        public decimal IGSTAmt { get; set; }
        public decimal IGSTRate { get; set; }
        public decimal UTGSTAmt { get; set; }
        public decimal UTGSTRate { get; set; }
        public decimal Weight1 { get; set; }
        public string? UOMID { get; set; }
        public string? UOMCode { get; set; }
        public string HSNID { get; set; }
        public string? HSNCode { get; set; }
        public string WHSID { get; set; }
        public string? WhsCode { get; set; }
        public string? StockInWhs { get; set; }
        public string? AcctCode { get; set; }
        public string? Remarks { get; set; }

        public string? Status { get; set; }
        public string? BaseObjType { get; set; }
        public string? BaseDocEntry { get; set; }
        public string? BaseLine { get; set; }


        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }

    }
    public class SALESCREDITNOTEFREIGHT
    {
        public string ID { get; set; }
        public string DocEntry { get; set; }
        public string FreID { get; set; }
        public string Name { get; set; }

        public decimal NetAmt { get; set; }
        public string TaxCodeId { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal CGSTRate { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal SGSTRate { get; set; }
        public decimal IGSTAmt { get; set; }
        public decimal IGSTRate { get; set; }
        public decimal UTGSTAmt { get; set; }
        public decimal UTGSTRate { get; set; }
        public string? Remark { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }

    }
}
