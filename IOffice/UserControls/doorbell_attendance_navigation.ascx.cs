using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;


public partial class UserControls_doorbell_attendance_navigation : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           ChangeStyle();
        }
    }
    public void ChangeStyle()
    {
        string currentPageName = string.Empty;
        string absolutePage = HttpContext.Current.Request.Url.AbsoluteUri;
        currentPageName = Path.GetFileName(absolutePage);
        int qsSymbolIndex = currentPageName.IndexOf("?");
        if (qsSymbolIndex > -1)
        {
            currentPageName = currentPageName.Substring(0, qsSymbolIndex);
        }

        if (currentPageName.ToString() == "Attendance.aspx" || currentPageName.ToString() == "EmpAttendancewithempcode.aspx" || currentPageName.ToString() == "EmpAttendancewithuserId.aspx" || currentPageName.ToString() == "EmployeeAttendanceFrmMaster.aspx")
        {
            liPricePlan.Attributes.Add("class", "current");
        }

        else if (currentPageName.ToString() == "DoorBell.aspx")
        {

            liAboutUs.Attributes.Add("class", "current");
        }


    }
    public static string GetCurrentPageName()
    {
        string currentPageName = string.Empty;
        string absolutePage = HttpContext.Current.Request.Url.AbsoluteUri;
        currentPageName = Path.GetFileName(absolutePage);
        int qsSymbolIndex = currentPageName.IndexOf("?");
        if (qsSymbolIndex > -1)
        {
            currentPageName = currentPageName.Substring(0, qsSymbolIndex);
        }
        return currentPageName;
    }
}