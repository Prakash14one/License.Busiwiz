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


public partial class DesignationWisePanelRights : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(PageConn.connnn);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }

        if (!IsPostBack)
        {
            fillstore();
            designation();
            headerformat();
            
        }


    }
    protected void designation()
    {
        string str = "select DepartmentmasterMNC.Departmentname,DepartmentmasterMNC.Departmentname+'-'+DesignationMaster.DesignationName as DesignationName ,DesignationMaster.DesignationMasterId from DepartmentmasterMNC inner join DesignationMaster on DesignationMaster.DeptID=DepartmentmasterMNC.id where Companyid='" + Session["Comid"].ToString() + "' and Whid='" + ddlbusinessname.SelectedValue + "' order by DesignationName ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);


        GridView1.DataSource = dt;
        GridView1.DataBind();

        int flag = 0;
        foreach (GridViewRow gdr in GridView1.Rows)
        {

            Label lbldesignationid = (Label)gdr.FindControl("lbldesignationid");
            string strdetail = "select * from DesignationWisePanelRights where PageMenuId='" + ddlpagename.SelectedValue + "' and DesignationId='" + lbldesignationid.Text + "'";
            SqlCommand cmddetail = new SqlCommand(strdetail, con);
            SqlDataAdapter adpdetail = new SqlDataAdapter(cmddetail);
            DataTable dtdetail = new DataTable();
            adpdetail.Fill(dtdetail);

            if (dtdetail.Rows.Count > 0)
            {
                flag = 1;
            }

        }
        if (flag == 1)
        {
            Button1.Visible = false;
            Button2.Visible = true;
        }
        else
        {
            Button1.Visible = true;
            Button2.Visible = false;
        }

    }
    protected void fillstore()
    {
        ddlbusinessname.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlbusinessname.DataSource = ds;
        ddlbusinessname.DataTextField = "Name";
        ddlbusinessname.DataValueField = "WareHouseId";
        ddlbusinessname.DataBind();

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlbusinessname.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }


    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gdr in GridView1.Rows)
        {

            Label lbldesignationid = (Label)gdr.FindControl("lbldesignationid");
            CheckBox CheckBoxpanel1 = (CheckBox)gdr.FindControl("CheckBoxpanel1");
            CheckBox CheckBoxpanel2 = (CheckBox)gdr.FindControl("CheckBoxpanel2");
            CheckBox CheckBoxpanel3 = (CheckBox)gdr.FindControl("CheckBoxpanel3");
            CheckBox CheckBoxpanel4 = (CheckBox)gdr.FindControl("CheckBoxpanel4");
            CheckBox CheckBoxpanel5 = (CheckBox)gdr.FindControl("CheckBoxpanel5");
            CheckBox CheckBoxpanel6 = (CheckBox)gdr.FindControl("CheckBoxpanel6");
            CheckBox CheckBoxpanel7 = (CheckBox)gdr.FindControl("CheckBoxpanel7");
            CheckBox CheckBoxpanel8 = (CheckBox)gdr.FindControl("CheckBoxpanel8");


            string str1 = "Insert into DesignationWisePanelRights (PageMenuId,PageName,DesignationId,Whid,Compid,Panel1,Panel2,Panel3,Panel4,Panel5,Panel6,Panel7,Panel8) values ('" + ddlpagename.SelectedValue + "','" + ddlpagename.SelectedItem.Text + "','" + lbldesignationid.Text + "','" + ddlbusinessname.SelectedValue + "','" + Session["Comid"].ToString() + "','" + CheckBoxpanel1.Checked + "','" + CheckBoxpanel2.Checked + "','" + CheckBoxpanel3.Checked + "','" + CheckBoxpanel4.Checked + "','" + CheckBoxpanel5.Checked + "','" + CheckBoxpanel6.Checked + "','" + CheckBoxpanel7.Checked + "','" + CheckBoxpanel8.Checked + "')";
            SqlCommand cmd1 = new SqlCommand(str1, con);
            con.Open();
            cmd1.ExecuteNonQuery();
            con.Close();
            lblmsg.Text = "Record inserted successfully";

        }
        designation();
    }
    protected void ddlbusinessname_SelectedIndexChanged(object sender, EventArgs e)
    {
        designation();
    

    }
    protected void ddlpagename_SelectedIndexChanged(object sender, EventArgs e)
    {
        designation();
        headerformat();
       
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            Label lbldesignationid = (Label)e.Row.FindControl("lbldesignationid");
            CheckBox CheckBoxpanel1 = (CheckBox)e.Row.FindControl("CheckBoxpanel1");
            CheckBox CheckBoxpanel2 = (CheckBox)e.Row.FindControl("CheckBoxpanel2");
            CheckBox CheckBoxpanel3 = (CheckBox)e.Row.FindControl("CheckBoxpanel3");
            CheckBox CheckBoxpanel4 = (CheckBox)e.Row.FindControl("CheckBoxpanel4");
            CheckBox CheckBoxpanel5 = (CheckBox)e.Row.FindControl("CheckBoxpanel5");
            CheckBox CheckBoxpanel6 = (CheckBox)e.Row.FindControl("CheckBoxpanel6");
            CheckBox CheckBoxpanel7 = (CheckBox)e.Row.FindControl("CheckBoxpanel7");
            CheckBox CheckBoxpanel8 = (CheckBox)e.Row.FindControl("CheckBoxpanel8");

            string str = "select * from DesignationWisePanelRights where PageMenuId='" + ddlpagename.SelectedValue + "' and DesignationId='" + lbldesignationid.Text + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                CheckBoxpanel1.Checked = Convert.ToBoolean(dt.Rows[0]["Panel1"].ToString());
                CheckBoxpanel2.Checked = Convert.ToBoolean(dt.Rows[0]["Panel2"].ToString());
                CheckBoxpanel3.Checked = Convert.ToBoolean(dt.Rows[0]["Panel3"].ToString());
                CheckBoxpanel4.Checked = Convert.ToBoolean(dt.Rows[0]["Panel4"].ToString());
                CheckBoxpanel5.Checked = Convert.ToBoolean(dt.Rows[0]["Panel5"].ToString());
                CheckBoxpanel6.Checked = Convert.ToBoolean(dt.Rows[0]["Panel6"].ToString());
                CheckBoxpanel7.Checked = Convert.ToBoolean(dt.Rows[0]["Panel7"].ToString());
                CheckBoxpanel8.Checked = Convert.ToBoolean(dt.Rows[0]["Panel8"].ToString());

               

                
            }
           
        }

       
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gdr in GridView1.Rows)
        {

            Label lbldesignationid = (Label)gdr.FindControl("lbldesignationid");
            CheckBox CheckBoxpanel1 = (CheckBox)gdr.FindControl("CheckBoxpanel1");
            CheckBox CheckBoxpanel2 = (CheckBox)gdr.FindControl("CheckBoxpanel2");
            CheckBox CheckBoxpanel3 = (CheckBox)gdr.FindControl("CheckBoxpanel3");
            CheckBox CheckBoxpanel4 = (CheckBox)gdr.FindControl("CheckBoxpanel4");
            CheckBox CheckBoxpanel5 = (CheckBox)gdr.FindControl("CheckBoxpanel5");
            CheckBox CheckBoxpanel6 = (CheckBox)gdr.FindControl("CheckBoxpanel6");
            CheckBox CheckBoxpanel7 = (CheckBox)gdr.FindControl("CheckBoxpanel7");
            CheckBox CheckBoxpanel8 = (CheckBox)gdr.FindControl("CheckBoxpanel8");


             string str = "select * from DesignationWisePanelRights where PageMenuId='" + ddlpagename.SelectedValue + "' and DesignationId='" + lbldesignationid.Text + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {

                string str1 = "update DesignationWisePanelRights set Panel1='" + CheckBoxpanel1.Checked + "',Panel2='" + CheckBoxpanel2.Checked + "',Panel3='" + CheckBoxpanel3.Checked + "',Panel4='" + CheckBoxpanel4.Checked + "',Panel5='" + CheckBoxpanel5.Checked + "',Panel6='" + CheckBoxpanel6.Checked + "',Panel7='" + CheckBoxpanel7.Checked + "',Panel8='" + CheckBoxpanel8.Checked + "' where DesignationId='" + lbldesignationid.Text + "' and PageMenuId='"+ddlpagename.SelectedValue+"'";
                SqlCommand cmd1 = new SqlCommand(str1, con);
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                string str1 = "Insert into DesignationWisePanelRights (PageMenuId,PageName,DesignationId,Whid,Compid,Panel1,Panel2,Panel3,Panel4,Panel5,Panel6,Panel7,Panel8) values ('" + ddlpagename.SelectedValue + "','" + ddlpagename.SelectedItem.Text + "','" + lbldesignationid.Text + "','" + ddlbusinessname.SelectedValue + "','" + Session["Comid"].ToString() + "','" + CheckBoxpanel1.Checked + "','" + CheckBoxpanel2.Checked + "','" + CheckBoxpanel3.Checked + "','" + CheckBoxpanel4.Checked + "','" + CheckBoxpanel5.Checked + "','" + CheckBoxpanel6.Checked + "','" + CheckBoxpanel7.Checked + "','" + CheckBoxpanel8.Checked + "')";
                SqlCommand cmd1 = new SqlCommand(str1, con);
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();
                
            }

            

        }
        lblmsg.Text = "Record updated successfully";

        designation();

        
    }
    protected void headerformat()
    {
        GridViewRow ft = (GridViewRow)(GridView1.HeaderRow);
        Label lblpanel1hdr = (Label)(ft.FindControl("lblpanel1hdr"));
        Label lblpanel2hdr = (Label)(ft.FindControl("lblpanel2hdr"));
        Label lblpanel3hdr = (Label)(ft.FindControl("lblpanel3hdr"));
        Label lblpanel4hdr = (Label)(ft.FindControl("lblpanel4hdr"));
        Label lblpanel5hdr = (Label)(ft.FindControl("lblpanel5hdr"));
        Label lblpanel6hdr = (Label)(ft.FindControl("lblpanel6hdr"));
        Label lblpanel7hdr = (Label)(ft.FindControl("lblpanel7hdr"));
        Label lblpanel8hdr = (Label)(ft.FindControl("lblpanel8hdr"));
        

        if (ddlpagename.SelectedValue == "1")
        {
            lblpanel1hdr.Text = "(Key Finacial)";
            lblpanel2hdr.Text = "(Sales Collection Management)";
            lblpanel3hdr.Text = "(Payment Management)";
            lblpanel4hdr.Text = "(Cash & Bank Balances)";
            lblpanel5hdr.Text = "(Inventory)";
            lblpanel6hdr.Text = "(Accounts Receivable)";
            lblpanel7hdr.Text = "(Accounts Payable)";
            lblpanel8hdr.Text = "(List of Outstanding Sales Invoices)";

        }
        if (ddlpagename.SelectedValue == "2")
        {
            lblpanel1hdr.Text = "Document Search";
            lblpanel2hdr.Text = "Document Tree";
            lblpanel3hdr.Text = "Document Search By Cabinet-Drawer-Folder";
            lblpanel4hdr.Text = "Upload Documents";
            lblpanel5hdr.Text = "My Documents";
            lblpanel6hdr.Text = "List of Documents to be Approved by me";
            lblpanel7hdr.Text = "";
            lblpanel8hdr.Text = "";
        }
        if (ddlpagename.SelectedValue == "3")
        {
            lblpanel1hdr.Text = "";
            lblpanel2hdr.Text = "";
            lblpanel3hdr.Text = "";
            lblpanel4hdr.Text = "";
            lblpanel5hdr.Text = "";
            lblpanel6hdr.Text = "";
            lblpanel7hdr.Text = "";
            lblpanel8hdr.Text = "";
        }
        if (ddlpagename.SelectedValue == "4")
        {
            lblpanel1hdr.Text = "";
            lblpanel2hdr.Text = "";
            lblpanel3hdr.Text = "";
            lblpanel4hdr.Text = "";
            lblpanel5hdr.Text = "";
            lblpanel6hdr.Text = "";
            lblpanel7hdr.Text = "";
            lblpanel8hdr.Text = "";
        }
        if (ddlpagename.SelectedValue == "5")
        {
            lblpanel1hdr.Text = "Today's Presence";
            lblpanel2hdr.Text = "Reminder Note";
            lblpanel3hdr.Text = "List of Leave Request";
            lblpanel4hdr.Text = "List of Gate Pass Approval";
            lblpanel5hdr.Text = "";
            lblpanel6hdr.Text = "";
            lblpanel7hdr.Text = "";
            lblpanel8hdr.Text = "";

        }


    }
}
