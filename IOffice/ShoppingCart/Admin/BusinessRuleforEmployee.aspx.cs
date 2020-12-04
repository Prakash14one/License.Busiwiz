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
using System.Text;
using System.Net.Mail;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using System.Net;

public partial class BusinessRuleforEmployee : System.Web.UI.Page
{
    MasterCls clsMaster = new MasterCls();
    PayrollCls clsPayroll = new PayrollCls();
    SqlConnection con;
    DataTable dt = new DataTable();
    InstructionCls clsInstruction = new InstructionCls();
    DocumentCls1 clsDocument = new DocumentCls1();
    EmployeeCls clsEmployee = new EmployeeCls();
    protected int DesignationId;
    public static string fildocidtit = "";
    object paramMissing = Type.Missing;
    public string errormessage;
    private bool wordavailable = false;
    private bool checkedword = false;
    public static string abcdy = "";
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

        DesignationId = Convert.ToInt32(Session["DesignationId"]);
        if (!Page.IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            pageMailAccess();
            DateTime startdayofweek = DateTime.Today.AddDays(-1 * (int)(DateTime.Today.DayOfWeek));
            txtdatefrom.Text = startdayofweek.ToShortDateString();
            DateTime endofweek = startdayofweek.AddDays(7);
            txtdateto.Text = endofweek.ToShortDateString();
            lblcompny.Text = "All";
            string str = "SELECT WareHouseId,Name,Address,CurrencyId  FROM WareHouseMaster where comid = '" + Session["comid"] + "'and WareHouseMaster.Status='" + 1 + "' order by name";

            SqlCommand cmd1 = new SqlCommand(str, con);
            cmd1.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlbusiness.DataSource = dt;
            ddlbusiness.DataTextField = "Name";
            ddlbusiness.DataValueField = "WareHouseId";
            ddlbusiness.DataBind();

            string strds = "SELECT EmployeeMaster.Whid  FROM WareHouseMaster inner join EmployeeMaster on EmployeeMaster.Whid= WareHouseMaster.WarehouseId where EmployeeMaster.EmployeeMasterId = '" + Session["EmployeeId"] + "'";

            SqlCommand cmd1ds = new SqlCommand(strds, con);
            cmd1ds.CommandType = CommandType.Text;
            SqlDataAdapter dads = new SqlDataAdapter(cmd1ds);
            DataTable dtds = new DataTable();
            dads.Fill(dtds);
            if (dtds.Rows.Count > 0)
            {
                ddlbusiness.SelectedIndex = ddlbusiness.Items.IndexOf(ddlbusiness.Items.FindByValue(dtds.Rows[0]["Whid"].ToString()));
            }
            fillapro();
            ViewState["sortOrder"] = "";
            FillRuleType();

            ImageButton1_Click1(sender, e);
        }

    }

    protected void fillapro()
    {
        ddlapprule.Items.Clear();
        string str = "Select Distinct RuleApproveTypeMaster.RuleApproveTypeId,RuleApproveTypeMaster.RuleApproveTypeName from RuleApproveTypeMaster inner join RuleDetail on  RuleDetail.RuleApproveTypeId=RuleApproveTypeMaster.RuleApproveTypeId where  RuleDetail.Employeeid='" + Session["EmployeeId"] + "' order by RuleApproveTypeName";

        SqlCommand cmd1 = new SqlCommand(str, con);
        cmd1.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlapprule.DataSource = dt;
            ddlapprule.DataTextField = "RuleApproveTypeName";
            ddlapprule.DataValueField = "RuleApproveTypeId";
            ddlapprule.DataBind();

        }
        ddlapprule.Items.Insert(0, "All");
        ddlapprule.Items[0].Value = "0";
    }
    protected void FillRuleType()
    {
        //dt = new DataTable();
        //dt = clsInstruction.SelectRuleTypeMaster();
        ////ddlruletype.DataSource = dt;
        //// ddlruletype.DataBind();
        //// ddlruletype.Items.Insert(0, "- All - ");
        //// ddlruletype.SelectedItem.Value = "0";
        //if (dt.Rows.Count > 0)
        //{
        //    //     ddlruletype.SelectedIndex = 0;
        //}
    }
    protected void FillRuleGridSearch()
    {
        DataTable DtMain = new DataTable();

        //
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


        DataColumn dtcom4 = new DataColumn();
        dtcom4.DataType = System.Type.GetType("System.String");
        dtcom4.ColumnName = "RuleTitle";
        dtcom4.ReadOnly = false;
        dtcom4.Unique = false;
        dtcom4.AllowDBNull = true;
        DtMain.Columns.Add(dtcom4);

        DataColumn dtcom8 = new DataColumn();
        dtcom8.DataType = System.Type.GetType("System.String");
        dtcom8.ColumnName = "Description";
        dtcom8.ReadOnly = false;
        dtcom8.Unique = false;
        dtcom8.AllowDBNull = true;
        DtMain.Columns.Add(dtcom8);

        DataColumn dtcom9 = new DataColumn();
        dtcom9.DataType = System.Type.GetType("System.String");
        dtcom9.ColumnName = "RuleApproveTypeName";
        dtcom9.ReadOnly = false;
        dtcom9.Unique = false;
        dtcom9.AllowDBNull = true;
        DtMain.Columns.Add(dtcom9);

        //
        if (Session["EmployeeId"] != null)
        {
            Int32 EmpId = Convert.ToInt32(Session["EmployeeId"]);
            string Appvise = "";
            if (ddlapprule.SelectedIndex > 0)
            {
                Appvise = " and (RuleApproveTypeMaster.RuleApproveTypeId='" + ddlapprule.SelectedValue + "') ";
            }

            DataTable dtr = (DataTable)select("SELECT RuleMaster.DocumentMainId,RuleMaster.DocumentSubId,RuleMaster.DocumentTypeId, RuleDetail.RuleDetailId, RuleDetail.RuleId, RuleDetail.RuleApproveTypeId, RuleDetail.StepId, RuleDetail.Days,RuleApproveTypeMaster.RuleApproveTypeName,RuleMaster.ConditionTypeId, RuleMaster.RuleTypeId, CASE RuleMaster.ConditionTypeId WHEN '1' THEN 'Step Wise in Serial Order' WHEN '2' THEN 'Simultaneous to All' END AS ConditionTypeName, RuleTypeMaster.RuleType AS RuleTypeName FROM         DesignationMaster RIGHT OUTER JOIN EmployeeMaster ON DesignationMaster.DesignationMasterId = EmployeeMaster.DesignationMasterId RIGHT OUTER JOIN RuleApproveTypeMaster RIGHT OUTER JOIN RuleTypeMaster INNER JOIN RuleDetail INNER JOIN RuleMaster ON RuleDetail.RuleId = RuleMaster.RuleId ON RuleTypeMaster.RuleTypeId = RuleMaster.RuleTypeId ON RuleApproveTypeMaster.RuleApproveTypeId = RuleDetail.RuleApproveTypeId ON EmployeeMaster.EmployeeMasterID = RuleDetail.EmployeeId WHERE   (RuleMaster.Whid= '" + ddlbusiness.SelectedValue + "')" + Appvise + " and (RuleDetail.EmployeeId = '" + EmpId + "')");
            if (dtr.Rows.Count > 0)
            {
                if (Convert.ToString(dtr.Rows[0]["DocumentTypeId"]) != "0")
                {
                    dt = clsInstruction.SelectRuleDetailforEmployee(EmpId, ddlapprule.SelectedValue, ddlbusiness.SelectedValue);

                }
                else if (Convert.ToString(dtr.Rows[0]["DocumentSubId"]) != "0")
                {
                    dt = clsInstruction.SelectRuleDetailforEmployeeSub(EmpId, ddlapprule.SelectedValue, ddlbusiness.SelectedValue);
                }
                else if (Convert.ToString(dtr.Rows[0]["DocumentMainId"]) != "0")
                {
                    dt = clsInstruction.SelectRuleDetailforEmployeeMain(EmpId, ddlapprule.SelectedValue, ddlbusiness.SelectedValue);

                }
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    int ConditionTypeId = Convert.ToInt32(dr["ConditionTypeId"].ToString());


                    DataTable dtDocProcessDisc = new DataTable();
                   
                    string status = "";
                    if (ddlstatus.SelectedValue == "Pending")
                    {

                        status = " and (DocumentMaster.DocumentId NOT IN  (SELECT     DocumentId  FROM          DocumentProcessing AS DocumentProcessing_1  WHERE      (Approve IS NULL) OR (Approve = 0))) and documentmaster.DocumentTypeId  = '" + Convert.ToInt32(dr["DocumentTypeId"]) + "'  AND (DocumentMaster.DocumentId not  IN  (SELECT     RuleProcessMaster_1.DocumentId   FROM          RuleProcessMaster AS RuleProcessMaster_1 LEFT OUTER JOIN   RuleDetail ON RuleProcessMaster_1.RuleDetailId = RuleDetail.RuleDetailId  WHERE      (RuleProcessMaster_1.EmployeeId = '" + Session["EmployeeId"] + "') AND (RuleDetail.RuleId = '" + Convert.ToInt32(dr["RuleId"]) + "')))  order by DocumentId desc";
                    }
                    else if (ddlstatus.SelectedValue == "True")
                    {
                        status = " and (DocumentMaster.DocumentId Not IN  (SELECT     DocumentId  FROM          DocumentProcessing AS DocumentProcessing_1  WHERE      (Approve IS NULL) OR (Approve = 0))) and documentmaster.DocumentTypeId  = '" + Convert.ToInt32(dr["DocumentTypeId"]) + "'  AND (DocumentMaster.DocumentId  IN  (SELECT     RuleProcessMaster_1.DocumentId   FROM          RuleProcessMaster AS RuleProcessMaster_1 LEFT OUTER JOIN   RuleDetail ON RuleProcessMaster_1.RuleDetailId = RuleDetail.RuleDetailId  WHERE   (RuleProcessMaster_1.Approve='1') and   (RuleProcessMaster_1.EmployeeId = '" + Session["EmployeeId"] + "') AND (RuleDetail.RuleId = '" + Convert.ToInt32(dr["RuleId"]) + "')))  order by DocumentId desc";

                    }
                    else if (ddlstatus.SelectedValue == "False")
                    {
                        status = " and (DocumentMaster.DocumentId NOT IN  (SELECT     DocumentId  FROM   DocumentProcessing AS DocumentProcessing_1  WHERE      (Approve IS NULL) OR (Approve = 0))) and documentmaster.DocumentTypeId  = '" + Convert.ToInt32(dr["DocumentTypeId"]) + "'  AND (DocumentMaster.DocumentId  IN  (SELECT     RuleProcessMaster_1.DocumentId   FROM          RuleProcessMaster AS RuleProcessMaster_1 LEFT OUTER JOIN   RuleDetail ON RuleProcessMaster_1.RuleDetailId = RuleDetail.RuleDetailId  WHERE   (RuleProcessMaster_1.Approve='0') and   (RuleProcessMaster_1.EmployeeId = '" + Session["EmployeeId"] + "') AND (RuleDetail.RuleId = '" + Convert.ToInt32(dr["RuleId"]) + "')))  order by DocumentId desc";

                    }
                   

                    dtDocProcessDisc = select("SELECT DISTINCT  DocumentMaster.DocumentId, DocumentMaster.DocumentTypeId, DocumentMaster.DocumentUploadTypeId, DocumentMaster.DocumentUploadDate,  DocumentMaster.DocumentName, DocumentMaster.DocumentTitle, DocumentMaster.Description, DocumentMaster.PartyId, DocumentMaster.DocumentRefNo,   DocumentMaster.DocumentAmount, DocumentMaster.EmployeeId  FROM         DocumentMaster RIGHT OUTER JOIN  DocumentProcessing ON DocumentMaster.DocumentId = DocumentProcessing.DocumentId  WHERE (Cast(DocumentMaster.DocumentId as nvarchar)='" + txtSearch.Text + "' or (DocumentMaster.DocumentTitle='" + txtSearch.Text + "') ) " + status);
            
                    if (dtDocProcessDisc.Rows.Count > 0)
                    {
                        foreach (DataRow DrDocProcess1 in dtDocProcessDisc.Rows)
                        {
                            DataTable dtDocProcess = new DataTable();
                            dtDocProcess = select("SELECT     TOP (1) DocumentMaster.DocumentId, DocumentMaster.DocumentTypeId, DocumentMaster.DocumentUploadTypeId, DocumentMaster.DocumentUploadDate,  DocumentMaster.DocumentName, DocumentMaster.DocumentTitle, DocumentMaster.Description, DocumentMaster.PartyId, DocumentMaster.DocumentRefNo,   DocumentMaster.DocumentAmount, DocumentMaster.EmployeeId, DocumentProcessing.ProcessingId as DocumentProcessingId, DocumentProcessing.DocumentId AS Expr1,   DocumentProcessing.EmployeeId AS Expr2, DocumentProcessing.DocAllocateDate, DocumentProcessing.Approve, DocumentProcessing.ApproveDate,   DocumentProcessing.Note  FROM         DocumentMaster RIGHT OUTER JOIN  DocumentProcessing ON DocumentMaster.DocumentId = DocumentProcessing.DocumentId  WHERE     (DocumentMaster.DocumentId = '" + Convert.ToInt32(DrDocProcess1["DocumentId"].ToString()) + "')" + status);
                            // dtDocProcess = clsDocument.SelectProcessingDocumentbyDocTypeIdwiseTop(Convert.ToInt32(dr["DocumentTypeId"]), Convert.ToInt32(dr["RuleId"]), Convert.ToInt32(DrDocProcess1["DocumentId"].ToString()));
                            if (dtDocProcess.Rows.Count > 0)
                            {
                                foreach (DataRow DrDocProcess in dtDocProcess.Rows)
                                {
                                    if (Convert.ToInt32(dr["StepId"]) == 1)
                                    {
                                        DataTable DtdrEmpSel = new DataTable();
                                        DtdrEmpSel = clsInstruction.SelectRuleEmpSelectionMaster(Convert.ToInt32(dr["StepId"]), Convert.ToInt32(dr["RuleId"]));
                                        if (DtdrEmpSel.Rows.Count > 0)
                                        {
                                            if (DtdrEmpSel.Rows[0]["EmpSelectionId"].ToString() == "1") // Any
                                            {
                                                DataTable DtRuleProcess = new DataTable();
                                                DtRuleProcess = clsInstruction.SelectRuleProcessMasterDocIdWiseRuleIdWise(Convert.ToInt32(DrDocProcess["DocumentId"].ToString()), Convert.ToInt32(dr["RuleId"]));
                                                if (DtRuleProcess.Rows.Count > 0)
                                                {
                                                    // no rqure to put in list bcz one emp checked it.
                                                }
                                                else
                                                {  // he has to bcz atleast one emp has to approve
                                                    DateTime ProcessDt = System.DateTime.Now;
                                                    if ((DrDocProcess["ApproveDate"] != System.DBNull.Value) || (DrDocProcess["ApproveDate"].ToString() != ""))
                                                    {
                                                        ProcessDt = Convert.ToDateTime(DrDocProcess["ApproveDate"].ToString());
                                                    }
                                                    double Days = Convert.ToDouble(dr["Days"].ToString());
                                                    DateTime RecentTime = ProcessDt.AddDays(0);
                                                    //if (DateTime.Now > RecentTime) // time over
                                                    //{
                                                    //}
                                                    //else
                                                    //{
                                                    DataRow DtMainRow = DtMain.NewRow();
                                                    DtMainRow["DocId"] = DrDocProcess["DocumentId"].ToString();
                                                    DtMainRow["DocumentTitle"] = DrDocProcess["DocumentTitle"].ToString();
                                                    DtMainRow["ProcessDate"] = Convert.ToString(Convert.ToDateTime(RecentTime).ToShortDateString());

                                                    DtMainRow["RuleId"] = dr["RuleId"].ToString();
                                                    DtMainRow["RuleDetailId"] = dr["RuleDetailId"].ToString();

                                                    DtMainRow["RuleTitle"] = dr["RuleTitle"].ToString();
                                                    DtMainRow["Description"] = dr["Description"].ToString();
                                                    DtMainRow["RuleApproveTypeName"] = dr["RuleApproveTypeName"].ToString();
                                                    DtMain.Rows.Add(DtMainRow);
                                                    // }
                                                }
                                            }
                                            else // All Emp
                                            {
                                                DateTime ProcessDt = System.DateTime.Now;
                                                if ((DrDocProcess["ApproveDate"] != System.DBNull.Value) || (DrDocProcess["ApproveDate"].ToString() != ""))
                                                {
                                                    ProcessDt = Convert.ToDateTime(DrDocProcess["ApproveDate"].ToString());
                                                }
                                                double Days = Convert.ToDouble(dr["Days"].ToString());
                                                DateTime RecentTime = ProcessDt.AddDays(0);
                                                if (DateTime.Now > RecentTime) // time over
                                                {
                                                }
                                                else
                                                {
                                                    DataRow DtMainRow = DtMain.NewRow();
                                                    DtMainRow["DocId"] = DrDocProcess["DocumentId"].ToString();
                                                    DtMainRow["DocumentTitle"] = DrDocProcess["DocumentTitle"].ToString();
                                                    DtMainRow["ProcessDate"] = Convert.ToString(Convert.ToDateTime(RecentTime).ToShortDateString());
                                                    DtMainRow["RuleTitle"] = dr["RuleTitle"].ToString();
                                                    DtMainRow["Description"] = dr["Description"].ToString();
                                                    DtMainRow["RuleApproveTypeName"] = dr["RuleApproveTypeName"].ToString();
                                                    DtMainRow["RuleId"] = dr["RuleId"].ToString();
                                                    DtMain.Rows.Add(DtMainRow);
                                                }
                                            }
                                        }
                                        else // for not any and other
                                        {
                                            DateTime ProcessDt = System.DateTime.Now;
                                            if ((DrDocProcess["ApproveDate"] != System.DBNull.Value) || (DrDocProcess["ApproveDate"].ToString() != ""))
                                            {
                                                ProcessDt = Convert.ToDateTime(DrDocProcess["ApproveDate"].ToString());
                                            }
                                            double Days = Convert.ToDouble(dr["Days"].ToString());
                                            DateTime RecentTime = ProcessDt.AddDays(0);
                                            //if (DateTime.Now > RecentTime) // time over
                                            //{
                                            //}
                                            //else
                                            //{
                                            DataRow DtMainRow = DtMain.NewRow();
                                            DtMainRow["DocId"] = DrDocProcess["DocumentId"].ToString();
                                            DtMainRow["DocumentTitle"] = DrDocProcess["DocumentTitle"].ToString();
                                            DtMainRow["ProcessDate"] = Convert.ToString(Convert.ToDateTime(RecentTime).ToShortDateString());
                                            DtMainRow["RuleTitle"] = dr["RuleTitle"].ToString();
                                            DtMainRow["Description"] = dr["Description"].ToString();
                                            DtMainRow["RuleApproveTypeName"] = dr["RuleApproveTypeName"].ToString();
                                            DtMainRow["RuleId"] = dr["RuleId"].ToString();
                                            DtMainRow["RuleDetailId"] = dr["RuleDetailId"].ToString();
                                            DtMain.Rows.Add(DtMainRow);
                                            //  }
                                        }
                                    }
                                    else  // for other step
                                    {
                                        if (ConditionTypeId == 1) // Para
                                        {
                                            // // 
                                            DataTable LastEmpProcess = new DataTable();

                                            LastEmpProcess = clsInstruction.SelectRuleProcessMasterDocIdWiseRuleIdWise(Convert.ToInt32(DrDocProcess["DocumentId"].ToString()), Convert.ToInt32(dr["RuleId"]));
                                            if (LastEmpProcess.Rows.Count > 0)
                                            { // check last Step   and there is data for last steps                      
                                                // now check last steps position
                                                Int32 StepinProcess = Convert.ToInt32(LastEmpProcess.Rows[0]["StepId"].ToString());
                                                if (StepinProcess > Convert.ToInt32(dr["StepId"])) // 4
                                                { // greater then no need to do anything 
                                                }
                                                else if (StepinProcess < Convert.ToInt32(dr["StepId"])) // 2
                                                { // St 2 start\
                                                    DataTable DtEmpSelforLess = new DataTable();
                                                    DtEmpSelforLess = clsInstruction.SelectRuleEmpSelectionMaster(StepinProcess, Convert.ToInt32(dr["RuleId"]));
                                                    if (DtEmpSelforLess.Rows.Count > 0)
                                                    {
                                                        {
                                                            if (DtEmpSelforLess.Rows[0]["EmpSelectionId"].ToString() == "1") // Any
                                                            {

                                                                DataTable DtRuleProcessForLess = new DataTable();
                                                                DtRuleProcessForLess = clsInstruction.SelectRuleProcessMasterDocIdWiseRuleIdWise(Convert.ToInt32(dr["DocumentId"].ToString()), StepinProcess);
                                                                if (DtRuleProcessForLess.Rows.Count > 0)
                                                                {
                                                                    // One done so now we can do STTtttt
                                                                }
                                                                else
                                                                { // no one approved in step 2 so we cant move to step 3  
                                                                }
                                                            }
                                                            else // All Emp
                                                            {
                                                                // //  //
                                                                DataTable checkAllCond = new DataTable();
                                                                checkAllCond = clsInstruction.SelectRuleProcesstocheckAllCond(Convert.ToInt32(dr["DocumentId"].ToString()), Convert.ToInt32(dr["RuleId"]), Convert.ToInt32(LastEmpProcess.Rows[0]["StepId"].ToString()));
                                                                if (checkAllCond.Rows.Count > 0)
                                                                {
                                                                }
                                                                else
                                                                { // add for process
                                                                    DateTime ProcessDt = System.DateTime.Now;
                                                                    if ((DrDocProcess["ApproveDate"] != System.DBNull.Value) || (DrDocProcess["ApproveDate"].ToString() != ""))
                                                                    {
                                                                        ProcessDt = Convert.ToDateTime(DrDocProcess["ApproveDate"].ToString());
                                                                    }
                                                                    double Days = Convert.ToDouble(dr["Days"].ToString());
                                                                    DateTime RecentTime = ProcessDt.AddDays(0);
                                                                    //if (DateTime.Now > RecentTime) // time over
                                                                    //{
                                                                    //}
                                                                    //else
                                                                    //{
                                                                    DataRow DtMainRow = DtMain.NewRow();
                                                                    DtMainRow["DocId"] = DrDocProcess["DocumentId"].ToString();
                                                                    DtMainRow["DocumentTitle"] = DrDocProcess["DocumentTitle"].ToString();
                                                                    DtMainRow["ProcessDate"] = Convert.ToString(Convert.ToDateTime(RecentTime).ToShortDateString());
                                                                    DtMainRow["RuleTitle"] = dr["RuleTitle"].ToString();
                                                                    DtMainRow["Description"] = dr["Description"].ToString();
                                                                    DtMainRow["RuleApproveTypeName"] = dr["RuleApproveTypeName"].ToString();
                                                                    DtMainRow["RuleId"] = dr["RuleId"].ToString();
                                                                    DtMainRow["RuleDetailId"] = dr["RuleDetailId"].ToString();
                                                                    DtMain.Rows.Add(DtMainRow);
                                                                    // }
                                                                }
                                                                // // //

                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (StepinProcess + 1 == Convert.ToInt32(dr["StepId"]))
                                                        {
                                                            DateTime ProcessDt = System.DateTime.Now;
                                                            if ((DrDocProcess["ApproveDate"] != System.DBNull.Value) || (DrDocProcess["ApproveDate"].ToString() != ""))
                                                            {
                                                                ProcessDt = Convert.ToDateTime(DrDocProcess["ApproveDate"].ToString());
                                                            }
                                                            double Days = Convert.ToDouble(dr["Days"].ToString());
                                                            DateTime RecentTime = ProcessDt.AddDays(0);
                                                            //if (DateTime.Now > RecentTime) // time over
                                                            //{
                                                            //}
                                                            //else
                                                            //{
                                                            DataRow DtMainRow = DtMain.NewRow();
                                                            DtMainRow["DocId"] = DrDocProcess["DocumentId"].ToString();
                                                            DtMainRow["DocumentTitle"] = DrDocProcess["DocumentTitle"].ToString();
                                                            DtMainRow["ProcessDate"] = Convert.ToString(Convert.ToDateTime(RecentTime).ToShortDateString());
                                                            DtMainRow["RuleTitle"] = dr["RuleTitle"].ToString();
                                                            DtMainRow["Description"] = dr["Description"].ToString();
                                                            DtMainRow["RuleApproveTypeName"] = dr["RuleApproveTypeName"].ToString();
                                                            DtMainRow["RuleId"] = dr["RuleId"].ToString();
                                                            DtMainRow["RuleDetailId"] = dr["RuleDetailId"].ToString();
                                                            DtMain.Rows.Add(DtMainRow);
                                                        }

                                                    }
                                                }
                                                else if (StepinProcess == Convert.ToInt32(dr["StepId"]))
                                                {
                                                    DataTable DtdrEmpSel = new DataTable();
                                                    DtdrEmpSel = clsInstruction.SelectRuleEmpSelectionMaster(Convert.ToInt32(dr["StepId"]), Convert.ToInt32(dr["RuleId"]));
                                                    if (DtdrEmpSel.Rows.Count > 0)
                                                    {
                                                        if (DtdrEmpSel.Rows[0]["EmpSelectionId"].ToString() == "1") // Any
                                                        {
                                                            DataTable DtRuleProcess = new DataTable();
                                                            DtRuleProcess = clsInstruction.SelectRuleProcessMasterDocIdWiseRuleIdWise(Convert.ToInt32(DrDocProcess["DocumentId"].ToString()), Convert.ToInt32(dr["RuleId"]));
                                                            if (DtRuleProcess.Rows.Count > 0)
                                                            {
                                                                // no rqure to put in list bcz one emp checked it.
                                                            }
                                                            else
                                                            {  // he has to bcz atleast one emp has to approve
                                                                DateTime ProcessDt = System.DateTime.Now;
                                                                if ((DrDocProcess["ApproveDate"] != System.DBNull.Value) || (DrDocProcess["ApproveDate"].ToString() != ""))
                                                                {
                                                                    ProcessDt = Convert.ToDateTime(DrDocProcess["ApproveDate"].ToString());
                                                                }
                                                                double Days = Convert.ToDouble(dr["Days"].ToString());
                                                                DateTime RecentTime = ProcessDt.AddDays(0);
                                                                //if (DateTime.Now > RecentTime) // time over
                                                                //{
                                                                //}
                                                                //else
                                                                //{
                                                                DataRow DtMainRow = DtMain.NewRow();
                                                                DtMainRow["DocId"] = DrDocProcess["DocumentId"].ToString();
                                                                DtMainRow["DocumentTitle"] = DrDocProcess["DocumentTitle"].ToString();
                                                                DtMainRow["ProcessDate"] = Convert.ToString(Convert.ToDateTime(RecentTime).ToShortDateString());
                                                                DtMainRow["RuleTitle"] = dr["RuleTitle"].ToString();
                                                                DtMainRow["Description"] = dr["Description"].ToString();
                                                                DtMainRow["RuleApproveTypeName"] = dr["RuleApproveTypeName"].ToString();
                                                                DtMainRow["RuleId"] = dr["RuleId"].ToString();
                                                                DtMainRow["RuleDetailId"] = dr["RuleDetailId"].ToString();
                                                                DtMain.Rows.Add(DtMainRow);
                                                                // }
                                                            }
                                                        }
                                                        else // All Emp
                                                        {
                                                            DateTime ProcessDt = System.DateTime.Now;
                                                            if ((DrDocProcess["ApproveDate"] != System.DBNull.Value) || (DrDocProcess["ApproveDate"].ToString() != ""))
                                                            {
                                                                ProcessDt = Convert.ToDateTime(DrDocProcess["ApproveDate"].ToString());
                                                            }
                                                            double Days = Convert.ToDouble(dr["Days"].ToString());
                                                            DateTime RecentTime = ProcessDt.AddDays(0);
                                                            //if (DateTime.Now > RecentTime) // time over
                                                            //{
                                                            //}
                                                            //else
                                                            //{
                                                            DataRow DtMainRow = DtMain.NewRow();
                                                            DtMainRow["DocId"] = DrDocProcess["DocumentId"].ToString();
                                                            DtMainRow["DocumentTitle"] = DrDocProcess["DocumentTitle"].ToString();
                                                            DtMainRow["ProcessDate"] = Convert.ToString(Convert.ToDateTime(RecentTime).ToShortDateString());
                                                            DtMainRow["RuleTitle"] = dr["RuleTitle"].ToString();
                                                            DtMainRow["Description"] = dr["Description"].ToString();
                                                            DtMainRow["RuleApproveTypeName"] = dr["RuleApproveTypeName"].ToString();
                                                            DtMainRow["RuleId"] = dr["RuleId"].ToString();
                                                            DtMainRow["RuleDetailId"] = dr["RuleDetailId"].ToString();
                                                            DtMain.Rows.Add(DtMainRow);
                                                            // }
                                                        }
                                                    }
                                                    else // for not any and other
                                                    {
                                                        DateTime ProcessDt = System.DateTime.Now;
                                                        if ((DrDocProcess["ApproveDate"] != System.DBNull.Value) || (DrDocProcess["ApproveDate"].ToString() != ""))
                                                        {
                                                            ProcessDt = Convert.ToDateTime(DrDocProcess["ApproveDate"].ToString());
                                                        }
                                                        double Days = Convert.ToDouble(dr["Days"].ToString());
                                                        DateTime RecentTime = ProcessDt.AddDays(0);
                                                        //if (DateTime.Now > RecentTime) // time over
                                                        //{
                                                        //}
                                                        //else
                                                        //{
                                                        DataRow DtMainRow = DtMain.NewRow();
                                                        DtMainRow["DocId"] = DrDocProcess["DocumentId"].ToString();
                                                        DtMainRow["DocumentTitle"] = DrDocProcess["DocumentTitle"].ToString();
                                                        DtMainRow["ProcessDate"] = Convert.ToString(Convert.ToDateTime(RecentTime).ToShortDateString());
                                                        DtMainRow["RuleTitle"] = dr["RuleTitle"].ToString();
                                                        DtMainRow["Description"] = dr["Description"].ToString();
                                                        DtMainRow["RuleApproveTypeName"] = dr["RuleApproveTypeName"].ToString();
                                                        DtMainRow["RuleId"] = dr["RuleId"].ToString();
                                                        DtMainRow["RuleDetailId"] = dr["RuleDetailId"].ToString();
                                                        DtMain.Rows.Add(DtMainRow);
                                                        //  }
                                                    }
                                                }     // //
                                                //else
                                                //{ 
                                                // if (StepinProcess + 1 ==  Convert.ToInt32(dr["StepId"])) 
                                                // {
                                                //     DataRow DtMainRow = DtMain.NewRow();
                                                //     DtMainRow["DocId"] = DrDocProcess["DocumentId"].ToString();
                                                //     DtMainRow["DocumentTitle"] = DrDocProcess["DocumentTitle"].ToString();
                                                //     DtMainRow["ProcessDate"] = Convert.ToString(Convert.ToDateTime(RecentTime).ToShortDateString());
                                                //     DtMainRow["RuleApproveTypeName"] = dr["RuleApproveTypeName"].ToString();
                                                //     DtMainRow["RuleId"] = dr["RuleId"].ToString();
                                                //     DtMainRow["RuleDetailId"] = dr["RuleDetailId"].ToString();
                                                //     DtMain.Rows.Add(DtMainRow);
                                                // }
                                                //}
                                            }
                                        }
                                        else // simu
                                        {
                                            DataTable DtdrEmpSel = new DataTable();
                                            DtdrEmpSel = clsInstruction.SelectRuleEmpSelectionMaster(Convert.ToInt32(dr["StepId"]), Convert.ToInt32(dr["RuleId"]));
                                            if (DtdrEmpSel.Rows.Count > 0)
                                            {
                                                if (DtdrEmpSel.Rows[0]["EmpSelectionId"].ToString() == "1") // Any
                                                {
                                                    DataTable DtRuleProcess = new DataTable();
                                                    DtRuleProcess = clsInstruction.SelectRuleProcessMasterDocIdWiseRuleIdWise(Convert.ToInt32(DrDocProcess["DocumentId"].ToString()), Convert.ToInt32(dr["RuleId"]));
                                                    if (DtRuleProcess.Rows.Count > 0)
                                                    {
                                                        // no rqure to put in list bcz one emp checked it.
                                                    }
                                                    else
                                                    {  // he has to bcz atleast one emp has to approve
                                                        DateTime ProcessDt = System.DateTime.Now;
                                                        if ((DrDocProcess["ApproveDate"] != System.DBNull.Value) || (DrDocProcess["ApproveDate"].ToString() != ""))
                                                        {
                                                            ProcessDt = Convert.ToDateTime(DrDocProcess["ApproveDate"].ToString());
                                                        }
                                                        double Days = Convert.ToDouble(dr["Days"].ToString());
                                                        DateTime RecentTime = ProcessDt.AddDays(0);
                                                        //if (DateTime.Now > RecentTime) // time over
                                                        //{
                                                        //}
                                                        //else
                                                        //{
                                                        DataRow DtMainRow = DtMain.NewRow();
                                                        DtMainRow["DocId"] = DrDocProcess["DocumentId"].ToString();
                                                        DtMainRow["DocumentTitle"] = DrDocProcess["DocumentTitle"].ToString();
                                                        DtMainRow["ProcessDate"] = Convert.ToString(Convert.ToDateTime(RecentTime).ToShortDateString());
                                                        DtMainRow["RuleTitle"] = dr["RuleTitle"].ToString();
                                                        DtMainRow["Description"] = dr["Description"].ToString();
                                                        DtMainRow["RuleApproveTypeName"] = dr["RuleApproveTypeName"].ToString();
                                                        DtMainRow["RuleId"] = dr["RuleId"].ToString();
                                                        DtMainRow["RuleDetailId"] = dr["RuleDetailId"].ToString();
                                                        DtMain.Rows.Add(DtMainRow);
                                                        // }
                                                    }
                                                }
                                                else // All Emp
                                                {
                                                    DateTime ProcessDt = System.DateTime.Now;
                                                    if ((DrDocProcess["ApproveDate"] != System.DBNull.Value) || (DrDocProcess["ApproveDate"].ToString() != ""))
                                                    {
                                                        ProcessDt = Convert.ToDateTime(DrDocProcess["ApproveDate"].ToString());
                                                    }
                                                    double Days = Convert.ToDouble(dr["Days"].ToString());
                                                    DateTime RecentTime = ProcessDt.AddDays(0);
                                                    //if (DateTime.Now > RecentTime) // time over
                                                    //{
                                                    //}
                                                    //else
                                                    //{
                                                    DataRow DtMainRow = DtMain.NewRow();
                                                    DtMainRow["DocId"] = DrDocProcess["DocumentId"].ToString();
                                                    DtMainRow["DocumentTitle"] = DrDocProcess["DocumentTitle"].ToString();
                                                    DtMainRow["ProcessDate"] = Convert.ToString(Convert.ToDateTime(RecentTime).ToShortDateString());
                                                    DtMainRow["RuleTitle"] = dr["RuleTitle"].ToString();
                                                    DtMainRow["Description"] = dr["Description"].ToString();
                                                    DtMainRow["RuleApproveTypeName"] = dr["RuleApproveTypeName"].ToString();
                                                    DtMainRow["RuleId"] = dr["RuleId"].ToString();
                                                    DtMainRow["RuleDetailId"] = dr["RuleDetailId"].ToString();
                                                    DtMain.Rows.Add(DtMainRow);
                                                    // }
                                                }
                                            }
                                            else // for not any and other
                                            {
                                                DateTime ProcessDt = System.DateTime.Now;
                                                if ((DrDocProcess["ApproveDate"] != System.DBNull.Value) || (DrDocProcess["ApproveDate"].ToString() != ""))
                                                {
                                                    ProcessDt = Convert.ToDateTime(DrDocProcess["ApproveDate"].ToString());
                                                }
                                                double Days = Convert.ToDouble(dr["Days"].ToString());
                                                DateTime RecentTime = ProcessDt.AddDays(0);
                                                //if (DateTime.Now > RecentTime) // time over
                                                //{
                                                //}
                                                //else
                                                //{
                                                DataRow DtMainRow = DtMain.NewRow();
                                                DtMainRow["DocId"] = DrDocProcess["DocumentId"].ToString();
                                                DtMainRow["DocumentTitle"] = DrDocProcess["DocumentTitle"].ToString();
                                                DtMainRow["ProcessDate"] = Convert.ToString(Convert.ToDateTime(RecentTime).ToShortDateString());
                                                DtMainRow["RuleTitle"] = dr["RuleTitle"].ToString();
                                                DtMainRow["Description"] = dr["Description"].ToString();
                                                DtMainRow["RuleApproveTypeName"] = dr["RuleApproveTypeName"].ToString();
                                                DtMainRow["RuleId"] = dr["RuleId"].ToString();
                                                DtMainRow["RuleDetailId"] = dr["RuleDetailId"].ToString();
                                                DtMain.Rows.Add(DtMainRow);
                                                //  }
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                    //

                }
            }

            grid_Rule_master.DataSource = DtMain;
            DataView myDataView = new DataView();
            myDataView = DtMain.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            grid_Rule_master.DataBind();

            foreach (GridViewRow gdr in grid_Rule_master.Rows)
            {
                Label Label1 = (Label)gdr.FindControl("Label1");

                DataTable dt4 = new DataTable();
                dt4 = select("Select RuleProcessMaster.* from RuleProcessMaster inner join RuleDetail on RuleDetail.RuleDetailId=RuleProcessMaster.RuleDetailId where RuleProcessMaster.EmployeeId='" + Convert.ToInt32(Session["EmployeeId"]) + "' and RuleDetail.RuleDetailId='" + grid_Rule_master.DataKeys[gdr.DataItemIndex].Value + "' and RuleProcessMaster.DocumentId='" + Label1.Text + "'");
                if (dt4.Rows.Count > 0)
                {
                    DropDownList rbtnAcceptReject = (DropDownList)gdr.FindControl("rbtnAcceptReject");
                    TextBox txtNote = (TextBox)gdr.FindControl("txtNote");
                    rbtnAcceptReject.SelectedValue = dt4.Rows[0]["Approve"].ToString();
                    if (RadioButtonList1.SelectedIndex == 1)
                    {
                        txtNote.Enabled = false;
                        rbtnAcceptReject.Enabled = false;
                        imgbtnSubmit.Visible = true;
                    }
                    else
                    {
                        txtNote.Enabled = true;
                        rbtnAcceptReject.Enabled = true;
                        imgbtnSubmit.Visible = false;
                    }

                    txtNote.Text = dt4.Rows[0]["Note"].ToString();

                }



            }
        }
        else
        {
            Response.Redirect("~/ShoppingCart/Admin/ShoppingCartlogin.aspx");
        }
    }
    protected void grid_Rule_master_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;

            ImageButton1_Click1(sender, e);
       
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
    protected void FillRuleGrid(string sortExp, string sortDir)
    {
        DataTable DtMain = new DataTable();

        //
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
        //

        DataColumn dtcom4 = new DataColumn();
        dtcom4.DataType = System.Type.GetType("System.String");
        dtcom4.ColumnName = "RuleTitle";
        dtcom4.ReadOnly = false;
        dtcom4.Unique = false;
        dtcom4.AllowDBNull = true;
        DtMain.Columns.Add(dtcom4);

        DataColumn dtcom8 = new DataColumn();
        dtcom8.DataType = System.Type.GetType("System.String");
        dtcom8.ColumnName = "Description";
        dtcom8.ReadOnly = false;
        dtcom8.Unique = false;
        dtcom8.AllowDBNull = true;
        DtMain.Columns.Add(dtcom8);

        DataColumn dtcom9 = new DataColumn();
        dtcom9.DataType = System.Type.GetType("System.String");
        dtcom9.ColumnName = "RuleApproveTypeName";
        dtcom9.ReadOnly = false;
        dtcom9.Unique = false;
        dtcom9.AllowDBNull = true;
        DtMain.Columns.Add(dtcom9);
         fildocidtit = " Cast(DocumentMaster.DocumentUploadDate as Date) Between '"+txtdatefrom.Text+"' and '"+txtdateto.Text+"' and ";
        if (txtSearch.Text.Length > 0)
        {
            fildocidtit = "  (Cast(DocumentMaster.DocumentId as nvarchar)='" + txtSearch.Text + "' or (DocumentMaster.DocumentTitle='" + txtSearch.Text + "')) and";
        }
        if (Session["EmployeeId"] != null)
        {
            string Appvise = "";
            if (ddlapprule.SelectedIndex > 0)
            {
                Appvise = " and (RuleApproveTypeMaster.RuleApproveTypeId='" + ddlapprule.SelectedValue + "') ";
            }
            Int32 EmpId = Convert.ToInt32(Session["EmployeeId"]);

            DataTable dtr = (DataTable)select("SELECT RuleMaster.DocumentMainId,RuleMaster.DocumentSubId,RuleMaster.DocumentTypeId,RuleDetail.RuleApproveTypeId FROM DesignationMaster RIGHT OUTER JOIN EmployeeMaster ON DesignationMaster.DesignationMasterId = EmployeeMaster.DesignationMasterId RIGHT OUTER JOIN RuleApproveTypeMaster RIGHT OUTER JOIN RuleTypeMaster INNER JOIN RuleDetail INNER JOIN RuleMaster ON RuleDetail.RuleId = RuleMaster.RuleId ON RuleTypeMaster.RuleTypeId = RuleMaster.RuleTypeId ON RuleApproveTypeMaster.RuleApproveTypeId = RuleDetail.RuleApproveTypeId ON EmployeeMaster.EmployeeMasterID = RuleDetail.EmployeeId WHERE (RuleMaster.Active='1') and (RuleMaster.Whid= '" + ddlbusiness.SelectedValue + "')" + Appvise + " and (RuleDetail.EmployeeId = '" + EmpId + "') order by DocumentMainId,DocumentSubId,DocumentTypeId");

            if (dtr.Rows.Count > 0)
            {
                if (Convert.ToString(dtr.Rows[0]["DocumentTypeId"]) != "0")
                {
                    dt = clsInstruction.SelectRuleDetailforEmployee(EmpId,ddlapprule.SelectedValue,ddlbusiness.SelectedValue);

                }
                else if (Convert.ToString(dtr.Rows[0]["DocumentSubId"]) != "0")
                {
                    dt = clsInstruction.SelectRuleDetailforEmployeeSub(EmpId,ddlapprule.SelectedValue,ddlbusiness.SelectedValue);
                }
                else if (Convert.ToString(dtr.Rows[0]["DocumentMainId"]) != "0")
                {
                    dt = clsInstruction.SelectRuleDetailforEmployeeMain(EmpId,ddlapprule.SelectedValue,ddlbusiness.SelectedValue);

                }

                //dt = clsInstruction.SelectRuleDetailforEmployee(EmpId, ddlapprule.SelectedValue, ddlbusiness.SelectedValue);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    int ConditionTypeId = Convert.ToInt32(dr["ConditionTypeId"].ToString());

                    //
                    //  SelectProcessingDocumentbyDocTypeIdwiseDisc

                    //DataTable dtDocProcessDisc = new DataTable();
                    //dtDocProcessDisc = clsDocument.SelectProcessingDocumentbyDocTypeIdwiseDisc(Convert.ToInt32(dr["DocumentTypeId"]), Convert.ToInt32(dr["RuleId"]));
                    //if (dtDocProcessDisc.Rows.Count > 0)
                    //{
                    //    foreach (DataRow DrDocProcess1 in dtDocProcessDisc.Rows)
                    //    { //DocumentProcessing.DocAllocateDate, DocumentProcessing.Approve,
                            DataTable dtDocProcess = new DataTable();
                            //dtDocProcess = clsDocument.SelectProcessingDocumentbyDocTypeIdwiseTop(Convert.ToInt32(dr["DocumentTypeId"]), Convert.ToInt32(dr["RuleId"]), Convert.ToInt32(DrDocProcess1["DocumentId"].ToString()));
                            dtDocProcess = select("SELECT distinct DocumentMaster.DocumentId, DocumentMaster.DocumentTypeId, DocumentMaster.DocumentUploadTypeId, DocumentMaster.DocumentUploadDate, "+
                     " DocumentMaster.DocumentName, DocumentMaster.DocumentTitle, DocumentMaster.Description, DocumentMaster.PartyId, DocumentMaster.DocumentRefNo, "+
                     " DocumentMaster.DocumentAmount, DocumentMaster.EmployeeId,"+
                    "  DocumentUploadDate as ApproveDate"+
" FROM   DocumentMaster RIGHT OUTER JOIN DocumentProcessing ON DocumentMaster.DocumentId = DocumentProcessing.DocumentId"+
" WHERE  "+fildocidtit+"  (DocumentMaster.DocumentId NOT IN (SELECT DocumentId FROM  DocumentProcessing AS DocumentProcessing_1 WHERE (Approve IS NULL) OR"+
 "(Approve = 0))) and documentmaster.DocumentTypeId  = '" + dr["DocumentTypeId"] + "' AND (DocumentMaster.DocumentId not   IN(SELECT     RuleProcessMaster_1.DocumentId FROM RuleProcessMaster AS RuleProcessMaster_1 LEFT OUTER JOIN" +
  " RuleDetail ON RuleProcessMaster_1.RuleDetailId = RuleDetail.RuleDetailId WHERE (RuleProcessMaster_1.EmployeeId = '"+Session["EmployeeId"]+"') AND (RuleDetail.RuleId ='" + Convert.ToInt32(dr["RuleId"]) + "')))" +
" ORDER BY DocumentUploadDate DESC");
                       
                    if (dtDocProcess.Rows.Count > 0)
                            {
                                foreach (DataRow DrDocProcess in dtDocProcess.Rows)
                                {
                                    if (Convert.ToInt32(dr["StepId"]) == 1)
                                    {
                                        DataTable DtdrEmpSel = new DataTable();
                                        DtdrEmpSel = clsInstruction.SelectRuleEmpSelectionMaster(Convert.ToInt32(dr["StepId"]), Convert.ToInt32(dr["RuleId"]));
                                        if (DtdrEmpSel.Rows.Count > 0)
                                        {
                                            if (DtdrEmpSel.Rows[0]["EmpSelectionId"].ToString() == "1") // Any
                                            {
                                                DataTable DtRuleProcess = new DataTable();
                                                DtRuleProcess = clsInstruction.SelectRuleProcessMasterDocIdWiseRuleIdWise(Convert.ToInt32(DrDocProcess["DocumentId"].ToString()), Convert.ToInt32(dr["RuleId"]));
                                                if (DtRuleProcess.Rows.Count > 0)
                                                {
                                                    // no rqure to put in list bcz one emp checked it.
                                                }
                                                else
                                                {  // he has to bcz atleast one emp has to approve
                                                    DateTime ProcessDt = Convert.ToDateTime(DrDocProcess["ApproveDate"].ToString());
                                                    double Days = Convert.ToDouble(dr["Days"].ToString());
                                                    DateTime RecentTime = ProcessDt.AddDays(0);
                                                    //if (DateTime.Now > RecentTime) // time over
                                                    //{
                                                    //}
                                                    //else
                                                    //{
                                                    DataRow DtMainRow = DtMain.NewRow();
                                                    DtMainRow["DocId"] = DrDocProcess["DocumentId"].ToString();
                                                    DtMainRow["DocumentTitle"] = DrDocProcess["DocumentTitle"].ToString();
                                                    DtMainRow["ProcessDate"] = Convert.ToString(Convert.ToDateTime(RecentTime).ToShortDateString());
                                                    DtMainRow["RuleTitle"] = dr["RuleTitle"].ToString();
                                                    DtMainRow["Description"] = dr["Description"].ToString();
                                                    DtMainRow["RuleApproveTypeName"] = dr["RuleApproveTypeName"].ToString();
                                                    DtMainRow["RuleId"] = dr["RuleId"].ToString();
                                                    DtMainRow["RuleDetailId"] = dr["RuleDetailId"].ToString();
                                                    DtMain.Rows.Add(DtMainRow);
                                                    // }
                                                }
                                            }
                                            else // All Emp
                                            {
                                                DateTime ProcessDt = Convert.ToDateTime(DrDocProcess["ApproveDate"].ToString());
                                                double Days = Convert.ToDouble(dr["Days"].ToString());
                                                DateTime RecentTime = ProcessDt.AddDays(0);
                                                if (DateTime.Now > RecentTime) // time over
                                                {
                                                }
                                                else
                                                {
                                                    DataRow DtMainRow = DtMain.NewRow();
                                                    DtMainRow["DocId"] = DrDocProcess["DocumentId"].ToString();
                                                    DtMainRow["DocumentTitle"] = DrDocProcess["DocumentTitle"].ToString();
                                                    DtMainRow["ProcessDate"] = Convert.ToString(Convert.ToDateTime(RecentTime).ToShortDateString());
                                                    DtMainRow["RuleTitle"] = dr["RuleTitle"].ToString();
                                                    DtMainRow["Description"] = dr["Description"].ToString();
                                                    DtMainRow["RuleApproveTypeName"] = dr["RuleApproveTypeName"].ToString();
                                                    DtMainRow["RuleId"] = dr["RuleId"].ToString();
                                                    DtMain.Rows.Add(DtMainRow);
                                                }
                                            }
                                        }
                                        else // for not any and other
                                        {
                                            DateTime ProcessDt = System.DateTime.Now;
                                            if ((DrDocProcess["ApproveDate"] != System.DBNull.Value) || (DrDocProcess["ApproveDate"].ToString() != ""))
                                            {
                                                ProcessDt = Convert.ToDateTime(DrDocProcess["ApproveDate"].ToString());
                                            }
                                            double Days = Convert.ToDouble(dr["Days"].ToString());
                                            DateTime RecentTime = ProcessDt.AddDays(0);
                                            //if (DateTime.Now > RecentTime) // time over
                                            //{
                                            //}
                                            //else
                                            //{
                                            DataRow DtMainRow = DtMain.NewRow();
                                            DtMainRow["DocId"] = DrDocProcess["DocumentId"].ToString();
                                            DtMainRow["DocumentTitle"] = DrDocProcess["DocumentTitle"].ToString();
                                            DtMainRow["ProcessDate"] = Convert.ToString(Convert.ToDateTime(RecentTime).ToShortDateString());
                                            DtMainRow["RuleTitle"] = dr["RuleTitle"].ToString();
                                            DtMainRow["Description"] = dr["Description"].ToString();
                                            DtMainRow["RuleApproveTypeName"] = dr["RuleApproveTypeName"].ToString();
                                            DtMainRow["RuleId"] = dr["RuleId"].ToString();
                                            DtMainRow["RuleDetailId"] = dr["RuleDetailId"].ToString();
                                            DtMain.Rows.Add(DtMainRow);
                                            //  }
                                        }
                                    }
                                    else  // for other step
                                    {
                                        if (ConditionTypeId == 1) // Para
                                        {
                                            // // 
                                            DataTable LastEmpProcess = new DataTable();

                                            LastEmpProcess = clsInstruction.SelectRuleProcessMasterDocIdWiseRuleIdWise(Convert.ToInt32(DrDocProcess["DocumentId"].ToString()), Convert.ToInt32(dr["RuleId"]));
                                            if (LastEmpProcess.Rows.Count > 0)
                                            { // check last Step   and there is data for last steps                      
                                                // now check last steps position
                                                Int32 StepinProcess = Convert.ToInt32(LastEmpProcess.Rows[0]["StepId"].ToString());
                                                if (StepinProcess > Convert.ToInt32(dr["StepId"])) // 4
                                                { // greater then no need to do anything 
                                                }
                                                else if (StepinProcess < Convert.ToInt32(dr["StepId"])) // 2
                                                { // St 2 start\
                                                    DataTable DtEmpSelforLess = new DataTable();
                                                    DtEmpSelforLess = clsInstruction.SelectRuleEmpSelectionMaster(StepinProcess, Convert.ToInt32(dr["RuleId"]));
                                                    if (DtEmpSelforLess.Rows.Count > 0)
                                                    {
                                                        {
                                                            if (DtEmpSelforLess.Rows[0]["EmpSelectionId"].ToString() == "1") // Any
                                                            {

                                                                DataTable DtRuleProcessForLess = new DataTable();
                                                                DtRuleProcessForLess = clsInstruction.SelectRuleProcessMasterDocIdWiseRuleIdWise(Convert.ToInt32(DrDocProcess["DocumentId"].ToString()), StepinProcess);
                                                                if (DtRuleProcessForLess.Rows.Count > 0)
                                                                {
                                                                    // One done so now we can do STTtttt
                                                                    DateTime ProcessDt = Convert.ToDateTime(DrDocProcess["ApproveDate"].ToString());
                                                                    double Days = Convert.ToDouble(dr["Days"].ToString());
                                                                    DateTime RecentTime = ProcessDt.AddDays(0);
                                                                    //if (DateTime.Now > RecentTime) // time over
                                                                    //{
                                                                    //}
                                                                    //else
                                                                    //{
                                                                    DataRow DtMainRow = DtMain.NewRow();
                                                                    DtMainRow["DocId"] = DrDocProcess["DocumentId"].ToString();
                                                                    DtMainRow["DocumentTitle"] = DrDocProcess["DocumentTitle"].ToString();
                                                                    DtMainRow["ProcessDate"] = Convert.ToString(Convert.ToDateTime(RecentTime).ToShortDateString());
                                                                    DtMainRow["RuleTitle"] = dr["RuleTitle"].ToString();
                                                                    DtMainRow["Description"] = dr["Description"].ToString();
                                                                    DtMainRow["RuleApproveTypeName"] = dr["RuleApproveTypeName"].ToString();
                                                                    DtMainRow["RuleId"] = dr["RuleId"].ToString();
                                                                    DtMainRow["RuleDetailId"] = dr["RuleDetailId"].ToString();
                                                                    DtMain.Rows.Add(DtMainRow);
                                                                }
                                                                else
                                                                { // no one approved in step 2 so we cant move to step 3  
                                                                }
                                                            }
                                                            else // All Emp
                                                            {
                                                                // //  //
                                                                DataTable checkAllCond = new DataTable();
                                                                checkAllCond = clsInstruction.SelectRuleProcesstocheckAllCond(Convert.ToInt32(DrDocProcess["DocumentId"].ToString()), Convert.ToInt32(dr["RuleId"]), Convert.ToInt32(LastEmpProcess.Rows[0]["StepId"].ToString()));
                                                                if (checkAllCond.Rows.Count > 0)
                                                                {
                                                                }
                                                                else
                                                                { // add for process
                                                                    DateTime ProcessDt = Convert.ToDateTime(DrDocProcess["ApproveDate"].ToString());
                                                                    double Days = Convert.ToDouble(dr["Days"].ToString());
                                                                    DateTime RecentTime = ProcessDt.AddDays(0);
                                                                    //if (DateTime.Now > RecentTime) // time over
                                                                    //{
                                                                    //}
                                                                    //else
                                                                    //{
                                                                    DataRow DtMainRow = DtMain.NewRow();
                                                                    DtMainRow["DocId"] = DrDocProcess["DocumentId"].ToString();
                                                                    DtMainRow["DocumentTitle"] = DrDocProcess["DocumentTitle"].ToString();
                                                                    DtMainRow["ProcessDate"] = Convert.ToString(Convert.ToDateTime(RecentTime).ToShortDateString());
                                                                    DtMainRow["RuleTitle"] = dr["RuleTitle"].ToString();
                                                                    DtMainRow["Description"] = dr["Description"].ToString();
                                                                    DtMainRow["RuleApproveTypeName"] = dr["RuleApproveTypeName"].ToString();
                                                                    DtMainRow["RuleId"] = dr["RuleId"].ToString();
                                                                    DtMainRow["RuleDetailId"] = dr["RuleDetailId"].ToString();
                                                                    DtMain.Rows.Add(DtMainRow);
                                                                    // }
                                                                }
                                                                // // //

                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (StepinProcess + 1 == Convert.ToInt32(dr["StepId"]))
                                                        {
                                                            DateTime ProcessDt = Convert.ToDateTime(DrDocProcess["ApproveDate"].ToString());
                                                            double Days = Convert.ToDouble(dr["Days"].ToString());
                                                            DateTime RecentTime = ProcessDt.AddDays(0);
                                                            //if (DateTime.Now > RecentTime) // time over
                                                            //{
                                                            //}
                                                            //else
                                                            //{
                                                            DataRow DtMainRow = DtMain.NewRow();
                                                            DtMainRow["DocId"] = DrDocProcess["DocumentId"].ToString();
                                                            DtMainRow["DocumentTitle"] = DrDocProcess["DocumentTitle"].ToString();
                                                            DtMainRow["ProcessDate"] = Convert.ToString(Convert.ToDateTime(RecentTime).ToShortDateString());
                                                            DtMainRow["RuleTitle"] = dr["RuleTitle"].ToString();
                                                            DtMainRow["Description"] = dr["Description"].ToString();
                                                            DtMainRow["RuleApproveTypeName"] = dr["RuleApproveTypeName"].ToString();
                                                            DtMainRow["RuleId"] = dr["RuleId"].ToString();
                                                            DtMainRow["RuleDetailId"] = dr["RuleDetailId"].ToString();
                                                            DtMain.Rows.Add(DtMainRow);
                                                        }

                                                    }
                                                }
                                                else if (StepinProcess == Convert.ToInt32(dr["StepId"]))
                                                {
                                                    DataTable DtdrEmpSel = new DataTable();
                                                    DtdrEmpSel = clsInstruction.SelectRuleEmpSelectionMaster(Convert.ToInt32(dr["StepId"]), Convert.ToInt32(dr["RuleId"]));
                                                    if (DtdrEmpSel.Rows.Count > 0)
                                                    {
                                                        if (DtdrEmpSel.Rows[0]["EmpSelectionId"].ToString() == "1") // Any
                                                        {
                                                            DataTable DtRuleProcess = new DataTable();
                                                            DtRuleProcess = clsInstruction.SelectRuleProcessMasterDocIdWiseRuleIdWise(Convert.ToInt32(DrDocProcess["DocumentId"].ToString()), Convert.ToInt32(dr["RuleId"]));
                                                            if (DtRuleProcess.Rows.Count > 0)
                                                            {
                                                                // no rqure to put in list bcz one emp checked it.
                                                            }
                                                            else
                                                            {  // he has to bcz atleast one emp has to approve
                                                                DateTime ProcessDt = Convert.ToDateTime(DrDocProcess["ApproveDate"].ToString());
                                                                double Days = Convert.ToDouble(dr["Days"].ToString());
                                                                DateTime RecentTime = ProcessDt.AddDays(0);
                                                                //if (DateTime.Now > RecentTime) // time over
                                                                //{
                                                                //}
                                                                //else
                                                                //{
                                                                DataRow DtMainRow = DtMain.NewRow();
                                                                DtMainRow["DocId"] = DrDocProcess["DocumentId"].ToString();
                                                                DtMainRow["DocumentTitle"] = DrDocProcess["DocumentTitle"].ToString();
                                                                DtMainRow["ProcessDate"] = Convert.ToString(Convert.ToDateTime(RecentTime).ToShortDateString());
                                                                DtMainRow["RuleTitle"] = dr["RuleTitle"].ToString();
                                                                DtMainRow["Description"] = dr["Description"].ToString();
                                                                DtMainRow["RuleApproveTypeName"] = dr["RuleApproveTypeName"].ToString();
                                                                DtMainRow["RuleId"] = dr["RuleId"].ToString();
                                                                DtMainRow["RuleDetailId"] = dr["RuleDetailId"].ToString();
                                                                DtMain.Rows.Add(DtMainRow);
                                                                // }
                                                            }
                                                        }
                                                        else // All Emp
                                                        {
                                                            DateTime ProcessDt = Convert.ToDateTime(DrDocProcess["ApproveDate"].ToString());
                                                            double Days = Convert.ToDouble(dr["Days"].ToString());
                                                            DateTime RecentTime = ProcessDt.AddDays(0);
                                                            //if (DateTime.Now > RecentTime) // time over
                                                            //{
                                                            //}
                                                            //else
                                                            //{
                                                            DataRow DtMainRow = DtMain.NewRow();
                                                            DtMainRow["DocId"] = DrDocProcess["DocumentId"].ToString();
                                                            DtMainRow["DocumentTitle"] = DrDocProcess["DocumentTitle"].ToString();
                                                            DtMainRow["ProcessDate"] = Convert.ToString(Convert.ToDateTime(RecentTime).ToShortDateString());
                                                            DtMainRow["RuleTitle"] = dr["RuleTitle"].ToString();
                                                            DtMainRow["Description"] = dr["Description"].ToString();
                                                            DtMainRow["RuleApproveTypeName"] = dr["RuleApproveTypeName"].ToString();
                                                            DtMainRow["RuleId"] = dr["RuleId"].ToString();
                                                            DtMainRow["RuleDetailId"] = dr["RuleDetailId"].ToString();
                                                            DtMain.Rows.Add(DtMainRow);
                                                            // }
                                                        }
                                                    }
                                                    else // for not any and other
                                                    {
                                                        DateTime ProcessDt = Convert.ToDateTime(DrDocProcess["ApproveDate"].ToString());
                                                        double Days = Convert.ToDouble(dr["Days"].ToString());
                                                        DateTime RecentTime = ProcessDt.AddDays(0);
                                                        //if (DateTime.Now > RecentTime) // time over
                                                        //{
                                                        //}
                                                        //else
                                                        //{
                                                        DataRow DtMainRow = DtMain.NewRow();
                                                        DtMainRow["DocId"] = DrDocProcess["DocumentId"].ToString();
                                                        DtMainRow["DocumentTitle"] = DrDocProcess["DocumentTitle"].ToString();
                                                        DtMainRow["ProcessDate"] = Convert.ToString(Convert.ToDateTime(RecentTime).ToShortDateString());
                                                        DtMainRow["RuleTitle"] = dr["RuleTitle"].ToString();
                                                        DtMainRow["Description"] = dr["Description"].ToString();
                                                        DtMainRow["RuleApproveTypeName"] = dr["RuleApproveTypeName"].ToString();
                                                        DtMainRow["RuleId"] = dr["RuleId"].ToString();
                                                        DtMainRow["RuleDetailId"] = dr["RuleDetailId"].ToString();
                                                        DtMain.Rows.Add(DtMainRow);
                                                        //  }
                                                    }
                                                }     // //
                                                //else
                                                //{ 
                                                // if (StepinProcess + 1 ==  Convert.ToInt32(dr["StepId"])) 
                                                // {
                                                //     DataRow DtMainRow = DtMain.NewRow();
                                                //     DtMainRow["DocId"] = DrDocProcess["DocumentId"].ToString();
                                                //     DtMainRow["DocumentTitle"] = DrDocProcess["DocumentTitle"].ToString();
                                                //     DtMainRow["ProcessDate"] = Convert.ToString(Convert.ToDateTime(RecentTime).ToShortDateString());
                                                //     DtMainRow["RuleApproveTypeName"] = dr["RuleApproveTypeName"].ToString();
                                                //     DtMainRow["RuleId"] = dr["RuleId"].ToString();
                                                //     DtMainRow["RuleDetailId"] = dr["RuleDetailId"].ToString();
                                                //     DtMain.Rows.Add(DtMainRow);
                                                // }
                                                //}
                                            }
                                        }
                                        else // simu
                                        {
                                            DataTable DtdrEmpSel = new DataTable();
                                            DtdrEmpSel = clsInstruction.SelectRuleEmpSelectionMaster(Convert.ToInt32(dr["StepId"]), Convert.ToInt32(dr["RuleId"]));
                                            if (DtdrEmpSel.Rows.Count > 0)
                                            {
                                                if (DtdrEmpSel.Rows[0]["EmpSelectionId"].ToString() == "1") // Any
                                                {
                                                    DataTable DtRuleProcess = new DataTable();
                                                    DtRuleProcess = clsInstruction.SelectRuleProcessMasterDocIdWiseRuleIdWise(Convert.ToInt32(DrDocProcess["DocumentId"].ToString()), Convert.ToInt32(dr["RuleId"]));
                                                    if (DtRuleProcess.Rows.Count > 0)
                                                    {
                                                        // no rqure to put in list bcz one emp checked it.
                                                    }
                                                    else
                                                    {  // he has to bcz atleast one emp has to approve
                                                        DateTime ProcessDt = Convert.ToDateTime(DrDocProcess["ApproveDate"].ToString());
                                                        double Days = Convert.ToDouble(dr["Days"].ToString());
                                                        DateTime RecentTime = ProcessDt.AddDays(0);
                                                        //if (DateTime.Now > RecentTime) // time over
                                                        //{
                                                        //}
                                                        //else
                                                        //{
                                                        DataRow DtMainRow = DtMain.NewRow();
                                                        DtMainRow["DocId"] = DrDocProcess["DocumentId"].ToString();
                                                        DtMainRow["DocumentTitle"] = DrDocProcess["DocumentTitle"].ToString();
                                                        DtMainRow["ProcessDate"] = Convert.ToString(Convert.ToDateTime(RecentTime).ToShortDateString());
                                                        DtMainRow["RuleTitle"] = dr["RuleTitle"].ToString();
                                                        DtMainRow["Description"] = dr["Description"].ToString();
                                                        DtMainRow["RuleApproveTypeName"] = dr["RuleApproveTypeName"].ToString();
                                                        DtMainRow["RuleId"] = dr["RuleId"].ToString();
                                                        DtMainRow["RuleDetailId"] = dr["RuleDetailId"].ToString();
                                                        DtMain.Rows.Add(DtMainRow);
                                                        // }
                                                    }
                                                }
                                                else // All Emp
                                                {
                                                    DateTime ProcessDt = Convert.ToDateTime(DrDocProcess["ApproveDate"].ToString());
                                                    double Days = Convert.ToDouble(dr["Days"].ToString());
                                                    DateTime RecentTime = ProcessDt.AddDays(0);
                                                    //if (DateTime.Now > RecentTime) // time over
                                                    //{
                                                    //}
                                                    //else
                                                    //{
                                                    DataRow DtMainRow = DtMain.NewRow();
                                                    DtMainRow["DocId"] = DrDocProcess["DocumentId"].ToString();
                                                    DtMainRow["DocumentTitle"] = DrDocProcess["DocumentTitle"].ToString();
                                                    DtMainRow["ProcessDate"] = Convert.ToString(Convert.ToDateTime(RecentTime).ToShortDateString());
                                                    DtMainRow["RuleTitle"] = dr["RuleTitle"].ToString();
                                                    DtMainRow["Description"] = dr["Description"].ToString();
                                                    DtMainRow["RuleApproveTypeName"] = dr["RuleApproveTypeName"].ToString();
                                                    DtMainRow["RuleId"] = dr["RuleId"].ToString();
                                                    DtMainRow["RuleDetailId"] = dr["RuleDetailId"].ToString();
                                                    DtMain.Rows.Add(DtMainRow);
                                                    // }
                                                }
                                            }
                                            else // for not any and other
                                            {
                                                DateTime ProcessDt = System.DateTime.Now;
                                                if (DrDocProcess["ApproveDate"] != System.DBNull.Value)
                                                {
                                                    ProcessDt = Convert.ToDateTime(DrDocProcess["ApproveDate"].ToString());
                                                }
                                                //DateTime ProcessDt = Convert.ToDateTime(DrDocProcess["ApproveDate"].ToString());
                                                double Days = Convert.ToDouble(dr["Days"].ToString());
                                                DateTime RecentTime = ProcessDt.AddDays(0);
                                                //if (DateTime.Now > RecentTime) // time over
                                                //{
                                                //}
                                                //else
                                                //{
                                                DataRow DtMainRow = DtMain.NewRow();
                                                DtMainRow["DocId"] = DrDocProcess["DocumentId"].ToString();
                                                DtMainRow["DocumentTitle"] = DrDocProcess["DocumentTitle"].ToString();
                                                DtMainRow["ProcessDate"] = Convert.ToString(Convert.ToDateTime(RecentTime).ToShortDateString());
                                                DtMainRow["RuleTitle"] = dr["RuleTitle"].ToString();
                                                DtMainRow["Description"] = dr["Description"].ToString();
                                                DtMainRow["RuleApproveTypeName"] = dr["RuleApproveTypeName"].ToString();
                                                DtMainRow["RuleId"] = dr["RuleId"].ToString();
                                                DtMainRow["RuleDetailId"] = dr["RuleDetailId"].ToString();
                                                DtMain.Rows.Add(DtMainRow);
                                                //  }
                                            }
                                        }
                                    }

                                }
                            }
                    //    }
                    //}
                    //

                }
            }

            grid_Rule_master.DataSource = DtMain;
            DataView myDataView = new DataView();
            myDataView = DtMain.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            grid_Rule_master.DataBind();
            if (grid_Rule_master.Rows.Count > 0)
            {
                imgbtnSubmit.Visible = true;
            }
        }
        else
        {
            Response.Redirect("~/ShoppingCart/Admin/ShoppingCartlogin.aspx");
        }
    }


    protected void grid_Rule_master_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            //string i;
            //i = e.CommandArgument.ToString();
            //Response.Redirect("RuleUpdate.aspx?RuleId=" + i);
            //   InsertRuleProcess
        }
    }

    protected void DdlRuleName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void imgbtnSubmit_Click(object sender, EventArgs e)
    {
        if ((ddlstatus.SelectedIndex == 0) || (ddlstatus.SelectedIndex == 1))
        {
            int i;
            i = 0;
            Int32 RuleDetailId;
            Int32 RuleId;
            Int32 DocId;
            CheckBox chk;
            DropDownList Rbtn;
            bool success;
            success = false;
            ViewState["rid"] = "";
            if (grid_Rule_master.Rows.Count > 0)
            {
                do
                {
                    chk = (CheckBox)grid_Rule_master.Rows[i].FindControl("chkAccept");
                    Rbtn = (DropDownList)grid_Rule_master.Rows[i].FindControl("rbtnAcceptReject");

                    if (Rbtn.SelectedValue != "Pending")
                    {
                        RuleDetailId = Int32.Parse(grid_Rule_master.DataKeys[i].Value.ToString());

                        Label lblid = (Label)grid_Rule_master.Rows[i].FindControl("Label1");
                        TextBox Note = (TextBox)grid_Rule_master.Rows[i].FindControl("txtnote");
                        Label lblruleid = (Label)grid_Rule_master.Rows[i].FindControl("lblruleid");


                        if (Rbtn.Enabled == true)
                        {
                            success = clsDocument.InsertRuleProcess(Convert.ToInt32(lblid.Text), RuleDetailId, Note.Text, Convert.ToBoolean(Rbtn.SelectedValue)); //(.InsertDocumentApprove(DocProcId, Convert.ToBoolean(Rbtn.SelectedValue), Note.Text);
                            success = true;
                            if (Convert.ToString(lblruleid.Text) != Convert.ToString(ViewState["rid"]))
                            {
                                ViewState["rid"] = lblruleid.Text;
                                DataTable dte = select("Select * from RuleMaster where Approvemail='1' and RuleId='" + lblruleid.Text + "'");
                                if (dte.Rows.Count > 0)
                                {
                                    try
                                    {
                                        sendmail(lblid.Text.ToString(), RuleDetailId.ToString(), lblruleid.Text.ToString());
                                    }
                                    catch
                                    {
                                    }
                                    
                                }
                            }
                            else
                            {
                                try
                                {
                                    sendmail(lblid.Text.ToString(), RuleDetailId.ToString(), lblruleid.Text.ToString());
                                }
                                catch
                                {
                                }

                            }
                        }
                    }

                    i = i + 1;
                }

                while (i <= grid_Rule_master.Rows.Count - 1);
            }
            lblmsg.Visible = true;

            if (success == true)
            {

                lblmsg.Text = "Document Approved Successfully.";
                FillRuleGrid("", "");
            }
            else
            {
                lblmsg.Text = "Document Approved Failed.";
            }
            setGridisize();
        }
        else if ((ddlstatus.SelectedIndex > 1) && ((ddlstatus.SelectedItem.Text == "Accepted") || (ddlstatus.SelectedItem.Text == "Rejected")))
        {
            int i;
            i = 0;
            Int32 RuleDetailId;
            Int32 RuleId;
            Int32 DocId;
            Int32 rst;
            CheckBox chk;
            DropDownList Rbtn;
            bool success;
            success = false;
            if (grid_Rule_master.Rows.Count > 0)
            {
                do
                {

                    Rbtn = (DropDownList)grid_Rule_master.Rows[i].FindControl("rbtnAcceptReject");
                    if (Rbtn.SelectedValue.ToString() == "Pending")
                    {

                        Label lblid = (Label)grid_Rule_master.Rows[i].FindControl("Label1");
                        rst = clsDocument.DeleteRuleProcess(Convert.ToInt32(lblid.Text));



                    }
                    else
                    {
                        if (Rbtn.SelectedValue.ToString() != "None")
                        {
                            RuleDetailId = Int32.Parse(grid_Rule_master.DataKeys[i].Value.ToString());
                            Label lblid = (Label)grid_Rule_master.Rows[i].FindControl("Label1");
                            TextBox Note = (TextBox)grid_Rule_master.Rows[i].FindControl("txtnote");

                            success = clsDocument.UpdateRuleProcess(Convert.ToInt32(lblid.Text), RuleDetailId, Note.Text, Convert.ToBoolean(Rbtn.SelectedValue)); //(.InsertDocumentApprove(DocProcId, Convert.ToBoolean(Rbtn.SelectedValue), Note.Text);
                            success = true;
                        }
                    }
                    i = i + 1;
                }

                while (i <= grid_Rule_master.Rows.Count - 1);
            }
            lblmsg.Visible = true;

            if (success == true)
            {

                lblmsg.Text = "Document Approved Successfully.";
                FillRuleGrid("", "");
            }
        }

    }
    public void sendmail(string docid, string RuleDetailId, string ruleid)
    {
        DataTable dte = select("Select distinct RuleDetail.RuleApproveTypeId, RuleMaster.RuleId, RuleDetail.EmployeeId,ConditionTypeId, EmployeeMaster.Email, RuleApproveTypeMaster.RuleApproveTypeName, RuleMaster.RuleDate,StepId,EmployeeMaster.EmployeeName, RuleMaster.RuleTitle,RuleDetail.RuleDetailId from RuleMaster inner join " +
                  "RuleDetail on RuleDetail.RuleId=RuleMaster.RuleId inner join EmployeeMaster on  EmployeeMaster.EmployeeMasterId= " +
                   " RuleDetail.EmployeeId inner join RuleApproveTypeMaster on RuleApproveTypeMaster.RuleApproveTypeId=RuleDetail.RuleApproveTypeId where RuleDetail.RuleDetailId not in(Select RuleProcessMaster.RuleDetailId from RuleProcessMaster inner join  RuleDetail on RuleDetail.RuleDetailId=RuleProcessMaster.RuleDetailId where RuleDetail.RuleId= '" + ruleid + "' and DocumentId=  '" + docid + "') and RuleDetail.RuleId= '" + ruleid + "' order by RuleDetailId ASC");
        if (dte.Rows.Count > 0)
        {
            if (Convert.ToInt32(dte.Rows[0]["ConditionTypeId"]) == 1)
            {

                string str = "Select Distinct logourl,MasterEmailId, CompanyMaster.CompanyName, EmployeeMaster.EmployeeName, OutGoingMailServer,WebMasterEmail, EmailMasterLoginPassword, AdminEmail, CompanyMaster.CompanyLogo,WarehouseMaster.Name as Wname,CityMasterTbl.CityName,StateMasterTbl.Statename,CountryMaster.CountryName from EmployeeMaster inner join WarehouseMaster on WarehouseMaster.WarehouseId=EmployeeMaster.Whid" +
         " inner join CompanyWebsitMaster on  CompanyWebsitMaster.Whid= WarehouseMaster.WarehouseId inner join " +
         " CompanyMaster on CompanyMaster.CompanyId=CompanyWebsitMaster.CompanyId inner join CompanyAddressMaster" +
         " on CompanyAddressMaster.CompanyMasterId=CompanyMaster.CompanyId inner join CountryMaster on " +
          "CountryMaster.CountryId=CompanyAddressMaster.Country inner join StateMasterTbl on " +
          "StateMasterTbl.StateId=CompanyAddressMaster.State inner join CityMasterTbl on " +
          "CityMasterTbl.CityId=CompanyAddressMaster.City where  EmployeeMaster.EmployeeMasterId='" + dte.Rows[0]["EmployeeId"] + "'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    SqlCommand cmdxx = new SqlCommand();
                    cmdxx.CommandText = "InsertEmailApproval";
                    cmdxx.CommandType = CommandType.StoredProcedure;

                    cmdxx.Parameters.Add(new SqlParameter("@Whid", SqlDbType.Int));
                    cmdxx.Parameters["@Whid"].Value = ddlbusiness.SelectedValue;
                    cmdxx.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
                    cmdxx.Parameters["@DocumentId"].Value = docid;
                    cmdxx.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
                    cmdxx.Parameters["@RuleId"].Value = ruleid;
                    cmdxx.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar));
                    cmdxx.Parameters["@UserId"].Value = dte.Rows[0]["EmployeeId"];
                    cmdxx.Parameters.Add(new SqlParameter("@EmailSend", SqlDbType.NVarChar));
                    cmdxx.Parameters["@EmailSend"].Value = true;
                    cmdxx.Parameters.Add(new SqlParameter("@AnswerReceived", SqlDbType.NVarChar));
                    cmdxx.Parameters["@AnswerReceived"].Value = false;
                    cmdxx.Parameters.Add(new SqlParameter("@ApprovalReject", SqlDbType.NVarChar));
                    cmdxx.Parameters["@ApprovalReject"].Value = false;

                    cmdxx.Parameters.Add(new SqlParameter("@DocApprovalType", SqlDbType.NVarChar));
                    cmdxx.Parameters["@DocApprovalType"].Value = dte.Rows[0]["RuleApproveTypeId"];
                    cmdxx.Parameters.Add(new SqlParameter("@DatetimeEmailSend", SqlDbType.NVarChar));
                    cmdxx.Parameters["@DatetimeEmailSend"].Value = DateTime.Now.ToString();
                    cmdxx.Parameters.Add(new SqlParameter("@DatetimeInventorySend", SqlDbType.NVarChar));
                    cmdxx.Parameters["@DatetimeInventorySend"].Value = DateTime.Now.ToString();
                    cmdxx.Parameters.Add(new SqlParameter("@ControlNo", SqlDbType.Int));
                    cmdxx.Parameters["@ControlNo"].Direction = ParameterDirection.Output;
                    cmdxx.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
                    cmdxx.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
                    Int32 result = DatabaseCls1.ExecuteNonQueryep(cmdxx);
                    result = Convert.ToInt32(cmdxx.Parameters["@ControlNo"].Value);

                    StringBuilder strhead = new StringBuilder();
                    strhead.Append("<table width=\"100%\"> ");
                    //strhead.Append("<tr><td align=\"left\"> <img src=" + Request.Url.Host.ToString() + "/ShoppingCart/images/" + Convert.ToString(dt.Rows[0]["CompanyLogo"]) + "\" \"border=\"0\" Width=\"176px\" Height=\"106px\" / > </td><td align=\"right\"><b><span style=\"color: #996600\">" + Convert.ToString(dt.Rows[0]["Wname"]) + "</span></b><BR>" + Convert.ToString(dt.Rows[0]["CityName"]) + "<Br>" + Convert.ToString(dt.Rows[0]["Statename"]) + "<Br>" + Convert.ToString(dt.Rows[0]["CountryName"]) + "</td></tr>  ");
                    strhead.Append("<tr><td Width=\"80%\" align=\"left\"> <img src=\"http://" + Request.Url.Host.ToString() + "/Shoppingcart/images/" + Convert.ToString(dt.Rows[0]["logourl"]) + "\" \"border=\"0\" Width=\"176px\" Height=\"106px\" / > </td><td align=\"left\"><b><span style=\"color: #996600\">" + Convert.ToString(dt.Rows[0]["Wname"]) + "</span></b><BR>" + Convert.ToString(dt.Rows[0]["CityName"]) + "<Br>" + Convert.ToString(dt.Rows[0]["Statename"]) + "<Br>" + Convert.ToString(dt.Rows[0]["CountryName"]) + "</td></tr>  ");

                    strhead.Append("<tr><td><br></td></tr>");
                    strhead.Append("<tr><td><b>Dear " + Convert.ToString(dt.Rows[0]["EmployeeName"]) + ",</b></td></tr>");
                    strhead.Append("<tr><td><br></td></tr>");
                    strhead.Append("<tr><td align=\"left\"><b> The following company has send you the following document for your approval kindly do need full in the matter.</b></td></tr>");
                    strhead.Append("<tr><td><table width=\"100%\">");
                    DataTable dteap = select("Select DocumentName, DocumentTitle,DocumentMainType.DocumentMainType+':'+DocumentSubType.DocumentSubType+':'+DocumentType.DocumentType as docmane from DocumentMaster inner join DocumentType on DocumentType.DocumentTypeId=DocumentMaster.DocumentTypeId " +
    " inner join DocumentSubType on DocumentSubType.DocumentSubTypeId=DocumentType.DocumentSubTypeId inner join " +
    " DocumentMainType on DocumentMainType.DocumentMainTypeId=DocumentSubType.DocumentMainTypeId Where DocumentId='" + docid + "'");
                    if (dteap.Rows.Count > 0)
                    {
                        strhead.Append("<tr><td> Document Title :</td><td>" + dteap.Rows[0]["DocumentTitle"] + "</td></tr>");
                        strhead.Append("<tr><td> Cabinet-Drower-Folder :</td><td>" + dteap.Rows[0]["docmane"] + "</td></tr>");
                    }
                    strhead.Append("<tr><td> Document Approval Rule Type :</td><td>" + dte.Rows[0]["RuleApproveTypeName"] + "</td></tr>");
                    strhead.Append("<tr><td>  Document Approval Rule Name :</td><td>" + dte.Rows[0]["RuleTitle"] + "</td></tr>");
                    strhead.Append("</table></td></tr> ");
                    DataTable dt2 = select(" Select EmployeeMaster.EmployeeName, RuleDetail.RuleDetailId,DesignationMaster.DesignationName,DepartmentmasterMNC.Departmentname,RuleProcessDate from RuleMaster inner join RuleDetail on RuleDetail.RuleId=RuleMaster.RuleId " +
                   " inner join RuleProcessMaster on RuleProcessMaster.RuleDetailId=RuleDetail.RuleDetailId" +
                     " inner join EmployeeMaster on  EmployeeMaster.EmployeeMasterId=  RuleProcessMaster.EmployeeId inner join DesignationMaster" +
                    " on DesignationMaster.DesignationMasterId=EmployeeMaster.DesignationMasterId inner join DepartmentmasterMNC on DepartmentmasterMNC.id=DesignationMaster.DeptID where RuleProcessMaster.DocumentId='" + docid + "' and RuleMaster.RuleId='" + ruleid + "'");
                    int i = 0;
                    foreach (DataRow dx in dt2.Rows)
                    {
                        if (i == 0)
                        {
                            strhead.Append("<tr><td><table width=\"100% BorderColor=Black BorderStyle=Solid\">");
                            strhead.Append("<tr><td colsplan=\"4\"><b>This Document as already approved by </b> </td>");
                            strhead.Append("<tr><td align=left>Employee Name </td><td  align=left>Designation Name</td> <td  align=left>Department Name</td>  <td  align=left>Aproval DateTime</td></tr>");
                        }

                        strhead.Append("<tr><td align=left>" + dx["EmployeeName"] + " </td><td  align=left>" + dx["DesignationName"] + "</td> <td  align=left>" + dx["Departmentname"] + "</td>  <td  align=left>" + dx["RuleProcessDate"] + "</td></tr>");
                        i = i + 1;

                    }
                    if (i > 0)
                    {
                        strhead.Append("</table></td></tr> ");

                    }
                    strhead.Append("<tr><td><br></td></tr>");
                    strhead.Append("<tr><td align=\"left\"><b>if you want to Approve/Reject above document,you can do it from here bellow. </b></td></tr>");
                    strhead.Append("<tr><td align=\"left\"><b><a href=http://" + Request.Url.Host.ToString() + "/EmailDocApprove.aspx?cn=" + result + "&rdt=" + dte.Rows[0]["RuleDetailId"] + "&cid=" + Session["Comid"] + ">Approve</a></b></td></tr>");
                    strhead.Append("<tr><td align=\"left\"><b><a href=http://" + Request.Url.Host.ToString() + "/EmailDocApprove.aspx?ap=ync&cn=" + result + "&rdt=" + dte.Rows[0]["RuleDetailId"] + "&cid=" + Session["Comid"] + ">Reject</a></b></td></tr>");


                    strhead.Append("<tr><td><br></td></tr>");
                    strhead.Append("<tr><td><b>Thanking You</b></td></tr>");
                    strhead.Append("<tr><td><b>Sincerely</b></td></tr>");
                    strhead.Append("<tr><td><br></td></tr>");
                    strhead.Append("<tr><td><b>For,</b></td></tr>");
                    strhead.Append("<tr><td><b> " + Convert.ToString(dt.Rows[0]["CompanyName"]) + "</b></td></tr>");
                    strhead.Append("</table> ");
                    string AdminEmail = "";
                    if (Convert.ToString(dt.Rows[0]["WebMasterEmail"]) != "")
                    {
                        AdminEmail = dt.Rows[0]["WebMasterEmail"].ToString();// TextAdminEmail.Text;
                    }
                    else
                    {
                        AdminEmail = dt.Rows[0]["MasterEmailId"].ToString();// TextAdminEmail.Text;
                    }
                    //string AdminEmail = txtusmail.Text;
                    String Password = dt.Rows[0]["EmailMasterLoginPassword"].ToString();// TextEmailMasterLoginPassword.Text;

                    //string body = "Test Mail Server - TestIwebshop";
                    MailAddress to = new MailAddress(dte.Rows[0]["Email"].ToString());
                    //MailAddress to = new MailAddress("maheshsorathiya500@gmail.com");
                    MailAddress from = new MailAddress(AdminEmail);

                    MailMessage objEmail = new MailMessage(from, to);
                    objEmail.Subject = "Document Approved by " + Convert.ToString(dt.Rows[0]["EmployeeName"]);

                    // if (RadioButtonList1.SelectedValue == "0")
                    {
                        objEmail.Body = strhead.ToString();
                        objEmail.IsBodyHtml = true;

                    }


                    objEmail.Priority = MailPriority.High;
                    string path2 = Server.MapPath("~\\Account\\" + Session["comid"] + "\\UploadedDocuments\\" + dteap.Rows[0]["DocumentName"].ToString());

                    Attachment attachFile = new Attachment(path2);
                    objEmail.Attachments.Add(attachFile);

                    SmtpClient client = new SmtpClient();

                    client.Credentials = new NetworkCredential(AdminEmail, Password);
                    client.Host = dt.Rows[0]["OutGoingMailServer"].ToString();


                    client.Send(objEmail);



                }

            }

        }
    }
    public void setGridisize()
    {
        // doc grid
        // doc grid
        if (grid_Rule_master.Rows.Count == 0)
        {
            pnlgrid.CssClass = "GridPanel20";
        }
        else if (grid_Rule_master.Rows.Count == 1)
        {
            pnlgrid.CssClass = "GridPanel250";
        }
        else if (grid_Rule_master.Rows.Count == 2)
        {
            pnlgrid.CssClass = "GridPanel300";
        }
        else if (grid_Rule_master.Rows.Count == 3)
        {
            pnlgrid.CssClass = "GridPanel350";
        }
        else if (grid_Rule_master.Rows.Count == 4)
        {
            pnlgrid.CssClass = "GridPanel400";
        }
        else if (grid_Rule_master.Rows.Count == 5)
        {
            pnlgrid.CssClass = "GridPanel450";
        }
        else
        {
            pnlgrid.CssClass = "GridPanel475";
        }
    }
    protected void ImageButton1_Click1(object sender, EventArgs e)
    {
        //grid_Rule_master.Columns[9].Visible = true;

        //lblcomname.Text = ddlbusiness.SelectedItem.Text;
        //lblApp.Text = ddlapprule.SelectedItem.Text;
        //lblst.Text = ddlstatus.SelectedItem.Text;
        //lblser.Text = "Search " + RadioButtonList1.SelectedItem.Text + " : " + txtSearch.Text;
        //lblser.Visible = true;
        //grid_Rule_master.DataSource = null;
        //grid_Rule_master.DataBind();
        //FillRuleGridSearch();
        lbldate.Text = "From Date " + txtdatefrom.Text + " To Date " + txtdateto.Text;
        lblser.Visible = false;
        lblcomname.Text = ddlbusiness.SelectedItem.Text;
        lblApp.Text = ddlapprule.SelectedItem.Text;
        lblst.Text = ddlstatus.SelectedItem.Text;
        grid_Rule_master.DataSource = null;
        grid_Rule_master.DataBind();
        DataTable dtsts = new DataTable();
        grid_Rule_master.Columns[9].Visible = true;
        if (txtSearch.Text.Length > 0)
        {
            lblser.Text = "Search " + RadioButtonList1.SelectedItem.Text + " : " + txtSearch.Text;
            lblser.Visible = true;
        }
        fildocidtit = " Cast(RuleProcessDate as Date) Between '" + txtdatefrom.Text + "' and '" + txtdateto.Text + "' and ";
        if (txtSearch.Text.Length > 0)
        {
            fildocidtit = "  (Cast(DocumentMaster.DocumentId as nvarchar)='" + txtSearch.Text + "' or (DocumentMaster.DocumentTitle='" + txtSearch.Text + "')) and";
        }
        string docrtyp = "";
        if (ddlapprule.SelectedIndex > 0)
        {
             docrtyp = " and RuleApproveTypeMaster.RuleApproveTypeId='" + ddlapprule.SelectedValue + "'";
        }
        if ((ddlstatus.SelectedIndex > 0) && ((ddlstatus.SelectedValue == "True")))
        {
            grid_Rule_master.Columns[1].HeaderText = "Approval <br/> by Date";        
            //dtsts = clsDocument.SelectDocumentMasterByStatusinDocumentFlowByEmployee(Convert.ToBoolean(ddlstatus.SelectedValue), ddlbusiness.SelectedValue, ddlapprule.SelectedValue);
            dtsts = select("SELECT DISTINCT   DocumentMaster.DocumentId as DocId, DocumentMaster.DocumentTypeId, DocumentMaster.DocumentUploadDate,DocumentMaster.DocumentName, DocumentMaster.DocumentTitle, RuleApproveTypeMaster.Description, DocumentMaster.PartyId,RuleMaster.RuleId,RuleMaster.RuleTitle, RuleApproveTypeMaster.RuleApproveTypeName,RuleDetail.RuleDetailId,Convert(nvarchar, RuleProcessMaster.RuleProcessDate,101) as ProcessDate,RuleProcessMaster.Note,RuleDetail.Days " +
" FROM         DocumentMaster RIGHT OUTER JOIN RuleProcessMaster ON RuleProcessMaster.DocumentId=DocumentMaster.DocumentId RIGHT OUTER JOIN "+
 " RuleDetail ON RuleDetail.RuleDetailId=RuleProcessMaster.RuleDetailId RIGHT OUTER JOIN RuleMaster on "+
 " RuleMaster.RuleId=RuleDetail.RuleId inner join RuleApproveTypeMaster on RuleApproveTypeMaster.RuleApproveTypeId=RuleDetail.RuleApproveTypeId "+
" WHERE " + fildocidtit + " RuleMaster.Whid= '" + ddlbusiness.SelectedValue + "' " + docrtyp + " and   RuleProcessMaster.EmployeeId='" + Session["EmployeeId"] + "' AND (DocumentMaster.DocumentId  IN " +
"(SELECT     RuleProcessMaster_1.DocumentId FROM   RuleProcessMaster AS RuleProcessMaster_1 LEFT OUTER JOIN RuleDetail ON RuleProcessMaster_1.RuleDetailId = RuleDetail.RuleDetailId "+
" WHERE      (RuleProcessMaster_1.EmployeeId = '"+Session["EmployeeId"]+"') And (RuleProcessMaster_1.Approve='" + Convert.ToBoolean(ddlstatus.SelectedValue) + "'))) order by DocId desc");

            grid_Rule_master.Columns[9].Visible = false;
            grid_Rule_master.DataSource = dtsts;
            DataView myDataView = new DataView();
            myDataView = dtsts.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            grid_Rule_master.DataBind();


            foreach (GridViewRow gdr in grid_Rule_master.Rows)
            {
                Label Label1 = (Label)gdr.FindControl("Label1");

                DataTable dt4 = new DataTable();
                dt4 = select("Select RuleProcessMaster.*,Left(RuleProcessMaster.Note,60) as Note1 from RuleProcessMaster inner join RuleDetail on RuleDetail.RuleDetailId=RuleProcessMaster.RuleDetailId where RuleProcessMaster.EmployeeId='" + Convert.ToInt32(Session["EmployeeId"]) + "' and RuleDetail.RuleDetailId='" + grid_Rule_master.DataKeys[gdr.DataItemIndex].Value + "' and RuleProcessMaster.DocumentId='" + Label1.Text + "'");

                if (dt4.Rows.Count > 0)
                {
                    DropDownList rbtnAcceptReject = (DropDownList)gdr.FindControl("rbtnAcceptReject");
                    TextBox txtNote = (TextBox)gdr.FindControl("txtNote");
                    rbtnAcceptReject.SelectedValue = dt4.Rows[0]["Approve"].ToString();
                    Label lbltxtnote = (Label)gdr.FindControl("lbltxtnote");

                    txtNote.Text = dt4.Rows[0]["Note"].ToString();
                    lbltxtnote.Text = dt4.Rows[0]["Note1"].ToString();
                    rbtnAcceptReject.Enabled = false;



                }
                imgbtnSubmit.Visible = false;


            }
            if (grid_Rule_master.Rows.Count > 0)
            {
                LinkButton LinkButton3 = (LinkButton)grid_Rule_master.HeaderRow.FindControl("LinkButton3");

                LinkButton3.Text = "View";
            }

        }
        else if (ddlstatus.SelectedIndex > 0 && ddlstatus.SelectedValue == "False")
        {
            grid_Rule_master.Columns[1].HeaderText = "Document<br>Reject Date";    
            dtsts = select("SELECT DISTINCT   DocumentMaster.DocumentId as DocId, DocumentMaster.DocumentTypeId, DocumentMaster.DocumentUploadDate,DocumentMaster.DocumentName, DocumentMaster.DocumentTitle, RuleApproveTypeMaster.Description, DocumentMaster.PartyId,RuleMaster.RuleId,RuleMaster.RuleTitle, RuleApproveTypeMaster.RuleApproveTypeName,RuleDetail.RuleDetailId,Convert(nvarchar, RuleProcessMaster.RuleProcessDate,101) as ProcessDate,RuleProcessMaster.Note,RuleDetail.Days " +
                " FROM         DocumentMaster RIGHT OUTER JOIN RuleProcessMaster ON RuleProcessMaster.DocumentId=DocumentMaster.DocumentId RIGHT OUTER JOIN " +
                " RuleDetail ON RuleDetail.RuleDetailId=RuleProcessMaster.RuleDetailId RIGHT OUTER JOIN RuleMaster on " +
                " RuleMaster.RuleId=RuleDetail.RuleId inner join RuleApproveTypeMaster on RuleApproveTypeMaster.RuleApproveTypeId=RuleDetail.RuleApproveTypeId " +
                " WHERE  " + fildocidtit + " RuleMaster.Whid= '" + ddlbusiness.SelectedValue + "' " + docrtyp + " and   RuleProcessMaster.EmployeeId='" + Session["EmployeeId"] + "' AND (DocumentMaster.DocumentId  IN " +
                "(SELECT     RuleProcessMaster_1.DocumentId FROM   RuleProcessMaster AS RuleProcessMaster_1 LEFT OUTER JOIN RuleDetail ON RuleProcessMaster_1.RuleDetailId = RuleDetail.RuleDetailId " +
                " WHERE      (RuleProcessMaster_1.EmployeeId = '" + Session["EmployeeId"] + "') And (RuleProcessMaster_1.Approve='" + Convert.ToBoolean(ddlstatus.SelectedValue) + "'))) order by DocId desc");

            //dtsts = clsDocument.SelectDocumentMasterByStatusinDocumentFlowByEmployee(Convert.ToBoolean(ddlstatus.SelectedValue), ddlbusiness.SelectedValue, ddlapprule.SelectedValue);

            grid_Rule_master.Columns[9].Visible = false;
            grid_Rule_master.DataSource = dtsts;
            DataView myDataView = new DataView();
            myDataView = dtsts.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }
            grid_Rule_master.DataBind();


            foreach (GridViewRow gdr in grid_Rule_master.Rows)
            {
                Label Label1 = (Label)gdr.FindControl("Label1");

                DataTable dt4 = new DataTable();
                dt4 = select("Select RuleProcessMaster.*,Left(RuleProcessMaster.Note,60) as Note1 from RuleProcessMaster inner join RuleDetail on RuleDetail.RuleDetailId=RuleProcessMaster.RuleDetailId where RuleProcessMaster.EmployeeId='" + Convert.ToInt32(Session["EmployeeId"]) + "' and RuleDetail.RuleDetailId='" + grid_Rule_master.DataKeys[gdr.DataItemIndex].Value + "' and RuleProcessMaster.DocumentId='" + Label1.Text + "'");
                if (dt4.Rows.Count > 0)
                {
                    DropDownList rbtnAcceptReject = (DropDownList)gdr.FindControl("rbtnAcceptReject");
                    TextBox txtNote = (TextBox)gdr.FindControl("txtNote");
                    rbtnAcceptReject.SelectedValue = dt4.Rows[0]["Approve"].ToString();
                    Label lbltxtnote = (Label)gdr.FindControl("lbltxtnote");
                    rbtnAcceptReject.Enabled = false;

                    txtNote.Text = dt4.Rows[0]["Note"].ToString();
                    lbltxtnote.Text = dt4.Rows[0]["Note1"].ToString();




                }
                imgbtnSubmit.Visible = false;
                
            }
            if (grid_Rule_master.Rows.Count > 0)
            {
                LinkButton LinkButton3 = (LinkButton)grid_Rule_master.HeaderRow.FindControl("LinkButton3");

                LinkButton3.Text = "View";
            }

        }

        else if (ddlstatus.SelectedIndex == 0)
        {
            grid_Rule_master.Columns[1].HeaderText = "Document<br>Upload Date";    
            FillRuleGrid("", "");
            if (grid_Rule_master.Rows.Count > 0)
            {
                LinkButton LinkButton3 = (LinkButton)grid_Rule_master.HeaderRow.FindControl("LinkButton3");

                LinkButton3.Text = "Add/View";
            }
        }


    }


   
    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillapro();
        //ddlapprule_SelectedIndexChanged(sender, e);
    }
    //protected void ddlapprule_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ImageButton1_Click1(sender, e);
    //}
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Print and Export")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Print and Export";
            Button7.Visible = true;
            ddlExport.Visible = true;
            grid_Rule_master.Columns[6].Visible = false;
            if (grid_Rule_master.Columns[9].Visible == true)
            {
                ViewState["vewHide"] = "tt";
                grid_Rule_master.Columns[9].Visible = false;
            }
            if (grid_Rule_master.Columns[10].Visible == true)
            {
                ViewState["msgHide"] = "tt";
                grid_Rule_master.Columns[10].Visible = false;
            }
        }
        else
        {
            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(200);

            Button1.Text = "Print and Export";
            Button7.Visible = false;
            ddlExport.Visible = false;
            grid_Rule_master.Columns[6].Visible = true;
            if (ViewState["vewHide"] != null)
            {
                grid_Rule_master.Columns[9].Visible = true;
            }
            if (ViewState["msgHide"] != null)
            {
                grid_Rule_master.Columns[10].Visible = true;
            }
        }
    }
    protected void grid_Rule_master_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid_Rule_master.PageIndex = e.NewPageIndex;

        ImageButton1_Click1(sender, e);
    }
    protected void linkdow1_Click(object sender, EventArgs e)
    {

        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;

        int rinrow = row.RowIndex;


        int data = Convert.ToInt32(grid_Rule_master.DataKeys[rinrow].Value);

        string strdetailid = "SELECT *  FROM RuleDetail where RuleDetailId='" + data + "'";
        SqlCommand cmddetailid = new SqlCommand(strdetailid, con);
        cmddetailid.CommandType = CommandType.Text;
        SqlDataAdapter dadetailid = new SqlDataAdapter(cmddetailid);
        DataTable dtdetailid = new DataTable();
        dadetailid.Fill(dtdetailid);

        if (dtdetailid.Rows.Count > 0)
        {


            string strds = "SELECT *  FROM RuleApproveTypeMaster where RuleApproveTypeId='" + dtdetailid.Rows[0]["RuleApproveTypeId"].ToString() + "'";
            SqlCommand cmd1ds = new SqlCommand(strds, con);
            cmd1ds.CommandType = CommandType.Text;
            SqlDataAdapter dads = new SqlDataAdapter(cmd1ds);
            DataTable dtds = new DataTable();
            dads.Fill(dtds);

            if (dtds.Rows.Count > 0)
            {
                lblapprovaltype123456.Text = dtds.Rows[0]["RuleApproveTypeName"].ToString();
                lblapprovaldescription123456.Text = dtds.Rows[0]["Description"].ToString();

                ModalPopupExtender1.Show();
            }
        }


    }

    protected void btnsubmitnote_Click(object sender, EventArgs e)
    {




    }
    protected void btnupdatenote_Click(object sender, EventArgs e)
    {


    }
    protected void btncancelnote_Click(object sender, EventArgs e)
    {
    }
    protected void btnaddnotes_Click(object sender, EventArgs e)
    {
        //GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        //int rinrow = row.RowIndex;
        //int data = Convert.ToInt32(grid_Rule_master.DataKeys[rinrow].Value);
        //Label Label1 = (Label)grid_Rule_master.Rows[rinrow].FindControl("Label1");

        //string strds = "SELECT *  FROM RuleProcessMaster where RuleApproveTypeId='" + dtdetailid.Rows[0]["RuleApproveTypeId"].ToString() + "'";
        //SqlCommand cmd1ds = new SqlCommand(strds, con);
        //cmd1ds.CommandType = CommandType.Text;
        //SqlDataAdapter dads = new SqlDataAdapter(cmd1ds);
        //DataTable dtds = new DataTable();
        //dads.Fill(dtds);

        //if (dtds.Rows.Count > 0)
        //{

        //}
        //else
        //{

        //}

    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {

        LinkButton LinkButton3 = (LinkButton)grid_Rule_master.HeaderRow.FindControl("LinkButton3");




        foreach (GridViewRow grd in grid_Rule_master.Rows)
        {
            TextBox txtNote = (TextBox)grd.FindControl("txtNote");
            Label lbltxtnote = (Label)grd.FindControl("lbltxtnote");

            if (LinkButton3.Text == "Add/View" || LinkButton3.Text == "View")
            {

                txtNote.Visible = true;
                lbltxtnote.Visible = false;

            }
            if (LinkButton3.Text == "Hide")
            {

                txtNote.Visible = false;
                lbltxtnote.Visible = true;
            }



        }
        if (LinkButton3.Text == "Add/View" || LinkButton3.Text == "View")
        {
            LinkButton3.Text = "Hide";
        }
        else
        {
            if (ddlstatus.SelectedValue == "True" || ddlstatus.SelectedValue == "False")
            {
                LinkButton3.Text = "View";
            }
            else
            {
                LinkButton3.Text = "Add/View";
            }
        }




    }

    protected DataTable selectbcon(string str)
    {
        SqlCommand cmd = new SqlCommand(str, PageConn.licenseconn());
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);
        return dt;

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


        string avfr = "  and PageMaster.PageName='" + ClsEncDesc.EncDyn("MessageCompose.aspx") + "'";
        DataTable drt = selectbcon("SELECT distinct " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.MenuId,PageMaster.PageName FROM MainMenuMaster inner join " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl on " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.MenuId=MainMenuMaster.MainMenuId inner join PageMaster on PageMaster.MainMenuId=" + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.MenuId  inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.PageId  INNER JOIN  " + PageConn.busdatabase + ".dbo.User_Role ON " + PageConn.busdatabase + ".dbo.RoleMenuAccessRightTbl.RoleId = " + PageConn.busdatabase + ".dbo.User_Role.Role_id INNER JOIN " + PageConn.busdatabase + ".dbo.User_master ON " + PageConn.busdatabase + ".dbo.User_Role.User_id = " + PageConn.busdatabase + ".dbo.User_master.UserID where pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "'" + avfr + " and PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(Session["verId"].ToString()) + "' and  " + PageConn.busdatabase + ".dbo.User_master.UserID ='" + Session["userid"] + "'");
        if (drt.Rows.Count <= 0)
        {

            drt = selectbcon("SELECT PageMaster.PageName FROM PageMaster inner join " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl on " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl.PageId=PageMaster.PageId inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.PageId INNER JOIN " + PageConn.busdatabase + ".dbo.User_Role ON " + PageConn.busdatabase + ".dbo.RolePageAccessRightTbl.RoleId = " + PageConn.busdatabase + ".dbo.User_Role.Role_id INNER JOIN " + PageConn.busdatabase + ".dbo.User_master ON " + PageConn.busdatabase + ".dbo.User_Role.User_id = " + PageConn.busdatabase + ".dbo.User_master.UserID where pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "' " + avfr + " and PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(Session["verId"].ToString()) + "' and  " + PageConn.busdatabase + ".dbo.User_master.UserID ='" + Session["userid"] + "'");
            if (drt.Rows.Count <= 0)
            {
                drt = selectbcon("SELECT distinct PageMaster.PageName FROM MainMenuMaster inner join  SubMenuMaster on SubMenuMaster.MainMenuId=MainMenuMaster.MainMenuId inner join " + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl on " + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl.SubMenuId=SubMenuMaster.SubMenuId inner join PageMaster on PageMaster.SubMenuId=" + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl.SubMenuId  inner join pageplaneaccesstbl on pageplaneaccesstbl.Pageid=PageMaster.PageId  INNER JOIN  " + PageConn.busdatabase + ".dbo.User_Role ON " + PageConn.busdatabase + ".dbo.RoleSubMenuAccessRightTbl.RoleId = " + PageConn.busdatabase + ".dbo.User_Role.Role_id INNER JOIN " + PageConn.busdatabase + ".dbo.User_master ON " + PageConn.busdatabase + ".dbo.User_Role.User_id = " + PageConn.busdatabase + ".dbo.User_master.UserID where pageplaneaccesstbl.Priceplanid='" + ClsEncDesc.EncDyn(Session["PriceId"].ToString()) + "'" + avfr + " and PageMaster.VersionInfoMasterId='" + ClsEncDesc.EncDyn(Session["verId"].ToString()) + "' and  " + PageConn.busdatabase + ".dbo.User_master.UserID ='" + Session["userid"] + "'");


                if (drt.Rows.Count <= 0)
                {


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
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    public void createPDFDoc(String strhtml)
    {
        string strfilename = HttpContext.Current.Server.MapPath("TempDoc/GridViewExport.pdf");

        Document doc = new Document(PageSize.A2, 30f, 30f, 30f, 30f);
        PdfWriter.GetInstance(doc, new FileStream(strfilename, FileMode.Create));
        System.IO.StringReader se = new StringReader(strhtml.ToString());
        HTMLWorker obj = new HTMLWorker(doc);

        doc.Open();
        obj.Parse(se);
        doc.Close();
        Showpdf(strfilename);
    }
    public void Showpdf(string strFileName)
    {
        Response.ClearContent();
        Response.ClearHeaders();
        //Response.AddHeader("Content-Disposition", "inline;filename=" + strFileName);
        //Response.ContentType = "application/pdf";
        //Response.WriteFile(strFileName);
        Response.Flush();
        Response.Clear();
    }
    protected void ddlExport_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (grid_Rule_master .Rows.Count > 0)
        {
            if ( ddlExport.SelectedValue == "1" || ddlExport.SelectedValue == "4")
            {

                //this.EnableViewState = false;

                ////////////////////////////////////////////////////
                //Response.Charset = string.Empty;

                //Document document = new Document(PageSize.A2, 0f, 0f, 30f, 30f);
                //System.IO.MemoryStream msReport = new System.IO.MemoryStream();

                //try
                //{

                //    PdfWriter writer = PdfWriter.GetInstance(document, msReport);

                //    document.AddSubject("Export to PDF");

                //    document.Open();

                //    iTextSharp.text.Table datatable = new iTextSharp.text.Table(8);

                //    datatable.Padding = 2;
                //    datatable.Spacing = 1;
                //    datatable.Width = 90;
                //    float[] headerwidths = new float[8];
                //    //for (int i = 0; i < 8; i++)
                //    //{
                //    headerwidths[0] = 5;
                //    headerwidths[1] = 8;
                //    headerwidths[2] = 20;
                //    headerwidths[3] = 15;
                //    headerwidths[4] = 15;
                //    headerwidths[5] = 4;
                //    headerwidths[6] = 8;
                //    headerwidths[7] = 25;
                //    //}
                //    datatable.Widths = headerwidths;

                //    Cell cell = new Cell(new Phrase("Business Name :" + ddlbusiness.SelectedItem.Text, FontFactory.GetFont(FontFactory.HELVETICA, 16, Font.BOLD)));
                //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //    //cell.Leading = 30;
                //    cell.BorderWidth = 0.001f;
                //    cell.Colspan = 8;
                //    cell.Border = Rectangle.NO_BORDER;

                //    datatable.AddCell(cell);

                //    datatable.DefaultCellBorderWidth = 1;
                //    datatable.DefaultHorizontalAlignment = 1;

                //    Cell cell3 = new Cell(new Phrase("List of Documents for My Approval (With Document Approval Flow Rule)", FontFactory.GetFont(FontFactory.HELVETICA, 16, Font.BOLD)));
                //    cell3.HorizontalAlignment = Element.ALIGN_CENTER;


                //    cell3.Colspan = 8;
                //    cell3.Border = Rectangle.NO_BORDER;

                //    datatable.AddCell(cell3);

                //    Cell cell1 = new Cell(new Phrase(Label2.Text + " " + lblApp.Text, FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLD)));
                //    cell1.HorizontalAlignment = Element.ALIGN_LEFT;

                //    cell1.Colspan = 8;

                //    cell1.Border = Rectangle.NO_BORDER;

                //    datatable.AddCell(cell1);

                //    Cell cell2 = new Cell(new Phrase(Label3.Text + " " + lblst.Text, FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLD)));
                //    cell2.HorizontalAlignment = Element.ALIGN_LEFT;
                //    cell2.Colspan = 8;
                //    cell2.Border = Rectangle.NO_BORDER;
                //    datatable.AddCell(cell2);
                //    datatable.DefaultHorizontalAlignment = Element.ALIGN_LEFT;

                //    Cell cell6 = new Cell(new Phrase(lbldate.Text, FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLD)));
                //    cell6.HorizontalAlignment = Element.ALIGN_LEFT;
                //    cell6.Colspan = 8;
                //    cell6.Border = Rectangle.NO_BORDER;
                //    datatable.AddCell(cell6);
                //    datatable.DefaultHorizontalAlignment = Element.ALIGN_LEFT;


                //    Cell cell5 = new Cell(new Phrase(lblser.Text, FontFactory.GetFont(FontFactory.HELVETICA, 15, Font.BOLD)));
                //    cell5.HorizontalAlignment = Element.ALIGN_LEFT;
                //    cell5.Colspan = 8;
                //    cell5.Border = Rectangle.NO_BORDER;
                //    datatable.AddCell(cell5);

                //    datatable.DefaultHorizontalAlignment = Element.ALIGN_LEFT;

                //    datatable.AddCell(new Cell(new Phrase("DocId", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));

                //    datatable.AddCell(new Cell(new Phrase(grid_Rule_master.Columns[1].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                //    datatable.AddCell(new Cell(new Phrase("DocumentTitle", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                //    datatable.AddCell(new Cell(new Phrase("Approval Rule", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                //    datatable.AddCell(new Cell(new Phrase("Approval Type", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                //    datatable.AddCell(new Cell(new Phrase("Approval History", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                //    datatable.AddCell(new Cell(new Phrase("Accept/Reject", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));
                //    datatable.AddCell(new Cell(new Phrase("Approval Notes", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))));

                //    for (int i = 0; i < grid_Rule_master.Rows.Count; i++)
                //    {
                //        datatable.DefaultHorizontalAlignment = Element.ALIGN_LEFT;

                //        Label lbldoid = (Label)grid_Rule_master.Rows[i].FindControl("Label1");
                //        Label lblapprovetobedate = (Label)grid_Rule_master.Rows[i].FindControl("lblapprovetobedate");
                //        string Tit = grid_Rule_master.Rows[i].Cells[2].Text;
                //        Label lblruletitle123 = (Label)grid_Rule_master.Rows[i].FindControl("lblruletitle123");
                //        Label txtDescraa = (Label)grid_Rule_master.Rows[i].FindControl("txtDescraa");
                      
                //        ImageButton ImageButton2approval = (ImageButton)grid_Rule_master.Rows[i].FindControl("ImageButton2approval");
                //        DropDownList rbtnAcceptReject = (DropDownList)grid_Rule_master.Rows[i].FindControl("rbtnAcceptReject");
                //        Label lbltxtnote = (Label)grid_Rule_master.Rows[i].FindControl("lbltxtnote");

                //        datatable.AddCell(lbldoid.Text);
                //        datatable.AddCell(lblapprovetobedate.Text);
                //        datatable.AddCell(Tit);
                //        datatable.AddCell(lblruletitle123.Text);
                //        datatable.AddCell(txtDescraa.Text);
                //        datatable.AddCell(ImageButton2approval.AlternateText);

                //        datatable.AddCell(rbtnAcceptReject.SelectedItem.Text);


                //        datatable.AddCell(lbltxtnote.Text);


                //    }
                //    document.Add(datatable);
                //}
                //catch (Exception ex)
                //{
                //    Response.Write(ex);
                //}
                //document.Close();
                //Response.Clear();
                //Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
                //Response.ContentType = "application/pdf";
                //Response.BinaryWrite(msReport.ToArray());
                //Response.End();
                 Response.Buffer = true;
                Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                string filename = "GrdM_" + System.DateTime.Today.Day + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second;
                pnlgrid.RenderControl(hw);
                string style = "";
                string path = HttpContext.Current.Server.MapPath("TempDoc/" + filename + ".Doc");
                System.IO.File.WriteAllText(path, style + sw.ToString());

                //set exportformat to pdf
                Microsoft.Office.Interop.Word.WdExportFormat paramExportFormat = Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF;
                bool paramOpenAfterExport = false;
                Microsoft.Office.Interop.Word.WdExportOptimizeFor paramExportOptimizeFor = Microsoft.Office.Interop.Word.WdExportOptimizeFor.wdExportOptimizeForPrint;
                Microsoft.Office.Interop.Word.WdExportRange paramExportRange = Microsoft.Office.Interop.Word.WdExportRange.wdExportAllDocument;
                int paramStartPage = 0;
                int paramEndPage = 0;

                Microsoft.Office.Interop.Word.WdExportItem paramExportItem = Microsoft.Office.Interop.Word.WdExportItem.wdExportDocumentContent;
                bool paramIncludeDocProps = true;
                bool paramKeepIRM = true;
                Microsoft.Office.Interop.Word.WdExportCreateBookmarks paramCreateBookmarks = Microsoft.Office.Interop.Word.WdExportCreateBookmarks.wdExportCreateWordBookmarks;

                bool paramDocStructureTags = true;
                bool paramBitmapMissingFonts = true;
                bool paramUseISO19005_1 = true;
                object paramSourceDocPath = HttpContext.Current.Server.MapPath("TempDoc/" + filename + ".Doc");
                string paramExportFilePath = HttpContext.Current.Server.MapPath("TempDoc/" + filename + ".pdf");
                Session["Emfile"] = filename + ".pdf";
                Session["GrdmailA"] = null;

                Microsoft.Office.Interop.Word.Application wordApp = null;
                wordApp = new Microsoft.Office.Interop.Word.Application();

                wordApp.Documents.Open(ref paramSourceDocPath, ref paramMissing, ref paramMissing,
                                            ref paramMissing, ref paramMissing, ref paramMissing,
                                            ref paramMissing, ref paramMissing, ref paramMissing,
                                            ref paramMissing, ref paramMissing, ref paramMissing,
                                            ref paramMissing, ref paramMissing, ref paramMissing,
                                            ref paramMissing);

                wordApp.ActiveDocument.ExportAsFixedFormat(paramExportFilePath,
                                                                        paramExportFormat, paramOpenAfterExport,
                                                                        paramExportOptimizeFor, paramExportRange, paramStartPage,
                                                                        paramEndPage, paramExportItem, paramIncludeDocProps,
                                                                        paramKeepIRM, paramCreateBookmarks, paramDocStructureTags,
                                                                        paramBitmapMissingFonts, paramUseISO19005_1,
                                                                        ref paramMissing);


                if (wordApp != null)
                {
                    wordApp.Quit(ref paramMissing, ref paramMissing, ref paramMissing);

                    wordApp = null;
                }
                if (ddlExport.SelectedValue == "4")
                {

                    string te = "MessageComposeExt.aspx?ema=Azxcvyute";
                    try
                    {
                        System.Threading.Thread.Sleep(100);
                        File.Delete(path);
                    }
                    catch
                    {
                    }
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

                }
                else
                {
                    Response.Clear();
                    Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
                    Response.ContentType = "application/pdf";
                    Response.TransmitFile(paramExportFilePath);
                    Response.End();
                }


            
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


                //Change the Header Row back to white color

                grid_Rule_master.HeaderRow.Style.Add("background-color", "#FFFFFF");


                for (int i = 0; i < grid_Rule_master.Rows.Count; i++)
                {

                    GridViewRow row = grid_Rule_master.Rows[i];

                    //Change Color back to white

                    row.BackColor = System.Drawing.Color.White;

                    //Apply text style to each Row

                    row.Attributes.Add("class", "textmode");


                }

                pnlgrid.RenderControl(hw);

                //style to format numbers to string

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
            
        }

    }
    

}
