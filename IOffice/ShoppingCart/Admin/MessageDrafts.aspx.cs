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
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public partial class Account_MessageDrafts : System.Web.UI.Page
{
    MessageCls clsMessage = new MessageCls();
    MasterCls clsMaster = new MasterCls();
    DataTable dt = new DataTable();
    EmployeeCls clsEmployee = new EmployeeCls();
    SqlConnection con;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();
        Session["PageUrl"] = strData;
        Session["PageName"] = page;
        Page.Title = pg.getPageTitle(page);
        DataTable dsss = new DataTable();
        dsss = clsMessage.Empid("158");
        if (dsss.Rows.Count > 0)
        {
            Session["EmployeeIdep"] = dsss.Rows[0]["EmployeeMasterId"].ToString();
        }
        if (Session["CompanyName"] != null)
        {
            this.Title = Session["CompanyName"] + " IFileCabinet.com Draft Messages ";
        }

        Session["PageName"] = "MessageDrafts.aspx";

        pnlmsg.Visible = false;
        if (!Page.IsPostBack)
        {
            filldatebyperiod();
            fillusertype();
            fillusertype111();
            fillusername();

            //ddlperiod.SelectedValue = "3";
            SelectMsgforDraft();
        }

    }
    protected void SelectMsgforDraft()
    {
        //dt = new DataTable();
        //dt = clsMessage.SelectMsgforDraft(Convert.ToInt32(Session["PartyId"].ToString()));

        string mes1 = "";
        string mes2 = "";
        string mes3 = "";
        string mes4 = "";
        string mes5 = "";
        string mes6 = "";
        string mes7 = "";
        string mes8 = "";
        string mes9 = "";
        string mes10 = "";
        string mes11 = "";
        string mes12 = "";
        string mes13 = "";
        string mes14 = "";
        string mes15 = "";
        string mes16 = "";
        string mes17 = "";

        string ThisWeek = (System.DateTime.Today.DayOfWeek.ToString());

        if (TextBox1.Text != "")
        {
            mes1 += " and ((" + PageConn.intmsg11 + ".dbo.MsgMaster.MsgDate like '%" + TextBox1.Text.Replace("'", "''") + "%') or (" + PageConn.intmsg11 + ".dbo.MsgMaster.MsgSubject like '%" + TextBox1.Text.Replace("'", "''") + "%') or (Party_master.Compname like '%" + TextBox1.Text.Replace("'", "''") + "%'))";
        }


        if (RadioButtonList1.SelectedValue == "1")
        {
            if (ddlperiod.SelectedItem.Text == "All")
            {

                mes2 += "";
            }
            if (ddlperiod.SelectedItem.Text == "Today")
            {

                mes3 += " and cast(" + PageConn.intmsg11 + ".dbo.MsgMaster.MsgDate as Date)='" + System.DateTime.Now.ToShortDateString() + "'";
            }
            if (ddlperiod.SelectedItem.Text == "Yesterday")
            {

                mes4 += " and cast(" + PageConn.intmsg11 + ".dbo.MsgMaster.MsgDate as Date)='" + System.DateTime.Now.AddDays(-1).ToShortDateString() + "'";
            }
            if (ddlperiod.SelectedItem.Text == "This Week")
            {


                mes5 += " and " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgDate between '" + ViewState["thisweekstart"] + "' and '" + ViewState["thisweekend"] + "'";
            }

            if (ddlperiod.SelectedItem.Text == "Last Week")
            {


                mes6 += " and " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgDate between '" + ViewState["lastweekstart"] + "' and '" + ViewState["lastweekend"] + "'";
            }

            if (ddlperiod.SelectedItem.Text == "Last 2 Weeks")
            {


                mes7 += " and " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgDate between '" + ViewState["last2weekstart"] + "' and '" + ViewState["lastweekend"] + "'";
            }

            if (ddlperiod.SelectedItem.Text == "This Month")
            {


                mes8 += " and " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgDate between '" + ViewState["thismonthstartdate"] + "' and '" + ViewState["thismonthenddate"] + "'";
            }

            if (ddlperiod.SelectedItem.Text == "Last Month")
            {


                mes9 += " and " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgDate between '" + ViewState["lastmonthstartdate"] + "' and '" + ViewState["lastmonthenddate"] + "'";
            }

            if (ddlperiod.SelectedItem.Text == "Last 2 Months")
            {

                mes10 += " and " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgDate between '" + ViewState["last2monthstart"] + "' and '" + ViewState["lastmonthenddate"] + "'";
            }
            if (ddlperiod.SelectedItem.Text == "Current Year")
            {

                mes11 += " and " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgDate between '" + ViewState["thisyearstartdate"] + "' and '" + ViewState["thisyearenddate"] + "'";
            }

            if (ddlperiod.SelectedItem.Text == "Last Year")
            {

                mes12 += " and " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgDate between '" + ViewState["lastyearstartdate"] + "' and '" + ViewState["lastyearenddate"] + "'";
            }

            if (ddlperiod.SelectedItem.Text == "Last 2 Years")
            {


                mes13 += " and " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgDate between '" + ViewState["last2yearstartdate"] + "' and '" + ViewState["lastyearenddate"] + "'";
            }
        }
        if (RadioButtonList1.SelectedValue == "0")
        {
            if (txtestartdate.Text != "" && txteenddate.Text != "")
            {
                mes17 += " and " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgDate between '" + txtestartdate.Text + "' and '" + txteenddate.Text + "'";
            }

        }

        if (RadioButtonList2.SelectedValue == "0")
        {
            if (ddlusertype.SelectedIndex > 0)
            {


                mes14 += " and Party_master.PartyTypeId='" + ddlusertype.SelectedValue + "'";
            }

        }

        if (RadioButtonList2.SelectedValue == "1")
        {

            if (ddlusertype1.SelectedIndex > 0)
            {


                mes15 += " and Party_master.PartyTypeId='" + ddlusertype1.SelectedValue + "'";
            }

            if (ddlusername.SelectedIndex > 0)
            {

                mes16 += " and Party_master.PartyId='" + ddlusername.SelectedValue + "'";
            }
        }



        //  string mes = "SELECT distinct MsgMaster.MsgDate, MsgMaster.MsgSubject, MsgStatusMaster.MsgStatusName,MsgDetail.MsgStatusId,MsgMaster.FromPartyId, MsgMaster.MsgId FROM MsgDetail INNER JOIN MsgMaster ON MsgDetail.MsgId = MsgMaster.MsgId INNER JOIN MsgStatusMaster ON MsgDetail.MsgStatusId = MsgStatusMaster.MsgStatusId where FromPartyId = '" + Convert.ToInt32(Session["PartyId"].ToString()) + "' and MsgDetail.MsgStatusId in(3) " + mes1 + " " + mes2 + " " + mes3 + " " + mes4 + " " + mes5 + " " + mes6 + " " + mes7 + " " + mes8 + " " + mes9 + " " + mes10 + " " + mes11 + " " + mes12 + " " + mes13 + " " + mes14 + " " + mes15 + " " + mes16 + " " + mes17 + "  order by Msgdate desc";

        string mes = "  " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgDate, " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgSubject, " + PageConn.intmsg11 + ".dbo.MsgStatusMaster.MsgStatusName," + PageConn.intmsg11 + ".dbo.MsgDetail.MsgStatusId," + PageConn.intmsg11 + ".dbo.MsgMaster.FromPartyId, " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgId FROM " + PageConn.intmsg11 + ".dbo.MsgDetail INNER JOIN " + PageConn.intmsg11 + ".dbo.MsgMaster ON " + PageConn.intmsg11 + ".dbo.MsgDetail.MsgId = " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgId INNER JOIN " + PageConn.intmsg11 + ".dbo.MsgStatusMaster ON " + PageConn.intmsg11 + ".dbo.MsgDetail.MsgStatusId = " + PageConn.intmsg11 + ".dbo.MsgStatusMaster.MsgStatusId inner join Party_master on Party_master.PartyID=" + PageConn.intmsg11 + ".dbo.MsgMaster.FromPartyId inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId where Party_master.PartyID = '" + Convert.ToInt32(Session["PartyId"].ToString()) + "' and " + PageConn.intmsg11 + ".dbo.MsgDetail.MsgStatusId in(3) " + mes1 + " " + mes2 + " " + mes3 + " " + mes4 + " " + mes5 + " " + mes6 + " " + mes7 + " " + mes8 + " " + mes9 + " " + mes10 + " " + mes11 + " " + mes12 + " " + mes13 + " " + mes14 + " " + mes15 + " " + mes16 + " " + mes17 + "";
        //order by Msgdate desc";

        string str2 = "select count(" + PageConn.intmsg11 + ".dbo.MsgMaster.MsgId) as ci FROM " + PageConn.intmsg11 + ".dbo.MsgDetail INNER JOIN " + PageConn.intmsg11 + ".dbo.MsgMaster ON " + PageConn.intmsg11 + ".dbo.MsgDetail.MsgId = " + PageConn.intmsg11 + ".dbo.MsgMaster.MsgId INNER JOIN " + PageConn.intmsg11 + ".dbo.MsgStatusMaster ON " + PageConn.intmsg11 + ".dbo.MsgDetail.MsgStatusId = " + PageConn.intmsg11 + ".dbo.MsgStatusMaster.MsgStatusId inner join Party_master on Party_master.PartyID=" + PageConn.intmsg11 + ".dbo.MsgMaster.FromPartyId inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId where Party_master.PartyID = '" + Convert.ToInt32(Session["PartyId"].ToString()) + "' and " + PageConn.intmsg11 + ".dbo.MsgDetail.MsgStatusId in(3) " + mes1 + " " + mes2 + " " + mes3 + " " + mes4 + " " + mes5 + " " + mes6 + " " + mes7 + " " + mes8 + " " + mes9 + " " + mes10 + " " + mes11 + " " + mes12 + " " + mes13 + " " + mes14 + " " + mes15 + " " + mes16 + " " + mes17 + "";
        //order by Msgdate desc";

        gridDraft.VirtualItemCount = GetRowCount(str2);

        string sortExpression = " " + PageConn.intmsg11 + ".dbo.MsgMaster.Msgdate desc";


        //SqlDataAdapter da = new SqlDataAdapter(mes, con);
        //DataTable dt = new DataTable();
        //da.Fill(dt);

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            DataTable dt = GetDataPage(gridDraft.PageIndex, gridDraft.PageSize, sortExpression, mes);

            gridDraft.DataSource = dt;
            gridDraft.DataBind();
        }
        else
        {
            gridDraft.DataSource = null;
            gridDraft.DataBind();
        }

        if (gridDraft.Rows.Count > 0)
        {
            imgbtnsend.Visible = true;
            imgbtndiscard.Visible = true;
        }
        else
        {
            imgbtnsend.Visible = false;
            imgbtndiscard.Visible = false;

        }

        setGridisze();
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

    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }

    protected void gridDraft_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gridDraft_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridDraft.PageIndex = e.NewPageIndex;
        SelectMsgforDraft();
    }
    protected void imgbtnsend_Click(object sender, EventArgs e)
    {
        bool Gcheck = false;
        bool UpdateMsg = false;
        if (gridDraft.Rows.Count > 0)
        {
            foreach (GridViewRow GR in gridDraft.Rows)
            {
                CheckBox chk = (CheckBox)GR.FindControl("chkMsg");
                if (chk.Checked == true)
                {
                    Gcheck = true;
                    break;
                }
            }
            if (Gcheck == false)
            {
                lblmsg.Text = "Please select atleast one Draft Message to send.";
                pnlmsg.Visible = true;
                return;
            }
            else
            {
                foreach (GridViewRow GR in gridDraft.Rows)
                {
                    CheckBox chk = (CheckBox)GR.FindControl("chkMsg");
                    if (chk.Checked == true)
                    {
                        Int32 MsgId = Convert.ToInt32(gridDraft.DataKeys[GR.RowIndex].Value);
                        bool UpdateMsgMaster = clsMessage.UpdateMsgMasterforDraftSend(MsgId);
                        UpdateMsg = clsMessage.UpdateMsgDetailusingMsgId(MsgId, 1);
                    }
                }
                if (UpdateMsg == true)
                {
                    pnlmsg.Visible = true;
                    lblmsg.Visible = true;
                    lblmsg.Text = "Message Sent Successfully.";
                    SelectMsgforDraft();
                }
            }
        }
    }
    protected void imgbtndiscard_Click(object sender, EventArgs e)
    {
        bool Gcheck = false;

        if (gridDraft.Rows.Count > 0)
        {
            foreach (GridViewRow GR in gridDraft.Rows)
            {
                CheckBox chk = (CheckBox)GR.FindControl("chkMsg");
                if (chk.Checked == true)
                {
                    Gcheck = true;
                    break;
                }
            }
            if (Gcheck == false)
            {
                lblmsg.Text = "Please select atleast one Draft Message to Delete.";
                pnlmsg.Visible = true;
                return;
            }
            else
            {
                bool MsgMasterDelete = false;
                foreach (GridViewRow GR in gridDraft.Rows)
                {
                    CheckBox chk = (CheckBox)GR.FindControl("chkMsg");
                    if (chk.Checked == true)
                    {
                        Int32 MsgId = Convert.ToInt32(gridDraft.DataKeys[GR.RowIndex].Value);
                        bool MsgDetailDelete = clsMessage.DeleteMsgDetail(MsgId);
                        bool FileDelete = clsMessage.DeleteMsgFileAttachDetail(MsgId);
                        MsgMasterDelete = clsMessage.DeleteMsgMaster(MsgId);
                    }
                }
                if (MsgMasterDelete == true)
                {
                    lblmsg.Visible = true;
                    pnlmsg.Visible = true;
                    lblmsg.Text = "Message Deleted Successfully.";
                    SelectMsgforDraft();
                }
            }
        }
    }
    protected void gridDraft_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Int32 MsgId = 0;
            DataTable dtMain = new DataTable();
            MsgId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "MsgId").ToString());
            dtMain = new DataTable();
            dtMain = clsMessage.SelectMsgDetailforDraftPartyList(MsgId);
            Label sentTo = (Label)e.Row.FindControl("lblSentTo");

            if (dtMain.Rows.Count > 0)
            {
                String ToList = "";
                int i = 0;
                foreach (DataRow DR in dtMain.Rows)
                {

                    if (i >= 1)
                    {
                        ToList = ToList + " , " + DR["Compname"].ToString();
                    }
                    if (i == 0)
                    {
                        ToList = DR["Compname"].ToString();
                        i = 1;
                    }
                    if (ToList.Length > 25)
                    {
                        ToList = ToList + " ....";
                        break;
                    }
                }
                sentTo.Text = ToList.ToString();
            }
            dtMain = new DataTable();
            dtMain = clsMessage.SelectMsgforFileAttach(MsgId);
            Image img = (Image)e.Row.FindControl("ImgFile");


            if (dtMain.Rows.Count > 0)
            {
                img.ImageUrl = "~/Account/images/attach.png";
                img.Visible = true;
            }
            else
            {
                img.ImageUrl = "";
                img.Visible = false;
            }
        }
        try
        {
            if (gridDraft.Rows.Count > 0)
            {
                CheckBox cbHeader = (CheckBox)gridDraft.HeaderRow.FindControl("HeaderChkbox");
                cbHeader.Attributes["onclick"] = "ChangeAllCheckBoxStates(this.checked);";
                List<string> ArrayValues = new List<string>();
                ArrayValues.Add(string.Concat("'", cbHeader.ClientID, "'"));
                foreach (GridViewRow gvr in gridDraft.Rows)
                {
                    CheckBox cb = (CheckBox)gvr.FindControl("chkMsg");
                    cb.Attributes["onclick"] = "ChangeHeaderAsNeeded();";
                    ArrayValues.Add(string.Concat("'", cb.ClientID, "'"));
                }
                CheckBoxIDsArray.Text = "<script type='text/javascript'>" + "\n" + "<!--" + "\n" + String.Concat("var CheckBoxIDs =  new Array(", String.Join(",", ArrayValues.ToArray()), ");") + "\n // -->" + "\n" + "</script>";

            }
            else
            {
            }
        }
        catch (Exception ex)
        {
            pnlmsg.Visible = true;
            lblmsg.Text = "Error in databound : " + ex.Message.ToString();
        }
    }

    public void setGridisze()
    {
        // doc grid
        if (gridDraft.Rows.Count == 0)
        {
            Panel2.CssClass = "GridPanel20";
        }
        else if (gridDraft.Rows.Count == 1)
        {
            Panel2.CssClass = "GridPanel125";
        }
        else if (gridDraft.Rows.Count == 2)
        {
            Panel2.CssClass = "GridPanel1175";
        }
        else if (gridDraft.Rows.Count == 3)
        {
            Panel2.CssClass = "GridPanel225";
        }
        else if (gridDraft.Rows.Count == 4)
        {
            Panel2.CssClass = "GridPanel275";
        }
        else if (gridDraft.Rows.Count == 5)
        {
            Panel2.CssClass = "GridPanel325";
        }
        else if (gridDraft.Rows.Count == 6)
        {
            Panel2.CssClass = "GridPanel375";
        }
        //else if (gridDraft.Rows.Count == 7)
        //{
        //    Panel2.CssClass = "GridPanel425";
        //}
        //else if (gridDraft.Rows.Count == 8)
        //{
        //    Panel2.CssClass = "GridPanel";
        //}
        //else if (gridDraft.Rows.Count == 9)
        //{
        //    Panel2.CssClass = "GridPanel325";
        //}
        //else if (gridDraft.Rows.Count == 10)
        //{
        //    Panel2.CssClass = "GridPanel350";
        //}

        else
        {
            Panel2.CssClass = "GridPanel425";
        }


    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            Panel1.Visible = true;
        }
        if (CheckBox1.Checked == false)
        {
            Panel1.Visible = false;
        }
    }
    protected void btngo_Click(object sender, EventArgs e)
    {
        SelectMsgforDraft();
    }
    protected void chkfilter_CheckedChanged(object sender, EventArgs e)
    {
        if (chkfilter.Checked == true)
        {
            pnltype.Visible = true;
        }
        if (chkfilter.Checked == false)
        {
            pnltype.Visible = false;
        }
    }
    protected void chkdate_CheckedChanged(object sender, EventArgs e)
    {
        if (chkdate.Checked == true)
        {
            pnldate.Visible = true;
            RadioButtonList1_SelectedIndexChanged(sender, e);
        }
        if (chkdate.Checked == false)
        {
            pnldate.Visible = false;
            pnlfromto.Visible = false;
            pnlperiod.Visible = false;
        }
    }
    protected void chkuser_CheckedChanged(object sender, EventArgs e)
    {
        if (chkuser.Checked == true)
        {
            pnluser.Visible = true;
            RadioButtonList2_SelectedIndexChanged(sender, e);
        }
        if (chkuser.Checked == false)
        {
            pnluser.Visible = false;
            pnlusername.Visible = false;
            pnlusertype.Visible = false;
        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "0")
        {
            pnlfromto.Visible = true;
            pnlperiod.Visible = false;
            txteenddate.Text = System.DateTime.Now.ToShortDateString();
            txtestartdate.Text = System.DateTime.Now.ToShortDateString();
        }
        if (RadioButtonList1.SelectedValue == "1")
        {
            ddlperiod_SelectedIndexChanged(sender, e);
            pnlfromto.Visible = false;
            pnlperiod.Visible = true;
        }
    }
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList2.SelectedValue == "0")
        {
            pnlusertype.Visible = true;
            pnlusername.Visible = false;
        }
        if (RadioButtonList2.SelectedValue == "1")
        {
            pnlusertype.Visible = false;
            pnlusername.Visible = true;
        }
    }
    protected void buttongo_Click(object sender, EventArgs e)
    {
        SelectMsgforDraft();
    }
    protected void ddlperiod_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectMsgforDraft();
    }
    protected void ddlusertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectMsgforDraft();
    }
    protected void ddlusertype1_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectMsgforDraft();
        fillusername();
    }
    protected void ddlusername_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectMsgforDraft();
    }
    protected void fillusername()
    {
        ddlusername.Items.Clear();

        if (ddlusertype1.SelectedItem.Text == "Candidate")
        {

            string emprole = "select CandidateMaster.lastname +''+ CandidateMaster.firstname +''+ CandidateMaster.middlename +' : '+ VacancyPositionTitleMaster.VacancyPositionTitle as CName,Party_master.PartyID from CandidateMaster inner join VacancyPositionTitleMaster on VacancyPositionTitleMaster.id=CandidateMaster.Jobpositionid inner join Party_master on Party_master.PartyID=CandidateMaster.PartyID inner join PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId where PartytTypeMaster.PartyTypeId='" + ddlusertype1.SelectedValue + "' and CandidateMaster.Active='1' order by CName";
            SqlCommand cmdrole = new SqlCommand(emprole, con);
            SqlDataAdapter darole = new SqlDataAdapter(cmdrole);
            DataTable dtrole = new DataTable();
            darole.Fill(dtrole);

            ddlusername.DataSource = dtrole;
            ddlusername.DataTextField = "CName";
            ddlusername.DataValueField = "PartyID";
            ddlusername.DataBind();

            ddlusername.Items.Insert(0, "All");
            ddlusername.Items[0].Value = "0";

        }

        else if (ddlusertype1.SelectedItem.Text == "Employee")
        {

            string emprole = "SELECT distinct Employeemaster.EmployeeName as Name,Party_master.PartyID FROM Party_master inner join PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId inner join Employeemaster on Employeemaster.PartyID=Party_master.PartyID  where PartytTypeMaster.PartyTypeId='" + ddlusertype1.SelectedValue + "' and EmployeeMaster.Active='1' order by Name";
            SqlCommand cmdrole = new SqlCommand(emprole, con);
            SqlDataAdapter darole = new SqlDataAdapter(cmdrole);
            DataTable dtrole = new DataTable();
            darole.Fill(dtrole);

            ddlusername.DataSource = dtrole;
            ddlusername.DataTextField = "Name";
            ddlusername.DataValueField = "PartyID";
            ddlusername.DataBind();

            ddlusername.Items.Insert(0, "All");
            ddlusername.Items[0].Value = "0";
        }
        else if (ddlusertype1.SelectedItem.Text == "Customer" || ddlusertype1.SelectedItem.Text == "Other" || ddlusertype1.SelectedItem.Text == "Vendor" || ddlusertype1.SelectedItem.Text == "Admin")
        {
            string emprole = "SELECT distinct Party_master.Compname +' : '+ Party_master.Contactperson as Name,Party_master.PartyID FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId where PartytTypeMaster.PartyTypeId='" + ddlusertype1.SelectedValue + "' and AccountMaster.Status='1' order by Name";
            SqlCommand cmdrole = new SqlCommand(emprole, con);
            SqlDataAdapter darole = new SqlDataAdapter(cmdrole);
            DataTable dtrole = new DataTable();
            darole.Fill(dtrole);

            ddlusername.DataSource = dtrole;
            ddlusername.DataTextField = "Name";
            ddlusername.DataValueField = "PartyID";
            ddlusername.DataBind();

            ddlusername.Items.Insert(0, "All");
            ddlusername.Items[0].Value = "0";
        }
        else
        {
            ddlusername.Items.Insert(0, "All");
            ddlusername.Items[0].Value = "0";
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

        ddlusertype.Items.Insert(0, "All");
        ddlusertype.Items[0].Value = "0";

        //ddlusertype.SelectedIndex = ddlusertype.Items.IndexOf(ddlusertype.Items.FindByText("Employee"));
    }

    protected void fillusertype111()
    {
        ddlusertype1.Items.Clear();

        string emprole = "select PartyTypeId,PartType from [PartytTypeMaster] where compid='" + Session["Comid"] + "' order by PartType";
        SqlCommand cmdrole = new SqlCommand(emprole, con);
        SqlDataAdapter darole = new SqlDataAdapter(cmdrole);
        DataTable dtrole = new DataTable();
        darole.Fill(dtrole);

        ddlusertype1.DataSource = dtrole;
        ddlusertype1.DataTextField = "PartType";
        ddlusertype1.DataValueField = "PartyTypeId";
        ddlusertype1.DataBind();

        ddlusertype1.Items.Insert(0, "All");
        ddlusertype1.Items[0].Value = "0";

        //ddlusertype.SelectedIndex = ddlusertype.Items.IndexOf(ddlusertype.Items.FindByText("Employee"));
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

        //.................this week .....................


        DateTime d17, d8, d9, d10, d11, d12, d13;
        DateTime lastweekstart, lastweekend;

        d17 = Convert.ToDateTime(System.DateTime.Today.AddDays(-7).ToShortDateString());
        d8 = Convert.ToDateTime(System.DateTime.Today.AddDays(-8).ToShortDateString());
        d9 = Convert.ToDateTime(System.DateTime.Today.AddDays(-9).ToShortDateString());
        d10 = Convert.ToDateTime(System.DateTime.Today.AddDays(-10).ToShortDateString());
        d11 = Convert.ToDateTime(System.DateTime.Today.AddDays(-11).ToShortDateString());
        d12 = Convert.ToDateTime(System.DateTime.Today.AddDays(-12).ToShortDateString());
        d13 = Convert.ToDateTime(System.DateTime.Today.AddDays(-13).ToShortDateString());
        string thisday = (System.DateTime.Today.DayOfWeek.ToString());
        if (thisday.ToString() == "Monday")
        {
            lastweekstart = d17;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (Convert.ToString(thisday) == "Tuesday")
        {
            lastweekstart = d8;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Wednesday")
        {
            lastweekstart = d9;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Thursday")
        {
            lastweekstart = d10;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Friday")
        {
            lastweekstart = d11;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Saturday")
        {
            lastweekstart = d12;
            lastweekend = lastweekstart.Date.AddDays(+6);

        }
        else
        {
            lastweekstart = d13;
            lastweekend = lastweekstart.Date.AddDays(+6);
        }
        string lastweekstartdate = lastweekstart.ToShortDateString();
        ViewState["lastweekstart"] = lastweekstartdate;
        string lastweekenddate = lastweekend.ToShortDateString();
        ViewState["lastweekend"] = lastweekenddate;

        //.................last week .....................

        DateTime d14, d15, d16, d171, d18, d19, d20;
        DateTime last2weekstart, last2weekend;

        d14 = Convert.ToDateTime(System.DateTime.Today.AddDays(-14).ToShortDateString());
        d15 = Convert.ToDateTime(System.DateTime.Today.AddDays(-15).ToShortDateString());
        d16 = Convert.ToDateTime(System.DateTime.Today.AddDays(-16).ToShortDateString());
        d171 = Convert.ToDateTime(System.DateTime.Today.AddDays(-17).ToShortDateString());
        d18 = Convert.ToDateTime(System.DateTime.Today.AddDays(-18).ToShortDateString());
        d19 = Convert.ToDateTime(System.DateTime.Today.AddDays(-19).ToShortDateString());
        d20 = Convert.ToDateTime(System.DateTime.Today.AddDays(-20).ToShortDateString());

        //string thisday = (System.DateTime.Today.DayOfWeek.ToString());
        if (thisday.ToString() == "Monday")
        {
            last2weekstart = d14;
            last2weekend = last2weekstart.Date.AddDays(+6);
        }
        else if (Convert.ToString(thisday) == "Tuesday")
        {
            last2weekstart = d15;
            last2weekend = last2weekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Wednesday")
        {
            last2weekstart = d16;
            last2weekend = last2weekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Thursday")
        {
            last2weekstart = d171;
            last2weekend = last2weekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Friday")
        {
            last2weekstart = d18;
            last2weekend = last2weekstart.Date.AddDays(+6);
        }
        else if (thisday.ToString() == "Saturday")
        {
            last2weekstart = d19;
            last2weekend = last2weekstart.Date.AddDays(+6);

        }
        else
        {
            last2weekstart = d20;
            last2weekend = last2weekstart.Date.AddDays(+6);
        }

        string last2weekstartdate = last2weekstart.ToShortDateString();
        ViewState["last2weekstart"] = last2weekstartdate;
        //string last2weekenddate = last2weekend.ToShortDateString();
        //ViewState["last2week"] = last2weekenddate;



        //------------------this month period-----------------

        DateTime thismonthstart = Convert.ToDateTime(System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString());
        string thismonthstartdate = thismonthstart.ToShortDateString();
        ViewState["thismonthstartdate"] = thismonthstartdate;
        string thismonthenddate = Today.ToString();
        ViewState["thismonthenddate"] = thismonthenddate;

        //------------------this month period end................



        //-----------------last month period start ---------------

        // int last2monthno = Convert.ToInt32(thismonthstart.Month.ToString()) - 2;



        int lastmonthno = Convert.ToInt32(thismonthstart.Month.ToString()) - 1;
        string lastmonthNumber = Convert.ToString(lastmonthno.ToString());
        DateTime lastmonth = Convert.ToDateTime(lastmonthNumber.ToString() + "/1/" + ThisYear.ToString());
        string lastmonthstart = lastmonth.ToShortDateString();
        string lastmonthend = "";

        if (lastmonthNumber == "1" || lastmonthNumber == "3" || lastmonthNumber == "5" || lastmonthNumber == "7" || lastmonthNumber == "8" || lastmonthNumber == "10" || lastmonthNumber == "12")
        {
            lastmonthend = lastmonthNumber + "/31/" + ThisYear.ToString();
        }
        else if (lastmonthNumber == "4" || lastmonthNumber == "6" || lastmonthNumber == "9" || lastmonthNumber == "11")
        {
            lastmonthend = lastmonthNumber + "/30/" + ThisYear.ToString();
        }
        else
        {
            if (System.DateTime.IsLeapYear(Convert.ToInt32(ThisYear.ToString())))
            {
                lastmonthend = lastmonthNumber + "/29/" + ThisYear.ToString();
            }
            else
            {
                lastmonthend = lastmonthNumber + "/28/" + ThisYear.ToString();
            }
        }

        string lastmonthstartdate = lastmonthstart.ToString();
        ViewState["lastmonthstartdate"] = lastmonthstartdate;
        string lastmonthenddate = lastmonthend.ToString();
        ViewState["lastmonthenddate"] = lastmonthenddate;

        //-----------------last month period end -----------------------

        int last2monthno = Convert.ToInt32(thismonthstart.Month.ToString()) - 2;
        string last2monthNumber = Convert.ToString(last2monthno.ToString());
        DateTime last2month = Convert.ToDateTime(last2monthNumber.ToString() + "/1/" + ThisYear.ToString());
        string last2monthstart = last2month.ToShortDateString();
        ViewState["last2monthstart"] = last2monthstart;

        //-----------------last 2 month period end -----------------------


        //--------------this year period start----------------------


        DateTime thisyearstart = Convert.ToDateTime("1/1/" + ThisYear.ToString());
        DateTime thisyearend = Convert.ToDateTime("12/31/" + ThisYear.ToString());

        string thisyearstartdate = thisyearstart.ToShortDateString();
        ViewState["thisyearstartdate"] = thisyearstartdate;
        string thisyearenddate = thisyearend.ToShortDateString();
        ViewState["thisyearenddate"] = thisyearenddate;

        //---------------this year period end-------------------



        //--------------last year period start----------------------


        DateTime lastyearstart = Convert.ToDateTime("1/1/" + System.DateTime.Today.AddYears(-1).Year.ToString());
        DateTime lastyearend = Convert.ToDateTime("12/31/" + System.DateTime.Today.AddYears(-1).Year.ToString());

        string lastyearstartdate = lastyearstart.ToShortDateString();
        ViewState["lastyearstartdate"] = lastyearstartdate;
        string lastyearenddate = lastyearend.ToShortDateString();
        ViewState["lastyearenddate"] = lastyearenddate;



        //---------------last year period end-------------------

        DateTime last2yearstart = Convert.ToDateTime("1/1/" + System.DateTime.Today.AddYears(-2).Year.ToString());
        string last2yearstartdate = last2yearstart.ToShortDateString();
        ViewState["last2yearstartdate"] = last2yearstartdate;

        //---------------last 2 year period -------------------
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            gridDraft.AllowPaging = false;
            gridDraft.PageSize = 1000;
            SelectMsgforDraft();

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;

            if (gridDraft.Columns[0].Visible == true)
            {
                ViewState["editHide"] = "tt";
                gridDraft.Columns[0].Visible = false;
            }

        }
        else
        {
            gridDraft.AllowPaging = true;
            gridDraft.PageSize = 10;
            SelectMsgforDraft();

            Button1.Text = "Printable Version";
            Button7.Visible = false;

            if (ViewState["editHide"] != null)
            {
                gridDraft.Columns[0].Visible = true;
            }
        }
    }
}
