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
using System.Net;
using System.Net.Mail;

public partial class Account_AddCompanyEmail : System.Web.UI.Page
{
    SqlConnection con;
    MessageCls clsMessage = new MessageCls();
    MasterCls clsMaster = new MasterCls();
    DocumentCls1 clsDocument = new DocumentCls1();
    Companycls clscompany = new Companycls();
    EmployeeCls clsEmployee = new EmployeeCls();
    protected static Int32 flg = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/ShoppingCart/Admin/ShoppingCartLogin.aspx");
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

        if (Session["CompanyName"] != null)
        {
            this.Title = Session["CompanyName"] + " IFileCabinet.com - Manage Email";
        }
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            Pagecontrol.dypcontrol(Page, page);
             string str = " SELECT WareHouseId,Name,Address,CurrencyId  FROM WareHouseMaster where comid = '" + Session["comid"] + "'and WareHouseMaster.Status='" + 1 + "' order by name";
           // string str = " SELECT Name,WareHouseId from warehousemaster inner join employeemaster on warehousemaster.WareHouseId=employeemaster.whid where EmployeeMasterID='" + Convert.ToInt32(Session["EmployeeId"]) + "'";

            SqlCommand cmd1 = new SqlCommand(str, con);
            cmd1.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlstore.DataSource = dt;
            ddlstore.DataTextField = "Name";
            ddlstore.DataValueField = "WareHouseId";
            ddlstore.DataBind();

            ddlbusinessfil.DataSource = dt;
            ddlbusinessfil.DataTextField = "Name";
            ddlbusinessfil.DataValueField = "WareHouseId";
            ddlbusinessfil.DataBind();
            ddlbusinessfil.Items.Insert(0, "All");
            ddlbusinessfil.Items[0].Value = "0";
            DataTable dsss = new DataTable();
            dsss = clsMessage.Empid(ddlstore.SelectedValue);
            if (dsss.Rows.Count > 0)
            {
                Session["EmployeeIdep"] = dsss.Rows[0]["EmployeeMasterId"].ToString();
            }
            else
            {
                Session["EmployeeIdep"] = "0";
            }

            //ddlempfil.Items.Insert(0, "All");
            //ddlempfil.Items[0].Value = "0";
            //ddlemp.Items.Insert(0, "Select");
            //ddlemp.Items[0].Value = "0";
            ViewState["Dltitem"] = "";
            lblCompany.Text = Session["comid"].ToString();
            //  lblBusiness.Text = "All";

            pnle.Visible = true;
            //DataTable dtemp = new DataTable();
            //ddlemp.Items.Clear();
            //dtemp = select(" select EmployeeMaster.EmployeeMasterID,EmployeeMaster.EmployeeName + ' - '+ DesignationMaster.DesignationName as EmployeeName,dbo.EmployeeMaster.Active from EmployeeMaster inner join DesignationMaster on DesignationMaster.DesignationMasterId=EmployeeMaster.DesignationMasterId where  dbo.EmployeeMaster.Whid and EmployeeMaster.Active='1' order by EmployeeMaster.EmployeeName");//EmployeeMaster.EmployeeMasterID='" + Convert.ToInt32(Session["EmployeeId"]) + "' and
            //ddlemp.DataSource = dtemp;

            //ddlemp.DataTextField = "EmployeeName";
            //ddlemp.DataValueField = "EmployeeMasterID";
            //ddlemp.DataBind();

            //ddlempfil.DataSource = dtemp;
            //ddlempfil.DataTextField = "EmployeeName";
            //ddlempfil.DataValueField = "EmployeeMasterID";
            //ddlempfil.DataBind();


            ddlempfil.Items.Insert(0, "-Select-");
            ddlempfil.SelectedItem.Value = "0";


            FillGrid();           
            imgupdate.Visible = false;
            imgbtnsubmitCabinetAdd.Visible = true;

