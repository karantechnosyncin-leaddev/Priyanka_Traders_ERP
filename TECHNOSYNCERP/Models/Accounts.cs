namespace TECHNOSYNCERP.Models
{
    public class Accounts
    {
    }
    public class ACCOUNTSPAYMENT
    {
        public ACCOUNTSPAYMENTHEAD header { get; set; }
        public ACCOUNTSPAYMENTROW[] lines { get; set; }
    }
    public class ACCOUNTSPAYMENTHEAD
    {
        public string DocEntry { get; set; }
        public string ItemType { get; set; }
        public string Docnum { get; set; }
        public string DocStatus { get; set; }
        public string PostingDate { get; set; }
        public string DocumentDate { get; set; }
        public string DueDate { get; set; }
        public string FYearId { get; set; }
        public string RefNo { get; set; }
        public string Remarks { get; set; }
        public string LedgerID { get; set; }
        public string LedgerName { get; set; }
        public string LedgerType { get; set; }
        public string LedgerCode { get; set; }

        public decimal NETTotal { get; set; }
        public decimal PayOnAccAmt { get; set; }
        public decimal OpenBalance { get; set; }
        public string PayOnAccCheck { get; set; }

        public decimal LedgerBalance { get; set; }
        public string IsCash { get; set; }
        public string CashLedgerId { get; set; }
        public decimal CashAmt { get; set; }
        public string IsBank { get; set; }
        public string BankLedgerId { get; set; }
        public decimal BankAmt { get; set; }
        public string IsCheque { get; set; }
        public string ChequeBankId { get; set; }
        public string ChequeLedgerId { get; set; }
        public string ChequeDate { get; set; }
        public string ChequeNo { get; set; }
        public decimal ChequeAmt { get; set; }
        public string PayRemarks { get; set; }


        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class ACCOUNTSPAYMENTROW
    {
        public string ID { get; set; }
        public string DocEntry { get; set; }
        public string DocName { get; set; }
        public string DocNum { get; set; }
        public string DocumentDate { get; set; }
        public string DueDate { get; set; }
        public string LedgerType { get; set; }
        public decimal TotalPayment { get; set; }
        public decimal BalanceDue { get; set; }
        public string? Remarks { get; set; }
        public string? LedgerID { get; set; }
        public string? LedgerName { get; set; }
        public string BaseObjType { get; set; }
        public string BaseDocEntry { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class ACCOUNTSRECEIPT
    {
        public ACCOUNTSRECEIPTHEAD header { get; set; }
        public ACCOUNTSRECEIPTROW[] lines { get; set; }
    }
    public class ACCOUNTSRECEIPTHEAD
    {
        public string DocEntry { get; set; }
        public string ItemType { get; set; }
        public string Docnum { get; set; }
        public string DocStatus { get; set; }
        public string PostingDate { get; set; }
        public string DueDate { get; set; }
        public string DocumentDate { get; set; }
        public string FYearId { get; set; }
        public string RefNo { get; set; }
        public string Remarks { get; set; }
        public string LedgerID { get; set; }
        public string LedgerName { get; set; }
        public string LedgerCode { get; set; }
        public string LedgerType { get; set; }

        public decimal NETTotal { get; set; }
        public decimal PayOnAccAmt { get; set; }
        public decimal OpenBalance { get; set; }
        public string PayOnAccCheck { get; set; }

        public decimal LedgerBalance { get; set; }
        public string IsCash { get; set; }
        public string CashLedgerId { get; set; }
        public decimal CashAmt { get; set; }
        public string IsBank { get; set; }
        public string BankLedgerId { get; set; }
        public decimal BankAmt { get; set; }
        public string IsCheque { get; set; }
        public string ChequeBankId { get; set; }
        public string ChequeLedgerId { get; set; }
        public string ChequeDate { get; set; }
        public string ChequeNo { get; set; }
        public decimal ChequeAmt { get; set; }
        public string PayRemarks { get; set; }


        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class ACCOUNTSRECEIPTROW
    {
        public string ID { get; set; }
        public string DocEntry { get; set; }
        public string DocName { get; set; }
        public string DocNum { get; set; }
        public string DocumentDate { get; set; }
        public string DueDate { get; set; }
        public string LedgerType { get; set; }
        public decimal TotalPayment { get; set; }
        public decimal BalanceDue { get; set; }
        public string? Remarks { get; set; }
        public string? LedgerID { get; set; }
        public string? LedgerName { get; set; }
        public string BaseObjType { get; set; }
        public string BaseDocEntry { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class LEDGEROPNING
    {
        public LEDGEROPNINGHEAD header { get; set; }
        public LEDGEROPNINGROW[] lines { get; set; }
    }
    public class LEDGEROPNINGHEAD
    {
        public string DocEntry { get; set; }
        public string Docnum { get; set; }
        public string DocumentDate { get; set; }
        public string PostingDate { get; set; }
        public string DueDate { get; set; }
        public string FYearId { get; set; }
        public string RefNo { get; set; }
        public string Remarks { get; set; }
        public string LedgerId { get; set; }
        public string LedgerName { get; set; }

        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class LEDGEROPNINGROW
    {
        public string ID { get; set; }
        public int LineNum { get; set; }
        public string DocEntry { get; set; }
        public string DueDate { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string? LedgerId { get; set; }
        public string? LedgerName { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class JOURNALENTRY
    {
        public JOURNALENTRYHEAD header { get; set; }
        public JOURNALENTRYROW[] lines { get; set; }
    }
    public class JOURNALENTRYHEAD
    {
        public string DocEntry { get; set; }
        public string Docnum { get; set; }
        public string PostingDate { get; set; }
        public string DocumentDate { get; set; }
        public string DueDate { get; set; }
        public string FYearId { get; set; }
        public string RefNo { get; set; }
        public string Remarks { get; set; }
        public string BaseDocEntry { get; set; }
        public string BaseObjType { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class JOURNALENTRYROW
    {
        public string ID { get; set; }
        public string DocEntry { get; set; }
        public string DocName { get; set; }
        public string DocNum { get; set; }
        public string DocumentDate { get; set; }
        public string LedgerType { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string? Remarks { get; set; }
        public string? LedgerID { get; set; }
        public string? LedgerName { get; set; }
        public string BaseObjType { get; set; }
        public string BaseDocEntry { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class CONTRA
    {
        public JOURNALENTRYHEAD header { get; set; }
        public JOURNALENTRYROW[] lines { get; set; }
    }
    public class CONTRAHEAD
    {
        public string DocEntry { get; set; }
        public string Docnum { get; set; }
        public string PostingDate { get; set; }
        public string DocumentDate { get; set; }
        public string DueDate { get; set; }
        public string FYearId { get; set; }
        public string RefNo { get; set; }
        public string Remarks { get; set; }
        public string BaseDocEntry { get; set; }
        public string BaseObjType { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class CONTRAROW
    {
        public string ID { get; set; }
        public string DocEntry { get; set; }
        public string DocName { get; set; }
        public string DocNum { get; set; }
        public string DocumentDate { get; set; }
        public string LedgerType { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string? Remarks { get; set; }
        public string? LedgerID { get; set; }
        public string? LedgerName { get; set; }
        public string BaseObjType { get; set; }
        public string BaseDocEntry { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class CHEQUEDEPOSIT
    {
        public CHEQUEDEPOSITHEAD header { get; set; }
        public CHEQUEDEPOSITROW[] lines { get; set; }
    }
    public class CHEQUEDEPOSITHEAD
    {
        public string DocEntry { get; set; }
        public string Docnum { get; set; }
        public string PostingDate { get; set; }
        public string DocumentDate { get; set; }
        public string FYearId { get; set; }
        public string RefNo { get; set; }
        public string Remarks { get; set; }
        public string LedgerID { get; set; }
        public string LedgerName { get; set; }
        public decimal NETTotal { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class CHEQUEDEPOSITROW
    {
        public string ID { get; set; }
        public string DocEntry { get; set; }
        public string DocNum { get; set; }
        public string DocumentDate { get; set; }
        public string ChequeDate { get; set; }
        public decimal ChequeAmt { get; set; }
        public string ChequeNo { get; set; }
        public string? LedgerID { get; set; }
        public string? LedgerName { get; set; }
        public string BankName { get; set; }
        public string BankId { get; set; }
        public string? Remarks { get; set; }
        public string? RefNo { get; set; }
        public string BaseObjType { get; set; }
        public string BaseDocEntry { get; set; }
        public string ChequeLedgerId { get; set; }
    
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }

}
