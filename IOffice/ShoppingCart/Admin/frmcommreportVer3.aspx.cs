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
using System.IO;
using System.Text;
using System.Data.Common;
using System.ServiceProcess;
using System.Diagnostics;
using System.Windows;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using System.Text;
using System.Net;
using System.Net.Mail;

public partial class Admin_frmcommreport : System.Web.UI.Page
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    Clscommunication clsobj = new Clscommunication();
    SqlConnection con;

    DataByCompany obj = new DataByCompany();
    DocumentCls1 clsDocument = new DocumentCls1();

    string currentmonth = System.DateTime.Now.Month.ToString();
    string currentdate = System.DateTime.Now.ToShortDateString();

    string str, today, yesterday, lastweek, lastmonth, lastquerter, lastyear;

    DataTable dtclone = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int ik = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[ik - 1].ToString();
        Session["PageUrl"] = strData;
        Session["PageName"] = page;
        Page.Title = pg.getPageTitle(page);

        Label6.Visible = false;
        if (Session["UserId"] != null)
        {
            ViewState["id"] = Session["UserId"].ToString();
        }

        today = System.DateTime.Now.ToShortDateString();
        //today = "7/22/2009";
        yesterday = DateTime.Now.AddDays(-1).ToShortDateString();
        lastweek = DateTime.Now.AddDays(-7).ToShortDateString();
        lastmonth = DateTime.Now.AddMonths(-1).ToShortDateString();
        lastquerter = DateTime.Now.AddMonths(-3).ToShortDateString();
        lastyear = DateTime.Now.AddYears(-1).ToShortDateString();



        //ddlpartytypenaem.Items.Insert(0, "--Select--");
        //ddlpartytypenaem.SelectedItem.Value = "0";
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            ViewState["sortOrder"] = "";

            pageMailAccess();

            txtfromdate.Text = DateTime.Now.ToShortDateString();
            txttodate.Text = DateTime.Now.ToShortDateString();
            PanelReminderDate.Visible = false;
            txtfromdate1.Text = DateTime.Now.ToShortDateString();
            fillstore();
            filldatebyperiod();
            //rdcrselection_SelectedIndexChanged(sender, e);
            btnsearch_Click(sender, e);
            ddlstore_SelectedIndexChanged(sender, e);
            fillusertype();
            ddlusertype_SelectedIndexChanged(sender, e);
            fillemployee();
            fillmessagetype();
            fillfullweek();
            fillfullproject();
            fillfulltask();
            fillemailid();
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

    //protected void rdcrselection_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    griedviewperiodwise.DataSource = null;
    //    griedviewperiodwise.DataBind();
    //    if (rdcrselection.SelectedItem.Text == "DateWise")
    //    {
    //        PanelReminderDate.Visible = false;
    //        //pnlpartywise.Visible = false;
    //        panelsearch.Visible = false;
    //        pandatewise.Visible = true;
    //        griedviewperiodwise.Visible = true;
    //        panperiodwise.Visible = false;

    //        pnldateby.Visible = true;
    //        PanelReminderDate.Visible = false;
    //        txtfromdate.Text = DateTime.Now.ToShortDateString();
    //        txttodate.Text = DateTime.Now.ToShortDateString();


    //        ddlrelweekgoal1.Items.Clear();
    //        ddltask1.Items.Clear();
    //        ddlproject1.Items.Clear();

    //    }
    //    else if (rdcrselection.SelectedItem.Text == "Reminder Date Wise")
    //    {
    //        PanelReminderDate.Visible = true;
    //        pnlpartywise.Visible = false;
    //        panelsearch.Visible = false;
    //        pandatewise.Visible = true;
    //        griedviewperiodwise.Visible = true;
    //        panperiodwise.Visible = false;
    //        panperiodwise.Visible = false;
    //        pnldateby.Visible = true;
    //        PanelReminderDate.Visible = false;
    //        PanelReminderDate.Visible = true;
    //        txtfromdate1.Text = DateTime.Now.ToShortDateString();
    //    }


    //    else if (rdcrselection.SelectedItem.Text == "PartyType/PartyName")
    //    {
    //        pnlpartywise.Visible = true;
    //        if (ddlpartytypenaem.Items.Count <= 0)
    //        {
    //            DataTable dt = obj.Tablemaster("SELECT distinct pp.PartyTypeId, pp.PartType + ' :' + p.Contactperson AS ds,  p.PartyID  " +
    //               "FROM Party_master as p inner join   PartytTypeMaster as pp on pp.PartyTypeId = p.PartyTypeId where p.id='" + Session["Comid"].ToString() + "'  order by ds");
    //            if (dt.Rows.Count > 0)
    //            {

    //                ddlpartytypenaem.DataSource = dt;
    //                ddlpartytypenaem.DataTextField = "ds";
    //                ddlpartytypenaem.DataValueField = "PartyID";
    //                ddlpartytypenaem.DataBind();

    //            }
    //            ddlpartytypenaem.Items.Insert(0, "--Select--");
    //            ddlpartytypenaem.SelectedItem.Value = "0";
    //        }
    //        panelsearch.Visible = false;
    //        pandatewise.Visible = false;

    //        panperiodwise.Visible = false;
    //        griedviewperiodwise.Visible = true;

    //        PanelReminderDate.Visible = false;
    //        PanelReminderDate.Visible = false;
    //        pandatewise.Visible = true;

    //    }
    //    else if (rdcrselection.SelectedItem.Text == "Search")
    //    {

    //        pandatewise.Visible = false;
    //        pnlpartywise.Visible = false;
    //        panperiodwise.Visible = false;
    //        PanelReminderDate.Visible = false;
    //        griedviewperiodwise.Visible = true;
    //        panelsearch.Visible = true;
    //        PanelReminderDate.Visible = false;
    //        pandatewise.Visible = false;
    //    }
    //}




    protected void gridviewdata_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void griedviewperiodwise_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
        }

        //else if (e.CommandName == "AddDoc")
        //{
        //    // GridView2.SelectedIndex = Convert.ToInt32(e.CommandArgument);
        //    int dk = Convert.ToInt32(e.CommandArgument);// Convert.ToInt32(GridView2.DataKeys[GridView2.SelectedIndex].Value);
        //    ViewState["Dk"] = dk;
        //    string scpt = "select DocumetAttachmentTbl.* from TableMaster inner join  DocumetAttachmentTbl on DocumetAttachmentTbl.TableMasterId=TableMaster.Id where RecordId='" + ViewState["Dk"] + "' and TableName='CommunicationDetail'";

        //    SqlDataAdapter adp58 = new SqlDataAdapter(scpt, con);
        //    DataTable ds58 = new DataTable();
        //    adp58.Fill(ds58);
        //    if (ds58.Rows.Count > 0)
        //    {
        //        grd.DataSource = ds58;
        //        grd.DataBind();
        //        ModalPopupExtender1.Show();

        //    }
        //}
        else if (e.CommandName == "Send")
        {
            //grid.SelectedIndex = 
            ////ViewState["MissionId"] = grid.SelectedDataKey.Value;

            //int index = grid.SelectedIndex;

            //Label MId = (Label)grid.Rows[index].FindControl("lblMasterId");

            int id = Convert.ToInt32(e.CommandArgument);

            if (id != 0)
            {
                ViewState["MissionId"] = Convert.ToInt32(e.CommandArgument);

                // DataTable dtcrNew11 = clsDocument.SelectOfficedocwithmissionId(Convert.ToString(ViewState["MissionId"]));
                DataTable dtcrNew11 = MainAcocount.SelectOfficedocforGeneral(Convert.ToString(ViewState["MissionId"]), 111);
                GridView1.DataSource = dtcrNew11;
                GridView1.DataBind();
                ModalPopupExtenderAddnew.Show();
            }
            else
            {
                //if (e.CommandName == "internal")
                //{
                //    int mm1 = Convert.ToInt32(e.CommandArgument);

                //    string te = "Messageview.aspx?MsgDetailId=" + mm1;
                //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
                //}

                //if (e.CommandName == "external")
                //{
                //    int mm2 = Convert.ToInt32(e.CommandArgument);

                //    string te = "Messageviewext.aspx?MsgDetailId=" + mm2;
                //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
                //}
            }

        }

        if (e.CommandName == "comm")
        {
            int mm = Convert.ToInt32(e.CommandArgument);

            string te = "CommunicationProfile.aspx?ID=" + mm;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }

        if (e.CommandName == "internal")
        {
            int mm1 = Convert.ToInt32(e.CommandArgument);

            string te = "Messageview.aspx?MsgDetailId=" + mm1;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }

        if (e.CommandName == "external")
        {
            int mm2 = Convert.ToInt32(e.CommandArgument);

            string te = "Messageviewext.aspx?MsgDetailId=" + mm2;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }


    }
    protected void griedviewperiodwise_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        griedviewperiodwise.PageIndex = e.NewPageIndex;
        btnsearch_Click(sender, e);

    }
    protected void griedviewperiodwise_RowEditing(object sender, GridViewEditEventArgs e)
    {
        griedviewperiodwise.EditIndex = e.NewEditIndex;
        btnsearch_Click(sender, e);

        DropDownList ddlPartyTypeName = (DropDownList)griedviewperiodwise.Rows[griedviewperiodwise.EditIndex].FindControl("ddlPartyTypeName");

        Label lblpartytypename = (Label)griedviewperiodwise.Rows[griedviewperiodwise.EditIndex].FindControl("Label1");

        string str1 = "SELECT     PartyTypeName, PartyTypeId FROM  PartyTypeMaster ";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        // cmd.Connection = con;
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        //da.SelectCommand = cmd;
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            //da.Fill(ds, "CountryMaster");
            ddlPartyTypeName.DataSource = dt1;
            ddlPartyTypeName.DataTextField = "PartyTypeName";
            ddlPartyTypeName.DataValueField = "PartyTypeId";
            ddlPartyTypeName.DataBind();
            ddlPartyTypeName.Items.Insert(0, "--Select--");
            ddlPartyTypeName.Items[0].Value = "0";

            ddlPartyTypeName.SelectedIndex = ddlPartyTypeName.Items.IndexOf(ddlPartyTypeName.Items.FindByValue(lblpartytypename.Text));
        }

        DropDownList ddlpartyname = (DropDownList)griedviewperiodwise.Rows[griedviewperiodwise.EditIndex].FindControl("ddlPartyName");

        Label lblpartyname = (Label)griedviewperiodwise.Rows[griedviewperiodwise.EditIndex].FindControl("Label2");

        string str11 = "SELECT     PartyId, PartyName FROM         PartyMaster";
        SqlCommand cmd11 = new SqlCommand(str11, con);
        // cmd.Connection = con;
        SqlDataAdapter da11 = new SqlDataAdapter(cmd11);
        //da.SelectCommand = cmd;
        DataTable dt11 = new DataTable();
        da11.Fill(dt11);
        if (dt11.Rows.Count > 0)
        {
            //da.Fill(ds, "CountryMaster");
            ddlpartyname.DataSource = dt11;
            ddlpartyname.DataTextField = "PartyName";
            ddlpartyname.DataValueField = "PartyId";
            ddlpartyname.DataBind();
            ddlpartyname.Items.Insert(0, "--Select--");
            ddlpartyname.Items[0].Value = "0";

            ddlpartyname.SelectedIndex = ddlpartyname.Items.IndexOf(ddlpartyname.Items.FindByValue(lblpartyname.Text));
        }

        DropDownList ddlEmployeeName = (DropDownList)griedviewperiodwise.Rows[griedviewperiodwise.EditIndex].FindControl("ddlEmployeeName");

        Label lblEmployeeName = (Label)griedviewperiodwise.Rows[griedviewperiodwise.EditIndex].FindControl("Label3");

        string str12 = "SELECT     EmployeeID, EmployeeName FROM         EmployeeMaster ";
        SqlCommand cmd12 = new SqlCommand(str12, con);
        // cmd.Connection = con;
        SqlDataAdapter da12 = new SqlDataAdapter(cmd12);
        //da.SelectCommand = cmd;
        DataTable dt12 = new DataTable();
        da12.Fill(dt12);
        if (dt12.Rows.Count > 0)
        {
            //da.Fill(ds, "CountryMaster");
            ddlEmployeeName.DataSource = dt12;
            ddlEmployeeName.DataTextField = "EmployeeName";
            ddlEmployeeName.DataValueField = "EmployeeID";
            ddlEmployeeName.DataBind();
            ddlEmployeeName.Items.Insert(0, "--Select--");
            ddlEmployeeName.Items[0].Value = "0";

            ddlEmployeeName.SelectedIndex = ddlEmployeeName.Items.IndexOf(ddlEmployeeName.Items.FindByValue(lblEmployeeName.Text));
        }


    }
    protected void griedviewperiodwise_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        int dk = Convert.ToInt32(griedviewperiodwise.DataKeys[e.RowIndex].Value);
        string Date = ((TextBox)griedviewperiodwise.Rows[e.RowIndex].FindControl("txtDate")).Text;
        string ddlPartyTypeName = ((DropDownList)griedviewperiodwise.Rows[e.RowIndex].FindControl("ddlPartyTypeName")).Text;
        string ddlPartyName = ((DropDownList)griedviewperiodwise.Rows[e.RowIndex].FindControl("ddlPartyName")).Text;
        string ddlEmployeeName = ((DropDownList)griedviewperiodwise.Rows[e.RowIndex].FindControl("ddlEmployeeName")).Text;
        string Phoneno = ((TextBox)griedviewperiodwise.Rows[e.RowIndex].FindControl("txtPhoneno")).Text;
        string Description = ((TextBox)griedviewperiodwise.Rows[e.RowIndex].FindControl("txtDescription")).Text;

        string Reminder = ((TextBox)griedviewperiodwise.Rows[e.RowIndex].FindControl("txtreminder")).Text;




        string str = "update communicationDetail set " +
            "Date='" + Convert.ToDateTime(Date).ToShortDateString() + "', " +
            "ReminderDate'" + Convert.ToDateTime(Reminder).ToShortDateString() + "'" +
            "Phoneno='" + Phoneno + "',Description = '" + Description + "', " +
            "CommWith = '" + ddlPartyName + "',CapmanPartyTypeId = ' " + ddlPartyTypeName + " '," +
            "CommFor = ' " + ddlEmployeeName + " '" +
            "where CommID ='" + dk + "' ";
        SqlCommand cmd = new SqlCommand(str, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();



        griedviewperiodwise.EditIndex = -1;
        //Fillddlcountry();
        btnsearch_Click(sender, e);

    }
    protected void griedviewperiodwise_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void griedviewperiodwise_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        griedviewperiodwise.EditIndex = -1;
        btnsearch_Click(sender, e);
    }

    protected void yes_Click(object sender, ImageClickEventArgs e)
    {

        // int dk = Convert.ToInt32(griedviewperiodwise.SelectedDataKey.Values);
        SqlCommand cmd = new SqlCommand("delete from communicationDetail where CommID='" + ViewState["Id"] + "'", con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        Label6.Visible = true;
        Label6.Text = "Record Deleted Successfully";
        btnsearch_Click(sender, e);


    }

    protected void ImageButton6_Click1(object sender, ImageClickEventArgs e)
    {

    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        string datag = "";
        griedviewperiodwise.DataSource = null;
        griedviewperiodwise.DataBind();
        griedviewperiodwise.Columns[1].Visible = false;
        lblcompany.Text = Session["Cname"].ToString();
        lblBusiness.Text = ddlstore.SelectedItem.Text;
        string datew = "";

        if (Panel2.Visible == true)
        {
            datag += " and p.PartyTypeId= '" + ddlusertype.SelectedValue + "'";
        }
        if (Panel3.Visible == true)
        {
            datag += " and m.PartyID= '" + ddlmf.SelectedValue + "'";
        }
        if (Panel4.Visible == true)
        {
            datag += " and c.CommTypeId= '" + ddlsmt.SelectedValue + "'";
        }
        if (Panel5.Visible == true)
        {
            datag += " and c.CommFor= '" + ddlmfor.SelectedValue + "'";
        }
        if (pnldivision.Visible == true)
        {
            datag += " and c.RelatedBusiness = '" + ddlrelatedbus1.SelectedValue + "'";
        }

        if (pnlweekly.Visible == true)
        {
            datag += " and c.RelatedWeekGoal = '" + ddlrelweekgoal1.SelectedValue + "'";
        }
        if (pnlproject.Visible == true)
        {
            datag += " and c.Project = '" + ddlproject1.SelectedValue + "'";
        }
        if (pnltask.Visible == true)
        {
            datag += " and c.Task = '" + ddltask1.SelectedValue + "'";
        }

        if (rddatelist.SelectedIndex == 0)
        {
            if (ddlperiodwise.SelectedItem.Text == "Today")
            {
                datew += " and  (c.date =  '" + Convert.ToDateTime(today).ToShortDateString() + "')";

            }
            else if (ddlperiodwise.SelectedItem.Text == "Yesterday")
            {
                datew += " and  (c.date between '" + Convert.ToDateTime(yesterday).ToShortDateString() + "' and  '" + Convert.ToDateTime(today).ToShortDateString() + "')";


            }
            else if (ddlperiodwise.SelectedItem.Text == "LastWeek")
            {
                datew += " and (c.date between '" + Convert.ToDateTime(lastweek).ToShortDateString() + "' and  '" + Convert.ToDateTime(today).ToShortDateString() + "')";


            }
            else if (ddlperiodwise.SelectedItem.Text == "LastMonth")
            {
                datew += " and  (c.date between '" + Convert.ToDateTime(lastmonth).ToShortDateString() + "' and  '" + Convert.ToDateTime(today).ToShortDateString() + "')";


            }
            else if (ddlperiodwise.SelectedItem.Text == "LastQuerter")
            {
                datew += " and  (c.date between '" + Convert.ToDateTime(lastquerter).ToShortDateString() + "' and  '" + Convert.ToDateTime(today).ToShortDateString() + "')";


            }
            else if (ddlperiodwise.SelectedItem.Text == "LastYear")
            {
                datew += " and  (c.date between '" + Convert.ToDateTime(lastyear).ToShortDateString() + "' and  '" + Convert.ToDateTime(today).ToShortDateString() + "')";


            }
        }
        else if (rddatelist.SelectedIndex == 1)
        {
            datew += " and (c.date between '" + Convert.ToDateTime(txtfromdate.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txttodate.Text).ToShortDateString() + "')";
        }


        str = "SELECT distinct  Convert(nvarchar(10),c.Date,101) as Date,c.Flag as MsgID,CASE WHEN (c.Flag='0')THEN '' ELSE c.Flag END as Email,w.Title as week,p1.ProjectName as Project,t.TaskName as Task,Convert(nvarchar(10),c.ReminderDate,101)As ReminderDate,c.Time, p.PartType as PartyTypeName, LEFT(p.PartType,4) + ' : ' + LEFT(m.Compname,10) + ' : ' + LEFT(m.Contactperson,10) as PartyName, e.EmployeeName, " +
                       " c.Phoneno,LEFT(c.Description,90) as Description, c.CommID,c.CommID as DocumentId, SmallMessageType.Smallmesstype, c.RelatedBusiness " +
                       " FROM  PartytTypeMaster AS p " +
                       " INNER JOIN  Party_master AS m  ON p.PartyTypeId = m.PartyTypeId " +
                       " INNER JOIN CommunicationDetail AS c ON c.CommWith = m.PartyID " +
                       " Left Outer JOIN WMaster AS w ON w.MasterId = c.RelatedWeekGoal " +
                       " Left Outer JOIN ProjectMaster AS p1 ON p1.ProjectId = c.Project " +
                       " Left Outer JOIN TaskMaster AS t ON t.Taskid = c.Task " +
                       " INNER JOIN EmployeeMaster AS e ON c.CommFor = e.EmployeeMasterID " +
                       " INNER JOIN SmallMessageType ON c.CommTypeId = SmallMessageType.SmallmesstypeId " +
                        " WHERE m.Whid='" + ddlstore.SelectedValue + "'";


        if (pnldateby.Visible == true || panperiodwise.Visible == true)
        {
            str += datag + datew;
            //" order by Convert(nvarchar(10),c.Date,101) asc"
            if (rddatelist.SelectedIndex == 1)
            {
                lblreport.Text = "Communnication Report by Date - " + txtfromdate.Text + " To " + txttodate.Text;
            }
            else
            {
                //lblreport.Text = "Communnication Report by Period " + ddlperiodwise.SelectedItem.Text;
            }

        }
        if (PanelReminderDate.Visible == true)
        {
            str += datag + datew + " and (c.ReminderDate ='" + Convert.ToDateTime(txtfromdate1.Text).ToShortDateString() + "')";
           // griedviewperiodwise.Columns[1].Visible = true;

            //lblreport.Text = "Communnication Report by Reminder Date - " + txtfromdate1.Text;

        }
        if (Panel3.Visible == true)
        {
            str += datag + datew + " and m.PartyId='" + ddlmf.SelectedValue + "'";

        }
        if (panelsearch.Visible == true)
        {
            str += datag + " and (((e.EmployeeName  like '%" + txtsearch.Text.Replace("'", "''") + "%') or (m.Compname  like '%" + txtsearch.Text.Replace("'", "''") + "%') or(c.Description like '%" + txtsearch.Text + "%' ) or (m.Email like '%" + txtsearch.Text.Replace("'", "''") + "%' )) and m.id='" + Session["Comid"].ToString() + "')";
            //lblreport.Text = "Communnication Report by Party Type-Name/Phone No/Communication Description";
        }

        str += " order by Convert(nvarchar(10),c.Date,101) desc";

        lblcrepo.Text = lblreport.Text;

        DataTable ds = new DataTable();

        if (panelemail.Visible == true)
        {

        }
        else
        {
            SqlDataAdapter adp = new SqlDataAdapter(str, con);
            ds = new DataTable();
            adp.Fill(ds);
        }


        DataTable dsor = new DataTable();
        dsor = CreateDatatable();

        if (ds.Rows.Count > 0 || panelemail.Visible == true || Panel4.Visible == true)
        {

            for (int rowindex = 0; rowindex < ds.Rows.Count; rowindex++)
            {

                string sg = "Select COUNT(DocumentId) as DocumentCount  from OfficeManagerDocuments where CommunicationId='" + Convert.ToString(ds.Rows[rowindex]["CommID"]) + "'";
                SqlDataAdapter sd = new SqlDataAdapter(sg, con);
                DataTable dtcrNew11 = new DataTable();
                sd.Fill(dtcrNew11);

                if (dtcrNew11.Rows.Count > 0)
                {
                    ds.Rows[rowindex]["DocumentId"] = dtcrNew11.Rows[0]["DocumentCount"];

                }

                DataRow Drow = dsor.NewRow();
                Drow["CommID"] = Convert.ToString(ds.Rows[rowindex]["CommID"]);
                Drow["ReminderDate"] = Convert.ToString(ds.Rows[rowindex]["ReminderDate"]);
                Drow["PartyTypeName"] = Convert.ToString(ds.Rows[rowindex]["PartyTypeName"]);

                Drow["PartyName"] = Convert.ToString(ds.Rows[rowindex]["PartyName"]);
                Drow["Smallmesstype"] = Convert.ToString(ds.Rows[rowindex]["Smallmesstype"]);
                Drow["Description"] = Convert.ToString(ds.Rows[rowindex]["Description"]);

                Drow["Phoneno"] = Convert.ToString(ds.Rows[rowindex]["Phoneno"]);
                Drow["EmployeeName"] = Convert.ToString(ds.Rows[rowindex]["EmployeeName"]);
                Drow["DocumentId"] = Convert.ToString(ds.Rows[rowindex]["DocumentId"]);

                Drow["Date"] = Convert.ToString(ds.Rows[rowindex]["Date"]);

                Drow["week"] = Convert.ToString(ds.Rows[rowindex]["week"]);
                Drow["Project"] = Convert.ToString(ds.Rows[rowindex]["Project"]);
                Drow["Task"] = Convert.ToString(ds.Rows[rowindex]["Task"]);
                Drow["Email"] = "";


                dsor.Rows.Add(Drow);
            }


            //message data
            string messdataw = "";
            string md = "";

            if (rddatelist.SelectedIndex == 0)
            {
                if (ddlperiodwise.SelectedItem.Text == "Today")
                {
                    messdataw += " and  (c.MsgDate =  '" + Convert.ToDateTime(today).ToShortDateString() + "')";

                }
                else if (ddlperiodwise.SelectedItem.Text == "Yesterday")
                {
                    messdataw += " and  (c.MsgDate between '" + Convert.ToDateTime(yesterday).ToShortDateString() + "' and  '" + Convert.ToDateTime(today).ToShortDateString() + "')";


                }
                else if (ddlperiodwise.SelectedItem.Text == "LastWeek")
                {
                    messdataw += " and (c.MsgDate between '" + Convert.ToDateTime(lastweek).ToShortDateString() + "' and  '" + Convert.ToDateTime(today).ToShortDateString() + "')";


                }
                else if (ddlperiodwise.SelectedItem.Text == "LastMonth")
                {
                    messdataw += " and  (c.MsgDate between '" + Convert.ToDateTime(lastmonth).ToShortDateString() + "' and  '" + Convert.ToDateTime(today).ToShortDateString() + "')";


                }
                else if (ddlperiodwise.SelectedItem.Text == "LastQuerter")
                {
                    messdataw += " and  (c.MsgDate between '" + Convert.ToDateTime(lastquerter).ToShortDateString() + "' and  '" + Convert.ToDateTime(today).ToShortDateString() + "')";


                }
                else if (ddlperiodwise.SelectedItem.Text == "LastYear")
                {
                    messdataw += " and  (c.MsgDate between '" + Convert.ToDateTime(lastyear).ToShortDateString() + "' and  '" + Convert.ToDateTime(today).ToShortDateString() + "')";


                }
            }
            else if (rddatelist.SelectedIndex == 1)
            {
                messdataw += " and (c.MsgDate between '" + Convert.ToDateTime(txtfromdate.Text).ToShortDateString() + "' and  '" + Convert.ToDateTime(txttodate.Text).ToShortDateString() + "')";
            }


            if (Panel2.Visible == true)
            {
                md += " and p.PartyTypeId= '" + ddlusertype.SelectedValue + "'";
            }
            if (Panel3.Visible == true)
            {
                md += " and m.PartyID= '" + ddlmf.SelectedValue + "'";
            }
            //if (Panel4.Visible == true)
            //{
            //    md += " and c.CommTypeId= '" + ddlsmt.SelectedValue + "'";
            //}
            if (Panel5.Visible == true)
            {
                SqlDataAdapter da = new SqlDataAdapter("select employeename from employeemaster where employeemasterid='" + ddlmfor.SelectedValue + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                md += " and e.EmployeeName= '" + dt.Rows[0]["employeename"].ToString() + "'";
            }

            if (pnldateby.Visible == true || panperiodwise.Visible == true)
            {
                md += messdataw + "";

            }
            if (PanelReminderDate.Visible == true)
            {
                md += messdataw + " and (c.MsgDate ='" + Convert.ToDateTime(txtfromdate1.Text).ToShortDateString() + "')";


            }
            if (Panel3.Visible == true)
            {
                md += messdataw + " and m.PartyId='" + ddlmf.SelectedValue + "'";

            }
            if (panelsearch.Visible == true)
            {
                md += " and (((e.EmployeeName  like '%" + txtsearch.Text.Replace("'", "''") + "%') or (m.Compname  like '%" + txtsearch.Text.Replace("'", "''") + "%') or(c.MsgSubject +' : '+ c.MsgDetail like '%" + txtsearch.Text + "%' ) or (m.Email like '%" + txtsearch.Text.Replace("'", "''") + "%' )) and m.id='" + Session["Comid"].ToString() + "')";

            }

            if (panelemail.Visible == true)
            {
                md += " and m.Email='" + ddlemailid.SelectedItem.Text + "'";
            }

            string smms = "sELECT distinct  Convert(nvarchar(10),c.MsgDate,101) as Date,md.MsgDetailId,p.PartType as PartyTypeName,  m.CompName as PartyName, e.EmployeeName, m.Phoneno as Phoneno,'Sub' + ' - ' + c.MsgSubject +' , '+ 'Detail' + ' - ' + c.MsgDetail as Description FROM  PartytTypeMaster AS p INNER JOIN Party_master AS m ON p.PartyTypeId = m.PartyTypeId INNER JOIN MsgMaster AS c ON c.FromPartyId = m.PartyID INNER JOIN MsgDetail as md on md.MsgId=c.MsgId INNER JOIN Party_master AS tp ON tp.PartyID=md.ToPartyId INNER JOIN EmployeeMaster AS e ON e.PartyId = m.PartyID WHERE m.Whid='" + ddlstore.SelectedValue + "' " + md + "   order by Convert(nvarchar(10),c.MsgDate,101) desc";
            SqlDataAdapter sdms = new SqlDataAdapter(smms, con);
            DataTable dtcms = new DataTable();
            sdms.Fill(dtcms);

            if (pnlweekly.Visible == true || pnltask.Visible == true || pnlproject.Visible == true || pnldivision.Visible == true)
            {

            }
            else
            {
                if (Panel4.Visible == true)
                {
                    if (ddlsmt.SelectedItem.Text == "Internal Message")
                    {
                        foreach (DataRow dsc in dtcms.Rows)
                        {
                            DataRow Drow = dsor.NewRow();
                            Drow["CommID"] = "0";
                            Drow["MsgID"] = Convert.ToString(dsc["MsgDetailId"]);
                            //Convert.ToString(dsc["CommID"])
                            Drow["ReminderDate"] = "";
                            Drow["PartyTypeName"] = Convert.ToString(dsc["PartyTypeName"]);

                            Drow["PartyName"] = Convert.ToString(dsc["PartyName"]);
                            Drow["Smallmesstype"] = "Internal Message";
                            Drow["Description"] = Convert.ToString(dsc["Description"]);

                            Drow["week"] = "";
                            Drow["Project"] = "";
                            Drow["Task"] = "";
                            Drow["Email"] = "";

                            Drow["Phoneno"] = Convert.ToString(dsc["Phoneno"]);
                            Drow["EmployeeName"] = Convert.ToString(dsc["EmployeeName"]);
                            Drow["DocumentId"] = "0";

                            Drow["Date"] = Convert.ToString(dsc["Date"]);
                            dsor.Rows.Add(Drow);
                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    foreach (DataRow dsc in dtcms.Rows)
                    {
                        DataRow Drow = dsor.NewRow();
                        Drow["CommID"] = "0";
                        Drow["MsgID"] = Convert.ToString(dsc["MsgDetailId"]);
                        //Convert.ToString(dsc["CommID"])
                        Drow["ReminderDate"] = "";
                        Drow["PartyTypeName"] = Convert.ToString(dsc["PartyTypeName"]);

                        Drow["PartyName"] = Convert.ToString(dsc["PartyName"]);
                        Drow["Smallmesstype"] = "Internal Message";
                        Drow["Description"] = Convert.ToString(dsc["Description"]);

                        Drow["week"] = "";
                        Drow["Project"] = "";
                        Drow["Task"] = "";
                        Drow["Email"] = "";

                        Drow["Phoneno"] = Convert.ToString(dsc["Phoneno"]);
                        Drow["EmployeeName"] = Convert.ToString(dsc["EmployeeName"]);
                        Drow["DocumentId"] = "0";

                        Drow["Date"] = Convert.ToString(dsc["Date"]);
                        dsor.Rows.Add(Drow);
                    }
                }
            }
            string smmse = "sELECT distinct Convert(nvarchar(10),c.MsgDate,101) as Date,md.MsgDetailId,CASE WHEN (c.FromEmail IS NULL)THEN '' ELSE c.FromEmail END as Email,p.PartType as PartyTypeName,  m.CompName as PartyName, e.EmployeeName, m.Phoneno as Phoneno,'Sub' + ' - ' + c.MsgSubject +' , '+ 'Detail' + ' - ' + LEFT(MsgBodyExt.MsgDetail,70) as Description FROM  PartytTypeMaster AS p INNER JOIN Party_master AS m ON p.PartyTypeId = m.PartyTypeId INNER JOIN MsgMasterExt AS c ON c.FromPartyId = m.PartyID INNER JOIN MsgDetailExt as md on md.MsgId=c.MsgId INNER JOIN MsgBodyExt on MsgBodyExt.MsgId=c.MsgId INNER JOIN Party_master AS tp ON tp.PartyID=md.ToPartyId INNER JOIN EmployeeMaster AS e ON e.PartyId = m.PartyID WHERE m.Whid='" + ddlstore.SelectedValue + "' " + md + "   order by Convert(nvarchar(10),c.MsgDate,101) desc";
            SqlDataAdapter sdmse = new SqlDataAdapter(smmse, con);
            DataTable dtcmse = new DataTable();
            sdmse.Fill(dtcmse);

            if (pnlweekly.Visible == true || pnltask.Visible == true || pnlproject.Visible == true || pnldivision.Visible == true)
            {

            }
            else
            {
                if (Panel4.Visible == true)
                {
                    if (ddlsmt.SelectedItem.Text == "External Message")
                    {

                        foreach (DataRow dsc in dtcmse.Rows)
                        {
                            DataRow Drow = dsor.NewRow();
                            Drow["CommID"] = "0";
                            Drow["MsgIDS"] = Convert.ToString(dsc["MsgDetailId"]);
                            Drow["ReminderDate"] = "";
                            Drow["PartyTypeName"] = Convert.ToString(dsc["PartyTypeName"]);

                            Drow["PartyName"] = Convert.ToString(dsc["PartyName"]);
                            Drow["Smallmesstype"] = "External Message";
                            Drow["Description"] = Convert.ToString(dsc["Description"]);

                            Drow["week"] = "";
                            Drow["Project"] = "";
                            Drow["Task"] = "";
                            Drow["Email"] = Convert.ToString(dsc["Email"]);

                            Drow["Phoneno"] = Convert.ToString(dsc["Phoneno"]);
                            Drow["EmployeeName"] = Convert.ToString(dsc["EmployeeName"]);
                            Drow["DocumentId"] = "0";

                            Drow["Date"] = Convert.ToString(dsc["Date"]);
                            dsor.Rows.Add(Drow);
                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    foreach (DataRow dsc in dtcmse.Rows)
                    {
                        DataRow Drow = dsor.NewRow();
                        Drow["CommID"] = "0";
                        Drow["MsgIDS"] = Convert.ToString(dsc["MsgDetailId"]);
                        Drow["ReminderDate"] = "";
                        Drow["PartyTypeName"] = Convert.ToString(dsc["PartyTypeName"]);

                        Drow["PartyName"] = Convert.ToString(dsc["PartyName"]);
                        Drow["Smallmesstype"] = "External Message";
                        Drow["Description"] = Convert.ToString(dsc["Description"]);

                        Drow["week"] = "";
                        Drow["Project"] = "";
                        Drow["Task"] = "";
                        Drow["Email"] = Convert.ToString(dsc["Email"]);

                        Drow["Phoneno"] = Convert.ToString(dsc["Phoneno"]);
                        Drow["EmployeeName"] = Convert.ToString(dsc["EmployeeName"]);
                        Drow["DocumentId"] = "0";

                        Drow["Date"] = Convert.ToString(dsc["Date"]);
                        dsor.Rows.Add(Drow);
                    }
                }
            }

            DataView myDataView = new DataView();
            myDataView = dsor.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

            griedviewperiodwise.Visible = true;
            griedviewperiodwise.DataSource = myDataView;
            griedviewperiodwise.DataBind();


            foreach (GridViewRow gdrr in griedviewperiodwise.Rows)
            {
                Label lblSmallmesstype = (Label)gdrr.FindControl("lblSmallmesstype");
                LinkButton LinkButton2 = (LinkButton)gdrr.FindControl("LinkButton2");
                LinkButton LinkButton3 = (LinkButton)gdrr.FindControl("LinkButton3");
                LinkButton LinkButton4 = (LinkButton)gdrr.FindControl("LinkButton4");

                LinkButton LinkButton1 = (LinkButton)gdrr.FindControl("LinkButton1");
                LinkButton LinkButton5 = (LinkButton)gdrr.FindControl("LinkButton5");
                LinkButton LinkButton6 = (LinkButton)gdrr.FindControl("LinkButton6");

                if (lblSmallmesstype.Text == "Internal Message")
                {
                    LinkButton3.Visible = true;
                    LinkButton2.Visible = false;
                    LinkButton4.Visible = false;

                    LinkButton1.Visible = false;
                    LinkButton5.Visible = true;
                    LinkButton6.Visible = false;
                }
                else if (lblSmallmesstype.Text == "External Message")
                {
                    LinkButton3.Visible = false;
                    LinkButton2.Visible = false;
                    LinkButton4.Visible = true;

                    LinkButton1.Visible = false;
                    LinkButton5.Visible = false;
                    LinkButton6.Visible = true;
                }
                else
                {
                    LinkButton2.Visible = true;
                    LinkButton3.Visible = false;
                    LinkButton4.Visible = false;

                    LinkButton1.Visible = true;
                    LinkButton5.Visible = false;
                    LinkButton6.Visible = false;
                }
            }

        }
    }

    public DataTable CreateDatatable()
    {
        DataTable dt = new DataTable();
        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "CommID";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "Date";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;



        DataColumn Dcom2 = new DataColumn();
        Dcom2.DataType = System.Type.GetType("System.String");
        Dcom2.ColumnName = "ReminderDate";
        Dcom2.AllowDBNull = true;
        Dcom2.Unique = false;
        Dcom2.ReadOnly = false;
        DataColumn Dcom3 = new DataColumn();
        Dcom3.DataType = System.Type.GetType("System.String");
        Dcom3.ColumnName = "PartyTypeName";
        Dcom3.AllowDBNull = true;
        Dcom3.Unique = false;
        Dcom3.ReadOnly = false;

        DataColumn Dcom4 = new DataColumn();
        Dcom4.DataType = System.Type.GetType("System.String");
        Dcom4.ColumnName = "PartyName";
        Dcom4.AllowDBNull = true;
        Dcom4.Unique = false;
        Dcom4.ReadOnly = false;


        DataColumn Dcom5 = new DataColumn();
        Dcom5.DataType = System.Type.GetType("System.String");
        Dcom5.ColumnName = "Smallmesstype";
        Dcom5.AllowDBNull = true;
        Dcom5.Unique = false;
        Dcom5.ReadOnly = false;


        DataColumn Dcom6 = new DataColumn();
        Dcom6.DataType = System.Type.GetType("System.String");
        Dcom6.ColumnName = "Description";
        Dcom6.AllowDBNull = true;
        Dcom6.Unique = false;
        Dcom6.ReadOnly = false;

        DataColumn Dcom7 = new DataColumn();
        Dcom7.DataType = System.Type.GetType("System.String");
        Dcom7.ColumnName = "Phoneno";
        Dcom7.AllowDBNull = true;
        Dcom7.Unique = false;
        Dcom7.ReadOnly = false;

        DataColumn Dcom8 = new DataColumn();
        Dcom8.DataType = System.Type.GetType("System.String");
        Dcom8.ColumnName = "EmployeeName";
        Dcom8.AllowDBNull = true;
        Dcom8.Unique = false;
        Dcom8.ReadOnly = false;


        DataColumn Dcom9 = new DataColumn();
        Dcom9.DataType = System.Type.GetType("System.String");
        Dcom9.ColumnName = "DocumentId";
        Dcom9.AllowDBNull = true;
        Dcom9.Unique = false;
        Dcom9.ReadOnly = false;

        DataColumn Dcom10 = new DataColumn();
        Dcom10.DataType = System.Type.GetType("System.String");
        Dcom10.ColumnName = "Week";
        Dcom10.AllowDBNull = true;
        Dcom10.Unique = false;
        Dcom10.ReadOnly = false;

        DataColumn Dcom11 = new DataColumn();
        Dcom11.DataType = System.Type.GetType("System.String");
        Dcom11.ColumnName = "Project";
        Dcom11.AllowDBNull = true;
        Dcom11.Unique = false;
        Dcom11.ReadOnly = false;

        DataColumn Dcom12 = new DataColumn();
        Dcom12.DataType = System.Type.GetType("System.String");
        Dcom12.ColumnName = "Task";
        Dcom12.AllowDBNull = true;
        Dcom12.Unique = false;
        Dcom12.ReadOnly = false;

        DataColumn Dcom13 = new DataColumn();
        Dcom13.DataType = System.Type.GetType("System.String");
        Dcom13.ColumnName = "Email";
        Dcom13.AllowDBNull = true;
        Dcom13.Unique = false;
        Dcom13.ReadOnly = false;

        DataColumn Dcom14 = new DataColumn();
        Dcom14.DataType = System.Type.GetType("System.String");
        Dcom14.ColumnName = "MsgID";
        Dcom14.AllowDBNull = true;
        Dcom14.Unique = false;
        Dcom14.ReadOnly = false;

        DataColumn Dcom15 = new DataColumn();
        Dcom15.DataType = System.Type.GetType("System.String");
        Dcom15.ColumnName = "MsgIDS";
        Dcom15.AllowDBNull = true;
        Dcom15.Unique = false;
        Dcom15.ReadOnly = false;

        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        dt.Columns.Add(Dcom2);
        dt.Columns.Add(Dcom3);
        dt.Columns.Add(Dcom4);
        dt.Columns.Add(Dcom5);
        dt.Columns.Add(Dcom6);
        dt.Columns.Add(Dcom7);
        dt.Columns.Add(Dcom8);
        dt.Columns.Add(Dcom9);
        dt.Columns.Add(Dcom10);
        dt.Columns.Add(Dcom11);
        dt.Columns.Add(Dcom12);
        dt.Columns.Add(Dcom13);
        dt.Columns.Add(Dcom14);
        dt.Columns.Add(Dcom15);

        return dt;
    }

    //protected void ddlproject1_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    if (ddlproject1.SelectedIndex > 0)
    //    {
    //        DataTable DTS = obj.Tablemaster("select TaskId,TaskName from TaskMaster Where ProjectId = '" + ddlproject1.SelectedValue + "' order by TaskName asc");

    //        if (DTS.Rows.Count > 0)
    //        {

    //            ddltask1.Items.Clear();
    //            ddltask1.DataSource = DTS;
    //            ddltask1.DataTextField = "TaskName";
    //            ddltask1.DataValueField = "TaskId";
    //            ddltask1.DataBind();
    //            ddltask1.Items.Insert(0, "---select---");
    //            ddltask1.Items[0].Value = "0";
    //        }
    //        else
    //        {
    //            ddltask1.Items.Clear();
    //            ddltask1.Items.Insert(0, "---select---");
    //            ddltask1.Items[0].Value = "0";
    //        }
    //    }


    //}


    //protected void ddlrelweekgoal1_SelectedIndexChanged(object sender, EventArgs e)
    //{


    //    ddlproject1.Items.Clear();
    //    if (ddlrelweekgoal1.SelectedIndex > 0)
    //    {
    //        DataTable DTS = obj.Tablemaster("select ProjectId, ProjectName from ProjectMaster where WTMasterId='" + ddlrelweekgoal1.SelectedValue + "' order by ProjectName");

    //        if (DTS.Rows.Count > 0)
    //        {


    //            ddlproject1.DataSource = DTS;
    //            ddlproject1.DataTextField = "ProjectName";
    //            ddlproject1.DataValueField = "ProjectId";
    //            ddlproject1.DataBind();

    //        }
    //        ddlproject1.Items.Insert(0, "---select---");
    //        ddlproject1.Items[0].Value = "0";
    //    }
    //    ddlproject1_SelectedIndexChanged(sender, e);
    //   }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            DataTable dt = new DataTable();
            dt = clsDocument.SelectDoucmentMasterByIDwithobj(Convert.ToInt32(e.CommandArgument));

            string docname = dt.Rows[0]["DocumentName"].ToString();
            string filepath = Server.MapPath("~//Account//" + Session["comid"] + "//UploadedDocuments//" + docname);
            string name = docname.Trim();
            string extension = name.Substring(name.Length - 3);
            if (Convert.ToString(extension) == "pdf")
            {
                Session["ABCDE"] = "ABCDE";

                //                    string popupScript = "<script language='javascript'>" +
                //"newWindow=window.open('ViewDocument.aspx?id='" + e.CommandArgument + ", 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')" + "</script>";
                int docid = 0;
                docid = Convert.ToInt32(e.CommandArgument);

                //                    Page.RegisterClientScriptBlock("newWindow", popupScript);
                //LinkButton lnkbtn = (LinkButton)Gridreqinfo.FindControl("LinkButton1");
                //lnkbtn.Attributes.Add("onclick", "window.open('ViewDocument.aspx?id='" + e.CommandArgument + ",, 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')");


                String temp = "ViewDocument.aspx?id=" + docid + "&Siddd=VHDS";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + temp + "');", true);


                //    Response.Redirect("ViewDocument.aspx?id=" + docid + "&Siddd=VHDS");
            }
            else
            {
                FileInfo file = new FileInfo(filepath);

                if (file.Exists)
                {
                    Response.ClearContent();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = ReturnExtension(file.Extension.ToLower());
                    Response.TransmitFile(file.FullName);

                    Response.End();

                }
            }
        }
    }
    private string ReturnExtension(string fileExtension)
    {
        switch (fileExtension)
        {
            case ".htm":
            case ".html":
            case ".log":
                return "text/HTML";
            case ".txt":
                return "text/plain";
            case ".doc":
                return "application/ms-word";
            case ".tiff":
            case ".tif":
                return "image/tiff";
            case ".asf":
                return "video/x-ms-asf";
            case ".avi":
                return "video/avi";
            case ".zip":
                return "application/zip";
            case ".xls":
            case ".csv":
                return "application/vnd.ms-excel";
            case ".gif":
                return "image/gif";
            case ".jpg":
            case "jpeg":
                return "image/jpeg";
            case ".bmp":
                return "image/bmp";
            case ".wav":
                return "audio/wav";
            case ".mp3":
                return "audio/mpeg3";
            case ".mpg":
            case "mpeg":
                return "video/mpeg";
            case ".rtf":
                return "application/rtf";
            case ".asp":
                return "text/asp";
            case ".pdf":
                return "application/pdf";
            case ".fdf":
                return "application/vnd.fdf";
            case ".ppt":
                return "application/mspowerpoint";
            case ".dwg":
                return "image/vnd.dwg";
            case ".msg":
                return "application/msoutlook";
            case ".xml":
            case ".sdxl":
                return "application/xml";
            case ".xdp":
                return "application/vnd.adobe.xdp+xml";
            default:
                return "application/octet-stream";
        }
    }
    protected void ddltask1_SelectedIndexChanged(object sender, EventArgs e)
    {

        ViewState["btn"] = "ddltask1_SelectedIndexChanged(sender, e)";

    }
    protected void griedviewperiodwise_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void fillstore()
    {
        ddlstore.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlstore.DataSource = ds;
        ddlstore.DataTextField = "Name";
        ddlstore.DataValueField = "WareHouseId";
        ddlstore.DataBind();



        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlstore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }


    }

    protected void ddlstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillbusiness();
        fillpartywithtype();
        fillemployee();
        fillfullweek();
        fillfullproject();
        fillfulltask();

        //ddlrelweekgoal1.Items.Clear();
        //if (ddlrelatedbus1.SelectedIndex > 0)
        //{
        //    DataTable DTS = obj.Tablemaster("select WMaster.MasterId,WMaster.Title from WMaster inner join  MMaster on MMaster.MasterId=WMaster.MMasterId inner join YMaster on YMaster.MasterId=MMaster.YMasterId inner join STGMaster on STGMaster.MasterId=YMaster.StgMasterId  inner join LTGMaster on LTGMaster.MasterId=STGMaster.ltgmasterid inner join  objectivemaster on LTGMaster.ObjectiveMasterId=objectivemaster.MasterId where objectivemaster.StoreId = '" + ddlstore.SelectedValue + "'  order by WMaster.Title asc");

        //    if (DTS.Rows.Count > 0)
        //    {

        //        ddlrelweekgoal1.Items.Clear();

        //        ddlrelweekgoal1.DataSource = DTS;
        //        ddlrelweekgoal1.DataTextField = "Title";
        //        ddlrelweekgoal1.DataValueField = "MasterId";

        //    }

        //    ddlrelweekgoal1.Items.Insert(0, "---select---");
        //    ddlrelweekgoal1.Items[0].Value = "0";

        //}
        //ddlrelweekgoal1_SelectedIndexChanged(sender, e);
    }
    protected void rddatelist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rddatelist.SelectedIndex == 0)
        {
            pnldateby.Visible = false;
            panperiodwise.Visible = true;
        }
        else if (rddatelist.SelectedIndex == 1)
        {
            pnldateby.Visible = true;
            panperiodwise.Visible = false;

        }
    }

    protected void chkdiv_CheckedChanged(object sender, EventArgs e)
    {
        if (chkdiv.Checked == true)
        {
            pnldivision.Visible = true;
        }
        else
        {
            pnldivision.Visible = false;
        }
    }
    protected void chkweek_CheckedChanged(object sender, EventArgs e)
    {
        if (chkweek.Checked == true)
        {
            pnlweekly.Visible = true;
        }
        else
        {
            pnlweekly.Visible = false;
        }
    }
    protected void chkproject_CheckedChanged(object sender, EventArgs e)
    {
        if (chkproject.Checked == true)
        {
            pnlproject.Visible = true;
        }
        else
        {
            pnlproject.Visible = false;
        }
    }
    protected void chktask_CheckedChanged(object sender, EventArgs e)
    {
        if (chktask.Checked == true)
        {
            pnltask.Visible = true;
        }
        else
        {
            pnltask.Visible = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Print and Export")
        {
            Button1.Text = "Hide Print and Export";
            Button7.Visible = true;

            ddlExport.Visible = true;

            griedviewperiodwise.AllowPaging = false;
            griedviewperiodwise.PageSize = 1000;
            btnsearch_Click(sender, e);

            //if (griedviewperiodwise.Columns[10].Visible == true)
            //{
            //    ViewState["viewm"] = "tt";
            //    griedviewperiodwise.Columns[10].Visible = false;
            //}
        }
        else
        {
            Button1.Text = "Printable Version";
            Button7.Visible = false;
            ddlExport.Visible = false;

            griedviewperiodwise.AllowPaging = true;
            griedviewperiodwise.PageSize = 10;
            btnsearch_Click(sender, e);

            //if (ViewState["viewm"] != null)
            //{
            //    griedviewperiodwise.Columns[10].Visible = true;
            //}
        }
    }
    protected void griedviewperiodwise_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        btnsearch_Click(sender, e);
    }

    protected void chkcommtype_CheckedChanged(object sender, EventArgs e)
    {
        if (chkcommtype.Checked == true)
        {
            Panel4.Visible = true;
        }
        else
        {
            Panel4.Visible = false;
        }
    }
    protected void chkcommname_CheckedChanged(object sender, EventArgs e)
    {
        if (chkcommname.Checked == true)
        {
            Panel5.Visible = true;
        }
        else
        {
            Panel5.Visible = false;
        }
    }
    protected void chkpartytype_CheckedChanged(object sender, EventArgs e)
    {
        if (chkpartytype.Checked == true)
        {
            Panel2.Visible = true;
        }
        else
        {
            Panel2.Visible = false;
        }
    }
    protected void chkpartyname_CheckedChanged(object sender, EventArgs e)
    {
        if (chkpartyname.Checked == true)
        {
            Panel3.Visible = true;
        }
        else
        {
            Panel3.Visible = false;
        }
    }


    protected void fillusertype()
    {
        ddlusertype.Items.Clear();

        string emprole = "select PartyTypeId,PartType from [PartytTypeMaster] where compid='" + Session["Comid"] + "' order by PartType";
        SqlCommand cmdrole = new SqlCommand(emprole, con);
        SqlDataAdapter darole = new SqlDataAdapter(cmdrole);
        DataTable dtrole = new DataTable();
        darole.Fill(dtrole);

        ddlusertype.DataSource = dtrole;
        ddlusertype.DataTextField = "PartType";
        ddlusertype.DataValueField = "PartyTypeId";
        ddlusertype.DataBind();

        ddlusertype.SelectedIndex = ddlusertype.Items.IndexOf(ddlusertype.Items.FindByText("Vendor"));
    }

    protected void fillpartywithtype()
    {
        ddlmf.Items.Clear();

        string str12 = "select  (Party_master.Compname  +':'+ Party_master.Contactperson) as Compname ,Party_master.PartyID from Party_master inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId where Party_master.Whid='" + ddlstore.SelectedValue + "' and PartytTypeMaster.PartyTypeId='" + ddlusertype.SelectedValue + "' order by Compname";
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);

        ddlmf.DataSource = ds12;
        ddlmf.DataTextField = "Compname";
        ddlmf.DataValueField = "PartyID";
        ddlmf.DataBind();

    }
    protected void ddlusertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillpartywithtype();
    }

    protected void fillbusiness()
    {
        ddlrelatedbus1.Items.Clear();

        string str = "select distinct BusinessMaster.BusinessID,DepartmentmasterMNC.Departmentname +' : '+ BusinessMaster.BusinessName  as Divisionname from BusinessMaster inner join DepartmentmasterMNC on DepartmentmasterMNC.id=BusinessMaster.DepartmentId where BusinessMaster.company_id='" + Session["Comid"].ToString() + "' and BusinessMaster.whid='" + ddlstore.SelectedValue + "' order by Divisionname";
        SqlDataAdapter daaaaa = new SqlDataAdapter(str, con);
        DataTable dtaaaa = new DataTable();
        daaaaa.Fill(dtaaaa);

        if (dtaaaa.Rows.Count > 0)
        {
            ddlrelatedbus1.DataSource = dtaaaa;
            ddlrelatedbus1.DataTextField = "Divisionname";
            ddlrelatedbus1.DataValueField = "BusinessID";
            ddlrelatedbus1.DataBind();
        }
        ddlrelatedbus1.Items.Insert(0, "-Select-");
        ddlrelatedbus1.Items[0].Value = "0";

    }

    protected void fillemployee()
    {
        ddlmfor.Items.Clear();

        string str12 = "select DepartmentmasterMNC.Departmentname+':'+EmployeeMaster.EmployeeName as EmployeeName,EmployeeMaster.EmployeeMasterID from EmployeeMaster inner join DepartmentmasterMNC on EmployeeMaster.DeptID=DepartmentmasterMNC.id where EmployeeMaster.Active='1' and EmployeeMaster.Whid='" + ddlstore.SelectedValue + "' order by EmployeeName asc ";
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);

        if (ds12.Rows.Count > 0)
        {
            ddlmfor.DataSource = ds12;
            ddlmfor.DataTextField = "EmployeeName";
            ddlmfor.DataValueField = "EmployeeMasterID";
            ddlmfor.DataBind();
        }
    }

    protected void fillmessagetype()
    {
        ddlsmt.Items.Clear();
        string str12 = "Select distinct SmallmesstypeId,Smallmesstype from SmallMessageType  where Company_id='" + Session["Comid"].ToString() + "' order by Smallmesstype asc";
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);
        if (ds12.Rows.Count > 0)
        {

            ddlsmt.DataSource = ds12;
            ddlsmt.DataTextField = "Smallmesstype";
            ddlsmt.DataValueField = "SmallmesstypeId";
            ddlsmt.DataBind();
        }
    }
    protected void chldate_CheckedChanged(object sender, EventArgs e)
    {
        if (chldate.Checked == true)
        {
            PanelReminderDate.Visible = true;
        }
        else
        {
            PanelReminderDate.Visible = false;
        }
    }
    protected void chksearch_CheckedChanged(object sender, EventArgs e)
    {
        if (chksearch.Checked == true)
        {
            panelsearch.Visible = true;
        }
        else
        {
            panelsearch.Visible = false;
        }
    }

    protected void fillfullweek()
    {
        ddlrelweekgoal1.Items.Clear();

        string str11 = "Select  'Busi' + ' : ' + WMaster.Title as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id where WMaster.BusinessID='" + ddlstore.SelectedValue + "' and month.monthid='" + currentmonth + "' and week.lastdate1>='" + currentdate + "' and WMaster.Departmentid IS NULL and WMaster.Divisionid IS NULL and WMaster.Employeeid IS NULL order by WMaster.Title asc";

        SqlDataAdapter da11 = new SqlDataAdapter(str11, con);
        DataTable dt11 = new DataTable();
        da11.Fill(dt11);

        if (dt11.Rows.Count > 0)
        {
            ddlrelweekgoal1.DataSource = dt11;
            ddlrelweekgoal1.DataTextField = "Title";
            ddlrelweekgoal1.DataValueField = "MasterId";
            ddlrelweekgoal1.DataBind();
        }

        string str12 = "select DepartmentmasterMNC.id from warehousemaster inner join DepartmentmasterMNC on warehousemaster.warehouseid=DepartmentmasterMNC.whid where warehousemaster.warehouseid='" + ddlstore.SelectedValue + "' ";
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);

        for (int rowindex = 0; rowindex < ds12.Rows.Count; rowindex++)
        {
            string str = "Select 'Dept' + ' : ' + WMaster.Title as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id where WMaster.Departmentid='" + Convert.ToString(ds12.Rows[rowindex]["id"]) + "' and month.monthid='" + currentmonth + "' and week.lastdate1>='" + currentdate + "' and WMaster.Divisionid IS NULL and WMaster.Employeeid IS NULL  order by WMaster.Title asc";

            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                ddlrelweekgoal1.DataSource = dt;
                ddlrelweekgoal1.DataTextField = "Title";
                ddlrelweekgoal1.DataValueField = "MasterId";
                ddlrelweekgoal1.DataBind();
            }
        }

        string str1222 = "select BusinessMaster.BusinessID from warehousemaster inner join BusinessMaster on warehousemaster.warehouseid=BusinessMaster.whid where warehousemaster.warehouseid='" + ddlstore.SelectedValue + "' ";
        DataTable ds1222 = new DataTable();
        SqlDataAdapter da1222 = new SqlDataAdapter(str1222, con);
        da1222.Fill(ds1222);

        for (int rowindex = 0; rowindex < ds1222.Rows.Count; rowindex++)
        {
            string str1 = "Select 'Divi' + ' : ' + WMaster.Title as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id where WMaster.Divisionid='" + Convert.ToString(ds1222.Rows[rowindex]["BusinessID"]) + "' and month.monthid='" + currentmonth + "' and week.lastdate1>='" + currentdate + "' and WMaster.Employeeid IS NULL order by WMaster.Title asc";

            SqlDataAdapter da1 = new SqlDataAdapter(str1, con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            if (dt1.Rows.Count > 0)
            {
                ddlrelweekgoal1.DataSource = dt1;
                ddlrelweekgoal1.DataTextField = "Title";
                ddlrelweekgoal1.DataValueField = "MasterId";
                ddlrelweekgoal1.DataBind();
            }
        }

        string str12223 = "select Employeemaster.employeemasterid from warehousemaster inner join Employeemaster on warehousemaster.warehouseid=Employeemaster.whid where warehousemaster.warehouseid='" + ddlstore.SelectedValue + "' ";
        DataTable ds12223 = new DataTable();
        SqlDataAdapter da12223 = new SqlDataAdapter(str12223, con);
        da12223.Fill(ds12223);

        for (int rowindex = 0; rowindex < ds12223.Rows.Count; rowindex++)
        {
            string str2 = "Select 'Empl' + ' : ' + WMaster.Title as Title,WMaster.MasterId from Week inner join month on month.id=week.mid inner join WMaster on WMaster.Week=Week.Id where WMaster.Employeeid='" + Convert.ToString(ds12223.Rows[rowindex]["employeemasterid"]) + "' and month.monthid='" + currentmonth + "' and week.lastdate1>='" + currentdate + "' order by WMaster.Title asc";

            SqlDataAdapter da2 = new SqlDataAdapter(str2, con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);

            if (dt2.Rows.Count > 0)
            {
                ddlrelweekgoal1.DataSource = dt2;
                ddlrelweekgoal1.DataTextField = "Title";
                ddlrelweekgoal1.DataValueField = "MasterId";
                ddlrelweekgoal1.DataBind();
            }
        }

    }
    protected void filldatebyperiod()
    {
        string Today, Yesterday, ThisYear;
        Today = Convert.ToString(System.DateTime.Today.ToShortDateString());
        Yesterday = Convert.ToString(System.DateTime.Today.AddDays(-1).ToShortDateString());
        ThisYear = Convert.ToString(System.DateTime.Today.Year.ToString());


        DateTime d1, d2, d3, d4, d5, d6, d7;
        DateTime weekstart, weekend;
        d1 = Convert.ToDateTime(System.DateTime.Today.ToShortDateString());
        d2 = Convert.ToDateTime(System.DateTime.Today.AddDays(-1).ToShortDateString());
        d3 = Convert.ToDateTime(System.DateTime.Today.AddDays(-2).ToShortDateString());
        d4 = Convert.ToDateTime(System.DateTime.Today.AddDays(-3).ToShortDateString());
        d5 = Convert.ToDateTime(System.DateTime.Today.AddDays(-4).ToShortDateString());
        d6 = Convert.ToDateTime(System.DateTime.Today.AddDays(-5).ToShortDateString());
        d7 = Convert.ToDateTime(System.DateTime.Today.AddDays(-6).ToShortDateString());

        string ThisWeek = (System.DateTime.Today.DayOfWeek.ToString());
        if (ThisWeek.ToString() == "Monday")
        {
            weekstart = d1;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (Convert.ToString(ThisWeek) == "Tuesday")
        {
            weekstart = d2;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Wednesday")
        {
            weekstart = d3;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Thursday")
        {
            weekstart = d4;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Friday")
        {
            weekstart = d5;
            weekend = weekstart.Date.AddDays(+6);
        }
        else if (ThisWeek.ToString() == "Saturday")
        {
            weekstart = d6;
            weekend = weekstart.Date.AddDays(+6);

        }
        else
        {
            weekstart = d7;
            weekend = weekstart.Date.AddDays(+6);
        }
        string thisweekstart = weekstart.ToShortDateString();
        ViewState["thisweekstart"] = thisweekstart;
        string thisweekend = weekend.ToShortDateString();
        ViewState["thisweekend"] = thisweekend;

    }
    protected void fillfulltask()
    {
        ddltask1.Items.Clear();

        string strr = "select employeemaster.SuprviserId from EmployeeMaster where EmployeeMaster.EmployeeMasterID='" + ddlmfor.SelectedValue + "' ";
        DataTable dtss = new DataTable();
        SqlDataAdapter dass = new SqlDataAdapter(strr, con);
        dass.Fill(dtss);

        string str12 = "select TaskMaster.TaskId,TaskMaster.TaskName from TaskMaster inner join TaskAllocationMaster on TaskAllocationMaster.TaskId=TaskMaster.TaskId where (TaskAllocationMaster.EmployeeId = '" + ddlmfor.SelectedValue + "' OR TaskAllocationMaster.EmployeeId='" + Convert.ToString(dtss.Rows[0]["SuprviserId"]) + "') and TaskAllocationMaster.TaskAllocationDate between '" + ViewState["thisweekstart"].ToString() + "' and '" + ViewState["thisweekend"].ToString() + "' order by TaskName asc";
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);
        if (ds12.Rows.Count > 0)
        {
            ddltask1.DataSource = ds12;
            ddltask1.DataTextField = "TaskName";
            ddltask1.DataValueField = "TaskId";
            ddltask1.DataBind();
        }
        ddltask1.Items.Insert(0, "-Select-");
        ddltask1.Items[0].Value = "0";
    }

    protected void fillfullproject()
    {
        ddlproject1.Items.Clear();

        string str0 = "select 'Busi' + ' : ' + ProjectMaster.ProjectName as ProjectName,ProjectMaster.ProjectId from ProjectMaster Where ProjectMaster.whid='" + ddlstore.SelectedValue + "' and ProjectMaster.Deptid='0' and ProjectMaster.Businessid='0' and ProjectMaster.Employeeid='0' and Status='Pending' Order by ProjectMaster.ProjectName";
        DataTable dt0 = new DataTable();
        SqlDataAdapter da0 = new SqlDataAdapter(str0, con);
        da0.Fill(dt0);

        if (dt0.Rows.Count > 0)
        {
            ddlproject1.DataSource = dt0;
            ddlproject1.DataTextField = "ProjectName";
            ddlproject1.DataValueField = "ProjectId";
            ddlproject1.DataBind();
        }

        string str12 = "select DepartmentmasterMNC.id from warehousemaster inner join DepartmentmasterMNC on warehousemaster.warehouseid=DepartmentmasterMNC.whid where warehousemaster.warehouseid='" + ddlstore.SelectedValue + "' ";
        DataTable ds12 = new DataTable();
        SqlDataAdapter da12 = new SqlDataAdapter(str12, con);
        da12.Fill(ds12);

        for (int rowindex = 0; rowindex < ds12.Rows.Count; rowindex++)
        {
            string str = "Select 'Dept' + ' : ' + ProjectMaster.ProjectName as ProjectName,ProjectMaster.ProjectId from ProjectMaster Where ProjectMaster.Deptid='" + Convert.ToString(ds12.Rows[rowindex]["id"]) + "' and ProjectMaster.Businessid='0' and ProjectMaster.Employeeid='0' and Status='Pending' Order by ProjectMaster.ProjectName";

            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                ddlproject1.DataSource = dt;
                ddlproject1.DataTextField = "ProjectName";
                ddlproject1.DataValueField = "ProjectId";
                ddlproject1.DataBind();
            }
        }

        string str1222 = "select BusinessMaster.BusinessID from warehousemaster inner join BusinessMaster on warehousemaster.warehouseid=BusinessMaster.whid where warehousemaster.warehouseid='" + ddlstore.SelectedValue + "' ";
        DataTable ds1222 = new DataTable();
        SqlDataAdapter da1222 = new SqlDataAdapter(str1222, con);
        da1222.Fill(ds1222);

        for (int rowindex = 0; rowindex < ds1222.Rows.Count; rowindex++)
        {
            string str1 = "Select 'Divi' + ' : ' + ProjectMaster.ProjectName as ProjectName,ProjectMaster.ProjectId from ProjectMaster Where ProjectMaster.Businessid='" + Convert.ToString(ds1222.Rows[rowindex]["BusinessID"]) + "' and ProjectMaster.Employeeid='0' and Status='Pending' Order by ProjectMaster.ProjectName";

            SqlDataAdapter da1 = new SqlDataAdapter(str1, con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            if (dt1.Rows.Count > 0)
            {
                ddlproject1.DataSource = dt1;
                ddlproject1.DataTextField = "ProjectName";
                ddlproject1.DataValueField = "ProjectId";
                ddlproject1.DataBind();
            }
        }

        string str12223 = "select Employeemaster.employeemasterid from warehousemaster inner join Employeemaster on warehousemaster.warehouseid=Employeemaster.whid where warehousemaster.warehouseid='" + ddlstore.SelectedValue + "' ";
        DataTable ds12223 = new DataTable();
        SqlDataAdapter da12223 = new SqlDataAdapter(str12223, con);
        da12223.Fill(ds12223);

        for (int rowindex = 0; rowindex < ds12223.Rows.Count; rowindex++)
        {
            string str2 = "Select 'Empl' + ' : ' + ProjectMaster.ProjectName as ProjectName,ProjectMaster.ProjectId from ProjectMaster Where ProjectMaster.Employeeid='" + Convert.ToString(ds12223.Rows[rowindex]["employeemasterid"]) + "' and Status='Pending' Order by ProjectMaster.ProjectName";

            SqlDataAdapter da2 = new SqlDataAdapter(str2, con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);

            if (dt2.Rows.Count > 0)
            {
                ddlproject1.DataSource = dt2;
                ddlproject1.DataTextField = "ProjectName";
                ddlproject1.DataValueField = "ProjectId";
                ddlproject1.DataBind();
            }
        }
    }
    protected void ddlmfor_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillfulltask();
    }
    protected void chkwwww_CheckedChanged(object sender, EventArgs e)
    {
        if (chkwwww.Checked == true)
        {
            griedviewperiodwise.Columns[7].Visible = true;
        }
        else
        {
            griedviewperiodwise.Columns[7].Visible = false;
        }
    }
    protected void chkppp_CheckedChanged(object sender, EventArgs e)
    {
        if (chkppp.Checked == true)
        {
            griedviewperiodwise.Columns[8].Visible = true;
        }
        else
        {
            griedviewperiodwise.Columns[8].Visible = false;
        }
    }
    protected void chkttt_CheckedChanged(object sender, EventArgs e)
    {
        if (chkttt.Checked == true)
        {
            griedviewperiodwise.Columns[9].Visible = true;
        }
        else
        {
            griedviewperiodwise.Columns[9].Visible = false;
        }
    }
    protected void chkeee_CheckedChanged(object sender, EventArgs e)
    {
        if (chkeee.Checked == true)
        {
            griedviewperiodwise.Columns[10].Visible = true;
        }
        else
        {
            griedviewperiodwise.Columns[10].Visible = false;
        }
    }
    protected void chkemailid_CheckedChanged(object sender, EventArgs e)
    {
        if (chkemailid.Checked == true)
        {
            panelemail.Visible = true;
        }
        else
        {
            panelemail.Visible = false;
        }
    }

    protected void fillemailid()
    {
        ddlemailid.Items.Clear();

        //string stg1 = "select ID as PartyID,InEmailID as Email from InOutCompanyEmail where whid='" + ddlstore.SelectedValue + "'";

        //SqlDataAdapter daff1 = new SqlDataAdapter(stg1, con);
        //DataTable dtff1 = new DataTable();
        //daff1.Fill(dtff1);

        //if (dtff1.Rows.Count > 0)
        //{
        //    ddlemailid.DataSource = dtff1;
        //    ddlemailid.DataTextField = "Email";
        //    ddlemailid.DataValueField = "PartyID";
        //    ddlemailid.DataBind();
        //}

        string stg = "select PartyID,Email from Party_master where whid='" + ddlstore.SelectedValue + "'";

        SqlDataAdapter daff = new SqlDataAdapter(stg, con);
        DataTable dtff = new DataTable();
        daff.Fill(dtff);

        if (dtff.Rows.Count > 0)
        {
            ddlemailid.DataSource = dtff;
            ddlemailid.DataTextField = "Email";
            ddlemailid.DataValueField = "PartyID";
            ddlemailid.DataBind();
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {

    }
    protected void ddlExport_SelectedIndexChanged(object sender, EventArgs e)
    {

        //Button1.Text = "Printable Version";
        //Button1_Click(sender, e);
        //Button7.Visible = false;

        if (ddlExport.SelectedValue == "1")
        {
            System.IO.MemoryStream msReport = new System.IO.MemoryStream();

            Document document = new Document(PageSize.A2, 0f, 0f, 30f, 30f);

            PdfWriter writer = PdfWriter.GetInstance(document, msReport);

            this.EnableViewState = false;

            Response.Charset = string.Empty;

            document.AddSubject("Export to PDF");
            document.Open();


            if (griedviewperiodwise.Rows.Count > 0)
            {
                iTextSharp.text.Table datatable = new iTextSharp.text.Table(6);
                datatable.Padding = 2;
                datatable.Spacing = 1;
                datatable.Width = 90;

                float[] headerwidths = new float[6];

                headerwidths[0] = 7;
                headerwidths[1] = 18;
                headerwidths[2] = 10;
                headerwidths[3] = 10;
                headerwidths[4] = 50;
                headerwidths[5] = 4;

                datatable.Widths = headerwidths;

                Cell cell = new Cell(new Phrase("Business Name :" + ddlstore.SelectedItem.Text, FontFactory.GetFont(FontFactory.HELVETICA, 16, Font.BOLD)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;

                cell.Colspan = 6;
                cell.Border = Rectangle.NO_BORDER;

                datatable.AddCell(cell);

                datatable.DefaultCellBorderWidth = 1;
                datatable.DefaultHorizontalAlignment = 1;

                Cell cell3 = new Cell(new Phrase("Communication Report", FontFactory.GetFont(FontFactory.HELVETICA, 16, Font.BOLD)));
                cell3.HorizontalAlignment = Element.ALIGN_CENTER;

                cell3.Colspan = 6;
                cell3.Border = Rectangle.NO_BORDER;

                datatable.AddCell(cell3);

                Cell cell1 = new Cell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLD)));
                cell1.HorizontalAlignment = Element.ALIGN_LEFT;

                cell1.Colspan = 6;
                cell1.Border = Rectangle.NO_BORDER;

                datatable.AddCell(cell1);             

                datatable.DefaultHorizontalAlignment = Element.ALIGN_LEFT;

                datatable.AddCell(new Cell(new Phrase("Comm Date", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                datatable.AddCell(new Cell(new Phrase("User Name", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                datatable.AddCell(new Cell(new Phrase("Comm Type", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                datatable.AddCell(new Cell(new Phrase("Comm By", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                datatable.AddCell(new Cell(new Phrase("Communication Description", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                datatable.AddCell(new Cell(new Phrase("Docs", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));


                for (int i = 0; i < griedviewperiodwise.Rows.Count; i++)
                {
                    Label lblDate = (Label)griedviewperiodwise.Rows[i].FindControl("lblDate");
                    Label lblPartyName = (Label)griedviewperiodwise.Rows[i].FindControl("lblPartyName");

                    Label lblSmallmesstype = (Label)griedviewperiodwise.Rows[i].FindControl("lblSmallmesstype");
                    Label lblEmployeeName = (Label)griedviewperiodwise.Rows[i].FindControl("lblEmployeeName");

                    LinkButton LinkButton1 = (LinkButton)griedviewperiodwise.Rows[i].FindControl("LinkButton1");
                    LinkButton LinkButton2 = (LinkButton)griedviewperiodwise.Rows[i].FindControl("LinkButton2");
                    LinkButton LinkButton3 = (LinkButton)griedviewperiodwise.Rows[i].FindControl("LinkButton3");
                    LinkButton LinkButton4 = (LinkButton)griedviewperiodwise.Rows[i].FindControl("LinkButton4");
                    LinkButton LinkButton5 = (LinkButton)griedviewperiodwise.Rows[i].FindControl("LinkButton5");
                    LinkButton LinkButton6 = (LinkButton)griedviewperiodwise.Rows[i].FindControl("LinkButton6");

                    datatable.AddCell(lblDate.Text);
                    datatable.AddCell(lblPartyName.Text);
                    datatable.AddCell(lblSmallmesstype.Text);
                    datatable.AddCell(lblEmployeeName.Text); 

                    if (lblSmallmesstype.Text == "Internel Message")
                    {
                        datatable.AddCell(LinkButton3.Text);
                        datatable.AddCell(LinkButton5.Text);
                    }
                    else if (lblSmallmesstype.Text == "Externel Message")
                    {
                        datatable.AddCell(LinkButton4.Text);
                        datatable.AddCell(LinkButton6.Text);
                    }
                    else
                    {
                        datatable.AddCell(LinkButton2.Text);
                        datatable.AddCell(LinkButton1.Text);                        
                    }                                                                    
                }
                document.Add(datatable);
            }

            document.Close();

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(msReport.ToArray());

            Response.End();

        }
        else if (ddlExport.SelectedValue == "2")
        {

            Response.Clear();

            Response.Buffer = true;

            Response.AddHeader("content-disposition",

            "attachment;filename=GridViewExport.xls");

            Response.Charset = "";

            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();

            HtmlTextWriter hw = new HtmlTextWriter(sw);

            if (griedviewperiodwise.Rows.Count > 0)
            {

                griedviewperiodwise.HeaderRow.Style.Add("background-color", "#FFFFFF");

                for (int i = 0; i < griedviewperiodwise.Rows.Count; i++)
                {
                    GridViewRow row = griedviewperiodwise.Rows[i];

                    row.BackColor = System.Drawing.Color.White;

                    row.Attributes.Add("class", "textmode");
                }
            }

            pnlgrid.RenderControl(hw);

            string style = @"<style> .textmode { mso-number-format:\@; } </style>";

            Response.Write(style);

            Response.Output.Write(sw.ToString());

            Response.Flush();

            Response.End();
        }
        else if (ddlExport.SelectedValue == "3")
        {
            Response.Clear();

            Response.Buffer = true;

            Response.AddHeader("content-disposition",

            "attachment;filename=GridViewExport.doc");

            Response.Charset = "";

            Response.ContentType = "application/vnd.ms-word ";

            StringWriter sw = new StringWriter();

            HtmlTextWriter hw = new HtmlTextWriter(sw);

            pnlgrid.RenderControl(hw);

            Response.Output.Write(sw.ToString());

            Response.Flush();

            Response.End();

        }
        else if (ddlExport.SelectedValue == "4")
        {

            Document document = new Document(PageSize.A2, 0f, 0f, 30f, 30f);
            string filename = "GrdM_" + System.DateTime.Today.Day + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second;
            Session["Emfile"] = filename + ".pdf";
            Session["GrdmailA"] = null;
            PdfWriter.GetInstance(document, new FileStream(HttpContext.Current.Server.MapPath("TempDoc/" + filename + ".pdf"), FileMode.Create));

            try
            {
                document.AddSubject("Export to PDF");
                document.Open();

                if (griedviewperiodwise.Rows.Count > 0)
                {
                    iTextSharp.text.Table datatable = new iTextSharp.text.Table(6);
                    datatable.Padding = 2;
                    datatable.Spacing = 1;
                    datatable.Width = 90;

                    float[] headerwidths = new float[6];

                    headerwidths[0] = 7;
                    headerwidths[1] = 18;
                    headerwidths[2] = 10;
                    headerwidths[3] = 10;
                    headerwidths[4] = 50;
                    headerwidths[5] = 4;

                    datatable.Widths = headerwidths;

                    Cell cell = new Cell(new Phrase("Business Name :" + ddlstore.SelectedItem.Text, FontFactory.GetFont(FontFactory.HELVETICA, 16, Font.BOLD)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;

                    cell.Colspan = 6;
                    cell.Border = Rectangle.NO_BORDER;

                    datatable.AddCell(cell);

                    datatable.DefaultCellBorderWidth = 1;
                    datatable.DefaultHorizontalAlignment = 1;

                    Cell cell3 = new Cell(new Phrase("Communication Report", FontFactory.GetFont(FontFactory.HELVETICA, 16, Font.BOLD)));
                    cell3.HorizontalAlignment = Element.ALIGN_CENTER;

                    cell3.Colspan = 6;
                    cell3.Border = Rectangle.NO_BORDER;

                    datatable.AddCell(cell3);

                    Cell cell1 = new Cell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLD)));
                    cell1.HorizontalAlignment = Element.ALIGN_LEFT;

                    cell1.Colspan = 6;
                    cell1.Border = Rectangle.NO_BORDER;

                    datatable.AddCell(cell1);

                    datatable.DefaultHorizontalAlignment = Element.ALIGN_LEFT;

                    datatable.AddCell(new Cell(new Phrase("Comm Date", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    datatable.AddCell(new Cell(new Phrase("User Name", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    datatable.AddCell(new Cell(new Phrase("Comm Type", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    datatable.AddCell(new Cell(new Phrase("Comm By", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    datatable.AddCell(new Cell(new Phrase("Communication Description", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                    datatable.AddCell(new Cell(new Phrase("Docs", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));


                    for (int i = 0; i < griedviewperiodwise.Rows.Count; i++)
                    {
                        Label lblDate = (Label)griedviewperiodwise.Rows[i].FindControl("lblDate");
                        Label lblPartyName = (Label)griedviewperiodwise.Rows[i].FindControl("lblPartyName");

                        Label lblSmallmesstype = (Label)griedviewperiodwise.Rows[i].FindControl("lblSmallmesstype");
                        Label lblEmployeeName = (Label)griedviewperiodwise.Rows[i].FindControl("lblEmployeeName");

                        LinkButton LinkButton1 = (LinkButton)griedviewperiodwise.Rows[i].FindControl("LinkButton1");
                        LinkButton LinkButton2 = (LinkButton)griedviewperiodwise.Rows[i].FindControl("LinkButton2");
                        LinkButton LinkButton3 = (LinkButton)griedviewperiodwise.Rows[i].FindControl("LinkButton3");
                        LinkButton LinkButton4 = (LinkButton)griedviewperiodwise.Rows[i].FindControl("LinkButton4");
                        LinkButton LinkButton5 = (LinkButton)griedviewperiodwise.Rows[i].FindControl("LinkButton5");
                        LinkButton LinkButton6 = (LinkButton)griedviewperiodwise.Rows[i].FindControl("LinkButton6");

                        datatable.AddCell(lblDate.Text);
                        datatable.AddCell(lblPartyName.Text);
                        datatable.AddCell(lblSmallmesstype.Text);
                        datatable.AddCell(lblEmployeeName.Text);

                        if (lblSmallmesstype.Text == "Internel Message")
                        {
                            datatable.AddCell(LinkButton3.Text);
                            datatable.AddCell(LinkButton5.Text);
                        }
                        else if (lblSmallmesstype.Text == "Externel Message")
                        {
                            datatable.AddCell(LinkButton4.Text);
                            datatable.AddCell(LinkButton6.Text);
                        }
                        else
                        {
                            datatable.AddCell(LinkButton2.Text);
                            datatable.AddCell(LinkButton1.Text);
                        }
                    }
                    document.Add(datatable);
                }


                document.Close();
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
            document.Close();
            string te = "MessageComposeExt.aspx?ema=Azxcvyute";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }

        //}

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void pageMailAccess()
    {
        ddlExport.Items.Insert(0, "Export Type");
        ddlExport.Items[0].Value = "0";
        ddlExport.Items.Insert(1, "Export to PDF");
        ddlExport.Items[1].Value = "1";
        ddlExport.Items.Insert(2, "Export to Excel");
        ddlExport.Items[2].Value = "2";
        ddlExport.Items.Insert(3, "Export to Word");
        ddlExport.Items[3].Value = "3";
        DataTable drt = select("SELECT distinct RoleMenuAccessRightTbl.MenuId,PageMaster.PageName FROM MainMenuMaster inner join RoleMenuAccessRightTbl on RoleMenuAccessRightTbl.MenuId=MainMenuMaster.MainMenuId inner join PageMaster on PageMaster.MainMenuId=RoleMenuAccessRightTbl.MenuId  inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.PageId  INNER JOIN  User_Role ON RoleMenuAccessRightTbl.RoleId = User_Role.Role_id INNER JOIN User_master ON User_Role.User_id = User_master.UserID where pageplaneaccesstbl.Priceplanid='" + Session["PriceId"] + "' and PageMaster.PageName='MessageCompose.aspx' and PageMaster.VersionInfoMasterId='" + Session["verId"] + "' and  User_master.UserID ='" + Session["userid"] + "'");
        if (drt.Rows.Count <= 0)
        {
            drt = select("SELECT PageMaster.PageName FROM PageMaster inner join RolePageAccessRightTbl on RolePageAccessRightTbl.PageId=PageMaster.PageId inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.PageId INNER JOIN User_Role ON RolePageAccessRightTbl.RoleId = User_Role.Role_id INNER JOIN User_master ON User_Role.User_id = User_master.UserID where pageplaneaccesstbl.Priceplanid='" + Session["PriceId"] + "' and PageMaster.PageName='MessageCompose.aspx' and PageMaster.VersionInfoMasterId='" + Session["verId"] + "' and  User_master.UserID ='" + Session["userid"] + "'");
            if (drt.Rows.Count <= 0)
            {
                drt = select("SELECT distinct PageMaster.PageName FROM MainMenuMaster inner join  SubMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join RoleSubMenuAccessRightTbl on RoleSubMenuAccessRightTbl.SubMenuId=SubMenuMaster.SubMenuId inner join PageMaster on PageMaster.SubMenuId=RoleSubMenuAccessRightTbl.SubMenuId  inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.PageId  INNER JOIN  User_Role ON RoleSubMenuAccessRightTbl.RoleId = User_Role.Role_id INNER JOIN User_master ON User_Role.User_id = User_master.UserID where pageplaneaccesstbl.Priceplanid='" + Session["PriceId"] + "' and PageMaster.PageName='MessageCompose.aspx' and PageMaster.VersionInfoMasterId='" + Session["verId"] + "' and  User_master.UserID ='" + Session["userid"] + "'");
                if (drt.Rows.Count <= 0)
                {
                    ddlExport.Items.Insert(4, "Email with PDF");
                    ddlExport.Items[4].Value = "4";
                }
                else
                {
                    ddlExport.Items.Insert(4, "Email with PDF");
                    ddlExport.Items[4].Value = "4";
                }
            }
            else
            {
                ddlExport.Items.Insert(4, "Email with PDF");
                ddlExport.Items[4].Value = "4";
            }
        }
        else
        {
            ddlExport.Items.Insert(4, "Email with PDF");
            ddlExport.Items[4].Value = "4";
        }
    }

    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        return dt;
    }
    protected void chkremin_CheckedChanged(object sender, EventArgs e)
    {
        if (chkremin.Checked == true)
        {
            griedviewperiodwise.Columns[1].Visible = true;
        }
        else
        {
            griedviewperiodwise.Columns[1].Visible = false;
        }
    }
}
