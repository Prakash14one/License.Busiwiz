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
using System.Data;
using System.Data.SqlClient;

public partial class ShoppingCart_Admin_Master_Default : System.Web.UI.Page
{

    SqlConnection con;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int ik = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[ik - 1].ToString();
        Session["PageUrl"] = strData;
        Session["PageName"] = page;
        Page.Title = pg.getPageTitle(page);


        if (Request.QueryString["ID"] != null)
        {
            int id = Convert.ToInt32(Request.QueryString["ID"]);

            string str = "SELECT distinct  Convert(nvarchar(10),c.Date,101) as Date,p.PartType as PartyTypeName,m.Contactperson as PartyName, e.EmployeeName, " +
                       " c.Phoneno, c.Description, c.CommID,c.CommID as DocumentId, SmallMessageType.Smallmesstype, c.RelatedBusiness " +
                       " FROM  PartytTypeMaster AS p " +
                       " INNER JOIN  Party_master AS m  ON p.PartyTypeId = m.PartyTypeId " +
                       " INNER JOIN CommunicationDetail AS c ON c.CommWith = m.PartyID " +                                                                     
                       " INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeMasterID " +
                       " INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId " +
                       " WHERE c.CommID='" + id + "'";

            SqlDataAdapter da = new SqlDataAdapter(str,con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                lbldate.Text = Convert.ToString(dt.Rows[0]["Date"]);

                lblutype.Text = Convert.ToString(dt.Rows[0]["PartyTypeName"]);

                lbluname.Text = Convert.ToString(dt.Rows[0]["PartyName"]);

                lblcomtype.Text = Convert.ToString(dt.Rows[0]["Smallmesstype"]);

                lblcomname.Text = Convert.ToString(dt.Rows[0]["EmployeeName"]);

                lbldesc.Text = Convert.ToString(dt.Rows[0]["Description"]);
            }
        }
    }
}
