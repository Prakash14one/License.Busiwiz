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
using System.Data.SqlClient;

public partial class Account_MessageViewSent : System.Web.UI.Page
{


    MessageCls clsMessage = new MessageCls();
    MasterCls clsMaster = new MasterCls();
    DataTable dt = new DataTable();
    SqlConnection con;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);
        //Session["pnl1"] = "8";
        // Session["pagename"] = "MessageViewSent.aspx";
        if (!Page.IsPostBack)
        {
            GetMessgeDetail();
            FillFileAttachDetail();
        }
    }
    protected void GetToPartyDetail()
    {
        Int32 MsgId = Convert.ToInt32(Request.QueryString["MsgId"]);
        dt = new DataTable();

        dt = clsMessage.SelectMsgforDetailExt(MsgId);
        if (dt.Rows.Count > 0)
        {
            lblFrom.Text = dt.Rows[0]["PartyName"].ToString();
            lblsubject.Text = dt.Rows[0]["MsgSubject"].ToString();
            lblmessage.Text = dt.Rows[0]["MsgDetail"].ToString();
            lbldate.Text = dt.Rows[0]["MsgDate"].ToString();
        }
    }
    protected void FillFileAttachDetail()
    {
        //dt = new DataTable();
        //Int32 MsgId;
        //MsgId = 0;
        //DataTable dtMain = new DataTable();
        //Int32 MsgDetailId = Convert.ToInt32(Request.QueryString["MsgDetailId"]);
        //dt = clsMessage.SelectMsgIdUsingMsgDetailId(MsgDetailId);
        //// if (dt.Rows.Count > 0)
        //// {
        //// MsgId = Convert.ToInt32(Request.QueryString["MsgId"]);
        //if (dt.Rows.Count > 0)
        //{
        Int32 MsgId = Convert.ToInt32(Request.QueryString["MsgId"]);
        dt = new DataTable();
        dt = clsMessage.SelectMsgforFileAttachExt(MsgId);
        if (dt.Rows.Count > 0)
        {
            GrdFileList.DataSource = dt;
            GrdFileList.DataBind();
            lblAttachment.Visible = false;
            setGridisze();
        }
        //  }
        else
        {
            lblAttachment.Visible = true;
        }
        // }
    }
    protected void GetMessgeDetail()
    {
        dt = new DataTable();
        Int32 MsgId = Convert.ToInt32(Request.QueryString["MsgId"]);
        dt = clsMessage.SelectMsgDetailforSentPartyListExt(MsgId);
        if (dt.Rows.Count > 0)
        {
            if (Convert.ToString(dt.Rows[0]["PartyId"]) != "")
            {

                String ToList = "";
                int i = 0;
                foreach (DataRow DR in dt.Rows)
                {

                    if (i >= 1)
                    {
                        ToList = ToList + " , " + DR["compname"].ToString();
                    }
                    if (i == 0)
                    {
                        ToList = DR["compname"].ToString();
                        i = 1;
                    }
                }
                lblto.Text = ToList;
            }

        }
        else
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT " + PageConn.extmsg11 + ".dbo.MsgMasterExt.MsgDate," + PageConn.extmsg11 + ".dbo.MsgMasterExt.FromEmail, " + PageConn.extmsg11 + ".dbo.MsgMasterExt.MsgSubject, " + PageConn.extmsg11 + ".dbo.MsgMasterExt.FromPartyId, " + PageConn.extmsg11 + ".dbo.MsgMasterExt.MsgId, " + PageConn.extmsg11 + ".dbo.MsgMasterExt.MsgStatus, " + PageConn.extmsg11 + ".dbo.MsgDetailExt.MsgDetailId FROM " + PageConn.extmsg11 + ".dbo.MsgDetailExt INNER JOIN " + PageConn.extmsg11 + ".dbo.MsgMasterExt ON " + PageConn.extmsg11 + ".dbo.MsgDetailExt.MsgId = " + PageConn.extmsg11 + ".dbo.MsgMasterExt.MsgId INNER JOIN " + PageConn.extmsg11 + ".dbo.MsgStatusMasterExt ON " + PageConn.extmsg11 + ".dbo.MsgDetailExt.MsgStatusId = " + PageConn.extmsg11 + ".dbo.MsgStatusMasterExt.MsgStatusId WHERE (" + PageConn.extmsg11 + ".dbo.MsgMasterExt.MsgId = '" + MsgId + "') AND (" + PageConn.extmsg11 + ".dbo.MsgMasterExt.MsgStatus <> 'Deleted' OR " + PageConn.extmsg11 + ".dbo.MsgMasterExt.MsgStatus IS NULL) AND (" + PageConn.extmsg11 + ".dbo.MsgDetailExt.MsgStatusId NOT IN (3))", con);
            da.Fill(dt);

            lblto.Text = dt.Rows[0]["FromEmail"].ToString();
        }


        Int32 MsgDetailId = Convert.ToInt32(dt.Rows[0]["MsgDetailId"].ToString());
        dt = new DataTable();
        dt = clsMessage.SelectMsgforDetailExt(MsgDetailId);
        if (dt.Rows.Count > 0)
        {
            lblFrom.Text = dt.Rows[0]["partyname"].ToString();
            lblsubject.Text = dt.Rows[0]["MsgSubject"].ToString();
            lblmessage.Text = dt.Rows[0]["MsgDetail"].ToString();
            lbldate.Text = dt.Rows[0]["MsgDate"].ToString();
        }
    }


    protected void GrdFileList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label txt = (Label)e.Row.FindControl("TxtNo");
            txt.Text = Convert.ToString(GrdFileList.Rows.Count + 1).ToString();
            HyperLink FileOpenLink = (HyperLink)e.Row.FindControl("FileOpen");
            String FileName = DataBinder.Eval(e.Row.DataItem, "FileName").ToString();
            FileOpenLink.NavigateUrl = "~/Account/" + Session["Comid"] + "/UploadedDocuments/" + FileName.ToString();
        }
    }

    protected void imgbtnfw_Click1(object sender, EventArgs e)
    {
        Int32 MsgDetailId = Convert.ToInt32(Request.QueryString["MsgId"]);
        Response.Redirect("MessageComposeext.aspx?MsgDetailIdF111=" + MsgDetailId);
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void Button5_Click(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e)
    {

    }
    public void setGridisze()
    {
        // doc grid
        if (GrdFileList.Rows.Count == 0)
        {
            pnlgrid.CssClass = "GridPanel20";
        }
        else if (GrdFileList.Rows.Count == 1)
        {
            pnlgrid.CssClass = "GridPanel125";
        }
        else if (GrdFileList.Rows.Count == 2)
        {
            pnlgrid.CssClass = "GridPanel150";
        }
        else if (GrdFileList.Rows.Count == 3)
        {
            pnlgrid.CssClass = "GridPanel175";
        }
        else if (GrdFileList.Rows.Count == 4)
        {
            pnlgrid.CssClass = "GridPanel200";
        }
        else if (GrdFileList.Rows.Count == 5)
        {
            pnlgrid.CssClass = "GridPanel225";
        }
        else if (GrdFileList.Rows.Count == 6)
        {
            pnlgrid.CssClass = "GridPanel250";
        }
        else if (GrdFileList.Rows.Count == 7)
        {
            pnlgrid.CssClass = "GridPanel275";
        }
        else if (GrdFileList.Rows.Count == 8)
        {
            pnlgrid.CssClass = "GridPanel";
        }
        else if (GrdFileList.Rows.Count == 9)
        {
            pnlgrid.CssClass = "GridPanel325";
        }
        else if (GrdFileList.Rows.Count == 10)
        {
            pnlgrid.CssClass = "GridPanel350";
        }

        else
        {
            pnlgrid.CssClass = "GridPanel375";
        }


    }
}
