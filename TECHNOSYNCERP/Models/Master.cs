using System.ComponentModel.DataAnnotations;

namespace TECHNOSYNCERP.Models
{
    public class Master
    {
    }
    public class WhsMaster
    {
        public string ID { get; set; }
        public string WhsCode { get; set; }
        public string WhsName { get; set; }
        public bool Locked { get; set; }
        public string CretedByUId { get; set; }
        public string CretedByUName { get; set; }
        public string? CreatedDate { get; set; }
        public string UpdatedByUId { get; set; }
        public string UpdatedByUName { get; set; }
        public string? UpdatedDate { get; set; }
    }
    public class Taxtype_mst
    {
        public string? TaxTypeId { get; set; }
        public string? TaxTypeCode { get; set; }
        public string? TaxTypePer { get; set; }
        public string? IsActive { get; set; }
        public string? errormessage { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class COUNTRY
    {
        public string Coun_Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? IsActive { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class STATE
    {
        public string Stat_Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Coun_Id { get; set; }
        public string? GstCode { get; set; }
        public string? IsActive { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class EmployeeMaster
    {
        public string EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string IsActive { get; set; }
        public string Position { get; set; }
        public string RoleID { get; set; }
        public string PhoneNo { get; set; }
        public string MobilePhone { get; set; }
        public string Addres { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Coun_Id { get; set; }
        public string Stat_Id { get; set; }
        public string CretedByUId { get; set; }
        public string CretedByUName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedByUId { get; set; }
        public string UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
    public class Role
    {
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public string IsActive { get; set; }

        public string CreatedByUId { get; set; }
        public string CreatedByUName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedByUId { get; set; }
        public string UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
    public class LinkUser
    {
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
    public class BAKNS
    {
        public string BankID { get; set; }
        public string Coun_Id { get; set; }
        public string? BankCode { get; set; }
        public string? BankName { get; set; }
        public string? IsActive { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CretedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class COMPANY
    {
        public string CompanyID { get; set; }
        public string ComName { get; set; }
        public string? ComAddre { get; set; }
        public string? City { get; set; }
        public string? PinCode { get; set; }
        public string? Coun_Id { get; set; }
        public string? Stat_Id { get; set; }
        public string? MobileNo { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
        public string? BankID { get; set; }
        public string? BankAcctNo { get; set; }
        public string? BankAcctName { get; set; }
        public string? GSTIN { get; set; }
        public string? CINNO { get; set; }
        public string? TANNO { get; set; }
        public string? PAN { get; set; }
        public string? ValidFrom { get; set; }
        public string? ValidTo { get; set; }
        public string? IFSCCode { get; set; }
        public string? IsActive { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class TaxCombinationSetup
    {

        public string? TaxCodeId { get; set; }
        public string? TaxCode { get; set; }
        public string? TaxFormula { get; set; }
        public string? TaxCalRate { get; set; }
        public string? IsActive { get; set; }
        public string? Freight_app { get; set; }
        public string? UpdatedByUName { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? errormessage { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUName { get; set; }
        public string? CretedByUId { get; set; }
    }
    public class TaxCombibnationForm
    {

        public string? TaxRate { get; set; }
        public string? TaxTypeId { get; set; }
        public string? TaxCodeFormId { get; set; }
        public string? TaxCodeId { get; set; }

    }
    public class HSN
    {
        public string HSNID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string TaxCodeId { get; set; }
        public string Class { get; set; }
        public string CretedByUId { get; set; }
        public string CretedByUName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedByUId { get; set; }
        public string UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
    public class UOM
    {
        public string UomID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string IsActive { get; set; }

        public string CretedByUId { get; set; }
        public string CretedByUName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedByUId { get; set; }
        public string UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
   
    public class ITEMGRP
    {
        public string ItmGrpID { get; set; }
        public string ItmGrpNam { get; set; }
        public string GrpValMeth { get; set; }
        public string ItemCateg { get; set; }
        public string IsActive { get; set; }

        public string CretedByUId { get; set; }
        public string CretedByUName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedByUId { get; set; }
        public string UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
    public class ITEMMASTER {

        public ITEM itemData { get; set; }
        public PRISESETUP[] PriceSetup { get; set; }
        public WHSDETAILS[] WhsDetails { get; set; }
    }
    public class ITEM
    {
        public string? ItemID { get; set; }
        public string ItemCode { get; set; }
        public string EanCode { get; set; }
        public string ItemName { get; set; }
        public string ItemTyp { get; set; }
        public string UomID { get; set; }
        public string ItmGrpID { get; set; }
        public string HSNID { get; set; }
        public string PriListNum { get; set; }
        public string PurItem { get; set; }
        public string Weight1 { get; set; }
        public string SaleItem { get; set; }
        public string InvItem { get; set; }
        public string TaxCodeId { get; set; }
        public string TypeOfSupp { get; set; }
        public string IsActiveFrmDate { get; set; }
        public string IsActiveToDate { get; set; }
        public string IsActive { get; set; }
        public string CretedByUId { get; set; }
        public string CretedByUName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedByUId { get; set; }
        public string UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
    public class PRISESETUP
    {
        public string PriceSetID { get; set; }
        public string UomID { get; set; }
        public string ItemID { get; set; }
        public string ConveQTY { get; set; }
        public string BaseUomID { get; set; }
        public string MRP { get; set; }
        public string Discount { get; set; }
        public string NetAmount { get; set; }

        public string IsActive { get; set; }
        public string CretedByUId { get; set; }
        public string CretedByUName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedByUId { get; set; }
        public string UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
    public class WHSDETAILS
    {
        public string InvtDetId { get; set; }
        public string ItemId { get; set; }
        public string MaxStock { get; set; }
        public string MinStock { get; set; }
        public string WhsId { get; set; }
        public string WhsLocked { get; set; }
        public string CretedByUId { get; set; }
        public string CretedByUName { get; set; }
        public DateTime? CretedDate { get; set; }
        public string UpdatedByUId { get; set; }
        public string UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
    public class LEDGER
    { 
        public LEDGERDATA[] Ledgers { get; set; }
        public ADDRESS[] Address { get; set; }
        public BANKINFO[] BankInfo { get; set; }
    }
    public class LEDGERDATA
    {
        public string ID { get; set; }
        public string GroupName { get; set; }
        public string BaseID { get; set; }
        public string? Postable { get; set; }
        public string LedgerType { get; set; }
        public string ControlAcc { get; set; }
        public string CashAcc { get; set; }
        public string BankAcc { get; set; }
        public string Code { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public string ContPer { get; set; }
        public string ContMobiNo { get; set; }
        public string GSTIN { get; set; }
        public string GSTINTYP { get; set; }
        public string PANNO { get; set; }
        public string TDSID { get; set; }
        public string CredID { get; set; }
        public string CredAmt { get; set; }
        public string TDSApplicable { get; set; }
        public string AssesseeTyp { get; set; }

        public string IsActive { get; set; }
        public string CretedByUId { get; set; }
        public string CretedByUName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedByUId { get; set; }
        public string UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
    public class ADDRESS
    {
        public string AddID { get; set; }
        public string AddressId { get; set; }
        public string AdresType { get; set; }
        public string LedgerID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PinCode { get; set; }
        public string City { get; set; }
        public string GSTIN { get; set; }
        public string GSTINTYP { get; set; }
        public string Stat_Id { get; set; }
        public string Coun_Id { get; set; }
        public string IsActive { get; set; }
        public string CretedByUId { get; set; }
        public string CretedByUName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedByUId { get; set; }
        public string UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
    public class BANKINFO
    {
        public string ID { get; set; }
        public string BankID { get; set; }
        public string LedgerID { get; set; }
        public string BankAcctName { get; set; }
        public string BankAcctNo { get; set; }
        public string IFSCCode { get; set; }
        public string UPIID { get; set; }
        
        public string IsActive { get; set; }
        public string CretedByUId { get; set; }
        public string CretedByUName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedByUId { get; set; }
        public string UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
    public class PAYPERMS
    {
        public string CredID { get; set; }
        public string? CredName { get; set; }
        public string? CredDays { get; set; }
        public string? Remark { get; set; }
        public string? IsActive { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class TDS
    {
        public string? TDSID { get; set; }
        public string? TDSCode { get; set; }
        public string? TDSName { get; set; }
        public string? Rate { get; set; }
        public string? Section { get; set; }
        public string? Assessee { get; set; }
        public string? PurTDSAcct { get; set; }
        public string? SaleTDSAcct { get; set; }
        public string? IsActive { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class FYYEAR
    {
        public string? FYearId { get; set; }
        public string? FYear { get; set; }
        public string? StarDate { get; set; }
        public string? EndDate { get; set; }
        public string? Year { get; set; }
        public string? Status { get; set; }
        public string? PurTDSAcct { get; set; }
        public string? SaleTDSAcct { get; set; }
        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
    public class FREIGHT
    {
        public string? FreID { get; set; }
        public string? Name { get; set; }
        public string? Income { get; set; }
        public string? IncLedgerName { get; set; }
        public string? Expenses { get; set; }
        public string? ExpsLedgerName { get; set; }
        public string? IsActive { get; set; }

        public string? UpdatedByUName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CretedByUId { get; set; }
        public string? UpdatedByUId { get; set; }
        public string? CretedByUName { get; set; }
    }
}
