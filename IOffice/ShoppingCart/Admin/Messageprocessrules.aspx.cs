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

public partial class ShoppingCart_Admin_MessageProcessRules : System.Web.UI.Page
{
    SqlConnection con;
   // string save;
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
        Session["PageUrl"] = strData;
        Session["PageName"] = page;
        Page.Title = pg.getPageTitle(page);
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            string str = "SELECT WareHouseId,Name,Address,CurrencyId  FROM WareHouseMaster where comid = '" + Session["comid"] + "'and WareHouseMaster.Status='" + 1 + "' order by name";

            SqlCommand cmd1 = new SqlCommand(str, con);
            cmd1.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            da.Fill(dt);
            lblCompany.Text = Session["comid"].ToString();
            lblBusiness.Text = "All";
            ddlstore.DataSource = dt;
            ddlstore.DataTextField = "Name";
            ddlstore.DataValueField = "WareHouseId";
            ddlstore.DataBind();

            ddlstore_SelectedIndexChanged(sender, e);

            ddlfilterstore.DataSource = dt;
            ddlfilterstore.DataTextField = "Name";
            ddlfilterstore.DataValueField = "WareHouseId";
            ddlfilterstore.DataBind();
            ddlfilterstore.Items.Insert(0, "All");
            ddlfilterstore.Items[0].Value = "0";
            //ddlinoutemailmasterid.Items.Insert(0, "Select");
            //ddlinoutemailmasterid.Items[0].Value = "0";

            //ddlforwordemailiD.Items.Insert(0, "Select");
            //ddlforwordemailiD.Items[0].Value = "0";
            ddlmovefolder.Items.Insert(0, "Select");
            ddlmovefolder.Items[0].Value = "0";


