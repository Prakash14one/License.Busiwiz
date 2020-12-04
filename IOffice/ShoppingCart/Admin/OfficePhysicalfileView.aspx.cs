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
using System.Data.SqlClient;

public partial class ShoppingCart_Admin_DocumentSubSubType : System.Web.UI.Page
{

    SqlConnection con;
    DocumentCls1 clsDocument = new DocumentCls1();
    EmployeeCls clsEmployee = new EmployeeCls();
    int key = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;       
        if (!IsPostBack)
        {
            lblBusiness0.Text = Session["Cname"].ToString();
            ViewState["sortOrder"] = "";
            lblmsg.Text = "";           
            fillfilterbusiness();
            fillpartyfilter();
            filterbycabinet();
            fillfilterddlsubtype();
            FillDocumentType();
        }
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
    protected void FillDocumentType()
    {
        lblBusiness.Text = DropDownList1.SelectedItem.Text;
        lblCabinet.Text = DropDownList2.SelectedItem.Text;
        lblDrawer.Text = DropDownList3.SelectedItem.Text;
        lblpartyy.Text = ddlpartyfilter.SelectedItem.Text;
        string str145 = "";
        string st1 = "";
        string st2 = "";
        string st3 = "";
        string st4 = "";
        string st5 = "";
        string st6 = "";

        if (DropDownList1.SelectedIndex > 0)
        {
            st1 = " and OfficeDocumentMainType.Whid='" + DropDownList1.SelectedValue + "'";
        }
        if (ddlpartyfilter.SelectedIndex > 0)
        {
            st4 = " and OfficeDocumentType.PartyID='" + ddlpartyfilter.SelectedValue + "'";
        }
        if (DropDownList2.SelectedIndex > 0)
        {
            st2 = " and OfficeDocumentMainType.DocumentMainTypeId='" + DropDownList2.SelectedValue + "'";
        }
        if (DropDownList3.SelectedIndex > 0)
        {
            st3 = " and OfficeDocumentSubType.DocumentSubTypeId ='" + DropDownList3.SelectedValue + "'";
        }
        if (DropDownList4.SelectedValue != "6")
        {
            st5 = " and OfficeDocumentType.FileType ='" + DropDownList4.SelectedValue + "'";
        }
        if (TextBox1.Text != "")
        {
            st6 += " and ((OfficeDocumentMainType.DocumentMainType like '%" + TextBox1.Text.Replace("'", "''") + "%') or (OfficeDocumentSubType.DocumentSubType like '%" + TextBox1.Text.Replace("'", "''") + "%') or (WareHouseMaster.Name like '%" + TextBox1.Text.Replace("'", "''") + "%') or (OfficeDocumentType.DocumentType like '%" + TextBox1.Text.Replace("'", "''") + "%') or (OfficeDocumentType.Description like '%" + TextBox1.Text.Replace("'", "''") + "%'))";
        }

        str145 = "  WareHouseMaster.Name,WareHouseMaster.WareHouseId,OfficeDocumentType.Date,case when (OfficeDocumentType.Status='1') then 'Active' else 'Inactive' end as Status ,OfficeDocumentMainType.DocumentMainTypeId, OfficeDocumentMainType.DocumentMainType, OfficeDocumentType.DocumentTypeId, OfficeDocumentType.DocumentType, OfficeDocumentSubType.DocumentSubTypeId, OfficeDocumentSubType.DocumentSubType FROM OfficeDocumentType inner join OfficeDocumentSubType ON OfficeDocumentType.DocumentSubTypeId = OfficeDocumentSubType.DocumentSubTypeId   inner join OfficeDocumentMainType  on OfficeDocumentMainType.DocumentMainTypeId=OfficeDocumentSubType.DocumentMainTypeId inner join WareHouseMaster on WareHouseMaster.WareHouseId=OfficeDocumentMainType.Whid where OfficeDocumentType.CID='" + Session["Comid"] + "' " + st1 + " " + st4 + " " + st2 + " " + st3 + " " + st5 + " " + st6 + " ";

        string strmak = " select Count(OfficeDocumentType.DocumentTypeId) as ci FROM OfficeDocumentType inner join OfficeDocumentSubType ON OfficeDocumentType.DocumentSubTypeId = OfficeDocumentSubType.DocumentSubTypeId   inner join OfficeDocumentMainType  on OfficeDocumentMainType.DocumentMainTypeId=OfficeDocumentSubType.DocumentMainTypeId inner join WareHouseMaster on WareHouseMaster.WareHouseId=OfficeDocumentMainType.Whid where OfficeDocumentType.CID='" + Session["Comid"] + "' " + st1 + " " + st4 + " " + st2 + " " + st3 + " " + st5 + " " + st6 + " ";


        gridocsubsubtype.VirtualItemCount = GetRowCount(strmak);

        string sortExpression = " WareHouseMaster.Name,OfficeDocumentMainType.DocumentMainType,OfficeDocumentSubType.DocumentSubType,OfficeDocumentType.DocumentType ";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt = GetDataPage(gridocsubsubtype.PageIndex, gridocsubsubtype.PageSize, sortExpression, str145);

            gridocsubsubtype.DataSource = dt;

            DataView myDataView = new DataView();
            myDataView = dt.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            gridocsubsubtype.DataBind();
        }

