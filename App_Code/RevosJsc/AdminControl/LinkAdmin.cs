
namespace RevosJsc.AdminControl
{
    public class LinkAdmin
    {
        public static string GoAdminMenu(string control, string action, string app, string imnIid)
        {
            return "/admin?control=" + control + "&action=" + action + "&app=" + app + "&imnid=" + imnIid;
        }
        /// <summary>
        /// Link tới một control nào đó trong admin (vd: tới control tin tức)
        /// </summary>
        /// <param name="control">Mã của control (TypeControl)</param>
        /// <returns></returns>
        public static string GoAdminControl(string control)
        {
            return "/admin?control=" + control;
        }

        /// <summary>
        /// Link tới một subcontrol trong trang admin (vd: tạo danh danh mục tin tức)
        /// </summary>
        /// <param name="control">Mã của control (vd: TypeControl.New)</param>
        /// <param name="action">Mã của sub control (vd: TypePage.CreateCategory)</param>
        /// <returns></returns>
        public static string GoAdminSubControl(string control, string action)
        {
            return "/admin?control=" + control + "&action=" + action;
        }

        /// <summary>
        /// Link tới một subcontrol trong trang admin (vd: tạo danh danh mục tin tức)
        /// </summary>
        /// <param name="control">Mã của control (vd: TypeControl.New)</param>
        /// <param name="action">Mã của sub control (vd: TypePage.CreateCategory)</param>
        /// <param name="parent">Igid của danh mục cha</param>
        /// <returns></returns>
        public static string GoAdminSubControl(string control, string action, string parent)
        {
            return "/admin?control=" + control + "&action=" + action + "&parent=" + parent;
        }

        /// <summary>
        /// Link tới một subcontrol trong trang admin (vd: cập nhật tin tức)
        /// </summary>
        /// <param name="control">Mã của control (vd: TypeControl.New)</param>
        /// <param name="action">Mã của sub control (vd: TypePage.UpdateItem)</param>
        /// <param name="ni"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string GoAdminSubControl(string control, string action, string ni, string p)
        {
            return "/admin?control=" + control + "&action=" + action + "&ni=" + ni + "&p=" + p;
        }

        public static string GoAdminOption(string control, string action, string param, string value)
        {
            return "/admin?control=" + control + "&action=" + action + "&"+ param + "=" + value;
        }
        public static string GoAdminItem(string control, string action, string iid)
        {
            return "/admin?control=" + control + "&action=" + action + "&iid=" + iid;
        }
        public static string GoAdminItem(string control, string action, string igid, string iid)
        {
            return "/admin?control=" + control + "&action=" + action + "&igid=" + igid + "&iid=" + iid;
        }

        public static string GoAdminItem(string control, string action, string iid, string ni, string p)
        {
            return "/admin?control=" + control + "&action=" + action + "&iid=" + iid + "&ni=" + ni + "&p=" + p;
        }

        public static string GoAdminItem(string control, string action, string iid, string ni, string p, string key)
        {
            return "/admin?control=" + control + "&action=" + action + "&iid=" + iid + "&ni=" + ni + "&p=" + p + "&key=" + key;
        }

        public static string GoAdminCategory(string control, string action, string igid)
        {
            return "/admin?control=" + control + "&action=" + action + "&igid=" + igid;
        }

        public static string GoAdminCategory(string control, string action, string igid, string parent)
        {
            return "/admin?control=" + control + "&action=" + action + "&igid=" + igid + "&parent=" + parent;
        }

        public static string GoAdminCategory(string control, string action, string igid, string ni, string p)
        {
            return "/admin?control=" + control + "&action=" + action + "&igid=" + igid + "&ni=" + ni + "&p=" + p;
        }

        public static string GoAdminCategory(string control, string action, string igid, string ni, string p, string key)
        {
            return "/admin?control=" + control + "&action=" + action + "&igid=" + igid + "&ni=" + ni + "&p=" + p + "&key=" + key;
        }

        /// <summary>
        /// Tạo url cho trang admin
        /// </summary>
        /// <param name="control">QueryString control</param>
        /// <param name="action">QueryString action</param>
        /// <param name="igid">QueryString igid</param>
        /// <param name="key">QueryString key</param>
        /// <param name="numberShowItem">QueryString NumberShowItem</param>
        /// <returns></returns>
        public static string UrlAdmin(string control, string action, string igid, string key, string numberShowItem)
        {
            var s = "";
            if (action.Equals("") && igid.Equals("") && key.Equals(""))
            {
                s = "/admin?control=" + control + "&NumberShowItem=" + numberShowItem;
            }
            else if (!action.Equals("") && igid.Equals("") && key.Equals(""))
            {
                s = "/admin?control=" + control + "&action=" + action + "&NumberShowItem=" + numberShowItem;
            }
            else if (!action.Equals("") && !igid.Equals("") && key.Equals(""))
            {
                s = "/admin?control=" + control + "&action=" + action + "&igid=" + igid + "&NumberShowItem=" + numberShowItem;
            }
            else if (!action.Equals("") && !igid.Equals("") && !key.Equals(""))
            {
                s = "/admin?control=" + control + "&action=" + action + "&igid=" + igid + "&key=" + key + "&NumberShowItem=" + numberShowItem;
            }
            else if (action.Equals("") && !igid.Equals("") && key.Equals(""))
            {
                s = "/admin?control=" + control + "&igid=" + igid + "&NumberShowItem=" + numberShowItem;
            }
            else if (action.Equals("") && !igid.Equals("") && !key.Equals(""))
            {
                s = "/admin?control=" + control + "&igid=" + igid + "&key=" + key + "&NumberShowItem=" + numberShowItem;
            }
            else if (action.Equals("") && igid.Equals("") && !key.Equals(""))
            {
                s = "/admin?control=" + control + "&key=" + key + "&NumberShowItem=" + numberShowItem;
            }
            else if (!action.Equals("") && igid.Equals("") && !key.Equals(""))
            {
                s = "/admin?control=" + control + "&action=" + action + "&key=" + key + "&NumberShowItem=" + numberShowItem;
            }
            else
            {
                s = "";
            }
            return s;
        }

        public static string UrlAdminUser(string username, string phone, string email, string numberShowItem)
        {
            return "/admin?control=Users&username=" + username + "&phone=" + phone + "&email=" + email + "&NumberShowItem=" + numberShowItem;
        }

        public static string UrlAdminRedirect(string link, string des, string numberShowItem)
        {
            return "/admin?control=Redirects&link=" + link + "&des=" + des + "&NumberShowItem=" + numberShowItem;
        }
        public static string UrlAdminRedirect(string action, string link, string des, string numberShowItem)
        {
            return "/admin?control=Redirects&action=" + action + "&link=" + link + "&des=" + des + "&NumberShowItem=" + numberShowItem;
        }
    }
}