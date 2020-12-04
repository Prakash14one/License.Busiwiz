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
using System.Data.SqlClient;

public partial class Account_MessageCompose : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(PageConn.connnn);
    MessageCls clsMessage = new MessageCls();
    MasterCls1 clsMaster = new MasterCls1();
    DataTable dt = new DataTable();
    EmployeeCls clsEmployee = new EmployeeCls();
    DocumentCls1 clsDocument = new DocumentCls1();
    string str = System.DateTime.Now.ToString();

    protected void Page_Load(object sender, EventArgs e)
    {

        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        //PageConn pgcon = new PageConn();

        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i12 = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i12 - 1].ToString();
        Session["PageUrl"] = strData;
        Session["PageName"] = page;
        Page.Title = pg.getPageTitle(page);
        DataTable dsss = new DataTable();

        pnlmsg.Visible = false;
        if (Session["CompanyName"] != null)
        {
            this.Title = Session["CompanyName"] + " IFileCabinet.com Compose Message ";
        }

        Session["PageName"] = "MessageCompose.aspx";

        int i = Convert.ToInt32(Request.QueryString["id"]);
        if (Convert.ToString(Request.QueryString["id"]) != null)
        {
            AddAttachment(i);
        }
        if (!Page.IsPostBack)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            string str = "SELECT WareHouseId,Name,Address,CurrencyId  FROM WareHouseMaster where comid = '" + Session["comid"] + "'and WareHouseMaster.Status='" + 1 + "' order by name";

            SqlCommand cmd1 = new SqlCommand(str, con);
            cmd1.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlstore.DataSource = dt;
            ddlstore.DataTextField = "Name";
            ddlstore.DataValueField = "WareHouseId";
            ddlstore.DataBind();

            DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

            if (dteeed.Rows.Count > 0)
            {
                ddlstore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
            }
            fillusertype();
            Session["EmployeeIdep"] = Session["EmployeeId"];
            if (Request.QueryString["apd"] != null)
            {

                DataTable dt4 = new DataTable();
                dt4 = select("Select RuleApproveTypeMaster.Whid,RuleApproveTypeMaster.RuleApproveTypeId from  RuleDetail  inner join RuleApproveTypeMaster on RuleApproveTypeMaster.RuleApproveTypeId=RuleDetail.RuleApproveTypeId   where RuleDetail.EmployeeId='" + Convert.ToInt32(Session["EmployeeId"]) + "' and RuleDetail.RuleDetailId='" + Request.QueryString["Rd"] + "'");
                if (dt4.Rows.Count > 0)
                {
                    ddlstore.SelectedValue = dt4.Rows[0]["Whid"].ToString();

                    FillPartyGrid();
                    txtsub.Text = "Approval Document No - " + Request.QueryString["apd"].ToString();
                    FillFileAttachDetail();
                }
            }
            else if (Request.QueryString["wid"] != null)
            {
                ddlstore.SelectedValue = Request.QueryString["wid"].ToString();
                FillPartyGrid();
                txtsub.Text = "Doc Message";
                multimessage();

            }

            else
            {
                FillPartyGrid();
                fillcompanyname();
                filljobapplied();
                sugnature();
                fillphoto();
                CheckBox1_CheckedChanged(sender, e);
            }
            if (Request.QueryString["MsgId"] != null)
            {
                FillDraftDetail();
                FillFileAttachDetail();
            }
            if (Request.QueryString["MsgDetailIdR"] != null)
            {
                FillReplyDetail();

            }
            if (Request.QueryString["MsgDetailIdF"] != null)
            {
                FillForwardDetail();
            }
            if (Request.QueryString["DocId"] != null)
            {
                txtsub.Text = "Query about Document Approval Status for Document  No - " + Request.QueryString["DocId"].ToString();
                DataTable dt45 = select("Select EmployeeMaster.Whid from  EmployeeMaster  where EmployeeMasterId='" + Request.QueryString["EmpId"] + "'");
                if (dt45.Rows.Count > 0)
                {
                    ddlstore.SelectedValue = dt45.Rows[0]["Whid"].ToString();
                }

                FillPartyNameforApproval();
            }


        }

    }
    protected void FillPartyNameforApproval()
    {
        clsMaster = new MasterCls1();
        dt = new DataTable();
        dt = clsMaster.SelectPartyMasterEmpIdwise(Convert.ToInt32(Request.QueryString["EmpId"].ToString()), ddlstore.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            DataTable dt1 = new DataTable();
            DataColumn dtcom = new DataColumn();
            dtcom.DataType = System.Type.GetType("System.Int32");
            dtcom.ColumnName = "PartyId";
            dtcom.ReadOnly = false;
            dtcom.Unique = false;
            dtcom.AllowDBNull = true;
            dt1.Columns.Add(dtcom);

            DataColumn dtcom1 = new DataColumn();
            dtcom1.DataType = System.Type.GetType("System.String");
            dtcom1.ColumnName = "Name";
            dtcom1.ReadOnly = false;
            dtcom1.Unique = false;
            dtcom1.AllowDBNull = true;
            dt1.Columns.Add(dtcom1);
            DataRow drow = dt1.NewRow();
            drow["PartyId"] = dt.Rows[0]["PartyId"].ToString(); // ToPartyId.ToString();
            drow["Name"] = dt.Rows[0]["PartyName"].ToString(); // GR.Cells[1].Text;
            dt1.Rows.Add(drow);
            Session["to"] = dt1;
            lblAddresses.Text = dt.Rows[0]["PartyName"].ToString(); ;
        }
    }
    protected void FillForwardDetail()
    {
        Int32 MsgDetailId = Convert.ToInt32(Request.QueryString["MsgDetailIdF"]);
        dt = new DataTable();
        dt = clsMessage.SelectMsgforDetail(MsgDetailId);
        if (dt.Rows.Count > 0)
        {
            txtsub.Text = "Fw : " + dt.Rows[0]["MsgSubject"].ToString();
            TxtMsgDetail.Text = dt.Rows[0]["MsgDetail"].ToString();
            FillFileAttachDetailForward();
        }

    }
    protected void FillFileAttachDetailForward()
    {
        gridFileAttach.DataSource = null;
        gridFileAttach.DataBind();

        dt = new DataTable();
        Int32 MsgId;
        MsgId = 0;
        DataTable dtMain = new DataTable();
        Int32 MsgDetailId = Convert.ToInt32(Request.QueryString["MsgDetailIdF"]);
        dt = clsMessage.SelectMsgIdUsingMsgDetailId(MsgDetailId);
        if (dt.Rows.Count > 0)
        {
            MsgId = Convert.ToInt32(dt.Rows[0]["MsgId"].ToString());
        }
        dt = new DataTable();
        dt = clsMessage.SelectMsgforFileAttach(MsgId);
        if (dt.Rows.Count > 0)
        {
            if (Session["GridFileAttach1"] == null)
            {

                DataColumn dtcom2 = new DataColumn();
                dtcom2.DataType = System.Type.GetType("System.String");
                dtcom2.ColumnName = "FileName";
                dtcom2.ReadOnly = false;
                dtcom2.Unique = false;
                dtcom2.AllowDBNull = true;
                dtMain.Columns.Add(dtcom2);
                DataColumn dtcom3 = new DataColumn();
                dtcom3.DataType = System.Type.GetType("System.String");
                dtcom3.ColumnName = "FileNameChanged";
                dtcom3.ReadOnly = false;
                dtcom3.Unique = false;
                dtcom3.AllowDBNull = true;
                dtMain.Columns.Add(dtcom3);
            }
            else
            {
                dtMain = (DataTable)Session["GridFileAttach1"];
            }
            foreach (DataRow DR in dt.Rows)
            {
                DataRow dtrow = dtMain.NewRow();
                dtrow["FileName"] = DR["FileName"].ToString();
                dtrow["FileNameChanged"] = DR["FileName"].ToString();
                dtMain.Rows.Add(dtrow);

                chkattach.Checked = true;
                PnlFileAttachLbl.Visible = true;
            }
            Session["GridFileAttach1"] = dtMain;
            gridFileAttach.DataSource = dtMain;
            gridFileAttach.DataBind();
            setGridisze();
            ////
        }
    }
    protected void FillReplyDetail()
    {
        Int32 MsgDetailId = Convert.ToInt32(Request.QueryString["MsgDetailIdR"]);
        dt = new DataTable();
        dt = clsMessage.SelectMsgforDetail(MsgDetailId);
        if (dt.Rows.Count > 0)
        {
            txtsub.Text = "Re : " + dt.Rows[0]["MsgSubject"].ToString();
            //    TxtMsgDetail.Text = dt.Rows[0]["MsgDetail"].ToString();
            Int32 frmprt = Convert.ToInt32(dt.Rows[0]["PartyId"].ToString());
            DataTable dtprt = new DataTable();
            dtprt = clsMessage.SelectPartynamebypartyid(frmprt);
            if (dtprt.Rows.Count > 0)
            {
                lblAddresses.Text = dtprt.Rows[0]["PartyName"].ToString();
            }
            if (grdPartyList.Rows.Count > 0)
            {
                foreach (GridViewRow GR in grdPartyList.Rows)
                {
                    Int32 ToPartyId = Convert.ToInt32(grdPartyList.DataKeys[GR.RowIndex].Value);
                    if (ToPartyId.ToString() == dt.Rows[0]["PartyId"].ToString())
                    {
                        CheckBox chk = (CheckBox)GR.FindControl("chkParty");
                        chk.Checked = true;
                        break;
                    }
                }
            }
        }
    }

    protected void multimessage()
    {
        dt = new DataTable();
        DataTable dtMain = new DataTable();

        DataColumn dtcom2 = new DataColumn();
        dtcom2.DataType = System.Type.GetType("System.String");
        dtcom2.ColumnName = "FileName";
        dtcom2.ReadOnly = false;
        dtcom2.Unique = false;
        dtcom2.AllowDBNull = true;
        dtMain.Columns.Add(dtcom2);
        DataColumn dtcom3 = new DataColumn();
        dtcom3.DataType = System.Type.GetType("System.String");
        dtcom3.ColumnName = "FileNameChanged";
        dtcom3.ReadOnly = false;
        dtcom3.Unique = false;
        dtcom3.AllowDBNull = true;
        dtMain.Columns.Add(dtcom3);



        DataTable dt4 = select("Select * From DocumentMaster Where  DocumentId In(" + Session["did"] + ")");
        foreach (DataRow dts in dt4.Rows)
        {
            DataRow dtrow = dtMain.NewRow();
            dtrow["FileName"] = dts["DocumentName"].ToString();
            dtrow["FileNameChanged"] = dts["DocumentTitle"].ToString();
            dtMain.Rows.Add(dtrow);
        }


        PnlFileAttachLbl.Visible = true;
        Session["GridFileAttach1"] = dtMain;
        gridFileAttach.DataSource = dtMain;
        gridFileAttach.DataBind();

    }
    protected void FillFileAttachDetail()
    {
        gridFileAttach.DataSource = null;
        gridFileAttach.DataBind();

        dt = new DataTable();
        DataTable dtMain = new DataTable();
        if (Request.QueryString["apd"] == null)
        {
            Int32 MsgId = Convert.ToInt32(Request.QueryString["MsgId"]);
            dt = clsMessage.SelectMsgforFileAttach(MsgId);
            if (dt.Rows.Count > 0)
            {
                if (Session["GridFileAttach1"] == null)
                {

                    DataColumn dtcom2 = new DataColumn();
                    dtcom2.DataType = System.Type.GetType("System.String");
                    dtcom2.ColumnName = "FileName";
                    dtcom2.ReadOnly = false;
                    dtcom2.Unique = false;
                    dtcom2.AllowDBNull = true;
                    dtMain.Columns.Add(dtcom2);
                    DataColumn dtcom3 = new DataColumn();
                    dtcom3.DataType = System.Type.GetType("System.String");
                    dtcom3.ColumnName = "FileNameChanged";
                    dtcom3.ReadOnly = false;
                    dtcom3.Unique = false;
                    dtcom3.AllowDBNull = true;
                    dtMain.Columns.Add(dtcom3);
                }
                else
                {
                    dtMain = (DataTable)Session["GridFileAttach1"];
                }
                foreach (DataRow DR in dt.Rows)
                {

                    DataRow dtrow = dtMain.NewRow();
                    dtrow["FileName"] = DR["FileName"].ToString();
                    dtrow["FileNameChanged"] = DR["FileName"].ToString();
                    dtMain.Rows.Add(dtrow);
                    chkattach.Checked = true;
                    PnlFileAttachLbl.Visible = true;
                }
                PnlFileAttachLbl.Visible = true;
                Session["GridFileAttach1"] = dtMain;
                gridFileAttach.DataSource = dtMain;
                gridFileAttach.DataBind();
                setGridisze();
            }
        }
        else if (Request.QueryString["apd"] != null)
        {



            DataColumn dtcom2 = new DataColumn();
            dtcom2.DataType = System.Type.GetType("System.String");
            dtcom2.ColumnName = "FileName";
            dtcom2.ReadOnly = false;
            dtcom2.Unique = false;
            dtcom2.AllowDBNull = true;
            dtMain.Columns.Add(dtcom2);
            DataColumn dtcom3 = new DataColumn();
            dtcom3.DataType = System.Type.GetType("System.String");
            dtcom3.ColumnName = "FileNameChanged";
            dtcom3.ReadOnly = false;
            dtcom3.Unique = false;
            dtcom3.AllowDBNull = true;
            dtMain.Columns.Add(dtcom3);



            DataTable dt4 = select("Select * From DocumentMaster Where  DocumentId='" + Request.QueryString["apd"] + "'");
            if (dt4.Rows.Count > 0)
            {
                DataRow dtrow = dtMain.NewRow();
                dtrow["FileName"] = dt4.Rows[0]["DocumentName"].ToString();
                dtrow["FileNameChanged"] = dt4.Rows[0]["DocumentTitle"].ToString();
                dtMain.Rows.Add(dtrow);
                chkattach.Checked = true;
                PnlFileAttachLbl.Visible = true;
            }


            PnlFileAttachLbl.Visible = true;
            Session["GridFileAttach1"] = dtMain;
            gridFileAttach.DataSource = dtMain;
            gridFileAttach.DataBind();
        }

    }
    protected void FillDraftDetail()
    {
        dt = new DataTable();
        Int32 MsgId = Convert.ToInt32(Request.QueryString["MsgId"]);
        dt = clsMessage.SelectMsgforDraftDetail(MsgId);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow DR in dt.Rows)
            {
                if (grdPartyList.Rows.Count > 0)
                {
                    foreach (GridViewRow GR in grdPartyList.Rows)
                    {
                        Int32 ToPartyId = Convert.ToInt32(grdPartyList.DataKeys[GR.RowIndex].Value);
                        if (ToPartyId.ToString() == DR["ToPartyId"].ToString())
                        {
                            CheckBox chk = (CheckBox)GR.FindControl("chkParty");
                            chk.Checked = true;
                            break;
                        }
                    }
                }
                TxtMsgDetail.Text = DR["MsgDetail"].ToString();
                txtsub.Text = DR["MsgSubject"].ToString();
                //   lblAddresses.Text = DR["ToPartyId"].ToString();
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

    protected void fillcompanyname()
    {
        ddlcompname.Items.Clear();

        //string str = "SELECT distinct Party_master.Compname,Party_master.PartyID FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId where Party_master.Whid='" + ddlstore.SelectedValue + "' and PartytTypeMaster.PartyTypeId='" + ddlusertype.SelectedValue + "' and AccountMaster.Status='1' order by Party_master.Compname";
        string str = "select [PartyTypeId],[PartType] from [PartytTypeMaster] inner join [PartyMasterCategory] on [PartyMasterCategory].[PartyMasterCategoryNo]=[PartytTypeMaster].[PartyCategoryId]  where [PartytTypeMaster].[compid]='" + Session["Comid"] + "' and [PartytTypeMaster].[PartyCategoryId]='" + ddlusertype.SelectedValue + "'";

        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        ddlcompname.DataSource = dt;
        ddlcompname.DataTextField = "PartType";
        ddlcompname.DataValueField = "PartyTypeId";
        ddlcompname.DataBind();

        ddlcompname.Items.Insert(0, "All");
        ddlcompname.Items[0].Value = "0";
    }

    protected void filljobapplied()
    {
        ddlcandi.Items.Clear();

        string str11 = "select VacancyPositionTitle,ID from VacancyPositionTitleMaster where Active='1' order by VacancyPositionTitle asc";
        SqlCommand cmd11 = new SqlCommand(str11, con);
        SqlDataAdapter adp11 = new SqlDataAdapter(cmd11);
        DataTable dt11 = new DataTable();
        adp11.Fill(dt11);

        if (dt11.Rows.Count > 0)
        {
            ddlcandi.DataSource = dt11;
            ddlcandi.DataTextField = "VacancyPositionTitle";
            ddlcandi.DataValueField = "ID";
            ddlcandi.DataBind();
        }
        ddlcandi.Items.Insert(0, "All");
        ddlcandi.Items[0].Value = "0";
    }

    protected void FillPartyGrid()
    {
        pnlusertypeother.Visible = false;
        pnlusertypecandidate.Visible = false;

        dt = new DataTable();
        if (Request.QueryString["pid"] != null)
        {
            grdPartyList.Columns[0].Visible = true;
            grdPartyList.Columns[1].Visible = false;
            grdPartyList.Columns[2].Visible = true;
            grdPartyList.Columns[3].Visible = false;
            grdPartyList.Columns[4].Visible = true;
            grdPartyList.Columns[5].Visible = false;
            grdPartyList.Columns[6].Visible = false;

            dt = select("SELECT   distinct Party_master.Whid, Party_master.Compname,PartytTypeMaster.PartType,AccountMaster.Status " +
            " FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId " +
            " where  (Party_master.PartyId='" + Request.QueryString["pid"] + "')and(AccountMaster.Status='1')");
            if (dt.Rows.Count > 0)
            {
                ddlstore.SelectedValue = dt.Rows[0]["Whid"].ToString();
            }
        }
        else
        {

        }


        //dt = select("SELECT   distinct  Party_master.Compname,PartytTypeMaster.PartType, Party_master.PartyID,AccountMaster.Status " +
        //" FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId " +
        //" where  (Party_master.Whid='" + ddlstore.SelectedValue + "')and(AccountMaster.Status='1')");

        if (ddlusertype.SelectedItem.Text == "Employee")
        {
            // dt = select("SELECT distinct employeepayrollmaster.LastName + ' : ' + employeepayrollmaster.FirstName as Name,Party_master.Compname,Party_master.Contactperson, DepartmentmasterMNC.Departmentname + ' : ' + DesignationMaster.DesignationName as dname,Party_master.PartyID FROM Party_master  inner join PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId  inner join Employeemaster on Employeemaster.PartyID=Party_master.PartyID inner join employeepayrollmaster on employeepayrollmaster.EmpId=Employeemaster.EmployeeMasterID inner join DepartmentmasterMNC on DepartmentmasterMNC.id=Employeemaster.DeptID inner join DesignationMaster on DesignationMaster.DesignationMasterId=Employeemaster.DesignationMasterId where Party_master.Whid='" + ddlstore.SelectedValue + "' and PartytTypeMaster.PartyTypeId='" + ddlusertype.SelectedValue + "' order by Name");

            SqlDataAdapter daggg = new SqlDataAdapter("select EmployeeMaster.DesignationMasterId from EmployeeMaster where EmployeeMasterID='" + Session["EmployeeId"] + "'", con);
            DataTable dtggg = new DataTable();
            daggg.Fill(dtggg);


            SqlDataAdapter daggg1 = new SqlDataAdapter("select MessageCenterRightsTbl.Employee,MessageCenterRightsTbl.BusinessID,MessageCenterRightsTbl.Business from MessageCenterRightsTbl where DesignationID='" + dtggg.Rows[0]["DesignationMasterId"].ToString() + "'", con);
            DataTable dtggg1 = new DataTable();
            daggg1.Fill(dtggg1);

            if (dtggg1.Rows.Count > 0)
            {
                if (Convert.ToString(dtggg1.Rows[0]["Employee"]) == "True" && Convert.ToString(dtggg1.Rows[0]["Business"]) == "True")
                {
                    string str1 = "";
                    string mes1 = "";
                    if (txtsearch.Text != "")
                    {
                        mes1 = " and (Employeemaster.EmployeeName like '%" + txtsearch.Text.Replace("'", "''") + "%')";
                    }

                    //str1 = "SELECT distinct Employeemaster.EmployeeName as Name,Party_master.Compname,Party_master.Contactperson,Party_master.PartyID FROM Party_master  inner join PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId  inner join Employeemaster on Employeemaster.PartyID=Party_master.PartyID  where Party_master.Whid='" + ddlstore.SelectedValue + "' and PartytTypeMaster.PartyTypeId='" + ddlusertype.SelectedValue + "' and EmployeeMaster.Active=1 " + mes1 + "  order by Name";
                    str1 = " SELECT distinct Employeemaster.EmployeeName as Name,Party_master.Compname,Party_master.Contactperson,Party_master.PartyID FROM Party_master inner join PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId inner join PartyMasterCategory on PartyMasterCategory.PartyMasterCategoryNo=PartytTypeMaster.PartyCategoryId inner join Employeemaster on Employeemaster.PartyID=Party_master.PartyID where Party_master.Whid='" + ddlstore.SelectedValue + "' and PartyMasterCategory.PartyMasterCategoryNo='" + ddlusertype.SelectedValue + "' and EmployeeMaster.Active=1 " + mes1 + "  order by Name";
                    SqlDataAdapter dal = new SqlDataAdapter(str1, con);
                    dt = new DataTable();
                    dal.Fill(dt);

                    grdPartyList.Columns[0].Visible = true;
                    grdPartyList.Columns[1].Visible = false;
                    grdPartyList.Columns[2].Visible = false;
                    grdPartyList.Columns[3].Visible = true;
                    grdPartyList.Columns[4].Visible = false;
                    grdPartyList.Columns[5].Visible = false;
                    grdPartyList.Columns[6].Visible = false;
                }
                if (Convert.ToString(dtggg1.Rows[0]["Employee"]) == "True" && Convert.ToString(dtggg1.Rows[0]["Business"]) == "False")
                {
                    string str1 = "";
                    string mes1 = "";

                    if (txtsearch.Text != "")
                    {
                        mes1 = " and (Employeemaster.EmployeeName like '%" + txtsearch.Text.Replace("'", "''") + "%')";
                    }

                    str1 = " SELECT distinct Employeemaster.EmployeeName as Name,Party_master.Compname,Party_master.Contactperson,Party_master.PartyID FROM Party_master inner join PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId inner join PartyMasterCategory on PartyMasterCategory.PartyMasterCategoryNo=PartytTypeMaster.PartyCategoryId inner join Employeemaster on Employeemaster.PartyID=Party_master.PartyID where Party_master.Whid='" + ddlstore.SelectedValue + "' and PartyMasterCategory.PartyMasterCategoryNo='" + ddlusertype.SelectedValue + "' and EmployeeMaster.Whid='" + dtggg1.Rows[0]["BusinessID"].ToString() + "' and EmployeeMaster.Active=1 " + mes1 + "  order by Name";
                    SqlDataAdapter dal = new SqlDataAdapter(str1, con);
                    dt = new DataTable();
                    dal.Fill(dt);

                    grdPartyList.Columns[0].Visible = true;
                    grdPartyList.Columns[1].Visible = false;
                    grdPartyList.Columns[2].Visible = false;
                    grdPartyList.Columns[3].Visible = true;
                    grdPartyList.Columns[4].Visible = false;
                    grdPartyList.Columns[5].Visible = false;
                    grdPartyList.Columns[6].Visible = false;
                }
            }

            ////str1 = "SELECT distinct Employeemaster.EmployeeName as Name,Party_master.Compname,Party_master.Contactperson,Party_master.PartyID FROM Party_master  inner join PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId  inner join Employeemaster on Employeemaster.PartyID=Party_master.PartyID  where Party_master.Whid='" + ddlstore.SelectedValue + "' and PartytTypeMaster.PartyTypeId='" + ddlusertype.SelectedValue + "' and EmployeeMaster.Active=1 " + mes1 + "  order by Name";
            //str1 = "SELECT distinct Employeemaster.EmployeeName as Name,Party_master.Compname,Party_master.Contactperson,Party_master.PartyID FROM Party_master  inner join PartytTypeMaster ON Party_master.PartyTypeId = PartytTypeMaster.PartyTypeId  inner join Employeemaster on Employeemaster.PartyID=Party_master.PartyID inner join [MessageCenterRightsTbl] on [MessageCenterRightsTbl].[DesignationID]=EmployeeMaster.DesignationMasterId where Party_master.Whid='" + ddlstore.SelectedValue + "' and [MessageCenterRightsTbl].[Employee]=1 and PartytTypeMaster.PartyTypeId='" + ddlusertype.SelectedValue + "' and EmployeeMaster.Active=1 " + mes1 + " order by Name";


        }
        if (ddlusertype.SelectedItem.Text == "Customer")
        {
            SqlDataAdapter daggg = new SqlDataAdapter("select EmployeeMaster.DesignationMasterId from EmployeeMaster where EmployeeMasterID='" + Session["EmployeeId"] + "'", con);
            DataTable dtggg = new DataTable();
            daggg.Fill(dtggg);


            SqlDataAdapter daggg1 = new SqlDataAdapter("select MessageCenterRightsTbl.Customer,MessageCenterRightsTbl.BusinessID,MessageCenterRightsTbl.Business from MessageCenterRightsTbl where DesignationID='" + dtggg.Rows[0]["DesignationMasterId"].ToString() + "'", con);
            DataTable dtggg1 = new DataTable();
            daggg1.Fill(dtggg1);

            if (dtggg1.Rows.Count > 0)
            {
                if (Convert.ToString(dtggg1.Rows[0]["Customer"]) == "True" && Convert.ToString(dtggg1.Rows[0]["Business"]) == "True")
                {

                    pnlusertypeother.Visible = true;

                    grdPartyList.Columns[0].Visible = true;
                    grdPartyList.Columns[1].Visible = true;
                    grdPartyList.Columns[2].Visible = true;
                    grdPartyList.Columns[2].HeaderText = "Company Name";
                    grdPartyList.Columns[3].Visible = false;
                    grdPartyList.Columns[4].Visible = false;
                    grdPartyList.Columns[5].Visible = false;
                    grdPartyList.Columns[6].Visible = false;

                    string mes2 = "";

                    if (txtsearch.Text != "")
                    {
                        mes2 = " and ((Party_master.Compname like '%" + txtsearch.Text.Replace("'", "''") + "%') or (Party_master.Contactperson like '%" + txtsearch.Text.Replace("'", "''") + "%'))";
                    }
                    if (ddlcompname.SelectedIndex > 0)
                    {
                        mes2 += " and PartytTypeMaster.PartyTypeId='" + ddlcompname.SelectedValue + "'";
                    }

                    //string str2 = "	SELECT distinct Party_master.Compname,Party_master.Contactperson,PartytTypeMaster.PartType, Party_master.PartyID,AccountMaster.Status FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId= PartytTypeMaster.PartyTypeId where Party_master.Whid='" + ddlstore.SelectedValue + "' and PartytTypeMaster.PartyTypeId='" + ddlusertype.SelectedValue + "' and AccountMaster.Status='1' " + mes2 + " order by Party_master.Contactperson,Party_master.Compname";
                    string str2 = "  SELECT distinct Party_master.Compname,Party_master.Contactperson,PartytTypeMaster.PartType, Party_master.PartyID,AccountMaster.Status FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId= PartytTypeMaster.PartyTypeId inner join [PartyMasterCategory] on [PartyMasterCategory].[PartyMasterCategoryNo]=[PartytTypeMaster].[PartyCategoryId] where Party_master.Whid='" + ddlstore.SelectedValue + "' and PartytTypeMaster.PartyCategoryId='" + ddlusertype.SelectedValue + "' and AccountMaster.Status='1' " + mes2 + " order by Party_master.Contactperson,Party_master.Compname";
                    SqlDataAdapter dal = new SqlDataAdapter(str2, con);
                    dt = new DataTable();
                    dal.Fill(dt);
                }

                if (Convert.ToString(dtggg1.Rows[0]["Customer"]) == "True" && Convert.ToString(dtggg1.Rows[0]["Business"]) == "False")
                {

                    pnlusertypeother.Visible = true;

                    grdPartyList.Columns[0].Visible = true;
                    grdPartyList.Columns[1].Visible = true;
                    grdPartyList.Columns[2].Visible = true;
                    grdPartyList.Columns[2].HeaderText = "Company Name";
                    grdPartyList.Columns[3].Visible = false;
                    grdPartyList.Columns[4].Visible = false;
                    grdPartyList.Columns[5].Visible = false;
                    grdPartyList.Columns[6].Visible = false;

                    string mes2 = "";

                    if (txtsearch.Text != "")
                    {
                        mes2 = " and ((Party_master.Compname like '%" + txtsearch.Text.Replace("'", "''") + "%') or (Party_master.Contactperson like '%" + txtsearch.Text.Replace("'", "''") + "%'))";
                    }
                    if (ddlcompname.SelectedIndex > 0)
                    {
                        mes2 += " and PartytTypeMaster.PartyTypeId='" + ddlcompname.SelectedValue + "'";
                    }

                    //string str2 = "	SELECT distinct Party_master.Compname,Party_master.Contactperson,PartytTypeMaster.PartType, Party_master.PartyID,AccountMaster.Status FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId= PartytTypeMaster.PartyTypeId where Party_master.Whid='" + ddlstore.SelectedValue + "' and PartytTypeMaster.PartyTypeId='" + ddlusertype.SelectedValue + "' and AccountMaster.Status='1' " + mes2 + " order by Party_master.Contactperson,Party_master.Compname";
                    string str2 = "  SELECT distinct Party_master.Compname,Party_master.Contactperson,PartytTypeMaster.PartType, Party_master.PartyID,AccountMaster.Status FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId= PartytTypeMaster.PartyTypeId inner join [PartyMasterCategory] on [PartyMasterCategory].[PartyMasterCategoryNo]=[PartytTypeMaster].[PartyCategoryId] where Party_master.Whid='" + dtggg1.Rows[0]["BusinessID"].ToString() + "' and PartytTypeMaster.PartyCategoryId='" + ddlusertype.SelectedValue + "' and Party_master.Whid='" + ddlstore.SelectedValue + "' and AccountMaster.Status='1' " + mes2 + " order by Party_master.Contactperson,Party_master.Compname";
                    SqlDataAdapter dal = new SqlDataAdapter(str2, con);
                    dt = new DataTable();
                    dal.Fill(dt);
                }
            }
        }

        if (ddlusertype.SelectedItem.Text == "Others")
        {
            SqlDataAdapter daggg = new SqlDataAdapter("select EmployeeMaster.DesignationMasterId from EmployeeMaster where EmployeeMasterID='" + Session["EmployeeId"] + "'", con);
            DataTable dtggg = new DataTable();
            daggg.Fill(dtggg);


            SqlDataAdapter daggg1 = new SqlDataAdapter("select MessageCenterRightsTbl.Others,MessageCenterRightsTbl.BusinessID,MessageCenterRightsTbl.Business from MessageCenterRightsTbl where DesignationID='" + dtggg.Rows[0]["DesignationMasterId"].ToString() + "'", con);
            DataTable dtggg1 = new DataTable();
            daggg1.Fill(dtggg1);

            if (dtggg1.Rows.Count > 0)
            {
                if (Convert.ToString(dtggg1.Rows[0]["Others"]) == "True" && Convert.ToString(dtggg1.Rows[0]["Business"]) == "True")
                {

                    pnlusertypeother.Visible = true;

                    grdPartyList.Columns[0].Visible = true;
                    grdPartyList.Columns[1].Visible = true;
                    grdPartyList.Columns[2].Visible = true;
                    grdPartyList.Columns[2].HeaderText = "Company Name";
                    grdPartyList.Columns[3].Visible = false;
                    grdPartyList.Columns[4].Visible = false;
                    grdPartyList.Columns[5].Visible = false;
                    grdPartyList.Columns[6].Visible = false;


                    string mes2 = "";
                    if (txtsearch.Text != "")
                    {
                        mes2 = " and ((Party_master.Compname like '%" + txtsearch.Text.Replace("'", "''") + "%') or (Party_master.Contactperson like '%" + txtsearch.Text.Replace("'", "''") + "%'))";
                    }
                    if (ddlcompname.SelectedIndex > 0)
                    {
                        mes2 += " and PartytTypeMaster.PartyTypeId='" + ddlcompname.SelectedValue + "'";
                    }

                    string str2 = "	SELECT distinct Party_master.Compname,Party_master.Contactperson,PartytTypeMaster.PartType, Party_master.PartyID,AccountMaster.Status FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId= PartytTypeMaster.PartyTypeId inner join [PartyMasterCategory] on [PartyMasterCategory].[PartyMasterCategoryNo]=[PartytTypeMaster].[PartyCategoryId] where Party_master.Whid='" + ddlstore.SelectedValue + "' and PartytTypeMaster.PartyCategoryId='" + ddlusertype.SelectedValue + "' and AccountMaster.Status='1' " + mes2 + " order by Party_master.Contactperson,Party_master.Compname";
                    SqlDataAdapter dal = new SqlDataAdapter(str2, con);
                    dt = new DataTable();
                    dal.Fill(dt);
                }

                if (Convert.ToString(dtggg1.Rows[0]["Others"]) == "True" && Convert.ToString(dtggg1.Rows[0]["Business"]) == "False")
                {

                    pnlusertypeother.Visible = true;

                    grdPartyList.Columns[0].Visible = true;
                    grdPartyList.Columns[1].Visible = true;
                    grdPartyList.Columns[2].Visible = true;
                    grdPartyList.Columns[2].HeaderText = "Company Name";
                    grdPartyList.Columns[3].Visible = false;
                    grdPartyList.Columns[4].Visible = false;
                    grdPartyList.Columns[5].Visible = false;
                    grdPartyList.Columns[6].Visible = false;


                    string mes2 = "";
                    if (txtsearch.Text != "")
                    {
                        mes2 = " and ((Party_master.Compname like '%" + txtsearch.Text.Replace("'", "''") + "%') or (Party_master.Contactperson like '%" + txtsearch.Text.Replace("'", "''") + "%'))";
                    }
                    if (ddlcompname.SelectedIndex > 0)
                    {
                        mes2 += " and PartytTypeMaster.PartyTypeId='" + ddlcompname.SelectedValue + "'";
                    }

                    string str2 = "	SELECT distinct Party_master.Compname,Party_master.Contactperson,PartytTypeMaster.PartType, Party_master.PartyID,AccountMaster.Status FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId= PartytTypeMaster.PartyTypeId inner join [PartyMasterCategory] on [PartyMasterCategory].[PartyMasterCategoryNo]=[PartytTypeMaster].[PartyCategoryId] where Party_master.Whid='" + dtggg1.Rows[0]["BusinessID"].ToString() + "' and PartytTypeMaster.PartyCategoryId='" + ddlusertype.SelectedValue + "' and Party_master.Whid='" + ddlstore.SelectedValue + "' and AccountMaster.Status='1' " + mes2 + " order by Party_master.Contactperson,Party_master.Compname";
                    SqlDataAdapter dal = new SqlDataAdapter(str2, con);
                    dt = new DataTable();
                    dal.Fill(dt);
                }
            }
        }

        if (ddlusertype.SelectedItem.Text == "Vendor")
        {
            SqlDataAdapter daggg = new SqlDataAdapter("select EmployeeMaster.DesignationMasterId from EmployeeMaster where EmployeeMasterID='" + Session["EmployeeId"] + "'", con);
            DataTable dtggg = new DataTable();
            daggg.Fill(dtggg);


            SqlDataAdapter daggg1 = new SqlDataAdapter("select MessageCenterRightsTbl.Vendor,MessageCenterRightsTbl.BusinessID,MessageCenterRightsTbl.Business from MessageCenterRightsTbl where DesignationID='" + dtggg.Rows[0]["DesignationMasterId"].ToString() + "'", con);
            DataTable dtggg1 = new DataTable();
            daggg1.Fill(dtggg1);

            if (dtggg1.Rows.Count > 0)
            {
                if (Convert.ToString(dtggg1.Rows[0]["Vendor"]) == "True" && Convert.ToString(dtggg1.Rows[0]["Business"]) == "True")
                {

                    pnlusertypeother.Visible = true;

                    grdPartyList.Columns[0].Visible = true;
                    grdPartyList.Columns[1].Visible = true;
                    grdPartyList.Columns[2].Visible = true;
                    grdPartyList.Columns[2].HeaderText = "Company Name";
                    grdPartyList.Columns[3].Visible = false;
                    grdPartyList.Columns[4].Visible = false;
                    grdPartyList.Columns[5].Visible = false;
                    grdPartyList.Columns[6].Visible = false;


                    string mes2 = "";
                    if (txtsearch.Text != "")
                    {
                        mes2 = " and ((Party_master.Compname like '%" + txtsearch.Text.Replace("'", "''") + "%') or (Party_master.Contactperson like '%" + txtsearch.Text.Replace("'", "''") + "%'))";
                    }
                    if (ddlcompname.SelectedIndex > 0)
                    {
                        mes2 += " and PartytTypeMaster.PartyTypeId='" + ddlcompname.SelectedValue + "'";
                    }

                    string str2 = "	SELECT distinct Party_master.Compname,Party_master.Contactperson,PartytTypeMaster.PartType, Party_master.PartyID,AccountMaster.Status FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId= PartytTypeMaster.PartyTypeId inner join [PartyMasterCategory] on [PartyMasterCategory].[PartyMasterCategoryNo]=[PartytTypeMaster].[PartyCategoryId] where Party_master.Whid='" + ddlstore.SelectedValue + "' and PartytTypeMaster.PartyCategoryId='" + ddlusertype.SelectedValue + "' and AccountMaster.Status='1' " + mes2 + " order by Party_master.Contactperson,Party_master.Compname";
                    SqlDataAdapter dal = new SqlDataAdapter(str2, con);
                    dt = new DataTable();
                    dal.Fill(dt);
                }

                if (Convert.ToString(dtggg1.Rows[0]["Vendor"]) == "True" && Convert.ToString(dtggg1.Rows[0]["Business"]) == "False")
                {

                    pnlusertypeother.Visible = true;

                    grdPartyList.Columns[0].Visible = true;
                    grdPartyList.Columns[1].Visible = true;
                    grdPartyList.Columns[2].Visible = true;
                    grdPartyList.Columns[2].HeaderText = "Company Name";
                    grdPartyList.Columns[3].Visible = false;
                    grdPartyList.Columns[4].Visible = false;
                    grdPartyList.Columns[5].Visible = false;
                    grdPartyList.Columns[6].Visible = false;


                    string mes2 = "";
                    if (txtsearch.Text != "")
                    {
                        mes2 = " and ((Party_master.Compname like '%" + txtsearch.Text.Replace("'", "''") + "%') or (Party_master.Contactperson like '%" + txtsearch.Text.Replace("'", "''") + "%'))";
                    }
                    if (ddlcompname.SelectedIndex > 0)
                    {
                        mes2 += " and PartytTypeMaster.PartyTypeId='" + ddlcompname.SelectedValue + "'";
                    }

                    string str2 = "	SELECT distinct Party_master.Compname,Party_master.Contactperson,PartytTypeMaster.PartType, Party_master.PartyID,AccountMaster.Status FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId= PartytTypeMaster.PartyTypeId inner join [PartyMasterCategory] on [PartyMasterCategory].[PartyMasterCategoryNo]=[PartytTypeMaster].[PartyCategoryId] where Party_master.Whid='" + dtggg1.Rows[0]["BusinessID"].ToString() + "' and PartytTypeMaster.PartyCategoryId='" + ddlusertype.SelectedValue + "' and Party_master.Whid='" + ddlstore.SelectedValue + "' and AccountMaster.Status='1' " + mes2 + " order by Party_master.Contactperson,Party_master.Compname";
                    SqlDataAdapter dal = new SqlDataAdapter(str2, con);
                    dt = new DataTable();
                    dal.Fill(dt);
                }
            }
        }

        if (ddlusertype.SelectedItem.Text == "Admin")
        {
            SqlDataAdapter daggg = new SqlDataAdapter("select EmployeeMaster.DesignationMasterId from EmployeeMaster where EmployeeMasterID='" + Session["EmployeeId"] + "'", con);
            DataTable dtggg = new DataTable();
            daggg.Fill(dtggg);


            SqlDataAdapter daggg1 = new SqlDataAdapter("select MessageCenterRightsTbl.AdminRights,MessageCenterRightsTbl.BusinessID,MessageCenterRightsTbl.Business from MessageCenterRightsTbl where DesignationID='" + dtggg.Rows[0]["DesignationMasterId"].ToString() + "'", con);
            DataTable dtggg1 = new DataTable();
            daggg1.Fill(dtggg1);

            if (dtggg1.Rows.Count > 0)
            {
                if (Convert.ToString(dtggg1.Rows[0]["AdminRights"]) == "True" && Convert.ToString(dtggg1.Rows[0]["Business"]) == "True")
                {
                    grdPartyList.Columns[0].Visible = true;
                    grdPartyList.Columns[1].Visible = true;
                    grdPartyList.Columns[2].Visible = true;
                    grdPartyList.Columns[2].HeaderText = "Admin";
                    grdPartyList.Columns[3].Visible = false;
                    grdPartyList.Columns[4].Visible = false;
                    grdPartyList.Columns[5].Visible = false;
                    grdPartyList.Columns[6].Visible = false;

                    string mes2 = "";
                    if (txtsearch.Text != "")
                    {
                        mes2 = " and ((Party_master.Compname like '%" + txtsearch.Text.Replace("'", "''") + "%') or (Party_master.Contactperson like '%" + txtsearch.Text.Replace("'", "''") + "%'))";
                    }

                    string str2 = "	SELECT distinct  Party_master.Compname,Party_master.Contactperson,PartytTypeMaster.PartType, Party_master.PartyID,AccountMaster.Status,employeemaster.DesignationMasterId FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId= PartytTypeMaster.PartyTypeId inner join [PartyMasterCategory] on [PartyMasterCategory].[PartyMasterCategoryNo]=[PartytTypeMaster].[PartyCategoryId] inner join employeemaster on employeemaster.PartyID=Party_master.PartyID where Party_master.Whid='" + ddlstore.SelectedValue + "' and PartytTypeMaster.PartyCategoryId='" + ddlusertype.SelectedValue + "' and AccountMaster.Status='1' " + mes2 + " order by Party_master.Contactperson,Party_master.Compname";
                    SqlDataAdapter dal = new SqlDataAdapter(str2, con);
                    dt = new DataTable();
                    dal.Fill(dt);
                }

                if (Convert.ToString(dtggg1.Rows[0]["AdminRights"]) == "True" && Convert.ToString(dtggg1.Rows[0]["Business"]) == "False")
                {
                    grdPartyList.Columns[0].Visible = true;
                    grdPartyList.Columns[1].Visible = true;
                    grdPartyList.Columns[2].Visible = true;
                    grdPartyList.Columns[2].HeaderText = "Admin";
                    grdPartyList.Columns[3].Visible = false;
                    grdPartyList.Columns[4].Visible = false;
                    grdPartyList.Columns[5].Visible = false;
                    grdPartyList.Columns[6].Visible = false;

                    string mes2 = "";
                    if (txtsearch.Text != "")
                    {
                        mes2 = " and ((Party_master.Compname like '%" + txtsearch.Text.Replace("'", "''") + "%') or (Party_master.Contactperson like '%" + txtsearch.Text.Replace("'", "''") + "%'))";
                    }

                    string str2 = "	SELECT distinct  Party_master.Compname,Party_master.Contactperson,PartytTypeMaster.PartType, Party_master.PartyID,AccountMaster.Status,employeemaster.DesignationMasterId FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId= PartytTypeMaster.PartyTypeId inner join [PartyMasterCategory] on [PartyMasterCategory].[PartyMasterCategoryNo]=[PartytTypeMaster].[PartyCategoryId] inner join employeemaster on employeemaster.PartyID=Party_master.PartyID where Party_master.Whid='" + dtggg1.Rows[0]["BusinessID"].ToString() + "' and PartytTypeMaster.PartyCategoryId='" + ddlusertype.SelectedValue + "' and Party_master.Whid='" + ddlstore.SelectedValue + "' and AccountMaster.Status='1' " + mes2 + " order by Party_master.Contactperson,Party_master.Compname";
                    SqlDataAdapter dal = new SqlDataAdapter(str2, con);
                    dt = new DataTable();
                    dal.Fill(dt);
                }
            }
        }


        if (ddlusertype.SelectedItem.Text == "Candidate")
        {
            SqlDataAdapter daggg = new SqlDataAdapter("select EmployeeMaster.DesignationMasterId from EmployeeMaster where EmployeeMasterID='" + Session["EmployeeId"] + "'", con);
            DataTable dtggg = new DataTable();
            daggg.Fill(dtggg);


            SqlDataAdapter daggg1 = new SqlDataAdapter("select MessageCenterRightsTbl.Candidate,MessageCenterRightsTbl.BusinessID,MessageCenterRightsTbl.Business from MessageCenterRightsTbl where DesignationID='" + dtggg.Rows[0]["DesignationMasterId"].ToString() + "'", con);
            DataTable dtggg1 = new DataTable();
            daggg1.Fill(dtggg1);

            if (dtggg1.Rows.Count > 0)
            {
                if (Convert.ToString(dtggg1.Rows[0]["Candidate"]) == "True" && Convert.ToString(dtggg1.Rows[0]["Business"]) == "True")
                {
                    pnlusertypecandidate.Visible = true;

                    grdPartyList.Columns[0].Visible = true;
                    grdPartyList.Columns[1].Visible = false;
                    grdPartyList.Columns[2].Visible = false;
                    grdPartyList.Columns[3].Visible = false;
                    grdPartyList.Columns[4].Visible = false;
                    grdPartyList.Columns[5].Visible = true;
                    grdPartyList.Columns[6].Visible = true;

                    string mes2 = "";
                    if (txtsearch.Text != "")
                    {
                        mes2 = " and ((CandidateMaster.lastname +''+ CandidateMaster.firstname +''+ CandidateMaster.middlename like '%" + txtsearch.Text.Replace("'", "''") + "%') or (VacancyPositionTitleMaster.VacancyPositionTitle like '%" + txtsearch.Text.Replace("'", "''") + "%'))";
                    }
                    if (ddlcandi.SelectedIndex > 0)
                    {
                        mes2 += " and VacancyPositionTitleMaster.id='" + ddlcandi.SelectedValue + "'";
                    }

                    string str2 = "select CandidateMaster.lastname +''+ CandidateMaster.firstname +''+ CandidateMaster.middlename as CName,Party_master.Compname,Party_master.Contactperson,Party_master.PartyID,CandidateMaster.Jobpositionid,VacancyPositionTitleMaster.VacancyPositionTitle from CandidateMaster inner join VacancyPositionTitleMaster on VacancyPositionTitleMaster.id=CandidateMaster.Jobpositionid inner join Party_master on Party_master.PartyID=CandidateMaster.PartyID where Party_master.whid='" + ddlstore.SelectedValue + "' " + mes2 + " order by CName";

                    //string str2 = "select CandidateMaster.lastname +''+ CandidateMaster.firstname +''+ CandidateMaster.middlename as CName,Party_master.Compname,Party_master.Contactperson,Party_master.PartyID,CandidateMaster.Jobpositionid,VacancyPositionTitleMaster.VacancyPositionTitle from CandidateMaster inner join VacancyPositionTitleMaster on VacancyPositionTitleMaster.id=CandidateMaster.Jobpositionid inner join Party_master on Party_master.PartyID=CandidateMaster.PartyID inner join MessageCenterRightsTbl on CandidateMaster.DesignationMasterId=MessageCenterRightsTbl.DesignationID where Party_master.whid='" + ddlstore.SelectedValue + "' and [MessageCenterRightsTbl].Candidate=1 " + mes2 + " order by CName";

                    SqlDataAdapter dal = new SqlDataAdapter(str2, con);
                    dt = new DataTable();
                    dal.Fill(dt);
                }

                if (Convert.ToString(dtggg1.Rows[0]["Candidate"]) == "True" && Convert.ToString(dtggg1.Rows[0]["Business"]) == "False")
                {
                    pnlusertypecandidate.Visible = true;

                    grdPartyList.Columns[0].Visible = true;
                    grdPartyList.Columns[1].Visible = false;
                    grdPartyList.Columns[2].Visible = false;
                    grdPartyList.Columns[3].Visible = false;
                    grdPartyList.Columns[4].Visible = false;
                    grdPartyList.Columns[5].Visible = true;
                    grdPartyList.Columns[6].Visible = true;

                    string mes2 = "";
                    if (txtsearch.Text != "")
                    {
                        mes2 = " and ((CandidateMaster.lastname +''+ CandidateMaster.firstname +''+ CandidateMaster.middlename like '%" + txtsearch.Text.Replace("'", "''") + "%') or (VacancyPositionTitleMaster.VacancyPositionTitle like '%" + txtsearch.Text.Replace("'", "''") + "%'))";
                    }
                    if (ddlcandi.SelectedIndex > 0)
                    {
                        mes2 += " and VacancyPositionTitleMaster.id='" + ddlcandi.SelectedValue + "'";
                    }

                    string str2 = "select CandidateMaster.lastname +''+ CandidateMaster.firstname +''+ CandidateMaster.middlename as CName,Party_master.Compname,Party_master.Contactperson,Party_master.PartyID,CandidateMaster.Jobpositionid,VacancyPositionTitleMaster.VacancyPositionTitle from CandidateMaster inner join VacancyPositionTitleMaster on VacancyPositionTitleMaster.id=CandidateMaster.Jobpositionid inner join Party_master on Party_master.PartyID=CandidateMaster.PartyID where Party_master.whid='" + dtggg1.Rows[0]["BusinessID"].ToString() + "' and Party_master.Whid='" + ddlstore.SelectedValue + "' " + mes2 + " order by CName";

                    //string str2 = "select CandidateMaster.lastname +''+ CandidateMaster.firstname +''+ CandidateMaster.middlename as CName,Party_master.Compname,Party_master.Contactperson,Party_master.PartyID,CandidateMaster.Jobpositionid,VacancyPositionTitleMaster.VacancyPositionTitle from CandidateMaster inner join VacancyPositionTitleMaster on VacancyPositionTitleMaster.id=CandidateMaster.Jobpositionid inner join Party_master on Party_master.PartyID=CandidateMaster.PartyID inner join MessageCenterRightsTbl on CandidateMaster.DesignationMasterId=MessageCenterRightsTbl.DesignationID where Party_master.whid='" + ddlstore.SelectedValue + "' and [MessageCenterRightsTbl].Candidate=1 " + mes2 + " order by CName";

                    SqlDataAdapter dal = new SqlDataAdapter(str2, con);
                    dt = new DataTable();
                    dal.Fill(dt);
                }
            }
        }

        if (ddlusertype.SelectedItem.Text == "Visitor")
        {
            SqlDataAdapter daggg = new SqlDataAdapter("select EmployeeMaster.DesignationMasterId from EmployeeMaster where EmployeeMasterID='" + Session["EmployeeId"] + "'", con);
            DataTable dtggg = new DataTable();
            daggg.Fill(dtggg);


            SqlDataAdapter daggg1 = new SqlDataAdapter("select MessageCenterRightsTbl.Visitor,MessageCenterRightsTbl.BusinessID,MessageCenterRightsTbl.Business from MessageCenterRightsTbl where DesignationID='" + dtggg.Rows[0]["DesignationMasterId"].ToString() + "'", con);
            DataTable dtggg1 = new DataTable();
            daggg1.Fill(dtggg1);

            if (dtggg1.Rows.Count > 0)
            {
                if (Convert.ToString(dtggg1.Rows[0]["Visitor"]) == "True" && Convert.ToString(dtggg1.Rows[0]["Business"]) == "True")
                {
                    pnlusertypeother.Visible = true;

                    grdPartyList.Columns[0].Visible = true;
                    grdPartyList.Columns[1].Visible = true;
                    grdPartyList.Columns[2].Visible = true;
                    grdPartyList.Columns[2].HeaderText = "Company Name";
                    grdPartyList.Columns[3].Visible = false;
                    grdPartyList.Columns[4].Visible = false;
                    grdPartyList.Columns[5].Visible = false;
                    grdPartyList.Columns[6].Visible = false;


                    string mes2 = "";
                    if (txtsearch.Text != "")
                    {
                        mes2 = " and ((Party_master.Compname like '%" + txtsearch.Text.Replace("'", "''") + "%') or (Party_master.Contactperson like '%" + txtsearch.Text.Replace("'", "''") + "%'))";
                    }
                    if (ddlcompname.SelectedIndex > 0)
                    {
                        mes2 += " and PartytTypeMaster.PartyTypeId='" + ddlcompname.SelectedValue + "'";
                    }

                    string str2 = "	SELECT distinct Party_master.Compname,Party_master.Contactperson,PartytTypeMaster.PartType, Party_master.PartyID,AccountMaster.Status FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId= PartytTypeMaster.PartyTypeId inner join [PartyMasterCategory] on [PartyMasterCategory].[PartyMasterCategoryNo]=[PartytTypeMaster].[PartyCategoryId] where Party_master.Whid='" + ddlstore.SelectedValue + "' and PartytTypeMaster.PartyCategoryId='" + ddlusertype.SelectedValue + "' and AccountMaster.Status='1' " + mes2 + " order by Party_master.Contactperson,Party_master.Compname";
                    SqlDataAdapter dal = new SqlDataAdapter(str2, con);
                    dt = new DataTable();
                    dal.Fill(dt);
                }

                if (Convert.ToString(dtggg1.Rows[0]["Visitor"]) == "True" && Convert.ToString(dtggg1.Rows[0]["Business"]) == "False")
                {
                    pnlusertypeother.Visible = true;

                    grdPartyList.Columns[0].Visible = true;
                    grdPartyList.Columns[1].Visible = true;
                    grdPartyList.Columns[2].Visible = true;
                    grdPartyList.Columns[2].HeaderText = "Company Name";
                    grdPartyList.Columns[3].Visible = false;
                    grdPartyList.Columns[4].Visible = false;
                    grdPartyList.Columns[5].Visible = false;
                    grdPartyList.Columns[6].Visible = false;


                    string mes2 = "";
                    if (txtsearch.Text != "")
                    {
                        mes2 = " and ((Party_master.Compname like '%" + txtsearch.Text.Replace("'", "''") + "%') or (Party_master.Contactperson like '%" + txtsearch.Text.Replace("'", "''") + "%'))";
                    }
                    if (ddlcompname.SelectedIndex > 0)
                    {
                        mes2 += " and PartytTypeMaster.PartyTypeId='" + ddlcompname.SelectedValue + "'";
                    }

                    string str2 = "	SELECT distinct Party_master.Compname,Party_master.Contactperson,PartytTypeMaster.PartType, Party_master.PartyID,AccountMaster.Status FROM AccountMaster inner join Party_master on Party_master.Account=AccountMaster.AccountId inner join PartytTypeMaster ON Party_master.PartyTypeId= PartytTypeMaster.PartyTypeId inner join [PartyMasterCategory] on [PartyMasterCategory].[PartyMasterCategoryNo]=[PartytTypeMaster].[PartyCategoryId] where Party_master.Whid='" + dtggg1.Rows[0]["BusinessID"].ToString() + "' and PartytTypeMaster.PartyCategoryId='" + ddlusertype.SelectedValue + "' and Party_master.Whid='" + ddlstore.SelectedValue + "' and AccountMaster.Status='1' " + mes2 + " order by Party_master.Contactperson,Party_master.Compname";
                    SqlDataAdapter dal = new SqlDataAdapter(str2, con);
                    dt = new DataTable();
                    dal.Fill(dt);
                }
            }
        }

        grdPartyList.DataSource = dt;
        grdPartyList.DataBind();

        if (Request.QueryString["pid"] != null)
        {
            foreach (GridViewRow GR in grdPartyList.Rows)
            {
                CheckBox chk = (CheckBox)GR.FindControl("chkParty");


                Int32 ToPartyId = Convert.ToInt32(grdPartyList.DataKeys[GR.RowIndex].Value);
                if (ToPartyId == Convert.ToInt32(Request.QueryString["pid"]))
                {
                    chk.Checked = true;
                }

            }
            EventArgs e = new EventArgs();
            object sender = new object();
            Button1_Click(sender, e);
            for (int i = 0; i < grdPartyList.PageCount; i++)
            {
                grdPartyList.PageIndex = i;
                GridViewPageEventArgs e1 = new GridViewPageEventArgs(grdPartyList.PageIndex);
                object sender1 = new object();
                grdPartyList_PageIndexChanging(sender1, e1);
            }


        }

        setGridisze();
    }

    protected void imgbtnattach_Click(object sender, EventArgs e)
    {
        gridFileAttach.DataSource = null;
        gridFileAttach.DataBind();

        String filename = "";
        PnlFileAttachLbl.Visible = true;
        if (fileuploadadattachment.HasFile)
        {
            filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + fileuploadadattachment.FileName;
            fileuploadadattachment.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["Comid"] + "\\UploadedDocuments\\") + filename);
            fileuploadadattachment.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["Comid"] + "\\UploadedDocumentsTemp\\") + filename);
            hdnFileName.Value = filename;
            DataTable dt = new DataTable();
            if (Session["GridFileAttach1"] == null)
            {
                DataColumn dtcom2 = new DataColumn();
                dtcom2.DataType = System.Type.GetType("System.String");
                dtcom2.ColumnName = "FileName";
                dtcom2.ReadOnly = false;
                dtcom2.Unique = false;
                dtcom2.AllowDBNull = true;
                dt.Columns.Add(dtcom2);
                DataColumn dtcom3 = new DataColumn();
                dtcom3.DataType = System.Type.GetType("System.String");
                dtcom3.ColumnName = "FileNameChanged";
                dtcom3.ReadOnly = false;
                dtcom3.Unique = false;
                dtcom3.AllowDBNull = true;
                dt.Columns.Add(dtcom3);
            }
            else
            {
                dt = (DataTable)Session["GridFileAttach1"];
            }
            DataRow dtrow = dt.NewRow();
            dtrow["FileName"] = fileuploadadattachment.FileName.ToString();
            dtrow["FileNameChanged"] = hdnFileName.Value;
            dt.Rows.Add(dtrow);
            Session["GridFileAttach1"] = dt;
            gridFileAttach.DataSource = dt;
            gridFileAttach.DataBind();
        }
        else
        {
            pnlmsg.Visible = true;
            lblmsg.Text = "Please Attach File to Upload.";
            return;
        }
        setGridisze();
    }

    public void AddAttachment(int j)
    {
        DataTable dt = new DataTable();
        dt = clsMessage.SelectDocumentMasterByID(j);
        DataListAttach.DataSource = dt;
        DataListAttach.DataBind();
        setGridisze();
    }
    protected void gridFileAttach_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        gridFileAttach.SelectedIndex = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "Remove")
        {
            DataTable dt = new DataTable();
            if (Session["GridFileAttach1"] != null)
            {
                dt = (DataTable)Session["GridFileAttach1"];
                dt.Rows.Remove(dt.Rows[gridFileAttach.SelectedIndex]);
                gridFileAttach.DataSource = dt;
                gridFileAttach.DataBind();
                Session["GridFileAttach1"] = dt;
                if (dt.Rows.Count == 0)
                {
                    PnlFileAttachLbl.Visible = false;
                }
                setGridisze();
            }
        }
    }
    protected void imgbtnsend_Click(object sender, EventArgs e)
    {
        bool Gcheck = false;
        if (grdPartyList.Rows.Count > 0)
        {
            foreach (GridViewRow GR in grdPartyList.Rows)
            {
                CheckBox chk = (CheckBox)GR.FindControl("chkParty");
                if (chk.Checked == true)
                {
                    Gcheck = true;
                    break;
                }
            }
            if (Gcheck == false)
            {
                lblmsg.Text = "Please select atleast ONE Party to send Message . ";
                pnlmsg.Visible = true;
                return;
            }
            else
            {
                /////// START
                try
                {
                    Int32 FromPartyId;
                    FromPartyId = Convert.ToInt32(Session["PartyId"].ToString());
                    if (Convert.ToString(Request.QueryString["MsgId"]) != null) // For Draft Message
                    {
                        Int32 MsgId = Convert.ToInt32(Request.QueryString["MsgId"]);
                        bool MsgUpdate = clsMessage.UpdateMsgMaster(MsgId, txtsub.Text, TxtMsgDetail.Text);
                        if (MsgUpdate == true)
                        {
                            bool MsgDetailDelete = clsMessage.DeleteMsgDetail(MsgId);
                            if (MsgDetailDelete == true)
                            {
                                foreach (GridViewRow GR in grdPartyList.Rows)
                                {
                                    CheckBox chk = (CheckBox)GR.FindControl("chkParty");
                                    if (chk.Checked == true)
                                    {
                                        Int32 ToPartyId = Convert.ToInt32(grdPartyList.DataKeys[GR.RowIndex].Value);
                                        Int32 MessageDetailId = clsMessage.InsertMsgDetail(MsgId, ToPartyId);
                                    }
                                }
                                if (gridFileAttach.Rows.Count > 0)
                                {
                                    bool FileDelete = clsMessage.DeleteMsgFileAttachDetail(MsgId);
                                    if (FileDelete == true)
                                    {
                                        foreach (GridViewRow GR in gridFileAttach.Rows)
                                        {
                                            string FileName = (gridFileAttach.DataKeys[GR.RowIndex].Value.ToString());
                                            bool ins = clsMessage.InsertMsgFileAttachDetail(MsgId, FileName);
                                        }
                                    }
                                }
                            }
                            pnlmsg.Visible = true;
                            lblmsg.Text = "Message Sent Successfully.";
                            ClearAll();
                        }
                    }
                    else
                    {


                        Int32 MsgId = clsMessage.InsertMsgMaster(FromPartyId, txtsub.Text, TxtMsgDetail.Text + "<Br>" + Label10.Text + ' ' + Label11.Text + ' ' + Label12.Text + ' ' + Label13.Text, Label10.Text + ' ' + Label11.Text + ' ' + Label12.Text + ' ' + Label13.Text, chkpicture.Checked, str);

                        //Int32 MsgId = 0;

                        //string ins1 = "insert into MsgMaster (FromPartyId,MsgDate,MsgSubject,MsgDetail,Picture,Signature) values('" + FromPartyId + "','" + System.DateTime.Now.ToString() + "','" + txtsub.Text + "','" + TxtMsgDetail.Text + ' ' + Label10.Text + ' ' + Label11.Text + ' ' + Label12.Text + ' ' + Label13.Text + "','" + chkpicture.Checked + "','" + Label10.Text + ' ' + Label11.Text + ' ' + Label12.Text + ' ' + Label13.Text + "')";
                        //SqlCommand cmd = new SqlCommand(ins1, con);
                        //if (con.State.ToString() != "Open")
                        //{
                        //    con.Open();
                        //}
                        //cmd.ExecuteNonQuery();
                        //con.Close();

                        //SqlDataAdapter da = new SqlDataAdapter("select max(MsgId) as MsgId from MsgMaster", con);
                        //DataTable dt = new DataTable();
                        //da.Fill(dt);

                        //if (dt.Rows.Count > 0)
                        //{
                        //    MsgId = Convert.ToInt32(dt.Rows[0]["MsgId"].ToString());
                        //}

                        if (MsgId > 0)
                        {
                            foreach (GridViewRow GR in grdPartyList.Rows)
                            {
                                CheckBox chk = (CheckBox)GR.FindControl("chkParty");
                                if (chk.Checked == true)
                                {
                                    Int32 ToPartyId = Convert.ToInt32(grdPartyList.DataKeys[GR.RowIndex].Value);
                                    Int32 MessageDetailId = clsMessage.InsertMsgDetail(MsgId, ToPartyId);
                                }
                            }
                            if (gridFileAttach.Rows.Count > 0)
                            {
                                foreach (GridViewRow GR in gridFileAttach.Rows)
                                {
                                    string FileName = (gridFileAttach.DataKeys[GR.RowIndex].Value.ToString());
                                    bool ins = clsMessage.InsertMsgFileAttachDetail(MsgId, FileName);
                                }
                            }
                            pnlmsg.Visible = true;
                            lblmsg.Text = "Message Sent Successfully";
                            ClearAll();
                        }
                    }
                }
                catch (Exception es)
                {
                    Response.Write(es.Message.ToString());
                }
                //////// END

            }
        }
        else
        {
            lblmsg.Text = "No single party is available to send Message.";
            pnlmsg.Visible = true;
            return;
        }
        PnlFileAttachLbl.Visible = false;
    }
    protected void ClearAll()
    {
        foreach (GridViewRow GR in grdPartyList.Rows)
        {
            CheckBox chk = (CheckBox)GR.FindControl("chkParty");
            chk.Checked = false;
        }
        CheckBox chkhead = (CheckBox)grdPartyList.HeaderRow.Cells[1].FindControl("HeaderChkbox");
        chkhead.Checked = false;
        Session["GridFileAttach1"] = null;
        gridFileAttach.DataSource = Session["GridFileAttach1"];
        gridFileAttach.DataBind();
        TxtMsgDetail.Text = "";
        txtsub.Text = "";
        Session["to"] = null;
        lblAddresses.Text = "";
        pblattach.Visible = false;
        chkattach.Checked = false;
        PnlFileAttachLbl.Visible = false;
    }


    protected void imgbtnsaveasdraft_Click(object sender, EventArgs e)
    {
        bool Gcheck = false;
        if (grdPartyList.Rows.Count > 0)
        {
            foreach (GridViewRow GR in grdPartyList.Rows)
            {
                CheckBox chk = (CheckBox)GR.FindControl("chkParty");
                if (chk.Checked == true)
                {
                    Gcheck = true;
                    break;
                }
            }
            if (Gcheck == false)
            {
                lblmsg.Text = "Please select atleast ONE Party to send Message . ";
                pnlmsg.Visible = true;
                return;
            }
            else
            {
                /////// START
                try
                {
                    Int32 FromPartyId;
                    FromPartyId = Convert.ToInt32(Session["PartyId"].ToString());
                    if (Convert.ToString(Request.QueryString["MsgId"]) != null) // For Draft Message
                    {
                        Int32 MsgId = Convert.ToInt32(Request.QueryString["MsgId"]);
                        bool MsgUpdate = clsMessage.UpdateMsgMaster(MsgId, txtsub.Text, TxtMsgDetail.Text);
                        if (MsgUpdate == true)
                        {
                            bool MsgDetailDelete = clsMessage.DeleteMsgDetail(MsgId);
                            if (MsgDetailDelete == true)
                            {
                                foreach (GridViewRow GR in grdPartyList.Rows)
                                {
                                    CheckBox chk = (CheckBox)GR.FindControl("chkParty");
                                    if (chk.Checked == true)
                                    {
                                        Int32 ToPartyId = Convert.ToInt32(grdPartyList.DataKeys[GR.RowIndex].Value);
                                        Int32 MessageDetailId = clsMessage.InsertMsgDetail(MsgId, ToPartyId);
                                        bool draft = clsMessage.UpdateMsgDetail(MessageDetailId, 3);
                                    }
                                }
                                if (gridFileAttach.Rows.Count > 0)
                                {
                                    bool FileDelete = clsMessage.DeleteMsgFileAttachDetail(MsgId);
                                    if (FileDelete == true)
                                    {
                                        foreach (GridViewRow GR in gridFileAttach.Rows)
                                        {
                                            string FileName = (gridFileAttach.DataKeys[GR.RowIndex].Value.ToString());
                                            bool ins = clsMessage.InsertMsgFileAttachDetail(MsgId, FileName);
                                        }
                                    }
                                }
                            }
                            pnlmsg.Visible = true;
                            lblmsg.Text = "Message is Successfully saved as Drafts.";
                            ClearAll();
                        }

                    }
                    else
                    {
                        //Int32 MsgId = clsMessage.InsertMsgMaster(FromPartyId, txtsub.Text, TxtMsgDetail.Text);
                        Int32 MsgId = clsMessage.InsertMsgMaster(FromPartyId, txtsub.Text, TxtMsgDetail.Text + "<Br>" + Label10.Text + ' ' + Label11.Text + ' ' + Label12.Text + ' ' + Label13.Text, Label10.Text + ' ' + Label11.Text + ' ' + Label12.Text + ' ' + Label13.Text, chkpicture.Checked, str);
                        if (MsgId > 0)
                        {

                            foreach (GridViewRow GR in grdPartyList.Rows)
                            {
                                CheckBox chk = (CheckBox)GR.FindControl("chkParty");
                                if (chk.Checked == true)
                                {
                                    Int32 ToPartyId = Convert.ToInt32(grdPartyList.DataKeys[GR.RowIndex].Value);
                                    Int32 MsgDetailId = clsMessage.InsertMsgDetail(MsgId, ToPartyId);
                                    bool draft = clsMessage.UpdateMsgDetail(MsgDetailId, 3);

                                }
                            }

                            if (gridFileAttach.Rows.Count > 0)
                            {
                                foreach (GridViewRow GR in gridFileAttach.Rows)
                                {
                                    string FileName = (gridFileAttach.DataKeys[GR.RowIndex].Value.ToString());
                                    bool ins = clsMessage.InsertMsgFileAttachDetail(MsgId, FileName);
                                }
                            }
                            pnlmsg.Visible = true;
                            lblmsg.Text = "Message is Successfully saved as Drafts.";
                            ClearAll();
                        }
                    }
                }
                catch (Exception es)
                {
                    Response.Write(es.Message.ToString());
                }
                //////// END
            }
        }
        else
        {
            lblmsg.Text = "No single party is available to send Message.";
            pnlmsg.Visible = true;
            return;
        }
    }
    protected void imgbtndiscard_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["MsgId"] != null)
        {
            Int32 MsgId = Convert.ToInt32(Request.QueryString["MsgId"]);
            bool MsgDetailDelete = clsMessage.DeleteMsgDetail(MsgId);
            bool FileDelete = clsMessage.DeleteMsgFileAttachDetail(MsgId);
            bool MsgMasterDelete = clsMessage.DeleteMsgMaster(MsgId);
            ClearAll();
        }
        else
        {
            ClearAll();
        }
    }
    public void setGridisze()
    {

        //ATTTCHMENT

        //PnlFileAttachLbl
        //gridFileAttach
        if (gridFileAttach.Rows.Count == 0)
        {
            PnlFileAttachLbl.CssClass = "GridPanel20";
        }
        else if (gridFileAttach.Rows.Count == 1)
        {
            PnlFileAttachLbl.CssClass = "GridPanel125";
        }
        else if (gridFileAttach.Rows.Count == 2)
        {
            PnlFileAttachLbl.CssClass = "GridPanel150";
        }
        else if (gridFileAttach.Rows.Count == 3)
        {
            PnlFileAttachLbl.CssClass = "GridPanel175";
        }
        else if (gridFileAttach.Rows.Count == 4)
        {
            PnlFileAttachLbl.CssClass = "GridPanel200";
        }
        else if (gridFileAttach.Rows.Count == 5)
        {
            PnlFileAttachLbl.CssClass = "GridPanel225";
        }
        //else if (gridFileAttach.Rows.Count == 6)
        //{
        //    PnlFileAttachLbl.CssClass = "GridPanel250";
        //}
        //else if (gridFileAttach.Rows.Count == 7)
        //{
        //    PnlFileAttachLbl.CssClass = "GridPanel275";
        //}
        //else if (gridFileAttach.Rows.Count == 8)
        //{
        //    PnlFileAttachLbl.CssClass = "GridPanel";
        //}
        //else if (gridFileAttach.Rows.Count == 9)
        //{
        //    PnlFileAttachLbl.CssClass = "GridPanel325";
        //}
        //else if (gridFileAttach.Rows.Count == 10)
        //{
        //    PnlFileAttachLbl.CssClass = "GridPanel350";
        //}

        else
        {
            PnlFileAttachLbl.CssClass = "GridPanel250";
        }

    }
    //protected void grdPartyList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    grdPartyList1.PageIndex = e.NewPageIndex;
    //    //SelectMsgforDraft();
    //    FillPartyGrid();
    //}
    protected void gridFileAttach_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridFileAttach.PageIndex = e.NewPageIndex;
        //SelectMsgforDraft();
        FillPartyGrid();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        DataColumn dtcom = new DataColumn();
        dtcom.DataType = System.Type.GetType("System.Int32");
        dtcom.ColumnName = "PartyId";
        dtcom.ReadOnly = false;
        dtcom.Unique = false;
        dtcom.AllowDBNull = true;
        dt.Columns.Add(dtcom);

        DataColumn dtcom1 = new DataColumn();
        dtcom1.DataType = System.Type.GetType("System.String");
        dtcom1.ColumnName = "Name";
        dtcom1.ReadOnly = false;
        dtcom1.Unique = false;
        dtcom1.AllowDBNull = true;
        dt.Columns.Add(dtcom1);

        DataColumn dtcom2 = new DataColumn();
        dtcom2.DataType = System.Type.GetType("System.String");
        dtcom2.ColumnName = "CName";
        dtcom2.ReadOnly = false;
        dtcom2.Unique = false;
        dtcom2.AllowDBNull = true;
        dt.Columns.Add(dtcom2);

        DataColumn dtcom3 = new DataColumn();
        dtcom3.DataType = System.Type.GetType("System.String");
        dtcom3.ColumnName = "Contactperson";
        dtcom3.ReadOnly = false;
        dtcom3.Unique = false;
        dtcom3.AllowDBNull = true;
        dt.Columns.Add(dtcom3);

        //DataColumn dtcom1 = new DataColumn();
        //dtcom1.DataType = System.Type.GetType("System.String");
        //dtcom1.ColumnName = "Name";
        //dtcom1.ReadOnly = false;
        //dtcom1.Unique = false;
        //dtcom1.AllowDBNull = true;
        //dt.Columns.Add(dtcom1);

        //DataColumn dtcom1 = new DataColumn();
        //dtcom1.DataType = System.Type.GetType("System.String");
        //dtcom1.ColumnName = "Name";
        //dtcom1.ReadOnly = false;
        //dtcom1.Unique = false;
        //dtcom1.AllowDBNull = true;
        //dt.Columns.Add(dtcom1);

        string address = "";

        foreach (GridViewRow GR in grdPartyList.Rows)
        {
            CheckBox chk = (CheckBox)GR.FindControl("chkParty");

            if (chk.Checked == true)
            {
                Int32 ToPartyId = Convert.ToInt32(grdPartyList.DataKeys[GR.RowIndex].Value);
                DataRow drow = dt.NewRow();
                drow["PartyId"] = ToPartyId.ToString();

                // drow["Contactperson"] = GR.Cells[1].Text;

                if (ddlusertype.SelectedItem.Text == "Candidate")
                {
                    drow["CName"] = GR.Cells[5].Text;
                    dt.Rows.Add(drow);
                    if (Convert.ToString(address) == "")
                    {
                        address = GR.Cells[5].Text;
                    }
                    else
                    {
                        address = address + "; " + GR.Cells[5].Text;
                    }

                }
                if (ddlusertype.SelectedItem.Text == "Employee")
                {
                    drow["Name"] = GR.Cells[3].Text;
                    dt.Rows.Add(drow);
                    if (Convert.ToString(address) == "")
                    {
                        address = GR.Cells[3].Text;
                    }
                    else
                    {
                        address = address + "; " + GR.Cells[3].Text;
                    }
                }
                if (ddlusertype.SelectedItem.Text == "Customer" || ddlusertype.SelectedItem.Text == "Other" || ddlusertype.SelectedItem.Text == "Vendor" || ddlusertype.SelectedItem.Text == "Admin")
                {
                    drow["Contactperson"] = GR.Cells[1].Text;
                    dt.Rows.Add(drow);
                    if (Convert.ToString(address) == "")
                    {
                        address = GR.Cells[1].Text;
                    }
                    else
                    {
                        address = address + "; " + GR.Cells[1].Text;
                    }
                }
                //if (ddlusertype.SelectedItem.Text == "Admin")
                //{
                //    drow["Contactperson"] = GR.Cells[1].Text;
                //    dt.Rows.Add(drow);
                //    if (Convert.ToString(address) == "")
                //    {
                //        address = GR.Cells[1].Text;
                //    }
                //    else
                //    {
                //        address = address + "; " + GR.Cells[1].Text;
                //    }
                //}
            }
        }

        Session["to"] = dt;

        if (Session["to"] != null)
        {
            lblAddresses.Text = address.ToString();
        }
        ModalPopupExtender1.Hide();
        imgbtnsend.Enabled = true;


    }

    protected void grdPartyList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (grdPartyList.Rows.Count > 0)
            {
                CheckBox cbHeader = (CheckBox)grdPartyList.HeaderRow.FindControl("HeaderChkbox");
                cbHeader.Attributes["onclick"] = "ChangeAllCheckBoxStates(this.checked);";
                List<string> ArrayValues = new List<string>();
                ArrayValues.Add(string.Concat("'", cbHeader.ClientID, "'"));
                foreach (GridViewRow gvr in grdPartyList.Rows)
                {
                    CheckBox cb = (CheckBox)gvr.FindControl("chkParty");
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
    protected void ImgBtnAddress_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ddlstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillPartyGrid();
    }
    protected void lblAddresses_TextChanged(object sender, EventArgs e)
    {

    }
    protected void grdPartyList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPartyList.PageIndex = e.NewPageIndex;

        FillPartyGrid();
        ModalPopupExtender1.Show();
    }
    protected void HeaderChkbox_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)grdPartyList.HeaderRow.Cells[0].Controls[1];
        for (int i = 0; i < grdPartyList.Rows.Count; i++)
        {
            CheckBox ch = (CheckBox)grdPartyList.Rows[i].Cells[0].Controls[1];
            ch.Checked = chk.Checked;
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        String filename = "";
        PnlFileAttachLbl.Visible = true;
        if (fileuploadadattachment.HasFile)
        {
            filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "@" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + fileuploadadattachment.FileName;
            fileuploadadattachment.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["Comid"] + "\\UploadedDocuments\\") + filename);
            fileuploadadattachment.PostedFile.SaveAs(Server.MapPath("~\\Account\\" + Session["Comid"] + "\\UploadedDocumentsTemp\\") + filename);
            hdnFileName.Value = filename;
            DataTable dt = new DataTable();
            if (Session["GridFileAttach1"] == null)
            {
                DataColumn dtcom2 = new DataColumn();
                dtcom2.DataType = System.Type.GetType("System.String");
                dtcom2.ColumnName = "FileName";
                dtcom2.ReadOnly = false;
                dtcom2.Unique = false;
                dtcom2.AllowDBNull = true;
                dt.Columns.Add(dtcom2);
                DataColumn dtcom3 = new DataColumn();
                dtcom3.DataType = System.Type.GetType("System.String");
                dtcom3.ColumnName = "FileNameChanged";
                dtcom3.ReadOnly = false;
                dtcom3.Unique = false;
                dtcom3.AllowDBNull = true;
                dt.Columns.Add(dtcom3);
            }
            else
            {
                dt = (DataTable)Session["GridFileAttach1"];
            }
            DataRow dtrow = dt.NewRow();
            dtrow["FileName"] = fileuploadadattachment.FileName.ToString();
            dtrow["FileNameChanged"] = hdnFileName.Value;
            dt.Rows.Add(dtrow);
            Session["GridFileAttach1"] = dt;
            gridFileAttach.DataSource = dt;
            gridFileAttach.DataBind();
        }
        else
        {
            pnlmsg.Visible = true;
            lblmsg.Text = "Please Attach File to Upload.";
            return;
        }
        setGridisze();
    }

    protected void fillusertype()
    {
        ddlusertype.Items.Clear();

        string emprole = "select PartyMasterCategoryNo,Name from [PartyMasterCategory] order by Name";
        //select PartyTypeId,PartType from [PartytTypeMaster] where compid='1133' and PartType in ('Admin','Candidate','Customer','Employee','Other','Vendor') order by PartType
        SqlCommand cmdrole = new SqlCommand(emprole, con);
        SqlDataAdapter darole = new SqlDataAdapter(cmdrole);
        DataTable dtrole = new DataTable();
        darole.Fill(dtrole);

        ddlusertype.DataSource = dtrole;
        ddlusertype.DataTextField = "Name";
        ddlusertype.DataValueField = "PartyMasterCategoryNo";
        ddlusertype.DataBind();

        ddlusertype.SelectedIndex = ddlusertype.Items.IndexOf(ddlusertype.Items.FindByText("Employee"));
    }
    protected void ddlusertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillcompanyname();
        filljobapplied();
        FillPartyGrid();
        ModalPopupExtender1.Show();
    }
    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {
        FillPartyGrid();
        ModalPopupExtender1.Show();
    }
    protected void chkattach_CheckedChanged(object sender, EventArgs e)
    {
        if (chkattach.Checked == true)
        {
            pblattach.Visible = true;
        }
        if (chkattach.Checked == false)
        {
            pblattach.Visible = false;
        }
    }
    protected void ddlcompname_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillPartyGrid();
        ModalPopupExtender1.Show();
    }
    protected void ddlcandi_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillPartyGrid();
        ModalPopupExtender1.Show();
    }

    protected void sugnature()
    {
        SqlDataAdapter da = new SqlDataAdapter("select signature from EmailSignatureMaster inner join InOutCompanyEmail on InOutCompanyEmail.ID=EmailSignatureMaster.InoutgoingMasterId where InOutCompanyEmail.EmployeeID='" + Session["EmployeeId"].ToString() + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            if (Convert.ToString(dt.Rows[0]["signature"]) != "")
            {
                //  Label10.Text = dt.Rows[0]["signature"].ToString();             

                string strValues = Convert.ToString(dt.Rows[0]["Signature"]);
                string[] strArray = strValues.Split(':');

                Label10.Text = strArray[0];

                if (strValues.Contains(":"))
                {
                    Label11.Text = strArray[1];
                    Label12.Text = strArray[2];
                    Label13.Text = strArray[3];
                }


                CheckBox1.Checked = true;
            }
            else
            {
                CheckBox1.Checked = false;
            }
        }
        else
        {
            CheckBox1.Checked = false;
        }
    }

    protected void fillphoto()
    {
        SqlDataAdapter da = new SqlDataAdapter("select photo from User_master inner join EmployeeMaster on EmployeeMaster.PartyID=User_master.PartyID where EmployeeMaster.EmployeeMasterID='" + Session["EmployeeId"].ToString() + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        SqlDataAdapter dase = new SqlDataAdapter("select sex from employeemaster where employeemasterid='" + Session["EmployeeId"].ToString() + "'", con);
        DataTable dtse = new DataTable();
        dase.Fill(dtse);

        if (dt.Rows.Count > 0)
        {
            if (Convert.ToString(dt.Rows[0]["photo"]) != "")
            {
                string imag = dt.Rows[0]["photo"].ToString();
                image1.ImageUrl = "~/ShoppingCart/images/" + imag;
                chkpicture.Checked = true;
            }
            else
            {
                chkpicture.Checked = false;
                chkpicture.Enabled = false;
                if (dtse.Rows[0]["sex"].ToString() == "0")
                {
                    image1.ImageUrl = "~/Account/images/Maleprofilepic.png";
                }
                if (dtse.Rows[0]["sex"].ToString() == "1")
                {
                    image1.ImageUrl = "~/Account/images/Femaleprofilepic.png";
                }
            }
        }
        else
        {
            chkpicture.Checked = false;
            chkpicture.Enabled = false;
            if (dtse.Rows[0]["sex"].ToString() == "0")
            {
                image1.ImageUrl = "~/Account/images/Maleprofilepic.png";
            }
            if (dtse.Rows[0]["sex"].ToString() == "1")
            {
                image1.ImageUrl = "~/Account/images/Femaleprofilepic.png";
            }
        }
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        string empsig = "";

        DataTable dtempemail = new DataTable();
        if (CheckBox1.Checked == true)
        {
            Label10.Visible = true;
            Label11.Visible = true;
            Label12.Visible = true;
            Label13.Visible = true;

            dtempemail = select("  Select Signature from InOutCompanyEmail inner join EmailSignatureMaster on EmailSignatureMaster.InoutgoingMasterId=InOutCompanyEmail.ID where EmployeeID='" + Session["EmployeeIdep"] + "'");
            if (dtempemail.Rows.Count == 0)
            {
                dtempemail = select("  Select Signature from InOutCompanyEmail inner join EmailSignatureMaster on EmailSignatureMaster.InoutgoingMasterId=InOutCompanyEmail.ID where Whid='" + ddlstore.SelectedValue + "'");

            }
            if (dtempemail.Rows.Count > 0)
            {
                if (dtempemail.Rows[0]["Signature"] != System.DBNull.Value)
                {
                    empsig = dtempemail.Rows[0]["Signature"].ToString();

                    //  TxtMsgDetail.Text = empsig;
                }
            }
        }
        else if (CheckBox1.Checked == false)
        {
            Label10.Visible = false;
            Label11.Visible = false;
            Label12.Visible = false;
            Label13.Visible = false;
            empsig = "";
            // TxtMsgDetail.Text = empsig;
        }
    }
    protected void chkpicture_CheckedChanged(object sender, EventArgs e)
    {
        if (chkpicture.Checked == true)
        {
            image1.Visible = true;
        }
        if (chkpicture.Checked == false)
        {
            image1.Visible = false;
        }
    }
    protected void linkAddCabinet_Click(object sender, EventArgs e)
    {
        DataTable dtempemail = new DataTable();
        string upsig = "";
        //  dtempemail = clsMaster.SelectEmpEmail(Convert.ToInt32( Session["EmployeeIdep"]));
        dtempemail = select("Select Signature,InOutCompanyEmail.Id from InOutCompanyEmail inner join EmailSignatureMaster on EmailSignatureMaster.InoutgoingMasterId=InOutCompanyEmail.ID where EmployeeID='" + Session["EmployeeId"] + "'");
        if (dtempemail.Rows.Count == 0)
        {
            dtempemail = select("Select Signature,InOutCompanyEmail.Id from InOutCompanyEmail inner join EmailSignatureMaster on EmailSignatureMaster.InoutgoingMasterId=InOutCompanyEmail.ID where Whid='" + ddlstore.SelectedValue + "'");

        }
        if (dtempemail.Rows.Count > 0)
        {
            if (dtempemail.Rows[0]["Signature"] != System.DBNull.Value)
            {
                upsig = dtempemail.Rows[0]["Signature"].ToString();
                ViewState["sigid"] = dtempemail.Rows[0]["Id"].ToString();


                string strValues = upsig;
                string[] strArray = strValues.Split(':');

                TextBox1.Text = strArray[0];

                if (strValues.Contains(":"))
                {
                    TextBox2.Text = strArray[1];
                    TextBox3.Text = strArray[2];
                    TextBox4.Text = strArray[3];
                }

            }
        }
        ModalPopupExtender9.Show();
    }
    protected void ibtnCancelCabinetAdd_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender9.Hide();
    }
    protected void imgbtnsubmitCabinetAdd_Click(object sender, EventArgs e)
    {
        if (ViewState["sigid"] == null)
        {
            ModalPopupExtender9.Hide();
        }
        else
        {
            SqlCommand cmdu = new SqlCommand("update EmailSignatureMaster set Signature='" + TextBox1.Text + ':' + TextBox2.Text + ':' + TextBox3.Text + ':' + TextBox4.Text + "' where InoutgoingMasterId='" + ViewState["sigid"].ToString() + "'", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdu.ExecuteNonQuery();
            con.Close();

            // bool upsignature = clsDocument.UpdateEmailSignatureMaster(ViewState["sigid"].ToString(), TextBox1.Text);
            sugnature();
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            ModalPopupExtender9.Hide();
        }
    }
}
