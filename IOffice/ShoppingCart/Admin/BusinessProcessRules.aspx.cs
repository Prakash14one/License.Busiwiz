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
public partial class Account_BusinessProcessRules : System.Web.UI.Page
{
    SqlConnection con;
    MasterCls clsMaster = new MasterCls();
    DataTable dt = new DataTable();
    InstructionCls clsInstruction = new InstructionCls();
    DocumentCls1 clsDocument = new DocumentCls1();
    EmployeeCls clsEmployee = new EmployeeCls();
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
			Session["PageUrl"]=strData;
            Session["PageName"] = page;
            Page.Title = pg.getPageTitle(page);

           
            if (!Page.IsPostBack)
            {
                Pagecontrol.dypcontrol(Page, page);

                ddlcondition1_SelectedIndexChanged(sender, e);
                
                string str = "SELECT Distinct WareHouseId,Name  FROM WareHouseMaster inner join EmployeeWarehouseRights on EmployeeWarehouseRights.Whid=WareHouseMaster.WareHouseId where comid = '" + Session["comid"] + "'and WareHouseMaster.Status='" + 1 + "' and EmployeeWarehouseRights.AccessAllowed='True' order by name";

                SqlCommand cmd1 = new SqlCommand(str, con);
                cmd1.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlbusiness.DataSource = dt;
                ddlbusiness.DataTextField = "Name";
                ddlbusiness.DataValueField = "WareHouseId";
                ddlbusiness.DataBind();

                ddlbuemp.DataSource = dt;
                ddlbuemp.DataTextField = "Name";
                ddlbuemp.DataValueField = "WareHouseId";
                ddlbuemp.DataBind();
                ddlbusparty.DataSource = dt;
                ddlbusparty.DataTextField = "Name";
                ddlbusparty.DataValueField = "WareHouseId";
                ddlbusparty.DataBind();

                ddlbusbyfilter.DataSource = dt;
                ddlbusbyfilter.DataTextField = "Name";
                ddlbusbyfilter.DataValueField = "WareHouseId";
                ddlbusbyfilter.DataBind();
                ddlbusbyfilter.Items.Insert(0, "All");
                ddlbusbyfilter.Items[0].Value = "0";


                string eeed = " Select distinct EmployeeMaster.Whid from  EmployeeMaster where EmployeeMasterId='" + Session["EmployeeId"] + "'";
                SqlCommand cmdeeed = new SqlCommand(eeed, con);
                SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
                DataTable dteeed = new DataTable();
                adpeeed.Fill(dteeed);
                if (dteeed.Rows.Count > 0)
                {
                    ddlbusiness.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
                    ddlbuemp.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
                    ddlbusparty.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);

                }
                Fillddldocmaintype();
                
                FillDesignation();
                FillRuleType();
                FillDocumentType();
                FillRuleApproveType();
                FillUser();
                lblcomname.Text =  ddlbusbyfilter.SelectedItem.Text;
                txtruledate.Text = DateTime.Now.ToShortDateString();
                hdnRuleMaster.Value = "0";
                txt1.Text = "1";
                lblStepNo.Text = "1";