            Fillgrid();
        }

    }

    protected void ddlstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable inoutfill = new DataTable();
        inoutfill = select("Select * From InOutCompanyEmail Where Whid='" + ddlstore.SelectedValue + "' order by EmailId,OutEmailID asc");
        ddlinoutemailmasterid.DataSource = inoutfill;
        ddlinoutemailmasterid.DataTextField = "EmailId";
        ddlinoutemailmasterid.DataValueField = "Id";
        ddlinoutemailmasterid.DataBind();
        ddlinoutemailmasterid.Items.Insert(0, "Select");
        ddlinoutemailmasterid.Items[0].Value = "0";

        ddlforwordemailiD.DataSource = inoutfill;
        ddlforwordemailiD.DataTextField = "OutEmailID";
        ddlforwordemailiD.DataValueField = "Id";
        ddlforwordemailiD.DataBind();
        ddlforwordemailiD.Items.Insert(0, "Select");
        ddlforwordemailiD.Items[0].Value = "0";
        inoutfill = select("Select Compname,PartyId From Party_master Where Whid='" + ddlstore.SelectedValue + "' order by Compname asc");
        ddlparty.DataSource = inoutfill;
        ddlparty.DataTextField = "Compname";
        ddlparty.DataValueField = "PartyId";
        ddlparty.DataBind();
        ddlparty.Items.Insert(0, "Select");
        ddlparty.Items[0].Value = "0";

    }
    protected void ddlinoutemailmasterid_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable inoutfill = new DataTable();
        inoutfill = select("Select * From MessageFolderMaster Where InoutcommingId='" + ddlinoutemailmasterid.SelectedValue + "' order by Foldername asc");
        ddlmovefolder.DataSource = inoutfill;
        ddlmovefolder.DataTextField = "Foldername";
        ddlmovefolder.DataValueField = "Id";
        ddlmovefolder.DataBind();
        ddlmovefolder.Items.Insert(0, "Select");
        ddlmovefolder.Items[0].Value = "0";
    }
    protected void btninsert_Click(object sender, EventArgs e)
    {
        if (btninsert.Text == "Submit")
        {
            DataTable ds = new DataTable();
            ds = select("select * from MessageProcessRules where InoutgoingEmailID='" + ddlinoutemailmasterid.SelectedValue + "'");
            if (ds.Rows.Count == 0)
            {
                string indd = "insert into MessageProcessRules(InoutgoingEmailID)values('" + ddlinoutemailmasterid.SelectedValue + "')";
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(indd, con);
                cmd.ExecuteNonQuery();
                con.Close();
                DataTable dsap = new DataTable();
                dsap = select("select * from MessageProcessRules where InoutgoingEmailID='" + ddlinoutemailmasterid.SelectedValue + "'");
                int mesprulesid = 0;
                if (dsap.Rows.Count > 0)
                {
                    mesprulesid = Convert.ToInt32(dsap.Rows[0]["Id"]);

                }

                string indd1 = "insert into MessageProcessRulesDetail(MessageProcessRuleMasterId,ExecuteOnSend,ExecuteOnReceive,MovetoFolderID,Deleted,PermanentlyDelete,IfSpamForEMailID,MoveToSpam,ForwardtoEmalID,SubjectLineContainsWord,FromParty,MessageDetailContainWord,IFBelongtoMasterSPAMList,RulePriorityNumber)values " +
                    "('" + mesprulesid + "','" + chkexecute.Checked + "','" + chkexreci.Checked + "','" + ddlmovefolder.SelectedValue + "','" + chkdelete.Checked + "','" + chktempdelete.Checked + "','" + chkifsp.Checked + "','" + chkmovespam.Checked + "','" + ddlforwordemailiD.SelectedValue + "','" + txtsubline.Text + "','" + ddlparty.SelectedValue + "','" + txtmessagesdeword.Text + "','" + ifbilmasterspam.Checked + "','" + txtrulepriority.Text + "')";
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmd1 = new SqlCommand(indd1, con);
                cmd1.ExecuteNonQuery();
                con.Close();
                //     lblmsg.Visible = true;
                lblmsg.Text = "Record inserted successfully";
                //     pnlmsg.Visible = true;
                Fillgrid();
                Clear();
                ddlstore_SelectedIndexChanged(sender, e);
                ddlinoutemailmasterid.SelectedIndex = 0;
                ddlparty.SelectedIndex = 0;
                ddlmovefolder.Items.Clear();
                ddlmovefolder.Items.Insert(0, "Select");
                ddlmovefolder.SelectedIndex = 0;
                ddlforwordemailiD.SelectedIndex = 0;
                btnadddd.Visible = true;
                pnladdd.Visible = false;
                lbllege.Text = "";
                
            }
            else
            {
                //     lblmsg.Visible = true;
                lblmsg.Text = "Record already exists";
                //      pnlmsg.Visible = true;
            }
        }
        else
        {
            DataTable ds = new DataTable();
            ds = select("select * from MessageProcessRules where InoutgoingEmailID='" + ddlinoutemailmasterid.SelectedValue + "' and Id<>'" + ViewState["mid"] + "'");
            if (ds.Rows.Count == 0)
            {
                string indd = "Update  MessageProcessRules Set InoutgoingEmailID='" + ddlinoutemailmasterid.SelectedValue + "' where ID In(Select MessageProcessRuleMasterId from MessageProcessRulesDetail where ID='" + ViewState["mid"] + "')";
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(indd, con);
                cmd.ExecuteNonQuery();
                con.Close();

                string indd1 = "Update  MessageProcessRulesDetail Set MessageProcessRuleMasterId='" + ddlinoutemailmasterid.SelectedValue + "', " +
                 "ExecuteOnSend='" + chkexecute.Checked + "',ExecuteOnReceive='" + chkexreci.Checked + "',MovetoFolderID='" + ddlmovefolder.SelectedValue + "'," +
                 "Deleted='" + chkdelete.Checked + "',PermanentlyDelete='" + chktempdelete.Checked + "',IfSpamForEMailID='" + chkifsp.Checked + "',MoveToSpam='" + chkmovespam.Checked + "',ForwardtoEmalID='" + ddlforwordemailiD.SelectedValue + "',SubjectLineContainsWord='" + txtsubline.Text + "',FromParty='" + ddlparty.SelectedValue + "',MessageDetailContainWord='" + txtmessagesdeword.Text + "',IFBelongtoMasterSPAMList='" + ifbilmasterspam.Checked + "',RulePriorityNumber='" + txtrulepriority.Text + "' where Id='" + ViewState["mid"] + "'";
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmd1 = new SqlCommand(indd1, con);
                cmd1.ExecuteNonQuery();
                con.Close();
                //      lblmsg.Visible = true;
                lblmsg.Text = "Record updated successfully";
                //      pnlmsg.Visible = true;
                btninsert.Text = "Submit";
                Fillgrid();
                Clear();
                ddlstore_SelectedIndexChanged(sender, e);
                ddlinoutemailmasterid.SelectedIndex = 0;
                ddlparty.SelectedIndex = 0;
                ddlmovefolder.Items.Clear();
                ddlmovefolder.Items.Insert(0, "Select");
                ddlmovefolder.SelectedIndex = 0;
                ddlforwordemailiD.SelectedIndex = 0;
                btnadddd.Visible = true;
                pnladdd.Visible = false;
                lbllege.Text = "";
                
            }
            else
            {
                //     lblmsg.Visible = true;
                lblmsg.Text = "Record already exists";
                //     pnlmsg.Visible = true;
            }
        }

    }
    protected void Fillgrid()
    {
        DataTable dt = new DataTable();
        if (ddlfilterstore.SelectedIndex > 0)
        {
            lblBusiness.Text = ddlfilterstore.SelectedItem.Text;
            dt = select("SELECT  MessageFolderMaster.Foldername,InOutCompanyEmail.EmailId,Party_Master.Compname,MessageProcessRulesDetail.MoveToSpam,MessageProcessRulesDetail.PermanentlyDelete,MessageProcessRulesDetail.Id FROM MessageProcessRules inner join InOutCompanyEmail on InOutCompanyEmail.Id=MessageProcessRules.InoutgoingEmailID inner join MessageFolderMaster on MessageFolderMaster.InoutcommingId=InOutCompanyEmail.Id left join  MessageProcessRulesDetail on  MessageProcessRulesDetail.MovetoFolderID=MessageFolderMaster.Id Left join Party_master on Party_master.PartyID=MessageProcessRulesDetail.FromParty where Party_master.id='" + Session["comid"] + "' and InOutCompanyEmail.Whid='" + ddlfilterstore.SelectedValue + "' order by EmailId,Foldername,Compname asc");
            //Party_master.id='" + Session["comid"] + "' and

        }
        else
        {
            dt = select("SELECT  MessageFolderMaster.Foldername,InOutCompanyEmail.EmailId,Party_Master.Compname,MessageProcessRulesDetail.MoveToSpam,MessageProcessRulesDetail.PermanentlyDelete,MessageProcessRulesDetail.Id FROM MessageProcessRules inner join InOutCompanyEmail on InOutCompanyEmail.Id=MessageProcessRules.InoutgoingEmailID inner join MessageFolderMaster on MessageFolderMaster.InoutcommingId=InOutCompanyEmail.Id left join  MessageProcessRulesDetail on  MessageProcessRulesDetail.MovetoFolderID=MessageFolderMaster.Id Left join Party_master on Party_master.PartyID=MessageProcessRulesDetail.FromParty where  Party_master.id='" + Session["comid"] + "' order by EmailId,Foldername,Compname asc");

        }
        if (dt.Rows.Count > 0)
        {

            DataView myDataView = new DataView();
            myDataView = dt.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            GridEmail.DataSource = dt;
            GridEmail.DataBind();
        }
        else
        {
            GridEmail.DataSource = null;
            GridEmail.DataBind();

        }
    }
    protected void Clear()
    {
        ddlstore.SelectedIndex = 0;
        
        
        chkexecute.Checked = false;
        chkexreci.Checked = false;
        chkifsp.Checked = false;
        ifbilmasterspam.Checked = false;
        txtsubline.Text = "";
        
        txtmessagesdeword.Text = "";
        
        chkdelete.Checked = false;
        chktempdelete.Checked = false;
        chkmovespam.Checked = false;
        
        txtrulepriority.Text = "";



        btninsert.Text = "Submit";
    }
    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        
        Clear();
        ddlstore_SelectedIndexChanged(sender, e);
        ddlinoutemailmasterid.SelectedIndex = 0;
        ddlparty.SelectedIndex = 0;
        ddlmovefolder.Items.Clear();
        ddlmovefolder.Items.Insert(0, "Select");
        ddlmovefolder.SelectedIndex = 0;
        ddlforwordemailiD.SelectedIndex = 0;
        lblmsg.Text = "";
        btnadddd.Visible = true;
        pnladdd.Visible = false;
        lbllege.Text = "";
    }
    protected void ddlfilterstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillgrid();
        //    pnlmsg.Visible = false;
        lblmsg.Text = "";
    }
    protected void GridEmail_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridEmail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            btnadddd.Visible = false;
            pnladdd.Visible = true;
            lbllege.Text = "Edit Message Process Rule";
            lblmsg.Text = "";

            //int indx = Convert.ToInt32(e.CommandArgument.ToString());
            //Int32 datakey = Int32.Parse(GridEmail.DataKeys[indx].Value.ToString());

            int datakey = Convert.ToInt32(e.CommandArgument);

            ViewState["mid"] = datakey;
            DataTable dt = new DataTable();
            dt = select("SELECT * FROM MessageProcessRulesDetail where  MessageProcessRulesDetail.ID='" + datakey + "'");

            if (dt.Rows.Count > 0)
            {
                DataTable dsc = new DataTable();
                dsc = select("select InOutCompanyEmail.Whid from MessageProcessRules inner join InOutCompanyEmail on InOutCompanyEmail.Id=MessageProcessRules.InoutgoingEmailID where MessageProcessRules.Id='" + dt.Rows[0]["MessageProcessRuleMasterId"] + "'");
                if (dsc.Rows.Count > 0)
                {
                    ddlstore.SelectedIndex = ddlstore.Items.IndexOf(ddlstore.Items.FindByValue(dsc.Rows[0]["Whid"].ToString()));

                }
                //    ddlstore_SelectedIndexChanged(sender, e);
                DataTable inoutfill = new DataTable();
                inoutfill = select("Select * From InOutCompanyEmail Where Whid='" + ddlstore.SelectedValue + "'");
                ddlinoutemailmasterid.DataSource = inoutfill;
                ddlinoutemailmasterid.DataTextField = "EmailId";
                ddlinoutemailmasterid.DataValueField = "Id";
                ddlinoutemailmasterid.DataBind();

                ddlforwordemailiD.DataSource = inoutfill;
                ddlforwordemailiD.DataTextField = "OutEmailID";
                ddlforwordemailiD.DataValueField = "Id";
                ddlforwordemailiD.DataBind();

                inoutfill = select("Select Compname,PartyId From Party_master Where Whid='" + ddlstore.SelectedValue + "'");
                ddlparty.DataSource = inoutfill;
                ddlparty.DataTextField = "Compname";
                ddlparty.DataValueField = "PartyId";
                ddlparty.DataBind();

                DataTable inoutfill1 = new DataTable();
                inoutfill1 = select("Select * From MessageFolderMaster Where InoutcommingId='" + ddlinoutemailmasterid.SelectedValue + "'");
                ddlmovefolder.DataSource = inoutfill1;
                ddlmovefolder.DataTextField = "Foldername";
                ddlmovefolder.DataValueField = "Id";
                ddlmovefolder.DataBind();

                //     ddlinoutemailmasterid_SelectedIndexChanged(sender, e);

                ddlinoutemailmasterid.SelectedIndex = ddlinoutemailmasterid.Items.IndexOf(ddlinoutemailmasterid.Items.FindByValue(dt.Rows[0]["MessageProcessRuleMasterId"].ToString()));
                ddlmovefolder.SelectedIndex = ddlmovefolder.Items.IndexOf(ddlmovefolder.Items.FindByValue(dt.Rows[0]["MovetoFolderID"].ToString()));
                ddlparty.SelectedIndex = ddlparty.Items.IndexOf(ddlparty.Items.FindByValue(dt.Rows[0]["FromParty"].ToString()));
                ddlforwordemailiD.SelectedIndex = ddlforwordemailiD.Items.IndexOf(ddlforwordemailiD.Items.FindByValue(dt.Rows[0]["ForwardtoEmalID"].ToString()));

                chkexecute.Checked = Convert.ToBoolean(dt.Rows[0]["ExecuteOnSend"]);
                chkexreci.Checked = Convert.ToBoolean(dt.Rows[0]["ExecuteOnReceive"]);
                chkdelete.Checked = Convert.ToBoolean(dt.Rows[0]["Deleted"]);
                chktempdelete.Checked = Convert.ToBoolean(dt.Rows[0]["PermanentlyDelete"]);
                chkifsp.Checked = Convert.ToBoolean(dt.Rows[0]["IfSpamForEMailID"]);
                chkmovespam.Checked = Convert.ToBoolean(dt.Rows[0]["MoveToSpam"]);
                ifbilmasterspam.Checked = Convert.ToBoolean(dt.Rows[0]["IFBelongtoMasterSPAMList"]);
                txtmessagesdeword.Text = Convert.ToString(dt.Rows[0]["MessageDetailContainWord"]);
                txtsubline.Text = Convert.ToString(dt.Rows[0]["SubjectLineContainsWord"]);
                txtrulepriority.Text = Convert.ToString(dt.Rows[0]["RulePriorityNumber"]);
                btninsert.Text = "Update";
            }
        }
        if (e.CommandName == "Delete")
        {
            int datakey = Convert.ToInt32(e.CommandArgument);

            //int indx = Convert.ToInt32(e.CommandArgument.ToString());
            //Int32 datakey = Int32.Parse(GridEmail.DataKeys[indx].Value.ToString());

            string result11 = "delete from MessageProcessRules where ID In(Select MessageProcessRuleMasterId from MessageProcessRulesDetail where ID='" + datakey + "')";
            string result1 = "delete from MessageProcessRulesDetail where ID='" + datakey + "'";
            SqlCommand cmddel1 = new SqlCommand(result11, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmddel1.ExecuteNonQuery();
            con.Close();

            SqlCommand cmddel = new SqlCommand(result1, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmddel.ExecuteNonQuery();
            con.Close();
            //   lblmsg.Visible = true;
            lblmsg.Text = "Record deleted successfully";
            //    pnlmsg.Visible = true;
            Fillgrid();




        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        if (Button2.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");
            //    pnlmsg.Visible = false;
            lblmsg.Text = "";
            Button2.Text = "Hide Printable Version";
            Button1.Visible = true;
            if (GridEmail.Columns[5].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridEmail.Columns[5].Visible = false;
            }
            if (GridEmail.Columns[6].Visible == true)
            {
                ViewState["delHide"] = "tt";
                GridEmail.Columns[6].Visible = false;
            }
        }
        else
        {

            //  pnlgrid.ScrollBars = ScrollBars.Vertical;
            //  pnlgrid.Height = new Unit(200);

            Button2.Text = "Printable Version";
            Button1.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridEmail.Columns[5].Visible = true;
            }
            if (ViewState["delHide"] != null)
            {
                GridEmail.Columns[6].Visible = true;
            }
        }
    }

    protected void GridEmail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnadddd_Click(object sender, EventArgs e)
    {
        btnadddd.Visible = false;
        pnladdd.Visible = true;
        lblmsg.Text = "";
        lbllege.Text = "Add New Message Process Rule";
    }
    protected void GridEmail_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        Fillgrid();
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
}
