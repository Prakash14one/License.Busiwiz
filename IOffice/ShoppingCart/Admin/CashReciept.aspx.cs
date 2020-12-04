using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using System.Net;
using System.Net.Mail;
public partial class CashReciept : System.Web.UI.Page
{
    int q;
    string s;
    //int q;
    String qryStr;
    int groupid = 0;
    string accid = "";
    int classid = 0;
    double x1 = 0, x2 = 0, x3 = 0, x4 = 0, x5 = 0, x6 = 0, x7 = 0, x8 = 0, x9 = 0, xz = 0;
   
   
      SqlConnection con=new SqlConnection(PageConn.connnn);
    DataSet dsnew = new DataSet();
    DataTable dtnew = new DataTable();
    string txtuserid;
    double db;
    //int flag = 0;
    string compid;
    string page;
    protected void Page_Load(object sender, EventArgs e)
    {
        //PageConn pgcon = new PageConn();
        //con = pgcon.dynconn;
        //conn = pgcon.dynconn;
      
           
            pagetitleclass pg = new pagetitleclass();
            string strData = Request.Url.ToString();

            char[] separator = new char[] { '/' };
            compid = Session["Comid"].ToString();
            string[] strSplitArr = strData.Split(separator);
            int i = Convert.ToInt32(strSplitArr.Length);
            page = strSplitArr[i - 1].ToString();


            Page.Title = pg.getPageTitle(page);
           
            if (Session["AccountId"] == null)
            {
                Button2.Visible = false;
                Button3.Visible = false;
            }
            else
            {
                Button2.Visible = true;
                Button3.Visible = true;
            }
            
           // Calendar1.Visible = false;
            lblmsg.Visible = false;
            Label2.Visible = false;
         

        if (!IsPostBack)
        {

            Fillddlwarehouse();

            txtTodate.Text = Convert.ToString(System.DateTime.Now.ToShortDateString());

            SqlCommand cmd511 = new SqlCommand("Sp_Select_Cash Receipt", con);
            cmd511.CommandType = CommandType.StoredProcedure;
            cmd511.Parameters.AddWithValue("@compid", Session["comid"]);
            cmd511.Parameters.AddWithValue("@whid", ddlwarehouse.SelectedValue);
            // cmd.Parameters.AddWithValue("@GroupId", Convert.ToInt32(ddlGroupName1.SelectedValue));
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
                    txtenteryNumber.Text = b.ToString();
                }
                else
                {
                    txtenteryNumber.Text = "1";
                   
                }
                //ViewState["Entrytypeno"] = ds511.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                txtenteryNumber.Text = "1";
            }