            //imgcancel.Visible = false;
        }

    }

    protected void imgbtnsubmitCabinetAdd_Click(object sender, EventArgs e)
    {
        bool flg = false;
        bool flg1 = false;
        DataTable dtminprt = new DataTable();
        dtminprt = clsDocument.SelectCompanyInEmailexit(txtuser.Text, ddlstore.SelectedValue);
        if (dtminprt.Rows.Count > 0)
        {
            if (dtminprt.Rows[0]["InEmailID"] != System.DBNull.Value)
            {
                string cabdoc = dtminprt.Rows[0][0].ToString();
                if (cabdoc == txtuser.Text)
                {
                    flg = true;
                }
                else
                {
                    flg = false;
                    lblext.Text = "";
                }
            }
        }
        DataTable dtminprt1 = new DataTable();
        dtminprt1 = clsDocument.SelectCompanyEmailexit(txtoutemail.Text, ddlstore.SelectedValue);
        if (dtminprt1.Rows.Count > 0)
        {
            if (dtminprt1.Rows[0]["OutEmailID"] != System.DBNull.Value)
            {
                string cabdoc = dtminprt1.Rows[0][0].ToString();
                if (cabdoc == txtoutemail.Text)
                {
                    flg1 = true;
                }
                else
                {
                    flg1 = false;
                    lblext.Text = "";
                }
            }
        }
        if (flg == true && flg1 == false)
        {
            lblext.Text = "This Incoming Email ID is already used.";
        }
        else if (flg == false && flg1 == true)
        {
            lblext.Text = "This Outgoing Email ID is already used.";
        }
        else if (flg == true && flg1 == true)
        {
            lblext.Text = "This In and Outgoing Email ID is already used.";
        }
        else if (flg == false && flg1 == false)
        {
            lblext.Text = "";
            bool chk = false;
            if (CheckBox1.Checked == true)
            {
                chk = true;
            }
            Int32 indx = 0;
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            int i = clsDocument.InsertOutgoingCompanyEmail(txtoutserver.Text, txtoutemail.Text, txtoutpass.Text, txtserver.Text, txtuser.Text, txtpass.Text, Convert.ToDateTime("01/01/1980"), indx, chk, "0", "0", txtemail.Text, txtport1.Text, txtport2.Text, txtmemailname.Text);
            SqlDataAdapter daf = new SqlDataAdapter("select max(ID) as ID from InOutCompanyEmail", con);
            DataTable dtf = new DataTable();
            daf.Fill(dtf);
            int maxinout;
            if (dtf.Rows.Count > 0)
            {
                maxinout = Convert.ToInt32(dtf.Rows[0]["ID"]);

                string strnewappru = "insert into InOutCompanyEmail_Panding(inoutid,whid,employeeid) values('" + maxinout + "','" + ddlstore.SelectedValue + "','" + ddlemp.SelectedValue + "' )";
                SqlCommand cmd1 = new SqlCommand(strnewappru, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd1.ExecuteNonQuery();
                con.Close();
            }

            if (i > 0)
            {
                string opup = "";
                if (CheckBox1.Checked == true)
                {
                    if (RadioButtonList1.SelectedValue == "2")
                    {
                        opup = "Update InOutCompanyEmail Set SetforOutgoingemail='0' where ID<>'" + i + "' and EmployeeID='" + ddlemp.SelectedValue + "'";
                    }
                    else
                    {
                        opup = "Update InOutCompanyEmail Set SetforOutgoingemail='0' where ID<>'" + i + "' and (EmployeeID='0' or EmployeeID IS NULL) and Whid='" + ddlstore.SelectedValue + "'";

                    }
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    SqlCommand cmde = new SqlCommand(opup, con);
                    cmde.ExecuteNonQuery();
                    con.Close();
                }
                DataTable dt121 = new DataTable();
                Session["EmailID"] = i.ToString();

                FillGrid();
                txtserver.Text = "";
                txtmemailname.Text = "";
                txtuser.Text = "";
                txtemail.Text = "";
                ddlemp.SelectedIndex = 0;
                txtpass.Attributes.Add("value", "");
                txtoutserver.Text = "";
                txtoutemail.Text = "";
                txtoutpass.Attributes.Add("value", "");
                CheckBox2.Checked = false;

                if (CheckBox3.Checked == true)
                {
                    ImageButton4.Visible = true;
                    btnsigup.Visible = false;
                    lblmmms.Text = "";
                    imgbtncancel.Visible = false;

                    TextBox1.Text = "";
                    TextBox2.Text = "";
                    TextBox3.Text = "";
                    TextBox4.Text = "";
                    TextBox5.Text = "";
                    TextBox6.Text = "";
                    TextBox7.Text = "";
                    TextBox8.Text = "";
                    TextBox9.Text = "";
                    TextBox10.Text = "";

                    ModalPopupExtender2.Show();
                }
                lblext.Text = "Record inserted successfully";
                btnadddd.Visible = true;
                pnladdd.Visible = false;
                Label24.Text = "";
            }

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
    public void FillGrid()
    {
        //string str = "";

        //string str2 = "";
       
        //str = "  [EmployeeMaster].EmployeeName,warehousemaster.name as Wname,ID,OutgoingEmailServer, OutEmailID, OutPassword,IncomingEmailServer,InEmailID,InPassword,LastDownloadedTime,LastDownloadIndex,SetforOutgoingemail,InOutCompanyEmail.EmailId FROM InOutCompanyEmail Left Join EmployeeMaster on EmployeeMaster.EmployeeMasterID=InOutCompanyEmail.EmployeeId inner join warehousemaster on warehousemaster.WareHouseId=InOutCompanyEmail.Whid where comid='" + Session["Comid"].ToString() + "' and InOutCompanyEmail.Employeeid='" + ddlempfil.SelectedValue + "' and warehousemaster.status='1'";
        //str2 = "select count(InOutCompanyEmail.ID) as ci FROM InOutCompanyEmail Left Join EmployeeMaster on EmployeeMaster.EmployeeMasterID=InOutCompanyEmail.EmployeeId inner join warehousemaster on warehousemaster.WareHouseId=InOutCompanyEmail.Whid where comid='" + Session["Comid"].ToString() + "' and InOutCompanyEmail.Employeeid='" + ddlempfil.SelectedValue + "' and warehousemaster.status='1'";

        //GridEmail.VirtualItemCount = GetRowCount(str2);

        //string sortExpression = " warehousemaster.name,EmployeeName,EmailId";

        //if (Convert.ToInt32(ViewState["count"]) > 0)
        //{
        //    DataTable dt = GetDataPage(GridEmail.PageIndex, GridEmail.PageSize, sortExpression, str);

        //    DataView myDataView = new DataView();
        //    myDataView = dt.DefaultView;

        //    if (hdnsortExp.Value != string.Empty)
        //    {
        //        myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        //    }
        //    GridEmail.DataSource = dt;
        //    GridEmail.DataBind();
        //}

        //else
        //{
        //    GridEmail.DataSource = null;
        //    GridEmail.DataBind();

        //}
        string condi="";
        if (ddlbusinessfil.SelectedIndex > 0)
        {
            condi = " and dbo.InOutCompanyEmail_Panding.whid='" + ddlbusinessfil.SelectedValue + "' ";
        }
        if (ddlempfil.SelectedIndex > 0)
        {
            condi += "  and InOutCompanyEmail_Panding.employeeid='" + ddlempfil.SelectedValue + "' ";
        }
        string strgrd = " SELECT dbo.EmployeeMaster.EmployeeName, dbo.WareHouseMaster.Name AS Wname, dbo.InOutCompanyEmail.ID, dbo.InOutCompanyEmail.OutgoingEmailServer, dbo.InOutCompanyEmail.OutEmailID, dbo.InOutCompanyEmail.OutPassword, dbo.InOutCompanyEmail.IncomingEmailServer, dbo.InOutCompanyEmail.InEmailID,  dbo.InOutCompanyEmail.InPassword, dbo.InOutCompanyEmail.LastDownloadedTime, dbo.InOutCompanyEmail.LastDownloadIndex,  dbo.InOutCompanyEmail.SetforOutgoingemail, dbo.InOutCompanyEmail.EmailId FROM dbo.InOutCompanyEmail INNER JOIN dbo.InOutCompanyEmail_Panding ON dbo.InOutCompanyEmail.ID = dbo.InOutCompanyEmail_Panding.inoutid INNER JOIN dbo.EmployeeMaster ON dbo.InOutCompanyEmail_Panding.employeeid = dbo.EmployeeMaster.EmployeeMasterID INNER JOIN dbo.WareHouseMaster ON dbo.InOutCompanyEmail_Panding.whid = dbo.WareHouseMaster.WareHouseId where comid='" + Session["Comid"].ToString() + "'   and dbo.InOutCompanyEmail.Whid='0' AND dbo.InOutCompanyEmail.EmployeeID='0' " + condi + " ";
        SqlDataAdapter daff = new SqlDataAdapter(strgrd, con);
        DataTable dtg = new DataTable();
        daff.Fill(dtg);
        GridEmail.DataSource = dtg;

        //  DataView myDataView = new DataView();
        // myDataView = dtg.DefaultView;
        //if (hdnsortExp.Value != string.Empty)
        //{
        //    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        //}
        GridEmail.DataBind();


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

    //protected DataTable select(string qu)
    //{
    //    SqlCommand cmd = new SqlCommand(qu, con);
    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    adp.Fill(dt);
    //    return dt;
    //}

    protected void GridEmail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void GridEmail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete1")
        {
            int indx = Convert.ToInt32(e.CommandArgument);

            SqlDataAdapter dafv = new SqlDataAdapter("select SetforOutgoingemail from InOutCompanyEmail where ID='" + indx + "'", con);
            DataTable dtfv = new DataTable();
            dafv.Fill(dtfv);

            if (Convert.ToString(dtfv.Rows[0]["SetforOutgoingemail"]) == "True")
            {
                ModalPopupExtender1.Show();
            }
            else
            {
                if (indx != null)
                {
                    string result1 = "delete from InOutCompanyEmail where ID='" + indx + "'";
                    SqlCommand cmddel = new SqlCommand(result1, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmddel.ExecuteNonQuery();
                    con.Close();
                    FillGrid();
                    lblext.Text = "Record deleted successfully.";
                }
            }
        }

        if (e.CommandName == "Edit")
        {
            btnadddd.Visible = false;
            pnladdd.Visible = true;
            Label24.Text = "Edit Company Email";
            lblext.Text = "";

            int datakey = Convert.ToInt32(e.CommandArgument);
            Session["EmailID"] = datakey;
            DataTable dt = new DataTable();

            dt = select("SELECT ID,EmailName,OutgoingEmailServer, OutEmailID,Whid, OutPassword,IncomingEmailServer,InEmailID,InPassword,LastDownloadedTime,LastDownloadIndex,SetforOutgoingemail,EmailId,EmployeeID,InPort,OutPort FROM         InOutCompanyEmail where  ID='" + datakey + "'");

            if (dt.Rows.Count > 0)
            {
                ddlstore.SelectedIndex = ddlstore.Items.IndexOf(ddlstore.Items.FindByValue(dt.Rows[0]["Whid"].ToString()));

                if (Convert.ToString(dt.Rows[0]["EmployeeID"]) != "0")
                {
                    RadioButtonList1.SelectedValue = "2";
                    RadioButtonList1_SelectedIndexChanged(sender, e);
                    ddlemp.SelectedIndex = ddlemp.Items.IndexOf(ddlemp.Items.FindByValue(dt.Rows[0]["EmployeeID"].ToString()));

                }
                else
                {
                    RadioButtonList1.SelectedValue = "1";
                    RadioButtonList1_SelectedIndexChanged(sender, e);
                }

                txtmemailname.Text = Convert.ToString(dt.Rows[0]["EmailName"].ToString());

                txtemail.Text = Convert.ToString(dt.Rows[0]["EmailId"].ToString());

                txtoutserver.Text = Convert.ToString(dt.Rows[0]["OutgoingEmailServer"].ToString());

                txtoutemail.Text = Convert.ToString(dt.Rows[0]["OutEmailID"].ToString());

                txtport1.Text = Convert.ToString(dt.Rows[0]["InPort"].ToString());

                txtport2.Text = Convert.ToString(dt.Rows[0]["OutPort"].ToString());

                if (txtoutpass.TextMode == TextBoxMode.Password)
                {
                    txtoutpass.Text = Convert.ToString(dt.Rows[0]["OutPassword"].ToString());
                    txtoutpass.Attributes.Add("value", txtoutpass.Text);
                }
                txtserver.Text = Convert.ToString(dt.Rows[0]["IncomingEmailServer"].ToString());

                txtuser.Text = Convert.ToString(dt.Rows[0]["InEmailID"].ToString());


                if (txtpass.TextMode == TextBoxMode.Password)
                {
                    txtpass.Text = Convert.ToString(dt.Rows[0]["InPassword"].ToString());
                    txtpass.Attributes.Add("value", txtpass.Text);
                }
                bool sts = false;
                if (dt.Rows[0]["SetforOutgoingemail"] != System.DBNull.Value)
                {
                    sts = Convert.ToBoolean(dt.Rows[0]["SetforOutgoingemail"].ToString());
                }
                if (sts == true)
                {
                    CheckBox1.Checked = true;
                }
                dt = select("Select * from EmailSignatureMaster where  InoutgoingMasterId='" + Session["EmailID"] + "'");

                if (dt.Rows.Count > 0)
                {
                    CheckBox3.Checked = true;
                }
                imgupdate.Visible = true;
                imgbtnsubmitCabinetAdd.Visible = false;
                imgcancel.Visible = true;

            }
        }
        else if (e.CommandName == "Viewsignature")
        {          

             int datakey = Convert.ToInt32(e.CommandArgument);
             string strgrd = " SELECT dbo.InOutCompanyEmail_Panding.inoutid, dbo.InOutCompanyEmail_Panding.employeeid, dbo.InOutCompanyEmail_Panding.whid  FROM            dbo.InOutCompanyEmail INNER JOIN dbo.InOutCompanyEmail_Panding ON dbo.InOutCompanyEmail.ID = dbo.InOutCompanyEmail_Panding.inoutid where dbo.InOutCompanyEmail.ID='"+datakey+"' "; 
             SqlDataAdapter daff = new SqlDataAdapter(strgrd, con);
             DataTable dtg = new DataTable();
             daff.Fill(dtg);
            string strValuesemployeeid = Convert.ToString(dtg.Rows[0]["employeeid"]);
            string strValueswhid = Convert.ToString(dtg.Rows[0]["whid"]);            
                   
                    string    opup = " Update InOutCompanyEmail Set EmployeeID='"+strValuesemployeeid+"', Whid='"+strValueswhid+"' where ID='"+datakey+"' ";                   
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    SqlCommand cmde = new SqlCommand(opup, con);
                    cmde.ExecuteNonQuery();
                    con.Close();
                    
                    string result1 = "delete from InOutCompanyEmail_Panding where ID='" + datakey + "'";
                    SqlCommand cmddel = new SqlCommand(result1, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmddel.ExecuteNonQuery();
                    con.Close();

                    FillGrid();
           
             lblext.Text = "Record Approve SuccessFully ";
          


           
        }
    }
    protected void GridEmail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridEmail_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridEmail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void GridEmail_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridEmail_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        FillGrid();
    }
    protected void imgcancel_Click(object sender, EventArgs e)
    {
        imgbtnsubmitCabinetAdd.Enabled = true;
        btnadddd.Visible = true;
        pnladdd.Visible = false;
        Label24.Text = "";
        lblext.Text = "";

        imgupdate.Visible = false;
        imgbtnsubmitCabinetAdd.Visible = true;
        txtmemailname.Text = "";

        txtserver.Text = "";
        lblext.Text = "";
        txtuser.Text = "";
        txtpass.Attributes.Add("value", "");
        txtoutserver.Text = "";
        txtoutemail.Text = "";
        txtoutpass.Attributes.Add("value", "");
        CheckBox1.Checked = false;
        CheckBox2.Checked = false;
        CheckBox3.Checked = false;
        txtemail.Text = "";
        txtport1.Text = "";
        txtport2.Text = "";
        ddlemp.SelectedIndex = 0;
        RadioButtonList1.SelectedValue = "1";
        pnle.Visible = false;
    }
    protected void imgupdate_Click(object sender, EventArgs e)
    {
        Int32 id;
        id = Convert.ToInt32(Session["EmailID"].ToString());
        bool chk = false;
        if (CheckBox1.Checked == true)
        {
            chk = true;
        }
        bool scs_ftp = clsDocument.UpdateInOutCompanyEmailMaster(id, txtoutserver.Text, txtoutemail.Text, txtoutpass.Text, txtserver.Text, txtuser.Text, txtpass.Text, chk, ddlstore.SelectedValue, ddlemp.SelectedValue, txtemail.Text, txtport1.Text, txtport2.Text, txtmemailname.Text);

        if (Convert.ToInt32(scs_ftp) > 0)
        {
            string opup = "";
            if (CheckBox1.Checked == true)
            {
                if (CheckBox3.Checked == true)
                {
                    opup = "Update InOutCompanyEmail Set SetforOutgoingemail='0' where ID<>'" + id + "' and EmployeeID='" + ddlemp.SelectedValue + "'";
                }
                else
                {
                    opup = "Update InOutCompanyEmail Set SetforOutgoingemail='0' where ID<>'" + id + "' and (EmployeeID='0' or EmployeeID IS NULL) and Whid='" + ddlstore.SelectedValue + "'";

                }
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmde = new SqlCommand(opup, con);
                cmde.ExecuteNonQuery();
                con.Close();
            }
            //pnlmsg.Visible = true;
            //lblmsg.Visible = true;

            lblext.Text = "Record updated successfully.";
            imgupdate.Visible = false;
            imgbtnsubmitCabinetAdd.Visible = true;
            imgbtncancel.Visible = true;
            //imgcancel.Visible = false;
            txtserver.Text = "";
            txtuser.Text = "";
            txtemail.Text = "";
            txtmemailname.Text = "";
            ddlemp.SelectedIndex = 0;
            txtpass.Attributes.Add("value", "");
            txtoutserver.Text = "";
            txtoutemail.Text = "";
            txtoutpass.Attributes.Add("value", "");

            if (CheckBox3.Checked == true)
            {
                DataTable dt = new DataTable();
                dt = select("Select * from EmailSignatureMaster where  InoutgoingMasterId='" + Session["EmailID"] + "'");

                if (dt.Rows.Count > 0)
                {
                    string strValues = Convert.ToString(dt.Rows[0]["Signature"]);
                    string[] strArray = strValues.Split(':');

                    TextBox1.Text = strArray[0];

                    int rr = Convert.ToInt32(strArray.Length);

                    if (strValues.Contains(":") && rr <= 4)
                    {
                        TextBox2.Text = strArray[1];
                        TextBox3.Text = strArray[2];
                        TextBox4.Text = strArray[3];
                    }
                    else
                    {
                        TextBox2.Text = strArray[1];
                        TextBox3.Text = strArray[2];
                        TextBox4.Text = strArray[3];

                        TextBox5.Text = strArray[4];
                        TextBox6.Text = strArray[5];
                        TextBox7.Text = strArray[6];

                        TextBox8.Text = strArray[7];
                        TextBox9.Text = strArray[8];
                        TextBox10.Text = strArray[9];
                    }


                    // txtsignature.Text = Convert.ToString(dt.Rows[0]["Signature"]);
                    ImageButton4.Visible = false;
                    btnsigup.Visible = true;
                    lblmmms.Text = "";

                    ModalPopupExtender2.Show();
                }

            }
        }
        CheckBox1.Checked = false;
        CheckBox2.Checked = false;
        CheckBox3.Checked = false;
        FillGrid();
        btnadddd.Visible = true;
        pnladdd.Visible = false;
        Label24.Text = "";
    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox2.Checked == true)
        {
            txtoutserver.Text = txtserver.Text;
            txtoutemail.Text = txtuser.Text;
            //txtoutpass.Text = txtpass.Text;
            string strqa = txtpass.Text;
            txtpass.Attributes.Add("Value", strqa);
            txtoutpass.Attributes.Add("Value", strqa);
            txtport2.Text = txtport1.Text;
        }
        else if (CheckBox2.Checked == false)
        {
            txtoutserver.Text = "";
            txtoutemail.Text = "";
            string strqa = "";
            txtoutpass.Attributes.Add("Value", strqa);
            txtport2.Text = "";
        }
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        ModalPopupExtender9.Hide();
        Int32 patyid = 0;
        if (ViewState["Dltitem"] != null)
        {
            //DataTable dtorgeml = new DataTable();
            //dtorgeml = clsMaster.SelectPartyIdfromCompanyEmailId(Convert.ToInt32(ViewState["Dltitem"]));
            ////HttpContext.Current.Session["Company"] = "1";
            //if (dtorgeml.Rows.Count > 0)
            //{
            //    if (dtorgeml.Rows[0][0] != System.DBNull.Value)
            //    {
            //         patyid = Convert.ToInt32(dtorgeml.Rows[0][0].ToString());
            //    }
            //}
            //MessageCls clsMessage = new MessageCls();
            //bool dlt = clsMessage.DeleteCompanyMsgDetailExt(patyid);
            string result1 = "delete from InOutCompanyEmail where ID='" + ViewState["Dltitem"] + "'";
            SqlCommand cmddel = new SqlCommand(result1, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmddel.ExecuteNonQuery();
            con.Close();
            FillGrid();
        }

        ViewState["Dltitem"] = "";
    }

    protected void GridEmail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    LinkButton lk = (LinkButton)e.Row.FindControl("");
        //    lk.Attributes.Add("onclick","javascript"
        //}
    }

    protected void txtuser_TextChanged(object sender, EventArgs e)
    {
        DataTable dtminprt = new DataTable();
        dtminprt = clsDocument.SelectCompanyEmailexit(txtuser.Text, ddlstore.SelectedValue);
        if (dtminprt.Rows.Count > 0)
        {
            if (dtminprt.Rows[0]["OutEmailID"] != System.DBNull.Value)
            {
                string cabdoc = dtminprt.Rows[0][0].ToString();
                if (cabdoc == txtuser.Text)
                {
                    //Panel1.Visible = true;
                    lblext.Text = "This Email ID is already used.";
                    imgbtnsubmitCabinetAdd.Enabled = false;
                }
                else
                {
                    imgbtnsubmitCabinetAdd.Enabled = true;
                    lblext.Text = "";
                }
            }
        }
    }
    protected void ddlstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dsss = new DataTable();
        dsss = clsMessage.Empid(ddlstore.SelectedValue);
        if (dsss.Rows.Count > 0)
        {
            Session["EmployeeIdep"] = dsss.Rows[0]["EmployeeMasterId"].ToString();
        }
        else
        {
            Session["EmployeeIdep"] = "0";
        }
        RadioButtonList1_SelectedIndexChanged(sender, e);
        // FillGrid();
    }
    protected void txtemail_TextChanged(object sender, EventArgs e)
    {
        DataTable dtminprt = new DataTable();
        dtminprt = clsDocument.SelectCompanyEmailexitoriginal(txtuser.Text, ddlstore.SelectedValue, ddlemp.SelectedValue);
        if (dtminprt.Rows.Count > 0)
        {
            if (dtminprt.Rows[0]["EmailId"] != System.DBNull.Value)
            {
                string cabdoc = dtminprt.Rows[0][0].ToString();
                if (cabdoc == txtemail.Text)
                {
                    //Panel1.Visible = true;
                    lblext.Text = "This Email ID is already used.";
                    txtemail.Focus();
                }
                else
                {

                }
            }
        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue.ToString() == "1")
        {
            ddlemp.Items.Insert(0, "");
            ddlemp.SelectedItem.Value = "0";
            //  ddlemp.Items[0].Value = "0";
            pnle.Visible = false;
        }
        else
        {
            pnle.Visible = true;
            DataTable dt = new DataTable();
            ddlemp.Items.Clear();
            dt = select(" select EmployeeMaster.EmployeeMasterID,EmployeeMaster.EmployeeName + ' - '+ DesignationMaster.DesignationName as EmployeeName,dbo.EmployeeMaster.Active from EmployeeMaster inner join DesignationMaster on DesignationMaster.DesignationMasterId=EmployeeMaster.DesignationMasterId where EmployeeMaster.Whid='" + ddlstore.SelectedValue + "' and EmployeeMaster.EmployeeMasterID='" + Convert.ToInt32(Session["EmployeeId"]) + "' and EmployeeMaster.Active='1' order by EmployeeMaster.EmployeeName");
            ddlemp.DataSource = dt;

            ddlemp.DataTextField = "EmployeeName";
            ddlemp.DataValueField = "EmployeeMasterID";
            ddlemp.DataBind();
            ddlemp.Items.Insert(0, "-Select-");
            ddlemp.SelectedItem.Value = "0";
        }
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        //ModalPopupExtender2.Show();
        int i = clsDocument.InsertSignatureMaster(Session["EmailID"].ToString(), TextBox1.Text + ':' + TextBox2.Text + ':' + TextBox3.Text + ':' + TextBox4.Text + ':' + TextBox5.Text + ':' + TextBox6.Text + ':' + TextBox7.Text + ':' + TextBox8.Text + ':' + TextBox9.Text + ':' + TextBox10.Text);
        //  ImageButton4.Visible = false;
        lblmmms.Text = "Signature added successfully";
        TextBox1.Text = "";
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox7.Text = "";
        TextBox8.Text = "";
        TextBox9.Text = "";
        TextBox10.Text = "";

        //ModalPopupExtender2.Hide();

    }
    protected void ibtnCancelCabinetAdd_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender2.Hide();
    }
    protected void btnsigup_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Show();
        bool i = clsDocument.UpdateEmailSignatureMaster(Session["EmailID"].ToString(), TextBox1.Text + ':' + TextBox2.Text + ':' + TextBox3.Text + ':' + TextBox4.Text + ':' + TextBox5.Text + ':' + TextBox6.Text + ':' + TextBox7.Text + ':' + TextBox8.Text + ':' + TextBox9.Text + ':' + TextBox10.Text);
        lblmmms.Text = "Signature updated successfully";
        TextBox1.Text = "";
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox7.Text = "";
        TextBox8.Text = "";
        TextBox9.Text = "";
        TextBox10.Text = "";

    }
    protected void ddlbusinessfil_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlempfil.Items.Clear();
        DataTable dt = new DataTable();
        dt = select("select EmployeeMaster.EmployeeMasterID,EmployeeMaster.EmployeeName + ' - '+ DesignationMaster.DesignationName as EmployeeName,dbo.EmployeeMaster.Active from EmployeeMaster inner join DesignationMaster on DesignationMaster.DesignationMasterId=EmployeeMaster.DesignationMasterId where EmployeeMaster.Whid='" + ddlbusinessfil.SelectedValue + "' and EmployeeMaster.Active='1' order by EmployeeMaster.EmployeeName");
        ddlempfil.DataSource = dt;
        if (dt.Rows.Count > 0)
        {
            ddlempfil.DataTextField = "EmployeeName";
            ddlempfil.DataValueField = "EmployeeMasterID";
            ddlempfil.DataBind();
        }
        ddlempfil.Items.Insert(0, "All");
        ddlempfil.Items[0].Value = "0";
        lblBusiness.Text = ddlbusinessfil.SelectedItem.Text;
        FillGrid();
    }
    protected void ddlempfil_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            GridEmail.AllowPaging = false;
            GridEmail.PageSize = 1000;
            FillGrid();

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (GridEmail.Columns[8].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridEmail.Columns[8].Visible = false;
            }
            if (GridEmail.Columns[6].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridEmail.Columns[6].Visible = false;
            }
            if (GridEmail.Columns[7].Visible == true)
            {
                ViewState["delHide"] = "tt";
                GridEmail.Columns[7].Visible = false;
            }
        }
        else
        {
            GridEmail.AllowPaging = true;
            GridEmail.PageSize = 10;
            FillGrid();

            Button1.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridEmail.Columns[8].Visible = true;
            }
            if (ViewState["editHide"] != null)
            {
                GridEmail.Columns[6].Visible = true;
            }
            if (ViewState["delHide"] != null)
            {
                GridEmail.Columns[7].Visible = true;
            }
        }
    }

    protected void btnadddd_Click(object sender, EventArgs e)
    {
        btnadddd.Visible = false;
        pnladdd.Visible = true;
        Label24.Text = "Add New Company Email";
        lblext.Text = "";
        CheckBox3.Checked = true;
        txtport1.Text = "110";
        txtport2.Text = "25";
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
    //protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
    //{
    //    //ModalPopupExtender2.Show();
    //}
    protected void imgbtncancel_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Hide();
    }
    protected void GridEmail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridEmail.PageIndex = e.NewPageIndex;
        FillGrid();
    }

    protected void ControlenableTEstmail(bool t)
    {
        txtemail.Enabled = t;
        txtoutserver.Enabled = t;
        txtserver.Enabled = t;
        txtpass.Enabled = t;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        // ControlenableTEstmail(true);

        try
        {
            string AdminEmail = txtuser.Text;
            string incommingserver = txtserver.Text;
            String Password = txtpass.Text;
            string port1 = txtport1.Text;

            string body = "Please ignore this message.<br> This email was generated to test the email server account configuration by " + Request.Url.Host + ".<br> The test was successful.<br> Thank you for using " + Request.Url.Host + ".";

            MailAddress to = new MailAddress(AdminEmail);
            MailAddress from = new MailAddress(AdminEmail);
            MailMessage objEmail = new MailMessage(from, to);
            objEmail.Subject = "Test of mail server by " + Request.Url.Host + " account configuration ";
            objEmail.Body = body.ToString();
            objEmail.IsBodyHtml = true;

            objEmail.Priority = MailPriority.High;

            SmtpClient client = new SmtpClient();

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential(AdminEmail, Password);
            client.Host = incommingserver;
            //client.Port = Convert.ToInt32(port1);
            client.Send(objEmail);

            lblext.Text = "Test Connection Successful";
            lblext.Visible = true;
            txtpass.Attributes.Add("Value", Password);
        }
        catch (Exception ert)
        {
            lblext.Text = "Error Connecting Mail Server :  " + ert.Message;
            lblext.Visible = true;
        }

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        //  ControlenableTEstmail(true);


        try
        {
            string AdminEmail = txtoutemail.Text;
            string outgoinmailserver = txtoutserver.Text;
            String Password = txtoutpass.Text;
            string port2 = txtport2.Text;

            string body = "Please ignore this message.<br> This email was generated to test the email server account configuration by " + Request.Url.Host + ".<br> The test was successful.<br> Thank you for using " + Request.Url.Host + ".";

            MailAddress to = new MailAddress(AdminEmail);
            MailAddress from = new MailAddress(AdminEmail);
            MailMessage objEmail = new MailMessage(from, to);

            objEmail.Subject = "Test of mail server by " + Request.Url.Host + " account configuration ";
            objEmail.Body = body.ToString();
            objEmail.IsBodyHtml = true;

            objEmail.Priority = MailPriority.High;

            SmtpClient client = new SmtpClient();

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential(AdminEmail, Password);
            client.Host = outgoinmailserver;
            //client.Port = Convert.ToInt32(port2);
            client.Send(objEmail);

            lblext.Text = "Test Connection Successful";
            lblext.Visible = true;
            txtoutpass.Attributes.Add("Value", Password);
        }
        catch (Exception ert)
        {
            lblext.Text = "Error Connecting Mail Server :  " + ert.Message;
            lblext.Visible = true;
        }

    }
}