                ViewState["sortOrder"] = "";
                lblcomid.Text=Session["comid"].ToString();
                lblcomname.Text = "All";
                FillRuleGrid("", "");
            }
           
    }
    protected void FillUser()
    {
        if (ddldeg1.SelectedIndex >= 0)
        {
            dt = new DataTable();
            dlstusr1.Items.Clear();
            dt = clsMaster.selectEmployeewithDesignation(Convert.ToInt32(ddldeg1.SelectedItem.Value.ToString()),ddlbuemp.SelectedValue);
            dlstusr1.DataSource = dt;
            dlstusr1.DataBind();
           
                dlstusr1.Items.Insert(0, "- Any - ");
                dlstusr1.Items[0].Value = "0";
                //dlstusr1.Items.Insert(1, "- All - ");
                //dlstusr1.Items[1].Value = "All";
           
        }
    }
    protected void ClearField()
    {
        ddlapprovetype1.SelectedIndex = 0;
        ddlaprvtype.SelectedIndex = 0;
        ddlpartytype.SelectedIndex = 0;
        //ddlpartyname.SelectedIndex = 0;
        ddldeg1.SelectedIndex = 0;
        //dlstusr1.SelectedIndex = 0;
        hdnRuleMaster.Value = "0";
        txt1.Text = "1";
        TextBox1.Text = "1";
        ibtnstep1.Text = "Add Employee";
        
    }
    protected void ibtnstep1_Click(object sender, EventArgs e)
    {
        int flag = 0;
        DataTable dt = new DataTable();
        if (gridRuleDetail.Rows.Count <= 0)
        {
            Session["GridRule"] = null;
        }
        else
        {
            dt = (DataTable)Session["GridRule"];
        }
        foreach (DataRow df in dt.Rows)
        {
            if ((df["Whid"].ToString() == ddlbuemp.SelectedValue.ToString()) && (df["DesId"].ToString() == ddldeg1.SelectedItem.Value.ToString()) && (df["EmpId"].ToString() == dlstusr1.SelectedItem.Value.ToString()) && (df["ApprovedTypeId"].ToString() == ddlapprovetype1.SelectedItem.Value.ToString()))
            {
                flag = 2;
            }
            else
            {
                flag = 0;
            }
           

           
            
        }
        if(flag!=2)
        {
        int i; 
        string RuleD;
       
        if (hdnRuleDetail.Value == "1")
        {
            if (Session["GridRule"] == null)
            {
                DataColumn dtcom1 = new DataColumn();
                dtcom1.DataType = System.Type.GetType("System.String");
                dtcom1.ColumnName = "RuleDetail";
                dtcom1.ReadOnly = false;
                dtcom1.Unique = false;
                dtcom1.AllowDBNull = true;

                dt.Columns.Add(dtcom1);
                DataColumn dtcom2 = new DataColumn();
                dtcom2.DataType = System.Type.GetType("System.String");
                dtcom2.ColumnName = "StepNo";
                dtcom2.ReadOnly = false;
                dtcom2.Unique = false;
                dtcom2.AllowDBNull = true;

                dt.Columns.Add(dtcom2);

                DataColumn dtcom3 = new DataColumn();
                dtcom3.DataType = System.Type.GetType("System.String");
                dtcom3.ColumnName = "DesId";
                dtcom3.ReadOnly = false;
                dtcom3.Unique = false;
                dtcom3.AllowDBNull = true;
                dt.Columns.Add(dtcom3);

                DataColumn dtcom4 = new DataColumn();
                dtcom4.DataType = System.Type.GetType("System.String");
                dtcom4.ColumnName = "EmpId";
                dtcom4.ReadOnly = false;
                dtcom4.Unique = false;
                dtcom4.AllowDBNull = true;

                dt.Columns.Add(dtcom4);

                DataColumn dtcom5 = new DataColumn();
                dtcom5.DataType = System.Type.GetType("System.String");
                dtcom5.ColumnName = "ApprovedTypeId";
                dtcom5.ReadOnly = false;
                dtcom5.Unique = false;
                dtcom5.AllowDBNull = true;

                dt.Columns.Add(dtcom5);

                 
                DataColumn dtcom6 = new DataColumn();
                dtcom6.DataType = System.Type.GetType("System.String");
                dtcom6.ColumnName = "Days";
                dtcom6.ReadOnly = false;
                dtcom6.Unique = false;
                dtcom6.AllowDBNull = true;
                dt.Columns.Add(dtcom6);

                DataColumn dtcom7 = new DataColumn();
                dtcom7.DataType = System.Type.GetType("System.String");
                dtcom7.ColumnName = "BusinessName";
                dtcom7.ReadOnly = false;
                dtcom7.Unique = false;
                dtcom7.AllowDBNull = true;
                dt.Columns.Add(dtcom7);

                DataColumn dtcom8 = new DataColumn();
                dtcom8.DataType = System.Type.GetType("System.String");
                dtcom8.ColumnName = "Whid";
                dtcom8.ReadOnly = false;
                dtcom8.Unique = false;
                dtcom8.AllowDBNull = true;
                dt.Columns.Add(dtcom8); 
            }
            else
            {
                dt = (DataTable)Session["GridRule"];
            }
            dt.Rows.Remove(dt.Rows[gridRuleDetail.SelectedIndex]);
            DataRow dtrow = dt.NewRow();         
           
            if (ddlapprovetype1.SelectedIndex > 0)
            {
                RuleD = "This Document is to be approved by<b> " + ddldeg1.SelectedItem.Text + "  " + dlstusr1.SelectedItem.Text.ToString() + " </b> within <b> " + txt1.Text + " </b>Days.";
            }
            RuleD = "This Document is to be approved by <b>" + ddldeg1.SelectedItem.Text + "  " + dlstusr1.SelectedItem.Text.ToString() + " </b> for <b>" + ddlapprovetype1.SelectedItem.Text.ToString() + " </b> within <b>" + txt1.Text + " </b>Days.";
            dtrow["RuleDetail"] = RuleD;
            dtrow["Whid"] = ddlbuemp.SelectedValue;
            dtrow["BusinessName"] = ddlbuemp.SelectedItem.Text;
            dtrow["StepNo"] = gridRuleDetail.Rows.Count + 1;
            dtrow["DesId"] = ddldeg1.SelectedItem.Value.ToString();
            dtrow["EmpId"] = dlstusr1.SelectedItem.Value.ToString();
            dtrow["ApprovedTypeId"] = ddlapprovetype1.SelectedItem.Value.ToString();
            dtrow["Days"] = txt1.Text;
            dt.Rows.InsertAt(dtrow, Convert.ToInt32(gridRuleDetail.SelectedIndex));
            i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                dr["StepNo"] = i.ToString();
                i = i + 1;
            }
           hdnStepNo.Value =  Convert.ToString( i  ).ToString();
           lblStepNo.Text = hdnStepNo.Value;
           Session["GridRule"] = dt;
           gridRuleDetail.DataSource = dt;
           gridRuleDetail.DataBind() ;
           ClearField();
        }
        else
        {
            if (Session["GridRule"] == null)
            {
                DataColumn dtcom1 = new DataColumn();
                dtcom1.DataType = System.Type.GetType("System.String");
                dtcom1.ColumnName = "RuleDetail";
                dtcom1.ReadOnly = false;
                dtcom1.Unique = false;
                dtcom1.AllowDBNull = true;

                dt.Columns.Add(dtcom1);
                DataColumn dtcom2 = new DataColumn();
                dtcom2.DataType = System.Type.GetType("System.String");
                dtcom2.ColumnName = "StepNo";
                dtcom2.ReadOnly = false;
                dtcom2.Unique = false;
                dtcom2.AllowDBNull = true;

                dt.Columns.Add(dtcom2);

                DataColumn dtcom3 = new DataColumn();
                dtcom3.DataType = System.Type.GetType("System.String");
                dtcom3.ColumnName = "DesId";
                dtcom3.ReadOnly = false;
                dtcom3.Unique = false;
                dtcom3.AllowDBNull = true;
                dt.Columns.Add(dtcom3);

                DataColumn dtcom4 = new DataColumn();
                dtcom4.DataType = System.Type.GetType("System.String");
                dtcom4.ColumnName = "EmpId";
                dtcom4.ReadOnly = false;
                dtcom4.Unique = false;
                dtcom4.AllowDBNull = true;

                dt.Columns.Add(dtcom4);

                DataColumn dtcom5 = new DataColumn();
                dtcom5.DataType = System.Type.GetType("System.String");
                dtcom5.ColumnName = "ApprovedTypeId";
                dtcom5.ReadOnly = false;
                dtcom5.Unique = false;
                dtcom5.AllowDBNull = true;

                dt.Columns.Add(dtcom5);


                DataColumn dtcom6 = new DataColumn();
                dtcom6.DataType = System.Type.GetType("System.String");
                dtcom6.ColumnName = "Days";
                dtcom6.ReadOnly = false;
                dtcom6.Unique = false;
                dtcom6.AllowDBNull = true;

                dt.Columns.Add(dtcom6);
                DataColumn dtcom7 = new DataColumn();
                dtcom7.DataType = System.Type.GetType("System.String");
                dtcom7.ColumnName = "BusinessName";
                dtcom7.ReadOnly = false;
                dtcom7.Unique = false;
                dtcom7.AllowDBNull = true;
                dt.Columns.Add(dtcom7);

                DataColumn dtcom8 = new DataColumn();
                dtcom8.DataType = System.Type.GetType("System.String");
                dtcom8.ColumnName = "Whid";
                dtcom8.ReadOnly = false;
                dtcom8.Unique = false;
                dtcom8.AllowDBNull = true;
                dt.Columns.Add(dtcom8); 
            }
            else
            {
                dt = (DataTable)Session["GridRule"];
            }
           
            DataRow dtrow = dt.NewRow();
            if (ddlapprovetype1.SelectedIndex > 0)
            {
                RuleD = "This Document is to be approved by<b> " + ddldeg1.SelectedItem.Text + "  " + dlstusr1.SelectedItem.Text.ToString() + " </b> within <b> " + txt1.Text + " </b>Days.";
            }
            RuleD = "This Document is to be approved by <b>" + ddldeg1.SelectedItem.Text + "  " + dlstusr1.SelectedItem.Text.ToString() + " </b> for <b>" + ddlapprovetype1.SelectedItem.Text.ToString() + " </b> within <b>" + txt1.Text + " </b>Days.";
            dtrow["RuleDetail"] = RuleD;
            dtrow["Whid"] = ddlbuemp.SelectedValue;
            dtrow["BusinessName"] = ddlbuemp.SelectedItem.Text;

            dtrow["StepNo"] = gridRuleDetail.Rows.Count + 1;  
            dtrow["DesId"] = ddldeg1.SelectedItem.Value.ToString() ;
            dtrow["EmpId"] = dlstusr1.SelectedItem.Value.ToString() ;
            dtrow["ApprovedTypeId"] = ddlapprovetype1.SelectedItem.Value.ToString(); 
            dtrow["Days"] = txt1.Text  ;             
            dt.Rows.Add(dtrow);
            i = 1;
            foreach (DataRow dr in dt.Rows)
            {

                dr["StepNo"] = i.ToString();
                i = i + 1;
            }
           hdnStepNo.Value   = Convert.ToString(i  ).ToString();
           lblStepNo.Text = hdnStepNo.Value;
            Session["GridRule"] = dt;
            gridRuleDetail.DataSource = dt;
            gridRuleDetail.DataBind();
            ClearField();
        }
        }
        else
        {
            lblmsg.Text = "Record Already Exists";
            lblmsg.Visible=true;
            
            ddlbuemp.Focus();
        }
    }     
    protected void FillDesignation()
    {
        DataTable dt;
        dt = new DataTable();
        clsMaster = new MasterCls();
        dt = clsMaster.SelectDesignationMasterwithDeptName(ddlbuemp.SelectedValue);
        ddldeg1.DataSource = dt;
        ddldeg1.DataBind();
        EventArgs e = new EventArgs();
        object sender = new object();
        ddldeg1_SelectedIndexChanged(sender, e);
         
    }
    protected void ddldeg1_SelectedIndexChanged(object sender, EventArgs e)
    {
        dt = new DataTable();
        dlstusr1.Items.Clear();
        dt = clsMaster.selectEmployeewithDesignation(Convert.ToInt32(ddldeg1.SelectedItem.Value.ToString()),ddlbuemp.SelectedValue);
        dlstusr1.DataSource = dt;
        dlstusr1.DataBind() ;
        dlstusr1.Items.Insert(0, "- Any - ");
            dlstusr1.Items[0].Value = "0";
            //dlstusr1.Items.Insert(1, "- All - ");
            //dlstusr1.Items[1].Value = "All";
           
    }              
    protected void FillRuleType()
    {
        dt = new DataTable();
        dt = clsInstruction.SelectRuleTypeMaster(ddlbusiness.SelectedValue);
        ddlruletype.DataSource = dt;
        ddlruletype.DataBind();
        //ddlruletype.Items.Insert(0, "- All - ");
        //ddlruletype.SelectedItem.Value = "0";
        if (dt.Rows.Count > 0)
        {
            ddlruletype.SelectedIndex = 0;
        }

       
    }
    protected void FillDocumentType()
    {
        //dt = new DataTable();
        //dt = clsDocument.SelectDocTypeAll(ddlbusiness.SelectedValue);
        //ddltypeofdoc.DataTextField = "doctype";
        //ddltypeofdoc.DataValueField = "DocumentTypeId";
        //ddltypeofdoc.DataSource  = dt;
        //ddltypeofdoc.DataBind();
        //ddltypeofdoc.Items.Insert(0, "- All - ");
        //ddltypeofdoc.SelectedItem.Value = "0";
    }
    protected void FillPublicDocumentType()
    {
        //dt = new DataTable();
        //dt = clsDocument.SelectPublicDocTypeAll(ddlbusiness.SelectedValue);
        //ddltypeofdoc.DataTextField = "doctype";
        //ddltypeofdoc.DataValueField = "DocumentTypeId";
        //ddltypeofdoc.DataSource = dt;
        //ddltypeofdoc.DataBind();
        //ddltypeofdoc.Items.Insert(0, "- All - ");
        //ddltypeofdoc.SelectedItem.Value = "0";
    }
    protected void FillRuleApproveType()
    {
        dt = new DataTable();
        if (RadioButtonList1.SelectedIndex == 0)
        {
            dt = clsInstruction.SelectRuleApproveTypeMaster(ddlbuemp.SelectedValue);
            ddlapprovetype1.DataTextField = "RuleApproveTypeName";

            ddlapprovetype1.DataValueField = "RuleApproveTypeId";
            ddlapprovetype1.DataSource = dt;
            ddlapprovetype1.DataBind();
            ddlapprovetype1.Items.Insert(0, "- Select - ");
            ddlapprovetype1.SelectedItem.Value = "0";
        }
        else if (RadioButtonList1.SelectedIndex == 1)
        {
            dt = clsInstruction.SelectRuleApproveTypeMaster(ddlbusparty.SelectedValue);
            ddlaprvtype.DataTextField = "RuleApproveTypeName";

            ddlaprvtype.DataValueField = "RuleApproveTypeId";
            ddlaprvtype.DataSource = dt;
            ddlaprvtype.DataBind();
            ddlaprvtype.Items.Insert(0, "- Select - ");
            ddlaprvtype.SelectedItem.Value = "0";
        }
    }
    protected void ibtnadd1_Click(object sender, EventArgs e)
    {
        Int32 condn;
        if (Session["GridRule"] == null)
        {
            
            lblmsg.Text = "You have to select atleast one Step.";
            return;
        }
        ibtnstep1.Text = "Add Employee";
        if (txt1.Text.Trim().Length <= 0)
        { 
            txt1.Text = "1";
        }
        string str = "SELECT * FROM RuleMaster where RuleTitle = '" + txtrulename.Text + "' and RuleTypeId='" + ddlruletype.SelectedValue + "' and DocumentMainId='" + ddlcabinet.SelectedValue + "' and DocumentSubId='" + ddldrower.SelectedValue + "' and DocumentTypeId='"+ddltypeofdoc.SelectedValue+"' and Whid='" + ddlbusiness.SelectedValue + "' ";

          SqlCommand cmd1 = new SqlCommand(str, con);
          cmd1.CommandType = CommandType.Text;
          SqlDataAdapter da = new SqlDataAdapter(cmd1);
          DataTable dt = new DataTable();
          da.Fill(dt);
          if (dt.Rows.Count == 0)
          {

              condn = Convert.ToInt32(ddlcondition1.SelectedItem.Value.ToString());
              Int32 scs = clsInstruction.InsertRuleMaster(Convert.ToInt32(ddlruletype.SelectedItem.Value.ToString()), Convert.ToInt32(ddltypeofdoc.SelectedItem.Value.ToString()), Convert.ToDateTime(txtruledate.Text), txtrulename.Text, condn, ddlcabinet.SelectedValue, ddldrower.SelectedValue, ddlbusiness.SelectedValue,Convert.ToBoolean(chkcheckapp.Checked),Convert.ToInt32(ddlstatus.SelectedValue));
              if (scs > 0)
              {
                  hdnRuleMaster.Value = scs.ToString();
                  if (Session["GridRule"] != null)
                  {
                      dt = new DataTable();
                      dt = (DataTable)Session["GridRule"];
                      foreach (DataRow dr in dt.Rows)
                      {
                          Int32 Empid;
                          bool insdetail;
                          insdetail = false;
                          Int32 AprvRuleType;
                          string Days = null;
                          AprvRuleType = Convert.ToInt32(dr["ApprovedTypeId"].ToString());
                          Days = dr["Days"].ToString();
                          Int32 step = Convert.ToInt32(dr["StepNo"].ToString());
                          bool empsel = false;
                          if (dr["EmpId"].ToString() == "0" | dr["EmpId"].ToString() == "All")
                          {
                              if (empsel == false)
                              {
                                  if (dr["EmpId"].ToString() == "0")
                                  {
                                      bool sc = clsInstruction.InsertRuleEmpSelectionMaster(Convert.ToInt32(hdnRuleMaster.Value), step, 1);
                                      empsel = true;
                                  }
                                  else
                                  {
                                      if (dr["EmpId"].ToString() == "All")
                                      {
                                          bool sc = clsInstruction.InsertRuleEmpSelectionMaster(Convert.ToInt32(hdnRuleMaster.Value), step, 2);
                                          empsel = true;
                                      }
                                  }
                              }
                              DataTable DtEmp = new DataTable();
                              Int32 desid = Convert.ToInt32(dr["DesId"].ToString());
                              DtEmp = clsMaster.selectEmployeewithDesignation(desid, dr["Whid"].ToString());
                              foreach (DataRow drdes in DtEmp.Rows)
                              {
                                  Empid = Convert.ToInt32(drdes["EmployeeId"].ToString());
                                  insdetail = clsInstruction.InsertRuleDetail(Convert.ToInt32(hdnRuleMaster.Value), Empid, AprvRuleType, step, Days);
                              }
                          }
                          else
                          {
                              Empid = Convert.ToInt32(dr["EmpId"].ToString());
                              insdetail = clsInstruction.InsertRuleDetail(Convert.ToInt32(hdnRuleMaster.Value), Empid, AprvRuleType, step, Days);
                          }
                      }
                  }
                  else
                  {
                  }
              }

              Session["GridRule"] = null;
              hdnRuleDetail.Value = "0";
              hdnStepNo.Value = "1";
              lblStepNo.Text = hdnStepNo.Value;
              ClearField();
              ClearAll();
              
              lblmsg.Text = "Record inserted successfully";
              FillRuleGrid("", "");

              Response.Redirect("BusinessProcessRules.aspx");
          }
          else
          {
              
              lblmsg.Text = "Record Already Exist";
          }
    }    
    protected void ibtnReset_Click(object sender, EventArgs e)
    {
       
        ClearAll();

    }
    protected void ClearAll()
    {
        Session["GridRule"] = null;
        hdnRuleDetail.Value = "0";
        hdnStepNo.Value = "1";
        lblStepNo.Text = hdnStepNo.Value;
        ibtnstep1.Text = "Add Employee";
        hdnRuleMaster.Value = "0";
        gridRuleDetail.DataSource = null;
        gridRuleDetail.DataBind();
        pnlstep1.Enabled = true;
        lbllegend.Text = "";
        btnadd.Visible = true;
        pnlsh.Visible = false;
        ddlruletype.SelectedIndex = 0;
        ddltypeofdoc.SelectedIndex = 0;
        txtrulename.Text = "";
        if (ddldeg1.Items.Count > 0)
        {
            ddldeg1.SelectedIndex = 0;
        }
        txt1.Text = "1";
        ibtnstep1.Visible = true;
      
    }
    protected void gridRuleDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      //  gridRuleDetail.SelectedIndex = Convert.ToInt32(e.CommandArgument);
       
        //Int32 indx = Convert.ToInt32(gridRuleDetail.SelectedIndex.ToString());
       
        //if (Convert.ToInt32(gridRuleDetail.SelectedIndex) >= 0)
        //{
            

        //    if (e.CommandName == "Edit1")
        //    {
        //        hdnRuleDetail.Value = "1";

        //        DataTable dtt = new DataTable();
        //        dtt =    (DataTable)Session["GridRule"];
              
        //        ddlbuemp.SelectedIndex = ddlbuemp.Items.IndexOf(ddlbuemp.Items.FindByValue(dtt.Rows[indx]["Whid"].ToString()));
             
        //        ddldeg1.SelectedIndex = ddldeg1.Items.IndexOf(ddldeg1.Items.FindByValue(dtt.Rows[indx]["DesId"].ToString()));
        //        dlstusr1.SelectedIndex = dlstusr1.Items.IndexOf(dlstusr1.Items.FindByValue(dtt.Rows[indx]["EmpId"].ToString()));
        //        FillRuleApproveType();
        //        ddlapprovetype1.SelectedIndex = ddlapprovetype1.Items.IndexOf(ddlapprovetype1.Items.FindByValue(dtt.Rows[indx]["ApprovedTypeId"].ToString()));
        //        txt1.Text = dtt.Rows[indx]["Days"].ToString();
        //        hdnStepNo.Value =  dtt.Rows[indx]["StepNo"].ToString() ;
        //        lblStepNo.Text = hdnStepNo.Value;
        //        ibtnstep1.Text = "Update";
        //    }
        //    else
        //    {
                
        //    }
        //}

        if (e.CommandName == "Delete1")
        {
            gridRuleDetail.SelectedIndex = Convert.ToInt32(e.CommandArgument);

            DataTable dt = new DataTable();
            if (Session["GridRule"] != null)
            {
                dt = (DataTable)Session["GridRule"];

                dt.Rows.Remove(dt.Rows[gridRuleDetail.SelectedIndex]);
                int i;
                i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    dr["StepNo"] = i.ToString();
                    i = i + 1;
                }
                hdnStepNo.Value = Convert.ToString(i).ToString();
                lblStepNo.Text = hdnStepNo.Value;
                gridRuleDetail.DataSource = dt;
                gridRuleDetail.DataBind();
                Session["GridRule"] = dt;
                ibtnstep1.Text = "Add Employee";
            }

        }
    }
    protected void grid_Rule_master_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "detail")
        {
            grid_Rule_master.Columns[13].Visible = true;
            foreach (GridViewRow gdr in grid_Rule_master.Rows)
            {
                GridView gr = (GridView)gdr.Cells[6].FindControl("grdRuleDetail");
                gr.DataSource = null;
                gr.DataBind();
            }
            grid_Rule_master.SelectedIndex = Convert.ToInt32(e.CommandArgument);
           int RuleId = Convert.ToInt32(grid_Rule_master.SelectedDataKey.Value);
            //int RuleId1 = Convert.ToInt32(e.CommandArgument);
            DataTable dt = new DataTable();
            dt = clsInstruction.SelectRuleDetail(RuleId);
            
              
            GridView gridDetail = (GridView)grid_Rule_master.Rows[grid_Rule_master.SelectedIndex].Cells[6].FindControl("grdRuleDetail");
            gridDetail.DataSource = dt;
            gridDetail.DataBind();
        }
        if (e.CommandName == "Delete")
        {
            grid_Rule_master.SelectedIndex = Convert.ToInt32(e.CommandArgument);

           // int RuleId1 = Convert.ToInt32(grid_Rule_master.SelectedDataKey.Value);
            int RuleId1 = Convert.ToInt32(e.CommandArgument);
            ViewState["delId"] = RuleId1.ToString();
            ViewState["hdnt"] = "Employee";
            //mdlpopupconfirm.Show();
            imgconfirmok_Click(sender, e);
        }
    }
    protected void grid_Rule_master_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid_Rule_master.PageIndex = e.NewPageIndex;
        FillRuleGrid("","");

    }
    protected void Fillddldocmaintype()
    {
        string str132 = "";
        if (RadioButtonList1.SelectedItem.Text == "Party")
        {
            str132 = " SELECT [DocumentMainTypeId],DocumentMainType  FROM  [dbo].[DocumentMainType] where Whid='" + ddlbusiness.SelectedValue + "' and  DocumentMainType in('General','Management')";
        }
        else
        {
            str132 = " SELECT [DocumentMainTypeId],DocumentMainType  FROM  [dbo].[DocumentMainType] where Whid='" + ddlbusiness.SelectedValue + "'";

        }
        //string str132 = " SELECT [DocumentMainTypeId],upper(DocumentMainType) as DocumentMainType FROM  [dbo].[DocumentMainType] where CID='" + Session["Comid"] + "'";
        SqlCommand cgw = new SqlCommand(str132, con);
        SqlDataAdapter adgw = new SqlDataAdapter(cgw);

        DataTable dt = new DataTable();

        adgw.Fill(dt);

        ddlcabinet.DataSource = dt;
        ddlcabinet.DataBind();
        EventArgs e = new EventArgs();
        object sender = new object();
        ddlcabinet_SelectedIndexChanged(sender, e);
        //ddldrower.Items.Insert(0, "All");

        //ddldrower.SelectedItem.Value = "0";
        //ddltypeofdoc.Items.Insert(0, "All");

        //ddltypeofdoc.SelectedItem.Value = "0";
    }
    protected void FillRuleGrid(string sortExp, string sortDir)
    {
        dt = new DataTable();
        dt = clsInstruction.SelectRuleMaster(ddlbusbyfilter.SelectedValue);
        DataView myDataView = new DataView();
        myDataView = dt.DefaultView  ;

        if (sortExp != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
        }
        grid_Rule_master.DataSource = dt;
        grid_Rule_master.DataBind();
    }
    protected void FillRuleGridParty(string sortExp, string sortDir)
    {
        dt = new DataTable();
        dt = clsInstruction.SelectRuleMasterforParty(ddlbusbyfilter.SelectedValue);
        DataView myDataView = new DataView();
        myDataView = dt.DefaultView;

        if (sortExp != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
        }
        grid_Rule_master_party.DataSource = dt;
        grid_Rule_master_party.DataBind();
      
    }
    protected void FillRuleGriDocTypeWise(string sortExp, string sortDir)
    {
        dt = new DataTable();
        dt = clsInstruction.SelectRuleMasterDocTypeWise(Convert.ToInt32(ddltypeofdoc.SelectedValue.ToString()));
        DataView myDataView = new DataView();
        myDataView = dt.DefaultView;

        if (sortExp != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
        }
        grid_Rule_master.DataSource = dt;
        grid_Rule_master.DataBind();
    }
    protected void FillRuleGriRuleTypeWise(string sortExp, string sortDir)
    {
        dt = new DataTable();
        dt = clsInstruction.SelectRuleMasterRuleTypeWise(Convert.ToInt32(ddlruletype.SelectedValue.ToString()),ddlbusbyfilter.SelectedValue); 
        DataView myDataView = new DataView();
        myDataView = dt.DefaultView;

        if (sortExp != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
        }
        grid_Rule_master.DataSource = dt;
        grid_Rule_master.DataBind();
    }
    protected void FillRuleGriDocTypeWiseRuleTypewise(string sortExp, string sortDir)
    {
        dt = new DataTable();
        dt = clsInstruction.SelectRuleMasterDocTypeWiseRuleTypeWise(Convert.ToInt32 ( ddltypeofdoc.SelectedValue.ToString()) ,Convert.ToInt32(ddlruletype.SelectedValue.ToString()));
        DataView myDataView = new DataView();
        myDataView = dt.DefaultView;

        if (sortExp != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
        }
        grid_Rule_master.DataSource = dt;
        grid_Rule_master.DataBind();
    }
    protected void grid_Rule_master_Sorting(object sender, GridViewSortEventArgs e)
    {
        SetFillGridSorting(e.SortExpression, sortOrder);
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
    protected void ddlruletype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedItem.Text == "Employee")
        {
           // SetFillGrid();
        }
        else if (RadioButtonList1.SelectedItem.Text == "Party")
        {

        }
    }
    protected void ddltypeofdoc_SelectedIndexChanged(object sender, EventArgs e)
    {
     // SetFillGrid() ;
    }
    protected void SetFillGrid()
    {
        if (ddlruletype.SelectedIndex > 0 & ddltypeofdoc.SelectedIndex > 0)
        {
            FillRuleGriDocTypeWiseRuleTypewise("", "");
        }
        else
        {
            if (ddlruletype.SelectedIndex == 0 & ddltypeofdoc.SelectedIndex == 0)
            {
                FillRuleGrid("", "");
            }
            else
            {
                if (ddltypeofdoc.SelectedIndex > 0 & ddlruletype.SelectedIndex == 0)
                {
                    FillRuleGriDocTypeWise("", "");
                }
                else
                {
                    FillRuleGriRuleTypeWise("", "");

                }
            }
        }
    }
    protected void SetFillGridSorting(string sortExp, string sortDir)
    {
        if (ddlruletype.SelectedIndex > 0 & ddltypeofdoc.SelectedIndex > 0)
        {
            FillRuleGriDocTypeWiseRuleTypewise(sortExp, sortDir);
        }
        else
        {
            if (ddlruletype.SelectedIndex == 0 & ddltypeofdoc.SelectedIndex == 0)
            {
                FillRuleGrid(sortExp, sortDir);
            }
            else
            {
                if (ddltypeofdoc.SelectedIndex > 0 & ddlruletype.SelectedIndex == 0)
                {
                    FillRuleGriDocTypeWise(sortExp, sortDir);
                }
                else
                {
                    FillRuleGriRuleTypeWise(sortExp, sortDir);

                }
            }
        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillddldocmaintype();
        if (RadioButtonList1.SelectedItem.Text == "Party")
        {
            trparty.Visible = true;
            tremp.Visible = false;
            tr16.Visible = false;
            FillPublicDocumentType();
            Fillddl_party_type();

            FillRuleApproveType();
            ddlpartyname.Items.Clear();
            //FillRuleGridParty("", "");
            //grid_Rule_master.DataSource = null;
            //grid_Rule_master.DataBind();
            //grid_Rule_master.Visible = false;
            //grid_Rule_master_party.Visible = true;
            ibtnadd1.Visible = false;
            Label2.Text = "1";
            ddlaprvtype_SelectedIndexChanged(sender, e);
        }
        else if (RadioButtonList1.SelectedItem.Text == "Employee")
        {
            trparty.Visible = false;
            tremp.Visible = true;
            tr16.Visible = true;
            FillDocumentType();
            //grid_Rule_master.Visible = true;
            //grid_Rule_master_party.Visible = false;
            //FillRuleGrid("", "");

            ddlapprovetype1_SelectedIndexChanged(sender, e);
            ibtnadd1.Visible = true;
        }
    }
    protected void Fillddl_party_type()
    {
        dt = clsMaster.SelectPartyTypeMasterwithoutEmployee();
        ddlpartytype.DataSource = dt;
        ddlpartytype.DataBind();
        ddlpartytype.Items.Insert(0, "-Select-");
        ddlpartytype.SelectedItem.Value = "0";
    }
    protected void ddlpartytype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlpartytype.SelectedIndex > 0)
        {
            FillPartyMasterwithpartytype(Int32.Parse(ddlpartytype.SelectedValue.ToString()));
        }
    }
    protected void FillPartyMasterwithpartytype(Int32 PartyTypeId)
    {
        DataTable dt;
        dt = new DataTable();
        clsMaster = new MasterCls();
        dt = clsMaster.SelectPartyNamebyPartyTypeId(PartyTypeId,ddlbusparty.SelectedValue);
        ddlpartyname.DataSource = dt;
        ddlpartyname.DataBind();
        ddlpartyname.Items.Insert(0, "- Any - ");
        ddlpartyname.Items[0].Value = "0";

    }
    protected void imgbtnsubmit_Click(object sender, EventArgs e)
    {
        Session["GridRule"] = null;
        int i;
        string RuleD;
        DataTable dt = new DataTable();
        if (hdnRuleDetail.Value == "1")
        {
            if (Session["GridRule"] == null)
            {
                DataColumn dtcom1 = new DataColumn();
                dtcom1.DataType = System.Type.GetType("System.String");
                dtcom1.ColumnName = "RuleDetail";
                dtcom1.ReadOnly = false;
                dtcom1.Unique = false;
                dtcom1.AllowDBNull = true;

                dt.Columns.Add(dtcom1);
                DataColumn dtcom2 = new DataColumn();
                dtcom2.DataType = System.Type.GetType("System.String");
                dtcom2.ColumnName = "StepNo";
                dtcom2.ReadOnly = false;
                dtcom2.Unique = false;
                dtcom2.AllowDBNull = true;

                dt.Columns.Add(dtcom2);

                DataColumn dtcom3 = new DataColumn();
                dtcom3.DataType = System.Type.GetType("System.String");
                dtcom3.ColumnName = "PartyTypeId";
                dtcom3.ReadOnly = false;
                dtcom3.Unique = false;
                dtcom3.AllowDBNull = true;
                dt.Columns.Add(dtcom3);

                DataColumn dtcom4 = new DataColumn();
                dtcom4.DataType = System.Type.GetType("System.String");
                dtcom4.ColumnName = "PartyId";
                dtcom4.ReadOnly = false;
                dtcom4.Unique = false;
                dtcom4.AllowDBNull = true;

                dt.Columns.Add(dtcom4);

                DataColumn dtcom5 = new DataColumn();
                dtcom5.DataType = System.Type.GetType("System.String");
                dtcom5.ColumnName = "ApprovedTypeId";
                dtcom5.ReadOnly = false;
                dtcom5.Unique = false;
                dtcom5.AllowDBNull = true;

                dt.Columns.Add(dtcom5);


                DataColumn dtcom6 = new DataColumn();
                dtcom6.DataType = System.Type.GetType("System.String");
                dtcom6.ColumnName = "Days";
                dtcom6.ReadOnly = false;
                dtcom6.Unique = false;
                dtcom6.AllowDBNull = true;
                dt.Columns.Add(dtcom6);
                DataColumn dtcom7 = new DataColumn();
                dtcom7.DataType = System.Type.GetType("System.String");
                dtcom7.ColumnName = "BusinessName";
                dtcom7.ReadOnly = false;
                dtcom7.Unique = false;
                dtcom7.AllowDBNull = true;
                dt.Columns.Add(dtcom7);

                DataColumn dtcom8 = new DataColumn();
                dtcom8.DataType = System.Type.GetType("System.String");
                dtcom8.ColumnName = "Whid";
                dtcom8.ReadOnly = false;
                dtcom8.Unique = false;
                dtcom8.AllowDBNull = true;
                dt.Columns.Add(dtcom8); 
            }
            else
            {
                dt = (DataTable)Session["GridRule"];
            }
            dt.Rows.Remove(dt.Rows[gridRuleDetail.SelectedIndex]);
            DataRow dtrow = dt.NewRow();

            if (ddlaprvtype.SelectedIndex > 0)
            {
                RuleD = "This Document is to be approved by<b> " + ddlpartytype.SelectedItem.Text + "  " + ddlpartyname.SelectedItem.Text.ToString() + " </b> within <b> " + TextBox1.Text + " </b>Days.";
            }
            RuleD = "This Document is to be approved by <b>" + ddlpartytype.SelectedItem.Text + "  " + ddlpartyname.SelectedItem.Text.ToString() + " </b> for <b>" + ddlaprvtype.SelectedItem.Text.ToString() + " </b> within <b>" + TextBox1.Text + " </b>Days.";
            dtrow["RuleDetail"] = RuleD;

            dtrow["Whid"] = ddlbusparty.SelectedValue;
            dtrow["BusinessName"] = ddlbusparty.SelectedItem.Text;
            dtrow["StepNo"] = gridRuleDetail.Rows.Count + 1;
            dtrow["PartyTypeId"] = ddlpartytype.SelectedItem.Value.ToString();
            dtrow["PartyId"] = ddlpartyname.SelectedItem.Value.ToString();
            dtrow["ApprovedTypeId"] = ddlaprvtype.SelectedItem.Value.ToString();
            dtrow["Days"] = TextBox1.Text;
            dt.Rows.InsertAt(dtrow, Convert.ToInt32(gridRuleDetail.SelectedIndex));
            i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                dr["StepNo"] = i.ToString();
                //i = i + 1;
            }
            hdnStepNo.Value = Convert.ToString(i).ToString();
            Label2.Text = hdnStepNo.Value;
            Session["GridRule"] = dt;
            gridRuleDetail.DataSource = dt;
            gridRuleDetail.DataBind();
            ClearField();
        }
        else
        {
            if (Session["GridRule"] == null)
            {
                DataColumn dtcom1 = new DataColumn();
                dtcom1.DataType = System.Type.GetType("System.String");
                dtcom1.ColumnName = "RuleDetail";
                dtcom1.ReadOnly = false;
                dtcom1.Unique = false;
                dtcom1.AllowDBNull = true;

                dt.Columns.Add(dtcom1);
                DataColumn dtcom2 = new DataColumn();
                dtcom2.DataType = System.Type.GetType("System.String");
                dtcom2.ColumnName = "StepNo";
                dtcom2.ReadOnly = false;
                dtcom2.Unique = false;
                dtcom2.AllowDBNull = true;

                dt.Columns.Add(dtcom2);

                DataColumn dtcom3 = new DataColumn();
                dtcom3.DataType = System.Type.GetType("System.String");
                dtcom3.ColumnName = "PartyTypeId";
                dtcom3.ReadOnly = false;
                dtcom3.Unique = false;
                dtcom3.AllowDBNull = true;
                dt.Columns.Add(dtcom3);

                DataColumn dtcom4 = new DataColumn();
                dtcom4.DataType = System.Type.GetType("System.String");
                dtcom4.ColumnName = "PartyId";
                dtcom4.ReadOnly = false;
                dtcom4.Unique = false;
                dtcom4.AllowDBNull = true;

                dt.Columns.Add(dtcom4);

                DataColumn dtcom5 = new DataColumn();
                dtcom5.DataType = System.Type.GetType("System.String");
                dtcom5.ColumnName = "ApprovedTypeId";
                dtcom5.ReadOnly = false;
                dtcom5.Unique = false;
                dtcom5.AllowDBNull = true;

                dt.Columns.Add(dtcom5);


                DataColumn dtcom6 = new DataColumn();
                dtcom6.DataType = System.Type.GetType("System.String");
                dtcom6.ColumnName = "Days";
                dtcom6.ReadOnly = false;
                dtcom6.Unique = false;
                dtcom6.AllowDBNull = true;

                dt.Columns.Add(dtcom6);

                DataColumn dtcom7 = new DataColumn();
                dtcom7.DataType = System.Type.GetType("System.String");
                dtcom7.ColumnName = "BusinessName";
                dtcom7.ReadOnly = false;
                dtcom7.Unique = false;
                dtcom7.AllowDBNull = true;
                dt.Columns.Add(dtcom7);

                DataColumn dtcom8 = new DataColumn();
                dtcom8.DataType = System.Type.GetType("System.String");
                dtcom8.ColumnName = "Whid";
                dtcom8.ReadOnly = false;
                dtcom8.Unique = false;
                dtcom8.AllowDBNull = true;
                dt.Columns.Add(dtcom8); 
            }
            else
            {
                dt = (DataTable)Session["GridRule"];
            }

            DataRow dtrow = dt.NewRow();
            if (ddlaprvtype.SelectedIndex > 0)
            {
                RuleD = "This Document is to be approved by<b> " + ddlpartytype.SelectedItem.Text + "  " + ddlpartyname.SelectedItem.Text.ToString() + " </b> within <b> " + TextBox1.Text + " </b>Days.";
            }
            RuleD = "This Document is to be approved by <b>" + ddlpartytype.SelectedItem.Text + "  " + ddlpartyname.SelectedItem.Text.ToString() + " </b> for <b>" + ddlaprvtype.SelectedItem.Text.ToString() + " </b> within <b>" + TextBox1.Text + " </b>Days.";
            dtrow["RuleDetail"] = RuleD;
            dtrow["Whid"] = ddlbusparty.SelectedValue;
            dtrow["BusinessName"] = ddlbusparty.SelectedItem.Text;
            dtrow["StepNo"] = gridRuleDetail.Rows.Count + 1;
            dtrow["PartyTypeId"] = ddlpartytype.SelectedItem.Value.ToString();
            dtrow["PartyId"] = ddlpartyname.SelectedItem.Value.ToString();
            dtrow["ApprovedTypeId"] = ddlaprvtype.SelectedItem.Value.ToString();
            dtrow["Days"] = TextBox1.Text;
            dt.Rows.Add(dtrow);
            i = 1;
            foreach (DataRow dr in dt.Rows)
            {

                dr["StepNo"] = i.ToString();
                i = i + 1;
            }
            hdnStepNo.Value = Convert.ToString(i).ToString();
            Label2.Text = hdnStepNo.Value;
            Session["GridRule"] = dt;
            gridRuleDetail.DataSource = dt;
            gridRuleDetail.DataBind();

            ClearField();
        }


        Int32 condn=1;
        if (Session["GridRule"] == null)
        {
            
            lblmsg.Text = "You have to select atleast one Step.";
            return;
        }
        ibtnstep1.Text = "Add Employee";
        if (TextBox1.Text.Trim().Length <= 0)
        {
            TextBox1.Text = "1";
        }
        string strd = "SELECT * FROM RuleMasterforParty where RuleTitle = '" + txtrulename.Text + "' and RuleTypeId='" + ddlruletype.SelectedValue + "' and DocumentMainId='" + ddlcabinet.SelectedValue + "' and DocumentSubId='" + ddldrower.SelectedValue + "' and DocumentTypeId='" + ddltypeofdoc.SelectedValue + "' and Whid='" + ddlbusiness.SelectedValue + "' ";

          SqlCommand cmd1d = new SqlCommand(strd, con);
          cmd1d.CommandType = CommandType.Text;
          SqlDataAdapter dad = new SqlDataAdapter(cmd1d);
          DataTable dtd = new DataTable();
          dad.Fill(dtd);
          if (dtd.Rows.Count == 0)
          {

              //condn = Convert.ToInt32(ddlcondition1.SelectedItem.Value.ToString());
              Int32 scs = clsInstruction.InsertRuleMasterforParty(Convert.ToInt32(ddlruletype.SelectedItem.Value.ToString()), Convert.ToInt32(ddltypeofdoc.SelectedItem.Value.ToString()), Convert.ToDateTime(txtruledate.Text), txtrulename.Text, condn, ddlcabinet.SelectedValue, ddldrower.SelectedValue, ddlbusiness.SelectedValue);
              if (scs > 0)
              {
                  hdnRuleMaster.Value = scs.ToString();
                  if (Session["GridRule"] != null)
                  {
                      dt = new DataTable();
                      dt = (DataTable)Session["GridRule"];
                      foreach (DataRow dr in dt.Rows)
                      {
                          Int32 Empid;
                          Int32 Partyid;
                          bool insdetail;
                          insdetail = false;
                          Int32 AprvRuleType;
                          string Days = null;
                          AprvRuleType = Convert.ToInt32(dr["ApprovedTypeId"].ToString());
                          Days = dr["Days"].ToString();
                          Int32 step = Convert.ToInt32(dr["StepNo"].ToString());
                          bool empsel = false;
                          if (dr["PartyId"].ToString() == "0" | dr["PartyId"].ToString() == "All")
                          {
                              if (empsel == false)
                              {
                                  if (dr["PartyId"].ToString() == "0")
                                  {
                                      bool sc = clsInstruction.InsertRulePartySelectionMaster(Convert.ToInt32(hdnRuleMaster.Value), step, 1);
                                      empsel = true;
                                  }
                                  else
                                  {
                                      if (dr["PartyId"].ToString() == "All")
                                      {
                                          bool sc = clsInstruction.InsertRulePartySelectionMaster(Convert.ToInt32(hdnRuleMaster.Value), step, 2);
                                          empsel = true;
                                      }
                                  }
                              }
                              DataTable DtEmp = new DataTable();
                              Int32 desid = Convert.ToInt32(dr["PartyTypeId"].ToString());
                              //  DtEmp = clsMaster.selectEmployeewithDesignation(desid,ddlbusiness.SelectedValue);
                              DtEmp = clsMaster.SelectParty_MasterbyPartyTypeId(desid, ddlbusparty.SelectedValue);
                              foreach (DataRow drdes in DtEmp.Rows)
                              {
                                  Empid = Convert.ToInt32(drdes["PartyId"].ToString());
                                  insdetail = clsInstruction.InsertRuleDetailforParty(Convert.ToInt32(hdnRuleMaster.Value), Empid, AprvRuleType, step, Days);
                              }
                          }
                          else
                          {
                              Partyid = Convert.ToInt32(dr["PartyId"].ToString());
                              insdetail = clsInstruction.InsertRuleDetailforParty(Convert.ToInt32(hdnRuleMaster.Value), Partyid, AprvRuleType, step, Days);
                          }
                      }
                  }
                  else
                  {
                  }
              }

              Session["GridRule"] = null;
              hdnRuleDetail.Value = "0";
              hdnStepNo.Value = "1";
              lblStepNo.Text = hdnStepNo.Value;
              ClearField();
              ClearAll();
             
              lblmsg.Text = "Rule is set successfully.";
              FillRuleGridParty("", "");
              grid_Rule_master.DataSource = null;
              grid_Rule_master.DataBind();
              grid_Rule_master.Visible = false;
          }
          else
          {
              
              lblmsg.Text = "Record Already Inserted.";
          }
    }
    protected void grid_Rule_master_party_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid_Rule_master.PageIndex = e.NewPageIndex;
        FillRuleGridParty("", "");
    }
    protected void grid_Rule_master_party_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "detail1")
        {
           
            grid_Rule_master_party.Columns[11].Visible = true;

            foreach (GridViewRow gdr in grid_Rule_master_party.Rows)
            {
                GridView gr = (GridView)gdr.Cells[6].FindControl("grdRuleDetailParty");
                gr.DataSource = null;
                gr.DataBind();
            }
            grid_Rule_master_party.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int RuleId = Convert.ToInt32(grid_Rule_master_party.SelectedDataKey.Value);
            DataTable dt = new DataTable();
            dt = clsInstruction.SelectRuleDetailParty(RuleId);


            GridView gridDetail = (GridView)grid_Rule_master_party.Rows[grid_Rule_master_party.SelectedIndex].Cells[6].FindControl("grdRuleDetailParty");
            gridDetail.DataSource = dt;
            gridDetail.DataBind();
        }
        if (e.CommandName == "Delete")
        {
            int currentRowIndex = Int32.Parse(e.CommandArgument.ToString());
         //   int RuleId = Convert.ToInt32(grid_Rule_master_party.DataKeys[currentRowIndex].Value.ToString());
            int RuleId = Int32.Parse(e.CommandArgument.ToString());
            ViewState["delId"] = RuleId.ToString();
            ViewState["hdnt"] = "Party";
           // mdlpopupconfirm.Show();
            imgconfirmok_Click(sender, e);

        }
    }
    protected void grid_Rule_master_party_Sorting(object sender, GridViewSortEventArgs e)
    {
        SetFillGridSorting(e.SortExpression, sortOrder);
    }
    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
       // FillDesignation();
        FillRuleType();
        FillDocumentType();
        Fillddldocmaintype();
       // FillRuleApproveType();
       // FillUser();
        FillRuleGrid("", "");
        RadioButtonList1_SelectedIndexChanged(sender, e);
       
    }
    protected void ddlapprovetype1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string str = "SELECT * FROM RuleApproveTypeMaster where   RuleApproveTypeId='"+ddlapprovetype1.SelectedValue+"' ";

          SqlCommand cmd1 = new SqlCommand(str, con);
          cmd1.CommandType = CommandType.Text;
          SqlDataAdapter da = new SqlDataAdapter(cmd1);
          DataTable dt = new DataTable();
          da.Fill(dt);
          if (dt.Rows.Count > 0)
          {
              txtdes1.Text = Convert.ToString(dt.Rows[0]["Description"]);
             // txtdes1.Visible = true;
          }
          else
          {
              txtdes1.Text = "";
              //txtdes1.Visible = false;
          }
    }
    protected void ddlaprvtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        string str = "SELECT * FROM RuleApproveTypeMaster where   RuleApproveTypeId='" + ddlaprvtype.SelectedValue + "' ";

        SqlCommand cmd1 = new SqlCommand(str, con);
        cmd1.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtdesc2.Text = Convert.ToString(dt.Rows[0]["Description"]);
           // txtdesc2.Visible = true;
        }
        else
        {
            txtdesc2.Text = "";
           // txtdesc2.Visible = false;
        }
    }
    protected void grid_Rule_master_party_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grid_Rule_master_party.EditIndex = -1;
        FillRuleGridParty("", "");
    }
    protected void grid_Rule_master_party_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grid_Rule_master_party_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grid_Rule_master_party.EditIndex = e.NewEditIndex;
        grid_Rule_master_party.Columns[11].Visible = false;


        Label lblwhid = (Label)grid_Rule_master_party.Rows[e.NewEditIndex].FindControl("lblwhid");
        Label lblwname = (Label)grid_Rule_master_party.Rows[e.NewEditIndex].FindControl("lblwname");
        Label lblmain = (Label)grid_Rule_master_party.Rows[e.NewEditIndex].FindControl("lblmain");
        Label lblsub = (Label)grid_Rule_master_party.Rows[e.NewEditIndex].FindControl("lblsub");
        Label lblclnm = (Label)grid_Rule_master_party.Rows[e.NewEditIndex].FindControl("lblruletype");

        
        //string lbldoc = grid_Rule_master.Rows[e.NewEditIndex].Cells[4].Text.ToString();
        Label lbldoc = (Label)grid_Rule_master_party.Rows[e.NewEditIndex].FindControl("lblDocType");
        Label lblcon = (Label)grid_Rule_master_party.Rows[e.NewEditIndex].FindControl("lblConditionTypeName");
        int id = Convert.ToInt32(grid_Rule_master_party.DataKeys[e.NewEditIndex].Value.ToString());
        //string lblcon = grid_Rule_master.Rows[e.NewEditIndex].Cells[5].Text.ToString();
        FillRuleGridParty("", "");
        DropDownList ddl = (DropDownList)(grid_Rule_master_party.Rows[e.NewEditIndex].FindControl("ddlruletype"));
        //ddl.SelectedValue = gridDesignation.DataKeys[e.NewEditIndex].Value.ToString();
        DataTable dt;
        dt = new DataTable();
        clsMaster = new MasterCls();
        dt = clsInstruction.SelectRuleTypeMaster(ddlbusiness.SelectedValue);
        //return (DataSet) dt.DataSet;
        ddl.DataSource = dt;
        ddl.DataBind();
        ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(lblclnm.Text));

        //Label lbldoc = (Label)grid_Rule_master.Rows[e.NewEditIndex].Cells[4].FindControl("lblDocType");
        DropDownList ddlw = (DropDownList)(grid_Rule_master_party.Rows[e.NewEditIndex].FindControl("ddlwname"));
        DropDownList ddlm = (DropDownList)(grid_Rule_master_party.Rows[e.NewEditIndex].FindControl("ddlmaindoc"));
        DropDownList ddls = (DropDownList)(grid_Rule_master_party.Rows[e.NewEditIndex].FindControl("ddlsubdoc"));

       
        DropDownList ddldoc = (DropDownList)(grid_Rule_master_party.Rows[e.NewEditIndex].FindControl("ddlDocType"));
        //ddl.SelectedValue = gridDesignation.DataKeys[e.NewEditIndex].Value.ToString();
        DataTable dtdoc;
        dtdoc = new DataTable();
        clsMaster = new MasterCls();
        dt = clsInstruction.SelectRuleTypeMaster(lblwhid.Text);
        //return (DataSet) dt.DataSet;
        ddl.DataSource = dt;
        ddl.DataBind();
        ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(lblclnm.Text));

        string str = "SELECT WareHouseId as Whid,Name,Address,CurrencyId  FROM WareHouseMaster where comid = '" + Session["comid"] + "'and WareHouseMaster.Status='" + 1 + "' order by name";

        SqlCommand cmd1 = new SqlCommand(str, con);
        cmd1.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        DataTable dtw = new DataTable();
        da.Fill(dtw);

        ddlw.DataSource = dtw;
        
        ddlw.DataBind();
        ddlw.SelectedIndex = ddlw.Items.IndexOf(ddlw.Items.FindByText(lblwname.Text));

       // DocumentSubId
     
        string str132q = " SELECT [DocumentMainTypeId] as DocumentMainId,DocumentMainType  FROM  [dbo].[DocumentMainType] where Whid='" + ddlw.SelectedValue + "'";
        SqlCommand cgm = new SqlCommand(str132q, con);
        SqlDataAdapter adgm = new SqlDataAdapter(cgm);

        DataTable dtm = new DataTable();

        adgm.Fill(dtm);

        ddlm.DataSource = dtm;
        ddlm.DataBind();
        
      
        ddlm.SelectedIndex = ddlm.Items.IndexOf(ddlm.Items.FindByText(lblmain.Text));

        string str1781 = " SELECT     DocumentSubType.DocumentSubTypeId as DocumentSubId, DocumentSubType.DocumentSubType  FROM  DocumentSubType  WHERE     (DocumentMainTypeId = '" + ddlm.SelectedValue + "') ";
        SqlCommand cgs = new SqlCommand(str1781, con);
        SqlDataAdapter adgs = new SqlDataAdapter(cgs);

        DataTable dts = new DataTable();

        adgs.Fill(dts);
        if (dts.Rows.Count > 0)
        {
            ddls.DataSource = dts;
            ddls.DataBind();
        }
        ddls.Items.Insert(0, "All");
        ddls.SelectedItem.Value = "0";
        ddls.SelectedIndex = ddls.Items.IndexOf(ddls.Items.FindByText(lblsub.Text));




               // DocumentTypeId
        //  DocumentSubType
        string str132ss = "SELECT     DocumentType.DocumentTypeId, DocumentType.DocumentType   FROM         DocumentType WHERE     (DocumentType.DocumentSubTypeId = '" + ddls.SelectedValue + "')";

        SqlCommand cgss = new SqlCommand(str132ss, con);
        SqlDataAdapter adgwss = new SqlDataAdapter(cgss);

        DataTable dtss = new DataTable();

        adgwss.Fill(dtss);
        if (dtss.Rows.Count > 0)
        {
            ddldoc.DataSource = dtss;
            ddldoc.DataBind();
        }
        ddldoc.Items.Insert(0, "All");
        ddldoc.SelectedItem.Value = "0";
        ddldoc.SelectedIndex = ddldoc.Items.IndexOf(ddldoc.Items.FindByText(lbldoc.Text));

        //Label lblcon = (Label)grid_Rule_master.Rows[e.NewEditIndex].Cells[5].FindControl("lblConditionTypeName");

        DropDownList ddlcon = (DropDownList)(grid_Rule_master_party.Rows[e.NewEditIndex].FindControl("ddlcondition1"));
        ddlcon.SelectedIndex = ddlcon.Items.IndexOf(ddlcon.Items.FindByText(lblcon.Text));

        foreach (GridViewRow gdr in grid_Rule_master_party.Rows)
        {
            GridView gr = (GridView)gdr.Cells[6].FindControl("grdRuleDetailParty");
            gr.DataSource = null;
            gr.DataBind();
        }
        //grid_Rule_master.SelectedIndex = Convert.ToInt32(e.CommandArgument);
        //int RuleId = Convert.ToInt32(grid_Rule_master.SelectedDataKey.Value);
        DataTable dtssx = new DataTable();
        dtssx = clsInstruction.SelectRuleDetailParty(id);


        GridView gridDetail = (GridView)grid_Rule_master_party.Rows[e.NewEditIndex].FindControl("grdRuleDetailParty");
        gridDetail.DataSource = dtssx;
        gridDetail.DataBind();

    }
    protected void grid_Rule_master_party_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Int32 id = Int32.Parse(grid_Rule_master_party.DataKeys[e.RowIndex].Value.ToString());
        string name = ((TextBox)grid_Rule_master_party.Rows[e.RowIndex].FindControl("txtrulename")).Text;
        string ruledate = ((TextBox)grid_Rule_master_party.Rows[e.RowIndex].FindControl("txtruledate")).Text;
        DropDownList ddlruletype = (DropDownList)grid_Rule_master_party.Rows[e.RowIndex].FindControl("ddlruletype");
        DropDownList ddlDocType = (DropDownList)grid_Rule_master_party.Rows[e.RowIndex].FindControl("ddlDocType");
        DropDownList ddlcondition1 = (DropDownList)grid_Rule_master_party.Rows[e.RowIndex].FindControl("ddlcondition1");

        DropDownList ddlwname = (DropDownList)grid_Rule_master_party.Rows[e.RowIndex].FindControl("ddlwname");
        DropDownList ddlmaindoc = (DropDownList)grid_Rule_master_party.Rows[e.RowIndex].FindControl("ddlmaindoc");
        DropDownList ddlsubdoc = (DropDownList)grid_Rule_master_party.Rows[e.RowIndex].FindControl("ddlsubdoc");
        
        Int32 ruletypeid = Int32.Parse(ddlruletype.SelectedValue);
        Int32 doctypeid = Int32.Parse(ddlDocType.SelectedValue);
        Int32 condid = Int32.Parse(ddlcondition1.SelectedValue);
        string str = "SELECT * FROM RuleMasterforParty where RuleTitle = '" + name + "' and RuleTypeId='" + ddlruletype.SelectedValue + "' and DocumentMainId='" + ddlmaindoc.SelectedValue + "' and DocumentSubId='" + ddlsubdoc.SelectedValue + "' and DocumentTypeId='" + ddlDocType.SelectedValue + "' and Whid='" + ddlwname.SelectedValue + "' and RuleId<>'" + id + "' ";

          SqlCommand cmd1 = new SqlCommand(str, con);
          cmd1.CommandType = CommandType.Text;
          SqlDataAdapter da = new SqlDataAdapter(cmd1);
          DataTable dt = new DataTable();
          da.Fill(dt);
          if (dt.Rows.Count == 0)
          {
              bool success = clsInstruction.UpdateRuleMasterforParty(id, ruletypeid, doctypeid, Convert.ToDateTime(ruledate), name, condid, ddlmaindoc.SelectedValue, ddlsubdoc.SelectedValue, ddlwname.SelectedValue);
              if (Convert.ToString(success) == "True")
              {
                  
                  lblmsg.Visible = true;
                  lblmsg.Text = "Record is updated successfully";
              }
              else
              {
                  
                  lblmsg.Visible = true;
                  lblmsg.Text = "Record is not updated successfully";
              }
          }
          else
          {
              
              lblmsg.Visible = true;
              lblmsg.Text = "Record Already Inserted";
          }
        grid_Rule_master_party.EditIndex = -1;
        FillRuleGridParty("", "");
    }

    protected void imgconfirmok_Click(object sender, EventArgs e)
    {
       // mdlpopupconfirm.Hide();

        if (ViewState["hdnt"].ToString() == "Employee")
        {
            string str = "Select RuleProcessMaster.* From RuleProcessMaster inner join RuleDetail on RuleDetail.RuleDetailId=RuleProcessMaster.RuleDetailId where  RuleDetail.RuleId='" + Convert.ToInt32(ViewState["delId"]) + "' ";

          SqlCommand cmd1 = new SqlCommand(str, con);
          cmd1.CommandType = CommandType.Text;
          SqlDataAdapter da = new SqlDataAdapter(cmd1);
          DataTable dt = new DataTable();
          da.Fill(dt);
          if (dt.Rows.Count <= 0)
          {
              bool rst = clsInstruction.DeleteRuleMaster(Convert.ToInt32(ViewState["delId"]));
              
              lblmsg.Visible = true;
              lblmsg.Text = "Record deleted successfully";
              FillRuleGrid("", "");
          }
          else
          {
              
              lblmsg.Visible = true;
              lblmsg.Text = "You are unable to delete this rule as there are documents that are approved or rejected using this approval rule.  ";
          }
        }
        else if (ViewState["hdnt"].ToString() == "Party")
        {
            string str = "Select RuleProcessMasterforParty.* From RuleProcessMasterforParty inner join RuleDetailforParty on RuleDetailforParty.RuleDetailId=RuleProcessMasterforParty.RuleDetailId where  RuleDetailforParty.RuleId='" + Convert.ToInt32(ViewState["delId"]) + "' ";

          SqlCommand cmd1 = new SqlCommand(str, con);
          cmd1.CommandType = CommandType.Text;
          SqlDataAdapter da = new SqlDataAdapter(cmd1);
          DataTable dt = new DataTable();
          da.Fill(dt);
          if (dt.Rows.Count <= 0)
          {
              bool rst = clsInstruction.DeleteRuleMasterforParty(Convert.ToInt32(ViewState["delId"]));
              
              lblmsg.Visible = true;
              lblmsg.Text = "Record deleted successfully";
              FillRuleGridParty("", "");
          }
          else
          {
              
              lblmsg.Visible = true;
              lblmsg.Text = "You are unable to delete this rule as there are documents that are approved or rejected using this approval rule.  ";

          }
        }
    }
    protected void imgconfirmcalcel_Click(object sender, EventArgs e)
    {
       mdlpopupconfirm.Hide();
    }
    protected void grid_Rule_master_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grid_Rule_master.EditIndex = e.NewEditIndex;
        grid_Rule_master.Columns[13].Visible = false;
        Label lblwhid = (Label)grid_Rule_master.Rows[e.NewEditIndex].FindControl("lblwhid");
        Label lblwname = (Label)grid_Rule_master.Rows[e.NewEditIndex].FindControl("lblwname");
        Label lblmain = (Label)grid_Rule_master.Rows[e.NewEditIndex].FindControl("lblmain");
        Label lblsub = (Label)grid_Rule_master.Rows[e.NewEditIndex].FindControl("lblsub");
        Label lblclnm = (Label)grid_Rule_master.Rows[e.NewEditIndex].FindControl("lblruletype");


        Label lbldoc = (Label)grid_Rule_master.Rows[e.NewEditIndex].FindControl("lblDocType");
        Label lblcon = (Label)grid_Rule_master.Rows[e.NewEditIndex].FindControl("lblConditionTypeName");
        int id = Convert.ToInt32(grid_Rule_master.DataKeys[e.NewEditIndex].Value.ToString());
        //string lblcon = grid_Rule_master.Rows[e.NewEditIndex].Cells[5].Text.ToString();
        FillRuleGrid("", "");
        DropDownList ddlw = (DropDownList)(grid_Rule_master.Rows[e.NewEditIndex].FindControl("ddlwname"));
        DropDownList ddlm = (DropDownList)(grid_Rule_master.Rows[e.NewEditIndex].FindControl("ddlmaindoc"));
        DropDownList ddls = (DropDownList)(grid_Rule_master.Rows[e.NewEditIndex].FindControl("ddlsubdoc"));

       
        DropDownList ddl = (DropDownList)(grid_Rule_master.Rows[e.NewEditIndex].FindControl("ddlruletype"));
        CheckBox chkapp = (CheckBox)(grid_Rule_master.Rows[e.NewEditIndex].FindControl("chkapp"));
        //ddl.SelectedValue = gridDesignation.DataKeys[e.NewEditIndex].Value.ToString();
        DataTable dt;
        dt = new DataTable();
        clsMaster = new MasterCls();
        dt = clsInstruction.SelectRuleTypeMaster(lblwhid.Text);
        //return (DataSet) dt.DataSet;
        ddl.DataSource = dt;
        ddl.DataBind();
        ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(lblclnm.Text));
        chkcheckapp.Checked = chkapp.Checked;
        string str = "SELECT WareHouseId as Whid,Name,Address,CurrencyId  FROM WareHouseMaster where comid = '" + Session["comid"] + "'and WareHouseMaster.Status='" + 1 + "' order by name";

        SqlCommand cmd1 = new SqlCommand(str, con);
        cmd1.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        DataTable dtw = new DataTable();
        da.Fill(dtw);

        ddlw.DataSource = dtw;
        
        ddlw.DataBind();
        ddlw.SelectedIndex = ddlw.Items.IndexOf(ddlw.Items.FindByText(lblwname.Text));

       // DocumentSubId
     
        string str132q = " SELECT [DocumentMainTypeId] as DocumentMainId,DocumentMainType  FROM  [dbo].[DocumentMainType] where Whid='" + ddlw.SelectedValue + "'";
        SqlCommand cgm = new SqlCommand(str132q, con);
        SqlDataAdapter adgm = new SqlDataAdapter(cgm);

        DataTable dtm = new DataTable();

        adgm.Fill(dtm);

        ddlm.DataSource = dtm;
        ddlm.DataBind();
        
      
        ddlm.SelectedIndex = ddlm.Items.IndexOf(ddlm.Items.FindByText(lblmain.Text));

        string str1781 = " SELECT     DocumentSubType.DocumentSubTypeId as DocumentSubId, DocumentSubType.DocumentSubType  FROM  DocumentSubType  WHERE     (DocumentMainTypeId = '" + ddlm.SelectedValue + "') ";
        SqlCommand cgs = new SqlCommand(str1781, con);
        SqlDataAdapter adgs = new SqlDataAdapter(cgs);

        DataTable dts = new DataTable();

        adgs.Fill(dts);
        if (dts.Rows.Count > 0)
        {
            ddls.DataSource = dts;
            ddls.DataBind();
        }
        ddls.Items.Insert(0, "All");
        ddls.SelectedItem.Value = "0";
        ddls.SelectedIndex = ddls.Items.IndexOf(ddls.Items.FindByText(lblsub.Text));




        DropDownList ddldoc = (DropDownList)(grid_Rule_master.Rows[e.NewEditIndex].FindControl("ddlDocType"));
        // DocumentTypeId
        //  DocumentSubType
        string str132ss = "SELECT     DocumentType.DocumentTypeId, DocumentType.DocumentType   FROM         DocumentType WHERE     (DocumentType.DocumentSubTypeId = '" + ddls.SelectedValue + "')";

        SqlCommand cgss = new SqlCommand(str132ss, con);
        SqlDataAdapter adgwss = new SqlDataAdapter(cgss);

        DataTable dtss = new DataTable();

        adgwss.Fill(dtss);
        if (dtss.Rows.Count > 0)
        {
            ddldoc.DataSource = dtss;
            ddldoc.DataBind();
        }
        ddldoc.Items.Insert(0, "All");
        ddldoc.SelectedItem.Value = "0";
        ddldoc.SelectedIndex = ddldoc.Items.IndexOf(ddldoc.Items.FindByText(lbldoc.Text));

        //Label lblcon = (Label)grid_Rule_master.Rows[e.NewEditIndex].Cells[5].FindControl("lblConditionTypeName");

        DropDownList ddlcon = (DropDownList)(grid_Rule_master.Rows[e.NewEditIndex].FindControl("ddlcondition1"));
        ddlcon.SelectedIndex = ddlcon.Items.IndexOf(ddlcon.Items.FindByText(lblcon.Text));

        foreach (GridViewRow gdr in grid_Rule_master.Rows)
        {
            GridView gr = (GridView)gdr.Cells[6].FindControl("grdRuleDetail");
            gr.DataSource = null;
            gr.DataBind();
        }
        //grid_Rule_master.SelectedIndex = Convert.ToInt32(e.CommandArgument);
        //int RuleId = Convert.ToInt32(grid_Rule_master.SelectedDataKey.Value);
        DataTable dtsr = new DataTable();
        dtsr = clsInstruction.SelectRuleDetail(id);


        GridView gridDetail = (GridView)grid_Rule_master.Rows[e.NewEditIndex].FindControl("grdRuleDetail");
        gridDetail.DataSource = dtsr;
        gridDetail.DataBind();

    }
    protected void grid_Rule_master_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Int32 id = Int32.Parse(grid_Rule_master.DataKeys[e.RowIndex].Value.ToString());
        string name = ((TextBox)grid_Rule_master.Rows[e.RowIndex].FindControl("txtrulename")).Text;
        string ruledate = ((TextBox)grid_Rule_master.Rows[e.RowIndex].FindControl("txtruledate")).Text;
        DropDownList ddlruletype = (DropDownList)grid_Rule_master.Rows[e.RowIndex].FindControl("ddlruletype");
        DropDownList ddlDocType = (DropDownList)grid_Rule_master.Rows[e.RowIndex].FindControl("ddlDocType");
        DropDownList ddlcondition1 = (DropDownList)grid_Rule_master.Rows[e.RowIndex].FindControl("ddlcondition1");

        DropDownList ddlwname = (DropDownList)grid_Rule_master.Rows[e.RowIndex].FindControl("ddlwname");
        DropDownList ddlmaindoc = (DropDownList)grid_Rule_master.Rows[e.RowIndex].FindControl("ddlmaindoc");
        DropDownList ddlsubdoc = (DropDownList)grid_Rule_master.Rows[e.RowIndex].FindControl("ddlsubdoc");
        CheckBox chkapp = (CheckBox)(grid_Rule_master.Rows[e.RowIndex].FindControl("chkapp"));
        Int32 ruletypeid = Int32.Parse(ddlruletype.SelectedValue);
        Int32 doctypeid = Int32.Parse(ddlDocType.SelectedValue);
        Int32 condid = Int32.Parse(ddlcondition1.SelectedValue);

        string str = "SELECT * FROM RuleMaster where RuleTitle = '" + name + "' and RuleTypeId='" + ddlruletype.SelectedValue + "' and DocumentMainId='" + ddlmaindoc.SelectedValue + "' and DocumentSubId='" + ddlsubdoc.SelectedValue + "' and DocumentTypeId='" + ddlDocType.SelectedValue + "' and Whid='" + ddlwname.SelectedValue + "' and RuleId<>'"+id+"' ";

          SqlCommand cmd1 = new SqlCommand(str, con);
          cmd1.CommandType = CommandType.Text;
          SqlDataAdapter da = new SqlDataAdapter(cmd1);
          DataTable dt = new DataTable();
          da.Fill(dt);
          if (dt.Rows.Count == 0)
          {

              bool success = clsInstruction.UpdateRuleMaster(id, ruletypeid, doctypeid, Convert.ToDateTime(ruledate), name, condid, ddlmaindoc.SelectedValue, ddlsubdoc.SelectedValue, ddlwname.SelectedValue,Convert.ToBoolean(chkapp.Checked));
              if (Convert.ToString(success) == "True")
              {                  
                  lblmsg.Visible = true;
                  lblmsg.Text = "Record is updated successfully";
              }
              else
              {                  
                  lblmsg.Visible = true;
                  lblmsg.Text = "Record is not updated successfully";
              }
          }
          else
          {              
              lblmsg.Visible = true;
              lblmsg.Text = "Record Already Inserted";
          }
        grid_Rule_master.EditIndex = -1;
        FillRuleGrid("", "");
    }
    protected void grid_Rule_master_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grid_Rule_master.EditIndex = -1;
        FillRuleGrid("", "");
    }
    protected void grid_Rule_master_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void ddlbuemp_SelectedIndexChanged(object sender, EventArgs e)
    {     
         FillDesignation();
         
         FillRuleApproveType();
         FillUser();
         ddlapprovetype1_SelectedIndexChanged(sender,e);
    }
    protected void ddlbusparty_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillPartyMasterwithpartytype(Int32.Parse(ddlpartytype.SelectedValue.ToString()));
        FillRuleApproveType();

    }
    protected void ddlbusbyfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblcomname.Text =ddlbusbyfilter.SelectedItem.Text;
        if (RadioButtonList1.SelectedIndex == 0)
        {
           
            FillRuleGrid("","");
        }
        else if (RadioButtonList1.SelectedIndex == 1)
        {
           
            FillRuleGridParty("", "");
        }
    }
    protected void ddlcabinet_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddldrower.Items.Clear();
        if (ddlcabinet.SelectedIndex > -1)
        {
            FillDocumentSubTypeWithMainType(Int32.Parse(ddlcabinet.SelectedValue.ToString()));
        }
    }
    protected void FillDocumentSubTypeWithMainType(Int32 DocumentMainTypeId)
    {
        //DataTable dt = new DataTable();
        //dt = clsDocument.SelectDocumentSubTypeWithMainType(DocumentMainTypeId);
        ddldrower.Items.Clear();
        string str178 = "";
        if (RadioButtonList1.SelectedItem.Text == "Party")
        {
            str178 = " SELECT     DocumentSubType.DocumentSubTypeId, DocumentSubType.DocumentSubType, DocumentMainType.DocumentMainTypeId as DocumentMainTypeId,  DocumentMainType.DocumentMainType FROM         DocumentMainType RIGHT OUTER JOIN DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId WHERE     (DocumentMainType.DocumentMainTypeId = '" + DocumentMainTypeId + "') and DocumentSubType in('External Parties','Master Documents')";
        }
        else
        {
            str178 = " SELECT     DocumentSubType.DocumentSubTypeId, DocumentSubType.DocumentSubType, DocumentMainType.DocumentMainTypeId as DocumentMainTypeId,  DocumentMainType.DocumentMainType FROM         DocumentMainType RIGHT OUTER JOIN DocumentSubType ON DocumentMainType.DocumentMainTypeId = DocumentSubType.DocumentMainTypeId WHERE     (DocumentMainType.DocumentMainTypeId = '" + DocumentMainTypeId + "')";

        }
             SqlCommand cgw = new SqlCommand(str178, con);
        SqlDataAdapter adgw = new SqlDataAdapter(cgw);

        DataTable dt = new DataTable();

        adgw.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddldrower.DataSource = dt;
            ddldrower.DataBind();
        }
        ddldrower.Items.Insert(0, "All");
        ddldrower.SelectedItem.Value = "0";
        FillDocumentTypeWithSubType(Int32.Parse(ddldrower.SelectedValue.ToString()));
    }
    protected void ddldrower_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddldrower.SelectedIndex > 0)
        {
            FillDocumentTypeWithSubType(Int32.Parse(ddldrower.SelectedValue.ToString()));
        }
    }
    protected void FillDocumentTypeWithSubType(Int32 DocumentSubTypeId)
    {
        //DataTable dt = new DataTable();
        //dt = clsDocument.SelectDocumentTypeWithSubType(DocumentSubTypeId);
        ddltypeofdoc.Items.Clear();
        string str132 = "";
        if (RadioButtonList1.SelectedItem.Text == "Party")
        {
            str132 = "SELECT     DocumentType.DocumentTypeId, DocumentType.DocumentType   FROM         DocumentType Inner join DocumentSubType on DocumentSubType.DocumentSubTypeId=DocumentType.DocumentSubTypeId WHERE     (DocumentType.DocumentSubTypeId = '" + ddldrower.SelectedValue + "') and DocumentType In ('File For Approval(F)','General','Mission Documents','Party OutboundDocuments','Party InboundDocuments')";

                    }
        else
        {
            str132 = "SELECT     DocumentType.DocumentTypeId, DocumentType.DocumentType   FROM         DocumentType WHERE     (DocumentType.DocumentSubTypeId = '" + ddldrower.SelectedValue + "')";

        }
        SqlCommand cgw = new SqlCommand(str132, con);
        SqlDataAdapter adgw = new SqlDataAdapter(cgw);

        DataTable dt = new DataTable();

        adgw.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddltypeofdoc.DataSource = dt;
            ddltypeofdoc.DataBind();
        }
        ddltypeofdoc.Items.Insert(0, "All");
        ddltypeofdoc.SelectedItem.Value = "0";
       
    }
    protected void ddlwname_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((DropDownList)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;
        DropDownList ddlwname = new DropDownList();
       DropDownList ddlmaindoc = new DropDownList();
       DropDownList ddlruletype = new DropDownList();
       DropDownList ddlsubdoc = new DropDownList();
       DropDownList ddlDocType = new DropDownList();
       if (RadioButtonList1.SelectedIndex == 0)
       {
            ddlwname = (DropDownList)(grid_Rule_master.Rows[rinrow].FindControl("ddlwname"));
            ddlruletype = (DropDownList)(grid_Rule_master.Rows[rinrow].FindControl("ddlruletype"));
           ddlmaindoc = (DropDownList)(grid_Rule_master.Rows[rinrow].FindControl("ddlmaindoc"));
           ddlsubdoc = (DropDownList)(grid_Rule_master.Rows[rinrow].FindControl("ddlsubdoc"));
           ddlDocType = (DropDownList)(grid_Rule_master.Rows[rinrow].FindControl("ddlDocType"));
       }
       else if (RadioButtonList1.SelectedIndex ==1)
       {
           ddlwname = (DropDownList)(grid_Rule_master_party.Rows[rinrow].FindControl("ddlwname"));
           ddlruletype = (DropDownList)(grid_Rule_master_party.Rows[rinrow].FindControl("ddlruletype"));
           ddlmaindoc = (DropDownList)(grid_Rule_master_party.Rows[rinrow].FindControl("ddlmaindoc"));
           ddlsubdoc = (DropDownList)(grid_Rule_master_party.Rows[rinrow].FindControl("ddlsubdoc"));
           ddlDocType = (DropDownList)(grid_Rule_master_party.Rows[rinrow].FindControl("ddlDocType"));
       }
       fillgridmm(ddlwname, ddlruletype, ddlmaindoc, ddlsubdoc, ddlDocType);
     }
    protected void fillgridmm(DropDownList ddlwname, DropDownList ddlruletype, DropDownList ddlmaindoc, DropDownList ddlsubdoc, DropDownList ddlDocType)
    {
        dt = clsInstruction.SelectRuleTypeMaster(ddlwname.SelectedValue);
        ddlruletype.DataSource = dt;
        ddlruletype.DataBind();
        ddlmaindoc.Items.Clear();
        string str132q = " SELECT [DocumentMainTypeId] as DocumentMainId,DocumentMainType  FROM  [dbo].[DocumentMainType] where Whid='" + ddlwname.SelectedValue + "'";
        SqlCommand cgm = new SqlCommand(str132q, con);
        SqlDataAdapter adgm = new SqlDataAdapter(cgm);

        DataTable dtm = new DataTable();
        adgm.Fill(dtm);
        if (dtm.Rows.Count > 0)
        {
           
            ddlmaindoc.DataSource = dtm;
            ddlmaindoc.DataBind();
        }

        fillgridmain(ddlsubdoc, ddlmaindoc, ddlDocType);
    }
    protected void ddlmaindoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((DropDownList)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;

       DropDownList ddlsubdoc = new DropDownList();
       DropDownList ddlmaindoc = new DropDownList();
       DropDownList ddlDocType = new DropDownList();
        if (rdfilterpartyemployee.SelectedIndex == 0)
        {
             ddlsubdoc = (DropDownList)(grid_Rule_master.Rows[rinrow].FindControl("ddlsubdoc"));
             ddlmaindoc = (DropDownList)(grid_Rule_master.Rows[rinrow].FindControl("ddlmaindoc"));
             ddlDocType = (DropDownList)(grid_Rule_master.Rows[rinrow].FindControl("ddlDocType"));
        }
        else if (rdfilterpartyemployee.SelectedIndex == 1)
        {
            ddlsubdoc = (DropDownList)(grid_Rule_master_party.Rows[rinrow].FindControl("ddlsubdoc"));
            ddlmaindoc = (DropDownList)(grid_Rule_master_party.Rows[rinrow].FindControl("ddlmaindoc"));
            ddlDocType = (DropDownList)(grid_Rule_master_party.Rows[rinrow].FindControl("ddlDocType"));
        }
        fillgridmain(ddlsubdoc, ddlmaindoc, ddlDocType);
    }
    protected void fillgridmain(DropDownList ddlsubdoc, DropDownList ddlmaindoc, DropDownList ddlDocType)
    {
        ddlsubdoc.Items.Clear();
        string str1781 = " SELECT     DocumentSubType.DocumentSubTypeId as DocumentSubId, DocumentSubType.DocumentSubType  FROM  DocumentSubType  WHERE     (DocumentMainTypeId = '" + ddlmaindoc.SelectedValue + "') ";
        SqlCommand cgs = new SqlCommand(str1781, con);
        SqlDataAdapter adgs = new SqlDataAdapter(cgs);

        DataTable dts = new DataTable();

        adgs.Fill(dts);
        if (dts.Rows.Count > 0)
        {
            ddlsubdoc.DataSource = dts;
            ddlsubdoc.DataBind();
        }
        ddlsubdoc.Items.Insert(0, "All");
        ddlsubdoc.SelectedItem.Value = "0";
        EventArgs e = new EventArgs();
        object sender = new object();
        fillgridsub(ddlsubdoc, ddlDocType);
    }

    protected void ddlsubdoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((DropDownList)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;
        DropDownList ddlsubdoc = new DropDownList();
        DropDownList ddlDocType = new DropDownList();
        if (rdfilterpartyemployee.SelectedIndex == 0)
        {
            ddlsubdoc = (DropDownList)(grid_Rule_master.Rows[rinrow].FindControl("ddlsubdoc"));
            ddlDocType = (DropDownList)(grid_Rule_master.Rows[rinrow].FindControl("ddlDocType"));
        }
        else if (rdfilterpartyemployee.SelectedIndex == 1)
        {
            ddlsubdoc = (DropDownList)(grid_Rule_master_party.Rows[rinrow].FindControl("ddlsubdoc"));
            ddlDocType = (DropDownList)(grid_Rule_master_party.Rows[rinrow].FindControl("ddlDocType"));
    
        }
        fillgridsub(ddlsubdoc, ddlDocType);
    }
    protected void fillgridsub(DropDownList ddlsubdoc, DropDownList ddlDocType)
    {
        ddlDocType.Items.Clear();
        string str132ss = "SELECT     DocumentType.DocumentTypeId, DocumentType.DocumentType   FROM         DocumentType WHERE     (DocumentType.DocumentSubTypeId = '" + ddlsubdoc.SelectedValue + "')";

        SqlCommand cgss = new SqlCommand(str132ss, con);
        SqlDataAdapter adgwss = new SqlDataAdapter(cgss);

        DataTable dtss = new DataTable();

        adgwss.Fill(dtss);
        if (dtss.Rows.Count > 0)
        {
            ddlDocType.DataSource = dtss;
            ddlDocType.DataBind();
        }
        ddlDocType.Items.Insert(0, "All");
        ddlDocType.SelectedItem.Value = "0";

    }

    protected void lnkadd0_Click(object sender, ImageClickEventArgs e)
    {
        FillRuleType();
    }
    protected void rdfilterpartyemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdfilterpartyemployee.SelectedItem.Text == "Party")
        {
            lblhead.Text = "List of Business rule for party";
            lblheaa.Text = "List of Business rule for party";
            FillRuleGridParty("", "");
            grid_Rule_master.DataSource = null;
            grid_Rule_master.DataBind();
            grid_Rule_master.Visible = false;
            grid_Rule_master_party.Visible = true;
            
        }
        else if (rdfilterpartyemployee.SelectedItem.Text == "Employee")
        {
            lblhead.Text = "List of Business rule for employee";
            lblheaa.Text = "List of Business rule for employee";
            grid_Rule_master.Visible = true;
            grid_Rule_master_party.Visible = false;
            FillRuleGrid("", "");

           
        }
    }
    protected void LinkButton1_Click(object sender, ImageClickEventArgs e)
    { 
        string te = "AddBusinessRuleType.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button3.Visible = true;
            if (grid_Rule_master.Columns[9].Visible == true)
            {
                ViewState["editHide"] = "tt";
                grid_Rule_master.Columns[9].Visible = false;
            }
            if (grid_Rule_master.Columns[11].Visible == true)
            {
                ViewState["delHide"] = "tt";
                grid_Rule_master.Columns[11].Visible = false;
            }
            
            if (grid_Rule_master_party.Columns[8].Visible == true)
            {
                ViewState["editHidep"] = "tt";
                grid_Rule_master_party.Columns[8].Visible = false;
            }
            
            if (grid_Rule_master_party.Columns[10].Visible == true)
            {
                ViewState["viewHidep"] = "tt";
                grid_Rule_master_party.Columns[10].Visible = false;
            }
        }
        else
        {

            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(200);

            Button1.Text = "Printable Version";
            Button3.Visible = false;
            if (ViewState["editHide"] != null)
            {
                grid_Rule_master.Columns[9].Visible = true;
            }
            if (ViewState["delHide"] != null)
            {
                grid_Rule_master.Columns[11].Visible = true;
            }
           
            if (ViewState["editHidep"] != null)
            {
                grid_Rule_master_party.Columns[8].Visible = true;
            }
           
            if (ViewState["viewHidep"] != null)
            {
                grid_Rule_master_party.Columns[10].Visible = true;
            }
        }
    }
    protected void ddlcondition1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "True")
        {
            if (ddlcondition1.SelectedValue == "2")
            {
                Label25.Visible = true;

                lblflowtypelabel.Text = ddlcondition1.SelectedItem.Text;
            }
            else if(ddlcondition1.SelectedValue == "1")
            {
                Label25.Visible = false;

                lblflowtypelabel.Text = ddlcondition1.SelectedItem.Text;
            }
            else
            {
                lblflowtypelabel.Text = ddlcondition1.SelectedItem.Text;

            }
        }
        else
        {
            Label25.Visible = false;

            Label26.Visible = false;
            lblflowtypelabel.Visible = false;

        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string str = "SELECT * FROM RuleApproveTypeMaster where RuleApproveTypeId = '" + ddlapprovetype1.SelectedValue + "'  ";

        SqlCommand cmd1 = new SqlCommand(str, con);
        cmd1.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblapprovetypedescription.Text = dt.Rows[0]["Description"].ToString();

            ModalPopupExtender2.Show();
        }
        else
        {
            ModalPopupExtender2.Hide();
        }
        
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        string str = "SELECT * FROM RuleApproveTypeMaster where RuleApproveTypeId = '" + ddlaprvtype.SelectedValue + "'  ";

        SqlCommand cmd1 = new SqlCommand(str, con);
        cmd1.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Label31.Text = dt.Rows[0]["Description"].ToString();

            ModalPopupExtender1.Show();
        }
        else
        {
            ModalPopupExtender1.Hide();
        }

    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (pnlsh.Visible == false)
        {
            lbllegend.Text = "Add New Document Approval Rule";
            pnlsh.Visible = true;
            btnadd.Visible = false;
        }
       
    }
    protected DataTable select(string str)
    {
        if (str == "")
        {
            str = "";
        }

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (btnsave.Text == "Edit Status")
        {
            lblmsg.Text = "";
            btnsave.Text = "Update Status";
            foreach (GridViewRow item in grid_Rule_master.Rows)
            {
                CheckBox chksta = (CheckBox)item.FindControl("chksta");
                Label lblstat = (Label)item.FindControl("lblstat");
                if (lblstat.Text == "Inactive")
                {
                    chksta.Visible = false;
                    lblstat.Visible = true;
                }
                else
                {
                    lblstat.Visible = false;
                    chksta.Visible = true;
                }
            }
        }
        else
        {
            btnsave.Text = "Edit Status";
            foreach (GridViewRow item in grid_Rule_master.Rows)
            {
                string ruleid = grid_Rule_master.DataKeys[item.RowIndex].Value.ToString();
                CheckBox chksta = (CheckBox)item.FindControl("chksta");
                Label lblstat = (Label)item.FindControl("lblstat");
                if (chksta.Checked == true)
                {
                    string strrulup = "Update RuleMaster Set Active='0' where RuleId='" + ruleid + "'";
                    SqlCommand sqlcmd = new SqlCommand(strrulup, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    int ab = Convert.ToInt32(sqlcmd.ExecuteNonQuery());
                    con.Close();
                    if (ab == 1)
                    {
                        //string strrulupEm = "Delete from  EmailApproval  where RuleId='" + ruleid + "'";
                        //SqlCommand sqlcmdem = new SqlCommand(strrulupEm, con);
                        //if (con.State.ToString() != "Open")
                        //{
                        //    con.Open();
                        //}
                        //int abc = Convert.ToInt32(sqlcmdem.ExecuteNonQuery());
                        //con.Close();
                        DataTable dts = select("Select top(1) StepId,RuleDetailId,EmployeeId  from RuleDetail where RuleId='" + ruleid + "' order by StepId Desc");
                        if (dts.Rows.Count > 0)
                        {
                            string strrulupEmdet = "Delete from  RuleProcessMaster where DocumentId NOT in(Select DocumentId from RuleProcessMaster where RuleDetailId='" + dts.Rows[0]["RuleDetailId"] + "') and " +
                                " RuleDetailId In(Select RuleDetailId  from RuleDetail where RuleId='" + ruleid + "')";
                            SqlCommand sqlcmdemdet = new SqlCommand(strrulupEmdet, con);
                            if (con.State.ToString() != "Open")
                            {
                                con.Open();
                            }
                            int abcd = Convert.ToInt32(sqlcmdemdet.ExecuteNonQuery());
                            con.Close();
                        }
                    }
                  
                     //btnsave_Click(sender,e);
                    lblmsg.Text = "Record updated successfully";
                    FillRuleGrid("", "");
                }
            }
           
        }
    }
}
