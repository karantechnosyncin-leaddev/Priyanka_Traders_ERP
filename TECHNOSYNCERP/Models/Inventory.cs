namespace TECHNOSYNCERP.Models
{
    public class Inventory
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        // Additional properties can be added as needed
    }

    public class BulkItemUploadModel
    {
        public BulkItem[] Items { get; set; }
    }
    public class PRISELIST
    {
        public PriceSetup[] lines { get; set; }
    }
   
    public class BulkItem
    {
        public ItemData ItemData { get; set; }
        public PriceSetup[] PriceSetups { get; set; }
        public InventoryDetail[] InventoryDetails { get; set; }
    }

    public class ItemData
    {
        public string ItemID { get; set; }
        public string ItemCode { get; set; }
        public string EanCode { get; set; }
        public string ItemName { get; set; }
        public string ItemTyp { get; set; }
        public string UomCode { get; set; }
        public string UomID { get; set; }
        public string ItmGrpName { get; set; }
        public string ItmGrpID { get; set; }
        public string HSNCode { get; set; }
        public string HSNID { get; set; }
        public string PurItem { get; set; }
        public string SaleItem { get; set; }
        public string InvItem { get; set; }
        public string TaxCode { get; set; }
        public string TaxCodeId { get; set; }
        public string TypeOfSupp { get; set; }
        public string IsActive { get; set; }
        public string IsActiveFrmDate { get; set; }
        public string IsActiveToDate { get; set; }
        public decimal? Weight1 { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CretedByUId { get; set; }
        public string CretedByUName { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedByUId { get; set; }
        public string UpdatedByUName { get; set; }
    }

    public class PriceSetup
    {
        public string PriceSetID { get; set; }
        public string ItemID { get; set; }
        public string ItemCode { get; set; }
        public string ConversionCode { get; set; }
        public string UomID { get; set; }
        public decimal ConveQTY { get; set; }
        public string BaseUom { get; set; }
        public string BaseUomID { get; set; }
        public decimal MRP { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmount { get; set; }
        public string IsActive { get; set; }
        public string CreatedDate { get; set; }
        public string CretedByUId { get; set; }
        public string CretedByUName { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedByUId { get; set; }
        public string UpdatedByUName { get; set; }
    }

    public class InventoryDetail
    {
        public string InvtDetId { get; set; }
        public string ItemId { get; set; }
        public string ItemCode { get; set; }
        public string WhsCode { get; set; }
        public string WhsId { get; set; }
        public decimal MinStock { get; set; }
        public decimal MaxStock { get; set; }
        public string WhsLocked { get; set; }
        public decimal ItemCost { get; set; }
        public decimal Stock { get; set; }
        public DateTime CretedDate { get; set; }
        public string CretedByUId { get; set; }
        public string CretedByUName { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedByUId { get; set; }
        public string UpdatedByUName { get; set; }
    }

    public class GOODSRECEIPT
    {
        public GOODSHEAD header { get; set; }
        public GOODSRECEIPTITEM[] lines { get; set; }
    }
    public class GOODSHEAD
    {
        public string DocEntry { get; set; }
        public string Docnum { get; set; }
        public string DocStatus { get; set; }
        public string PostingDate { get; set; }
        public string DocumentDate { get; set; }
        public string FYearId { get; set; }
        public string RefNo { get; set; }
        public string Remarks { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class GOODSRECEIPTITEM
    {
        public string ID { get; set; }
        public int LineNum { get; set; }
        public string DocEntry { get; set; }
        public string? ItemID { get; set; }
        public string? EanCode { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string Qty { get; set; }
        public decimal ItemCost { get; set; }
        public string Price { get; set; }
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

    public class INVENTORYTRANSFER
    {
        public INVENTORYTRANSFERHEAD header { get; set; }
        public INVENTORYTRANSFERITEM[] lines { get; set; }
    }
    public class INVENTORYTRANSFERHEAD
    {
        public string DocEntry { get; set; }
        public string Docnum { get; set; }            // Document Number (string because sometimes alphanumeric)
        public string DocStatus { get; set; }            // Document Number (string because sometimes alphanumeric)
        public DateTime PostingDate { get; set; }     // Posting Date
        public DateTime DocumentDate { get; set; }    // Document Date
        public string FromWhsId { get; set; }           // From Warehouse ID
        public string FromWhsCode { get; set; }       // From Warehouse Code
        public string ToWhsId { get; set; }             // To Warehouse ID
        public string ToWhsCode { get; set; }         // To Warehouse Code
        public string EmployeeID { get; set; }          // Employee linked to document
        public string FYearId { get; set; }             // Financial Year ID
        public string RefNo { get; set; }             // Reference Number
        public string Remarks { get; set; }           // Header Remarks
        public decimal Weight { get; set; }

        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class INVENTORYTRANSFERITEM
    {
        public string ID { get; set; }
        public string DocEntry { get; set; }               // Parent Document Entry
        public string Status { get; set; }               // Parent Document Entry
        public int LineNum { get; set; }                // Line Number
        public string ItemID { get; set; }                // Item Master ID
        public string EanCode { get; set; }             // Barcode
        public string ItemCode { get; set; }            // Item Code (text)
        public string ItemName { get; set; }            // Item Name
        public decimal Qty { get; set; }                // Quantity
        //public decimal? OpenQty { get; set; }           // Open Qty (if applicable)
        public string UOMID { get; set; }                 // UOM Master ID
        public string UOMCode { get; set; }             // UOM Code
        public string HSNID { get; set; }                 // HSN ID
        public string HSNCode { get; set; }             // HSN Value
        public string FromWhsCode { get; set; }         // From Warehouse Code
        public string FromWhsID { get; set; }             // From Warehouse ID
        public string ToWhsCode { get; set; }           // To Warehouse Code
        public string ToWhsID { get; set; }               // To Warehouse ID
        public string StockInWhs { get; set; }        // Available stock in warehouse
        public decimal? ItemCost { get; set; }          // Cost per item
        public string AcctCode { get; set; }            // Account Code (GL)
        public decimal? Weight { get; set; }          // Cost per item
        public string Remarks { get; set; }             // Line level remarks


        //public string? BaseLine { get; set; }              // Base Document LineNum
        //public string? BaseObj { get; set; }               // Base Document Type (ObjType)
        //public string? BaseDocEntry { get; set; }          // Base Document DocEntry
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }

    }

    public class INVENTORYREQUEST
    {
        public INVENTORYREQUESTHEAD header { get; set; }
        public INVENTORYREQUESTITEM[] lines { get; set; }
    }
    public class INVENTORYREQUESTHEAD
    {
        public string DocEntry { get; set; }          
        public string Docnum { get; set; }            // Document Number (string because sometimes alphanumeric)
        public string DocStatus { get; set; }            // Document Number (string because sometimes alphanumeric)
        public DateTime PostingDate { get; set; }     // Posting Date
        public DateTime DocumentDate { get; set; }    // Document Date
        public string FromWhsId { get; set; }           // From Warehouse ID
        public string FromWhsCode { get; set; }       // From Warehouse Code
        public string ToWhsId { get; set; }             // To Warehouse ID
        public string ToWhsCode { get; set; }         // To Warehouse Code
        public string EmployeeID { get; set; }          // Employee linked to document
        public string FYearId { get; set; }             // Financial Year ID
        public string RefNo { get; set; }             // Reference Number
        public string Remarks { get; set; }           // Header Remarks
        public decimal Weight { get; set; }

        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class INVENTORYREQUESTITEM
    {
        public string ID { get; set; }
        public string DocEntry { get; set; }               // Parent Document Entry
        public int LineNum { get; set; }                // Line Number
        public string ItemID { get; set; }                // Item Master ID
        public string EanCode { get; set; }             // Barcode
        public string ItemCode { get; set; }            // Item Code (text)
        public string ItemName { get; set; }            // Item Name
        public decimal Qty { get; set; }                // Quantity
        public decimal? OpenQty { get; set; }           // Open Qty (if applicable)
        public string UOMID { get; set; }                 // UOM Master ID
        public string UOMCode { get; set; }             // UOM Code
        public string HSNID { get; set; }                 // HSN ID
        public string HSNCode { get; set; }             // HSN Value
        public string FromWhsCode { get; set; }         // From Warehouse Code
        public string FromWhsID { get; set; }             // From Warehouse ID
        public string ToWhsCode { get; set; }           // To Warehouse Code
        public string ToWhsID { get; set; }               // To Warehouse ID
        public string StockInWhs { get; set; }        // Available stock in warehouse
        public decimal? ItemCost { get; set; }          // Cost per item
        public decimal? Weight { get; set; }          // Cost per item
        public string? Status { get; set; }          // Cost per item
        public string AcctCode { get; set; }            // Account Code (GL)
        public string Remarks { get; set; }             // Line level remarks
        public string? BaseLine { get; set; }              // Base Document LineNum
        public string? BaseObj { get; set; }               // Base Document Type (ObjType)
        public string? BaseDocEntry { get; set; }          // Base Document DocEntry
        public int? TargetLine { get; set; }        
        public string? TargetObj { get; set; }         
        public string? TargetEntry { get; set; }       
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }

    }

    public class INVENTORYREQUESTIN
    {
        public INVENTORYREQUESTINHEAD header { get; set; }
        public INVENTORYREQUESTINITEM[] lines { get; set; }
    }
    public class INVENTORYREQUESTINHEAD
    {
        public string DocEntry { get; set; }
        public string Docnum { get; set; }            // Document Number (string because sometimes alphanumeric)
        public string DocStatus { get; set; }            // Document Number (string because sometimes alphanumeric)
        public DateTime PostingDate { get; set; }     // Posting Date
        public DateTime DocumentDate { get; set; }    // Document Date
        public string Status { get; set; }           // From Status ID
        public string FromWhsId { get; set; }           // From Warehouse ID
        public string FromWhsCode { get; set; }       // From Warehouse Code
        public string ToWhsId { get; set; }             // To Warehouse ID
        public string ToWhsCode { get; set; }         // To Warehouse Code
        public string EmployeeID { get; set; }          // Employee linked to document
        public string FYearId { get; set; }             // Financial Year ID
        public string RefNo { get; set; }             // Reference Number
        public string Remarks { get; set; }           // Header Remarks
        public decimal Weight { get; set; }

        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class INVENTORYREQUESTINITEM
    {
        public string ID { get; set; }
        public string DocEntry { get; set; }               // Parent Document Entry
        public int LineNum { get; set; }                // Line Number
        public string ItemID { get; set; }                // Item Master ID
        public string EanCode { get; set; }             // Barcode
        public string ItemCode { get; set; }            // Item Code (text)
        public string ItemName { get; set; }            // Item Name
        public decimal Qty { get; set; }                // Quantity
        public decimal? OpenQty { get; set; }           // Open Qty (if applicable)
        public string UOMID { get; set; }                 // UOM Master ID
        public string UOMCode { get; set; }             // UOM Code
        public string HSNID { get; set; }                 // HSN ID
        public string HSNCode { get; set; }             // HSN Value
        public string FromWhsCode { get; set; }         // From Warehouse Code
        public string FromWhsID { get; set; }             // From Warehouse ID
        public string ToWhsCode { get; set; }           // To Warehouse Code
        public string ToWhsID { get; set; }               // To Warehouse ID
        public string StockInWhs { get; set; }        // Available stock in warehouse
        public decimal? ItemCost { get; set; }          // Cost per item
        public decimal? Weight { get; set; }          // Cost per item
        public string? Status { get; set; }          // Cost per item
        public string AcctCode { get; set; }            // Account Code (GL)
        public string Remarks { get; set; }             // Line level remarks
        public string? BaseLine { get; set; }              // Base Document LineNum
        public string? BaseObj { get; set; }               // Base Document Type (ObjType)
        public string? BaseDocEntry { get; set; }          // Base Document DocEntry

        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }

    }

    public class INVENTORYREQUESTOUT
    {
        public INVENTORYREQUESTOUTHEAD header { get; set; }
        public INVENTORYREQUESTOUTITEM[] lines { get; set; }
    }
    public class INVENTORYREQUESTOUTHEAD
    {
        public string DocEntry { get; set; }
        public string Docnum { get; set; }            // Document Number (string because sometimes alphanumeric)
        public string DocStatus { get; set; }            // Document Number (string because sometimes alphanumeric)
        public DateTime PostingDate { get; set; }     // Posting Date
        public DateTime DocumentDate { get; set; }    // Document Date
        public string FromWhsId { get; set; }           // From Warehouse ID
        public string FromWhsCode { get; set; }       // From Warehouse Code
        public string ToWhsId { get; set; }             // To Warehouse ID
        public string ToWhsCode { get; set; }         // To Warehouse Code
        public string EmployeeID { get; set; }          // Employee linked to document
        public string FYearId { get; set; }             // Financial Year ID
        public string RefNo { get; set; }             // Reference Number
        public string Remarks { get; set; }           // Header Remarks
        public decimal Weight { get; set; }

        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class INVENTORYREQUESTOUTITEM
    {
        public string ID { get; set; }
        public string DocEntry { get; set; }               // Parent Document Entry
        public string Status { get; set; }               // Status Document Entry
        public int LineNum { get; set; }                // Line Number
        public string ItemID { get; set; }                // Item Master ID
        public string EanCode { get; set; }             // Barcode
        public string ItemCode { get; set; }            // Item Code (text)
        public string ItemName { get; set; }            // Item Name
        public decimal Qty { get; set; }                // Quantity
        public decimal? OpenQty { get; set; }           // Open Qty (if applicable)
        public string UOMID { get; set; }                 // UOM Master ID
        public string UOMCode { get; set; }             // UOM Code
        public string HSNID { get; set; }                 // HSN ID
        public string HSNCode { get; set; }             // HSN Value
        public string FromWhsCode { get; set; }         // From Warehouse Code
        public string FromWhsID { get; set; }             // From Warehouse ID
        public string ToWhsCode { get; set; }           // To Warehouse Code
        public string ToWhsID { get; set; }               // To Warehouse ID
        public string StockInWhs { get; set; }        // Available stock in warehouse
        public decimal? ItemCost { get; set; }          // Cost per item
        public decimal? Weight { get; set; }          // Cost per item
        public string AcctCode { get; set; }            // Account Code (GL)
        public string Remarks { get; set; }             // Line level remarks
        public string? BaseLine { get; set; }              // Base Document LineNum
        public string? BaseObj { get; set; }               // Base Document Type (ObjType)
        public string? BaseDocEntry { get; set; }          // Base Document DocEntry
        public int? TargetLine { get; set; }
        public string? TargetObj { get; set; }
        public string? TargetEntry { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }

    }

    public class INVENTORYCOUNTING
    {
        public INVENTORYCOUNTINGHEAD header { get; set; }
        public INVENTORYCOUNTINGITEM[] lines { get; set; }
    }
    public class INVENTORYCOUNTINGHEAD
    {
        public string DocEntry { get; set; }
        public string Docnum { get; set; }            // Document Number (string because sometimes alphanumeric)
        public string DocStatus { get; set; }            // Document Number (string because sometimes alphanumeric)
        public string CountDate { get; set; }     // Posting Date
        public string CountTime { get; set; }     // Posting Date
        public string DocumentDate { get; set; }    // Document Date
        public string WhsId { get; set; }           // From Warehouse ID
        public string WhsCode { get; set; }           // From Warehouse ID
        
        public string EmployeeID { get; set; }          // Employee linked to document
        public string FYearId { get; set; }             // Financial Year ID
        public string RefNo { get; set; }             // Reference Number
        public string Remarks { get; set; }           // Header Remarks

        public decimal Weight { get; set; }

        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class INVENTORYCOUNTINGITEM
    {
        public string ID { get; set; }
        public string DocEntry { get; set; } 
        public int LineNum { get; set; }                // Line Number              // Parent Document Entry
        public string Status { get; set; }               // Parent Document Entry
        public string ItemID { get; set; }                // Item Master ID
        public string EanCode { get; set; }             // Barcode
        public string ItemCode { get; set; }            // Item Code (text)
        public string ItemName { get; set; }            // Item Name
        public decimal Qty { get; set; }                // Quantity
        public decimal CountedQty { get; set; }                // Quantity
        public decimal Price { get; set; }                // Quantity
        public decimal Variance { get; set; }                // Quantity
        public decimal VariancePer { get; set; }                // Quantity
        public decimal? OpenQty { get; set; }           // Open Qty (if applicable)
        public string UOMID { get; set; }                 // UOM Master ID
        public string UOMCode { get; set; }             // UOM Code
        public string HSNID { get; set; }                 // HSN ID
        public string HSNCode { get; set; }             // HSN Value
        public string WhsCode { get; set; }         // From Warehouse Code
        public string WhsID { get; set; }             // From Warehouse ID

        //public string FromWhsCode { get; set; }         // From Warehouse Code
        //public string FromWhsID { get; set; }             // From Warehouse ID
        //public string ToWhsCode { get; set; }           // To Warehouse Code
        //public string ToWhsID { get; set; }               // To Warehouse ID
        public string StockInWhs { get; set; }        // Available stock in warehouse
        //public decimal? ItemCost { get; set; }          // Cost per item
        public string AcctCode { get; set; }            // Account Code (GL)
        public decimal? Weight { get; set; }          // Cost per item
        public string Remarks { get; set; }             // Line level remarks


        public string? BaseLine { get; set; }              // Base Document LineNum
        public string? BaseObj { get; set; }               // Base Document Type (ObjType)
        public string? BaseDocEntry { get; set; }          // Base Document DocEntry
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }

    }

    public class INVENTORYCOUNTINGNEW
    {
        public INVENTORYCOUNTINGHEADNEW header { get; set; }
        public INVENTORYCOUNTINGITEMNEW[] lines { get; set; }
    }
    public class INVENTORYCOUNTINGHEADNEW
    {
        public string DocEntry { get; set; }          // ""
        public string OBJType { get; set; }            // "IICN"
        public string Docnum { get; set; }             // "6"
        public string DocStatus { get; set; }          // "O"

        public string CountDate { get; set; }          // "2025-12-29"
        public string CountTime { get; set; }          // "12:13"
        public string DocumentDate { get; set; }       // "2025-12-29"

        public string FYearId { get; set; }            // "1"
        public string RefNo { get; set; }              // "0"
        public string Remarks { get; set; }            // " "

        public string Weight { get; set; }             // "0.00"

        // 🔹 Audit (optional – frontend not sending these)
        public string? CretedByUId { get; set; }
        public string? CretedByUName { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string? UpdatedByUId { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
    public class INVENTORYCOUNTINGITEMNEW
    {
        public string ID { get; set; }                 // ""

        public string DocEntry { get; set; }           // ""

        public int LineNum { get; set; }               // 1

        public string ItemID { get; set; }             // "96"
        public string ItemCode { get; set; }           // "00089"
        public string ItemName { get; set; }           // "1 MODULE GANG BOX"

        public string HSNID { get; set; }              // "00000"
        public string HSNCode { get; set; }            // "00000"

        public string WhsID { get; set; }                 // 1
        public string WhsCode { get; set; }            // "01"

        public decimal StockInWhs { get; set; }        // 7304

        public string EmployeeID { get; set; }         // "1"
        public string StockTakerName { get; set; }         // "1"


        public decimal Price { get; set; }             // 0
        public string Qty { get; set; }             // 0
        public string OpenQty { get; set; }             // 0
        public string UOMID { get; set; }             // 0
        public string UOMCode { get; set; }             // 0
        public string AcctCode { get; set; }           // "10"

        public int BaseDocEntry { get; set; }           // 0
        public string BaseObj { get; set; }             // ""
        public string BaseLine { get; set; }               // 0

        public string EanCode { get; set; }             // "00089"

        public string Remarks { get; set; }             // ""

        public string Weight { get; set; }             // 100
        public string CountedQty { get; set; }             // 100
        public string Variance { get; set; }             // 100
        public string VariancePer { get; set; }             // 100

        public string Status { get; set; }              // "O"

        // 🔹 Audit (server side)
        public string? CretedByUId { get; set; }
        public string? CretedByUName { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string? UpdatedByUId { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class INVENTORYPOSTINGNEW
    {
        public INVENTORYPOSTINGHEADNEW header { get; set; }
        public INVENTORYPOSTINGITEMNEW[] lines { get; set; }
    }
    public class INVENTORYPOSTINGHEADNEW
    {
        public string DocEntry { get; set; }          // ""
        public string OBJType { get; set; }            // "IICN"
        public string Docnum { get; set; }             // "6"
        public string DocStatus { get; set; }          // "O"

        public string CountDate { get; set; }          // "2025-12-29"
        public string CountTime { get; set; }          // "12:13"           
        public string DocumentDate { get; set; }       // "2025-12-29"

        public string FYearId { get; set; }            // "1"
        public string RefNo { get; set; }              // "0"
        public string Remarks { get; set; }            // " "

        public string Weight { get; set; }             // "0.00"

        // 🔹 Audit (optional – frontend not sending these)
        public string? CretedByUId { get; set; }
        public string? CretedByUName { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string? UpdatedByUId { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
    public class INVENTORYPOSTINGITEMNEW
    {
        public string ID { get; set; }                 // ""

        public string DocEntry { get; set; }           // ""

        public int LineNum { get; set; }               // 1

        public string ItemID { get; set; }             // "96"
        public string ItemCode { get; set; }           // "00089"

        public string EanCode { get; set; }            // "01"
        public string ItemName { get; set; }           // "1 MODULE GANG BOX"

        //public string HSNID { get; set; }              // "00000"
        //public string HSNCode { get; set; }            // "00000"

        public string WhsID { get; set; }                 // 1
        public string WhsCode { get; set; }            // "01"

        public string StockInWhs { get; set; }        // 7304
        public string CountedQty { get; set; }        // 7304

        public string EmployeeID { get; set; }         // "1"
        public string LogEmployes { get; set; }         // "1"
        public string StockTakerName { get; set; }         // "1"


        public decimal Price { get; set; }             // 0
        public string AcctCode { get; set; }           // "10"

        public string BaseDocEntry { get; set; }           // 0
        public string BaseObj { get; set; }             // ""
        public string BaseLine { get; set; }               // 0

        public string Remarks { get; set; }             // ""

        public string Weight { get; set; }             // 100

        public string Status { get; set; }              // "O"

        // 🔹 Audit (server side)
        public string? CretedByUId { get; set; }
        public string? CretedByUName { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string? UpdatedByUId { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }



    public class INVENTORYPOSTING
    {
        public INVENTORYPOSTINGHEAD header { get; set; }
        public INVENTORYPOSTINGITEM[] lines { get; set; }
    }
    public class INVENTORYPOSTINGHEAD
    {
        public string DocEntry { get; set; }
        public string Docnum { get; set; }            // Document Number (string because sometimes alphanumeric)
        public string DocStatus { get; set; }            // Document Number (string because sometimes alphanumeric)
        public string CountDate { get; set; }     // Posting Date
        public string CountTime { get; set; }     // Posting Date
        public string DocumentDate { get; set; }    // Document Date
        public string WhsId { get; set; }           // From Warehouse ID
        public string WhsCode { get; set; }           // From Warehouse ID

        public string EmployeeID { get; set; }          // Employee linked to document
        public string FYearId { get; set; }             // Financial Year ID
        public string RefNo { get; set; }             // Reference Number
        public string Remarks { get; set; }           // Header Remarks

        public decimal Weight { get; set; }

        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class INVENTORYPOSTINGITEM
    {
        public string ID { get; set; }
        public string DocEntry { get; set; }
        public int LineNum { get; set; }                // Line Number              // Parent Document Entry
        public string Status { get; set; }               // Parent Document Entry
        public string ItemID { get; set; }                // Item Master ID
        public string EanCode { get; set; }             // Barcode
        public string ItemCode { get; set; }            // Item Code (text)
        public string ItemName { get; set; }            // Item Name
        public decimal Qty { get; set; }                // Quantity
        public decimal CountedQty { get; set; }                // Quantity
        public decimal Price { get; set; }                // Quantity
        public decimal Variance { get; set; }                // Quantity
        public decimal VariancePer { get; set; }                // Quantity
        public decimal? OpenQty { get; set; }           // Open Qty (if applicable)
        public string UOMID { get; set; }                 // UOM Master ID
        public string UOMCode { get; set; }             // UOM Code
        public string HSNID { get; set; }                 // HSN ID
        public string HSNCode { get; set; }             // HSN Value
        public string WhsCode { get; set; }         // From Warehouse Code
        public string WhsID { get; set; }             // From Warehouse ID

        //public string FromWhsCode { get; set; }         // From Warehouse Code
        //public string FromWhsID { get; set; }             // From Warehouse ID
        //public string ToWhsCode { get; set; }           // To Warehouse Code
        //public string ToWhsID { get; set; }               // To Warehouse ID
        public string StockInWhs { get; set; }        // Available stock in warehouse
        //public decimal? ItemCost { get; set; }          // Cost per item
        public string AcctCode { get; set; }            // Account Code (GL)
        public decimal? Weight { get; set; }          // Cost per item
        public string Remarks { get; set; }             // Line level remarks


        public string? BaseLine { get; set; }              // Base Document LineNum
        public string? BaseObj { get; set; }               // Base Document Type (ObjType)
        public string? BaseDocEntry { get; set; }          // Base Document DocEntry
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }

    }

    public class GOODSISSUE
    {
        public GOODSISSUEHEAD header { get; set; }
        public GOODSISSUETITEM[] lines { get; set; }
    }
    public class GOODSISSUEHEAD
    {
        public string DocEntry { get; set; }
        public string Docnum { get; set; }
        public string DocStatus { get; set; }
        public string PostingDate { get; set; }
        public string DocumentDate { get; set; }
        public string FYearId { get; set; }
        public string RefNo { get; set; }
        public string Remarks { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class GOODSISSUETITEM
    {
        public string ID { get; set; }
        public int LineNum { get; set; }
        public string DocEntry { get; set; }
        public string? ItemID { get; set; }
        public string? EanCode { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string Qty { get; set; }
        public string Price { get; set; }
        public decimal ItemCost { get; set; }
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

 

    public class MATERIALIN
    {
        public MATERIALINHEAD header { get; set; }
        public MATERIALINITEM[] lines { get; set; }
    }
    public class MATERIALINHEAD
    {
        public string DocEntry { get; set; }
        public string Docnum { get; set; }
        public string DocStatus { get; set; }
        public string PostingDate { get; set; }
        public string DeliveryDate { get; set; }
        public string DocumentDate { get; set; }
        public string FYearId { get; set; }
        public string RefNo { get; set; }
        public string Remarks { get; set; }
        public string LedgerID { get; set; }
        public string LedgerCode { get; set; }
        public string LedgerName { get; set; }
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
    public class MATERIALINITEM
    {
        public string ID { get; set; }
        public int LineNum { get; set; }
        public string DocEntry { get; set; }
        public string? ItemID { get; set; }
        public string? EanCode { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public decimal Quantity { get; set; }
        public decimal ItemCost { get; set; }
        public decimal UnitePrice { get; set; }
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
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }

    }
    public class MATERIALOUT
    {
        public MATERIALOUTHEAD header { get; set; }
        public MATERIALOUTITEM[] lines { get; set; }
    }
    public class MATERIALOUTHEAD
    {
        public string DocEntry { get; set; }
        public string Docnum { get; set; }
        public string DocStatus { get; set; }
        public string PostingDate { get; set; }
        public string DeliveryDate { get; set; }
        public string DocumentDate { get; set; }
        public string FYearId { get; set; }
        public string RefNo { get; set; }
        public string Remarks { get; set; }
        public string LedgerID { get; set; }
        public string LedgerCode { get; set; }
        public string LedgerName { get; set; }
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
    public class MATERIALOUTITEM
    {
        public string ID { get; set; }
        public int LineNum { get; set; }
        public string DocEntry { get; set; }
        public string? ItemID { get; set; }
        public string? EanCode { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public decimal Quantity { get; set; }
        public decimal OpenQuantity { get; set; }
        public decimal BaseQuantity { get; set; }

        public decimal ItemCost { get; set; }
        public decimal UnitePrice { get; set; }
        public decimal DisPer { get; set; }
        public decimal DisAmount { get; set; }
        public decimal TaxableAmt { get; set; }
        public decimal AftBillDisc { get; set; }
        public string TaxCodeId { get; set; }
        public string TaxCode { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }

        public string? Status { get; set; }
        public string? BaseObjType { get; set; }
        public string? BaseDocEntry { get; set; }
        public string? BaseLine { get; set; }

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
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }

    }
    public class OPENINGBALANCE
    {
        public OPENINGBALANCEHEAD header { get; set; }
        public OPENINGBALANCEROW[] lines { get; set; }
    }
    public class OPENINGBALANCEHEAD
    {
        public string DocEntry { get; set; }
        public string Docnum { get; set; }
        public string DocStatus { get; set; }
        public string PostingDate { get; set; }
        public string DocumentDate { get; set; }
        public string FYearId { get; set; }
        public string RefNo { get; set; }
        public string Remarks { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class OPENINGBALANCEROW
    {
        public string ID { get; set; }
        public int LineNum { get; set; }
        public string DocEntry { get; set; }
        public string? ItemID { get; set; }
        public string? EanCode { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string Qty { get; set; }
        public string Price { get; set; }
        public decimal ItemCost { get; set; }
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
}

