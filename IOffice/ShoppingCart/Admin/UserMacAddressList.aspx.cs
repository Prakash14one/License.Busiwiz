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
using System.Data.SqlClient;
using System.Xml.Linq;

public partial class UserMacAddressList : System.Web.UI.Page
{

    SqlConnection con;
    string compid;
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

        compid = Session["Comid"].ToString();
        Page.Title = pg.getPageTitle(page);

        if (!IsPostBack)
        {
            fillbusiness();
            fillusertype();
            filluser();
            fillgrid();

            addnewfillbusiness();
            addnewfillusertype();
            addnewfilluser();

            lblcomname.Text = Session["Cname"].ToString();
            lblBusiness.Text = ddlbusinessname.SelectedItem.Text;
        }
    }

    protected void fillgrid()
    {

        string str = "select Ipaddresschangerequesttbl.*,PartytTypeMaster.PartType,Party_master.Compname,WareHouseMaster.Name as WName,case when(Ipaddresschangerequesttbl.ApprovalStatus='1') then 'Approved' else 'Unapproved' end as StatusLabel from Ipaddresschangerequesttbl inner join User_master on User_master.UserID=Ipaddresschangerequesttbl.emailgenerateduserid inner join Party_master on Party_master.PartyID=User_master.PartyID  inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId inner join WareHouseMaster on WareHouseMaster.WareHouseId=Party_master.Whid where Party_master.id='" + Session["Comid"].ToString() + "' and Ipaddresschangerequesttbl.Requesttype='0' ";

        string strbusiness = "";
        string strusertype = "";
        string searchbyusername = "";
        string strstatus = "";

        if (ddlbusinessname.SelectedIndex > 0)
        {
            strbusiness = " and Party_master.Whid='" + ddlbusinessname.SelectedValue + "' ";
        }

        if (ddlPartyType.SelectedIndex > 0)
        {
            strusertype = " and PartytTypeMaster.PartyTypeId='" + ddlPartyType.SelectedValue + "' ";
        }

        if (ddlusername.SelectedIndex > 0)
        {
            searchbyusername = " and Ipaddresschangerequesttbl.emailgenerateduserid='" + ddlusername.SelectedValue + "' ";
        }

        if (DropDownList1.SelectedValue != "2")
        {
            strstatus = " and Ipaddresschangerequesttbl.ApprovalStatus='" + DropDownList1.SelectedValue + "' ";
        }

        string finalstr = str + strbusiness + strusertype + searchbyusername + strstatus;
        SqlCommand cmd = new SqlCommand(finalstr, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        grdallowedlist.DataSource = dt;
        grdallowedlist.DataBind();

        if (DropDownList1.SelectedValue == "1")
        {
            Button2.Visible = false;
        }
        if (DropDownList1.SelectedValue == "0")
        {
            if (dt.Rows.Count > 0)
            {
                Button2.Visible = true;
            }
            else
            {
                Button2.Visible = false;
            }
        }



    }

    protected void fillbusiness()
    {
        ddlbusinessname.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlbusinessname.DataSource = ds;
        ddlbusinessname.DataTextField = "Name";
        ddlbusinessname.DataValueField = "WareHouseId";
        ddlbusinessname.DataBind();
        ddlbusinessname.Items.Insert(0, "All");
        ddlbusinessname.Items[0].Value = "0";

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();
        if (dteeed.Rows.Count > 0)
        {
            ddlbusinessname.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }


    }
    protected void fillusertype()
    {
        string qryStr = "select * from PartytTypeMaster  where  compid='" + Session["Comid"] + "'  order by PartType";
        SqlCommand cmdeeed = new SqlCommand(qryStr, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);

        ddlPartyType.DataSource = dteeed;
        ddlPartyType.DataTextField = "PartType";
        ddlPartyType.DataValueField = "PartyTypeId";
        ddlPartyType.DataBind();

        ddlPartyType.Items.Insert(0, "All");
        ddlPartyType.Items[0].Value = "0";
    }
    protected void filluser()
    {
        string str = "";
        str = "select User_master.UserID, Party_master.PartyID,Party_master.Compname +' : '+ Party_master.Contactperson as PartyName from Party_master inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId inner join User_master on User_master.PartyID=Party_master.PartyID where Party_master.Whid='" + ddlbusinessname.SelectedValue + "'  and Party_master.PartyTypeId='" + ddlPartyType.SelectedValue + "' order by PartyName  ";
        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        ddlusername.DataSource = dt;
        ddlusername.DataTextField = "PartyName";
        ddlusername.DataValueField = "UserID";
        ddlusername.DataBind();

        ddlusername.Items.Insert(0, "All");
        ddlusername.Items[0].Value = "0";
    }

    protected void ddlbusinessname_SelectedIndexChanged(object sender, EventArgs e)
    {

        filluser();
    }
    protected void ddlPartyType_SelectedIndexChanged(object sender, EventArgs e)
    {
        filluser();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void grdallowedlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList DropDownList1grd = (DropDownList)e.Row.FindControl("DropDownList1grd");
            LinkButton LinkButton1 = (LinkButton)e.Row.FindControl("LinkButton1");
            Label lbluserid = (Label)e.Row.FindControl("lbluserid");

            string qryStr = " select * from UserMacAddressMasterTbl where UserId='" + lbluserid.Text + "' ";
            SqlCommand cmdeeed = new SqlCommand(qryStr, con);
            SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
            DataTable dteeed = new DataTable();
            adpeeed.Fill(dteeed);


            LinkButton1.Text = dteeed.Rows.Count.ToString();
            

            if (DropDownList1.SelectedValue == "0")
            {
                DropDownList1grd.SelectedValue = "0";
                DropDownList1grd.Enabled = true;
            }

            if (DropDownList1.SelectedValue == "1")
            {
                DropDownList1grd.SelectedValue = "1";
                DropDownList1grd.Enabled = false;
            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gdr in grdallowedlist.Rows)
        {
            DropDownList DropDownList1grd = (DropDownList)gdr.FindControl("DropDownList1grd");
            Label lblmasterid = (Label)gdr.FindControl("lblmasterid");
            Label lbluserid = (Label)gdr.FindControl("lbluserid");
            Label lblmacaddress = (Label)gdr.FindControl("lblmacaddress");
            Label lblrealcompname = (Label)gdr.FindControl("lblrealcompname");


            if (DropDownList1grd.SelectedValue == "1")
            {
                string str = "update Ipaddresschangerequesttbl set ApprovalStatus='1' where Id='" + lblmasterid.Text + "' ";
                SqlCommand cmd = new SqlCommand(str, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();

                string strinsert = "insert into UserMacAddressMasterTbl (UserId,MacAddress,ComputerRealName) values ('" + lbluserid.Text + "','" + lblmacaddress.Text + "','" + lblrealcompname.Text + "') ";
                SqlCommand cmdinsert = new SqlCommand(strinsert, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdinsert.ExecuteNonQuery();
                con.Close();

                statuslable.Visible = true;
                statuslable.Text = "Computer approved successfully";


            }
        }

        fillgrid();
    }



    protected void Button3_Click(object sender, EventArgs e)
    {
        if (Button3.Text == "Printable Version")
        {
            Button3.Text = "Hide Printable Version";
            Button4.Visible = true;

        }
        else
        {
            Button3.Text = "Printable Version";
            Button4.Visible = false;

        }
    }
    protected void Button5_Click(object sender, EventArgs e)
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
        Button5.Visible = false;
        statuslable.Text = "";

    }

    protected void addnewfillbusiness()
    {
        DropDownList2.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        DropDownList2.DataSource = ds;
        DropDownList2.DataTextField = "Name";
        DropDownList2.DataValueField = "WareHouseId";
        DropDownList2.DataBind();


        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();
        if (dteeed.Rows.Count > 0)
        {
            DropDownList2.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }


    }
    protected void addnewfillusertype()
    {
        string qryStr = "select * from PartytTypeMaster  where  compid='" + Session["Comid"] + "'  order by PartType";
        SqlCommand cmdeeed = new SqlCommand(qryStr, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);

        DropDownList3.DataSource = dteeed;
        DropDownList3.DataTextField = "PartType";
        DropDownList3.DataValueField = "PartyTypeId";
        DropDownList3.DataBind();


    }
    protected void addnewfilluser()
    {
        string str = "";
        str = "select User_master.UserID, Party_master.PartyID,Party_master.Compname +' : '+ Party_master.Contactperson as PartyName from Party_master inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId inner join User_master on User_master.PartyID=Party_master.PartyID where Party_master.Whid='" + DropDownList2.SelectedValue + "'  and Party_master.PartyTypeId='" + DropDownList3.SelectedValue + "' order by PartyName  ";
        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        DropDownList4.DataSource = dt;
        DropDownList4.DataTextField = "PartyName";
        DropDownList4.DataValueField = "UserID";
        DropDownList4.DataBind();


    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        addnewfilluser();
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        addnewfilluser();
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        string qryStr = "select * from UserMacAddressMasterTbl  where  UserId='" + DropDownList4.SelectedValue + "' and MacAddress='" + TextBox1.Text + "'";
        SqlCommand cmdeeed = new SqlCommand(qryStr, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);

        if (dteeed.Rows.Count > 0)
        {
            statuslable.Visible = true;
            statuslable.Text = "Record already exist.";
        }
        else
        {
            string strinsert = "insert into UserMacAddressMasterTbl (UserId,MacAddress,ComputerRealName) values ('" + DropDownList4.SelectedValue + "','" + TextBox1.Text + "','" + TextBox2.Text + "') ";
            SqlCommand cmdinsert = new SqlCommand(strinsert, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdinsert.ExecuteNonQuery();
            con.Close();

            pnladd.Visible = false;
            lbllegend.Visible = false;
            statuslable.Visible = true;
            statuslable.Text = "Record inserted successfully.";
        }
    }

    protected void link1_Click(object sender, EventArgs e)
    {
      
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        int rinrow = row.RowIndex;
        Label lbluserid = (Label)grdallowedlist.Rows[rinrow].FindControl("lbluserid");



        string qryStr = "select UserMacAddressMasterTbl.*,PartytTypeMaster.PartType,Party_master.Compname from UserMacAddressMasterTbl inner join User_master on User_master.UserID=UserMacAddressMasterTbl.UserId inner join Party_master on Party_master.PartyID=User_master.PartyID  inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId where Party_master.id='" + Session["Comid"].ToString() + "' and UserMacAddressMasterTbl.UserId='" + lbluserid.Text + "' ";
        SqlCommand cmdeeed = new SqlCommand(qryStr, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);

        GridView1.DataSource = dteeed;
        GridView1.DataBind();

        ModalPopupExtender1333.Show();





    }
}