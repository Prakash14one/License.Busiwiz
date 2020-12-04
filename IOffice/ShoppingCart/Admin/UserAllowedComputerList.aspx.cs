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


public partial class UserAllowedComputerList : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(PageConn.connnn);
    SqlConnection con1;

    protected void Page_Load(object sender, EventArgs e)
    {
        con1 = PageConn.licenseconn();
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        if (!IsPostBack)
        {
            txtDate.Text = System.DateTime.Now.ToShortDateString();
            txttodate.Text = System.DateTime.Now.ToShortDateString();
            getwhid();
            newrequest();
            fillusertype();
            fillgrid();

        }
    }
    protected void fillgrid()
    {
        string str = "select UserMacAddressMasterTbl.*,PartytTypeMaster.PartType,Party_master.Compname,DepartmentmasterMNC.Departmentname,DesignationMaster.DesignationName,case when(UserMacAddressMasterTbl.Status='0') then 'Pending' else 'Approved' end as StatusLabel from UserMacAddressMasterTbl inner join User_master on User_master.UserID=UserMacAddressMasterTbl.UserId inner join Party_master on Party_master.PartyID=User_master.PartyID inner join EmployeeMaster on EmployeeMaster.PartyID=Party_master.PartyID inner join DepartmentmasterMNC on DepartmentmasterMNC.id=EmployeeMaster.DeptID inner join DesignationMaster on DesignationMaster.DesignationMasterId=EmployeeMaster.DesignationMasterId inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId where Party_master.id='" + Session["Comid"].ToString() + "' ";

        string strdate = "";
        string strusertype = "";
        string searchbyusername = "";
        string strstatus = "";

        if (txtDate.Text != "" && txttodate.Text != "")
        {
            strdate = " and UserMacAddressMasterTbl.DateandTimeRequest between '" + txtDate.Text + "' and '" + txttodate.Text + "' ";
        }

        if (ddlPartyType.SelectedIndex > 0)
        {
            strusertype = " and PartytTypeMaster.PartyTypeId='" + ddlPartyType.SelectedValue + "' ";
        }

        if (TextBox1.Text != "" && TextBox1.Text.Length > 0)
        {

            searchbyusername = " and (Party_master.Compname like '%" + TextBox1.Text.Replace("'", "''") + "%')";
        }

        if (DropDownList1.SelectedValue != "2")
        {
            strstatus = " and UserMacAddressMasterTbl.Status='" + DropDownList1.SelectedValue + "' ";
        }

        string finalstr = str + strdate + strusertype + searchbyusername + strstatus;

        SqlCommand cmd = new SqlCommand(finalstr, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        grdallowedlist.DataSource = dt;
        grdallowedlist.DataBind();


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
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void newrequest()
    {
        string qryStr = "select count(UserMacAddressMasterTbl.Id) as MasterId from UserMacAddressMasterTbl inner join User_master on User_master.UserID=UserMacAddressMasterTbl.UserId inner join Party_master on Party_master.PartyID=User_master.PartyID where Party_master.Whid='" + Session["Whid"].ToString() + "' and UserMacAddressMasterTbl.Status='0'";
        SqlCommand cmdeeed = new SqlCommand(qryStr, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);

        if (dteeed.Rows.Count > 0)
        {
            if (dteeed.Rows[0]["MasterId"].ToString() != null)
            {

                LinkButton1.Text = dteeed.Rows[0]["MasterId"].ToString() + " New Request";
            }
        }
       

    }
    protected void getwhid()
    {
        string qryStr = "select Party_master.Whid from User_master inner join Party_master on Party_master.PartyID=User_master.PartyID where User_master.UserID='" + Session["userid"].ToString() + "'";
        SqlCommand cmdeeed = new SqlCommand(qryStr, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);

        if (dteeed.Rows.Count > 0)
        {
            Session["Whid"] = dteeed.Rows[0]["Whid"].ToString();
        }
        else
        {
            Session["Whid"] = "0";
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        fillgridpopup();
        ModalPopupExtender1333.Show();
    }

    protected void fillgridpopup()
    {
        string str = "select UserMacAddressMasterTbl.*,PartytTypeMaster.PartType,Party_master.Compname,DepartmentmasterMNC.Departmentname,DesignationMaster.DesignationName,case when(UserMacAddressMasterTbl.Status='0') then 'Pending' else 'Approved' end as StatusLabel from UserMacAddressMasterTbl inner join User_master on User_master.UserID=UserMacAddressMasterTbl.UserId inner join Party_master on Party_master.PartyID=User_master.PartyID inner join EmployeeMaster on EmployeeMaster.PartyID=Party_master.PartyID inner join DepartmentmasterMNC on DepartmentmasterMNC.id=EmployeeMaster.DeptID inner join DesignationMaster on DesignationMaster.DesignationMasterId=EmployeeMaster.DesignationMasterId inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId where Party_master.id='" + Session["Comid"].ToString() + "' and Party_master.Whid='" + Session["Whid"].ToString() + "' and UserMacAddressMasterTbl.Status='0' ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);

        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gdr in GridView1.Rows)
        {


            DropDownList DropDownList2 = (DropDownList)gdr.FindControl("DropDownList2");
            Label lblusermacaddressmasterid = (Label)gdr.FindControl("lblusermacaddressmasterid");

            if (DropDownList2.SelectedValue != "2"  )
            {
                string strupdate = " update UserMacAddressMasterTbl set Status='" + DropDownList2.SelectedValue + "',DateandTimeActive='" + DateTime.Now.ToString() + "',ApprovalUserId='" + Session["userid"].ToString() + "' where Id='" + lblusermacaddressmasterid.Text + "'";
                SqlCommand cmdupdate = new SqlCommand(strupdate, con);
                con.Open();
                cmdupdate.ExecuteNonQuery();
                con.Close();

                string str = "select * from MacAddressRemovalRequestMasterTbl where NewUserMacAccessTableId='" + lblusermacaddressmasterid.Text + "'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    string strdelete = " delete from UserMacAddressMasterTbl where Id='" + dt.Rows[0]["RemovalUserMacAddressMasterId"].ToString() + "' ";
                    SqlCommand cmddelete = new SqlCommand(strdelete, con);
                    con.Open();
                    cmddelete.ExecuteNonQuery();
                    con.Close();


                    string strupdateremove = " update UserMacAddressMasterTbl set RemovalProcessStatus='1'  where RemovalUserMacAddressMasterId='" + dt.Rows[0]["ID"].ToString() + "' ";
                    SqlCommand cmdupdateremove = new SqlCommand(strupdateremove, con);
                    con.Open();
                    cmdupdateremove.ExecuteNonQuery();
                    con.Close();



                }

            }

            
        }
        newrequest();
        fillgrid();
    }
}
