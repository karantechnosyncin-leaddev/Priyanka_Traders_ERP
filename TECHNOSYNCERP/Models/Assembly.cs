namespace TECHNOSYNCERP.Models
{
    public class Assembly
    {
    }
    public class AssemblyFirstVisualData
    {
        public AssemblyFirstVisualDataHead header { get; set; }
        public AssemblyFirstVisualDataLineItem[] lines { get; set; }


    }
    public class AssemblyFirstVisualDataHead
    {
        public string DocEntry { get; set; }
        public string FYearId { get; set; }
        public string FormatNo { get; set; }
        public string RevNo { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class AssemblyFirstVisualDataLineItem
    {
        public string ID { get; set; }
        public string RevNo { get; set; }
        public string DocEntry { get; set; }

        public DateTime Date { get; set; }
        public string OpetorName { get; set; }
        public string Rating { get; set; }
        public decimal TotalQty { get; set; }
        public decimal OkQty { get; set; }
        public decimal RejQty { get; set; }
        public string ItemID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }

        public string Delatch { get; set; }
        public string ArcRunBend { get; set; }
        public string MixRating { get; set; }
        public string JointProb { get; set; }
        public string BreadProb { get; set; }
        public string MoviCont { get; set; }
        public string OtherProb { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class AssemblyFinalVisualData
    {
        public AssemblyFinalVisualDataHead header { get; set; }
        public AssemblyFinalVisualDataLineItem[] lines { get; set; }
    }
    public class AssemblyFinalVisualDataHead
    {
        public string DocEntry { get; set; }
        public string FYearId { get; set; }
        public string FormatNo { get; set; }
        public string RevNo { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class AssemblyFinalVisualDataLineItem
    {
        public string ID { get; set; }
        public string BaseRefId { get; set; }

        public string RevNo { get; set; }
        public string DocEntry { get; set; }

        public DateTime Date { get; set; }
        public string OpetorName { get; set; }
        public string Rating { get; set; }
        public string SPMP { get; set; }
        public decimal TotalQty { get; set; }
        public decimal OkQty { get; set; }
        public decimal RejQty { get; set; }
        public string Delatch { get; set; }
        public string ItemID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }



        public string DamHousing { get; set; }
        public string DamCover { get; set; }
        public string RedIndicator { get; set; }
        public string SlowReset { get; set; }
        public string KnobStuck { get; set; }
        public string OtherProb { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class DEFPROGRESSREJECTIONRECORD
    {
        public DEFPROGRESSREJECTIONRECORDHead header { get; set; }
        public DEFPROGRESSREJECTIONRECORDLINE[] lines { get; set; }
    }
    public class DEFPROGRESSREJECTIONRECORDHead
    {
        public string DocEntry { get; set; }
        public string FYearId { get; set; }
        public string FormatNo { get; set; }
        public string RevNo { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class DEFPROGRESSREJECTIONRECORDLINE
    {
        public string ID { get; set; }
        public string RevNo { get; set; }
        public string DocEntry { get; set; }

        public DateTime Date { get; set; }
        public string OpetorName { get; set; }
        public string CatNo { get; set; }
        public decimal Qty { get; set; }
        public string UOM { get; set; }
        public string ShortMolding { get; set; }
        public string WhiteSpot { get; set; }
        public string YellowSpot { get; set; }

        public string KidneyDamage { get; set; }
        public string SlotMissing { get; set; }
        public string Breakage { get; set; }
        public string OtherProb { get; set; }


        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class INFPROGRESSREJECTIONRECORD
    {
        public INFPROGRESSREJECTIONRECORDHead header { get; set; }
        public INFPROGRESSREJECTIONRECORDLINE[] lines { get; set; }
    }
    public class INFPROGRESSREJECTIONRECORDHead
    {
        public string DocEntry { get; set; }
        public string FYearId { get; set; }
        public string FormatNo { get; set; }
        public string RevNo { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class INFPROGRESSREJECTIONRECORDLINE
    {
        public string ID { get; set; }
        public string RevNo { get; set; }
        public string DocEntry { get; set; }

        public DateTime Date { get; set; }
        public string OpetorName { get; set; }
        public string CatNo { get; set; }
        public decimal Qty { get; set; }
        public string UOM { get; set; }
        public string ShortMolding { get; set; }
        public string WhiteSpot { get; set; }
        public string YellowSpot { get; set; }

        public string KidneyDamage { get; set; }
        public string SlotMissing { get; set; }
        public string Breakage { get; set; }
        public string OtherProb { get; set; }


        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }

}
