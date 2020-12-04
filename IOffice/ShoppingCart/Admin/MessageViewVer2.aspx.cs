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

public partial class Account_MessageView : System.Web.UI.Page
{
    MessageCls clsMessage = new MessageCls();
    MasterCls clsMaster = new MasterCls();
    DataTable dt = new DataTable();
    EmployeeCls clsEmployee = new EmployeeCls();
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
        Session["PageUrl"] = strData;
        Session["PageName"] = page;
        Page.Title = pg.getPageTitle(page);

        if (Session["CompanyName"] != null)
        {
            this.Title = Session["CompanyName"] + " IFileCabinet.com View Messages ";
        }

        Session["PageName"] = "MessageView.aspx";

        if (!Page.IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);

            if (Request.QueryString["Status"] != null)
            {
                SetMessageStatus();
                PnlDeletedMsg.Visible = false;
                PnlInboxMsg.Visible = true;
            }
            else
            {
                PnlDeletedMsg.Visible = true;
                PnlInboxMsg.Visible = false;
            }
            GetMessgeDetail();
            FillFileAttachDetail();
        }

    }
    protected void SetMessageStatus()
    {
        Int32 MsgDetailId = Convert.ToInt32(Request.QueryString["MsgDetailId"]);
        Int32 MsgStatusId = Convert.ToInt32(Request.QueryString["Status"]);
        if (MsgStatusId == 1)
        {
            bool Updatestatus = clsMessage.UpdateMsgDetail(MsgDetailId, 2);
        }
    }
    protected void GetMessgeDetail()
    {

        Int32 MsgDetailId = Convert.ToInt32(Request.QueryString["MsgDetailId"]);
        dt = new DataTable();
        dt = clsMessage.SelectMsgforDetail(MsgDetailId);
        if (dt.Rows.Count > 0)
        {
            lblfrom.Text = dt.Rows[0]["Compname"].ToString();
            lblsubject.Text = dt.Rows[0]["MsgSubject"].ToString();
            lblmessage.Text = dt.Rows[0]["MsgDetail"].ToString();
            lbldate.Text = dt.Rows[0]["MsgDate"].ToString();
            lblsignature.Text = dt.Rows[0]["signature"].ToString();
            if (Convert.ToString(dt.Rows[0]["picture"]) == "True")
            {
                SqlDataAdapter da = new SqlDataAdapter("select photo from User_master inner join EmployeeMaster on EmployeeMaster.PartyID=User_master.PartyID where EmployeeMaster.EmployeeMasterID='" + Session["EmployeeId"].ToString() + "'", con);
                DataTable dtpic = new DataTable();
                da.Fill(dtpic);

                string imag = dtpic.Rows[0]["photo"].ToString();
                image1.ImageUrl = "~/ShoppingCart/images/" + imag;
            }
            else
            {
                image1.Visible = false;
            }
        }
    }
    protected void FillFileAttachDetail()
    {
        dt = new DataTable();
        Int32 MsgId;
        MsgId = 0;
        DataTable dtMain = new DataTable();
        Int32 MsgDetailId = Convert.ToInt32(Request.QueryString["MsgDetailId"]);
        dt = clsMessage.SelectMsgIdUsingMsgDetailId(MsgDetailId);
        if (dt.Rows.Count > 0)
        {
            MsgId = Convert.ToInt32(dt.Rows[0]["MsgId"].ToString());
            dt = new DataTable();
            dt = clsMessage.SelectMsgforFileAttach(MsgId);
            if (dt.Rows.Count > 0)
            {
                GrdFileList.DataSource = dt;
                GrdFileList.DataBind();
                //   DataList1.DataSource = dt;
                // DataList1.DataBind();
                setGridisze();
            }
        }
    }
    protected void imgbtnreply_Click(object sender, EventArgs e)
    {
        Int32 MsgDetailId = Convert.ToInt32(Request.QueryString["MsgDetailId"]);
        Response.Redirect("MessageCompose.aspx?MsgDetailIdR=" + MsgDetailId);

    }
    protected void imgbtnfw_Click(object sender, EventArgs e)
    {
        Int32 MsgDetailId = Convert.ToInt32(Request.QueryString["MsgDetailId"]);
        Response.Redirect("MessageCompose.aspx?MsgDetailIdF=" + MsgDetailId);
    }

    protected void ImgBtnDelete_Click(object sender, EventArgs e)
    {
        Int32 MsgDetailId = Convert.ToInt32(Request.QueryString["MsgDetailId"]);
        clsMessage.UpdateMsgDetail(MsgDetailId, 4);
        Response.Redirect("MessageInbox.aspx");
    }
    //protected void ImgBtnMovetoInbox_Click(object sender, ImageClickEventArgs e)
    //{
    //    Int32 MsgDetailId = Convert.ToInt32(Request.QueryString["MsgDetailId"]);
    //  bool   UpdateMsgDetail = clsMessage.UpdateMsgDetail(MsgDetailId, 1);
    //    Response.Redirect("MessageInbox.aspx"); 
    //}
    protected void GrdFileList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label txt = (Label)e.Row.FindControl("TxtNo");
            txt.Text = Convert.ToString(GrdFileList.Rows.Count + 1).ToString();
            HyperLink FileOpenLink = (HyperLink)e.Row.FindControl("FileOpen");
            String FileName = DataBinder.Eval(e.Row.DataItem, "FileName").ToString();
            //FileOpenLink.NavigateUrl= "UploadedDocumentsTemp/" + FileName.ToString() ; 
            FileOpenLink.NavigateUrl = "~/Account/" + Session["Comid"] + "/UploadedDocuments/" + FileName.ToString();
        }
    }
    protected void DataList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    //{
    //    if(e.Item.ItemType == ListItemType.Item)
    //    //if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        Label txt = (Label)e.Item.FindControl("TxtNo1");
    //        if (DataList1.Items.Count > 0)
    //        {
    //           // txt.Text = Convert.ToString(DataList1.Items.Count + 1).ToString();
    //        }
    //        HyperLink FileOpenLink = (HyperLink)e.Item.FindControl("FileOpen1");
    //        String FileName = DataBinder.Eval(e.Item.DataItem, "FileName").ToString();
    //        FileOpenLink.NavigateUrl = "UploadedDocuments/" + FileName.ToString();
    //    }
    //}
    protected void GrdFileList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdFileList.PageIndex = e.NewPageIndex;
        FillFileAttachDetail();
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
