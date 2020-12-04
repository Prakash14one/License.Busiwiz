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

public partial class ShoppingCart_Admin_JournalEntryCrDrCompany : System.Web.UI.Page
{
    int i;
    int accid = 0;

    SqlConnection con=new SqlConnection(PageConn.connnn);
    //SqlConnection connection;
    //SqlConnection ifile = new SqlConnection(ConfigurationManager.ConnectionStrings["ifilecabinateConnectionString"].ConnectionString);

    string compid;
    string page;
    protected void Page_Load(object sender, EventArgs e)
    {
        //PageConn pgcon = new PageConn();
        //con = pgcon.dynconn;
        //connection = pgcon.dynconn; 
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();
        compid = Session["Comid"].ToString();
        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);

        if (!IsPostBack)
        {
            if (Request.QueryString["docid"] != null)
            {

                DataTable dtpt11 = MainAcocount.SelectDocumentMasterwithId(Convert.ToString(Request.QueryString["docid"]));


                if (dtpt11.Rows.Count > 0)
                {
                    Label2.Text = dtpt11.Rows[0]["DocumentId"].ToString();
                    Label10.Text = dtpt11.Rows[0]["DocumentTitle"].ToString();

                    Label11.Text = Convert.ToDateTime(dtpt11.Rows[0]["DocumentDate"]).ToShortDateString(); ;

                    Label12.Text = dtpt11.Rows[0]["DocumentType"].ToString();
                    Fillddlwarehouse();
                    ddlwarehouse.SelectedValue = dtpt11.Rows[0]["WarehouseId"].ToString();
                    ddlwarehouse.Enabled = false;
                    ddlwarehouse_SelectedIndexChanged(sender, e);
                    pnnl.Visible = true;
                }
            }
        }
        if (!IsPostBack)
        {

            if (Request.QueryString["docid"] == null)
            {
                fillware();

                DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

                if (dteeed.Rows.Count > 0)
                {
                    ddlwarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);

                }

            }


