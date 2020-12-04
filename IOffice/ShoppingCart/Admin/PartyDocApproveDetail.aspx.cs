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
public partial class Account_PartyDocApproveDetail : System.Web.UI.Page
{
    SqlConnection con;
    MasterCls clsMaster = new MasterCls();
    DataTable dt = new DataTable();
    protected int DesignationId;
    InstructionCls clsInstruction = new InstructionCls();
    DocumentCls1 clsDocument = new DocumentCls1();
    EmployeeCls clsEmployee = new EmployeeCls();
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
			Session["PageUrl"]=strData;
            Session["PageName"] = page;
            Page.Title = pg.getPageTitle(page);

            DesignationId = Convert.ToInt32(Session["DesignationId"]);
            if (!Page.IsPostBack)
            {
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
                string eeed = " Select distinct EmployeeMaster.Whid from  EmployeeMaster where EmployeeMasterId='" + Session["EmployeeId"] + "'";
                SqlCommand cmdeeed = new SqlCommand(eeed, con);
                SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
                DataTable dteeed = new DataTable();
                adpeeed.Fill(dteeed);
                if (dteeed.Rows.Count > 0)
                {
                    ddlbusiness.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);

                }
                DesignationId = Convert.ToInt32(Session["DesignationId"]);
                Session["PageName"] = "PartyDocApproveDetail.aspx";
                ddlbusiness_SelectedIndexChanged(sender, e);
            }
            
    }
    protected void FillDocumentType()
    {
        dt = new DataTable();
        //dt = clsDocument.SelectDocTypeAll();
        dt = clsDocument.SelectPublicDocTypeAll(ddlbusiness.SelectedValue);
        ddltypeofdoc.DataTextField = "doctype";
        ddltypeofdoc.DataValueField = "DocumentTypeId";
        ddltypeofdoc.DataSource = dt;
        ddltypeofdoc.DataBind();
        ddltypeofdoc.Items.Insert(0, "- All - ");
        ddltypeofdoc.SelectedItem.Value = "0";
    }
    /*protected void FillRuleType()
    {
        dt = new DataTable();
        dt = clsInstruction.SelectRuleTypeMaster();
        ddlruletype.DataTextField = "RuleType";
        ddlruletype.DataValueField = "RuleTypeId";
        ddlruletype.DataSource = dt;
        ddlruletype.DataBind();
        ddlruletype.Items.Insert(0, "- All - ");
        ddlruletype.SelectedItem.Value = "0";
        if (dt.Rows.Count > 0)
        {
            ddlruletype.SelectedIndex = 0;
        }
    }*/
    protected void FillRuleTypeAllforParty()
    {
        dt = new DataTable();

        //dt = clsInstruction.SelectRuleMasterRuleTypeWise(Convert.ToInt32(ddlruletype.SelectedValue.ToString()));
        dt = clsInstruction.SelectRuleTypeAllforParty(ddlbusiness.SelectedValue);
        DdlRuleName.DataSource = dt;
        DdlRuleName.DataBind();
        DdlRuleName.Items.Insert(0, "- All - ");
        DdlRuleName.SelectedItem.Value = "0";
    }
    protected DataTable select(String str)
    {
        DataTable dt = new DataTable();
        SqlCommand cmdeeed = new SqlCommand(str, con);
        SqlDataAdapter adpeeed = new SqlDataAdapter(cmdeeed);
        DataTable dteeed = new DataTable();
        adpeeed.Fill(dteeed);
        return dteeed;
    }
    protected void FillddlParty()
    {
        MasterCls clsMaster = new MasterCls();
        DataTable dt = new DataTable();
        dt = clsMaster.SelectPartyMasterWithoutEmployeeType(ddlbusiness.SelectedValue);
        ddlparty.DataSource = dt;
        ddlparty.DataBind();
        ddlparty.Items.Insert(0, "-Select-");
        ddlparty.SelectedItem.Value = "0";
    }
    protected void FillRuleGrid(string sortExp, string sortDir)
    {
        DataTable DtMain = new DataTable();
        lblcompany.Text = Session["Cname"].ToString();
        lblcomname.Text = ddlbusiness.SelectedItem.Text;
        lblcabddff.Text = ddltypeofdoc.SelectedItem.Text;
        lblpartyname.Text = ddlparty.SelectedItem.Text;
       lblApp.Text = DdlRuleName.SelectedItem.Text;
        lblst.Text = ddlstatus.SelectedItem.Text;
        lblser.Text = "Search " + RadioButtonList1.SelectedItem.Text + " : " + TextBox1.Text;
        lblser.Visible = false;
        DataColumn dtcom1 = new DataColumn();
        dtcom1.DataType = System.Type.GetType("System.String");
        dtcom1.ColumnName = "DocId";
        dtcom1.ReadOnly = false;
        dtcom1.Unique = false;
        dtcom1.AllowDBNull = true;

        DtMain.Columns.Add(dtcom1);
        DataColumn dtcom2 = new DataColumn();
        dtcom2.DataType = System.Type.GetType("System.String");
        dtcom2.ColumnName = "DocumentTitle";
        dtcom2.ReadOnly = false;
        dtcom2.Unique = false;
        dtcom2.AllowDBNull = true;
        DtMain.Columns.Add(dtcom2);

        DataColumn dtcom3 = new DataColumn();
        dtcom3.DataType = System.Type.GetType("System.String");
        dtcom3.ColumnName = "ProcessDate";

        dtcom3.ReadOnly = false;
        dtcom3.Unique = false;
        dtcom3.AllowDBNull = true;
        DtMain.Columns.Add(dtcom3);

        //DataColumn dtcom4 = new DataColumn();
        //dtcom4.DataType = System.Type.GetType("System.String");
        //dtcom4.ColumnName = "RuleApproveTypeName";
        //dtcom4.ReadOnly = false;
        //dtcom4.Unique = false;
        //dtcom4.AllowDBNull = true;
        //DtMain.Columns.Add(dtcom4);

        DataColumn dtcom5 = new DataColumn();
        dtcom5.DataType = System.Type.GetType("System.String");
        dtcom5.ColumnName = "RuleId";
        dtcom5.ReadOnly = false;
        dtcom5.Unique = false;
        dtcom5.AllowDBNull = true;
        DtMain.Columns.Add(dtcom5);

        DataColumn dtcom6 = new DataColumn();
        dtcom6.DataType = System.Type.GetType("System.String");
        dtcom6.ColumnName = "RuleDetailId";
        dtcom6.ReadOnly = false;
        dtcom6.Unique = false;
        dtcom6.AllowDBNull = true;
        DtMain.Columns.Add(dtcom6);
        DataColumn dtcom7 = new DataColumn();
        dtcom7.DataType = System.Type.GetType("System.String");
        dtcom7.ColumnName = "RuleTitle";
        dtcom7.ReadOnly = false;
        dtcom7.Unique = false;
        dtcom7.AllowDBNull = true;
        DtMain.Columns.Add(dtcom7);
        //

        DataColumn dtcom8 = new DataColumn();
        dtcom8.DataType = System.Type.GetType("System.String");
        dtcom8.ColumnName = "RuleTypeName";
        dtcom8.ReadOnly = false;
        dtcom8.Unique = false;
        dtcom8.AllowDBNull = true;
        DtMain.Columns.Add(dtcom8);


        DataColumn dtcom9 = new DataColumn();
        dtcom9.DataType = System.Type.GetType("System.String");
        dtcom9.ColumnName = "ConditionTypeName";
        dtcom9.ReadOnly = false;
        dtcom9.Unique = false;
        dtcom9.AllowDBNull = true;
        DtMain.Columns.Add(dtcom9);

        if (Session["EmployeeId"] != null)
        {
            string para = "";
            para="RuleMasterforParty.Whid='"+ddlbusiness.SelectedValue+"'";
            if (ddltypeofdoc.SelectedIndex > 0)
            {
                para+=" and DocumentMaster.DocumentTypeId='"+ddltypeofdoc.SelectedValue+"'";
            }
            if (DdlRuleName.SelectedIndex > 0)
            {
                para += " and RuleMasterforParty.RuleId='" + DdlRuleName.SelectedValue + "'";

            }
            if (ddlparty.SelectedIndex > 0)
            {
                para += " and DocumentMaster.PartyId='" + ddlparty.SelectedValue + "'";

            }

            if (ddlstatus.SelectedIndex > 0)
            {
                if (ddlstatus.SelectedIndex == 2)
                {
                    para += " and DocumentMaster.DocumentId not in(Select RuleProcessMasterforParty.DocumentId from RuleProcessMasterforParty where CID='" + Session["comid"] + "' and RuleProcessMasterforParty.Approve='" + Convert.ToBoolean(ddlstatus.SelectedValue) + "')";
                 }
                else if(ddlstatus.SelectedIndex == 1)
                {
                    para += " and RuleProcessMasterforParty.Approve='" + Convert.ToBoolean(ddlstatus.SelectedValue) + "'";
     
                }
            }
           
                          
              dt = select("SELECT Distinct DocumentMaster.DocumentId, DocumentMaster.DocumentTypeId, DocumentMaster.DocumentUploadTypeId, DocumentMaster.DocumentUploadDate, DocumentMaster.DocumentName, DocumentMaster.DocumentTitle, DocumentMaster.Description, DocumentMaster.PartyId, DocumentMaster.DocumentRefNo,    DocumentMaster.DocumentAmount, DocumentMaster.EmployeeId, RuleMasterforParty.RuleId, RuleMasterforParty.RuleTypeId, RuleMasterforParty.DocumentTypeId AS Expr1,   RuleMasterforParty.RuleDate, RuleMasterforParty.RuleTitle, RuleMasterforParty.ConditionTypeId, RuleApproveTypeMaster.RuleApproveTypeId,    RuleApproveTypeMaster.RuleApproveTypeName, RuleTypeMaster.RuleTypeId AS Expr2, RuleTypeMaster.RuleType as RuleTypeName  ,CASE RuleMasterforParty.ConditionTypeId WHEN '1' THEN 'Step Wise in Serial Order' WHEN '2' THEN 'Simultaneous to All' END AS ConditionTypeName  FROM    RuleApproveTypeMaster RIGHT OUTER JOIN  RuleTypeMaster ON RuleApproveTypeMaster.RuleApproveTypeId = RuleTypeMaster.RuleTypeId RIGHT OUTER JOIN  RuleMasterforParty ON RuleTypeMaster.RuleTypeId = RuleMasterforParty.RuleTypeId INNER JOIN DocumentMainType iNNER JOIN DocumentSubType INNER JOIN DocumentType  INNER JOIN DocumentMaster oN DocumentMaster.DocumentTypeId=DocumentType.DocumentTypeId oN DocumentType.DocumentSubTypeId=DocumentSubType.DocumentSubTypeId oN DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId ON RuleMasterforParty.DocumentMainId = DocumentMainType.DocumentMainTypeId Left Outer JOIN RuleDetailforParty ON RuleDetailforParty.RuleId=RuleMasterforParty.RuleId Left Outer Join  RuleProcessMasterforParty ON RuleProcessMasterforParty.RuleDetailId=RuleDetailforParty.RuleDetailId   WHERE " +
               "" + para + "  and  (DocumentMaster.DocumentTypeId  IN   (SELECT     DocumentMaster_2.DocumentTypeId  FROM          DocumentMaster AS DocumentMaster_2 RIGHT OUTER JOIN   DocumentProcessing ON DocumentMaster_2.DocumentId = DocumentProcessing.DocumentId   WHERE      (DocumentProcessing.Approve = 1) AND (DocumentProcessing.DocumentId NOT IN  (SELECT     DocumentProcessing_1.DocumentId  FROM    DesignationMaster AS DesignationMaster_1 RIGHT OUTER JOIN   EmployeeMaster AS EmployeeMaster_1 ON DesignationMaster_1.DesignationMasterID = EmployeeMaster_1.DesignationMasterId RIGHT OUTER JOIN   DocumentProcessing AS DocumentProcessing_1 ON   EmployeeMaster_1.EmployeeMasterID = DocumentProcessing_1.EmployeeId LEFT OUTER JOIN  DocumentMaster AS DocumentMaster_1 ON DocumentProcessing_1.DocumentId = DocumentMaster_1.DocumentId LEFT OUTER JOIN  Party_master AS PartyMaster_1 ON DocumentMaster_1.PartyId = PartyMaster_1.PartyId  WHERE      (DocumentProcessing_1.Approve IS NULL) OR (DocumentProcessing_1.Approve = 0)))))  ");
                       

            Int32 EmpId = Convert.ToInt32(Session["EmployeeId"]);
            //if (ddltypeofdoc.SelectedIndex > 0)
            //{
            //    if (DdlRuleName.SelectedIndex > 0)
            //    {
            //        if (ddlparty.SelectedIndex > 0)
            //        {
            //            if (ddlstatus.SelectedIndex > 0)
            //            {
            //                dt = clsInstruction.SelectRuleDetailAllRuleIdwiseDocTypeIdWiseStatusWiseforParty(Convert.ToInt32(DdlRuleName.SelectedItem.Value.ToString()), Convert.ToInt32(ddltypeofdoc.SelectedItem.Value.ToString()), Convert.ToInt32(ddlparty.SelectedValue), Convert.ToBoolean(ddlstatus.SelectedValue));

            //                dt = select("SELECT DocumentMaster.DocumentId, DocumentMaster.DocumentTypeId, DocumentMaster.DocumentUploadTypeId, DocumentMaster.DocumentUploadDate, DocumentMaster.DocumentName, DocumentMaster.DocumentTitle, DocumentMaster.Description, DocumentMaster.PartyId, DocumentMaster.DocumentRefNo,    DocumentMaster.DocumentAmount, DocumentMaster.EmployeeId, RuleMasterforParty.RuleId, RuleMasterforParty.RuleTypeId, RuleMasterforParty.DocumentTypeId AS Expr1,   RuleMasterforParty.RuleDate, RuleMasterforParty.RuleTitle, RuleMasterforParty.ConditionTypeId, RuleApproveTypeMaster.RuleApproveTypeId,    RuleApproveTypeMaster.RuleApproveTypeName, RuleTypeMaster.RuleTypeId AS Expr2, RuleTypeMaster.RuleType as RuleTypeName  ,CASE RuleMasterforParty.ConditionTypeId WHEN '1' THEN 'Step Wise in Serial Order' WHEN '2' THEN 'Simultaneous to All' END AS ConditionTypeName  FROM    RuleApproveTypeMaster RIGHT OUTER JOIN  RuleTypeMaster ON RuleApproveTypeMaster.RuleApproveTypeId = RuleTypeMaster.RuleTypeId RIGHT OUTER JOIN  RuleMasterforParty ON RuleTypeMaster.RuleTypeId = RuleMasterforParty.RuleTypeId LEFT OUTER JOIN  DocumentMaster ON RuleMasterforParty.DocumentTypeId = DocumentMaster.DocumentTypeId Left Outer JOIN RuleDetailforParty ON RuleDetailforParty.RuleId=RuleMasterforParty.RuleId Left Outer Join  RuleProcessMasterforParty ON RuleProcessMasterforParty.RuleDetailId=RuleDetailforParty.RuleDetailId  WHERE "+
            //                    " RuleMasterforParty.RuleId=@RuleId  and DocumentMaster.DocumentTypeId=@DocumentTypeId and RuleMasterforParty.CID=@CID and DocumentMaster.PartyId=@PartyId and RuleProcessMasterforParty.Approve=@Approve  and  (RuleMasterforParty.DocumentTypeId IN   (SELECT     DocumentMaster_2.DocumentTypeId  FROM          DocumentMaster AS DocumentMaster_2 RIGHT OUTER JOIN   DocumentProcessing ON DocumentMaster_2.DocumentId = DocumentProcessing.DocumentId   WHERE      (DocumentProcessing.Approve = 1) AND (DocumentProcessing.DocumentId NOT IN  (SELECT     DocumentProcessing_1.DocumentId  FROM    DesignationMaster AS DesignationMaster_1 RIGHT OUTER JOIN   EmployeeMaster AS EmployeeMaster_1 ON DesignationMaster_1.DesignationMasterID = EmployeeMaster_1.DesignationMasterId RIGHT OUTER JOIN   DocumentProcessing AS DocumentProcessing_1 ON   EmployeeMaster_1.EmployeeMasterID = DocumentProcessing_1.EmployeeId LEFT OUTER JOIN  DocumentMaster AS DocumentMaster_1 ON DocumentProcessing_1.DocumentId = DocumentMaster_1.DocumentId LEFT OUTER JOIN  Party_master AS PartyMaster_1 ON DocumentMaster_1.PartyId = PartyMaster_1.PartyId  WHERE      (DocumentProcessing_1.Approve IS NULL) OR (DocumentProcessing_1.Approve = 0)))))  ");
            //            }
            //            else
            //            {
            //                dt = clsInstruction.SelectRuleDetailAllRuleIdwiseDocTypeIdWisePartyWiseforParty(Convert.ToInt32(DdlRuleName.SelectedItem.Value.ToString()), Convert.ToInt32(ddltypeofdoc.SelectedItem.Value.ToString()), Convert.ToInt32(ddlparty.SelectedValue));
            //            }
            //        }
            //        else
            //        {

            //            dt = clsInstruction.SelectRuleDetailAllRuleIdwiseDocTypeIdWiseforParty(Convert.ToInt32(DdlRuleName.SelectedItem.Value.ToString()), Convert.ToInt32(ddltypeofdoc.SelectedItem.Value.ToString()));
            //        }
            //    }
            //    //else if (ddlruletype.SelectedIndex > 0)
            //    //{
            //    //    dt = clsInstruction.SelectRuleDetailAllRuleTypeIdwiseDocTypeIdWiseforParty(Convert.ToInt32(ddlruletype.SelectedItem.Value.ToString()), Convert.ToInt32(ddltypeofdoc.SelectedItem.Value.ToString()));
            //    //}
                
            //}
            //else if (ddlparty.SelectedIndex > 0 || ddltypeofdoc.SelectedIndex > 0)
            //{
            //    if (ddlparty.SelectedIndex > 0)
            //    {
            //        dt = clsInstruction.SelectRuleDetailAllDocTypeIdWisePartywiseforParty(Convert.ToInt32(ddltypeofdoc.SelectedItem.Value.ToString()), Convert.ToInt32(ddlparty.SelectedValue));
            //    }
            //    else 
            //    {
            //        dt = clsInstruction.SelectRuleDetailAllDocTypeIdWiseforParty(Convert.ToInt32(ddltypeofdoc.SelectedItem.Value.ToString()));
            //    }

            //}
            //else
            //{
            //    if (DdlRuleName.SelectedIndex > 0)
            //    {
            //        dt = clsInstruction.SelectRuleDetailAllRuleIdwiseforParty(Convert.ToInt32(DdlRuleName.SelectedItem.Value.ToString()));

            //    }
            //    //else if (ddlruletype.SelectedIndex > 0)
            //    //{
            //    //    dt = clsInstruction.SelectRuleDetailAllRuleTypeIdwiseforParty(Convert.ToInt32(ddlruletype.SelectedItem.Value.ToString()));

            //    //}
            //    else
            //    {
            //        dt = clsInstruction.SelectRuleDetailAllforParty(ddlbusiness.SelectedValue); // (Convert.ToInt32(ddltypeofdoc.SelectedItem.Value.ToString()));

            //    }
            //}
            //  dt = clsInstruction.SelectRuleDetailAll(); // Get Collection of File with Rules
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow DtMainRow = DtMain.NewRow();
                    DtMainRow["DocId"] = dr["DocumentId"].ToString();
                    DtMainRow["DocumentTitle"] = dr["DocumentTitle"].ToString();
                    DtMainRow["ProcessDate"] = Convert.ToString("RecentTime");
                    DtMainRow["RuleId"] = dr["RuleId"].ToString();
                    DtMainRow["RuleDetailId"] = "34"; // dr["RuleDetailId"].ToString();
                    DtMainRow["RuleTitle"] = dr["RuleTitle"].ToString();
                    DtMainRow["RuleTypeName"] = dr["RuleTypeName"].ToString();
                    DtMainRow["ConditionTypeName"] = dr["ConditionTypeName"].ToString();
                    DtMain.Rows.Add(DtMainRow);

                }

            }



            DataView myDataView = new DataView();
            myDataView = DtMain.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            GridView1.DataSource = DtMain;
            GridView1.DataBind();
        }
        else
        {
            Response.Redirect("~/Shoppingcart/Admin/Shoppingcartlogin.aspx");
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ViewState["FinalStatus"] = "";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            lblcompany.Text = Session["Cname"].ToString();
            lblcomname.Text = ddlbusiness.SelectedItem.Text;
            //lblApp.Text = ddlapprule.SelectedItem.Text;
            lblst.Text = ddlstatus.SelectedItem.Text;
            DataTable DtDetail = new DataTable();
            DataRow dr = DtDetail.NewRow();
            DataRow DtMainRow1 = DtDetail.NewRow();

            DateTime ProcessDt = System.DateTime.Now;
            double Days = 0;
            DateTime RecentTime;
            RecentTime = DateTime.Now;
            //
            DataColumn dtcom1 = new DataColumn();
            dtcom1.DataType = System.Type.GetType("System.String");
            dtcom1.ColumnName = "StepNo";
            dtcom1.ReadOnly = false;
            dtcom1.Unique = false;
            dtcom1.AllowDBNull = true;
            DtDetail.Columns.Add(dtcom1);
            DataColumn dtcom2 = new DataColumn();
            dtcom2.DataType = System.Type.GetType("System.String");
            dtcom2.ColumnName = "ApprovalReqDate";
            dtcom2.ReadOnly = false;
            dtcom2.Unique = false;
            dtcom2.AllowDBNull = true;
            DtDetail.Columns.Add(dtcom2);
            DataColumn dtcom3 = new DataColumn();
            dtcom3.DataType = System.Type.GetType("System.String");
            dtcom3.ColumnName = "ApprovedDate";
            dtcom3.ReadOnly = false;
            dtcom3.Unique = false;
            dtcom3.AllowDBNull = true;
            DtDetail.Columns.Add(dtcom3);
            DataColumn dtcom4 = new DataColumn();
            dtcom4.DataType = System.Type.GetType("System.String");
            dtcom4.ColumnName = "ApprovalStatus";
            dtcom4.ReadOnly = false;
            dtcom4.Unique = false;
            dtcom4.AllowDBNull = true;
            DtDetail.Columns.Add(dtcom4);
            DataColumn dtcom5 = new DataColumn();
            dtcom5.DataType = System.Type.GetType("System.String");
            dtcom5.ColumnName = "PartyName";
            dtcom5.ReadOnly = false;
            dtcom5.Unique = false;
            dtcom5.AllowDBNull = true;
            DtDetail.Columns.Add(dtcom5);
            DataColumn dtcom6 = new DataColumn();
            dtcom6.DataType = System.Type.GetType("System.String");
            dtcom6.ColumnName = "ApprovalType";
            dtcom6.ReadOnly = false;
            dtcom6.Unique = false;
            dtcom6.AllowDBNull = true;
            DtDetail.Columns.Add(dtcom6);

            DataColumn dtcom7 = new DataColumn();
            dtcom7.DataType = System.Type.GetType("System.String");
            dtcom7.ColumnName = "AnyAll";
            dtcom7.ReadOnly = false;
            dtcom7.Unique = false;
            dtcom7.AllowDBNull = true;
            DtDetail.Columns.Add(dtcom7);
            //

            DataColumn dtcom8 = new DataColumn();
            dtcom8.DataType = System.Type.GetType("System.String");
            dtcom8.ColumnName = "DocId";
            dtcom8.ReadOnly = false;
            dtcom8.Unique = false;
            dtcom8.AllowDBNull = true;
            DtDetail.Columns.Add(dtcom8);

            DataColumn dtcom9 = new DataColumn();
            dtcom9.DataType = System.Type.GetType("System.String");
            dtcom9.ColumnName = "PartyId";
            dtcom9.ReadOnly = false;
            dtcom9.Unique = false;
            dtcom9.AllowDBNull = true;
            DtDetail.Columns.Add(dtcom9);
            //

            int RuleId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RuleId").ToString());
            DataTable dtr = new DataTable();
            DataTable dtDocProcess = new DataTable();
            DataTable dtstep = new DataTable();
            dtstep = clsInstruction.SelectRuleDetailStepforParty(Convert.ToInt32(RuleId));
            if (dtstep.Rows.Count > 0)
            {
                foreach (DataRow Drdtstep in dtstep.Rows) // Get Steps in RUle
                {
                    Int32 StepinProcess = Convert.ToInt32(Drdtstep["StepId"].ToString());

                    DataTable DtEmpSelforLess = new DataTable();
                    DtEmpSelforLess = clsInstruction.SelectRulePartySelectionMaster(Convert.ToInt32(RuleId), StepinProcess);
                    if (DtEmpSelforLess.Rows.Count > 0)
                    {

                        if (DtEmpSelforLess.Rows[0]["PartySelectionId"].ToString() == "1") // Any
                        {
                            dtDocProcess = clsInstruction.SelectRuleDetailRuleIdwiseStepWiseforParty(Convert.ToInt32(RuleId), Convert.ToInt32(Drdtstep["StepId"]));
                            if (dtDocProcess.Rows.Count > 0)
                            {
                                //
                                DataTable dtDocProcess1 = new DataTable();
                                dtDocProcess1 = clsDocument.SelectProcessingDocumentbyDocIdwise(Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DocId").ToString()));


                                if (dtDocProcess1.Rows.Count > 0)
                                {
                                    if ((dtDocProcess1.Rows[0]["ApproveDate"] != System.DBNull.Value) || (dtDocProcess1.Rows[0]["ApproveDate"].ToString() != ""))
                                    {
                                        ProcessDt = Convert.ToDateTime(dtDocProcess1.Rows[0]["ApproveDate"].ToString());
                                    }
                                    Days = Convert.ToDouble(dtDocProcess.Rows[0]["Days"].ToString());
                                    RecentTime = ProcessDt.AddDays(Days);
                                }

                                bool donebyany1 = false;

                                foreach (DataRow DrDocProcess in dtDocProcess.Rows)
                                {
                                    DataTable LastEmpProcess = new DataTable();
                                    LastEmpProcess = clsInstruction.SelectRuleProcessMasterDocIdWiseRuleIdWiseRuleDetailIdwiseforParty(Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DocId").ToString()), Convert.ToInt32(DrDocProcess["RuleDetailId"]));
                                    if (LastEmpProcess.Rows.Count > 0)
                                    {
                                        DtMainRow1 = DtDetail.NewRow();
                                        DtMainRow1["PartyName"] = DrDocProcess["PartyName"].ToString();
                                        DtMainRow1["AnyAll"] = "Any"; // dtDocProcess.Rows[0]["EmployeeName"].ToString();

                                        DtMainRow1["StepNo"] = DrDocProcess["StepId"].ToString();
                                        DtMainRow1["ApprovalType"] = DrDocProcess["RuleApproveTypeName"].ToString();
                                        DtMainRow1["ApprovalReqDate"] = RecentTime.ToShortDateString(); // RecentTime.ToShortDateString();
                                        //  DtMainRow1["ApprovalReqDate"] = LastEmpProcess.Rows[0]["RuleProcessDate"].ToString(); //["DocumentTitle"].ToString();
                                        DtMainRow1["ApprovedDate"] = LastEmpProcess.Rows[0]["RuleProcessDate"].ToString(); //["DocumentTitle"].ToString();
                                        DtMainRow1["ApprovalStatus"] = Convert.ToString("Approved");
                                        DtMainRow1["DocId"] = DataBinder.Eval(e.Row.DataItem, "DocId");
                                        DtMainRow1["PartyId"] = DrDocProcess["PartyId"].ToString();

                                        DtDetail.Rows.Add(DtMainRow1);
                                        donebyany1 = true;
                                        // if there then green 
                                        // Add data with requied date approve date etc  color info etc
                                    }
                                    else
                                    {
                                    }
                                }
                                if (donebyany1 != true)
                                {
                                    foreach (DataRow DrDocProcess in dtDocProcess.Rows)
                                    {
                                        DtMainRow1 = DtDetail.NewRow();
                                        DtMainRow1["PartyName"] = DrDocProcess["PartyName"].ToString();
                                        DtMainRow1["StepNo"] = DrDocProcess["StepId"].ToString();
                                        DtMainRow1["ApprovalType"] = DrDocProcess["RuleApproveTypeName"].ToString();
                                        DtMainRow1["AnyAll"] = "Any";
                                        //   DtMainRow1["ApprovalReqDate"] = RecentTime.ToShortDateString();
                                        DtMainRow1["ApprovalReqDate"] = RecentTime.ToShortDateString(); // date LastEmpProcess.Rows[0]["RuleProcessDate"].ToString(); //["DocumentTitle"].ToString();
                                        DtMainRow1["ApprovedDate"] = "";// LastEmpProcess.Rows[0]["RuleProcessDate"].ToString(); //["DocumentTitle"].ToString();
                                        DtMainRow1["ApprovalStatus"] = "Pending"; // Convert.ToString("Approved");
                                        ViewState["FinalStatus"] = "Pending";
                                        DtMainRow1["DocId"] = DataBinder.Eval(e.Row.DataItem, "DocId");
                                        DtMainRow1["PartyId"] = DrDocProcess["PartyId"].ToString();
                                        DtDetail.Rows.Add(DtMainRow1);
                                    }
                                }

                            }

                        }
                        else // All Emp
                        {
                            dtDocProcess = clsInstruction.SelectRuleDetailRuleIdwiseStepWiseforParty(Convert.ToInt32(RuleId), Convert.ToInt32(Drdtstep["StepId"]));
                            if (dtDocProcess.Rows.Count > 0)
                            {
                                //
                                DataTable dtDocProcess1 = new DataTable();
                                dtDocProcess1 = clsDocument.SelectProcessingDocumentbyDocIdwise(Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DocId").ToString()));


                                if (dtDocProcess1.Rows.Count > 0)
                                {
                                    if ((dtDocProcess1.Rows[0]["ApproveDate"] != System.DBNull.Value) || (dtDocProcess1.Rows[0]["ApproveDate"].ToString() != ""))
                                    {
                                        ProcessDt = Convert.ToDateTime(dtDocProcess1.Rows[0]["ApproveDate"].ToString());
                                    }
                                    Days = Convert.ToDouble(dtDocProcess.Rows[0]["Days"].ToString());
                                    RecentTime = ProcessDt.AddDays(Days);
                                }


                                bool donebyany1 = false;

                                foreach (DataRow DrDocProcess in dtDocProcess.Rows)
                                {
                                    DataTable LastEmpProcess = new DataTable();
                                    LastEmpProcess = clsInstruction.SelectRuleProcessMasterDocIdWiseRuleIdWiseRuleDetailIdwiseforParty(Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DocId").ToString()), Convert.ToInt32(DrDocProcess["RuleDetailId"]));
                                    if (LastEmpProcess.Rows.Count > 0)
                                    {
                                        DtMainRow1 = DtDetail.NewRow();
                                        DtMainRow1["PartyName"] = DrDocProcess["PartyName"].ToString();
                                        DtMainRow1["StepNo"] = DrDocProcess["StepId"].ToString();
                                        DtMainRow1["ApprovalReqDate"] = RecentTime.ToShortDateString();
                                        DtMainRow1["ApprovalType"] = DrDocProcess["RuleApproveTypeName"].ToString();
                                        DtMainRow1["AnyAll"] = "All";
                                        // DtMainRow1["ApprovalReqDate"] = LastEmpProcess.Rows[0]["RuleProcessDate"].ToString(); //["DocumentTitle"].ToString();
                                        DtMainRow1["ApprovedDate"] = LastEmpProcess.Rows[0]["RuleProcessDate"].ToString(); //["DocumentTitle"].ToString();
                                        DtMainRow1["ApprovalStatus"] = Convert.ToString("Approved");
                                        DtMainRow1["DocId"] = DataBinder.Eval(e.Row.DataItem, "DocId");
                                        DtMainRow1["PartyId"] = DrDocProcess["PartyId"].ToString();
                                        DtDetail.Rows.Add(DtMainRow1);
                                        donebyany1 = true;
                                        // if there then green 
                                        // Add data with requied date approve date etc  color info etc
                                    }
                                    else
                                    {
                                        DtMainRow1 = DtDetail.NewRow();
                                        DtMainRow1["PartyName"] = DrDocProcess["PartyName"].ToString();
                                        DtMainRow1["StepNo"] = DrDocProcess["StepId"].ToString();
                                        DtMainRow1["ApprovalReqDate"] = RecentTime.ToShortDateString();
                                        DtMainRow1["ApprovalType"] = DrDocProcess["RuleApproveTypeName"].ToString();
                                        DtMainRow1["AnyAll"] = "All";
                                        //   DtMainRow1["ApprovalReqDate"] = LastEmpProcess.Rows[0]["RuleProcessDate"].ToString(); //["DocumentTitle"].ToString();
                                        DtMainRow1["ApprovedDate"] = "";// LastEmpProcess.Rows[0]["RuleProcessDate"].ToString(); //["DocumentTitle"].ToString();
                                        DtMainRow1["ApprovalStatus"] = Convert.ToString("Pending");
                                        DtMainRow1["DocId"] = DataBinder.Eval(e.Row.DataItem, "DocId");
                                        DtMainRow1["PartyId"] = DrDocProcess["PartyId"].ToString();
                                        ViewState["FinalStatus"] = "Pending";
                                        DtDetail.Rows.Add(DtMainRow1);
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        dtDocProcess = clsInstruction.SelectRuleDetailRuleIdwiseStepWiseforParty(Convert.ToInt32(RuleId), Convert.ToInt32(Drdtstep["StepId"]));
                        if (dtDocProcess.Rows.Count > 0)
                        {
                            //
                            DataTable dtDocProcess1 = new DataTable();
                            dtDocProcess1 = clsDocument.SelectProcessingDocumentbyDocIdwise(Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DocId").ToString()));


                            if (dtDocProcess1.Rows.Count > 0)
                            {
                                if ((dtDocProcess1.Rows[0]["ApproveDate"] != System.DBNull.Value) || (dtDocProcess1.Rows[0]["ApproveDate"].ToString() != ""))
                                {
                                    ProcessDt = Convert.ToDateTime(dtDocProcess1.Rows[0]["ApproveDate"].ToString());
                                }
                                Days = Convert.ToDouble(dtDocProcess.Rows[0]["Days"].ToString());
                                RecentTime = ProcessDt.AddDays(Days);
                            }

                            foreach (DataRow DrDocProcess in dtDocProcess.Rows)
                            {
                                // DataRow dr = new DataRow(); // DtDetail.NewRow();
                                dr = DtDetail.NewRow();
                                //  DataRow DtMainRow1 = DtDetail.NewRow();
                                dr["PartyName"] = DrDocProcess["PartyName"].ToString();
                                dr["StepNo"] = DrDocProcess["StepId"].ToString();
                                dr["ApprovalType"] = DrDocProcess["RuleApproveTypeName"].ToString();
                                dr["AnyAll"] = "";
                                dr["DocId"] = DataBinder.Eval(e.Row.DataItem, "DocId");
                                dr["PartyId"] = DrDocProcess["PartyId"].ToString();
                                dr["ApprovalReqDate"] = RecentTime.ToShortDateString();
                                DataTable LastEmpProcess = new DataTable();
                                LastEmpProcess = clsInstruction.SelectRuleProcessMasterDocIdWiseRuleIdWiseRuleDetailIdwiseforParty(Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DocId")), Convert.ToInt32(DrDocProcess["RuleDetailId"]));
                                if (LastEmpProcess.Rows.Count > 0)
                                {
                                    // dr["ApprovalReqDate"] = LastEmpProcess.Rows[0]["RuleProcessDate"].ToString(); //["DocumentTitle"].ToString();
                                    dr["ApprovedDate"] = LastEmpProcess.Rows[0]["RuleProcessDate"].ToString(); //["DocumentTitle"].ToString();
                                    dr["ApprovalStatus"] = Convert.ToString("Approved");
                                    if (Convert.ToBoolean(LastEmpProcess.Rows[0]["Approve"]) == true)
                                    {
                                        dr["ApprovalStatus"] = Convert.ToString("Approved");
                                        ViewState["FinalStatus"] = "Approved";
                                    }
                                    else
                                    {
                                        dr["ApprovalStatus"] = Convert.ToString("Rejected");
                                        ViewState["FinalStatus"] = "Rejected";
                                    }
                                    // if there then green 
                                    // Add data with requied date approve date etc  color info etc
                                }
                                else
                                {
                                    // other wise Red 
                                    //  DtMainRow1["ApprovalReqDate"] = ""; // LastEmpProcess.Rows[0]["RuleProcessDate"].ToString(); //["DocumentTitle"].ToString();
                                    dr["ApprovedDate"] = ""; // LastEmpProcess.Rows[0]["RuleProcessDate"].ToString(); //["DocumentTitle"].ToString();
                                    dr["ApprovalStatus"] = Convert.ToString("Pending");
                                    ViewState["FinalStatus"] = "Pending";
                                    //  DtDetail.Rows.Add(DtMainRow1);
                                }
                                DtDetail.Rows.Add(dr);
                            }
                        }
                    }
                }
            }
            DataList gridDetail1 = (DataList)e.Row.FindControl("DRuleDetail");
            ViewState["StepNo"] = "";
            gridDetail1.DataSource = DtDetail;
            gridDetail1.DataBind();
            Image imgfinalstatus = (Image)e.Row.FindControl("imgfinalstatus");

            Label lblFinalStatus = (Label)e.Row.FindControl("lblFinalStatus");
            if (ViewState["FinalStatus"].ToString().Length > 0)
            {
                if (Convert.ToString(ViewState["FinalStatus"]) == "Rejected")
                {
                    imgfinalstatus.ImageUrl = "~/Account/images/closeicon.png";
                }
                else if (Convert.ToString(ViewState["FinalStatus"]) == "Approved")
                {
                    imgfinalstatus.ImageUrl = "~/Account/images/Right.jpg";
                }
                else
                {
                    imgfinalstatus.ImageUrl = "~/Account/images/Datapending.jpg";
                    //  lblFinalStatus.Text = "Approved";
                }
            }
            //  lblempname.Text ="<b>" + (DataBinder.Eval(e.Item.DataItem, "EmployeeName").ToString()) + "</b>";

        }
    }
    protected void ddltypeofdoc_SelectedIndexChanged(object sender, EventArgs e)
    {
       // FillRuleGrid("", "");
    }


    protected void DdlRuleName_SelectedIndexChanged(object sender, EventArgs e)
    {
       // FillRuleGrid("", "");
    }





    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;

        if (lblser.Visible != true)
        {
            FillRuleGrid("", "");
        }
        else if (lblser.Visible == true)
        {

            ImageButton1_Click1(sender, e);
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
    /*protected void ddlruletype_SelectedIndexChanged(object sender, EventArgs e)
    {
        dt = new DataTable();
        dt = clsInstruction.SelectRuleMasterRuleTypeWise(Convert.ToInt32(ddlruletype.SelectedValue.ToString()));
        DdlRuleName.DataSource = dt;
        DdlRuleName.DataBind();
        DdlRuleName.Items.Insert(0, "- All - ");
        DdlRuleName.SelectedItem.Value = "0";
        //  SetFillGrid();
        FillRuleGrid("", "");
    }*/

    protected void GridView1_Sorting1(object sender, GridViewSortEventArgs e)
    {
        //   SetFillGridSorting(e.SortExpression, sortOrder);
    }
    protected void DRuleDetail_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        //if (e.Item.ItemType == ListItemType.Item)

        //{ 
        Label lblempname = (Label)e.Item.FindControl("lblRuleDetail1");
        lblempname.Text = "<b>" + (DataBinder.Eval(e.Item.DataItem, "PartyName").ToString()) + "</b>";
        Label lblstep = (Label)e.Item.FindControl("lblStep");
        lblstep.Text = (DataBinder.Eval(e.Item.DataItem, "StepNo").ToString()) + "    <b>" + (DataBinder.Eval(e.Item.DataItem, "AnyAll").ToString()) + "</b>";
        Label lblReqDate = (Label)e.Item.FindControl("lblReqDate");
        lblReqDate.Text = (DataBinder.Eval(e.Item.DataItem, "ApprovalReqDate").ToString());
        Label lblApprovedDate = (Label)e.Item.FindControl("lblApprovedDate");
        lblApprovedDate.Text = (DataBinder.Eval(e.Item.DataItem, "ApprovedDate").ToString());
        Label lblStatus = (Label)e.Item.FindControl("lblStatus");
        lblStatus.Text = (DataBinder.Eval(e.Item.DataItem, "ApprovalStatus").ToString());
        Label lblConType = (Label)e.Item.FindControl("lblApprovalType");
        lblConType.Text = (DataBinder.Eval(e.Item.DataItem, "ApprovalType").ToString());
        Panel pnlshow = (Panel)e.Item.FindControl("pnlDetail"); //pnlhd");        
        //if (DateTime.Now > Convert.ToDateTime( lblReqDate.Text ) ) // time over
        // {
        //   //  pnlshow.BackColor = System.Drawing.Color.Red;
        //     lblempname.BackColor = System.Drawing.Color.Red;
        // }
        Panel pnlHead = (Panel)e.Item.FindControl("pnlhd"); //pnlhd");
        int stepno = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "StepNo").ToString());
        if (stepno % 2 == 0)
        {
            // It's even
            pnlHead.BackColor = System.Drawing.Color.DarkKhaki;
        }
        else
        {
            // It's odd
            pnlHead.BackColor = System.Drawing.Color.PaleGoldenrod;
        }
        if (DateTime.Now > Convert.ToDateTime(lblReqDate.Text)) // time over
        {
            pnlHead.BackColor = System.Drawing.Color.Red;
            lblstep.ForeColor = System.Drawing.Color.White;
        }
        LinkButton lbtnmsglink = (LinkButton)e.Item.FindControl("msglink");
        lbtnmsglink.PostBackUrl = "MessageCompose.aspx?Docid=" + DataBinder.Eval(e.Item.DataItem, "DocId").ToString() + "&EmpId=" + DataBinder.Eval(e.Item.DataItem, "PartyId"); //<%# Eval("DocId") ;





        // msglink
        // href ="MessageCompose.aspx?<%# Eval("DocId") %>
    }
    /* protected void lbtnViewNote_Click(object sender, EventArgs e)
     {
         dt = new DataTable();
         clsDocument = new DocumentCls();

         dt = clsDocument.SelectDoucmentMasterByID(Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "EmployeeId"))); // (Convert.ToInt32(GridView1.SelectedRow.Cells[0].Text));
        // dt = clsSelectReminderMaster(Convert.ToDateTime(DateTime.Now.ToShortDateString()));
        // gridReminder.DataSource = dt;
       //  gridReminder.DataBind();
         if (dt.Rows.Count > 0)
         {
             lblReminderDetail.Text = dt.Rows[0]["Description"].ToString();
         }
         Modalpopupextender2.Show();
     }*/
    protected void ibtnReminderClose_Click(object sender, ImageClickEventArgs e)
    {
        Modalpopupextender2.Hide();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
    }
    protected void ddltypeofdoc_SelectedIndexChanged1(object sender, EventArgs e)
    {
       // FillRuleGrid("", "");
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void ImageButton1_Click1(object sender, EventArgs e)
    {
        FillRuleGridSearch("", "");

    }
    protected void FillRuleGridSearch(string sortExp, string sortDir)
    {
        DataTable DtMain = new DataTable();
        lblcompany.Text = Session["Cname"].ToString();
        lblcomname.Text = ddlbusiness.SelectedItem.Text;
        lblcabddff.Text = ddltypeofdoc.SelectedItem.Text;
        lblpartyname.Text = ddlparty.SelectedItem.Text;
        lblApp.Text = DdlRuleName.SelectedItem.Text;
        lblst.Text = ddlstatus.SelectedItem.Text;
        lblser.Text = "Search " + RadioButtonList1.SelectedItem.Text + " : " + TextBox1.Text;
        lblser.Visible = true;
        DataColumn dtcom1 = new DataColumn();
        dtcom1.DataType = System.Type.GetType("System.String");
        dtcom1.ColumnName = "DocId";
        dtcom1.ReadOnly = false;
        dtcom1.Unique = false;
        dtcom1.AllowDBNull = true;

        DtMain.Columns.Add(dtcom1);
        DataColumn dtcom2 = new DataColumn();
        dtcom2.DataType = System.Type.GetType("System.String");
        dtcom2.ColumnName = "DocumentTitle";
        dtcom2.ReadOnly = false;
        dtcom2.Unique = false;
        dtcom2.AllowDBNull = true;
        DtMain.Columns.Add(dtcom2);

        DataColumn dtcom3 = new DataColumn();
        dtcom3.DataType = System.Type.GetType("System.String");
        dtcom3.ColumnName = "ProcessDate";

        dtcom3.ReadOnly = false;
        dtcom3.Unique = false;
        dtcom3.AllowDBNull = true;
        DtMain.Columns.Add(dtcom3);

        //DataColumn dtcom4 = new DataColumn();
        //dtcom4.DataType = System.Type.GetType("System.String");
        //dtcom4.ColumnName = "RuleApproveTypeName";
        //dtcom4.ReadOnly = false;
        //dtcom4.Unique = false;
        //dtcom4.AllowDBNull = true;
        //DtMain.Columns.Add(dtcom4);

        DataColumn dtcom5 = new DataColumn();
        dtcom5.DataType = System.Type.GetType("System.String");
        dtcom5.ColumnName = "RuleId";
        dtcom5.ReadOnly = false;
        dtcom5.Unique = false;
        dtcom5.AllowDBNull = true;
        DtMain.Columns.Add(dtcom5);

        DataColumn dtcom6 = new DataColumn();
        dtcom6.DataType = System.Type.GetType("System.String");
        dtcom6.ColumnName = "RuleDetailId";
        dtcom6.ReadOnly = false;
        dtcom6.Unique = false;
        dtcom6.AllowDBNull = true;
        DtMain.Columns.Add(dtcom6);
        DataColumn dtcom7 = new DataColumn();
        dtcom7.DataType = System.Type.GetType("System.String");
        dtcom7.ColumnName = "RuleTitle";
        dtcom7.ReadOnly = false;
        dtcom7.Unique = false;
        dtcom7.AllowDBNull = true;
        DtMain.Columns.Add(dtcom7);
        //

        DataColumn dtcom8 = new DataColumn();
        dtcom8.DataType = System.Type.GetType("System.String");
        dtcom8.ColumnName = "RuleTypeName";
        dtcom8.ReadOnly = false;
        dtcom8.Unique = false;
        dtcom8.AllowDBNull = true;
        DtMain.Columns.Add(dtcom8);


        DataColumn dtcom9 = new DataColumn();
        dtcom9.DataType = System.Type.GetType("System.String");
        dtcom9.ColumnName = "ConditionTypeName";
        dtcom9.ReadOnly = false;
        dtcom9.Unique = false;
        dtcom9.AllowDBNull = true;
        DtMain.Columns.Add(dtcom9);

        if (Session["EmployeeId"] != null)
        {
            Int32 EmpId = Convert.ToInt32(Session["EmployeeId"]);

            if (RadioButtonList1.SelectedIndex == 0)
            {

                dt = clsInstruction.SelectRuleDetailAllDocIdWiseforParty(Convert.ToInt32(TextBox1.Text), ddlbusiness.SelectedValue); // (Convert.ToInt32(GridView1.SelectedRow.Cells[0].Text));
            }
            else
            {
                dt = clsInstruction.SelectRuleDetailAllDocNameWiseforParty(TextBox1.Text,ddlbusiness.SelectedValue);
            }
            //  dt = clsInstruction.SelectRuleDetailAll(); // Get Collection of File with Rules
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow DtMainRow = DtMain.NewRow();
                    DtMainRow["DocId"] = dr["DocumentId"].ToString();
                    DtMainRow["DocumentTitle"] = dr["DocumentTitle"].ToString();
                    DtMainRow["ProcessDate"] = Convert.ToString("RecentTime");
                    DtMainRow["RuleId"] = dr["RuleId"].ToString();
                   // DtMainRow["RuleDetailId"] =  dr["RuleDetailId"].ToString();
                    //DtMainRow["RuleDetailId"] = dr["RuleDetailId"].ToString();
                    DtMainRow["RuleTitle"] = dr["RuleTitle"].ToString();
                    DtMainRow["RuleTypeName"] = dr["RuleTypeName"].ToString();
                    DtMainRow["ConditionTypeName"] = dr["ConditionTypeName"].ToString();
                    DtMain.Rows.Add(DtMainRow);

                }

            }

            DataView myDataView = new DataView();
            myDataView = DtMain.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            GridView1.DataSource = DtMain;
            GridView1.DataBind();
        }
        else
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartlogin.aspx");
        }
    }
    protected void DRuleDetail_ItemCommand(object source, DataListCommandEventArgs e)
    {
        int Partyid = 0;
        int docid = 0;
        if (e.CommandName == "notes")
        {
            Partyid = Convert.ToInt32(e.CommandArgument);
            LinkButton lnk = (LinkButton)e.Item.FindControl("lbtnViewNote");
            docid = Convert.ToInt32(lnk.ToolTip.ToString());
        }

        DataTable aprvnote = new DataTable();
        aprvnote = clsInstruction.SelectApproveNotefromRuleProcessMasterforParty(docid, Partyid);
        lblReminderDetail.Text = "";
        if (aprvnote.Rows.Count > 0)
        {
            lblReminderDetail.Text = aprvnote.Rows[0]["Note"].ToString();
        }
        Modalpopupextender2.Show();
    }
    protected void ddlparty_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillRuleGrid("", "");
    }
    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDocumentType();
        FillddlParty();
       
        FillRuleTypeAllforParty();
        ViewState["sortOrder"] = "";
        //FillRuleGrid("", "");
    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillRuleGrid("", "");
    }
    protected void btngo_Click(object sender, EventArgs e)
    {
        FillRuleGrid("", "");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            pnlgridd.ScrollBars = ScrollBars.None;
            pnlgridd.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (GridView1.Columns[0].Visible == true)
            {
                ViewState["viewHide"] = "tt";
                GridView1.Columns[0].Visible = false;
            }
           
        }
        else
        {

            pnlgridd.ScrollBars = ScrollBars.Vertical;
            pnlgridd.Height = new Unit(200);

            Button1.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["viewHide"] != null)
            {
                GridView1.Columns[0].Visible = true;
            }            
        }
    }
}