        else
        {
            gridocsubsubtype.DataSource = null;
            gridocsubsubtype.DataBind();
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

    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;
    }

  
    protected void gridocsubsubtype_RowEditing(object sender, GridViewEditEventArgs e)
    {


    }
    protected void gridocsubsubtype_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            lblmsg.Text = "";
            int dk1 = Convert.ToInt32(e.CommandArgument);
            ViewState["MasterId"] = dk1.ToString();

            SqlCommand cmdedit = new SqlCommand("Select * from OfficeDocumentType  where OfficeDocumentType.DocumentTypeId='" + dk1 + "' and OfficeDocumentType.DocumentType='GENERAL'", con);
            SqlDataAdapter dtpedit = new SqlDataAdapter(cmdedit);
            DataTable dtedit = new DataTable();
            dtpedit.Fill(dtedit);

            if (dtedit.Rows.Count > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "You are unable to edit this folder as it is a system generated folder and cannot be edited";
                key = 1;
            }
            else
            {
                SqlCommand cmdmaster = new SqlCommand("select OfficeDocumentMainType.Whid,OfficeDocumentMainType.DocumentMainTypeId,OfficeDocumentType.* from OfficeDocumentType inner join OfficeDocumentSubType on OfficeDocumentSubType.DocumentSubTypeId=OfficeDocumentType.DocumentSubTypeId inner join OfficeDocumentMainType on OfficeDocumentMainType.DocumentMainTypeId=OfficeDocumentSubType.DocumentMainTypeId where OfficeDocumentType.DocumentTypeId='" + dk1 + "' ", con);
                SqlDataAdapter adpmaster = new SqlDataAdapter(cmdmaster);
                DataTable dtmaster = new DataTable();
                adpmaster.Fill(dtmaster);
                if (dtmaster.Rows.Count > 0)
                {                  
                   
                }
            }

        }
    }
    protected void gridocsubsubtype_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {      
 
    }
    protected void gridocsubsubtype_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridocsubsubtype.EditIndex = -1;
        FillDocumentType();
    }

   

    protected void fillpartyfilter()
    {
        ddlpartyfilter.Items.Clear();

        string str1 = "select party_master.partyId,Compname from party_master where Compname !='' And Whid = '" + DropDownList1.SelectedValue + "' order by Compname asc";

        SqlCommand cmdparty = new SqlCommand(str1, con);
        SqlDataAdapter adpparty = new SqlDataAdapter(cmdparty);
        DataTable dtparty = new DataTable();
        adpparty.Fill(dtparty);
        if (dtparty.Rows.Count > 0)
        {
            ddlpartyfilter.DataSource = dtparty;
            ddlpartyfilter.DataTextField = "Compname";
            ddlpartyfilter.DataValueField = "partyId";
            ddlpartyfilter.DataBind();
        }
        ddlpartyfilter.Items.Insert(0, "All");
        ddlpartyfilter.Items[0].Value = "0";
    }

    protected void ddldocsubtypename_SelectedIndexChanged1(object sender, EventArgs e)
    {
        gridocsubsubtype.EditIndex = -1;
        FillDocumentType();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillpartyfilter();
        filterbycabinet();
        fillfilterddlsubtype();
        FillDocumentType();
    }

    protected void gridocsubsubtype_Sorting(object sender, GridViewSortEventArgs e)
    {
      
    }
    protected void gridocsubsubtype_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridocsubsubtype.PageIndex = e.NewPageIndex;
        FillDocumentType();
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillfilterddlsubtype();
        FillDocumentType();
    }

    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDocumentType();
    }
    protected void imgAdd_Click(object sender, ImageClickEventArgs e)
    {
        string te = "OfficeDocumentPhysicalCabinetAddmanage.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
    {
      
    }
    protected void imgAdd2_Click(object sender, ImageClickEventArgs e)
    {
        string te = "OfficeCabinetPhysicalDrawerAddManage.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void imgRefresh2_Click(object sender, ImageClickEventArgs e)
    {
      
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Button2.Text == "Printable Version")
        {
            Button2.Text = "Hide Printable Version";
            Button1.Visible = true;

            gridocsubsubtype.AllowPaging = false;
            gridocsubsubtype.PageSize = 1000;
            FillDocumentType();

            if (gridocsubsubtype.Columns[6].Visible == true)
            {
                ViewState["editHide"] = "tt";
                gridocsubsubtype.Columns[6].Visible = false;
            }
            if (gridocsubsubtype.Columns[7].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                gridocsubsubtype.Columns[7].Visible = false;
            }
        }
        else
        {
            Button2.Text = "Printable Version";
            Button1.Visible = false;

            gridocsubsubtype.AllowPaging = true;
            gridocsubsubtype.PageSize = 10;
            FillDocumentType();

            if (ViewState["editHide"] != null)
            {
                gridocsubsubtype.Columns[6].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                gridocsubsubtype.Columns[7].Visible = true;
            }
        }
    }

  
  
   
    protected void fillfilterbusiness()
    {
        DropDownList1.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "Name";
        DropDownList1.DataValueField = "WareHouseId";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "All");
        DropDownList1.Items[0].Value = "0";

        string eeed = " Select distinct EmployeeMaster.Whid from  EmployeeMaster where EmployeeMasterId='" + Session["EmployeeId"] + "'";
        SqlCommand cmdeeed = new SqlCommand(eeed, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);

        if (dteeed.Rows.Count > 0)
        {
            DropDownList1.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }

    }
    protected void filterbycabinet()
    {
        DropDownList2.Items.Clear();

        if (DropDownList1.SelectedIndex >= 0)
        {
            string str132 = " SELECT [DocumentMainTypeId], DocumentMainType as DocumentMainType FROM  [dbo].[OfficeDocumentMainType] inner join WarehouseMaster on WarehouseMaster.WarehouseId=OfficeDocumentMainType.Whid where CID='" + Session["Comid"] + "' and OfficeDocumentMainType.Whid='" + DropDownList1.SelectedValue + "' ";
            SqlCommand cgw = new SqlCommand(str132, con);
            SqlDataAdapter adgw = new SqlDataAdapter(cgw);
            DataTable dt = new DataTable();
            adgw.Fill(dt);

            DropDownList2.DataSource = dt;
            DropDownList2.DataTextField = "DocumentMainType";
            DropDownList2.DataValueField = "DocumentMainTypeId";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, "All");
            DropDownList2.Items[0].Value = "0";
        }
        else
        {
            DropDownList2.DataSource = null;
            DropDownList2.DataTextField = "DocumentMainType";
            DropDownList2.DataValueField = "DocumentMainTypeId";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, "All");
            DropDownList2.Items[0].Value = "0";
        }
    }
    protected void fillfilterddlsubtype()
    {
        DropDownList3.Items.Clear();

        if (DropDownList2.SelectedIndex >= 0)
        {
            string str178 = " SELECT OfficeDocumentSubType.DocumentSubTypeId, OfficeDocumentSubType.DocumentSubType, OfficeDocumentMainType.DocumentMainTypeId as DocumentMainTypeId, OfficeDocumentMainType.DocumentMainType FROM OfficeDocumentMainType RIGHT OUTER JOIN OfficeDocumentSubType ON OfficeDocumentMainType.DocumentMainTypeId = OfficeDocumentSubType.DocumentMainTypeId WHERE (OfficeDocumentMainType.DocumentMainTypeId = '" + DropDownList2.SelectedValue + "') and OfficeDocumentMainType.CID='" + Session["Comid"] + "' ";
            SqlCommand cgw = new SqlCommand(str178, con);
            SqlDataAdapter adgw = new SqlDataAdapter(cgw);
            DataTable dt = new DataTable();
            adgw.Fill(dt);

            DropDownList3.DataSource = dt;
            DropDownList3.DataTextField = "DocumentSubType";
            DropDownList3.DataValueField = "DocumentSubTypeId";
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, "All");
            DropDownList3.Items[0].Value = "0";
        }
        else
        {
            DropDownList3.DataSource = null;
            DropDownList3.DataTextField = "DocumentSubType";
            DropDownList3.DataValueField = "DocumentSubTypeId";
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, "All");
            DropDownList3.Items[0].Value = "0";
        }
    }
   
    protected void imgbtnsubmit0_Click(object sender, EventArgs e)
    {
       
    }
  
    protected void ddlpartyfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDocumentType();
    }
    protected void ddlfiletype_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDocumentType();
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        FillDocumentType();
    }
}