            lblMSG.Visible = false;
            txtDate.Text = System.DateTime.Now.ToShortDateString();
            lblentrytype.Text = "Journal Entry";
            if (Request.QueryString["Tid"] != null)
            { // if (Request.QueryString["EntryN"] != null && Request.QueryString["EntryT"] != null)
                ddlwarehouse.Enabled = false;
                ViewState["Trid"] = Request.QueryString["Tid"].ToString();
                ViewState["tid"] = ViewState["Trid"];
                FillRequestedData();

            }
            else
            {
                fillwareselect();
            }
        }

    }
    public void fillwareselect()
    {
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
        lblMSG.Text = "";
        ImageButton3.Enabled = true;
        btnedit.Enabled = true;
        DataTable dtpaacc = select("Select * from AccountPageRightAccess where cid='" + Session["Comid"] + "' and Access='1' and AccType='0'");
        if (dtpaacc.Rows.Count > 0)
        {
            ImageButton3.Enabled = false;
            btnedit.Enabled = false;
            DataTable dtrc = select(" select Accountingpagerightwithdesignation.*  from Accountingpagerightwithdesignation inner join DesignationMaster on Accountingpagerightwithdesignation.DesignationId=DesignationMaster.DesignationMasterId " +
           " inner join DepartmentmasterMNC on DepartmentmasterMNC.Id=DesignationMaster.DeptId where DesignationId='" + Session["DesignationId"] + "' and  DepartmentmasterMNC.whid='" + ddlwarehouse.SelectedValue + "'");
            if (dtrc.Rows.Count > 0)
            {
                if (Convert.ToInt32(dtrc.Rows[0]["AccessRight"]) == 0)
                {

                    lblMSG.Text = "Sorry,you are not access right for this business";
                }
                else if (Convert.ToInt32(dtrc.Rows[0]["AccessRight"]) == 2)
                {
                    if (Convert.ToInt32(dtrc.Rows[0]["Edit_Right"]) == 1)
                    {
                        btnedit.Enabled = true;
                    }
                    else if (Convert.ToInt32(dtrc.Rows[0]["Insert_Right"]) == 1)
                    {
                        ImageButton3.Enabled = true;
                    }
                }
                else
                {
                    ImageButton3.Enabled = true;
                    btnedit.Enabled = true;
                }
            }
            else
            {
                lblMSG.Text = "Sorry,you are not access right for this business";

            }
        }
        if (Request.QueryString["docid"] != null)
        {
            chkdoc.Checked = false;
            chkdoc.Visible = false;
        }
        else
        {

            chkdoc.Visible = true;

        }

        DataTable ds = MainAcocount.SelectEntrynumber("3", ddlwarehouse.SelectedValue);

        if (ds.Rows.Count > 0)
        {
            if (ds.Rows[0]["Id"].ToString() != "")
            {
                i = Convert.ToInt32(ds.Rows[0]["Id"]) + 1;
            }
            else
            {
                i = 1;

            }
        }
        else
        {
            i = 1;
        }
        txtEntryNo.Text = Convert.ToString(i);

        accountfill();

    }
    public void accountfill()
    {
        ddlAccount.Items.Clear();
       // DataTable ds1 = MainAcocount.SelectAccountwithwhid(ddlwarehouse.SelectedValue);

        string str = "SELECT  distinct LEFT( GroupCompanyMaster.groupdisplayname,30) + '-' +  LEFT( AccountMaster.AccountName,30)  AS Account,  AccountMaster.AccountId   FROM       AccountMaster INNER JOIN  GroupCompanyMaster ON AccountMaster.GroupId = GroupCompanyMaster.GroupId  WHERE   (AccountMaster.AccountId IS NOT NULL) and AccountMaster.Status=1 and AccountMaster.compid ='" + Session["Comid"].ToString() + "' and accountmaster.Whid='" + ddlwarehouse.SelectedValue + "' and GroupCompanyMaster.Whid='" + ddlwarehouse.SelectedValue + "'  ORDER BY Account  ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds1 = new DataTable();
        adp.Fill(ds1);

        if (ds1.Rows.Count > 0)
        {
            ddlAccount.DataSource = ds1;
            ddlAccount.DataTextField = "Account";
            ddlAccount.DataValueField = "AccountId";
            ddlAccount.DataBind();
        }
        //ddlAccount.Items.Insert(0, "-Select-");
        //ddlAccount.SelectedItem.Value = "0";

    }
    protected void FillRequestedData()
    {
        if (ViewState["Trid"] != null)
        {


            DataTable dtR1 = MainAcocount.SelectFillTrndata(Convert.ToString(ViewState["Trid"]));



            if (dtR1.Rows.Count > 0)
            {
                lblsss.Visible = true;
                lbldise.Visible = true;
                ddlwarehouse.SelectedValue = Convert.ToString(dtR1.Rows[0]["Whid"]);
                EventArgs e=new EventArgs();
                object sender=new object();
                ddlwarehouse_SelectedIndexChanged(sender, e);
                ViewState["TrMid1"] = Convert.ToString(dtR1.Rows[0]["Tranction_Master_Id"]);
               
                ViewState["TrDdatetimeOfTrs1"] = Convert.ToDateTime(dtR1.Rows[0]["DateTimeOfTransaction"]);
                //ViewState["TrMSuppId1"] = Convert.ToInt32(dtR1.Rows[0]["Tranction_Master_SupplimentId"]);


                txtEntryNo.Text = dtR1.Rows[0]["EntryNumber"].ToString();
                lbldise.Text = dtR1.Rows[0]["EntryNumber"].ToString();
                txtDate.Text = dtR1.Rows[0]["Date"].ToString();
                //txtdesc.Text = dtR1.Rows[0]["MasterMemo"].ToString();
                int entrytypeforCRDRinGrd = Convert.ToInt32(dtR1.Rows[0]["EntryTypeId"]);




                DataTable dt = new DataTable();
                foreach (DataRow ddd in dtR1.Rows)
                {



                    if (ViewState["dt"] == null)
                    {
                        //DataTable dt = new DataTable();

                        DataColumn dtcom = new DataColumn();
                        dtcom.DataType = System.Type.GetType("System.String");
                        dtcom.ColumnName = "AccountId";
                        dtcom.ReadOnly = false;
                        dtcom.Unique = false;
                        dtcom.AllowDBNull = true;

                        dt.Columns.Add(dtcom);


                        DataColumn dtcomd = new DataColumn();
                        dtcomd.DataType = System.Type.GetType("System.String");
                        dtcomd.ColumnName = "TrDid";
                        dtcomd.ReadOnly = false;
                        dtcomd.Unique = false;
                        dtcomd.AllowDBNull = true;

                        dt.Columns.Add(dtcomd);

                        DataColumn dtcomid = new DataColumn();
                        dtcomid.DataType = System.Type.GetType("System.String");
                        dtcomid.ColumnName = "Account";
                        dtcomid.ReadOnly = false;
                        dtcomid.Unique = false;
                        dtcomid.AllowDBNull = true;

                        dt.Columns.Add(dtcomid);


                        DataColumn dtcom12 = new DataColumn();
                        dtcom12.DataType = System.Type.GetType("System.String");
                        dtcom12.ColumnName = "CrDr";
                        dtcom12.ReadOnly = false;
                        dtcom12.Unique = false;
                        dtcom12.AllowDBNull = true;

                        dt.Columns.Add(dtcom12);

                        DataColumn dtcom13 = new DataColumn();
                        dtcom13.DataType = System.Type.GetType("System.String");
                        dtcom13.ColumnName = "Amount";
                        dtcom13.ReadOnly = false;
                        dtcom13.Unique = false;
                        dtcom13.AllowDBNull = true;

                        dt.Columns.Add(dtcom13);

                        DataColumn dtcom14 = new DataColumn();
                        dtcom14.DataType = System.Type.GetType("System.String");
                        dtcom14.ColumnName = "Memo";
                        dtcom14.ReadOnly = false;
                        dtcom14.Unique = false;
                        dtcom14.AllowDBNull = true;

                        dt.Columns.Add(dtcom14);

                        DataRow dtrow = dt.NewRow();

                        //dtrow["AccountId"] = ddlAccount.SelectedValue;
                        //dtrow["Account"] = ddlAccount.SelectedItem.Text;
                        //dtrow["CrDr"] = ddlcrdr.SelectedItem.Text;
                        //dtrow["Amount"] = txtAmount.Text;
                        if (ddd["AccountCredit"].ToString() != "0" && ddd["AccountCredit"].ToString() != "")
                        {
                            dtrow["AccountId"] = ddd["AccountCredit"].ToString();


                            DataTable dtAccNm = MainAcocount.SelectAccountId(ddd["AccountCredit"].ToString(), ddlwarehouse.SelectedValue);


                            dtrow["Account"] = dtAccNm.Rows[0]["AccountName"].ToString();
                            dtrow["CrDr"] = "Credit";

                            dtrow["Amount"] = ddd["AmountCredit"].ToString();
                            dtrow["TrDid"] = ddd["Tranction_Details_Id"].ToString();
                        }
                        else if (ddd["AccountDebit"].ToString() != "0" && ddd["AccountDebit"].ToString() != "")
                        {
                            dtrow["AccountId"] = ddd["AccountDebit"];

                            DataTable dtAccNm = MainAcocount.SelectAccountId(ddd["AccountDebit"].ToString(), ddlwarehouse.SelectedValue);

                            dtrow["Account"] = Convert.ToString(dtAccNm.Rows[0]["AccountName"]);
                            dtrow["CrDr"] = "Debit";

                            dtrow["Amount"] = ddd["AmountDebit"].ToString();
                            dtrow["TrDid"] = ddd["Tranction_Details_Id"].ToString();
                        }
                        else
                        {
                            dtrow["Account"] ="";
                            dtrow["CrDr"] = "Debit";

                            dtrow["Amount"] = ddd["AmountDebit"].ToString();
                            dtrow["TrDid"] = ddd["Tranction_Details_Id"].ToString();
                        }

                        dtrow["Memo"] =Convert.ToString( ddd["Memo"]);
                        dt.Rows.Add(dtrow);
                        ViewState["dt"] = dt;
                        //GridView1.DataSource = dt;
                        //GridView1.DataBind();
                    }
                    else
                    {
                        //DataTable dt = new DataTable();
                        dt = (DataTable)ViewState["dt"];

                        DataRow dtrow = dt.NewRow();
                        //dtrow["AccountId"] = ddlAccount.SelectedValue;
                        //dtrow["Account"] = ddlAccount.SelectedItem.Text;
                        //dtrow["CrDr"] = ddlcrdr.SelectedItem.Text;
                        //dtrow["Amount"] = txtAmount.Text;
                        if (ddd["AccountCredit"].ToString() != "0" && ddd["AccountCredit"].ToString() != "")
                        {
                            dtrow["AccountId"] = ddd["AccountCredit"].ToString();


                            DataTable dtAccNm = MainAcocount.SelectAccountId(ddd["AccountCredit"].ToString(), ddlwarehouse.SelectedValue);

                            dtrow["Account"] = dtAccNm.Rows[0]["AccountName"].ToString();
                            dtrow["CrDr"] = "Credit";

                            dtrow["Amount"] = ddd["AmountCredit"].ToString();
                            dtrow["TrDid"] = ddd["Tranction_Details_Id"].ToString();

                        }
                        else if (ddd["AccountDebit"].ToString() != "0" && ddd["AccountDebit"].ToString() != "")
                        {
                            dtrow["AccountId"] = ddd["AccountDebit"];

                            DataTable dtAccNm = MainAcocount.SelectAccountId(ddd["AccountDebit"].ToString(), ddlwarehouse.SelectedValue);

                            dtrow["Account"] = dtAccNm.Rows[0]["AccountName"].ToString();
                            dtrow["CrDr"] = "Debit";

                            dtrow["Amount"] = ddd["AmountDebit"].ToString();
                            dtrow["TrDid"] = ddd["Tranction_Details_Id"].ToString();

                        }
                        else
                        {
                            dtrow["Account"] = "";
                            dtrow["CrDr"] = "Debit";

                            dtrow["Amount"] = ddd["AmountDebit"].ToString();
                            dtrow["TrDid"] = ddd["Tranction_Details_Id"].ToString();
                        }

                        dtrow["Memo"] = Convert.ToString(ddd["Memo"]);
                        dt.Rows.Add(dtrow);


                        //dtrow["Memo"] = txtmemo.Text;
                        //dt.Rows.Add(dtrow);
                        ViewState["dt"] = dt;
                        //GridView1.DataSource = dt;
                        //GridView1.DataBind();
                    }

                    DataTable ForEditGrd = new DataTable();

                    //dtE.Rows.RemoveAt(0);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    txtEntryNo.Text = dtR1.Rows[0]["EntryNumber"].ToString();
                    lbldise.Text = dtR1.Rows[0]["EntryNumber"].ToString();
                    if (Request.QueryString["vid"] != null)
                    {
                        Panel1.Enabled = false;
                        GridView1.Columns[6].Visible = false;
                        btnedit.Visible = false;
                    }
                    else
                    {
                        Panel1.Enabled = false;
                        GridView1.Columns[6].Visible = false;
                        btnedit.Visible = true;
                        //btnup.Visible = true;
                    }
                }


            }
        }
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {

        lblMSG.Visible = true;
        lblMSG.Text = "";

        if (ViewState["dt"] == null)
        {
            DataTable dt = new DataTable();

            DataColumn dtcom = new DataColumn();
            dtcom.DataType = System.Type.GetType("System.String");
            dtcom.ColumnName = "AccountId";
            dtcom.ReadOnly = false;
            dtcom.Unique = false;
            dtcom.AllowDBNull = true;

            dt.Columns.Add(dtcom);

            DataColumn dtcomid = new DataColumn();
            dtcomid.DataType = System.Type.GetType("System.String");
            dtcomid.ColumnName = "Account";
            dtcomid.ReadOnly = false;
            dtcomid.Unique = false;
            dtcomid.AllowDBNull = true;

            dt.Columns.Add(dtcomid);


            DataColumn dtcom12 = new DataColumn();
            dtcom12.DataType = System.Type.GetType("System.String");
            dtcom12.ColumnName = "CrDr";
            dtcom12.ReadOnly = false;
            dtcom12.Unique = false;
            dtcom12.AllowDBNull = true;

            dt.Columns.Add(dtcom12);

            DataColumn dtcom13 = new DataColumn();
            dtcom13.DataType = System.Type.GetType("System.String");
            dtcom13.ColumnName = "Amount";
            dtcom13.ReadOnly = false;
            dtcom13.Unique = false;
            dtcom13.AllowDBNull = true;

            dt.Columns.Add(dtcom13);

            DataColumn dtcom14 = new DataColumn();
            dtcom14.DataType = System.Type.GetType("System.String");
            dtcom14.ColumnName = "Memo";
            dtcom14.ReadOnly = false;
            dtcom14.Unique = false;
            dtcom14.AllowDBNull = true;

            dt.Columns.Add(dtcom14);

            DataColumn dtcomd = new DataColumn();
            dtcomd.DataType = System.Type.GetType("System.String");
            dtcomd.ColumnName = "TrDid";
            dtcomd.ReadOnly = false;
            dtcomd.Unique = false;
            dtcomd.AllowDBNull = true;

            dt.Columns.Add(dtcomd);

            DataRow dtrow = dt.NewRow();

            dtrow["AccountId"] = ddlAccount.SelectedValue;
            dtrow["Account"] = ddlAccount.SelectedItem.Text;
            dtrow["CrDr"] = ddlcrdr.SelectedItem.Text;
            dtrow["Amount"] = Math.Round(Convert.ToDecimal(txtAmount.Text), 2, MidpointRounding.AwayFromZero).ToString();
            dtrow["Memo"] = txtmemo.Text;
            dtrow["TrDid"] = "0";

            dt.Rows.Add(dtrow);
            ViewState["dt"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {int flag = 0;
                    foreach (GridViewRow item in GridView1.Rows)
                    {
                        string acid = item.Cells[0].Text;
                        if (acid == ddlAccount.SelectedValue)
                        {
                            lblMSG.Visible = true;
                            lblMSG.Text = "You cannot use same account name twice in one particular entry transaction";
                            flag = 1;
                            break;
                        }
                    }
                    if (flag == 0)
                    {
                        DataTable dt = new DataTable();
                        dt = (DataTable)ViewState["dt"];

                        DataRow dtrow = dt.NewRow();
                        dtrow["AccountId"] = ddlAccount.SelectedValue;
                        dtrow["Account"] = ddlAccount.SelectedItem.Text;
                        dtrow["CrDr"] = ddlcrdr.SelectedItem.Text;
                        dtrow["Amount"] = Math.Round(Convert.ToDecimal(txtAmount.Text), 2, MidpointRounding.AwayFromZero).ToString();
                        dtrow["Memo"] = txtmemo.Text;
                        dtrow["TrDid"] = "0";
                        dt.Rows.Add(dtrow);
                        ViewState["dt"] = dt;
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
        }

      


        if (Request.QueryString["Tid"] == null)
        {
            if (GridView1.Rows.Count > 0)
            {
                ImageButton3.Visible = true;
                Button2.Visible = false;
                ddlwarehouse.Enabled = false;
                lblsss.Visible = true;
                lbldise.Visible = true;
                lbldise.Text = txtEntryNo.Text;
            }
            else
            {
                ImageButton3.Visible = false;
                Button2.Visible = false;
                ddlwarehouse.Enabled = true;
                lblsss.Visible = false;
                lbldise.Visible = false;
            }
        }
        decimal totalCr = 0;
        decimal totalDr = 0;
        foreach (GridViewRow gdr in GridView1.Rows)
        {
            if (gdr.Cells[2].Text == "Credit")
            {
                totalCr = totalCr + Convert.ToDecimal(gdr.Cells[3].Text);
            }
            else
            {
                totalDr = totalDr + Convert.ToDecimal(gdr.Cells[3].Text);
            }
        }
        if (totalCr > totalDr)
        {
            ddlcrdr.SelectedIndex = ddlcrdr.Items.IndexOf(ddlcrdr.Items.FindByText("Debit"));
            txtAmount.Text = Convert.ToString(totalCr - totalDr);
        }
        else if (totalCr < totalDr)
        {
            ddlcrdr.SelectedIndex = ddlcrdr.Items.IndexOf(ddlcrdr.Items.FindByText("Credit"));
            txtAmount.Text = Convert.ToString(totalDr - totalCr);
        }
        txtmemo.Focus();



    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {

        DataTable dt1111 = MainAcocount.SelectReportPeriodwithWhid(ddlwarehouse.SelectedValue);
        if (dt1111.Rows.Count > 0)
        {
            if (Convert.ToDateTime(txtDate.Text) >= Convert.ToDateTime(dt1111.Rows[0]["StartDate"]) && Convert.ToDateTime(txtDate.Text) <= Convert.ToDateTime(dt1111.Rows[0]["EndDate"]))
            {
                lblMSG.Text = "";
               // lblstartdate.Text = dt1111.Rows[0][0].ToString();
              //  ModalPopupExtender2.Show();
                checkCrDr();
                lblsss.Visible = false;
                lbldise.Visible = false;
                   }
            else
            {
               
                lblMSG.Visible = true;
                lblMSG.Text = "Please check your date. You cannot select any date earlier/later than start/end date of the year";

        
            }
        }


        


    }
       protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;

    }
     
    public void checkCrDr()
    {  int accesin = 0;
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
            bool access = UserAccess.Usercon("TranctionMaster", "", "Tranction_Master_Id", "", "", "compid", "TranctionMaster");
            if (access == true)
            {
                decimal totalCr = 0;
                decimal totalDr = 0;
                foreach (GridViewRow gdr in GridView1.Rows)
                {
                    if (gdr.Cells[2].Text == "Credit")
                    {
                        totalCr = totalCr + Convert.ToDecimal(gdr.Cells[3].Text);
                    }
                    else
                    {
                        totalDr = totalDr + Convert.ToDecimal(gdr.Cells[3].Text);
                    }
                }

                if (totalCr == totalDr)
                {
                    try
                    {

                        DataTable ds9 = MainAcocount.SelectEntrynumber("3", ddlwarehouse.SelectedValue);

                        if (ds9.Rows.Count > 0)
                        {
                            if (ds9.Rows[0]["Id"].ToString() != "")
                            {
                                i = Convert.ToInt32(ds9.Rows[0]["Id"]) + 1;
                            }
                            else
                            {
                                i = 1;

                            }
                        }
                        else
                        {
                            i = 1;
                        }



                        int id = MainAcocount.InsertTransactionMaster(Convert.ToDateTime(txtDate.Text), i.ToString(), "3", Convert.ToInt32(Session["userid"]), totalDr, ddlwarehouse.SelectedValue);
                        if (i > 0)
                        {
                            ViewState["tid"] = id.ToString();


                            int tsid = MainAcocount.Sp_Insert_TranctionMasterSuppliment(id, "", 0, 0, 0);
                            foreach (GridViewRow gdr1 in GridView1.Rows)
                            {


                                if (gdr1.Cells[2].Text == "Credit")
                                {

                                    int tdid = MainAcocount.Sp_Insert_Tranction_Details1(0, Convert.ToInt32(gdr1.Cells[0].Text), 0, Convert.ToDecimal(gdr1.Cells[3].Text), id, Convert.ToString(gdr1.Cells[4].Text.Replace("&nbsp;", "")), Convert.ToDateTime(txtDate.Text), ddlwarehouse.SelectedValue);


                                }
                                else
                                {

                                    int tdid = MainAcocount.Sp_Insert_Tranction_Details1(Convert.ToInt32(gdr1.Cells[0].Text), 0, Convert.ToDecimal(gdr1.Cells[3].Text), 0, id, Convert.ToString(gdr1.Cells[4].Text.Replace("&nbsp;", "")), Convert.ToDateTime(txtDate.Text), ddlwarehouse.SelectedValue);



                                }



                            }
                            DataTable dtapprequirment = ClsAccountAppr.Apprreuqired();
                            if (dtapprequirment.Rows.Count > 0)
                            {

                                ClsAccountAppr.AccountAppMaster(ddlwarehouse.SelectedValue, ViewState["tid"].ToString(), chkappentry.Checked);
                            }
                            lblMSG.Visible = true;
                            lblMSG.Text = "Record inserted successfully";

                        }
                        else
                        {
                            lblMSG.Visible = true;
                            lblMSG.Text = "Record is not added";
                        }


                        txtDate.Text = System.DateTime.Now.ToShortDateString();
                        lblentrytype.Text = "Journal Entry";

                        DataTable ds56 = MainAcocount.SelectEntrynumber("3", ddlwarehouse.SelectedValue);


                        if (ds56.Rows.Count > 0)
                        {
                            if (ds56.Rows[0]["Id"].ToString() != "")
                            {
                                i = Convert.ToInt32(ds56.Rows[0]["Id"]) + 1;
                            }
                            else
                            {
                                i = 1;

                            }
                        }
                        else
                        {
                            i = 1;
                        }
                        txtEntryNo.Text = Convert.ToString(i);
                        lbldise.Text = Convert.ToString(i);



                        GridView1.DataSource = null;
                        GridView1.DataBind();
                        ViewState["dt"] = null;

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
                                // filldoc();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMSG.Visible = true;
                        lblMSG.Text = "Error:" + ex.Message.ToString();

                    }



                }
                else
                {
                    if (totalCr > totalDr)
                    {
                        ddlcrdr.SelectedIndex = ddlcrdr.Items.IndexOf(ddlcrdr.Items.FindByText("Debit"));
                        txtAmount.Text = Convert.ToString(totalCr - totalDr);
                    }
                    else if (totalCr < totalDr)
                    {
                        ddlcrdr.SelectedIndex = ddlcrdr.Items.IndexOf(ddlcrdr.Items.FindByText("Credit"));
                        txtAmount.Text = Convert.ToString(totalDr - totalCr);
                    }
                    txtmemo.Focus();
                    txtmemo.Text = "";
                    lblMSG.Visible = true;
                    lblMSG.Text = "Please check the total amount of all account debited must equal the total amount of all account credited";
                }
                clear();
            }
            else
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Sorry, You are not permitted for greater record as per price plan";
            }
        }
        else
        {
            lblMSG.Visible = true;
            lblMSG.Text = "You are not permited to insert record for this page.";

        }
    }
    public void inserdocatt()
    {

        string sqlselect = "select * from DocumentMaster where DocumentId='" + Request.QueryString["docid"] + "'";
        SqlDataAdapter adpt = new SqlDataAdapter(sqlselect, con);
        DataTable dtpt = new DataTable();
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

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "remove")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["dt"];

            dt.Rows.Remove(dt.Rows[GridView1.SelectedIndex]);

            GridView1.DataSource = dt;
            GridView1.DataBind();
            ViewState["dt"] = dt;
            if (Request.QueryString["Tid"] == null)
            {
                if (GridView1.Rows.Count > 0)
                {
                    ImageButton3.Visible = true;
                    Button2.Visible = false;
                    ddlwarehouse.Enabled = false;
                    lblsss.Visible = true;
                    lbldise.Visible = true;


                }
                else
                {
                    ImageButton3.Visible = false;
                    Button2.Visible = false;
                    ddlwarehouse.Enabled = true;
                    lblsss.Visible = false;
                    lbldise.Visible = false;
                }
            }
        }
    }

    public void clear()
    {
        txtAmount.Text = "";
        txtDate.Text = DateTime.Now.ToShortDateString();
        // txtdesc.Text = "";
        //  txtEntryNo.Text = "";
        txtmemo.Text = "";

        ddlAccount.SelectedIndex = 0;
        ddlcrdr.SelectedIndex = 0;
        if (GridView1.Rows.Count > 0)
        {
            ImageButton3.Visible = true;
            Button2.Visible = false;
            ddlwarehouse.Enabled = false;

        }
        else
        {
            ImageButton3.Visible = false;
            Button2.Visible = false;
            ddlwarehouse.Enabled = true;
        }

    }

    protected void txtbalance_TextChanged(object sender, EventArgs e)
    {

    }


    protected void fillware()
    {


        DataTable ds = ClsStore.SelectStorename();

        if (ds.Rows.Count > 0)
        {
            ddlwarehouse.DataSource = ds;
            ddlwarehouse.DataValueField = "WareHouseId";
            ddlwarehouse.DataTextField = "Name";

            ddlwarehouse.DataBind();
        }
        //ddlwarehouse.Items.Insert(0, "--Select--");
        //ddlwarehouse.Items[0].Value = "0";
    }


    protected void ImageButton7_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/LedgerJour_New.aspx");
    }

    protected void ddllimittype_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
  
    protected void Fillddlwarehouse()
    {
        DataTable ds = ClsStore.SelectStorename();

        if (ds.Rows.Count > 0)
        {

            ddlwarehouse.DataSource = ds;
            ddlwarehouse.DataTextField = "Name";
            ddlwarehouse.DataValueField = "WareHouseId";
            ddlwarehouse.DataBind();
            ddlwarehouse.Items.Insert(0, "-Select-");

            //ddlwarehouse.Items[0].Value = "0";
            //ddlAccount.Items.Insert(0, "--Select--");
        }
    }
    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        fillwareselect();
    }

    public void entry()
    {
        String te = "AccEntryDocUp.aspx?Tid=" + ViewState["tid"];


        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void ImageButton10_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Hide();
    }


    protected void LinkButton97666667_Click(object sender, ImageClickEventArgs e)
    {
        string te = "AccountMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void ImageButton48_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlwarehouse.SelectedIndex > -1)
        {
            accountfill();
        }
    }
    protected void btnup_Click(object sender, EventArgs e)
    {DataTable dt1111 = MainAcocount.SelectReportPeriodwithWhid(ddlwarehouse.SelectedValue);
        if (dt1111.Rows.Count > 0)
        {
            if (Convert.ToDateTime(txtDate.Text) >= Convert.ToDateTime(dt1111.Rows[0]["StartDate"]) && Convert.ToDateTime(txtDate.Text) <= Convert.ToDateTime(dt1111.Rows[0]["EndDate"]))
            {
        ModalPopupExtender1.Show();
            }
            else
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Please check your date. You cannot select any date earlier/later than start/end date of the year";
            }

        }
    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        Panel1.Enabled = true;
        GridView1.Columns[6].Visible = true;
        ddlwarehouse.Enabled = false;
        btnup.Visible = true;
        btnedit.Visible = true;
    }
    protected void btncon_Click(object sender, EventArgs e)
    { 
        //bool access = UserAccess.Usercon("TranctionMaster", "", "Tranction_Master_Id", "", "", "compid", "TranctionMaster");
        //if (access == true)
        //{
            ModalPopupExtender1.Hide();
            string updetail = "";
            int alltd = 0;
            decimal totalCr = 0;
            decimal totalDr = 0;
            foreach (GridViewRow gdr in GridView1.Rows)
            {
                if (gdr.Cells[2].Text == "Credit")
                {
                    totalCr = totalCr + Convert.ToDecimal(gdr.Cells[3].Text);
                }
                else
                {
                    totalDr = totalDr + Convert.ToDecimal(gdr.Cells[3].Text);
                }
            }

            if (totalCr == totalDr)
            {

                try
                {



                    string strn = "update   TranctionMaster set Date='" + Convert.ToDateTime(txtDate.Text) + "' , " +
                        "  UserId='" + Convert.ToInt32(Session["userid"]) + "' , Tranction_Amount= '" + Math.Round(totalDr, 2) + "' " +
                                " where Tranction_Master_Id=" + Convert.ToInt32(ViewState["TrMid1"]) + " ";
                    SqlCommand cmdn = new SqlCommand(strn, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmdn.ExecuteNonQuery();
                    con.Close();


                    //string strg = "update   TranctionMasterSuppliment set Memo='" + txtdesc.Text + "' " +
                    //        " where Tranction_Master_SupplimentId='" + ViewState["TrMSuppId1"] + "'  ";
                    //SqlCommand cmdg = new SqlCommand(strg, con);
                    //if (con.State.ToString() != "Open")
                    //{
                    //    con.Open();
                    //}
                    //cmdg.ExecuteNonQuery();
                    //con.Close();

                    foreach (GridViewRow gdr1 in GridView1.Rows)
                    {
                        string str3 = "";
                        Label trdid = (Label)(gdr1.FindControl("lblTrDid"));
                        if (trdid.Text != "0")
                        {
                            if (trdid.Text != "")
                            {
                                if (updetail.ToString() != "")
                                {
                                    updetail = updetail + ",";
                                }

                                updetail = updetail + trdid.Text;
                                if (gdr1.Cells[2].Text == "Credit")
                                {
                                    str3 = "Update Tranction_Details Set AccountDebit='0',AccountCredit='" + Convert.ToInt32(gdr1.Cells[0].Text) + "',AmountDebit='0',AmountCredit='" + Convert.ToDecimal(gdr1.Cells[3].Text) + "',Tranction_Master_Id='" + Convert.ToInt32(ViewState["TrMid1"]) + "',Memo='" + Convert.ToString(gdr1.Cells[4].Text.Replace("&nbsp;","")) + "',DateTimeOfTransaction='" + Convert.ToDateTime(txtDate.Text).ToShortDateString() + "',whid='" + ddlwarehouse.SelectedValue + "' where Tranction_Details_Id='" + trdid.Text + "'";



                                }
                                else
                                {

                                    str3 = "Update Tranction_Details Set AccountDebit='" + Convert.ToInt32(gdr1.Cells[0].Text) + "',AccountCredit='0',AmountDebit='" + Convert.ToDecimal(gdr1.Cells[3].Text) + "',AmountCredit='0',Tranction_Master_Id='" + Convert.ToInt32(ViewState["TrMid1"]) + "',Memo='" + Convert.ToString(gdr1.Cells[4].Text.Replace("&nbsp;","")) + "',DateTimeOfTransaction='" + Convert.ToDateTime(txtDate.Text).ToShortDateString() + "',whid='" + ddlwarehouse.SelectedValue + "' where Tranction_Details_Id='" + trdid.Text + "'";

                                }
                            }

                        }

                        else
                        {
                            if (alltd == 0)
                            {
                                alltd = 1;
                                string delTrD = "";
                                if (updetail.ToString() != "")
                                {
                                    //updetail = updetail.Remove(updetail.Length - 1, 1);
                                    delTrD = "delete Tranction_Details where Tranction_Details_Id not in(" + updetail + ") and  Tranction_Master_Id='" + Convert.ToInt32(ViewState["TrMid1"]) + "'";

                                }
                                else
                                {
                                    delTrD = "delete Tranction_Details where Tranction_Master_Id='" + Convert.ToInt32(ViewState["TrMid1"]) + "'";

                                }
                                SqlCommand cmdtrd = new SqlCommand(delTrD, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                int cin = cmdtrd.ExecuteNonQuery();
                                con.Close();
                            }

                            if (gdr1.Cells[2].Text == "Credit")
                            {
                                str3 = "INSERT INTO Tranction_Details " +
                                 " (AccountDebit, AccountCredit, AmountDebit, AmountCredit, Tranction_Master_Id, Memo, DateTimeOfTransaction,compid,whid) " +
                               " VALUES     ('0','" + Convert.ToInt32(gdr1.Cells[0].Text) + "','0','" + Convert.ToDecimal(gdr1.Cells[3].Text) + "','" + Convert.ToInt32(ViewState["TrMid1"]) + "','" + Convert.ToString(gdr1.Cells[4].Text.Replace("&nbsp;","")) + "','" + Convert.ToDateTime(txtDate.Text).ToShortDateString() + "','" + Session["comid"] + "','" + ddlwarehouse.SelectedValue + "' )";



                            }
                            else
                            {
                                str3 = "INSERT INTO Tranction_Details " +
                                " (AccountDebit, AccountCredit, AmountDebit, AmountCredit, Tranction_Master_Id, Memo, DateTimeOfTransaction,compid,whid) " +
                              " VALUES     ('" + Convert.ToInt32(gdr1.Cells[0].Text) + "','0','" + Convert.ToDecimal(gdr1.Cells[3].Text) + "','0','" + Convert.ToInt32(ViewState["TrMid1"]) + "','" + Convert.ToString(gdr1.Cells[4].Text.Replace("&nbsp;","")) + "','" + Convert.ToDateTime(txtDate.Text).ToShortDateString() + "','" + Session["comid"] + "','" + ddlwarehouse.SelectedValue + "') ";


                            }
                        }

                        SqlCommand cmd4 = new SqlCommand(str3, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd4.ExecuteNonQuery();
                        con.Close();



                    }
                    btnedit.Visible = false;
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    ViewState["dt"] = null;

                    if (Request.QueryString["docid"] != null)
                    {
                        inserdocatt();
                    }
                    else
                    {
                        if (chkdoc.Checked == true)
                        {
                            entry();

                        }
                    }
                    txtAmount.Text = "";
                    txtmemo.Text = "";
                    lblMSG.Visible = true;
                    lblMSG.Text = "Record Update successfully";
                    btnup.Visible = false;
                    lblsss.Visible = false;
                    lbldise.Visible = false;
                }


                catch (Exception ex)
                {
                    lblMSG.Visible = true;
                    lblMSG.Text = "Error:" + ex.Message.ToString();

                }



            }
            else
            {
                if (totalCr > totalDr)
                {
                    ddlcrdr.SelectedIndex = ddlcrdr.Items.IndexOf(ddlcrdr.Items.FindByText("Debit"));
                    txtAmount.Text = Convert.ToString(totalCr - totalDr);
                }
                else if (totalCr < totalDr)
                {
                    ddlcrdr.SelectedIndex = ddlcrdr.Items.IndexOf(ddlcrdr.Items.FindByText("Credit"));
                    txtAmount.Text = Convert.ToString(totalDr - totalCr);
                }
                txtmemo.Focus();
                txtmemo.Text = "";
                lblMSG.Visible = true;
                lblMSG.Text = "Please check the total amount of all account debited must equal the total amount of all account credited";
            }

        //}
        //else
        //{

        //    lblMSG.Visible = true;
        //    lblMSG.Text = "Sorry, You are not permitted for greater record to Priceplan";
        //}
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        ViewState["dt"] = null;
        txtAmount.Text = "";
        txtmemo.Text = "";
        lblsss.Visible = false;
        lbldise.Visible = false;
        ImageButton3.Visible = false;
        Button2.Visible = false;

    }
}

