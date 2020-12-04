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
public partial class Account_DocumentRelatedFolders : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(PageConn.connnn);

    DocumentCls1 clsDocument = new DocumentCls1();
    protected int DesignationId;
    EmployeeCls clsEmployee = new EmployeeCls();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }

        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();
        Session["PageUrl"] = strData;
        Session["PageName"] = page;
        Page.Title = pg.getPageTitle(page);
        if (Session["EmployeeId"] != null)
        {


            if (!IsPostBack)
            {

                Pagecontrol.dypcontrol(Page, page);
                DataTable desgdept = new DataTable();
                string str = "SELECT Distinct FolderID,FolderName  FROM DocumentFolderMaster where EmployeeId = '" + Session["EmployeeId"] + "'  order by FolderName";

                SqlCommand cmd1 = new SqlCommand(str, con);
                cmd1.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ddlfolder.DataSource = dt;
                    ddlfolder.DataTextField = "FolderName";
                    ddlfolder.DataValueField = "FolderID";
                    ddlfolder.DataBind();
                    ddlfolder.Items.Insert(0, "All");
                    ddlfolder.Items[0].Value = "0";
                }
                if (Request.QueryString["Sid"] != null)
                {
                    int FolderId = Convert.ToInt32(Request.QueryString["Sid"]);
                    ViewState["FolderId"] = FolderId.ToString();

                    ddlfolder.SelectedIndex = ddlfolder.Items.IndexOf(ddlfolder.Items.FindByValue(ViewState["FolderId"].ToString()));


                    fillDatalistwithfolder(Convert.ToInt32(ddlfolder.SelectedValue));
                }
                else if (Request.QueryString["id"] != null)
                {
                    int DocumentId = Convert.ToInt32(Request.QueryString["id"]);
                    ViewState["docid"] = DocumentId.ToString();
                    fillDatalist(DocumentId);
                }
                else
                {
                    ddlfolder_SelectedIndexChanged(sender, e);
                }
            }
        }
        else
        {
            Response.Redirect("~/Shoppingcart/Admin/Soppingcartlogin.aspx");
        }

    }

    protected void fillDatalist(int DocumentId)
    {
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDocumentFolderByDocumentId(DocumentId);
        DataList1.DataSource = dt;
        DataList1.DataBind();

        foreach (DataListItem ditem in DataList1.Items)
        {
            //DataList1.SelectedIndex = ditem.ItemIndex;
            int folderid = Convert.ToInt32(DataList1.DataKeys[ditem.ItemIndex]);
           // ddlfolder.SelectedValue = folderid.ToString();
            DataTable dt1 = new DataTable();
            dt1 = clsDocument.SelectDoucmentTotalInFolder(folderid);

            LinkButton lnk = (LinkButton)ditem.FindControl("LinkButton1");
            lnk.Text = lnk.Text + "(" + dt1.Rows[0]["total"].ToString() + ")";

        }
    }
    protected void fillDatalistwithfolder(int Folderid)
    {
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDocumentFolderByFolderID(Folderid);
        DataList1.DataSource = dt;
        DataList1.DataBind();

        foreach (DataListItem ditem in DataList1.Items)
        {
            //DataList1.SelectedIndex = ditem.ItemIndex;
            int folderid = Convert.ToInt32(DataList1.DataKeys[ditem.ItemIndex]);
            DataTable dt1 = new DataTable();
            dt1 = clsDocument.SelectDoucmentTotalInFolder(folderid);

            LinkButton lnk = (LinkButton)ditem.FindControl("LinkButton1");
            lnk.Text = lnk.Text + "(" + dt1.Rows[0]["total"].ToString() + ")";

        }
    }

    protected void FillGrid(int FolderID)
    {

        DataTable dt = new DataTable();
        dt = clsDocument.SelectDocumentAccessRigthsByDesignationID();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        int flag = 1;
        foreach (DataRow dr in dt.Rows)
        {

            dt1 = clsDocument.SelectDocumentMasterByDocumentTypeIDFolder(Convert.ToInt32(dr["DocumentTypeId"]), FolderID);
            if (flag == 1)
            {
                dt2 = dt1.Clone();
                flag = 0;
            }
            foreach (DataRow r in dt1.Rows)
            {
                dt2.ImportRow(r);
            }
        }
        DataView dv = dt2.DefaultView;
        if (dv.Table.Rows.Count > 0)
        {
            dv.Sort = "DocumentId desc";
        }

        GridView Gridreqinfo = (GridView)DataList1.Items[DataList1.SelectedIndex].FindControl("Gridreqinfo");

        Gridreqinfo.DataSource = dv;
        Gridreqinfo.DataBind();

        int gridrow = 0;

        int flagcheck = checkaccess(FolderID);

        if (Gridreqinfo.Rows.Count > 0)
        {
            do
            {
                if (flagcheck == 0)
                {
                    Gridreqinfo.Rows[gridrow].Cells[0].Enabled = false;
                    ImageButton imgDel = (ImageButton)Gridreqinfo.Rows[gridrow].Cells[0].FindControl("ImageButton2");
                    imgDel.ImageUrl = "~/Account/images/AD.png";

                }




                gridrow = gridrow + 1;

            } while (gridrow <= Gridreqinfo.Rows.Count - 1);


        }



    }
    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {

        DataList1.SelectedIndex = e.Item.ItemIndex;
        int FolderId = Convert.ToInt32(DataList1.DataKeys[DataList1.SelectedIndex]);
        ViewState["FolderId"] = FolderId.ToString();
        foreach (DataListItem ditem in DataList1.Items)
        {
            GridView Gridreqinfo = (GridView)ditem.FindControl("Gridreqinfo");

            Gridreqinfo.DataSource = null;
            Gridreqinfo.DataBind();


        }
        FillGrid(FolderId);
    }
    protected void Gridreqinfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView Gridreqinfo = (GridView)DataList1.Items[DataList1.SelectedIndex].FindControl("Gridreqinfo");
        Gridreqinfo.PageIndex = e.NewPageIndex;
        FillGrid(Convert.ToInt32(ViewState["FolderId"]));
    }
    protected void Gridreqinfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "delete1")
        {
            int RelationId = Convert.ToInt32(e.CommandArgument);
            bool rst = clsDocument.DeleteDocumentFolderRelation(RelationId);

            pnlmsg.Visible = true;
            lblmsg.Text = "Document successfully deleted from folder.";

            //fillDatalistwithfolder(Convert.ToInt32(ddlfolder.SelectedValue));
            // fillDatalist(Convert.ToInt32(ViewState["docid"]));
            FillGrid(Convert.ToInt32(ViewState["FolderId"]));
        }
    }

    protected int checkaccess(int FolderId)
    {
        DataTable dt = new DataTable();
        dt = clsDocument.SelectDocumentFolderByFolderID(FolderId);
        int EmployeeId = Convert.ToInt32(Session["EmployeeId"]);
        int CreatedEmployeeId = Convert.ToInt32(dt.Rows[0]["EmployeeId"]);
        if (EmployeeId == CreatedEmployeeId)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    protected void ddlfolder_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlfolder.SelectedIndex > -1)
        {
            fillDatalistwithfolder(Convert.ToInt32(ddlfolder.SelectedValue));
        }
    }
}
