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
public partial class Account_DocumentFolderMaster : System.Web.UI.Page
{
    SqlConnection con;
    DocumentCls1 clsDocument = new DocumentCls1();
    EmployeeCls clsEmployee = new EmployeeCls();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }


        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        if (Session["EmployeeId"] != null)
        {
            pagetitleclass pg = new pagetitleclass();
            string strData = Request.Url.ToString();

            char[] separator = new char[] { '/' };

            string[] strSplitArr = strData.Split(separator);
            int i = Convert.ToInt32(strSplitArr.Length);
            string page = strSplitArr[i - 1].ToString();
            Session["PageUrl"] = strData;
            Session["PageName"] = page;
            Page.Title = pg.getPageTitle(page);


            Session["PageName"] = "DocumentFolderMaster.aspx";

            if (!IsPostBack)
            {
                Pagecontrol.dypcontrol(Page, page);

                ViewState["sortOrder"] = "";
                lblcomname.Text = Session["Cname"].ToString();

                FillDocumentFolderByEmpID();
            }

        }
        else
        {
            Response.Redirect("~/Shoppingcart/Admin/Soppingcartlogin.aspx");
        }

    }
    protected void FillDocumentFolderByEmpID()
    {
        //DataTable dt = new DataTable();
        //dt = clsDocument.SelectDocumentFolderByEmpId(Convert.ToInt32(Session["EmployeeId"]));

        string str = " DocumentFolderMaster.FolderID, DocumentFolderMaster.FolderName, DocumentFolderMaster.EmployeeId, Convert(nvarchar,DocumentFolderMaster.CreatedDate,101) As DocumentAddedDate  FROM DocumentFolderMaster LEFT OUTER JOIN  DocumentRelationshipMaster ON DocumentRelationshipMaster.FolderID = DocumentFolderMaster.FolderID WHERE (DocumentFolderMaster.EmployeeId ='" + Convert.ToInt32(Session["EmployeeId"]) + "' and DocumentFolderMaster.CID='" + Session["Comid"] + "')";
        //order by DocumentFolderMaster.FolderName";

        string str2 = "select count(DocumentFolderMaster.FolderID) as ci FROM DocumentFolderMaster LEFT OUTER JOIN  DocumentRelationshipMaster ON DocumentRelationshipMaster.FolderID = DocumentFolderMaster.FolderID WHERE (DocumentFolderMaster.EmployeeId ='" + Convert.ToInt32(Session["EmployeeId"]) + "' and DocumentFolderMaster.CID='" + Session["Comid"] + "')";

        gridDocumentFolder.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " DocumentFolderMaster.FolderName ";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt = GetDataPage(gridDocumentFolder.PageIndex, gridDocumentFolder.PageSize, sortExpression, str);

            DataView myDataView = new DataView();
            myDataView = dt.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, sortOrder);
            }
            gridDocumentFolder.DataSource = dt;
            gridDocumentFolder.DataBind();
        }
        else
        {
            gridDocumentFolder.DataSource = null;
            gridDocumentFolder.DataBind();
        }
    }

    private int GetRowCount(string str)
    {
        int count = 0;
        DataTable dte = new DataTable();
        dte = select(str);
        if (dte.Rows.Count > 0)
        {
            count += Convert.ToInt32(dte.Rows[0]["ci"]);
        }
        ViewState["count"] = count;
        return count;

    }

    private DataTable GetDataPage(int pageIndex, int pageSize, string sortExpression, string query)
    {
        DataTable dt = select(string.Format("SELECT * FROM (select TOP {0} ROW_NUMBER() OVER (ORDER BY {1}) as ROW_NUM,   " + " {2} ORDER BY ROW_NUM) innerSelect WHERE ROW_NUM > {3}", ((pageIndex + 1) * pageSize), sortExpression, query, (pageIndex * pageSize)));

        dt.Columns.Remove("ROW_NUM");

        return dt;

        ViewState["dt"] = dt;
    }

    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }

    protected void imgbtnsubmit_Click(object sender, EventArgs e)
    {
        bool access = UserAccess.Usercon("DocumentFolderMaster", "", "FolderID", "", "", "CID", "DocumentFolderMaster");
        if (access == true)
        {
            string str = "SELECT * FROM DocumentFolderMaster where EmployeeId='" + Session["EmployeeId"] + "' and FolderName='" + txtFoldername.Text + "'";

            SqlCommand cmd1 = new SqlCommand(str, con);
            cmd1.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count <= 0)
            {

                Int32 rst = clsDocument.InsertDocumentFolder(txtFoldername.Text, Convert.ToInt32(Session["EmployeeId"]));
                if (rst > 0)
                {

                    lblmsg.Text = "Record inserted successfully";
                    FillDocumentFolderByEmpID();
                    txtFoldername.Text = "";
                }
            }
            else
            {

                lblmsg.Text = "Record already exist";
            }
        }
        else
        {
            lblmsg.Text = "Sorry, You are not permitted for greater record to Priceplan";
        }
        pnladd.Visible = false;
        btnadd.Visible = true;
        lbllegend.Visible = false;
        imgbtnsubmit.Visible = true;
        Button2.Visible = false;
    }

    protected void gridDocumentFolder_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void gridDocumentFolder_RowEditing(object sender, GridViewEditEventArgs e)
    {


    }
    protected void gridDocumentFolder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gridDocumentFolder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblmsg.Text = "";
        if (e.CommandName == "Edit")
        {
            int currentRowIndex = Int32.Parse(e.CommandArgument.ToString());
            ViewState["Id"] = currentRowIndex.ToString();

            SqlCommand cmdedit = new SqlCommand("Select * from DocumentFolderMaster where FolderID='" + currentRowIndex + "'", con);
            SqlDataAdapter dtpedit = new SqlDataAdapter(cmdedit);
            DataTable dtedit = new DataTable();
            dtpedit.Fill(dtedit);
            if (dtedit.Rows.Count > 0)
            {
                txtFoldername.Text = dtedit.Rows[0]["FolderName"].ToString();

                pnladd.Visible = true;
                btnadd.Visible = false;
                lbllegend.Visible = true;
                lbllegend.Text = "Edit Folder";
                imgbtnsubmit.Visible = false;
                Button2.Visible = true;
            }


        }
        if (e.CommandName == "Delete")
        {

            Int32 DocumentFolder = Int32.Parse(e.CommandArgument.ToString());
            hdncnfm.Value = DocumentFolder.ToString();

            bool rst = clsDocument.DeleteDocumentFolderMaster(Convert.ToInt32(hdncnfm.Value));
            if (rst == true)
            {

                lblmsg.Text = "Record deleted successfully";
                FillDocumentFolderByEmpID();

            }

        }

    }
    protected void gridDocumentFolder_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridDocumentFolder.EditIndex = -1;

        FillDocumentFolderByEmpID();

    }
    protected void gridDocumentFolder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridDocumentFolder.PageIndex = e.NewPageIndex;
        FillDocumentFolderByEmpID();
    }

    protected void gridDocumentFolder_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        FillDocumentFolderByEmpID();
    }
    public string sortOrder
    {
        get
        {
            if (ViewState["sortOrder"].ToString() == "desc")
            {
                ViewState["sortOrder"] = "asc";
            }
            else
            {
                ViewState["sortOrder"] = "desc";
            }

            return ViewState["sortOrder"].ToString();
        }
        set
        {
            ViewState["sortOrder"] = value;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;

            gridDocumentFolder.AllowPaging = false;
            gridDocumentFolder.PageSize = 1000;
            FillDocumentFolderByEmpID();

            if (gridDocumentFolder.Columns[2].Visible == true)
            {
                ViewState["editHide"] = "tt";
                gridDocumentFolder.Columns[2].Visible = false;
            }
            if (gridDocumentFolder.Columns[3].Visible == true)
            {
                ViewState["deleteHide"] = "tt";
                gridDocumentFolder.Columns[3].Visible = false;
            }
        }
        else
        {
            Button1.Text = "Printable Version";
            Button7.Visible = false;

            gridDocumentFolder.AllowPaging = true;
            gridDocumentFolder.PageSize = 10;
            FillDocumentFolderByEmpID();

            if (ViewState["editHide"] != null)
            {
                gridDocumentFolder.Columns[2].Visible = true;
            }
            if (ViewState["deleteHide"] != null)
            {
                gridDocumentFolder.Columns[3].Visible = true;
            }
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {

        if (pnladd.Visible == false)
        {
            pnladd.Visible = true;
            lbllegend.Visible = true;
        }
        else
        {
            pnladd.Visible = false;
            lbllegend.Visible = false;
        }
        btnadd.Visible = false;

        lbllegend.Text = "Add New Folder";
        lblmsg.Text = "";
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear();
        lblmsg.Text = "";
        btnadd.Visible = true;
        lbllegend.Visible = false;
        pnladd.Visible = false;
        imgbtnsubmit.Visible = false;
        Button2.Visible = true;
    }
    protected void clear()
    {
        txtFoldername.Text = "";

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        bool access = UserAccess.Usercon("DocumentFolderMaster", "", "FolderID", "", "", "CID", "DocumentFolderMaster");
        if (access == true)
        {

            string str = "SELECT *  FROM DocumentFolderMaster where EmployeeId='" + Session["EmployeeId"] + "' AND FolderID<>'" + ViewState["Id"] + "' and FolderName='" + txtFoldername.Text + "'";

            SqlCommand cmd1 = new SqlCommand(str, con);
            cmd1.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                lblmsg.Text = "Record already exist";



            }
            else
            {
                bool success = clsDocument.UpdateDocumentFolder(Convert.ToInt32(ViewState["Id"].ToString()), txtFoldername.Text);
                if (Convert.ToString(success) == "True")
                {

                    lblmsg.Text = "Record updated successfully";
                }
                else
                {

                    lblmsg.Text = "Record not updated successfully";
                }


            }

            gridDocumentFolder.EditIndex = -1;

            FillDocumentFolderByEmpID();
        }
        else
        {
            lblmsg.Text = "Sorry, You are not permitted for greater record to Priceplan";
        }

        pnladd.Visible = false;
        btnadd.Visible = true;
        lbllegend.Visible = false;
        lbllegend.Text = "Add New Folder";
        imgbtnsubmit.Visible = true;
        Button2.Visible = false;
        txtFoldername.Text = "";

    }
}