            if (Session["AccountId"] == null)
            { }
            else
            {

                Button2.Visible = true;
                Button3.Visible = true;

                txtAmount1.Text = Session["AmountCredit"].ToString();
                txtTodate.Text = Session["Date"].ToString();
                txtenteryNumber.Text = Session["EntryNumber"].ToString();
                txtAmount.Text = Session["AmountCredit"].ToString();


                // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                SqlCommand cmd5 = new SqlCommand("Sp_Select_TranctionId", con);
                cmd5.CommandType = CommandType.StoredProcedure;
                cmd5.Parameters.AddWithValue("@Tranction_Details_Id", Session["AccountId"].ToString());
                SqlDataAdapter adp5 = new SqlDataAdapter(cmd5);
                DataSet ds5 = new DataSet();
                adp5.Fill(ds5);
                ViewState["TranctionMasterID"] = ds5.Tables[0].Rows[0][0].ToString();

                ViewState["AccountName1Id"] = ds5.Tables[0].Rows[0][2].ToString();

                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                SqlCommand cmd58 = new SqlCommand("Sp_Select_AccountNameforAccountCredit", con);
                cmd58.CommandType = CommandType.StoredProcedure;
                cmd58.Parameters.AddWithValue("@Tranction_Details_Id", Session["AccountId"].ToString());
                SqlDataAdapter adp58 = new SqlDataAdapter(cmd58);
                DataSet ds58 = new DataSet();
                adp58.Fill(ds58);
                ddlAccount.SelectedValue = ds58.Tables[0].Rows[0][0].ToString();
                txtmemo1.Text = ds58.Tables[0].Rows[0][1].ToString();


                ddlAccountname1_SelectedIndexChanged(sender, e);
                ddlAccountname1.SelectedValue = ViewState["AccountName1Id"].ToString();
                //ddlPartyName.SelectedItem.Text= .ToString();
                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                SqlCommand cmd4 = new SqlCommand("Sp_Select_DepartmentById", con);
                cmd4.CommandType = CommandType.StoredProcedure;
                cmd4.Parameters.AddWithValue("@Compname", Session["PartyName"].ToString());
                cmd4.Parameters.AddWithValue("@compid", compid);
                SqlDataAdapter adp4 = new SqlDataAdapter(cmd4);
                DataSet ds4 = new DataSet();
                adp4.Fill(ds4);
                ViewState["Dept2"] = ds4.Tables[0].Rows[0][0].ToString();


            }
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
            }
        }
    }
    protected void Fillddlwarehouse()
    {

        string str = "SELECT Distinct WareHouseId,Name  FROM WareHouseMaster inner join EmployeeWarehouseRights on EmployeeWarehouseRights.Whid=WareHouseMaster.WareHouseId where comid = '" + Session["comid"] + "'and WareHouseMaster.Status='" + 1 + "' and EmployeeWarehouseRights.AccessAllowed='True' order by name";

        SqlCommand cmd = new SqlCommand(str, con);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        ddlwarehouse.DataSource = dt;
        ddlwarehouse.DataTextField = "Name";
        ddlwarehouse.DataValueField = "WareHouseId";
        ddlwarehouse.DataBind();
        ddlwarehouse.Items.Insert(0, "-Select-");

        ddlwarehouse.Items[0].Value = "0";
        ddlAccount.Items.Insert(0, "--Select--");
        if (Request.QueryString["docid"] != null)
        {

            string sssx11 = "SELECT WarehouseMaster.WarehouseId, DocumentMainType.DocumentMainType + '/'+DocumentSubType.DocumentSubType +'/'+ DocumentType.DocumentType as DocumentType,DocumentDate,DocumentTitle,DocumentId FROM WarehouseMaster inner join DocumentMainType on DocumentMainType.Whid=WarehouseId inner join DocumentSubType on DocumentSubType.DocumentMainTypeId=DocumentMainType.DocumentMainTypeId inner join DocumentType on DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId inner join DocumentMaster on DocumentMaster.DocumentTypeId=DocumentType.DocumentTypeId where DocumentId='" + Request.QueryString["docid"] + "'";
            SqlDataAdapter adpt11 = new SqlDataAdapter(sssx11, con);
            DataTable dtpt11 = new DataTable();
            adpt11.Fill(dtpt11);
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
                
                pnnl.Visible = true;
                if (Request.QueryString["docid"] != null)
                {
                    chkdoc.Checked = false;
                    chkdoc.Visible = false;
                }
                else
                {

                    chkdoc.Visible = true;

                }
            }
        }
        else
        {
            string eeed = " Select distinct EmployeeMaster.Whid from  EmployeeMaster where EmployeeMasterId='" + Session["EmployeeId"] + "'";
            SqlCommand cmdeeed = new SqlCommand(eeed, con);
            SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
            DataTable dteeed = new DataTable();
            adpeeed.Fill(dteeed);
            if (dteeed.Rows.Count > 0)
            {
                ddlwarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);

            }
        }
    }
    private void ddlAccountname1_SelectedIndexChanged(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
    public DataSet fillddl()
    {
                SqlCommand cmd = new SqlCommand("SELECT  distinct  GroupCompanyMaster.groupdisplayname + '-' + AccountMaster.AccountName AS Account,  AccountMaster.AccountId  FROM       AccountMaster INNER JOIN  GroupCompanyMaster ON AccountMaster.GroupId = GroupCompanyMaster.GroupId  WHERE   (AccountMaster.AccountId IS NOT NULL) and AccountMaster.Status=1 and AccountMaster.compid = '" + Session["comid"] + "' and accountmaster.Whid='" + ddlwarehouse.SelectedValue + "' and GroupCompanyMaster.Whid='" + ddlwarehouse.SelectedValue + "'  ORDER BY Account", con);
       
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;

    }

    public void entryno()
    {
       
        SqlCommand cmd511 = new SqlCommand("Sp_Select_Cash Receipt", con);
        cmd511.CommandType = CommandType.StoredProcedure;
        // cmd.Parameters.AddWithValue("@GroupId", Convert.ToInt32(ddlGroupName1.SelectedValue));
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
                txtenteryNumber.Text = b.ToString();
            }
            else
            {
                txtenteryNumber.Text = "1";
                //ViewState["Entrytypeno"] = "1";
            }


            //ViewState["Entrytypeno"] = ds511.Tables[0].Rows[0][0].ToString();
        }
        else
        {
            txtenteryNumber.Text = "1";
        }
        
    }

     public DataSet fillClassType()
     {
                 SqlCommand cmd = new SqlCommand("Sp_Select_PartyTypeMaster", con);
         cmd.CommandType = CommandType.StoredProcedure;
         cmd.Parameters.AddWithValue("@compid", compid);
         SqlDataAdapter adp = new SqlDataAdapter(cmd);
         DataSet ds = new DataSet();
         adp.Fill(ds);
         return ds;
     }
    public DataSet fillddl2()
    {
             //SqlCommand cmd = new SqlCommand("Sp_Select_AccountName", con);
        //cmd.CommandType = CommandType.StoredProcedure;
        //SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //adp.Fill(ds);
        //return ds;
        SqlCommand cmd = new SqlCommand("SELECT     AccountMaster.AccountName,AccountMaster.AccountId,GroupMaster.GroupId " +
" FROM         AccountMaster INNER JOIN " +
                     "GroupMaster ON AccountMaster.GroupId = GroupMaster.GroupId " +
" WHERE     (GroupMaster.GroupId = 1) and AccountMaster.Status=1 and AccountMaster.compid = '" + compid + "'  and AccountMaster.Whid='"+ ddlwarehouse.SelectedValue+"' order by AccountMaster.AccountName asc", con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        dtp.Fill(ds);
        return ds;


    }

    protected void ddlClassName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }    
    protected void btnSubmit_Click8(object sender, EventArgs e)
    {
        //DropDownList db = GridView8.FindControl("DropDownList3") as DropDownList;

        //db.DataSource = (DataSet)fillddl();
        //db.DataTextField = "AccountName";
        //db.DataValueField = "AccountId";
        //db.DataBind();

    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
    }    

    protected void rbVendor_CheckedChanged(object sender, EventArgs e)
    {
        ddlPartyName.DataSource = (DataSet)fillddl3();
        ddlPartyName.DataTextField = "Compname";
        ddlPartyName.DataValueField = "PartyID";
        ddlPartyName.DataBind();
        ddlPartyName.Items.Insert(0, "--Select--");
    }
    public DataSet fillddl3()
    {
               SqlCommand cmd = new SqlCommand("Sp_Select_PartyNamebyId", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@DeptID", 42);
        cmd.Parameters.AddWithValue("@compid", compid);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;



    }
    protected void rbCustomer_CheckedChanged(object sender, EventArgs e)
    {
        ddlPartyName.DataSource = (DataSet)fillddlcus();
        ddlPartyName.DataTextField = "Compname";
        ddlPartyName.DataValueField = "PartyID";
        ddlPartyName.DataBind();
        ddlPartyName.Items.Insert(0, "--Select--");
    }
    public DataSet fillddlcus()
    {
             SqlCommand cmd = new SqlCommand("Sp_Select_PartyNamebyId", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@deptid", 41);
        cmd.Parameters.AddWithValue("@compid", compid);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;



    }
    protected void rbOther_CheckedChanged(object sender, EventArgs e)
    {
        ddlPartyName.DataSource = (DataSet)fillother();
        ddlPartyName.DataTextField = "Compname";
        ddlPartyName.DataValueField = "PartyID";
        ddlPartyName.DataBind();
        ddlPartyName.Items.Insert(0, "--Select--");
    }
    public DataSet fillother()
    {
              SqlCommand cmd = new SqlCommand("Sp_Select_PartyNamebyId", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@deptid", 43);
        cmd.Parameters.AddWithValue("@compid", compid);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;



    }    
    protected void rbEmployee_CheckedChanged(object sender, EventArgs e)
    {
        ddlPartyName.DataSource = (DataSet)fillddlemp();
        ddlPartyName.DataTextField = "Compname";
        ddlPartyName.DataValueField = "PartyID";
        ddlPartyName.DataBind();
        ddlPartyName.Items.Insert(0, "--Select--");

    }
    public DataSet fillddlemp()
    {
              SqlCommand cmd = new SqlCommand("Sp_Select_PartyNamebyId", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@deptid", 44);
        cmd.Parameters.AddWithValue("@compid", compid);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }
 
   
    protected void ddlAccountname1_SelectedIndexChanged1(object sender, EventArgs e)
    {

        if (ddlAccountname1.SelectedIndex > 0)
        {
            if (txtAmount.Text != "")
            {
                xz = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount.Text),2,MidpointRounding.AwayFromZero).ToString());

                txtAmount1.Text = xz.ToString();
                lblgratot.Text = "";
                lblgratot.Text = xz.ToString();
                String.Format("{0:n}", Convert.ToDecimal(lblgratot.Text));
  
            }
         }
        else
        { }
    }
    protected void ddlAccountname2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAccountname2.SelectedIndex > 0)
        {
            if (txtAmount.Text != "" && txtAmount1.Text != "")
            {
                xz = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount.Text),2,MidpointRounding.AwayFromZero).ToString());
                x1 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount1.Text),2,MidpointRounding.AwayFromZero).ToString());
                //double tot = x1;
                //lblgratot.Text = "";
                //lblgratot.Text = Convert.ToString(tot);
                lblgratot.Text = "";
                lblgratot.Text = xz.ToString();
                String.Format("{0:n}", Convert.ToDecimal(lblgratot.Text));
  
                if (xz <= 0)
                {

                }
                else if (x1 > 0)
                {
                    x2 = xz - x1;
                    txtAmount2.Text = x2.ToString();

                }
                else
                {
                    x2 = xz - x1;
                    txtAmount2.Text = x2.ToString();
                }
                x2 = xz - x1;
            }
        }
        else
        { }
    }
    protected void ddlAccountname3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtAmount.Text != "" && txtAmount1.Text != "" && txtAmount2.Text != "")
        {
            xz = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount.Text),2,MidpointRounding.AwayFromZero).ToString());
            x1 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount1.Text),2,MidpointRounding.AwayFromZero).ToString());
            x2 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount2.Text),2,MidpointRounding.AwayFromZero).ToString());
            //double tot = x1 + x2;
            //lblgratot.Text = "";
            //lblgratot.Text = Convert.ToString(tot);
            lblgratot.Text = "";
            lblgratot.Text = xz.ToString();
            String.Format("{0:n}", Convert.ToDecimal(lblgratot.Text));
            if (ddlAccountname3.SelectedIndex > 0)
            {
                if (x2 > 0)
                {
                    x3 = xz - x2 - x1;
                    txtAmount3.Text = x3.ToString();

                }
                else if (x2 < 0)
                {
                    x3 = xz - x2 - x1;
                    txtAmount3.Text = x3.ToString();
                }
                x3 = xz - x2 - x1;
            }
        }
        else
        { }

        
    }
    protected void ddlAccountname4_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtAmount.Text != "" && txtAmount1.Text != "" && txtAmount2.Text != "" && txtAmount3.Text != "")
        {
            xz = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount.Text),2,MidpointRounding.AwayFromZero).ToString());
            x1 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount1.Text),2,MidpointRounding.AwayFromZero).ToString());
            x2 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount2.Text),2,MidpointRounding.AwayFromZero).ToString());
            x3 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount3.Text),2,MidpointRounding.AwayFromZero).ToString());
            //double tot = x1 + x2 + x3;
            //lblgratot.Text = "";
            //lblgratot.Text = Convert.ToString(tot);
            lblgratot.Text = "";
            lblgratot.Text = xz.ToString();
            String.Format("{0:n}", Convert.ToDecimal(lblgratot.Text));
  
            if (ddlAccountname4.SelectedIndex > 0)
            {
                if (x3 > 0)
                {
                    x4 = xz - x2 - x1 - x3;
                    txtAmount4.Text = x4.ToString();

                }
                else if (x3 < 0)
                {
                    x4 = xz - x2 - x1 - x3;
                    txtAmount4.Text = x4.ToString();
                }

                x4 = xz - x2 - x1 - x3;
            }
        }
        else
        { }
    }
    protected void ddlAccountname5_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtAmount.Text != "" && txtAmount1.Text != "" && txtAmount2.Text != "" && txtAmount3.Text != "" && txtAmount4.Text != "")
        {
            xz = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount.Text),2,MidpointRounding.AwayFromZero).ToString());
            x1 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount1.Text),2,MidpointRounding.AwayFromZero).ToString());
            x2 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount2.Text),2,MidpointRounding.AwayFromZero).ToString());
            x3 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount3.Text),2,MidpointRounding.AwayFromZero).ToString());
            x4 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount4.Text),2,MidpointRounding.AwayFromZero).ToString());

            //double tot = x1 + x2 + x3 + x4;
            //lblgratot.Text = "";
            //lblgratot.Text = Convert.ToString(tot);
            lblgratot.Text = "";
            lblgratot.Text = xz.ToString();
            String.Format("{0:n}", Convert.ToDecimal(lblgratot.Text));
  
            if (ddlAccountname5.SelectedIndex > 0)
            {

                if (x4 > 0)
                {
                    x5 = xz - x2 - x1 - x3 - x4;
                    txtAmount5.Text = x5.ToString();

                }
                else if (x4 < 0)
                {
                    x5 = xz - x2 - x1 - x3 - x4;
                    txtAmount5.Text = x5.ToString();
                }
                x5 = xz - x2 - x1 - x3 - x4;
            }
        }
        else
        { }
    }
    protected void ddlAccountname6_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtAmount.Text != "" && txtAmount1.Text != "" && txtAmount2.Text != "" && txtAmount3.Text != "" && txtAmount4.Text != "" && txtAmount5.Text != "")
        {
            xz = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount.Text),2,MidpointRounding.AwayFromZero).ToString());
            x1 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount1.Text),2,MidpointRounding.AwayFromZero).ToString());
            x2 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount2.Text),2,MidpointRounding.AwayFromZero).ToString());
            x3 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount3.Text),2,MidpointRounding.AwayFromZero).ToString());
            x4 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount4.Text),2,MidpointRounding.AwayFromZero).ToString());
            x5 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount5.Text),2,MidpointRounding.AwayFromZero).ToString());

            //double tot = x1 + x2 + x3 + x4 + x5;
            //lblgratot.Text = "";
            //lblgratot.Text = Convert.ToString(tot);
            lblgratot.Text = "";
            lblgratot.Text = xz.ToString();
            String.Format("{0:n}", Convert.ToDecimal(lblgratot.Text));
  
            if (ddlAccountname6.SelectedIndex > 0)
            {
                if (x5 > 0)
                {
                    x6 = xz - x2 - x1 - x3 - x4 - x5;
                    txtAmount6.Text = x6.ToString();

                }
                else if (x5 < 0)
                {
                    x6 = xz - x2 - x1 - x3 - x4 - x5;
                    txtAmount6.Text = x6.ToString();
                }
                x6 = xz - x2 - x1 - x3 - x4 - x5;
            }
        }
        else
        { }
    }
    protected void ddlAccountname7_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtAmount.Text != "" && txtAmount1.Text != "" && txtAmount2.Text != "" && txtAmount3.Text != "" && txtAmount4.Text != "" && txtAmount5.Text != ""  && txtAmount6.Text != "")
        {
            xz = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount.Text),2,MidpointRounding.AwayFromZero).ToString());
            x1 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount1.Text),2,MidpointRounding.AwayFromZero).ToString());
            x2 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount2.Text),2,MidpointRounding.AwayFromZero).ToString());
            x3 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount3.Text),2,MidpointRounding.AwayFromZero).ToString());
            x4 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount4.Text),2,MidpointRounding.AwayFromZero).ToString());
            x5 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount5.Text),2,MidpointRounding.AwayFromZero).ToString());
            x6 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount6.Text),2,MidpointRounding.AwayFromZero).ToString());

            //double tot = x1 + x2 + x3 + x4 + x5 + x6;
            //lblgratot.Text = "";
            //lblgratot.Text = Convert.ToString(tot);
            lblgratot.Text = "";
            lblgratot.Text = xz.ToString();
            String.Format("{0:n}", Convert.ToDecimal(lblgratot.Text));
  
            if (ddlAccountname7.SelectedIndex > 0)
            {
                if (x6 > 0)
                {
                    x7 = xz - x2 - x1 - x3 - x4 - x5 - x6;
                    txtAmount7.Text = x7.ToString();

                }
                else if (x6 < 0)
                {
                    x7 = xz - x2 - x1 - x3 - x4 - x5 - x6;
                    txtAmount7.Text = x7.ToString();
                }
                x7 = xz - x2 - x1 - x3 - x4 - x5 - x6;
            }
        }
        else
        { }
    }
    protected void ddlAccountname8_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtAmount.Text != "" && txtAmount1.Text != "" && txtAmount2.Text != "" && txtAmount3.Text != "" && txtAmount4.Text != "" && txtAmount5.Text != "" && txtAmount6.Text != "" && txtAmount7.Text != "")
        {
            xz = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount.Text),2,MidpointRounding.AwayFromZero).ToString());
            x1 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount1.Text),2,MidpointRounding.AwayFromZero).ToString());
            x2 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount2.Text),2,MidpointRounding.AwayFromZero).ToString());
            x3 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount3.Text),2,MidpointRounding.AwayFromZero).ToString());
            x4 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount4.Text),2,MidpointRounding.AwayFromZero).ToString());
            x5 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount5.Text),2,MidpointRounding.AwayFromZero).ToString());
            x6 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount6.Text),2,MidpointRounding.AwayFromZero).ToString());
            x7 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount7.Text),2,MidpointRounding.AwayFromZero).ToString());

            //double tot = x1 + x2 + x3 + x4 + x5 + x6 + x7;
            //lblgratot.Text = "";
            //lblgratot.Text = Convert.ToString(tot);
            lblgratot.Text = "";
            lblgratot.Text = xz.ToString();
            String.Format("{0:n}", Convert.ToDecimal(lblgratot.Text));
  
            if (ddlAccountname8.SelectedIndex > 0)
            {
                if (x7 > 0)
                {
                    x8 = xz - x2 - x1 - x3 - x4 - x5 - x6 - x7;
                    txtAmount8.Text = x8.ToString();

                }
                else if (x7 < 0)
                {
                    x8 = xz - x2 - x1 - x3 - x4 - x5 - x6 - x7;
                    txtAmount8.Text = x8.ToString();
                }
                x8 = xz - x2 - x1 - x3 - x4 - x5 - x6 - x7;
            }
        }
        else
        { }

    }
    protected void ddlAccountname9_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtAmount.Text != "" && txtAmount1.Text != "" && txtAmount2.Text != "" && txtAmount3.Text != "" && txtAmount4.Text != "" && txtAmount5.Text != "" && txtAmount6.Text != "" && txtAmount7.Text != "" && txtAmount8.Text != "")
        {
            xz = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount.Text),2,MidpointRounding.AwayFromZero).ToString());
            x1 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount1.Text),2,MidpointRounding.AwayFromZero).ToString());
            x2 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount2.Text),2,MidpointRounding.AwayFromZero).ToString());
            x3 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount3.Text),2,MidpointRounding.AwayFromZero).ToString());
            x4 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount4.Text),2,MidpointRounding.AwayFromZero).ToString());
            x5 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount5.Text),2,MidpointRounding.AwayFromZero).ToString());
            x6 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount6.Text),2,MidpointRounding.AwayFromZero).ToString());
            x7 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount7.Text),2,MidpointRounding.AwayFromZero).ToString());
            x8 = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount8.Text),2,MidpointRounding.AwayFromZero).ToString());
            //double tot = x1 + x2 + x3 + x4 + x5 + x6 + x7 + x8;
            //lblgratot.Text = "";
            //lblgratot.Text = Convert.ToString(tot);
            lblgratot.Text = "";
            lblgratot.Text = xz.ToString();
            String.Format("{0:n}", Convert.ToDecimal(lblgratot.Text));
  
            if (ddlAccountname9.SelectedIndex > 0)
            {
                if (x8 > 0)
                {
                    x9 = xz - x2 - x1 - x3 - x4 - x5 - x6 - x7 - x8;
                    txtAmount9.Text = x9.ToString();

                }
                else if (x8 < 0)
                {
                    x9 = xz - x2 - x1 - x3 - x4 - x5 - x6 - x7 - x8;
                    txtAmount9.Text = x9.ToString();
                }
                x9 = xz - x2 - x1 - x3 - x4 - x5 - x6 - x7 - x8;
            }
        }
        else
        { }
    }
    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (args.Value == "--Select--")
        { 
            args.IsValid = false; q = 1; }
        else
        {
            args.IsValid = true; q = 0;

        }
    }
    protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
    {

        if (args.Value == "--Select--")
        { args.IsValid = false; q = 1; }
        else
        {
            args.IsValid = true; q = 0;

        }
    }
    protected void CustomValidator1_ServerValidate1(object source, ServerValidateEventArgs args)
    {
        if (args.Value == "--Select--")
        { args.IsValid = false; q = 1; }
        else
        {
            args.IsValid = true; q = 0;

        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        //Calendar1.Visible = true;
    }


    protected void Button2_Click(object sender, EventArgs e)
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
                    dt = (DataTable)select("Select distinct EntryTypeMaster.Entry_Type_Name, TranctionMaster.Tranction_Amount, TranctionMaster.Whid,TranctionMasterSuppliment.Memo,TranctionMaster.EntryNumber, TranctionMaster.EntryTypeId,TranctionMaster.Tranction_Master_Id,TranctionMaster.Whid,Tranction_Details.AccountDebit,Tranction_Details.AccountCredit,Tranction_Details.AmountCredit,Tranction_Details.AmountDebit,Tranction_Details.Tranction_Details_Id,TranctionMasterSuppliment.Tranction_Master_SupplimentId,TranctionMaster.Date,TranctionMasterSuppliment.Party_MasterId from EntryTypeMaster inner join TranctionMaster on TranctionMaster.EntryTypeId=EntryTypeMaster.Entry_Type_Id inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id inner join TranctionMasterSuppliment on TranctionMasterSuppliment.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where TranctionMaster.compid='" + Session["Comid"] + "' and TranctionMaster.Tranction_Master_Id='" + ViewState["Trid"] + "' and TranctionMaster.EntryTypeId='2'");
                    if (dt.Rows.Count > 0)
                    {

                        ViewState["Trid"] = Convert.ToString(dt.Rows[0]["Tranction_Master_Id"]);


                        SqlCommand cmdamtups = new SqlCommand("Update TranctionMaster set Tranction_Amount = '" + Math.Round(Convert.ToDecimal(txtAmount.Text), 2, MidpointRounding.AwayFromZero).ToString() + "',Date='" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "',Whid='" + ddlwarehouse.SelectedValue + "' where Tranction_Master_Id ='" + ViewState["Trid"] + "'", con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdamtups.ExecuteNonQuery();
                        SqlCommand cmdamtups1 = new SqlCommand("Update Tranction_Details set AccountDebit='" + ddlAccount.SelectedValue + "', AmountDebit='" + Math.Round(Convert.ToDecimal(txtAmount.Text), 2, MidpointRounding.AwayFromZero).ToString() + "', Memo  = '" + txtMemo.Text + "',DateTimeOfTransaction='" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "',Whid='" + ddlwarehouse.SelectedValue + "'  where Tranction_Master_Id ='" + ViewState["Trid"] + "' and Tranction_Details_Id='" + dt.Rows[0]["Tranction_Details_Id"] + "'", con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }

                        cmdamtups1.ExecuteNonQuery();
                        if (rbClassType.SelectedValue == "1")
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
                                    insertTdetail("0", ddlAccountname2.SelectedValue.ToString(), "0", Math.Round(Convert.ToDecimal(txtAmount2.Text), 2, MidpointRounding.AwayFromZero).ToString(), ViewState["Trid"].ToString(), txtmemo2.Text.ToString());

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
                                    insertTdetail("0", ddlAccountname3.SelectedValue.ToString(), "0", Math.Round(Convert.ToDecimal(txtAmount3.Text), 2, MidpointRounding.AwayFromZero).ToString(), ViewState["Trid"].ToString(), txtmemo3.Text.ToString());

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
                                    insertTdetail("0", ddlAccountname4.SelectedValue.ToString(), "0", Math.Round(Convert.ToDecimal(txtAmount4.Text), 2, MidpointRounding.AwayFromZero).ToString(), ViewState["Trid"].ToString(), txtmemo4.Text.ToString());

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
                                    insertTdetail("0", ddlAccountname5.SelectedValue.ToString(), "0", Math.Round(Convert.ToDecimal(txtAmount5.Text), 2, MidpointRounding.AwayFromZero).ToString(), ViewState["Trid"].ToString(), txtmemo5.Text.ToString());

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
                                    insertTdetail("0", ddlAccountname6.SelectedValue.ToString(), "0", Math.Round(Convert.ToDecimal(txtAmount6.Text), 2, MidpointRounding.AwayFromZero).ToString(), ViewState["Trid"].ToString(), txtmemo6.Text.ToString());

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
                                    insertTdetail("0", ddlAccountname7.SelectedValue.ToString(), "0", Math.Round(Convert.ToDecimal(txtAmount7.Text), 2, MidpointRounding.AwayFromZero).ToString(), ViewState["Trid"].ToString(), txtmemo7.Text.ToString());

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
                                    insertTdetail("0", ddlAccountname8.SelectedValue.ToString(), "0", Math.Round(Convert.ToDecimal(txtAmount8.Text), 2, MidpointRounding.AwayFromZero).ToString(), ViewState["Trid"].ToString(), txtmemo8.Text.ToString());

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
                                    insertTdetail("0", ddlAccountname9.SelectedValue.ToString(), "0", Math.Round(Convert.ToDecimal(txtAmount9.Text), 2, MidpointRounding.AwayFromZero).ToString(), ViewState["Trid"].ToString(), txtmemo9.Text.ToString());

                                }
                            }
                            SqlCommand cmdamtups3 = new SqlCommand("Update TranctionMasterSuppliment set Memo  = '" + txtMemo.Text + "' where Tranction_Master_Id ='" + ViewState["Trid"] + "'", con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdamtups3.ExecuteNonQuery();
                        }
                        else if (rbClassType.SelectedValue == "2")
                        {
                            SqlCommand cmds = new SqlCommand("select Account From Party_master left outer join PartytTypeMaster  on Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId where PartyID='" + ddlPartyName.SelectedValue + "' and PartytTypeMaster.compid = '" + compid + "' and Party_master.Whid='" + ddlwarehouse.SelectedValue + "'", con);

                            SqlDataAdapter dtps = new SqlDataAdapter(cmds);
                            DataTable dss = new DataTable();
                            dtps.Fill(dss);
                            if (dss.Rows.Count > 0)
                            {
                                updatetdetail(dss.Rows[0]["Account"].ToString(), Math.Round(Convert.ToDecimal(txtAmount.Text), 2, MidpointRounding.AwayFromZero).ToString(), txtMemo.Text, dt.Rows[1]["Tranction_Details_Id"].ToString());


                            }
                            SqlCommand cmdamtups3 = new SqlCommand("Update TranctionMasterSuppliment set Party_MasterId='" + ddlPartyName.SelectedValue + "', Memo  = '" + txtMemo.Text + "' where Tranction_Master_Id ='" + ViewState["Trid"] + "'", con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            cmdamtups3.ExecuteNonQuery();

                        }
                        controlclear();
                        lblmsg.Text = "Record updated successfully";
                        lblmsg.Visible = true;
                        lblgratot.Text = "";

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
    protected void insertTdetail(string accdebit,string acccredit,string amdebit,string amdcredit, string tid, string memo)
    {
        SqlCommand cmd = new SqlCommand("Insert into Tranction_Details(AccountDebit,AccountCredit,AmountDebit,AmountCredit,Tranction_Master_Id,Memo,DateTimeofTransaction,compid,whid) values('" + accdebit + "','" + acccredit + "','" + amdebit + "','"+amdcredit+"','" + tid + "','" + memo + "','" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "','" + compid + "','" + ddlwarehouse.SelectedValue + "')", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
           cmd.ExecuteNonQuery();
           con.Close();
        
    }
    protected void updatetdetail(string Account, string amt,string memo, string tdid)
    {
        SqlCommand cmdamtups2 = new SqlCommand("Update Tranction_Details set AccountCredit='" + Account + "', AmountCredit = '" + amt + "',Memo  = '" + memo + "',DateTimeOfTransaction='" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "',Whid='" + ddlwarehouse.SelectedValue + "' where Tranction_Master_Id ='" + ViewState["Trid"] + "' and Tranction_Details_Id='" + tdid + "'", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmdamtups2.ExecuteNonQuery();
        con.Close();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
       
    }
   
    protected void rbClassType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbClassType.SelectedValue == "2")
        {
            controlclear();
            //txtAmount.Text = "";
            lblmsg.Visible = false;
            Panel1.Visible = false;
            //chkbkinvoice.Visible = true;
            chkappamount.Visible = true;

            ddlPartyName.Visible = true;
            lblpartytype.Visible = true;
            ddlpartytype.Visible = true;
            lblpartname.Visible = true;
            ImageButton3.Visible = true;
            ImageButton4.Visible = true;
            SqlCommand cmdpt = new SqlCommand("Select PartType,PartyTypeId from PartytTypeMaster where PartytTypeMaster.compid = '"+compid+"' order by PartType", con);
            SqlDataAdapter dtppt = new SqlDataAdapter(cmdpt);
            DataTable dtpt = new DataTable();
            dtppt.Fill(dtpt);

            if (dtpt.Rows.Count > 0)
            {
                ddlpartytype.DataSource = dtpt;
                ddlpartytype.DataTextField = "PartType";
                ddlpartytype.DataValueField = "PartyTypeId";
                ddlpartytype.DataBind();
                ddlpartytype.Items.Insert(0, "-Select-");
                ddlpartytype.Items[0].Value = "0";
                ddlpartytype.SelectedIndex = ddlpartytype.Items.IndexOf(ddlpartytype.Items.FindByText("Customer"));
            }
            ddlpartytype_SelectedIndexChanged(sender, e);
        }
        else
        {
            ddlPartyName.Items.Insert(0, "--Select--");
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


            controlclear();
          //  txtAmount.Text = "";
            lblmsg.Visible = false;
            Panel1.Visible = true;
            pnlinvdata.Visible = true;
            GridView2.DataSource = null;
            GridView2.DataBind();
            chkbkinvoice.Visible = false;
            chkbkinvoice.Checked = false;
            Panel2.Visible = false;
            chkappamount.Visible = false;
            chkorder.Visible = false;
            ddlpartytype.Visible = false;
            ddlPartyName.Visible = false;
            lblpartname.Visible = false;
            lblpartytype.Visible = false;
            ImageButton3.Visible = false;
            ImageButton4.Visible = false;
         
        }
      
    
    }
    protected void ddlpartytype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlpartytype.SelectedIndex > 0)
        {
                       SqlCommand cmd = new SqlCommand("Sp_Select_PartyNameByPartyType", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@PartyTypeId", Convert.ToInt32(rbClassType.SelectedValue));
            cmd.Parameters.AddWithValue("@PartyTypeId", ddlpartytype.SelectedValue);
            cmd.Parameters.AddWithValue("@compid", compid);
            cmd.Parameters.AddWithValue("@whid", ddlwarehouse.SelectedValue);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            ddlPartyName.DataSource = ds;
            ddlPartyName.DataTextField = "Compname";
            ddlPartyName.DataValueField = "PartyID";
            ddlPartyName.DataBind();
            ddlPartyName.Items.Insert(0, "--Select--");
        }
        
        ddlPartyName_SelectedIndexChanged(sender, e);
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
        txtMemo.Text = "";
       // txtAmount.Text = "";

    }
    protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillin();
        fillgr();
        invordcal();
    }
    protected void fillin()
    {
        if (txtAmount.Text.Length > 0)
        {
            if (ddlPartyName.SelectedIndex == 0)
            {
                pnlinvdata.Visible = false;
            }
            else
            {
                pnlinvdata.Visible = true;
            }
        }
        else
        {
            pnlinvdata.Visible = false;
        }
    }
    protected void fillgr()
    {
        dtnew.Clone();
        dtnew.Clear();
        dtnew.Columns.Clear();
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

        DataColumn dtcom7 = new DataColumn();
        dtcom7.DataType = System.Type.GetType("System.String");
        dtcom7.ColumnName = "SalesOrderNo";
        dtcom7.ReadOnly = false;
        dtcom7.Unique = false;
        dtcom7.AllowDBNull = true;

        dtnew.Columns.Add(dtcom7);

        DataColumn dtcom8 = new DataColumn();
        dtcom8.DataType = System.Type.GetType("System.String");
        dtcom8.ColumnName = "SalesOrderId";
        dtcom8.ReadOnly = false;
        dtcom8.Unique = false;
        dtcom8.AllowDBNull = true;

        dtnew.Columns.Add(dtcom8);

        DataColumn dtcom9 = new DataColumn();
        dtcom9.DataType = System.Type.GetType("System.String");
        dtcom9.ColumnName = "TransactionId";
        dtcom9.ReadOnly = false;
        dtcom9.Unique = false;
        dtcom9.AllowDBNull = true;

        dtnew.Columns.Add(dtcom9);

        //chkappamount.Checked = false;
        if (ddlPartyName.SelectedIndex > 0)
        {
            lblmsg.Text = "";
            //chkbkinvoice.Checked = false;
            //GridView2.Visible = false;
            GridView2.DataSource = null;
            GridView2.DataBind();
            if (chkbkinvoice.Checked == true)
            {
                //txtAmount.Text = "";
                // controlclear();
                //SqlCommand cmds = new SqlCommand("select SalesOrderId From SalesOrderMaster where PartyId='" + ddlPartyName.SelectedValue + "'", con);
                SqlCommand cmds = new SqlCommand("select Account From Party_master inner join PartytTypeMaster on Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId where PartyID='" + ddlPartyName.SelectedValue + "' and PartytTypeMaster.compid = '" + compid + "' ", con);

                SqlDataAdapter dtps = new SqlDataAdapter(cmds);
                DataSet dss = new DataSet();
                dtps.Fill(dss);
                //" + ddlPartyName.SelectedValue + "
                if (dss.Tables[0].Rows.Count > 0)
                {
                    SqlCommand cmdt = new SqlCommand("Select distinct AccountDebit,AmountDebit,TranctionMaster.Tranction_Master_Id,DateTimeOfTransaction from TranctionMaster inner join  Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id where  AccountDebit='" + dss.Tables[0].Rows[0]["Account"].ToString() + "' and Tranction_Details.Whid='" + ddlwarehouse.SelectedValue + "' and TranctionMaster.Whid='" + ddlwarehouse.SelectedValue + "'", con);

                    SqlDataAdapter dtpt = new SqlDataAdapter(cmdt);
                    DataSet dst = new DataSet();
                    dtpt.Fill(dst);
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drt in dst.Tables[0].Rows)
                        {
                            //SqlCommand cmdtd = new SqlCommand("select AmountDebit,AmountCredit from Tranction_Details where Tranction_Master_Id ='" + drt["Tranction_Master_Id"].ToString() + "'", con);
                            SqlCommand cmdtd = new SqlCommand("SELECT distinct  TransactionMasterMoreInfo.SalesOrderId,  TranctionMaster.EntryTypeId, TranctionMaster.Tranction_Master_Id, TranctionMaster.Date, TranctionMaster.EntryNumber, TranctionMaster.UserId, " +
                      " TranctionMaster.Tranction_Amount,EntryTypeMaster.Entry_Type_Id,EntryTypeMaster.Entry_Type_Name " +
    " FROM  TransactionMasterMoreInfo  inner join TranctionMaster on TransactionMasterMoreInfo.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id inner join " +
                      " EntryTypeMaster ON TranctionMaster.EntryTypeId = EntryTypeMaster.Entry_Type_Id where TranctionMaster.Tranction_Master_Id='" + drt["Tranction_Master_Id"].ToString() + "' and EntryTypeId in ('5','6','21','30','26') and TranctionMaster.Whid = '" + ddlwarehouse.SelectedValue + "'", con);
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
                                SqlCommand cmdsupp = new SqlCommand("Select AmountDue from TranctionMasterSuppliment where Tranction_Master_Id='" + dstd.Tables[0].Rows[0]["Tranction_Master_Id"].ToString() + "'", con);
                                SqlDataAdapter dtpsupp = new SqlDataAdapter(cmdsupp);
                                DataTable dtsupp = new DataTable();
                                dtpsupp.Fill(dtsupp);
                                if (dtsupp.Rows.Count > 0)
                                {
                                    if (Convert.ToDouble(dtsupp.Rows[0]["AmountDue"].ToString()) > 0)
                                    {


                                        DataRow drnew = dtnew.NewRow();

                                        drnew["Date"] =Convert.ToDateTime(drt["DateTimeOfTransaction"]).ToShortDateString();
                                        //drnew["EntryType"] = drt["Entry_Type_Name"].ToString();
                                        drnew["EntryType"] = dstd.Tables[0].Rows[0]["Entry_Type_Name"].ToString();
                                        //drnew["EntryNo"] = drt["EntryNumber"].ToString();
                                        drnew["EntryNo"] = dstd.Tables[0].Rows[0]["EntryNumber"].ToString();
                                        drnew["TranAmount"] = String.Format("{0:n}", Convert.ToDecimal(drt["AmountDebit"]));

                                        drnew["DueBalance"] = String.Format("{0:n}", Convert.ToDecimal(dtsupp.Rows[0]["AmountDue"]));
                                    
                                        //drnew["EntryTypeId"] = drt["Entry_Type_Id"].ToString();
                                        drnew["EntryTypeId"] = dstd.Tables[0].Rows[0]["Entry_Type_Id"].ToString();
                                        drnew["SalesOrderNo"] = "";
                                        drnew["SalesOrderId"] = dstd.Tables[0].Rows[0]["SalesOrderId"].ToString();
                                        drnew["TransactionId"] = dstd.Tables[0].Rows[0]["Tranction_Master_Id"].ToString();
                                        dtnew.Rows.Add(drnew);
                                    }
                                }

                            }

                        }
                    }



                }
            }
            if (chkorder.Checked == true)
            {
                SqlCommand cmdsal = new SqlCommand("Select SalesOrderId,SalesOrderNo,GrossAmount,PartyId,SalesOrderDate from SalesOrderMaster where PartyId='" + ddlPartyName.SelectedValue + "'", con);
                SqlDataAdapter dtpsal = new SqlDataAdapter(cmdsal);
                DataTable dtsal = new DataTable();
                dtpsal.Fill(dtsal);

                foreach (DataRow dtrow in dtsal.Rows)
                {
                    SqlCommand cmdsalmor = new SqlCommand("Select AmountDue from SalesOrderSuppliment where SalesOrderMasterId='" + dtrow["SalesOrderId"].ToString() + "'", con);
                    SqlDataAdapter dtpsalmor = new SqlDataAdapter(cmdsalmor);
                    DataTable dtsalmor = new DataTable();
                    dtpsalmor.Fill(dtsalmor);


                    if (dtsalmor.Rows.Count > 0)
                    {
                        if (Convert.ToDouble(dtsalmor.Rows[0]["AmountDue"].ToString()) > 0)
                        {
                            DataRow drnew = dtnew.NewRow();

                            drnew["Date"] =Convert.ToDateTime(dtrow["SalesOrderDate"]).ToShortDateString();

                            drnew["EntryType"] = "";

                            drnew["EntryNo"] = "";
                            //drnew["TranAmount"] = drtm["GrossAmount"].ToString();
                            drnew["TranAmount"] = String.Format("{0:n}", Convert.ToDecimal(dtrow["GrossAmount"]));
                          
                            if (dtsalmor.Rows.Count > 0)
                            {
                                drnew["DueBalance"] = String.Format("{0:n}", Convert.ToDecimal(dtsalmor.Rows[0]["AmountDue"])); 
                            }
                            else
                            {
                                drnew["DueBalance"] = "";
                            }

                            //drnew["EntryTypeId"] = drt["Entry_Type_Id"].ToString();
                            drnew["EntryTypeId"] = "";
                            //drnew["SalesOrderNo"] = dtsal.Rows[0]["SalesOrderNo"].ToString();
                            drnew["SalesOrderNo"] = dtrow["SalesOrderNo"].ToString();
                            //drnew["SalesOrderId"] = dtsal.Rows[0]["SalesOrderId"].ToString();
                            drnew["SalesOrderId"] = dtrow["SalesOrderId"].ToString();
                            //drnew["TransactionId"] = dstd.Tables[0].Rows[0]["Tranction_Master_Id"].ToString();
                            drnew["TransactionId"] = "";
                            dtnew.Rows.Add(drnew);
                        }

                    }
                }
            }
            if (dtnew.Rows.Count > 0)
            {
                GridView2.DataSource = dtnew;
                GridView2.DataBind();


            }
        }
    }
    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        if (txtAmount.Text.Length > 0)
        {
            panelafamt.Visible = true;
        }
        else
        {
            panelafamt.Visible = false;
        }
        ddlpartytype.SelectedIndex = ddlpartytype.Items.IndexOf(ddlpartytype.Items.FindByText("Customer"));
        ddlpartytype_SelectedIndexChanged(sender, e);
        ddlPartyName_SelectedIndexChanged(sender, e);
       
    }
    protected void chkbkinvoice_CheckedChanged(object sender, EventArgs e)
    {


           fillgr();
            invordcal();
          
            if (chkbkinvoice.Checked == true && chkorder.Checked == true)
            {
                GridView2.Columns[3].Visible = true;
                GridView2.Columns[4].Visible = true;
                GridView2.Columns[5].Visible = true;
                Panel2.Visible = true;
                GridView2.Visible = true;
            }
            else if (chkbkinvoice.Checked == true )
            {
                GridView2.Columns[3].Visible = true;
                GridView2.Columns[4].Visible = true;
                GridView2.Columns[5].Visible = false;
                Panel2.Visible = true;
                GridView2.Visible = true;
            }
            else if (chkorder.Checked == true)
            {
                GridView2.Columns[3].Visible = false;
                GridView2.Columns[4].Visible = false;
                GridView2.Columns[5].Visible = true;
                Panel2.Visible = true;
                GridView2.Visible = true;
            }
            else
            {
                Panel2.Visible = false;
                //GridView2.Visible = false;
            }
      
    }
    protected void invordcal()
    {
        if (txtAmount.Text != "")
        {

            if (GridView2.Rows.Count > 0)
            {
                double amt = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount.Text), 2, MidpointRounding.AwayFromZero).ToString());
                foreach (GridViewRow gdr in GridView2.Rows)
                {
                    Label lbl = (Label)(gdr.FindControl("lblbaldue"));

                    TextBox txt = (TextBox)(gdr.FindControl("txtnewamt"));
                    CheckBox chkck = (CheckBox)(gdr.FindControl("chkbox"));
                    double amt1 = Convert.ToDouble(lbl.Text);
                    Label lblsalesorderNo = (Label)(gdr.FindControl("lblsalesorderNo"));
                    Label lblentryNo = (Label)(gdr.FindControl("lblentryNo"));

                    if (chkorder.Checked == true && chkbkinvoice.Checked == true)
                    {
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

                        amt = amt - amt1;
                    }
                    else if (chkbkinvoice.Checked == true)
                    {
                        if (Convert.ToString(lblentryNo.Text) != "")
                        {
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

                            amt = amt - amt1;
                        }
                    }
                    else if (chkorder.Checked == true)
                    {
                        if (Convert.ToString(lblsalesorderNo.Text) != "")
                        {
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

                            amt = amt - amt1;
                        }
                    }
                }
            }
        }
    }
    protected void chkappamount_CheckedChanged(object sender, EventArgs e)
    {
        if (chkappamount.Checked == true)
        {
            //chkbkinvoice.Checked = true;
            //chkorder.Checked = true;
            chkbkinvoice.Visible = true;
            chkorder.Visible = true;
        }
        else
        {
            chkbkinvoice.Checked = false;
            chkorder.Checked = false;
            chkbkinvoice.Visible = false;
            chkorder.Visible = false;
            Panel2.Visible = false;
            GridView2.DataSource = null;
            GridView2.DataBind();
        }
    }

     
   
    public DataSet fillddl(String qry)
    {
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;

    }

    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;

    }
   
      protected void Delete(string query)
    {
        SqlCommand cmd = new SqlCommand(query, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
    }

  
   
 
 
    public void inserdocatt()
    {
        
        string sqlselect="select * from DocumentMaster where DocumentId='"+Request.QueryString["docid"]+"'";
        SqlDataAdapter adpt = new SqlDataAdapter(sqlselect,con);
        DataTable dtpt=new DataTable();
        adpt.Fill(dtpt);
        if (dtpt.Rows.Count > 0)
        {

            SqlCommand cmdi = new SqlCommand("InsertAttachmentMaster", con);

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
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            Int32 result = cmdi.ExecuteNonQuery();
            result = Convert.ToInt32(cmdi.Parameters["@Attachment"].Value);
            con.Close();
           
          }
    }
 
   
   
    protected void fillauto()
    {
        string sssx = "Select top(1)  TranctionMaster.Tranction_Master_Id,TranctionMaster.Whid,Tranction_Details.AccountDebit,TranctionMaster.Date,TranctionMasterSuppliment.Party_MasterId from TranctionMaster inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id inner join TranctionMasterSuppliment on TranctionMasterSuppliment.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where TranctionMaster.compid='" + Session["comid"] + "'  and TranctionMaster.EntryTypeId='2' and Tranction_Details.compid='" + Session["comid"] + "' and (Tranction_Details.AccountDebit IS NOT NULL or Tranction_Details.AccountDebit<>'0') order by TranctionMaster.Tranction_Master_Id Desc";
        SqlDataAdapter adpt = new SqlDataAdapter(sssx, con);
        DataTable dtpt = new DataTable();
        adpt.Fill(dtpt);
        EventArgs e = new EventArgs();
        object sender = new object();
        if (dtpt.Rows.Count > 0)
        {
            ddlwarehouse.SelectedIndex = ddlwarehouse.Items.IndexOf(ddlwarehouse.Items.FindByValue(dtpt.Rows[0]["Whid"].ToString()));
            txtTodate.Text = Convert.ToDateTime(dtpt.Rows[0]["Date"]).ToShortDateString();
           
            ddlwarehouse_SelectedIndexChanged(sender, e);
            ddlAccount.SelectedIndex = ddlAccount.Items.IndexOf(ddlAccount.Items.FindByValue(dtpt.Rows[0]["AccountDebit"].ToString()));

            if (Convert.ToString(dtpt.Rows[0]["Party_MasterId"]) != "0")
            {
                string sssx1 = "Select Party_master.PartyTypeId from Party_master Where PartyId='" + dtpt.Rows[0]["Party_MasterId"].ToString() + "'";
                SqlDataAdapter adpt1 = new SqlDataAdapter(sssx1, con);
                DataTable dtpt1 = new DataTable();
                adpt1.Fill(dtpt1);
                if (dtpt1.Rows.Count > 0)
                {
                    // rbClassType.SelectedIndex = rbClassType.Items.IndexOf(rbClassType.Items.FindByValue("2"));
                    // rbClassType_SelectedIndexChanged(sender, e);
                    // ddlpartytype.SelectedIndex = ddlpartytype.Items.IndexOf(ddlpartytype.Items.FindByValue(dtpt1.Rows[0]["PartyTypeId"].ToString()));
                    ddlpartytype.SelectedIndex = ddlpartytype.Items.IndexOf(ddlpartytype.Items.FindByText("Customer"));
                    ddlpartytype_SelectedIndexChanged(sender, e);
                    ddlPartyName.SelectedIndex = ddlPartyName.Items.IndexOf(ddlPartyName.Items.FindByValue(dtpt.Rows[0]["Party_MasterId"].ToString()));

                    ddlPartyName_SelectedIndexChanged(sender, e);
                }
            }
            else
            {
                //  rbClassType.SelectedIndex = rbClassType.Items.IndexOf(rbClassType.Items.FindByValue("1"));
                //  rbClassType_SelectedIndexChanged(sender, e);
            }

        }
        else
        {
           
                  ddlwarehouse_SelectedIndexChanged(sender, e);
                  ddlpartytype.SelectedIndex = ddlpartytype.Items.IndexOf(ddlpartytype.Items.FindByText("Customer"));
                  rbClassType_SelectedIndexChanged(sender, e);
                  ddlpartytype_SelectedIndexChanged(sender, e);
                  ddlPartyName_SelectedIndexChanged(sender, e);
        }

    }

    protected void fillUpdate()
    {
        DataTable dt = new DataTable();
        dt = (DataTable)select("Select distinct Tranction_Details.Memo as dm, EntryTypeMaster.Entry_Type_Name, TranctionMaster.Tranction_Amount, TranctionMaster.Whid,TranctionMasterSuppliment.Memo,TranctionMaster.EntryNumber, TranctionMaster.EntryTypeId,TranctionMaster.Tranction_Master_Id,TranctionMaster.Whid,Tranction_Details.AccountDebit,Tranction_Details.AccountCredit,Tranction_Details.AmountCredit,Tranction_Details.AmountDebit,Tranction_Details.Tranction_Details_Id,TranctionMasterSuppliment.Tranction_Master_SupplimentId,TranctionMaster.Date,TranctionMasterSuppliment.Party_MasterId from EntryTypeMaster inner join TranctionMaster on TranctionMaster.EntryTypeId=EntryTypeMaster.Entry_Type_Id inner join Tranction_Details on Tranction_Details.Tranction_Master_Id=TranctionMaster.Tranction_Master_Id inner join TranctionMasterSuppliment on TranctionMasterSuppliment.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where TranctionMaster.compid='" + Session["Comid"] + "' and TranctionMaster.Tranction_Master_Id='" + ViewState["Trid"] + "' and TranctionMaster.EntryTypeId='2'");
        if (dt.Rows.Count > 0)
        {
            btnSubmit.Visible = false;
            Button1.Visible = false;
            btnupdate.Visible = true;
         
            pnlinvdata.Visible = true;
            ddlwarehouse.SelectedIndex = ddlwarehouse.Items.IndexOf(ddlwarehouse.Items.FindByValue(dt.Rows[0]["Whid"].ToString()));
            txtTodate.Text = Convert.ToDateTime(dt.Rows[0]["Date"]).ToShortDateString();
            EventArgs e = new EventArgs();
            object sender = new object();
           
                ddlAccount.DataSource = (DataSet)fillddl2();
                ddlAccount.DataValueField = "AccountId";
                ddlAccount.DataTextField = "AccountName";
                ddlAccount.DataBind();
                //ddlAccount.DataBind();
                ddlAccount.Items.Insert(0, "--Select--");
            
                if (Convert.ToString(dt.Rows[0]["Party_MasterId"]) == "0" || Convert.ToString(dt.Rows[0]["Party_MasterId"]) == "")
                {
                    ddlAccount.SelectedIndex = ddlAccount.Items.IndexOf(ddlAccount.Items.FindByValue(dt.Rows[0]["AccountDebit"].ToString()));
               

                    rbClassType.SelectedIndex = rbClassType.Items.IndexOf(rbClassType.Items.FindByValue("1"));

                  
                    rbClassType_SelectedIndexChanged(sender, e);
                    ddlPartyName.Items.Clear();
                    ddlPartyName.Items.Insert(0, "--Select--");
                    if (dt.Rows.Count >= 2)
                    {
                        ddlAccountname1.SelectedIndex = ddlAccountname1.Items.IndexOf(ddlAccountname1.Items.FindByValue(dt.Rows[1]["AccountCredit"].ToString()));
                        txtAmount1.Text=Convert.ToString( dt.Rows[1]["AmountCredit"]);
                        txtmemo1.Text = Convert.ToString(dt.Rows[1]["dm"]);
                    }
                    if (dt.Rows.Count >= 3)
                    {
                        ddlAccountname2.SelectedIndex = ddlAccountname2.Items.IndexOf(ddlAccountname2.Items.FindByValue(dt.Rows[2]["AccountCredit"].ToString()));
                        txtAmount2.Text = Convert.ToString(dt.Rows[2]["AmountCredit"]);
                        txtmemo2.Text = Convert.ToString(dt.Rows[2]["dm"]);
                    }
                    if (dt.Rows.Count >= 4)
                    {
                        ddlAccountname3.SelectedIndex = ddlAccountname3.Items.IndexOf(ddlAccountname3.Items.FindByValue(dt.Rows[3]["AccountCredit"].ToString()));
                        txtAmount3.Text = Convert.ToString(dt.Rows[3]["AmountCredit"]);
                        txtmemo3.Text = Convert.ToString(dt.Rows[3]["dm"]);
                    }
                    if (dt.Rows.Count >= 5)
                    {
                        ddlAccountname4.SelectedIndex = ddlAccountname4.Items.IndexOf(ddlAccountname4.Items.FindByValue(dt.Rows[4]["AccountCredit"].ToString()));
                        txtAmount4.Text = Convert.ToString(dt.Rows[4]["AmountCredit"]);
                        txtmemo4.Text = Convert.ToString(dt.Rows[4]["dm"]);
                    }
                    if (dt.Rows.Count >= 6)
                    {
                        ddlAccountname5.SelectedIndex = ddlAccountname5.Items.IndexOf(ddlAccountname5.Items.FindByValue(dt.Rows[5]["AccountCredit"].ToString()));
                        txtAmount5.Text = Convert.ToString(dt.Rows[5]["AmountCredit"]);
                        txtmemo5.Text = Convert.ToString(dt.Rows[5]["dm"]);
                    }
                    if (dt.Rows.Count >= 7)
                    {
                        ddlAccountname6.SelectedIndex = ddlAccountname6.Items.IndexOf(ddlAccountname6.Items.FindByValue(dt.Rows[6]["AccountCredit"].ToString()));
                        txtAmount6.Text = Convert.ToString(dt.Rows[6]["AmountCredit"]);
                        txtmemo6.Text = Convert.ToString(dt.Rows[6]["dm"]);
                    }
                    if (dt.Rows.Count >= 8)
                    {
                        ddlAccountname7.SelectedIndex = ddlAccountname7.Items.IndexOf(ddlAccountname7.Items.FindByValue(dt.Rows[7]["AccountCredit"].ToString()));
                        txtAmount7.Text = Convert.ToString(dt.Rows[7]["AmountCredit"]);
                        txtmemo7.Text = Convert.ToString(dt.Rows[7]["dm"]);
                    }
                    if (dt.Rows.Count >= 9)
                    {
                        ddlAccountname8.SelectedIndex = ddlAccountname8.Items.IndexOf(ddlAccountname8.Items.FindByValue(dt.Rows[8]["AccountCredit"].ToString()));
                        txtAmount8.Text = Convert.ToString(dt.Rows[8]["AmountCredit"]);
                        txtmemo8.Text = Convert.ToString(dt.Rows[8]["dm"]);
                    }
                    if (dt.Rows.Count >= 10)
                    {
                        ddlAccountname9.SelectedIndex = ddlAccountname9.Items.IndexOf(ddlAccountname9.Items.FindByValue(dt.Rows[9]["AccountCredit"].ToString()));
                        txtAmount9.Text = Convert.ToString(dt.Rows[9]["AmountCredit"]);
                        txtmemo9.Text = Convert.ToString(dt.Rows[9]["dm"]);
                    }
                    lblgratot.Text = Convert.ToString(dt.Rows[0]["Tranction_Amount"]);
                    String.Format("{0:n}", Convert.ToDecimal(lblgratot.Text));
                }
                else
                {
                    ddlAccount.SelectedIndex = ddlAccount.Items.IndexOf(ddlAccount.Items.FindByValue(dt.Rows[0]["AccountDebit"].ToString()));
               
                    string sssx1 = "Select Party_master.PartyTypeId from Party_master Where PartyId='" + dt.Rows[0]["Party_MasterId"] + "'";
                    SqlDataAdapter adpt1 = new SqlDataAdapter(sssx1, con);
                    DataTable dtpt1 = new DataTable();
                    adpt1.Fill(dtpt1);
                    if (dtpt1.Rows.Count > 0)
                    {
                        
                        rbClassType.SelectedIndex = rbClassType.Items.IndexOf(rbClassType.Items.FindByValue("2"));
                        rbClassType_SelectedIndexChanged(sender, e);
                        txtAmount.Text = Convert.ToString(dt.Rows[0]["Tranction_Amount"]);
                        ddlpartytype.SelectedIndex = ddlpartytype.Items.IndexOf(ddlpartytype.Items.FindByValue(dtpt1.Rows[0]["PartyTypeId"].ToString()));
                        ddlpartytype_SelectedIndexChanged(sender, e);
                        ddlPartyName.SelectedIndex = ddlPartyName.Items.IndexOf(ddlPartyName.Items.FindByValue(dt.Rows[0]["Party_MasterId"].ToString()));
                        ddlPartyName_SelectedIndexChanged(sender, e);
                    }
                    
                }
                rbClassType.Enabled = false;
                txtenteryNumber.Text = Convert.ToString(dt.Rows[0]["EntryNumber"]);
                txtAmount.Text = Convert.ToString(dt.Rows[0]["Tranction_Amount"]);
              //  txtAmount_TextChanged(sender, e);
                panelafamt.Visible = true;
                
                txtMemo.Text = Convert.ToString(dt.Rows[0]["Memo"]);
               
                lblmsg.Text = "";

        }
    }
    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        SqlCommand cmd511 = new SqlCommand("Sp_Select_Cash Receipt", con);
        cmd511.CommandType = CommandType.StoredProcedure;
        // cmd.Parameters.AddWithValue("@GroupId", Convert.ToInt32(ddlGroupName1.SelectedValue));
        cmd511.Parameters.AddWithValue("@compid", Session["comid"]);
        cmd511.Parameters.AddWithValue("@whid", ddlwarehouse.SelectedValue);
        SqlDataAdapter adp511 = new SqlDataAdapter(cmd511);
        DataSet ds511 = new DataSet();
        adp511.Fill(ds511);
        if (ds511.Tables[0].Rows.Count > 0)
        {
            if (ds511.Tables[0].Rows[0]["EntryNumber"].ToString() != "")
            {
                ViewState["entryId"] = ds511.Tables[0].Rows[0][0].ToString();
                db = Convert.ToDouble(ViewState["entryId"]);
                db = db + 1;
                ViewState["eid"] = db;
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
        txtenteryNumber.Text = db.ToString();
        ddlAccount.DataSource = (DataSet)fillddl2();
        ddlAccount.DataValueField = "AccountId";
        ddlAccount.DataTextField = "AccountName";
        ddlAccount.DataBind();
        //ddlAccount.DataBind();
        ddlAccount.Items.Insert(0, "--Select--");
        rbClassType_SelectedIndexChanged(sender, e);
        ddlPartyName.Items.Clear();
        ddlPartyName.Items.Insert(0, "--Select--");
        chkappentry.Visible = false;
        DataTable appri = ClsAccountAppr.Apprreuqired();
           if(appri.Rows.Count>0)
           {
               ViewState["AccRS"]="ACC";
               int kn = ClsAccountAppr.Allowchkappr(ddlwarehouse.SelectedValue);
               if (kn == 1)
               {
                   chkappentry.Visible = true;
               }
           }
           else
           {
                ViewState["AccRS"]="";
           } 
           btnSubmit.Enabled = true;
           Button2.Enabled = true;
           DataTable dtpaacc = select("Select * from AccountPageRightAccess where cid='" + Session["Comid"] + "' and Access='1' and AccType='0'");
           if (dtpaacc.Rows.Count > 0)
           {
               btnSubmit.Enabled = false;
               Button2.Enabled = false;
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
                           Button2.Enabled = true;
                       }
                       else if (Convert.ToInt32(dtrc.Rows[0]["Insert_Right"]) == 1)
                       {
                           btnSubmit.Enabled = true;
                       }
                   }
                   else
                   {
                       btnSubmit.Enabled = true;
                       Button2.Enabled = true;
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





    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int accesin = 0;
        DataTable dtdr = select("Select  AccountAddDataRequireTbl.* from AccountAddDataRequireTbl  where  Compid='" +Session["Comid"] + "' and Access='1'");

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
       //int  accesin = ClsAccountAppr.AccessInsert(ddlwarehouse.SelectedValue);
   
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

                        string date = "select Convert(nvarchar,StartDate,101) as StartDate,EndDate from [ReportPeriod] where Whid = '" + ddlwarehouse.SelectedValue + "' and Active='1'";
                        SqlCommand cmd1111111 = new SqlCommand(date, con);
                        SqlDataAdapter adp1111 = new SqlDataAdapter(cmd1111111);
                        DataTable dt1111 = new DataTable();
                        adp1111.Fill(dt1111);
                        //txtdate.Text = Convert.ToString(System.DateTime.Now.Date.ToShortDateString());
                        if (dt1111.Rows.Count > 0)
                        {
                            if (Convert.ToDateTime(txtTodate.Text) < Convert.ToDateTime(dt1111.Rows[0][0].ToString()))
                            {
                                lblstartdate.Text = dt1111.Rows[0][0].ToString();
                                ModalPopupExtender1222.Show();

                            }

                            else
                            {
                                //SqlTransaction tr1 = null;
                                if (rbClassType.SelectedValue == "2")
                                {
                                    SqlCommand cmd511 = new SqlCommand("Sp_Select_Cash Receipt", con);
                                    cmd511.CommandType = CommandType.StoredProcedure;
                                    // cmd.Parameters.AddWithValue("@GroupId", Convert.ToInt32(ddlGroupName1.SelectedValue));
                                    cmd511.Parameters.AddWithValue("@compid", Session["comid"]);
                                    cmd511.Parameters.AddWithValue("@whid", ddlwarehouse.SelectedValue);
                                    SqlDataAdapter adp511 = new SqlDataAdapter(cmd511);
                                    DataSet ds511 = new DataSet();
                                    adp511.Fill(ds511);
                                    if (ds511.Tables[0].Rows.Count > 0)
                                    {
                                        if (ds511.Tables[0].Rows[0]["EntryNumber"].ToString() != "")
                                        {
                                            ViewState["entryId"] = ds511.Tables[0].Rows[0][0].ToString();
                                            db = Convert.ToDouble(ViewState["entryId"]);
                                            db = db + 1;
                                            ViewState["eid"] = db;
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


                                    if (chkbkinvoice.Checked == true || chkorder.Checked == true)
                                    {


                                        SqlCommand cmdin = new SqlCommand("Insert into TranctionMaster values('" + txtTodate.Text + "','" + db + "','2','" + Convert.ToInt32(Session["userid"]) + "','" + Math.Round(Convert.ToDecimal(txtAmount.Text), 2, MidpointRounding.AwayFromZero) + "','" + Session["comid"] + "','" + ddlwarehouse.SelectedValue + "')", con);
                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdin.ExecuteNonQuery();
                                        con.Close();

                                        SqlCommand cmdint = new SqlCommand("Select Max(Tranction_Master_Id) as TMID from TranctionMaster inner join EntryTypeMaster on TranctionMaster.EntryTypeId = EntryTypeMaster.Entry_Type_Id where TranctionMaster.compid = '" + compid + "' and TranctionMaster.Whid='" + ddlwarehouse.SelectedValue + "'", con);
                                        SqlDataAdapter dtpint = new SqlDataAdapter(cmdint);
                                        DataTable dtint = new DataTable();
                                        dtpint.Fill(dtint);



                                        if (dtint.Rows.Count > 0)
                                        {
                                            ViewState["tid"] = dtint.Rows[0]["TMID"].ToString();
                                            SqlCommand cmds = new SqlCommand("select Account From Party_master inner join PartytTypeMaster  on Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId where PartyID='" + ddlPartyName.SelectedValue + "' and PartytTypeMaster.compid = '" + compid + "' and Party_master.Whid='" + ddlwarehouse.SelectedValue + "'", con);

                                            SqlDataAdapter dtps = new SqlDataAdapter(cmds);
                                            DataSet dss = new DataSet();
                                            dtps.Fill(dss);
                                            if (dss.Tables[0].Rows.Count > 0)
                                            {
                                                SqlCommand cmdtd = new SqlCommand("Insert into Tranction_Details values('" + ddlAccount.SelectedValue + "','0','" + Math.Round(Convert.ToDecimal(txtAmount.Text), 2, MidpointRounding.AwayFromZero).ToString() + "','0','" + dtint.Rows[0]["TMID"].ToString() + "','" + txtMemo.Text + "','" + txtTodate.Text + "','0','0','" + compid + "','" + ddlwarehouse.SelectedValue + "')", con);
                                                if (con.State.ToString() != "Open")
                                                {
                                                    con.Open();
                                                }
                                                cmdtd.ExecuteNonQuery();
                                                con.Close();

                                                SqlCommand cmdtdc = new SqlCommand("Insert into Tranction_Details values('0','" + dss.Tables[0].Rows[0]["Account"].ToString() + "','0','" + Math.Round(Convert.ToDecimal(txtAmount.Text), 2, MidpointRounding.AwayFromZero).ToString() + "','" + dtint.Rows[0]["TMID"].ToString() + "','" + txtMemo.Text + "','" + txtTodate.Text + "','0','0','" + compid + "','" + ddlwarehouse.SelectedValue + "')", con);
                                                if (con.State.ToString() != "Open")
                                                {
                                                    con.Open();
                                                }
                                                cmdtdc.ExecuteNonQuery();
                                                con.Close();

                                                SqlCommand cmdsupp = new SqlCommand("insert into TranctionMasterSuppliment(Tranction_Master_Id,Memo,Party_MasterId) values ('" + dtint.Rows[0]["TMID"].ToString() + "','" + txtMemo.Text + "','" + ddlPartyName.SelectedValue + "')", con);
                                                if (con.State.ToString() != "Open")
                                                {
                                                    con.Open();
                                                }
                                                cmdsupp.ExecuteNonQuery();
                                                con.Close();
                                                if (GridView2.Rows.Count > 0)
                                                {
                                                    int k = 0;
                                                    double counter = 0;
                                                    foreach (GridViewRow grdv in GridView2.Rows)
                                                    {
                                                        //Label enttypeid = (Label)(grdv.FindControl("lblentrytypeid"));
                                                        Label entno = (Label)(grdv.FindControl("lblentryNo"));//EntryNo
                                                        TextBox txtamt = (TextBox)(grdv.FindControl("txtnewamt"));
                                                        Label trnid = (Label)(grdv.FindControl("lblTrnId"));
                                                        Label lblsalNo = (Label)(grdv.FindControl("lblsalesorderNo"));
                                                        Label lblsalId = (Label)(grdv.FindControl("lblsalesorderId"));
                                                        CheckBox chk = (CheckBox)(grdv.FindControl("chkbox"));
                                                        Label lblamtdue = (Label)(grdv.FindControl("lblbaldue"));


                                                        if (chk.Checked == true)
                                                        {
                                                            if (txtamt.Text.ToString() != "")
                                                            {
                                                                if (Convert.ToDouble(txtamt.Text) > Convert.ToDouble(lblamtdue.Text))
                                                                {
                                                                    txtamt.Text = lblamtdue.Text;
                                                                }
                                                            }
                                                            counter += Convert.ToDouble(txtamt.Text);
                                                            if (counter > Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount.Text), 2, MidpointRounding.AwayFromZero).ToString()))
                                                            {
                                                                if (k == 0)
                                                                {
                                                                    txtamt.Text = (counter - Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount.Text), 2, MidpointRounding.AwayFromZero).ToString())).ToString();
                                                                    counter = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount.Text), 2, MidpointRounding.AwayFromZero).ToString());
                                                                    k += 1;
                                                                }
                                                            }
                                                            if (counter <= Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount.Text), 2, MidpointRounding.AwayFromZero).ToString()))
                                                            {
                                                                if (lblsalId.Text != "" && trnid.Text == "")
                                                                {
                                                                    SqlCommand cmdsalamt = new SqlCommand("Select AmountDue from SalesOrderSuppliment where SalesOrderMasterId='" + lblsalId.Text + "' and AmountDue > 0", con);
                                                                    SqlDataAdapter dtpsalamt = new SqlDataAdapter(cmdsalamt);
                                                                    DataSet dssalamt = new DataSet();

                                                                    dtpsalamt.Fill(dssalamt);

                                                                    if (dssalamt.Tables[0].Rows.Count > 0)
                                                                    {
                                                                        if (txtamt.Text != "")
                                                                        {
                                                                            if (Convert.ToDouble(dssalamt.Tables[0].Rows[0]["AmountDue"].ToString()) > 0)
                                                                            {
                                                                                double amt;
                                                                                if (Convert.ToDouble(txtamt.Text) >= Convert.ToDouble(dssalamt.Tables[0].Rows[0]["AmountDue"].ToString()))
                                                                                {
                                                                                    amt = 0;
                                                                                }
                                                                                else
                                                                                {
                                                                                    amt = Convert.ToDouble(dssalamt.Tables[0].Rows[0]["AmountDue"].ToString()) - (Convert.ToDouble(txtamt.Text));
                                                                                }

                                                                                SqlCommand cmdamtups = new SqlCommand("Update SalesOrderSuppliment set AmountDue = '" + amt + "' where SalesOrderMasterId ='" + lblsalId.Text + "'", con);
                                                                                if (con.State.ToString() != "Open")
                                                                                {
                                                                                    con.Open();
                                                                                }
                                                                                cmdamtups.ExecuteNonQuery();
                                                                                con.Close();

                                                                                if (Convert.ToDouble(txtamt.Text) == Convert.ToDouble(lblamtdue.Text))
                                                                                {
                                                                                    //s = "update StatusControl set StatusMasterId='35' where SalesOrderId='" + lblsalId.Text + "'";
                                                                                    s = "insert into StatusControl(TranctionMasterId,StatusMasterId,Datetime,UserMasterId,SalesOrderId) values('" + trnid.Text + "','30','" + System.DateTime.Now.ToShortDateString() + "','" + Convert.ToInt32(Session["userid"]) + "','" + lblsalId.Text + "')";
                                                                                }
                                                                                else if (Convert.ToDouble(txtamt.Text) < Convert.ToDouble(lblamtdue.Text))
                                                                                {
                                                                                    //s = "update StatusControl set StatusMasterId='34' where SalesOrderId='" + lblsalId.Text + "'";
                                                                                    s = "insert into StatusControl(TranctionMasterId,StatusMasterId,Datetime,UserMasterId,SalesOrderId) values('" + trnid.Text + "','29','" + System.DateTime.Now.ToShortDateString() + "','" + Convert.ToInt32(Session["userid"]) + "','" + lblsalId.Text + "')";
                                                                                }
                                                                                else
                                                                                {
                                                                                    //s = "update StatusControl set StatusMasterId='28' where SalesOrderId='" + lblsalId.Text + "'";
                                                                                }
                                                                                SqlCommand cmdupstatus = new SqlCommand(s, con);
                                                                                if (con.State.ToString() != "Open")
                                                                                {
                                                                                    con.Open();
                                                                                }
                                                                                cmdupstatus.ExecuteNonQuery();
                                                                                con.Close();
                                                                            }
                                                                        }
                                                                    }
                                                                    if (txtamt.Text != "")
                                                                    {

                                                                        SqlCommand cmdinspay = new SqlCommand("Insert into PaymentApplicationTbl values('" + dtint.Rows[0]["TMID"].ToString() + "','" + trnid.Text + "','" + txtamt.Text + "','" + txtTodate.Text + "','" + Convert.ToInt32(Session["userid"]) + "','" + lblsalId.Text + "')", con);
                                                                        if (con.State.ToString() != "Open")
                                                                        {
                                                                            con.Open();
                                                                        }
                                                                        cmdinspay.ExecuteNonQuery();
                                                                        con.Close();
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    SqlCommand cmdsalamt = new SqlCommand("Select AmountDue from TranctionMasterSuppliment where Tranction_Master_Id ='" + trnid.Text + "' and AmountDue > 0", con);
                                                                    SqlDataAdapter dtpsalamt = new SqlDataAdapter(cmdsalamt);
                                                                    DataSet dssalamt = new DataSet();

                                                                    dtpsalamt.Fill(dssalamt);

                                                                    if (dssalamt.Tables[0].Rows.Count > 0)
                                                                    {
                                                                        if (Convert.ToDouble(dssalamt.Tables[0].Rows[0]["AmountDue"].ToString()) > 0)
                                                                        {
                                                                            double amt;
                                                                            if (Convert.ToDouble(txtamt.Text) >= Convert.ToDouble(dssalamt.Tables[0].Rows[0]["AmountDue"].ToString()))
                                                                            {
                                                                                amt = 0;
                                                                            }
                                                                            else
                                                                            {
                                                                                amt = Convert.ToDouble(dssalamt.Tables[0].Rows[0]["AmountDue"].ToString()) - (Convert.ToDouble(txtamt.Text));
                                                                            }

                                                                            SqlCommand cmdamtups = new SqlCommand("Update TranctionMasterSuppliment set AmountDue = '" + amt + "' where Tranction_Master_Id ='" + trnid.Text + "' and AmountDue > 0", con);
                                                                            if (con.State.ToString() != "Open")
                                                                            {
                                                                                con.Open();
                                                                            }
                                                                            cmdamtups.ExecuteNonQuery();
                                                                            con.Close();




                                                                        }
                                                                    }
                                                                    if (txtamt.Text != "")
                                                                    {
                                                                        SqlCommand cmdinspay = new SqlCommand("Insert into PaymentApplicationTbl values('" + dtint.Rows[0]["TMID"].ToString() + "','" + trnid.Text + "','" + txtamt.Text + "','" + txtTodate.Text + "','" + Convert.ToInt32(Session["userid"]) + "','0')", con);
                                                                        if (con.State.ToString() != "Open")
                                                                        {
                                                                            con.Open();
                                                                        }
                                                                        cmdinspay.ExecuteNonQuery();
                                                                        con.Close();
                                                                        if (Convert.ToDouble(txtamt.Text) < Convert.ToDouble(lblamtdue.Text))
                                                                        {

                                                                            s = "insert into StatusControl(TranctionMasterId,StatusMasterId,Datetime,UserMasterId,SalesOrderId) values('" + trnid.Text + "','29','" + System.DateTime.Now.ToShortDateString() + "','" + Convert.ToInt32(Session["userid"]) + "','" + lblsalId.Text + "')";
                                                                        }
                                                                        else if (Convert.ToDouble(txtamt.Text) == Convert.ToDouble(lblamtdue.Text))
                                                                        {

                                                                            s = "insert into StatusControl(TranctionMasterId,StatusMasterId,Datetime,UserMasterId,SalesOrderId) values('" + trnid.Text + "','30','" + System.DateTime.Now.ToShortDateString() + "','" + Convert.ToInt32(Session["userid"]) + "','" + lblsalId.Text + "')";
                                                                        }

                                                                        SqlCommand cmdupstatus = new SqlCommand(s, con);
                                                                        if (con.State.ToString() != "Open")
                                                                        {
                                                                            con.Open();
                                                                        }
                                                                        cmdupstatus.ExecuteNonQuery();
                                                                        con.Close();
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
                                                    txtMemo.Text = "";
                                                    txtAmount.Text = "";

                                                    GridView2.DataSource = null;
                                                    GridView2.DataBind();
                                                    //chkappamount.Checked = false;
                                                    //chkappamount_CheckedChanged(sender, e);

                                                    txtAmount_TextChanged(sender, e);
                                                    entryno();

                                                }

                                            }
                                        }


                                    }
                                    else
                                    {
                                        SqlCommand cmdin = new SqlCommand("Insert into TranctionMaster values('" + Convert.ToDateTime(txtTodate.Text).ToShortDateString() + "','" + db + "','2','" + Session["userid"].ToString() + "','" + Math.Round(Convert.ToDecimal(txtAmount.Text), 2, MidpointRounding.AwayFromZero).ToString() + "','" + Session["comid"] + "','" + ddlwarehouse.SelectedValue + "')", con);

                                        if (con.State.ToString() != "Open")
                                        {
                                            con.Open();
                                        }
                                        cmdin.ExecuteNonQuery();
                                        con.Close();

                                        SqlCommand cmdint = new SqlCommand("Select Max(Tranction_Master_Id) as TMID from TranctionMaster left outer join EntryTypeMaster on TranctionMaster.EntryTypeId = EntryTypeMaster.Entry_Type_Id where  TranctionMaster.Whid = '" + ddlwarehouse.SelectedValue + "'  ", con);
                                        SqlDataAdapter dtpint = new SqlDataAdapter(cmdint);
                                        DataTable dtint = new DataTable();
                                        dtpint.Fill(dtint);

                                        if (dtint.Rows.Count > 0)
                                        {
                                            ViewState["tid"] = dtint.Rows[0]["TMID"].ToString();
                                            SqlCommand cmds = new SqlCommand("select Account From Party_master left outer join PartytTypeMaster  on Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId where PartyID='" + ddlPartyName.SelectedValue + "' and PartytTypeMaster.compid = '" + compid + "' and Party_master.Whid='" + ddlwarehouse.SelectedValue + "'", con);

                                            SqlDataAdapter dtps = new SqlDataAdapter(cmds);
                                            DataSet dss = new DataSet();
                                            dtps.Fill(dss);
                                            if (dss.Tables[0].Rows.Count > 0)
                                            {
                                                SqlCommand cmdtd = new SqlCommand("Insert into Tranction_Details values('" + ddlAccount.SelectedValue + "','0','" + Math.Round(Convert.ToDecimal(txtAmount.Text), 2, MidpointRounding.AwayFromZero).ToString() + "','0','" + dtint.Rows[0]["TMID"].ToString() + "','" + txtMemo.Text + "','" + txtTodate.Text + "','0','0','" + compid + "','" + ddlwarehouse.SelectedValue + "')", con);
                                                if (con.State.ToString() != "Open")
                                                {
                                                    con.Open();
                                                }
                                                cmdtd.ExecuteNonQuery();
                                                con.Close();

                                                SqlCommand cmdtdc = new SqlCommand("Insert into Tranction_Details values('0','" + dss.Tables[0].Rows[0]["Account"].ToString() + "','0','" + Math.Round(Convert.ToDecimal(txtAmount.Text), 2, MidpointRounding.AwayFromZero).ToString() + "','" + dtint.Rows[0]["TMID"].ToString() + "','" + txtMemo.Text + "','" + txtTodate.Text + "','0','0','" + compid + "','" + ddlwarehouse.SelectedValue + "')", con);
                                                if (con.State.ToString() != "Open")
                                                {
                                                    con.Open();
                                                }
                                                cmdtdc.ExecuteNonQuery();
                                                con.Close();

                                                SqlCommand cmdsupp = new SqlCommand("insert into TranctionMasterSuppliment(Tranction_Master_Id,Memo,Party_MasterId) values ('" + dtint.Rows[0]["TMID"].ToString() + "','" + txtMemo.Text + "','" + ddlPartyName.SelectedValue + "')", con);
                                                if (con.State.ToString() != "Open")
                                                {
                                                    con.Open();
                                                }
                                                cmdsupp.ExecuteNonQuery();
                                                con.Close();


                                                ////.................Update of account Master Balance End......................//////////////////
                                                SqlCommand cmdsalamt = new SqlCommand("Select AmountDue from TranctionMasterSuppliment where Tranction_Master_Id ='" + dtint.Rows[0]["TMID"].ToString() + "' and AmountDue > 0", con);
                                                SqlDataAdapter dtpsalamt = new SqlDataAdapter(cmdsalamt);
                                                DataSet dssalamt = new DataSet();

                                                dtpsalamt.Fill(dssalamt);
                                                if (dssalamt.Tables[0].Rows.Count > 0)
                                                {
                                                    double amt;
                                                    if (Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount.Text), 2, MidpointRounding.AwayFromZero).ToString()) > Convert.ToDouble(dssalamt.Tables[0].Rows[0]["AmountDue"].ToString()))
                                                    {
                                                        amt = 0;
                                                    }
                                                    else
                                                    {
                                                        amt = Convert.ToDouble(dssalamt.Tables[0].Rows[0]["AmountDue"].ToString()) - (Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount.Text), 2, MidpointRounding.AwayFromZero).ToString()));
                                                    }

                                                    SqlCommand cmdamtups = new SqlCommand("Update TranctionMasterSuppliment set AmountDue = '" + amt + "' where Tranction_Master_Id ='" + dtint.Rows[0]["TMID"].ToString() + "' and AmountDue > 0", con);
                                                    if (con.State.ToString() != "Open")
                                                    {
                                                        con.Open();
                                                    }
                                                    cmdamtups.ExecuteNonQuery();
                                                    con.Close();




                                                }
                                                SqlCommand cmdinspay = new SqlCommand("Insert into PaymentApplicationTbl values('" + dtint.Rows[0]["TMID"].ToString() + "','0','" + Math.Round(Convert.ToDecimal(txtAmount.Text), 2, MidpointRounding.AwayFromZero).ToString() + "','" + txtTodate.Text + "','" + Session["userid"].ToString() + "','0')", con);
                                                if (con.State.ToString() != "Open")
                                                {
                                                    con.Open();
                                                }
                                                cmdinspay.ExecuteNonQuery();
                                                con.Close();
                                            }
                                            DataTable dtapprequirment = ClsAccountAppr.Apprreuqired();
                                            if (dtapprequirment.Rows.Count > 0)
                                            {

                                                ClsAccountAppr.AccountAppMaster(ddlwarehouse.SelectedValue, ViewState["tid"].ToString(),chkappentry.Checked);
                                            }
                                        }
                                        txtMemo.Text = "";
                                        lblmsg.Visible = true;
                                        lblmsg.Text = "";
                                        lblmsg.Text = "Record inserted successfully";
                                        txtAmount.Text = "";
                                        //GridView2.Visible = false;
                                        GridView2.DataSource = null;
                                        GridView2.DataBind();
                                        chkbkinvoice.Checked = false;

                                        chkappamount.Checked = false;
                                        txtAmount_TextChanged(sender, e);
                                        entryno();
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
                                            //  ModalPopupExtender1.Show();
                                            //filldoc();
                                        }
                                    }

                                }

                                else
                                {
                                    if (q == 1)
                                    { return; }
                                    else
                                    {
                                        lblamt1.Text = "";
                                        lblamt2.Text = "";
                                        lblamt3.Text = "";
                                        lblamt4.Text = "";
                                        lblamt5.Text = "";
                                        lblamt6.Text = "";
                                        lblamt7.Text = "";
                                        lblamt8.Text = "";
                                        lblamt9.Text = "";
                                        Label2.Text = "";
                                        SqlCommand cmd511 = new SqlCommand("Sp_Select_Cash Receipt", con);
                                        cmd511.CommandType = CommandType.StoredProcedure;
                                        // cmd.Parameters.AddWithValue("@GroupId", Convert.ToInt32(ddlGroupName1.SelectedValue));
                                        SqlDataAdapter adp511 = new SqlDataAdapter(cmd511);
                                        cmd511.Parameters.AddWithValue("@compid", Session["comid"]);
                                        cmd511.Parameters.AddWithValue("@whid", ddlwarehouse.SelectedValue);
                                        DataSet ds511 = new DataSet();
                                        adp511.Fill(ds511);
                                        if (ds511.Tables[0].Rows.Count > 0)
                                        {
                                            if (ds511.Tables[0].Rows[0]["EntryNumber"].ToString() != "")
                                            {
                                                ViewState["entryId"] = ds511.Tables[0].Rows[0][0].ToString();
                                                db = Convert.ToDouble(ViewState["entryId"]);
                                                db = db + 1;
                                                ViewState["eid"] = db;
                                            }
                                            else
                                            {
                                                db = 1;
                                                ViewState["eid"] = db;
                                            }
                                        }
                                        else
                                        {
                                            db = 1;
                                            ViewState["eid"] = db;
                                        }

                                        double a, b, c, d, x, f, g, h, i;
                                        if (txtAmount1.Text == "")
                                        { a = 0; }
                                        else { a = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount1.Text), 2, MidpointRounding.AwayFromZero).ToString()); }
                                        if (txtAmount2.Text == "")
                                        { b = 0; }
                                        else { b = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount2.Text), 2, MidpointRounding.AwayFromZero).ToString()); }
                                        if (txtAmount3.Text == "")
                                        { c = 0; }
                                        else { c = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount3.Text), 2, MidpointRounding.AwayFromZero).ToString()); }
                                        if (txtAmount4.Text == "")
                                        { d = 0; }
                                        else { d = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount4.Text), 2, MidpointRounding.AwayFromZero).ToString()); }
                                        if (txtAmount5.Text == "")
                                        { x = 0; }
                                        else { x = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount5.Text), 2, MidpointRounding.AwayFromZero).ToString()); }
                                        if (txtAmount6.Text == "")
                                        { f = 0; }
                                        else { f = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount6.Text), 2, MidpointRounding.AwayFromZero).ToString()); }
                                        if (txtAmount7.Text == "")
                                        { g = 0; }
                                        else { g = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount7.Text), 2, MidpointRounding.AwayFromZero).ToString()); }
                                        if (txtAmount8.Text == "")
                                        { h = 0; }
                                        else { h = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount8.Text), 2, MidpointRounding.AwayFromZero).ToString()); }
                                        if (txtAmount9.Text == "")
                                        { i = 0; }
                                        else { i = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount9.Text), 2, MidpointRounding.AwayFromZero).ToString()); }

                                        //a = Convert.ToDouble(txtAmount1.Text);
                                        //b = Convert.ToDouble(txtAmount2.Text);
                                        //c = Convert.ToDouble(txtAmount3.Text);
                                        //d = Convert.ToDouble(txtAmount4.Text);
                                        //x = Convert.ToDouble(txtAmount5.Text);
                                        //f = Convert.ToDouble(txtAmount6.Text);
                                        //g = Convert.ToDouble(txtAmount7.Text);
                                        //h = Convert.ToDouble(txtAmount8.Text);
                                        //i = Convert.ToDouble(txtAmount9.Text);

                                        double y = a + b + c + d + x + f + g + h + i;
                                        //txtAmount.Text = Convert.ToString(y);
                                        double z = Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount.Text), 2, MidpointRounding.AwayFromZero).ToString());
                                        //Label1.Text = Convert.ToString(y);
                                        z = z - y;
                                        //if (y == z)
                                        if (z == 0)
                                        {

                                            SqlCommand cmd = new SqlCommand("Sp_Insert_TranctionMaster1", con);
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(txtTodate.Text));
                                            cmd.Parameters.AddWithValue("@EntryNumber", Convert.ToInt64(ViewState["eid"].ToString()));
                                            cmd.Parameters.AddWithValue("@EntryTypeId", 2);

                                            // cmd.Parameters.AddWithValue("@UserId", 1);

                                            cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["userid"]));
                                            cmd.Parameters.AddWithValue("@comid", Session["comid"]);
                                            cmd.Parameters.AddWithValue("@whid", ddlwarehouse.SelectedValue);
                                            //double dec = 0;
                                            //TextBox txt = new TextBox();
                                            //txt.Text = txtAmount.Text;
                                            //dec = Convert.ToDouble(txtAmount.Text);
                                            cmd.Parameters.AddWithValue("@Tranction_Amount", Convert.ToDecimal(Math.Round(Convert.ToDecimal(txtAmount.Text), 2, MidpointRounding.AwayFromZero).ToString()));

                                            if (con.State.ToString() != "Open")
                                            {
                                                con.Open();
                                            }
                                            cmd.ExecuteNonQuery();


                                            SqlCommand cmd2 = new SqlCommand("Sp_Select_MaxTranctionMasterID", con);
                                            cmd2.CommandType = CommandType.StoredProcedure;
                                            cmd2.Parameters.AddWithValue("@whid", ddlwarehouse.SelectedValue);
                                            SqlDataAdapter adp = new SqlDataAdapter(cmd2);
                                            DataSet ds = new DataSet();
                                            adp.Fill(ds);
                                            ViewState["Id"] = ds.Tables[0].Rows[0][0].ToString();

                                            ViewState["tid"] = ds.Tables[0].Rows[0][0].ToString();
                                            SqlCommand cmd78 = new SqlCommand("Sp_Insert_TranctionMasterSuppliment", con);
                                            cmd78.CommandType = CommandType.StoredProcedure;
                                            cmd78.Parameters.AddWithValue("@TransactionMasterId", Convert.ToInt32(ViewState["Id"]));
                                            cmd78.Parameters.AddWithValue("@Memo", Convert.ToString(txtMemo.Text));
                                            cmd78.Parameters.AddWithValue("@AmountDue", DBNull.Value);

                                            //*******chnages codes
                                            //cmd78.Parameters.AddWithValue("@PartyMasterId", Convert.ToInt32(ddlPartyName.SelectedValue));
                                            //***********
                                            if (rbClassType.SelectedValue == "2")
                                            {
                                                cmd78.Parameters.AddWithValue("@PartyMasterId", Convert.ToInt32(ddlPartyName.SelectedValue));
                                            }
                                            else
                                            {
                                                cmd78.Parameters.AddWithValue("@PartyMasterId", 0);
                                            }

                                            cmd78.Parameters.AddWithValue("@GrnMasterId", DBNull.Value);
                                            cmd78.ExecuteNonQuery();


                                            SqlCommand cmd123 = new SqlCommand("Sp_Insert_Tranction_Details1", con);
                                            cmd123.CommandType = CommandType.StoredProcedure;
                                            cmd123.Parameters.AddWithValue("@AccountDebit", Convert.ToDouble(ddlAccount.SelectedValue));
                                            cmd123.Parameters.AddWithValue("@AccountCredit", "0");
                                            cmd123.Parameters.AddWithValue("@AmountDebit", Math.Round(Convert.ToDecimal(txtAmount.Text), 2, MidpointRounding.AwayFromZero).ToString());
                                            cmd123.Parameters.AddWithValue("@AmountCredit", "0");
                                            cmd123.Parameters.AddWithValue("@Tranction_Master_Id", Convert.ToDouble(ViewState["Id"]));
                                            cmd123.Parameters.AddWithValue("@Memo", txtMemo.Text);
                                            cmd123.Parameters.AddWithValue("@DateTimeOfTransaction", System.DateTime.Now);
                                            cmd123.Parameters.AddWithValue("@DiscEarn", DBNull.Value);
                                            cmd123.Parameters.AddWithValue("@DiscPaid", DBNull.Value);
                                            cmd123.Parameters.AddWithValue("@comid", Session["comid"]);
                                            cmd123.Parameters.AddWithValue("@whid", ddlwarehouse.SelectedValue);

                                            cmd123.ExecuteNonQuery();

                                            if (ddlAccountname1.SelectedIndex > 0)
                                            {

                                                SqlCommand cmd1 = new SqlCommand("Sp_Insert_Tranction_Details1", con);
                                                cmd1.CommandType = CommandType.StoredProcedure;
                                                if (Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount1.Text), 2, MidpointRounding.AwayFromZero).ToString()) > 0)
                                                {
                                                    cmd1.Parameters.AddWithValue("@AccountDebit", "0");
                                                    cmd1.Parameters.AddWithValue("@AccountCredit", Convert.ToInt32(ddlAccountname1.SelectedValue));
                                                    cmd1.Parameters.AddWithValue("@AmountDebit", "0");
                                                    cmd1.Parameters.AddWithValue("@AmountCredit", Math.Round(Convert.ToDecimal(txtAmount1.Text), 2, MidpointRounding.AwayFromZero).ToString());
                                                }
                                                else
                                                {
                                                    cmd1.Parameters.AddWithValue("@AccountDebit", Convert.ToInt32(ddlAccountname1.SelectedValue));
                                                    cmd1.Parameters.AddWithValue("@AccountCredit", "0");
                                                    cmd1.Parameters.AddWithValue("@AmountDebit", (Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount1.Text), 2, MidpointRounding.AwayFromZero).ToString()) * (-1)).ToString());
                                                    cmd1.Parameters.AddWithValue("@AmountCredit", "0");
                                                }

                                                cmd1.Parameters.AddWithValue("@Tranction_Master_Id", Convert.ToInt32(ViewState["Id"]));
                                                cmd1.Parameters.AddWithValue("@Memo", txtmemo1.Text);
                                                cmd1.Parameters.AddWithValue("@DateTimeOfTransaction", System.DateTime.Now);
                                                cmd1.Parameters.AddWithValue("@DiscEarn", DBNull.Value);
                                                cmd1.Parameters.AddWithValue("@DiscPaid", DBNull.Value);
                                                cmd1.Parameters.AddWithValue("@comid", Session["comid"]);
                                                cmd1.Parameters.AddWithValue("@whid", ddlwarehouse.SelectedValue);


                                                cmd1.ExecuteNonQuery();

                                            }

                                            if (ddlAccountname2.SelectedIndex > 0)
                                            {

                                                SqlCommand cmd1 = new SqlCommand("Sp_Insert_Tranction_Details1", con);
                                                cmd1.CommandType = CommandType.StoredProcedure;
                                                if (Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount2.Text), 2, MidpointRounding.AwayFromZero).ToString()) > 0)
                                                {
                                                    cmd1.Parameters.AddWithValue("@AccountDebit", "0");
                                                    cmd1.Parameters.AddWithValue("@AccountCredit", Convert.ToInt32(ddlAccountname2.SelectedValue));
                                                    cmd1.Parameters.AddWithValue("@AmountDebit", "0");
                                                    cmd1.Parameters.AddWithValue("@AmountCredit", Math.Round(Convert.ToDecimal(txtAmount2.Text), 2, MidpointRounding.AwayFromZero).ToString());
                                                }
                                                else
                                                {
                                                    cmd1.Parameters.AddWithValue("@AccountDebit", Convert.ToInt32(ddlAccountname2.SelectedValue));
                                                    cmd1.Parameters.AddWithValue("@AccountCredit", "0");
                                                    cmd1.Parameters.AddWithValue("@AmountDebit", (Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount2.Text), 2, MidpointRounding.AwayFromZero).ToString()) * (-1)).ToString());
                                                    cmd1.Parameters.AddWithValue("@AmountCredit", "0");
                                                }
                                                cmd1.Parameters.AddWithValue("@Tranction_Master_Id", Convert.ToInt32(ViewState["Id"]));
                                                cmd1.Parameters.AddWithValue("@Memo", txtmemo2.Text);
                                                cmd1.Parameters.AddWithValue("@DateTimeOfTransaction", System.DateTime.Now);
                                                cmd1.Parameters.AddWithValue("@DiscEarn", DBNull.Value);
                                                cmd1.Parameters.AddWithValue("@DiscPaid", DBNull.Value);
                                                cmd1.Parameters.AddWithValue("@comid", Session["comid"]);
                                                cmd1.Parameters.AddWithValue("@whid", ddlwarehouse.SelectedValue);


                                                cmd1.ExecuteNonQuery();

                                            }
                                            if (ddlAccountname3.SelectedIndex > 0)
                                            {

                                                SqlCommand cmd1 = new SqlCommand("Sp_Insert_Tranction_Details1", con);
                                                cmd1.CommandType = CommandType.StoredProcedure;
                                                if (Convert.ToDouble(txtAmount3.Text) > 0)
                                                {
                                                    cmd1.Parameters.AddWithValue("@AccountDebit", "0");
                                                    cmd1.Parameters.AddWithValue("@AccountCredit", Convert.ToInt32(ddlAccountname3.SelectedValue));
                                                    cmd1.Parameters.AddWithValue("@AmountDebit", "0");
                                                    cmd1.Parameters.AddWithValue("@AmountCredit", Math.Round(Convert.ToDecimal(txtAmount3.Text), 2, MidpointRounding.AwayFromZero).ToString());
                                                }
                                                else
                                                {
                                                    cmd1.Parameters.AddWithValue("@AccountDebit", Convert.ToInt32(ddlAccountname3.SelectedValue));
                                                    cmd1.Parameters.AddWithValue("@AccountCredit", "0");
                                                    cmd1.Parameters.AddWithValue("@AmountDebit", (Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount3.Text), 2, MidpointRounding.AwayFromZero).ToString()) * (-1)).ToString());
                                                    cmd1.Parameters.AddWithValue("@AmountCredit", "0");
                                                }
                                                cmd1.Parameters.AddWithValue("@Tranction_Master_Id", Convert.ToInt32(ViewState["Id"]));
                                                cmd1.Parameters.AddWithValue("@Memo", txtmemo3.Text);
                                                cmd1.Parameters.AddWithValue("@DateTimeOfTransaction", System.DateTime.Now);
                                                cmd1.Parameters.AddWithValue("@DiscEarn", DBNull.Value);
                                                cmd1.Parameters.AddWithValue("@DiscPaid", DBNull.Value);
                                                cmd1.Parameters.AddWithValue("@comid", Session["comid"]);
                                                cmd1.Parameters.AddWithValue("@whid", ddlwarehouse.SelectedValue);


                                                cmd1.ExecuteNonQuery();

                                            }
                                            if (ddlAccountname4.SelectedIndex > 0)
                                            {

                                                SqlCommand cmd1 = new SqlCommand("Sp_Insert_Tranction_Details1", con);
                                                cmd1.CommandType = CommandType.StoredProcedure;
                                                if (Convert.ToDouble(txtAmount4.Text) > 0)
                                                {
                                                    cmd1.Parameters.AddWithValue("@AccountDebit", "0");
                                                    cmd1.Parameters.AddWithValue("@AccountCredit", Convert.ToInt32(ddlAccountname4.SelectedValue));
                                                    cmd1.Parameters.AddWithValue("@AmountDebit", "0");
                                                    cmd1.Parameters.AddWithValue("@AmountCredit", Math.Round(Convert.ToDecimal(txtAmount4.Text), 2, MidpointRounding.AwayFromZero).ToString());
                                                }
                                                else
                                                {
                                                    cmd1.Parameters.AddWithValue("@AccountDebit", Convert.ToInt32(ddlAccountname4.SelectedValue));
                                                    cmd1.Parameters.AddWithValue("@AccountCredit", "0");
                                                    cmd1.Parameters.AddWithValue("@AmountDebit", (Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount4.Text), 2, MidpointRounding.AwayFromZero).ToString()) * (-1)).ToString());
                                                    cmd1.Parameters.AddWithValue("@AmountCredit", "0");
                                                }
                                                cmd1.Parameters.AddWithValue("@Tranction_Master_Id", Convert.ToInt32(ViewState["Id"]));
                                                cmd1.Parameters.AddWithValue("@Memo", txtmemo4.Text);
                                                cmd1.Parameters.AddWithValue("@DateTimeOfTransaction", System.DateTime.Now);
                                                cmd1.Parameters.AddWithValue("@DiscEarn", DBNull.Value);
                                                cmd1.Parameters.AddWithValue("@DiscPaid", DBNull.Value);
                                                cmd1.Parameters.AddWithValue("@comid", Session["comid"]);
                                                cmd1.Parameters.AddWithValue("@whid", ddlwarehouse.SelectedValue);


                                                cmd1.ExecuteNonQuery();


                                            }
                                            if (ddlAccountname5.SelectedIndex > 0)
                                            {

                                                SqlCommand cmd1 = new SqlCommand("Sp_Insert_Tranction_Details1", con);
                                                cmd1.CommandType = CommandType.StoredProcedure;
                                                if (Convert.ToDouble(txtAmount5.Text) > 0)
                                                {
                                                    cmd1.Parameters.AddWithValue("@AccountDebit", "0");
                                                    cmd1.Parameters.AddWithValue("@AccountCredit", Convert.ToInt32(ddlAccountname5.SelectedValue));
                                                    cmd1.Parameters.AddWithValue("@AmountDebit", "0");
                                                    cmd1.Parameters.AddWithValue("@AmountCredit", Math.Round(Convert.ToDecimal(txtAmount5.Text), 2, MidpointRounding.AwayFromZero).ToString());
                                                }
                                                else
                                                {
                                                    cmd1.Parameters.AddWithValue("@AccountDebit", Convert.ToInt32(ddlAccountname5.SelectedValue));
                                                    cmd1.Parameters.AddWithValue("@AccountCredit", "0");
                                                    cmd1.Parameters.AddWithValue("@AmountDebit", (Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount5.Text), 2, MidpointRounding.AwayFromZero).ToString()) * (-1)).ToString());
                                                    cmd1.Parameters.AddWithValue("@AmountCredit", "0");
                                                }
                                                cmd1.Parameters.AddWithValue("@Tranction_Master_Id", Convert.ToInt32(ViewState["Id"]));
                                                cmd1.Parameters.AddWithValue("@Memo", txtmemo5.Text);

                                                cmd1.Parameters.AddWithValue("@DateTimeOfTransaction", System.DateTime.Now);
                                                cmd1.Parameters.AddWithValue("@DiscEarn", DBNull.Value);
                                                cmd1.Parameters.AddWithValue("@DiscPaid", DBNull.Value);
                                                cmd1.Parameters.AddWithValue("@comid", Session["comid"]);
                                                cmd1.Parameters.AddWithValue("@whid", ddlwarehouse.SelectedValue);


                                                cmd1.ExecuteNonQuery();


                                            }
                                            if (ddlAccountname6.SelectedIndex > 0)
                                            {

                                                SqlCommand cmd1 = new SqlCommand("Sp_Insert_Tranction_Details1", con);
                                                cmd1.CommandType = CommandType.StoredProcedure;
                                                if (Convert.ToDouble(txtAmount6.Text) > 0)
                                                {
                                                    cmd1.Parameters.AddWithValue("@AccountDebit", "0");
                                                    cmd1.Parameters.AddWithValue("@AccountCredit", Convert.ToInt32(ddlAccountname6.SelectedValue));
                                                    cmd1.Parameters.AddWithValue("@AmountDebit", "0");
                                                    cmd1.Parameters.AddWithValue("@AmountCredit", Math.Round(Convert.ToDecimal(txtAmount6.Text), 2, MidpointRounding.AwayFromZero).ToString());
                                                }
                                                else
                                                {
                                                    cmd1.Parameters.AddWithValue("@AccountDebit", Convert.ToInt32(ddlAccountname6.SelectedValue));
                                                    cmd1.Parameters.AddWithValue("@AccountCredit", "0");
                                                    cmd1.Parameters.AddWithValue("@AmountDebit", (Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount6.Text), 2, MidpointRounding.AwayFromZero).ToString()) * (-1)).ToString());
                                                    cmd1.Parameters.AddWithValue("@AmountCredit", "0");
                                                }
                                                cmd1.Parameters.AddWithValue("@Tranction_Master_Id", Convert.ToInt32(ViewState["Id"]));
                                                cmd1.Parameters.AddWithValue("@Memo", txtmemo6.Text);
                                                cmd1.Parameters.AddWithValue("@DateTimeOfTransaction", System.DateTime.Now);
                                                cmd1.Parameters.AddWithValue("@DiscEarn", DBNull.Value);
                                                cmd1.Parameters.AddWithValue("@DiscPaid", DBNull.Value);
                                                cmd1.Parameters.AddWithValue("@comid", Session["comid"]);
                                                cmd1.Parameters.AddWithValue("@whid", ddlwarehouse.SelectedValue);


                                                cmd1.ExecuteNonQuery();


                                            }
                                            if (ddlAccountname7.SelectedIndex > 0)
                                            {

                                                SqlCommand cmd1 = new SqlCommand("Sp_Insert_Tranction_Details1", con);
                                                cmd1.CommandType = CommandType.StoredProcedure;
                                                if (Convert.ToDouble(txtAmount7.Text) > 0)
                                                {
                                                    cmd1.Parameters.AddWithValue("@AccountDebit", "0");
                                                    cmd1.Parameters.AddWithValue("@AccountCredit", Convert.ToInt32(ddlAccountname7.SelectedValue));
                                                    cmd1.Parameters.AddWithValue("@AmountDebit", "0");
                                                    cmd1.Parameters.AddWithValue("@AmountCredit", Math.Round(Convert.ToDecimal(txtAmount7.Text), 2, MidpointRounding.AwayFromZero).ToString());
                                                }
                                                else
                                                {
                                                    cmd1.Parameters.AddWithValue("@AccountDebit", Convert.ToInt32(ddlAccountname7.SelectedValue));
                                                    cmd1.Parameters.AddWithValue("@AccountCredit", "0");
                                                    cmd1.Parameters.AddWithValue("@AmountDebit", (Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount7.Text), 2, MidpointRounding.AwayFromZero).ToString()) * (-1)).ToString());
                                                    cmd1.Parameters.AddWithValue("@AmountCredit", "0");
                                                }
                                                cmd1.Parameters.AddWithValue("@Tranction_Master_Id", Convert.ToInt32(ViewState["Id"]));
                                                cmd1.Parameters.AddWithValue("@Memo", txtmemo7.Text);

                                                cmd1.Parameters.AddWithValue("@DateTimeOfTransaction", System.DateTime.Now);
                                                cmd1.Parameters.AddWithValue("@DiscEarn", DBNull.Value);
                                                cmd1.Parameters.AddWithValue("@DiscPaid", DBNull.Value);
                                                cmd1.Parameters.AddWithValue("@comid", Session["comid"]);
                                                cmd1.Parameters.AddWithValue("@whid", ddlwarehouse.SelectedValue);


                                                cmd1.ExecuteNonQuery();


                                            }
                                            if (ddlAccountname8.SelectedIndex > 0)
                                            {

                                                SqlCommand cmd1 = new SqlCommand("Sp_Insert_Tranction_Details1", con);
                                                cmd1.CommandType = CommandType.StoredProcedure;
                                                if (Convert.ToDouble(txtAmount8.Text) > 0)
                                                {
                                                    cmd1.Parameters.AddWithValue("@AccountDebit", "0");
                                                    cmd1.Parameters.AddWithValue("@AccountCredit", Convert.ToInt32(ddlAccountname8.SelectedValue));
                                                    cmd1.Parameters.AddWithValue("@AmountDebit", "0");
                                                    cmd1.Parameters.AddWithValue("@AmountCredit", Math.Round(Convert.ToDecimal(txtAmount8.Text), 2, MidpointRounding.AwayFromZero).ToString());
                                                }
                                                else
                                                {
                                                    cmd1.Parameters.AddWithValue("@AccountDebit", Convert.ToInt32(ddlAccountname8.SelectedValue));
                                                    cmd1.Parameters.AddWithValue("@AccountCredit", "0");
                                                    cmd1.Parameters.AddWithValue("@AmountDebit", (Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount8.Text), 2, MidpointRounding.AwayFromZero).ToString()) * (-1)).ToString());
                                                    cmd1.Parameters.AddWithValue("@AmountCredit", "0");
                                                }
                                                cmd1.Parameters.AddWithValue("@Tranction_Master_Id", Convert.ToInt32(ViewState["Id"]));
                                                cmd1.Parameters.AddWithValue("@Memo", txtmemo8.Text);
                                                cmd1.Parameters.AddWithValue("@DateTimeOfTransaction", System.DateTime.Now);
                                                cmd1.Parameters.AddWithValue("@DiscEarn", DBNull.Value);
                                                cmd1.Parameters.AddWithValue("@DiscPaid", DBNull.Value);
                                                cmd1.Parameters.AddWithValue("@comid", Session["comid"]);
                                                cmd1.Parameters.AddWithValue("@whid", ddlwarehouse.SelectedValue);

                                                cmd1.ExecuteNonQuery();


                                            }
                                            if (ddlAccountname9.SelectedIndex > 0)
                                            {

                                                SqlCommand cmd1 = new SqlCommand("Sp_Insert_Tranction_Details1", con);
                                                cmd1.CommandType = CommandType.StoredProcedure;
                                                if (Convert.ToDouble(txtAmount9.Text) > 0)
                                                {
                                                    cmd1.Parameters.AddWithValue("@AccountDebit", "0");
                                                    cmd1.Parameters.AddWithValue("@AccountCredit", Convert.ToInt32(ddlAccountname9.SelectedValue));
                                                    cmd1.Parameters.AddWithValue("@AmountDebit", "0");
                                                    cmd1.Parameters.AddWithValue("@AmountCredit", Math.Round(Convert.ToDecimal(txtAmount9.Text), 2, MidpointRounding.AwayFromZero).ToString());
                                                }
                                                else
                                                {
                                                    cmd1.Parameters.AddWithValue("@AccountDebit", Convert.ToInt32(ddlAccountname9.SelectedValue));
                                                    cmd1.Parameters.AddWithValue("@AccountCredit", "0");
                                                    cmd1.Parameters.AddWithValue("@AmountDebit", (Convert.ToDouble(Math.Round(Convert.ToDecimal(txtAmount9.Text), 2, MidpointRounding.AwayFromZero).ToString()) * (-1)).ToString());
                                                    cmd1.Parameters.AddWithValue("@AmountCredit", "0");
                                                }
                                                cmd1.Parameters.AddWithValue("@Tranction_Master_Id", Convert.ToInt32(ViewState["Id"]));
                                                cmd1.Parameters.AddWithValue("@Memo", txtmemo9.Text);
                                                cmd1.Parameters.AddWithValue("@DateTimeOfTransaction", System.DateTime.Now);
                                                cmd1.Parameters.AddWithValue("@DiscEarn", DBNull.Value);
                                                cmd1.Parameters.AddWithValue("@DiscPaid", DBNull.Value);
                                                cmd1.Parameters.AddWithValue("@comid", Session["comid"]);
                                                cmd1.Parameters.AddWithValue("@whid", ddlwarehouse.SelectedValue);


                                                cmd1.ExecuteNonQuery();


                                            }

                                            DataTable dtapprequirment = ClsAccountAppr.Apprreuqired();
                                            if (dtapprequirment.Rows.Count > 0)
                                            {

                                                ClsAccountAppr.AccountAppMaster(ddlwarehouse.SelectedValue, ViewState["Id"].ToString(), chkappentry.Checked);
                                            }
                                            txtAmount.Text = "";
                                            //GridView2.Visible = false;
                                            GridView2.DataSource = null;
                                            GridView2.DataBind();
                                            chkbkinvoice.Checked = false;
                                            chkappamount.Checked = false;
                                            entryno();
                                            //ddlpartytype.Items.Clear();
                                            // ddlPartyName.Items.Clear();
                                            //  rbClassType.Items[0].Selected = false;
                                            //  rbClassType.Items[1].Selected = false;
                                            txtMemo.Text = "";
                                            Label2.Visible = true;
                                            //ddlAccount.SelectedIndex = -1;
                                            Label2.Text = "Record inserted successfully";
                                            //lblmsg.Text = "Record inserted successfully";
                                            lblgratot.Text = "";
                                            controlclear();
                                            txtAmount_TextChanged(sender, e);
                                            entryno();
                                        }
                                        else
                                        {
                                            Label2.Visible = true; Label2.Text = "Your payment applied amount does not match the total payment amount " +
                     " Please change the amount of the existing account debited or apply remaining " +
                     " amount to another account ";
                                            if (txtAmount1.Text == "")
                                            {
                                                lblamt1.Text = "*";
                                            }
                                            else if (txtAmount2.Text == "")
                                            {
                                                lblamt2.Text = "*";
                                            }
                                            else if (txtAmount3.Text == "")
                                            {
                                                lblamt3.Text = "*";
                                            }
                                            else if (txtAmount4.Text == "")
                                            {
                                                lblamt4.Text = "*";
                                            }
                                            else if (txtAmount5.Text == "")
                                            {
                                                lblamt5.Text = "*";
                                            }
                                            else if (txtAmount6.Text == "")
                                            {
                                                lblamt6.Text = "*";
                                            }
                                            else if (txtAmount7.Text == "")
                                            {
                                                lblamt7.Text = "*";
                                            }
                                            else if (txtAmount8.Text == "")
                                            {
                                                lblamt8.Text = "*";
                                            }
                                            else if (txtAmount9.Text == "")
                                            {
                                                lblamt9.Text = "*";
                                            }

                                            return;
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
                                            //  ModalPopupExtender1.Show();
                                            // filldoc();
                                        }
                                    }

                                }

                            }
                        }
                    }
                    else
                    {
                        Label2.Visible = true;
                        Label2.Text = "Sorry, You are not permitted to insert greater record as per Price plan";
                    }
                }
                else
                {
                    Label2.Visible = true;
                    Label2.Text = "Please check your date. You cannot select any date earlier/later than start/end date of the year";
                }
            }
       }
        else
        {
            Label2.Visible = true;
            Label2.Text = "You are not permited to insert record for this page.";

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        txtAmount1.Text = "";
        txtAmount2.Text = "";
        txtAmount3.Text = "";
        txtAmount4.Text = "";
        txtAmount5.Text = "";
        txtAmount6.Text = "";
        txtAmount7.Text = "";
        txtAmount8.Text = "";
        txtAmount9.Text = "";
        //txtTodate.Text = "";
        //txtenteryNumber.Text = "";
        txtMemo.Text = "";
        txtmemo1.Text = "";
        txtmemo2.Text = "";
        txtmemo3.Text = "";
        txtmemo4.Text = "";
        txtmemo5.Text = "";
        txtmemo6.Text = "";
        txtmemo7.Text = "";
        txtmemo8.Text = "";
        txtmemo9.Text = "";
        txtAmount.Text = "";
        ddlAccount.SelectedIndex = -1;
        ddlAccountname1.SelectedIndex = -1;
        ddlAccountname2.SelectedIndex = -1;
        ddlAccountname3.SelectedIndex = -1;
        ddlAccountname4.SelectedIndex = -1;
        ddlAccountname5.SelectedIndex = -1;
        ddlAccountname6.SelectedIndex = -1;

        ddlAccountname7.SelectedIndex = -1;
        ddlAccountname8.SelectedIndex = -1;
        ddlAccountname9.SelectedIndex = -1;
        ddlPartyName.SelectedIndex = -1;
        GridView2.DataSource = null;
        GridView2.DataBind();
        GridView2.EmptyDataText = "";
        chkbkinvoice.Checked = false;
        lblmsg.Visible = false;
    

    }
    //protected void ImageButton4_Click1(object sender, EventArgs e)
    //{
    //    ModalPopupExtender1.Hide();
    //}


    protected void ImageButton1212_Click(object sender, EventArgs e)
    {

    }
    //protected void btnCancel_Click1(object sender, EventArgs e)
    //{
        
    //}
   
  
  
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (ddlpartytype.SelectedIndex > 0)
        {
                    SqlCommand cmd = new SqlCommand("Sp_Select_PartyNameByPartyType", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@PartyTypeId", Convert.ToInt32(rbClassType.SelectedValue));
            cmd.Parameters.AddWithValue("@PartyTypeId", ddlpartytype.SelectedValue);
            cmd.Parameters.AddWithValue("@compid", compid);
            cmd.Parameters.AddWithValue("@whid", ddlwarehouse.SelectedValue);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            ddlPartyName.DataSource = ds;
            ddlPartyName.DataTextField = "Compname";
            ddlPartyName.DataValueField = "PartyID";
            ddlPartyName.DataBind();
            ddlPartyName.Items.Insert(0, "--Select--");
        }
    }
    protected void LinkButton2_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlwarehouse.SelectedIndex > 0)
        {
            ddlPartyName.Items.Insert(0, "--Select--");
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
    protected void lnkadd0_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlwarehouse.SelectedIndex > 0)
        {
            ddlAccount.DataSource = (DataSet)fillddl2();
            ddlAccount.DataValueField = "AccountId";
            ddlAccount.DataTextField = "AccountName";
            ddlAccount.DataBind();
            //ddlAccount.DataBind();
            ddlAccount.Items.Insert(0, "--Select--");
        }
    }
    protected void LinkButton97666667_Click(object sender, ImageClickEventArgs e)
    {
        string te = "AccountMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void LinkButton112244_Click(object sender, ImageClickEventArgs e)
    {
        string te = "AccountMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }

    protected void chkorder_CheckedChanged(object sender, EventArgs e)
    {
        fillgr();
        invordcal();

        if (chkbkinvoice.Checked == true && chkorder.Checked == true)
        {
            GridView2.Columns[3].Visible = true;
            GridView2.Columns[4].Visible = true;
            GridView2.Columns[5].Visible = true;
            Panel2.Visible = true;
            GridView2.Visible = true;
        }
        else if (chkbkinvoice.Checked == true)
        {
            GridView2.Columns[3].Visible = true;
            GridView2.Columns[4].Visible = true;
            GridView2.Columns[5].Visible = false;
            Panel2.Visible = true;
            GridView2.Visible = true;
        }
        else if (chkorder.Checked == true)
        {
            GridView2.Columns[3].Visible = false;
            GridView2.Columns[4].Visible = false;
            GridView2.Columns[5].Visible = true;
            Panel2.Visible = true;
            GridView2.Visible = true;
        }
        else
        {
            Panel2.Visible = false;
            //GridView2.Visible = false;
        }
      
    }

    protected void LinkButton97666667_Click1(object sender, ImageClickEventArgs e)
    {
        string te = "PartyMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void LinkButton1_Click(object sender, ImageClickEventArgs e)
    {

        ddlpartytype_SelectedIndexChanged(sender, e);
      
    }
}
