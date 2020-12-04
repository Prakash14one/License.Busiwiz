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

public partial class ShoppingCart_Admin_ShippersShipOptionMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(PageConn.connnn);
    protected void Page_Load(object sender, EventArgs e)
    {
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page); 

        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            fillstore();
            fillfilterstore();
            fillddldepartment();
            fillgrid();
            statuslable.Visible = false;
            lblCompany.Text = Session["Cname"].ToString();
           
            lblBusiness.Text = "All";
          
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
        ddlfilterbusiness.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlfilterbusiness.DataSource = ds;
        ddlfilterbusiness.DataTextField = "Name";
        ddlfilterbusiness.DataValueField = "WareHouseId";
        ddlfilterbusiness.DataBind();
        ddlfilterbusiness.Items.Insert(0, "All");
        ddlfilterbusiness.Items[0].Value = "0";



       

    }
    public void fillddldepartment()
    {
        ddldesignation.Items.Clear();

        string strfillgrid = "SELECT ShippersName,ShippersId FROM ShippersMaster  where compid='"+Session["comid"]+"' and Whid='"+ddlbusiness.SelectedValue+"' order by ShippersName";
        SqlCommand cmdfillgrid = new SqlCommand(strfillgrid, con);
        SqlDataAdapter adpfillgrid = new SqlDataAdapter(cmdfillgrid);
        DataTable dtfill = new DataTable();
        adpfillgrid.Fill(dtfill);

        ddldesignation.DataSource = dtfill;
        ddldesignation.DataValueField = "ShippersId";
        ddldesignation.DataTextField = "ShippersName";
        ddldesignation.DataBind();
     
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        //if (ddldesignation.SelectedIndex > 0)
        //{
            string str1 = "SELECT OptionName,ShippersShipOptionMasterId FROM ShippersShipOptionMaster where OptionName='" + txtdegnation.Text + "' and [ShippersId]= '" + ddldesignation.SelectedValue + "' and OptionName2= '" + txt2.Text + "' and OptionName3='" + txt3.Text + "' and OptionName4='" + TextBox1.Text + "' and OptionName5='" + TextBox2.Text + "' and whid='"+ddlbusiness.SelectedValue+"'    order by OptionName";
            SqlCommand cmd1 = new SqlCommand(str1, con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                statuslable.Visible = true;
                statuslable.Text = "Record already exist";
            }
            else
            {
                string str = "insert into  ShippersShipOptionMaster  values ('" + ddldesignation.SelectedValue + "','" + txtdegnation.Text + "','" + txt2.Text + "','" + txt3.Text + "','" + TextBox1.Text + "','" + TextBox2.Text + "','"+ddlbusiness.SelectedValue+"')";
                SqlCommand cmd = new SqlCommand(str, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
                fillgrid();
                statuslable.Visible = true;
                statuslable.Text = "Record inserted successfully";
                txtdegnation.Text = "";
                ddldesignation.SelectedIndex = 0;
                addinventoryroom.Visible = false;
                btnaddroom.Visible = true;
                clear();
                title.Visible = false;

            }
       // }
        //else
        //{
        //    statuslable.Visible = true;
        //    statuslable.Text = "Please select Shipping Company name";
        //}
    }
    public void fillgrid()
    {
        lblBusiness.Text = "All";
        string str1 = "SELECT     ShippersShipOptionMaster.ShippersId, ShippersShipOptionMaster.ShippersShipOptionMasterId,ShippersShipOptionMaster.OptionName,ShippersShipOptionMaster.OptionName2,ShippersShipOptionMaster.OptionName3,ShippersShipOptionMaster.OptionName4,ShippersShipOptionMaster.OptionName5 ,ShippersShipOptionMaster.OptionName +':'+ ShippersShipOptionMaster.OptionName2 + ':' + ShippersShipOptionMaster.OptionName3 + ':' + ShippersShipOptionMaster.OptionName4 + ':' + ShippersShipOptionMaster.OptionName5 as Options,ShippersShipOptionMaster.whid,WarehouseMaster.Name, ShippersMaster.ShippersName" +
                     " FROM         ShippersMaster INNER JOIN" +
                     " ShippersShipOptionMaster ON ShippersMaster.ShippersId = ShippersShipOptionMaster.ShippersId inner join WarehouseMaster on ShippersShipOptionMaster.whid=WarehouseMaster.WarehouseId where ShippersMaster.compid='" + Session["comid"] + "' order by  WarehouseMaster.Name,ShippersShipOptionMaster.OptionName,ShippersShipOptionMaster.OptionName2,ShippersShipOptionMaster.OptionName3,ShippersShipOptionMaster.OptionName4,ShippersShipOptionMaster.OptionName5";
       
        if (ddlfilterbusiness.SelectedIndex > 0 && txtsearch.Text.Length>0)
        {
            lblBusiness.Text = ddlfilterbusiness.SelectedItem.Text;
            str1 = "SELECT     ShippersShipOptionMaster.ShippersId, ShippersShipOptionMaster.ShippersShipOptionMasterId, ShippersShipOptionMaster.OptionName,ShippersShipOptionMaster.OptionName2,ShippersShipOptionMaster.OptionName3,ShippersShipOptionMaster.OptionName4,ShippersShipOptionMaster.OptionName5 ,ShippersShipOptionMaster.OptionName +':'+ ShippersShipOptionMaster.OptionName2 + ':' + ShippersShipOptionMaster.OptionName3 + ':' + ShippersShipOptionMaster.OptionName4 + ':' + ShippersShipOptionMaster.OptionName5 as Options,ShippersShipOptionMaster.whid,WarehouseMaster.Name, ShippersMaster.ShippersName" +
                    " FROM         ShippersMaster INNER JOIN" +
                    " ShippersShipOptionMaster ON ShippersMaster.ShippersId = ShippersShipOptionMaster.ShippersId inner join WarehouseMaster on ShippersShipOptionMaster.whid=WarehouseMaster.WarehouseId where ShippersMaster.compid='" + Session["comid"] + "' and ShippersShipOptionMaster.whid='"+ddlfilterbusiness.SelectedValue+"' "+
                    " and (ShippersShipOptionMaster.OptionName like '%" + txtsearch.Text + "%' or ShippersShipOptionMaster.OptionName2 like '%" + txtsearch.Text + "%' or ShippersShipOptionMaster.OptionName3 like '%" + txtsearch.Text + "%' or ShippersShipOptionMaster.OptionName4 like '%" + txtsearch.Text + "%' or ShippersShipOptionMaster.OptionName5 like '%" + txtsearch.Text + "%' or ShippersMaster.ShippersName like '%" + txtsearch.Text + "%') order by  WarehouseMaster.Name,ShippersShipOptionMaster.OptionName,ShippersShipOptionMaster.OptionName2,ShippersShipOptionMaster.OptionName3,ShippersShipOptionMaster.OptionName4,ShippersShipOptionMaster.OptionName5";

        }
        if (ddlfilterbusiness.SelectedIndex > 0 && txtsearch.Text.Length <= 0)
        {
            lblBusiness.Text = ddlfilterbusiness.SelectedItem.Text;
            str1 = "SELECT     ShippersShipOptionMaster.ShippersId, ShippersShipOptionMaster.ShippersShipOptionMasterId,ShippersShipOptionMaster.OptionName,ShippersShipOptionMaster.OptionName2,ShippersShipOptionMaster.OptionName3,ShippersShipOptionMaster.OptionName4,ShippersShipOptionMaster.OptionName5 , ShippersShipOptionMaster.OptionName +':'+ ShippersShipOptionMaster.OptionName2 + ':' + ShippersShipOptionMaster.OptionName3 + ':' + ShippersShipOptionMaster.OptionName4 + ':' + ShippersShipOptionMaster.OptionName5 as Options,ShippersShipOptionMaster.whid,WarehouseMaster.Name, ShippersMaster.ShippersName" +
                     " FROM         ShippersMaster INNER JOIN" +
                     " ShippersShipOptionMaster ON ShippersMaster.ShippersId = ShippersShipOptionMaster.ShippersId inner join WarehouseMaster on ShippersShipOptionMaster.whid=WarehouseMaster.WarehouseId where ShippersMaster.compid='" + Session["comid"] + "' and ShippersShipOptionMaster.whid='" + ddlfilterbusiness.SelectedValue + "' order by  WarehouseMaster.Name,ShippersShipOptionMaster.OptionName,ShippersShipOptionMaster.OptionName2,ShippersShipOptionMaster.OptionName3,ShippersShipOptionMaster.OptionName4,ShippersShipOptionMaster.OptionName5";
        }
        if (ddlfilterbusiness.SelectedIndex <= 0 && txtsearch.Text.Length > 0)
        {
            lblBusiness.Text = ddlfilterbusiness.SelectedItem.Text;
            str1 = "SELECT     ShippersShipOptionMaster.ShippersId, ShippersShipOptionMaster.ShippersShipOptionMasterId, ShippersShipOptionMaster.OptionName,ShippersShipOptionMaster.OptionName2,ShippersShipOptionMaster.OptionName3,ShippersShipOptionMaster.OptionName4,ShippersShipOptionMaster.OptionName5 ,ShippersShipOptionMaster.OptionName +':'+ ShippersShipOptionMaster.OptionName2 + ':' + ShippersShipOptionMaster.OptionName3 + ':' + ShippersShipOptionMaster.OptionName4 + ':' + ShippersShipOptionMaster.OptionName5 as Options,ShippersShipOptionMaster.whid,WarehouseMaster.Name, ShippersMaster.ShippersName" +
                   " FROM         ShippersMaster INNER JOIN" +
                   " ShippersShipOptionMaster ON ShippersMaster.ShippersId = ShippersShipOptionMaster.ShippersId inner join WarehouseMaster on ShippersShipOptionMaster.whid=WarehouseMaster.WarehouseId where ShippersMaster.compid='" + Session["comid"] + "'"+
                   " and (ShippersShipOptionMaster.OptionName like '%" + txtsearch.Text + "%' or ShippersShipOptionMaster.OptionName2 like '%" + txtsearch.Text + "%' or ShippersShipOptionMaster.OptionName3 like '%" + txtsearch.Text + "%' or ShippersShipOptionMaster.OptionName4 like '%" + txtsearch.Text + "%' or ShippersShipOptionMaster.OptionName5 like '%" + txtsearch.Text + "%' or ShippersMaster.ShippersName like '%" + txtsearch.Text + "%')order by  WarehouseMaster.Name,ShippersShipOptionMaster.OptionName,ShippersShipOptionMaster.OptionName2,ShippersShipOptionMaster.OptionName3,ShippersShipOptionMaster.OptionName4,ShippersShipOptionMaster.OptionName5";

        
        
        }
       
            DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(str1, con);
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {

            GridView1.DataSource = dt;
            DataView myDataView = new DataView();
            myDataView = dt.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            GridView1.DataBind();
        }
        else
        {
            //GridView1.EmptyDataText = "No Record Found.";
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        fillgrid();
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
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        if (e.CommandName == "Sort")
        {
            return;
        }
      
        if (e.CommandName == "Edit")
        {
            statuslable.Visible = false;
            addinventoryroom.Visible = true;
            int a = Convert.ToInt32(e.CommandArgument);
            lblid.Text = a.ToString();

            SqlCommand cmd = new SqlCommand("   SELECT     ShippersShipOptionMaster.ShippersId,  ShippersShipOptionMaster.OptionName,ShippersShipOptionMaster.OptionName2,ShippersShipOptionMaster.OptionName3,ShippersShipOptionMaster.OptionName4,ShippersShipOptionMaster.OptionName5,ShippersShipOptionMaster.whid,WarehouseMaster.Name, ShippersMaster.ShippersName" +
                     " FROM         ShippersMaster INNER JOIN" +
                     " ShippersShipOptionMaster ON ShippersMaster.ShippersId = ShippersShipOptionMaster.ShippersId inner join WarehouseMaster on ShippersShipOptionMaster.whid=WarehouseMaster.WarehouseId where ShippersShipOptionMaster.ShippersShipOptionMasterId=" + a, con);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        btnaddroom.Visible = false;
        title.Visible = true;
        title.Text = "Edit Shipping Options";
        ImageButton1.Visible = false;
        ImageButton7.Visible = true;
        ddlbusiness.Items.Clear();
        fillstore();
            
        ddlbusiness.SelectedIndex = ddlbusiness.Items.IndexOf(ddlbusiness.Items.FindByValue(Convert.ToInt32(dt.Rows[0]["whid"]).ToString()));
      //  ddlbusiness.SelectedItem.Text = dt.Rows[0]["Name"].ToString();
        ddldesignation.Items.Clear();
        fillddldepartment();
        ddldesignation.SelectedIndex = ddldesignation.Items.IndexOf(ddldesignation.Items.FindByValue(Convert.ToInt32(dt.Rows[0]["ShippersId"]).ToString()));
           // ddldesignation.SelectedItem.Text = dt.Rows[0]["ShippersName"].ToString();
        txtdegnation.Text = dt.Rows[0]["OptionName"].ToString();
        txt2.Text = dt.Rows[0]["OptionName2"].ToString();
        txt3.Text = dt.Rows[0]["OptionName3"].ToString();
        TextBox1.Text = dt.Rows[0]["OptionName4"].ToString();
        TextBox2.Text = dt.Rows[0]["OptionName5"].ToString();
            


        }
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgrid();
        statuslable.Visible = false;
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
       
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
       
       
    }
    protected void GridView1_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
        ViewState["Id"] = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        //ModalPopupExtender1222.Show();
        ImageButton2_Click(sender, e);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("delete  from ShippersShipOptionMaster where [ShippersShipOptionMasterId]=" + ViewState["Id"] + " ", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
        statuslable.Visible = true;
        statuslable.Text = "Record deleted successfully";
        fillgrid();

        GridView1.SelectedIndex = -1;
        clear();
    }
   
    protected void ImageButtonasd2_Click(object sender, EventArgs e)
    {
        statuslable.Visible = false;
        ddldesignation.SelectedIndex = 0;
        txtdegnation.Text = "";
        addinventoryroom.Visible = false;
        btnaddroom.Visible = true;
        title.Visible = false;
        ImageButton1.Visible = true;
        ImageButton7.Visible = false;
        clear();
        
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (GridView1.Columns[9].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[9].Visible = false;
            }
            if (GridView1.Columns[10].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[10].Visible = false;
            }

        }
        else
        {

            pnlgrid.ScrollBars = ScrollBars.Vertical;
            pnlgrid.Height = new Unit(250);

            Button1.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[9].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[10].Visible = true;
            }

        }
    }
    protected void btnaddroom_Click(object sender, EventArgs e)
    {
        if (addinventoryroom.Visible == false)
        {
            addinventoryroom.Visible = true;
            statuslable.Visible = false;

        }
        else if (addinventoryroom.Visible == true)
        {
            addinventoryroom.Visible = false;

        }
        // statuslable.Text = "";
        btnaddroom.Visible = false;
        title.Visible = true;
        title.Text = "Add Shipping Options";
    }
    protected void ImageButton7_Click(object sender, EventArgs e)
    {
        string str1 = "SELECT OptionName,ShippersShipOptionMasterId FROM ShippersShipOptionMaster where OptionName='" + txtdegnation.Text + "' and [ShippersId]= '" + ddldesignation.SelectedValue + "' and  ShippersShipOptionMasterId<>'" + Convert.ToInt32(lblid.Text) + "' and OptionName2= '" + txt2.Text + "' and OptionName3='" + txt3.Text + "' and OptionName4='" + TextBox1.Text + "' and OptionName5='" + TextBox2.Text + "' and whid='" + ddlbusiness.SelectedValue + "'";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            statuslable.Visible = true;
            statuslable.Text = "ShippersOptionName already exist";
        }
        else
        {
            string update = "update  ShippersShipOptionMaster set OptionName='" + txtdegnation.Text + "', " +
                   " ShippersId='" + ddldesignation.SelectedValue + "',OptionName2= '" + txt2.Text + "' , OptionName3='" + txt3.Text + "' ,OptionName4='" + TextBox1.Text + "' , OptionName5='" + TextBox2.Text + "' ,whid='" + ddlbusiness.SelectedValue + "' where ShippersShipOptionMasterId='" + Convert.ToInt32(lblid.Text) + "' ";

            SqlCommand cmdupate = new SqlCommand(update, con);
            //SqlDataAdapter adpupdate = new SqlDataAdapter(cmdupate);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdupate.ExecuteNonQuery();
            con.Close();
            statuslable.Text = "Record updated successfully";
            statuslable.Visible = true;
            GridView1.EditIndex = -1;
            fillgrid();
            addinventoryroom.Visible = false;
            btnaddroom.Visible = true;
            ImageButton1.Visible = true;
            ImageButton7.Visible = false;
            clear();
            title.Visible = false;
        }
    }
    protected void ddlfilterbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    
    protected void clear()
    {
        ddldesignation.SelectedIndex = 0;
        txtdegnation.Text = "";
        txt2.Text = "";
        txt3.Text = "";
        TextBox1.Text = "";
        TextBox2.Text = "";
    }
    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddldepartment();
    }
    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {
        fillgrid();
        statuslable.Visible = false;
    }
}
