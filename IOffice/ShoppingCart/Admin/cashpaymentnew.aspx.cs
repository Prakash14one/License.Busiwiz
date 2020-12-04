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
using System.Text;
using System.Net;
using System.Net.Mail;
public partial class ShoppingCart_Admin_cashpaymentnew : System.Web.UI.Page
{
    DataSet dsnew = new DataSet();
    DataTable dtnew = new DataTable();
    //int q;
    String qryStr;
    int groupid = 0;
    string accid = "";
    int classid = 0;
    //SqlTransaction tr = null;
   
    double db;
    string compid;
    string page;
    double x1 = 0, x2 = 0, x3 = 0, x4 = 0, x5 = 0, x6 = 0, x7 = 0, x8 = 0, x9 = 0, xz = 0,x10;
     string strconn;
   
    SqlConnection conn=new SqlConnection(PageConn.connnn);
  
   
    DocumentCls1 clsDocument = new DocumentCls1();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        
            pagetitleclass pg = new pagetitleclass();
            string strData = Request.Url.ToString();

            char[] separator = new char[] { '/' };
            compid = Session["Comid"].ToString();
            string[] strSplitArr = strData.Split(separator);
            int i = Convert.ToInt32(strSplitArr.Length);
            page = strSplitArr[i - 1].ToString();


            Page.Title = pg.getPageTitle(page);
        
            if (Session["Comid"] == null)
            {
                Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
            }

            lblmsg.Visible = false;
          

