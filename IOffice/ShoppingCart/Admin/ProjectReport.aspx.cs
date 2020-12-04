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


public partial class ProjectReport : System.Web.UI.Page
{
  
    SqlConnection con=new SqlConnection(PageConn.connnn);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
     
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };
      //  compid = Session["comid"].ToString();
        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();
     

        Page.Title = pg.getPageTitle(page);

        if (!Page.IsPostBack)
        {
            ViewState["sortOrder"] = "";
            Pagecontrol.dypcontrol(Page, page);
            fillstore();
            fillparty();
            Employee();
            fillgrid();
            filljob();
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
    protected void fillstore()
    {
        ddlstore.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlstore.DataSource = ds;
        ddlstore.DataTextField = "Name";
        ddlstore.DataValueField = "WareHouseId";
        ddlstore.DataBind();
        //ddlstore.Items.Insert(0, "Select");

        ViewState["cd"] = "1";
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlstore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }

    }


    protected void ddlStoreName_SelectedIndexChanged(object sender, EventArgs e)
    {  
        fillparty();
        lblmsg.Text = "";
        Employee();
    }
    protected void Employee()
    {
       // string str1 = "select EmployeeMasterID,EmployeeName from EmployeeMaster where Whid='" + ddlstore.SelectedValue + "' order by EmployeeName";
        string str1 = "select distinct EmployeeMasterID,EmployeeName  from Party_Master inner join JobMaster on JobMaster.PartyId=Party_Master.PartyId inner join JobEmployeeDailyTaskDetail on JobEmployeeDailyTaskDetail.JobMasterId=JobMaster.Id inner join JobEmployeeDailyTaskTbl on JobEmployeeDailyTaskTbl.Id=JobEmployeeDailyTaskDetail.JobDailyTaskMasterId inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=JobEmployeeDailyTaskTbl.EmployeeId where JobEmployeeDailyTaskTbl.Whid='" + ddlstore.SelectedValue + "'  and (FromToTime IS NOT NULL and FromToTime<>'')  order by EmployeeName Asc";
     
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        DataTable empdt = new DataTable();

        adp1.Fill(empdt);
        ddlemployee.DataSource = empdt;
        ddlemployee.DataTextField = "EmployeeName";
        ddlemployee.DataValueField = "EmployeeMasterID";
        ddlemployee.DataBind();
        ddlemployee.Items.Insert(0, "All");
        ddlemployee.Items[0].Value = "0";

    }
    protected void fillparty()
    {
        ddlparty.Items.Clear();
        string party = "";
        party = "Select Party_master.PartyId,PartytTypeMaster.PartType+':'+Party_master.Compname as Partyname from Party_master inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId";

        party = party + " Where Party_master.Whid='" + ddlstore.SelectedValue + "' and PartytTypeMaster.PartType='Customer' order by Partyname";


        SqlDataAdapter adp = new SqlDataAdapter(party, con);
        DataTable dts = new DataTable();
        adp.Fill(dts);
        if (dts.Rows.Count > 0)
        {
            ddlparty.DataSource = dts;
            ddlparty.DataTextField = "Partyname";
            ddlparty.DataValueField = "PartyId";
            ddlparty.DataBind();

        }
        ddlparty.Items.Insert(0, "All");
        ddlparty.Items[0].Value = "0";

      
        
    }

    protected void imgAdd2_Click(object sender, ImageClickEventArgs e)
    {
        string te = "PartyMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void LinkButton97666667_Click(object sender, ImageClickEventArgs e)
    {
        fillparty();
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        string te = "EmployeeMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
   
   
  
 

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
   
    }
    public DataTable CreateDatatableDept()
    {
        DataTable dtTemp = new DataTable();
        DataColumn dtc1 = new DataColumn();
        dtc1.ColumnName = "Id";
        dtc1.DataType = System.Type.GetType("System.String");
        dtc1.AllowDBNull = true;
        dtTemp.Columns.Add(dtc1);


        DataColumn dtc2 = new DataColumn();
        dtc2.ColumnName = "JobName";
        dtc2.DataType = System.Type.GetType("System.String");
        dtc2.AllowDBNull = true;
        dtTemp.Columns.Add(dtc2);

        DataColumn dtc3 = new DataColumn();
        dtc3.ColumnName = "Employee";
        dtc3.DataType = System.Type.GetType("System.String");
        dtc3.AllowDBNull = true;
        dtTemp.Columns.Add(dtc3);



        DataColumn dtc4 = new DataColumn();
        dtc4.ColumnName = "FromTime";
        dtc4.DataType = System.Type.GetType("System.String");
        dtc4.AllowDBNull = true;
        dtTemp.Columns.Add(dtc4);


        DataColumn dtc5 = new DataColumn();
        dtc5.ColumnName = "FromToTime";
        dtc5.DataType = System.Type.GetType("System.String");
        dtc5.AllowDBNull = true;
        dtTemp.Columns.Add(dtc5);


        DataColumn dtc6 = new DataColumn();
        dtc6.ColumnName = "Hrs";
        dtc6.DataType = System.Type.GetType("System.String");
        dtc6.AllowDBNull = true;
        dtTemp.Columns.Add(dtc6);

        DataColumn dtc7 = new DataColumn();
        dtc7.ColumnName = "Rate";
        dtc7.DataType = System.Type.GetType("System.String");
        dtc7.AllowDBNull = true;
        dtTemp.Columns.Add(dtc7);


        DataColumn dtc8= new DataColumn();
        dtc8.ColumnName = "Cost";
        dtc8.DataType = System.Type.GetType("System.String");
        dtc8.AllowDBNull = true;
        dtTemp.Columns.Add(dtc8);
        return dtTemp;
    }
    protected void fillgrid()
    {
        lblBusiness.Text = ddlstore.SelectedItem.Text;
        string trc = "";
        lblgcust.Text = "All";
        if (ddlparty.SelectedIndex > 0)
        {
            lblgcust.Text = ddlparty.SelectedItem.Text;
            trc += " and JobMaster.PartyId='" + ddlparty.SelectedValue + "'";
        }
        lblemp.Text = "All";
        if (ddlemployee.SelectedIndex > 0)
        {
            lblemp.Text = ddlemployee.SelectedItem.Text;
            trc += " and JobEmployeeDailyTaskTbl.EmployeeId='" + ddlemployee.SelectedValue + "'";
        }
        lbljj.Text = "All";
        if (ddljob.SelectedIndex > 0)
        {
            lbljj.Text = ddljob.SelectedItem.Text;
            trc += " and JobEmployeeDailyTaskDetail.JobMasterId='" + ddljob.SelectedValue + "'";
        }
        lblgdate.Text = "All";
        if (txtissuedate.Text.Length > 0)
        {
            lblgdate.Text = txtissuedate.Text;
            trc += " and JobEmployeeDailyTaskTbl.Date>='" + txtissuedate.Text + "'and JobEmployeeDailyTaskTbl.Enddate<='" + txtissuedate.Text + "'";

        }
        string str = "select distinct EmployeeMaster.EmployeeName as Employee, Case when(Cost='') then '0' else Cost end as  Cost,Case when(Rate='' or Rate IS NULL) then '0' else Rate end as  Rate, FromToTime,Hrs,JobEmployeeDailyTaskTbl.EmployeeId, Convert(nvarchar, JobEmployeeDailyTaskTbl.Enddate,101) as Enddate, Convert(nvarchar, JobEmployeeDailyTaskTbl.Date,101) as Date,JobEmployeeDailyTaskDetail.FromTime, JobEmployeeDailyTaskDetail.Id,JobName,Party_Master.Compname from Party_Master inner join JobMaster on JobMaster.PartyId=Party_Master.PartyId inner join JobEmployeeDailyTaskDetail on JobEmployeeDailyTaskDetail.JobMasterId=JobMaster.Id inner join JobEmployeeDailyTaskTbl on JobEmployeeDailyTaskTbl.Id=JobEmployeeDailyTaskDetail.JobDailyTaskMasterId inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=JobEmployeeDailyTaskTbl.EmployeeId where JobEmployeeDailyTaskTbl.Whid='" + ddlstore.SelectedValue + "' " + trc + "   and (FromToTime IS NOT NULL and FromToTime<>'') order by JobEmployeeDailyTaskDetail.Id Desc";
   
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);


        //foreach (DataRow item in ds.Rows)
        //{
        //    DataTable dtdytable = new DataTable();
        //    dtdytable = CreateDatatableDept();
        //}


        DataView myDataView = new DataView();
        myDataView = ds.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        GridView1.DataSource = ds;

        GridView1.DataSource = myDataView;

        GridView1.DataBind();

        Decimal totcost = 0;
        decimal tothrs = 0;
        Decimal bhour = 0;
        int ac = 0;
        foreach (GridViewRow item in GridView1.Rows)
        {
            Label lblhor = (Label)item.FindControl("lblhor");
            Label lblCost = (Label)item.FindControl("lblCost");
            if (lblhor.Text.Length <= 0)
            {
                lblhor.Text = "00:00";
            }
            if (lblCost.Text.Length <= 0)
            {
                lblCost.Text = "0";
            }
            totcost += Convert.ToDecimal(lblCost.Text);
         
            string[] separbm = new string[] { ":" };
            string[] strSplitArrbm = lblhor.Text.Split(separbm, StringSplitOptions.RemoveEmptyEntries);
            bhour += ((Convert.ToDecimal(strSplitArrbm[0]) + (Convert.ToDecimal(strSplitArrbm[1]) / 60)));
            bhour = Math.Round(bhour, 2);
            ac += Convert.ToInt32(strSplitArrbm[1]);
            if (ac >= 60)
            {
                ac = ac - 60;
            }
        }
        if (GridView1.Rows.Count > 0)
        {
            GridViewRow ft = (GridViewRow)(GridView1.FooterRow);
            Label lblfoothrs = (Label)(ft.FindControl("lblfoothrs"));


            string[] sepd = new string[] { "." };
            string[] straa = bhour.ToString().Split(sepd, StringSplitOptions.RemoveEmptyEntries);
            int i111 = Convert.ToInt32(straa.Length);
            string hhh = "0";
            if (i111 >= 2)
            {
                string ad = "";
                if (ac < 10)
                {
                    ad = "0" + ac;
                }
                else
                {
                    ad = ac.ToString();
                }
                hhh = ((Convert.ToString(straa[0]) + ":" + ad));

            }
            else
            {
                hhh = (Convert.ToString(straa[0]) + ":00");

            }
            Label lblfootcost = (Label)(ft.FindControl("lblfootcost"));
            lblfoothrs.Text = hhh;
            lblfootcost.Text = totcost.ToString();
        }
    }

    protected void btnstime_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        fillgrid();
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        fillgrid();
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            Button1.Text = "Hide Printable Version";
            Button2.Visible = true;
            if (GridView1.Columns[7].Visible == true)
            {
                ViewState["edith"] = "tt";
                GridView1.Columns[7].Visible = false;
            }
            if (GridView1.Columns[8].Visible == true)
            {
                ViewState["deleteh"] = "tt";
                GridView1.Columns[8].Visible = false;
            }
            if (GridView1.Columns[9].Visible == true)
            {
                ViewState["viewm"] = "tt";
                GridView1.Columns[9].Visible = false;
            }

        }
        else
        {
            Button1.Text = "Printable Version";
            Button2.Visible = false;
            if (ViewState["edith"] != null)
            {
                GridView1.Columns[7].Visible = true;
            }
            if (ViewState["deleteh"] != null)
            {
                GridView1.Columns[8].Visible = true;
            }
            if (ViewState["viewm"] != null)
            {
                GridView1.Columns[9].Visible = true;
            }
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string te = "DailyWorkSheet.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void ddlparty_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        filljob();
    }
    protected void filljob()
    {
        string str = "";

        str = "select JobMaster.Id,JobName from JobMaster inner join JobEmployeeDailyTaskDetail on JobEmployeeDailyTaskDetail.JobMasterId=JobMaster.Id inner join JobEmployeeDailyTaskTbl on JobEmployeeDailyTaskTbl.Id=JobEmployeeDailyTaskDetail.JobDailyTaskMasterId where JobEmployeeDailyTaskTbl.Whid='" + ddlstore.SelectedValue + "' and PartyId='" + ddlparty.SelectedValue + "' and (FromToTime IS NOT NULL or FromToTime<>'') order by JobMaster.Id Desc";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            ddljob.DataSource = ds;
            ddljob.DataTextField = "JobName";
            ddljob.DataValueField = "Id";
            ddljob.DataBind();
            ddljob.Items.Insert(0, "All");
            ddljob.Items[0].Value = "0";


    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
         if (e.CommandName == "Delete")
         {
             string te = "DailyWorksheet.aspx";
             ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
  
            //ViewState["trid"] = e.CommandArgument.ToString();
            //string str = "select distinct JobEmployeeDailyTaskTbl.Id from JobEmployeeDailyTaskDetail  inner join JobEmployeeDailyTaskTbl on JobEmployeeDailyTaskTbl.Id=JobEmployeeDailyTaskDetail.JobDailyTaskMasterId where JobEmployeeDailyTaskDetail.Id='" + ViewState["trid"] + "'";

            //    SqlCommand cmd = new SqlCommand(str, con);
            //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
            //    DataTable ds = new DataTable();
            //    adp.Fill(ds);

            //    if (ds.Rows.Count > 0)
            //    {
            //        string Delsttr = "Delete  JobEmployeeDailyTaskTbl where iD='" + ds.Rows[0]["Id"] + "'";
            //        SqlCommand cmdinsert1 = new SqlCommand(Delsttr, con);
            //        if (con.State.ToString() != "Open")
            //        {
            //            con.Open();
            //        }
            //        cmdinsert1.ExecuteNonQuery();
            //        con.Close();
            //    }

            //string delstrdetail1 = "Delete JobEmployeeDailyTaskDetail where Id=" + ViewState["trid"] + " ";
            //SqlCommand cmdinsert11 = new SqlCommand(delstrdetail1, con);
            //if (con.State.ToString() != "Open")
            //{
            //    con.Open();
            //}
            //cmdinsert11.ExecuteNonQuery();
            //con.Close();


            //lblmsg.Text = "Record deleted successfully";

            //fillgrid();
        }
        else if (e.CommandName == "Edit")
        {
      
            int dk = Convert.ToInt32(e.CommandArgument);
            //string te = "ProjectTimer.aspx?jdid=" + dk;
            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
            string te = "DailyWorksheet.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
  
        }
         else if (e.CommandName == "view")
         {

             int dk = Convert.ToInt32(e.CommandArgument);
             string te = "JobProfile.aspx?jdid=" + dk;
             ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

         }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}
