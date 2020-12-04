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


public partial class ShoppingCart_Admin_DocumentMainType : System.Web.UI.Page
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ifilecabinateConnectionString"].ConnectionString);
    SqlConnection con;

    DocumentCls1 clsDocument = new DocumentCls1();
    EmployeeCls clsEmployee = new EmployeeCls();

    int key = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
      
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        MasterCls1 clsMaster = new MasterCls1();
        DataTable dt = new DataTable();
        DocumentCls1 clsDocument = new DocumentCls1();
        EmployeeCls clsEmployee = new EmployeeCls();

        if (!IsPostBack)
        {
           
            lblBusiness0.Text = Session["Cname"].ToString();
            ViewState["sortOrder"] = "";
            lblmsg.Text = "";
            fillstore();
            fillfilterstore();
            fillgrid();
        }
    }
    protected void griddocmaintype_RowEditing(object sender, GridViewEditEventArgs e)
    {

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

    protected void griddocmaintype_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = griddocmaintype.DataKeys[e.RowIndex].Value.ToString();
        Session["ID9"] = id;
        // Execute the delete command



        SqlCommand cmdedit = new SqlCommand("Select * from OfficeDocumentMainType where OfficeDocumentMainType.DocumentMainTypeId='" + id + "' and OfficeDocumentMainType.DocumentMainType='GENERAL'", con);
        SqlDataAdapter dtpedit = new SqlDataAdapter(cmdedit);
        DataTable dtedit = new DataTable();
        dtpedit.Fill(dtedit);
        if (dtedit.Rows.Count > 0)
        {

            lblmsg.Text = "You are unable to delete this cabinet as it is a system generated cabinet and cannot be deleted";
        }
        else
        {
            string str = "Select OfficeDocumentSubType.DocumentMainTypeId,OfficeDocumentMainType.Whid from [OfficeDocumentSubType] inner join OfficeDocumentMainType on OfficeDocumentMainType.DocumentMainTypeId=[OfficeDocumentSubType].DocumentMainTypeId where OfficeDocumentSubType.DocumentMainTypeId='" + id + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "You cannot delete this cabinet as there are drawers within it. Please delete the drawers, then try again.";
                griddocmaintype.EditIndex = -1;
                fillgrid();


            }
            else
            {

                string str1 = "delete  from OfficeDocumentMainType where [OfficeDocumentMainType].DocumentMainTypeId='" + id + "'";
                SqlCommand cmd1 = new SqlCommand(str1, con);
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();
                lblmsg.Text = "Record deleted successfully";

            }


            griddocmaintype.EditIndex = -1;

            // Reload the grid
            fillgrid();
        }
    }

    protected void griddocmaintype_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        griddocmaintype.EditIndex = -1;
        lblmsg.Text = "";
        fillgrid();
    }
    public void setGridisze()
    {
        // doc grid

        if (griddocmaintype.Rows.Count == 0)
        {
            Panel2.CssClass = "GridPanel20";
        }
        else if (griddocmaintype.Rows.Count == 1)
        {
            Panel2.CssClass = "GridPanel125";
        }
        else if (griddocmaintype.Rows.Count == 2)
        {
            Panel2.CssClass = "GridPanel150";
        }
        else if (griddocmaintype.Rows.Count == 3)
        {
            Panel2.CssClass = "GridPanel175";
        }
        else if (griddocmaintype.Rows.Count == 4)
        {
            Panel2.CssClass = "GridPanel200";
        }
        else if (griddocmaintype.Rows.Count == 5)
        {
            Panel2.CssClass = "GridPanel225";
        }
        else if (griddocmaintype.Rows.Count == 6)
        {
            Panel2.CssClass = "GridPanel250";
        }
        else if (griddocmaintype.Rows.Count == 7)
        {
            Panel2.CssClass = "GridPanel275";
        }
        else if (griddocmaintype.Rows.Count == 8)
        {
            Panel2.CssClass = "GridPanel";
        }
        else if (griddocmaintype.Rows.Count == 9)
        {
            Panel2.CssClass = "GridPanel325";
        }
        else if (griddocmaintype.Rows.Count == 10)
        {
            Panel2.CssClass = "GridPanel350";
        }

        else
        {
            Panel2.CssClass = "GridPanel375";
        }

    }
    protected void griddocmaintype_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        griddocmaintype.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void imgbtnsubmit_Click(object sender, EventArgs e)
    {
        string str = "Select * from OfficeDocumentMainType where DocumentMainType='" + txtdocmaintype.Text + "' and Whid='" + ddlbusiness.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exist";
        }
        else
        {
            bool access = UserAccess.Usercon("OfficeDocumentMainType", "", "DocumentMainTypeId", "", "", "OfficeDocumentMainType.CID", "OfficeDocumentMainType");
            if (access == true)
            {
                SqlCommand cmd1 = new SqlCommand("INSERT INTO  OfficeDocumentMainType(DocumentMainType,CID,Whid,description) VALUES('" + txtdocmaintype.Text.ToUpper() + "','" + Session["Comid"].ToString() + "','" + ddlbusiness.SelectedValue + "','" + txtdescription.Text + "')", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd1.ExecuteNonQuery();
                con.Close();

                SqlDataAdapter da1 = new SqlDataAdapter("select max(DocumentMainTypeId) as DocumentMainTypeId from OfficeDocumentMainType where CID='" + Session["Comid"].ToString() + "'", con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);

                Int32 rst = Convert.ToInt32(dt1.Rows[0]["DocumentMainTypeId"]);
                //clsDocument.InsertDocumentMainType(txtdocmaintype.Text, ddlbusiness.SelectedValue);

                if (rst > 0)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Record inserted successfully";
                    fillgrid();
                    txtdocmaintype.Text = "";
                    txtdescription.Text = "";

                    string strmax = " Select Max(DocumentMainTypeId) as Id from OfficeDocumentMainType";
                    SqlCommand cmdmax = new SqlCommand(strmax, con);
                    DataTable dtmax = new DataTable();
                    SqlDataAdapter adpmax = new SqlDataAdapter(cmdmax);
                    adpmax.Fill(dtmax);
                    string id = "0";
                    if (dtmax.Rows.Count > 0)
                    {
                        id = dtmax.Rows[0]["Id"].ToString();

                        if (CheckBox1.Checked == true)
                        {
                            string te1 = "OfficeCabinetPhysicalDrawerAddManage.aspx?Id=" + id;
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te1 + "');", true);
                        }
                    }
                }
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Sorry, You don't permited greter record as per your price plan";
            }

            pnladd.Visible = false;
            Label6.Visible = false;
            btnadd.Visible = true;
            Label6.Text = "Add New Office Document Cabinet";
        }
    }
    public void fillgrid()
    {
        string str132 = "";

        string str2 = "";

        lblBusiness.Text = DropDownList1.SelectedItem.Text;

        if (DropDownList1.SelectedIndex > 0)
        {
            str132 = "  WarehouseMaster.Name, [DocumentMainTypeId],upper(DocumentMainType) as DocumentMainType FROM  [dbo].[OfficeDocumentMainType] inner join WarehouseMaster on WarehouseMaster.WarehouseId=OfficeDocumentMainType.Whid where OfficeDocumentMainType.CID='" + Session["comid"] + "' and OfficeDocumentMainType.Whid='" + DropDownList1.SelectedValue + "'";

            str2 = " select Count(OfficeDocumentMainType.DocumentMainTypeId) as ci FROM  [dbo].[OfficeDocumentMainType] inner join WarehouseMaster on WarehouseMaster.WarehouseId=OfficeDocumentMainType.Whid where OfficeDocumentMainType.CID='" + Session["comid"] + "' and OfficeDocumentMainType.Whid='" + DropDownList1.SelectedValue + "'";
        }
        else
        {
            str132 = "  WarehouseMaster.Name, [DocumentMainTypeId],upper(DocumentMainType) as DocumentMainType FROM  [dbo].[OfficeDocumentMainType] inner join WarehouseMaster on WarehouseMaster.WarehouseId=OfficeDocumentMainType.Whid where OfficeDocumentMainType.CID='" + Session["comid"] + "'";

            str2 = " select Count(OfficeDocumentMainType.DocumentMainTypeId) as ci FROM  [dbo].[OfficeDocumentMainType] inner join WarehouseMaster on WarehouseMaster.WarehouseId=OfficeDocumentMainType.Whid where OfficeDocumentMainType.CID='" + Session["comid"] + "'";
        }

        //SqlCommand cgw = new SqlCommand(str132, con);
        //SqlDataAdapter adgw = new SqlDataAdapter(cgw);
        //DataTable dt = new DataTable();
        //adgw.Fill(dt);       

        griddocmaintype.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " WarehouseMaster.Name, DocumentMainType";

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt = GetDataPage(griddocmaintype.PageIndex, griddocmaintype.PageSize, sortExpression, str132);

            DataView myDataView = new DataView();
            myDataView = dt.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            griddocmaintype.DataSource = dt;
            griddocmaintype.DataBind();
        }
        else
        {
            griddocmaintype.DataSource = null;
            griddocmaintype.DataBind();
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
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void imdcancel_Click(object sender, EventArgs e)
    {
        txtdescription.Text = "";
        ddlbusiness.SelectedIndex = -1;
        txtdocmaintype.Text = "";
        lblmsg.Text = "";
        pnladd.Visible = false;
        Label6.Visible = false;
        btnadd.Visible = true;
        Label6.Text = "Add New Office Document Cabinet";
    }
    protected void griddocmaintype_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        fillgrid();
    }
    protected void griddocmaintype_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            lblmsg.Text = "";
            int dk1 = Convert.ToInt32(e.CommandArgument);
            ViewState["MasterId"] = dk1.ToString();

            SqlCommand cmdedit = new SqlCommand("Select * from OfficeDocumentMainType where OfficeDocumentMainType.DocumentMainTypeId='" + dk1 + "' and OfficeDocumentMainType.DocumentMainType='GENERAL'", con);
            SqlDataAdapter dtpedit = new SqlDataAdapter(cmdedit);
            DataTable dtedit = new DataTable();
            dtpedit.Fill(dtedit);

            if (dtedit.Rows.Count > 0)
            {

                lblmsg.Text = "You are unable to edit this cabinet as it is a system generated cabinet and cannot be edit";

            }
            else
            {
                SqlCommand cmdmaster = new SqlCommand("Select * from OfficeDocumentMainType where OfficeDocumentMainType.DocumentMainTypeId='" + dk1 + "' ", con);
                SqlDataAdapter adpmaster = new SqlDataAdapter(cmdmaster);
                DataTable dtmaster = new DataTable();
                adpmaster.Fill(dtmaster);

                if (dtmaster.Rows.Count > 0)
                {
                    fillstore();
                    ddlbusiness.SelectedIndex = ddlbusiness.Items.IndexOf(ddlbusiness.Items.FindByValue(dtmaster.Rows[0]["Whid"].ToString()));

                    txtdocmaintype.Text = dtmaster.Rows[0]["DocumentMainType"].ToString();
                    txtdescription.Text = dtmaster.Rows[0]["description"].ToString();
                    imgbtnsubmit.Visible = false;
                    btnupdate.Visible = true;


                    pnladd.Visible = true;
                    Label6.Visible = true;
                    btnadd.Visible = false;
                    Label6.Text = "Edit Office Document Cabinet";
                }

            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Button2.Text == "Printable Version")
        {
            Button2.Text = "Hide Printable Version";
            Button1.Visible = true;

            griddocmaintype.AllowPaging = false;
            griddocmaintype.PageSize = 1000;
            fillgrid();

            if (griddocmaintype.Columns[2].Visible == true)
            {
                ViewState["editHide"] = "tt";
                griddocmaintype.Columns[2].Visible = false;
            }
            if (griddocmaintype.Columns[3].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                griddocmaintype.Columns[3].Visible = false;
            }

        }
        else
        {
            Button2.Text = "Printable Version";
            Button1.Visible = false;

            griddocmaintype.AllowPaging = true;
            griddocmaintype.PageSize = 20;
            fillgrid();

            if (ViewState["editHide"] != null)
            {
                griddocmaintype.Columns[2].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                griddocmaintype.Columns[3].Visible = true;
            }

        }
    }

    protected void fillstore()
    {
        ddlbusiness.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlbusiness.DataSource = ds;
        ddlbusiness.DataTextField = "Name";
        ddlbusiness.DataValueField = "WareHouseId";
        ddlbusiness.DataBind();

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlbusiness.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }


    }
    protected void fillfilterstore()
    {
        DropDownList1.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "Name";
        DropDownList1.DataValueField = "WareHouseId";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "All");
        DropDownList1.Items[0].Value = "0";



    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string str = "Select * from OfficeDocumentMainType where DocumentMainType='" + txtdocmaintype.Text + "' and Whid='" + ddlbusiness.SelectedValue + "' and DocumentMainTypeId<>'" + ViewState["MasterId"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exist";
        }
        else
        {
            bool access = UserAccess.Usercon("OfficeDocumentMainType", "", "DocumentMainTypeId", "", "", "OfficeDocumentMainType.CID", "OfficeDocumentMainType");
            if (access == true)
            {

                SqlCommand cmd1 = new SqlCommand("Update OfficeDocumentMainType set DocumentMainType='" + txtdocmaintype.Text.ToUpper() + "',Whid='" + ddlbusiness.SelectedValue + "',description='" + txtdescription.Text + "' where DocumentMainTypeId='" + Convert.ToInt32(ViewState["MasterId"]) + "'", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd1.ExecuteNonQuery();
                con.Close();

                //SqlDataAdapter da1 = new SqlDataAdapter("select max(DocumentMainTypeId) as DocumentMainTypeId from OfficeDocumentMainType where CID='" + Session["Comid"].ToString() + "'", con);
                //DataTable dt1 = new DataTable();
                //da1.Fill(dt1);
                //bool success = clsDocument.UpdateDocumentMainType(Convert.ToInt32(ViewState["MasterId"]), txtdocmaintype.Text, ddlbusiness.SelectedValue);

                //if (Convert.ToString(success) == "True")
                //{
                lblmsg.Visible = true;
                lblmsg.Text = "Record updated successfully";
                //}
                //else
                //{
                //    lblmsg.Visible = true;
                //    lblmsg.Text = "Record is not updated successfully";
                //}

            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Sorry, You don't permited greter record as per your price plan";
            }

            btnupdate.Visible = false;
            imgbtnsubmit.Visible = true;
            pnladd.Visible = false;
            Label6.Visible = false;
            btnadd.Visible = true;
            Label6.Text = "Add New Office Document Cabinet";
            ddlbusiness.SelectedIndex = -1;
            txtdocmaintype.Text = "";
            txtdescription.Text = "";
        }

        griddocmaintype.EditIndex = -1;
        fillgrid();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (pnladd.Visible == false)
        {
            pnladd.Visible = true;
            Label6.Visible = true;
        }
        else
        {
            pnladd.Visible = false;
            Label6.Visible = false;
        }
        btnadd.Visible = false;

        Label6.Text = "Add New Office Document Cabinet";
        lblmsg.Text = "";

    }
}
