namespace TECHNOSYNCERP.Models
{
    public class DevUtility
    {

    }
    public class MenuList { 
        public Menu[] Menus { get; set; }

    }
    public class Menu
    {
        public string Controller { get; set; }
        public string Icon { get; set; }
        public string IsActive { get; set; }
        public string MenuID { get; set; }
        public string MenuName { get; set; }
        public string ParentID { get; set; }
        public int MenuOrderID { get; set; }
        public string Role { get; set; }
        public string Action { get; set; }
    }

    public class BTN
    {
        public string BtnID { get; set; }
        public string BtnName { get; set; }
        public string Icon { get; set; }
        public string ColorClass { get; set; }
        public string Role { get; set; }

        public string idattr { get; set; }
    }
    public class MainGl {
        public GLACCOUNTS[] List { get; set; }
    }
    public class GLACCOUNTS
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string DefLedgerId { get; set; }
        public string DefLedgerName { get; set; }
        public string ObjType { get; set; }
        public string GroupType { get; set; }
    }
    public class BTNAUTHLIST { 
        public BTNAUTH[] authorizations { get; set; }
    }
    public class BTNAUTH
    {
        public string AuthID { get; set; }
        public string MenuID { get; set; }
        public string BtnID { get; set; }
        public string UserID { get; set; }
        public string Auth { get; set; }
        public string Objtype { get; set; }
    }
    public class MainDLC
    {

        public LAYOUTC[] List { get; set; }

    }

    public class LAYOUTC

    {

        public string ID { get; set; }

        public string ObjType { get; set; }

        public string MenuName { get; set; }

        public string LayoutName { get; set; }

        public string Icon { get; set; }
        public string Size { get; set; }

        public string FileName { get; set; }

    }

}