            //Label2.Visible = false;
        
       
        if (!Page.IsPostBack)
        {
         
            Panel3.Visible = false;
            ViewState["EntryNumber"] = "";

           

            txtTodate.Text = System.DateTime.Now.ToShortDateString();
            lblbdate.Text = txtTodate.Text;
            SqlCommand cmd511 = new SqlCommand("Sp_Select_CashPayment", conn);
            cmd511.CommandType = CommandType.StoredProcedure;
            cmd511.Parameters.AddWithValue("@compid", compid);
            cmd511.Parameters.AddWithValue("@whid", ddlwarehouse.SelectedValue);
            // cmd.Parameters.AddWithValue("@GroupId", Convert.ToInt32(ddlGroupName1.SelectedValue));
            SqlDataAdapter adp511 = new SqlDataAdapter(cmd511);
            DataSet ds511 = new DataSet();
            adp511.Fill(ds511);
            double b = 0;
            if (ds511.Tables[0].Rows.Count > 0)
            {
                if (ds511.Tables[0].Rows[0]["EntryNumber"].ToString() != "")
                {

                    ViewState["EntryTypeNumber"] = ds511.Tables[0].Rows[0][0].ToString();
                    double a = Convert.ToDouble(ViewState["EntryTypeNumber"]);
                    b = a + 1;
                }
                else
                {
                    b = 1;
                }
            }
            else
            {
                b = 1;
            }

            //txtenteryNumber.Text = b.ToString();
            lblentrynumber.Text = b.ToString();
            Session["EN"] = lblentrynumber.Text;
            Session["ET"] = lblEntryType.Text;

            Fillddlwarehouse();

            if (Request.QueryString["docid"] == null)
            {
                if (Request.QueryString["Trid"] != null)
                {
                    ViewState["Trid"] = Request.QueryString["Trid"].ToString();
                    fillUpdate();
                }
                else
                {

                    fillauto();
                }
                ddlpartynamename.Items.Insert(0, "Select");
                ddlpartynamename.Items[0].Value = "0";
            }
        }
    }
    protected void fillUpdate()
    {
        DataTable dt = new DataTable();
        dt = (DataTable)select("Select distinct EntryTypeMaster.Entry_Type_Name, TranctionMaster.Tranction_Amount, TranctionMaster.Whid,TranctionMasterSuppliment.Memo,TranctionMaster.EntryNumber, TranctionMaster.EntryTypeId,TranctionMaster.Tranction_Master_Id,TranctionMaster.Whid,Tranction_Details.Memo as md, Tranction_Details.AccountDebit,Tranction_Details.AccountCredit,Tranction_Details.AmountCredit,Tranction_Details.AmountDebit,Tranction_Details.Tranction_Details_Id,TranctionMasterSuppliment.Tranction_Master_SupplimentId,TranctionMaster.Date,TranctionMasterSuppliment.Party_MasterId from EntryTypeMaster inner join TranctionMaster on TranctionMaster.EntryTypeId=EntryTypeMaster.Entry_Type_Id inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id inner join TranctionMasterSuppliment on TranctionMasterSuppliment.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where TranctionMaster.compid='" + Session["Comid"] + "' and TranctionMaster.Tranction_Master_Id='" + ViewState["Trid"] + "' and TranctionMaster.EntryTypeId='1'");
        if (dt.Rows.Count > 0)
        {
           
            ddlwarehouse.SelectedIndex = ddlwarehouse.Items.IndexOf(ddlwarehouse.Items.FindByValue(dt.Rows[0]["Whid"].ToString()));
            txtTodate.Text = Convert.ToDateTime(dt.Rows[0]["Date"]).ToShortDateString();
            lblbdate.Text = txtTodate.Text;
            EventArgs e = new EventArgs();
            object sender = new object();

            ddlAccount.DataSource = (DataSet)fillddl2();
            ddlAccount.DataValueField = "AccountId";
            ddlAccount.DataTextField = "AccountName";
            ddlAccount.DataBind();
            //ddlAccount.DataBind();
            //ddlAccount.Items.Insert(0, "--Select--");
            LinkButton97666667.Visible = false;
            if (Convert.ToString(dt.Rows[0]["Party_MasterId"]) == "0" || Convert.ToString(dt.Rows[0]["Party_MasterId"]) == "")
            {
                btnsubradio2.Visible = false;
                Button3.Visible = false;
               // btnupdateAccc.Visible = true;
                btnedit.Visible = true;
                ddlAccount.SelectedIndex = ddlAccount.Items.IndexOf(ddlAccount.Items.FindByValue(dt.Rows[0]["AccountCredit"].ToString()));


                RadioButtonList1.SelectedIndex = RadioButtonList1.Items.IndexOf(RadioButtonList1.Items.FindByValue("0"));


                RadioButtonList1_SelectedIndexChanged(sender, e);
                ddlpartynamename.Items.Clear();
                ddlpartynamename.Items.Insert(0, "--Select--");
                if (dt.Rows.Count >= 2)
                {
                    ddlAccountname1.SelectedIndex = ddlAccountname1.Items.IndexOf(ddlAccountname1.Items.FindByValue(dt.Rows[1]["AccountDebit"].ToString()));
                    txtAmount1.Text = Convert.ToString(dt.Rows[1]["AmountDebit"]);
                    txtmemo1.Text = Convert.ToString(dt.Rows[1]["md"]);
                }
                if (dt.Rows.Count >= 3)
                {
                    ddlAccountname2.SelectedIndex = ddlAccountname2.Items.IndexOf(ddlAccountname2.Items.FindByValue(dt.Rows[2]["AccountDebit"].ToString()));
                    txtAmount2.Text = Convert.ToString(dt.Rows[2]["AmountDebit"]);
                    txtmemo2.Text = Convert.ToString(dt.Rows[2]["md"]);
                }
                if (dt.Rows.Count >= 4)
                {
                    ddlAccountname3.SelectedIndex = ddlAccountname3.Items.IndexOf(ddlAccountname3.Items.FindByValue(dt.Rows[3]["AccountDebit"].ToString()));
                    txtAmount3.Text = Convert.ToString(dt.Rows[3]["AmountDebit"]);
                    txtmemo3.Text = Convert.ToString(dt.Rows[3]["md"]);
                }
                if (dt.Rows.Count >= 5)
                {
                    ddlAccountname4.SelectedIndex = ddlAccountname4.Items.IndexOf(ddlAccountname4.Items.FindByValue(dt.Rows[4]["AccountDebit"].ToString()));
                    txtAmount4.Text = Convert.ToString(dt.Rows[4]["AmountDebit"]);
                    txtmemo4.Text = Convert.ToString(dt.Rows[4]["md"]);
                }
                if (dt.Rows.Count >= 6)
                {
                    ddlAccountname5.SelectedIndex = ddlAccountname5.Items.IndexOf(ddlAccountname5.Items.FindByValue(dt.Rows[5]["AccountDebit"].ToString()));
                    txtAmount5.Text = Convert.ToString(dt.Rows[5]["AmountDebit"]);
                    txtmemo5.Text = Convert.ToString(dt.Rows[5]["md"]);
                }
                if (dt.Rows.Count >= 7)
                {
                    ddlAccountname6.SelectedIndex = ddlAccountname6.Items.IndexOf(ddlAccountname6.Items.FindByValue(dt.Rows[6]["AccountDebit"].ToString()));
                    txtAmount6.Text = Convert.ToString(dt.Rows[6]["AmountDebit"]);
                    txtmemo6.Text = Convert.ToString(dt.Rows[6]["md"]);
                }
                if (dt.Rows.Count >= 8)
                {
                    ddlAccountname7.SelectedIndex = ddlAccountname7.Items.IndexOf(ddlAccountname7.Items.FindByValue(dt.Rows[7]["AccountDebit"].ToString()));
                    txtAmount7.Text = Convert.ToString(dt.Rows[7]["AmountDebit"]);
                    txtmemo7.Text = Convert.ToString(dt.Rows[7]["md"]);
                }
                if (dt.Rows.Count >= 9)
                {
                    ddlAccountname8.SelectedIndex = ddlAccountname8.Items.IndexOf(ddlAccountname8.Items.FindByValue(dt.Rows[8]["AccountDebit"].ToString()));
                    txtAmount8.Text = Convert.ToString(dt.Rows[8]["AmountDebit"]);
                    txtmemo8.Text = Convert.ToString(dt.Rows[8]["md"]);
                }
                if (dt.Rows.Count >= 10)
                {
                    ddlAccountname9.SelectedIndex = ddlAccountname9.Items.IndexOf(ddlAccountname9.Items.FindByValue(dt.Rows[9]["AccountDebit"].ToString()));
                    txtAmount9.Text = Convert.ToString(dt.Rows[9]["AmountDebit"]);
                    txtmemo9.Text = Convert.ToString(dt.Rows[9]["md"]);
                }
                lblgtotal.Text = Convert.ToString(dt.Rows[0]["Tranction_Amount"]);
                fillenableparty(false);
            }
            else
            {
                btnsubmit.Visible = false;
                Button2.Visible = false;
                //btnupdatepaety.Visible = true;
                btneditgene.Visible = true;
                ddlAccount.SelectedIndex = ddlAccount.Items.IndexOf(ddlAccount.Items.FindByValue(dt.Rows[0]["AccountCredit"].ToString()));

                string sssx1 = "Select Party_master.PartyTypeId from Party_master Where PartyId='" + dt.Rows[0]["Party_MasterId"] + "'";
                SqlDataAdapter adpt1 = new SqlDataAdapter(sssx1, conn);
                DataTable dtpt1 = new DataTable();
                adpt1.Fill(dtpt1);
                if (dtpt1.Rows.Count > 0)
                {

                    RadioButtonList1.SelectedIndex = RadioButtonList1.Items.IndexOf(RadioButtonList1.Items.FindByValue("1"));
                    RadioButtonList1_SelectedIndexChanged(sender, e);
                    ddlpartytypetype.SelectedIndex = ddlpartytypetype.Items.IndexOf(ddlpartytypetype.Items.FindByValue(dtpt1.Rows[0]["PartyTypeId"].ToString()));
                    ddlpartytypetype_SelectedIndexChanged(sender, e);
                    ddlpartynamename.SelectedIndex = ddlpartynamename.Items.IndexOf(ddlpartynamename.Items.FindByValue(dt.Rows[0]["Party_MasterId"].ToString()));
                    ddlpartynamename_SelectedIndexChanged(sender, e);
                    fillenableparty(false);
            
                }

            }
            RadioButtonList1.Enabled = false;
            lblentrynumber.Text = Convert.ToString(dt.Rows[0]["EntryNumber"]);
            lblEntryType.Text = Convert.ToString(dt.Rows[0]["Entry_Type_Name"]);
            txtpayamount.Text = Convert.ToString(dt.Rows[0]["Tranction_Amount"]);
            txtmmo.Text = Convert.ToString(dt.Rows[0]["Memo"]);

            lblmsg.Text = "";

        }
    }
    protected void fillenableparty(bool t)
    {
        ddlwarehouse.Enabled = t;
        ddlAccount.Enabled = t;
       
        txtTodate.Enabled = t;
        txtpayamount.Enabled = t;
        txtmmo.Enabled = t;
         chkdoc.Enabled = t;
       
        if (RadioButtonList1.SelectedValue == "1")
        {
            ddlpartynamename.Enabled = t;
            ddlpartytypetype.Enabled = t;
            chkappamount.Enabled = t;
            CheckBox1.Enabled = t;

            if (Request.QueryString["Trid"] == null)
            {
                Button2.Visible = true;
                
            }
 
        }
        else if (RadioButtonList1.SelectedValue == "0")
        {
            ddlAccountname1.Enabled = t;
            ddlAccountname2.Enabled = t;
            ddlAccountname3.Enabled = t;
            ddlAccountname4.Enabled = t;
            ddlAccountname5.Enabled = t;
            ddlAccountname6.Enabled = t;
            ddlAccountname7.Enabled = t;
            ddlAccountname8.Enabled = t;
            ddlAccountname9.Enabled = t;
            txtAmount1.Enabled = t;
            txtAmount2.Enabled = t;
            txtAmount3.Enabled = t;
            txtAmount4.Enabled = t;
            txtAmount5.Enabled = t;
            txtAmount6.Enabled = t;
            txtAmount7.Enabled = t;
            txtAmount8.Enabled = t;
            txtAmount9.Enabled = t;
            txtmemo1.Enabled = t;
            txtmemo2.Enabled = t;
            txtmemo3.Enabled = t;
            txtmemo4.Enabled = t;
            txtmemo5.Enabled = t;
            txtmemo6.Enabled = t;
            txtmemo7.Enabled = t;
            txtmemo8.Enabled = t;
            txtmemo9.Enabled = t;
            chktrackcost.Enabled = t;
            if (Request.QueryString["Trid"] == null)
            {
                Button3.Visible = true;
            }
 

        }


    }
    protected void Fillddlwarehouse()
    {

        DataTable dt = ClsStore.SelectStorename();
        if (dt.Rows.Count > 0)
        {
            ddlwarehouse.DataSource = dt;
            ddlwarehouse.DataTextField = "Name";
            ddlwarehouse.DataValueField = "WareHouseId";
            ddlwarehouse.DataBind();
        }
        //ddlwarehouse.DataBind();
        //ddlwarehouse.Items.Insert(0, "-Select-");

        //ddlwarehouse.Items[0].Value = "0";
      
        if (Request.QueryString["docid"] != null)
        {
            string sssx11 = "SELECT WarehouseMaster.WarehouseId, DocumentMainType.DocumentMainType + '/'+DocumentSubType.DocumentSubType +'/'+ DocumentType.DocumentType as DocumentType,DocumentDate,DocumentTitle,DocumentId FROM WarehouseMaster inner join DocumentMainType on DocumentMainType.Whid=WarehouseId inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentMaster on DocumentMaster.DocumentTypeId=DocumentType.DocumentTypeId where DocumentId='" + Request.QueryString["docid"] + "'";
            SqlDataAdapter adpt11 = new SqlDataAdapter(sssx11, conn);
            DataTable dtpt11 = new DataTable();
            adpt11.Fill(dtpt11);
            if (dtpt11.Rows.Count > 0)
            {
                if (dtpt11.Rows.Count > 0)
                {
                    Label9.Text = dtpt11.Rows[0]["DocumentId"].ToString();
                    Label10.Text = dtpt11.Rows[0]["DocumentTitle"].ToString();

                    Label11.Text = Convert.ToDateTime(dtpt11.Rows[0]["DocumentDate"]).ToShortDateString(); ;

                    Label12.Text = dtpt11.Rows[0]["DocumentType"].ToString();
                   
                    ddlwarehouse.SelectedValue = dtpt11.Rows[0]["WarehouseId"].ToString();
                    EventArgs e = new EventArgs();
                    object sender = new object();
                    ddlwarehouse_SelectedIndexChanged(sender, e);
                    ddlwarehouse.Enabled = false;
                    if (Request.QueryString["docid"] != null)
                    {
                        chkdoc.Checked = false;
                        chkdoc.Visible = false;
                    }
                    else
                    {

                        chkdoc.Visible = true;

                    }
                    pnnl.Visible = true;

                }
            }

        }
        else
        {
            DataTable dteeed = ClsStore.SelectEmployeewithIdwise();
            if (dteeed.Rows.Count > 0)
            {
                ddlwarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);

            }
        }
    }
    public DataSet fillddl()
    {
       // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        //SqlCommand cmd = new SqlCommand("Sp_select_AccountandGroupName", conn);
      
        SqlCommand cmd = new SqlCommand("SELECT  distinct  GroupCompanyMaster.groupdisplayname + '-' + AccountMaster.AccountName AS Account,  AccountMaster.AccountId  FROM       AccountMaster INNER JOIN  GroupCompanyMaster ON AccountMaster.GroupId = GroupCompanyMaster.GroupId  WHERE   (AccountMaster.AccountId IS NOT NULL) and AccountMaster.Status=1 and AccountMaster.compid = '" + Session["comid"] + "' and accountmaster.Whid='" + ddlwarehouse.SelectedValue + "' and GroupCompanyMaster.Whid='" + ddlwarehouse.SelectedValue + "'  ORDER BY Account", conn);

        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }
    public DataSet fillddl2()
    {
               SqlCommand cmd = new SqlCommand("Sp_Select_AccountName", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@compid", compid);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;



    }
    protected void fillddlpartynamename()
    {
        ddlpartynamename.Items.Clear();
        if (ddlpartytypetype.SelectedIndex >0)
        {
                   SqlCommand cmdname1 = new SqlCommand("SELECT     Party_master.PartyID, PartytTypeMaster.PartyTypeId, PartytTypeMaster.PartType, Party_master.Compname, User_master.Active " +
" FROM         Party_master INNER JOIN " +
                      " PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId LEFT OUTER JOIN " +
                      " User_master ON Party_master.PartyID = User_master.PartyID where Party_master.PartyTypeId= '" + ddlpartytypetype.SelectedValue + "' and User_master.Active='1' and PartytTypeMaster.compid = '" + compid + "' and Party_master.Whid='" + ddlwarehouse.SelectedValue + "'  order by Compname asc", conn);
            SqlDataAdapter dtpname1 = new SqlDataAdapter(cmdname1);
            DataSet dsname1 = new DataSet();
            dtpname1.Fill(dsname1);
            //dtpname1.Fill(dsname1);

           
            ddlpartynamename.DataSource = dsname1;
            ddlpartynamename.DataTextField = "Compname";
            ddlpartynamename.DataValueField = "PartyID";
            ddlpartynamename.DataBind();


            //ddlPartyName.Items.Insert(0, "Add New");
            ddlpartynamename.Items.Insert(0, "Select");
            ddlpartynamename.Items[0].Value = "0";
        }
    }
    protected void fillddlpartyname()
    {
        SqlCommand cmdname = new SqlCommand("Select Compname,PartyID from Party_Master left outer join PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId where PartyTypeId = '1' and PartytTypeMaster.compid = '" + compid + "' and Party_Master.Whid='" + ddlwarehouse.SelectedValue + "'  order by Compname asc", conn);
        SqlDataAdapter dtpname = new SqlDataAdapter(cmdname);
        DataSet dsname = new DataSet();

        dtpname.Fill(dsname);

       

    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "1")
        {
            controlclear();
          //  CheckBox1.Visible = true;
            chkappamount.Visible = true;
            Panel3.Visible = false;
            Panel4.Visible = true;
            
          
            lblpartname.Visible = true;
            lblparttyp.Visible = true;
            ddlpartytypetype.Visible = true;
            pnlparty.Visible = true;
            ddlpartynamename.Visible = true;
            chktrackcost.Visible = false;
            chktrackcost_CheckedChanged(sender, e);
            LinkButton97666667.Visible = true;
            LinkButton1.Visible = true;

            SqlCommand cmdpartytype1 = new SqlCommand("Select PartType,PartyTypeId from PartytTypeMaster where PartytTypeMaster.compid = '" + compid + "' order by PartType", conn);

            SqlDataAdapter dtppatty1 = new SqlDataAdapter(cmdpartytype1);
            DataSet dsparty1 = new DataSet();
            dtppatty1.Fill(dsparty1);
            //dtppatty1.Fill(dsparty1);

            if (dsparty1.Tables[0].Rows.Count > 0)
            {
                ddlpartytypetype.Items.Clear();
                ddlpartytypetype.DataSource = dsparty1;
                ddlpartytypetype.DataTextField = "PartType";
                ddlpartytypetype.DataValueField = "PartyTypeId";
                ddlpartytypetype.DataBind();
                ddlpartytypetype.Items.Insert(0, "Select");
                ddlpartytypetype.Items[0].Value = "0";

               // ddlpartytypetype.SelectedIndex = ddlpartytypetype.Items.IndexOf(ddlpartytypetype.Items.FindByValue("1"));
                //ddlpartytypetype_SelectedIndexChanged(sender, e);
            }
            ddlpartytypetype_SelectedIndexChanged(sender, e);
        }
        else
        {
            




            Panel3.Visible = true;
        }
        if (RadioButtonList1.SelectedValue == "0")
        {

            fillacc();

            controlclear();
            Panel4.Visible = false;
            Panel3.Visible = true;
            chkappamount.Visible = false;
            GridView2.DataSource = null;
            GridView2.DataBind();
            //chktrackcost.Visible = true;
            chktrackcost_CheckedChanged(sender, e);
            lblpartname.Visible = false;
            lblparttyp.Visible = false;
            ddlpartytypetype.Visible = false;
            pnlparty.Visible = false;
            ddlpartynamename.Visible = false;
            LinkButton97666667.Visible = false;
            LinkButton1.Visible = false;


            ddlpartytypetype_SelectedIndexChanged(sender, e);

        }
        else
        {
            Panel4.Visible = true;
            Panel3.Visible = false;
        }
    }
    protected void fillacc()
    {
        if (RadioButtonList1.SelectedValue == "0")
        {

            ddlAccountname1.DataSource = (DataSet)fillddl();
            ddlAccountname1.DataTextField = "Account";
            ddlAccountname1.DataValueField = "AccountId";
            ddlAccountname1.DataBind();
            ddlAccountname1.Items.Insert(0, "--Select--");

            ddlAccountname2.DataSource = (DataSet)fillddl();
            ddlAccountname2.DataTextField = "Account";
            ddlAccountname2.DataValueField = "AccountId";
            ddlAccountname2.DataBind();
            ddlAccountname2.Items.Insert(0, "--Select--");

            ddlAccountname3.DataSource = (DataSet)fillddl();
            ddlAccountname3.DataTextField = "Account";
            ddlAccountname3.DataValueField = "AccountId";
            ddlAccountname3.DataBind();
            ddlAccountname3.Items.Insert(0, "--Select--");

            ddlAccountname4.DataSource = (DataSet)fillddl();
            ddlAccountname4.DataTextField = "Account";
            ddlAccountname4.DataValueField = "AccountId";
            ddlAccountname4.DataBind();
            ddlAccountname4.Items.Insert(0, "--Select--");


            ddlAccountname5.DataSource = (DataSet)fillddl();
            ddlAccountname5.DataTextField = "Account";
            ddlAccountname5.DataValueField = "AccountId";
            ddlAccountname5.DataBind();
            ddlAccountname5.Items.Insert(0, "--Select--");

            ddlAccountname6.DataSource = (DataSet)fillddl();
            ddlAccountname6.DataTextField = "Account";
            ddlAccountname6.DataValueField = "AccountId";
            ddlAccountname6.DataBind();
            ddlAccountname6.Items.Insert(0, "--Select--");

            ddlAccountname7.DataSource = (DataSet)fillddl();
            ddlAccountname7.DataTextField = "Account";
            ddlAccountname7.DataValueField = "AccountId";
            ddlAccountname7.DataBind();
            ddlAccountname7.Items.Insert(0, "--Select--");

            ddlAccountname8.DataSource = (DataSet)fillddl();
            ddlAccountname8.DataTextField = "Account";
            ddlAccountname8.DataValueField = "AccountId";
            ddlAccountname8.DataBind();
            ddlAccountname8.Items.Insert(0, "--Select--");

            ddlAccountname9.DataSource = (DataSet)fillddl();
            ddlAccountname9.DataTextField = "Account";
            ddlAccountname9.DataValueField = "AccountId";
            ddlAccountname9.DataBind();
            ddlAccountname9.Items.Insert(0, "--Select--");
        }
    }
    protected void ddlpartytypetype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlpartytypetype.SelectedIndex > 0)
        {
            
            CheckBox1.Visible = false;

            fillddlpartynamename();
        }
        else
        {
            
           // CheckBox1.Visible = true;
        }
    }
    protected void controlclear()
    {
        ddlAccountname1.SelectedIndex = ddlAccountname1.Items.IndexOf(ddlAccountname1.Items.FindByValue("--Select--"));
        ddlAccountname2.SelectedIndex = ddlAccountname2.Items.IndexOf(ddlAccountname2.Items.FindByValue("--Select--"));
        ddlAccountname3.SelectedIndex = ddlAccountname3.Items.IndexOf(ddlAccountname3.Items.FindByValue("--Select--"));
        ddlAccountname4.SelectedIndex = ddlAccountname4.Items.IndexOf(ddlAccountname4.Items.FindByValue("--Select--"));
        ddlAccountname5.SelectedIndex = ddlAccountname5.Items.IndexOf(ddlAccountname5.Items.FindByValue("--Select--"));
        ddlAccountname6.SelectedIndex = ddlAccountname6.Items.IndexOf(ddlAccountname6.Items.FindByValue("--Select--"));
        ddlAccountname7.SelectedIndex = ddlAccountname7.Items.IndexOf(ddlAccountname7.Items.FindByValue("--Select--"));
        ddlAccountname8.SelectedIndex = ddlAccountname8.Items.IndexOf(ddlAccountname8.Items.FindByValue("--Select--"));
        ddlAccountname9.SelectedIndex = ddlAccountname9.Items.IndexOf(ddlAccountname9.Items.FindByValue("--Select--"));
        txtAmount1.Text = "";
        txtAmount2.Text = "";
        txtAmount3.Text = "";
        txtAmount4.Text = "";
        txtAmount5.Text = "";
        txtAmount6.Text = "";
        txtAmount7.Text = "";
        txtAmount8.Text = "";
        txtAmount9.Text = "";
        txtmemo1.Text = "";
        txtmemo2.Text = "";
        txtmemo3.Text = "";
        txtmemo4.Text = "";
        txtmemo5.Text = "";
        txtmemo6.Text = "";
        txtmemo7.Text = "";
        txtmemo8.Text = "";
        txtmemo9.Text = "";
        //txtmmo.Text = "";
        //txtpayamount.Text = "";

    }
    protected void ddlpartynamename_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataColumn dtcom1 = new DataColumn();
        dtcom1.DataType = System.Type.GetType("System.String");
        dtcom1.ColumnName = "Date";
        dtcom1.ReadOnly = false;
        dtcom1.Unique = false;
        dtcom1.AllowDBNull = true;

        dtnew.Columns.Add(dtcom1);

        DataColumn dtcom2 = new DataColumn();
        dtcom2.DataType = System.Type.GetType("System.String");
        dtcom2.ColumnName = "EntryType";
        dtcom2.ReadOnly = false;
        dtcom2.Unique = false;
        dtcom2.AllowDBNull = true;

        dtnew.Columns.Add(dtcom2);

        DataColumn dtcom3 = new DataColumn();
        dtcom3.DataType = System.Type.GetType("System.String");
        dtcom3.ColumnName = "EntryNo";
        dtcom3.ReadOnly = false;
        dtcom3.Unique = false;
        dtcom3.AllowDBNull = true;

        dtnew.Columns.Add(dtcom3);

        DataColumn dtcom4 = new DataColumn();
        dtcom4.DataType = System.Type.GetType("System.String");
        dtcom4.ColumnName = "TranAmount";
        dtcom4.ReadOnly = false;
        dtcom4.Unique = false;
        dtcom4.AllowDBNull = true;

        dtnew.Columns.Add(dtcom4);

        DataColumn dtcom5 = new DataColumn();
        dtcom5.DataType = System.Type.GetType("System.String");
        dtcom5.ColumnName = "DueBalance";
        dtcom5.ReadOnly = false;
        dtcom5.Unique = false;
        dtcom5.AllowDBNull = true;

        dtnew.Columns.Add(dtcom5);

        DataColumn dtcom6 = new DataColumn();
        dtcom6.DataType = System.Type.GetType("System.String");
        dtcom6.ColumnName = "EntryTypeId";
        dtcom6.ReadOnly = false;
        dtcom6.Unique = false;
        dtcom6.AllowDBNull = true;

        dtnew.Columns.Add(dtcom6);

        //DataColumn dtcom7 = new DataColumn();
        //dtcom7.DataType = System.Type.GetType("System.String");
        //dtcom7.ColumnName = "SalesOrderNo";
        //dtcom7.ReadOnly = false;
        //dtcom7.Unique = false;
        //dtcom7.AllowDBNull = true;

        //dtnew.Columns.Add(dtcom7);

        //DataColumn dtcom8 = new DataColumn();
        //dtcom8.DataType = System.Type.GetType("System.String");
        //dtcom8.ColumnName = "SalesOrderId";
        //dtcom8.ReadOnly = false;
        //dtcom8.Unique = false;
        //dtcom8.AllowDBNull = true;

        //dtnew.Columns.Add(dtcom8);

        DataColumn dtcom9 = new DataColumn();
        dtcom9.DataType = System.Type.GetType("System.String");
        dtcom9.ColumnName = "TransactionId";
        dtcom9.ReadOnly = false;
        dtcom9.Unique = false;
        dtcom9.AllowDBNull = true;

        dtnew.Columns.Add(dtcom9);

         CheckBox1.Checked = false;
        if (ddlpartynamename.SelectedIndex >0)
        {
            if (RadioButtonList1.SelectedValue == "1")
            {
                GridView2.DataSource = null;
                GridView2.DataBind();

                CheckBox1.Visible = false;
                GridView2.Visible = false;
                chkappamount.Checked = false;
                //controlclear();
                //SqlCommand cmds = new SqlCommand("select SalesOrderId From SalesOrderMaster where PartyId='" + ddlPartyName.SelectedValue + "'", conn);
                SqlCommand cmds = new SqlCommand("select Account From Party_master inner join PartytTypeMaster on Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId where PartyID='" + ddlpartynamename.SelectedValue + "' and Party_master.Whid='" + ddlwarehouse.SelectedValue + "'", conn);

                SqlDataAdapter dtps = new SqlDataAdapter(cmds);
                DataSet dss = new DataSet();
                dtps.Fill(dss);
                //" + ddlPartyName.SelectedValue + "
                if (dss.Tables[0].Rows.Count > 0)
                {
                    SqlCommand cmdt = new SqlCommand("Select Distinct AccountCredit,AmountCredit,TranctionMaster.Tranction_Master_Id,TranctionMaster.Date from TranctionMaster inner join  Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id where AccountCredit='" + dss.Tables[0].Rows[0]["Account"].ToString() + "' and TranctionMaster.Whid='" + ddlwarehouse.SelectedValue + "'", conn);

                    SqlDataAdapter dtpt = new SqlDataAdapter(cmdt);
                    DataSet dst = new DataSet();
                    dtpt.Fill(dst);
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drt in dst.Tables[0].Rows)
                        {
                            //SqlCommand cmdtd = new SqlCommand("select AmountDebit,AmountCredit from Tranction_Details where Tranction_Master_Id ='" + drt["Tranction_Master_Id"].ToString() + "'", conn);
                            string scrt = "SELECT  Distinct    TranctionMaster.EntryTypeId, TranctionMaster.Tranction_Master_Id, TranctionMaster.Date, TranctionMaster.EntryNumber, TranctionMaster.UserId, " +
                      " TranctionMaster.Tranction_Amount,EntryTypeMaster.Entry_Type_Id,EntryTypeMaster.Entry_Type_Name " +
    " FROM         TranctionMaster inner join " +
                      " EntryTypeMaster ON TranctionMaster.EntryTypeId = EntryTypeMaster.Entry_Type_Id where Tranction_Master_Id='" + drt["Tranction_Master_Id"].ToString() + "' and EntryTypeId in ('4','7','8','22','27') and TranctionMaster.Whid = '" + ddlwarehouse.SelectedValue + "'";
                            SqlCommand cmdtd = new SqlCommand(scrt, conn);
                            SqlDataAdapter dtptd = new SqlDataAdapter(cmdtd);
                            DataSet dstd = new DataSet();
                            dtptd.Fill(dstd);
                            if (dstd.Tables[0].Rows.Count > 0)
                            {
                                //double dbamountdebit = 0;
                                //double dbamountcredit = 0;
                                //double Duebalance = 0;
                                //foreach (DataRow dsamt in dstd.Tables[0].Rows)
                                //{

                                //    dbamountdebit  = dbamountdebit + Convert.ToDouble(dsamt["AmountDebit"].ToString());
                                //    dbamountcredit = dbamountcredit + Convert.ToDouble(dsamt["AmountCredit"].ToString());

                                //}
                                //Duebalance = dbamountdebit - dbamountcredit;
                                SqlCommand cmdsupp = new SqlCommand("Select Distinct AmountDue from TranctionMasterSuppliment where Tranction_Master_Id='" + dstd.Tables[0].Rows[0]["Tranction_Master_Id"].ToString() + "'", conn);
                                SqlDataAdapter dtpsupp = new SqlDataAdapter(cmdsupp);
                                DataTable dtsupp = new DataTable();
                                dtpsupp.Fill(dtsupp);
                                if (dtsupp.Rows.Count > 0)
                                {
                                    if (Convert.ToDouble(dtsupp.Rows[0]["AmountDue"].ToString()) > 0)
                                    {


                                        DataRow drnew = dtnew.NewRow();

                                        drnew["Date"] =Convert.ToDateTime( drt["Date"]).ToShortDateString();
                                        //drnew["EntryType"] = drt["Entry_Type_Name"].ToString();
                                        drnew["EntryType"] = dstd.Tables[0].Rows[0]["Entry_Type_Name"].ToString();
                                        //drnew["EntryNo"] = drt["EntryNumber"].ToString();
                                        drnew["EntryNo"] = dstd.Tables[0].Rows[0]["EntryNumber"].ToString();
                                        drnew["TranAmount"] = String.Format("{0:n}", Convert.ToDecimal(drt["AmountCredit"])); 
                                        drnew["DueBalance"] = String.Format("{0:n}", Convert.ToDecimal(dtsupp.Rows[0]["AmountDue"]));
                                        //drnew["EntryTypeId"] = drt["Entry_Type_Id"].ToString();
                                        drnew["EntryTypeId"] = dstd.Tables[0].Rows[0]["Entry_Type_Id"].ToString();
                                        //drnew["SalesOrderNo"] = "";
                                        //drnew["SalesOrderId"] = "";
                                        drnew["TransactionId"] = dstd.Tables[0].Rows[0]["Tranction_Master_Id"].ToString();
                                        dtnew.Rows.Add(drnew);
                                    }
                                }

                            }

                        
                        }
                    }

               
                }
            }
            else
            {

            }
        }
        //if (dsnew == null)
        //{
        if (dtnew.Rows.Count > 0)
        {
            
            GridView2.DataSource = dtnew;
            GridView2.DataBind();
            GridView2.Visible = false;
            Panel4.Visible=true;

        }             
        

    }
    public void entryno()
    {
        
        SqlCommand cmd511 = new SqlCommand("Sp_Select_CashPayment", conn);
        cmd511.CommandType = CommandType.StoredProcedure;
      
        cmd511.Parameters.AddWithValue("@compid", compid);
        cmd511.Parameters.AddWithValue("@whid", ddlwarehouse.SelectedValue);
        SqlDataAdapter adp511 = new SqlDataAdapter(cmd511);
        DataSet ds511 = new DataSet();
        adp511.Fill(ds511);
        if (ds511.Tables[0].Rows.Count > 0)
        {

            if (ds511.Tables[0].Rows[0]["EntryNumber"].ToString() != "")
            {
                ViewState["EntryTypeNumber"] = ds511.Tables[0].Rows[0][0].ToString();
                double a = Convert.ToDouble(ViewState["EntryTypeNumber"]);
                double b = a + 1;
                lbltotentry.Text = a.ToString();
                lblentrynumber.Text = b.ToString();
            }
            else
            {
                lblentrynumber.Text = "1";
              
            }

        }
        else
        {
            lblentrynumber.Text = "1";
        }

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        int accesin = 0;
        DataTable dtdr = select("Select  AccountAddDataRequireTbl.* from AccountAddDataRequireTbl  where  Compid='" + Session["Comid"] + "' and Access='1'");

        if (dtdr.Rows.Count > 0)
        {
            DataTable dtd = select("Select Distinct AccountPageDataAddDesignationRight.* from AccountPageDataAddDesignationRight  where   DesignationId='" + Convert.ToString(Session["DesignationId"]) + "'");

            if (dtd.Rows.Count > 0)
            {
                if (Convert.ToInt32(dtd.Rows[0]["Accessbus"]) == 0)
                {
                    accesin = 1;
                }
                else if (Convert.ToInt32(dtd.Rows[0]["Accessbus"]) == Convert.ToInt32(ddlwarehouse.SelectedValue))
                {
                    accesin = 1;
                }
            }
        }
        else
        {
            accesin = 1;
        }
        //int accesin = ClsAccountAppr.AccessInsert(ddlwarehouse.SelectedValue);

        if (accesin == 1)
        {

            DataTable dtss = (DataTable)select("select Convert(nvarchar,StartDate,101) as StartDate, Convert(nvarchar,EndDate,101) as EndDate from [ReportPeriod] where Active='1' and Whid='" + ddlwarehouse.SelectedValue + "'");

            if (dtss.Rows.Count > 0)
            {
                if (Convert.ToDateTime(txtTodate.Text) >= Convert.ToDateTime(dtss.Rows[0]["StartDate"]) && Convert.ToDateTime(txtTodate.Text) <= Convert.ToDateTime(dtss.Rows[0]["EndDate"]))
                {
                    bool access = UserAccess.Usercon("TranctionMaster", "", "Tranction_Master_Id", "", "", "compid", "TranctionMaster");
                    if (access == true)
                    {
                        string date = "select Convert(nvarchar,StartDate,101) as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Whid='" + ddlwarehouse.SelectedValue + "' and Active='1'";
                        SqlCommand cmd = new SqlCommand(date, conn);
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        //txtdate.Text = Convert.ToString(System.DateTime.Now.Date.ToShortDateString());
                        if (dt.Rows.Count > 0)
                        {
                            if (Convert.ToDateTime(txtTodate.Text) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                            {
                                lblstartdate.Text = dt.Rows[0][0].ToString();
                                ModalPopupExtender1222.Show();

                            }

                            else
                            {


                                if (RadioButtonList1.SelectedValue == "1")
                                {
                                    SqlCommand cmd511 = new SqlCommand("Sp_Select_CashPayment", conn);
                                    cmd511.CommandType = CommandType.StoredProcedure;
                                    cmd511.Parameters.AddWithValue("@compid", compid);
                                    cmd511.Parameters.AddWithValue("@whid", ddlwarehouse.SelectedValue);
                                    // cmd.Parameters.AddWithValue("@GroupId", Convert.ToInt32(ddlGroupName1.SelectedValue));
                                    SqlDataAdapter adp511 = new SqlDataAdapter(cmd511);

                                    DataSet ds511 = new DataSet();
                                    adp511.Fill(ds511);
                                    if (ds511.Tables[0].Rows.Count > 0)
                                    {
                                        if (ds511.Tables[0].Rows[0]["EntryNumber"].ToString() != "")
                                        {

                                            db = Convert.ToDouble(ds511.Tables[0].Rows[0][0].ToString());
                                            db = db + 1;
                                        }
                                        else
                                        {
                                            db = 1;
                                        }
                                    }
                                    else
                                    {
                                        db = 1;
                                    }

                                    if (chkappamount.Checked == true && CheckBox1.Checked == true)
                                    {
                                        if (GridView2.Rows.Count > 0)
                                        {
                                            SqlCommand cmdin = new SqlCommand("Insert into TranctionMaster values('" + txtTodate.Text + "','" + db + "','1','" + Session["UserId"].ToString() + "','" + Math.Round(Convert.ToDecimal(txtpayamount.Text), 2, MidpointRounding.AwayFromZero) + "','" + Session["comid"] + "','" + ddlwarehouse.SelectedValue + "')", conn);
                                            if (conn.State.ToString() != "Open")
                                            {
                                                conn.Open();
                                            }
                                            cmdin.ExecuteNonQuery();
                                            conn.Close();

                                            SqlCommand cmdint = new SqlCommand("Select Max(Tranction_Master_Id) as TMID from TranctionMaster inner  join EntryTypeMaster on TranctionMaster.EntryTypeId = EntryTypeMaster.Entry_Type_Id where TranctionMaster.compid = '" + compid + "' and TranctionMaster.Whid='" + ddlwarehouse.SelectedValue + "'", conn);
                                            SqlDataAdapter dtpint = new SqlDataAdapter(cmdint);
                                            DataTable dtint = new DataTable();
                                            dtpint.Fill(dtint);

                                            if (dtint.Rows.Count > 0)
                                            {
                                                ViewState["tid"] = dtint.Rows[0]["TMID"].ToString();
                                                SqlCommand cmds = new SqlCommand("select Account From Party_master inner join PartytTypeMaster  on Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId where PartyID='" + ddlpartynamename.SelectedValue + "' and PartytTypeMaster.compid = '" + compid + "' and Party_master.Whid='" + ddlwarehouse.SelectedValue + "'", conn);

                                                SqlDataAdapter dtps = new SqlDataAdapter(cmds);
                                                DataSet dss = new DataSet();
                                                dtps.Fill(dss);
                                                if (dss.Tables[0].Rows.Count > 0)
                                                {
                                                    SqlCommand cmdtd = new SqlCommand("Insert into Tranction_Details values('0','" + ddlAccount.SelectedValue + "','0','" + Math.Round(Convert.ToDecimal(txtpayamount.Text), 2, MidpointRounding.AwayFromZero) + "','" + dtint.Rows[0]["TMID"].ToString() + "','" + txtmmo.Text + "','" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "','0','0','" + compid + "','" + ddlwarehouse.SelectedValue + "')", conn);
                                                    conn.Open();
                                                    cmdtd.ExecuteNonQuery();
                                                    conn.Close();

                                                    SqlCommand cmdtdi = new SqlCommand("Insert into Tranction_Details values('" + dss.Tables[0].Rows[0]["Account"].ToString() + "','0','" + Math.Round(Convert.ToDecimal(txtpayamount.Text), 2, MidpointRounding.AwayFromZero) + "','0','" + dtint.Rows[0]["TMID"].ToString() + "','" + txtmmo.Text + "','" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "','0','0','" + compid + "','" + ddlwarehouse.SelectedValue + "')", conn);
                                                    conn.Open();
                                                    cmdtdi.ExecuteNonQuery();
                                                    conn.Close();


                                                    SqlCommand cmdtrn = new SqlCommand("insert into TranctionMasterSuppliment(Tranction_Master_Id,Memo,Party_MasterId) values('" + dtint.Rows[0]["TMID"].ToString() + "','" + txtmmo.Text + "','" + ddlpartynamename.SelectedValue + "')", conn);
                                                    if (conn.State.ToString() != "Open")
                                                    {
                                                        conn.Open();
                                                    }
                                                    cmdtrn.ExecuteNonQuery();
                                                    conn.Close();
                                                    int k = 0;
                                                    double counter = 0;
                                                    foreach (GridViewRow grdv in GridView2.Rows)
                                                    {
                                                        //Label enttypeid = (Label)(grdv.FindControl("lblentrytypeid"));
                                                        Label entno = (Label)(grdv.FindControl("lblentryNo"));//EntryNo
                                                        TextBox txtamt = (TextBox)(grdv.FindControl("txtnewamt"));
                                                        Label trnid = (Label)(grdv.FindControl("lbltranid"));
                                                        Label lblamtdue = (Label)(grdv.FindControl("lblbaldue"));
                                                        //Label lblsalNo = (Label)(grdv.FindControl("lblsalesorderNo"));
                                                        //Label lblsalId = (Label)(grdv.FindControl("lblsalesorderId"));
                                                        CheckBox chk = (CheckBox)(grdv.FindControl("chkbox"));
                                                        if (chk.Checked == true)
                                                        {


                                                            if (txtamt.Text != "")
                                                            {
                                                                if (txtamt.Text.ToString() != "")
                                                                {
                                                                    if (Convert.ToDouble(txtamt.Text) > Convert.ToDouble(lblamtdue.Text))
                                                                    {
                                                                        txtamt.Text = lblamtdue.Text;
                                                                    }
                                                                }
                                                                counter += Convert.ToDouble(txtamt.Text);
                                                                if (counter > Convert.ToDouble(Math.Round(Convert.ToDecimal(txtpayamount.Text), 2, MidpointRounding.AwayFromZero)))
                                                                {
                                                                    if (k == 0)
                                                                    {
                                                                        txtamt.Text = (counter - Convert.ToDouble(Math.Round(Convert.ToDecimal(txtpayamount.Text), 2, MidpointRounding.AwayFromZero))).ToString();
                                                                        counter = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtpayamount.Text), 2, MidpointRounding.AwayFromZero));
                                                                        k += 1;
                                                                    }
                                                                }
                                                                if (counter <= Convert.ToDouble(Math.Round(Convert.ToDecimal(txtpayamount.Text), 2, MidpointRounding.AwayFromZero)))
                                                                {
                                                                    //SqlCommand cmdfind = new SqlCommand("select AmountDue from TranctionMasterSuppliment where Tranction_Master_Id ='" + dtint.Rows[0]["TMID"].ToString() + "' and AmountDue > 0", conn);
                                                                    SqlCommand cmdfind = new SqlCommand("select AmountDue from TranctionMasterSuppliment where Tranction_Master_Id ='" + trnid.Text + "' and AmountDue > 0", conn);
                                                                    SqlDataAdapter dtpfind = new SqlDataAdapter(cmdfind);
                                                                    DataTable dtfind = new DataTable();
                                                                    dtpfind.Fill(dtfind);

                                                                    if (dtfind.Rows.Count > 0)
                                                                    {
                                                                        if (Convert.ToDouble(dtfind.Rows[0]["AmountDue"].ToString()) > 0)
                                                                        {
                                                                            double amt;
                                                                            if (Convert.ToDouble(txtamt.Text) >= Convert.ToDouble(dtfind.Rows[0]["AmountDue"].ToString()))
                                                                            {
                                                                                amt = 0;
                                                                            }
                                                                            else
                                                                            {
                                                                                amt = Convert.ToDouble(dtfind.Rows[0]["AmountDue"].ToString()) - (Convert.ToDouble(txtamt.Text));
                                                                            }

                                                                            SqlCommand cmdamtup = new SqlCommand("Update TranctionMasterSuppliment set AmountDue = '" + amt + "' where Tranction_Master_Id ='" + trnid.Text + "' and AmountDue > 0", conn);
                                                                            conn.Open();
                                                                            cmdamtup.ExecuteNonQuery();
                                                                            conn.Close();

                                                                            SqlCommand cmdinspay = new SqlCommand("Insert into PaymentApplicationTbl values('" + dtint.Rows[0]["TMID"].ToString() + "','" + trnid.Text + "','" + txtamt.Text + "','" + txtTodate.Text + "','" + Session["UserId"].ToString() + "','0')", conn);
                                                                            conn.Open();
                                                                            cmdinspay.ExecuteNonQuery();
                                                                            conn.Close();

                                                                        }
                                                                    }

                                                                }

                                                            }
                                                        }
                                                    }
                                                    DataTable dtapprequirment = ClsAccountAppr.Apprreuqired();
                                                    if (dtapprequirment.Rows.Count > 0)
                                                    {

                                                        ClsAccountAppr.AccountAppMaster(ddlwarehouse.SelectedValue, ViewState["tid"].ToString(), chkappentry.Checked);
                                                    }
                                                    lblmsg.Visible = true;
                                                    lblmsg.Text = "";
                                                    lblmsg.Text = "Record inserted successfully";
                                                    txtmmo.Text = "";
                                                    CheckBox1.Checked = false;
                                                    CheckBox1.Visible = false;
                                                    GridView2.DataSource = null;
                                                    GridView2.DataBind();
                                                    entryno();
                                                    GridView2.Visible = false;
                                                    chkappamount.Checked = false;
                                                    // ddlpartytypetype.Items.Clear();
                                                    // ddlpartynamename.Items.Clear();
                                                    // RadioButtonList1.Items[0].Selected = false;
                                                    // RadioButtonList1.Items[1].Selected = false;
                                                    txtpayamount.Text = "";
                                                    if (Request.QueryString["docid"] != null)
                                                    {
                                                        inserdocatt();
                                                    }
                                                    else
                                                    {
                                                        if (chkdoc.Checked == true)
                                                        {
                                                            entry();

                                                            //filldoc();
                                                        }
                                                    }


                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        SqlCommand cmdin = new SqlCommand("Insert into TranctionMaster values('" + txtTodate.Text + "','" + db + "','1','" + Session["UserId"].ToString() + "','" + Math.Round(Convert.ToDecimal(txtpayamount.Text), 2, MidpointRounding.AwayFromZero) + "','" + Session["comid"] + "','" + ddlwarehouse.SelectedValue + "')", conn);

                                        if (conn.State.ToString() != "Open")
                                        {
                                            conn.Open();
                                        }
                                        cmdin.ExecuteNonQuery();
                                        conn.Close();
                                        SqlCommand cmdint = new SqlCommand("Select Max(Tranction_Master_Id) as TMID from TranctionMaster inner  join EntryTypeMaster on TranctionMaster.EntryTypeId = EntryTypeMaster.Entry_Type_Id where TranctionMaster.compid = '" + compid + "' and TranctionMaster.Whid='" + ddlwarehouse.SelectedValue + "'", conn);

                                        SqlDataAdapter dtpint = new SqlDataAdapter(cmdint);
                                        DataTable dtint = new DataTable();
                                        dtpint.Fill(dtint);

                                        if (dtint.Rows.Count > 0)
                                        {
                                            ViewState["tid"] = dtint.Rows[0]["TMID"].ToString();
                                            SqlCommand cmds = new SqlCommand("select Account From Party_master inner join PartytTypeMaster  on Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId where PartyID='" + ddlpartynamename.SelectedValue + "' and PartytTypeMaster.compid = '" + compid + "' and Party_master.Whid='" + ddlwarehouse.SelectedValue + "'", conn);

                                            SqlDataAdapter dtps = new SqlDataAdapter(cmds);
                                            DataSet dss = new DataSet();
                                            dtps.Fill(dss);
                                            if (dss.Tables[0].Rows.Count > 0)
                                            {
                                                SqlCommand cmdtd = new SqlCommand("Insert into Tranction_Details values('0','" + ddlAccount.SelectedValue + "','0','" + Math.Round(Convert.ToDecimal(txtpayamount.Text), 2, MidpointRounding.AwayFromZero) + "','" + dtint.Rows[0]["TMID"].ToString() + "','" + txtmmo.Text + "','" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "','0','0','" + compid + "','" + ddlwarehouse.SelectedValue + "')", conn);
                                                conn.Open();
                                                cmdtd.ExecuteNonQuery();
                                                conn.Close();

                                                SqlCommand cmdtdi = new SqlCommand("Insert into Tranction_Details values('" + dss.Tables[0].Rows[0]["Account"].ToString() + "','0','" + Math.Round(Convert.ToDecimal(txtpayamount.Text), 2, MidpointRounding.AwayFromZero) + "','0','" + dtint.Rows[0]["TMID"].ToString() + "','" + txtmmo.Text + "','" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "','0','0','" + compid + "','" + ddlwarehouse.SelectedValue + "')", conn);
                                                conn.Open();
                                                cmdtdi.ExecuteNonQuery();
                                                conn.Close();

                                                SqlCommand cmdtrn = new SqlCommand("insert into TranctionMasterSuppliment(Tranction_Master_Id,Memo,Party_MasterId) values('" + dtint.Rows[0]["TMID"].ToString() + "','" + txtmmo.Text + "','" + ddlpartynamename.SelectedValue + "')", conn);
                                                if (conn.State.ToString() != "Open")
                                                {
                                                    conn.Open();
                                                }
                                                cmdtrn.ExecuteNonQuery();
                                                conn.Close();

                                                SqlCommand cmdfind = new SqlCommand("select AmountDue from TranctionMasterSuppliment where Tranction_Master_Id ='" + dtint.Rows[0]["TMID"].ToString() + "' and AmountDue > 0", conn);
                                                SqlDataAdapter dtpfind = new SqlDataAdapter(cmdfind);
                                                DataTable dtfind = new DataTable();
                                                dtpfind.Fill(dtfind);

                                                if (dtfind.Rows.Count > 0)
                                                {
                                                    if (Convert.ToDouble(dtfind.Rows[0]["AmountDue"].ToString()) > 0)
                                                    {
                                                        double amt;
                                                        if (Convert.ToDouble(Math.Round(Convert.ToDecimal(txtpayamount.Text), 2, MidpointRounding.AwayFromZero)) > Convert.ToDouble(dtfind.Rows[0]["AmountDue"].ToString()))
                                                        {
                                                            amt = 0;
                                                        }
                                                        else
                                                        {
                                                            amt = Convert.ToDouble(dtfind.Rows[0]["AmountDue"].ToString()) - (Convert.ToDouble(Math.Round(Convert.ToDecimal(txtpayamount.Text), 2, MidpointRounding.AwayFromZero)));
                                                        }

                                                        SqlCommand cmdamtup = new SqlCommand("Update TranctionMasterSuppliment set AmountDue = '" + amt + "' where Tranction_Master_Id ='" + dtint.Rows[0]["TMID"].ToString() + "' and AmountDue > 0", conn);
                                                        conn.Open();
                                                        cmdamtup.ExecuteNonQuery();
                                                        conn.Close();



                                                    }
                                                }
                                                SqlCommand cmdinspay = new SqlCommand("Insert into PaymentApplicationTbl values('" + dtint.Rows[0]["TMID"].ToString() + "','0','" + Math.Round(Convert.ToDecimal(txtpayamount.Text), 2, MidpointRounding.AwayFromZero) + "','" + txtTodate.Text + "','" + Session["UserId"].ToString() + "','0')", conn);
                                                conn.Open();
                                                cmdinspay.ExecuteNonQuery();
                                                conn.Close();


                                            }
                                            if (Request.QueryString["docid"] != null)
                                            {
                                                inserdocatt();
                                            }
                                            else
                                            {
                                                if (chkdoc.Checked == true)
                                                {
                                                    entry();
                                                    // ModalPopupExtender1.Show();
                                                    //filldoc();
                                                }
                                            }
                                        }
                                        DataTable dtapprequirment = ClsAccountAppr.Apprreuqired();
                                        if (dtapprequirment.Rows.Count > 0)
                                        {

                                            ClsAccountAppr.AccountAppMaster(ddlwarehouse.SelectedValue, ViewState["tid"].ToString(), chkappentry.Checked);
                                        }
                                        lblmsg.Visible = true;
                                        lblmsg.Text = "";
                                        lblmsg.Text = "Record inserted successfully";
                                        txtmmo.Text = "";
                                        CheckBox1.Checked = false;
                                        GridView2.DataSource = null;
                                        GridView2.DataBind();
                                        GridView2.Visible = false;
                                        chkappamount.Checked = false;

                                        txtpayamount.Text = "";

                                        entryno();
                                    }



                                }
                            }
                        }
                    }
                    else
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "";
                        lblmsg.Text = "Sorry, You are not permitted to insert greater record as per Price plan";
                    }
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Please check your date. You cannot select any date earlier/later than start/end date of the year";
                }
            }
            
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "You are not permited to insert record for this page.";

        }
    }
    protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
   
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (ddlpartynamename.SelectedIndex>0)
        {
            if (CheckBox1.Checked == true)
            {
                GridView2.Visible = true;
            }
            else
            {
                GridView2.Visible = false;
            }
        }
    }
    protected void ddlAccountname1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAccountname1.SelectedIndex > 0)
        {
            if (txtpayamount.Text != "")
            {
                xz = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtpayamount.Text),2,MidpointRounding.AwayFromZero));
            }
            if (xz <= 0)
            {
                txtAmount1.Text = xz.ToString();
                x10 = xz;
                lblgtotal.Text = "";
                lblgtotal.Text = x10.ToString();
            }
            else
            {
                txtAmount1.Text = xz.ToString();
                if (xz > 0)
                {
                    x10 = xz;
                    lblgtotal.Text = "";
                    lblgtotal.Text = xz.ToString();
                  
                }
            }
            if (txtAmount1.Text.Length <= 0)
            {
                txtAmount1.Text = "0";
            }

            
        }
    }
    protected void ddlAccountname2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAccountname2.SelectedIndex > 0)
        {
            if (txtpayamount.Text != "")
            {
                xz = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtpayamount.Text),2,MidpointRounding.AwayFromZero));
                x1 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount1.Text),2,MidpointRounding.AwayFromZero));
            }
            if (x1 > 0)
            {
                x2 = Math.Round(xz - (x1), 2, MidpointRounding.AwayFromZero);
                //x2 = xz - x1;
                
                if (x2 > 0)
                {
                    txtAmount2.Text = x2.ToString();
                    x10 = x1 + x2;
                    lblgtotal.Text = "";
                    lblgtotal.Text = x10.ToString();
                    txtmemo2.Text = txtmemo1.Text;
                }
            }
            if (txtAmount2.Text.Length <= 0)
            {
                txtAmount2.Text = "0";
            }
        }
    }
    protected void ddlAccountname3_SelectedIndexChanged(object sender, EventArgs e)
    {
       

        if (ddlAccountname3.SelectedIndex > 0)
        {
            if (txtpayamount.Text != "")
            {
                xz = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtpayamount.Text), 2, MidpointRounding.AwayFromZero));
                x1 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount1.Text), 2, MidpointRounding.AwayFromZero));
                x2 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount2.Text), 2, MidpointRounding.AwayFromZero).ToString());
              
            }
            if (x2 > 0)
            {
                x3 = Math.Round(xz - (x1 + x2), 2, MidpointRounding.AwayFromZero);
                if (x3 > 0)
                {

                    txtAmount3.Text = x3.ToString();
                    x10 = x1 + x2 + x3 ;
                    lblgtotal.Text = "";
                    lblgtotal.Text = x10.ToString();
                    txtmemo3.Text = txtmemo2.Text;
                }
            }
            if (txtAmount3.Text.Length <= 0)
            {
                txtAmount3.Text = "0";
            }
        }
    }
    protected void ddlAccountname4_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAccountname4.SelectedIndex > 0)
        {
            if (txtpayamount.Text != "")
            {
                xz = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtpayamount.Text),2,MidpointRounding.AwayFromZero));
                x1 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount1.Text),2,MidpointRounding.AwayFromZero));
                x2 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount2.Text),2,MidpointRounding.AwayFromZero).ToString());
                x3 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount3.Text),2,MidpointRounding.AwayFromZero).ToString());

            }
            if (x3 > 0)
            {
                x4 = Math.Round(xz - (x1 + x2 + x3), 2, MidpointRounding.AwayFromZero);
                //x4 = xz - x1 - x2 - x3;
                if (x4 > 0)
                {

                    txtAmount4.Text = x4.ToString();
                    x10 = x1 + x2 + x3 + x4;
                    lblgtotal.Text = "";
                    lblgtotal.Text = x10.ToString();
                    txtmemo4.Text = txtmemo3.Text;
                }
            }
            if (txtAmount4.Text.Length <= 0)
            {
                txtAmount4.Text = "0";
            }
        }
    }
    protected void ddlAccountname5_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAccountname5.SelectedIndex > 0)
        {
            if (txtpayamount.Text != "")
            {
                xz = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtpayamount.Text),2,MidpointRounding.AwayFromZero));
                x1 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount1.Text),2,MidpointRounding.AwayFromZero));
                x2 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount2.Text),2,MidpointRounding.AwayFromZero).ToString());
                x3 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount3.Text),2,MidpointRounding.AwayFromZero).ToString());
                x4 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount4.Text),2,MidpointRounding.AwayFromZero).ToString());
            }
            if (x4 > 0)
            {
                x5 = Math.Round(xz - (x1 + x2 + x3 + x4), 2, MidpointRounding.AwayFromZero);
                //x5 = xz - x1 - x2 - x3 - x4;
                if (x5 > 0)
                {

                    txtAmount5.Text = x5.ToString();
                    x10 = x1 + x2 + x3 + x4 + x5;
                    lblgtotal.Text = "";
                    lblgtotal.Text = x10.ToString();
                    txtmemo5.Text = txtmemo3.Text;
                }
            }
            if (txtAmount5.Text.Length <= 0)
            {
                txtAmount5.Text = "0";
            }
        }
    }
    protected void ddlAccountname6_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAccountname6.SelectedIndex > 0)
        {
            if (txtpayamount.Text != "")
            {
                xz = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtpayamount.Text),2,MidpointRounding.AwayFromZero));
                x1 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount1.Text),2,MidpointRounding.AwayFromZero));
                x2 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount2.Text),2,MidpointRounding.AwayFromZero).ToString());
                x3 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount3.Text),2,MidpointRounding.AwayFromZero).ToString());
                x4 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount4.Text),2,MidpointRounding.AwayFromZero).ToString());
                x5 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount5.Text),2,MidpointRounding.AwayFromZero).ToString());
            }
            if (x5 > 0)
            {
                x6 = Math.Round(xz - (x1 + x2 + x3 + x4 + x5), 2, MidpointRounding.AwayFromZero);
          
                //x6 = xz - x1 - x2 - x3 - x4 - x5;
                if (x6 > 0)
                {

                    txtAmount6.Text = x6.ToString();
                    x10 = x1 + x2 + x3 + x4 + x5 + x6;
                    lblgtotal.Text = "";
                    lblgtotal.Text = x10.ToString();
                    txtmemo6.Text = txtmemo5.Text;
                }
            }
            if (txtAmount6.Text.Length <= 0)
            {
                txtAmount6.Text = "0";
            }
        }
    }
    protected void ddlAccountname7_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAccountname7.SelectedIndex > 0)
        {
            if (txtpayamount.Text != "")
            {
                xz = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtpayamount.Text),2,MidpointRounding.AwayFromZero));
                x1 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount1.Text),2,MidpointRounding.AwayFromZero));
                x2 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount2.Text),2,MidpointRounding.AwayFromZero).ToString());
                x3 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount3.Text),2,MidpointRounding.AwayFromZero).ToString());
                x4 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount4.Text),2,MidpointRounding.AwayFromZero).ToString());
                x5 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount5.Text),2,MidpointRounding.AwayFromZero).ToString());
                x6 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount6.Text),2,MidpointRounding.AwayFromZero).ToString());
            }
            if (x6 > 0)
            {
                x7 = Math.Round(xz - (x1 + x2 + x3 + x4 + x5 + x6), 2, MidpointRounding.AwayFromZero);
          
                //x7 = xz - x1 - x2 - x3 - x4 - x5 - x6;
                if (x7 > 0)
                {

                    txtAmount7.Text = x7.ToString();
                    x10 = x1 + x2 + x3 + x4 + x5 + x6 + x7;
                    lblgtotal.Text = "";
                    lblgtotal.Text = x10.ToString();
                    txtmemo7.Text = txtmemo6.Text;
                }
            }
            if (txtAmount7.Text.Length <= 0)
            {
                txtAmount7.Text = "0";
            }
        }
    }
    protected void ddlAccountname8_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAccountname8.SelectedIndex > 0)
        {
            if (txtpayamount.Text != "")
            {
                xz = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtpayamount.Text),2,MidpointRounding.AwayFromZero));
                x1 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount1.Text),2,MidpointRounding.AwayFromZero));
                x2 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount2.Text),2,MidpointRounding.AwayFromZero).ToString());
                x3 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount3.Text),2,MidpointRounding.AwayFromZero).ToString());
                x4 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount4.Text),2,MidpointRounding.AwayFromZero).ToString());
                x5 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount5.Text),2,MidpointRounding.AwayFromZero).ToString());
                x6 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount6.Text),2,MidpointRounding.AwayFromZero).ToString());
                x7 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount7.Text),2,MidpointRounding.AwayFromZero).ToString());
            }
            if (x7 > 0)
            {
                x8 = Math.Round(xz - (x1 + x2 + x3 + x4 + x5 + x6 + x7), 2, MidpointRounding.AwayFromZero);
          
                //x8 = xz - x1 - x2 - x3 - x4 - x5 - x6 - x7;
                if (x8 > 0)
                {

                    txtAmount8.Text = x8.ToString();
                    x10 = x1 + x2 + x3 + x4 + x5 + x6 + x7 + x8;
                    lblgtotal.Text = "";
                    lblgtotal.Text = x10.ToString();
                    txtmemo8.Text = txtmemo7.Text;
                }
            }
            if (txtAmount8.Text.Length <= 0)
            {
                txtAmount8.Text = "0";
            }
        }
    }
    protected void ddlAccountname9_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAccountname9.SelectedIndex > 0)
        {
            if (txtpayamount.Text != "")
            {
                xz = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtpayamount.Text),2,MidpointRounding.AwayFromZero));
                x1 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount1.Text),2,MidpointRounding.AwayFromZero));
                x2 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount2.Text),2,MidpointRounding.AwayFromZero).ToString());
                x3 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount3.Text),2,MidpointRounding.AwayFromZero).ToString());
                x4 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount4.Text),2,MidpointRounding.AwayFromZero).ToString());
                x5 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount5.Text),2,MidpointRounding.AwayFromZero).ToString());
                x6 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount6.Text),2,MidpointRounding.AwayFromZero).ToString());
                x7 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount7.Text),2,MidpointRounding.AwayFromZero).ToString());
                x8 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount7.Text),2,MidpointRounding.AwayFromZero).ToString());
            }
            if (x8 > 0)
            {
                x9 = Math.Round(xz - (x1 + x2 + x3 + x4 + x5 + x6 + x7 + x8), 2, MidpointRounding.AwayFromZero);
          
                //x9 = xz - x1 - x2 - x3 - x4 - x5 - x6 - x7 - x8;
                if (x9 > 0)
                {

                    txtAmount9.Text = x9.ToString();
                    x10 = x1 + x2 + x3 + x4 + x5 + x6 + x7 + x8 + x9;
                    lblgtotal.Text = "";
                    lblgtotal.Text = x10.ToString();
                    txtmemo9.Text = txtmemo8.Text;
                }
            }
            if (txtAmount9.Text.Length <= 0)
            {
                txtAmount9.Text = "0";
            }
        }
    }
    protected void btnsubradio2_Click(object sender, EventArgs e)
    {
        int accesin = 0;
        DataTable dtdr = select("Select  AccountAddDataRequireTbl.* from AccountAddDataRequireTbl  where  Compid='" + Session["Comid"] + "' and Access='1'");

        if (dtdr.Rows.Count > 0)
        {
            DataTable dtd = select("Select Distinct AccountPageDataAddDesignationRight.* from AccountPageDataAddDesignationRight  where   DesignationId='" + Convert.ToString(Session["DesignationId"]) + "'");

            if (dtd.Rows.Count > 0)
            {
                if (Convert.ToInt32(dtd.Rows[0]["Accessbus"]) == 0)
                {
                    accesin = 1;
                }
                else if (Convert.ToInt32(dtd.Rows[0]["Accessbus"]) == Convert.ToInt32(ddlwarehouse.SelectedValue))
                {
                    accesin = 1;
                }
            }
        }
        else
        {
            accesin = 1;
        }
        //int accesin = ClsAccountAppr.AccessInsert(ddlwarehouse.SelectedValue);

        if (accesin == 1)
        {
            DataTable dtss = (DataTable)select("select Convert(nvarchar,StartDate,101) as StartDate, Convert(nvarchar,EndDate,101) as EndDate from [ReportPeriod] where Active='1' and Whid='" + ddlwarehouse.SelectedValue + "'");

            if (dtss.Rows.Count > 0)
            {
                if (Convert.ToDateTime(txtTodate.Text) >= Convert.ToDateTime(dtss.Rows[0]["StartDate"]) && Convert.ToDateTime(txtTodate.Text) <= Convert.ToDateTime(dtss.Rows[0]["EndDate"]))
                {
                    bool access = UserAccess.Usercon("TranctionMaster", "", "Tranction_Master_Id", "", "", "compid", "TranctionMaster");
                    if (access == true)
                    {

                        string date = "select Convert(nvarchar,StartDate,101) as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Whid='" + ddlwarehouse.SelectedValue + "' and Active='1'";
                        SqlCommand cmd = new SqlCommand(date, conn);
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        //txtdate.Text = Convert.ToString(System.DateTime.Now.Date.ToShortDateString());
                        if (dt.Rows.Count > 0)
                        {
                            if (Convert.ToDateTime(txtTodate.Text) < Convert.ToDateTime(dt.Rows[0][0].ToString()))
                            {
                                lblstartdate.Text = dt.Rows[0][0].ToString();
                                ModalPopupExtender1222.Show();

                            }

                            else
                            {
                                double a, b, c, d, x, f, g, h, i;
                                if (txtAmount1.Text == "" || ddlAccountname1.SelectedIndex == 0)
                                { a = 0; }
                                else { a = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount1.Text), 2, MidpointRounding.AwayFromZero)); }
                                if (txtAmount2.Text == "" || ddlAccountname2.SelectedIndex == 0)
                                { b = 0; }
                                else { b = Convert.ToDouble(txtAmount2.Text); }
                                if (txtAmount3.Text == "" || ddlAccountname3.SelectedIndex == 0)
                                { c = 0; }
                                else { c = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount3.Text), 2, MidpointRounding.AwayFromZero).ToString()); }
                                if (txtAmount4.Text == "" || ddlAccountname4.SelectedIndex == 0)
                                { d = 0; }
                                else { d = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount4.Text), 2, MidpointRounding.AwayFromZero).ToString()); }
                                if (txtAmount5.Text == "" || ddlAccountname5.SelectedIndex == 0)
                                { x = 0; }
                                else { x = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount5.Text), 2, MidpointRounding.AwayFromZero).ToString()); }
                                if (txtAmount6.Text == "" || ddlAccountname6.SelectedIndex == 0)
                                { f = 0; }
                                else { f = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount6.Text), 2, MidpointRounding.AwayFromZero).ToString()); }
                                if (txtAmount7.Text == "" || ddlAccountname7.SelectedIndex == 0)
                                { g = 0; }
                                else { g = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount7.Text), 2, MidpointRounding.AwayFromZero).ToString()); }
                                if (txtAmount8.Text == "" || ddlAccountname8.SelectedIndex == 0)
                                { h = 0; }
                                else { h = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount8.Text), 2, MidpointRounding.AwayFromZero).ToString()); }
                                if (txtAmount9.Text == "" || ddlAccountname9.SelectedIndex == 0)
                                { i = 0; }
                                else { i = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount9.Text), 2, MidpointRounding.AwayFromZero).ToString()); }

                                double y = Math.Round((a + b + c + d + x + f + g + h + i), 2, MidpointRounding.AwayFromZero);

                                double z = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtpayamount.Text), 2, MidpointRounding.AwayFromZero));
                                if (z == y)
                                {
                                    SqlCommand cmd5111 = new SqlCommand("Sp_Select_CashPayment", conn);
                                    cmd5111.CommandType = CommandType.StoredProcedure;
                                    cmd5111.Parameters.AddWithValue("@compid", compid);
                                    cmd5111.Parameters.AddWithValue("@whid", ddlwarehouse.SelectedValue);
                                    // cmd.Parameters.AddWithValue("@GroupId", Convert.ToInt32(ddlGroupName1.SelectedValue));
                                    SqlDataAdapter adp5111 = new SqlDataAdapter(cmd5111);
                                    DataSet ds5111 = new DataSet();
                                    adp5111.Fill(ds5111);
                                    if (ds5111.Tables[0].Rows.Count > 0)
                                    {
                                        if (ds5111.Tables[0].Rows[0]["EntryNumber"].ToString() != "")
                                        {

                                            db = Convert.ToDouble(ds5111.Tables[0].Rows[0][0].ToString());
                                            db = db + 1;
                                        }
                                        else
                                        {
                                            db = 1;
                                        }
                                    }
                                    else
                                    {
                                        db = 1;
                                    }
                                    SqlCommand cmdins = new SqlCommand("insert into TranctionMaster(Date,EntryNumber,EntryTypeId,UserId,Tranction_Amount,compid,Whid) values('" + txtTodate.Text + "','" + db + "','1','" + Convert.ToString(Session["UserId"]) + "','" + Math.Round(Convert.ToDecimal(txtpayamount.Text), 2, MidpointRounding.AwayFromZero) + "','" + Session["comid"] + "','" + ddlwarehouse.SelectedValue + "')", conn);
                                    if (conn.State.ToString() != "Open")
                                    {
                                        conn.Open();
                                    }
                                    cmdins.ExecuteNonQuery();
                                    conn.Close();

                                    SqlCommand cmdmaxtid = new SqlCommand("Select Max(Tranction_Master_Id) as TMID from TranctionMaster left outer join EntryTypeMaster on TranctionMaster.EntryTypeId = EntryTypeMaster.Entry_Type_Id where TranctionMaster.compid = '" + compid + "' and TranctionMaster.Whid='" + ddlwarehouse.SelectedValue + "' ", conn);
                                    SqlDataAdapter dtpmaxid = new SqlDataAdapter(cmdmaxtid);
                                    DataTable dtmaxid = new DataTable();

                                    dtpmaxid.Fill(dtmaxid);
                                    if (dtmaxid.Rows.Count > 0)
                                    {
                                        ViewState["tid"] = dtmaxid.Rows[0]["TMID"].ToString();
                                        SqlCommand instd = new SqlCommand("Insert into Tranction_Details(AccountDebit,AccountCredit,AmountDebit,AmountCredit,Tranction_Master_Id,Memo,DateTimeofTransaction,compid,whid) values('0','" + ddlAccount.SelectedValue + "','0','" + Math.Round(Convert.ToDecimal(txtpayamount.Text), 2, MidpointRounding.AwayFromZero) + "','" + dtmaxid.Rows[0]["TMID"].ToString() + "','" + txtmmo.Text + "','" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "','" + compid + "','" + ddlwarehouse.SelectedValue + "')", conn);
                                        conn.Open();
                                        instd.ExecuteNonQuery();
                                        conn.Close();

                                        SqlCommand cmdtrn = new SqlCommand("insert into TranctionMasterSuppliment(Tranction_Master_Id,Memo,Party_MasterId) values('" + dtmaxid.Rows[0]["TMID"].ToString() + "','" + txtmmo.Text + "','0')", conn);
                                        conn.Open();
                                        cmdtrn.ExecuteNonQuery();
                                        conn.Close();
                                    }



                                    if (ddlAccountname1.SelectedIndex > 0 && txtAmount1.Text != "")
                                    {
                                        insert(Math.Round(Convert.ToDecimal(txtAmount1.Text), 2, MidpointRounding.AwayFromZero).ToString(), ddlAccountname1.SelectedValue, txtmemo1.Text, dtmaxid.Rows[0]["TMID"].ToString());
                                        //update account master balance fields


                                    }
                                    if (ddlAccountname2.SelectedIndex > 0 && txtAmount2.Text != "")
                                    {
                                        insert(Math.Round(Convert.ToDecimal(txtAmount2.Text), 2, MidpointRounding.AwayFromZero).ToString(), ddlAccountname2.SelectedValue, txtmemo2.Text, dtmaxid.Rows[0]["TMID"].ToString());

                                        //update account master balance fields

                                    }
                                    if (ddlAccountname3.SelectedIndex > 0 && txtAmount3.Text != "")
                                    {
                                        insert(Math.Round(Convert.ToDecimal(txtAmount3.Text), 2, MidpointRounding.AwayFromZero).ToString(), ddlAccountname3.SelectedValue, txtmemo3.Text, dtmaxid.Rows[0]["TMID"].ToString());


                                    }
                                    if (ddlAccountname4.SelectedIndex > 0 && txtAmount4.Text != "")
                                    {
                                        insert(Math.Round(Convert.ToDecimal(txtAmount4.Text), 2, MidpointRounding.AwayFromZero).ToString(), ddlAccountname4.SelectedValue, txtmemo4.Text, dtmaxid.Rows[0]["TMID"].ToString());


                                    }
                                    if (ddlAccountname5.SelectedIndex > 0 && txtAmount5.Text != "")
                                    {
                                        insert(Math.Round(Convert.ToDecimal(txtAmount5.Text), 2, MidpointRounding.AwayFromZero).ToString(), ddlAccountname5.SelectedValue, txtmemo5.Text, dtmaxid.Rows[0]["TMID"].ToString());


                                    }
                                    if (ddlAccountname6.SelectedIndex > 0 && txtAmount6.Text != "")
                                    {
                                        insert(Math.Round(Convert.ToDecimal(txtAmount6.Text), 2, MidpointRounding.AwayFromZero).ToString(), ddlAccountname6.SelectedValue, txtmemo6.Text, dtmaxid.Rows[0]["TMID"].ToString());


                                    }
                                    if (ddlAccountname7.SelectedIndex > 0 && txtAmount7.Text != "")
                                    {
                                        insert(Math.Round(Convert.ToDecimal(txtAmount7.Text), 2, MidpointRounding.AwayFromZero).ToString(), ddlAccountname7.SelectedValue, txtmemo7.Text, dtmaxid.Rows[0]["TMID"].ToString());

                                    }
                                    if (ddlAccountname8.SelectedIndex > 0 && txtAmount8.Text != "")
                                    {
                                        insert(Math.Round(Convert.ToDecimal(txtAmount8.Text), 2, MidpointRounding.AwayFromZero).ToString(), ddlAccountname8.SelectedValue, txtmemo8.Text, dtmaxid.Rows[0]["TMID"].ToString());

                                    }
                                    if (ddlAccountname9.SelectedIndex > 0 && txtAmount9.Text != "")
                                    {
                                        insert(Math.Round(Convert.ToDecimal(txtAmount9.Text), 2, MidpointRounding.AwayFromZero).ToString(), ddlAccountname9.SelectedValue, txtmemo9.Text, dtmaxid.Rows[0]["TMID"].ToString());


                                    }
                                    DataTable dtapprequirment = ClsAccountAppr.Apprreuqired();
                                    if (dtapprequirment.Rows.Count > 0)
                                    {

                                        ClsAccountAppr.AccountAppMaster(ddlwarehouse.SelectedValue, ViewState["tid"].ToString(), chkappentry.Checked);
                                    }
                                    lblmsg.Visible = true;
                                    lblmsg.Text = "";
                                    lblmsg.Text = "Record inserted successfully";
                                    txtmmo.Text = "";
                                    CheckBox1.Checked = false;
                                    //txtmmo.Text = "";
                                    // txtTodate.Text = "";
                                    GridView2.DataSource = null;
                                    GridView2.DataBind();
                                    txtmmo.Text = "";
                                    GridView2.Visible = false;

                                    chkappamount.Checked = false;
                                    // ddlwarehouse.SelectedIndex = 0;
                                    // ddlAccount.SelectedIndex = 0;
                                    ddlpartytypetype.Items.Clear();
                                    ddlpartynamename.Items.Clear();
                                    //  RadioButtonList1.Items[0].Selected = false;
                                    // RadioButtonList1.Items[1].Selected = false;
                                    entryno();
                                    txtpayamount.Text = "";
                                    ddlAccountname1.SelectedIndex = -1;
                                    ddlAccountname2.SelectedIndex = -1;
                                    ddlAccountname3.SelectedIndex = -1;
                                    ddlAccountname4.SelectedIndex = -1;
                                    ddlAccountname5.SelectedIndex = -1;
                                    ddlAccountname6.SelectedIndex = -1;
                                    ddlAccountname7.SelectedIndex = -1;
                                    ddlAccountname8.SelectedIndex = -1;
                                    ddlAccountname9.SelectedIndex = -1;
                                    if (Request.QueryString["docid"] != null)
                                    {
                                        inserdocatt();
                                    }
                                    else
                                    {
                                        if (chkdoc.Checked == true)
                                        {
                                            entry();
                                            //ModalPopupExtender1.Show();
                                            //filldoc();
                                        }
                                    }

                                    txtAmount1.Text = "";
                                    txtAmount2.Text = "";
                                    txtAmount3.Text = "";
                                    txtAmount4.Text = "";
                                    txtAmount5.Text = "";
                                    txtAmount6.Text = "";
                                    txtAmount7.Text = "";
                                    txtAmount8.Text = "";
                                    txtAmount9.Text = "";
                                    txtmemo1.Text = "";
                                    txtmemo2.Text = "";
                                    txtmemo3.Text = "";
                                    txtmemo4.Text = "";
                                    txtmemo5.Text = "";
                                    txtmemo6.Text = "";
                                    txtmemo7.Text = "";
                                    txtmemo8.Text = "";
                                    txtmemo9.Text = "";
                                    lblamt1.Text = "";
                                    lblamt2.Text = "";
                                    lblamt3.Text = "";
                                    lblamt4.Text = "";
                                    lblamt5.Text = "";
                                    lblamt6.Text = "";
                                    lblamt7.Text = "";
                                    lblamt8.Text = "";
                                    lblamt9.Text = "";
                                    // txtmmo.Text = "";
                                    //txtpayamount.Text = "";
                                    lblgtotal.Text = "";

                                }
                                else
                                {
                                    lblmsg.Visible = true;
                                    lblmsg.Text = "";
                                    lblmsg.Text = "The payment of $" + y.ToString() + " that you are trying to record for various expenses" +
                        " does not add up to the total payment amount of $" + txtpayamount.Text + ".<br>Please adjust your payment amount to match your total expense.";
                                    if (txtAmount1.Text == "" || ddlAccountname1.SelectedIndex == 0)
                                    {
                                        lblamt1.Text = "*";
                                    }
                                    else if (txtAmount2.Text == "" || ddlAccountname2.SelectedIndex == 0)
                                    {
                                        lblamt2.Text = "*";
                                    }
                                    else if (txtAmount3.Text == "" || ddlAccountname3.SelectedIndex == 0)
                                    {
                                        lblamt3.Text = "*";
                                    }
                                    else if (txtAmount4.Text == "" || ddlAccountname4.SelectedIndex == 0)
                                    {
                                        lblamt4.Text = "*";
                                    }
                                    else if (txtAmount5.Text == "" || ddlAccountname5.SelectedIndex == 0)
                                    {
                                        lblamt5.Text = "*";
                                    }
                                    else if (txtAmount6.Text == "" || ddlAccountname6.SelectedIndex == 0)
                                    {
                                        lblamt6.Text = "*";
                                    }
                                    else if (txtAmount7.Text == "" || ddlAccountname7.SelectedIndex == 0)
                                    {
                                        lblamt7.Text = "*";
                                    }
                                    else if (txtAmount8.Text == "" || ddlAccountname8.SelectedIndex == 0)
                                    {
                                        lblamt8.Text = "*";
                                    }
                                    else if (txtAmount9.Text == "" || ddlAccountname9.SelectedIndex == 0)
                                    {
                                        lblamt9.Text = "*";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "";
                        lblmsg.Text = "Sorry, You are not permitted to insert greater record as per Price plan";

                    }
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Please check your date. You cannot select any date earlier/later than start/end date of the year";
                }

            }
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "You are not permited to insert record for this page.";

        }
    }
    protected void insert(string amt,string value,string memo,string tid)
    {
        SqlCommand cmd = new SqlCommand("Insert into Tranction_Details(AccountDebit,AccountCredit,AmountDebit,AmountCredit,Tranction_Master_Id,Memo,DateTimeofTransaction,compid,whid) values('" + value + "','0','" + amt + "','0','" + tid + "','" + memo + "','" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "','" + compid + "','" + ddlwarehouse.SelectedValue + "')", conn);
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
    }
    protected void fillauto()
    {
        string sssx = "Select top(1)  TranctionMaster.Tranction_Master_Id,TranctionMaster.Whid,Tranction_Details.AccountCredit,TranctionMaster.Date,TranctionMasterSuppliment.Party_MasterId from TranctionMaster inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id inner join TranctionMasterSuppliment on TranctionMasterSuppliment.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where TranctionMaster.compid='" + Session["comid"] + "' and TranctionMaster.EntryTypeId='1' and Tranction_Details.compid='" + Session["comid"] + "' and (Tranction_Details.AccountDebit IS NOT NULL or Tranction_Details.AccountDebit<>'0') order by TranctionMaster.Tranction_Master_Id Desc";
        SqlDataAdapter adpt = new SqlDataAdapter(sssx, conn);
        DataTable dtpt = new DataTable();
        adpt.Fill(dtpt);

        if (dtpt.Rows.Count > 0)
        {
            ddlwarehouse.SelectedIndex = ddlwarehouse.Items.IndexOf(ddlwarehouse.Items.FindByValue(dtpt.Rows[0]["Whid"].ToString()));
            txtTodate.Text = Convert.ToDateTime(dtpt.Rows[0]["Date"]).ToShortDateString();
            lblbdate.Text = txtTodate.Text;
            EventArgs e = new EventArgs();
            object sender = new object();
            ddlwarehouse_SelectedIndexChanged(sender, e);
            ddlAccount.SelectedIndex = ddlAccount.Items.IndexOf(ddlAccount.Items.FindByValue(dtpt.Rows[0]["AccountCredit"].ToString()));

            if (Convert.ToString(dtpt.Rows[0]["Party_MasterId"]) != "0")
            {
                string sssx1 = "Select Party_master.PartyTypeId from Party_master Where PartyId='" + dtpt.Rows[0]["Party_MasterId"].ToString() + "'";
                SqlDataAdapter adpt1 = new SqlDataAdapter(sssx1, conn);
                DataTable dtpt1 = new DataTable();
                adpt1.Fill(dtpt1);
                if (dtpt1.Rows.Count > 0)
                {
                    RadioButtonList1.SelectedIndex = RadioButtonList1.Items.IndexOf(RadioButtonList1.Items.FindByValue("1"));
                    RadioButtonList1_SelectedIndexChanged(sender, e);
                    // ddlpartytypetype.SelectedIndex = ddlpartytypetype.Items.IndexOf(ddlpartytypetype.Items.FindByValue(dtpt1.Rows[0]["PartyTypeId"].ToString()));
                    // ddlpartytypetype_SelectedIndexChanged(sender, e);
                    // ddlpartynamename.SelectedIndex = ddlpartynamename.Items.IndexOf(ddlpartynamename.Items.FindByValue(dtpt.Rows[0]["Party_MasterId"].ToString()));
                    //  ddlpartynamename_SelectedIndexChanged(sender, e);
                }
            }
            else
            {
                RadioButtonList1.SelectedIndex = RadioButtonList1.Items.IndexOf(RadioButtonList1.Items.FindByValue("0"));
                RadioButtonList1_SelectedIndexChanged(sender, e);
            }

        }
        else
        {
          
            lblbdate.Text = txtTodate.Text;
            EventArgs e = new EventArgs();
            object sender = new object();
            ddlwarehouse_SelectedIndexChanged(sender, e);
           
                RadioButtonList1.SelectedIndex = RadioButtonList1.Items.IndexOf(RadioButtonList1.Items.FindByValue("0"));
                RadioButtonList1_SelectedIndexChanged(sender, e);
           

        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ddlAccount.SelectedIndex = -1;
        ddlpartynamename.SelectedIndex = -1;
        txtpayamount.Text = "";
        txtmmo.Text = "";
        CheckBox1.Checked = false;
        GridView2.DataSource = null;
        GridView2.DataBind();
        GridView2.EmptyDataText = "";
        lblmsg.Visible = false;
        RadioButtonList1.Enabled = true;
        fillenableparty(true);

    }
    protected void Button3_Click(object sender, EventArgs e)
    {

        lblmsg.Visible = false;
        ddlAccount.SelectedIndex = -1;
        ddlpartynamename.SelectedIndex = -1;
        //txtpayamount.Text = "";
        txtmmo.Text = "";
        CheckBox1.Checked = false;
        GridView2.DataSource = null;
        GridView2.DataBind();
        GridView2.EmptyDataText = "";
        ddlAccountname1.SelectedIndex = -1;
        ddlAccountname2.SelectedIndex=-1;
        ddlAccountname3.SelectedIndex = -1;
        ddlAccountname4.SelectedIndex = -1;
        ddlAccountname5.SelectedIndex = -1;
        ddlAccountname6.SelectedIndex = -1;
        ddlAccountname7.SelectedIndex = -1;
        ddlAccountname8.SelectedIndex = -1;
        ddlAccountname9.SelectedIndex = -1;
        fillenableparty(true);

        //ddlAccountname1.SelectedIndex = ddlAccountname1.Items.IndexOf(ddlAccountname1.Items.FindByValue("--Select--"));
        //ddlAccountname2.SelectedIndex = ddlAccountname2.Items.IndexOf(ddlAccountname2.Items.FindByValue("--Select--"));
        //ddlAccountname3.SelectedIndex = ddlAccountname3.Items.IndexOf(ddlAccountname3.Items.FindByValue("--Select--"));
        //ddlAccountname4.SelectedIndex = ddlAccountname4.Items.IndexOf(ddlAccountname4.Items.FindByValue("--Select--"));
        //ddlAccountname5.SelectedIndex = ddlAccountname5.Items.IndexOf(ddlAccountname5.Items.FindByValue("--Select--"));
        //ddlAccountname6.SelectedIndex = ddlAccountname6.Items.IndexOf(ddlAccountname6.Items.FindByValue("--Select--"));
        //ddlAccountname7.SelectedIndex = ddlAccountname7.Items.IndexOf(ddlAccountname7.Items.FindByValue("--Select--"));
        //ddlAccountname8.SelectedIndex = ddlAccountname8.Items.IndexOf(ddlAccountname8.Items.FindByValue("--Select--"));
        //ddlAccountname9.SelectedIndex = ddlAccountname9.Items.IndexOf(ddlAccountname9.Items.FindByValue("--Select--"));
        txtAmount1.Text = "";
        txtAmount2.Text = "";
        txtAmount3.Text = "";
        txtAmount4.Text = "";
        txtAmount5.Text = "";
        txtAmount6.Text = "";
        txtAmount7.Text = "";
        txtAmount8.Text = "";
        txtAmount9.Text = "";
        txtmemo1.Text = "";
        txtmemo2.Text = "";
        txtmemo3.Text = "";
        txtmemo4.Text = "";
        txtmemo5.Text = "";
        txtmemo6.Text = "";
        txtmemo7.Text = "";
        txtmemo8.Text = "";
        txtmemo9.Text = "";
        lblamt1.Text = "";
        lblamt2.Text = "";
        lblamt3.Text = "";
        lblamt4.Text = "";
        lblamt5.Text = "";
        lblamt6.Text = "";
        lblamt7.Text = "";
        lblamt8.Text = "";
        lblamt9.Text = "";
        RadioButtonList1.Enabled = true;
  // txtmmo.Text = "";
     txtpayamount.Text = "";
     lblgtotal.Text = "";
    }
    protected void txtpayamount_TextChanged(object sender, EventArgs e)
    {
        
    }
   
    protected void chkappamount_CheckedChanged(object sender, EventArgs e)
    {
        if (chkappamount.Checked == true)
        {
            CheckBox1.Checked = true;
            //CheckBox1.Visible = true;
            CheckBox1_CheckedChanged(sender, e);
            double amt = 0;
            if (txtpayamount.Text != "")
            {
                amt = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtpayamount.Text),2,MidpointRounding.AwayFromZero));
            }
            foreach (GridViewRow gdr in GridView2.Rows)
            {
                Label lbl = (Label)(gdr.FindControl("lblbaldue"));

                TextBox txt = (TextBox)(gdr.FindControl("txtnewamt"));
                CheckBox chkck = (CheckBox)(gdr.FindControl("chkbox"));

                double amt1 = Convert.ToDouble(lbl.Text);
                //amt = amt - amt1;
                //if (amt > 0)
                //{
                //    txt.Text = Convert.ToString(amt1);
                //}
                if (amt > 0)
                {
                    if (amt1 >= amt)
                    {
                        txt.Text = Convert.ToString(amt);
                        chkck.Checked = true;
                    }
                    else if (amt1 <= amt)
                    {
                        txt.Text = lbl.Text;
                        chkck.Checked = true;
                    }
                }
                //else
                //{
                //    txt.Text = lbl.Text;
                //}
                amt = amt - amt1;
                // btnsubmit.Focus();
            }
        }
        else
        {
            CheckBox1.Visible = false;
        }
    }

    protected void LinkButton97666667_Click(object sender, EventArgs e)
    {
        ///ModalPopupExtender142422.Show();
        ///tbPassword.Attributes.Clear();
        //tbConPassword.Attributes.Clear();
    }
  
    public void fillddlOther(DropDownList ddl, String dtf, String dvf)
    {
        ddl.Items.Clear();
        ddl.DataTextField = dtf;
        ddl.DataValueField = dvf;
        ddl.DataBind();
        //ModalPopupExtender142422.Show();
        //ddl.Items.Insert(0, "--Select--");
        //ddl.Items[0].Value = "0";
    }
    public DataSet fillddl(String qry)
    {
        SqlCommand cmd = new SqlCommand(qry, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
        
    }

    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;

    }
  
   
    protected void Delete(string query)
    {
        SqlCommand cmd = new SqlCommand(query, conn);
        if (conn.State.ToString() != "Open")
        {
            conn.Open();
        }
        cmd.ExecuteNonQuery();
        conn.Close();
    }
  
 
   
   
    
    public void inserdocatt()
    {

        string sqlselect = "select * from DocumentMaster where DocumentId='" + Request.QueryString["docid"] + "'";
        SqlDataAdapter adpt = new SqlDataAdapter(sqlselect, conn);
        DataTable dtpt = new DataTable();
        adpt.Fill(dtpt);
        if (dtpt.Rows.Count > 0)
        {

            SqlCommand cmdi = new SqlCommand("InsertAttachmentMaster", conn);

            cmdi.CommandType = CommandType.StoredProcedure;
            cmdi.Parameters.Add(new SqlParameter("@Titlename", SqlDbType.NVarChar));
            cmdi.Parameters["@Titlename"].Value = dtpt.Rows[0]["DocumentTitle"].ToString();
            cmdi.Parameters.Add(new SqlParameter("@Filename", SqlDbType.NVarChar));
            cmdi.Parameters["@Filename"].Value = dtpt.Rows[0]["DocumentName"].ToString();

            cmdi.Parameters.Add(new SqlParameter("@Datetime", SqlDbType.DateTime));
            cmdi.Parameters["@Datetime"].Value = dtpt.Rows[0]["DocumentUploadDate"].ToString(); ;
            cmdi.Parameters.Add(new SqlParameter("@RelatedtablemasterId", SqlDbType.NVarChar));
            cmdi.Parameters["@RelatedtablemasterId"].Value = "5";
            cmdi.Parameters.Add(new SqlParameter("@RelatedTableId", SqlDbType.NVarChar));
            cmdi.Parameters["@RelatedTableId"].Value = ViewState["tid"].ToString();
            cmdi.Parameters.Add(new SqlParameter("@IfilecabinetDocId", SqlDbType.NVarChar));
            cmdi.Parameters["@IfilecabinetDocId"].Value = dtpt.Rows[0]["DocumentId"].ToString();
            cmdi.Parameters.Add(new SqlParameter("@Attachment", SqlDbType.Int));
            cmdi.Parameters["@Attachment"].Direction = ParameterDirection.Output;

            cmdi.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
            cmdi.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
            if (conn.State.ToString() != "Open")
            {
                conn.Open();
            }
            Int32 result = cmdi.ExecuteNonQuery();
            result = Convert.ToInt32(cmdi.Parameters["@Attachment"].Value);
            conn.Close();
        }
    }

    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlCommand cmd511 = new SqlCommand("Sp_Select_CashPayment", conn);
        cmd511.CommandType = CommandType.StoredProcedure;
        cmd511.Parameters.AddWithValue("@compid", compid);
        cmd511.Parameters.AddWithValue("@whid", ddlwarehouse.SelectedValue);
        // cmd.Parameters.AddWithValue("@GroupId", Convert.ToInt32(ddlGroupName1.SelectedValue));
        SqlDataAdapter adp511 = new SqlDataAdapter(cmd511);

        DataSet ds511 = new DataSet();
        adp511.Fill(ds511);
        if (ds511.Tables[0].Rows.Count > 0)
        {
            if (ds511.Tables[0].Rows[0]["EntryNumber"].ToString() != "")
            {

                db = Convert.ToDouble(ds511.Tables[0].Rows[0][0].ToString());
                db = db + 1;
            }
            else
            {
                db = 1;
            }
        }
        else
        {
            db = 1;
        }
        lbltotentry.Text = (db - 1).ToString();
        lblentrynumber.Text = db.ToString();
        SqlCommand cmd123 = new SqlCommand("select AccountId ,AccountName from AccountMaster where GroupId=1 and Status='1' and AccountMaster.compid = '" + compid + "' and Whid='" + ddlwarehouse.SelectedValue + "' order by AccountName", conn);
        SqlDataAdapter adp123 = new SqlDataAdapter(cmd123);
        DataTable dt123 = new DataTable();
        adp123.Fill(dt123);
      
            ddlAccount.DataSource = dt123;
            ddlAccount.DataValueField = "AccountId";
            ddlAccount.DataTextField = "AccountName";
            ddlAccount.DataBind();
            //ddlAccount.Items.Insert(0, "--Select--");
            ddlAccount_SelectedIndexChanged(sender, e);
       RadioButtonList1_SelectedIndexChanged(sender,e);
       chkappentry.Visible = false;
       DataTable appri = ClsAccountAppr.Apprreuqired();
       if (appri.Rows.Count > 0)
       {
           ViewState["AccRS"] = "ACC";
           int kn = ClsAccountAppr.Allowchkappr(ddlwarehouse.SelectedValue);
           if (kn == 1)
           {
               chkappentry.Visible = true;
           }
       }
       else
       {
           ViewState["AccRS"] = "";
       }
       btnsubmit.Enabled = true;
       btneditgene.Enabled = true;
       btnedit.Enabled = true;
       btnsubradio2.Enabled = true;
       DataTable dtpaacc = select("Select * from AccountPageRightAccess where cid='" + Session["Comid"] + "' and Access='1' and AccType='0'");
       if (dtpaacc.Rows.Count > 0)
       {
           btnedit.Enabled = false;
           btnsubradio2.Enabled = false;
           btnsubmit.Enabled = false;
           btneditgene.Enabled = false;
           DataTable dtrc = select(" select Accountingpagerightwithdesignation.*  from Accountingpagerightwithdesignation inner join DesignationMaster on Accountingpagerightwithdesignation.DesignationId=DesignationMaster.DesignationMasterId " +
          " inner join DepartmentmasterMNC on DepartmentmasterMNC.Id=DesignationMaster.DeptId where DesignationId='" + Session["DesignationId"] + "' and  DepartmentmasterMNC.whid='" + ddlwarehouse.SelectedValue + "'");
           if (dtrc.Rows.Count > 0)
           {
               if (Convert.ToInt32(dtrc.Rows[0]["AccessRight"]) == 0)
               {

                   lblmsg.Text = "Sorry,you are not access right for this business";
               }
               else if (Convert.ToInt32(dtrc.Rows[0]["AccessRight"]) == 2)
               {
                   if (Convert.ToInt32(dtrc.Rows[0]["Edit_Right"]) == 1)
                   {
                       btnedit.Enabled = false;
                       btneditgene.Enabled = true;
                   }
                   else if (Convert.ToInt32(dtrc.Rows[0]["Insert_Right"]) == 1)
                   {
                       btnsubradio2.Enabled = false;
                       btnsubmit.Enabled = true;
                   }
               }
               else
               {
                   btnedit.Enabled = true;
                   btnsubradio2.Enabled = true;
                   btnsubmit.Enabled = true;
                   btneditgene.Enabled = true;
               }
           }
           else
           {
               lblmsg.Text = "Sorry,you are not access right for this business";

           }
       }
    }
   
 
    public void entry()
    {
        String te = "AccEntryDocUp.aspx?Tid=" + ViewState["tid"];


        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
     
    protected void txtTodate_TextChanged(object sender, EventArgs e)
    {
        openbal();
    }
    protected void openbal()
    {
        lblbdate.Text = txtTodate.Text;
         DataTable dt = new DataTable();
         dt = (DataTable)select("select Convert(nvarchar,StartDate,101)   as StartDate,EndDate,Report_Period_Id from [ReportPeriod] where Compid = '" + Session["Comid"] + "' and Active='1'  and Whid='" + ddlwarehouse.SelectedValue + "'");


             if (dt.Rows.Count > 0)
             {
                 if (Convert.ToDateTime(txtTodate.Text) < Convert.ToDateTime(dt.Rows[0]["StartDate"].ToString()))
                 {
                     lblstartdate.Text = dt.Rows[0][0].ToString();
                     ModalPopupExtender1222.Show();
                     txtTodate.Text = dt.Rows[0][0].ToString();

                 }
                 else
                 {
                     double deb = 0;
                     double crd = 0;
                    
                     double salballst = 0;

                     DataTable dtamtd = new DataTable();
                     dtamtd = (DataTable)select("Select sum(AmountDebit) as amtb from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where TranctionMaster.Whid='" + ddlwarehouse.SelectedValue + "' and  Tranction_Details.whid='" + ddlwarehouse.SelectedValue + "'  and AccountDebit='" + ddlAccount.SelectedValue + "' and DateTimeOfTransaction<='"+Convert.ToDateTime(txtTodate.Text).ToShortDateString()+"'");
                     if (dtamtd.Rows.Count > 0)
                     {
                         if (dtamtd.Rows[0]["amtb"].ToString() != "")
                         {
                           
                             deb = Convert.ToDouble(dtamtd.Rows[0]["amtb"].ToString());
                             
                         }
                     }

                     DataTable dtamtc = new DataTable();
                     dtamtc = (DataTable)select("Select sum(AmountCredit) as amtc from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where TranctionMaster.Whid='" + ddlwarehouse.SelectedValue + "' and  Tranction_Details.whid='" + ddlwarehouse.SelectedValue + "'  and AccountCredit='" + ddlAccount.SelectedValue + "' and DateTimeOfTransaction<='" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "'");
                     if (dtamtc.Rows.Count > 0)
                     {
                         if (dtamtc.Rows[0]["amtc"].ToString() != "")
                         {
                            
                             crd = Convert.ToDouble(dtamtc.Rows[0]["amtc"].ToString());
                           
                         }
                     }
                     DataTable dtbal = new DataTable();
                     dtbal = (DataTable)select("Select Balance from AccountBalance where AccountMasterId=(select Max(Id) as Id from AccountMaster where AccountId='" + ddlAccount.SelectedValue + "' and Whid='" + ddlwarehouse.SelectedValue + "' ) and Report_Period_Id<'" + dt.Rows[0]["Report_Period_Id"].ToString() + "' Order by Report_Period_Id Desc ");
                     if (dtbal.Rows.Count > 0)
                     {
                         if (dtbal.Rows[0]["Balance"].ToString() != "")
                         {

                             salballst = Convert.ToDouble(dtbal.Rows[0]["Balance"].ToString());
                            
                         }
                     }
                     lblbalanceason.Text = ((deb - crd) + salballst).ToString("###,###.##");
                     if (lblbalanceason.Text.Length <= 0)
                     {
                         lblbalanceason.Text = "0";
                     }
                 }
             }

    }
    protected void btnupdatepaety_Click(object sender, EventArgs e)
    {
        DataTable dtss = (DataTable)select("select Convert(nvarchar,StartDate,101) as StartDate, Convert(nvarchar,EndDate,101) as EndDate from [ReportPeriod] where Active='1' and Whid='" + ddlwarehouse.SelectedValue + "'");

        if (dtss.Rows.Count > 0)
        {
            if (Convert.ToDateTime(txtTodate.Text) >= Convert.ToDateTime(dtss.Rows[0]["StartDate"]) && Convert.ToDateTime(txtTodate.Text) <= Convert.ToDateTime(dtss.Rows[0]["EndDate"]))
            {
                bool access = UserAccess.Usercon("TranctionMaster", "", "Tranction_Master_Id", "", "", "compid", "TranctionMaster");
                if (access == true)
                {
                    DataTable dt = new DataTable();
                    dt = (DataTable)select("Select distinct EntryTypeMaster.Entry_Type_Name, TranctionMaster.Tranction_Amount, TranctionMaster.Whid,TranctionMasterSuppliment.Memo,TranctionMaster.EntryNumber, TranctionMaster.EntryTypeId,TranctionMaster.Tranction_Master_Id,TranctionMaster.Whid,Tranction_Details.AccountDebit,Tranction_Details.AccountCredit,Tranction_Details.AmountCredit,Tranction_Details.AmountDebit,Tranction_Details.Tranction_Details_Id,TranctionMasterSuppliment.Tranction_Master_SupplimentId,TranctionMaster.Date,TranctionMasterSuppliment.Party_MasterId from EntryTypeMaster inner join TranctionMaster on TranctionMaster.EntryTypeId=EntryTypeMaster.Entry_Type_Id inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id inner join TranctionMasterSuppliment on TranctionMasterSuppliment.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where TranctionMaster.compid='" + Session["Comid"] + "' and TranctionMaster.Tranction_Master_Id='" + ViewState["Trid"] + "' and TranctionMaster.EntryTypeId='1'");
                    if (dt.Rows.Count > 0)
                    {

                        ViewState["Trid"] = Convert.ToString(dt.Rows[0]["Tranction_Master_Id"]);
                        SqlCommand cmdamtups = new SqlCommand("Update TranctionMaster set Tranction_Amount = '" + Math.Round(Convert.ToDecimal(txtpayamount.Text), 2, MidpointRounding.AwayFromZero) + "',Date='" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "',Whid='" + ddlwarehouse.SelectedValue + "' where Tranction_Master_Id ='" + ViewState["Trid"] + "'", conn);
                        if (conn.State.ToString() != "Open")
                        {
                            conn.Open();
                        }
                        cmdamtups.ExecuteNonQuery();
                        SqlCommand cmdamtups1 = new SqlCommand("Update Tranction_Details set AccountCredit='" + ddlAccount.SelectedValue + "', AmountCredit='" + Math.Round(Convert.ToDecimal(txtpayamount.Text), 2, MidpointRounding.AwayFromZero) + "', Memo  = '" + txtmmo.Text + "',DateTimeOfTransaction='" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "',Whid='" + ddlwarehouse.SelectedValue + "'  where Tranction_Master_Id ='" + ViewState["Trid"] + "' and Tranction_Details_Id='" + dt.Rows[0]["Tranction_Details_Id"] + "'", conn);
                        if (conn.State.ToString() != "Open")
                        {
                            conn.Open();
                        }

                        cmdamtups1.ExecuteNonQuery();
                        if (RadioButtonList1.SelectedValue == "1")
                        {

                            SqlCommand cmds = new SqlCommand("select Account From Party_master inner join PartytTypeMaster  on Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId where PartyID='" + ddlpartynamename.SelectedValue + "' and PartytTypeMaster.compid = '" + compid + "' and Party_master.Whid='" + ddlwarehouse.SelectedValue + "'", conn);

                            SqlDataAdapter dtps = new SqlDataAdapter(cmds);
                            DataTable dss = new DataTable();
                            dtps.Fill(dss);
                            if (dss.Rows.Count > 0)
                            {
                                updatetdetail(dss.Rows[0]["Account"].ToString(), Math.Round(Convert.ToDecimal(txtpayamount.Text), 2, MidpointRounding.AwayFromZero).ToString(), txtmmo.Text, dt.Rows[1]["Tranction_Details_Id"].ToString());


                            }
                            SqlCommand cmdamtups3 = new SqlCommand("Update TranctionMasterSuppliment set Party_MasterId='" + ddlpartynamename.SelectedValue + "', Memo  = '" + txtmmo.Text + "' where Tranction_Master_Id ='" + ViewState["Trid"] + "'", conn);
                            if (conn.State.ToString() != "Open")
                            {
                                conn.Open();
                            }
                            cmdamtups3.ExecuteNonQuery();
                            if (Request.QueryString["docid"] != null)
                            {
                                inserdocatt();
                            }
                            else
                            {
                                if (chkdoc.Checked == true)
                                {
                                    entry();

                                    //filldoc();
                                }
                            }
                            if (Request.QueryString["docid"] != null)
                            {
                                inserdocatt();
                            }
                            else
                            {
                                if (chkdoc.Checked == true)
                                {
                                    entry();

                                    //filldoc();
                                }
                            }
                            controlclear();
                            lblmsg.Text = "Record updated successfully";
                            lblmsg.Visible = true;
                            txtmmo.Text = "";
                            entryno();
                            if (Request.QueryString["Trid"] == null)
                            {
                                btnsubmit.Visible = true;
                                Button2.Visible = true;
                                btnupdatepaety.Visible = false;
                                RadioButtonList1.Enabled = true;
                            }

                        }
                    }
                }
                else
                {
                    lblmsg.Text = "Sorry, You are not permitted to insert greater record as per Price plan";
                    lblmsg.Visible = true;

                }
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Please check your date. You cannot select any date earlier/later than start/end date of the year";
            }

        }
    }
    protected void btnupdateAccc_Click(object sender, EventArgs e)
    {
        DataTable dtss = (DataTable)select("select Convert(nvarchar,StartDate,101) as StartDate, Convert(nvarchar,EndDate,101) as EndDate from [ReportPeriod] where Active='1' and Whid='" + ddlwarehouse.SelectedValue + "'");

        if (dtss.Rows.Count > 0)
        {
            if (Convert.ToDateTime(txtTodate.Text) >= Convert.ToDateTime(dtss.Rows[0]["StartDate"]) && Convert.ToDateTime(txtTodate.Text) <= Convert.ToDateTime(dtss.Rows[0]["EndDate"]))
            {
                bool access = UserAccess.Usercon("TranctionMaster", "", "Tranction_Master_Id", "", "", "compid", "TranctionMaster");
                if (access == true)
                {
                    string date = "select Convert(nvarchar,StartDate,101) as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Whid='" + ddlwarehouse.SelectedValue + "' and Active='1'";
                    SqlCommand cmd = new SqlCommand(date, conn);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dta = new DataTable();
                    adp.Fill(dta);
                    //txtdate.Text = Convert.ToString(System.DateTime.Now.Date.ToShortDateString());
                    if (dta.Rows.Count > 0)
                    {
                        if (Convert.ToDateTime(txtTodate.Text) < Convert.ToDateTime(dta.Rows[0][0].ToString()))
                        {
                            lblstartdate.Text = dta.Rows[0][0].ToString();
                            ModalPopupExtender1222.Show();

                        }

                        else
                        {
                            double a, b, c, d, x, f, g, h, i;
                            if (txtAmount1.Text == "" || ddlAccountname1.SelectedIndex == 0)
                            { a = 0; }
                            else { a = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount1.Text), 2, MidpointRounding.AwayFromZero)); }
                            if (txtAmount2.Text == "" || ddlAccountname2.SelectedIndex == 0)
                            { b = 0; }
                            else { b = Convert.ToDouble(txtAmount2.Text); }
                            if (txtAmount3.Text == "" || ddlAccountname3.SelectedIndex == 0)
                            { c = 0; }
                            else { c = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount3.Text), 2, MidpointRounding.AwayFromZero).ToString()); }
                            if (txtAmount4.Text == "" || ddlAccountname4.SelectedIndex == 0)
                            { d = 0; }
                            else { d = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount4.Text), 2, MidpointRounding.AwayFromZero).ToString()); }
                            if (txtAmount5.Text == "" || ddlAccountname5.SelectedIndex == 0)
                            { x = 0; }
                            else { x = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount5.Text), 2, MidpointRounding.AwayFromZero).ToString()); }
                            if (txtAmount6.Text == "" || ddlAccountname6.SelectedIndex == 0)
                            { f = 0; }
                            else { f = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount6.Text), 2, MidpointRounding.AwayFromZero).ToString()); }
                            if (txtAmount7.Text == "" || ddlAccountname7.SelectedIndex == 0)
                            { g = 0; }
                            else { g = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount7.Text), 2, MidpointRounding.AwayFromZero).ToString()); }
                            if (txtAmount8.Text == "" || ddlAccountname8.SelectedIndex == 0)
                            { h = 0; }
                            else { h = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount8.Text), 2, MidpointRounding.AwayFromZero).ToString()); }
                            if (txtAmount9.Text == "" || ddlAccountname9.SelectedIndex == 0)
                            { i = 0; }
                            else { i = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount9.Text), 2, MidpointRounding.AwayFromZero).ToString()); }

                            double y = a + b + c + d + x + f + g + h + i;

                            double z = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtpayamount.Text), 2, MidpointRounding.AwayFromZero));
                            if (z == y)
                            {
                                DataTable dt = new DataTable();
                                dt = (DataTable)select("Select distinct EntryTypeMaster.Entry_Type_Name, TranctionMaster.Tranction_Amount, TranctionMaster.Whid,TranctionMasterSuppliment.Memo,TranctionMaster.EntryNumber, TranctionMaster.EntryTypeId,TranctionMaster.Tranction_Master_Id,TranctionMaster.Whid,Tranction_Details.AccountDebit,Tranction_Details.AccountCredit,Tranction_Details.AmountCredit,Tranction_Details.AmountDebit,Tranction_Details.Tranction_Details_Id,TranctionMasterSuppliment.Tranction_Master_SupplimentId,TranctionMaster.Date,TranctionMasterSuppliment.Party_MasterId from EntryTypeMaster inner join TranctionMaster on TranctionMaster.EntryTypeId=EntryTypeMaster.Entry_Type_Id inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id inner join TranctionMasterSuppliment on TranctionMasterSuppliment.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where TranctionMaster.compid='" + Session["Comid"] + "' and TranctionMaster.Tranction_Master_Id='" + ViewState["Trid"] + "' and TranctionMaster.EntryTypeId='1'");
                                if (dt.Rows.Count > 0)
                                {

                                    ViewState["Trid"] = Convert.ToString(dt.Rows[0]["Tranction_Master_Id"]);


                                    SqlCommand cmdamtups = new SqlCommand("Update TranctionMaster set Tranction_Amount = '" + Math.Round(Convert.ToDecimal(txtpayamount.Text), 2, MidpointRounding.AwayFromZero) + "',Date='" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "',Whid='" + ddlwarehouse.SelectedValue + "' where Tranction_Master_Id ='" + ViewState["Trid"] + "'", conn);
                                    if (conn.State.ToString() != "Open")
                                    {
                                        conn.Open();
                                    }
                                    cmdamtups.ExecuteNonQuery();
                                    SqlCommand cmdamtups1 = new SqlCommand("Update Tranction_Details set AccountCredit='" + ddlAccount.SelectedValue + "', AmountCredit='" + Math.Round(Convert.ToDecimal(txtpayamount.Text), 2, MidpointRounding.AwayFromZero) + "', Memo  = '" + txtmmo.Text + "',DateTimeOfTransaction='" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "',Whid='" + ddlwarehouse.SelectedValue + "'  where Tranction_Master_Id ='" + ViewState["Trid"] + "' and Tranction_Details_Id='" + dt.Rows[0]["Tranction_Details_Id"] + "'", conn);
                                    if (conn.State.ToString() != "Open")
                                    {
                                        conn.Open();
                                    }

                                    cmdamtups1.ExecuteNonQuery();
                                    if (RadioButtonList1.SelectedValue == "0")
                                    {


                                        if (ddlAccountname1.SelectedIndex > 0)
                                        {
                                            if (dt.Rows.Count >= 2)
                                            {
                                                updatetdetail(ddlAccountname1.Text, Math.Round(Convert.ToDecimal(txtAmount1.Text), 2, MidpointRounding.AwayFromZero).ToString(), txtmemo1.Text, dt.Rows[1]["Tranction_Details_Id"].ToString());

                                            }
                                            else
                                            {
                                                insertTdetail("0", ddlAccountname1.SelectedValue.ToString(), "0", Math.Round(Convert.ToDecimal(txtAmount1.Text), 2, MidpointRounding.AwayFromZero).ToString(), ViewState["Trid"].ToString(), txtmemo1.Text.ToString());

                                            }
                                        }
                                        if (ddlAccountname2.SelectedIndex > 0)
                                        {
                                            if (dt.Rows.Count >= 3)
                                            {
                                                updatetdetail(ddlAccountname2.Text, Math.Round(Convert.ToDecimal(txtAmount2.Text), 2, MidpointRounding.AwayFromZero).ToString(), txtmemo1.Text, dt.Rows[2]["Tranction_Details_Id"].ToString());

                                            }
                                            else
                                            {
                                                insertTdetail(ddlAccountname2.SelectedValue.ToString(), "0", Math.Round(Convert.ToDecimal(txtAmount2.Text), 2, MidpointRounding.AwayFromZero).ToString(), "0", ViewState["Trid"].ToString(), txtmemo2.Text.ToString());

                                            }
                                        }
                                        if (ddlAccountname3.SelectedIndex > 0)
                                        {
                                            if (dt.Rows.Count >= 4)
                                            {
                                                updatetdetail(ddlAccountname3.Text, Math.Round(Convert.ToDecimal(txtAmount3.Text), 2, MidpointRounding.AwayFromZero).ToString(), txtmemo1.Text, dt.Rows[3]["Tranction_Details_Id"].ToString());

                                            }
                                            else
                                            {
                                                insertTdetail(ddlAccountname3.SelectedValue.ToString(), "0", Math.Round(Convert.ToDecimal(txtAmount3.Text), 2, MidpointRounding.AwayFromZero).ToString(), "0", ViewState["Trid"].ToString(), txtmemo3.Text.ToString());

                                            }
                                        }
                                        if (ddlAccountname4.SelectedIndex > 0)
                                        {
                                            if (dt.Rows.Count >= 5)
                                            {
                                                updatetdetail(ddlAccountname4.Text, Math.Round(Convert.ToDecimal(txtAmount4.Text), 2, MidpointRounding.AwayFromZero).ToString(), txtmemo1.Text, dt.Rows[4]["Tranction_Details_Id"].ToString());

                                            }
                                            else
                                            {
                                                insertTdetail(ddlAccountname4.SelectedValue.ToString(), "0", Math.Round(Convert.ToDecimal(txtAmount4.Text), 2, MidpointRounding.AwayFromZero).ToString(), "0", ViewState["Trid"].ToString(), txtmemo4.Text.ToString());

                                            }
                                        }
                                        if (ddlAccountname5.SelectedIndex > 0)
                                        {
                                            if (dt.Rows.Count >= 6)
                                            {
                                                updatetdetail(ddlAccountname5.Text, Math.Round(Convert.ToDecimal(txtAmount5.Text), 2, MidpointRounding.AwayFromZero).ToString(), txtmemo1.Text, dt.Rows[5]["Tranction_Details_Id"].ToString());

                                            }
                                            else
                                            {
                                                insertTdetail(ddlAccountname5.SelectedValue.ToString(), "0", Math.Round(Convert.ToDecimal(txtAmount5.Text), 2, MidpointRounding.AwayFromZero).ToString(), "0", ViewState["Trid"].ToString(), txtmemo5.Text.ToString());

                                            }
                                        }
                                        if (ddlAccountname6.SelectedIndex > 0)
                                        {
                                            if (dt.Rows.Count >= 7)
                                            {
                                                updatetdetail(ddlAccountname6.Text, Math.Round(Convert.ToDecimal(txtAmount6.Text), 2, MidpointRounding.AwayFromZero).ToString(), txtmemo1.Text, dt.Rows[6]["Tranction_Details_Id"].ToString());

                                            }
                                            else
                                            {
                                                insertTdetail(ddlAccountname6.SelectedValue.ToString(), "0", Math.Round(Convert.ToDecimal(txtAmount6.Text), 2, MidpointRounding.AwayFromZero).ToString(), "0", ViewState["Trid"].ToString(), txtmemo6.Text.ToString());

                                            }
                                        }
                                        if (ddlAccountname7.SelectedIndex > 0)
                                        {
                                            if (dt.Rows.Count >= 8)
                                            {
                                                updatetdetail(ddlAccountname7.Text, Math.Round(Convert.ToDecimal(txtAmount7.Text), 2, MidpointRounding.AwayFromZero).ToString(), txtmemo1.Text, dt.Rows[7]["Tranction_Details_Id"].ToString());

                                            }
                                            else
                                            {
                                                insertTdetail(ddlAccountname7.SelectedValue.ToString(), "0", Math.Round(Convert.ToDecimal(txtAmount7.Text), 2, MidpointRounding.AwayFromZero).ToString(), "0", ViewState["Trid"].ToString(), txtmemo7.Text.ToString());

                                            }
                                        }
                                        if (ddlAccountname8.SelectedIndex > 0)
                                        {
                                            if (dt.Rows.Count >= 9)
                                            {
                                                updatetdetail(ddlAccountname8.Text, Math.Round(Convert.ToDecimal(txtAmount8.Text), 2, MidpointRounding.AwayFromZero).ToString(), txtmemo1.Text, dt.Rows[8]["Tranction_Details_Id"].ToString());

                                            }
                                            else
                                            {
                                                insertTdetail(ddlAccountname8.SelectedValue.ToString(), "0", Math.Round(Convert.ToDecimal(txtAmount8.Text), 2, MidpointRounding.AwayFromZero).ToString(), "0", ViewState["Trid"].ToString(), txtmemo8.Text.ToString());

                                            }
                                        }
                                        if (ddlAccountname9.SelectedIndex > 0)
                                        {
                                            if (dt.Rows.Count >= 10)
                                            {
                                                updatetdetail(ddlAccountname9.Text, Math.Round(Convert.ToDecimal(txtAmount9.Text), 2, MidpointRounding.AwayFromZero).ToString(), txtmemo1.Text, dt.Rows[9]["Tranction_Details_Id"].ToString());

                                            }
                                            else
                                            {
                                                insertTdetail(ddlAccountname9.SelectedValue.ToString(), "0", Math.Round(Convert.ToDecimal(txtAmount9.Text), 2, MidpointRounding.AwayFromZero).ToString(), "0", ViewState["Trid"].ToString(), txtmemo9.Text.ToString());

                                            }
                                        }
                                        SqlCommand cmdamtups3 = new SqlCommand("Update TranctionMasterSuppliment set Memo  = '" + txtmmo.Text + "' where Tranction_Master_Id ='" + ViewState["Trid"] + "'", conn);
                                        if (conn.State.ToString() != "Open")
                                        {
                                            conn.Open();
                                        }
                                        cmdamtups3.ExecuteNonQuery();
                                        conn.Close();
                                    }
                                    if (Request.QueryString["Trid"] == null)
                                    {
                                        btnsubradio2.Visible = true;
                                        Button3.Visible = true;
                                        RadioButtonList1.Enabled = true;
                                        btnupdateAccc.Visible = false;
                                    }
                                    controlclear();
                                    lblmsg.Text = "Record updated successfully";
                                    lblmsg.Visible = true;
                                    lblgtotal.Text = "";
                                    txtmmo.Text = "";
                                    entryno();
                                }
                            }
                            else
                            {
                                lblmsg.Visible = true;
                                lblmsg.Text = "";
                                lblmsg.Text = "The payment you are trying to record for various expenses " + y.ToString() +
                    " does not add up to the total payment amount of " + txtpayamount.Text + ".Please adjust your payment amount to match your total expense.";
                                if (txtAmount1.Text == "" || ddlAccountname1.SelectedIndex == 0)
                                {
                                    lblamt1.Text = "*";
                                }
                                else if (txtAmount2.Text == "" || ddlAccountname2.SelectedIndex == 0)
                                {
                                    lblamt2.Text = "*";
                                }
                                else if (txtAmount3.Text == "" || ddlAccountname3.SelectedIndex == 0)
                                {
                                    lblamt3.Text = "*";
                                }
                                else if (txtAmount4.Text == "" || ddlAccountname4.SelectedIndex == 0)
                                {
                                    lblamt4.Text = "*";
                                }
                                else if (txtAmount5.Text == "" || ddlAccountname5.SelectedIndex == 0)
                                {
                                    lblamt5.Text = "*";
                                }
                                else if (txtAmount6.Text == "" || ddlAccountname6.SelectedIndex == 0)
                                {
                                    lblamt6.Text = "*";
                                }
                                else if (txtAmount7.Text == "" || ddlAccountname7.SelectedIndex == 0)
                                {
                                    lblamt7.Text = "*";
                                }
                                else if (txtAmount8.Text == "" || ddlAccountname8.SelectedIndex == 0)
                                {
                                    lblamt8.Text = "*";
                                }
                                else if (txtAmount9.Text == "" || ddlAccountname9.SelectedIndex == 0)
                                {
                                    lblamt9.Text = "*";
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblmsg.Text = "Sorry, You are not permitted to insert greater record as per Price plan";
                    lblmsg.Visible = true;
                }
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Please check your date. You cannot select any date earlier/later than start/end date of the year";
            }

        }
    }
    protected void insertTdetail(string accdebit, string acccredit, string amdebit, string amdcredit, string tid, string memo)
    {
        SqlCommand cmd = new SqlCommand("Insert into Tranction_Details(AccountDebit,AccountCredit,AmountDebit,AmountCredit,Tranction_Master_Id,Memo,DateTimeofTransaction,compid,whid) values('" + accdebit + "','" + acccredit + "','" + amdebit + "','" + amdcredit + "','" + tid + "','" + memo + "','" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "','" + compid + "','" + ddlwarehouse.SelectedValue + "')", conn);
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();

    }
    protected void updatetdetail(string Account, string amt, string memo, string tdid)
    {
        SqlCommand cmdamtups2 = new SqlCommand("Update Tranction_Details set AccountDebit='" + Account + "', AmountDebit = '" + amt + "',Memo  = '" + memo + "',DateTimeOfTransaction='" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "',Whid='" + ddlwarehouse.SelectedValue + "' where Tranction_Master_Id ='" + ViewState["Trid"] + "' and Tranction_Details_Id='" + tdid + "'", conn);
        if (conn.State.ToString() != "Open")
        {
            conn.Open();
        }
        cmdamtups2.ExecuteNonQuery();
        conn.Close();
    }

    protected void lnkadd0_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlwarehouse.SelectedIndex > 0)
        {
            SqlCommand cmd123 = new SqlCommand("select AccountId ,AccountName from AccountMaster where GroupId=1 and Status='1' and AccountMaster.compid = '" + compid + "' and Whid='" + ddlwarehouse.SelectedValue + "' order by AccountName", conn);
            SqlDataAdapter adp123 = new SqlDataAdapter(cmd123);
            DataTable dt123 = new DataTable();
            adp123.Fill(dt123);

            ddlAccount.DataSource = dt123;
            ddlAccount.DataValueField = "AccountId";
            ddlAccount.DataTextField = "AccountName";
            ddlAccount.DataBind();
            //ddlAccount.Items.Insert(0, "--Select--");
        }
    }
    protected void LinkButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlpartytypetype.SelectedIndex > 0)
        {

            CheckBox1.Visible = false;

            fillddlpartynamename();
        }
        else
        {

            // CheckBox1.Visible = true;
        }
    }

    protected void lnkadd_Click(object sender, ImageClickEventArgs e)
    {
        string te = "AccountMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }

    protected void LinkButton97666667_Click1(object sender, ImageClickEventArgs e)
    {
        string te = "PartyMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }

    protected void lnkadd2_Click(object sender, ImageClickEventArgs e)
    {
        fillacc();
    }
    protected void btngoservh_Click(object sender, EventArgs e)
    {
        if (ddlwarehouse.SelectedIndex !=-1)
        {
            SqlCommand cmd123 = new SqlCommand("select Tranction_Master_Id from TranctionMaster where Whid='" + ddlwarehouse.SelectedValue + "' and EntryNumber='" + txtserachno.Text + "' and EntryTypeId='1'", conn);
            SqlDataAdapter adp123 = new SqlDataAdapter(cmd123);
            DataTable dt123 = new DataTable();
            adp123.Fill(dt123);
            if (dt123.Rows.Count > 0)
            {
                ViewState["Trid"] = Convert.ToString(dt123.Rows[0]["Tranction_Master_Id"]);
                fillUpdate();
            }
            else
            {
                lblmsg.Text = "Sorry,This entry number not exist for current business.";
            }
        }
    }
    protected void btnprivious_Click(object sender, EventArgs e)
    {
        if (ddlwarehouse.SelectedIndex != -1)
        {
            SqlCommand cmd123t = new SqlCommand("select top(1) Tranction_Master_Id,EntryNumber from TranctionMaster where Whid='" + ddlwarehouse.SelectedValue + "'and EntryTypeId='1' and EntryNumber<'" + lblentrynumber.Text + "' Order by Tranction_Master_Id Desc", conn);
            SqlDataAdapter adp123t = new SqlDataAdapter(cmd123t);
            DataTable dt123t = new DataTable();
            adp123t.Fill(dt123t);
            if (dt123t.Rows.Count > 0)
            {


                SqlCommand cmd123 = new SqlCommand("select Tranction_Master_Id from TranctionMaster where Whid='" + ddlwarehouse.SelectedValue + "'and EntryTypeId='1' and EntryNumber='" + Convert.ToString(dt123t.Rows[0]["EntryNumber"]) + "'", conn);
                SqlDataAdapter adp123 = new SqlDataAdapter(cmd123);
                DataTable dt123 = new DataTable();
                adp123.Fill(dt123);
                if (dt123.Rows.Count > 0)
                {
                    ViewState["Trid"] = Convert.ToString(dt123.Rows[0]["Tranction_Master_Id"]);
                    fillUpdate();
                }
            }
            else
            {
                lblmsg.Text = "No Record Found.";
            }
        }
    }
    protected void btnnext_Click(object sender, EventArgs e)
    {
        if (ddlwarehouse.SelectedIndex != -1)
        {
            SqlCommand cmd123t = new SqlCommand("select top(1) Tranction_Master_Id,EntryNumber from TranctionMaster where Whid='" + ddlwarehouse.SelectedValue + "'and EntryTypeId='1' and EntryNumber>'" + lblentrynumber.Text + "' Order by Tranction_Master_Id Asc", conn);
            SqlDataAdapter adp123t = new SqlDataAdapter(cmd123t);
            DataTable dt123t = new DataTable();
            adp123t.Fill(dt123t);
            if (dt123t.Rows.Count > 0)
            {


                SqlCommand cmd123 = new SqlCommand("select Tranction_Master_Id from TranctionMaster where Whid='" + ddlwarehouse.SelectedValue + "'and EntryTypeId='1' and EntryNumber='" + Convert.ToString(dt123t.Rows[0]["EntryNumber"]) + "'", conn);
                SqlDataAdapter adp123 = new SqlDataAdapter(cmd123);
                DataTable dt123 = new DataTable();
                adp123.Fill(dt123);
                if (dt123.Rows.Count > 0)
                {
                    ViewState["Trid"] = Convert.ToString(dt123.Rows[0]["Tranction_Master_Id"]);
                    fillUpdate();
                }
            }
            else
            {
                lblmsg.Text = "No Record Found.";
            }
        }
    }
    protected void btneditgene_Click(object sender, EventArgs e)
    {
        btnupdatepaety.Visible = true;
        btneditgene.Visible = false;
        fillenableparty(true);
    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        btnupdateAccc.Visible = true;
        btnedit.Visible = false;
        fillenableparty(true);
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
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblwname.Text = "Business-Department Name : ";
       
        if (RadioButtonList2.SelectedValue == "0")
        {
            lblwname.Text = "Business Name : ";
           
          
            pnldiv.Visible = false;
            pnlemp.Visible = false;
            ddlbusiness.Items.Clear();
            ddlemployee.Items.Clear();
            if (Convert.ToString(ViewState["cd"]) != "1")
            {
                fillstore();
            }
        }
        if (RadioButtonList2.SelectedValue == "1")
        {
           
            pnldiv.Visible = false;
            pnlemp.Visible = false;
            ddlbusiness.Items.Clear();
            ddlemployee.Items.Clear();
            if (Convert.ToString(ViewState["cd"]) != "2")
            {
                ddldept();
            }
        }
        if (RadioButtonList2.SelectedValue == "2")
        {
          
            pnldiv.Visible = true;
            pnlemp.Visible = false;
            ddlemployee.Items.Clear();
            if (Convert.ToString(ViewState["cd"]) != "2")
            {
                ddldept();
            }
            fillbusiness();
        }
        if (RadioButtonList2.SelectedValue == "3")
        {
         
            ddlbusiness.Items.Clear();
            //Panel4.Visible = true;
            pnldiv.Visible = false;
            pnlemp.Visible = true;
            if (Convert.ToString(ViewState["cd"]) != "2")
            {
                ddldept();
            }
            fillemp();

        }
        fillobj();
    }
    protected void ddldept()
    {
        ddlstore.Items.Clear();

        ViewState["cd"] = "2";
        DataTable dsemp = MainAcocount.SelectDepartmentmasterMNCwithCID();
        if (dsemp.Rows.Count > 0)
        {
            ddlstore.DataSource = dsemp;
            ddlstore.DataTextField = "Departmentname";
            ddlstore.DataValueField = "id";
            ddlstore.DataBind();
        }
        //ddlstore.Items.Insert(0, "-Select-");
        //ddlstore.Items[0].Value = "0";
        //ddlDepartment_SelectedIndexChanged(sender, e);
    }
    protected void fillbusiness()
    {
        ddlbusiness.Items.Clear();
        if (ddlstore.SelectedIndex > -1)
        {

            DataTable ds1 = clsDocument.SelectDivisionwithStoreIdanddeptId(ddlstore.SelectedValue, "0", 1);


            ddlbusiness.DataSource = ds1;
            ddlbusiness.DataTextField = "BusinessName";
            ddlbusiness.DataValueField = "BusinessID";
            ddlbusiness.DataBind();
        }
        //ddlbusiness.Items.Insert(0, "Select");
        //ddlbusiness.Items[0].Value = "0";


    }
    protected void fillobj()
    {

        ddlproject.Items.Clear();
        string bus = "0";
        string em = "0";
        if (ddlemployee.SelectedIndex != -1)
        {
            em = ddlemployee.SelectedValue;
        }
        else
        {
            em = "0";
        }
        if (ddlbusiness.SelectedIndex != -1)
        {
            bus = ddlbusiness.SelectedValue;
        }
        else
        {
            bus = "0";
        }
        DataTable ds12 = new DataTable();
        if (RadioButtonList1.SelectedIndex == 0)
        {
            ds12 = ClsProject.SpProjectbydd(ddlstore.SelectedValue, "0", bus, em);

        }
        else
        {
            ds12 = ClsProject.SpProjectbydd("0", ddlstore.SelectedValue, bus, em);
        }
        if (ds12.Rows.Count > 0)
        {
            ddlproject.DataSource = ds12;

            ddlproject.DataMember = "projectname";
            ddlproject.DataTextField = "projectname";
            ddlproject.DataValueField = "ProjectId";


            ddlproject.DataBind();


        }
        //ddlobjective.Items.Insert(0, "-Select-");
        //ddlobjective.Items[0].Value = "0";


    }
    protected void fillemp()
    {
        ddlemployee.Items.Clear();
        if (ddlstore.SelectedIndex > -1)
        {
            DataTable ds12 = clsDocument.SelectEmployeeMasterwithDivId("0", 1, ddlstore.SelectedValue);

            ddlemployee.DataSource = ds12;
            ddlemployee.DataTextField = "employeename";
            ddlemployee.DataValueField = "EmployeeMasterID";
            ddlemployee.DataBind();
        }

    }
    protected void chktrackcost_CheckedChanged(object sender, EventArgs e)
    {
        if (chktrackcost.Checked == true)
        {
            pnltcost.Visible = true;
            RadioButtonList2_SelectedIndexChanged(sender, e);
        }
        else
        {
            pnltcost.Visible = false;
        }
    }
    protected void ddlstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList2.SelectedValue == "2")
        {
            fillbusiness();
        }
        if (RadioButtonList2.SelectedValue == "3")
        {
            fillemp();

        }
        fillobj();

    }
    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillobj();
    }
    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillobj();
    }
    protected void btnsall_Click(object sender, EventArgs e)
    {
        ddlproject.Items.Clear();
        string bus = "0";
        string em = "0";
        if (ddlemployee.SelectedIndex != -1)
        {
            em = ddlemployee.SelectedValue;
        }
        else
        {
            em = "0";
        }
        if (ddlbusiness.SelectedIndex != -1)
        {
            bus = ddlbusiness.SelectedValue;
        }
        else
        {
            bus = "0";
        }
        DataTable ds12 = new DataTable();
        if (RadioButtonList1.SelectedIndex == 0)
        {
            ds12 = ClsProject.SpProjectbyddnotcomplete(ddlstore.SelectedValue, "0", bus, em);

        }
        else
        {
            ds12 = ClsProject.SpProjectbyddnotcomplete("0", ddlstore.SelectedValue, bus, em);
        }
        if (ds12.Rows.Count > 0)
        {
            ddlproject.DataSource = ds12;

            ddlproject.DataMember = "projectname";
            ddlproject.DataTextField = "projectname";
            ddlproject.DataValueField = "ProjectId";


            ddlproject.DataBind();


        }
    }
    protected void ddlproject_SelectedIndexChanged(object sender, EventArgs e)
    {
        string str12 = "Select Status from ProjectMaster Where ProjectId='" + ddlproject.SelectedValue + "' ";
        SqlCommand cmd1 = new SqlCommand(str12, conn);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable ds1 = new DataTable();
        adp1.Fill(ds1);
        if (ds1.Rows.Count > 0)
        {
            ddlstatus.SelectedIndex = ddlproject.Items.IndexOf(ddlstatus.Items.FindByText(ds1.Rows[0]["Status"].ToString()));
        }
    }
    protected void ddlAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        openbal();
    }
   
    protected void lnkadd3_Click(object sender, ImageClickEventArgs e)
    {
        openbal();
    }

    protected void chkdc_CheckedChanged(object sender, EventArgs e)
    {
        if (chkdc.Checked == true)
        {
            pnldc.Visible = true;
        }
        else
        {
            pnldc.Visible = false;
        }
    }
    protected void btncashre_Click(object sender, EventArgs e)
    {
        string te = "CashRegister.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }

    protected void txtmmo_TextChanged(object sender, EventArgs e)
    {
        txtmemo1.Text = txtmmo.Text;
        txtmemo2.Text = txtmmo.Text;
        txtmemo3.Text = txtmmo.Text;
        txtmemo4.Text = txtmmo.Text;
        txtmemo5.Text = txtmmo.Text;
        txtmemo6.Text = txtmmo.Text;
        txtmemo7.Text = txtmmo.Text;
        txtmemo8.Text = txtmmo.Text;
        txtmemo9.Text = txtmmo.Text;
    }
    protected void txtAmount1_TextChanged(object sender, EventArgs e)
    {
        double zzz = 0;
        if (txtpayamount.Text != "")
        {
            if (txtAmount1.Text != "")
            {
                zzz = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount1.Text), 2, MidpointRounding.AwayFromZero));
            }
            if (txtAmount2.Text != "")
            {
                zzz += Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount2.Text), 2, MidpointRounding.AwayFromZero));
            }
            if (txtAmount3.Text != "")
            {
                zzz += Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount3.Text), 2, MidpointRounding.AwayFromZero));
            }
            if (txtAmount4.Text != "")
            {
                zzz += Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount4.Text), 2, MidpointRounding.AwayFromZero));
            }
            if (txtAmount5.Text != "")
            {
                zzz += Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount5.Text), 2, MidpointRounding.AwayFromZero));
            }
            if (txtAmount6.Text != "")
            {
                zzz += Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount6.Text), 2, MidpointRounding.AwayFromZero));
            }
            if (txtAmount7.Text != "")
            {
                zzz += Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount7.Text), 2, MidpointRounding.AwayFromZero));
            }
            if (txtAmount8.Text != "")
            {
                zzz += Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount8.Text), 2, MidpointRounding.AwayFromZero));
            }
            if (txtAmount9.Text != "")
            {
                zzz += Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount9.Text), 2, MidpointRounding.AwayFromZero));
            }
            
              
        }
       
        lblgtotal.Text = "";
        lblgtotal.Text = zzz.ToString();
           
        
    }
}
