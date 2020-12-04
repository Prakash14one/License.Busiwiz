using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Account_ViewDocForPrint : System.Web.UI.Page
{
    public String MainPath = "";
    DocumentCls1 clsDocument = new DocumentCls1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmployeeId"] != null)
        {
            pagetitleclass pg = new pagetitleclass();
            string strData = Request.Url.ToString();

            char[] separator = new char[] { '/' };

            string[] strSplitArr = strData.Split(separator);
            int i = Convert.ToInt32(strSplitArr.Length);
            string page = strSplitArr[i - 1].ToString();
				Session["PageUrl"]=strData;
            Session["PageName"] = page;
            Page.Title = pg.getPageTitle(page);

            if (Session["CompanyName"] != null)
            {
                this.Title = Session["CompanyName"] + " IFileCabinet.com Company Wizard - Document Print View ";
            }

            Session["PageName"] = "ViewDocForPrint.aspx";

            if (!IsPostBack)
            {
                int Docid = Convert.ToInt32(Request.QueryString["id"]);
                //int DesignationId = Convert.ToInt32(Request.QueryString["Did"]);
                LoadPdf(Docid);
                //SetAccessRights(Docid, DesignationId);
                //imgbtnPrint.Attributes.Add("onclick", "window.print();return false;"); 
            }
        }
        else
        {
            Response.Redirect("~/Shoppingcart/admin/ShoppingcartLogin.aspx");
        }

    }
    protected void LoadPdf(int Docid)
    {
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDoucmentMasterByID(Docid);
        if (dt.Rows.Count > 0)
        {
            string docname = dt.Rows[0]["DocumentName"].ToString();
            //lblTitle.Text = dt.Rows[0]["DocumentTitle"].ToString();
            string filepath = Server.MapPath("~//Account//" + Session["Comid"] + "//UploadedDocuments//" + docname);
            MainPath = Session["Comid"] + "/UploadedDocuments/";
            /// MainPath = "..//..//Account//" + Session["Comid"] + "//UploadedDocuments//" + docname;
            grid.DataSource = dt;
            grid.DataBind();
            ViewState["path"] = filepath.ToString();
        }
        
    }
    protected void grid_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
