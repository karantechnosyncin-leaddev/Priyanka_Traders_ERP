namespace TECHNOSYNCERP.Models
{
    public class Purchase
    {
    }
    public class PURCHASEQUOT
    {
        public PURCHASEQUOTHEAD header { get; set; }
        public PURCHASEQUOTITEM[] lines { get; set; }
        public PURCHASEQUOTFREIGHT[] freight { get; set; }
    }
    public class PURCHASEQUOTHEAD
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
    public class PURCHASEQUOTITEM
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


        public decimal UnitePrice { get; set; }
        public decimal DisPer { get; set; }
        public decimal DisAmount { get; set; }
        public decimal TaxableAmt { get; set; }
        public decimal AftBillDisc { get; set; }
        public decimal ItemCost { get; set; }
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
    public class PURCHASEQUOTFREIGHT
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
    public class PURCHASEORDER
    {
        public PURCHASEORDERHEAD header { get; set; }
        public PURCHASEORDERITEM[] lines { get; set; }
        public PURCHASEORDERFREIGHT[] freight { get; set; }


    }
    public class PURCHASEORDERHEAD
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
    public class PURCHASEORDERITEM
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


        public decimal UnitePrice { get; set; }
        public decimal DisPer { get; set; }
        public decimal DisAmount { get; set; }
        public decimal TaxableAmt { get; set; }
        public decimal AftBillDisc { get; set; }
        public decimal ItemCost { get; set; }
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
    public class PURCHASEORDERFREIGHT
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
    public class PURCHASEGRPO
    {
        public PURCHASEGRPOHEAD header { get; set; }
        public PURCHASEGRPOITEM[] lines { get; set; }
        public PURCHASEGRPOFREIGHT[] freight { get; set; }


    }
    public class PURCHASEGRPOHEAD
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
    public class PURCHASEGRPOITEM
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


        public decimal UnitePrice { get; set; }
        public decimal DisPer { get; set; }
        public decimal DisAmount { get; set; }
        public decimal TaxableAmt { get; set; }
        public decimal AftBillDisc { get; set; }
        public decimal ItemCost { get; set; }
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
    public class PURCHASEGRPOFREIGHT
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
    public class PURCHASEINVOICE
    {
        public PURCHASEINVOICEHEAD header { get; set; }
        public PURCHASEINVOICEITEM[] lines { get; set; }
        public PURCHASEINVOICEFREIGHT[] freight { get; set; }
    }
    public class PURCHASEINVOICEHEAD
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
        public decimal FreightTotal { get; set; }
        public decimal GSTTotal { get; set; }
        public decimal NETTotal { get; set; }
        public decimal AppliedAmount { get; set; }
        public decimal BalanceDue { get; set; }
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
    public class PURCHASEINVOICEITEM
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
        public decimal UnitePrice { get; set; }
        public decimal DisPer { get; set; }
        public decimal DisAmount { get; set; }
        public decimal TaxableAmt { get; set; }
        public decimal AftBillDisc { get; set; }
        public decimal ItemCost { get; set; }
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
        public string? Status { get; set; }
        public string? BaseObjType { get; set; }
        public string? BaseDocEntry { get; set; }
        public string? BaseLine { get; set; }
        public string? UOMID { get; set; }
        public string? UOMCode { get; set; }
        public string HSNID { get; set; }
        public string? HSNCode { get; set; }
        public string WHSID { get; set; }
        public string? WhsCode { get; set; }
        public string? StockInWhs { get; set; }
        public string? AcctCode { get; set; }
        public string? Remarks { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }

    }
    public class PURCHASEINVOICEFREIGHT
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
    public class PURCHASERETURN
    {
        public PURCHASERETURNHEAD header { get; set; }
        public PURCHASERETURNITEM[] lines { get; set; }
        public PURCHASERETURNFREIGHT[] freight { get; set; }
    }
    public class PURCHASERETURNHEAD
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
    public class PURCHASERETURNITEM
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


        public decimal UnitePrice { get; set; }
        public decimal DisPer { get; set; }
        public decimal DisAmount { get; set; }
        public decimal TaxableAmt { get; set; }
        public decimal AftBillDisc { get; set; }
        public decimal ItemCost { get; set; }
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
    public class PURCHASERETURNFREIGHT
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
    public class PURCHASEDEBITENOTE
    {
        public PURCHASEDEBITENOTEHEAD header { get; set; }
        public PURCHASEDEBITENOTEITEM[] lines { get; set; }
        public PURCHASEDEBITENOTEFREIGHT[] freight { get; set; }
    }
    public class PURCHASEDEBITENOTEHEAD
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
    public class PURCHASEDEBITENOTEITEM
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


        public decimal UnitePrice { get; set; }
        public decimal DisPer { get; set; }
        public decimal DisAmount { get; set; }
        public decimal TaxableAmt { get; set; }
        public decimal AftBillDisc { get; set; }
        public decimal ItemCost { get; set; }
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
    public class PURCHASEDEBITENOTEFREIGHT
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
