
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

public partial class ShoppingCart_Admin_DoorAccess : System.Web.UI.Page
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

        if (!Page.IsPostBack)
        {
            ViewState["sortOrder"] = "";
            lblCompany.Text = Session["Cname"].ToString();

            fillstore();
            //ddlwarehouse_SelectedIndexChanged(sender, e);

        }

    }
    protected void fillstore()
    {
        ddlwarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlwarehouse.DataSource = ds;
        ddlwarehouse.DataTextField = "Name";
        ddlwarehouse.DataValueField = "WareHouseId";
        ddlwarehouse.DataBind();
        //ddlstore.Items.Insert(0, "Select");

        ViewState["cd"] = "1";
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlwarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
      
    }
    protected void fillgrid()
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        DataTable gdr = new DataTable();
        lblBusiness.Text = ddlwarehouse.SelectedItem.Text;
        //lbltype.Text = rdlist.SelectedItem.Text;
        if (rdlist.SelectedValue == "1")
        {
            lbltype.Text = "Door Access Type : Designation";
            lblemptext.Text = "Designation : ";
            lblemp.Text = ddldesig.SelectedItem.Text;
            gdr = select("select distinct DoorNo,Location, Door_IPbaseDiviceMasterTbl.Id as DoorId, DoorAccessbyDesignation.Id, case when(DoorAccessbyDesignation.DoorOpen IS NULL) then Cast('0' as bit) else DoorOpen end as chk ,case when(DoorAccessbyDesignation.DoorStrart IS NULL) then Cast('0' as bit) else DoorStrart end as chk1 ,case when(DoorAccessbyDesignation.DoorStop IS NULL) then Cast('0' as bit) else DoorStop end as chk2 ," +
                  " Door_IPbaseDiviceMasterTbl.DoorName  from Door_IPbaseDiviceMasterTbl Left join DoorAccessbyDesignation on " +
                  " DoorAccessbyDesignation.DoorMasterId=Door_IPbaseDiviceMasterTbl.Id and DesignationId='" + ddldesig.SelectedValue + "' where Whid='" + ddlwarehouse.SelectedValue + "' Order by DoorName");


        }
        else
        {
            lbltype.Text = "Door Access Type : Employee";
            lblemptext.Text = "Employee :";
            lblemp.Text = ddlemp.SelectedItem.Text;
            gdr = select("select distinct DoorNo,Location, Door_IPbaseDiviceMasterTbl.Id as DoorId, DoorAccessbyEmployee.Id, case when(DoorAccessbyEmployee.DoorOpen IS NULL) then Cast('0' as bit) else DoorOpen end as chk ,case when(DoorAccessbyEmployee.DoorStrart IS NULL) then Cast('0' as bit) else DoorStrart end as chk1 ,case when(DoorAccessbyEmployee.DoorStop IS NULL) then Cast('0' as bit) else DoorStop end as chk2 ," +
                      " Door_IPbaseDiviceMasterTbl.DoorName  from Door_IPbaseDiviceMasterTbl Left join DoorAccessbyEmployee on " +
                      " DoorAccessbyEmployee.DoorMasterId=Door_IPbaseDiviceMasterTbl.Id and EmployeeId='" + ddlemp.SelectedValue + "' where Whid='" + ddlwarehouse.SelectedValue + "' Order by DoorName");

        }

        GridView1.DataSource = gdr;

        DataView myDataView = new DataView();
        myDataView = gdr.DefaultView;
        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        GridView1.DataBind();

    }
    protected void ch1_chachedChanged(object sender, EventArgs e)
    {
       
        foreach (GridViewRow item in GridView1.Rows)
        {
            CheckBox cbItem1 = (CheckBox)item.FindControl("cbItem");
            cbItem1.Checked = ((CheckBox)sender).Checked;
        }
    }
    protected void ch2_chachedChanged(object sender, EventArgs e)
    {

        foreach (GridViewRow item in GridView1.Rows)
        {
            CheckBox cbItem1 = (CheckBox)item.FindControl("cbItem1");
            cbItem1.Checked = ((CheckBox)sender).Checked;
        }
    }
    protected void ch3_chachedChanged(object sender, EventArgs e)
    {

        foreach (GridViewRow item in GridView1.Rows)
        {
            CheckBox cbItem1 = (CheckBox)item.FindControl("cbItem2");
            cbItem1.Checked = ((CheckBox)sender).Checked;
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
    protected void rdlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        pnlgd.Visible = false;
        Label1.Text = "";
        if (rdlist.SelectedValue == "1")
        {
            pndesi.Visible = true;
            pnlemp.Visible = false;
            lblList.Text = "The selected Designation has the following Door/IP Based Device Access Rights";
            lblghead.Text = "The selected Designation has the following Door/IP Based Device Access Rights";
            filldes();
        }
        else
        {
            pndesi.Visible = false;
            pnlemp.Visible = true;
            lblList.Text = "The selected employee has the following Door/IP Based Device Access Rights";
            lblghead.Text = "The selected employee has the following Door/IP Based Device Access Rights";
            fillemp();
        }
      
       

        //fillgrid();
    }
    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        pnlgd.Visible = false;
        Label1.Text = "";
        filldes();
        fillemp();

    }
    protected void filldes()
    {

        DataTable drt = select("select DesignationName,DesignationMasterId from DesignationMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.Id=DesignationMaster.DeptID where  DepartmentmasterMNC.Whid='" + ddlwarehouse.SelectedValue + "' order by DesignationName");
        ddldesig.DataSource = drt;
        ddldesig.DataTextField = "DesignationName";
        ddldesig.DataValueField = "DesignationMasterId";
        ddldesig.DataBind();
        ddldesig.Items.Insert(0, "Select");
        ddldesig.Items[0].Value = "0";

    }
    protected void fillemp()
    {

        DataTable drt = select("select DesignationName+':'+ EmployeeName as EmployeeName,EmployeeMasterId from EmployeeMaster inner join DesignationMaster on DesignationMaster.DesignationMasterId=EmployeeMaster.DesignationMasterId where Whid='" + ddlwarehouse.SelectedValue + "' order by  EmployeeName");
        ddlemp.DataSource = drt;
        ddlemp.DataTextField = "EmployeeName";
        ddlemp.DataValueField = "EmployeeMasterId";
        ddlemp.DataBind();
        ddlemp.Items.Insert(0, "Select");
        ddlemp.Items[0].Value = "0";
    }
    protected DataTable select(string str)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(dt);
        return dt;
    }
    protected void ddldesig_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        pnlgd.Visible = false;
        Label1.Text = "";
        if (ddldesig.SelectedIndex > 0)
        {
            pnlgd.Visible = true;
        }
        else
        {
            pnlgd.Visible = false;
        }
        fillgrid();
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        string datt="";
        DataTable drfin = select("select * from DoorAccessMastertbl where Whid='"+ddlwarehouse.SelectedValue+"'");
        if (drfin.Rows.Count <= 0)
        {
            datt = "insert into DoorAccessMastertbl(Whid,DoorAccessType)values('" + ddlwarehouse.SelectedValue + "','" + rdlist.SelectedValue + "')";
        }
        else
        {
            datt = "Update  DoorAccessMastertbl Set DoorAccessType='" + rdlist.SelectedValue + "' where  Whid='" + ddlwarehouse.SelectedValue + "'";

        }
        SqlCommand cmda = new SqlCommand(datt, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmda.ExecuteNonQuery();
        con.Close();
        foreach (GridViewRow item in GridView1.Rows)
        {
            string Id = GridView1.DataKeys[item.RowIndex].Value.ToString();
            Label lbldoorid = (Label)item.FindControl("lbldoorid");

            CheckBox cbItem = (CheckBox)item.FindControl("cbItem");
            CheckBox chkdef = (CheckBox)item.FindControl("chkdef");
            CheckBox cbItem1 = (CheckBox)item.FindControl("cbItem1");
            CheckBox chkdef1 = (CheckBox)item.FindControl("chkdef1");
            CheckBox cbItem2 = (CheckBox)item.FindControl("cbItem2");
            CheckBox chkdef2 = (CheckBox)item.FindControl("chkdef2");

            if (rdlist.SelectedValue == "1")
            {

                if (Convert.ToString(Id) == "")
                {
                    if (cbItem.Checked == true || cbItem1.Checked == true || cbItem2.Checked == true)
                    {
                        string spts = "insert into DoorAccessbyDesignation(DoorMasterId,DesignationId,DoorOpen,DoorStrart,DoorStop)values('" + lbldoorid.Text + "','" + ddldesig.SelectedValue + "','" + cbItem.Checked + "','" + cbItem1.Checked + "','" + cbItem2.Checked + "')";
                        SqlCommand cmd1s = new SqlCommand(spts, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd1s.ExecuteNonQuery();
                        con.Close();

                    }
                }
                else
                {
                    if (cbItem.Checked == true || cbItem1.Checked == true || cbItem2.Checked == true)
                    {
                        string update = "update  DoorAccessbyDesignation set DoorOpen='" + cbItem.Checked + "',DoorStrart='" + cbItem1.Checked + "',DoorStop='" + cbItem2.Checked + "' where  Id='" + Id + "'  ";
                        SqlCommand ccm = new SqlCommand(update, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        ccm.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        string update = "Delete from  DoorAccessbyDesignation  where  Id='" + Id + "'  ";
                        SqlCommand ccm = new SqlCommand(update, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        ccm.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            else
            {
                if (Convert.ToString(Id) == "")
                {
                    if (cbItem.Checked == true || cbItem1.Checked == true || cbItem2.Checked == true)
                    {
                        string spts = "insert into DoorAccessbyEmployee(DoorMasterId,EmployeeId,DoorOpen,DoorStrart,DoorStop)values('" + lbldoorid.Text + "','" + ddlemp.SelectedValue + "','" + cbItem.Checked + "','" + cbItem1.Checked + "','" + cbItem2.Checked + "')";
                        SqlCommand cmd1s = new SqlCommand(spts, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd1s.ExecuteNonQuery();
                        con.Close();

                    }
                }
                else
                {
                    if (cbItem.Checked == true || cbItem1.Checked == true || cbItem2.Checked == true)
                    {
                        string update = "update  DoorAccessbyEmployee set DoorOpen='" + cbItem.Checked + "',DoorStrart='" + cbItem1.Checked + "',DoorStop='" + cbItem2.Checked + "' where  Id='" + Id + "'  ";
                        SqlCommand ccm = new SqlCommand(update, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        ccm.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        string update = "Delete from  DoorAccessbyEmployee  where  Id='" + Id + "'  ";
                        SqlCommand ccm = new SqlCommand(update, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        ccm.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
        }

        fillgrid();
        Label1.Text = "Record save successfully";
    }
    protected void ddlemp_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        pnlgd.Visible = false;
        Label1.Text = "";
        if (ddlemp.SelectedIndex > 0)
        {
            pnlgd.Visible = true;
            
        }
        else
        {
            pnlgd.Visible = false;
        }
        fillgrid();
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button4.Text == "Printable Version")
        {
           

            Button4.Text = "Hide Printable Version";
            Button3.Visible = true;
           
        }
        else
        {

          
            Button4.Text = "Printable Version";
            Button3.Visible = false;
            
        }
    }
}
