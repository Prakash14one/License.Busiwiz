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


public partial class ProjectTimer : System.Web.UI.Page
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
            Pagecontrol.dypcontrol(Page, page);

            fillstore();
            if (Request.QueryString["jdid"] != null)
            {
                Rdprojecttype.SelectedIndex = 1;
            }
            fillparty();
            Rdprojecttype_SelectedIndexChanged(sender, e);
            //fillgrid();

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
        
        EventArgs e=new EventArgs();
        object sender=new object();
        ddlparty_SelectedIndexChanged(sender, e);
        
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
        string te = "JobMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        filljob();
    }
    protected void filljob()
    {
        string str = "";
        if (Rdprojecttype.SelectedValue == "0")
        {
            str = "select * from JobMaster where Id Not In(Select JobMaster.Id from JobMaster inner join JobEmployeeDailyTaskDetail on JobEmployeeDailyTaskDetail.JobMasterId=JobMaster.Id inner join JobEmployeeDailyTaskTbl on JobEmployeeDailyTaskTbl.Id=JobEmployeeDailyTaskDetail.JobDailyTaskMasterId where JobEmployeeDailyTaskTbl.Whid='" + ddlstore.SelectedValue + "' and PartyId='" + ddlparty.SelectedValue + "')  and PartyId='" + ddlparty.SelectedValue + "' order by JobMaster.Id Desc ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            ddljob.DataSource = ds;
            ddljob.DataTextField = "JobName";
            ddljob.DataValueField = "Id";
            ddljob.DataBind();
            pnljobend.Visible = false;
            pnljst.Visible = true;
        }
        else
        {
            str = "select JobEmployeeDailyTaskDetail.Id,JobName from JobMaster inner join JobEmployeeDailyTaskDetail on JobEmployeeDailyTaskDetail.JobMasterId=JobMaster.Id inner join JobEmployeeDailyTaskTbl on JobEmployeeDailyTaskTbl.Id=JobEmployeeDailyTaskDetail.JobDailyTaskMasterId where JobEmployeeDailyTaskTbl.Whid='" + ddlstore.SelectedValue + "' and PartyId='" + ddlparty.SelectedValue + "' and (FromToTime IS NULL or FromToTime='') order by JobMaster.Id Desc";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            ddljobend.DataSource = ds;
            ddljobend.DataTextField = "JobName";
            ddljobend.DataValueField = "Id";
            ddljobend.DataBind();
            pnljobend.Visible = true;
            pnljst.Visible = false;
              EventArgs e=new EventArgs();
        object sender=new object();
        ddljobend_SelectedIndexChanged(sender, e);
        }
           
    }
    protected void ddlparty_SelectedIndexChanged(object sender, EventArgs e)
    { lblmsg.Text = "";
        filljob();
        fillgrid();
    }
    protected void Rdprojecttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        if (Rdprojecttype.SelectedValue == "0")
        {
            pnlendtime.Visible = false;
            btnstime.Visible = true;
            btnetime.Visible = false;
           
            //pnlstm.Visible = true;
            reqfi.ControlToValidate = "ddljob";
        }
        else
        {
            pnlendtime.Visible = true;
            btnstime.Visible = false;
            btnetime.Visible = true;
            lblstime.Text = "";
            lbltimespend.Text = "";
            //pnlstm.Visible = false;

            reqfi.ControlToValidate = "ddljobend";
        }
        filljob();
        fillgrid();
    }
    protected void btnstime_Click(object sender, EventArgs e)
    {
        string insertdaily = "Insert Into JobEmployeeDailyTaskTbl (EmployeeId,Date,Whid,compid)values('" + Session["EmployeeId"] + "','" + DateTime.Now.ToShortDateString() + "','" + ddlstore.SelectedValue + "','" + Session["Comid"].ToString() + "')";
        SqlCommand cmddaily = new SqlCommand(insertdaily, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmddaily.ExecuteNonQuery();
        con.Close();

        string str = "select max(Id) as JobEmployeeDailyTaskTblId from JobEmployeeDailyTaskTbl where Whid='"+ddlstore.SelectedValue+"'  ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {   string st=DateTime.Now.ToShortTimeString();
        string ort = Convert.ToDateTime(st).ToString("HH:mm");
            double maxid = Convert.ToDouble(ds.Rows[0]["JobEmployeeDailyTaskTblId"].ToString());
            string Insert = "Insert into JobEmployeeDailyTaskDetail(JobDailyTaskMasterId,JobMasterId,FromTime,FromToTime,Hrs,Cost,Note) values ('" + maxid + "','" + ddljob.SelectedValue + "','" + ort + "','','','0','')";
            SqlCommand cmdinsert = new SqlCommand(Insert, con);
            con.Open();
            cmdinsert.ExecuteNonQuery();
            con.Close();
            lblmsg.Text = "Record inserted successfully";
            filljob();
            fillgrid();
        }

    }
   
    protected void btnetime_Click(object sender, EventArgs e)
    { 
        string str = "select JobEmployeeDailyTaskDetail.FromTime,Date from JobMaster inner join JobEmployeeDailyTaskDetail on JobEmployeeDailyTaskDetail.JobMasterId=JobMaster.Id inner join JobEmployeeDailyTaskTbl on JobEmployeeDailyTaskTbl.Id=JobEmployeeDailyTaskDetail.JobDailyTaskMasterId where JobEmployeeDailyTaskDetail.Id='" + ddljobend.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
    
           //string stime=  Convert.ToDateTime(ds.Rows[0]["FromTime"]).ToString("HH:mm");
           //   TimeSpan tc = new TimeSpan();
           //                             tc = TimeSpan.Parse(stime);

           // DateTime dtim =Convert.ToDateTime(ds.Rows[0]["Date"]).Add(tc);

           // string dts = DateTime.Now.Subtract(Convert.ToDateTime(dtim)).ToString();

           // string hour = Convert.ToDateTime(dts).ToString("HH:mm");
            string st = DateTime.Now.ToShortTimeString();
            string ort = Convert.ToDateTime(st).ToString("HH:mm");
          //  lbltimespend.Text = hour;
            string stime = Convert.ToDateTime(ds.Rows[0]["FromTime"]).ToString("HH:mm");
            TimeSpan tc = new TimeSpan();
            tc = TimeSpan.Parse(stime);

            DateTime dtim = Convert.ToDateTime(ds.Rows[0]["Date"]).Add(tc);

            string dts = DateTime.Now.Subtract(Convert.ToDateTime(dtim)).ToString();
            Decimal totalho = Convert.ToDecimal(System.Data.Linq.SqlClient.SqlMethods.DateDiffHour(dtim, DateTime.Now));
            Decimal totalmon = Convert.ToDecimal(System.Data.Linq.SqlClient.SqlMethods.DateDiffMinute(dtim, DateTime.Now));

            decimal totmi = totalho * 60;
            decimal orimin = totalmon - totmi;
            //string hour = Convert.ToDateTime(dts).ToString("HH:mm");
            string mi;
            if (orimin.ToString().Length < 2)
            {
                mi = "0" + orimin;
            }
            else
            {
                mi = orimin.ToString();

            }
            mi = mi.Replace("-", "");
            string ho;
            if (totalho.ToString().Length < 2)
            {
                ho = "0" + totalho;
            }
            else
            {
                ho = totalho.ToString();
            }
            string hour = ho + ":" + mi;
            lbltimespend.Text = hour;

            string rate = "0";
            decimal cost = 0;
            string[] separbm = new string[] { ":" };
            string[] strSplitArrbm = hour.Split(separbm, StringSplitOptions.RemoveEmptyEntries);

        
            decimal   bhour = ((Convert.ToDecimal(strSplitArrbm[0]) + (Convert.ToDecimal(strSplitArrbm[1]) / 60)));
            string stravgsal = " select AvgRate from EmployeeAvgSalaryMaster where Id=(select Max(Id)   from EmployeeAvgSalaryMaster where EmployeeId='"+Session["EmployeeId"]+"') ";
            SqlCommand cmdavgsal = new SqlCommand(stravgsal, con);
            SqlDataAdapter adpavgsal = new SqlDataAdapter(cmdavgsal);
            DataTable dsavgsal = new DataTable();
            adpavgsal.Fill(dsavgsal);
            if (dsavgsal.Rows.Count > 0)
            {
                if (Convert.ToString(dsavgsal.Rows[0]["AvgRate"]) != "")
                {
                    rate = Convert.ToString(dsavgsal.Rows[0]["AvgRate"]);
                    cost = Convert.ToDecimal(dsavgsal.Rows[0]["AvgRate"]) * bhour;
                    cost = Math.Round(cost, 2);
                }
            }


            

            string Insert = "Update JobEmployeeDailyTaskDetail Set FromToTime='" + ort + "',Hrs='" + hour + "',Cost='" + cost + "',Rate='" + rate + "' where JobEmployeeDailyTaskDetail.Id='" + ddljobend.SelectedValue + "' ";
        SqlCommand cmdinsert = new SqlCommand(Insert, con);
        con.Open();
        cmdinsert.ExecuteNonQuery();
        con.Close();
        string strx = "select distinct JobEmployeeDailyTaskTbl.Id from JobEmployeeDailyTaskDetail  inner join JobEmployeeDailyTaskTbl on JobEmployeeDailyTaskTbl.Id=JobEmployeeDailyTaskDetail.JobDailyTaskMasterId where JobEmployeeDailyTaskDetail.Id='" + ddljobend.SelectedValue + "'";

        SqlCommand cmdx = new SqlCommand(strx, con);
        SqlDataAdapter adpx = new SqlDataAdapter(cmdx);
        DataTable dsx = new DataTable();
        adpx.Fill(dsx);

        if (dsx.Rows.Count > 0)
        {
            string strupdate = " update JobEmployeeDailyTaskTbl set Enddate='" + DateTime.Now.ToShortDateString() + "'" +

           " where  Id='" + dsx.Rows[0]["Id"] + "'  ";

            SqlCommand cmdupdate = new SqlCommand(strupdate, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdupdate.ExecuteNonQuery();
            con.Close();
        }
                
        lblmsg.Text = "Record inserted successfully";
        filljob();
        fillgrid();
        lblstime.Text = "";
        lbltimespend.Text = "";
        }
    }
    protected void ddljobend_SelectedIndexChanged(object sender, EventArgs e)
    {
        string str = "select JobEmployeeDailyTaskDetail.FromTime,Convert(nvarchar, JobEmployeeDailyTaskTbl.Date,101) as Date from JobMaster inner join JobEmployeeDailyTaskDetail on JobEmployeeDailyTaskDetail.JobMasterId=JobMaster.Id inner join JobEmployeeDailyTaskTbl on JobEmployeeDailyTaskTbl.Id=JobEmployeeDailyTaskDetail.JobDailyTaskMasterId where JobEmployeeDailyTaskDetail.Id='" + ddljobend.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            string stime = Convert.ToDateTime(ds.Rows[0]["FromTime"]).ToString("HH:mm");
            TimeSpan tc = new TimeSpan();
            tc = TimeSpan.Parse(stime);

            DateTime dtim = Convert.ToDateTime(ds.Rows[0]["Date"]).Add(tc);

            string dts = DateTime.Now.Subtract(Convert.ToDateTime(dtim)).ToString();
            Decimal totalho = Convert.ToDecimal(System.Data.Linq.SqlClient.SqlMethods.DateDiffHour(dtim,DateTime.Now));
            Decimal totalmon = Convert.ToDecimal(System.Data.Linq.SqlClient.SqlMethods.DateDiffMinute(dtim, DateTime.Now));

            decimal totmi = totalho * 60;
            decimal orimin = totalmon - totmi;
            //string hour = Convert.ToDateTime(dts).ToString("HH:mm");
            string mi;
            if (orimin.ToString().Length < 2)
            {
                mi = "0" + orimin;
            }
            else
            {
                mi = orimin.ToString();

            }
            string ho;
            if (totalho.ToString().Length < 2)
            {
                ho = "0" + totalho;
            }
            else
            {
                ho = totalho.ToString();
            }
            string hour = ho + ":" + mi;
            lbltimespend.Text = hour;

            lblstime.Text = Convert.ToString(ds.Rows[0]["Date"])+" " + Convert.ToDateTime(ds.Rows[0]["FromTime"]).ToString("HH:mm");
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
   
    }
    protected void fillgrid()
    {
        string str = "";
        if (Rdprojecttype.SelectedValue == "0")
        {
            str = "select distinct JobMaster.Id as JobId,Case when(Rate='' or Rate IS NULL) then '0' else Rate end as  Rate, FromToTime,Hrs,Convert(nvarchar, JobEmployeeDailyTaskTbl.Enddate,101) as Enddate, Convert(nvarchar, JobEmployeeDailyTaskTbl.Date,101) as Date,JobEmployeeDailyTaskDetail.FromTime, JobEmployeeDailyTaskDetail.Id,JobName,Party_Master.Compname from Party_Master inner join JobMaster on JobMaster.PartyId=Party_Master.PartyId inner join JobEmployeeDailyTaskDetail on JobEmployeeDailyTaskDetail.JobMasterId=JobMaster.Id inner join JobEmployeeDailyTaskTbl on JobEmployeeDailyTaskTbl.Id=JobEmployeeDailyTaskDetail.JobDailyTaskMasterId where JobEmployeeDailyTaskTbl.Whid='" + ddlstore.SelectedValue + "' and JobMaster.PartyId='" + ddlparty.SelectedValue + "' and (FromToTime IS NULL or FromToTime='') order by JobEmployeeDailyTaskDetail.Id Desc";
            GridView1.Columns[3].Visible = false;
            GridView1.Columns[4].Visible = false;
            GridView1.Columns[5].Visible = false;
            GridView1.Columns[6].Visible = false;
            bbdf.Text = "List of Ongoing Projects";
        }
        else
        {
            str = "select distinct JobMaster.Id as JobId,Case when(Rate='' or Rate IS NULL) then '0' else Rate end as  Rate, FromToTime,Hrs,Convert(nvarchar, JobEmployeeDailyTaskTbl.Enddate,101) as Enddate, Convert(nvarchar, JobEmployeeDailyTaskTbl.Date,101) as Date,JobEmployeeDailyTaskDetail.FromTime, JobEmployeeDailyTaskDetail.Id,JobName,Party_Master.Compname from Party_Master inner join JobMaster on JobMaster.PartyId=Party_Master.PartyId inner join JobEmployeeDailyTaskDetail on JobEmployeeDailyTaskDetail.JobMasterId=JobMaster.Id inner join JobEmployeeDailyTaskTbl on JobEmployeeDailyTaskTbl.Id=JobEmployeeDailyTaskDetail.JobDailyTaskMasterId where JobEmployeeDailyTaskTbl.Whid='" + ddlstore.SelectedValue + "' and JobMaster.PartyId='" + ddlparty.SelectedValue + "' and (FromToTime IS NOT NULL and FromToTime<>'') order by JobEmployeeDailyTaskDetail.Id Desc";
            GridView1.Columns[3].Visible = true;
            GridView1.Columns[4].Visible = true;
            GridView1.Columns[5].Visible = true;
            GridView1.Columns[6].Visible = true;
            bbdf.Text = "Projects that have been Ended";
        }
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        //DataView myDataView = new DataView();
        //myDataView = ds.Tables[0].DefaultView;

        //if (hdnsortExp.Value != string.Empty)
        //{
        //    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        //}
        GridView1.DataSource = ds;

        //GridView1.DataSource = myDataView;

        GridView1.DataBind();

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int dk = Convert.ToInt32(GridView1.SelectedDataKey.Value);
            ViewState["Id"] = dk;
            string te = "DailyWorksheet.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
  


            GridViewEditEventArgs e12 = new GridViewEditEventArgs(GridView1.SelectedIndex);
            //GridView1_RowEditing(sender, e12);
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
       // GridView1.EditIndex = e.NewEditIndex;
       fillgrid();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgrid();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //GridView1.SelectedIndex = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        //int dk = GridView1.SelectedIndex;
     
        //Label lbljobid1 = (Label)GridView1.Rows[e.RowIndex].FindControl("lbljobid");

        //if (lbljobid1 == null)
        //{
        //     lbljobid1 = (Label)GridView1.Rows[e.RowIndex].FindControl("lbljobid1");

        //}
        //string Delsttr = "Delete  JobEmployeeDailyTaskTbl where iD='" + lbljobid1.Text + "'";
        //SqlCommand cmdinsert1 = new SqlCommand(Delsttr, con);
        //if (con.State.ToString() != "Open")
        //{
        //    con.Open();
        //}
        //cmdinsert1.ExecuteNonQuery();
        //con.Close();


        //string delstrdetail1 = "Delete JobEmployeeDailyTaskDetail where Id=" + dk + " ";
        //SqlCommand cmdinsert11 = new SqlCommand(delstrdetail1, con);
        //if (con.State.ToString() != "Open")
        //{
        //    con.Open();
        //}
        //cmdinsert11.ExecuteNonQuery();
        //con.Close();

       
        //Label1.Text = "Record deleted successfully";

        //fillgrid();
        string te = "DailyWorksheet.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
  
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int dkup = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        Label lbljobid1 = (Label)GridView1.Rows[e.RowIndex].FindControl("lbljobid1");
        TextBox txtGridInED = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtGridInED");
        TextBox lblfromtime11 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("lblfromtime11");
        TextBox txtGridInED1 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtGridInED1");
        TextBox lblfromtime1 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("lblfromtime1");
        Label lblrate1 = (Label)GridView1.Rows[e.RowIndex].FindControl("lblrate1");


        string stime = Convert.ToDateTime(lblfromtime11.Text).ToString("HH:mm");
        string etime = Convert.ToDateTime(lblfromtime1.Text).ToString("HH:mm");
        TimeSpan tc = new TimeSpan();
        tc = TimeSpan.Parse(stime);
        TimeSpan tce = new TimeSpan();
        tce = TimeSpan.Parse(etime);

        DateTime dtim = Convert.ToDateTime(txtGridInED.Text).Add(tc);
        DateTime dtnow = Convert.ToDateTime(txtGridInED1.Text).Add(tce); ;

        string dts = dtnow.Subtract(Convert.ToDateTime(dtim)).ToString();
        Decimal totalho = Convert.ToDecimal(System.Data.Linq.SqlClient.SqlMethods.DateDiffHour(dtim, dtnow));
        Decimal totalmon = Convert.ToDecimal(System.Data.Linq.SqlClient.SqlMethods.DateDiffMinute(dtim, dtnow));

        decimal totmi = totalho * 60;
        decimal orimin = totalmon - totmi;
        //string hour = Convert.ToDateTime(dts).ToString("HH:mm");
        string mi;
        if (orimin.ToString().Length < 2)
        {
            mi = "0" + orimin;
        }
        else
        {
            mi = orimin.ToString();

        }
        string ho;
        if (totalho.ToString().Length < 2)
        {
            ho = "0" + totalho;
        }
        else
        {
            ho = totalho.ToString();
        }
        string hour = ho + ":" + mi;

        string rate = "0";
        decimal cost = 0;
        string[] separbm = new string[] { ":" };
        string[] strSplitArrbm = hour.Split(separbm, StringSplitOptions.RemoveEmptyEntries);


        decimal bhour = ((Convert.ToDecimal(strSplitArrbm[0]) + (Convert.ToDecimal(strSplitArrbm[1]) / 60)));


        cost = Convert.ToDecimal(lblrate1.Text) * bhour;
                cost = Math.Round(cost, 2);



                string strupdate1 = " update JobEmployeeDailyTaskDetail set FromTime='" + lblfromtime11.Text + "', " +
                                     " FromToTime='" + lblfromtime1.Text + "',Hrs='" + hour + "',  " +
                                     "    Cost='" + cost + "' " +

                                   " where  Id='" + dkup.ToString() + "'  ";

                SqlCommand cmdupdate1 = new SqlCommand(strupdate1, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdupdate1.ExecuteNonQuery();
                con.Close();


                string str = "select distinct JobEmployeeDailyTaskTbl.Id from JobEmployeeDailyTaskDetail  inner join JobEmployeeDailyTaskTbl on JobEmployeeDailyTaskTbl.Id=JobEmployeeDailyTaskDetail.JobDailyTaskMasterId where JobEmployeeDailyTaskDetail.Id='" + dkup + "'";

                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);

                if (ds.Rows.Count > 0)
                {
                    string strupdate = " update JobEmployeeDailyTaskTbl set Date='" + txtGridInED.Text + "', " +
                    " Enddate='" + txtGridInED1.Text + "'" +

                   " where  Id='" + ds.Rows[0]["Id"] + "'  ";

                    SqlCommand cmdupdate = new SqlCommand(strupdate, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdupdate.ExecuteNonQuery();
                    con.Close();
                }
                
        Label1.Visible = true;
        Label1.Text = " Record updated successfully";
        GridView1.EditIndex = -1;
        fillgrid();

    }
    protected void btnaddpj_Click(object sender, EventArgs e)
    {
        string te = "JobMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
  
    }
}
